//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataAuditingProcess.cs
// 描述： 用户数据审核流程
// 作者：ChenJie 
// 编写日期：2018-10-11
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 用户数据审核流程
    /// </summary>
    public enum DataAuditingProcess
    {
        /// <summary>
        /// 单位初审
        /// </summary>
        [Description("启用初审")]
        InitReview = 0,

        /// <summary>
        /// 记录分配
        /// </summary>
        [Description("启用分配")]
        Allocation = 1,

        /// <summary>
        /// 单位初审
        /// </summary>
        [Description("启用终审")]
        FinalReview = 2

    }
}
