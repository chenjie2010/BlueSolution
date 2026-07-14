//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CellSelectionStyle.cs
// 描述： 单元格选择样式
// 作者：ChenJie 
// 编写日期：2018/10/01
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 单元格选择
    /// </summary>
    public enum CellSelectionStyle
    {
        /// <summary>
        /// 行高
        /// </summary>
        [Description("行高")]
        RowHeight,

        /// <summary>
        /// 列宽
        /// </summary>
        [Description("列宽")]
        ColumnWidth,

        /// <summary>
        /// 设置单元格颜色
        /// </summary>
        [Description("设置单元格颜色")]
        CellColor,

        /// <summary>
        /// 清除文本内容
        /// </summary>
        [Description("清除文本内容")]
        ResetCellText,

        /// <summary>
        /// 自适应调整
        /// </summary>
        [Description("自适应调整")]
        Adjustment,

        /// <summary>
        /// 字体
        /// </summary>
        [Description("字体")]
        Font,

        /// <summary>
        /// 字体名称
        /// </summary>
        [Description("字体名称")]
        FontName,

        /// <summary>
        /// 字体大小
        /// </summary>
        [Description("字体大小")]
        FontSize,

        /// <summary>
        /// 粗体
        /// </summary>
        [Description("粗体")]
        FontBlod,

        /// <summary>
        /// 斜体
        /// </summary>
        [Description("斜体")]
        FontItalic,

        /// <summary>
        /// 下划线
        /// </summary>
        [Description("下划线")]
        FontUnderline,

        /// <summary>
        /// 左
        /// </summary>
        [Description("左")]
        Left,

        /// <summary>
        /// 水平居中
        /// </summary>
        [Description("水平居中")]
        Middle,

        /// <summary>
        /// 右
        /// </summary>
        [Description("右")]
        Right,

        /// <summary>
        /// 上
        /// </summary>
        [Description("上")]
        Top,

        /// <summary>
        /// 垂直居中
        /// </summary>
        [Description("垂直居中")]
        Center,

        /// <summary>
        /// 下
        /// </summary>
        [Description("下")]
        Bottom,

        /// <summary>
        /// 水平垂直居中
        /// </summary>
        [Description("水平垂直居中")]
        MiddleCenter,

        /// <summary>
        /// 自动换行
        /// </summary>
        [Description("自动换行")]
        AutoLine,

        /// <summary>
        /// 表框
        /// </summary>
        [Description("表框")]
        Broder,

        /// <summary>
        /// 表格线
        /// </summary>
        [Description("表格线")]
        Grid,

        /// <summary>
        /// 格式
        /// </summary>
        [Description("格式")]
        Format
    }
}
