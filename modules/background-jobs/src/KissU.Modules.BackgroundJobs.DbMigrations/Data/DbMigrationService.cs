﻿using System.Threading.Tasks;
using KissU.Modules.BackgroundJobs.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace KissU.Modules.BackgroundJobs.DbMigrations.Data
{
    public class DbMigrationService : ITransientDependency
    {
        public ILogger<DbMigrationService> Logger { get; set; }

        private readonly IDataSeeder _dataSeeder;
        private readonly IDbSchemaMigrator _dbSchemaMigrator;

        public DbMigrationService(
            IDataSeeder dataSeeder,
            IDbSchemaMigrator dbSchemaMigrator)
        {
            _dataSeeder = dataSeeder;
            _dbSchemaMigrator = dbSchemaMigrator;

            Logger = NullLogger<DbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation($"Started {BackgroundJobsDbProperties.ConnectionStringName} database migrations...");

            Logger.LogInformation("Migrating database schema...");
            await _dbSchemaMigrator.MigrateAsync();

            Logger.LogInformation("Executing database seed...");
            await _dataSeeder.SeedAsync();

            Logger.LogInformation("Successfully completed database migrations.");
        }
    }
}