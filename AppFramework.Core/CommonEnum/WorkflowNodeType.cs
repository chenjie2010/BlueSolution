//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CombinedNodeType.cs
// 描述： 含两层分类的节点类型
// 作者：ChenJie 
// 编写日期：2016/10/03
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流节点类型
    /// </summary>
    [Description("关联节点类型")]
    public enum WorkflowNodeType
    {
        /// <summary>
        /// 根节点
        /// </summary>
        [Description("根节点")]
        Root = 0,

        /// <summary>
        /// 父分类节点
        /// </summary>
        [Description("父分类节点")]
        ParentCategory = 1,

        /// <summary>
        /// 子分类节点
        /// </summary>
        [Description("子分类节点")]
        ChildCategory = 2,

        /// <summary>
        /// 工作流
        /// </summary>
        [Description("工作流")]
        Workflow = 3,

        /// <summary>
        /// 工作流步骤
        /// </summary>
        [Description("工作流步骤")]
        WorkflowProcess = 4
    }
}
