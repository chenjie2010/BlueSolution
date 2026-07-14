//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AppointmentBussinesType.cs
// 描述： 业务类型
// 作者：ChenJie 
// 编写日期：2018-08-25
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 业务类型
    /// </summary>
    public enum AppointmentBussinesType
    {
        /// <summary>
        /// 数据填报类型
        /// </summary>
        [Description("数据填报类型")]
        DataFilled = 0,

        /// <summary>
        /// 工作流类型
        /// </summary>
        [Description("工作流类型")]
        Workflow = 1
    }
}
