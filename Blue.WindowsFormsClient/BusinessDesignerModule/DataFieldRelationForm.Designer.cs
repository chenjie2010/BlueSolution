namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class DataFieldRelationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataFieldRelationForm));
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
            this.gcDataFieldMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbDataFieldMode = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.icDataFieldMode = new DevExpress.Utils.ImageCollection(this.components);
            this.gcQuery = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFormat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCondition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.defaultBarAndDockingController = new DevExpress.XtraBars.DefaultBarAndDockingController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldMode)).BeginInit();
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiMove, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            this.barDockControlTop.Size = new System.Drawing.Size(999, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 561);
            this.barDockControlBottom.Size = new System.Drawing.Size(999, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 535);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(999, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 535);
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
            this.gcMain.Size = new System.Drawing.Size(999, 535);
            this.gcMain.TabIndex = 64;
            this.gcMain.Text = "查询字段设置";
            // 
            // gcDataFields
            // 
            this.gcDataFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDataFields.Location = new System.Drawing.Point(2, 21);
            this.gcDataFields.MainView = this.gvDataFields;
            this.gcDataFields.MenuManager = this.barManager;
            this.gcDataFields.Name = "gcDataFields";
            this.gcDataFields.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbDataFieldMode});
            this.gcDataFields.Size = new System.Drawing.Size(995, 512);
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
            this.gcDataFieldMode,
            this.gcQuery,
            this.gcFormat,
            this.gcCondition});
            this.gvDataFields.GridControl = this.gcDataFields;
            this.gvDataFields.GroupCount = 1;
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
            this.gvDataFields.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcDataFieldMode, DevExpress.Data.ColumnSortOrder.Ascending)});
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
            this.gcCategory.Width = 120;
            // 
            // gcTable
            // 
            this.gcTable.Caption = "表名";
            this.gcTable.FieldName = "TableLogicalName";
            this.gcTable.Name = "gcTable";
            this.gcTable.OptionsColumn.AllowEdit = false;
            this.gcTable.OptionsColumn.AllowMove = false;
            this.gcTable.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcTable.OptionsColumn.FixedWidth = true;
            this.gcTable.OptionsColumn.ReadOnly = true;
            this.gcTable.OptionsFilter.AllowAutoFilter = false;
            this.gcTable.Visible = true;
            this.gcTable.VisibleIndex = 2;
            this.gcTable.Width = 120;
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
            this.gcDataField.Width = 120;
            // 
            // gcDataFieldMode
            // 
            this.gcDataFieldMode.Caption = "字段模式";
            this.gcDataFieldMode.ColumnEdit = this.rcmbDataFieldMode;
            this.gcDataFieldMode.FieldName = "DataFieldMode";
            this.gcDataFieldMode.Name = "gcDataFieldMode";
            this.gcDataFieldMode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcDataFieldMode.OptionsColumn.FixedWidth = true;
            this.gcDataFieldMode.OptionsColumn.ReadOnly = true;
            this.gcDataFieldMode.OptionsFilter.AllowAutoFilter = false;
            this.gcDataFieldMode.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.gcDataFieldMode.Visible = true;
            this.gcDataFieldMode.VisibleIndex = 4;
            this.gcDataFieldMode.Width = 80;
            // 
            // rcmbDataFieldMode
            // 
            this.rcmbDataFieldMode.AutoHeight = false;
            this.rcmbDataFieldMode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbDataFieldMode.Name = "rcmbDataFieldMode";
            this.rcmbDataFieldMode.SmallImages = this.icDataFieldMode;
            // 
            // icDataFieldMode
            // 
            this.icDataFieldMode.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataFieldMode.ImageStream")));
            this.icDataFieldMode.Images.SetKeyName(0, "Common_DataFieldMode_Show_Small.png");
            this.icDataFieldMode.Images.SetKeyName(1, "Common_DataFieldMode_Group_Small.png");
            this.icDataFieldMode.Images.SetKeyName(2, "Common_DataFieldMode_Condition_Small.png");
            // 
            // gcQuery
            // 
            this.gcQuery.Caption = "查询";
            this.gcQuery.FieldName = "QueryAllowed";
            this.gcQuery.Name = "gcQuery";
            this.gcQuery.OptionsColumn.AllowMove = false;
            this.gcQuery.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcQuery.OptionsColumn.FixedWidth = true;
            this.gcQuery.OptionsColumn.ReadOnly = true;
            this.gcQuery.OptionsFilter.AllowAutoFilter = false;
            this.gcQuery.Visible = true;
            this.gcQuery.VisibleIndex = 4;
            this.gcQuery.Width = 60;
            // 
            // gcFormat
            // 
            this.gcFormat.Caption = "显示格式";
            this.gcFormat.FieldName = "DataFieldFormat";
            this.gcFormat.Name = "gcFormat";
            this.gcFormat.OptionsColumn.FixedWidth = true;
            this.gcFormat.OptionsColumn.ReadOnly = true;
            this.gcFormat.OptionsFilter.AllowAutoFilter = false;
            this.gcFormat.Visible = true;
            this.gcFormat.VisibleIndex = 5;
            this.gcFormat.Width = 160;
            // 
            // gcCondition
            // 
            this.gcCondition.Caption = "预设条件";
            this.gcCondition.FieldName = "Conditions";
            this.gcCondition.Name = "gcCondition";
            this.gcCondition.OptionsColumn.AllowMove = false;
            this.gcCondition.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcCondition.OptionsColumn.FixedWidth = true;
            this.gcCondition.OptionsColumn.ReadOnly = true;
            this.gcCondition.OptionsFilter.AllowAutoFilter = false;
            this.gcCondition.OptionsFilter.AllowFilter = false;
            this.gcCondition.Visible = true;
            this.gcCondition.VisibleIndex = 6;
            this.gcCondition.Width = 220;
            // 
            // defaultBarAndDockingController
            // 
            this.defaultBarAndDockingController.Controller.LookAndFeel.SkinName = "Money Twins";
            this.defaultBarAndDockingController.Controller.LookAndFeel.UseDefaultLookAndFeel = false;
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // DataFieldRelationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 561);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataFieldRelationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字段关系设置";
            this.Load += new System.EventHandler(this.DataFeildRelationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldMode)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn gcDataFieldMode;
        private DevExpress.XtraGrid.Columns.GridColumn gcQuery;
        private DevExpress.XtraGrid.Columns.GridColumn gcCondition;
        private DevExpress.XtraGrid.Columns.GridColumn gcFormat;
        private DevExpress.XtraGrid.Columns.GridColumn gcDataFieldId;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataFieldMode;
        private DevExpress.Utils.ImageCollection icDataFieldMode;
    }
}