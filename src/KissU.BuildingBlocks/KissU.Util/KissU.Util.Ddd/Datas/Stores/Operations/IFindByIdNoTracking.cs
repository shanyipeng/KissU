﻿using KissU.Util.Ddd.Domains;

namespace KissU.Util.Ddd.Datas.Stores.Operations
{
    /// <summary>
    /// 通过标识查找
    /// </summary>
    /// <typeparam name="TEntity">对象类型</typeparam>
    /// <typeparam name="TKey">对象标识类型</typeparam>
    public interface IFindByIdNoTracking<out TEntity, in TKey> where TEntity : class, IKey<TKey>
    {
        /// <summary>
        /// 查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        /// <returns>TEntity.</returns>
        TEntity FindByIdNoTracking(TKey id);
    }
}