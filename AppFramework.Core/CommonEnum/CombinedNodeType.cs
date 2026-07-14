//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CombinedNodeType.cs
// 描述： 含两层分类的节点类型
// 作者：ChenJie 
// 编写日期：2016/08/21
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 含两层分类的节点类型
    /// </summary>
    public enum CombinedNodeType
    {
        /// <summary>
        /// 根节点
        /// </summary>
        Root = 0,

        /// <summary>
        /// 父分类节点
        /// </summary>
        ParentCategory = 1,

        /// <summary>
        /// 子分类节点
        /// </summary>
        ChildCategory = 2,

        /// <summary>
        /// 叶子节点
        /// </summary>
        Leaf = 3
    }
}
