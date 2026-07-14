//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： RowColCopyType.cs
// 描述： 行列复制类型
// 作者：ChenJie 
// 编写日期：2019-05-21
// 版权所有 (C) 四川大学 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 行列复制类型
    /// </summary>
    public enum RowColCopyType
    {
        /// <summary>
        /// 行复制
        /// </summary>
        [Description("行复制")]
        DuplicateRow = 0,

        /// <summary>
        /// 列替换
        /// </summary>
        [Description("列替换")]
        AlternativeCol = 1         

    }
}
