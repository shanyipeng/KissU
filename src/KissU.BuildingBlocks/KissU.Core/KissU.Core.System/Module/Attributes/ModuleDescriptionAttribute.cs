﻿using System;

namespace KissU.Core.System.Module.Attributes
{
    /// <summary>
    /// ModuleDescriptionAttribute 自定义特性类。
    /// </summary>
    /// <remarks>
    ///     <para>创建：范亮</para>
    ///     <para>日期：2015/12/4</para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleDescriptionAttribute : Attribute
    {
        #region 方法

        /// <summary>
        /// 初始化一个新的 <see cref="ModuleDescriptionAttribute" /> 类实例。
        /// </summary>
        /// <param name="identifier">标识符 GUID 。</param>
        /// <param name="title">标题。</param>
        /// <param name="description">描述。</param>
        /// <remarks>
        ///     <para>创建：范亮</para>
        ///     <para>日期：2015/12/4</para>
        /// </remarks>
        public ModuleDescriptionAttribute(string identifier, string title, string description)
        {
            Identifier = identifier;
            Title = title;
            Description = description;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取标识符 GUID 。
        /// </summary>
        /// <remarks>
        ///     <para>创建：范亮</para>
        ///     <para>日期：2015/12/4</para>
        /// </remarks>
        public string Identifier { get; }

        /// <summary>
        /// 获取标题信息。
        /// </summary>
        /// <remarks>
        ///     <para>创建：范亮</para>
        ///     <para>日期：2015/12/4</para>
        /// </remarks>
        public string Title { get; }

        /// <summary>
        /// 获取描述信息。
        /// </summary>
        /// <remarks>
        ///     <para>创建：范亮</para>
        ///     <para>日期：2015/12/4</para>
        /// </remarks>
        public string Description { get; }

        #endregion
    }
}