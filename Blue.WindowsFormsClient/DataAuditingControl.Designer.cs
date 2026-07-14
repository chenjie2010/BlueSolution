namespace Blue.WindowsFormsClient
{
    partial class DataAuditingControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataAuditingControl));
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.nbcAuditing = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgAuditing = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiPersonalData = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiGroup = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgInfoUpdated = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiInfoAuditing = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiInfoAllocating = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiInfoAudited = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiInfoAuditedLog = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiInfoStatistics = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarSeparatorItem1 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.navBarSeparatorItem2 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.icAuditingLarge = new DevExpress.Utils.ImageCollection(this.components);
            this.icAuditingSmall = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbcAuditing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAuditingLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAuditingSmall)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.progressPanel);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(177, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(823, 477);
            this.pnlMain.TabIndex = 3;
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "数据正在加载......";
            this.progressPanel.Location = new System.Drawing.Point(336, 213);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(150, 50);
            this.progressPanel.TabIndex = 9;
            this.progressPanel.Text = "数据加载中......";
            this.progressPanel.Visible = false;
            // 
            // nbcAuditing
            // 
            this.nbcAuditing.ActiveGroup = this.nbgAuditing;
            this.nbcAuditing.AllowDrop = false;
            this.nbcAuditing.Dock = System.Windows.Forms.DockStyle.Left;
            this.nbcAuditing.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.None;
            this.nbcAuditing.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgAuditing,
            this.nbgInfoUpdated});
            this.nbcAuditing.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarSeparatorItem1,
            this.navBarSeparatorItem2,
            this.nbiInfoAuditing,
            this.nbiInfoAuditedLog,
            this.nbiInfoAllocating,
            this.nbiInfoStatistics,
            this.nbiPersonalData,
            this.nbiGroup,
            this.nbiInfoAudited});
            this.nbcAuditing.LargeImages = this.icAuditingLarge;
            this.nbcAuditing.Location = new System.Drawing.Point(0, 0);
            this.nbcAuditing.Name = "nbcAuditing";
            this.nbcAuditing.OptionsNavPane.ExpandedWidth = 177;
            this.nbcAuditing.OptionsNavPane.ShowOverflowButton = false;
            this.nbcAuditing.OptionsNavPane.ShowOverflowPanel = false;
            this.nbcAuditing.OptionsNavPane.ShowSplitter = false;
            this.nbcAuditing.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.nbcAuditing.Size = new System.Drawing.Size(177, 477);
            this.nbcAuditing.SmallImages = this.icAuditingSmall;
            this.nbcAuditing.StoreDefaultPaintStyleName = true;
            this.nbcAuditing.TabIndex = 2;
            this.nbcAuditing.Text = "navBarControl1";
            // 
            // nbgAuditing
            // 
            this.nbgAuditing.Caption = "通用信息审核";
            this.nbgAuditing.Expanded = true;
            this.nbgAuditing.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.nbgAuditing.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiPersonalData),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiGroup)});
            this.nbgAuditing.LargeImageIndex = 0;
            this.nbgAuditing.Name = "nbgAuditing";
            // 
            // nbiPersonalData
            // 
            this.nbiPersonalData.Caption = "个人信息审核";
            this.nbiPersonalData.LargeImageIndex = 1;
            this.nbiPersonalData.Name = "nbiPersonalData";
            this.nbiPersonalData.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiPersonalData_LinkClicked);
            // 
            // nbiGroup
            // 
            this.nbiGroup.Caption = "分组信息审核";
            this.nbiGroup.LargeImageIndex = 2;
            this.nbiGroup.Name = "nbiGroup";
            this.nbiGroup.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiGroup_LinkClicked);
            // 
            // nbgInfoUpdated
            // 
            this.nbgInfoUpdated.Caption = "信息更新审核";
            this.nbgInfoUpdated.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            this.nbgInfoUpdated.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.nbgInfoUpdated.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiInfoAuditing),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiInfoAllocating),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiInfoAudited),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiInfoAuditedLog),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiInfoStatistics)});
            this.nbgInfoUpdated.LargeImageIndex = 3;
            this.nbgInfoUpdated.Name = "nbgInfoUpdated";
            // 
            // nbiInfoAuditing
            // 
            this.nbiInfoAuditing.Caption = "信息更新待初审";
            this.nbiInfoAuditing.LargeImageIndex = 4;
            this.nbiInfoAuditing.Name = "nbiInfoAuditing";
            this.nbiInfoAuditing.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiInfoAuditing_LinkClicked);
            // 
            // nbiInfoAllocating
            // 
            this.nbiInfoAllocating.Caption = "信息更新待分配";
            this.nbiInfoAllocating.LargeImageIndex = 6;
            this.nbiInfoAllocating.Name = "nbiInfoAllocating";
            this.nbiInfoAllocating.LinkPressed += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiInfoAllocating_LinkPressed);
            // 
            // nbiInfoAudited
            // 
            this.nbiInfoAudited.Caption = "信息更新待终审";
            this.nbiInfoAudited.LargeImageIndex = 5;
            this.nbiInfoAudited.Name = "nbiInfoAudited";
            this.nbiInfoAudited.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiInfoAudited_LinkClicked);
            // 
            // nbiInfoAuditedLog
            // 
            this.nbiInfoAuditedLog.Caption = "信息更新日志";
            this.nbiInfoAuditedLog.LargeImageIndex = 5;
            this.nbiInfoAuditedLog.Name = "nbiInfoAuditedLog";
            this.nbiInfoAuditedLog.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiInfoAuditedLog_LinkClicked);
            // 
            // nbiInfoStatistics
            // 
            this.nbiInfoStatistics.Caption = "信息更新已统计";
            this.nbiInfoStatistics.LargeImageIndex = 8;
            this.nbiInfoStatistics.Name = "nbiInfoStatistics";
            this.nbiInfoStatistics.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiInfoStatistics_LinkClicked);
            // 
            // navBarSeparatorItem1
            // 
            this.navBarSeparatorItem1.CanDrag = false;
            this.navBarSeparatorItem1.Enabled = false;
            this.navBarSeparatorItem1.Hint = null;
            this.navBarSeparatorItem1.LargeImageIndex = 0;
            this.navBarSeparatorItem1.LargeImageSize = new System.Drawing.Size(0, 0);
            this.navBarSeparatorItem1.Name = "navBarSeparatorItem1";
            this.navBarSeparatorItem1.SmallImageIndex = 0;
            this.navBarSeparatorItem1.SmallImageSize = new System.Drawing.Size(0, 0);
            // 
            // navBarSeparatorItem2
            // 
            this.navBarSeparatorItem2.CanDrag = false;
            this.navBarSeparatorItem2.Enabled = false;
            this.navBarSeparatorItem2.Hint = null;
            this.navBarSeparatorItem2.LargeImageIndex = 0;
            this.navBarSeparatorItem2.LargeImageSize = new System.Drawing.Size(0, 0);
            this.navBarSeparatorItem2.Name = "navBarSeparatorItem2";
            this.navBarSeparatorItem2.SmallImageIndex = 0;
            this.navBarSeparatorItem2.SmallImageSize = new System.Drawing.Size(0, 0);
            // 
            // icAuditingLarge
            // 
            this.icAuditingLarge.ImageSize = new System.Drawing.Size(32, 32);
            this.icAuditingLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icAuditingLarge.ImageStream")));
            this.icAuditingLarge.Images.SetKeyName(0, "MyAuditingModule_CommonAuditing.png");
            this.icAuditingLarge.Images.SetKeyName(1, "MyAuditingModule_PersonalAduting.png");
            this.icAuditingLarge.Images.SetKeyName(2, "MyAuditingModule_GroupAduting.png");
            this.icAuditingLarge.Images.SetKeyName(3, "MyAuditingModule_InfoUpdated.png");
            this.icAuditingLarge.Images.SetKeyName(4, "MyAuditingModule_InfoAuditing.png");
            this.icAuditingLarge.Images.SetKeyName(5, "MyAuditingModule_InfoAudited.png");
            this.icAuditingLarge.Images.SetKeyName(6, "MyAuditingModule_InfoAllocating.png");
            this.icAuditingLarge.Images.SetKeyName(7, "MyAuditingModule_InfoAllocated.png");
            this.icAuditingLarge.Images.SetKeyName(8, "MyAuditingModule_InfoStatistics.png");
            // 
            // icAuditingSmall
            // 
            this.icAuditingSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icAuditingSmall.ImageStream")));
            this.icAuditingSmall.Images.SetKeyName(0, "MyAuditingModule_Auditing_Caption.png");
            // 
            // DataAuditingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.nbcAuditing);
            this.Name = "DataAuditingControl";
            this.Size = new System.Drawing.Size(1000, 477);
            this.Load += new System.EventHandler(this.DataAuditingControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nbcAuditing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAuditingLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAuditingSmall)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraNavBar.NavBarControl nbcAuditing;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem1;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem2;
        private DevExpress.Utils.ImageCollection icAuditingLarge;
        private DevExpress.Utils.ImageCollection icAuditingSmall;
        private DevExpress.XtraNavBar.NavBarGroup nbgInfoUpdated;
        private DevExpress.XtraNavBar.NavBarItem nbiInfoAuditing;
        private DevExpress.XtraNavBar.NavBarItem nbiInfoAuditedLog;
        private DevExpress.XtraNavBar.NavBarItem nbiInfoAllocating;
        private DevExpress.XtraNavBar.NavBarItem nbiInfoStatistics;
        private DevExpress.XtraNavBar.NavBarGroup nbgAuditing;
        private DevExpress.XtraNavBar.NavBarItem nbiPersonalData;
        private DevExpress.XtraNavBar.NavBarItem nbiGroup;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.XtraNavBar.NavBarItem nbiInfoAudited;
    }
}
