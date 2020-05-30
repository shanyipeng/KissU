﻿using System;
using System.Threading.Tasks;
using KissU.Core.Common;
using KissU.Core.Dependency;
using KissU.Modules.Identity.Application.Contracts;
using KissU.Surging.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Volo.Abp.Application.Dtos;

namespace KissU.Modules.Identity.Service.Contracts
{
    [ServiceBundle("api/{Service}")]
    public interface IIdentityRoleService : IServiceKey
    {
        [HttpGet(true)]
        [ServiceRoute("all")]
        Task<ListResult<IdentityRoleDto>> GetAllListAsync();

        [HttpPost(true)]
        Task<PagedResult<IdentityRoleDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        [HttpGet(true)]
        [ServiceRoute("{id}")]
        Task<IdentityRoleDto> GetAsync(string id);

        [HttpPost(true)]
        Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input);

        [HttpPut(true)]
        [ServiceRoute("{id}")]
        Task<IdentityRoleDto> UpdateAsync(string id, IdentityRoleUpdateDto input);

        [HttpDelete(true)]
        [ServiceRoute("{id}")]
        Task DeleteAsync(string id);
    }
}