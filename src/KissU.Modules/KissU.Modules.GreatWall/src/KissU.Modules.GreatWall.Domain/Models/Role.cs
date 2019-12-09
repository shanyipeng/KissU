﻿// <copyright file="Role.cs" company="KissU">
// Copyright (c) KissU. All Rights Reserved.
// </copyright>

namespace KissU.Modules.GreatWall.Domain.Models
{
    using Util;
    using Util.Helpers;

    /// <summary>
    /// 角色
    /// </summary>
    public partial class Role
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            base.Init();
            InitType();
            InitPinYin();
        }

        /// <summary>
        /// 初始化类型
        /// </summary>
        public void InitType()
        {
            if (Type.IsEmpty())
            {
                Type = "Role";
            }
        }

        /// <summary>
        /// 初始化拼音简码
        /// </summary>
        public void InitPinYin()
        {
            PinYin = String.PinYin(Name);
        }
    }
}
