//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CellRowAndCol.cs
// 描述： 单元格行列
// 作者：ChenJie 
// 编写日期：2018/10/01
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 单元格行列
    /// </summary>
    public enum CellRowAndCol
    {
        /// <summary>
        /// 行
        /// </summary>
        [Description("行")]
        Row = 0,

        /// <summary>
        /// 列
        /// </summary>
        [Description("列")]
        Col = 1
    }
}
