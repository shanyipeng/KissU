﻿using System.Threading.Tasks;
using KissU.Dependency;
using KissU.Extensions;
using KissU.Models;
using KissU.Modules.Identity.Application.Contracts;
using KissU.Modules.Identity.Service.Contracts;
using KissU.ServiceProxy;
using Volo.Abp.Application.Dtos;

namespace KissU.Modules.Identity.Service.Implements
{
    [ModuleName("IdentityRole")]
    public class IdentityRoleService : ProxyServiceBase, IIdentityRoleService
    {
        private readonly IIdentityRoleAppService _appService;

        public IdentityRoleService(IIdentityRoleAppService appService)
        {
            _appService = appService;
        }

        public virtual async Task<ListResult<IdentityRoleDto>> GetAllListAsync()
        {
            var result = await _appService.GetAllListAsync();
            return new ListResult<IdentityRoleDto>(result.Items);
        }

        public virtual async Task<PagedResult<IdentityRoleDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var result = await _appService.GetListAsync(input);
            return new PagedResult<IdentityRoleDto>(result.TotalCount, result.Items);
        }

        public virtual Task<IdentityRoleDto> GetAsync(string id)
        {
            return _appService.GetAsync(id.ToGuid());
        }

        public virtual Task<IdentityRoleDto> CreateAsync(IdentityRoleCreateDto input)
        {
            return _appService.CreateAsync(input);
        }

        public virtual Task<IdentityRoleDto> UpdateAsync(string id, IdentityRoleUpdateDto input)
        {
            return _appService.UpdateAsync(id.ToGuid(), input);
        }

        public virtual Task DeleteAsync(string id)
        {
            return _appService.DeleteAsync(id.ToGuid());
        }
    }
}