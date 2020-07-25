﻿using KissU.Abp.Business;
using KissU.Modules.Application;
using KissU.Services.Configuration.Contract;
using Volo.Abp.Modularity;

namespace KissU.Modules.Configuration.Service
{
    [DependsOn(
        typeof(ConfigurationServiceContractsModule),
        typeof(ConfigurationApplicationModule)
    )]
    public class ConfigurationServiceModule : AbpModule, IAbpStartupModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}