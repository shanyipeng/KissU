﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KissU.Services.SampleA.Contract;
using KissU.Surging.Protocol.WS;

namespace KissU.Modules.SampleA.Service.Implements
{
    /// <summary>
    /// MediaService.
    /// Implements the <see cref="KissU.Surging.Protocol.WS.WSBehavior" />
    /// Implements the <see cref="IMediaService" />
    /// </summary>
    /// <seealso cref="KissU.Surging.Protocol.WS.WSBehavior" />
    /// <seealso cref="IMediaService" />
    public class MediaService : WSBehavior, IMediaService
    {
        /// <summary>
        /// Pushes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Task.</returns>
        public Task Push(IEnumerable<byte> data)
        {
            GetClient().Broadcast(data.ToArray());
            return Task.CompletedTask;
        }
    }
}