//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataQueriedType.cs
// 描述： 查询表格类型
// 作者：ChenJie 
// 编写日期：2017-10-30
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 系统条件
    /// </summary>
    public enum SystemCondition
    {

        /// <summary>
        /// 用户条件
        /// </summary>
        [Description("用户条件")]
        User = 1,


        /// <summary>
        /// 单位名称条件
        /// </summary>
        [Description("单位名称条件")]
        DepId = 2,

        /// <summary>
        /// 单位属性条件
        /// </summary>
        [Description("单位属性条件")]
        DepartmentProperty = 4,

        /// <summary>
        /// 用户类型条件
        /// </summary>
        [Description("用户类型条件")]
        UserTypeId = 8
    }
}
