//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WorkflowHandlerType.cs
// 描述： 工作流处理类型
// 作者：ChenJie 
// 编写日期：2017/12/06
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流处理类型
    /// </summary>
    [Description("工作流处理配置")]
    public enum WorkflowProcessSetting
    {
        /// <summary>
        /// 跳过
        /// </summary>
        [Description("跳过")]
        SkipAllowed = 0,

        /// <summary>
        /// 超时自动通过
        /// </summary>
        [Description("超时自动通过")]
        AutoPass = 1,

        /// <summary>
        /// 终止
        /// </summary>
        [Description("终止")]
        AbortAllowed = 2,

        /// <summary>
        /// 驳回提交人
        /// </summary>
        [Description("驳回提交人")]
        Reject = 3,

        /// <summary>
        /// 模板意见
        /// </summary>
        [Description("模板意见")]
        Template = 4
    }
}
