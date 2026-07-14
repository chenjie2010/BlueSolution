//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WorkflowHandlerMode.cs
// 描述： 工作流处理模式
// 作者：ChenJie 
// 编写日期：2018/01/11
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流处理人模式
    /// </summary>
    [Description("工作流处理人模式")]
    public enum WorkflowHandlerMode
    {
        /// <summary>
        /// 轮流分配
        /// </summary>
        [Description("轮流分配")]
        InTurn = 0,

        /// <summary>
        /// 余量平均分配
        /// </summary>
        [Description("余量平均分配")]
        RemainingWorkflowsAveraged = 2,

        /// <summary>
        /// 用户选择
        /// </summary>
        [Description("用户选择")]
        UserSelected = 3
    }
}
