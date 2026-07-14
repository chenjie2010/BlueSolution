//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： QueriedDataRange.cs
// 描述： 数据查询范围
// 作者：ChenJie 
// 编写日期：2017-10-30
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据查询范围
    /// </summary>
    public enum QueriedDataRange
    {
        /// 忽略用户类型范围
        /// </summary>
        [Description("忽略用户类型范围")]
        UserTypeExclusive = 1,

        /// <summary>
        /// 忽略单位管理范围
        /// </summary>
        [Description("忽略单位管理范围")]
        DepExclusive = 2,

        /// <summary>
        /// 忽略单位属性范围
        /// </summary>
        [Description("忽略单位属性范围")]
        DepProperty = 4
    }
}
