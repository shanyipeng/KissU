﻿using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace KissU.Modules.TenantManagement.DbMigrations.Data
{
    /* This is used if database provider does't define
     * IIdentityDbSchemaMigrator implementation.
     */
    public class NullDbSchemaMigrator : IDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}