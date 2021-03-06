﻿using System.Threading.Tasks;
using KissU.CPlatform.Messages;
using KissU.CPlatform.Transport;

namespace KissU.CPlatform.Runtime.Server
{
    /// <summary>
    /// 一个抽象的服务执行器。
    /// </summary>
    public interface IServiceExecutor
    {
        /// <summary>
        /// 执行。
        /// </summary>
        /// <param name="sender">消息发送者。</param>
        /// <param name="message">调用消息。</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        Task ExecuteAsync(IMessageSender sender, TransportMessage message);
    }
}