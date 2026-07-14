namespace Blue.WindowsFormsClient.MyPersonDataModule
{
    partial class PersonalDataInstanceControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonalDataInstanceControl));
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gcTop = new DevExpress.XtraEditors.GroupControl();
            this.hlnkAdd = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.icButtons = new DevExpress.Utils.ImageCollection(this.components);
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.lblLine = new DevExpress.XtraEditors.LabelControl();
            this.hlnkView = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkApply = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkRefresh = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkBack = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkReport = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.dataTableControl = new Blue.WindowsFormsClient.Common.DataTableControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTop)).BeginInit();
            this.gcTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dataTableControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 55);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(898, 387);
            this.pnlMain.TabIndex = 1;
            // 
            // gcTop
            // 
            this.gcTop.Controls.Add(this.hlnkReport);
            this.gcTop.Controls.Add(this.hlnkAdd);
            this.gcTop.Controls.Add(this.lblTip);
            this.gcTop.Controls.Add(this.lblLine);
            this.gcTop.Controls.Add(this.hlnkView);
            this.gcTop.Controls.Add(this.hlnkApply);
            this.gcTop.Controls.Add(this.hlnkRefresh);
            this.gcTop.Controls.Add(this.hlnkBack);
            this.gcTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcTop.Location = new System.Drawing.Point(0, 0);
            this.gcTop.Name = "gcTop";
            this.gcTop.Size = new System.Drawing.Size(898, 55);
            this.gcTop.TabIndex = 2;
            this.gcTop.Text = "标题";
            // 
            // hlnkAdd
            // 
            this.hlnkAdd.Appearance.ImageIndex = 5;
            this.hlnkAdd.Appearance.ImageList = this.icButtons;
            this.hlnkAdd.Appearance.Options.UseImageIndex = true;
            this.hlnkAdd.Appearance.Options.UseImageList = true;
            this.hlnkAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkAdd.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkAdd.Location = new System.Drawing.Point(17, 28);
            this.hlnkAdd.Name = "hlnkAdd";
            this.hlnkAdd.Size = new System.Drawing.Size(105, 20);
            this.hlnkAdd.TabIndex = 0;
            this.hlnkAdd.Text = "信息增加申请...";
            this.hlnkAdd.Click += new System.EventHandler(this.hlnkAdd_Click);
            // 
            // icButtons
            // 
            this.icButtons.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icButtons.ImageStream")));
            this.icButtons.Images.SetKeyName(0, "Button_Application.png");
            this.icButtons.Images.SetKeyName(1, "Button_View.png");
            this.icButtons.Images.SetKeyName(2, "Client_Common_Back.png");
            this.icButtons.Images.SetKeyName(3, "Client_Common_Refresh.png");
            this.icButtons.Images.SetKeyName(4, "Tip_Information.png");
            this.icButtons.Images.SetKeyName(5, "Button_Add_New.png");
            this.icButtons.Images.SetKeyName(6, "Client_Common_Excel.png");
            // 
            // lblTip
            // 
            this.lblTip.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTip.Appearance.ImageIndex = 4;
            this.lblTip.Appearance.ImageList = this.icButtons;
            this.lblTip.Appearance.Options.UseImageAlign = true;
            this.lblTip.Appearance.Options.UseImageIndex = true;
            this.lblTip.Appearance.Options.UseImageList = true;
            this.lblTip.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblTip.Location = new System.Drawing.Point(334, 30);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(285, 20);
            this.lblTip.TabIndex = 40;
            this.lblTip.Text = "提示：个人信息在信息更新申请审核完成后生效。";
            // 
            // lblLine
            // 
            this.lblLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLine.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Vertical_Line;
            this.lblLine.Appearance.Options.UseImage = true;
            this.lblLine.Location = new System.Drawing.Point(732, 31);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(16, 16);
            this.lblLine.TabIndex = 39;
            // 
            // hlnkView
            // 
            this.hlnkView.Appearance.ImageIndex = 1;
            this.hlnkView.Appearance.ImageList = this.icButtons;
            this.hlnkView.Appearance.Options.UseImageIndex = true;
            this.hlnkView.Appearance.Options.UseImageList = true;
            this.hlnkView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkView.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkView.Location = new System.Drawing.Point(227, 28);
            this.hlnkView.Name = "hlnkView";
            this.hlnkView.Size = new System.Drawing.Size(105, 20);
            this.hlnkView.TabIndex = 2;
            this.hlnkView.Text = "查看申请记录...";
            this.hlnkView.Click += new System.EventHandler(this.hlnkView_Click);
            // 
            // hlnkApply
            // 
            this.hlnkApply.Appearance.ImageIndex = 0;
            this.hlnkApply.Appearance.ImageList = this.icButtons;
            this.hlnkApply.Appearance.Options.UseImageIndex = true;
            this.hlnkApply.Appearance.Options.UseImageList = true;
            this.hlnkApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkApply.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkApply.Location = new System.Drawing.Point(124, 28);
            this.hlnkApply.Name = "hlnkApply";
            this.hlnkApply.Size = new System.Drawing.Size(105, 20);
            this.hlnkApply.TabIndex = 1;
            this.hlnkApply.Text = "信息更新申请...";
            this.hlnkApply.Click += new System.EventHandler(this.hlnkApply_Click);
            // 
            // hlnkRefresh
            // 
            this.hlnkRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlnkRefresh.Appearance.ImageIndex = 2;
            this.hlnkRefresh.Appearance.ImageList = this.icButtons;
            this.hlnkRefresh.Appearance.Options.UseImageIndex = true;
            this.hlnkRefresh.Appearance.Options.UseImageList = true;
            this.hlnkRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkRefresh.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkRefresh.Location = new System.Drawing.Point(752, 28);
            this.hlnkRefresh.Name = "hlnkRefresh";
            this.hlnkRefresh.Size = new System.Drawing.Size(57, 20);
            this.hlnkRefresh.TabIndex = 3;
            this.hlnkRefresh.Text = "刷新...";
            this.hlnkRefresh.Click += new System.EventHandler(this.hlnkRefresh_Click);
            // 
            // hlnkBack
            // 
            this.hlnkBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlnkBack.Appearance.ImageIndex = 3;
            this.hlnkBack.Appearance.ImageList = this.icButtons;
            this.hlnkBack.Appearance.Options.UseImageIndex = true;
            this.hlnkBack.Appearance.Options.UseImageList = true;
            this.hlnkBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkBack.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkBack.Location = new System.Drawing.Point(816, 28);
            this.hlnkBack.Name = "hlnkBack";
            this.hlnkBack.Size = new System.Drawing.Size(57, 20);
            this.hlnkBack.TabIndex = 4;
            this.hlnkBack.Text = "返回...";
            this.hlnkBack.Click += new System.EventHandler(this.hlnkBack_Click);
            // 
            // hlnkReport
            // 
            this.hlnkReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlnkReport.Appearance.ImageIndex = 6;
            this.hlnkReport.Appearance.ImageList = this.icButtons;
            this.hlnkReport.Appearance.Options.UseImageIndex = true;
            this.hlnkReport.Appearance.Options.UseImageList = true;
            this.hlnkReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkReport.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkReport.Location = new System.Drawing.Point(647, 28);
            this.hlnkReport.Name = "hlnkReport";
            this.hlnkReport.Size = new System.Drawing.Size(81, 20);
            this.hlnkReport.TabIndex = 41;
            this.hlnkReport.Text = "下载报表...";
            this.hlnkReport.Click += new System.EventHandler(this.hlnkReport_Click);
            // 
            // dataTableControl
            // 
            this.dataTableControl.AddHandler = null;
            this.dataTableControl.AllowDataExported = false;
            this.dataTableControl.AllowDataImported = false;
            this.dataTableControl.AllowStatusSetting = false;
            this.dataTableControl.CustomRoleContract = null;
            this.dataTableControl.DeleteHandler = null;
            this.dataTableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataTableControl.EditHandler = null;
            this.dataTableControl.FormReadOnly = false;
            this.dataTableControl.LoadDataHanler = null;
            this.dataTableControl.Location = new System.Drawing.Point(2, 2);
            this.dataTableControl.MoveRecordHandler = null;
            this.dataTableControl.Name = "dataTableControl";
            this.dataTableControl.RefreshSortinHandler = null;
            this.dataTableControl.SetAuthorityHandler = null;
            this.dataTableControl.Size = new System.Drawing.Size(894, 383);
            this.dataTableControl.TabIndex = 0;
            this.dataTableControl.TableId = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.dataTableControl.UpdateCurretStateHandler = null;
            // 
            // PersonalDataInstanceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.gcTop);
            this.Name = "PersonalDataInstanceControl";
            this.Size = new System.Drawing.Size(898, 442);
            this.Load += new System.EventHandler(this.PersonalDataInstanceControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTop)).EndInit();
            this.gcTop.ResumeLayout(false);
            this.gcTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private Common.DataTableControl dataTableControl;
        private DevExpress.XtraEditors.GroupControl gcTop;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkRefresh;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkBack;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkView;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkApply;
        private DevExpress.Utils.ImageCollection icButtons;
        private DevExpress.XtraEditors.LabelControl lblLine;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkAdd;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkReport;
    }
}
