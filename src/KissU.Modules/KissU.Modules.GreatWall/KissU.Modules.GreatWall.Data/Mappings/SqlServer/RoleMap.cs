﻿// <copyright file="RoleMap.cs" company="KissU">
// Copyright (c) KissU. All Rights Reserved.
// </copyright>

using KissU.Modules.GreatWall.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KissU.Util.Datas.Ef.SqlServer;

namespace KissU.Modules.GreatWall.Data.Mappings.SqlServer
{
    /// <summary>
    /// 角色映射配置
    /// </summary>
    public class RoleMap : AggregateRootMap<Role>
    {
        /// <summary>
        /// 映射表
        /// </summary>
        protected override void MapTable(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role", "systems");
        }

        /// <summary>
        /// 映射属性
        /// </summary>
        protected override void MapProperties(EntityTypeBuilder<Role> builder)
        {
            builder.Property(t => t.Id).HasColumnName("RoleId");
            builder.Property(t => t.Path).HasColumnName("Path");
            builder.Property(t => t.Level).HasColumnName("Level");
            builder.HasQueryFilter(t => t.IsDeleted == false);
        }
    }
}