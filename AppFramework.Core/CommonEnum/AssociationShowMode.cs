//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AssociationShowMode.cs
// 描述： 关联数据表展示模式
// 作者：ChenJie 
// 编写日期：2017-11-10
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 关联数据表展示模式
    /// </summary>
    [Description("关联数据表展示模式")]
    public enum AssociationShowMode
    {
        /// <summary>
        /// 表格结构
        /// </summary>
        [Description("表格结构")]
        Table = 1,

        /// <summary>
        /// 层级结构
        /// </summary>
        [Description("层级结构")]
        Hierarchicy = 2
    }
}
