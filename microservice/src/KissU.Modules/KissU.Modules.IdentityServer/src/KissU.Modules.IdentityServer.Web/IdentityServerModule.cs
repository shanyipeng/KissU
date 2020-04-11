﻿using System;
using Autofac.Extensions.DependencyInjection;
using KissU.Core.Module;
using KissU.Surging.KestrelHttpServer;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace KissU.Modules.IdentityServer.Web
{
    /// <summary>
    /// 扩展系统模块
    /// </summary>
    public class IdentityServerModule : KestrelHttpModule
    {
        private IAbpApplicationWithExternalServiceProvider application = null;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="moduleContext">应用模块上下文</param>
        public override void Initialize(AppModuleContext moduleContext)
        {
            base.Initialize(moduleContext);
            var serviceProvoider = moduleContext.ServiceProvoider.GetInstances<IServiceProvider>();
            application.Initialize(serviceProvoider);
        }

        /// <summary>
        /// 注册第三方组件
        /// </summary>
        /// <param name="builder">容器构建器</param>
        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            base.RegisterBuilder(builder);
            var services = new ServiceCollection();
            services.AddObjectAccessor<BookStoreIdentityServerModule>();
            application = AbpApplicationFactory.Create<BookStoreIdentityServerModule>(services);
            builder.ContainerBuilder.Populate(services);
        }
    }
}