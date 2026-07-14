//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFilledBusinessType.cs
// 描述： 数据填报业务类型
// 作者：ChenJie 
// 编写日期：2018/11/03
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据填报业务类型
    /// </summary>
    [Description("数据填报业务类型")]
    public enum DataFilledBusinessType
    {
        /// <summary>
        /// 待处理填报
        /// </summary>
        [Description("待处理填报")]
        DataAuditing = 0,

        /// <summary>
        /// 已处理数据填报
        /// </summary>
        [Description("已处理填报")]
        DataAudited = 1,

        /// <summary>
        /// 填报统计
        /// </summary>
        [Description("填报统计")]
        DataStatistics = 2
    }
}
