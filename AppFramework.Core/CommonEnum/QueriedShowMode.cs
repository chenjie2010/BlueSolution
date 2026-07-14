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
    public enum QueriedShowMode
    {
        /// <summary>
        /// 表格
        /// </summary>
        [Description("表格")]
        Table = 1,

        /// <summary>
        /// 饼状图
        /// </summary>
        [Description("饼状图")]
        Pie = 2,

        /// <summary>
        /// 柱状图
        /// </summary>
        [Description("柱状图")]
        Bar = 3,

        /// <summary>
        /// 扩展模式
        /// </summary>
        [Description("扩展模式")]
        Expand = 11,

        /// <summary>
        /// 查询
        /// </summary>
        [Description("查询")]
        Query = 31
    }
}
