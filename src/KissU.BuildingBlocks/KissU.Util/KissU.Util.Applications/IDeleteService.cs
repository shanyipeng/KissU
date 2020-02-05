﻿using KissU.Util.Applications.Operations;
using KissU.Util.Datas.Queries;

namespace KissU.Util.Applications
{
    /// <summary>
    /// 删除服务
    /// </summary>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TQueryParameter">查询参数类型</typeparam>
    public interface IDeleteService<TDto, in TQueryParameter> : IQueryService<TDto, TQueryParameter>, IDelete,
        IDeleteAsync
        where TDto : new()
        where TQueryParameter : IQueryParameter
    {
    }
}