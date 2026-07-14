//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： BusinessType.cs
// 描述： 菜单业务类型
// 作者：ChenJie 
// 编写日期：2017-12-18
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 菜单业务类型
    /// </summary>
    public enum BusinessType
    {
        /// <summary>
        /// 个人信息
        /// </summary>
        [Description("个人信息")]
        PersonalData = 0,

        /// <summary>
        /// 信息审核
        /// </summary>
        [Description("信息审核")]
        DataAuditing = 2,

        /// <summary>
        /// 常用业务
        /// </summary>
        [Description("常用业务")]
        CommonBusiness = 3,

        /// 我的工作
        /// </summary>
        [Description("我的工作")]
        MyWork = 4,

        /// <summary>
        /// 填报业务
        /// </summary>
        [Description("填报业务")]
        UserData = 5,

        /// <summary>
        /// 填报审核
        /// </summary>
        [Description("填报审核")]
        Auditing = 6,

        /// <summary>
        /// 数据查询
        /// </summary>
        [Description("数据查询")]
        DataQuery = 7,

        /// <summary>
        /// 报表查询
        /// </summary>
        [Description("报表查询")]
        Report = 8,

        /// <summary>
        /// 数据业务
        /// </summary>
        [Description("数据业务")]
        DataBussiness = 9,
        
        /// <summary>
        /// 自定义类型
        /// </summary>
        [Description("自定义类型")]
        Custom = 9
    }
}
