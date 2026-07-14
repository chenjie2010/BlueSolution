//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldEditedState.cs
// 描述： 编辑状态枚举
// 作者：ChenJie 
// 编写日期：2018/09/08
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 编辑状态枚举
    /// </summary>
    public enum DataFieldEditedState
    {
        /// <summary>
        /// 无编辑
        /// </summary>
        [Description("无编辑")]
        None = 0,

        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        Edit = 1,

        /// <summary>
        /// 批量编辑
        /// </summary>
        [Description("批量编辑")]
        BathcEdit = 2,

        /// <summary>
        /// 全部编辑
        /// </summary>
        [Description("全部编辑")]
        CompleteEdit = 3
    }
}
