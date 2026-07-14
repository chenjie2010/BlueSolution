//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataAuthorityType.cs
// 描述： 数据权限类型
// 作者：ChenJie 
// 编写日期：2018-09-04
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据权限类型
    /// </summary>
    [Description("数据权限类型")]
    public enum DataAuthorityType
    {
        /// <summary>
        /// 填报业务类型
        /// </summary>
        [Description("填报业务类型")]
        Business = 0,

        /// <summary>
        /// 数据审核类型
        /// </summary>
        [Description("数据审核类型")]
        Auditing = 1,

        /// <summary>
        /// 数据查询类型
        /// </summary>
        [Description("数据查询类型")]
        Query = 2
    }
}
