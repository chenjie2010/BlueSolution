namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class WorkflowDataFieldForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkflowDataFieldForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiAdd = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEdit = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMove = new DevExpress.XtraBars.BarLinkContainerItem();
            this.bbiTop = new DevExpress.XtraBars.BarButtonItem();
            this.bbiPrevious = new DevExpress.XtraBars.BarButtonItem();
            this.bbiNext = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBottom = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiVerify = new DevExpress.XtraBars.BarButtonItem();
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.gcDataFields = new DevExpress.XtraGrid.GridControl();
            this.gvDataFields = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDataFieldId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDatabase = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDataField = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFstCondition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbDataFieldCondition = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.icDataFieldCondition = new DevExpress.Utils.ImageCollection(this.components);
            this.gcFstValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcScdCondition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcScdValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNextRelation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbNextRelation = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.icNextRelation = new DevExpress.Utils.ImageCollection(this.components);
            this.defaultBarAndDockingController = new DevExpress.XtraBars.DefaultBarAndDockingController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldCondition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbNextRelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icNextRelation)).BeginInit();
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
            this.bbiClose,
            this.bbiAdd,
            this.bbiDelete,
            this.bbiEdit,
            this.bbiMove,
            this.bbiPrevious,
            this.bbiNext,
            this.bbiBottom,
            this.bbiTop});
            this.barManager.MaxItemId = 13;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiAdd, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiMove, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bbiAdd
            // 
            this.bbiAdd.Caption = "增加(&A)";
            this.bbiAdd.Id = 3;
            this.bbiAdd.ImageIndex = 0;
            this.bbiAdd.Name = "bbiAdd";
            this.bbiAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAdd_ItemClick);
            // 
            // bbiEdit
            // 
            this.bbiEdit.Caption = "编辑(&E)";
            this.bbiEdit.Id = 5;
            this.bbiEdit.ImageIndex = 1;
            this.bbiEdit.Name = "bbiEdit";
            this.bbiEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEdit_ItemClick);
            // 
            // bbiDelete
            // 
            this.bbiDelete.Caption = "删除(&D)";
            this.bbiDelete.Id = 4;
            this.bbiDelete.ImageIndex = 2;
            this.bbiDelete.Name = "bbiDelete";
            this.bbiDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDelete_ItemClick);
            // 
            // bbiMove
            // 
            this.bbiMove.Caption = "移动(&M)";
            this.bbiMove.Id = 6;
            this.bbiMove.ImageIndex = 3;
            this.bbiMove.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiTop),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiPrevious),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiNext),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiBottom)});
            this.bbiMove.Name = "bbiMove";
            // 
            // bbiTop
            // 
            this.bbiTop.Caption = "置顶(&T)";
            this.bbiTop.Id = 10;
            this.bbiTop.ImageIndex = 4;
            this.bbiTop.Name = "bbiTop";
            this.bbiTop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiTop_ItemClick);
            // 
            // bbiPrevious
            // 
            this.bbiPrevious.Caption = "上移(&P)";
            this.bbiPrevious.Id = 7;
            this.bbiPrevious.ImageIndex = 5;
            this.bbiPrevious.Name = "bbiPrevious";
            this.bbiPrevious.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPrevious_ItemClick);
            // 
            // bbiNext
            // 
            this.bbiNext.Caption = "下移(&N)";
            this.bbiNext.Id = 8;
            this.bbiNext.ImageIndex = 6;
            this.bbiNext.Name = "bbiNext";
            this.bbiNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNext_ItemClick);
            // 
            // bbiBottom
            // 
            this.bbiBottom.Caption = "置底(&B)";
            this.bbiBottom.Id = 9;
            this.bbiBottom.ImageIndex = 7;
            this.bbiBottom.Name = "bbiBottom";
            this.bbiBottom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiBottom_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.Caption = "关闭(&C)";
            this.bbiClose.Id = 2;
            this.bbiClose.ImageIndex = 8;
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1029, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 333);
            this.barDockControlBottom.Size = new System.Drawing.Size(1029, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 307);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1029, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 307);
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "Tools_Add.png");
            this.icTools.Images.SetKeyName(1, "Tools_Edit.png");
            this.icTools.Images.SetKeyName(2, "Common_Remove_1.png");
            this.icTools.Images.SetKeyName(3, "Common_Move.png");
            this.icTools.Images.SetKeyName(4, "Common_Arrow_Top.png");
            this.icTools.Images.SetKeyName(5, "Common_Arrow_Up.png");
            this.icTools.Images.SetKeyName(6, "Common_Arrow_Down.png");
            this.icTools.Images.SetKeyName(7, "Common_Arrow_Bottom.png");
            this.icTools.Images.SetKeyName(8, "Common_Close_2.png");
            // 
            // bbiSave
            // 
            this.bbiSave.Id = 11;
            this.bbiSave.Name = "bbiSave";
            // 
            // bbiVerify
            // 
            this.bbiVerify.Id = 12;
            this.bbiVerify.Name = "bbiVerify";
            // 
            // gcMain
            // 
            this.gcMain.Controls.Add(this.gcDataFields);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 26);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(1029, 307);
            this.gcMain.TabIndex = 64;
            this.gcMain.Text = "字段条件列表";
            // 
            // gcDataFields
            // 
            this.gcDataFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDataFields.Location = new System.Drawing.Point(2, 21);
            this.gcDataFields.MainView = this.gvDataFields;
            this.gcDataFields.MenuManager = this.barManager;
            this.gcDataFields.Name = "gcDataFields";
            this.gcDataFields.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbDataFieldCondition,
            this.rcmbNextRelation});
            this.gcDataFields.Size = new System.Drawing.Size(1025, 284);
            this.gcDataFields.TabIndex = 0;
            this.gcDataFields.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDataFields});
            // 
            // gvDataFields
            // 
            this.gvDataFields.Appearance.FocusedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gvDataFields.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvDataFields.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvDataFields.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvDataFields.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gvDataFields.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvDataFields.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDataFieldId,
            this.gcDatabase,
            this.gcCategory,
            this.gcTable,
            this.gcDataField,
            this.gcFstCondition,
            this.gcFstValue,
            this.gcScdCondition,
            this.gcScdValue,
            this.gcNextRelation});
            this.gvDataFields.GridControl = this.gcDataFields;
            this.gvDataFields.IndicatorWidth = 25;
            this.gvDataFields.Name = "gvDataFields";
            this.gvDataFields.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvDataFields.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvDataFields.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvDataFields.OptionsBehavior.AutoPopulateColumns = false;
            this.gvDataFields.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gvDataFields.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gvDataFields.OptionsBehavior.Editable = false;
            this.gvDataFields.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvDataFields.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gvDataFields.OptionsBehavior.ReadOnly = true;
            this.gvDataFields.OptionsCustomization.AllowColumnMoving = false;
            this.gvDataFields.OptionsCustomization.AllowFilter = false;
            this.gvDataFields.OptionsCustomization.AllowSort = false;
            this.gvDataFields.OptionsFind.AllowFindPanel = false;
            this.gvDataFields.OptionsFind.ShowClearButton = false;
            this.gvDataFields.OptionsFind.ShowCloseButton = false;
            this.gvDataFields.OptionsFind.ShowFindButton = false;
            this.gvDataFields.OptionsMenu.EnableColumnMenu = false;
            this.gvDataFields.OptionsMenu.EnableFooterMenu = false;
            this.gvDataFields.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvDataFields.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gvDataFields.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gvDataFields.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gvDataFields.OptionsMenu.ShowSplitItem = false;
            this.gvDataFields.OptionsView.ColumnAutoWidth = false;
            this.gvDataFields.OptionsView.ShowGroupPanel = false;
            this.gvDataFields.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvDataFields_CustomDrawRowIndicator);
            // 
            // gcDataFieldId
            // 
            this.gcDataFieldId.Caption = "字段编号";
            this.gcDataFieldId.FieldName = "DataFieldId";
            this.gcDataFieldId.Name = "gcDataFieldId";
            // 
            // gcDatabase
            // 
            this.gcDatabase.Caption = "数据库名称";
            this.gcDatabase.FieldName = "DatabaseName";
            this.gcDatabase.Name = "gcDatabase";
            this.gcDatabase.OptionsColumn.AllowEdit = false;
            this.gcDatabase.OptionsColumn.AllowMove = false;
            this.gcDatabase.OptionsColumn.FixedWidth = true;
            this.gcDatabase.OptionsColumn.ReadOnly = true;
            this.gcDatabase.OptionsFilter.AllowAutoFilter = false;
            this.gcDatabase.Visible = true;
            this.gcDatabase.VisibleIndex = 0;
            this.gcDatabase.Width = 100;
            // 
            // gcCategory
            // 
            this.gcCategory.Caption = "分组名称";
            this.gcCategory.FieldName = "CategoryName";
            this.gcCategory.Name = "gcCategory";
            this.gcCategory.OptionsColumn.AllowEdit = false;
            this.gcCategory.OptionsColumn.AllowMove = false;
            this.gcCategory.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcCategory.OptionsColumn.FixedWidth = true;
            this.gcCategory.OptionsColumn.ReadOnly = true;
            this.gcCategory.OptionsFilter.AllowAutoFilter = false;
            this.gcCategory.Visible = true;
            this.gcCategory.VisibleIndex = 1;
            this.gcCategory.Width = 100;
            // 
            // gcTable
            // 
            this.gcTable.Caption = "表名";
            this.gcTable.FieldName = "LogicalName";
            this.gcTable.Name = "gcTable";
            this.gcTable.OptionsColumn.AllowEdit = false;
            this.gcTable.OptionsColumn.AllowMove = false;
            this.gcTable.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcTable.OptionsColumn.FixedWidth = true;
            this.gcTable.OptionsColumn.ReadOnly = true;
            this.gcTable.OptionsFilter.AllowAutoFilter = false;
            this.gcTable.Visible = true;
            this.gcTable.VisibleIndex = 2;
            this.gcTable.Width = 100;
            // 
            // gcDataField
            // 
            this.gcDataField.Caption = "字段名";
            this.gcDataField.FieldName = "DataFieldLogicalName";
            this.gcDataField.Name = "gcDataField";
            this.gcDataField.OptionsColumn.AllowEdit = false;
            this.gcDataField.OptionsColumn.AllowMove = false;
            this.gcDataField.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcDataField.OptionsColumn.FixedWidth = true;
            this.gcDataField.OptionsColumn.ReadOnly = true;
            this.gcDataField.OptionsFilter.AllowAutoFilter = false;
            this.gcDataField.Visible = true;
            this.gcDataField.VisibleIndex = 3;
            this.gcDataField.Width = 100;
            // 
            // gcFstCondition
            // 
            this.gcFstCondition.Caption = "条件模式一";
            this.gcFstCondition.ColumnEdit = this.rcmbDataFieldCondition;
            this.gcFstCondition.FieldName = "FstCondition";
            this.gcFstCondition.Name = "gcFstCondition";
            this.gcFstCondition.OptionsColumn.AllowEdit = false;
            this.gcFstCondition.OptionsColumn.AllowMove = false;
            this.gcFstCondition.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcFstCondition.OptionsColumn.FixedWidth = true;
            this.gcFstCondition.OptionsColumn.ReadOnly = true;
            this.gcFstCondition.OptionsFilter.AllowAutoFilter = false;
            this.gcFstCondition.Visible = true;
            this.gcFstCondition.VisibleIndex = 4;
            this.gcFstCondition.Width = 100;
            // 
            // rcmbDataFieldCondition
            // 
            this.rcmbDataFieldCondition.AutoHeight = false;
            this.rcmbDataFieldCondition.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbDataFieldCondition.Name = "rcmbDataFieldCondition";
            this.rcmbDataFieldCondition.ReadOnly = true;
            this.rcmbDataFieldCondition.SmallImages = this.icDataFieldCondition;
            // 
            // icDataFieldCondition
            // 
            this.icDataFieldCondition.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataFieldCondition.ImageStream")));
            this.icDataFieldCondition.Images.SetKeyName(0, "Enum_DataFieldCondition_None.png");
            this.icDataFieldCondition.Images.SetKeyName(1, "Enum_DataFieldCondition_More.png");
            this.icDataFieldCondition.Images.SetKeyName(2, "Enum_DataFieldCondition_MoreOrEqual.png");
            this.icDataFieldCondition.Images.SetKeyName(3, "Enum_DataFieldCondition_Less.png");
            this.icDataFieldCondition.Images.SetKeyName(4, "Enum_DataFieldCondition_LessOrEqual.png");
            this.icDataFieldCondition.Images.SetKeyName(5, "Enum_DataFieldCondition_Equal.png");
            this.icDataFieldCondition.Images.SetKeyName(6, "Enum_DataFieldCondition_Like.png");
            this.icDataFieldCondition.Images.SetKeyName(7, "Enum_DataFieldCondition_StartWith.png");
            this.icDataFieldCondition.Images.SetKeyName(8, "Enum_DataFieldCondition_Not.png");
            // 
            // gcFstValue
            // 
            this.gcFstValue.Caption = "条件值一";
            this.gcFstValue.FieldName = "FstConditionValue";
            this.gcFstValue.Name = "gcFstValue";
            this.gcFstValue.OptionsColumn.AllowEdit = false;
            this.gcFstValue.OptionsColumn.FixedWidth = true;
            this.gcFstValue.OptionsColumn.ReadOnly = true;
            this.gcFstValue.OptionsFilter.AllowAutoFilter = false;
            this.gcFstValue.Visible = true;
            this.gcFstValue.VisibleIndex = 5;
            this.gcFstValue.Width = 150;
            // 
            // gcScdCondition
            // 
            this.gcScdCondition.Caption = "条件模式二";
            this.gcScdCondition.ColumnEdit = this.rcmbDataFieldCondition;
            this.gcScdCondition.FieldName = "ScdCondition";
            this.gcScdCondition.Name = "gcScdCondition";
            this.gcScdCondition.OptionsColumn.AllowEdit = false;
            this.gcScdCondition.OptionsColumn.AllowMove = false;
            this.gcScdCondition.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcScdCondition.OptionsColumn.FixedWidth = true;
            this.gcScdCondition.OptionsColumn.ReadOnly = true;
            this.gcScdCondition.OptionsFilter.AllowAutoFilter = false;
            this.gcScdCondition.OptionsFilter.AllowFilter = false;
            this.gcScdCondition.Visible = true;
            this.gcScdCondition.VisibleIndex = 6;
            this.gcScdCondition.Width = 100;
            // 
            // gcScdValue
            // 
            this.gcScdValue.Caption = "条件值二";
            this.gcScdValue.FieldName = "ScdConditionValue";
            this.gcScdValue.Name = "gcScdValue";
            this.gcScdValue.OptionsColumn.AllowEdit = false;
            this.gcScdValue.OptionsColumn.AllowMove = false;
            this.gcScdValue.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcScdValue.OptionsColumn.FixedWidth = true;
            this.gcScdValue.OptionsColumn.ReadOnly = true;
            this.gcScdValue.Visible = true;
            this.gcScdValue.VisibleIndex = 7;
            this.gcScdValue.Width = 150;
            // 
            // gcNextRelation
            // 
            this.gcNextRelation.Caption = "与下一字段关系";
            this.gcNextRelation.ColumnEdit = this.rcmbNextRelation;
            this.gcNextRelation.FieldName = "NextRelation";
            this.gcNextRelation.Name = "gcNextRelation";
            this.gcNextRelation.OptionsColumn.AllowEdit = false;
            this.gcNextRelation.OptionsColumn.AllowMove = false;
            this.gcNextRelation.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcNextRelation.OptionsColumn.FixedWidth = true;
            this.gcNextRelation.OptionsColumn.ReadOnly = true;
            this.gcNextRelation.Visible = true;
            this.gcNextRelation.VisibleIndex = 8;
            this.gcNextRelation.Width = 95;
            // 
            // rcmbNextRelation
            // 
            this.rcmbNextRelation.AutoHeight = false;
            this.rcmbNextRelation.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbNextRelation.Name = "rcmbNextRelation";
            this.rcmbNextRelation.SmallImages = this.icNextRelation;
            // 
            // icNextRelation
            // 
            this.icNextRelation.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icNextRelation.ImageStream")));
            this.icNextRelation.Images.SetKeyName(0, "Enum_NextTableRelation_And.png");
            this.icNextRelation.Images.SetKeyName(1, "Enum_NextTableRelation_Or.png");
            this.icNextRelation.Images.SetKeyName(2, "Enum_NextTableRelation_None.png");
            // 
            // defaultBarAndDockingController
            // 
            this.defaultBarAndDockingController.Controller.LookAndFeel.SkinName = "Money Twins";
            this.defaultBarAndDockingController.Controller.LookAndFeel.UseDefaultLookAndFeel = false;
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // WorkflowDataFieldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 333);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WorkflowDataFieldForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工作流程字段条件设置";
            this.Load += new System.EventHandler(this.WorkflowDataFieldForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldCondition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbNextRelation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icNextRelation)).EndInit();
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
        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraBars.DefaultBarAndDockingController defaultBarAndDockingController;
        private DevExpress.XtraGrid.GridControl gcDataFields;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDataFields;
        private DevExpress.XtraBars.BarButtonItem bbiAdd;
        private DevExpress.XtraBars.BarButtonItem bbiDelete;
        private DevExpress.XtraBars.BarButtonItem bbiEdit;
        private DevExpress.XtraBars.BarLinkContainerItem bbiMove;
        private DevExpress.XtraBars.BarButtonItem bbiTop;
        private DevExpress.XtraBars.BarButtonItem bbiPrevious;
        private DevExpress.XtraBars.BarButtonItem bbiNext;
        private DevExpress.XtraBars.BarButtonItem bbiBottom;
        private DevExpress.XtraGrid.Columns.GridColumn gcDatabase;
        private DevExpress.XtraGrid.Columns.GridColumn gcCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gcTable;
        private DevExpress.XtraGrid.Columns.GridColumn gcDataField;
        private DevExpress.XtraGrid.Columns.GridColumn gcFstCondition;
        private DevExpress.XtraGrid.Columns.GridColumn gcScdCondition;
        private DevExpress.XtraGrid.Columns.GridColumn gcFstValue;
        private DevExpress.XtraGrid.Columns.GridColumn gcDataFieldId;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataFieldCondition;
        private DevExpress.XtraGrid.Columns.GridColumn gcScdValue;
        private DevExpress.XtraGrid.Columns.GridColumn gcNextRelation;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbNextRelation;
        private DevExpress.Utils.ImageCollection icNextRelation;
        private DevExpress.Utils.ImageCollection icDataFieldCondition;
    }
}