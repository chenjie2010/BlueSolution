namespace Blue.WindowsFormsClient.DataConvertionModule
{
    partial class TableRelationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableRelationForm));
            this.gcTableAndDataField = new DevExpress.XtraEditors.GroupControl();
            this.lblDestDatabaseName = new DevExpress.XtraEditors.LabelControl();
            this.txtDestDatabaseName = new DevExpress.XtraEditors.TextEdit();
            this.lblSourceDatabaseName = new DevExpress.XtraEditors.LabelControl();
            this.txtSourceDatabaseName = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcDataFieldRelation = new DevExpress.XtraGrid.GridControl();
            this.gridDataField = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDataFieldSourceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDataFieldSourceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDataFieldDestName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricmbDestinationDataField = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.gcSwap = new DevExpress.XtraEditors.GroupControl();
            this.gcTableRelation = new DevExpress.XtraGrid.GridControl();
            this.gridTable = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColSourceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColSourceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColDestinationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricmbDestinationTable = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnItmReset = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClear = new DevExpress.XtraBars.BarButtonItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiEdit = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCancel = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.btnItmResetDataField = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClearDataField = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuDataField = new DevExpress.XtraBars.PopupMenu(this.components);
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.defaultBarAndDockingController1 = new DevExpress.XtraBars.DefaultBarAndDockingController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcTableAndDataField)).BeginInit();
            this.gcTableAndDataField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDestDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFieldRelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDataField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbDestinationDataField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSwap)).BeginInit();
            this.gcSwap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTableRelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbDestinationTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuDataField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController1.Controller)).BeginInit();
            this.SuspendLayout();
            // 
            // gcTableAndDataField
            // 
            this.gcTableAndDataField.Controls.Add(this.lblDestDatabaseName);
            this.gcTableAndDataField.Controls.Add(this.txtDestDatabaseName);
            this.gcTableAndDataField.Controls.Add(this.lblSourceDatabaseName);
            this.gcTableAndDataField.Controls.Add(this.txtSourceDatabaseName);
            this.gcTableAndDataField.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcTableAndDataField.Location = new System.Drawing.Point(0, 26);
            this.gcTableAndDataField.LookAndFeel.SkinName = "Money Twins";
            this.gcTableAndDataField.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcTableAndDataField.Name = "gcTableAndDataField";
            this.gcTableAndDataField.Size = new System.Drawing.Size(877, 60);
            this.gcTableAndDataField.TabIndex = 1;
            this.gcTableAndDataField.Text = "数据库对应关系";
            // 
            // lblDestDatabaseName
            // 
            this.lblDestDatabaseName.Location = new System.Drawing.Point(422, 33);
            this.lblDestDatabaseName.Name = "lblDestDatabaseName";
            this.lblDestDatabaseName.Size = new System.Drawing.Size(72, 14);
            this.lblDestDatabaseName.TabIndex = 257;
            this.lblDestDatabaseName.Text = "目标数据库：";
            // 
            // txtDestDatabaseName
            // 
            this.txtDestDatabaseName.Location = new System.Drawing.Point(499, 30);
            this.txtDestDatabaseName.Name = "txtDestDatabaseName";
            this.txtDestDatabaseName.Properties.ReadOnly = true;
            this.txtDestDatabaseName.Size = new System.Drawing.Size(305, 20);
            this.txtDestDatabaseName.TabIndex = 255;
            // 
            // lblSourceDatabaseName
            // 
            this.lblSourceDatabaseName.Location = new System.Drawing.Point(8, 34);
            this.lblSourceDatabaseName.Name = "lblSourceDatabaseName";
            this.lblSourceDatabaseName.Size = new System.Drawing.Size(60, 14);
            this.lblSourceDatabaseName.TabIndex = 256;
            this.lblSourceDatabaseName.Text = "源数据库：";
            // 
            // txtSourceDatabaseName
            // 
            this.txtSourceDatabaseName.Location = new System.Drawing.Point(71, 31);
            this.txtSourceDatabaseName.Name = "txtSourceDatabaseName";
            this.txtSourceDatabaseName.Properties.ReadOnly = true;
            this.txtSourceDatabaseName.Size = new System.Drawing.Size(305, 20);
            this.txtSourceDatabaseName.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcDataFieldRelation);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(408, 2);
            this.groupControl1.LookAndFeel.SkinName = "Money Twins";
            this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(467, 328);
            this.groupControl1.TabIndex = 21;
            this.groupControl1.Text = "字段的对应关系";
            // 
            // gcDataFieldRelation
            // 
            this.gcDataFieldRelation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDataFieldRelation.Location = new System.Drawing.Point(2, 22);
            this.gcDataFieldRelation.LookAndFeel.SkinName = "Money Twins";
            this.gcDataFieldRelation.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcDataFieldRelation.MainView = this.gridDataField;
            this.gcDataFieldRelation.Name = "gcDataFieldRelation";
            this.gcDataFieldRelation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ricmbDestinationDataField});
            this.gcDataFieldRelation.Size = new System.Drawing.Size(463, 304);
            this.gcDataFieldRelation.TabIndex = 19;
            this.gcDataFieldRelation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridDataField});
            // 
            // gridDataField
            // 
            this.gridDataField.ActiveFilterEnabled = false;
            this.gridDataField.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridDataField.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridDataField.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridDataField.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridDataField.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gridDataField.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gridDataField.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gridDataField.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gridDataField.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(190)))), ((int)(((byte)(243)))));
            this.gridDataField.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gridDataField.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridDataField.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gridDataField.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gridDataField.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gridDataField.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gridDataField.Appearance.Empty.Options.UseBackColor = true;
            this.gridDataField.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.gridDataField.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridDataField.Appearance.EvenRow.Options.UseForeColor = true;
            this.gridDataField.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridDataField.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridDataField.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridDataField.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridDataField.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gridDataField.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gridDataField.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gridDataField.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gridDataField.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            this.gridDataField.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gridDataField.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gridDataField.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.gridDataField.Appearance.FixedLine.Options.UseBackColor = true;
            this.gridDataField.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gridDataField.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridDataField.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gridDataField.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.gridDataField.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridDataField.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridDataField.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridDataField.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridDataField.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridDataField.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridDataField.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridDataField.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gridDataField.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gridDataField.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridDataField.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridDataField.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridDataField.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.GroupButton.Options.UseBackColor = true;
            this.gridDataField.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gridDataField.Appearance.GroupButton.Options.UseForeColor = true;
            this.gridDataField.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridDataField.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridDataField.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gridDataField.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gridDataField.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gridDataField.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gridDataField.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridDataField.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gridDataField.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gridDataField.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridDataField.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridDataField.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gridDataField.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridDataField.Appearance.GroupRow.Options.UseBorderColor = true;
            this.gridDataField.Appearance.GroupRow.Options.UseFont = true;
            this.gridDataField.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridDataField.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridDataField.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridDataField.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridDataField.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridDataField.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridDataField.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gridDataField.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridDataField.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(228)))));
            this.gridDataField.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(224)))), ((int)(((byte)(251)))));
            this.gridDataField.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridDataField.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridDataField.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gridDataField.Appearance.HorzLine.Options.UseBackColor = true;
            this.gridDataField.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gridDataField.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.OddRow.Options.UseBackColor = true;
            this.gridDataField.Appearance.OddRow.Options.UseForeColor = true;
            this.gridDataField.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gridDataField.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(185)))));
            this.gridDataField.Appearance.Preview.Options.UseBackColor = true;
            this.gridDataField.Appearance.Preview.Options.UseForeColor = true;
            this.gridDataField.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridDataField.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.gridDataField.Appearance.Row.Options.UseBackColor = true;
            this.gridDataField.Appearance.Row.Options.UseForeColor = true;
            this.gridDataField.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            this.gridDataField.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gridDataField.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(126)))), ((int)(((byte)(217)))));
            this.gridDataField.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gridDataField.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridDataField.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridDataField.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gridDataField.Appearance.VertLine.Options.UseBackColor = true;
            this.gridDataField.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDataFieldSourceId,
            this.gcDataFieldSourceName,
            this.gcDataFieldDestName});
            this.gridDataField.GridControl = this.gcDataFieldRelation;
            this.gridDataField.IndicatorWidth = 30;
            this.gridDataField.Name = "gridDataField";
            this.gridDataField.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridDataField.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridDataField.OptionsCustomization.AllowColumnMoving = false;
            this.gridDataField.OptionsCustomization.AllowFilter = false;
            this.gridDataField.OptionsCustomization.AllowGroup = false;
            this.gridDataField.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridDataField.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridDataField.OptionsFilter.AllowFilterEditor = false;
            this.gridDataField.OptionsFind.AllowFindPanel = false;
            this.gridDataField.OptionsMenu.EnableColumnMenu = false;
            this.gridDataField.OptionsMenu.EnableFooterMenu = false;
            this.gridDataField.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridDataField.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridDataField.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gridDataField.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridDataField.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridDataField.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridDataField.OptionsView.EnableAppearanceEvenRow = true;
            this.gridDataField.OptionsView.EnableAppearanceOddRow = true;
            this.gridDataField.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridDataField.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridDataField.OptionsView.ShowGroupPanel = false;
            this.gridDataField.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridDataField_CustomDrawRowIndicator);
            this.gridDataField.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridDataField_CellValueChanged);
            this.gridDataField.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridDataField_MouseUp);
            // 
            // gcDataFieldSourceId
            // 
            this.gcDataFieldSourceId.Caption = "源数据表字段编号";
            this.gcDataFieldSourceId.Name = "gcDataFieldSourceId";
            // 
            // gcDataFieldSourceName
            // 
            this.gcDataFieldSourceName.Caption = "源数据表字段名称";
            this.gcDataFieldSourceName.FieldName = "SourceName";
            this.gcDataFieldSourceName.Name = "gcDataFieldSourceName";
            this.gcDataFieldSourceName.OptionsColumn.AllowEdit = false;
            this.gcDataFieldSourceName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcDataFieldSourceName.OptionsColumn.ReadOnly = true;
            this.gcDataFieldSourceName.OptionsFilter.AllowAutoFilter = false;
            this.gcDataFieldSourceName.OptionsFilter.AllowFilter = false;
            this.gcDataFieldSourceName.Visible = true;
            this.gcDataFieldSourceName.VisibleIndex = 0;
            this.gcDataFieldSourceName.Width = 136;
            // 
            // gcDataFieldDestName
            // 
            this.gcDataFieldDestName.Caption = "目的数据表字段名称";
            this.gcDataFieldDestName.ColumnEdit = this.ricmbDestinationDataField;
            this.gcDataFieldDestName.FieldName = "DestinationId";
            this.gcDataFieldDestName.Name = "gcDataFieldDestName";
            this.gcDataFieldDestName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcDataFieldDestName.OptionsFilter.AllowAutoFilter = false;
            this.gcDataFieldDestName.OptionsFilter.AllowFilter = false;
            this.gcDataFieldDestName.Visible = true;
            this.gcDataFieldDestName.VisibleIndex = 1;
            this.gcDataFieldDestName.Width = 157;
            // 
            // ricmbDestinationDataField
            // 
            this.ricmbDestinationDataField.AutoHeight = false;
            this.ricmbDestinationDataField.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricmbDestinationDataField.Name = "ricmbDestinationDataField";
            this.ricmbDestinationDataField.SmallImages = this.imageList;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Data_Table_Node.png");
            this.imageList.Images.SetKeyName(1, "Data_DataField_Node.png");
            // 
            // gcSwap
            // 
            this.gcSwap.Controls.Add(this.gcTableRelation);
            this.gcSwap.Dock = System.Windows.Forms.DockStyle.Left;
            this.gcSwap.Location = new System.Drawing.Point(2, 2);
            this.gcSwap.LookAndFeel.SkinName = "Money Twins";
            this.gcSwap.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcSwap.Name = "gcSwap";
            this.gcSwap.Size = new System.Drawing.Size(406, 328);
            this.gcSwap.TabIndex = 19;
            this.gcSwap.Text = "表的对应关系";
            // 
            // gcTableRelation
            // 
            this.gcTableRelation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTableRelation.Location = new System.Drawing.Point(2, 22);
            this.gcTableRelation.LookAndFeel.SkinName = "Money Twins";
            this.gcTableRelation.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcTableRelation.MainView = this.gridTable;
            this.gcTableRelation.Name = "gcTableRelation";
            this.gcTableRelation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ricmbDestinationTable});
            this.gcTableRelation.Size = new System.Drawing.Size(402, 304);
            this.gcTableRelation.TabIndex = 18;
            this.gcTableRelation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridTable});
            // 
            // gridTable
            // 
            this.gridTable.ActiveFilterEnabled = false;
            this.gridTable.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridTable.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridTable.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridTable.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridTable.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gridTable.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gridTable.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gridTable.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gridTable.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(190)))), ((int)(((byte)(243)))));
            this.gridTable.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gridTable.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridTable.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gridTable.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gridTable.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gridTable.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gridTable.Appearance.Empty.Options.UseBackColor = true;
            this.gridTable.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.gridTable.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridTable.Appearance.EvenRow.Options.UseForeColor = true;
            this.gridTable.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridTable.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridTable.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridTable.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridTable.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gridTable.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gridTable.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gridTable.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gridTable.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            this.gridTable.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gridTable.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gridTable.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.gridTable.Appearance.FixedLine.Options.UseBackColor = true;
            this.gridTable.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gridTable.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridTable.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gridTable.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.gridTable.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridTable.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridTable.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridTable.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridTable.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridTable.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridTable.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridTable.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gridTable.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gridTable.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridTable.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridTable.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridTable.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.GroupButton.Options.UseBackColor = true;
            this.gridTable.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gridTable.Appearance.GroupButton.Options.UseForeColor = true;
            this.gridTable.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridTable.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridTable.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gridTable.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gridTable.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gridTable.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gridTable.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridTable.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gridTable.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gridTable.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridTable.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridTable.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gridTable.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridTable.Appearance.GroupRow.Options.UseBorderColor = true;
            this.gridTable.Appearance.GroupRow.Options.UseFont = true;
            this.gridTable.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridTable.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridTable.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridTable.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridTable.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridTable.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridTable.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gridTable.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridTable.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(228)))));
            this.gridTable.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(224)))), ((int)(((byte)(251)))));
            this.gridTable.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridTable.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridTable.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gridTable.Appearance.HorzLine.Options.UseBackColor = true;
            this.gridTable.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gridTable.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.OddRow.Options.UseBackColor = true;
            this.gridTable.Appearance.OddRow.Options.UseForeColor = true;
            this.gridTable.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gridTable.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(185)))));
            this.gridTable.Appearance.Preview.Options.UseBackColor = true;
            this.gridTable.Appearance.Preview.Options.UseForeColor = true;
            this.gridTable.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridTable.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.gridTable.Appearance.Row.Options.UseBackColor = true;
            this.gridTable.Appearance.Row.Options.UseForeColor = true;
            this.gridTable.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            this.gridTable.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gridTable.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(126)))), ((int)(((byte)(217)))));
            this.gridTable.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gridTable.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridTable.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridTable.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gridTable.Appearance.VertLine.Options.UseBackColor = true;
            this.gridTable.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColSourceId,
            this.gridColSourceName,
            this.gridColDestinationName});
            this.gridTable.GridControl = this.gcTableRelation;
            this.gridTable.IndicatorWidth = 30;
            this.gridTable.Name = "gridTable";
            this.gridTable.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridTable.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridTable.OptionsCustomization.AllowColumnMoving = false;
            this.gridTable.OptionsCustomization.AllowFilter = false;
            this.gridTable.OptionsCustomization.AllowGroup = false;
            this.gridTable.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridTable.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridTable.OptionsFilter.AllowFilterEditor = false;
            this.gridTable.OptionsFind.AllowFindPanel = false;
            this.gridTable.OptionsMenu.EnableColumnMenu = false;
            this.gridTable.OptionsMenu.EnableFooterMenu = false;
            this.gridTable.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridTable.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridTable.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gridTable.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridTable.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridTable.OptionsView.EnableAppearanceEvenRow = true;
            this.gridTable.OptionsView.EnableAppearanceOddRow = true;
            this.gridTable.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridTable.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridTable.OptionsView.ShowGroupPanel = false;
            this.gridTable.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridTable_RowClick);
            this.gridTable.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridTable_CustomDrawRowIndicator);
            this.gridTable.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridTable_CellValueChanged);
            this.gridTable.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridTable_MouseUp);
            // 
            // gridColSourceId
            // 
            this.gridColSourceId.Caption = "源数据表编号";
            this.gridColSourceId.FieldName = "DestinationId";
            this.gridColSourceId.Name = "gridColSourceId";
            // 
            // gridColSourceName
            // 
            this.gridColSourceName.Caption = "源数据表名称";
            this.gridColSourceName.FieldName = "SourceName";
            this.gridColSourceName.Name = "gridColSourceName";
            this.gridColSourceName.OptionsColumn.AllowEdit = false;
            this.gridColSourceName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColSourceName.OptionsColumn.ReadOnly = true;
            this.gridColSourceName.OptionsFilter.AllowAutoFilter = false;
            this.gridColSourceName.OptionsFilter.AllowFilter = false;
            this.gridColSourceName.Visible = true;
            this.gridColSourceName.VisibleIndex = 0;
            this.gridColSourceName.Width = 136;
            // 
            // gridColDestinationName
            // 
            this.gridColDestinationName.Caption = "目的数据表名称";
            this.gridColDestinationName.ColumnEdit = this.ricmbDestinationTable;
            this.gridColDestinationName.FieldName = "DestinationId";
            this.gridColDestinationName.Name = "gridColDestinationName";
            this.gridColDestinationName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColDestinationName.OptionsFilter.AllowAutoFilter = false;
            this.gridColDestinationName.OptionsFilter.AllowFilter = false;
            this.gridColDestinationName.Visible = true;
            this.gridColDestinationName.VisibleIndex = 1;
            this.gridColDestinationName.Width = 157;
            // 
            // ricmbDestinationTable
            // 
            this.ricmbDestinationTable.AutoHeight = false;
            this.ricmbDestinationTable.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricmbDestinationTable.Name = "ricmbDestinationTable";
            this.ricmbDestinationTable.SmallImages = this.imageList;
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmReset),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmClear)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // btnItmReset
            // 
            this.btnItmReset.Caption = "重置(&R)";
            this.btnItmReset.Id = 11;
            this.btnItmReset.Name = "btnItmReset";
            this.btnItmReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmReset_ItemClick_1);
            // 
            // btnItmClear
            // 
            this.btnItmClear.Caption = "清除(&C)";
            this.btnItmClear.Id = 12;
            this.btnItmClear.Name = "btnItmClear";
            this.btnItmClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmClear_ItemClick_1);
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.icTools;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiEdit,
            this.bbiClose,
            this.bbiSave,
            this.bbiCancel,
            this.btnItmReset,
            this.btnItmClear,
            this.btnItmResetDataField,
            this.btnItmClearDataField});
            this.barManager.MaxItemId = 15;
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bbiSave, "保存(&S)", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiCancel, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Tools";
            // 
            // bbiEdit
            // 
            this.bbiEdit.Caption = "编辑(&E)";
            this.bbiEdit.Id = 0;
            this.bbiEdit.ImageIndex = 0;
            this.bbiEdit.Name = "bbiEdit";
            this.bbiEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEdit_ItemClick);
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "保存(&S)";
            this.bbiSave.Id = 7;
            this.bbiSave.ImageIndex = 1;
            this.bbiSave.Name = "bbiSave";
            this.bbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSave_ItemClick);
            // 
            // bbiCancel
            // 
            this.bbiCancel.Caption = "取消(&Q)";
            this.bbiCancel.Id = 10;
            this.bbiCancel.ImageIndex = 2;
            this.bbiCancel.Name = "bbiCancel";
            this.bbiCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCancel_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.Caption = "关闭(&C)";
            this.bbiClose.Id = 2;
            this.bbiClose.ImageIndex = 3;
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(877, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 418);
            this.barDockControlBottom.Size = new System.Drawing.Size(877, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 392);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(877, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 392);
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "Tools_Edit.png");
            this.icTools.Images.SetKeyName(1, "Tools_Save.png");
            this.icTools.Images.SetKeyName(2, "Tools_Cancel.png");
            this.icTools.Images.SetKeyName(3, "Common_Close_1.png");
            // 
            // btnItmResetDataField
            // 
            this.btnItmResetDataField.Caption = "重置(&R)";
            this.btnItmResetDataField.Id = 13;
            this.btnItmResetDataField.Name = "btnItmResetDataField";
            this.btnItmResetDataField.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmResetDataField_ItemClick_1);
            // 
            // btnItmClearDataField
            // 
            this.btnItmClearDataField.Caption = "清除(&C)";
            this.btnItmClearDataField.Id = 14;
            this.btnItmClearDataField.Name = "btnItmClearDataField";
            this.btnItmClearDataField.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmClearDataField_ItemClick_1);
            // 
            // popupMenuDataField
            // 
            this.popupMenuDataField.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmResetDataField),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmClearDataField)});
            this.popupMenuDataField.Manager = this.barManager;
            this.popupMenuDataField.Name = "popupMenuDataField";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.groupControl1);
            this.pnlMain.Controls.Add(this.gcSwap);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 86);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(877, 332);
            this.pnlMain.TabIndex = 6;
            // 
            // defaultBarAndDockingController1
            // 
            this.defaultBarAndDockingController1.Controller.LookAndFeel.SkinName = "Money Twins";
            this.defaultBarAndDockingController1.Controller.LookAndFeel.UseDefaultLookAndFeel = false;
            this.defaultBarAndDockingController1.Controller.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.defaultBarAndDockingController1.Controller.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // TableRelationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(877, 418);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.gcTableAndDataField);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TableRelationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库间表对应关系";
            this.Load += new System.EventHandler(this.LocalTableRelationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcTableAndDataField)).EndInit();
            this.gcTableAndDataField.ResumeLayout(false);
            this.gcTableAndDataField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDestDatabaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceDatabaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFieldRelation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDataField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbDestinationDataField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSwap)).EndInit();
            this.gcSwap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTableRelation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbDestinationTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuDataField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController1.Controller)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcTableAndDataField;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gcDataFieldRelation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridDataField;
        private DevExpress.XtraGrid.Columns.GridColumn gcDataFieldSourceId;
        private DevExpress.XtraGrid.Columns.GridColumn gcDataFieldSourceName;
        private DevExpress.XtraGrid.Columns.GridColumn gcDataFieldDestName;
        private DevExpress.XtraEditors.GroupControl gcSwap;
        private DevExpress.XtraGrid.GridControl gcTableRelation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridTable;
        private DevExpress.XtraGrid.Columns.GridColumn gridColSourceId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColSourceName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColDestinationName;
        private DevExpress.XtraEditors.TextEdit txtDestDatabaseName;
        private DevExpress.XtraEditors.TextEdit txtSourceDatabaseName;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ricmbDestinationDataField;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ricmbDestinationTable;
        private System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.PopupMenu popupMenuDataField;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.XtraEditors.LabelControl lblDestDatabaseName;
        private DevExpress.XtraEditors.LabelControl lblSourceDatabaseName;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem bbiEdit;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiCancel;
        private DevExpress.XtraBars.DefaultBarAndDockingController defaultBarAndDockingController1;
        private DevExpress.XtraBars.BarButtonItem btnItmReset;
        private DevExpress.XtraBars.BarButtonItem btnItmClear;
        private DevExpress.XtraBars.BarButtonItem btnItmResetDataField;
        private DevExpress.XtraBars.BarButtonItem btnItmClearDataField;
    }
}