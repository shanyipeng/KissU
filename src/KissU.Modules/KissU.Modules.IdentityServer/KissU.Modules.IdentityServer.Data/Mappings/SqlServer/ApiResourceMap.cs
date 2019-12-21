﻿// <copyright file="ApiResourceMap.cs" company="KissU">
// Copyright (c) KissU. All Rights Reserved.
// </copyright>

using KissU.Modules.IdentityServer.Domain.Models;
using KissU.Util.Datas.SqlServer.Ef;
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
        protected override void MapTable(EntityTypeBuilder<ApiResource> builder)
        {
            builder.ToTable(Consts.DbTablePrefix + "ApiResources", Consts.DbSchema);
        }

        /// <summary>
        /// 映射属性
        /// </summary>
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
        protected override void MapAssociations(EntityTypeBuilder<ApiResource> builder)
        {
            builder.OwnsMany(t => t.UserClaims, p =>
            {
                p.ToTable(Consts.DbTablePrefix + "ApiClaims", Consts.DbSchema);
                p.Property(x => x.Type);
            });

            builder.OwnsMany(t => t.Properties, p =>
            {
                p.ToTable(Consts.DbTablePrefix + "ApiProperties", Consts.DbSchema);
                p.Property(x => x.Key).HasMaxLength(250).IsRequired();
                p.Property(x => x.Value).HasMaxLength(2000).IsRequired();
            });

            builder.HasMany(x => x.Secrets).WithOne(x => x.ApiResource).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Scopes).WithOne(x => x.ApiResource).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
