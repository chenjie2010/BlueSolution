namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class CustomQueryDefinitionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomQueryDefinitionForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiVerify = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.scSecond = new DevExpress.XtraEditors.SeparatorControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.icmbDataWarehouse = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icDataWarehouse = new DevExpress.Utils.ImageCollection(this.components);
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomViewName = new DevExpress.XtraEditors.TextEdit();
            this.meConditions = new DevExpress.XtraEditors.MemoEdit();
            this.lblViewName = new DevExpress.XtraEditors.LabelControl();
            this.defaultBarAndDockingController = new DevExpress.XtraBars.DefaultBarAndDockingController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomViewName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meConditions.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController.Controller)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.icTools;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiSave,
            this.bbiVerify,
            this.bbiClose});
            this.barManager.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bbiVerify, "校验(&V)", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "保存(&S)";
            this.bbiSave.Id = 0;
            this.bbiSave.ImageIndex = 0;
            this.bbiSave.Name = "bbiSave";
            this.bbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSave_ItemClick);
            // 
            // bbiVerify
            // 
            this.bbiVerify.Caption = "barButtonItem2";
            this.bbiVerify.Id = 1;
            this.bbiVerify.ImageIndex = 1;
            this.bbiVerify.Name = "bbiVerify";
            this.bbiVerify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiVerify_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.Caption = "关闭(&C)";
            this.bbiClose.Id = 2;
            this.bbiClose.ImageIndex = 2;
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(568, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 344);
            this.barDockControlBottom.Size = new System.Drawing.Size(568, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 318);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(568, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 318);
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "Tools_Save.png");
            this.icTools.Images.SetKeyName(1, "Tools_Check.png");
            this.icTools.Images.SetKeyName(2, "Common_Close_1.png");
            // 
            // scSecond
            // 
            this.scSecond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.scSecond.Location = new System.Drawing.Point(8, 52);
            this.scSecond.Name = "scSecond";
            this.scSecond.Size = new System.Drawing.Size(555, 23);
            this.scSecond.TabIndex = 56;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.icmbDataWarehouse);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtCustomViewName);
            this.groupControl1.Controls.Add(this.meConditions);
            this.groupControl1.Controls.Add(this.lblViewName);
            this.groupControl1.Controls.Add(this.scSecond);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 26);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(568, 318);
            this.groupControl1.TabIndex = 64;
            this.groupControl1.Text = "自定义查询设计";
            // 
            // icmbDataWarehouse
            // 
            this.icmbDataWarehouse.Location = new System.Drawing.Point(84, 31);
            this.icmbDataWarehouse.Name = "icmbDataWarehouse";
            this.icmbDataWarehouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDataWarehouse.Properties.SmallImages = this.icDataWarehouse;
            this.icmbDataWarehouse.Size = new System.Drawing.Size(193, 20);
            this.icmbDataWarehouse.TabIndex = 0;
            // 
            // icDataWarehouse
            // 
            this.icDataWarehouse.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataWarehouse.ImageStream")));
            this.icDataWarehouse.Images.SetKeyName(0, "Common_Number_First.png");
            this.icDataWarehouse.Images.SetKeyName(1, "Common_Number_Second.png");
            this.icDataWarehouse.Images.SetKeyName(2, "Common_Number_Third.png");
            this.icDataWarehouse.Images.SetKeyName(3, "Common_Number_Fourth.png");
            this.icDataWarehouse.Images.SetKeyName(4, "Common_Number_Fifth.png");
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 34);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 58;
            this.labelControl2.Text = "数据仓库：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 79);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 57;
            this.labelControl1.Text = "查询语句：";
            // 
            // txtCustomViewName
            // 
            this.txtCustomViewName.Location = new System.Drawing.Point(365, 30);
            this.txtCustomViewName.Name = "txtCustomViewName";
            this.txtCustomViewName.Properties.MaxLength = 512;
            this.txtCustomViewName.Properties.ReadOnly = true;
            this.txtCustomViewName.Size = new System.Drawing.Size(193, 20);
            this.txtCustomViewName.TabIndex = 16;
            // 
            // meConditions
            // 
            this.meConditions.Location = new System.Drawing.Point(83, 76);
            this.meConditions.MenuManager = this.barManager;
            this.meConditions.Name = "meConditions";
            this.meConditions.Properties.MaxLength = 4000;
            this.meConditions.Size = new System.Drawing.Size(473, 230);
            this.meConditions.TabIndex = 1;
            // 
            // lblViewName
            // 
            this.lblViewName.Location = new System.Drawing.Point(290, 32);
            this.lblViewName.Name = "lblViewName";
            this.lblViewName.Size = new System.Drawing.Size(72, 14);
            this.lblViewName.TabIndex = 17;
            this.lblViewName.Text = "自定义查询：";
            // 
            // defaultBarAndDockingController
            // 
            this.defaultBarAndDockingController.Controller.LookAndFeel.SkinName = "Money Twins";
            this.defaultBarAndDockingController.Controller.LookAndFeel.UseDefaultLookAndFeel = false;
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // CustomQueryDefinitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 344);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CustomQueryDefinitionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义查询设置";
            this.Load += new System.EventHandler(this.QuerySettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomViewName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meConditions.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController.Controller)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiVerify;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.XtraEditors.SeparatorControl scSecond;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.MemoEdit meConditions;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblViewName;
        private DevExpress.XtraBars.DefaultBarAndDockingController defaultBarAndDockingController;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDataWarehouse;
        private DevExpress.Utils.ImageCollection icDataWarehouse;
        private DevExpress.XtraEditors.TextEdit txtCustomViewName;
    }
}