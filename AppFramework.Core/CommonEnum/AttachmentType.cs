//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AttachmentType.cs
// 描述： 附件类型
// 作者：ChenJie 
// 编写日期：2017-09-23
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 附件类型
    /// </summary>
    [Description("附件类型")]
    public enum AttachmentType
    {
        /// <summary>
        /// 内嵌附件
        /// </summary>
        [Description("内嵌附件")]
        InLine = 0,

        /// <summary>
        /// 下载附件
        /// </summary>
        [Description("下载附件")]
        Attachment = 1
    }
}
