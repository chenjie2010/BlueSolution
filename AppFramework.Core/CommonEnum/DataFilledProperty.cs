//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFilledProperty.cs
// 描述： 数据填报属性
// 作者：ChenJie 
// 编写日期：2018-02-19
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据填报属性
    /// </summary>
    public enum DataFilledProperty
    {
        /// <summary>
        /// 单用户模式
        /// </summary>
        [Description("单用户模式")]
        SingleUser = 1,

        /// <summary>
        /// 多用户模式
        /// </summary>
        [Description("多用户模式")]
        MultiUser = 2
    }
}
