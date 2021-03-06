﻿using System;

namespace KissU.Dependency
{
    /// <summary>
    /// 模块名称属性.
    /// Implements the <see cref="Attribute" />
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ModuleNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleNameAttribute" /> class.
        /// </summary>
        public ModuleNameAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleNameAttribute" /> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        public ModuleNameAttribute(string moduleName)
        {
            ModuleName = moduleName;
        }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; set; }
    }
}