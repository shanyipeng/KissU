﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using KissU.Modules.IdentityServer.Domain.Clients;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KissU.Modules.IdentityServer.EntityFrameworkCore.Clients
{
    public class ClientRepository : EfCoreRepository<IIdentityServerDbContext, Client, Guid>, IClientRepository
    {
        public ClientRepository(IDbContextProvider<IIdentityServerDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public virtual async Task<Client> FindByCliendIdAsync(
            string clientId,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeDetails(includeDetails)
                .FirstOrDefaultAsync(x => x.ClientId == clientId, GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<Client>> GetListAsync(
            string sorting, int skipCount, int maxResultCount, string filter, bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeDetails(includeDetails)
                .WhereIf(!filter.IsNullOrWhiteSpace(), x => x.ClientId.Contains(filter))
                .OrderBy(sorting ?? nameof(Client.ClientName) + " desc")
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<string>> GetAllDistinctAllowedCorsOriginsAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.ClientCorsOrigins
                .Select(x => x.Origin)
                .Distinct()
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<bool> CheckClientIdExistAsync(string clientId, Guid? expectedId = null, CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(c => c.Id != expectedId && c.ClientId == clientId, cancellationToken: cancellationToken);
        }

        public override IQueryable<Client> WithDetails()
        {
            return GetQueryable().IncludeDetails();
        }
    }
}
