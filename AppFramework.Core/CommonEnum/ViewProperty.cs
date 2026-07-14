//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ViewProperty.cs
// 描述： 视图属性
// 作者：ChenJie 
// 编写日期：2018-03-05
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 视图属性
    /// </summary>
    public enum ViewProperty
    {
        /// <summary>
        /// 用户视图
        /// </summary>
        [Description("用户视图")]
        User = 0,

        /// <summary>
        /// 填报视图
        /// </summary>
        [Description("业务视图")]
        Business = 1,

        /// <summary>
        /// 单位视图
        /// </summary>
        [Description("单位视图")]
        Department = 2
    }
}
