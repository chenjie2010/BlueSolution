//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： PlatformState.cs
// 描述： 平台枚举
// 作者：ChenJie 
// 编写日期：2016-07-4
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 平台枚举
    /// </summary>
    public enum PlatformState
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown,

        /// <summary>
        /// WebForms 系统
        /// </summary>
        WebForms = 1,

        /// <summary>
        /// Windows Forms 系统
        /// </summary>
        WinForms = 2
    }
}
