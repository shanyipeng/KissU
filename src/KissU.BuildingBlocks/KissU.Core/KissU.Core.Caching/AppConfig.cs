﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KissU.Core.Caching.DependencyResolution;
using KissU.Core.Caching.HashAlgorithms;
using KissU.Core.Caching.Models;
using KissU.Core.Caching.Utilities;
using Microsoft.Extensions.Configuration;

namespace KissU.Core.Caching
{
    /// <summary>
    /// AppConfig.
    /// </summary>
    public class AppConfig
    {
        private const string CacheSectionName = "CachingProvider";

        /// <summary>
        /// The path
        /// </summary>
        internal static string Path;

        private readonly CachingProvider _cacheWrapperSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfig" /> class.
        /// </summary>
        public AppConfig()
        {
            ServiceResolver.Current.Register(null, Activator.CreateInstance(typeof(HashAlgorithm), new object[] { }));
            _cacheWrapperSetting = Configuration.Get<CachingProvider>();
            RegisterConfigInstance();
            RegisterLocalInstance("ICacheClient`1");
            InitSettingMethod();
        }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        internal static IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// Gets the default instance.
        /// </summary>
        internal static AppConfig DefaultInstance
        {
            get
            {
                var config = ServiceResolver.Current.GetService<AppConfig>();
                if (config == null)
                {
                    config = Activator.CreateInstance(typeof(AppConfig), new object[] { }) as AppConfig;
                    ServiceResolver.Current.Register(null, config);
                }

                return config;
            }
        }

        /// <summary>
        /// Gets the context instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        public T GetContextInstance<T>() where T : class
        {
            var context = ServiceResolver.Current.GetService<T>(typeof(T));
            return context;
        }

        /// <summary>
        /// Gets the context instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns>T.</returns>
        public T GetContextInstance<T>(string name) where T : class
        {
            DebugCheck.NotEmpty(name);
            var context = ServiceResolver.Current.GetService<T>(name);
            return context;
        }

        private void RegisterLocalInstance(string typeName)
        {
            var types = GetType().GetTypeInfo().Assembly.GetTypes()
                .Where(p => p.GetTypeInfo().GetInterface(typeName) != null);
            foreach (var t in types)
            {
                var attribute = t.GetTypeInfo().GetCustomAttribute<IdentifyCacheAttribute>();
                ServiceResolver.Current.Register(attribute.Name.ToString(),
                    Activator.CreateInstance(t));
            }
        }

        private void RegisterConfigInstance()
        {
            var bingingSettings = _cacheWrapperSetting.CachingSettings;
            try
            {
                var types =
                    GetType().GetTypeInfo()
                        .Assembly.GetTypes()
                        .Where(
                            p => p.GetTypeInfo().GetInterface("ICacheProvider") != null);
                foreach (var t in types)
                {
                    foreach (var setting in bingingSettings)
                    {
                        var properties = setting.Properties;
                        var args = properties.Select(p => GetTypedPropertyValue(p)).ToArray();
                        ;
                        var maps =
                            properties.Select(p => p.Maps)
                                .FirstOrDefault(p => p != null && p.Any());
                        var type = Type.GetType(setting.Class, true);
                        if (ServiceResolver.Current.GetService(type, setting.Id) == null)
                            ServiceResolver.Current.Register(setting.Id, Activator.CreateInstance(type, args));
                        if (maps == null) continue;
                        if (!maps.Any()) continue;
                        foreach (
                            var mapsetting in
                            maps.Where(mapsetting =>
                                t.Name.StartsWith(mapsetting.Name, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            ServiceResolver.Current.Register(string.Format("{0}.{1}", setting.Id, mapsetting.Name),
                                Activator.CreateInstance(t, setting.Id));
                        }
                    }

                    var attribute = t.GetTypeInfo().GetCustomAttribute<IdentifyCacheAttribute>();
                    if (attribute != null)
                        ServiceResolver.Current.Register(attribute.Name.ToString(),
                            Activator.CreateInstance(t));
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Gets the typed property value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Object.</returns>
        public object GetTypedPropertyValue(Property obj)
        {
            var mapCollections = obj.Maps;
            if (mapCollections != null && mapCollections.Any())
            {
                var results = new List<object>();
                foreach (var map in mapCollections)
                {
                    object items = null;
                    if (map.Properties != null) items = map.Properties.Select(p => GetTypedPropertyValue(p)).ToArray();
                    results.Add(new
                    {
                        Name = Convert.ChangeType(obj.Name, typeof(string)),
                        Value = Convert.ChangeType(map.Name, typeof(string)),
                        Items = items
                    });
                }

                return results;
            }

            if (!string.IsNullOrEmpty(obj.Value))
            {
                return new
                {
                    Name = Convert.ChangeType(obj.Name ?? "", typeof(string)),
                    Value = Convert.ChangeType(obj.Value, typeof(string))
                };
            }

            if (!string.IsNullOrEmpty(obj.Ref))
                return Convert.ChangeType(obj.Ref, typeof(string));

            return null;
        }

        private void InitSettingMethod()
        {
            var settings =
                _cacheWrapperSetting.CachingSettings
                    .Where(p => !string.IsNullOrEmpty(p.InitMethod));
            foreach (var setting in settings)
            {
                var bindingInstance =
                    ServiceResolver.Current.GetService(Type.GetType(setting.Class, true),
                        setting.Id);
                bindingInstance.GetType().GetMethod(setting.InitMethod, BindingFlags.InvokeMethod)
                    .Invoke(bindingInstance, new object[] { });
            }
        }
    }
}