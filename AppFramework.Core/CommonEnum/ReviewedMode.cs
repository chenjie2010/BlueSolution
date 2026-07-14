//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AudittedMode
// 描述： 审核模式
// 作者：ChenJie 
// 编写日期：2018/02/16
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 审核模式
    /// </summary>
    [Description("审核模式")]
    public enum ReviewedMode
    {
        /// <summary>
        /// 初审模式
        /// </summary>
        [Description("初审模式")]
        FirstReview = 0,

        /// <summary>
        /// 终审模式
        /// </summary>
        [Description("终审模式")]
        FinalReview = 1            
    }
}
