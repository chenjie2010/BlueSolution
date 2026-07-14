//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MailBoxType.cs
// 描述： 邮箱类型
// 作者：ChenJie 
// 编写日期：2017-09-08
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 邮箱类型
    /// </summary>
    public enum MailBoxType
    {       
        /// <summary>
        /// 收件箱
        /// </summary>
        [Description("收件箱")]
        InBox = 0,

        /// <summary>
        /// 发件箱
        /// </summary>
        [Description("发件箱")]
        OutBox = 1,

        /// <summary>
        /// 草稿箱
        /// </summary>
        [Description("草稿箱")]
        DraftBox = 2,

        /// <summary>
        /// 垃圾箱
        /// </summary>
        [Description("垃圾箱")]
        RecycleBin = 3
    }
}
