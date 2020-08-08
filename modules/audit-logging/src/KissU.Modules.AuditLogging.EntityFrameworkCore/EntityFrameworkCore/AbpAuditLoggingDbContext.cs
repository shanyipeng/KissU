﻿using KissU.Modules.AuditLogging.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace KissU.Modules.AuditLogging.EntityFrameworkCore.EntityFrameworkCore
{
    [ConnectionStringName(AbpAuditLoggingDbProperties.ConnectionStringName)]
    public class AbpAuditLoggingDbContext : AbpDbContext<AbpAuditLoggingDbContext>, IAuditLoggingDbContext
    {
        public DbSet<AuditLog> AuditLogs { get; set; }

        public AbpAuditLoggingDbContext(DbContextOptions<AbpAuditLoggingDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureAuditLogging();
        }
    }
}
