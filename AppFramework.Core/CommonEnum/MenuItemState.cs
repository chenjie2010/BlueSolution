//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ButtonState.cs
// 描述： 按钮状态枚举
// 作者：ChenJie 
// 编写日期：2016/08/18
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 按钮状态枚举
    /// </summary>
    public enum MenuItemState
    {
        /// <summary>
        /// 初始化状态
        /// </summary>
        Init = 0,

        /// <summary>
        /// 增加状态
        /// </summary>
        AllowToAdd = 1,

        /// <summary>
        /// 编辑状态
        /// </summary>
        AllowToEdit = 2,

        /// <summary>
        /// 全部禁止状态
        /// </summary>
        Disabled = 3
    }
}
