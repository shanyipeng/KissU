﻿using System.Threading.Tasks;
using KissU.Modules.IdentityServer.Domain;
using KissU.Modules.IdentityServer.Domain.IdentityResources;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace KissU.Modules.IdentityServer.DbMigrations.Data
{
    public class DbMigrationService : ITransientDependency
    {
        public ILogger<DbMigrationService> Logger { get; set; }

        private readonly IIdentityResourceDataSeeder _dataSeeder;
        private readonly DbSchemaMigrator _dbSchemaMigrator;

        public DbMigrationService(
            IIdentityResourceDataSeeder dataSeeder,
            DbSchemaMigrator dbSchemaMigrator)
        {
            _dataSeeder = dataSeeder;
            _dbSchemaMigrator = dbSchemaMigrator;

            Logger = NullLogger<DbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation($"Started {AbpIdentityServerDbProperties.ConnectionStringName} database migrations...");

            Logger.LogInformation("Migrating database schema...");
            await _dbSchemaMigrator.MigrateAsync();

            Logger.LogInformation("Executing database seed...");
            await _dataSeeder.CreateStandardResourcesAsync();

            Logger.LogInformation("Successfully completed database migrations.");
        }
    }
}