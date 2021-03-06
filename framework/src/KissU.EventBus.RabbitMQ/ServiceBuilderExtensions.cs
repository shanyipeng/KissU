﻿using System;
using System.Collections.Generic;
using Autofac;
using KissU.EventBus.Events;
using KissU.EventBus.Implementation;
using KissU.EventBus.RabbitMQ.Configurations;
using KissU.EventBus.RabbitMQ.Implementation;
using KissU.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace KissU.EventBus.RabbitMQ
{
    /// <summary>
    /// ContainerBuilderExtensions.
    /// </summary>
    public static class ServiceBuilderExtensions
    {
        /// <summary>
        /// 使用RabbitMQ进行传输。
        /// </summary>
        /// <param name="builder">服务构建者。</param>
        /// <returns>服务构建者。</returns>
        public static IServiceBuilder UseRabbitMQTransport(this IServiceBuilder builder)
        {
            builder.ContainerBuilder.RegisterType(typeof(Implementation.EventBusRabbitMQ)).As(typeof(IEventBus))
                .SingleInstance();
            builder.ContainerBuilder.RegisterType(typeof(DefaultConsumeConfigurator)).As(typeof(IConsumeConfigurator))
                .SingleInstance();
            builder.ContainerBuilder.RegisterType(typeof(InMemoryEventBusSubscriptionsManager))
                .As(typeof(IEventBusSubscriptionsManager)).SingleInstance();
            builder.ContainerBuilder.Register(provider =>
            {
                var logger = provider.Resolve<ILogger<DefaultRabbitMQPersistentConnection>>();
                var option = new EventBusOption();
                var section = CPlatform.AppConfig.GetSection("EventBus");
                if (section.Exists())
                    option = section.Get<EventBusOption>();
                else if (AppConfig.Configuration != null)
                    option = AppConfig.Configuration.Get<EventBusOption>();
                var factory = new ConnectionFactory
                {
                    HostName = option.EventBusConnection,
                    UserName = option.EventBusUserName,
                    Password = option.EventBusPassword,
                    VirtualHost = option.VirtualHost,
                    Port = int.Parse(option.Port)
                };
                factory.RequestedHeartbeat = 60;
                AppConfig.BrokerName = option.BrokerName;
                AppConfig.MessageTTL = option.MessageTTL;
                AppConfig.RetryCount = option.RetryCount;
                AppConfig.FailCount = option.FailCount;
                AppConfig.PrefetchCount = option.PrefetchCount;
                return new DefaultRabbitMQPersistentConnection(factory, logger);
            }).As<IRabbitMQPersistentConnection>();
            return builder;
        }

        /// <summary>
        /// Uses the rabbit mq event adapt.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="adapt">The adapt.</param>
        /// <returns>IServiceBuilder.</returns>
        public static IServiceBuilder UseRabbitMQEventAdapt(this IServiceBuilder builder,
            Func<IServiceProvider, ISubscriptionAdapt> adapt)
        {
            var containerBuilder = builder.ContainerBuilder;
            containerBuilder.RegisterAdapter(adapt);
            return builder;
        }

        /// <summary>
        /// Adds the rabbit mq adapt.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>IServiceBuilder.</returns>
        public static IServiceBuilder AddRabbitMQAdapt(this IServiceBuilder builder)
        {
            return builder.UseRabbitMQEventAdapt(provider =>
                new RabbitMqSubscriptionAdapt(
                    provider.GetService<IConsumeConfigurator>(),
                    provider.GetService<IEnumerable<IIntegrationEventHandler>>()
                )
            );
        }
    }
}