//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CellType.cs
// 描述： 单元格类型
// 作者：ChenJie 
// 编写日期：2018/07/23
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 单元格类型
    /// </summary>
    [Description("单元格类型")]
    public enum CellDataType
    {
        /// <summary>
        /// 通用类型
        /// </summary>
        [Description("通用类型")]
        GeneralCellType = 0,

        /// <summary>
        /// 文本类型
        /// </summary>
        [Description("文本类型")]
        TextCellType = 1,

        /// <summary>
        /// 数字类型
        /// </summary>
        [Description("数字类型")]
        NumberCellType = 2,

        /// <summary>
        /// 百分比类型
        /// </summary>
        [Description("百分比类型")]
        PercentCellType = 3,

        /// <summary>
        /// 货币类型
        /// </summary>
        [Description("货币类型")]
        CurrencyCellType = 4,

        /// <summary>
        /// 日期时间类型
        /// </summary>
        [Description("日期时间类型")]
        DateTimeCellType = 5

    }
}
