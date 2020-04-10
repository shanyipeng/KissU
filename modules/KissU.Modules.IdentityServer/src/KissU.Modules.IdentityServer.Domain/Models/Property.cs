﻿using System.ComponentModel.DataAnnotations;
using KissU.Util.Ddd.Domain;

namespace KissU.Modules.IdentityServer.Domain.Models
{
    /// <summary>
    /// 属性
    /// </summary>
    public abstract class Property : ValueObjectBase<Property>
    {
        /// <summary>
        /// 键
        /// </summary>
        [StringLength(250)]
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [StringLength(2000)]
        public string Value { get; set; }
    }
}