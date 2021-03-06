﻿using System.Linq;
using KissU.CPlatform.Runtime.Server;
using KissU.Dependency;
using KissU.DotNetty.Mqtt.Internal.Services;

namespace KissU.DotNetty.Mqtt.Internal.Runtime.Implementation
{
    /// <summary>
    /// DefaultMqttBehaviorProvider.
    /// Implements the <see cref="IMqttBehaviorProvider" />
    /// </summary>
    /// <seealso cref="IMqttBehaviorProvider" />
    public class DefaultMqttBehaviorProvider : IMqttBehaviorProvider
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMqttBehaviorProvider" /> class.
        /// </summary>
        /// <param name="serviceEntryProvider">The service entry provider.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public DefaultMqttBehaviorProvider(IServiceEntryProvider serviceEntryProvider,
            CPlatformContainer serviceProvider)
        {
            _serviceEntryProvider = serviceEntryProvider;
            _serviceProvider = serviceProvider;
        }

        #endregion Constructor

        /// <summary>
        /// Gets the MQTT behavior.
        /// </summary>
        /// <returns>MqttBehavior.</returns>
        public MqttBehavior GetMqttBehavior()
        {
            if (_mqttBehavior == null)
            {
                _mqttBehavior = _serviceEntryProvider.GetTypes()
                    .Select(type => _serviceProvider.GetInstances(type) as MqttBehavior).Where(p => p != null)
                    .FirstOrDefault();
            }

            return _mqttBehavior;
        }

        #region Field

        private readonly IServiceEntryProvider _serviceEntryProvider;
        private readonly CPlatformContainer _serviceProvider;
        private MqttBehavior _mqttBehavior;

        #endregion Field
    }
}