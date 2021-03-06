﻿using KissU.Modules.FeatureManagement.DbMigrations.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace KissU.Modules.FeatureManagement.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(EntityFrameworkCoreDbMigrationsModule)
    )]
    public class DbMigratorModule : AbpModule
    {
    }
}