﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GreatWall.Systems.Domain.Models;

namespace GreatWall.Data.Mappings.Systems.SqlServer {
    /// <summary>
    /// Api许可范围映射配置
    /// </summary>
    public class ApiScopeMap : Util.Datas.Ef.SqlServer.AggregateRootMap<ApiScope> {
        /// <summary>
        /// 映射表
        /// </summary>
        protected override void MapTable( EntityTypeBuilder<ApiScope> builder ) {
            builder.ToTable( "ApiScope", "Systems" );
        }
        
        /// <summary>
        /// 映射属性
        /// </summary>
        protected override void MapProperties( EntityTypeBuilder<ApiScope> builder ) {
            //
            builder.Property(t => t.Id)
                .HasColumnName("ApiScopeId");
            builder.HasQueryFilter( t => t.IsDeleted == false );
        }
    }
}