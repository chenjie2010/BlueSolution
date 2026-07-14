namespace Blue.WindowsFormsClient.DataConvertionModule
{
    partial class DataExchangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataExchangeForm));
            this.pnTop = new System.Windows.Forms.Panel();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.gcDataFieldRelation = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColDestination = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricmbSource = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridColCustom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rsitmCustomValue = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtDestDataField = new DevExpress.XtraEditors.MemoEdit();
            this.txtSourceDataField = new DevExpress.XtraEditors.MemoEdit();
            this.btnDestDataField = new DevExpress.XtraEditors.SimpleButton();
            this.btnSourceDataField = new DevExpress.XtraEditors.SimpleButton();
            this.lblSourceDataField = new System.Windows.Forms.Label();
            this.cmbDestDataField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbSourceDataField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDestDataField = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.icmbImport = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.beDestTable = new DevExpress.XtraEditors.ButtonEdit();
            this.beSourceTable = new DevExpress.XtraEditors.ButtonEdit();
            this.rtxtToolTip = new System.Windows.Forms.RichTextBox();
            this.btnRowColClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnRowColSumbit = new DevExpress.XtraEditors.SimpleButton();
            this.btnRowColQuery = new DevExpress.XtraEditors.SimpleButton();
            this.lblImport = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gcQuery = new DevExpress.XtraEditors.GroupControl();
            this.devExpressGrid = new AppFramework.WinFormsControls.DevExpressGrid();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnItmReset = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClear = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFieldRelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rsitmCustomValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDestDataField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceDataField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDestDataField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSourceDataField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbImport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beDestTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beSourceTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuery)).BeginInit();
            this.gcQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.groupControl3);
            this.pnTop.Controls.Add(this.groupControl1);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(1156, 249);
            this.pnTop.TabIndex = 16;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.gcDataFieldRelation);
            this.groupControl3.Controls.Add(this.panelControl2);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(486, 0);
            this.groupControl3.LookAndFeel.SkinName = "Money Twins";
            this.groupControl3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(670, 249);
            this.groupControl3.TabIndex = 17;
            this.groupControl3.Text = "字段条件";
            // 
            // gcDataFieldRelation
            // 
            this.gcDataFieldRelation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDataFieldRelation.Location = new System.Drawing.Point(2, 22);
            this.gcDataFieldRelation.LookAndFeel.SkinName = "Money Twins";
            this.gcDataFieldRelation.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcDataFieldRelation.MainView = this.gridView;
            this.gcDataFieldRelation.Name = "gcDataFieldRelation";
            this.gcDataFieldRelation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ricmbSource,
            this.rsitmCustomValue});
            this.gcDataFieldRelation.Size = new System.Drawing.Size(377, 225);
            this.gcDataFieldRelation.TabIndex = 11;
            this.gcDataFieldRelation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
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
            this.gridColDestination,
            this.gridColSource,
            this.gridColCustom});
            this.gridView.GridControl = this.gcDataFieldRelation;
            this.gridView.IndicatorWidth = 50;
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
            this.gridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridView_MouseUp);
            // 
            // gridColDestination
            // 
            this.gridColDestination.Caption = "目的字段";
            this.gridColDestination.FieldName = "DestinationName";
            this.gridColDestination.Name = "gridColDestination";
            this.gridColDestination.OptionsColumn.AllowEdit = false;
            this.gridColDestination.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColDestination.OptionsColumn.ReadOnly = true;
            this.gridColDestination.OptionsFilter.AllowAutoFilter = false;
            this.gridColDestination.OptionsFilter.AllowFilter = false;
            this.gridColDestination.Visible = true;
            this.gridColDestination.VisibleIndex = 0;
            this.gridColDestination.Width = 85;
            // 
            // gridColSource
            // 
            this.gridColSource.Caption = "源字段";
            this.gridColSource.ColumnEdit = this.ricmbSource;
            this.gridColSource.FieldName = "SourceId";
            this.gridColSource.Name = "gridColSource";
            this.gridColSource.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColSource.OptionsFilter.AllowAutoFilter = false;
            this.gridColSource.OptionsFilter.AllowFilter = false;
            this.gridColSource.Visible = true;
            this.gridColSource.VisibleIndex = 1;
            this.gridColSource.Width = 85;
            // 
            // ricmbSource
            // 
            this.ricmbSource.AutoHeight = false;
            this.ricmbSource.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricmbSource.LookAndFeel.SkinName = "Blue";
            this.ricmbSource.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ricmbSource.Name = "ricmbSource";
            // 
            // gridColCustom
            // 
            this.gridColCustom.Caption = "自定义源值";
            this.gridColCustom.ColumnEdit = this.rsitmCustomValue;
            this.gridColCustom.FieldName = "CustomDataFieldName";
            this.gridColCustom.Name = "gridColCustom";
            this.gridColCustom.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColCustom.OptionsColumn.ReadOnly = true;
            this.gridColCustom.OptionsFilter.AllowAutoFilter = false;
            this.gridColCustom.OptionsFilter.AllowFilter = false;
            this.gridColCustom.Visible = true;
            this.gridColCustom.VisibleIndex = 2;
            this.gridColCustom.Width = 85;
            // 
            // rsitmCustomValue
            // 
            this.rsitmCustomValue.AutoHeight = false;
            this.rsitmCustomValue.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rsitmCustomValue.Name = "rsitmCustomValue";
            this.rsitmCustomValue.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.rsitmCustomValue_ButtonPressed);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.txtDestDataField);
            this.panelControl2.Controls.Add(this.txtSourceDataField);
            this.panelControl2.Controls.Add(this.btnDestDataField);
            this.panelControl2.Controls.Add(this.btnSourceDataField);
            this.panelControl2.Controls.Add(this.lblSourceDataField);
            this.panelControl2.Controls.Add(this.cmbDestDataField);
            this.panelControl2.Controls.Add(this.cmbSourceDataField);
            this.panelControl2.Controls.Add(this.lblDestDataField);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(379, 22);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(289, 225);
            this.panelControl2.TabIndex = 12;
            // 
            // txtDestDataField
            // 
            this.txtDestDataField.Location = new System.Drawing.Point(105, 146);
            this.txtDestDataField.Name = "txtDestDataField";
            this.txtDestDataField.Properties.LookAndFeel.SkinName = "Blue";
            this.txtDestDataField.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtDestDataField.Properties.MaxLength = 1024;
            this.txtDestDataField.Size = new System.Drawing.Size(171, 68);
            this.txtDestDataField.TabIndex = 10;
            // 
            // txtSourceDataField
            // 
            this.txtSourceDataField.Location = new System.Drawing.Point(105, 37);
            this.txtSourceDataField.Name = "txtSourceDataField";
            this.txtSourceDataField.Properties.LookAndFeel.SkinName = "Blue";
            this.txtSourceDataField.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtSourceDataField.Properties.MaxLength = 1024;
            this.txtSourceDataField.Size = new System.Drawing.Size(171, 68);
            this.txtSourceDataField.TabIndex = 8;
            // 
            // btnDestDataField
            // 
            this.btnDestDataField.Location = new System.Drawing.Point(256, 114);
            this.btnDestDataField.LookAndFeel.SkinName = "Money Twins";
            this.btnDestDataField.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnDestDataField.Name = "btnDestDataField";
            this.btnDestDataField.Size = new System.Drawing.Size(20, 21);
            this.btnDestDataField.TabIndex = 9;
            this.btnDestDataField.Text = "...";
            this.btnDestDataField.Click += new System.EventHandler(this.btnDestDataField_Click);
            // 
            // btnSourceDataField
            // 
            this.btnSourceDataField.Location = new System.Drawing.Point(256, 7);
            this.btnSourceDataField.LookAndFeel.SkinName = "Money Twins";
            this.btnSourceDataField.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSourceDataField.Name = "btnSourceDataField";
            this.btnSourceDataField.Size = new System.Drawing.Size(20, 21);
            this.btnSourceDataField.TabIndex = 7;
            this.btnSourceDataField.Text = "...";
            this.btnSourceDataField.Click += new System.EventHandler(this.btnSourceDataField_Click);
            // 
            // lblSourceDataField
            // 
            this.lblSourceDataField.AutoSize = true;
            this.lblSourceDataField.Location = new System.Drawing.Point(21, 11);
            this.lblSourceDataField.Name = "lblSourceDataField";
            this.lblSourceDataField.Size = new System.Drawing.Size(79, 14);
            this.lblSourceDataField.TabIndex = 229;
            this.lblSourceDataField.Text = "源字段条件：";
            // 
            // cmbDestDataField
            // 
            this.cmbDestDataField.Location = new System.Drawing.Point(105, 115);
            this.cmbDestDataField.Name = "cmbDestDataField";
            this.cmbDestDataField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDestDataField.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbDestDataField.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbDestDataField.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDestDataField.Size = new System.Drawing.Size(145, 20);
            this.cmbDestDataField.TabIndex = 8;
            // 
            // cmbSourceDataField
            // 
            this.cmbSourceDataField.Location = new System.Drawing.Point(105, 8);
            this.cmbSourceDataField.Name = "cmbSourceDataField";
            this.cmbSourceDataField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSourceDataField.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbSourceDataField.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbSourceDataField.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSourceDataField.Size = new System.Drawing.Size(145, 20);
            this.cmbSourceDataField.TabIndex = 6;
            // 
            // lblDestDataField
            // 
            this.lblDestDataField.AutoSize = true;
            this.lblDestDataField.Location = new System.Drawing.Point(9, 118);
            this.lblDestDataField.Name = "lblDestDataField";
            this.lblDestDataField.Size = new System.Drawing.Size(91, 14);
            this.lblDestDataField.TabIndex = 230;
            this.lblDestDataField.Text = "目的字段条件：";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.icmbImport);
            this.groupControl1.Controls.Add(this.beDestTable);
            this.groupControl1.Controls.Add(this.beSourceTable);
            this.groupControl1.Controls.Add(this.rtxtToolTip);
            this.groupControl1.Controls.Add(this.btnRowColClear);
            this.groupControl1.Controls.Add(this.btnRowColSumbit);
            this.groupControl1.Controls.Add(this.btnRowColQuery);
            this.groupControl1.Controls.Add(this.lblImport);
            this.groupControl1.Controls.Add(this.label11);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.LookAndFeel.SkinName = "Money Twins";
            this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(486, 249);
            this.groupControl1.TabIndex = 15;
            this.groupControl1.Text = "表对应关系";
            // 
            // icmbImport
            // 
            this.icmbImport.Location = new System.Drawing.Point(80, 88);
            this.icmbImport.Name = "icmbImport";
            this.icmbImport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbImport.Size = new System.Drawing.Size(385, 20);
            this.icmbImport.TabIndex = 241;
            // 
            // beDestTable
            // 
            this.beDestTable.Location = new System.Drawing.Point(80, 60);
            this.beDestTable.Name = "beDestTable";
            this.beDestTable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beDestTable.Size = new System.Drawing.Size(385, 20);
            this.beDestTable.TabIndex = 1;
            this.beDestTable.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beDestTable_ButtonClick);
            // 
            // beSourceTable
            // 
            this.beSourceTable.Location = new System.Drawing.Point(80, 29);
            this.beSourceTable.Name = "beSourceTable";
            this.beSourceTable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beSourceTable.Size = new System.Drawing.Size(385, 20);
            this.beSourceTable.TabIndex = 0;
            this.beSourceTable.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beSourceTable_ButtonClick);
            // 
            // rtxtToolTip
            // 
            this.rtxtToolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.rtxtToolTip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtToolTip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtxtToolTip.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.rtxtToolTip.Location = new System.Drawing.Point(2, 157);
            this.rtxtToolTip.MaxLength = 4000;
            this.rtxtToolTip.Name = "rtxtToolTip";
            this.rtxtToolTip.ReadOnly = true;
            this.rtxtToolTip.Size = new System.Drawing.Size(482, 90);
            this.rtxtToolTip.TabIndex = 240;
            this.rtxtToolTip.Text = "提示：\n【1】非空的自定义源值比源字段的值被优先使用；\n【2】导入方式为“行复制”时，目标字段条件无效；\n【3】源字段和目的字段均为物理字段类型，可自定义源值实现" +
    "逻辑字段的值。";
            // 
            // btnRowColClear
            // 
            this.btnRowColClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRowColClear.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.btnRowColClear.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRowColClear.Location = new System.Drawing.Point(349, 120);
            this.btnRowColClear.LookAndFeel.SkinName = "Blue";
            this.btnRowColClear.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnRowColClear.Name = "btnRowColClear";
            this.btnRowColClear.Size = new System.Drawing.Size(24, 24);
            this.btnRowColClear.TabIndex = 5;
            this.btnRowColClear.Click += new System.EventHandler(this.btnRowColClear_Click);
            // 
            // btnRowColSumbit
            // 
            this.btnRowColSumbit.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Apply_Small;
            this.btnRowColSumbit.Location = new System.Drawing.Point(254, 122);
            this.btnRowColSumbit.LookAndFeel.SkinName = "Money Twins";
            this.btnRowColSumbit.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnRowColSumbit.Name = "btnRowColSumbit";
            this.btnRowColSumbit.Size = new System.Drawing.Size(73, 21);
            this.btnRowColSumbit.TabIndex = 4;
            this.btnRowColSumbit.Text = "提交(&S)";
            this.btnRowColSumbit.Click += new System.EventHandler(this.btnRowColSumbit_Click);
            // 
            // btnRowColQuery
            // 
            this.btnRowColQuery.Image = global::Blue.WindowsFormsClient.Properties.Resources.Buttom_Quer_Small;
            this.btnRowColQuery.Location = new System.Drawing.Point(174, 123);
            this.btnRowColQuery.LookAndFeel.SkinName = "Money Twins";
            this.btnRowColQuery.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnRowColQuery.Name = "btnRowColQuery";
            this.btnRowColQuery.Size = new System.Drawing.Size(73, 21);
            this.btnRowColQuery.TabIndex = 3;
            this.btnRowColQuery.Text = "查询(&Q)";
            this.btnRowColQuery.Click += new System.EventHandler(this.btnRowColQuery_Click);
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Location = new System.Drawing.Point(10, 91);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(67, 14);
            this.lblImport.TabIndex = 232;
            this.lblImport.Text = "导入方式：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 14);
            this.label11.TabIndex = 230;
            this.label11.Text = "目的表：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 228;
            this.label3.Text = "源表：";
            // 
            // gcQuery
            // 
            this.gcQuery.Controls.Add(this.devExpressGrid);
            this.gcQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcQuery.Location = new System.Drawing.Point(0, 249);
            this.gcQuery.Name = "gcQuery";
            this.gcQuery.Size = new System.Drawing.Size(1156, 342);
            this.gcQuery.TabIndex = 17;
            this.gcQuery.Text = "查询结果";
            // 
            // devExpressGrid
            // 
            this.devExpressGrid.CheckboxColumnCaption = null;
            this.devExpressGrid.ColumnHeaderTexts = new string[0];
            this.devExpressGrid.DataKeyNames = new string[0];
            this.devExpressGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devExpressGrid.ExportedExcel = false;
            this.devExpressGrid.FootText = null;
            this.devExpressGrid.ImportedExcel = false;
            this.devExpressGrid.IsMainTable = false;
            this.devExpressGrid.Location = new System.Drawing.Point(2, 21);
            this.devExpressGrid.Name = "devExpressGrid";
            this.devExpressGrid.PageSize = 50;
            this.devExpressGrid.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devExpressGrid.Size = new System.Drawing.Size(1152, 319);
            this.devExpressGrid.TabIndex = 0;
            this.devExpressGrid.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.devExpressGrid_OnPageIndexChanged);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmReset),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmClear)});
            this.popupMenu.Manager = this.barManager1;
            this.popupMenu.Name = "popupMenu";
            // 
            // btnItmReset
            // 
            this.btnItmReset.Caption = "重置(&R)";
            this.btnItmReset.Id = 0;
            this.btnItmReset.Name = "btnItmReset";
            this.btnItmReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmReset_ItemClick);
            // 
            // btnItmClear
            // 
            this.btnItmClear.Caption = "清除(&C)";
            this.btnItmClear.Id = 1;
            this.btnItmClear.Name = "btnItmClear";
            this.btnItmClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmClear_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItmReset,
            this.btnItmClear});
            this.barManager1.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1156, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 591);
            this.barDockControlBottom.Size = new System.Drawing.Size(1156, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 591);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1156, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 591);
            // 
            // DataExchangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 591);
            this.Controls.Add(this.gcQuery);
            this.Controls.Add(this.pnTop);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataExchangeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据交换";
            this.Load += new System.EventHandler(this.DataExchangeForm_Load);
            this.pnTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFieldRelation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rsitmCustomValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDestDataField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourceDataField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDestDataField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSourceDataField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbImport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beDestTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beSourceTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuery)).EndInit();
            this.gcQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnTop;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraGrid.GridControl gcDataFieldRelation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColDestination;
        private DevExpress.XtraGrid.Columns.GridColumn gridColSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ricmbSource;
        private DevExpress.XtraGrid.Columns.GridColumn gridColCustom;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rsitmCustomValue;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.MemoEdit txtDestDataField;
        private DevExpress.XtraEditors.MemoEdit txtSourceDataField;
        private DevExpress.XtraEditors.SimpleButton btnDestDataField;
        private DevExpress.XtraEditors.SimpleButton btnSourceDataField;
        private System.Windows.Forms.Label lblSourceDataField;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDestDataField;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSourceDataField;
        private System.Windows.Forms.Label lblDestDataField;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.RichTextBox rtxtToolTip;
        private DevExpress.XtraEditors.SimpleButton btnRowColClear;
        private DevExpress.XtraEditors.SimpleButton btnRowColSumbit;
        private DevExpress.XtraEditors.SimpleButton btnRowColQuery;
        private System.Windows.Forms.Label lblImport;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.GroupControl gcQuery;
        private AppFramework.WinFormsControls.DevExpressGrid devExpressGrid;
        private DevExpress.XtraEditors.ButtonEdit beDestTable;
        private DevExpress.XtraEditors.ButtonEdit beSourceTable;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbImport;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem btnItmReset;
        private DevExpress.XtraBars.BarButtonItem btnItmClear;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}