//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldInnerRealtion.cs
// 描述： DataFieldInnerRealtion 字段连接关系枚举
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 查询字段之间的关系
    /// </summary>
    public enum DataFieldInnerRealtion
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        ///  与
        /// </summary>
        [Description("与")]
        And = 1,

        /// <summary>
        /// 或
        /// </summary>
        [Description("或")]
        Or = 2
    }
}
