//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataShowMode.cs
// 描述： 数据填报展现模式
// 作者：ChenJie 
// 编写日期：2018/02/11
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据填报展现模式
    /// </summary>
    [Description("数据填报展现模式")]
    public enum DataShowMode
    {
        /// <summary>
        /// 步骤模式
        /// </summary>
        [Description("步骤模式")]
        Step = 1,

        /// <summary>
        /// 卡片模式
        /// </summary>
        [Description("卡片模式")]
        Tab = 2
    }
}
