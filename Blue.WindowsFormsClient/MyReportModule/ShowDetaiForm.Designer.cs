namespace Blue.WindowsFormsClient.MyReportModule
{
    partial class ShowDetaiForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowDetaiForm));
            this.gcDetai = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColUserActualName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColUserIdentity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribtnCondition = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.ricmbCombine = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.btnItmClearAll = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClearSystem = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClearDataField = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClose = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiQuery = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmConfirm = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmCloseAll = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.btnItmAll = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmReverse = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClear = new DevExpress.XtraBars.BarButtonItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.gcDataField = new DevExpress.XtraEditors.GroupControl();
            this.lnkClear = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.icButtons = new DevExpress.Utils.ImageCollection(this.components);
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.lstDataFields = new DevExpress.XtraEditors.ListBoxControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.icmbDataWarehouse = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icDataWarehouse = new DevExpress.Utils.ImageCollection(this.components);
            this.lblDataWarehouse = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomDataFields = new DevExpress.XtraEditors.LabelControl();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribtnCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbCombine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataField)).BeginInit();
            this.gcDataField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDetai
            // 
            this.gcDetai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDetai.Location = new System.Drawing.Point(0, 32);
            this.gcDetai.LookAndFeel.SkinName = "Money Twins";
            this.gcDetai.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcDetai.MainView = this.gridView;
            this.gcDetai.Name = "gcDetai";
            this.gcDetai.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ribtnCondition,
            this.ricmbCombine});
            this.gcDetai.Size = new System.Drawing.Size(491, 441);
            this.gcDetai.TabIndex = 15;
            this.gcDetai.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.ActiveFilterEnabled = false;
            this.gridView.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridView.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridView.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gridView.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gridView.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gridView.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gridView.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(190)))), ((int)(((byte)(243)))));
            this.gridView.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gridView.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridView.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gridView.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gridView.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gridView.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gridView.Appearance.Empty.Options.UseBackColor = true;
            this.gridView.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.gridView.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView.Appearance.EvenRow.Options.UseForeColor = true;
            this.gridView.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridView.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridView.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gridView.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gridView.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gridView.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gridView.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            this.gridView.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gridView.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gridView.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.gridView.Appearance.FixedLine.Options.UseBackColor = true;
            this.gridView.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gridView.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridView.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gridView.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.gridView.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridView.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridView.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridView.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gridView.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gridView.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridView.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.GroupButton.Options.UseBackColor = true;
            this.gridView.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gridView.Appearance.GroupButton.Options.UseForeColor = true;
            this.gridView.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gridView.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gridView.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gridView.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gridView.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gridView.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gridView.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridView.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gridView.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridView.Appearance.GroupRow.Options.UseBorderColor = true;
            this.gridView.Appearance.GroupRow.Options.UseFont = true;
            this.gridView.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridView.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridView.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridView.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridView.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gridView.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(228)))));
            this.gridView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(224)))), ((int)(((byte)(251)))));
            this.gridView.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridView.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridView.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gridView.Appearance.HorzLine.Options.UseBackColor = true;
            this.gridView.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gridView.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.OddRow.Options.UseBackColor = true;
            this.gridView.Appearance.OddRow.Options.UseForeColor = true;
            this.gridView.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gridView.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(185)))));
            this.gridView.Appearance.Preview.Options.UseBackColor = true;
            this.gridView.Appearance.Preview.Options.UseForeColor = true;
            this.gridView.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridView.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.gridView.Appearance.Row.Options.UseBackColor = true;
            this.gridView.Appearance.Row.Options.UseForeColor = true;
            this.gridView.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            this.gridView.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gridView.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(126)))), ((int)(((byte)(217)))));
            this.gridView.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gridView.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridView.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gridView.Appearance.VertLine.Options.UseBackColor = true;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColName,
            this.gridColUserActualName,
            this.gridColDepartmentName,
            this.gridColUserIdentity});
            this.gridView.GridControl = this.gcDetai;
            this.gridView.IndicatorWidth = 60;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsCustomization.AllowColumnMoving = false;
            this.gridView.OptionsCustomization.AllowFilter = false;
            this.gridView.OptionsCustomization.AllowGroup = false;
            this.gridView.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView.OptionsFilter.AllowFilterEditor = false;
            this.gridView.OptionsFind.AllowFindPanel = false;
            this.gridView.OptionsMenu.EnableColumnMenu = false;
            this.gridView.OptionsMenu.EnableFooterMenu = false;
            this.gridView.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridView.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gridView.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView.OptionsView.EnableAppearanceOddRow = true;
            this.gridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            // 
            // gridColName
            // 
            this.gridColName.Caption = "用户名";
            this.gridColName.FieldName = "UserName";
            this.gridColName.Name = "gridColName";
            this.gridColName.OptionsColumn.AllowEdit = false;
            this.gridColName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColName.OptionsColumn.ReadOnly = true;
            this.gridColName.OptionsFilter.AllowAutoFilter = false;
            this.gridColName.OptionsFilter.AllowFilter = false;
            this.gridColName.Visible = true;
            this.gridColName.VisibleIndex = 0;
            this.gridColName.Width = 93;
            // 
            // gridColUserActualName
            // 
            this.gridColUserActualName.Caption = "姓名";
            this.gridColUserActualName.FieldName = "UserActualName";
            this.gridColUserActualName.Name = "gridColUserActualName";
            this.gridColUserActualName.OptionsColumn.AllowEdit = false;
            this.gridColUserActualName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColUserActualName.OptionsColumn.ReadOnly = true;
            this.gridColUserActualName.OptionsFilter.AllowAutoFilter = false;
            this.gridColUserActualName.OptionsFilter.AllowFilter = false;
            this.gridColUserActualName.Visible = true;
            this.gridColUserActualName.VisibleIndex = 1;
            this.gridColUserActualName.Width = 93;
            // 
            // gridColDepartmentName
            // 
            this.gridColDepartmentName.Caption = "单位名称";
            this.gridColDepartmentName.FieldName = "DepName";
            this.gridColDepartmentName.Name = "gridColDepartmentName";
            this.gridColDepartmentName.OptionsColumn.AllowEdit = false;
            this.gridColDepartmentName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColDepartmentName.OptionsColumn.ReadOnly = true;
            this.gridColDepartmentName.OptionsFilter.AllowAutoFilter = false;
            this.gridColDepartmentName.OptionsFilter.AllowFilter = false;
            this.gridColDepartmentName.Visible = true;
            this.gridColDepartmentName.VisibleIndex = 2;
            this.gridColDepartmentName.Width = 140;
            // 
            // gridColUserIdentity
            // 
            this.gridColUserIdentity.Caption = "用户类型";
            this.gridColUserIdentity.FieldName = "UserTypeName";
            this.gridColUserIdentity.Name = "gridColUserIdentity";
            this.gridColUserIdentity.OptionsColumn.AllowEdit = false;
            this.gridColUserIdentity.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColUserIdentity.OptionsColumn.ReadOnly = true;
            this.gridColUserIdentity.OptionsFilter.AllowAutoFilter = false;
            this.gridColUserIdentity.OptionsFilter.AllowFilter = false;
            this.gridColUserIdentity.Visible = true;
            this.gridColUserIdentity.VisibleIndex = 3;
            this.gridColUserIdentity.Width = 151;
            // 
            // ribtnCondition
            // 
            this.ribtnCondition.AutoHeight = false;
            this.ribtnCondition.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ribtnCondition.Name = "ribtnCondition";
            // 
            // ricmbCombine
            // 
            this.ricmbCombine.AutoHeight = false;
            this.ricmbCombine.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricmbCombine.Name = "ricmbCombine";
            // 
            // btnItmClearAll
            // 
            this.btnItmClearAll.Caption = "清除所有条件(&A)";
            this.btnItmClearAll.Id = 9;
            this.btnItmClearAll.ImageIndex = 4;
            this.btnItmClearAll.Name = "btnItmClearAll";
            // 
            // btnItmClearSystem
            // 
            this.btnItmClearSystem.Caption = "清除系统条件(&S)";
            this.btnItmClearSystem.Id = 11;
            this.btnItmClearSystem.ImageIndex = 3;
            this.btnItmClearSystem.Name = "btnItmClearSystem";
            // 
            // btnItmClearDataField
            // 
            this.btnItmClearDataField.Caption = "清除字段条件(&D)";
            this.btnItmClearDataField.Id = 10;
            this.btnItmClearDataField.ImageIndex = 2;
            this.btnItmClearDataField.Name = "btnItmClearDataField";
            // 
            // btnItmClose
            // 
            this.btnItmClose.Caption = "关闭(&C)";
            this.btnItmClose.Id = 3;
            this.btnItmClose.ImageIndex = 1;
            this.btnItmClose.Name = "btnItmClose";
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmClearDataField, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmClearSystem, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmClearAll, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barAndDockingController
            // 
            this.barAndDockingController.LookAndFeel.SkinName = "Blue";
            this.barAndDockingController.LookAndFeel.UseDefaultLookAndFeel = false;
            this.barAndDockingController.PropertiesBar.AllowLinkLighting = false;
            this.barAndDockingController.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.barAndDockingController.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.AllowShowToolbarsPopup = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.icTools;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItmCloseAll,
            this.btnItmConfirm,
            this.btnItmAll,
            this.btnItmReverse,
            this.btnItmClear,
            this.bbiQuery});
            this.barManager.MaxItemId = 13;
            // 
            // bar2
            // 
            this.bar2.BarItemHorzIndent = 5;
            this.bar2.BarItemVertIndent = 5;
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiQuery, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmConfirm, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmCloseAll, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Tools";
            // 
            // bbiQuery
            // 
            this.bbiQuery.Caption = "查询(&Q)";
            this.bbiQuery.Id = 12;
            this.bbiQuery.ImageIndex = 0;
            this.bbiQuery.Name = "bbiQuery";
            this.bbiQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiQuery_ItemClick);
            // 
            // btnItmConfirm
            // 
            this.btnItmConfirm.Caption = "导出EXCEL(&O)";
            this.btnItmConfirm.Id = 5;
            this.btnItmConfirm.ImageIndex = 1;
            this.btnItmConfirm.Name = "btnItmConfirm";
            this.btnItmConfirm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmConfirm_ItemClick);
            // 
            // btnItmCloseAll
            // 
            this.btnItmCloseAll.Caption = "关闭(&C)";
            this.btnItmCloseAll.Id = 3;
            this.btnItmCloseAll.ImageIndex = 2;
            this.btnItmCloseAll.Name = "btnItmCloseAll";
            this.btnItmCloseAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmCloseAll_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(913, 32);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 473);
            this.barDockControlBottom.Size = new System.Drawing.Size(913, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 32);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 441);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(913, 32);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 441);
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "Enum_DataAuthorityType_Query.png");
            this.icTools.Images.SetKeyName(1, "Clinet_Common_Export.png");
            this.icTools.Images.SetKeyName(2, "Common_Close.png");
            // 
            // btnItmAll
            // 
            this.btnItmAll.Caption = "全选(&A)";
            this.btnItmAll.Id = 6;
            this.btnItmAll.Name = "btnItmAll";
            // 
            // btnItmReverse
            // 
            this.btnItmReverse.Caption = "反选(&R)";
            this.btnItmReverse.Id = 7;
            this.btnItmReverse.Name = "btnItmReverse";
            // 
            // btnItmClear
            // 
            this.btnItmClear.Caption = "清除(&C)";
            this.btnItmClear.Id = 8;
            this.btnItmClear.Name = "btnItmClear";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // gcDataField
            // 
            this.gcDataField.Controls.Add(this.lblTip);
            this.gcDataField.Controls.Add(this.lnkClear);
            this.gcDataField.Controls.Add(this.btnRemove);
            this.gcDataField.Controls.Add(this.lstDataFields);
            this.gcDataField.Controls.Add(this.btnAdd);
            this.gcDataField.Controls.Add(this.icmbDataWarehouse);
            this.gcDataField.Controls.Add(this.lblDataWarehouse);
            this.gcDataField.Controls.Add(this.lblCustomDataFields);
            this.gcDataField.Dock = System.Windows.Forms.DockStyle.Right;
            this.gcDataField.Location = new System.Drawing.Point(491, 32);
            this.gcDataField.Name = "gcDataField";
            this.gcDataField.Size = new System.Drawing.Size(422, 441);
            this.gcDataField.TabIndex = 20;
            this.gcDataField.Text = "导出自定义字段";
            // 
            // lnkClear
            // 
            this.lnkClear.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkClear.Appearance.ImageIndex = 2;
            this.lnkClear.Appearance.ImageList = this.icButtons;
            this.lnkClear.Appearance.Options.UseImageAlign = true;
            this.lnkClear.Appearance.Options.UseImageIndex = true;
            this.lnkClear.Appearance.Options.UseImageList = true;
            this.lnkClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkClear.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lnkClear.Location = new System.Drawing.Point(340, 406);
            this.lnkClear.Name = "lnkClear";
            this.lnkClear.Size = new System.Drawing.Size(62, 20);
            this.lnkClear.TabIndex = 4;
            this.lnkClear.Text = "清除(&C)";
            this.lnkClear.Click += new System.EventHandler(this.lnkClear_Click);
            // 
            // icButtons
            // 
            this.icButtons.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icButtons.ImageStream")));
            this.icButtons.Images.SetKeyName(0, "Button_Add_New.png");
            this.icButtons.Images.SetKeyName(1, "Mail_Remove_Attachment.png");
            this.icButtons.Images.SetKeyName(2, "Button_Remove_Small.png");
            // 
            // btnRemove
            // 
            this.btnRemove.ImageIndex = 1;
            this.btnRemove.ImageList = this.icButtons;
            this.btnRemove.Location = new System.Drawing.Point(245, 404);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "移除(&R)";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lstDataFields
            // 
            this.lstDataFields.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstDataFields.Location = new System.Drawing.Point(81, 70);
            this.lstDataFields.Name = "lstDataFields";
            this.lstDataFields.Size = new System.Drawing.Size(336, 299);
            this.lstDataFields.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.ImageList = this.icButtons;
            this.btnAdd.Location = new System.Drawing.Point(158, 404);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "增加(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // icmbDataWarehouse
            // 
            this.icmbDataWarehouse.Location = new System.Drawing.Point(79, 36);
            this.icmbDataWarehouse.Name = "icmbDataWarehouse";
            this.icmbDataWarehouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDataWarehouse.Properties.SmallImages = this.icDataWarehouse;
            this.icmbDataWarehouse.Size = new System.Drawing.Size(338, 20);
            this.icmbDataWarehouse.TabIndex = 0;
            // 
            // icDataWarehouse
            // 
            this.icDataWarehouse.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataWarehouse.ImageStream")));
            this.icDataWarehouse.Images.SetKeyName(0, "Common_Digit_One.png");
            this.icDataWarehouse.Images.SetKeyName(1, "Common_Digit_Two.png");
            this.icDataWarehouse.Images.SetKeyName(2, "Common_Digit_Three.png");
            this.icDataWarehouse.Images.SetKeyName(3, "Common_Digit_Four.png");
            this.icDataWarehouse.Images.SetKeyName(4, "Common_Digit_Five.png");
            // 
            // lblDataWarehouse
            // 
            this.lblDataWarehouse.Location = new System.Drawing.Point(16, 37);
            this.lblDataWarehouse.Name = "lblDataWarehouse";
            this.lblDataWarehouse.Size = new System.Drawing.Size(60, 14);
            this.lblDataWarehouse.TabIndex = 78;
            this.lblDataWarehouse.Text = "数据仓库：";
            // 
            // lblCustomDataFields
            // 
            this.lblCustomDataFields.Location = new System.Drawing.Point(8, 72);
            this.lblCustomDataFields.Name = "lblCustomDataFields";
            this.lblCustomDataFields.Size = new System.Drawing.Size(68, 14);
            this.lblCustomDataFields.TabIndex = 0;
            this.lblCustomDataFields.Text = "自定义字段: ";
            // 
            // lblTip
            // 
            this.lblTip.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Information_16;
            this.lblTip.Appearance.Options.UseImage = true;
            this.lblTip.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblTip.Location = new System.Drawing.Point(81, 375);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(249, 20);
            this.lblTip.TabIndex = 79;
            this.lblTip.Text = "提示：含自定义字段时，请先查询再导出。";
            // 
            // ShowDetaiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 473);
            this.Controls.Add(this.gcDetai);
            this.Controls.Add(this.gcDataField);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ShowDetaiForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据详情";
            this.Load += new System.EventHandler(this.ShowDetaiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDetai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribtnCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbCombine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataField)).EndInit();
            this.gcDataField.ResumeLayout(false);
            this.gcDataField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDetai;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColUserActualName;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ribtnCondition;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ricmbCombine;
        private DevExpress.XtraBars.BarButtonItem btnItmClearAll;
        private DevExpress.XtraBars.BarButtonItem btnItmClearSystem;
        private DevExpress.XtraBars.BarButtonItem btnItmClearDataField;
        private DevExpress.XtraBars.BarButtonItem btnItmClose;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem btnItmConfirm;
        private DevExpress.XtraBars.BarButtonItem btnItmCloseAll;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnItmAll;
        private DevExpress.XtraBars.BarButtonItem btnItmReverse;
        private DevExpress.XtraBars.BarButtonItem btnItmClear;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private DevExpress.XtraGrid.Columns.GridColumn gridColUserIdentity;
        private DevExpress.XtraGrid.Columns.GridColumn gridColDepartmentName;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.XtraEditors.GroupControl gcDataField;
        private DevExpress.XtraEditors.LabelControl lblCustomDataFields;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDataWarehouse;
        private DevExpress.XtraEditors.LabelControl lblDataWarehouse;
        private DevExpress.Utils.ImageCollection icDataWarehouse;
        private DevExpress.XtraEditors.ListBoxControl lstDataFields;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.Utils.ImageCollection icButtons;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.HyperlinkLabelControl lnkClear;
        private DevExpress.XtraBars.BarButtonItem bbiQuery;
        private DevExpress.XtraEditors.LabelControl lblTip;
    }
}