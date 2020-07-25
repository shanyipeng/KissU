﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using KissU.Abp.Autofac;
using KissU.Helpers;
using KissU.Modularity;
using KissU.Surging.CPlatform;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Volo.Abp;
using Volo.Abp.Modularity.PlugIns;

namespace KissU.Abp.Business
{
    public class AbpBusinessModule : EnginePartModule
    {
        private IAbpApplicationWithExternalServiceProvider _application;
        private static readonly List<Assembly> _referenceAssembly = new List<Assembly>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="moduleContext">应用模块上下文</param>
        public override void Initialize(ModuleInitializationContext moduleContext)
        {
            var serviceProvider = moduleContext.ServiceProvoider.GetInstances<IServiceProvider>();
            _application.Initialize(serviceProvider);
        }

        /// <summary>
        /// 注册第三方组件
        /// </summary>
        /// <param name="builder">容器构建器</param>
        protected override void ConfigureContainer(ContainerBuilderWrapper builder)
        {
            base.ConfigureContainer(builder);
            var services = new ServiceCollection();
            _application = AbpApplicationFactory.Create<AbpStartupModule>(services, options =>
            {
                var referenceAssemblies = GetAssemblies();
                var moduleTypes = ReflectionHelper.FindTypes<IAbpStartupModule>(referenceAssemblies.ToArray());
                options.PlugInSources.AddTypes(moduleTypes.ToArray());
            });
            builder.ContainerBuilder.Populate(_application.Services);
        }

        /// <summary>
        /// 获取程序集.
        /// </summary>
        /// <param name="virtualPaths">The virtual paths.</param>
        /// <returns>List&lt;Assembly&gt;.</returns>
        private static List<Assembly> GetAssemblies(params string[] virtualPaths)
        {
            var referenceAssemblies = new List<Assembly>();
            if (virtualPaths.Any())
            {
                referenceAssemblies = GetReferenceAssembly(virtualPaths);
            }
            else
            {
                var assemblyNames = DependencyContext.Default.GetDefaultAssemblyNames().Select(p => p.Name).ToArray();
                assemblyNames = GetFilterAssemblies(assemblyNames);
                foreach (var name in assemblyNames)
                {
                    referenceAssemblies.Add(Assembly.Load(name));
                }

                _referenceAssembly.AddRange(referenceAssemblies.Except(_referenceAssembly));
            }

            return referenceAssemblies;
        }

        /// <summary>
        /// 获取引用程序集.
        /// </summary>
        /// <param name="virtualPaths">The virtual paths.</param>
        /// <returns>List&lt;Assembly&gt;.</returns>
        private static List<Assembly> GetReferenceAssembly(params string[] virtualPaths)
        {
            var refAssemblies = new List<Assembly>();
            var rootPath = AppContext.BaseDirectory;
            var existsPath = virtualPaths.Any();
            if (existsPath && !string.IsNullOrEmpty(AppConfig.ServerOptions.RootPath))
            {
                rootPath = AppConfig.ServerOptions.RootPath;
            }

            var result = _referenceAssembly;
            if (!result.Any() || existsPath)
            {
                var paths = virtualPaths.Select(m => Path.Combine(rootPath, m)).ToList();
                if (!existsPath)
                {
                    paths.Add(rootPath);
                }

                paths.ForEach(path =>
                {
                    var assemblyFiles = GetAllAssemblyFiles(path);

                    foreach (var referencedAssemblyFile in assemblyFiles)
                    {
                        var referencedAssembly = Assembly.LoadFrom(referencedAssemblyFile);
                        if (!_referenceAssembly.Contains(referencedAssembly))
                        {
                            _referenceAssembly.Add(referencedAssembly);
                        }

                        refAssemblies.Add(referencedAssembly);
                    }

                    result = existsPath ? refAssemblies : _referenceAssembly;
                });
            }

            return result;
        }

        /// <summary>
        /// 获取过滤器程序集.
        /// </summary>
        /// <param name="assemblyNames">The assembly names.</param>
        /// <returns>System.String[].</returns>
        private static string[] GetFilterAssemblies(string[] assemblyNames)
        {
            var notRelatedFile = AppConfig.ServerOptions.NotRelatedAssemblyFiles;
            var relatedFile = AppConfig.ServerOptions.RelatedAssemblyFiles;
            var pattern = string.Format(
                "^Microsoft.\\w*|^System.\\w*|^DotNetty.\\w*|^runtime.\\w*|^ZooKeeperNetEx\\w*|^StackExchange.Redis\\w*|^Consul\\w*|^Newtonsoft.Json.\\w*|^Autofac.\\w*{0}",
                string.IsNullOrEmpty(notRelatedFile) ? string.Empty : $"|{notRelatedFile}");
            var notRelatedRegex = new Regex(pattern,
                RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var relatedRegex = new Regex(relatedFile,
                RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (!string.IsNullOrEmpty(relatedFile))
            {
                return
                    assemblyNames.Where(
                        name => !notRelatedRegex.IsMatch(name) && relatedRegex.IsMatch(name)).ToArray();
            }

            return
                assemblyNames.Where(
                    name => !notRelatedRegex.IsMatch(name)).ToArray();
        }

        /// <summary>
        /// 获取所有程序集.
        /// </summary>
        /// <param name="parentDir">The parent dir.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        private static List<string> GetAllAssemblyFiles(string parentDir)
        {
            var notRelatedFile = AppConfig.ServerOptions.NotRelatedAssemblyFiles;
            var relatedFile = AppConfig.ServerOptions.RelatedAssemblyFiles;
            var pattern =
                $"^Microsoft.\\w*|^System.\\w*|^Netty.\\w*|^Autofac.\\w*{(string.IsNullOrEmpty(notRelatedFile) ? string.Empty : $"|{notRelatedFile}")}";
            var notRelatedRegex = new Regex(pattern,
                RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var relatedRegex = new Regex(relatedFile,
                RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (!string.IsNullOrEmpty(relatedFile))
            {
                return Directory.GetFiles(parentDir, "*.dll").Select(Path.GetFullPath)
                    .Where(a => !notRelatedRegex.IsMatch(a) && relatedRegex.IsMatch(a)).ToList();
            }

            return Directory.GetFiles(parentDir, "*.dll").Select(Path.GetFullPath)
                .Where(a => !notRelatedRegex.IsMatch(Path.GetFileName(a))).ToList();
        }
    }
}