﻿using KissU.Core.Events;
using KissU.Core.Events.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace KissU.Util.Events.Default
{
    /// <summary>
    /// 事件总线扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 注册事件总线服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.TryAddSingleton<IEventHandlerManager, EventHandlerManager>();
            services.TryAddSingleton<ISimpleEventBus, EventBus>();
            services.TryAddSingleton<IEventBus, EventBus>();
            return services;
        }
    }
}