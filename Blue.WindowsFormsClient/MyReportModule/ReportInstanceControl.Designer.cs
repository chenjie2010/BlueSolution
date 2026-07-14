namespace Blue.WindowsFormsClient.MyReportModule
{
    partial class ReportInstanceControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportInstanceControl));
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.lnkPrintReport = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.icReport = new DevExpress.Utils.ImageCollection(this.components);
            this.hlnkHistory = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btnViewReport = new DevExpress.XtraEditors.SimpleButton();
            this.hlnkRefresh = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkBack = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkMore = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.lblReportCaption = new DevExpress.XtraEditors.LabelControl();
            this.icReport_Big = new DevExpress.Utils.ImageCollection(this.components);
            this.meContent = new DevExpress.XtraEditors.MemoEdit();
            this.scSecond = new DevExpress.XtraEditors.SeparatorControl();
            this.scFirst = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icReport_Big)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scFirst)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.Client_Report_Instance;
            this.gcMain.Controls.Add(this.lnkPrintReport);
            this.gcMain.Controls.Add(this.hlnkHistory);
            this.gcMain.Controls.Add(this.btnViewReport);
            this.gcMain.Controls.Add(this.hlnkRefresh);
            this.gcMain.Controls.Add(this.hlnkBack);
            this.gcMain.Controls.Add(this.hlnkMore);
            this.gcMain.Controls.Add(this.lblReportCaption);
            this.gcMain.Controls.Add(this.meContent);
            this.gcMain.Controls.Add(this.scSecond);
            this.gcMain.Controls.Add(this.scFirst);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(744, 374);
            this.gcMain.TabIndex = 0;
            this.gcMain.Text = "查询报表名称";
            // 
            // lnkPrintReport
            // 
            this.lnkPrintReport.Appearance.ImageIndex = 4;
            this.lnkPrintReport.Appearance.ImageList = this.icReport;
            this.lnkPrintReport.Appearance.Options.UseImageIndex = true;
            this.lnkPrintReport.Appearance.Options.UseImageList = true;
            this.lnkPrintReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkPrintReport.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lnkPrintReport.Location = new System.Drawing.Point(216, 195);
            this.lnkPrintReport.Name = "lnkPrintReport";
            this.lnkPrintReport.Size = new System.Drawing.Size(117, 20);
            this.lnkPrintReport.TabIndex = 43;
            this.lnkPrintReport.Text = "批量打印基本报表";
            this.lnkPrintReport.Click += new System.EventHandler(this.lnkPrintReport_Click);
            // 
            // icReport
            // 
            this.icReport.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icReport.ImageStream")));
            this.icReport.Images.SetKeyName(0, "Client_Common_View.png");
            this.icReport.Images.SetKeyName(1, "Common_Report_View.png");
            this.icReport.Images.SetKeyName(2, "Client_Common_Back.png");
            this.icReport.Images.SetKeyName(3, "Client_Common_Refresh.png");
            this.icReport.Images.SetKeyName(4, "BarButtonItem_Print.png");
            // 
            // hlnkHistory
            // 
            this.hlnkHistory.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkHistory.Appearance.ImageIndex = 0;
            this.hlnkHistory.Appearance.ImageList = this.icReport;
            this.hlnkHistory.Appearance.Options.UseImageAlign = true;
            this.hlnkHistory.Appearance.Options.UseImageIndex = true;
            this.hlnkHistory.Appearance.Options.UseImageList = true;
            this.hlnkHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkHistory.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkHistory.Location = new System.Drawing.Point(135, 195);
            this.hlnkHistory.Name = "hlnkHistory";
            this.hlnkHistory.Size = new System.Drawing.Size(81, 20);
            this.hlnkHistory.TabIndex = 42;
            this.hlnkHistory.Text = "查看快照...";
            this.hlnkHistory.Click += new System.EventHandler(this.hlnkHistory_Click);
            // 
            // btnViewReport
            // 
            this.btnViewReport.ImageIndex = 1;
            this.btnViewReport.ImageList = this.icReport;
            this.btnViewReport.Location = new System.Drawing.Point(32, 193);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(95, 20);
            this.btnViewReport.TabIndex = 41;
            this.btnViewReport.Text = "查看报表(&S)";
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // hlnkRefresh
            // 
            this.hlnkRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlnkRefresh.Appearance.ImageIndex = 2;
            this.hlnkRefresh.Appearance.ImageList = this.icReport;
            this.hlnkRefresh.Appearance.Options.UseImageIndex = true;
            this.hlnkRefresh.Appearance.Options.UseImageList = true;
            this.hlnkRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkRefresh.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkRefresh.Location = new System.Drawing.Point(618, 34);
            this.hlnkRefresh.Name = "hlnkRefresh";
            this.hlnkRefresh.Size = new System.Drawing.Size(57, 20);
            this.hlnkRefresh.TabIndex = 39;
            this.hlnkRefresh.Text = "刷新...";
            this.hlnkRefresh.Click += new System.EventHandler(this.hlnkRefresh_Click);
            // 
            // hlnkBack
            // 
            this.hlnkBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlnkBack.Appearance.ImageIndex = 3;
            this.hlnkBack.Appearance.ImageList = this.icReport;
            this.hlnkBack.Appearance.Options.UseImageIndex = true;
            this.hlnkBack.Appearance.Options.UseImageList = true;
            this.hlnkBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkBack.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkBack.Location = new System.Drawing.Point(681, 34);
            this.hlnkBack.Name = "hlnkBack";
            this.hlnkBack.Size = new System.Drawing.Size(57, 20);
            this.hlnkBack.TabIndex = 38;
            this.hlnkBack.Text = "返回...";
            this.hlnkBack.Click += new System.EventHandler(this.hlnkBack_Click);
            // 
            // hlnkMore
            // 
            this.hlnkMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlnkMore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkMore.Location = new System.Drawing.Point(703, 151);
            this.hlnkMore.Name = "hlnkMore";
            this.hlnkMore.Size = new System.Drawing.Size(36, 14);
            this.hlnkMore.TabIndex = 36;
            this.hlnkMore.Text = "更多...";
            this.hlnkMore.Click += new System.EventHandler(this.hlnkMore_Click);
            // 
            // lblReportCaption
            // 
            this.lblReportCaption.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblReportCaption.Appearance.ImageIndex = 0;
            this.lblReportCaption.Appearance.ImageList = this.icReport_Big;
            this.lblReportCaption.Appearance.Options.UseImageAlign = true;
            this.lblReportCaption.Appearance.Options.UseImageIndex = true;
            this.lblReportCaption.Appearance.Options.UseImageList = true;
            this.lblReportCaption.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblReportCaption.Location = new System.Drawing.Point(26, 30);
            this.lblReportCaption.Name = "lblReportCaption";
            this.lblReportCaption.Size = new System.Drawing.Size(101, 28);
            this.lblReportCaption.TabIndex = 34;
            this.lblReportCaption.Text = "查询报表介绍";
            // 
            // icReport_Big
            // 
            this.icReport_Big.ImageSize = new System.Drawing.Size(24, 24);
            this.icReport_Big.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icReport_Big.ImageStream")));
            this.icReport_Big.Images.SetKeyName(0, "Client_Common_Data_Home_1.png");
            // 
            // meContent
            // 
            this.meContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.meContent.EditValue = "报表查询功能。";
            this.meContent.Location = new System.Drawing.Point(26, 79);
            this.meContent.Name = "meContent";
            this.meContent.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.meContent.Properties.Appearance.Options.UseBackColor = true;
            this.meContent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.meContent.Properties.ReadOnly = true;
            this.meContent.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.meContent.Size = new System.Drawing.Size(713, 66);
            this.meContent.TabIndex = 35;
            // 
            // scSecond
            // 
            this.scSecond.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scSecond.Location = new System.Drawing.Point(9, 164);
            this.scSecond.Name = "scSecond";
            this.scSecond.Size = new System.Drawing.Size(730, 23);
            this.scSecond.TabIndex = 37;
            // 
            // scFirst
            // 
            this.scFirst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scFirst.Location = new System.Drawing.Point(5, 52);
            this.scFirst.Name = "scFirst";
            this.scFirst.Size = new System.Drawing.Size(739, 23);
            this.scFirst.TabIndex = 40;
            // 
            // ReportInstanceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "ReportInstanceControl";
            this.Size = new System.Drawing.Size(744, 374);
            this.Load += new System.EventHandler(this.ReportInstanceControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            this.gcMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icReport_Big)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scFirst)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkRefresh;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkBack;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkMore;
        private DevExpress.XtraEditors.LabelControl lblReportCaption;
        private DevExpress.XtraEditors.MemoEdit meContent;
        private DevExpress.XtraEditors.SeparatorControl scSecond;
        private DevExpress.Utils.ImageCollection icReport;
        private DevExpress.Utils.ImageCollection icReport_Big;
        private DevExpress.XtraEditors.SeparatorControl scFirst;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkHistory;
        private DevExpress.XtraEditors.SimpleButton btnViewReport;
        private DevExpress.XtraEditors.HyperlinkLabelControl lnkPrintReport;
    }
}
