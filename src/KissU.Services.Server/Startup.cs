﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.EventBusRabbitMQ.Configurations;
using Util.Helpers;

namespace KissU.Services.Server
{
    /// <summary>
    ///  启动配置
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///  初始化启动配置
        /// </summary>
        /// <param name="configuration">配置</param>
        public Startup(IConfigurationBuilder build)
        {
            ConfigureEventBus(build);
            ConfigureCache(build);
        }

        /// <summary>
        ///  配置服务
        /// </summary>
        public IContainer ConfigureServices(ContainerBuilder builder)
        {
            var services = new ServiceCollection();
            ConfigureLogging(services);
            services.AddUtil(builder);
            builder.Populate(services);
            var container = builder.Build();
            Ioc.Register(container);
            ServiceLocator.Current = container;
            return ServiceLocator.Current;
        }

        /// <summary>
        ///  配置应用
        /// </summary>
        public void Configure(IContainer app)
        {
        }

        #region 私有方法

        /// <summary>
        ///  配置日志服务
        /// </summary>
        /// <param name="services">服务集合</param>
        private void ConfigureLogging(IServiceCollection services)
        {
            //services.AddLogging();
        }

        /// <summary>
        ///  配置事件总线
        /// </summary>
        /// <param name="build">服务构建者</param>
        private static void ConfigureEventBus(IConfigurationBuilder build)
        {
            build.AddEventBusFile("eventBusSettings.json", false);
        }

        /// <summary>
        ///  配置缓存服务
        /// </summary>
        private void ConfigureCache(IConfigurationBuilder build)
        {
            //build.AddCacheFile("cacheSettings.json", false);
        }

        #endregion
    }
}