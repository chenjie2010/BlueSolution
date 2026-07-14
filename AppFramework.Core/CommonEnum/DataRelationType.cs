//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataRelationType.cs
// 描述： 数据导入类型
// 作者：ChenJie 
// 编写日期：2018/10/23
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 转入到目标表中后源数据方式
    /// </summary>
    [Description("数据导入类型")]
    public enum DataRelationType
    {
        /// <summary>
        /// 转入到目标表中后删除源表中对应的数据
        /// </summary>
        [Description("完成后删除源数据")]
        ImportAndDelete = 0,

        /// <summary>
        /// 转入到目标表中后保留源表中对应的数据
        /// </summary>
        [Description("完成后保留源数据")]
        OnlyImport = 1,

        /// <summary>
        /// 仅删除源表中对应的数据
        /// </summary>
        [Description("仅删除源数据")]
        OnlyDelete = 2            
    }
}
