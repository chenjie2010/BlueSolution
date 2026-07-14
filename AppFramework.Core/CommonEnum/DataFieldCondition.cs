//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldCondition.cs
// 描述： DataFieldCondition 字段条件枚举
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 字段条件
    /// </summary>
    [Description("字段条件")]
    public enum DataFieldCondition
    {
        /// <summary>
        ///  无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        ///  大于
        /// </summary>
        [Description("大于")]
        More = 1,

        /// <summary>
        /// 大于或者等于
        /// </summary>
        [Description("大于或者等于")]
        MoreOrEqual =  2,

        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        Less = 3,

        /// <summary>
        /// 小于或等于
        /// </summary>
        [Description("小于或等于")]
        LessOrEqual = 4,

        /// <summary>
        /// 等于
        /// </summary>
        [Description("等于")]
        Equal = 5,

        /// <summary>
        ///  相似
        /// </summary>
        [Description("相似")]
        Like = 6,

        /// <summary>
        ///  开始匹配
        /// </summary>
        [Description("开始匹配")]
        StartWith = 7,

        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        Not = 8,
    }
}
