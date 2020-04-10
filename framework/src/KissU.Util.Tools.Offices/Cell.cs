﻿using KissU.Core;
using KissU.Util.Tools.Offices.Core;

namespace KissU.Util.Tools.Offices
{
    /// <summary>
    /// 单元格
    /// </summary>
    public class Cell
    {
        private int _columnSpan;

        private int _rowSpan;

        /// <summary>
        /// 初始化单元格
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="columnSpan">列跨度</param>
        /// <param name="rowSpan">行跨度</param>
        public Cell(object value, int columnSpan = 1, int rowSpan = 1)
        {
            Value = value;
            ColumnSpan = columnSpan;
            RowSpan = rowSpan;
        }

        /// <summary>
        /// 行
        /// </summary>
        public Row Row { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 列跨度
        /// </summary>
        public int ColumnSpan
        {
            get => _columnSpan;
            set
            {
                if (value < 1)
                    value = 1;
                _columnSpan = value;
            }
        }

        /// <summary>
        /// 行跨度
        /// </summary>
        public int RowSpan
        {
            get => _rowSpan;
            set
            {
                if (value < 1)
                    value = 1;
                _rowSpan = value;
            }
        }

        /// <summary>
        /// 行索引
        /// </summary>
        public int RowIndex
        {
            get
            {
                Row.CheckNull("Row");
                return Row.RowIndex;
            }
        }

        /// <summary>
        /// 列索引
        /// </summary>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// 结束行索引
        /// </summary>
        public int EndRowIndex => RowIndex + RowSpan - 1;

        /// <summary>
        /// 结束列索引
        /// </summary>
        public int EndColumnIndex => ColumnIndex + ColumnSpan - 1;

        /// <summary>
        /// 是否需要合并单元格
        /// </summary>
        public bool NeedMerge => ColumnSpan > 1 || RowSpan > 1;

        /// <summary>
        /// 是否为空单元格
        /// </summary>
        /// <returns><c>true</c> if this instance is null; otherwise, <c>false</c>.</returns>
        public virtual bool IsNull()
        {
            return false;
        }
    }
}