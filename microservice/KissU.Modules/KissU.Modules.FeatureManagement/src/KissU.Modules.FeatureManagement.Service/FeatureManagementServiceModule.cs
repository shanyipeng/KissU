﻿using KissU.Modules.FeatureManagement.Application;
using KissU.Modules.FeatureManagement.EntityFrameworkCore;
using KissU.Modules.FeatureManagement.Service.Contracts;
using KissU.Surging.Abp;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace KissU.Modules.FeatureManagement.Service
{
    [DependsOn(
        typeof(FeatureManagementServiceContractsModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
    public class FeatureManagementServiceModule : Volo.Abp.Modularity.AbpModule, IAbpServiceModule
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