﻿using System.Threading.Tasks;
using KissU.Modules.PermissionManagement.DbMigrations.Data;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace KissU.Modules.PermissionManagement.DbMigrations.EntityFrameworkCore
{
    [Dependency(ReplaceServices = true)]
    public class EntityFrameworkCoreDbSchemaMigrator : DbSchemaMigrator, ITransientDependency
    {
        private readonly MigrationsDbContext _dbContext;

        public EntityFrameworkCoreDbSchemaMigrator(MigrationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}