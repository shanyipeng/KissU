﻿using System.Threading.Tasks;
using IdentityServer4.Stores;
using Volo.Abp.ObjectMapping;

namespace KissU.Modules.IdentityServer.Domain.Clients
{
    public class ClientStore : IClientStore
    {
        protected IClientRepository ClientRepository { get; }
        protected IObjectMapper<AbpIdentityServerDomainModule> ObjectMapper { get; }

        public ClientStore(IClientRepository clientRepository, IObjectMapper<AbpIdentityServerDomainModule> objectMapper)
        {
            ClientRepository = clientRepository;
            ObjectMapper = objectMapper;
        }

        public virtual async Task<IdentityServer4.Models.Client> FindClientByIdAsync(string clientId)
        {
            var client = await ClientRepository.FindByCliendIdAsync(clientId);
            var result = ObjectMapper.Map<Client, IdentityServer4.Models.Client>(client);
            return result;
        }
    }
}
