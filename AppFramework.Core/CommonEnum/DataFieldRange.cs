//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldRange.cs
// 描述： DataFieldRange 字段范围
// 作者：ChenJie 
// 编写日期：2017/12/25
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 字段的条件
    /// </summary>
    public enum DataFieldRange
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("等于")]
        Equal = 1,

        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        Not = 2,

        /// <summary>
        ///  大于
        /// </summary>
        [Description("大于")]
        More = 3,

        /// <summary>
        /// 大于或者等于
        /// </summary>
        [Description("大于或者等于")]
        MoreOrEqual = 4,

        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        Less = 5,

        /// <summary>
        /// 小于或等于
        /// </summary>
        [Description("小于或等于")]
        LessOrEqual = 6,

        /// <summary>
        ///  相似
        /// </summary>
        [Description("相似")]
        Like = 7,

        /// <summary>
        ///  匹配开始
        /// </summary>
        [Description("匹配开始")]
        StartsWith = 8,

        /// <summary>
        ///  匹配结尾
        /// </summary>
        [Description("匹配结尾")]
        EndsWith = 9
    }
}
