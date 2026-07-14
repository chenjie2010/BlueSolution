//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ConditionType.cs
// 描述： 窗体表格类型
// 作者：ChenJie 
// 编写日期：2018-08-15
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 条件范围
    /// </summary>
    public enum ConditionType
    {
        /// <summary>
        /// 数据表
        /// </summary>
        [Description("数据表")]
        Table = 1,

        /// <summary>
        /// 视图
        /// </summary>
        [Description("视图")]
        View = 2 
    }
}
