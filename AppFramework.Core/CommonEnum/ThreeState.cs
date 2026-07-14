//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ThreeState.cs
// 描述： 自定义三种状态
// 作者：ChenJie 
// 编写日期：2018-12-05
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 自定义三种状态
    /// </summary>
    [Description("自定义三种状态")]
    public enum ThreeState
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("第一态")]
        First = 0,

        /// <summary>
        /// 
        /// </summary>
        [Description("第二态")]
        Second = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("第三态")]
        Third = 2
    }
}
