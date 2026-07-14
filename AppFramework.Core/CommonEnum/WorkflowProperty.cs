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
    /// 工作流属性
    /// </summary>
    [Description("工作流属性")]
    public enum WorkflowProperty
    {
        /// <summary>
        /// 步骤界面
        /// </summary>
        [Description("步骤界面")]
        Node = 0,

        /// <summary>
        /// 图形界面
        /// </summary>
        [Description("图形界面")]
        Graphic = 1
    }
}
