﻿using GreatWall.Systems.Domain.Models;
using GreatWall.Systems.Domain.Repositories;
using Util.Datas.Ef.Core;

namespace GreatWall.Data.Repositories.Systems {
    /// <summary>
    /// 角色仓储
    /// </summary>
    public class RoleRepository : TreeRepositoryBase<Role>, IRoleRepository {
        /// <summary>
        /// 初始化角色仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public RoleRepository( IKissU.GreatWallUnitOfWork unitOfWork ) : base( unitOfWork ) {
        }
    }
}
