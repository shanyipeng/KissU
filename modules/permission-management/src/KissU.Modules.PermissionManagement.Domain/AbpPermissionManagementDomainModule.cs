﻿using KissU.Modules.PermissionManagement.Domain.Shared;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace KissU.Modules.PermissionManagement.Domain
{
    [DependsOn(typeof(AbpAuthorizationModule))]
    [DependsOn(typeof(AbpDddDomainModule))]
    [DependsOn(typeof(AbpPermissionManagementDomainSharedModule))]
    [DependsOn(typeof(AbpCachingModule))]
    [DependsOn(typeof(AbpJsonModule))]
    public class AbpPermissionManagementDomainModule : AbpModule
    {
        
    }
}
