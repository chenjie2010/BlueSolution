//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： TableRelation.cs
// 描述： TableRelation 表链接时字段关系
// 作者：ChenJie 
// 编写日期：2017/10/25
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表链接时字段关系
    /// </summary>
    [Description("表链接时字段关系")]
    public enum TableRelation
    {
        /// <summary>
        /// 主表
        /// </summary>
        [Description("主表")]
        Primary = 0,

        /// 上一个表
        /// </summary>
        [Description("上一个表")]
        Previous = 1
    }
}
