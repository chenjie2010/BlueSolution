//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： NextTableRelation.cs
// 描述： 与下一字段关系
// 作者：ChenJie 
// 编写日期：2018/08/25
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表链接时字段关系
    /// </summary>
    [Description("表链接时字段关系")]
    public enum NextTableRelation
    {
        /// <summary>
        /// 与
        /// </summary>
        [Description("与")]
        And = 0,

        /// <summary> 
        /// 或
        /// </summary>
        [Description("或")]
        Or = 1,

        /// <summary> 
        /// 无
        /// </summary>
        [Description("无")]
        None = 2
    }
}
