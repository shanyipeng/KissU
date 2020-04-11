﻿using System.Collections.Generic;
using System.Threading.Tasks;
using KissU.Core.Datas;
using KissU.Modules.IdentityServer.Application.Contracts.Abstractions;
using KissU.Modules.IdentityServer.Application.Contracts.Dtos;
using KissU.Modules.IdentityServer.Application.Contracts.Dtos.Requests;
using KissU.Modules.IdentityServer.Application.Contracts.Queries;
using KissU.Modules.IdentityServer.Service.Contracts;
using KissU.Surging.ProxyGenerator;

namespace KissU.Modules.IdentityServer.Service.Implements
{
    /// <summary>
    /// 应用程序服务
    /// </summary>
    public class ClientService : ProxyServiceBase, IClientService
    {
        private readonly IClientAppService _appService;

        /// <summary>
        /// 初始化应用服务
        /// </summary>
        /// <param name="appService">应用服务</param>
        public ClientService(IClientAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// 通过标识获取
        /// </summary>
        /// <param name="id">实体编号</param>
        /// <returns>Task&lt;ClientDto&gt;.</returns>
        public async Task<ClientDto> GetByIdAsync(int id)
        {
            return await _appService.GetByIdAsync(id);
        }

        /// <summary>
        /// 通过标识列表获取
        /// </summary>
        /// <param name="ids">用逗号分隔的Id列表，范例："1,2"</param>
        /// <returns>Task&lt;List&lt;ClientDto&gt;&gt;.</returns>
        public async Task<List<ClientDto>> GetByIdsAsync(string ids)
        {
            return await _appService.GetByIdsAsync(ids);
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns>Task&lt;List&lt;ClientDto&gt;&gt;.</returns>
        public async Task<List<ClientDto>> GetAllAsync()
        {
            return await _appService.GetAllAsync();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns>Task&lt;List&lt;ClientDto&gt;&gt;.</returns>
        public async Task<List<ClientDto>> QueryAsync(ClientQuery parameter)
        {
            return await _appService.QueryAsync(parameter);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns>Task&lt;PagerList&lt;ClientDto&gt;&gt;.</returns>
        public async Task<PagerList<ClientDto>> PagerQueryAsync(ClientQuery parameter)
        {
            return await _appService.PagerQueryAsync(parameter);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="request">创建参数</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> CreateAsync(ClientCreateRequest request)
        {
            return await _appService.CreateAsync(request);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request">修改参数</param>
        public async Task UpdateAsync(ClientDto request)
        {
            await _appService.UpdateAsync(request);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用逗号分隔的Id列表，范例："1,2"</param>
        public async Task DeleteAsync(string ids)
        {
            await _appService.DeleteAsync(ids);
        }

        /// <summary>
        /// 克隆应用程序
        /// </summary>
        /// <param name="request">克隆请求参数</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public async Task<int> CloneAsync(ClientCloneRequest request)
        {
            return await _appService.CloneAsync(request);
        }

        /// <summary>
        /// 通过编码查找
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>Task&lt;ClientDto&gt;.</returns>
        public async Task<ClientDto> FindEnabledByIdAsync(string clientId)
        {
            return await _appService.FindEnabledByIdAsync(clientId);
        }

        #region 应用程序声明

        /// <summary>
        /// 获取应用程序声明
        /// </summary>
        /// <param name="clientId">应用程序编号</param>
        /// <returns>Task&lt;List&lt;ClientClaimDto&gt;&gt;.</returns>
        public async Task<List<ClientClaimDto>> GetClaimsAsync(int clientId)
        {
            return await _appService.GetClaimsAsync(clientId);
        }

        /// <summary>
        /// 更新应用程序声明
        /// </summary>
        /// <param name="clientId">应用标识</param>
        /// <param name="dto">应用程序声明</param>
        public async Task UpdateClaimAsync(int clientId, ClientClaimDto dto)
        {
            await _appService.UpdateClaimAsync(clientId, dto);
        }

        /// <summary>
        /// 获取应用程序声明
        /// </summary>
        /// <param name="id">应用程序声明编号</param>
        /// <returns>Task&lt;ClientClaimDto&gt;.</returns>
        public async Task<ClientClaimDto> GetClaimAsync(int id)
        {
            return await _appService.GetClaimAsync(id);
        }

        /// <summary>
        /// 添加应用程序声明
        /// </summary>
        /// <param name="clientId">应用标识</param>
        /// <param name="request">应用程序声明</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public async Task<int> CreateClaimAsync(int clientId, ClientClaimCreateRequest request)
        {
            return await _appService.CreateClaimAsync(clientId, request);
        }

        /// <summary>
        /// 删除应用程序声明
        /// </summary>
        /// <param name="id">应用程序声明编号</param>
        public async Task DeleteClaimAsync(int id)
        {
            await _appService.DeleteClaimAsync(id);
        }

        #endregion

        #region 应用程序密钥

        /// <summary>
        /// 获取应用程序密钥
        /// </summary>
        /// <param name="clientId">应用程序编号</param>
        /// <returns>Task&lt;List&lt;ClientSecretDto&gt;&gt;.</returns>
        public async Task<List<ClientSecretDto>> GetSecretsAsync(int clientId)
        {
            return await _appService.GetSecretsAsync(clientId);
        }

        /// <summary>
        /// 获取应用程序密钥
        /// </summary>
        /// <param name="id">应用程序密钥编号</param>
        /// <returns>Task&lt;ClientSecretDto&gt;.</returns>
        public async Task<ClientSecretDto> GetSecretAsync(int id)
        {
            return await _appService.GetSecretAsync(id);
        }

        /// <summary>
        /// 添加应用程序密钥
        /// </summary>
        /// <param name="clientId">应用标识</param>
        /// <param name="request">应用程序密钥</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public async Task<int> CreateSecretAsync(int clientId, ClientSecretCreateRequest request)
        {
            return await _appService.CreateSecretAsync(clientId, request);
        }

        /// <summary>
        /// 删除应用程序密钥
        /// </summary>
        /// <param name="id">应用程序密钥编号</param>
        public async Task DeleteSecretAsync(int id)
        {
            await _appService.DeleteSecretAsync(id);
        }

        #endregion
    }
}