namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class CellConditionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellConditionForm));
            this.lblReportCellType = new System.Windows.Forms.Label();
            this.grpCell = new DevExpress.XtraEditors.GroupControl();
            this.icmbCellConditon = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnItmSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClear = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClose = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imglstEdit = new System.Windows.Forms.ImageList(this.components);
            this.dataTableDropdownList = new Blue.WindowsFormsClient.DataTableDropdownList();
            this.label1 = new System.Windows.Forms.Label();
            this.lblReportCellTypeTip = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.grpDataFieldCondition = new DevExpress.XtraEditors.GroupControl();
            this.pnlDataField = new System.Windows.Forms.Panel();
            this.lblCellConditonTip = new System.Windows.Forms.Label();
            this.sbtnSetting = new DevExpress.XtraEditors.SimpleButton();
            this.etxtCondition = new DevExpress.XtraEditors.TextEdit();
            this.pnlExtendDataField = new System.Windows.Forms.Panel();
            this.lstDataFieldCondition = new DevExpress.XtraEditors.ListBoxControl();
            this.sbtnAddCondition = new DevExpress.XtraEditors.SimpleButton();
            this.etxtRowOrCol = new DevExpress.XtraEditors.TextEdit();
            this.lblConditionTip = new System.Windows.Forms.Label();
            this.sbtnRemoveCondition = new DevExpress.XtraEditors.SimpleButton();
            this.lblRowOrColTip = new System.Windows.Forms.Label();
            this.lblRowOrCol = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grpCell)).BeginInit();
            this.grpCell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbCellConditon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDataFieldCondition)).BeginInit();
            this.grpDataFieldCondition.SuspendLayout();
            this.pnlDataField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etxtCondition.Properties)).BeginInit();
            this.pnlExtendDataField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataFieldCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtRowOrCol.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReportCellType
            // 
            this.lblReportCellType.AutoSize = true;
            this.lblReportCellType.Location = new System.Drawing.Point(5, 59);
            this.lblReportCellType.Name = "lblReportCellType";
            this.lblReportCellType.Size = new System.Drawing.Size(79, 14);
            this.lblReportCellType.TabIndex = 0;
            this.lblReportCellType.Text = "单元格类型：";
            // 
            // grpCell
            // 
            this.grpCell.Controls.Add(this.icmbCellConditon);
            this.grpCell.Controls.Add(this.dataTableDropdownList);
            this.grpCell.Controls.Add(this.label1);
            this.grpCell.Controls.Add(this.lblReportCellTypeTip);
            this.grpCell.Controls.Add(this.lblTable);
            this.grpCell.Controls.Add(this.lblReportCellType);
            this.grpCell.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpCell.Location = new System.Drawing.Point(0, 32);
            this.grpCell.LookAndFeel.SkinName = "Money Twins";
            this.grpCell.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grpCell.Name = "grpCell";
            this.grpCell.Size = new System.Drawing.Size(363, 84);
            this.grpCell.TabIndex = 22;
            this.grpCell.Text = "单元格基本信息";
            // 
            // icmbCellConditon
            // 
            this.icmbCellConditon.Location = new System.Drawing.Point(81, 58);
            this.icmbCellConditon.MenuManager = this.barManager;
            this.icmbCellConditon.Name = "icmbCellConditon";
            this.icmbCellConditon.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbCellConditon.Size = new System.Drawing.Size(262, 20);
            this.icmbCellConditon.TabIndex = 83;
            this.icmbCellConditon.SelectedIndexChanged += new System.EventHandler(this.icmbCellConditon_SelectedIndexChanged);
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.AllowShowToolbarsPopup = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imglstEdit;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItmSave,
            this.btnItmClear,
            this.btnItmClose});
            this.barManager.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarItemHorzIndent = 5;
            this.bar1.BarItemVertIndent = 5;
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.btnItmClear, "清除条件", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnItmSave
            // 
            this.btnItmSave.Caption = "保存条件";
            this.btnItmSave.Id = 0;
            this.btnItmSave.ImageIndex = 0;
            this.btnItmSave.Name = "btnItmSave";
            this.btnItmSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmSave_ItemClick);
            // 
            // btnItmClear
            // 
            this.btnItmClear.Caption = "清除";
            this.btnItmClear.Id = 1;
            this.btnItmClear.ImageIndex = 1;
            this.btnItmClear.Name = "btnItmClear";
            this.btnItmClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmClear_ItemClick);
            // 
            // btnItmClose
            // 
            this.btnItmClose.Caption = "关闭";
            this.btnItmClose.Id = 3;
            this.btnItmClose.ImageIndex = 3;
            this.btnItmClose.Name = "btnItmClose";
            this.btnItmClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmClose_ItemClick);
            // 
            // barAndDockingController
            // 
            this.barAndDockingController.LookAndFeel.SkinName = "Blue";
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
            this.barDockControlTop.Size = new System.Drawing.Size(363, 32);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 290);
            this.barDockControlBottom.Size = new System.Drawing.Size(363, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 32);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 258);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(363, 32);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 258);
            // 
            // imglstEdit
            // 
            this.imglstEdit.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstEdit.ImageStream")));
            this.imglstEdit.TransparentColor = System.Drawing.Color.Maroon;
            this.imglstEdit.Images.SetKeyName(0, "");
            this.imglstEdit.Images.SetKeyName(1, "Report_Sheet_Clear.png");
            this.imglstEdit.Images.SetKeyName(2, "");
            this.imglstEdit.Images.SetKeyName(3, "");
            // 
            // dataTableDropdownList
            // 
            this.dataTableDropdownList.CustomCategoryContract = null;
            this.dataTableDropdownList.CustomDatabaseContract = null;
            this.dataTableDropdownList.CustomTableContract = null;
            this.dataTableDropdownList.DataWarehouseId = ((byte)(0));
            this.dataTableDropdownList.Location = new System.Drawing.Point(81, 28);
            this.dataTableDropdownList.Name = "dataTableDropdownList";
            this.dataTableDropdownList.OnlySelectedLeaf = true;
            this.dataTableDropdownList.Size = new System.Drawing.Size(262, 27);
            this.dataTableDropdownList.SkinName = "Blue";
            this.dataTableDropdownList.TabIndex = 82;
            this.dataTableDropdownList.TableFilter = AppFramework.Core.TableFilter.All;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(346, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 14);
            this.label1.TabIndex = 81;
            this.label1.Text = "*";
            // 
            // lblReportCellTypeTip
            // 
            this.lblReportCellTypeTip.AutoSize = true;
            this.lblReportCellTypeTip.ForeColor = System.Drawing.Color.Red;
            this.lblReportCellTypeTip.Location = new System.Drawing.Point(346, 60);
            this.lblReportCellTypeTip.Name = "lblReportCellTypeTip";
            this.lblReportCellTypeTip.Size = new System.Drawing.Size(14, 14);
            this.lblReportCellTypeTip.TabIndex = 77;
            this.lblReportCellTypeTip.Text = "*";
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(17, 30);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(67, 14);
            this.lblTable.TabIndex = 79;
            this.lblTable.Text = "选择的表：";
            // 
            // grpDataFieldCondition
            // 
            this.grpDataFieldCondition.Controls.Add(this.pnlDataField);
            this.grpDataFieldCondition.Controls.Add(this.pnlExtendDataField);
            this.grpDataFieldCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDataFieldCondition.Location = new System.Drawing.Point(0, 116);
            this.grpDataFieldCondition.LookAndFeel.SkinName = "Money Twins";
            this.grpDataFieldCondition.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grpDataFieldCondition.Name = "grpDataFieldCondition";
            this.grpDataFieldCondition.Size = new System.Drawing.Size(363, 174);
            this.grpDataFieldCondition.TabIndex = 28;
            this.grpDataFieldCondition.Text = "字段选择";
            // 
            // pnlDataField
            // 
            this.pnlDataField.Controls.Add(this.lblCellConditonTip);
            this.pnlDataField.Controls.Add(this.sbtnSetting);
            this.pnlDataField.Controls.Add(this.etxtCondition);
            this.pnlDataField.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDataField.Location = new System.Drawing.Point(2, 137);
            this.pnlDataField.Name = "pnlDataField";
            this.pnlDataField.Size = new System.Drawing.Size(359, 36);
            this.pnlDataField.TabIndex = 82;
            // 
            // lblCellConditonTip
            // 
            this.lblCellConditonTip.AutoSize = true;
            this.lblCellConditonTip.ForeColor = System.Drawing.Color.Red;
            this.lblCellConditonTip.Location = new System.Drawing.Point(345, 10);
            this.lblCellConditonTip.Name = "lblCellConditonTip";
            this.lblCellConditonTip.Size = new System.Drawing.Size(14, 14);
            this.lblCellConditonTip.TabIndex = 77;
            this.lblCellConditonTip.Text = "*";
            // 
            // sbtnSetting
            // 
            this.sbtnSetting.Location = new System.Drawing.Point(292, 5);
            this.sbtnSetting.LookAndFeel.SkinName = "Money Twins";
            this.sbtnSetting.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnSetting.Name = "sbtnSetting";
            this.sbtnSetting.Size = new System.Drawing.Size(50, 23);
            this.sbtnSetting.TabIndex = 79;
            this.sbtnSetting.Text = "设置...";
            this.sbtnSetting.Click += new System.EventHandler(this.sbtnSetting_Click);
            // 
            // etxtCondition
            // 
            this.etxtCondition.Location = new System.Drawing.Point(7, 7);
            this.etxtCondition.Name = "etxtCondition";
            this.etxtCondition.Properties.MaxLength = 256;
            this.etxtCondition.Properties.ReadOnly = true;
            this.etxtCondition.Size = new System.Drawing.Size(278, 20);
            this.etxtCondition.TabIndex = 80;
            // 
            // pnlExtendDataField
            // 
            this.pnlExtendDataField.Controls.Add(this.lstDataFieldCondition);
            this.pnlExtendDataField.Controls.Add(this.sbtnAddCondition);
            this.pnlExtendDataField.Controls.Add(this.etxtRowOrCol);
            this.pnlExtendDataField.Controls.Add(this.lblConditionTip);
            this.pnlExtendDataField.Controls.Add(this.sbtnRemoveCondition);
            this.pnlExtendDataField.Controls.Add(this.lblRowOrColTip);
            this.pnlExtendDataField.Controls.Add(this.lblRowOrCol);
            this.pnlExtendDataField.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlExtendDataField.Location = new System.Drawing.Point(2, 22);
            this.pnlExtendDataField.Name = "pnlExtendDataField";
            this.pnlExtendDataField.Size = new System.Drawing.Size(359, 115);
            this.pnlExtendDataField.TabIndex = 82;
            // 
            // lstDataFieldCondition
            // 
            this.lstDataFieldCondition.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstDataFieldCondition.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.lstDataFieldCondition.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.lstDataFieldCondition.Location = new System.Drawing.Point(8, 7);
            this.lstDataFieldCondition.LookAndFeel.SkinName = "Money Twins";
            this.lstDataFieldCondition.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lstDataFieldCondition.Name = "lstDataFieldCondition";
            this.lstDataFieldCondition.Size = new System.Drawing.Size(284, 75);
            this.lstDataFieldCondition.TabIndex = 83;
            // 
            // sbtnAddCondition
            // 
            this.sbtnAddCondition.Location = new System.Drawing.Point(297, 8);
            this.sbtnAddCondition.LookAndFeel.SkinName = "Money Twins";
            this.sbtnAddCondition.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnAddCondition.Name = "sbtnAddCondition";
            this.sbtnAddCondition.Size = new System.Drawing.Size(50, 23);
            this.sbtnAddCondition.TabIndex = 84;
            this.sbtnAddCondition.Text = "添加...";
            this.sbtnAddCondition.Click += new System.EventHandler(this.sbtnAddCondition_Click);
            // 
            // etxtRowOrCol
            // 
            this.etxtRowOrCol.Location = new System.Drawing.Point(89, 87);
            this.etxtRowOrCol.Name = "etxtRowOrCol";
            this.etxtRowOrCol.Properties.MaxLength = 256;
            this.etxtRowOrCol.Size = new System.Drawing.Size(64, 20);
            this.etxtRowOrCol.TabIndex = 87;
            this.etxtRowOrCol.TextChanged += new System.EventHandler(this.etxtRowOrCol_TextChanged);
            // 
            // lblConditionTip
            // 
            this.lblConditionTip.AutoSize = true;
            this.lblConditionTip.ForeColor = System.Drawing.Color.DarkRed;
            this.lblConditionTip.Location = new System.Drawing.Point(174, 91);
            this.lblConditionTip.Name = "lblConditionTip";
            this.lblConditionTip.Size = new System.Drawing.Size(168, 14);
            this.lblConditionTip.TabIndex = 88;
            this.lblConditionTip.Text = "提示：值的范围为(0, 2000)。";
            // 
            // sbtnRemoveCondition
            // 
            this.sbtnRemoveCondition.Location = new System.Drawing.Point(297, 37);
            this.sbtnRemoveCondition.LookAndFeel.SkinName = "Money Twins";
            this.sbtnRemoveCondition.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnRemoveCondition.Name = "sbtnRemoveCondition";
            this.sbtnRemoveCondition.Size = new System.Drawing.Size(50, 23);
            this.sbtnRemoveCondition.TabIndex = 85;
            this.sbtnRemoveCondition.Text = "移除...";
            this.sbtnRemoveCondition.Click += new System.EventHandler(this.sbtnRemoveCondition_Click);
            // 
            // lblRowOrColTip
            // 
            this.lblRowOrColTip.AutoSize = true;
            this.lblRowOrColTip.ForeColor = System.Drawing.Color.Red;
            this.lblRowOrColTip.Location = new System.Drawing.Point(158, 91);
            this.lblRowOrColTip.Name = "lblRowOrColTip";
            this.lblRowOrColTip.Size = new System.Drawing.Size(14, 14);
            this.lblRowOrColTip.TabIndex = 89;
            this.lblRowOrColTip.Text = "*";
            // 
            // lblRowOrCol
            // 
            this.lblRowOrCol.AutoSize = true;
            this.lblRowOrCol.Location = new System.Drawing.Point(10, 91);
            this.lblRowOrCol.Name = "lblRowOrCol";
            this.lblRowOrCol.Size = new System.Drawing.Size(79, 14);
            this.lblRowOrCol.TabIndex = 86;
            this.lblRowOrCol.Text = "可扩展行数：";
            // 
            // CellConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(363, 290);
            this.Controls.Add(this.grpDataFieldCondition);
            this.Controls.Add(this.grpCell);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "CellConditionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单元格数据源设置";
            this.Load += new System.EventHandler(this.CellConditionSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpCell)).EndInit();
            this.grpCell.ResumeLayout(false);
            this.grpCell.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbCellConditon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDataFieldCondition)).EndInit();
            this.grpDataFieldCondition.ResumeLayout(false);
            this.pnlDataField.ResumeLayout(false);
            this.pnlDataField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etxtCondition.Properties)).EndInit();
            this.pnlExtendDataField.ResumeLayout(false);
            this.pnlExtendDataField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataFieldCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtRowOrCol.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReportCellType;
        private DevExpress.XtraEditors.GroupControl grpCell;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnItmSave;
        private DevExpress.XtraBars.BarButtonItem btnItmClear;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnItmClose;
        private System.Windows.Forms.ImageList imglstEdit;
        private DevExpress.XtraEditors.GroupControl grpDataFieldCondition;
        private System.Windows.Forms.Label lblReportCellTypeTip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Panel pnlExtendDataField;
        private System.Windows.Forms.Panel pnlDataField;
        private System.Windows.Forms.Label lblCellConditonTip;
        protected DevExpress.XtraEditors.SimpleButton sbtnSetting;
        private DevExpress.XtraEditors.TextEdit etxtCondition;
        private DevExpress.XtraEditors.ListBoxControl lstDataFieldCondition;
        protected DevExpress.XtraEditors.SimpleButton sbtnAddCondition;
        protected DevExpress.XtraEditors.SimpleButton sbtnRemoveCondition;
        private System.Windows.Forms.Label lblRowOrCol;
        private System.Windows.Forms.Label lblRowOrColTip;
        private DevExpress.XtraEditors.TextEdit etxtRowOrCol;
        private System.Windows.Forms.Label lblConditionTip;
        private DataTableDropdownList dataTableDropdownList;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbCellConditon;
    }
}