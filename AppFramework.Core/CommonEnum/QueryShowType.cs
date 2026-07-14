//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： QueryShowType.cs
// 描述： 查询显示类型
// 作者：ChenJie 
// 编写日期：2019-06-10
// 版权所有 (C) 四川大学 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 查询显示类型
    /// </summary>
    public enum QueryShowType
    {
        /// <summary>
        /// 基础查询
        /// </summary>
        [Description("基础查询")]
        BasicQuery = 0,

        /// <summary>
        /// 基础查询
        /// </summary>
        [Description("高级查询")]
        AdvancedQuery = 1
    }
}
