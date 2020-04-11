﻿using System.Reflection;
using KissU.Core.Reflections;
using KissU.Modules.GreatWall.Domain.UnitOfWorks;
using KissU.Util.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace KissU.Modules.GreatWall.DbMigrator
{
    /// <summary>
    /// 此DbContext仅用于数据库迁移。
    /// </summary>
    public class DesignTimeDbContext : UnitOfWork, IGreatWallUnitOfWork
    {
        /// <summary>
        /// 类型查找器
        /// </summary>
        protected readonly IFind Finder;

        /// <summary>
        /// 初始化工作单元
        /// </summary>
        /// <param name="options">配置项</param>
        /// <param name="finder">类型查找器</param>
        public DesignTimeDbContext(DbContextOptions<DesignTimeDbContext> options, IFind finder = null) : base(options)
        {
            Finder = finder ?? new Finder();
        }

        /// <summary>
        /// 获取定义映射配置的程序集列表
        /// </summary>
        /// <returns>Assembly[].</returns>
        protected override Assembly[] GetAssemblies()
        {
            return Finder.GetAssemblies().ToArray();
        }
    }
}