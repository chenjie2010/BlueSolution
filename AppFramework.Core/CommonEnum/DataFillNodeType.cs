//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CombinedNodeType.cs
// 描述： 含两层分类的节点类型
// 作者：ChenJie 
// 编写日期：2016/10/03
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 关联节点类型
    /// </summary>
    [Description("关联节点类型")]
    public enum DataFillNodeType
    {
        /// <summary>
        /// 根节点
        /// </summary>
        [Description("根节点")]
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
        /// 数据填报
        /// </summary>
        [Description("数据填报")]
        CustomData = 3,

        /// <summary>
        /// 数据窗体
        /// </summary>
        [Description("数据窗体")]
        CustomForm = 4,

        /// <summary>
        /// 数据业务
        /// </summary>
        [Description("数据业务")]
        CustomTable = 5
    }
}
