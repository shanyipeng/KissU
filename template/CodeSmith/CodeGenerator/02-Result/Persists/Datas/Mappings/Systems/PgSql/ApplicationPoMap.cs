﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GreatWall.Data.Pos.Systems;

namespace GreatWall.Data.Mappings.Systems.PgSql {
    /// <summary>
    /// 应用程序持久化对象映射配置
    /// </summary>
    public class ApplicationPoMap : Util.Datas.Ef.PgSql.AggregateRootMap<ApplicationPo> {
        /// <summary>
        /// 映射表
        /// </summary>
        protected override void MapTable( EntityTypeBuilder<ApplicationPo> builder ) {
            builder.ToTable( "Application", "Systems" );
        }
        
        /// <summary>
        /// 映射属性
        /// </summary>
        protected override void MapProperties( EntityTypeBuilder<ApplicationPo> builder ) {
            //应用程序标识
            builder.Property(t => t.Id)
                .HasColumnName("ApplicationId");
            builder.HasQueryFilter( t => t.IsDeleted == false );
        }
    }
}