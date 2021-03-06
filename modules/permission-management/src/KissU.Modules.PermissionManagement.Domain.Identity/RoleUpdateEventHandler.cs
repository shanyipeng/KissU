﻿using System.Threading.Tasks;
using KissU.Modules.Identity.Domain;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace KissU.Modules.PermissionManagement.Domain.Identity
{
    public class RoleUpdateEventHandler :
        ILocalEventHandler<IdentityRoleNameChangedEvent>,
        ITransientDependency
    {
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IPermissionManager PermissionManager { get; }
        protected IPermissionGrantRepository PermissionGrantRepository { get; }

        public RoleUpdateEventHandler(
            IIdentityRoleRepository roleRepository,
            IPermissionManager permissionManager,
            IPermissionGrantRepository permissionGrantRepository)
        {
            RoleRepository = roleRepository;
            PermissionManager = permissionManager;
            PermissionGrantRepository = permissionGrantRepository;
        }

        public virtual async Task HandleEventAsync(IdentityRoleNameChangedEvent eventData)
        {
            var role = await RoleRepository.FindAsync(eventData.IdentityRole.Id, false);
            if (role == null)
            {
                return;
            }

            var permissionGrantsInRole = await PermissionGrantRepository.GetListAsync(RolePermissionValueProvider.ProviderName, eventData.OldName);
            foreach (var permissionGrant in permissionGrantsInRole)
            {
                await PermissionManager.UpdateProviderKeyAsync(permissionGrant, eventData.IdentityRole.Name);
            }
        }
    }
}
