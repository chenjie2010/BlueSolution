//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SystemDataField.cs
// 描述： 系统字段枚举
// 作者：ChenJie 
// 编写日期：2016/09/20
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 系统字段
    /// 系统字段：该系统字段用户可见，一部分来自业务表的系统字段，另外一部分来自系统表的系统字段
    /// </summary>
    [Description("系统字段")]
    public enum SystemDataField
    {
        /// <summary>
        /// 审核状态
        /// </summary>
        [Description("审核状态")]
        AuditedStatus = 1,

        /// <summary>
        /// 记录状态
        /// </summary>
        [Description("记录状态")]
        CurrentState = 2,
        
        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        UserName = 3,

        /// <summary>
        /// 姓名
        /// </summary>
        [Description("姓名")]
        UserActualName = 4,

        /// <summary>
        /// 用户类型名称
        /// </summary>
        [Description("用户类型名称")]
        UserTypeName = 5,

        /// <summary>
        /// 用户类型编码
        /// </summary>
        [Description("用户类型编码")]
        UserTypeCode = 6,

        /// <summary>
        /// 用户单位名称
        /// </summary>
        [Description("用户单位名称")]
        DepName = 7,
        
        /// <summary>
        /// 单位编码
        /// </summary>
        [Description("单位编码")]
        DepCode = 8,

        /// <summary>
        /// 单位值
        /// </summary>
        [Description("单位值")]
        DepValue = 9,

        /// <summary>
        /// 单位属性
        /// </summary>
        [Description("单位属性")]
        DepProperty = 10,

        /// <summary>
        /// 单位附加值一
        /// </summary>
        [Description("单位附加值一")]
        DepFstAdditionalCode = 11,

        /// <summary>
        /// 单位附加值二
        /// </summary>
        [Description("单位附加值二")]
        DepScdAdditionalCode = 12,

        /// <summary>
        /// 记录增加日期
        /// </summary>
        [Description("记录增加日期")]
        CreationTime = 13,

        /// <summary>
        /// 记录修改日期
        /// </summary>
        [Description("记录修改日期")]
        ModificationTime = 14
    }
}
