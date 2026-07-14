//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WorkflowType.cs
// 描述： 工作流类型
// 作者：ChenJie 
// 编写日期：2017/12/06
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流类型
    /// </summary>
    [Description("工作流类型")]
    public enum WorkflowType
    {
        /// <summary>
        /// 复用工作流
        /// </summary>
        [Description("复用工作流")]
        Common = 1,

        /// <summary>
        /// 单次工作流
        /// </summary>
        [Description("单次工作流")]
        Single = 2
    }
}
