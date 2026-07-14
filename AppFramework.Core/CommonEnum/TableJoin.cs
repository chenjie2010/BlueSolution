//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： TableJoin.cs
// 描述： TableJoin 表之间的链接关系
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表链接关系
    /// </summary>
    [Description("表链接关系")]
    public enum TableJoin
    {
        /// <summary>
        /// 内连接
        /// </summary>
        [Description("内连接")]
        InnerJoin = 0,

        /// <summary>
        /// 左外连接
        /// </summary>
        [Description("左外连接")]
        LeftOuterJoin = 1,

        /// <summary>
        /// 右外连接
        /// </summary>
        [Description("右外连接")]
        RightOuterJoin = 2,

        /// <summary>
        /// 全外连接
        /// </summary>
        [Description("全外连接")]
        FullOuterJoin = 3

    }
}
