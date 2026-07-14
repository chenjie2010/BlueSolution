//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AbortedResult.cs
// 描述： 工作流终止结果
// 作者：ChenJie 
// 编写日期：2018-06-25
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;


namespace AppFramework.Core
{
    /// <summary>
    /// 导入样式
    /// </summary>
    [Description("导入样式")]
    public enum ExportStyle
    {
        /// <summary>
        ///  导入全部记录
        /// </summary>
        [Description("导入全部记录")]
        Append = 0,

        /// <summary>
        ///  记录存在则更新，不存在则导入
        /// </summary>
        [Description("记录存在则更新，不存在则导入")]
        UpdateAndInsert = 1,

        /// <summary>
        ///  记录存在则更新，不存在则忽略
        /// </summary>
        [Description("记录存在则更新，不存在则忽略")]
        UpdateAndNotInsert = 2,

        /// <summary>
        ///  记录不存在则插入，存在则忽略
        /// </summary>
        [Description("记录存在则忽略，不存在则导入")]
        NotUpdateAndInsert = 3
    }
}
