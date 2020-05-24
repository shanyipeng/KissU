﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using KissU.Core.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KissU.QuickStart.Service.Host
{
    /// <summary>
    /// 启动配置
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// 初始化启动配置
        /// </summary>
        public Startup(IConfigurationBuilder build)
        {
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        public IContainer ConfigureServices(ContainerBuilder builder)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            builder.Populate(services);
            ServiceLocator.Current = builder.Build();
            return ServiceLocator.Current;
        }

        /// <summary>
        /// 配置应用
        /// </summary>
        public void Configure(IContainer container)
        {
        }
    }
}