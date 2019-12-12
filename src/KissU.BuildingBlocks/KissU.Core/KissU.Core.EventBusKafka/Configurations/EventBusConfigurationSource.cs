﻿using Microsoft.Extensions.Configuration;

namespace KissU.Core.EventBusKafka.Configurations
{
    public class EventBusConfigurationSource : FileConfigurationSource
    {
        public string ConfigurationKeyPrefix { get; set; }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            FileProvider = FileProvider ?? builder.GetFileProvider();
            return new EventBusConfigurationProvider(this);
        }
    }
}