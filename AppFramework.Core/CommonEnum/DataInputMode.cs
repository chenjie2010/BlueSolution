//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataInputMode.cs
// 描述： 数据录入模式
// 作者：ChenJie 
// 编写日期：2019/06/22
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据录入模式
    /// </summary>
    [Description("数据录入模式")]
    public enum DataInputMode
    {
        /// <summary>
        /// 表格模式
        /// </summary>
        [Description("表格模式")]
        Table = 1,

        /// <summary>
        /// 复表模式
        /// </summary>
        [Description("复表模式")]
        Report = 2
    }
}
