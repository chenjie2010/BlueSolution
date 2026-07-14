namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class WorkflowMapForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkflowMapForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnItmAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmDelete = new DevExpress.XtraBars.BarButtonItem();
            this.blcSorting = new DevExpress.XtraBars.BarLinkContainerItem();
            this.btnItmTop = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmPrevious = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmNext = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmBottom = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcProcess = new DevExpress.XtraGrid.GridControl();
            this.gvProcess = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcParentProcessId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcProcessId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcParentProcessName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcProcessName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.cmbWorkflowNodes = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkflowNodes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.icTools;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItmAdd,
            this.btnItmDelete,
            this.blcSorting,
            this.btnItmTop,
            this.btnItmPrevious,
            this.btnItmNext,
            this.btnItmBottom});
            this.barManager.MaxItemId = 9;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blcSorting, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnItmAdd
            // 
            this.btnItmAdd.Caption = "增加(&A)";
            this.btnItmAdd.Id = 1;
            this.btnItmAdd.ImageIndex = 0;
            this.btnItmAdd.Name = "btnItmAdd";
            this.btnItmAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmAdd_ItemClick);
            // 
            // btnItmDelete
            // 
            this.btnItmDelete.Caption = "删除(D)";
            this.btnItmDelete.Id = 3;
            this.btnItmDelete.ImageIndex = 2;
            this.btnItmDelete.Name = "btnItmDelete";
            this.btnItmDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmDelete_ItemClick);
            // 
            // blcSorting
            // 
            this.blcSorting.Caption = "排序(&Q)";
            this.blcSorting.Id = 4;
            this.blcSorting.ImageIndex = 3;
            this.blcSorting.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmTop),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmPrevious),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmNext),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmBottom)});
            this.blcSorting.Name = "blcSorting";
            // 
            // btnItmTop
            // 
            this.btnItmTop.Caption = "置顶(&T)";
            this.btnItmTop.Id = 5;
            this.btnItmTop.ImageIndex = 4;
            this.btnItmTop.Name = "btnItmTop";
            this.btnItmTop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmTop_ItemClick);
            // 
            // btnItmPrevious
            // 
            this.btnItmPrevious.Caption = "上移(&P)";
            this.btnItmPrevious.Id = 6;
            this.btnItmPrevious.ImageIndex = 5;
            this.btnItmPrevious.Name = "btnItmPrevious";
            this.btnItmPrevious.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmPrevious_ItemClick);
            // 
            // btnItmNext
            // 
            this.btnItmNext.Caption = "下移(&N)";
            this.btnItmNext.Id = 7;
            this.btnItmNext.ImageIndex = 6;
            this.btnItmNext.Name = "btnItmNext";
            this.btnItmNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmNext_ItemClick);
            // 
            // btnItmBottom
            // 
            this.btnItmBottom.Caption = "置底(&B)";
            this.btnItmBottom.Id = 8;
            this.btnItmBottom.ImageIndex = 7;
            this.btnItmBottom.Name = "btnItmBottom";
            this.btnItmBottom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmBottom_ItemClick);
            // 
            // barAndDockingController
            // 
            this.barAndDockingController.LookAndFeel.SkinName = "Money Twins";
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
            this.barDockControlTop.Size = new System.Drawing.Size(499, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 363);
            this.barDockControlBottom.Size = new System.Drawing.Size(499, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 337);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(499, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 337);
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "Button_Add.png");
            this.icTools.Images.SetKeyName(1, "Tools_Edit.png");
            this.icTools.Images.SetKeyName(2, "Tools_Delete.png");
            this.icTools.Images.SetKeyName(3, "Common_Sorting.png");
            this.icTools.Images.SetKeyName(4, "Common_Arrow_Top.png");
            this.icTools.Images.SetKeyName(5, "Common_Arrow_Up.png");
            this.icTools.Images.SetKeyName(6, "Common_Arrow_Down.png");
            this.icTools.Images.SetKeyName(7, "Common_Arrow_Bottom.png");
            this.icTools.Images.SetKeyName(8, "Button_Update.png");
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.groupControl1);
            this.pnlMain.Controls.Add(this.panelControl1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 26);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(499, 337);
            this.pnlMain.TabIndex = 4;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcProcess);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 51);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(495, 284);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "工作流程节点关系";
            // 
            // gcProcess
            // 
            this.gcProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcProcess.Location = new System.Drawing.Point(2, 21);
            this.gcProcess.MainView = this.gvProcess;
            this.gcProcess.Name = "gcProcess";
            this.gcProcess.Size = new System.Drawing.Size(491, 261);
            this.gcProcess.TabIndex = 3;
            this.gcProcess.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProcess});
            // 
            // gvProcess
            // 
            this.gvProcess.Appearance.FocusedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gvProcess.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvProcess.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvProcess.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvProcess.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gvProcess.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvProcess.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcParentProcessId,
            this.gcProcessId,
            this.gcParentProcessName,
            this.gcProcessName});
            this.gvProcess.GridControl = this.gcProcess;
            this.gvProcess.GroupCount = 1;
            this.gvProcess.IndicatorWidth = 45;
            this.gvProcess.Name = "gvProcess";
            this.gvProcess.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvProcess.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvProcess.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvProcess.OptionsBehavior.AutoPopulateColumns = false;
            this.gvProcess.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gvProcess.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gvProcess.OptionsBehavior.Editable = false;
            this.gvProcess.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvProcess.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gvProcess.OptionsBehavior.ReadOnly = true;
            this.gvProcess.OptionsCustomization.AllowColumnMoving = false;
            this.gvProcess.OptionsCustomization.AllowFilter = false;
            this.gvProcess.OptionsCustomization.AllowSort = false;
            this.gvProcess.OptionsFind.AllowFindPanel = false;
            this.gvProcess.OptionsFind.ShowClearButton = false;
            this.gvProcess.OptionsFind.ShowCloseButton = false;
            this.gvProcess.OptionsFind.ShowFindButton = false;
            this.gvProcess.OptionsMenu.EnableColumnMenu = false;
            this.gvProcess.OptionsMenu.EnableFooterMenu = false;
            this.gvProcess.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvProcess.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gvProcess.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gvProcess.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gvProcess.OptionsMenu.ShowSplitItem = false;
            this.gvProcess.OptionsView.ShowGroupPanel = false;
            this.gvProcess.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcParentProcessName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvProcess.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvProcess_CustomDrawRowIndicator);
            // 
            // gcParentProcessId
            // 
            this.gcParentProcessId.Caption = "父节点编号";
            this.gcParentProcessId.FieldName = "ParentProcessId";
            this.gcParentProcessId.Name = "gcParentProcessId";
            // 
            // gcProcessId
            // 
            this.gcProcessId.Caption = "子节点编号";
            this.gcProcessId.FieldName = "ProcessId";
            this.gcProcessId.Name = "gcProcessId";
            // 
            // gcParentProcessName
            // 
            this.gcParentProcessName.Caption = "父节点名称";
            this.gcParentProcessName.FieldName = "ParentProcessName";
            this.gcParentProcessName.Name = "gcParentProcessName";
            this.gcParentProcessName.OptionsColumn.AllowEdit = false;
            this.gcParentProcessName.OptionsColumn.AllowMove = false;
            this.gcParentProcessName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcParentProcessName.OptionsColumn.ReadOnly = true;
            this.gcParentProcessName.OptionsFilter.AllowAutoFilter = false;
            this.gcParentProcessName.Visible = true;
            this.gcParentProcessName.VisibleIndex = 0;
            this.gcParentProcessName.Width = 150;
            // 
            // gcProcessName
            // 
            this.gcProcessName.Caption = "子节点名称";
            this.gcProcessName.FieldName = "ProcessName";
            this.gcProcessName.Name = "gcProcessName";
            this.gcProcessName.OptionsFilter.AllowAutoFilter = false;
            this.gcProcessName.Visible = true;
            this.gcProcessName.VisibleIndex = 0;
            this.gcProcessName.Width = 150;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnConfirm);
            this.panelControl1.Controls.Add(this.cmbWorkflowNodes);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(495, 49);
            this.panelControl1.TabIndex = 5;
            // 
            // btnConfirm
            // 
            this.btnConfirm.ImageIndex = 8;
            this.btnConfirm.ImageList = this.icTools;
            this.btnConfirm.Location = new System.Drawing.Point(408, 13);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(79, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确定(&Q)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // cmbWorkflowNodes
            // 
            this.cmbWorkflowNodes.Location = new System.Drawing.Point(96, 15);
            this.cmbWorkflowNodes.MenuManager = this.barManager;
            this.cmbWorkflowNodes.Name = "cmbWorkflowNodes";
            this.cmbWorkflowNodes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWorkflowNodes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbWorkflowNodes.Size = new System.Drawing.Size(303, 20);
            this.cmbWorkflowNodes.TabIndex = 1;
            this.cmbWorkflowNodes.SelectedIndexChanged += new System.EventHandler(this.cmbWorkflowNodes_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(7, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "工作流根节点：";
            // 
            // WorkflowMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 363);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WorkflowMapForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工作流程关系设计";
            this.Load += new System.EventHandler(this.WorkflowMapForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWorkflowNodes.Properties)).EndInit();
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
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarButtonItem btnItmAdd;
        private DevExpress.XtraBars.BarButtonItem btnItmDelete;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraGrid.GridControl gcProcess;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProcess;
        private DevExpress.XtraGrid.Columns.GridColumn gcParentProcessId;
        private DevExpress.XtraGrid.Columns.GridColumn gcProcessId;
        private DevExpress.XtraGrid.Columns.GridColumn gcParentProcessName;
        private DevExpress.XtraGrid.Columns.GridColumn gcProcessName;
        private DevExpress.XtraBars.BarLinkContainerItem blcSorting;
        private DevExpress.XtraBars.BarButtonItem btnItmTop;
        private DevExpress.XtraBars.BarButtonItem btnItmPrevious;
        private DevExpress.XtraBars.BarButtonItem btnItmNext;
        private DevExpress.XtraBars.BarButtonItem btnItmBottom;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbWorkflowNodes;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
    }
}