//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WorkflowProcessCategory.cs
// 描述： 工作流节点类别
// 作者：ChenJie 
// 编写日期：2018/08/23
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流节点类别
    /// </summary>
    [Description("工作流节点类别")]
    public enum WorkflowProcessCategory
    {
        /// <summary>
        /// 开始节点
        /// </summary>
        [Description("开始节点")]
        Begin = 0,

        /// <summary>
        /// 中间节点
        /// </summary>
        [Description("中间节点")]
        Middle = 1,
        
        /// <summary>
        /// 动态节点
        /// </summary>
        [Description("结束节点")]
        End = 2
                    
    }
}