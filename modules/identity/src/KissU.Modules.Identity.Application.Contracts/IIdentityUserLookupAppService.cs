﻿using System;
using System.Threading.Tasks;
using KissU.Modules.Users.Abstractions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace KissU.Modules.Identity.Application.Contracts
{
    public interface IIdentityUserLookupAppService : IApplicationService
    {
        Task<UserData> FindByIdAsync(Guid id);

        Task<UserData> FindByUserNameAsync(string userName);

        Task<ListResultDto<UserData>> SearchAsync(UserLookupSearchInputDto input);
        
        Task<long> GetCountAsync(UserLookupCountInputDto input);
    }
}
