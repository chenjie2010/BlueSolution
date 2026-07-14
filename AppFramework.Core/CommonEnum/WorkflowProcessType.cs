//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ProcessType.cs
// 描述： 工作流节点类型
// 作者：ChenJie 
// 编写日期：2017/12/06
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流节点类型
    /// </summary>
    [Description("工作流节点类型")]
    public enum WorkflowProcessType
    {
        /// <summary>
        /// 单行节点
        /// </summary>
        [Description("单行节点")]
        SingleBranch = 0,

        /// <summary>
        /// 并行节点
        /// </summary>
        [Description("并行节点")]
        ParallelBranch = 1,

        /// <summary>
        /// 选择节点
        /// </summary>
        [Description("选择节点")]
        SelectiveBranch = 2,

        /// <summary>
        /// 动态节点
        /// </summary>
        [Description("动态节点")]
        DynamicBranch = 3,

        /// <summary>
        /// 单位内动态节点
        /// </summary>
        [Description("单位内动态节点")]
        DynamicBranchInDeps = 4,

        /// <summary>
        /// 单位间动态节点
        /// </summary>
        [Description("单位间动态节点")]
        DynamicBranchBetweenDeps = 5
    }
}