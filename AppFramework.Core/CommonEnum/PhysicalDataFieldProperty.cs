//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldCategory.cs
// 描述： 物理字段分类枚举
// 作者：ChenJie 
// 编写日期：2016/09/25
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 物理字段属性
    /// </summary>
    public enum PhysicalDataFieldProperty
    {
        /// <summary>
        /// 必填
        /// </summary>
        [Description("必填")]
        Required = 0,

        /// <summary>
        /// 索引
        /// </summary>
        [Description("索引")]
        Index = 1
    }
}
