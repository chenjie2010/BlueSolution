//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WorkflowBusinessType.cs
// 描述： 工作流业务类型
// 作者：ChenJie 
// 编写日期：2018-08-29
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 处理工作流业务类型
    /// </summary>
    public enum WorkflowBusinessType
    {
        /// <summary>
        /// 待处理工作流
        /// </summary>
        [Description("待处理工作流")]
        WorkflowAuditing = 0,

        /// <summary>
        /// 已处理工作流
        /// </summary>
        [Description("已处理工作流")]
        WorkflowAudited = 1,

        /// <summary>
        /// 工作流统计
        /// </summary>
        [Description("工作流统计")]
        WorkflowStatistics = 2
    }
}
