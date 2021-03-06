﻿using System.Threading.Tasks;
using KissU.Dependency;
using KissU.Modules.PermissionManagement.Application.Contracts;
using KissU.Modules.PermissionManagement.Service.Contracts;
using KissU.ServiceProxy;

namespace KissU.Modules.PermissionManagement.Service.Implements
{
    [ModuleName(PermissionManagementRemoteServiceConsts.RemoteServiceName)]
    public class PermissionService : ProxyServiceBase, IPermissionService
    {
        private readonly IPermissionAppService _permissionAppService;

        public PermissionService(IPermissionAppService permissionAppService)
        {
            _permissionAppService = permissionAppService;
        }

        public virtual Task<GetPermissionListResultDto> GetAsync(string providerName, string providerKey)
        {
            return _permissionAppService.GetAsync(providerName, providerKey);
        }

        public virtual Task UpdateAsync(string providerName, string providerKey, UpdatePermissionsDto input)
        {
            return _permissionAppService.UpdateAsync(providerName, providerKey, input);
        }
    }
}