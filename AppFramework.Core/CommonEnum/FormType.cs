//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： FormType.cs
// 描述： 窗体表格类型
// 作者：ChenJie 
// 编写日期：2017-10-31
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 窗体表格类型
    /// </summary>
    public enum FormType
    {
        /// <summary>
        /// 数据表
        /// </summary>
        [Description("数据表")]
        Table = 1,

        /// <summary>
        /// 组合表：主表
        /// </summary>
        [Description("组合表")]
        CombinedTable = 2
    }
}
