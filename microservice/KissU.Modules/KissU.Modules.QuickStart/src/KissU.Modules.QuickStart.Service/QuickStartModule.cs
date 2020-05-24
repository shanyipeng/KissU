﻿using System;
using Autofac.Extensions.DependencyInjection;
using KissU.Core.Module;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace KissU.Modules.QuickStart.Service
{
    /// <summary>
    /// 扩展系统模块
    /// </summary>
    public class QuickStartModule : BusinessModule
    {
        private IAbpApplicationWithExternalServiceProvider _application;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="moduleContext">应用模块上下文</param>
        public override void Initialize(AppModuleContext moduleContext)
        {
            base.Initialize(moduleContext);
            var serviceProvider = moduleContext.ServiceProvoider.GetInstances<IServiceProvider>();
            _application.Initialize(serviceProvider);
        }

        /// <summary>
        /// 注册第三方组件
        /// </summary>
        /// <param name="builder">容器构建器</param>
        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            var services = new ServiceCollection();
            services.AddObjectAccessor<AbpQuickStartModule>();
            _application = AbpApplicationFactory.Create<AbpQuickStartModule>(services);
            builder.ContainerBuilder.Populate(_application.Services);
        }
    }
}