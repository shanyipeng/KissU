﻿using System;

namespace KissU.Exceptions
{
    /// <summary>
    /// 通讯异常（与服务端进行通讯时发生的异常）。
    /// </summary>
    public class CommunicationException : CPlatformException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationException" /> class.
        /// 初始构造函数
        /// </summary>
        /// <param name="message">异常消息。</param>
        /// <param name="innerException">内部异常。</param>
        public CommunicationException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}