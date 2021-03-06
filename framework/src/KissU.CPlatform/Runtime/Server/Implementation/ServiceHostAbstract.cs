﻿using System.Net;
using System.Threading.Tasks;
using KissU.CPlatform.Messages;
using KissU.CPlatform.Transport;
using KissU.CPlatform.Transport.Implementation;

namespace KissU.CPlatform.Runtime.Server.Implementation
{
    /// <summary>
    /// 服务主机基类。
    /// </summary>
    public abstract class ServiceHostAbstract : IServiceHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHostAbstract" /> class.
        /// </summary>
        /// <param name="serviceExecutor">The service executor.</param>
        protected ServiceHostAbstract(IServiceExecutor serviceExecutor)
        {
            ServiceExecutor = serviceExecutor;
            MessageListener.Received += MessageListener_Received;
        }

        /// <summary>
        /// 服务执行器.
        /// </summary>
        public IServiceExecutor ServiceExecutor { get; }

        /// <summary>
        /// 消息监听者。
        /// </summary>
        protected IMessageListener MessageListener { get; } = new MessageListener();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// 启动主机。
        /// </summary>
        /// <param name="endPoint">主机终结点。</param>
        /// <returns>一个任务。</returns>
        public abstract Task StartAsync(EndPoint endPoint);

        /// <summary>
        /// 启动主机。
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        /// <returns>Task.</returns>
        public abstract Task StartAsync(string ip, int port);

        private async Task MessageListener_Received(IMessageSender sender, TransportMessage message)
        {
            await ServiceExecutor.ExecuteAsync(sender, message);
        }
    }
}