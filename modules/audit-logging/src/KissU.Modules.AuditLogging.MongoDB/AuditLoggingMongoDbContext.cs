﻿using KissU.Modules.AuditLogging.Domain;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace KissU.Modules.AuditLogging.MongoDB
{
    [ConnectionStringName(AbpAuditLoggingDbProperties.ConnectionStringName)]
    public class AuditLoggingMongoDbContext : AbpMongoDbContext, IAuditLoggingMongoDbContext
    {
        public IMongoCollection<AuditLog> AuditLogs => Collection<AuditLog>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureAuditLogging();
        }
    }
}
