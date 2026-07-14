namespace Blue.WindowsFormsClient.MyBusinessModule
{
    partial class BusinessMainControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusinessMainControl));
            this.icData = new DevExpress.Utils.ImageCollection(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.lblStatistics = new DevExpress.XtraEditors.LabelControl();
            this.hlblWithdraw = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.lblTimeSubmitted = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.hyperlinkLabelControl1 = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hyperlinkLabelControl3 = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.lblDataCaption = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).BeginInit();
            this.SuspendLayout();
            // 
            // icData
            // 
            this.icData.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icData.ImageStream")));
            this.icData.Images.SetKeyName(0, "Client_Common_General_Intro.png");
            this.icData.Images.SetKeyName(1, "Client_Common_Statistics.png");
            this.icData.Images.SetKeyName(2, "Client_Common_Data_Processing.png");
            this.icData.Images.SetKeyName(3, "Client_Common_Data_Draft.png");
            this.icData.Images.SetKeyName(4, "Client_Common_Data_Completed.png");
            // 
            // groupControl1
            // 
            this.groupControl1.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.Client_Common_Business_Caption;
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Controls.Add(this.pnlLeft);
            this.groupControl1.Controls.Add(this.separatorControl1);
            this.groupControl1.Controls.Add(this.memoEdit1);
            this.groupControl1.Controls.Add(this.lblDataCaption);
            this.groupControl1.Controls.Add(this.separatorControl3);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(791, 294);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "常用业务主界面";
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
            this.pnlLeft.Controls.Add(this.lblStatistics);
            this.pnlLeft.Controls.Add(this.hlblWithdraw);
            this.pnlLeft.Controls.Add(this.labelControl10);
            this.pnlLeft.Controls.Add(this.lblTimeSubmitted);
            this.pnlLeft.Controls.Add(this.labelControl1);
            this.pnlLeft.Controls.Add(this.hyperlinkLabelControl1);
            this.pnlLeft.Controls.Add(this.hyperlinkLabelControl3);
            this.pnlLeft.Location = new System.Drawing.Point(21, 146);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(221, 135);
            this.pnlLeft.TabIndex = 33;
            // 
            // lblStatistics
            // 
            this.lblStatistics.Appearance.ImageIndex = 1;
            this.lblStatistics.Appearance.ImageList = this.icData;
            this.lblStatistics.Appearance.Options.UseImageIndex = true;
            this.lblStatistics.Appearance.Options.UseImageList = true;
            this.lblStatistics.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblStatistics.Location = new System.Drawing.Point(5, 5);
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Size = new System.Drawing.Size(153, 20);
            this.lblStatistics.TabIndex = 24;
            this.lblStatistics.Text = "所有常用业务的统计信息";
            // 
            // hlblWithdraw
            // 
            this.hlblWithdraw.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlblWithdraw.Appearance.Options.UseImageAlign = true;
            this.hlblWithdraw.Appearance.Options.UseImageList = true;
            this.hlblWithdraw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlblWithdraw.Location = new System.Drawing.Point(86, 39);
            this.hlblWithdraw.Name = "hlblWithdraw";
            this.hlblWithdraw.Size = new System.Drawing.Size(43, 14);
            this.hlblWithdraw.TabIndex = 23;
            this.hlblWithdraw.Text = "共有5件";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl10.Appearance.ImageIndex = 3;
            this.labelControl10.Appearance.ImageList = this.icData;
            this.labelControl10.Appearance.Options.UseImageAlign = true;
            this.labelControl10.Appearance.Options.UseImageIndex = true;
            this.labelControl10.Appearance.Options.UseImageList = true;
            this.labelControl10.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControl10.Location = new System.Drawing.Point(4, 63);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(81, 20);
            this.labelControl10.TabIndex = 25;
            this.labelControl10.Text = "草稿状态：";
            // 
            // lblTimeSubmitted
            // 
            this.lblTimeSubmitted.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTimeSubmitted.Appearance.ImageIndex = 2;
            this.lblTimeSubmitted.Appearance.ImageList = this.icData;
            this.lblTimeSubmitted.Appearance.Options.UseImageAlign = true;
            this.lblTimeSubmitted.Appearance.Options.UseImageIndex = true;
            this.lblTimeSubmitted.Appearance.Options.UseImageList = true;
            this.lblTimeSubmitted.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblTimeSubmitted.Location = new System.Drawing.Point(4, 36);
            this.lblTimeSubmitted.Name = "lblTimeSubmitted";
            this.lblTimeSubmitted.Size = new System.Drawing.Size(69, 20);
            this.lblTimeSubmitted.TabIndex = 22;
            this.lblTimeSubmitted.Text = "待处理：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ImageIndex = 4;
            this.labelControl1.Appearance.ImageList = this.icData;
            this.labelControl1.Appearance.Options.UseImageIndex = true;
            this.labelControl1.Appearance.Options.UseImageList = true;
            this.labelControl1.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControl1.Location = new System.Drawing.Point(4, 90);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(69, 20);
            this.labelControl1.TabIndex = 26;
            this.labelControl1.Text = "已完成：";
            // 
            // hyperlinkLabelControl1
            // 
            this.hyperlinkLabelControl1.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hyperlinkLabelControl1.Appearance.Options.UseImageAlign = true;
            this.hyperlinkLabelControl1.Appearance.Options.UseImageList = true;
            this.hyperlinkLabelControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hyperlinkLabelControl1.Location = new System.Drawing.Point(86, 67);
            this.hyperlinkLabelControl1.Name = "hyperlinkLabelControl1";
            this.hyperlinkLabelControl1.Size = new System.Drawing.Size(43, 14);
            this.hyperlinkLabelControl1.TabIndex = 27;
            this.hyperlinkLabelControl1.Text = "共有5件";
            // 
            // hyperlinkLabelControl3
            // 
            this.hyperlinkLabelControl3.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hyperlinkLabelControl3.Appearance.Options.UseImageAlign = true;
            this.hyperlinkLabelControl3.Appearance.Options.UseImageList = true;
            this.hyperlinkLabelControl3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hyperlinkLabelControl3.Location = new System.Drawing.Point(86, 93);
            this.hyperlinkLabelControl3.Name = "hyperlinkLabelControl3";
            this.hyperlinkLabelControl3.Size = new System.Drawing.Size(43, 14);
            this.hyperlinkLabelControl3.TabIndex = 28;
            this.hyperlinkLabelControl3.Text = "共有5件";
            // 
            // separatorControl1
            // 
            this.separatorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.separatorControl1.Location = new System.Drawing.Point(3, 120);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(783, 23);
            this.separatorControl1.TabIndex = 32;
            // 
            // memoEdit1
            // 
            this.memoEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.memoEdit1.EditValue = "常用业务内容测试。";
            this.memoEdit1.Location = new System.Drawing.Point(21, 72);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.memoEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.memoEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoEdit1.Properties.ReadOnly = true;
            this.memoEdit1.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoEdit1.Size = new System.Drawing.Size(747, 47);
            this.memoEdit1.TabIndex = 29;
            // 
            // lblDataCaption
            // 
            this.lblDataCaption.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDataCaption.Appearance.ImageIndex = 0;
            this.lblDataCaption.Appearance.ImageList = this.icData;
            this.lblDataCaption.Appearance.Options.UseImageAlign = true;
            this.lblDataCaption.Appearance.Options.UseImageIndex = true;
            this.lblDataCaption.Appearance.Options.UseImageList = true;
            this.lblDataCaption.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblDataCaption.Location = new System.Drawing.Point(24, 31);
            this.lblDataCaption.Name = "lblDataCaption";
            this.lblDataCaption.Size = new System.Drawing.Size(69, 20);
            this.lblDataCaption.TabIndex = 30;
            this.lblDataCaption.Text = "简要说明";
            // 
            // separatorControl3
            // 
            this.separatorControl3.Location = new System.Drawing.Point(3, 49);
            this.separatorControl3.Name = "separatorControl3";
            this.separatorControl3.Size = new System.Drawing.Size(783, 23);
            this.separatorControl3.TabIndex = 31;
            // 
            // BusinessMainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "BusinessMainControl";
            this.Size = new System.Drawing.Size(791, 294);
            this.Load += new System.EventHandler(this.BusinessMainControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.HyperlinkLabelControl hyperlinkLabelControl3;
        private DevExpress.XtraEditors.HyperlinkLabelControl hyperlinkLabelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblTimeSubmitted;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlblWithdraw;
        private DevExpress.XtraEditors.LabelControl lblStatistics;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.LabelControl lblDataCaption;
        private DevExpress.XtraEditors.SeparatorControl separatorControl3;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.Utils.ImageCollection icData;
        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}
