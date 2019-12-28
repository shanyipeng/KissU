﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KissU.Util.Applications.Dtos;

namespace KissU.Modules.IdentityServer.Application.Dtos
{
    /// <summary>
    /// Api资源数据传输对象
    /// </summary>
    public class ApiResourceDto : DtoBase
    {
        /// <summary>
        /// API的唯一名称。此值用于内省身份验证，并将添加到传出访问令牌的受众。
        /// </summary>

        [Required]
        [StringLength(200, ErrorMessage = "名称输入过长，不能超过200位")]
        [Display(Name = "API的唯一名称")]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称。该值可以在例如同意屏幕上使用。
        /// </summary>

        [StringLength(200, ErrorMessage = "显示名称输入过长，不能超过200位")]
        [Display(Name = "显示名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述。该值可以在例如同意屏幕上使用。
        /// </summary>

        [StringLength(200, ErrorMessage = "描述输入过长，不能超过1000位")]
        [Display(Name = "描述")]
        public string Description { get; set; }

        /// <summary>
        /// 指示此资源是否已启用且可以请求。默认为true。
        /// </summary>

        [Required]
        [Display(Name = "是否启用")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 应包含在身份令牌中的关联用户声明类型的列表。
        /// </summary>

        [Display(Name = "声明类型的列表")]
        public List<string> UserClaims { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>

        [Display(Name = "版本号")]
        public byte[] Version { get; set; }
    }
}