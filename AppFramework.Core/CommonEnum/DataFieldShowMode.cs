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
    /// 字段展示模式
    /// </summary>
    public enum DataFieldShowMode
    {
        /// <summary>
        /// 数据仓库
        /// </summary>
        [Description("数据仓库")]
        DataWarehouse = 1,

        /// <summary>
        /// 逻辑数据库
        /// </summary>
        [Description("逻辑数据库")]
        Database = 2
    }
}
