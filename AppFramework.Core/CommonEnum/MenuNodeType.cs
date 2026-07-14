//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MenuNodeType.cs
// 描述： 菜单节点类型
// 作者：ChenJie 
// 编写日期：2018/01/16
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 菜单节点类型
    /// </summary>
    public enum MenuNodeType
    {
        /// <summary>
        /// 类型
        /// </summary>
        [Description("类型")]
        Category = 0,

        /// <summary>
        /// 目录
        /// </summary>
        [Description("目录")]
        Catalog = 1,

        /// <summary>
        /// 分类
        /// </summary>
        [Description("分类")]
        Class = 2,

        /// <summary>
        /// 业务
        /// </summary>
        [Description("业务")]
        Bussiness =3
    }
}
