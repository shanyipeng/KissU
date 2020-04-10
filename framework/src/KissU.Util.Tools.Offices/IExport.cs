﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KissU.Util.Tools.Offices
{
    /// <summary>
    /// 文件导出器
    /// </summary>
    public interface IExport
    {
        /// <summary>
        /// 设置日期格式
        /// </summary>
        /// <param name="format">日期格式，默认"yyyy-mm-dd"</param>
        /// <returns>IExport.</returns>
        IExport DateFormat(string format = "yyyy-mm-dd");

        /// <summary>
        /// 列宽
        /// </summary>
        /// <param name="columnIndex">列索引</param>
        /// <param name="width">宽度</param>
        /// <returns>IExport.</returns>
        IExport ColumnWidth(int columnIndex, int width);

        /// <summary>
        /// 设置表头样式
        /// </summary>
        /// <param name="style">单元格样式</param>
        /// <returns>IExport.</returns>
        IExport HeadStyle(CellStyle style);

        /// <summary>
        /// 设置正文样式
        /// </summary>
        /// <param name="style">单元格样式</param>
        /// <returns>IExport.</returns>
        IExport BodyStyle(CellStyle style);

        /// <summary>
        /// 设置页脚样式
        /// </summary>
        /// <param name="style">单元格样式</param>
        /// <returns>IExport.</returns>
        IExport FootStyle(CellStyle style);

        /// <summary>
        /// 添加表头
        /// </summary>
        /// <param name="titles">列标题</param>
        /// <returns>IExport.</returns>
        IExport Head(params string[] titles);

        /// <summary>
        /// 添加表头
        /// </summary>
        /// <param name="cells">单元格集合</param>
        /// <returns>IExport.</returns>
        IExport Head(params Cell[] cells);

        /// <summary>
        /// 添加正文
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">实体集合</param>
        /// <returns>IExport.</returns>
        IExport Body<T>(IEnumerable<T> list) where T : class;

        /// <summary>
        /// 添加正文
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">实体集合</param>
        /// <param name="propertiesExpression">属性列表表达式，范例：t =&gt; new object[]{t.A,t.B}</param>
        /// <returns>IExport.</returns>
        IExport Body<T>(IEnumerable<T> list, Expression<Func<T, object[]>> propertiesExpression) where T : class;

        /// <summary>
        /// 添加正文
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">实体集合</param>
        /// <param name="propertyNames">属性列表,范例："A","B"</param>
        /// <returns>IExport.</returns>
        IExport Body<T>(IEnumerable<T> list, params string[] propertyNames) where T : class;

        /// <summary>
        /// 添加页脚
        /// </summary>
        /// <param name="values">值</param>
        /// <returns>IExport.</returns>
        IExport Foot(params string[] values);

        /// <summary>
        /// 添加页脚
        /// </summary>
        /// <param name="cells">单元格集合</param>
        /// <returns>IExport.</returns>
        IExport Foot(params Cell[] cells);

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="directory">目录，不包括文件名</param>
        /// <param name="fileName">文件名，不包括扩展名</param>
        /// <returns>IExport.</returns>
        IExport Write(string directory, string fileName = "");
    }
}