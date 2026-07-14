//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SystemConfigKeyName.cs
// 描述： 系统配置项
// 作者：ChenJie 
// 编写日期：2016-08-19
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 单位默认属性
    /// </summary>
    [Description("单位默认属性")]
    public enum DepartmentProperty
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 学院（文科）
        /// </summary>
        [Description("学院（文科）")]
        Humanities = 1,

        /// <summary>
        /// 学院（理科）
        /// </summary>
        [Description("学院（理科）")]
        Science = 2,

        /// <summary>
        /// 学院（工科）
        /// </summary>
        [Description("学院（工科）")]
        Engineering = 3,

        /// <summary>
        /// 学院（医科）
        /// </summary>
        [Description("学院（医科）")]
        Medicine = 4,

        /// <summary>
        /// 科研机构
        /// </summary>
        [Description("科研机构")]
        Institutions = 5,

        /// <summary>
        /// 业务单位
        /// </summary>
        [Description("业务单位")]
        Business = 6,

        /// <summary>
        /// 党群
        /// </summary>
        [Description("党群")]
        Party = 7,

        /// <summary>
        /// 行政
        /// </summary>
        [Description("行政")]
        Administrator = 8,

        /// <summary>
        /// 附设机构
        /// </summary>
        [Description("附设机构")]
        Addition = 9

    }
}
