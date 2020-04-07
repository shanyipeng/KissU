﻿using System.ComponentModel.DataAnnotations;
using KissU.Util.Ddd.Applications.Dtos;

namespace KissU.Modules.IdentityServer.Application.Contracts.Dtos
{
    /// <summary>
    /// 应用克隆参数
    /// </summary>
    public class ClientCloneRequest : RequestBase
    {
        /// <summary>
        /// 应用程序编号
        /// </summary>
        [Required]
        public string ClientId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string ClientIdOriginal { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ClientNameOriginal { get; set; }

        /// <summary>
        /// 跨域策略
        /// </summary>
        public bool CloneClientCorsOrigins { get; set; }

        /// <summary>
        /// 登录重定向Uris
        /// </summary>
        public bool CloneClientRedirectUris { get; set; }

        /// <summary>
        /// 外部登录提供商
        /// </summary>
        public bool CloneClientIdPRestrictions { get; set; }

        /// <summary>
        /// 注销重定向Uris
        /// </summary>
        public bool CloneClientPostLogoutRedirectUris { get; set; }

        /// <summary>
        /// 授权类型
        /// </summary>
        public bool CloneClientGrantTypes { get; set; }

        /// <summary>
        /// 许可范围
        /// </summary>
        public bool CloneClientScopes { get; set; }
    }
}