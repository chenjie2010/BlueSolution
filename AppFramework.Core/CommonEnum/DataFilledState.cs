//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFilledState.cs
// 描述： 填报状态枚举
// 作者：ChenJie 
// 编写日期：2018/02/25
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 填报状态枚举
    /// </summary>
    public enum DataFilledState
    {
        /// <summary>
        /// 待填报
        /// </summary>
        [Description("待填报")]
        UnDataFilled = 0,

        /// <summary>
        /// 待提交
        /// </summary>
        [Description("待提交")]
        UnSumbitted = 1,

        /// <summary>
        /// 已提交
        /// </summary>
        [Description("已提交")]
        Sumbitted = 2,

        /// <summary>
        /// 已归档
        /// </summary>
        [Description("已归档")]
        IsArchived = 3
    }
}
