//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： InstanceStatus.cs
// 描述： 工作流状态枚举
// 作者：ChenJie 
// 编写日期：2018/05/02
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流枚举
    /// </summary>
    public enum InstanceStatus
    {
        /// <summary>
        /// 待提交
        /// </summary>
        [Description("待提交")]
        None = 0,

        /// <summary>
        /// 审核中
        /// </summary>
        [Description("审核中")]
        Review = 1,
        
        /// <summary>
        /// 审核终止
        /// </summary>
        [Description("审核终止")]
        ReviewAborted = 2,

        /// <summary>
        /// 审核完成
        /// </summary>
        [Description("审核完成")]
        Completed = 3
    }
}
