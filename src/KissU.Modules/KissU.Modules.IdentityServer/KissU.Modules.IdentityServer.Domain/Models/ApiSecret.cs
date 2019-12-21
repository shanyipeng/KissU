﻿// <copyright file="ApiResourceSecret.cs" company="KissU">
// Copyright (c) KissU. All Rights Reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using KissU.Util.Domains;

namespace KissU.Modules.IdentityServer.Domain.Models
{
    /// <summary>
    /// Api密钥
    /// </summary>
    public class ApiSecret : EntityBase<ApiSecret>
    {
        /// <summary>
        /// 初始化Api密钥
        /// </summary>
        public ApiSecret() : base(Guid.Empty)
        {
        }

        /// <summary>
        /// Api资源
        /// </summary>
        public ApiResource ApiResource { get; set; }

        /// <summary>
        /// 密钥类型
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Type { get; set; }

        /// <summary>
        /// 密钥值
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? Expiration { get; set; }
    }
}