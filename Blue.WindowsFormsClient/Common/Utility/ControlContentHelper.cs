//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ControlContentHelper.cs
// 描述： 控件内容帮助类
// 作者：ChenJie 
// 编写日期：2018/01/23
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using AppFramework.Core;

namespace Blue.WindowsFormsClient.Common
{
    /// <summary>
    /// 控件内容帮助类
    /// </summary>
    public sealed class ControlContentHelper
    {
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="textId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="text"></param>
        /// <param name="txtGuidance"></param>
        /// <param name="hleGuidance"></param>
        public static void ShowDialog(decimal textId, AttachmentCategory attachmentCategory, string text, TextEdit txtGuidance, HyperLinkEdit hleGuidance)
        {
            TextForm frmText = new TextForm();
            frmText.Text = text;
            frmText.AttachmentCategory = AttachmentCategory.DataFilled;
            if (txtGuidance.Tag != null)
            {
                frmText.HtmlText = DataConvertionHelper.GetString(txtGuidance.Tag);
            }
            else
            {
                frmText.HtmlText = string.Empty;
            }
            frmText.TextId = textId;
            frmText.GetRichTextAndAttachments = delegate (string richText, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
            {
                txtGuidance.Text = "[已设置]";
                txtGuidance.Tag = richText;
                hleGuidance.Tag = upLoadFileInfos;
            };
            frmText.ShowDialog();
        }

        /// <summary>
        /// 设置附件
        /// </summary>
        /// <param name="chkEnable"></param>
        /// <param name="txtGuidance"></param>
        /// <param name="hleGuidance"></param>
        /// <param name="enabled"></param>
        /// <param name="condition"></param>
        public static void SetAttachments(CheckEdit chkEnable, TextEdit txtGuidance, HyperLinkEdit hleGuidance, bool enabled, string condition)
        {
            chkEnable.Checked = enabled;
            if (!string.IsNullOrWhiteSpace(condition))
            {
                txtGuidance.Text = "[已设置]";
            }
            else
            {
                txtGuidance.Text = string.Empty;
            }
            txtGuidance.Tag = condition;
            /* null 表示指南内容的附件不变化 */
            hleGuidance.Tag = null;
        }
    }       
}
