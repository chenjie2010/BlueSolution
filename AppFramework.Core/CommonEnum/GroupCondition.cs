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
    /// 分组条件
    /// </summary>
    public enum GroupCondition
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户")]
        UserId = 1,

        /// <summary>
        /// 单位名称
        /// </summary>
        [Description("单位名称")]
        DepId = 2,

        /// <summary>
        /// 单位属性
        /// </summary>
        [Description("单位属性")]
        DepartmentProperty = 4,

        /// <summary>
        /// 用户类型
        /// </summary>
        [Description("用户类型")]
        UserTypeId = 8


    }
}
