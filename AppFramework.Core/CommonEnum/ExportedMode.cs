//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ExportedMode.cs
// 描述： 导入模式
// 作者：ChenJie 
// 编写日期：2018-09-19
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 导出模式
    /// </summary>
    [Description("导出模式")]
    public enum ExportedMode
    {
        /// <summary>
        ///  导出Excel模式
        /// </summary>
        [Description("导出Excel模式")]
        Excel = 0,

        /// <summary>
        ///  导出PDF模式
        /// </summary>
        [Description("导出PDF模式")]
        PDF = 1
    }
}
