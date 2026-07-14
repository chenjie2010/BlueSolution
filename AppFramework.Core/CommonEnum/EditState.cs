//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： EditState.cs
// 描述： 编辑状态枚举
// 作者：ChenJie 
// 编写日期：2016/08/18
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 编辑状态枚举
    /// </summary>
    public enum EditState
    {
        /// <summary>
        /// 未知状态
        /// </summary>
        None = 0,

        /// <summary>
        /// 增加状态
        /// </summary>
        Add = 1,

        /// <summary>
        /// 编辑状态
        /// </summary>
        Edit = 2
    }
}
