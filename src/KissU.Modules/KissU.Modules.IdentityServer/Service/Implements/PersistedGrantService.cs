﻿using System.Linq;
using KissU.IModuleServices.IdentityServer.Abstractions;
using KissU.IModuleServices.IdentityServer.Dtos;
using KissU.IModuleServices.IdentityServer.Queries;
using KissU.Modules.IdentityServer.Data.UnitOfWorks;
using KissU.Modules.IdentityServer.Domain.Models.PersistedGrantAggregate;
using KissU.Modules.IdentityServer.Domain.Repositories;
using Util.Applications;
using Util.Datas.Queries;
using Util.Domains.Repositories;

namespace KissU.Modules.IdentityServer.Service.Implements
{
    /// <summary>
    /// 认证操作数据服务
    /// </summary>
    public class PersistedGrantService : CrudServiceBase<PersistedGrant, PersistedGrantDto, PersistedGrantQuery>, IPersistedGrantService
    {
        /// <summary>
        /// 初始化认证操作数据服务
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="persistedGrantRepository">认证操作数据仓储</param>
        public PersistedGrantService(IIdentityServerUnitOfWork unitOfWork, IPersistedGrantRepository persistedGrantRepository)
            : base(unitOfWork, persistedGrantRepository)
        {
            PersistedGrantRepository = persistedGrantRepository;
            UnitOfWork = unitOfWork;
        }
        /// <summary>
        /// 认证操作数据仓储
        /// </summary>
        public IPersistedGrantRepository PersistedGrantRepository { get; set; }
        /// <summary>
        /// 工作单元
        /// </summary>
        public IIdentityServerUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <param name="param">认证操作数据查询实体</param>
        protected override IQueryBase<PersistedGrant> CreateQuery(PersistedGrantQuery param)
        {
            var query = new Query<PersistedGrant>(param);

            if (string.IsNullOrWhiteSpace(param.Order))
            {
                query.OrderBy(x => x.CreationTime, true);
            }

            return query;
        }

        /// <summary>
        /// 过滤
        /// </summary>
        protected override IQueryable<PersistedGrant> Filter(IQueryable<PersistedGrant> queryable, PersistedGrantQuery parameter)
        {
            return base.Filter(queryable,parameter);
        }
    }
}