//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AppointmentType.cs
// 描述： 预约类型
// 作者：ChenJie 
// 编写日期：2018-08-24
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 菜单业务类型
    /// </summary>
    public enum AppointmentType
    {
        /// <summary>
        /// 实时类型
        /// </summary>
        [Description("实时类型")]
        RealTime = 0,

        /// <summary>
        /// 定时类型
        /// </summary>
        [Description("定时类型")]
        Time = 1         

    }
}
