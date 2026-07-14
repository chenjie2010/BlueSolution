namespace Blue.WindowsFormsClient.MyReportModule
{
    partial class ReportMainPanelControl
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.meQueryCaption = new DevExpress.XtraEditors.MemoEdit();
            this.lblQueryCaption = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meQueryCaption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.Client_Report_Main_Icon;
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Controls.Add(this.pnlLeft);
            this.groupControl1.Controls.Add(this.separatorControl1);
            this.groupControl1.Controls.Add(this.meQueryCaption);
            this.groupControl1.Controls.Add(this.lblQueryCaption);
            this.groupControl1.Controls.Add(this.separatorControl3);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(791, 294);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "查询报表主界面";
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Location = new System.Drawing.Point(261, 146);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(507, 135);
            this.panelControl2.TabIndex = 34;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlLeft.Location = new System.Drawing.Point(21, 146);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(221, 135);
            this.pnlLeft.TabIndex = 33;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.separatorControl1.Location = new System.Drawing.Point(3, 120);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(783, 23);
            this.separatorControl1.TabIndex = 32;
            // 
            // meQueryCaption
            // 
            this.meQueryCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.meQueryCaption.EditValue = "查询内容测试。";
            this.meQueryCaption.Location = new System.Drawing.Point(21, 72);
            this.meQueryCaption.Name = "meQueryCaption";
            this.meQueryCaption.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.meQueryCaption.Properties.Appearance.Options.UseBackColor = true;
            this.meQueryCaption.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.meQueryCaption.Properties.ReadOnly = true;
            this.meQueryCaption.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.meQueryCaption.Size = new System.Drawing.Size(747, 47);
            this.meQueryCaption.TabIndex = 29;
            // 
            // lblQueryCaption
            // 
            this.lblQueryCaption.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblQueryCaption.Appearance.ImageIndex = 0;
            this.lblQueryCaption.Appearance.Options.UseImageAlign = true;
            this.lblQueryCaption.Appearance.Options.UseImageIndex = true;
            this.lblQueryCaption.Appearance.Options.UseImageList = true;
            this.lblQueryCaption.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblQueryCaption.Location = new System.Drawing.Point(24, 31);
            this.lblQueryCaption.Name = "lblQueryCaption";
            this.lblQueryCaption.Size = new System.Drawing.Size(48, 14);
            this.lblQueryCaption.TabIndex = 30;
            this.lblQueryCaption.Text = "简要说明";
            // 
            // separatorControl3
            // 
            this.separatorControl3.Location = new System.Drawing.Point(3, 49);
            this.separatorControl3.Name = "separatorControl3";
            this.separatorControl3.Size = new System.Drawing.Size(783, 23);
            this.separatorControl3.TabIndex = 31;
            // 
            // ReportMainPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ReportMainPanelControl";
            this.Size = new System.Drawing.Size(791, 294);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meQueryCaption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.MemoEdit meQueryCaption;
        private DevExpress.XtraEditors.LabelControl lblQueryCaption;
        private DevExpress.XtraEditors.SeparatorControl separatorControl3;
    }
}
