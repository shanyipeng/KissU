﻿using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Application.Services;

namespace KissU.Modules.PermissionManagement.Application.Contracts
{
    public interface IPermissionAppService : IApplicationService
    {
        Task<GetPermissionListResultDto> GetAsync([NotNull] string providerName, [NotNull] string providerKey);

        Task UpdateAsync([NotNull] string providerName, [NotNull] string providerKey, UpdatePermissionsDto input);
    }
}