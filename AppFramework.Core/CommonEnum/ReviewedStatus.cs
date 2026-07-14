//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ReviewedStatus.cs
// 描述： 工作流审核状态枚举
// 作者：ChenJie 
// 编写日期：2018/03/19
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流审核状态枚举
    /// </summary>
    public enum ReviewedStatus
    {
        /// <summary>
        /// 挂起
        /// </summary>
        [Description("挂起")]
        Pending = 0,

        /// <summary>
        /// 待处理
        /// </summary>
        [Description("待处理")]
        Reviewing = 1,

        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Completed = 2
    }
}
