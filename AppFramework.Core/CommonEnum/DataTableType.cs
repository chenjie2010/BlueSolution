//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSorting.cs
// 描述: CustomSorting 枚举类
// 作者：ChenJie 
// 编写日期：2016-09-17
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表的类型
    /// </summary>
    [Description("表的类型")]
    public enum DataTableType
    {
        /// <summary>
        /// 主表
        /// </summary>
        [Description("主表")]
        PrimaryTable = 1,

        /// <summary>
        /// 从表
        /// </summary>
        [Description("从表")]
        AssistanTable = 2,

        /// <summary>
        /// 主从表（仅一条是当前状态，其他的都是既往状态（增加时变化））
        /// </summary>
        [Description("主从表")]
        MasterSlaveTable = 3
    }
}
