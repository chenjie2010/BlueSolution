//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： GroupCondition.cs
// 描述： 分组条件
// 作者：ChenJie 
// 编写日期：2017-10-30
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 查询条件展示模式
    /// </summary>
    public enum CommonShowMode
    {
        /// <summary>
        /// 完整模式：子类的值在 1 - 9 之间
        /// </summary>
        [Description("完整模式")]
        Completed = 10,

        /// <summary>
        /// 自定义模式：子类的值在 11 - 19 之间
        /// </summary>
        [Description("简洁模式")]
        Clean = 20,

        /// <summary>
        /// 定制模式：子类的值在 21 - 79 之间
        /// </summary>
        [Description("定制模式")]
        Exclusive = 80
    }
}
