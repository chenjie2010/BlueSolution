//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataSumbittedState.cs
// 描述： 编辑状态枚举
// 作者：ChenJie 
// 编写日期：2018/02/25
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 业务状态枚举
    /// </summary>
    public enum DataSumbittedState
    {
        /// <summary>
        /// 未提交
        /// </summary>
        [Description("未提交")]
        Drfat = 0,

        /// <summary>
        /// 审核中
        /// </summary>
        [Description("审核中")]
        Review = 1,        

        /// <summary>
        /// 审核完成
        /// </summary>
        [Description("审核完成")]
        Completed = 2
    }
}
