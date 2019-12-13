﻿// <copyright file="Startup.cs" company="KissU">
// Copyright (c) KissU. All Rights Reserved.
// </copyright>

using Autofac;
using Autofac.Extensions.DependencyInjection;
using KissU.Core.Caching.Configurations;
using KissU.Core.CPlatform.Utilities;
using KissU.Core.EventBusKafka.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KissU.Services.Stage
{
    /// <summary>
    /// 启动配置
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 初始化启动配置
        /// </summary>
        public Startup(IConfigurationBuilder build)
        {
            ConfigureEventBus(build);
            ConfigureCache(build);
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        public IContainer ConfigureServices(ContainerBuilder builder)
        {
            var services = new ServiceCollection();
            ConfigureLogging(services);
            builder.Populate(services);
            ServiceLocator.Current = builder.Build();
            return ServiceLocator.Current;
        }

        /// <summary>
        /// 配置应用
        /// </summary>
        public void Configure(IContainer app)
        {
        }

        #region 私有方法

        /// <summary>
        /// 配置日志服务
        /// </summary>
        /// <param name="services">服务集合</param>
        private void ConfigureLogging(IServiceCollection services)
        {
            services.AddLogging();
        }

        /// <summary>
        /// 配置事件总线
        /// </summary>
        /// <param name="build">服务构建者</param>
        private static void ConfigureEventBus(IConfigurationBuilder build)
        {
            build.AddEventBusFile("eventbussettings.json", false);
        }

        /// <summary>
        /// 配置缓存服务
        /// </summary>
        private void ConfigureCache(IConfigurationBuilder build)
        {
            build.AddCacheFile("cachesettings.json", false);
        }

        #endregion
    }
}
