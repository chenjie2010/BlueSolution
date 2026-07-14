//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataAuditingType.cs
// 描述： 数据审核类型
// 作者：ChenJie 
// 编写日期：2018/09/08
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据审核类型
    /// </summary>
    [Description("数据审核类型")]
    public enum DataAuditingType
    {
        /// <summary>
        /// 个人信息审核
        /// </summary>
        [Description("个人信息审核")]
        Personal = 0,

        /// <summary>
        /// 分组信息审核
        /// </summary>
        [Description("分组信息审核")]
        Group = 1
    }
}
