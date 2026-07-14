//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AssociatedDataFieldCategory.cs
// 描述： 关联表字段类别
// 作者：ChenJie 
// 编写日期：2017/11/11
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 关联表字段类别
    /// </summary>
    [Description("关联表字段类别|主|从")]
    public enum AssociatedDataFieldCategory
    {
        /// <summary>
        /// 主字段类型
        /// </summary>
        [Description("主关联字段")]
        PrimaryDataField = 1,

        /// <summary>
        /// 从关联字段
        /// </summary>
        [Description("从关联字段")]
        AssociatedDataField = 2
    }
}
