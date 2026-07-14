namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class SystemMessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemMessageForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnItmWrite = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imgListToolbar = new System.Windows.Forms.ImageList(this.components);
            this.devExpressGrid = new AppFramework.WinFormsControls.DevExpressGrid();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imgListToolbar;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItmEdit,
            this.btnItmWrite,
            this.btnItmDelete});
            this.barManager.MaxItemId = 6;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.btnItmWrite, "创建(&W)", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.Text = "Tools";
            // 
            // btnItmWrite
            // 
            this.btnItmWrite.Caption = "创建(&D)";
            this.btnItmWrite.Id = 2;
            this.btnItmWrite.ImageIndex = 0;
            this.btnItmWrite.Name = "btnItmWrite";
            this.btnItmWrite.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmWrite_ItemClick);
            // 
            // btnItmEdit
            // 
            this.btnItmEdit.Caption = "编辑(&B)";
            this.btnItmEdit.Id = 1;
            this.btnItmEdit.ImageIndex = 1;
            this.btnItmEdit.Name = "btnItmEdit";
            this.btnItmEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmEdit_ItemClick);
            // 
            // btnItmDelete
            // 
            this.btnItmDelete.Caption = "删除(&D)";
            this.btnItmDelete.Id = 4;
            this.btnItmDelete.ImageIndex = 2;
            this.btnItmDelete.Name = "btnItmDelete";
            this.btnItmDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmDelete_ItemClick);
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
            this.barDockControlTop.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(200)))), ((int)(((byte)(239)))));
            this.barDockControlTop.Appearance.Options.UseBackColor = true;
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1022, 34);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 630);
            this.barDockControlBottom.Size = new System.Drawing.Size(1022, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 34);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 596);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1022, 34);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 596);
            // 
            // imgListToolbar
            // 
            this.imgListToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListToolbar.ImageStream")));
            this.imgListToolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListToolbar.Images.SetKeyName(0, "Account_Mail_Write.png");
            this.imgListToolbar.Images.SetKeyName(1, "System_Notice_Edit.png");
            this.imgListToolbar.Images.SetKeyName(2, "Account_Mail_Delete.png");
            // 
            // devExpressGrid
            // 
            this.devExpressGrid.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devExpressGrid.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devExpressGrid.CheckboxColumnCaption = null;
            this.devExpressGrid.ColumnHeaderTexts = new string[] {
        "标题",
        "内容类型",
        "起始时间",
        "截止时间",
        "状态",
        "附件",
        "创建时间"};
            this.devExpressGrid.DataKeyNames = new string[] {
        "MessageId"};
            this.devExpressGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devExpressGrid.ExportedExcel = false;
            this.devExpressGrid.FootText = null;
            this.devExpressGrid.ImportedExcel = false;
            this.devExpressGrid.IsMainTable = false;
            this.devExpressGrid.IsShowCheckBox = true;
            this.devExpressGrid.Location = new System.Drawing.Point(0, 34);
            this.devExpressGrid.Name = "devExpressGrid";
            this.devExpressGrid.PageSize = 30;
            this.devExpressGrid.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devExpressGrid.Size = new System.Drawing.Size(1022, 596);
            this.devExpressGrid.TabIndex = 4;
            this.devExpressGrid.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.devExpressGrid_OnPageIndexChanged);
            this.devExpressGrid.OnDeleteClick += new System.EventHandler<DevExpress.XtraBars.ItemClickEventArgs>(this.devExpressGrid_OnDeleteClick);
            this.devExpressGrid.OnBatchDeleteClick += new System.EventHandler<DevExpress.XtraBars.ItemClickEventArgs>(this.devExpressGrid_OnBatchDeleteClick);
            this.devExpressGrid.OnCustomColumnDisplayText += new System.EventHandler<DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs>(this.devExpressGrid_OnCustomColumnDisplayText);
            // 
            // SystemMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 630);
            this.Controls.Add(this.devExpressGrid);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "SystemMessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "消息与通知";
            this.Load += new System.EventHandler(this.NoticeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnItmEdit;
        private DevExpress.XtraBars.BarButtonItem btnItmWrite;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnItmDelete;
        private System.Windows.Forms.ImageList imgListToolbar;
        private AppFramework.WinFormsControls.DevExpressGrid devExpressGrid;
    }
}