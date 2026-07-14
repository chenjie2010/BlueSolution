//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataExchangeMode.cs
// 描述： 数据交换方式
// 作者：ChenJie 
// 编写日期：2018-07-18
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据交换方式
    /// </summary>
    [Description("数据交换方式")]
    public enum DataExchangeMode
    {
        /// <summary>
        /// 数据导出
        /// </summary>
        [Description("数据导出")]
        Export = 0,

        /// <summary>
        /// 数据导入
        /// </summary>
        [Description("数据导入")]
        Import = 1,

        /// <summary>
        /// 导出模板
        /// </summary>
        [Description("导出模板")]
        Template =2
    }
}
