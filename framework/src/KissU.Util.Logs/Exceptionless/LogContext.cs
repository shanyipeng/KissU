﻿using System;
using KissU.Core.Logs.Internal;
using KissU.Util.AspNetCore.Helpers;

namespace KissU.Util.Logs.Exceptionless
{
    /// <summary>
    /// Exceptionless日志上下文
    /// </summary>
    public class LogContext : Logs.LogContext
    {
        /// <summary>
        /// 创建日志上下文信息
        /// </summary>
        /// <returns>LogContextInfo.</returns>
        protected override LogContextInfo CreateInfo()
        {
            return new LogContextInfo
            {
                TraceId = Guid.NewGuid().ToString(),
                Stopwatch = GetStopwatch(),
                Url = Web.RequestUrl
            };
        }
    }
}