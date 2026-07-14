//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SoftwareVersion.cs
// 描述： 软件版本枚举类
// 作者：ChenJie 
// 编写日期：2016-07-28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 软件版本枚举类
    /// </summary>
    [Description("自定义 Bool 类型枚举")]
    public enum SoftwareVersion
    {
        /// <summary>
        /// 未知版本
        /// </summary>
        [Description("未知版本")]
        UnKown = 0,

        /// <summary>
        /// 试用版本
        /// </summary>
        [Description("试用版本")]
        Trial = 1,

        /// <summary>
        /// 专业版
        /// </summary>
        [Description("专业版")]
        Professional = 2,

        /// <summary>
        /// 旗舰版
        /// </summary>
        [Description("旗舰版")]
        Ultimate = 3,

        /// <summary>
        /// 定制版本
        /// </summary>
        [Description("定制版本")]
        Customized = 4
    }
}
