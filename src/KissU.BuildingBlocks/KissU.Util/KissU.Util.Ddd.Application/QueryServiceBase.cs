﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using KissU.Core.Datas;
using KissU.Core.Datas.Queries;
using KissU.Core.Maps;
using KissU.Util.Ddd.Application.Contracts;
using KissU.Util.Ddd.Domain;
using KissU.Util.Ddd.Domain.Datas;
using KissU.Util.Ddd.Domain.Datas.Queries;
using KissU.Util.Ddd.Domain.Datas.Repositories;
using KissU.Util.Ddd.Domain.Datas.Stores;
using Convert = KissU.Core.Helpers.Convert;

namespace KissU.Util.Ddd.Application
{
    /// <summary>
    /// 查询服务
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TQueryParameter">查询参数类型</typeparam>
    public abstract class
        QueryServiceBase<TEntity, TDto, TQueryParameter> : QueryServiceBase<TEntity, TDto, TQueryParameter, Guid>
        where TEntity : class, IKey<Guid>, IVersion
        where TDto : new()
        where TQueryParameter : IQueryParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryServiceBase{TEntity, TDto, TQueryParameter}" /> class.
        /// 初始化查询服务
        /// </summary>
        /// <param name="store">查询存储器</param>
        protected QueryServiceBase(IQueryStore<TEntity, Guid> store)
            : base(store)
        {
        }
    }

    /// <summary>
    /// 查询服务
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TQueryParameter">查询参数类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract class QueryServiceBase<TEntity, TDto, TQueryParameter, TKey> : ServiceBase,
        IQueryService<TDto, TQueryParameter>
        where TEntity : class, IKey<TKey>, IVersion
        where TDto : new()
        where TQueryParameter : IQueryParameter
    {
        /// <summary>
        /// 查询存储器
        /// </summary>
        private readonly IQueryStore<TEntity, TKey> _store;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryServiceBase{TEntity, TDto, TQueryParameter, TKey}" /> class.
        /// 初始化查询服务
        /// </summary>
        /// <param name="store">查询存储器</param>
        /// <exception cref="ArgumentNullException">store</exception>
        protected QueryServiceBase(IQueryStore<TEntity, TKey> store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
        }

        /// <summary>
        /// 查询时是否跟踪对象
        /// </summary>
        protected virtual bool IsTracking => false;

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns>结果</returns>
        public virtual List<TDto> GetAll()
        {
            return _store.FindAll().Select(ToDto).ToList();
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _store.FindAllAsync();
            return entities.Select(ToDto).ToList();
        }

        /// <summary>
        /// 通过标识获取
        /// </summary>
        /// <param name="id">实体编号</param>
        /// <returns>结果</returns>
        public virtual TDto GetById(object id)
        {
            var key = Convert.To<TKey>(id);
            return ToDto(_store.Find(key));
        }

        /// <summary>
        /// 通过标识获取
        /// </summary>
        /// <param name="id">实体编号</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public virtual async Task<TDto> GetByIdAsync(object id)
        {
            var key = Convert.To<TKey>(id);
            return ToDto(await _store.FindAsync(key));
        }

        /// <summary>
        /// 通过标识列表获取
        /// </summary>
        /// <param name="ids">用逗号分隔的Id列表，范例："1,2"</param>
        /// <returns>结果</returns>
        public virtual List<TDto> GetByIds(string ids)
        {
            return _store.FindByIds(ids).Select(ToDto).ToList();
        }

        /// <summary>
        /// 通过标识列表获取
        /// </summary>
        /// <param name="ids">用逗号分隔的Id列表，范例："1,2"</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public virtual async Task<List<TDto>> GetByIdsAsync(string ids)
        {
            var entities = await _store.FindByIdsAsync(ids);
            return entities.Select(ToDto).ToList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public virtual async Task<List<TDto>> QueryAsync(TQueryParameter parameter)
        {
            if (parameter == null)
            {
                return new List<TDto>();
            }

            var query = CreateQuery(parameter);
            var entities = await _store.QueryAsync(query);
            return entities.Select(ToDto).ToList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns>结果</returns>
        public virtual List<TDto> Query(TQueryParameter parameter)
        {
            if (parameter == null)
            {
                return new List<TDto>();
            }

            return ExecuteQuery(parameter).ToList().Select(ToDto).ToList();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns>结果</returns>
        public virtual PagerList<TDto> PagerQuery(TQueryParameter parameter)
        {
            if (parameter == null)
            {
                return new PagerList<TDto>();
            }

            var query = CreateQuery(parameter);
            var queryable = Filter(query);
            queryable = Filter(queryable, parameter);
            return queryable.ToPagerList(query.GetPager()).Convert(ToDto);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        public virtual async Task<PagerList<TDto>> PagerQueryAsync(TQueryParameter parameter)
        {
            if (parameter == null)
            {
                return new PagerList<TDto>();
            }

            var query = CreateQuery(parameter);
            var entities = await _store.PagerQueryAsync(query);
            return entities.Convert(ToDto);
        }

        /// <summary>
        /// 转换为数据传输对象
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        protected virtual TDto ToDto(TEntity entity)
        {
            return entity.MapTo<TDto>();
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        private IQueryable<TEntity> ExecuteQuery(TQueryParameter parameter)
        {
            var query = CreateQuery(parameter);
            var queryable = Filter(query);
            queryable = Filter(queryable, parameter);
            var order = query.GetOrder();
            return string.IsNullOrWhiteSpace(order) ? queryable : queryable.OrderBy(order);
        }

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns>结果</returns>
        protected virtual IQueryBase<TEntity> CreateQuery(TQueryParameter parameter)
        {
            return new Query<TEntity, TKey>(parameter);
        }

        /// <summary>
        /// 过滤
        /// </summary>
        private IQueryable<TEntity> Filter(IQueryBase<TEntity> query)
        {
            return IsTracking ? _store.Find().Where(query) : _store.FindAsNoTracking().Where(query);
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="queryable">The queryable.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>结果</returns>
        protected virtual IQueryable<TEntity> Filter(IQueryable<TEntity> queryable, TQueryParameter parameter)
        {
            return queryable;
        }
    }
}