﻿using System.Threading.Tasks;
using KissU.Dependency;
using KissU.Modules.Identity.Application.Contracts;
using KissU.CPlatform.Filters.Implementation;
using KissU.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using KissU.Models;
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

        [Authorization(AuthType = AuthorizationType.JWTBearer)]
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