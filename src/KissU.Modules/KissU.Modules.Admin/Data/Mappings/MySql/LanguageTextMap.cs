﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KissU.Domain.Systems.Models;

namespace KissU.Data.Mappings.Systems.MySql 
{
    /// <summary>
    /// 语言国际化配置映射配置
    /// </summary>
    public class LanguageTextMap : Util.Datas.Ef.MySql.EntityMap<LanguageDetail> 
	{
        /// <summary>
        /// 映射表
        /// </summary>
        protected override void MapTable( EntityTypeBuilder<LanguageDetail> builder ) 
		{
            builder.ToTable( "Systems.LanguageDetail" );
        }
        
        /// <summary>
        /// 映射属性
        /// </summary>
        protected override void MapProperties( EntityTypeBuilder<LanguageDetail> builder ) 
		{
            //标识
            builder.Property(t => t.Id)
                .HasColumnName("Id");
        }
    }
}
