//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldAuthority.cs
// 描述： 字段权限
// 作者：ChenJie 
// 编写日期：2016/09/20
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 字段权限
    /// </summary>
    [Description("字段属性")]
    public enum DataFieldAuthority
    {
        /// <summary>
        /// 不可见
        /// </summary>
        [Description("不可见")]
        InVisible = 0,

        /// <summary>
        /// 只读
        /// </summary>
        [Description("只读")]
        ReadOnly = 1,

        /// <summary>
        /// 可读写
        /// </summary>
        [Description("可读写")]
        ReadAndWrite = 2
    }
}
