﻿using KissU.Modules.IdentityServer.Application.Contracts.Abstractions;
using KissU.Modules.IdentityServer.Application.Contracts.Dtos;
using KissU.Modules.IdentityServer.Application.Contracts.Queries;
using KissU.Modules.IdentityServer.Domain.Models;
using KissU.Modules.IdentityServer.Domain.Repositories;
using KissU.Modules.IdentityServer.Domain.UnitOfWorks;
using KissU.Util.Ddd.Application;
using KissU.Util.Ddd.Domain.Datas.Queries;
using KissU.Util.Ddd.Domain.Datas.Repositories;

namespace KissU.Modules.IdentityServer.Application.Implements
{
    /// <summary>
    /// 设备流代码服务
    /// </summary>
    public class DeviceFlowCodeAppService :
        DeleteServiceBase<DeviceFlowCode, DeviceFlowCodeDto, DeviceFlowCodeQuery, int>,
        IDeviceFlowCodeAppService
    {
        /// <summary>
        /// 初始化设备流代码服务服务
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="deviceFlowCodeRepository">设备流代码服务仓储</param>
        public DeviceFlowCodeAppService(IIdentityServerUnitOfWork unitOfWork,
            IDeviceFlowCodeRepository deviceFlowCodeRepository)
            : base(unitOfWork, deviceFlowCodeRepository)
        {
            DeviceFlowCodeRepository = deviceFlowCodeRepository;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// 数据仓储
        /// </summary>
        public IDeviceFlowCodeRepository DeviceFlowCodeRepository { get; set; }

        /// <summary>
        /// 工作单元
        /// </summary>
        public IIdentityServerUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <param name="param">查询实体</param>
        /// <returns>IQueryBase&lt;DeviceFlowCode&gt;.</returns>
        protected override IQueryBase<DeviceFlowCode> CreateQuery(DeviceFlowCodeQuery param)
        {
            var query = new Query<DeviceFlowCode>(param);

            if (string.IsNullOrWhiteSpace(param.Order))
            {
                query.OrderBy(x => x.CreationTime, true);
            }

            return query;
        }
    }
}