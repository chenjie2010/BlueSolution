namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    partial class DataFieldListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataFieldListForm));
            this.gcLeft = new DevExpress.XtraEditors.GroupControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrevious = new DevExpress.XtraEditors.SimpleButton();
            this.icArrow = new DevExpress.Utils.ImageCollection(this.components);
            this.btnBottom = new DevExpress.XtraEditors.SimpleButton();
            this.btnTop = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.lstDataFields = new DevExpress.XtraEditors.ListBoxControl();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barTools = new DevExpress.XtraBars.Bar();
            this.bbiAdd = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRemove = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcLeft)).BeginInit();
            this.gcLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            this.SuspendLayout();
            // 
            // gcLeft
            // 
            this.gcLeft.Controls.Add(this.panelControl1);
            this.gcLeft.Controls.Add(this.separatorControl1);
            this.gcLeft.Controls.Add(this.lblTip);
            this.gcLeft.Controls.Add(this.lstDataFields);
            this.gcLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLeft.Location = new System.Drawing.Point(0, 26);
            this.gcLeft.Name = "gcLeft";
            this.gcLeft.Size = new System.Drawing.Size(288, 369);
            this.gcLeft.TabIndex = 0;
            this.gcLeft.Text = "字段列表";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnPrevious);
            this.panelControl1.Controls.Add(this.btnBottom);
            this.panelControl1.Controls.Add(this.btnTop);
            this.panelControl1.Controls.Add(this.btnNext);
            this.panelControl1.Location = new System.Drawing.Point(49, 261);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(179, 48);
            this.panelControl1.TabIndex = 8;
            // 
            // btnPrevious
            // 
            this.btnPrevious.ImageIndex = 1;
            this.btnPrevious.ImageList = this.icArrow;
            this.btnPrevious.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnPrevious.Location = new System.Drawing.Point(52, 8);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(32, 32);
            this.btnPrevious.TabIndex = 6;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // icArrow
            // 
            this.icArrow.ImageSize = new System.Drawing.Size(24, 24);
            this.icArrow.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icArrow.ImageStream")));
            this.icArrow.Images.SetKeyName(0, "Common_Arrow_Top_Big.png");
            this.icArrow.Images.SetKeyName(1, "Common_Arrow_Down_Big.png");
            this.icArrow.Images.SetKeyName(2, "Common_Arrow_Up_Big.png");
            this.icArrow.Images.SetKeyName(3, "Common_Arrow_Bottom_Big.png");
            // 
            // btnBottom
            // 
            this.btnBottom.ImageIndex = 3;
            this.btnBottom.ImageList = this.icArrow;
            this.btnBottom.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBottom.Location = new System.Drawing.Point(138, 8);
            this.btnBottom.Name = "btnBottom";
            this.btnBottom.Size = new System.Drawing.Size(32, 32);
            this.btnBottom.TabIndex = 7;
            this.btnBottom.Click += new System.EventHandler(this.btnBottom_Click);
            // 
            // btnTop
            // 
            this.btnTop.ImageIndex = 0;
            this.btnTop.ImageList = this.icArrow;
            this.btnTop.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnTop.Location = new System.Drawing.Point(9, 8);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(32, 32);
            this.btnTop.TabIndex = 4;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // btnNext
            // 
            this.btnNext.ImageIndex = 2;
            this.btnNext.ImageList = this.icArrow;
            this.btnNext.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNext.Location = new System.Drawing.Point(95, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(32, 32);
            this.btnNext.TabIndex = 5;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(5, 311);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(326, 23);
            this.separatorControl1.TabIndex = 1;
            // 
            // lblTip
            // 
            this.lblTip.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Information;
            this.lblTip.Appearance.ImageIndex = 4;
            this.lblTip.Appearance.ImageList = this.icTools;
            this.lblTip.Appearance.Options.UseImage = true;
            this.lblTip.Appearance.Options.UseImageIndex = true;
            this.lblTip.Appearance.Options.UseImageList = true;
            this.lblTip.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblTip.Location = new System.Drawing.Point(5, 328);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(37, 36);
            this.lblTip.TabIndex = 2;
            this.lblTip.Tag = "共有{0}个字段被选中";
            this.lblTip.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.lblTip.ToolTipTitle = "选择的字段";
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "Tools_Save.png");
            this.icTools.Images.SetKeyName(1, "Tools_Cancel.png");
            this.icTools.Images.SetKeyName(2, "Tools_Check.png");
            this.icTools.Images.SetKeyName(3, "Tools_Clear.png");
            this.icTools.Images.SetKeyName(4, "Tools_Add.png");
            this.icTools.Images.SetKeyName(5, "Tools_Remove.png");
            // 
            // lstDataFields
            // 
            this.lstDataFields.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstDataFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstDataFields.Location = new System.Drawing.Point(2, 21);
            this.lstDataFields.Name = "lstDataFields";
            this.lstDataFields.Size = new System.Drawing.Size(284, 231);
            this.lstDataFields.TabIndex = 3;
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTools});
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.icTools;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiSave,
            this.bbiCancel,
            this.bbiAdd,
            this.bbiRemove});
            this.barManager.MaxItemId = 6;
            // 
            // barTools
            // 
            this.barTools.BarName = "Tools";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 0;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiAdd, "", false, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiRemove, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barTools.OptionsBar.AllowQuickCustomization = false;
            this.barTools.OptionsBar.DrawBorder = false;
            this.barTools.OptionsBar.RotateWhenVertical = false;
            this.barTools.OptionsBar.UseWholeRow = true;
            this.barTools.Text = "Tools";
            // 
            // bbiAdd
            // 
            this.bbiAdd.Caption = "增加";
            this.bbiAdd.Id = 4;
            this.bbiAdd.ImageIndex = 4;
            this.bbiAdd.Name = "bbiAdd";
            this.bbiAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAdd_ItemClick);
            // 
            // bbiRemove
            // 
            this.bbiRemove.Caption = "移除";
            this.bbiRemove.Id = 5;
            this.bbiRemove.ImageIndex = 5;
            this.bbiRemove.Name = "bbiRemove";
            this.bbiRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRemove_ItemClick);
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "保存(&S)";
            this.bbiSave.Id = 0;
            this.bbiSave.ImageIndex = 0;
            this.bbiSave.Name = "bbiSave";
            this.bbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSave_ItemClick);
            // 
            // bbiCancel
            // 
            this.bbiCancel.Caption = "取消(&C)";
            this.bbiCancel.Id = 1;
            this.bbiCancel.ImageIndex = 1;
            this.bbiCancel.Name = "bbiCancel";
            this.bbiCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCancel_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(288, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 395);
            this.barDockControlBottom.Size = new System.Drawing.Size(288, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 369);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(288, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 369);
            // 
            // DataFieldListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 395);
            this.Controls.Add(this.gcLeft);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataFieldListForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字段列表设置";
            this.Load += new System.EventHandler(this.DataFieldListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcLeft)).EndInit();
            this.gcLeft.ResumeLayout(false);
            this.gcLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.icArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcLeft;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barTools;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiCancel;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.XtraEditors.ListBoxControl lstDataFields;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraBars.BarButtonItem bbiAdd;
        private DevExpress.XtraBars.BarButtonItem bbiRemove;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnBottom;
        private DevExpress.Utils.ImageCollection icArrow;
        private DevExpress.XtraEditors.SimpleButton btnPrevious;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnTop;
    }
}