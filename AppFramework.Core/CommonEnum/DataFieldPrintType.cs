//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldPrintType.cs
// 描述： 字段打印类型
// 作者：ChenJie 
// 编写日期：2019-02-02
// 版权所有 (C) 四川大学 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 字段打印类型
    /// </summary>
    [Description("字段打印类型")]
    public enum DataFieldPrintType
    {
        /// <summary>
        ///  数据单元格
        /// </summary>
        [Description("数据单元格")]
        DefalutValue = 0,

        /// <summary>
        ///  自定义输入
        /// </summary>
        [Description("自定义输入")]
        CustomInput = 1
    }
}
