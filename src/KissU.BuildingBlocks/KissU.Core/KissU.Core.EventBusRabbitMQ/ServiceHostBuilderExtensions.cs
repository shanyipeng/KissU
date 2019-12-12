﻿using Autofac;
using KissU.Core.CPlatform;
using KissU.Core.CPlatform.Engines;
using KissU.Core.CPlatform.EventBus;
using KissU.Core.CPlatform.EventBus.Implementation;
using KissU.Core.CPlatform.Routing;
using KissU.Core.ServiceHosting.Internal;

namespace KissU.Core.EventBusRabbitMQ
{
    public static class ServiceHostBuilderExtensions
    {
        public static IServiceHostBuilder SubscribeAt(this IServiceHostBuilder hostBuilder)
        {
            return hostBuilder.MapServices(mapper =>
            {
                mapper.Resolve<IServiceEngineLifetime>().ServiceEngineStarted.Register(() =>
                {
                      mapper.Resolve<ISubscriptionAdapt>().SubscribeAt();
                    new ServiceRouteWatch(mapper.Resolve<CPlatformContainer>(), () =>
                    {
                        var subscriptionAdapt = mapper.Resolve<ISubscriptionAdapt>();
                        mapper.Resolve<IEventBus>().OnShutdown += (sender, args) =>
                        {
                            subscriptionAdapt.Unsubscribe();
                        };
                        mapper.Resolve<ISubscriptionAdapt>().SubscribeAt();
                    });
                });
            });
        }
    }
}