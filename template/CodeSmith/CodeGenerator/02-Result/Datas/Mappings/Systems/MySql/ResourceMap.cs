﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GreatWall.Systems.Domain.Models;

namespace GreatWall.Data.Mappings.Systems.MySql {
    /// <summary>
    /// 资源映射配置
    /// </summary>
    public class ResourceMap : Util.Datas.Ef.MySql.AggregateRootMap<Resource> {
        /// <summary>
        /// 映射表
        /// </summary>
        protected override void MapTable( EntityTypeBuilder<Resource> builder ) {
            builder.ToTable( "Systems.Resource" );
        }
        
        /// <summary>
        /// 映射属性
        /// </summary>
        protected override void MapProperties( EntityTypeBuilder<Resource> builder ) {
            //资源标识
            builder.Property(t => t.Id)
                .HasColumnName("ResourceId");
            builder.HasQueryFilter( t => t.IsDeleted == false );
            builder.Property( t => t.Path ).HasColumnName( "Path" );
            builder.Property( t => t.Level ).HasColumnName( "Level" );
        }
    }
}