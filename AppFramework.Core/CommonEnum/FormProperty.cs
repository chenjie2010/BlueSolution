//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： FormType.cs
// 描述： 表格类型
// 作者：ChenJie 
// 编写日期：2017-10-31
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表格属性
    /// </summary>
    public enum FormProperty
    {
        /// <summary>
        /// 启用源数据对比
        /// </summary>
        [Description("启用源数据对比")]
        OriginalDataCompared = 0,

        /// <summary>
        /// 撤销源数据变更
        /// </summary>
        [Description("撤销源数据变更")]
        OriginalDataRollbacked = 1

    }
}
