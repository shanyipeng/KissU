﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KissU.Domain.Models;

namespace KissU.Data.Mappings.Systems.MySql {
    /// <summary>
    /// 应用程序映射配置
    /// </summary>
    public class ApplicationMap : Util.Datas.Ef.MySql.AggregateRootMap<Application> {
        /// <summary>
        /// 映射表
        /// </summary>
        protected override void MapTable( EntityTypeBuilder<Application> builder ) {
            builder.ToTable( "Systems.Application" );
        }
        
        /// <summary>
        /// 映射属性
        /// </summary>
        protected override void MapProperties( EntityTypeBuilder<Application> builder ) {
            //应用程序编号
            builder.Property(t => t.Id)
                .HasColumnName("ApplicationId");
            builder.HasQueryFilter( t => t.IsDeleted == false );
        }
    }
}
