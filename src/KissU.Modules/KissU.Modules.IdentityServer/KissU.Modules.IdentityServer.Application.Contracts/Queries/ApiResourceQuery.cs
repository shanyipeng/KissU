﻿using System.ComponentModel.DataAnnotations;
using KissU.Util.Ddd.Datas.Queries;

namespace KissU.Modules.IdentityServer.Application.Queries
{
    /// <summary>
    /// Api资源查询实体
    /// </summary>
    public class ApiResourceQuery : QueryParameter
    {
        /// <summary>
        /// 启用状态
        /// </summary>
        [Display(Name = "启用状态")]
        public bool? Enabled { get; set; }
    }
}