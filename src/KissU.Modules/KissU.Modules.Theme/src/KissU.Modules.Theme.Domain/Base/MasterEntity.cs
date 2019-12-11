﻿// <copyright file="MasterEntity.cs" company="KissU">
// Copyright (c) KissU. All Rights Reserved.
// </copyright>

using KissU.Util.Domains;
using KissU.Util.Domains.Auditing;

namespace KissU.Modules.Theme.Domain.Base
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// 主表
    /// </summary>
    public abstract class MasterEntity<TDetailEntity> : AggregateRoot<MasterEntity<TDetailEntity>>, IAudited
        where TDetailEntity : DetailEntity
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected MasterEntity() : this(Guid.Empty)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="id">系统设置-消毒计划标识</param>
        protected MasterEntity(Guid id) : base(id)
        {
            Details = new List<TDetailEntity>();
        }

        /// <summary>
        /// 从表
        /// </summary>
        public List<TDetailEntity> Details { get; protected set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public Guid? CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [DisplayName("修改人")]
        public Guid? LastModifierId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 添加明细
        /// </summary>
        /// <param name="detail">明细</param>
        public void AddDetail(TDetailEntity detail)
        {
            if (detail == null)
            {
                return;
            }

            detail.Init();
            detail.MainId = Id;
            Details.Add(detail);
        }
    }
}
