//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： RecommendType.cs
// 描述： 查询推荐类型
// 作者：ChenJie 
// 编写日期：2020-05-08
// 版权所有 (C) 四川大学 2020
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 查询推荐类型
    /// </summary>
    public enum RecommendType
    {
        /// <summary>
        /// 私有类型
        /// </summary>
        [Description("私有类型")]
        Private = 0,

        /// <summary>
        /// 共享类型
        /// </summary>
        [Description("共享类型")]
        Shared = 1
    }
}
