//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SQLServerFunction.cs
// 描述： SQL Server 函数枚举
// 作者：ChenJie 
// 编写日期：2017/07/20
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// SQL Server 函数枚举
    /// </summary>
    [Description("函数枚举")]
    public enum SQLServerFunction
    {
        [Description("最大值")]
        Max = 1,

        [Description("最小值")]
        Min = 0

    }
}
