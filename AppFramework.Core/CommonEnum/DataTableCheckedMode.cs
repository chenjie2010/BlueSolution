//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataTableCheckedMode.cs
// 描述： 数据表校验模式
// 作者：ChenJie 
// 编写日期：2018-08-03
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据表校验模式
    /// </summary>
    [Description("数据表校验模式")]
    public enum DataTableCheckedMode
    {
        /// <summary>
        ///  无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        ///  整行校验
        /// </summary>
        [Description("整行校验")]
        Row = 1,

        /// <summary>
        ///  整列校验
        /// </summary>
        [Description("整列校验")]
        Col = 2
    }
}
