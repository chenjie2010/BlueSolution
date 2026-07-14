//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： TableFilter.cs
// 描述： 表选择类型枚举
// 作者：ChenJie 
// 编写日期：2018/01/10
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表选择类型枚举
    /// </summary>
    [Description("表选择类型枚举")]
    public enum TableFilter
    {
        /// <summary>
        /// 全部类型表
        /// </summary>
        [Description("全部类型表")]
        All = 0,

        /// <summary>
        /// 显示系统表
        /// </summary>
        [Description("显示系统表")]
        System = 1,

        /// <summary>
        /// 显示主表
        /// </summary>
        [Description("显示主表")]
        PrimaryTable = 2,

        /// <summary>
        /// 显示主从表
        /// </summary>
        [Description("显示主从表")]
        MasterSlaveTable = 3
    }
}
