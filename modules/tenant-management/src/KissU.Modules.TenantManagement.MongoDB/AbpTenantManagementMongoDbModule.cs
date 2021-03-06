﻿using KissU.Modules.TenantManagement.Domain;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace KissU.Modules.TenantManagement.MongoDB
{
    [DependsOn(
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class AbpTenantManagementMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<TenantManagementMongoDbContext>(options =>
            {
                options.AddDefaultRepositories<ITenantManagementMongoDbContext>();

                options.AddRepository<Tenant, MongoTenantRepository>();
            });
        }
    }
}
