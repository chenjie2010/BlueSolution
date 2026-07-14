//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ImportedMode.cs
// 描述： 导入模式
// 作者：ChenJie 
// 编写日期：2018-07-22
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 导入模式
    /// </summary>
    [Description("导入模式")]
    public enum ImportedMode
    {
        /// <summary>
        ///  导入全部
        /// </summary>
        [Description("导入全部")]
        Append = 0,

        /// <summary>
        ///  存在忽略，不存在导入
        /// </summary>
        [Description("存在忽略，不存在导入")]
        NotUpdateAndInsert = 1,

        /// <summary>
        ///  存在则更新，不存在则导入
        /// </summary>
        [Description("存在更新，不存在导入")]
        UpdateAndInsert = 2,

        /// <summary>
        ///  存在更新，不存在忽略
        /// </summary>
        [Description("存在更新，不存在忽略")]
        UpdateAndNotInsert = 3
    }
}
