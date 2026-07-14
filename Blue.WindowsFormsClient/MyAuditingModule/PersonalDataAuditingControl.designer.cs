namespace Blue.WindowsFormsClient.MyAuditingModule
{
    partial class PersonalDataAuditingControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonalDataAuditingControl));
            this.imglstTreeview = new System.Windows.Forms.ImageList(this.components);
            this.gcCondition = new DevExpress.XtraEditors.GroupControl();
            this.hlnkClean = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.deDateTimeTo = new DevExpress.XtraEditors.DateEdit();
            this.lblTimeSumbitted = new DevExpress.XtraEditors.LabelControl();
            this.deTimeFrom = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.txtInstanceName = new DevExpress.XtraEditors.TextEdit();
            this.lblInstanceName = new DevExpress.XtraEditors.LabelControl();
            this.gcBusiness = new DevExpress.XtraEditors.GroupControl();
            this.fpSteps = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl2 = new DevExpress.Utils.FlyoutPanelControl();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.gcSteps = new DevExpress.XtraGrid.GridControl();
            this.gvSteps = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rcmbDataFieldAuthority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblReviewer = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.hlnkView = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.devExpressGrid = new AppFramework.WinFormsControls.DevExpressGrid();
            this.pnlAction = new DevExpress.XtraEditors.PanelControl();
            this.hlnkFresh = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.lblLine = new DevExpress.XtraEditors.LabelControl();
            this.btnReject = new DevExpress.XtraEditors.SimpleButton();
            this.btnAudit = new DevExpress.XtraEditors.SimpleButton();
            this.btnAllocate = new DevExpress.XtraEditors.SimpleButton();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).BeginInit();
            this.gcCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTimeTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTimeTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTimeFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTimeFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBusiness)).BeginInit();
            this.gcBusiness.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSteps)).BeginInit();
            this.fpSteps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).BeginInit();
            this.flyoutPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAction)).BeginInit();
            this.pnlAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // imglstTreeview
            // 
            this.imglstTreeview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstTreeview.ImageStream")));
            this.imglstTreeview.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstTreeview.Images.SetKeyName(0, "Common_Nodes_Up.png");
            this.imglstTreeview.Images.SetKeyName(1, "Common_Nodes_Down.png");
            this.imglstTreeview.Images.SetKeyName(2, "Common_Nodes_Selected.png");
            // 
            // gcCondition
            // 
            this.gcCondition.CaptionImageUri.Uri = "Zoom";
            this.gcCondition.Controls.Add(this.hlnkClean);
            this.gcCondition.Controls.Add(this.btnQuery);
            this.gcCondition.Controls.Add(this.deDateTimeTo);
            this.gcCondition.Controls.Add(this.lblTimeSumbitted);
            this.gcCondition.Controls.Add(this.deTimeFrom);
            this.gcCondition.Controls.Add(this.lblTo);
            this.gcCondition.Controls.Add(this.txtInstanceName);
            this.gcCondition.Controls.Add(this.lblInstanceName);
            this.gcCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcCondition.Location = new System.Drawing.Point(2, 2);
            this.gcCondition.Name = "gcCondition";
            this.gcCondition.Size = new System.Drawing.Size(1013, 60);
            this.gcCondition.TabIndex = 1;
            this.gcCondition.Text = "查询条件";
            // 
            // hlnkClean
            // 
            this.hlnkClean.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.hlnkClean.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkClean.Appearance.ImageIndex = 1;
            this.hlnkClean.Appearance.Options.UseImage = true;
            this.hlnkClean.Appearance.Options.UseImageAlign = true;
            this.hlnkClean.Appearance.Options.UseImageIndex = true;
            this.hlnkClean.Appearance.Options.UseImageList = true;
            this.hlnkClean.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkClean.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkClean.Location = new System.Drawing.Point(704, 30);
            this.hlnkClean.Name = "hlnkClean";
            this.hlnkClean.Size = new System.Drawing.Size(57, 20);
            this.hlnkClean.TabIndex = 32;
            this.hlnkClean.Text = "清除...";
            this.hlnkClean.Click += new System.EventHandler(this.hlnkClean_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Image = global::Blue.WindowsFormsClient.Properties.Resources.Buttom_Quer_Small;
            this.btnQuery.ImageIndex = 0;
            this.btnQuery.Location = new System.Drawing.Point(631, 27);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(70, 23);
            this.btnQuery.TabIndex = 31;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // deDateTimeTo
            // 
            this.deDateTimeTo.EditValue = null;
            this.deDateTimeTo.Location = new System.Drawing.Point(473, 29);
            this.deDateTimeTo.Name = "deDateTimeTo";
            this.deDateTimeTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateTimeTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateTimeTo.Size = new System.Drawing.Size(155, 20);
            this.deDateTimeTo.TabIndex = 30;
            // 
            // lblTimeSumbitted
            // 
            this.lblTimeSumbitted.Location = new System.Drawing.Point(234, 33);
            this.lblTimeSumbitted.Name = "lblTimeSumbitted";
            this.lblTimeSumbitted.Size = new System.Drawing.Size(60, 14);
            this.lblTimeSumbitted.TabIndex = 35;
            this.lblTimeSumbitted.Text = "提交时间：";
            // 
            // deTimeFrom
            // 
            this.deTimeFrom.EditValue = null;
            this.deTimeFrom.Location = new System.Drawing.Point(291, 29);
            this.deTimeFrom.Name = "deTimeFrom";
            this.deTimeFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTimeFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTimeFrom.Size = new System.Drawing.Size(155, 20);
            this.deTimeFrom.TabIndex = 29;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(451, 33);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(24, 14);
            this.lblTo.TabIndex = 34;
            this.lblTo.Text = "至：";
            // 
            // txtInstanceName
            // 
            this.txtInstanceName.EditValue = "";
            this.txtInstanceName.Location = new System.Drawing.Point(65, 29);
            this.txtInstanceName.Name = "txtInstanceName";
            this.txtInstanceName.Properties.MaxLength = 32;
            this.txtInstanceName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtInstanceName.Size = new System.Drawing.Size(164, 20);
            this.txtInstanceName.TabIndex = 28;
            // 
            // lblInstanceName
            // 
            this.lblInstanceName.Location = new System.Drawing.Point(8, 33);
            this.lblInstanceName.Name = "lblInstanceName";
            this.lblInstanceName.Size = new System.Drawing.Size(60, 14);
            this.lblInstanceName.TabIndex = 33;
            this.lblInstanceName.Text = "实例名称：";
            // 
            // gcBusiness
            // 
            this.gcBusiness.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.Client_Business_List_Caption;
            this.gcBusiness.Controls.Add(this.fpSteps);
            this.gcBusiness.Controls.Add(this.devExpressGrid);
            this.gcBusiness.Controls.Add(this.pnlAction);
            this.gcBusiness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBusiness.Location = new System.Drawing.Point(2, 62);
            this.gcBusiness.Name = "gcBusiness";
            this.gcBusiness.Size = new System.Drawing.Size(1013, 481);
            this.gcBusiness.TabIndex = 2;
            this.gcBusiness.Text = "业务列表";
            // 
            // fpSteps
            // 
            this.fpSteps.Controls.Add(this.flyoutPanelControl2);
            this.fpSteps.Location = new System.Drawing.Point(60, 139);
            this.fpSteps.Name = "fpSteps";
            this.fpSteps.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("Button", global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16, false, true, "")});
            this.fpSteps.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpSteps.OwnerControl = this.hlnkView;
            this.fpSteps.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.fpSteps.Size = new System.Drawing.Size(674, 203);
            this.fpSteps.TabIndex = 18;
            this.fpSteps.ButtonClick += new DevExpress.Utils.FlyoutPanelButtonClickEventHandler(this.fpSteps_ButtonClick);
            // 
            // flyoutPanelControl2
            // 
            this.flyoutPanelControl2.Controls.Add(this.progressPanel);
            this.flyoutPanelControl2.Controls.Add(this.gcSteps);
            this.flyoutPanelControl2.Controls.Add(this.panelControl3);
            this.flyoutPanelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl2.FlyoutPanel = this.fpSteps;
            this.flyoutPanelControl2.Location = new System.Drawing.Point(0, 30);
            this.flyoutPanelControl2.Name = "flyoutPanelControl2";
            this.flyoutPanelControl2.Size = new System.Drawing.Size(674, 173);
            this.flyoutPanelControl2.TabIndex = 0;
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "数据正在加载......";
            this.progressPanel.Location = new System.Drawing.Point(262, 61);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(150, 50);
            this.progressPanel.TabIndex = 19;
            this.progressPanel.Text = "数据加载中......";
            this.progressPanel.Visible = false;
            // 
            // gcSteps
            // 
            this.gcSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSteps.Location = new System.Drawing.Point(2, 32);
            this.gcSteps.MainView = this.gvSteps;
            this.gcSteps.Name = "gcSteps";
            this.gcSteps.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbDataFieldAuthority});
            this.gcSteps.Size = new System.Drawing.Size(670, 139);
            this.gcSteps.TabIndex = 2;
            this.gcSteps.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSteps});
            // 
            // gvSteps
            // 
            this.gvSteps.Appearance.FocusedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gvSteps.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvSteps.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvSteps.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvSteps.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvSteps.Appearance.Row.Options.UseTextOptions = true;
            this.gvSteps.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvSteps.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvSteps.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gvSteps.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvSteps.GridControl = this.gcSteps;
            this.gvSteps.IndicatorWidth = 45;
            this.gvSteps.Name = "gvSteps";
            this.gvSteps.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvSteps.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvSteps.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gvSteps.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gvSteps.OptionsBehavior.Editable = false;
            this.gvSteps.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvSteps.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gvSteps.OptionsBehavior.ReadOnly = true;
            this.gvSteps.OptionsCustomization.AllowColumnMoving = false;
            this.gvSteps.OptionsCustomization.AllowFilter = false;
            this.gvSteps.OptionsCustomization.AllowSort = false;
            this.gvSteps.OptionsFind.AllowFindPanel = false;
            this.gvSteps.OptionsFind.ShowClearButton = false;
            this.gvSteps.OptionsFind.ShowCloseButton = false;
            this.gvSteps.OptionsFind.ShowFindButton = false;
            this.gvSteps.OptionsMenu.EnableColumnMenu = false;
            this.gvSteps.OptionsMenu.EnableFooterMenu = false;
            this.gvSteps.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvSteps.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gvSteps.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gvSteps.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gvSteps.OptionsMenu.ShowSplitItem = false;
            this.gvSteps.OptionsView.ShowGroupPanel = false;
            // 
            // rcmbDataFieldAuthority
            // 
            this.rcmbDataFieldAuthority.AutoHeight = false;
            this.rcmbDataFieldAuthority.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbDataFieldAuthority.Name = "rcmbDataFieldAuthority";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.lblReviewer);
            this.panelControl3.Controls.Add(this.lblName);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(2, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(670, 30);
            this.panelControl3.TabIndex = 3;
            // 
            // lblReviewer
            // 
            this.lblReviewer.Location = new System.Drawing.Point(95, 8);
            this.lblReviewer.Name = "lblReviewer";
            this.lblReviewer.Size = new System.Drawing.Size(60, 14);
            this.lblReviewer.TabIndex = 1;
            this.lblReviewer.Text = "审核人名称";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(12, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(84, 14);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "下一步审核人：";
            // 
            // hlnkView
            // 
            this.hlnkView.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Check;
            this.hlnkView.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkView.Appearance.ImageIndex = 1;
            this.hlnkView.Appearance.Options.UseImage = true;
            this.hlnkView.Appearance.Options.UseImageAlign = true;
            this.hlnkView.Appearance.Options.UseImageIndex = true;
            this.hlnkView.Appearance.Options.UseImageList = true;
            this.hlnkView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkView.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkView.Location = new System.Drawing.Point(220, 8);
            this.hlnkView.Name = "hlnkView";
            this.hlnkView.Size = new System.Drawing.Size(69, 20);
            this.hlnkView.TabIndex = 16;
            this.hlnkView.Text = "查看流程";
            this.hlnkView.Click += new System.EventHandler(this.hlnkView_Click);
            // 
            // devExpressGrid
            // 
            this.devExpressGrid.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devExpressGrid.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devExpressGrid.CheckboxColumnCaption = null;
            this.devExpressGrid.ColumnHeaderTexts = new string[] {
        "信息更新名称",
        "用户名",
        "用户姓名",
        "信息更新类型",
        "信息更新状态",
        "申请原因",
        "申请时间"};
            this.devExpressGrid.DataKeyNames = new string[] {
        "AuditingLogId"};
            this.devExpressGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devExpressGrid.ExportedExcel = false;
            this.devExpressGrid.FootText = null;
            this.devExpressGrid.ImportedExcel = false;
            this.devExpressGrid.IsMainTable = false;
            this.devExpressGrid.IsShowCheckBox = true;
            this.devExpressGrid.Location = new System.Drawing.Point(2, 58);
            this.devExpressGrid.Name = "devExpressGrid";
            this.devExpressGrid.PageSize = 50;
            this.devExpressGrid.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devExpressGrid.Size = new System.Drawing.Size(1009, 421);
            this.devExpressGrid.TabIndex = 1;
            this.devExpressGrid.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.devExpressGrid_OnPageIndexChanged);
            this.devExpressGrid.OnCustomColumnDisplayText += new System.EventHandler<DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs>(this.devExpressGrid_OnCustomColumnDisplayText);
            // 
            // pnlAction
            // 
            this.pnlAction.Controls.Add(this.hlnkFresh);
            this.pnlAction.Controls.Add(this.lblLine);
            this.pnlAction.Controls.Add(this.hlnkView);
            this.pnlAction.Controls.Add(this.btnReject);
            this.pnlAction.Controls.Add(this.btnAudit);
            this.pnlAction.Controls.Add(this.btnAllocate);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAction.Location = new System.Drawing.Point(2, 23);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(1009, 35);
            this.pnlAction.TabIndex = 2;
            // 
            // hlnkFresh
            // 
            this.hlnkFresh.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Client_Common_Refresh;
            this.hlnkFresh.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkFresh.Appearance.ImageIndex = 1;
            this.hlnkFresh.Appearance.Options.UseImage = true;
            this.hlnkFresh.Appearance.Options.UseImageAlign = true;
            this.hlnkFresh.Appearance.Options.UseImageIndex = true;
            this.hlnkFresh.Appearance.Options.UseImageList = true;
            this.hlnkFresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkFresh.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkFresh.Location = new System.Drawing.Point(295, 9);
            this.hlnkFresh.Name = "hlnkFresh";
            this.hlnkFresh.Size = new System.Drawing.Size(45, 20);
            this.hlnkFresh.TabIndex = 18;
            this.hlnkFresh.Text = "刷新";
            this.hlnkFresh.Click += new System.EventHandler(this.hlnkFresh_Click);
            // 
            // lblLine
            // 
            this.lblLine.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Vertical_Line;
            this.lblLine.Appearance.Options.UseImage = true;
            this.lblLine.Location = new System.Drawing.Point(199, 10);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(16, 16);
            this.lblLine.TabIndex = 17;
            // 
            // btnReject
            // 
            this.btnReject.Image = global::Blue.WindowsFormsClient.Properties.Resources.Client_Common_Reject;
            this.btnReject.Location = new System.Drawing.Point(117, 6);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(75, 23);
            this.btnReject.TabIndex = 13;
            this.btnReject.Text = "驳回(&E)";
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnAudit
            // 
            this.btnAudit.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Audit;
            this.btnAudit.Location = new System.Drawing.Point(6, 6);
            this.btnAudit.Name = "btnAudit";
            this.btnAudit.Size = new System.Drawing.Size(105, 23);
            this.btnAudit.TabIndex = 12;
            this.btnAudit.Text = "审核通过(&V)";
            this.btnAudit.Click += new System.EventHandler(this.btnAudit_Click);
            // 
            // btnAllocate
            // 
            this.btnAllocate.Enabled = false;
            this.btnAllocate.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Archive;
            this.btnAllocate.Location = new System.Drawing.Point(5, 6);
            this.btnAllocate.Name = "btnAllocate";
            this.btnAllocate.Size = new System.Drawing.Size(105, 23);
            this.btnAllocate.TabIndex = 14;
            this.btnAllocate.Text = "审核分配(&D)";
            this.btnAllocate.Click += new System.EventHandler(this.btnAllocate_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gcBusiness);
            this.pnlMain.Controls.Add(this.gcCondition);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1017, 545);
            this.pnlMain.TabIndex = 4;
            // 
            // PersonalDataAuditingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "PersonalDataAuditingControl";
            this.Size = new System.Drawing.Size(1017, 545);
            this.Load += new System.EventHandler(this.PersonalDataAuditingControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).EndInit();
            this.gcCondition.ResumeLayout(false);
            this.gcCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTimeTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTimeTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTimeFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTimeFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBusiness)).EndInit();
            this.gcBusiness.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSteps)).EndInit();
            this.fpSteps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).EndInit();
            this.flyoutPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAction)).EndInit();
            this.pnlAction.ResumeLayout(false);
            this.pnlAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl gcCondition;
        private AppFramework.WinFormsControls.DevExpressGrid devExpressGrid;
        private DevExpress.XtraEditors.GroupControl gcBusiness;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkClean;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.DateEdit deDateTimeTo;
        private DevExpress.XtraEditors.LabelControl lblTimeSumbitted;
        private DevExpress.XtraEditors.DateEdit deTimeFrom;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private DevExpress.XtraEditors.TextEdit txtInstanceName;
        private DevExpress.XtraEditors.LabelControl lblInstanceName;
        private DevExpress.XtraEditors.PanelControl pnlAction;
        private DevExpress.XtraEditors.LabelControl lblLine;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkView;
        private DevExpress.XtraEditors.SimpleButton btnAllocate;
        private DevExpress.XtraEditors.SimpleButton btnAudit;
        private DevExpress.XtraEditors.SimpleButton btnReject;
        private System.Windows.Forms.ImageList imglstTreeview;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.Utils.FlyoutPanel fpSteps;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl2;
        private DevExpress.XtraGrid.GridControl gcSteps;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSteps;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataFieldAuthority;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblReviewer;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkFresh;
    }
}
