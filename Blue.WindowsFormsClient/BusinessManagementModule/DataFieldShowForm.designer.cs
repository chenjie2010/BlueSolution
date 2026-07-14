namespace Blue.WindowsFormsClient
{
    partial class DataFieldShowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataFieldShowForm));
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.hleConditionSetting = new DevExpress.XtraEditors.HyperLinkEdit();
            this.meConditions = new DevExpress.XtraEditors.MemoEdit();
            this.meDataFieldFormat = new DevExpress.XtraEditors.MemoEdit();
            this.chkQueryAllowed = new DevExpress.XtraEditors.CheckEdit();
            this.cmbDataField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.icmbDataFieldMode = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icDataFieldMode = new DevExpress.Utils.ImageCollection(this.components);
            this.lblConditions = new DevExpress.XtraEditors.LabelControl();
            this.lblDataFieldFormat = new DevExpress.XtraEditors.LabelControl();
            this.lblQueryAllowed = new DevExpress.XtraEditors.LabelControl();
            this.lblTableJoin = new DevExpress.XtraEditors.LabelControl();
            this.lblTableRelation = new DevExpress.XtraEditors.LabelControl();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiVerfiy = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.defaultBarAndDockingController = new DevExpress.XtraBars.DefaultBarAndDockingController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hleConditionSetting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meConditions.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meDataFieldFormat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkQueryAllowed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataFieldMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController.Controller)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.Controls.Add(this.hleConditionSetting);
            this.gcMain.Controls.Add(this.meConditions);
            this.gcMain.Controls.Add(this.meDataFieldFormat);
            this.gcMain.Controls.Add(this.chkQueryAllowed);
            this.gcMain.Controls.Add(this.cmbDataField);
            this.gcMain.Controls.Add(this.icmbDataFieldMode);
            this.gcMain.Controls.Add(this.lblConditions);
            this.gcMain.Controls.Add(this.lblDataFieldFormat);
            this.gcMain.Controls.Add(this.lblQueryAllowed);
            this.gcMain.Controls.Add(this.lblTableJoin);
            this.gcMain.Controls.Add(this.lblTableRelation);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 26);
            this.gcMain.Name = "gcMain";
            this.gcMain.Padding = new System.Windows.Forms.Padding(2);
            this.gcMain.Size = new System.Drawing.Size(412, 272);
            this.gcMain.TabIndex = 0;
            this.gcMain.Text = "选择项";
            // 
            // hleConditionSetting
            // 
            this.hleConditionSetting.EditValue = "条件设置...";
            this.hleConditionSetting.Location = new System.Drawing.Point(327, 247);
            this.hleConditionSetting.Name = "hleConditionSetting";
            this.hleConditionSetting.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleConditionSetting.Properties.Appearance.Options.UseBackColor = true;
            this.hleConditionSetting.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleConditionSetting.Size = new System.Drawing.Size(73, 18);
            this.hleConditionSetting.TabIndex = 5;
            // 
            // meConditions
            // 
            this.meConditions.Location = new System.Drawing.Point(77, 184);
            this.meConditions.Name = "meConditions";
            this.meConditions.Properties.MaxLength = 4000;
            this.meConditions.Size = new System.Drawing.Size(326, 57);
            this.meConditions.TabIndex = 4;
            // 
            // meDataFieldFormat
            // 
            this.meDataFieldFormat.Location = new System.Drawing.Point(77, 134);
            this.meDataFieldFormat.Name = "meDataFieldFormat";
            this.meDataFieldFormat.Properties.MaxLength = 4000;
            this.meDataFieldFormat.Size = new System.Drawing.Size(326, 36);
            this.meDataFieldFormat.TabIndex = 3;
            // 
            // chkQueryAllowed
            // 
            this.chkQueryAllowed.Location = new System.Drawing.Point(77, 101);
            this.chkQueryAllowed.Name = "chkQueryAllowed";
            this.chkQueryAllowed.Properties.Caption = "";
            this.chkQueryAllowed.Size = new System.Drawing.Size(19, 19);
            this.chkQueryAllowed.TabIndex = 2;
            // 
            // cmbDataField
            // 
            this.cmbDataField.Location = new System.Drawing.Point(77, 33);
            this.cmbDataField.Name = "cmbDataField";
            this.cmbDataField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDataField.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDataField.Size = new System.Drawing.Size(326, 20);
            this.cmbDataField.TabIndex = 0;
            // 
            // icmbDataFieldMode
            // 
            this.icmbDataFieldMode.Location = new System.Drawing.Point(77, 67);
            this.icmbDataFieldMode.Name = "icmbDataFieldMode";
            this.icmbDataFieldMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDataFieldMode.Properties.SmallImages = this.icDataFieldMode;
            this.icmbDataFieldMode.Size = new System.Drawing.Size(326, 20);
            this.icmbDataFieldMode.TabIndex = 1;
            // 
            // icDataFieldMode
            // 
            this.icDataFieldMode.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataFieldMode.ImageStream")));
            this.icDataFieldMode.Images.SetKeyName(0, "Common_DataFieldMode_Show_Small.png");
            this.icDataFieldMode.Images.SetKeyName(1, "Common_DataFieldMode_Group_Small.png");
            this.icDataFieldMode.Images.SetKeyName(2, "Common_DataFieldMode_Condition_Small.png");
            // 
            // lblConditions
            // 
            this.lblConditions.Location = new System.Drawing.Point(12, 185);
            this.lblConditions.Name = "lblConditions";
            this.lblConditions.Size = new System.Drawing.Size(60, 14);
            this.lblConditions.TabIndex = 21;
            this.lblConditions.Text = "预设条件：";
            // 
            // lblDataFieldFormat
            // 
            this.lblDataFieldFormat.Location = new System.Drawing.Point(12, 136);
            this.lblDataFieldFormat.Name = "lblDataFieldFormat";
            this.lblDataFieldFormat.Size = new System.Drawing.Size(60, 14);
            this.lblDataFieldFormat.TabIndex = 20;
            this.lblDataFieldFormat.Text = "字段格式：";
            // 
            // lblQueryAllowed
            // 
            this.lblQueryAllowed.Location = new System.Drawing.Point(12, 103);
            this.lblQueryAllowed.Name = "lblQueryAllowed";
            this.lblQueryAllowed.Size = new System.Drawing.Size(60, 14);
            this.lblQueryAllowed.TabIndex = 18;
            this.lblQueryAllowed.Text = "查询字段：";
            // 
            // lblTableJoin
            // 
            this.lblTableJoin.Location = new System.Drawing.Point(12, 69);
            this.lblTableJoin.Name = "lblTableJoin";
            this.lblTableJoin.Size = new System.Drawing.Size(60, 14);
            this.lblTableJoin.TabIndex = 17;
            this.lblTableJoin.Text = "字段模式：";
            // 
            // lblTableRelation
            // 
            this.lblTableRelation.Location = new System.Drawing.Point(12, 35);
            this.lblTableRelation.Name = "lblTableRelation";
            this.lblTableRelation.Size = new System.Drawing.Size(60, 14);
            this.lblTableRelation.TabIndex = 16;
            this.lblTableRelation.Text = "字段选择：";
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
            this.bbiVerfiy,
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiVerfiy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // bbiVerfiy
            // 
            this.bbiVerfiy.Caption = "校验(&V)";
            this.bbiVerfiy.Id = 1;
            this.bbiVerfiy.ImageIndex = 1;
            this.bbiVerfiy.Name = "bbiVerfiy";
            this.bbiVerfiy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiVerfiy_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(412, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 298);
            this.barDockControlBottom.Size = new System.Drawing.Size(412, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 272);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(412, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 272);
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "Tools_Save.png");
            this.icTools.Images.SetKeyName(1, "Tools_Check.png");
            this.icTools.Images.SetKeyName(2, "Common_Close_1.png");
            // 
            // defaultBarAndDockingController
            // 
            this.defaultBarAndDockingController.Controller.LookAndFeel.SkinName = "Money Twins";
            this.defaultBarAndDockingController.Controller.LookAndFeel.UseDefaultLookAndFeel = false;
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // DataFieldShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 298);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataFieldShowForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询字段设置";
            this.Load += new System.EventHandler(this.DataTableSelectedItemsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            this.gcMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hleConditionSetting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meConditions.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meDataFieldFormat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkQueryAllowed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataFieldMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController.Controller)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.Utils.ImageCollection icDataFieldMode;
        private DevExpress.XtraEditors.LabelControl lblDataFieldFormat;
        private DevExpress.XtraEditors.LabelControl lblQueryAllowed;
        private DevExpress.XtraEditors.LabelControl lblTableJoin;
        private DevExpress.XtraEditors.LabelControl lblTableRelation;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDataFieldMode;
        private DevExpress.XtraEditors.LabelControl lblConditions;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDataField;
        private DevExpress.XtraEditors.CheckEdit chkQueryAllowed;
        private DevExpress.XtraEditors.MemoEdit meDataFieldFormat;
        private DevExpress.XtraEditors.MemoEdit meConditions;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiVerfiy;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private DevExpress.XtraBars.DefaultBarAndDockingController defaultBarAndDockingController;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.XtraEditors.HyperLinkEdit hleConditionSetting;
    }
}