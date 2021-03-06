﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KissU.Modules.Identity.Domain.Shared.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;
using Volo.Abp.Threading;

namespace KissU.Modules.Identity.Domain
{
    public class IdentityRoleManager : RoleManager<IdentityRole>, IDomainService
    {
        protected override CancellationToken CancellationToken => CancellationTokenProvider.Token;

        protected IStringLocalizer<IdentityResource> Localizer { get; }
        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        public IdentityRoleManager(
            IdentityRoleStore store,
            IEnumerable<IRoleValidator<IdentityRole>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<IdentityRoleManager> logger,
            IStringLocalizer<IdentityResource> localizer,
            ICancellationTokenProvider cancellationTokenProvider)
            : base(
                  store, 
                  roleValidators, 
                  keyNormalizer, 
                  errors, 
                  logger)
        {
            Localizer = localizer;
            CancellationTokenProvider = cancellationTokenProvider;
        }

        public virtual async Task<IdentityRole> GetByIdAsync(Guid id)
        {
            var role = await Store.FindByIdAsync(id.ToString(), CancellationToken);
            if (role == null)
            {
                throw new EntityNotFoundException(typeof(IdentityRole), id);
            }

            return role;
        }

        public override async Task<IdentityResult> SetRoleNameAsync(IdentityRole role, string name)
        {
            if (role.IsStatic && role.Name != name)
            {
                throw new BusinessException(Localizer["Identity.StaticRoleRenamingErrorMessage"]); // TODO: localize & change exception type
            }

            return await base.SetRoleNameAsync(role, name);
        }

        public override async Task<IdentityResult> DeleteAsync(IdentityRole role)
        {
            if (role.IsStatic)
            {
                throw new BusinessException(Localizer["Identity.StaticRoleDeletionErrorMessage"]); // TODO: localize & change exception type
            }

            return await base.DeleteAsync(role);
        }
    }
}
