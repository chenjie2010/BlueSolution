//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： NodeRelationship.cs
// 描述： 工作流节点关系
// 作者：ChenJie 
// 编写日期：2018-06-15
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流节点关系
    /// </summary>
    public enum NodeRelationship
    {
        /// <summary>
        /// 一对一
        /// </summary>
        [Description("一对一")]
        OneToOne = 1,

        /// <summary>
        /// 一对多
        /// </summary>
        [Description("一对多")]
        OneToMany = 2,

        /// <summary>
        /// 多对一
        /// </summary>
        [Description("多对一")]
        ManyToOne = 3
    }
}
