//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MovedDriection.cs
// 描述： 节点移动方向枚举类
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 树形节点移动方向
    /// </summary>
    public enum MovedDriection
    {
        /// <summary>
        /// 移动到顶端
        /// </summary>
        Top,

        /// <summary>
        /// 向上移动
        /// </summary>
        Previous,

        /// <summary>
        /// 向下移动
        /// </summary>
        Next,

        /// <summary>
        /// 移动到底部
        /// </summary>
        Bottom
    }
}
