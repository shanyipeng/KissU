﻿using System.Threading.Tasks;
using KissU.Services.SampleA.Contract.Dtos;
using KissU.Surging.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;

namespace KissU.Services.SampleA.Contract
{
    /// <summary>
    /// Interface IRoteMangeService
    /// </summary>
    [ServiceBundle("Api/{Service}")]
    public interface IRoteMangeService
    {
        /// <summary>
        /// Gets the service by identifier.
        /// </summary>
        /// <param name="serviceId">The service identifier.</param>
        /// <returns>Task&lt;UserModel&gt;.</returns>
        Task<UserModel> GetServiceById(string serviceId);

        /// <summary>
        /// Sets the rote.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> SetRote(RoteModel model);
    }
}