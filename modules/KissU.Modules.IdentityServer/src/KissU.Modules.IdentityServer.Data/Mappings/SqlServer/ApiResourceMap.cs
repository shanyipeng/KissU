﻿using KissU.Modules.IdentityServer.Domain.Models;
using KissU.Util.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KissU.Modules.IdentityServer.Data.Mappings.SqlServer
{
    /// <summary>
    /// API资源映射配置
    /// </summary>
    public class ApiResourceMap : AggregateRootMap<ApiResource>
    {
        /// <summary>
        /// 映射表
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void MapTable(EntityTypeBuilder<ApiResource> builder)
        {
            builder.ToTable(DbConstants.DbTablePrefix + "ApiResources",
                DbConstants.DbSchema);
        }

        /// <summary>
        /// 映射属性
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void MapProperties(EntityTypeBuilder<ApiResource> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.DisplayName).HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(1000);

            builder.HasIndex(x => x.Name).IsUnique();
        }

        /// <summary>
        /// 映射导航属性
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void MapAssociations(EntityTypeBuilder<ApiResource> builder)
        {
            builder.OwnsMany(t => t.UserClaims, p =>
            {
                p.WithOwner(x => x.ApiResource);
                p.ToTable(DbConstants.DbTablePrefix + "ApiClaims", DbConstants.DbSchema);
                p.Property(x => x.Type);
            });

            builder.OwnsMany(t => t.Properties, p =>
            {
                p.WithOwner(x => x.ApiResource);
                p.ToTable(DbConstants.DbTablePrefix + "ApiProperties", DbConstants.DbSchema);
                p.Property(x => x.Key).HasMaxLength(250).IsRequired();
                p.Property(x => x.Value).HasMaxLength(2000).IsRequired();
            });

            builder.HasMany(x => x.ApiSecrets).WithOne(x => x.ApiResource).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Scopes).WithOne(x => x.ApiResource).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}