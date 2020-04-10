﻿using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace KissU.Util.Tools.Offices.Npoi
{
    /// <summary>
    /// Npoi Excel2007操作
    /// </summary>
    public class Excel2007 : ExcelBase
    {
        /// <summary>
        /// 创建工作薄
        /// </summary>
        /// <returns>IWorkbook.</returns>
        protected override IWorkbook GetWorkbook()
        {
            return new XSSFWorkbook();
        }
    }
}