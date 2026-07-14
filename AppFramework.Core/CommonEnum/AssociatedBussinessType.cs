//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AssociatedBussinessType.cs
// 描述： 关联类型
// 作者：ChenJie 
// 编写日期：2018-08-24
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 关联类型
    /// </summary>
    public enum AssociatedBussinessType
    {
        /// <summary>
        /// 数据表类型
        /// </summary>
        [Description("数据表类型")]
        Table = 0,

        /// <summary>
        /// 数据填报类型
        /// </summary>
        [Description("数据填报类型")]
        DataFilled = 1,

        /// <summary>
        /// 工作流类型
        /// </summary>
        [Description("工作流类型")]
        Workflow = 2         

    }
}
