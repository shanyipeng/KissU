﻿using System;
using Autofac;
using KissU.Core.CPlatform;
using KissU.Core.CPlatform.Convertibles;
using KissU.Core.CPlatform.Diagnostics;
using KissU.Core.CPlatform.Runtime.Client;
using KissU.Core.ProxyGenerator.Diagnostics;
using KissU.Core.ProxyGenerator.Implementation;
using KissU.Core.ProxyGenerator.Interceptors;
using KissU.Core.ProxyGenerator.Interceptors.Implementation;

namespace KissU.Core.ProxyGenerator
{
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// 添加客户端代理
        /// </summary>
        /// <param name="builder">服务构建者</param>
        /// <returns>服务构建者。</returns>
        public static IServiceBuilder AddClientProxy(this IServiceBuilder builder)
        {
            var services = builder.Services;
            services.RegisterType<ServiceProxyGenerater>().As<IServiceProxyGenerater>().SingleInstance();
            services.RegisterType<ServiceProxyProvider>().As<IServiceProxyProvider>().SingleInstance();
            builder.Services.Register(provider =>new ServiceProxyFactory(
                 provider.Resolve<IRemoteInvokeService>(),
                 provider.Resolve<ITypeConvertibleService>(),
                 provider.Resolve<IServiceProvider>(),
                 builder.GetInterfaceService(),
                 builder.GetDataContractName()
                 )).As<IServiceProxyFactory>().SingleInstance();
            return builder;
        }

        public static IServiceBuilder AddClientIntercepted(this IServiceBuilder builder,params Type[] interceptorServiceTypes )
        {
            var services = builder.Services; 
            services.RegisterTypes(interceptorServiceTypes).As<IInterceptor>().SingleInstance();
            services.RegisterType<InterceptorProvider>().As<IInterceptorProvider>().SingleInstance();
            return builder;
        }

        public static IServiceBuilder AddRpcTransportDiagnostic(this IServiceBuilder builder)
        {
            var services = builder.Services;
            services.RegisterType<RpcTransportDiagnosticProcessor>().As<ITracingDiagnosticProcessor>().SingleInstance();
            return builder;
        }

        /// <summary>
        /// 添加客户端拦截
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="interceptorServiceType"></param>
        /// <returns>服务构建者</returns>
        public static IServiceBuilder AddClientIntercepted(this IServiceBuilder builder, Type interceptorServiceType)
        {
            var services = builder.Services;
            services.RegisterType(interceptorServiceType).As<IInterceptor>().SingleInstance();
            services.RegisterType<InterceptorProvider>().As<IInterceptorProvider>().SingleInstance();
            return builder;
        }

        public static IServiceBuilder AddClient(this ContainerBuilder services)
        {
            return services
                .AddCoreService()
                .AddClientRuntime()
                .AddClientProxy();
        }

        /// <summary>
        /// 添加关联服务
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>服务构建者</returns>
        public static IServiceBuilder AddRelateService(this IServiceBuilder builder)
        {
            return builder.AddRelateServiceRuntime().AddClientProxy();
        }

        /// <summary>
        /// 添加客户端属性注入
        /// </summary>
        /// <param name="builder">服务构建者</param>
        /// <returns>服务构建者</returns>
        public static IServiceBuilder AddClient(this IServiceBuilder builder)
        {
            return builder
                .RegisterServices()
                .RegisterRepositories()
                .RegisterServiceBus()
                .RegisterModules()
                .RegisterInstanceByConstraint()
                .AddClientRuntime()
                .AddClientProxy();
        }
    }
}