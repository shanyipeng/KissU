﻿using Microsoft.Extensions.DependencyInjection;
using KissU.Modules.IdentityServer.Devices;
using KissU.Modules.IdentityServer.Grants;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;
using ApiResource = KissU.Modules.IdentityServer.ApiResources.ApiResource;
using Client = KissU.Modules.IdentityServer.Clients.Client;
using IdentityResource = KissU.Modules.IdentityServer.IdentityResources.IdentityResource;

namespace KissU.Modules.IdentityServer.MongoDB
{
    [DependsOn(
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpMongoDbModule)
    )]
    public class AbpIdentityServerMongoDbModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<IIdentityServerBuilder>(
                builder =>
                {
                    builder.AddAbpStores();
                }
            );
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<AbpIdentityServerMongoDbContext>(options =>
            {
                options.AddRepository<ApiResource, MongoApiResourceRepository>();
                options.AddRepository<IdentityResource, MongoIdentityResourceRepository>();
                options.AddRepository<Client, MongoClientRepository>();
                options.AddRepository<PersistedGrant, MongoPersistentGrantRepository>();
                options.AddRepository<DeviceFlowCodes, MongoDeviceFlowCodesRepository>();
            });
        }
    }
}
