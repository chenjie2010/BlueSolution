//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： InfoStatus.cs
// 描述： 信息审核状态类型
// 作者：ChenJie 
// 编写日期：2018-10-21
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 信息审核状态类型
    /// </summary>
    [Description("审核状态类型")]
    public enum InfoStatus
    {
        /// <summary>
        ///  待审核
        /// </summary>
        [Description("待审核")]
        InfoAuditing = 0,
        
        /// <summary>
        ///  待分配
        /// </summary>
        [Description("待分配")]
        InfoAllocating = 1,

        /// <summary>
        ///  待终审
        /// </summary>
        [Description("待终审")]
        InfoAudited = 2
    }
}
