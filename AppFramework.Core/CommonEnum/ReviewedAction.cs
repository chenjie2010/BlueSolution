//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ReviewedState.cs
// 描述： 审核动作枚举
// 作者：ChenJie 
// 编写日期：2018/03/19
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 审核动作枚举
    /// </summary>
    public enum ReviewedAction
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 提交
        /// </summary>
        [Description("提交")]
        Sumbitted = 1,

        /// <summary>
        /// 撤回
        /// </summary>
        [Description("撤回")]
        WithDraw = 2,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        Pass = 3,

        /// <summary>
        /// 审核完成
        /// </summary>
        [Description("审核完成")]
        Compelete = 4,

        /// <summary>
        /// 驳回
        /// </summary>
        [Description("驳回")]
        Reject = 5,

        /// <summary>
        /// 终止
        /// </summary>
        [Description("终止")]
        Abort = 6
    }
}
