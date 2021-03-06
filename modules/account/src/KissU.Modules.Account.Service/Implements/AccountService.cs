﻿using System.Threading.Tasks;
using KissU.Dependency;
using KissU.Modules.Account.Application.Contracts;
using KissU.Modules.Account.Service.Contracts;
using KissU.Modules.Identity.Application.Contracts;
using KissU.ServiceProxy;

namespace KissU.Modules.Account.Service.Implements
{
    [ModuleName(AccountRemoteServiceConsts.RemoteServiceName)]
    public class AccountService : ProxyServiceBase, IAccountService
    {
        private readonly IAccountAppService _accountAppService;

        public AccountService(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        public virtual Task<IdentityUserDto> RegisterAsync(RegisterDto input)
        {
            return _accountAppService.RegisterAsync(input);
        }
    }
}