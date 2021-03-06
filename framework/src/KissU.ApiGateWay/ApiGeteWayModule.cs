﻿using Autofac;
using KissU.Dependency;
using KissU.Modularity;
using KissU.ApiGateWay.Aggregation;
using KissU.ApiGateWay.OAuth;
using KissU.ApiGateWay.OAuth.Implementation;
using KissU.ApiGateWay.ServiceDiscovery;
using KissU.ApiGateWay.ServiceDiscovery.Implementation;
using KissU.CPlatform.Routing;
using KissU.CPlatform.Runtime.Client.HealthChecks;
using KissU.CPlatform.Runtime.Client.HealthChecks.Implementation;
using KissU.ServiceProxy;

namespace KissU.ApiGateWay
{
    /// <summary>
    /// ApiGeteWayModule.
    /// Implements the <see cref="EnginePartModule" />
    /// </summary>
    /// <seealso cref="EnginePartModule" />
    public class ApiGeteWayModule : EnginePartModule
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="builder">构建器包装</param>
        protected override void ConfigureContainer(ContainerBuilderWrapper builder)
        {
            builder.RegisterType<FaultTolerantProvider>().As<IFaultTolerantProvider>().SingleInstance();
            builder.RegisterType<DefaultHealthCheckService>().As<IHealthCheckService>().SingleInstance();
            builder.RegisterType<ServiceDiscoveryProvider>().As<IServiceDiscoveryProvider>().SingleInstance();
            builder.RegisterType<ServiceRegisterProvider>().As<IServiceRegisterProvider>().SingleInstance();
            builder.RegisterType<ServiceSubscribeProvider>().As<IServiceSubscribeProvider>().SingleInstance();
            builder.RegisterType<ServiceCacheProvider>().As<IServiceCacheProvider>().SingleInstance();
            builder.RegisterType<ServicePartProvider>().As<IServicePartProvider>().SingleInstance();

            builder.Register(provider =>
            {
                var serviceProxyProvider = provider.Resolve<IServiceProxyProvider>();
                var serviceRouteProvider = provider.Resolve<IServiceRouteProvider>();
                var serviceProvider = provider.Resolve<CPlatformContainer>();
                return new AuthorizationServerProvider(serviceProxyProvider, serviceRouteProvider, serviceProvider);
            }).As<IAuthorizationServerProvider>().SingleInstance();
        }
    }
}