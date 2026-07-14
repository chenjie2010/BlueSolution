namespace Blue.WindowsFormsClient
{
    partial class WorkflowDesignerForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkflowDesignerForm));
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar = new DevExpress.XtraBars.Bar();
            this.btnItmSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imgTools = new System.Windows.Forms.ImageList(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.dpnlLeft = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.nbcNodes = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgList = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbItmProcess = new DevExpress.XtraNavBar.NavBarItem();
            this.nbItmPolicy = new DevExpress.XtraNavBar.NavBarItem();
            this.imgNodes = new System.Windows.Forms.ImageList(this.components);
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.pnlMain = new AppFramework.WinFormsControls.WorkflowPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.dpnlLeft.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbcNodes)).BeginInit();
            this.dockPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            this.dockManager.MenuManager = this.barManager;
            this.dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dpnlLeft,
            this.dockPanel2});
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.DockManager = this.dockManager;
            this.barManager.Form = this;
            this.barManager.Images = this.imgTools;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.btnItmSave});
            this.barManager.MaxItemId = 2;
            // 
            // bar
            // 
            this.bar.BarName = "Tools";
            this.bar.DockCol = 0;
            this.bar.DockRow = 0;
            this.bar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmSave)});
            this.bar.OptionsBar.AllowQuickCustomization = false;
            this.bar.OptionsBar.DrawDragBorder = false;
            this.bar.OptionsBar.UseWholeRow = true;
            this.bar.Text = "Tools";
            // 
            // btnItmSave
            // 
            this.btnItmSave.Caption = "保存(&S)";
            this.btnItmSave.Id = 1;
            this.btnItmSave.ImageIndex = 0;
            this.btnItmSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.btnItmSave.Name = "btnItmSave";
            this.btnItmSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnItmSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1209, 39);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 727);
            this.barDockControlBottom.Size = new System.Drawing.Size(1209, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 39);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 688);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1209, 39);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 688);
            // 
            // imgTools
            // 
            this.imgTools.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTools.ImageStream")));
            this.imgTools.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTools.Images.SetKeyName(0, "Common_Save.ico");
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "保存";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // dpnlLeft
            // 
            this.dpnlLeft.Controls.Add(this.dockPanel1_Container);
            this.dpnlLeft.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpnlLeft.ID = new System.Guid("6de745e3-e47f-4e09-9203-74f644b9773d");
            this.dpnlLeft.Location = new System.Drawing.Point(0, 39);
            this.dpnlLeft.Name = "dpnlLeft";
            this.dpnlLeft.OriginalSize = new System.Drawing.Size(200, 200);
            this.dpnlLeft.Size = new System.Drawing.Size(200, 688);
            this.dpnlLeft.Text = "工具栏";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.nbcNodes);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(191, 661);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // nbcNodes
            // 
            this.nbcNodes.ActiveGroup = this.nbgList;
            this.nbcNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nbcNodes.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgList});
            this.nbcNodes.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbItmProcess,
            this.nbItmPolicy});
            this.nbcNodes.Location = new System.Drawing.Point(0, 0);
            this.nbcNodes.Name = "nbcNodes";
            this.nbcNodes.OptionsNavPane.ExpandedWidth = 191;
            this.nbcNodes.Size = new System.Drawing.Size(191, 661);
            this.nbcNodes.SmallImages = this.imgNodes;
            this.nbcNodes.StoreDefaultPaintStyleName = true;
            this.nbcNodes.TabIndex = 0;
            this.nbcNodes.Text = "节点";
            // 
            // nbgList
            // 
            this.nbgList.Caption = "节点列表";
            this.nbgList.Expanded = true;
            this.nbgList.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbItmProcess),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbItmPolicy)});
            this.nbgList.Name = "nbgList";
            // 
            // nbItmProcess
            // 
            this.nbItmProcess.Caption = "流程节点";
            this.nbItmProcess.Name = "nbItmProcess";
            this.nbItmProcess.SmallImageIndex = 0;
            // 
            // nbItmPolicy
            // 
            this.nbItmPolicy.Caption = "判断节点";
            this.nbItmPolicy.Name = "nbItmPolicy";
            this.nbItmPolicy.SmallImageIndex = 1;
            // 
            // imgNodes
            // 
            this.imgNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgNodes.ImageStream")));
            this.imgNodes.TransparentColor = System.Drawing.Color.Transparent;
            this.imgNodes.Images.SetKeyName(0, "WorkflowDesigner_PorcessNode.ico");
            this.imgNodes.Images.SetKeyName(1, "WorkflowDesigner_PolicyNode.ico");
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel2.ID = new System.Guid("74f0fb13-86b8-4b05-bb17-931ead8bf316");
            this.dockPanel2.Location = new System.Drawing.Point(1009, 39);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel2.Size = new System.Drawing.Size(200, 688);
            this.dockPanel2.Text = "属性";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Location = new System.Drawing.Point(5, 23);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(191, 661);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(200, 39);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(809, 688);
            this.pnlMain.TabIndex = 6;
            // 
            // WorkflowDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 727);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.dpnlLeft);
            this.Controls.Add(this.dockPanel2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WorkflowDesignerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工作流设计";
            this.Load += new System.EventHandler(this.WorkflowDesignerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.dpnlLeft.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nbcNodes)).EndInit();
            this.dockPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Docking.DockPanel dpnlLeft;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnItmSave;
        private System.Windows.Forms.ImageList imgTools;
        private DevExpress.XtraNavBar.NavBarControl nbcNodes;
        private DevExpress.XtraNavBar.NavBarGroup nbgList;
        private DevExpress.XtraNavBar.NavBarItem nbItmProcess;
        private System.Windows.Forms.ImageList imgNodes;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraNavBar.NavBarItem nbItmPolicy;
        private AppFramework.WinFormsControls.WorkflowPanel pnlMain;
    }
}

