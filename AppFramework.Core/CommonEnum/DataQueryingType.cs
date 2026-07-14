//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataQueryingType.cs
// 描述： 数据查询类型
// 作者：ChenJie 
// 编写日期：2018/11/03
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据查询类型
    /// </summary>
    [Description("数据查询类型")]
    public enum DataQueryingType
    {
        /// <summary>
        /// 统计数据查询
        /// </summary>
        [Description("统计数据查询")]
        DataQuerying = 0
    }
}
