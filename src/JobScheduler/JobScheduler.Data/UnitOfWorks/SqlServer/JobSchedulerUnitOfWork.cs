﻿using System;
using Microsoft.EntityFrameworkCore;
using Util.Datas.UnitOfWorks;

namespace JobScheduler.Data.UnitOfWorks.SqlServer 
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class JobSchedulerUnitOfWork : Util.Datas.Ef.SqlServer.UnitOfWork,IJobSchedulerUnitOfWork 
	{
        /// <summary>
        /// 初始化工作单元
        /// </summary>
        /// <param name="options">配置项</param>
        /// <param name="serviceProvider">服务提供器</param>
        public JobSchedulerUnitOfWork( DbContextOptions options, IServiceProvider serviceProvider ) : base( options, serviceProvider ) 
		{
        }
    }
}