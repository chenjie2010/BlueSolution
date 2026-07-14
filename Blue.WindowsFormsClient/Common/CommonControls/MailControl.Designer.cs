namespace Blue.WindowsFormsClient.Common.CommonControls
{
    partial class MailControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.richEditControl = new DevExpress.XtraRichEdit.RichEditControl();
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.chkCritical = new DevExpress.XtraEditors.CheckEdit();
            this.txtCopy = new DevExpress.XtraEditors.TextEdit();
            this.lblCopy = new DevExpress.XtraEditors.LabelControl();
            this.txtReceiver = new DevExpress.XtraEditors.TextEdit();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblReceiver = new DevExpress.XtraEditors.LabelControl();
            this.txtSendTime = new DevExpress.XtraEditors.TextEdit();
            this.lblSendTime = new DevExpress.XtraEditors.LabelControl();
            this.txtSender = new DevExpress.XtraEditors.TextEdit();
            this.lblCondition = new DevExpress.XtraEditors.LabelControl();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.pnlAttachment = new DevExpress.XtraEditors.PanelControl();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lblReply = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.lblCritical = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCritical.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCopy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiver.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAttachment)).BeginInit();
            this.SuspendLayout();
            // 
            // richEditControl
            // 
            this.richEditControl.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl.Location = new System.Drawing.Point(0, 152);
            this.richEditControl.Name = "richEditControl";
            this.richEditControl.ReadOnly = true;
            this.richEditControl.Size = new System.Drawing.Size(389, 172);
            this.richEditControl.TabIndex = 6;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblCritical);
            this.pnlHeader.Controls.Add(this.lblReply);
            this.pnlHeader.Controls.Add(this.chkCritical);
            this.pnlHeader.Controls.Add(this.txtCopy);
            this.pnlHeader.Controls.Add(this.lblCopy);
            this.pnlHeader.Controls.Add(this.txtReceiver);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblReceiver);
            this.pnlHeader.Controls.Add(this.txtSendTime);
            this.pnlHeader.Controls.Add(this.lblSendTime);
            this.pnlHeader.Controls.Add(this.txtSender);
            this.pnlHeader.Controls.Add(this.lblCondition);
            this.pnlHeader.Controls.Add(this.txtTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(389, 152);
            this.pnlHeader.TabIndex = 1;
            // 
            // chkCritical
            // 
            this.chkCritical.Location = new System.Drawing.Point(74, 122);
            this.chkCritical.Name = "chkCritical";
            this.chkCritical.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.chkCritical.Properties.Appearance.Options.UseBackColor = true;
            this.chkCritical.Properties.Caption = "";
            this.chkCritical.Properties.ReadOnly = true;
            this.chkCritical.Size = new System.Drawing.Size(75, 19);
            this.chkCritical.TabIndex = 3;
            // 
            // txtCopy
            // 
            this.txtCopy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCopy.EditValue = "";
            this.txtCopy.Location = new System.Drawing.Point(74, 97);
            this.txtCopy.Name = "txtCopy";
            this.txtCopy.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtCopy.Properties.Appearance.Options.UseBackColor = true;
            this.txtCopy.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtCopy.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCopy.Properties.ReadOnly = true;
            this.txtCopy.Size = new System.Drawing.Size(309, 18);
            this.txtCopy.TabIndex = 5;
            // 
            // lblCopy
            // 
            this.lblCopy.Location = new System.Drawing.Point(32, 100);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(36, 14);
            this.lblCopy.TabIndex = 32;
            this.lblCopy.Text = "抄送：";
            // 
            // txtReceiver
            // 
            this.txtReceiver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceiver.EditValue = "";
            this.txtReceiver.Location = new System.Drawing.Point(74, 74);
            this.txtReceiver.Name = "txtReceiver";
            this.txtReceiver.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtReceiver.Properties.Appearance.Options.UseBackColor = true;
            this.txtReceiver.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtReceiver.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtReceiver.Properties.ReadOnly = true;
            this.txtReceiver.Size = new System.Drawing.Size(309, 18);
            this.txtReceiver.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(32, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(36, 14);
            this.lblTitle.TabIndex = 30;
            this.lblTitle.Text = "标题：";
            // 
            // lblReceiver
            // 
            this.lblReceiver.Location = new System.Drawing.Point(20, 76);
            this.lblReceiver.Name = "lblReceiver";
            this.lblReceiver.Size = new System.Drawing.Size(48, 14);
            this.lblReceiver.TabIndex = 29;
            this.lblReceiver.Text = "收件人：";
            // 
            // txtSendTime
            // 
            this.txtSendTime.EditValue = "";
            this.txtSendTime.Location = new System.Drawing.Point(74, 51);
            this.txtSendTime.Name = "txtSendTime";
            this.txtSendTime.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtSendTime.Properties.Appearance.Options.UseBackColor = true;
            this.txtSendTime.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtSendTime.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSendTime.Properties.ReadOnly = true;
            this.txtSendTime.Size = new System.Drawing.Size(300, 18);
            this.txtSendTime.TabIndex = 2;
            // 
            // lblSendTime
            // 
            this.lblSendTime.Location = new System.Drawing.Point(8, 52);
            this.lblSendTime.Name = "lblSendTime";
            this.lblSendTime.Size = new System.Drawing.Size(60, 14);
            this.lblSendTime.TabIndex = 26;
            this.lblSendTime.Text = "发件日期：";
            // 
            // txtSender
            // 
            this.txtSender.EditValue = "";
            this.txtSender.Location = new System.Drawing.Point(74, 28);
            this.txtSender.Name = "txtSender";
            this.txtSender.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtSender.Properties.Appearance.Options.UseBackColor = true;
            this.txtSender.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtSender.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSender.Properties.ReadOnly = true;
            this.txtSender.Size = new System.Drawing.Size(300, 18);
            this.txtSender.TabIndex = 1;
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(20, 28);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(48, 14);
            this.lblCondition.TabIndex = 24;
            this.lblCondition.Text = "发件人：";
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.EditValue = "";
            this.txtTitle.Location = new System.Drawing.Point(74, 5);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtTitle.Properties.Appearance.Options.UseBackColor = true;
            this.txtTitle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtTitle.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtTitle.Properties.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(309, 18);
            this.txtTitle.TabIndex = 0;
            // 
            // pnlAttachment
            // 
            this.pnlAttachment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAttachment.Location = new System.Drawing.Point(0, 324);
            this.pnlAttachment.Name = "pnlAttachment";
            this.pnlAttachment.Size = new System.Drawing.Size(389, 77);
            this.pnlAttachment.TabIndex = 2;
            // 
            // lblReply
            // 
            this.lblReply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblReply.Location = new System.Drawing.Point(328, 124);
            this.lblReply.Name = "lblReply";
            this.lblReply.Size = new System.Drawing.Size(48, 14);
            this.lblReply.TabIndex = 33;
            this.lblReply.Text = "回复邮件";
            this.lblReply.Click += new System.EventHandler(this.lblReply_Click);
            // 
            // lblCritical
            // 
            this.lblCritical.Location = new System.Drawing.Point(10, 124);
            this.lblCritical.Name = "lblCritical";
            this.lblCritical.Size = new System.Drawing.Size(60, 14);
            this.lblCritical.TabIndex = 34;
            this.lblCritical.Text = "紧急邮件：";
            // 
            // MailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richEditControl);
            this.Controls.Add(this.pnlAttachment);
            this.Controls.Add(this.pnlHeader);
            this.Name = "MailControl";
            this.Size = new System.Drawing.Size(389, 401);
            this.Load += new System.EventHandler(this.MailControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCritical.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCopy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiver.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAttachment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraRichEdit.RichEditControl richEditControl;
        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraEditors.PanelControl pnlAttachment;
        private DevExpress.XtraEditors.TextEdit txtTitle;
        private DevExpress.XtraEditors.TextEdit txtSendTime;
        private DevExpress.XtraEditors.LabelControl lblSendTime;
        private DevExpress.XtraEditors.TextEdit txtSender;
        private DevExpress.XtraEditors.LabelControl lblCondition;
        private DevExpress.XtraEditors.LabelControl lblReceiver;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.TextEdit txtCopy;
        private DevExpress.XtraEditors.LabelControl lblCopy;
        private DevExpress.XtraEditors.TextEdit txtReceiver;
        private DevExpress.XtraEditors.CheckEdit chkCritical;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private DevExpress.XtraEditors.HyperlinkLabelControl lblReply;
        private DevExpress.XtraEditors.LabelControl lblCritical;
    }
}
