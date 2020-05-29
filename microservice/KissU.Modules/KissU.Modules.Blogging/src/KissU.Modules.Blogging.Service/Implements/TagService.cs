﻿using System.Collections.Generic;
using System.Threading.Tasks;
using KissU.Core.Extensions;
using KissU.Modules.Blogging.Application.Contracts.Tagging;
using KissU.Modules.Blogging.Application.Contracts.Tagging.Dtos;
using KissU.Surging.ProxyGenerator;

namespace KissU.Modules.Blogging.Service.Implements
{
    public class TagService : ProxyServiceBase, ITagService
    {
        private readonly ITagAppService _tagAppService;

        public TagService(ITagAppService tagAppService)
        {
            _tagAppService = tagAppService;
        }

        public Task<List<TagDto>> GetPopularTags(string blogId, GetPopularTagsInput input)
        {
            return _tagAppService.GetPopularTags(blogId.ToGuid(), input);
        }
    }
}