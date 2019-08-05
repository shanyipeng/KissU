﻿using System.Threading.Tasks;
using GreatWall.Results;
using GreatWall.Service.Dtos.Requests;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Util.Applications;
using Util.Aspects;
using Util.Validations.Aspects;

namespace GreatWall.Service.Abstractions {
    /// <summary>
    /// 安全服务
    /// </summary>
    [ServiceBundle("api/{Service}")]
    public interface ISecurityService : IService {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request">登录参数</param>
        [HttpPost(true)]
        Task<SignInResult> SignInAsync( [NotNull] [Valid] LoginRequest request );
        /// <summary>
        /// 退出登录
        /// </summary>
        [HttpGet(true)]
        Task SignOutAsync();
    }
}