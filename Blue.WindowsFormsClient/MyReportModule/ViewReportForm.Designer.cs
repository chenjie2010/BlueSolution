namespace Blue.WindowsFormsClient.MyReportModule
{
    partial class ViewReportForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewReportForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barMenu = new DevExpress.XtraBars.Bar();
            this.btnItmOpen = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmCompute = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmImport = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmInsertPhoto = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmInsertBatchNumber = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmPringSetting = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmPreview = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmPrint = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmPrintPdf = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmView = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dkpnlTop = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.imglstEdit = new System.Windows.Forms.ImageList(this.components);
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.fsReporting = new FarPoint.Win.Spread.FpSpread();
            this.fsReporting_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.dkpnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fsReporting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fsReporting_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barMenu});
            this.barManager.CloseButtonAffectAllTabs = false;
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.DockManager = this.dockManager;
            this.barManager.Form = this;
            this.barManager.Images = this.imglstEdit;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItmPreview,
            this.btnItmPrint,
            this.btnItmEdit,
            this.btnItmSave,
            this.btnItmImport,
            this.btnItmCompute,
            this.btnItmOpen,
            this.btnItmPringSetting,
            this.btnItmPrintPdf,
            this.btnItmInsertPhoto,
            this.btnItmInsertBatchNumber,
            this.btnItmView});
            this.barManager.MaxItemId = 14;
            // 
            // barMenu
            // 
            this.barMenu.BarItemHorzIndent = 7;
            this.barMenu.BarItemVertIndent = 7;
            this.barMenu.BarName = "Main menu";
            this.barMenu.DockCol = 0;
            this.barMenu.DockRow = 0;
            this.barMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmOpen, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmCompute, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmImport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmInsertPhoto, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmInsertBatchNumber, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmPringSetting, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmPreview, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmPrintPdf, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmView, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barMenu.OptionsBar.AllowQuickCustomization = false;
            this.barMenu.OptionsBar.DrawDragBorder = false;
            this.barMenu.OptionsBar.UseWholeRow = true;
            this.barMenu.Text = "Main menu";
            // 
            // btnItmOpen
            // 
            this.btnItmOpen.Caption = "打开快照(&O)";
            this.btnItmOpen.Id = 8;
            this.btnItmOpen.ImageIndex = 0;
            this.btnItmOpen.Name = "btnItmOpen";
            this.btnItmOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmOpen_ItemClick);
            // 
            // btnItmEdit
            // 
            this.btnItmEdit.Caption = "编辑报表(&E)";
            this.btnItmEdit.Id = 3;
            this.btnItmEdit.ImageIndex = 1;
            this.btnItmEdit.Name = "btnItmEdit";
            this.btnItmEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmEdit_ItemClick);
            // 
            // btnItmSave
            // 
            this.btnItmSave.Caption = "保存快照(&S)";
            this.btnItmSave.Id = 4;
            this.btnItmSave.ImageIndex = 2;
            this.btnItmSave.Name = "btnItmSave";
            this.btnItmSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmSave_ItemClick);
            // 
            // btnItmCompute
            // 
            this.btnItmCompute.Caption = "报表计算(&C)";
            this.btnItmCompute.Id = 6;
            this.btnItmCompute.ImageIndex = 3;
            this.btnItmCompute.Name = "btnItmCompute";
            this.btnItmCompute.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmCompute_ItemClick);
            // 
            // btnItmImport
            // 
            this.btnItmImport.Caption = "导出Excel(&I)";
            this.btnItmImport.Id = 5;
            this.btnItmImport.ImageIndex = 4;
            this.btnItmImport.Name = "btnItmImport";
            this.btnItmImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmImport_ItemClick);
            // 
            // btnItmInsertPhoto
            // 
            this.btnItmInsertPhoto.Caption = "插入照片(&P)";
            this.btnItmInsertPhoto.Id = 11;
            this.btnItmInsertPhoto.ImageIndex = 5;
            this.btnItmInsertPhoto.Name = "btnItmInsertPhoto";
            this.btnItmInsertPhoto.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmInsertPhoto_ItemClick);
            // 
            // btnItmInsertBatchNumber
            // 
            this.btnItmInsertBatchNumber.Caption = "插入编号(&B)";
            this.btnItmInsertBatchNumber.Id = 12;
            this.btnItmInsertBatchNumber.ImageIndex = 6;
            this.btnItmInsertBatchNumber.Name = "btnItmInsertBatchNumber";
            this.btnItmInsertBatchNumber.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmInsertBatchNumber_ItemClick);
            // 
            // btnItmPringSetting
            // 
            this.btnItmPringSetting.Caption = "打印设置(&D)";
            this.btnItmPringSetting.Id = 9;
            this.btnItmPringSetting.ImageIndex = 7;
            this.btnItmPringSetting.Name = "btnItmPringSetting";
            this.btnItmPringSetting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmPringSetting_ItemClick);
            // 
            // btnItmPreview
            // 
            this.btnItmPreview.Caption = "打印预览(&L)";
            this.btnItmPreview.Id = 1;
            this.btnItmPreview.ImageIndex = 8;
            this.btnItmPreview.Name = "btnItmPreview";
            this.btnItmPreview.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmPreview_ItemClick);
            // 
            // btnItmPrint
            // 
            this.btnItmPrint.Caption = "打印(&P)";
            this.btnItmPrint.Id = 2;
            this.btnItmPrint.ImageIndex = 9;
            this.btnItmPrint.Name = "btnItmPrint";
            this.btnItmPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmPrint_ItemClick);
            // 
            // btnItmPrintPdf
            // 
            this.btnItmPrintPdf.Caption = "打印PDF(&W)";
            this.btnItmPrintPdf.Id = 10;
            this.btnItmPrintPdf.ImageIndex = 10;
            this.btnItmPrintPdf.Name = "btnItmPrintPdf";
            this.btnItmPrintPdf.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmPrintPdf_ItemClick);
            // 
            // btnItmView
            // 
            this.btnItmView.Caption = "查看数据";
            this.btnItmView.Id = 13;
            this.btnItmView.ImageIndex = 11;
            this.btnItmView.Name = "btnItmView";
            this.btnItmView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmView_ItemClick);
            // 
            // barAndDockingController
            // 
            this.barAndDockingController.LookAndFeel.SkinName = "Money Twins";
            this.barAndDockingController.LookAndFeel.UseDefaultLookAndFeel = false;
            this.barAndDockingController.PropertiesBar.AllowLinkLighting = false;
            this.barAndDockingController.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.barAndDockingController.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlTop.Size = new System.Drawing.Size(1695, 42);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 808);
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlBottom.Size = new System.Drawing.Size(1695, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 42);
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 766);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1695, 42);
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 766);
            // 
            // dockManager
            // 
            this.dockManager.Controller = this.barAndDockingController;
            this.dockManager.Form = this;
            this.dockManager.MenuManager = this.barManager;
            this.dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dkpnlTop});
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dkpnlTop
            // 
            this.dkpnlTop.Controls.Add(this.dockPanel2_Container);
            this.dkpnlTop.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top;
            this.dkpnlTop.FloatVertical = true;
            this.dkpnlTop.ID = new System.Guid("cd0f160a-6f89-4c81-9bdd-b06bda9a0495");
            this.dkpnlTop.Location = new System.Drawing.Point(0, 42);
            this.dkpnlTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dkpnlTop.Name = "dkpnlTop";
            this.dkpnlTop.Options.AllowFloating = false;
            this.dkpnlTop.Options.FloatOnDblClick = false;
            this.dkpnlTop.Options.ShowCloseButton = false;
            this.dkpnlTop.Options.ShowMaximizeButton = false;
            this.dkpnlTop.OriginalSize = new System.Drawing.Size(200, 291);
            this.dkpnlTop.Size = new System.Drawing.Size(1695, 291);
            this.dkpnlTop.Text = "用户信息";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 28);
            this.dockPanel2_Container.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(1689, 258);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // imglstEdit
            // 
            this.imglstEdit.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstEdit.ImageStream")));
            this.imglstEdit.TransparentColor = System.Drawing.Color.Maroon;
            this.imglstEdit.Images.SetKeyName(0, "");
            this.imglstEdit.Images.SetKeyName(1, "Report_Sheet__Edit.png");
            this.imglstEdit.Images.SetKeyName(2, "");
            this.imglstEdit.Images.SetKeyName(3, "Report_Sheet_CopmuteData.gif");
            this.imglstEdit.Images.SetKeyName(4, "Report_Sheet_Import_Excel.gif");
            this.imglstEdit.Images.SetKeyName(5, "Report_View_Insert_Photo.png");
            this.imglstEdit.Images.SetKeyName(6, "Report_Sheet_Serial_Number.png");
            this.imglstEdit.Images.SetKeyName(7, "Common_Reprot_Setting.png");
            this.imglstEdit.Images.SetKeyName(8, "Common_Reprot_Preview.png");
            this.imglstEdit.Images.SetKeyName(9, "Common_Reprot_Print.png");
            this.imglstEdit.Images.SetKeyName(10, "Common_Reprot_PDF.png");
            this.imglstEdit.Images.SetKeyName(11, "Auditing_Input.png");
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "2.jpg");
            this.imageListTree.Images.SetKeyName(1, "1.jpg");
            this.imageListTree.Images.SetKeyName(2, "3.jpg");
            // 
            // fsReporting
            // 
            this.fsReporting.AccessibleDescription = "fsReporting, Sheet1, Row 0, Column 0, ";
            this.fsReporting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fsReporting.Location = new System.Drawing.Point(0, 333);
            this.fsReporting.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fsReporting.Name = "fsReporting";
            this.fsReporting.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fsReporting_Sheet1});
            this.fsReporting.Size = new System.Drawing.Size(1695, 475);
            this.fsReporting.TabIndex = 4;
            this.fsReporting.TabStripInsertTab = false;
            this.fsReporting.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fsReporting_CellDoubleClick);
            // 
            // fsReporting_Sheet1
            // 
            this.fsReporting_Sheet1.Reset();
            this.fsReporting_Sheet1.SheetName = "Sheet1";
            this.fsReporting_Sheet1.ColumnCount = 30;
            this.fsReporting_Sheet1.RowCount = 30;
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.ShowIcon = false;
            this.printPreviewDialog.Visible = false;
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "数据正在加载......";
            this.progressPanel.Location = new System.Drawing.Point(728, 550);
            this.progressPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(200, 62);
            this.progressPanel.TabIndex = 10;
            this.progressPanel.Text = "数据加载中......";
            this.progressPanel.Visible = false;
            // 
            // ViewReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1695, 808);
            this.Controls.Add(this.progressPanel);
            this.Controls.Add(this.fsReporting);
            this.Controls.Add(this.dkpnlTop);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ViewReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查看报表";
            this.Load += new System.EventHandler(this.ViewReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.dkpnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fsReporting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fsReporting_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barMenu;
        private DevExpress.XtraBars.BarButtonItem btnItmPreview;
        private DevExpress.XtraBars.BarButtonItem btnItmPrint;
        private DevExpress.XtraBars.BarButtonItem btnItmImport;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        protected DevExpress.XtraBars.BarButtonItem btnItmSave;
        private FarPoint.Win.Spread.SheetView fsReporting_Sheet1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.BarButtonItem btnItmOpen;
        private DevExpress.XtraBars.BarButtonItem btnItmEdit;
        private DevExpress.XtraBars.BarButtonItem btnItmCompute;
        private FarPoint.Win.Spread.FpSpread fsReporting;
        private DevExpress.XtraBars.Docking.DockPanel dkpnlTop;
        private System.Windows.Forms.ImageList imglstEdit;
        internal System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        private DevExpress.XtraBars.BarButtonItem btnItmPringSetting;
        private DevExpress.XtraBars.BarButtonItem btnItmPrintPdf;
        private DevExpress.XtraBars.BarButtonItem btnItmInsertPhoto;
        private DevExpress.XtraBars.BarButtonItem btnItmInsertBatchNumber;
        private DevExpress.XtraBars.BarButtonItem btnItmView;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
    }
}