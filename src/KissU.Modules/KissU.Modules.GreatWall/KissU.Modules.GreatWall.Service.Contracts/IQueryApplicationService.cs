﻿using System.Collections.Generic;
using System.Threading.Tasks;
using KissU.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using KissU.Modules.GreatWall.Application.Dtos;
using KissU.Modules.GreatWall.Application.Queries;
using KissU.Util;
using KissU.Util.Applications;
using KissU.Util.Domains.Repositories;

namespace KissU.Modules.GreatWall.Service.Contracts
{
    /// <summary>
    /// 应用程序查询服务
    /// </summary>
    [ServiceBundle("api/{Service}")]
    public interface IQueryApplicationService : IService
    {
        /// <summary>
        /// 通过编号获取
        /// </summary>
        /// <param name="id">实体编号</param>
        [HttpGet(true)]
        Task<ApplicationDto> GetByIdAsync(string id);

        /// <summary>
        /// 通过编号列表获取
        /// </summary>
        /// <param name="ids">用逗号分隔的Id列表，范例："1,2"</param>
        [HttpGet(true)]
        Task<List<ApplicationDto>> GetByIdsAsync(string ids);

        /// <summary>
        /// 获取全部
        /// </summary>
        [HttpGet(true)]
        Task<List<ApplicationDto>> GetAllAsync();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        [HttpGet(true)]
        Task<List<ApplicationDto>> QueryAsync(ApplicationQuery parameter);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        [HttpGet(true)]
        Task<PagerList<ApplicationDto>> PagerQueryAsync(ApplicationQuery parameter);

        /// <summary>
        /// 通过应用程序编码查找
        /// </summary>
        /// <param name="code">应用程序编码</param>
        Task<ApplicationDto> GetByCodeAsync(string code);

        /// <summary>
        /// 是否允许跨域访问
        /// </summary>
        /// <param name="origin">来源</param>
        Task<bool> IsOriginAllowedAsync(string origin);

        /// <summary>
        /// 获取作用域
        /// </summary>
        Task<List<Item>> GetScopes();
    }
}