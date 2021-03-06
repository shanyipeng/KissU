﻿using System;
using System.Linq;
using System.Threading.Tasks;
using KissU.Modules.Identity.Application.Contracts;
using KissU.Modules.Identity.Domain;
using KissU.Modules.Identity.Domain.Shared;
using KissU.Modules.Users.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace KissU.Modules.Identity.Application
{
    [Authorize(IdentityPermissions.UserLookup.Default)]
    public class IdentityUserLookupAppService : IdentityAppServiceBase, IIdentityUserLookupAppService
    {
        protected IdentityUserRepositoryExternalUserLookupServiceProvider UserLookupServiceProvider { get; }

        public IdentityUserLookupAppService(
            IdentityUserRepositoryExternalUserLookupServiceProvider userLookupServiceProvider)
        {
            UserLookupServiceProvider = userLookupServiceProvider;
        }

        public virtual async Task<UserData> FindByIdAsync(Guid id)
        {
            var userData = await UserLookupServiceProvider.FindByIdAsync(id);
            if (userData == null)
            {
                return null;
            }

            return new UserData(userData);
        }

        public virtual async Task<UserData> FindByUserNameAsync(string userName)
        {
            var userData = await UserLookupServiceProvider.FindByUserNameAsync(userName);
            if (userData == null)
            {
                return null;
            }

            return new UserData(userData);
        }

        public async Task<ListResultDto<UserData>> SearchAsync(UserLookupSearchInputDto input)
        {
            var users = await UserLookupServiceProvider.SearchAsync(
                input.Sorting,
                input.Filter,
                input.MaxResultCount,
                input.SkipCount
            );

            return new ListResultDto<UserData>(
                users
                    .Select(u => new UserData(u))
                    .ToList()
            );
        }

        public async Task<long> GetCountAsync(UserLookupCountInputDto input)
        {
            return await UserLookupServiceProvider.GetCountAsync(input.Filter);
        }
    }
}
