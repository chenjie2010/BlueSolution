//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataAuditingProperty.cs
// 描述： 用户数据审核属性
// 作者：ChenJie 
// 编写日期：2018-10-11
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 用户数据审核属性
    /// </summary>
    public enum DataAuditingProperty
    {
        /// <summary>
        /// 支持系统查询条件
        /// </summary>
        [Description("支持系统查询条件")]
        SystemConditions = 0    

    }
}
