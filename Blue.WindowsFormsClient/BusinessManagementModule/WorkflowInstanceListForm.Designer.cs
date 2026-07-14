namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    partial class WorkflowInstanceListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkflowInstanceListForm));
            this.gcWorkflow = new DevExpress.XtraEditors.GroupControl();
            this.devWorkflow = new AppFramework.WinFormsControls.DevExpressGrid();
            this.pnlQuery = new DevExpress.XtraEditors.PanelControl();
            this.btxtWorkflow = new DevExpress.XtraEditors.ButtonEdit();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiView = new DevExpress.XtraBars.BarButtonItem();
            this.bbInit = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAbort = new DevExpress.XtraBars.BarButtonItem();
            this.bbiArchiveOperation = new DevExpress.XtraBars.BarLinkContainerItem();
            this.bbiArchive = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBatchArchive = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAllArchives = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCancleArchive = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBatchCancelArchive = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCancelAllArchives = new DevExpress.XtraBars.BarButtonItem();
            this.bbiViewLog = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCancel = new DevExpress.XtraBars.BarButtonItem();
            this.lblWorkflow = new DevExpress.XtraEditors.LabelControl();
            this.ceArchived = new DevExpress.XtraEditors.CheckEdit();
            this.ccmbInstanceState = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblInstanceState = new DevExpress.XtraEditors.LabelControl();
            this.dtStart = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.lblTimeSumbitted = new DevExpress.XtraEditors.LabelControl();
            this.dtEnd = new DevExpress.XtraEditors.DateEdit();
            this.txtInstanceName = new DevExpress.XtraEditors.TextEdit();
            this.hlnkClear = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.lblInstanceName = new DevExpress.XtraEditors.LabelControl();
            this.barTools = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkflow)).BeginInit();
            this.gcWorkflow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlQuery)).BeginInit();
            this.pnlQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btxtWorkflow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceArchived.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbInstanceState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcWorkflow
            // 
            this.gcWorkflow.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.MyWork_Main_Head1;
            this.gcWorkflow.Controls.Add(this.devWorkflow);
            this.gcWorkflow.Controls.Add(this.pnlQuery);
            this.gcWorkflow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcWorkflow.Location = new System.Drawing.Point(0, 26);
            this.gcWorkflow.Name = "gcWorkflow";
            this.gcWorkflow.Size = new System.Drawing.Size(1169, 560);
            this.gcWorkflow.TabIndex = 4;
            this.gcWorkflow.Text = "待处理工作流";
            // 
            // devWorkflow
            // 
            this.devWorkflow.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devWorkflow.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devWorkflow.CheckboxColumnCaption = null;
            this.devWorkflow.ColumnHeaderTexts = new string[0];
            this.devWorkflow.DataKeyNames = new string[] {
        "InstanceId"};
            this.devWorkflow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devWorkflow.ExportedExcel = false;
            this.devWorkflow.FootText = null;
            this.devWorkflow.IsMainTable = false;
            this.devWorkflow.Location = new System.Drawing.Point(2, 93);
            this.devWorkflow.Name = "devWorkflow";
            this.devWorkflow.PageSize = 50;
            this.devWorkflow.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devWorkflow.Size = new System.Drawing.Size(1165, 465);
            this.devWorkflow.TabIndex = 10;
            this.devWorkflow.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.devWorkflow_OnPageIndexChanged);
            this.devWorkflow.OnCustomColumnDisplayText += new System.EventHandler<DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs>(this.devWorkflow_OnCustomColumnDisplayText);
            // 
            // pnlQuery
            // 
            this.pnlQuery.Controls.Add(this.btxtWorkflow);
            this.pnlQuery.Controls.Add(this.lblWorkflow);
            this.pnlQuery.Controls.Add(this.ceArchived);
            this.pnlQuery.Controls.Add(this.ccmbInstanceState);
            this.pnlQuery.Controls.Add(this.lblInstanceState);
            this.pnlQuery.Controls.Add(this.dtStart);
            this.pnlQuery.Controls.Add(this.lblTo);
            this.pnlQuery.Controls.Add(this.lblTimeSumbitted);
            this.pnlQuery.Controls.Add(this.dtEnd);
            this.pnlQuery.Controls.Add(this.txtInstanceName);
            this.pnlQuery.Controls.Add(this.hlnkClear);
            this.pnlQuery.Controls.Add(this.btnQuery);
            this.pnlQuery.Controls.Add(this.lblInstanceName);
            this.pnlQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlQuery.Location = new System.Drawing.Point(2, 23);
            this.pnlQuery.Name = "pnlQuery";
            this.pnlQuery.Size = new System.Drawing.Size(1165, 70);
            this.pnlQuery.TabIndex = 1;
            // 
            // btxtWorkflow
            // 
            this.btxtWorkflow.Location = new System.Drawing.Point(583, 11);
            this.btxtWorkflow.MenuManager = this.barManager;
            this.btxtWorkflow.Name = "btxtWorkflow";
            this.btxtWorkflow.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtWorkflow.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtWorkflow.Size = new System.Drawing.Size(408, 20);
            this.btxtWorkflow.TabIndex = 40;
            this.btxtWorkflow.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtWorkflow_ButtonPressed);
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
            this.bbiSave,
            this.bbiCancel,
            this.bbiAbort,
            this.bbiViewLog,
            this.bbiView,
            this.bbInit,
            this.bbiArchiveOperation,
            this.bbiArchive,
            this.bbiBatchArchive,
            this.bbiAllArchives,
            this.bbiCancleArchive,
            this.bbiBatchCancelArchive,
            this.bbiCancelAllArchives});
            this.barManager.MaxItemId = 13;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiView, "", false, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbInit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiAbort, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiArchiveOperation, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiViewLog, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.RotateWhenVertical = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bbiView
            // 
            this.bbiView.Caption = "查看(&V)";
            this.bbiView.Id = 4;
            this.bbiView.ImageIndex = 0;
            this.bbiView.Name = "bbiView";
            this.bbiView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiView_ItemClick);
            // 
            // bbInit
            // 
            this.bbInit.Caption = "初始化(&I)";
            this.bbInit.Id = 5;
            this.bbInit.ImageIndex = 1;
            this.bbInit.Name = "bbInit";
            this.bbInit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbInit_ItemClick);
            // 
            // bbiAbort
            // 
            this.bbiAbort.Caption = "终止(&B)";
            this.bbiAbort.Id = 2;
            this.bbiAbort.ImageIndex = 2;
            this.bbiAbort.Name = "bbiAbort";
            this.bbiAbort.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAbort_ItemClick);
            // 
            // bbiArchiveOperation
            // 
            this.bbiArchiveOperation.Caption = "归档操作...(&A)";
            this.bbiArchiveOperation.Id = 6;
            this.bbiArchiveOperation.ImageIndex = 3;
            this.bbiArchiveOperation.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiArchive),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiBatchArchive),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAllArchives),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCancleArchive, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiBatchCancelArchive),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCancelAllArchives)});
            this.bbiArchiveOperation.Name = "bbiArchiveOperation";
            // 
            // bbiArchive
            // 
            this.bbiArchive.Caption = "归档(&W)";
            this.bbiArchive.Id = 7;
            this.bbiArchive.ImageIndex = 5;
            this.bbiArchive.Name = "bbiArchive";
            this.bbiArchive.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiArchive_ItemClick);
            // 
            // bbiBatchArchive
            // 
            this.bbiBatchArchive.Caption = "批量归档(&B)";
            this.bbiBatchArchive.Id = 8;
            this.bbiBatchArchive.ImageIndex = 6;
            this.bbiBatchArchive.Name = "bbiBatchArchive";
            this.bbiBatchArchive.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiBatchArchive_ItemClick);
            // 
            // bbiAllArchives
            // 
            this.bbiAllArchives.Caption = "全部归档(&L)";
            this.bbiAllArchives.Id = 9;
            this.bbiAllArchives.ImageIndex = 7;
            this.bbiAllArchives.Name = "bbiAllArchives";
            this.bbiAllArchives.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAllArchives_ItemClick);
            // 
            // bbiCancleArchive
            // 
            this.bbiCancleArchive.Caption = "取消归档(&C)";
            this.bbiCancleArchive.Id = 10;
            this.bbiCancleArchive.ImageIndex = 8;
            this.bbiCancleArchive.Name = "bbiCancleArchive";
            this.bbiCancleArchive.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCancleArchive_ItemClick);
            // 
            // bbiBatchCancelArchive
            // 
            this.bbiBatchCancelArchive.Caption = "批量取消归档(&E)";
            this.bbiBatchCancelArchive.Id = 11;
            this.bbiBatchCancelArchive.ImageIndex = 9;
            this.bbiBatchCancelArchive.Name = "bbiBatchCancelArchive";
            this.bbiBatchCancelArchive.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiBatchCancelArchive_ItemClick);
            // 
            // bbiCancelAllArchives
            // 
            this.bbiCancelAllArchives.Caption = "全部取消归档(&D)";
            this.bbiCancelAllArchives.Id = 12;
            this.bbiCancelAllArchives.ImageIndex = 10;
            this.bbiCancelAllArchives.Name = "bbiCancelAllArchives";
            this.bbiCancelAllArchives.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCancelAllArchives_ItemClick);
            // 
            // bbiViewLog
            // 
            this.bbiViewLog.Caption = "查看日志(&R)";
            this.bbiViewLog.Id = 3;
            this.bbiViewLog.ImageIndex = 4;
            this.bbiViewLog.Name = "bbiViewLog";
            this.bbiViewLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiViewLog_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(1169, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 586);
            this.barDockControlBottom.Size = new System.Drawing.Size(1169, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 560);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1169, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 560);
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "Button_View.png");
            this.icTools.Images.SetKeyName(1, "Button_Init.png");
            this.icTools.Images.SetKeyName(2, "Button_Abort.png");
            this.icTools.Images.SetKeyName(3, "Button_Archive.png");
            this.icTools.Images.SetKeyName(4, "Button_View_1.png");
            this.icTools.Images.SetKeyName(5, "Workflow_Instance_List_Archive.png");
            this.icTools.Images.SetKeyName(6, "Workflow_Instance_List_Archive_Bulk.png");
            this.icTools.Images.SetKeyName(7, "Workflow_Instance_List_Archive_All.png");
            this.icTools.Images.SetKeyName(8, "Workflow_Instance_List_Archive_Cancel.png");
            this.icTools.Images.SetKeyName(9, "Workflow_Instance_List_Archive_Cancel_Bulk.png");
            this.icTools.Images.SetKeyName(10, "Workflow_Instance_List_Archive_Cancel_All.png");
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "保存(&S)";
            this.bbiSave.Id = 0;
            this.bbiSave.ImageIndex = 0;
            this.bbiSave.Name = "bbiSave";
            // 
            // bbiCancel
            // 
            this.bbiCancel.Caption = "取消(&C)";
            this.bbiCancel.Id = 1;
            this.bbiCancel.ImageIndex = 1;
            this.bbiCancel.Name = "bbiCancel";
            // 
            // lblWorkflow
            // 
            this.lblWorkflow.Location = new System.Drawing.Point(528, 15);
            this.lblWorkflow.Name = "lblWorkflow";
            this.lblWorkflow.Size = new System.Drawing.Size(48, 14);
            this.lblWorkflow.TabIndex = 41;
            this.lblWorkflow.Text = "工作流：";
            // 
            // ceArchived
            // 
            this.ceArchived.Location = new System.Drawing.Point(999, 41);
            this.ceArchived.Name = "ceArchived";
            this.ceArchived.Properties.Caption = "已归档";
            this.ceArchived.Size = new System.Drawing.Size(61, 19);
            this.ceArchived.TabIndex = 39;
            // 
            // ccmbInstanceState
            // 
            this.ccmbInstanceState.EditValue = "";
            this.ccmbInstanceState.Location = new System.Drawing.Point(111, 41);
            this.ccmbInstanceState.Name = "ccmbInstanceState";
            this.ccmbInstanceState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbInstanceState.Properties.PopupSizeable = false;
            this.ccmbInstanceState.Properties.SelectAllItemVisible = false;
            this.ccmbInstanceState.Size = new System.Drawing.Size(391, 20);
            this.ccmbInstanceState.TabIndex = 1;
            // 
            // lblInstanceState
            // 
            this.lblInstanceState.Location = new System.Drawing.Point(10, 44);
            this.lblInstanceState.Name = "lblInstanceState";
            this.lblInstanceState.Size = new System.Drawing.Size(96, 14);
            this.lblInstanceState.TabIndex = 38;
            this.lblInstanceState.Text = "工作流实例状态：";
            // 
            // dtStart
            // 
            this.dtStart.EditValue = null;
            this.dtStart.Location = new System.Drawing.Point(583, 41);
            this.dtStart.Name = "dtStart";
            this.dtStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStart.Size = new System.Drawing.Size(192, 20);
            this.dtStart.TabIndex = 2;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(781, 44);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(12, 14);
            this.lblTo.TabIndex = 37;
            this.lblTo.Text = "至";
            // 
            // lblTimeSumbitted
            // 
            this.lblTimeSumbitted.Location = new System.Drawing.Point(516, 43);
            this.lblTimeSumbitted.Name = "lblTimeSumbitted";
            this.lblTimeSumbitted.Size = new System.Drawing.Size(60, 14);
            this.lblTimeSumbitted.TabIndex = 36;
            this.lblTimeSumbitted.Text = "提交时间：";
            // 
            // dtEnd
            // 
            this.dtEnd.EditValue = null;
            this.dtEnd.Location = new System.Drawing.Point(799, 41);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEnd.Size = new System.Drawing.Size(192, 20);
            this.dtEnd.TabIndex = 3;
            // 
            // txtInstanceName
            // 
            this.txtInstanceName.Location = new System.Drawing.Point(111, 13);
            this.txtInstanceName.Name = "txtInstanceName";
            this.txtInstanceName.Properties.MaxLength = 64;
            this.txtInstanceName.Properties.NullValuePrompt = "请输入工作流实例名称或者其所属用户名";
            this.txtInstanceName.Size = new System.Drawing.Size(391, 20);
            this.txtInstanceName.TabIndex = 0;
            // 
            // hlnkClear
            // 
            this.hlnkClear.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.hlnkClear.Appearance.Options.UseImage = true;
            this.hlnkClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkClear.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkClear.Location = new System.Drawing.Point(1075, 42);
            this.hlnkClear.Name = "hlnkClear";
            this.hlnkClear.Size = new System.Drawing.Size(45, 20);
            this.hlnkClear.TabIndex = 5;
            this.hlnkClear.Text = "清除";
            this.hlnkClear.Click += new System.EventHandler(this.hlnkClear_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Image = global::Blue.WindowsFormsClient.Properties.Resources.Buttom_Quer_Small;
            this.btnQuery.Location = new System.Drawing.Point(1072, 14);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 20);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblInstanceName
            // 
            this.lblInstanceName.Location = new System.Drawing.Point(46, 15);
            this.lblInstanceName.Name = "lblInstanceName";
            this.lblInstanceName.Size = new System.Drawing.Size(60, 14);
            this.lblInstanceName.TabIndex = 32;
            this.lblInstanceName.Text = "查询条件：";
            // 
            // barTools
            // 
            this.barTools.BarName = "Tools";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 0;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.OptionsBar.AllowQuickCustomization = false;
            this.barTools.OptionsBar.DrawBorder = false;
            this.barTools.OptionsBar.RotateWhenVertical = false;
            this.barTools.OptionsBar.UseWholeRow = true;
            this.barTools.Text = "Tools";
            // 
            // WorkflowInstanceListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 586);
            this.Controls.Add(this.gcWorkflow);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WorkflowInstanceListForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工作流实例管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WorkflowInstanceListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkflow)).EndInit();
            this.gcWorkflow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlQuery)).EndInit();
            this.pnlQuery.ResumeLayout(false);
            this.pnlQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btxtWorkflow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceArchived.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbInstanceState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcWorkflow;
        private AppFramework.WinFormsControls.DevExpressGrid devWorkflow;
        private DevExpress.XtraEditors.PanelControl pnlQuery;
        private DevExpress.XtraEditors.DateEdit dtStart;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private DevExpress.XtraEditors.LabelControl lblTimeSumbitted;
        private DevExpress.XtraEditors.DateEdit dtEnd;
        private DevExpress.XtraEditors.TextEdit txtInstanceName;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkClear;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.LabelControl lblInstanceName;
        private DevExpress.XtraEditors.LabelControl lblInstanceState;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbInstanceState;
        private DevExpress.XtraEditors.CheckEdit ceArchived;
        private DevExpress.XtraBars.Bar barTools;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem bbiView;
        private DevExpress.XtraBars.BarButtonItem bbInit;
        private DevExpress.XtraBars.BarButtonItem bbiAbort;
        private DevExpress.XtraBars.BarButtonItem bbiViewLog;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiCancel;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLinkContainerItem bbiArchiveOperation;
        private DevExpress.XtraBars.BarButtonItem bbiArchive;
        private DevExpress.XtraBars.BarButtonItem bbiBatchArchive;
        private DevExpress.XtraBars.BarButtonItem bbiAllArchives;
        private DevExpress.XtraBars.BarButtonItem bbiCancleArchive;
        private DevExpress.XtraBars.BarButtonItem bbiBatchCancelArchive;
        private DevExpress.XtraBars.BarButtonItem bbiCancelAllArchives;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.XtraEditors.ButtonEdit btxtWorkflow;
        private DevExpress.XtraEditors.LabelControl lblWorkflow;
    }
}