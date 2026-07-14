namespace Blue.WindowsFormsClient.MyAuditingModule
{
    partial class AuditingInstanceControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditingInstanceControl));
            this.gcTop = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtpPersonalData = new DevExpress.XtraTab.XtraTabPage();
            this.dataTableControl = new Blue.WindowsFormsClient.Common.DataTableControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.hlnkAdd = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.icButtons = new DevExpress.Utils.ImageCollection(this.components);
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.userListControl = new Blue.WindowsFormsClient.Common.UserListControl();
            this.lblLine = new DevExpress.XtraEditors.LabelControl();
            this.hlnkView = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkRefresh = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkApply = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkBack = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.xtpGroupData = new DevExpress.XtraTab.XtraTabPage();
            this.xtcGroup = new DevExpress.XtraTab.XtraTabControl();
            this.xtpAllocating = new DevExpress.XtraTab.XtraTabPage();
            this.xtpAllocated = new DevExpress.XtraTab.XtraTabPage();
            this.xtpAuditing = new DevExpress.XtraTab.XtraTabPage();
            this.xtpAudited = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.gcTop)).BeginInit();
            this.gcTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtpPersonalData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).BeginInit();
            this.xtpGroupData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtcGroup)).BeginInit();
            this.xtcGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcTop
            // 
            this.gcTop.Controls.Add(this.xtraTabControl1);
            this.gcTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTop.Location = new System.Drawing.Point(0, 0);
            this.gcTop.Name = "gcTop";
            this.gcTop.Size = new System.Drawing.Size(1016, 476);
            this.gcTop.TabIndex = 2;
            this.gcTop.Text = "标题";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 21);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtpPersonalData;
            this.xtraTabControl1.Size = new System.Drawing.Size(1012, 453);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpPersonalData,
            this.xtpGroupData});
            // 
            // xtpPersonalData
            // 
            this.xtpPersonalData.Controls.Add(this.dataTableControl);
            this.xtpPersonalData.Controls.Add(this.panelControl1);
            this.xtpPersonalData.Name = "xtpPersonalData";
            this.xtpPersonalData.Size = new System.Drawing.Size(1006, 424);
            this.xtpPersonalData.Text = "个人数据审核";
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
            this.dataTableControl.Location = new System.Drawing.Point(0, 223);
            this.dataTableControl.MoveRecordHandler = null;
            this.dataTableControl.Name = "dataTableControl";
            this.dataTableControl.SetAuthorityHandler = null;
            this.dataTableControl.Size = new System.Drawing.Size(1006, 201);
            this.dataTableControl.TabIndex = 0;
            this.dataTableControl.UpdateCurretStateHandler = null;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.hlnkAdd);
            this.panelControl1.Controls.Add(this.lblTip);
            this.panelControl1.Controls.Add(this.userListControl);
            this.panelControl1.Controls.Add(this.lblLine);
            this.panelControl1.Controls.Add(this.hlnkView);
            this.panelControl1.Controls.Add(this.hlnkRefresh);
            this.panelControl1.Controls.Add(this.hlnkApply);
            this.panelControl1.Controls.Add(this.hlnkBack);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1006, 223);
            this.panelControl1.TabIndex = 1;
            // 
            // hlnkAdd
            // 
            this.hlnkAdd.Appearance.ImageIndex = 5;
            this.hlnkAdd.Appearance.ImageList = this.icButtons;
            this.hlnkAdd.Appearance.Options.UseImageIndex = true;
            this.hlnkAdd.Appearance.Options.UseImageList = true;
            this.hlnkAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkAdd.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkAdd.Location = new System.Drawing.Point(5, 7);
            this.hlnkAdd.Name = "hlnkAdd";
            this.hlnkAdd.Size = new System.Drawing.Size(105, 20);
            this.hlnkAdd.TabIndex = 0;
            this.hlnkAdd.Text = "信息增加申请...";
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
            this.lblTip.Location = new System.Drawing.Point(335, 8);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(285, 20);
            this.lblTip.TabIndex = 41;
            this.lblTip.Text = "提示：个人信息在信息更新申请审核完成后生效。";
            // 
            // userListControl
            // 
            this.userListControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.userListControl.IsPhotoShowed = false;
            this.userListControl.IsShowCheckBox = true;
            this.userListControl.Location = new System.Drawing.Point(2, 32);
            this.userListControl.Name = "userListControl";
            this.userListControl.Size = new System.Drawing.Size(1002, 189);
            this.userListControl.TabIndex = 39;
            // 
            // lblLine
            // 
            this.lblLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLine.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Vertical_Line;
            this.lblLine.Appearance.Options.UseImage = true;
            this.lblLine.Location = new System.Drawing.Point(847, 11);
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
            this.hlnkView.Location = new System.Drawing.Point(229, 7);
            this.hlnkView.Name = "hlnkView";
            this.hlnkView.Size = new System.Drawing.Size(105, 20);
            this.hlnkView.TabIndex = 3;
            this.hlnkView.Text = "查看申请记录...";
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
            this.hlnkRefresh.Location = new System.Drawing.Point(869, 6);
            this.hlnkRefresh.Name = "hlnkRefresh";
            this.hlnkRefresh.Size = new System.Drawing.Size(57, 20);
            this.hlnkRefresh.TabIndex = 4;
            this.hlnkRefresh.Text = "刷新...";
            // 
            // hlnkApply
            // 
            this.hlnkApply.Appearance.ImageIndex = 0;
            this.hlnkApply.Appearance.ImageList = this.icButtons;
            this.hlnkApply.Appearance.Options.UseImageIndex = true;
            this.hlnkApply.Appearance.Options.UseImageList = true;
            this.hlnkApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkApply.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkApply.Location = new System.Drawing.Point(122, 7);
            this.hlnkApply.Name = "hlnkApply";
            this.hlnkApply.Size = new System.Drawing.Size(105, 20);
            this.hlnkApply.TabIndex = 1;
            this.hlnkApply.Text = "信息更新申请...";
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
            this.hlnkBack.Location = new System.Drawing.Point(933, 6);
            this.hlnkBack.Name = "hlnkBack";
            this.hlnkBack.Size = new System.Drawing.Size(57, 20);
            this.hlnkBack.TabIndex = 5;
            this.hlnkBack.Text = "返回...";
            // 
            // xtpGroupData
            // 
            this.xtpGroupData.Controls.Add(this.xtcGroup);
            this.xtpGroupData.Name = "xtpGroupData";
            this.xtpGroupData.Size = new System.Drawing.Size(1006, 424);
            this.xtpGroupData.Text = "分组数据审核";
            // 
            // xtcGroup
            // 
            this.xtcGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcGroup.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtcGroup.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtcGroup.Location = new System.Drawing.Point(0, 0);
            this.xtcGroup.Name = "xtcGroup";
            this.xtcGroup.SelectedTabPage = this.xtpAllocating;
            this.xtcGroup.Size = new System.Drawing.Size(1006, 424);
            this.xtcGroup.TabIndex = 0;
            this.xtcGroup.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpAllocating,
            this.xtpAllocated,
            this.xtpAuditing,
            this.xtpAudited});
            // 
            // xtpAllocating
            // 
            this.xtpAllocating.Name = "xtpAllocating";
            this.xtpAllocating.Size = new System.Drawing.Size(926, 418);
            this.xtpAllocating.Text = "待分配记录";
            // 
            // xtpAllocated
            // 
            this.xtpAllocated.Name = "xtpAllocated";
            this.xtpAllocated.Size = new System.Drawing.Size(926, 418);
            this.xtpAllocated.Text = "已分配记录";
            // 
            // xtpAuditing
            // 
            this.xtpAuditing.Name = "xtpAuditing";
            this.xtpAuditing.Size = new System.Drawing.Size(926, 418);
            this.xtpAuditing.Text = "待审核记录";
            // 
            // xtpAudited
            // 
            this.xtpAudited.Name = "xtpAudited";
            this.xtpAudited.Size = new System.Drawing.Size(926, 418);
            this.xtpAudited.Text = "已审核记录";
            // 
            // AuditingInstanceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcTop);
            this.Name = "AuditingInstanceControl";
            this.Size = new System.Drawing.Size(1016, 476);
            this.Load += new System.EventHandler(this.AuditingInstanceControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcTop)).EndInit();
            this.gcTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtpPersonalData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).EndInit();
            this.xtpGroupData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtcGroup)).EndInit();
            this.xtcGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Common.DataTableControl dataTableControl;
        private DevExpress.XtraEditors.GroupControl gcTop;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkRefresh;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkBack;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkView;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkApply;
        private DevExpress.Utils.ImageCollection icButtons;
        private DevExpress.XtraEditors.LabelControl lblLine;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtpPersonalData;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Common.UserListControl userListControl;
        private DevExpress.XtraTab.XtraTabPage xtpGroupData;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkAdd;
        private DevExpress.XtraTab.XtraTabControl xtcGroup;
        private DevExpress.XtraTab.XtraTabPage xtpAllocating;
        private DevExpress.XtraTab.XtraTabPage xtpAllocated;
        private DevExpress.XtraTab.XtraTabPage xtpAuditing;
        private DevExpress.XtraTab.XtraTabPage xtpAudited;
    }
}
