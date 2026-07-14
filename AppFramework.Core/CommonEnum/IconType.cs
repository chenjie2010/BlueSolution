//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IconType.cs
// 描述： 图标类型
// 作者：ChenJie 
// 编写日期：2018-01-24
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 图标类型
    /// </summary>
    public enum IconType
    {
        /// <summary>
        /// 系统预置
        /// </summary>
        [Description("系统预置")]
        System = 0,

        /// <summary>
        /// 用户自定义
        /// </summary>
        [Description("用户自定义")]
        Custom = 1
    }
}
