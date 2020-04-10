﻿using Autofac;
using KissU.Core.Datas.Transactions;
using KissU.Core.Datas.UnitOfWorks;
using KissU.Core.Dependency;
using KissU.Core.Sessions;
using KissU.Util.Dapper;
using KissU.Util.Dapper.SqlServer;
using KissU.Util.Datas.Tests.Integration.Commons.Domains.Repositories;
using KissU.Util.Datas.Tests.Integration.Ef.SqlServer.Repositories;
using KissU.Util.Datas.Tests.Integration.Ef.SqlServer.Stores;
using KissU.Util.Datas.Tests.Integration.Ef.SqlServer.UnitOfWorks;
using KissU.Util.Ddd.Domain.Datas.Sql;
using KissU.Util.Ddd.Domain.Datas.Sql.Matedatas;

namespace KissU.Util.Datas.Tests.Integration.Commons.Datas.SqlServer.Configs
{
    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public class IocConfig : ConfigBase
    {
        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            LoadInfrastructure(builder);
            LoadRepositories(builder);
        }

        /// <summary>
        /// 加载基础设施
        /// </summary>
        private void LoadInfrastructure(ContainerBuilder builder)
        {
            builder.AddSingleton<ISession>(new Session(AppConfig.UserId));
            builder.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            builder.AddScoped<ITransactionActionManager, TransactionActionManager>();
            builder.RegisterType<SqlServerUnitOfWork>().AsSelf().InstancePerLifetimeScope().PropertiesAutowired();
            builder.Register(t => t.Resolve<SqlServerUnitOfWork>()).As<ISqlServerUnitOfWork>()
                .InstancePerLifetimeScope().PropertiesAutowired();
            builder.Register(t => t.Resolve<SqlServerUnitOfWork>()).As<IDatabase>().InstancePerLifetimeScope()
                .PropertiesAutowired();
            builder.Register(t => t.Resolve<SqlServerUnitOfWork>()).As<IEntityMatedata>().InstancePerLifetimeScope()
                .PropertiesAutowired();
            builder.AddScoped<ISqlQuery, SqlQuery>();
            builder.AddScoped<ISqlBuilder, SqlServerBuilder>();
            builder.AddScoped<IProductPoStore, ProductPoStore>();
        }

        /// <summary>
        /// 加载仓储
        /// </summary>
        private void LoadRepositories(ContainerBuilder builder)
        {
            builder.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.AddScoped<IOrderRepository, OrderRepository>();
            builder.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}