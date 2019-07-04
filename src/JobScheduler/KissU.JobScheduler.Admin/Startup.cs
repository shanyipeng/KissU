﻿using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Util.Datas.Ef;
using Util.Logs.Extensions;
using KissU.JobScheduler.Data;
using Util.Ui.Extensions;
using Util.Webs.Extensions;
using Util;
using Microsoft.AspNetCore.Hosting;
using KissU.JobScheduler.Data.UnitOfWorks.SqlServer;

namespace KissU.JobScheduler.Admin
{
    /// <summary>
    /// 启动配置
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 初始化启动配置
        /// </summary>
        /// <param name="env">环境</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();
            Environment = env;
        }

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 环境
        /// </summary>
        public IHostingEnvironment Environment { get; }

        /// <summary>
        /// 配置服务
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //注册Razor视图解析路径
            services.AddRazorViewLocationExpander();

            //添加Mvc服务
            services.AddMvc(options => {
                //options.Filters.Add( new AutoValidateAntiforgeryTokenAttribute() );
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
               .AddRazorPageConventions();

            //添加NLog日志操作
            services.AddNLog();

            //添加EF工作单元
            //====== 支持Sql Server 2012+ ==========
            services.AddUnitOfWork<IJobSchedulerUnitOfWork, JobSchedulerUnitOfWork>(Configuration.GetConnectionString("DefaultConnection"));
            //======= 支持Sql Server 2005+ ==========
            //services.AddUnitOfWork<ISampleUnitOfWork, KissU.JobScheduler.Data.UnitOfWorks.SqlServer.SampleUnitOfWork>( builder => {
            //    builder.UseSqlServer( Configuration.GetConnectionString( "DefaultConnection" ), option => option.UseRowNumberForPaging() );
            //} );
            //======= 支持PgSql =======
            //services.AddUnitOfWork<ISampleUnitOfWork, KissU.JobScheduler.Data.UnitOfWorks.PgSql.SampleUnitOfWork>( Configuration.GetConnectionString( "PgSqlConnection" ) );
            //======= 支持MySql =======
            //services.AddUnitOfWork<ISampleUnitOfWork, KissU.JobScheduler.Data.UnitOfWorks.MySql.SampleUnitOfWork>( Configuration.GetConnectionString( "MySqlConnection" ) );

            //添加Swagger
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new Info { Title = "KissU", Version = "v1" });
                //options.IncludeXmlComments( Path.Combine( AppContext.BaseDirectory, "Util.xml" ) );
                //options.IncludeXmlComments( Path.Combine( AppContext.BaseDirectory, "Util.Webs.xml" ) );
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "KissU.JobScheduler.Admin.xml"));
            });

            //添加Util基础设施服务
            return services.AddUtil();
        }

        /// <summary>
        /// 配置开发环境请求管道
        /// </summary>
        public void ConfigureDevelopment(IApplicationBuilder app)
        {
            app.UseBrowserLink();
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
            {
                HotModuleReplacement = true
            });
            app.UseSwaggerX();
            CommonConfig(app);
        }

        /// <summary>
        /// 配置生产环境请求管道
        /// </summary>
        public void ConfigureProduction(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/Home/Error");
            CommonConfig(app);
        }

        /// <summary>
        /// 公共配置
        /// </summary>
        private void CommonConfig(IApplicationBuilder app)
        {
            app.UseErrorLog();
            app.UseStaticFiles();
            app.UseAuthentication();
            ConfigRoute(app);
        }

        /// <summary>
        /// 路由配置,支持区域
        /// </summary>
        private void ConfigRoute(IApplicationBuilder app)
        {
            app.UseMvc(routes => {
                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });
        }
    }
}