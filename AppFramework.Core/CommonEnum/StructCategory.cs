//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： StructCategory.cs
// 描述： 工作流结构
// 作者：ChenJie 
// 编写日期：2018-09-05
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流结构
    /// </summary>
    public enum StructCategory
    {
        /// <summary>
        /// 通用结构类型
        /// </summary>
        [Description("通用结构类型")]
        Common = 0,

        /// <summary>
        /// 线性结构类型
        /// </summary>
        [Description("线性结构类型")]
        Liner = 1,

        /// <summary>
        /// 初复审类型
        /// </summary>
        [Description("初复审类型")]
        Auditing = 2
    }
}
