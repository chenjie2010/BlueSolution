//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonNodeProperty.cs
// 描述： 节点属性
// 作者：ChenJie 
// 编写日期：2016/09/23
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 节点属性
    /// </summary>
    [Description("基础类型字段")]
    public enum CommonNodeProperty
    {
        /// <summary>
        /// 节点编号
        /// </summary>
        [Description("节点编号")]
        Id = 0,

        /// <summary>
        /// 节点名称
        /// </summary>
        [Description("节点名称")]
        Name = 1,

        /// <summary>
        /// 节点值
        /// </summary>
        [Description("节点值")]
        Value = 2
    }
}
