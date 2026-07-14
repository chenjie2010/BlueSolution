using System;
using System.ComponentModel;

namespace AppFramework.WinFormsControls
{
    /// <summary>
    /// 文档类型
    /// </summary>
    [Description("文档类型")]
    public enum DocType
    {
        /// <summary>
        /// 任意类型
        /// </summary>
        [Description("任意类型")]
        ArbitraryAttachment = 0,

        /// <summary>
        /// 文件
        /// </summary>
        [Description("文件")]
        DocAttachment = 1,

        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        PicAttachment = 2,

        /// <summary>
        /// PDF文件
        /// </summary>
        [Description("PDF 文件")]
        PDFAttachment = 3
    }
}
