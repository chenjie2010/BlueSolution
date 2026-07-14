namespace AppFramework.WinFormsControls
{
    partial class DevExpressGrid
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevExpressGrid));
            this.panelControlFooter = new DevExpress.XtraEditors.PanelControl();
            this.cmbPages = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblFooter = new DevExpress.XtraEditors.LabelControl();
            this.sbtnLast = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnPrevious = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnNext = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControlMain = new DevExpress.XtraEditors.PanelControl();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu();
            this.barSubItemSelect = new DevExpress.XtraBars.BarSubItem();
            this.btnItmSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmReverse = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmCancel = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemEdit = new DevExpress.XtraBars.BarSubItem();
            this.btnItmBatchEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmCompleteEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemDelete = new DevExpress.XtraBars.BarSubItem();
            this.btnItmBatchDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmCompleteDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemMove = new DevExpress.XtraBars.BarSubItem();
            this.btnItmTopRecord = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmPreviousRecord = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmNextRecord = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmBottomRecord = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemTurnPage = new DevExpress.XtraBars.BarSubItem();
            this.btnItmFirstPage = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmPreviousPage = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmNextPage = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmLastPage = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmExport = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmImport = new DevExpress.XtraBars.BarButtonItem();
            this.barManager = new DevExpress.XtraBars.BarManager();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icStateItems = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).BeginInit();
            this.panelControlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPages.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icStateItems)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlFooter
            // 
            this.panelControlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlFooter.Controls.Add(this.cmbPages);
            this.panelControlFooter.Controls.Add(this.lblFooter);
            this.panelControlFooter.Controls.Add(this.sbtnLast);
            this.panelControlFooter.Controls.Add(this.sbtnPrevious);
            this.panelControlFooter.Controls.Add(this.sbtnNext);
            this.panelControlFooter.Controls.Add(this.sbtnFirst);
            this.panelControlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlFooter.Location = new System.Drawing.Point(0, 309);
            this.panelControlFooter.LookAndFeel.SkinName = "Blue";
            this.panelControlFooter.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControlFooter.Name = "panelControlFooter";
            this.panelControlFooter.Size = new System.Drawing.Size(688, 33);
            this.panelControlFooter.TabIndex = 1;
            // 
            // cmbPages
            // 
            this.cmbPages.Location = new System.Drawing.Point(264, 6);
            this.cmbPages.Name = "cmbPages";
            this.cmbPages.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPages.Properties.LookAndFeel.SkinName = "Money Twins";
            this.cmbPages.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbPages.Size = new System.Drawing.Size(67, 20);
            this.cmbPages.TabIndex = 6;
            this.cmbPages.SelectedIndexChanged += new System.EventHandler(this.cmbPages_SelectedIndexChanged);
            this.cmbPages.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbPages_KeyPress);
            // 
            // lblFooter
            // 
            this.lblFooter.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblFooter.Appearance.Options.UseForeColor = true;
            this.lblFooter.Location = new System.Drawing.Point(338, 9);
            this.lblFooter.LookAndFeel.SkinName = "Money Twins";
            this.lblFooter.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(276, 14);
            this.lblFooter.TabIndex = 5;
            this.lblFooter.Text = "总记录 0 条，共 0 页。第 0 页，当前页记录 0 条。";
            // 
            // sbtnLast
            // 
            this.sbtnLast.Location = new System.Drawing.Point(200, 6);
            this.sbtnLast.LookAndFeel.SkinName = "Money Twins";
            this.sbtnLast.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnLast.Name = "sbtnLast";
            this.sbtnLast.Size = new System.Drawing.Size(60, 20);
            this.sbtnLast.TabIndex = 3;
            this.sbtnLast.Text = "末页(&L)";
            this.sbtnLast.Click += new System.EventHandler(this.sbtnLast_Click);
            // 
            // sbtnPrevious
            // 
            this.sbtnPrevious.Location = new System.Drawing.Point(70, 6);
            this.sbtnPrevious.LookAndFeel.SkinName = "Money Twins";
            this.sbtnPrevious.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnPrevious.Name = "sbtnPrevious";
            this.sbtnPrevious.Size = new System.Drawing.Size(60, 20);
            this.sbtnPrevious.TabIndex = 2;
            this.sbtnPrevious.Text = "上一页(&P)";
            this.sbtnPrevious.Click += new System.EventHandler(this.sbtnPrevious_Click);
            // 
            // sbtnNext
            // 
            this.sbtnNext.Location = new System.Drawing.Point(135, 6);
            this.sbtnNext.LookAndFeel.SkinName = "Money Twins";
            this.sbtnNext.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnNext.Name = "sbtnNext";
            this.sbtnNext.Size = new System.Drawing.Size(60, 20);
            this.sbtnNext.TabIndex = 1;
            this.sbtnNext.Text = "下一页(&N)";
            this.sbtnNext.Click += new System.EventHandler(this.sbtnNext_Click);
            // 
            // sbtnFirst
            // 
            this.sbtnFirst.Location = new System.Drawing.Point(5, 6);
            this.sbtnFirst.LookAndFeel.SkinName = "Money Twins";
            this.sbtnFirst.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnFirst.Name = "sbtnFirst";
            this.sbtnFirst.Size = new System.Drawing.Size(60, 20);
            this.sbtnFirst.TabIndex = 0;
            this.sbtnFirst.Text = "首页(&F)";
            this.sbtnFirst.Click += new System.EventHandler(this.sbtnFirst_Click);
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.LookAndFeel.SkinName = "Blue";
            this.gridControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(688, 309);
            this.gridControl.TabIndex = 2;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            this.gridControl.DataSourceChanged += new System.EventHandler(this.gridControl_DataSourceChanged);
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.IndicatorWidth = 45;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView.OptionsFilter.AllowFilterEditor = false;
            this.gridView.OptionsFilter.AllowMRUFilterList = false;
            this.gridView.OptionsFind.AllowFindPanel = false;
            this.gridView.OptionsMenu.EnableColumnMenu = false;
            this.gridView.OptionsMenu.EnableFooterMenu = false;
            this.gridView.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView_RowClick);
            this.gridView.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView_RowCellClick);
            this.gridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);
            this.gridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView_RowStyle);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gridView_FocusedColumnChanged);
            this.gridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanged);
            this.gridView.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanging);
            this.gridView.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridView_CustomUnboundColumnData);
            this.gridView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView_CustomColumnDisplayText);
            this.gridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView_KeyDown);
            this.gridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridView_MouseUp);
            this.gridView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridView_MouseMove);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // panelControlMain
            // 
            this.panelControlMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlMain.Controls.Add(this.gridControl);
            this.panelControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlMain.Location = new System.Drawing.Point(0, 0);
            this.panelControlMain.Name = "panelControlMain";
            this.panelControlMain.Size = new System.Drawing.Size(688, 309);
            this.panelControlMain.TabIndex = 3;
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemSelect),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.KeyTip, this.btnItmAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard, "", ""),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmDelete),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.barSubItemDelete, "删除选择(&L)"),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemMove, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemTurnPage),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.btnItmRefresh, "刷新(&F)", true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmExport, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmImport)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            this.popupMenu.ShowCaption = true;
            this.popupMenu.BeforePopup += new System.ComponentModel.CancelEventHandler(this.popupMenu_BeforePopup);
            // 
            // barSubItemSelect
            // 
            this.barSubItemSelect.Caption = "选择选项(&O)";
            this.barSubItemSelect.Id = 61;
            this.barSubItemSelect.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmSelectAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmReverse),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmCancel)});
            this.barSubItemSelect.Name = "barSubItemSelect";
            this.barSubItemSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btnItmSelectAll
            // 
            this.btnItmSelectAll.Caption = "全选(&A)";
            this.btnItmSelectAll.Id = 49;
            this.btnItmSelectAll.Name = "btnItmSelectAll";
            this.btnItmSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmSelectAll_ItemClick);
            // 
            // btnItmReverse
            // 
            this.btnItmReverse.Caption = "反选(&R)";
            this.btnItmReverse.Id = 50;
            this.btnItmReverse.Name = "btnItmReverse";
            this.btnItmReverse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmReverse_ItemClick);
            // 
            // btnItmCancel
            // 
            this.btnItmCancel.Caption = "取消(&C)";
            this.btnItmCancel.Id = 51;
            this.btnItmCancel.Name = "btnItmCancel";
            this.btnItmCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmCancel_ItemClick);
            // 
            // btnItmAdd
            // 
            this.btnItmAdd.Caption = "增加(&I)";
            this.btnItmAdd.Id = 29;
            this.btnItmAdd.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.btnItmAdd.Name = "btnItmAdd";
            this.btnItmAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmAdd_ItemClick);
            // 
            // btnItmEdit
            // 
            this.btnItmEdit.Caption = "编辑(&E)";
            this.btnItmEdit.Id = 28;
            this.btnItmEdit.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this.btnItmEdit.Name = "btnItmEdit";
            this.btnItmEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmEdit_ItemClick);
            // 
            // barSubItemEdit
            // 
            this.barSubItemEdit.Caption = "编辑选项(&M)";
            this.barSubItemEdit.Id = 54;
            this.barSubItemEdit.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmBatchEdit, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmCompleteEdit)});
            this.barSubItemEdit.Name = "barSubItemEdit";
            // 
            // btnItmBatchEdit
            // 
            this.btnItmBatchEdit.Caption = "批量编辑(&B)";
            this.btnItmBatchEdit.Id = 26;
            this.btnItmBatchEdit.Name = "btnItmBatchEdit";
            this.btnItmBatchEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmBatchEdit_ItemClick);
            // 
            // btnItmCompleteEdit
            // 
            this.btnItmCompleteEdit.Caption = "全部编辑(&C)";
            this.btnItmCompleteEdit.Id = 27;
            this.btnItmCompleteEdit.Name = "btnItmCompleteEdit";
            this.btnItmCompleteEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmCompleteEdit_ItemClick);
            // 
            // btnItmDelete
            // 
            this.btnItmDelete.Caption = "删除(&D)";
            this.btnItmDelete.Id = 30;
            this.btnItmDelete.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D));
            this.btnItmDelete.Name = "btnItmDelete";
            this.btnItmDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmDelete_ItemClick);
            // 
            // barSubItemDelete
            // 
            this.barSubItemDelete.Caption = "删除选择(&R)";
            this.barSubItemDelete.Id = 56;
            this.barSubItemDelete.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmBatchDelete, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmCompleteDelete)});
            this.barSubItemDelete.Name = "barSubItemDelete";
            // 
            // btnItmBatchDelete
            // 
            this.btnItmBatchDelete.Caption = "批量删除(&B)";
            this.btnItmBatchDelete.Id = 33;
            this.btnItmBatchDelete.Name = "btnItmBatchDelete";
            this.btnItmBatchDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmBatchDelete_ItemClick);
            // 
            // btnItmCompleteDelete
            // 
            this.btnItmCompleteDelete.Caption = "完全删除(&C)";
            this.btnItmCompleteDelete.Id = 34;
            this.btnItmCompleteDelete.Name = "btnItmCompleteDelete";
            this.btnItmCompleteDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmCompleteDelete_ItemClick);
            // 
            // barSubItemMove
            // 
            this.barSubItemMove.Caption = "移动选择(&R)";
            this.barSubItemMove.Id = 57;
            this.barSubItemMove.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmTopRecord),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmPreviousRecord),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmNextRecord),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmBottomRecord)});
            this.barSubItemMove.Name = "barSubItemMove";
            // 
            // btnItmTopRecord
            // 
            this.btnItmTopRecord.Caption = "置顶(&T)";
            this.btnItmTopRecord.Id = 43;
            this.btnItmTopRecord.Name = "btnItmTopRecord";
            this.btnItmTopRecord.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmTopRecord_ItemClick);
            // 
            // btnItmPreviousRecord
            // 
            this.btnItmPreviousRecord.Caption = "向上(&P)";
            this.btnItmPreviousRecord.Id = 44;
            this.btnItmPreviousRecord.Name = "btnItmPreviousRecord";
            this.btnItmPreviousRecord.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmPreviousRecord_ItemClick);
            // 
            // btnItmNextRecord
            // 
            this.btnItmNextRecord.Caption = "向下(&N)";
            this.btnItmNextRecord.Id = 45;
            this.btnItmNextRecord.Name = "btnItmNextRecord";
            this.btnItmNextRecord.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmNextRecord_ItemClick);
            // 
            // btnItmBottomRecord
            // 
            this.btnItmBottomRecord.Caption = "置底(&B)";
            this.btnItmBottomRecord.Id = 46;
            this.btnItmBottomRecord.Name = "btnItmBottomRecord";
            this.btnItmBottomRecord.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmBottomRecord_ItemClick);
            // 
            // barSubItemTurnPage
            // 
            this.barSubItemTurnPage.Caption = "翻页选择(&T)";
            this.barSubItemTurnPage.Id = 58;
            this.barSubItemTurnPage.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmFirstPage),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmPreviousPage),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmNextPage),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmLastPage)});
            this.barSubItemTurnPage.Name = "barSubItemTurnPage";
            // 
            // btnItmFirstPage
            // 
            this.btnItmFirstPage.Caption = "首页(&F)";
            this.btnItmFirstPage.Id = 36;
            this.btnItmFirstPage.Name = "btnItmFirstPage";
            this.btnItmFirstPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmFirstPage_ItemClick);
            // 
            // btnItmPreviousPage
            // 
            this.btnItmPreviousPage.Caption = "上一页(&P)";
            this.btnItmPreviousPage.Id = 37;
            this.btnItmPreviousPage.Name = "btnItmPreviousPage";
            this.btnItmPreviousPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmPreviousPage_ItemClick);
            // 
            // btnItmNextPage
            // 
            this.btnItmNextPage.Caption = "下一页(&N)";
            this.btnItmNextPage.Id = 38;
            this.btnItmNextPage.Name = "btnItmNextPage";
            this.btnItmNextPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmNextPage_ItemClick);
            // 
            // btnItmLastPage
            // 
            this.btnItmLastPage.Caption = "尾页(&B)";
            this.btnItmLastPage.Id = 39;
            this.btnItmLastPage.Name = "btnItmLastPage";
            this.btnItmLastPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmLastPage_ItemClick);
            // 
            // btnItmRefresh
            // 
            this.btnItmRefresh.Caption = "barButtonItem1";
            this.btnItmRefresh.Id = 98;
            this.btnItmRefresh.Name = "btnItmRefresh";
            this.btnItmRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmRefresh_ItemClick);
            // 
            // btnItmExport
            // 
            this.btnItmExport.Caption = "导出Excel(&X)";
            this.btnItmExport.Id = 79;
            this.btnItmExport.Name = "btnItmExport";
            this.btnItmExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnItmExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmExport_ItemClick);
            // 
            // btnItmImport
            // 
            this.btnItmImport.Caption = "导入Excel(&I)";
            this.btnItmImport.Id = 97;
            this.btnItmImport.Name = "btnItmImport";
            this.btnItmImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmImport_ItemClick);
            // 
            // barManager
            // 
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItemEdit,
            this.btnItmBatchEdit,
            this.btnItmCompleteEdit,
            this.btnItmEdit,
            this.btnItmAdd,
            this.btnItmDelete,
            this.barSubItemDelete,
            this.btnItmBatchDelete,
            this.btnItmCompleteDelete,
            this.barSubItemTurnPage,
            this.btnItmFirstPage,
            this.btnItmPreviousPage,
            this.btnItmNextPage,
            this.btnItmLastPage,
            this.barSubItemMove,
            this.btnItmTopRecord,
            this.btnItmPreviousRecord,
            this.btnItmNextRecord,
            this.btnItmBottomRecord,
            this.btnItmSelectAll,
            this.btnItmReverse,
            this.btnItmCancel,
            this.barSubItemSelect,
            this.btnItmExport,
            this.btnItmImport,
            this.btnItmRefresh});
            this.barManager.MaxItemId = 99;
            // 
            // barAndDockingController
            // 
            this.barAndDockingController.LookAndFeel.SkinName = "Money Twins";
            this.barAndDockingController.LookAndFeel.UseDefaultLookAndFeel = false;
            this.barAndDockingController.PaintStyleName = "Skin";
            this.barAndDockingController.PropertiesBar.AllowLinkLighting = false;
            this.barAndDockingController.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.barAndDockingController.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(688, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 342);
            this.barDockControlBottom.Size = new System.Drawing.Size(688, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 342);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(688, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 342);
            // 
            // icStateItems
            // 
            this.icStateItems.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icStateItems.ImageStream")));
            this.icStateItems.Images.SetKeyName(0, "Enum_AuditedStatus_None.png");
            this.icStateItems.Images.SetKeyName(1, "Enum_AuditedStatus_Auditing.png");
            this.icStateItems.Images.SetKeyName(2, "Enum_AuditedStatus_Audited.png");
            // 
            // DevExpressGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlMain);
            this.Controls.Add(this.panelControlFooter);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "DevExpressGrid";
            this.Size = new System.Drawing.Size(688, 342);
            this.Load += new System.EventHandler(this.DevExpressGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlFooter)).EndInit();
            this.panelControlFooter.ResumeLayout(false);
            this.panelControlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPages.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icStateItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlFooter;
        private DevExpress.XtraEditors.SimpleButton sbtnFirst;
        private DevExpress.XtraEditors.SimpleButton sbtnLast;
        private DevExpress.XtraEditors.SimpleButton sbtnPrevious;
        private DevExpress.XtraEditors.SimpleButton sbtnNext;
        private DevExpress.XtraEditors.LabelControl lblFooter;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        protected DevExpress.XtraEditors.PanelControl panelControlMain;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPages;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarButtonItem btnItmAdd;
        private DevExpress.XtraBars.BarButtonItem btnItmEdit;
        private DevExpress.XtraBars.BarSubItem barSubItemEdit;
        private DevExpress.XtraBars.BarButtonItem btnItmBatchEdit;
        private DevExpress.XtraBars.BarButtonItem btnItmCompleteEdit;
        private DevExpress.XtraBars.BarButtonItem btnItmDelete;
        private DevExpress.XtraBars.BarSubItem barSubItemDelete;
        private DevExpress.XtraBars.BarButtonItem btnItmBatchDelete;
        private DevExpress.XtraBars.BarButtonItem btnItmCompleteDelete;
        private DevExpress.XtraBars.BarSubItem barSubItemTurnPage;
        private DevExpress.XtraBars.BarButtonItem btnItmFirstPage;
        private DevExpress.XtraBars.BarButtonItem btnItmPreviousPage;
        private DevExpress.XtraBars.BarButtonItem btnItmNextPage;
        private DevExpress.XtraBars.BarButtonItem btnItmLastPage;
        private DevExpress.XtraBars.BarSubItem barSubItemMove;
        private DevExpress.XtraBars.BarButtonItem btnItmTopRecord;
        private DevExpress.XtraBars.BarButtonItem btnItmPreviousRecord;
        private DevExpress.XtraBars.BarButtonItem btnItmNextRecord;
        private DevExpress.XtraBars.BarButtonItem btnItmBottomRecord;
        private DevExpress.XtraBars.BarSubItem barSubItemSelect;
        private DevExpress.XtraBars.BarButtonItem btnItmSelectAll;
        private DevExpress.XtraBars.BarButtonItem btnItmReverse;
        private DevExpress.XtraBars.BarButtonItem btnItmCancel;
        private DevExpress.XtraBars.BarButtonItem btnItmExport;
        private DevExpress.Utils.ImageCollection icStateItems;
        private DevExpress.XtraBars.BarButtonItem btnItmImport;
        private DevExpress.XtraBars.BarButtonItem btnItmRefresh;
    }
}
