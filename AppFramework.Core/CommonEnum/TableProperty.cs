//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： TableProperty.cs
// 描述： 表格属性
// 作者：ChenJie 
// 编写日期：2019-06-10
// 版权所有 (C) 四川大学 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表格属性：数据表，单位表，用户类型表，业务表
    /// </summary>
    public enum TableProperty
    {
        /// <summary>
        /// 数据表
        /// </summary>
        [Description("数据表")]
        Data = 0,

        /// <summary>
        /// 单位表
        /// </summary>
        [Description("单位表")]
        Department = 1,

        /// <summary>
        /// 用户类型表
        /// </summary>
        [Description("用户类型表")]
        UserType = 2,

        /// <summary>
        /// 业务表
        /// </summary>
        [Description("业务表")]
        Business = 3
    }
}
