//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： InstanceState.cs
// 描述： 数据填报状态状态枚举
// 作者：ChenJie 
// 编写日期：2018/02/25
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据填报状态状态枚举
    /// </summary>
    public enum InstanceState
    {
        /// <summary>
        /// 待提交
        /// </summary>
        [Description("待提交")]
        None = 0,

        /// <summary>
        /// 待初审
        /// </summary>
        [Description("待初审")]
        InitReview = 1,

        /// <summary>
        /// 待终审
        /// </summary>
        [Description("待终审")]
        FinalReview = 2,

        /// <summary>
        /// 初审终止
        /// </summary>
        [Description("初审终止")]
        InitReviewAborted = 3,

        /// <summary>
        /// 终审终止
        /// </summary>
        [Description("终审终止")]
        FinalReviewAborted = 4,

        /// <summary>
        /// 审核完成
        /// </summary>
        [Description("审核完成")]
        Completed = 5
    }
}
