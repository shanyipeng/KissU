﻿using KissU.Modules.TenantManagement.Application;
using KissU.Modules.TenantManagement.EntityFrameworkCore;
using KissU.Modules.TenantManagement.Service.Contracts;
using KissU.Abp.Business;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace KissU.Modules.TenantManagement.Service
{
    [DependsOn(
        typeof(TenantManagementServiceContractsModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule)
    )]
    public class TenantManagementServiceModule : Volo.Abp.Modularity.AbpModule, IAbpStartupModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            context.Services.AddAlwaysAllowAuthorization();
        }
    }
}