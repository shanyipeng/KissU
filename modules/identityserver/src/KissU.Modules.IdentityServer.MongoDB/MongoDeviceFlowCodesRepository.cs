﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KissU.Modules.IdentityServer.Domain.Devices;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace KissU.Modules.IdentityServer.MongoDB
{
    public class MongoDeviceFlowCodesRepository :
        MongoDbRepository<IAbpIdentityServerMongoDbContext, DeviceFlowCodes, Guid>, IDeviceFlowCodesRepository
    {
        public MongoDeviceFlowCodesRepository(
            IMongoDbContextProvider<IAbpIdentityServerMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public virtual async Task<DeviceFlowCodes> FindByUserCodeAsync(
            string userCode,
            CancellationToken cancellationToken = default)
        {
            return await GetMongoQueryable()
                .FirstOrDefaultAsync(d => d.UserCode == userCode, GetCancellationToken(cancellationToken))
                ;
        }

        public virtual async Task<DeviceFlowCodes> FindByDeviceCodeAsync(string deviceCode, CancellationToken cancellationToken = default)
        {
            return await GetMongoQueryable()
                .FirstOrDefaultAsync(d => d.DeviceCode == deviceCode, GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<DeviceFlowCodes>> GetListByExpirationAsync(
            DateTime maxExpirationDate, 
            int maxResultCount,
            CancellationToken cancellationToken = default)
        {
            return await GetMongoQueryable()
                .Where(x => x.Expiration != null && x.Expiration < maxExpirationDate)
                .OrderBy(x => x.ClientId)
                .Take(maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}