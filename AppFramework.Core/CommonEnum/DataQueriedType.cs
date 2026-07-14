//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataQueriedType.cs
// 描述： 查询类型
// 作者：ChenJie 
// 编写日期：2018-08-16
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 查询类型
    /// </summary>
    public enum DataQueriedType
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
        View = 2,

        /// <summary>
        /// 自定义查询
        /// </summary>
        [Description("自定义查询")]
        Custom = 3
    }
}
