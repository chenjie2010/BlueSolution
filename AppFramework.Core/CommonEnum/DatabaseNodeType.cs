//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DatabaseNodeType.cs
// 描述： 系统配置项
// 作者：ChenJie 
// 编写日期：2016-09-11
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据库节点
    /// </summary>
    [Description("数据库节点")]
    public enum DatabaseNodeType
    {
        /// <summary>
        /// 数据仓库
        /// </summary>
        [Description("数据仓库")]
        DataWarehouse = 0,

        /// <summary>
        /// 数据库
        /// </summary>
        [Description("数据库")]
        Database = 1,

        /// <summary>
        /// 分类
        /// </summary>
        [Description("分类")]
        Category = 2,

        /// <summary>
        /// 数据表
        /// </summary>
        [Description("数据表")]
        Table = 3,

        /// <summary>
        /// 字段
        /// </summary>
        [Description("字段")]
        DataField = 4
    }
}
