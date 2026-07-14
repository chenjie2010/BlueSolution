//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MatchingMode.cs
// 描述： 附件类型
// 作者：ChenJie 
// 编写日期：2018-04-13
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 匹配模式
    /// </summary>
    [Description("匹配模式")]
    public enum MatchingMode
    {
        /// <summary>
        ///  模糊查找
        /// </summary>
        [Description("模糊查找")]
        Like = 0,

        /// <summary>
        ///  精确查找
        /// </summary>
        [Description("精确查找")]
        Equal = 1

    }
}
