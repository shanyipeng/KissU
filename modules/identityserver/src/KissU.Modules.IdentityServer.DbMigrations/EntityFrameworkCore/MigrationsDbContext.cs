﻿using KissU.Modules.IdentityServer.Domain;
using KissU.Modules.IdentityServer.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace KissU.Modules.IdentityServer.DbMigrations.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See IdentityDbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     */
    [ConnectionStringName(AbpIdentityServerDbProperties.ConnectionStringName)]
    public class MigrationsDbContext : AbpDbContext<MigrationsDbContext>
    {
        public MigrationsDbContext(DbContextOptions<MigrationsDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure your own tables/entities inside the ConfigureIdentity method */

            builder.ConfigureIdentityServer();
        }
    }
}