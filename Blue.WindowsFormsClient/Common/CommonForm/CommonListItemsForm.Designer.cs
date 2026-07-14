namespace Blue.WindowsFormsClient
{
    partial class CommonListItemsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonListItemsForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiCreate = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRemove = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icToolItems = new DevExpress.Utils.ImageCollection(this.components);
            this.gcItems = new DevExpress.XtraEditors.GroupControl();
            this.btnBottom = new DevExpress.XtraEditors.SimpleButton();
            this.icButton = new DevExpress.Utils.ImageCollection(this.components);
            this.lstItems = new DevExpress.XtraEditors.ListBoxControl();
            this.btnPrevious = new DevExpress.XtraEditors.SimpleButton();
            this.btnTop = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icToolItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).BeginInit();
            this.gcItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstItems)).BeginInit();
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
            this.barManager.Images = this.icToolItems;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiCreate,
            this.bbiRemove,
            this.bbiSave,
            this.bbiClose});
            this.barManager.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiCreate, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiRemove, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bbiCreate
            // 
            this.bbiCreate.Caption = "增加(&N)";
            this.bbiCreate.Id = 0;
            this.bbiCreate.ImageIndex = 0;
            this.bbiCreate.Name = "bbiCreate";
            this.bbiCreate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCreate_ItemClick);
            // 
            // bbiRemove
            // 
            this.bbiRemove.Caption = "移除(&R)";
            this.bbiRemove.Id = 1;
            this.bbiRemove.ImageIndex = 1;
            this.bbiRemove.Name = "bbiRemove";
            this.bbiRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRemove_ItemClick);
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "保存(&S)";
            this.bbiSave.Id = 2;
            this.bbiSave.ImageIndex = 2;
            this.bbiSave.Name = "bbiSave";
            this.bbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSave_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.Caption = "关闭(&C)";
            this.bbiClose.Id = 3;
            this.bbiClose.ImageIndex = 3;
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(362, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 429);
            this.barDockControlBottom.Size = new System.Drawing.Size(362, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 403);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(362, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 403);
            // 
            // icToolItems
            // 
            this.icToolItems.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icToolItems.ImageStream")));
            this.icToolItems.Images.SetKeyName(0, "Tools_New.png");
            this.icToolItems.Images.SetKeyName(1, "Tools_Remove.png");
            this.icToolItems.Images.SetKeyName(2, "Tools_Save.png");
            this.icToolItems.Images.SetKeyName(3, "Common_Close_1.png");
            // 
            // gcItems
            // 
            this.gcItems.Controls.Add(this.btnBottom);
            this.gcItems.Controls.Add(this.lstItems);
            this.gcItems.Controls.Add(this.btnPrevious);
            this.gcItems.Controls.Add(this.btnTop);
            this.gcItems.Controls.Add(this.btnNext);
            this.gcItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcItems.Location = new System.Drawing.Point(0, 26);
            this.gcItems.Name = "gcItems";
            this.gcItems.Size = new System.Drawing.Size(362, 403);
            this.gcItems.TabIndex = 4;
            this.gcItems.Text = "列表";
            // 
            // btnBottom
            // 
            this.btnBottom.ImageIndex = 3;
            this.btnBottom.ImageList = this.icButton;
            this.btnBottom.Location = new System.Drawing.Point(270, 368);
            this.btnBottom.Name = "btnBottom";
            this.btnBottom.Size = new System.Drawing.Size(75, 23);
            this.btnBottom.TabIndex = 8;
            this.btnBottom.Text = "置底(&B)";
            this.btnBottom.Click += new System.EventHandler(this.btnBottom_Click);
            // 
            // icButton
            // 
            this.icButton.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icButton.ImageStream")));
            this.icButton.Images.SetKeyName(0, "Common_Arrow_Top.png");
            this.icButton.Images.SetKeyName(1, "Common_Arrow_Up.png");
            this.icButton.Images.SetKeyName(2, "Common_Arrow_Down.png");
            this.icButton.Images.SetKeyName(3, "Common_Arrow_Bottom.png");
            this.icButton.Images.SetKeyName(4, "Tool_Confirm.png");
            this.icButton.Images.SetKeyName(5, "Tool_Canel.png");
            // 
            // lstItems
            // 
            this.lstItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstItems.IncrementalSearch = true;
            this.lstItems.Location = new System.Drawing.Point(2, 21);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(358, 332);
            this.lstItems.TabIndex = 0;
            // 
            // btnPrevious
            // 
            this.btnPrevious.ImageIndex = 1;
            this.btnPrevious.ImageList = this.icButton;
            this.btnPrevious.Location = new System.Drawing.Point(108, 368);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 7;
            this.btnPrevious.Text = "上移(&P)";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnTop
            // 
            this.btnTop.ImageIndex = 0;
            this.btnTop.ImageList = this.icButton;
            this.btnTop.Location = new System.Drawing.Point(22, 368);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(75, 23);
            this.btnTop.TabIndex = 5;
            this.btnTop.Text = "置顶(&T)";
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // btnNext
            // 
            this.btnNext.ImageIndex = 2;
            this.btnNext.ImageList = this.icButton;
            this.btnNext.Location = new System.Drawing.Point(189, 368);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "下移(&N)";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // CommonListItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 429);
            this.Controls.Add(this.gcItems);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CommonListItemsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "列表设置";
            this.Load += new System.EventHandler(this.CommonListItemsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icToolItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcItems)).EndInit();
            this.gcItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.icButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiCreate;
        private DevExpress.XtraBars.BarButtonItem bbiRemove;
        private DevExpress.Utils.ImageCollection icToolItems;
        private DevExpress.XtraEditors.GroupControl gcItems;
        private DevExpress.XtraEditors.ListBoxControl lstItems;
        private DevExpress.XtraEditors.SimpleButton btnBottom;
        private DevExpress.XtraEditors.SimpleButton btnPrevious;
        private DevExpress.XtraEditors.SimpleButton btnTop;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.Utils.ImageCollection icButton;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
    }
}