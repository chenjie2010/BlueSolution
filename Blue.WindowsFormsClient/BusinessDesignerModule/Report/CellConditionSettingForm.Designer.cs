namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class CellConditionSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellConditionSettingForm));
            this.lblTable = new System.Windows.Forms.Label();
            this.lblCellConditon = new System.Windows.Forms.Label();
            this.grpDataFieldCondition = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.meCondition = new DevExpress.XtraEditors.MemoEdit();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnItmSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClear = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmVerify = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClose = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imglstEdit = new System.Windows.Forms.ImageList(this.components);
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.sbtnSetting = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnRemoveCondition = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnAddCondition = new DevExpress.XtraEditors.SimpleButton();
            this.lstDataFieldCondition = new DevExpress.XtraEditors.ListBoxControl();
            this.grpCellTemplate = new DevExpress.XtraEditors.GroupControl();
            this.meTemplate = new DevExpress.XtraEditors.MemoEdit();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.grpCell = new DevExpress.XtraEditors.GroupControl();
            this.btxtDataTable = new DevExpress.XtraEditors.ButtonEdit();
            this.ccmbDataFieldShowProperty = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblDataFieldShowProperty = new DevExpress.XtraEditors.LabelControl();
            this.icmbCellConditon = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.grpDataFieldShow = new DevExpress.XtraEditors.GroupControl();
            this.etxtRowOrCol = new DevExpress.XtraEditors.TextEdit();
            this.lblConditionTip = new System.Windows.Forms.Label();
            this.lblRowOrColTip = new System.Windows.Forms.Label();
            this.lblRowOrCol = new System.Windows.Forms.Label();
            this.sbtnRemoveDataFied = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnShowDataFied = new DevExpress.XtraEditors.SimpleButton();
            this.lstDataFieldShow = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.grpDataFieldCondition)).BeginInit();
            this.grpDataFieldCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataFieldCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCellTemplate)).BeginInit();
            this.grpCellTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meTemplate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCell)).BeginInit();
            this.grpCell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btxtDataTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldShowProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbCellConditon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDataFieldShow)).BeginInit();
            this.grpDataFieldShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etxtRowOrCol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataFieldShow)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(20, 31);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(91, 14);
            this.lblTable.TabIndex = 0;
            this.lblTable.Text = "单元格数据表：";
            // 
            // lblCellConditon
            // 
            this.lblCellConditon.AutoSize = true;
            this.lblCellConditon.Location = new System.Drawing.Point(8, 58);
            this.lblCellConditon.Name = "lblCellConditon";
            this.lblCellConditon.Size = new System.Drawing.Size(103, 14);
            this.lblCellConditon.TabIndex = 4;
            this.lblCellConditon.Text = "单元格条件类型：";
            // 
            // grpDataFieldCondition
            // 
            this.grpDataFieldCondition.Controls.Add(this.groupControl1);
            this.grpDataFieldCondition.Controls.Add(this.sbtnSetting);
            this.grpDataFieldCondition.Controls.Add(this.sbtnRemoveCondition);
            this.grpDataFieldCondition.Controls.Add(this.sbtnAddCondition);
            this.grpDataFieldCondition.Controls.Add(this.lstDataFieldCondition);
            this.grpDataFieldCondition.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpDataFieldCondition.Location = new System.Drawing.Point(324, 116);
            this.grpDataFieldCondition.LookAndFeel.SkinName = "Money Twins";
            this.grpDataFieldCondition.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grpDataFieldCondition.Name = "grpDataFieldCondition";
            this.grpDataFieldCondition.Size = new System.Drawing.Size(334, 354);
            this.grpDataFieldCondition.TabIndex = 6;
            this.grpDataFieldCondition.Text = "条件字段设置";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.meCondition);
            this.groupControl1.Controls.Add(this.richTextBox2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(2, 152);
            this.groupControl1.LookAndFeel.SkinName = "Money Twins";
            this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Padding = new System.Windows.Forms.Padding(1);
            this.groupControl1.Size = new System.Drawing.Size(330, 200);
            this.groupControl1.TabIndex = 29;
            this.groupControl1.Text = "条件";
            // 
            // meCondition
            // 
            this.meCondition.Location = new System.Drawing.Point(7, 79);
            this.meCondition.MenuManager = this.barManager;
            this.meCondition.Name = "meCondition";
            this.meCondition.Size = new System.Drawing.Size(316, 116);
            this.meCondition.TabIndex = 13;
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
            this.btnItmVerify,
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
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.btnItmVerify, "验证条件", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
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
            // btnItmVerify
            // 
            this.btnItmVerify.Caption = "关闭";
            this.btnItmVerify.Id = 2;
            this.btnItmVerify.ImageIndex = 2;
            this.btnItmVerify.Name = "btnItmVerify";
            this.btnItmVerify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmVerify_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(658, 32);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 470);
            this.barDockControlBottom.Size = new System.Drawing.Size(658, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 32);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 438);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(658, 32);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 438);
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
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.richTextBox2.Location = new System.Drawing.Point(3, 23);
            this.richTextBox2.MaxLength = 4000;
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(324, 58);
            this.richTextBox2.TabIndex = 220;
            this.richTextBox2.Text = "条件提示：\n(1)对多个字段设置条件时，请自己添加多个条件之间的关系，包括AND,OR, 括号等。";
            // 
            // sbtnSetting
            // 
            this.sbtnSetting.Location = new System.Drawing.Point(221, 86);
            this.sbtnSetting.LookAndFeel.SkinName = "Money Twins";
            this.sbtnSetting.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnSetting.Name = "sbtnSetting";
            this.sbtnSetting.Size = new System.Drawing.Size(86, 23);
            this.sbtnSetting.TabIndex = 12;
            this.sbtnSetting.Text = "设置条件...";
            this.sbtnSetting.Click += new System.EventHandler(this.sbtnSetting_Click);
            // 
            // sbtnRemoveCondition
            // 
            this.sbtnRemoveCondition.Location = new System.Drawing.Point(221, 57);
            this.sbtnRemoveCondition.LookAndFeel.SkinName = "Money Twins";
            this.sbtnRemoveCondition.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnRemoveCondition.Name = "sbtnRemoveCondition";
            this.sbtnRemoveCondition.Size = new System.Drawing.Size(50, 23);
            this.sbtnRemoveCondition.TabIndex = 10;
            this.sbtnRemoveCondition.Text = "移除...";
            this.sbtnRemoveCondition.Click += new System.EventHandler(this.sbtnRemoveCondition_Click);
            // 
            // sbtnAddCondition
            // 
            this.sbtnAddCondition.Location = new System.Drawing.Point(221, 28);
            this.sbtnAddCondition.LookAndFeel.SkinName = "Money Twins";
            this.sbtnAddCondition.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnAddCondition.Name = "sbtnAddCondition";
            this.sbtnAddCondition.Size = new System.Drawing.Size(50, 23);
            this.sbtnAddCondition.TabIndex = 9;
            this.sbtnAddCondition.Text = "添加...";
            this.sbtnAddCondition.Click += new System.EventHandler(this.sbtnAddCondition_Click);
            // 
            // lstDataFieldCondition
            // 
            this.lstDataFieldCondition.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstDataFieldCondition.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.lstDataFieldCondition.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.lstDataFieldCondition.Location = new System.Drawing.Point(5, 26);
            this.lstDataFieldCondition.LookAndFeel.SkinName = "Money Twins";
            this.lstDataFieldCondition.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lstDataFieldCondition.Name = "lstDataFieldCondition";
            this.lstDataFieldCondition.Size = new System.Drawing.Size(210, 119);
            this.lstDataFieldCondition.TabIndex = 8;
            // 
            // grpCellTemplate
            // 
            this.grpCellTemplate.Controls.Add(this.meTemplate);
            this.grpCellTemplate.Controls.Add(this.richTextBox1);
            this.grpCellTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCellTemplate.Location = new System.Drawing.Point(0, 267);
            this.grpCellTemplate.LookAndFeel.SkinName = "Money Twins";
            this.grpCellTemplate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grpCellTemplate.Name = "grpCellTemplate";
            this.grpCellTemplate.Padding = new System.Windows.Forms.Padding(1);
            this.grpCellTemplate.Size = new System.Drawing.Size(324, 203);
            this.grpCellTemplate.TabIndex = 1;
            this.grpCellTemplate.Text = "单元格模板设置";
            // 
            // meTemplate
            // 
            this.meTemplate.Location = new System.Drawing.Point(6, 125);
            this.meTemplate.MenuManager = this.barManager;
            this.meTemplate.Name = "meTemplate";
            this.meTemplate.Size = new System.Drawing.Size(310, 71);
            this.meTemplate.TabIndex = 7;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.richTextBox1.Location = new System.Drawing.Point(3, 23);
            this.richTextBox1.MaxLength = 4000;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(318, 103);
            this.richTextBox1.TabIndex = 220;
            this.richTextBox1.Text = "设置提示：\n(1) {0}表示显示第 0 个显示字段，依次类推。\n(2)统计表类型的数据单元格可以设置函数或其组合的四则运算，例如：count({0}), sum" +
    "({0}), avg({0}), distinct({0}),max({0}),min({0})\n(3)统计表类型的数据单元格默认为计数，即count({0})" +
    "";
            // 
            // grpCell
            // 
            this.grpCell.Controls.Add(this.btxtDataTable);
            this.grpCell.Controls.Add(this.ccmbDataFieldShowProperty);
            this.grpCell.Controls.Add(this.lblDataFieldShowProperty);
            this.grpCell.Controls.Add(this.icmbCellConditon);
            this.grpCell.Controls.Add(this.lblTable);
            this.grpCell.Controls.Add(this.lblCellConditon);
            this.grpCell.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpCell.Location = new System.Drawing.Point(0, 32);
            this.grpCell.LookAndFeel.SkinName = "Money Twins";
            this.grpCell.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grpCell.Name = "grpCell";
            this.grpCell.Size = new System.Drawing.Size(658, 84);
            this.grpCell.TabIndex = 22;
            this.grpCell.Text = "单元格基本信息";
            // 
            // btxtDataTable
            // 
            this.btxtDataTable.Location = new System.Drawing.Point(111, 28);
            this.btxtDataTable.MenuManager = this.barManager;
            this.btxtDataTable.Name = "btxtDataTable";
            this.btxtDataTable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtDataTable.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtDataTable.Size = new System.Drawing.Size(535, 20);
            this.btxtDataTable.TabIndex = 0;
            this.btxtDataTable.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtDataTable_ButtonPressed);
            // 
            // ccmbDataFieldShowProperty
            // 
            this.ccmbDataFieldShowProperty.EditValue = "";
            this.ccmbDataFieldShowProperty.Location = new System.Drawing.Point(393, 55);
            this.ccmbDataFieldShowProperty.Name = "ccmbDataFieldShowProperty";
            this.ccmbDataFieldShowProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbDataFieldShowProperty.Properties.PopupSizeable = false;
            this.ccmbDataFieldShowProperty.Properties.SelectAllItemVisible = false;
            this.ccmbDataFieldShowProperty.Size = new System.Drawing.Size(254, 20);
            this.ccmbDataFieldShowProperty.TabIndex = 2;
            // 
            // lblDataFieldShowProperty
            // 
            this.lblDataFieldShowProperty.Location = new System.Drawing.Point(331, 58);
            this.lblDataFieldShowProperty.Name = "lblDataFieldShowProperty";
            this.lblDataFieldShowProperty.Size = new System.Drawing.Size(60, 14);
            this.lblDataFieldShowProperty.TabIndex = 95;
            this.lblDataFieldShowProperty.Text = "显示属性：";
            // 
            // icmbCellConditon
            // 
            this.icmbCellConditon.Location = new System.Drawing.Point(110, 55);
            this.icmbCellConditon.MenuManager = this.barManager;
            this.icmbCellConditon.Name = "icmbCellConditon";
            this.icmbCellConditon.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbCellConditon.Size = new System.Drawing.Size(206, 20);
            this.icmbCellConditon.TabIndex = 1;
            this.icmbCellConditon.SelectedIndexChanged += new System.EventHandler(this.icmbCellConditon_SelectedIndexChanged);
            // 
            // grpDataFieldShow
            // 
            this.grpDataFieldShow.Controls.Add(this.etxtRowOrCol);
            this.grpDataFieldShow.Controls.Add(this.lblConditionTip);
            this.grpDataFieldShow.Controls.Add(this.lblRowOrColTip);
            this.grpDataFieldShow.Controls.Add(this.lblRowOrCol);
            this.grpDataFieldShow.Controls.Add(this.sbtnRemoveDataFied);
            this.grpDataFieldShow.Controls.Add(this.sbtnShowDataFied);
            this.grpDataFieldShow.Controls.Add(this.lstDataFieldShow);
            this.grpDataFieldShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDataFieldShow.Location = new System.Drawing.Point(0, 116);
            this.grpDataFieldShow.LookAndFeel.SkinName = "Money Twins";
            this.grpDataFieldShow.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grpDataFieldShow.Name = "grpDataFieldShow";
            this.grpDataFieldShow.Size = new System.Drawing.Size(324, 151);
            this.grpDataFieldShow.TabIndex = 23;
            this.grpDataFieldShow.Text = "显示字段设置";
            // 
            // etxtRowOrCol
            // 
            this.etxtRowOrCol.Location = new System.Drawing.Point(81, 126);
            this.etxtRowOrCol.Name = "etxtRowOrCol";
            this.etxtRowOrCol.Properties.MaxLength = 256;
            this.etxtRowOrCol.Size = new System.Drawing.Size(64, 20);
            this.etxtRowOrCol.TabIndex = 6;
            // 
            // lblConditionTip
            // 
            this.lblConditionTip.AutoSize = true;
            this.lblConditionTip.ForeColor = System.Drawing.Color.DarkRed;
            this.lblConditionTip.Location = new System.Drawing.Point(160, 130);
            this.lblConditionTip.Name = "lblConditionTip";
            this.lblConditionTip.Size = new System.Drawing.Size(168, 14);
            this.lblConditionTip.TabIndex = 92;
            this.lblConditionTip.Text = "提示：值的范围为(0, 2000)。";
            // 
            // lblRowOrColTip
            // 
            this.lblRowOrColTip.AutoSize = true;
            this.lblRowOrColTip.ForeColor = System.Drawing.Color.Red;
            this.lblRowOrColTip.Location = new System.Drawing.Point(148, 130);
            this.lblRowOrColTip.Name = "lblRowOrColTip";
            this.lblRowOrColTip.Size = new System.Drawing.Size(14, 14);
            this.lblRowOrColTip.TabIndex = 93;
            this.lblRowOrColTip.Text = "*";
            // 
            // lblRowOrCol
            // 
            this.lblRowOrCol.AutoSize = true;
            this.lblRowOrCol.Location = new System.Drawing.Point(5, 130);
            this.lblRowOrCol.Name = "lblRowOrCol";
            this.lblRowOrCol.Size = new System.Drawing.Size(79, 14);
            this.lblRowOrCol.TabIndex = 90;
            this.lblRowOrCol.Text = "可扩展行数：";
            // 
            // sbtnRemoveDataFied
            // 
            this.sbtnRemoveDataFied.Location = new System.Drawing.Point(223, 55);
            this.sbtnRemoveDataFied.LookAndFeel.SkinName = "Money Twins";
            this.sbtnRemoveDataFied.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnRemoveDataFied.Name = "sbtnRemoveDataFied";
            this.sbtnRemoveDataFied.Size = new System.Drawing.Size(50, 23);
            this.sbtnRemoveDataFied.TabIndex = 5;
            this.sbtnRemoveDataFied.Text = "移除...";
            this.sbtnRemoveDataFied.Click += new System.EventHandler(this.sbtnRemoveDataFied_Click);
            // 
            // sbtnShowDataFied
            // 
            this.sbtnShowDataFied.Location = new System.Drawing.Point(223, 26);
            this.sbtnShowDataFied.LookAndFeel.SkinName = "Money Twins";
            this.sbtnShowDataFied.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnShowDataFied.Name = "sbtnShowDataFied";
            this.sbtnShowDataFied.Size = new System.Drawing.Size(50, 23);
            this.sbtnShowDataFied.TabIndex = 4;
            this.sbtnShowDataFied.Text = "添加...";
            this.sbtnShowDataFied.Click += new System.EventHandler(this.sbtnShowDataFied_Click);
            // 
            // lstDataFieldShow
            // 
            this.lstDataFieldShow.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstDataFieldShow.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.lstDataFieldShow.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.lstDataFieldShow.Location = new System.Drawing.Point(7, 26);
            this.lstDataFieldShow.LookAndFeel.SkinName = "Money Twins";
            this.lstDataFieldShow.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lstDataFieldShow.Name = "lstDataFieldShow";
            this.lstDataFieldShow.Size = new System.Drawing.Size(210, 94);
            this.lstDataFieldShow.TabIndex = 3;
            // 
            // CellConditionSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(658, 470);
            this.Controls.Add(this.grpCellTemplate);
            this.Controls.Add(this.grpDataFieldShow);
            this.Controls.Add(this.grpDataFieldCondition);
            this.Controls.Add(this.grpCell);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "CellConditionSettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单元格数据源设置";
            this.Load += new System.EventHandler(this.CellConditionSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpDataFieldCondition)).EndInit();
            this.grpDataFieldCondition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.meCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataFieldCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCellTemplate)).EndInit();
            this.grpCellTemplate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.meTemplate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCell)).EndInit();
            this.grpCell.ResumeLayout(false);
            this.grpCell.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btxtDataTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldShowProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbCellConditon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDataFieldShow)).EndInit();
            this.grpDataFieldShow.ResumeLayout(false);
            this.grpDataFieldShow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etxtRowOrCol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataFieldShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblCellConditon;
        private DevExpress.XtraEditors.GroupControl grpDataFieldCondition;
        private DevExpress.XtraEditors.GroupControl grpCellTemplate;
        protected DevExpress.XtraEditors.SimpleButton sbtnSetting;
        protected DevExpress.XtraEditors.SimpleButton sbtnRemoveCondition;
        protected DevExpress.XtraEditors.SimpleButton sbtnAddCondition;
        private DevExpress.XtraEditors.GroupControl grpCell;
        private DevExpress.XtraEditors.GroupControl grpDataFieldShow;
        protected DevExpress.XtraEditors.SimpleButton sbtnRemoveDataFied;
        protected DevExpress.XtraEditors.SimpleButton sbtnShowDataFied;
        private DevExpress.XtraEditors.ListBoxControl lstDataFieldShow;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnItmSave;
        private DevExpress.XtraBars.BarButtonItem btnItmClear;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnItmVerify;
        private DevExpress.XtraBars.BarButtonItem btnItmClose;
        private System.Windows.Forms.ImageList imglstEdit;
        private DevExpress.XtraEditors.ListBoxControl lstDataFieldCondition;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private DevExpress.XtraEditors.TextEdit etxtRowOrCol;
        private System.Windows.Forms.Label lblConditionTip;
        private System.Windows.Forms.Label lblRowOrColTip;
        private System.Windows.Forms.Label lblRowOrCol;
        private DevExpress.XtraEditors.MemoEdit meCondition;
        private DevExpress.XtraEditors.MemoEdit meTemplate;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbCellConditon;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbDataFieldShowProperty;
        private DevExpress.XtraEditors.LabelControl lblDataFieldShowProperty;
        private DevExpress.XtraEditors.ButtonEdit btxtDataTable;
    }
}