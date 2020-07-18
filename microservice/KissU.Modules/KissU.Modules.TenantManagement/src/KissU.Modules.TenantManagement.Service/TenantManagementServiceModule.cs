﻿using KissU.Abp.Autofac;
using KissU.Modules.TenantManagement.Application;
using KissU.Modules.TenantManagement.EntityFrameworkCore;
using KissU.Services.TenantManagement.Contract;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace KissU.Modules.TenantManagement.Service
{
    [DependsOn(
        typeof(TenantManagementServiceContractsModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpAutofacModule)
    )]
    public class TenantManagementServiceModule : AbpModule
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