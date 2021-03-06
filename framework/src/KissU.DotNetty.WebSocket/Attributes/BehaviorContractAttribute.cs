﻿using System;

namespace KissU.DotNetty.WebSocket.Attributes
{
    /// <summary>
    /// BehaviorContractAttribute.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class BehaviorContractAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        public string Protocol { get; set; }
    }
}