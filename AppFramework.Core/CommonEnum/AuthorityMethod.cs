//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AuthorityMethod.cs
// 描述： 审核状态类型
// 作者：ChenJie 
// 编写日期：2019-01-03
// 版权所有 (C) 四川大学 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 审核状态类型
    /// </summary>
    [Description("审核状态类型")]
    public enum AuthorityMethod
    {
        /// <summary>
        ///  追加授权
        /// </summary>
        [Description("追加授权")]
        Append = 0,

        /// <summary>
        ///  重置授权
        /// </summary>
        [Description("重置授权")]
        Update = 1
    }
}
