namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class ReadMessageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadMessageForm));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.txtSendTime = new DevExpress.XtraEditors.TextEdit();
            this.lblSendTime = new DevExpress.XtraEditors.LabelControl();
            this.txtSender = new DevExpress.XtraEditors.TextEdit();
            this.lblCondition = new DevExpress.XtraEditors.LabelControl();
            this.txtTitle = new DevExpress.XtraEditors.TextEdit();
            this.gpAttachment = new DevExpress.XtraEditors.GroupControl();
            this.richEditControl = new DevExpress.XtraRichEdit.RichEditControl();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpAttachment)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblTitle);
            this.groupControl1.Controls.Add(this.txtSendTime);
            this.groupControl1.Controls.Add(this.lblSendTime);
            this.groupControl1.Controls.Add(this.txtSender);
            this.groupControl1.Controls.Add(this.lblCondition);
            this.groupControl1.Controls.Add(this.txtTitle);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1086, 100);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "消息";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.richEditControl);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 100);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1086, 389);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "内容";
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(56, 27);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(36, 14);
            this.lblTitle.TabIndex = 42;
            this.lblTitle.Text = "标题：";
            // 
            // txtSendTime
            // 
            this.txtSendTime.EditValue = "";
            this.txtSendTime.Location = new System.Drawing.Point(98, 72);
            this.txtSendTime.Name = "txtSendTime";
            this.txtSendTime.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtSendTime.Properties.Appearance.Options.UseBackColor = true;
            this.txtSendTime.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtSendTime.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSendTime.Properties.ReadOnly = true;
            this.txtSendTime.Size = new System.Drawing.Size(976, 18);
            this.txtSendTime.TabIndex = 39;
            // 
            // lblSendTime
            // 
            this.lblSendTime.Location = new System.Drawing.Point(32, 73);
            this.lblSendTime.Name = "lblSendTime";
            this.lblSendTime.Size = new System.Drawing.Size(60, 14);
            this.lblSendTime.TabIndex = 41;
            this.lblSendTime.Text = "发件日期：";
            // 
            // txtSender
            // 
            this.txtSender.EditValue = "";
            this.txtSender.Location = new System.Drawing.Point(98, 49);
            this.txtSender.Name = "txtSender";
            this.txtSender.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtSender.Properties.Appearance.Options.UseBackColor = true;
            this.txtSender.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtSender.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSender.Properties.ReadOnly = true;
            this.txtSender.Size = new System.Drawing.Size(976, 18);
            this.txtSender.TabIndex = 38;
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(44, 49);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(48, 14);
            this.lblCondition.TabIndex = 40;
            this.lblCondition.Text = "发件人：";
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.EditValue = "";
            this.txtTitle.Location = new System.Drawing.Point(98, 26);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtTitle.Properties.Appearance.Options.UseBackColor = true;
            this.txtTitle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtTitle.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtTitle.Properties.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(976, 18);
            this.txtTitle.TabIndex = 37;
            // 
            // gpAttachment
            // 
            this.gpAttachment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpAttachment.Location = new System.Drawing.Point(0, 489);
            this.gpAttachment.Name = "gpAttachment";
            this.gpAttachment.Size = new System.Drawing.Size(1086, 100);
            this.gpAttachment.TabIndex = 3;
            this.gpAttachment.Text = "附件";
            // 
            // richEditControl
            // 
            this.richEditControl.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl.Location = new System.Drawing.Point(2, 21);
            this.richEditControl.Name = "richEditControl";
            this.richEditControl.ReadOnly = true;
            this.richEditControl.Size = new System.Drawing.Size(1082, 366);
            this.richEditControl.TabIndex = 7;
            // 
            // ReadMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 589);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.gpAttachment);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ReadMessageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "消息内容";
            this.Load += new System.EventHandler(this.ReadMessageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSendTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpAttachment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.TextEdit txtSendTime;
        private DevExpress.XtraEditors.LabelControl lblSendTime;
        private DevExpress.XtraEditors.TextEdit txtSender;
        private DevExpress.XtraEditors.LabelControl lblCondition;
        private DevExpress.XtraEditors.TextEdit txtTitle;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl gpAttachment;
        private DevExpress.XtraRichEdit.RichEditControl richEditControl;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}