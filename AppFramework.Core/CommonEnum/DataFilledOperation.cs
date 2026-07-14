//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFilledOperation.cs
// 描述： 填报操作
// 作者：ChenJie 
// 编写日期：2018/03/05
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 填报操作
    /// </summary>
    [Description("填报操作")]
    public enum DataFilledOperation
    {
        /// <summary>
        /// 查看
        /// </summary>
        [Description("查看")]
        View = 1,

        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        Edit = 2,

        /// <summary>
        /// 单次填报
        /// </summary>
        [Description("删除")]
        Delte = 3,

        /// <summary>
        /// 撤回
        /// </summary>
        [Description("撤回")]
        Withdraw = 4
    }
}
