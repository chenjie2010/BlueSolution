//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： TableCategroy.cs
// 描述： 表格类型
// 作者：ChenJie 
// 编写日期：2018-09-07
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表格类型
    /// </summary>
    public enum TableCategroy
    {
        /// <summary>
        /// 单表
        /// </summary>
        [Description("单表")]
        Single = 0,

        /// <summary>
        /// 多表
        /// </summary>
        [Description("多表")]
        Multiple = 1
    }
}
