﻿using Util.Datas.Ef.Core;
using GreatWall.Systems.Domain.Models;
using GreatWall.Systems.Domain.Repositories;
using GreatWall.Data.Pos.Systems;
using GreatWall.Data.Stores.Abstractions.Systems;
using GreatWall.Data.Pos.Systems.Extensions;

namespace GreatWall.Data.Repositories.Systems {
    /// <summary>
    /// 用户仓储
    /// </summary>
    public class UserRepository : CompactRepositoryBase<User,UserPo>, IUserRepository {
        /// <summary>
        /// 初始化用户仓储
        /// </summary>
        /// <param name="store">用户存储器</param>
        public UserRepository( IUserPoStore store ) : base( store ) {
        }
        
        /// <summary>
        /// 转成实体
        /// </summary>
        /// <param name="po">持久化对象</param>
        protected override User ToEntity( UserPo po ) {
            return po.ToEntity();
        }

        /// <summary>
        /// 转成持久化对象
        /// </summary>
        /// <param name="entity">实体</param>
        protected override UserPo ToPo( User entity ) {
            return entity.ToPo();
        }
    }
}