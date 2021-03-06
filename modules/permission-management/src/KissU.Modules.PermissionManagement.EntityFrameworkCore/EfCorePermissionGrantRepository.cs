﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KissU.Modules.PermissionManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KissU.Modules.PermissionManagement.EntityFrameworkCore
{
    public class EfCorePermissionGrantRepository : EfCoreRepository<IPermissionManagementDbContext, PermissionGrant, Guid>, 
        IPermissionGrantRepository
    {
        public EfCorePermissionGrantRepository(IDbContextProvider<IPermissionManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<PermissionGrant> FindAsync(
            string name, 
            string providerName, 
            string providerKey,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .FirstOrDefaultAsync(s =>
                    s.Name == name &&
                    s.ProviderName == providerName &&
                    s.ProviderKey == providerKey,
                    GetCancellationToken(cancellationToken)
                );
        }

        public virtual async Task<List<PermissionGrant>> GetListAsync(
            string providerName,
            string providerKey,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(s =>
                    s.ProviderName == providerName &&
                    s.ProviderKey == providerKey
                ).ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
