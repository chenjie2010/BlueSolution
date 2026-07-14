//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AdditionalPhysicalDataField.cs
// 描述： 系统附加物理字段枚举
// 作者：ChenJie 
// 编写日期：2018/02/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 系统附加物理字段枚举
    /// 系统物理字段：业务表下所有的系统字段
    /// </summary>
    public enum AdditionalPhysicalDataField
    {
        /// <summary>
        /// 业务状态类型 BusinessStatus tinyint NULL
        /// </summary>
        [Description("业务状态")]
        BusinessStatus = 1,

        /// <summary>
        /// 业务备用编号 BusinessAlternativeId decimal(10,0) NULL
        /// </summary>
        [Description("备用编号")]
        BusinessAlternativeId = 2,

        /// <summary>
        /// 业务备用枚举类型 BusinessAlternativeStatus tinyint NULL
        /// </summary>
        [Description("备用枚举")]
        BusinessAlternativeStatus = 3,

        /// <summary>
        /// 业务备用布尔类型 BusinessVisible bit NULL
        /// </summary>
        [Description("备用布尔")]
        BusinessVisible = 4,

        /// <summary>
        /// 业务备用时间 BusinessTime datetime NULL
        /// </summary>
        [Description("备用时间")]
        BusinessTime = 5,        
    }
}
