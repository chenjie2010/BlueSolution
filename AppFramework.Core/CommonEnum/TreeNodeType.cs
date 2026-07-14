//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： TreeNodeType.cs
// 描述： 含一层分类的节点类型
// 作者：ChenJie 
// 编写日期：2016/08/21
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;


namespace AppFramework.Core
{
    /// <summary>
    /// 含一层分类的节点类型
    /// </summary>
    public enum TreeNodeType
    {
        /// <summary>
        /// 根节点
        /// </summary>
        Root = 0,

        /// <summary>
        /// 分类节点
        /// </summary>
        Category = 1,

        /// <summary>
        /// 叶子节点
        /// </summary>
        Leaf = 2
    }
}
