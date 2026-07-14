//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldSetting.cs
// 描述： 字段设置
// 作者：ChenJie 
// 编写日期：2018-09-01
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 字段设置
    /// </summary>
    [Description("字段设置")]
    public enum DataFieldSetting
    {
        /// <summary>
        /// 字段联动更新：该字段的值更改，关联的字段的值一并更改。
        /// </summary>
        [Description("字段联动更新")]
        Correlation = 1,

        /// <summary>
        /// 角色条件字段
        /// </summary>
        [Description("角色条件字段")]
        RoleUnderCondition = 2,

        /// <summary>
        /// 触发工作流
        /// </summary>
        [Description("触发器字段")]
        TriggerDataFiled = 3
    }
}
