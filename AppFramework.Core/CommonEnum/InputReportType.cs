//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： InputReportType.cs
// 描述： 录入报表类型
// 作者：ChenJie 
// 编写日期：2018-09-30
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表格类型
    /// </summary>
    public enum InputReportType
    {
        /// <summary>
        /// 个人数据报表
        /// </summary>
        [Description("个人数据报表")]
        Basic = 0,          

        /// <summary>
        /// 通用报表
        /// </summary>
        [Description("通用数据报表")]
        Common = 1
    }
}
