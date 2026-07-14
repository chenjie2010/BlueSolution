using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 录入报表单元格类型
    /// </summary>
    [Description("录入报表单元格类型")]
    public enum InputCellType
    {
        /// <summary>
        /// 单个字段类型
        /// </summary>
        [Description("数据单元类型")]
        Single = 0,

        /// <summary>
        /// 行扩展类型
        /// </summary>
        [Description("行扩展类型")]
        ExtendRow = 1,

        /// <summary>
        /// 列扩展类型
        /// </summary> 
        [Description("列扩展类型")]
        ExtendCol = 2,

        /// <summary>
        /// 条件行扩展类型
        /// </summary>
        [Description("条件行扩展类型")]
        ExtendRowByCondtion = 3,

        /// <summary>
        /// 条件列扩展类型
        /// </summary> 
        [Description("条件列扩展类型")]
        ExtendColByCondtion = 4,

        /// <summary>
        /// 个人照片类型
        /// </summary>
        [Description("个人照片类型")]
        Photo = 5
    }
}
