namespace Blue.WindowsFormsClient.MyDataModule
{
    partial class DataFilledListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataFilledListForm));
            this.gcCondition = new DevExpress.XtraEditors.GroupControl();
            this.ccmbInstanceState = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.hlnkClean = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.deDateTimeTo = new DevExpress.XtraEditors.DateEdit();
            this.lblTimeSumbitted = new DevExpress.XtraEditors.LabelControl();
            this.deTimeFrom = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.lblInstanceState = new DevExpress.XtraEditors.LabelControl();
            this.txtInstanceName = new DevExpress.XtraEditors.TextEdit();
            this.lblInstanceName = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.fpSteps = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl2 = new DevExpress.Utils.FlyoutPanelControl();
            this.gcSteps = new DevExpress.XtraGrid.GridControl();
            this.gvSteps = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rcmbDataFieldAuthority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblReviewer = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.btnWithdraw = new DevExpress.XtraEditors.SimpleButton();
            this.fpComment = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl1 = new DevExpress.Utils.FlyoutPanelControl();
            this.meLastestComment = new DevExpress.XtraEditors.MemoEdit();
            this.lblLastestComment = new DevExpress.XtraEditors.LabelControl();
            this.grdDataFilled = new AppFramework.WinFormsControls.DevExpressGrid();
            this.pnlTools = new DevExpress.XtraEditors.PanelControl();
            this.lblLine = new DevExpress.XtraEditors.LabelControl();
            this.hlnkView = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnView = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.icInstanceState = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).BeginInit();
            this.gcCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbInstanceState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTimeTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTimeTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTimeFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTimeFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSteps)).BeginInit();
            this.fpSteps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).BeginInit();
            this.flyoutPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpComment)).BeginInit();
            this.fpComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).BeginInit();
            this.flyoutPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meLastestComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTools)).BeginInit();
            this.pnlTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icInstanceState)).BeginInit();
            this.SuspendLayout();
            // 
            // gcCondition
            // 
            this.gcCondition.CaptionImageUri.Uri = "Zoom";
            this.gcCondition.Controls.Add(this.ccmbInstanceState);
            this.gcCondition.Controls.Add(this.hlnkClean);
            this.gcCondition.Controls.Add(this.btnQuery);
            this.gcCondition.Controls.Add(this.deDateTimeTo);
            this.gcCondition.Controls.Add(this.lblTimeSumbitted);
            this.gcCondition.Controls.Add(this.deTimeFrom);
            this.gcCondition.Controls.Add(this.lblTo);
            this.gcCondition.Controls.Add(this.lblInstanceState);
            this.gcCondition.Controls.Add(this.txtInstanceName);
            this.gcCondition.Controls.Add(this.lblInstanceName);
            this.gcCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcCondition.Location = new System.Drawing.Point(0, 0);
            this.gcCondition.Name = "gcCondition";
            this.gcCondition.Size = new System.Drawing.Size(1144, 54);
            this.gcCondition.TabIndex = 0;
            this.gcCondition.Text = "查询条件";
            // 
            // ccmbInstanceState
            // 
            this.ccmbInstanceState.EditValue = "";
            this.ccmbInstanceState.Location = new System.Drawing.Point(347, 28);
            this.ccmbInstanceState.Name = "ccmbInstanceState";
            this.ccmbInstanceState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbInstanceState.Size = new System.Drawing.Size(196, 20);
            this.ccmbInstanceState.TabIndex = 28;
            this.ccmbInstanceState.EditValueChanged += new System.EventHandler(this.ccmbInstanceState_EditValueChanged);
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
            this.hlnkClean.Location = new System.Drawing.Point(1076, 30);
            this.hlnkClean.Name = "hlnkClean";
            this.hlnkClean.Size = new System.Drawing.Size(57, 20);
            this.hlnkClean.TabIndex = 5;
            this.hlnkClean.Text = "清除...";
            this.hlnkClean.Click += new System.EventHandler(this.hlnkClean_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Image = global::Blue.WindowsFormsClient.Properties.Resources.Buttom_Quer_Small;
            this.btnQuery.ImageIndex = 0;
            this.btnQuery.Location = new System.Drawing.Point(994, 26);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // deDateTimeTo
            // 
            this.deDateTimeTo.EditValue = null;
            this.deDateTimeTo.Location = new System.Drawing.Point(820, 28);
            this.deDateTimeTo.Name = "deDateTimeTo";
            this.deDateTimeTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateTimeTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateTimeTo.Size = new System.Drawing.Size(164, 20);
            this.deDateTimeTo.TabIndex = 3;
            // 
            // lblTimeSumbitted
            // 
            this.lblTimeSumbitted.Location = new System.Drawing.Point(557, 30);
            this.lblTimeSumbitted.Name = "lblTimeSumbitted";
            this.lblTimeSumbitted.Size = new System.Drawing.Size(60, 14);
            this.lblTimeSumbitted.TabIndex = 27;
            this.lblTimeSumbitted.Text = "提交时间：";
            // 
            // deTimeFrom
            // 
            this.deTimeFrom.EditValue = null;
            this.deTimeFrom.Location = new System.Drawing.Point(621, 28);
            this.deTimeFrom.Name = "deTimeFrom";
            this.deTimeFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTimeFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTimeFrom.Size = new System.Drawing.Size(164, 20);
            this.deTimeFrom.TabIndex = 2;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(793, 30);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(24, 14);
            this.lblTo.TabIndex = 25;
            this.lblTo.Text = "至：";
            // 
            // lblInstanceState
            // 
            this.lblInstanceState.Location = new System.Drawing.Point(285, 30);
            this.lblInstanceState.Name = "lblInstanceState";
            this.lblInstanceState.Size = new System.Drawing.Size(60, 14);
            this.lblInstanceState.TabIndex = 22;
            this.lblInstanceState.Text = "业务状态：";
            // 
            // txtInstanceName
            // 
            this.txtInstanceName.EditValue = "";
            this.txtInstanceName.Location = new System.Drawing.Point(73, 28);
            this.txtInstanceName.Name = "txtInstanceName";
            this.txtInstanceName.Properties.MaxLength = 32;
            this.txtInstanceName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtInstanceName.Size = new System.Drawing.Size(195, 20);
            this.txtInstanceName.TabIndex = 0;
            // 
            // lblInstanceName
            // 
            this.lblInstanceName.Location = new System.Drawing.Point(11, 30);
            this.lblInstanceName.Name = "lblInstanceName";
            this.lblInstanceName.Size = new System.Drawing.Size(60, 14);
            this.lblInstanceName.TabIndex = 21;
            this.lblInstanceName.Text = "实例名称：";
            // 
            // groupControl2
            // 
            this.groupControl2.CaptionImageUri.Uri = "ListBullets";
            this.groupControl2.Controls.Add(this.fpSteps);
            this.groupControl2.Controls.Add(this.fpComment);
            this.groupControl2.Controls.Add(this.grdDataFilled);
            this.groupControl2.Controls.Add(this.pnlTools);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 54);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1144, 507);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "填报业务列表";
            // 
            // fpSteps
            // 
            this.fpSteps.Controls.Add(this.flyoutPanelControl2);
            this.fpSteps.Location = new System.Drawing.Point(31, 248);
            this.fpSteps.Name = "fpSteps";
            this.fpSteps.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("Button", global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16, false, true, "")});
            this.fpSteps.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpSteps.OwnerControl = this.btnWithdraw;
            this.fpSteps.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.fpSteps.Size = new System.Drawing.Size(557, 203);
            this.fpSteps.TabIndex = 15;
            this.fpSteps.ButtonClick += new DevExpress.Utils.FlyoutPanelButtonClickEventHandler(this.fpSteps_ButtonClick);
            // 
            // flyoutPanelControl2
            // 
            this.flyoutPanelControl2.Controls.Add(this.gcSteps);
            this.flyoutPanelControl2.Controls.Add(this.panelControl2);
            this.flyoutPanelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl2.FlyoutPanel = this.fpSteps;
            this.flyoutPanelControl2.Location = new System.Drawing.Point(0, 30);
            this.flyoutPanelControl2.Name = "flyoutPanelControl2";
            this.flyoutPanelControl2.Size = new System.Drawing.Size(557, 173);
            this.flyoutPanelControl2.TabIndex = 0;
            // 
            // gcSteps
            // 
            this.gcSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSteps.Location = new System.Drawing.Point(2, 32);
            this.gcSteps.MainView = this.gvSteps;
            this.gcSteps.Name = "gcSteps";
            this.gcSteps.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbDataFieldAuthority});
            this.gcSteps.Size = new System.Drawing.Size(553, 139);
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
            this.gvSteps.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvSteps_CustomColumnDisplayText);
            // 
            // rcmbDataFieldAuthority
            // 
            this.rcmbDataFieldAuthority.AutoHeight = false;
            this.rcmbDataFieldAuthority.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbDataFieldAuthority.Name = "rcmbDataFieldAuthority";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.lblReviewer);
            this.panelControl2.Controls.Add(this.lblName);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(553, 30);
            this.panelControl2.TabIndex = 3;
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
            // btnWithdraw
            // 
            this.btnWithdraw.Enabled = false;
            this.btnWithdraw.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Withdraw;
            this.btnWithdraw.Location = new System.Drawing.Point(267, 9);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.Size = new System.Drawing.Size(75, 23);
            this.btnWithdraw.TabIndex = 9;
            this.btnWithdraw.Text = "撤回(&W)";
            this.btnWithdraw.Click += new System.EventHandler(this.btnWithdraw_Click);
            // 
            // fpComment
            // 
            this.fpComment.Controls.Add(this.flyoutPanelControl1);
            this.fpComment.Location = new System.Drawing.Point(457, 129);
            this.fpComment.Name = "fpComment";
            this.fpComment.OptionsBeakPanel.CloseOnOuterClick = false;
            this.fpComment.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("Button", global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16, false, true, "")});
            this.fpComment.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpComment.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.fpComment.Size = new System.Drawing.Size(446, 150);
            this.fpComment.TabIndex = 12;
            this.fpComment.ButtonClick += new DevExpress.Utils.FlyoutPanelButtonClickEventHandler(this.fpComment_ButtonClick);
            // 
            // flyoutPanelControl1
            // 
            this.flyoutPanelControl1.Controls.Add(this.meLastestComment);
            this.flyoutPanelControl1.Controls.Add(this.lblLastestComment);
            this.flyoutPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl1.FlyoutPanel = this.fpComment;
            this.flyoutPanelControl1.Location = new System.Drawing.Point(0, 30);
            this.flyoutPanelControl1.Name = "flyoutPanelControl1";
            this.flyoutPanelControl1.Size = new System.Drawing.Size(446, 120);
            this.flyoutPanelControl1.TabIndex = 0;
            // 
            // meLastestComment
            // 
            this.meLastestComment.Location = new System.Drawing.Point(5, 25);
            this.meLastestComment.Name = "meLastestComment";
            this.meLastestComment.Properties.ReadOnly = true;
            this.meLastestComment.Size = new System.Drawing.Size(436, 90);
            this.meLastestComment.TabIndex = 34;
            // 
            // lblLastestComment
            // 
            this.lblLastestComment.Location = new System.Drawing.Point(5, 5);
            this.lblLastestComment.Name = "lblLastestComment";
            this.lblLastestComment.Size = new System.Drawing.Size(96, 14);
            this.lblLastestComment.TabIndex = 35;
            this.lblLastestComment.Text = "上一步审核意见：";
            // 
            // grdDataFilled
            // 
            this.grdDataFilled.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdDataFilled.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdDataFilled.CheckboxColumnCaption = "选择项";
            this.grdDataFilled.ColumnHeaderTexts = new string[0];
            this.grdDataFilled.DataKeyNames = new string[] {
        "InstanceId",
        "DataId",
        "UserId",
        "ParentUserId"};
            this.grdDataFilled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDataFilled.ExportedExcel = false;
            this.grdDataFilled.FootText = null;
            this.grdDataFilled.IsMainTable = false;
            this.grdDataFilled.Location = new System.Drawing.Point(2, 63);
            this.grdDataFilled.Name = "grdDataFilled";
            this.grdDataFilled.PageSize = 50;
            this.grdDataFilled.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grdDataFilled.Size = new System.Drawing.Size(1140, 442);
            this.grdDataFilled.TabIndex = 6;
            this.grdDataFilled.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.grdDataFilled_OnPageIndexChanged);
            this.grdDataFilled.OnFocusedRowChanged += new System.EventHandler<DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs>(this.grdDataFilled_OnFocusedRowChanged);
            this.grdDataFilled.OnRowCellClick += new System.EventHandler<DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs>(this.grdDataFilled_OnRowCellClick);
            this.grdDataFilled.OnCustomColumnDisplayText += new System.EventHandler<DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs>(this.grdDataFilled_OnCustomColumnDisplayText);
            this.grdDataFilled.RowStyle += new System.EventHandler<DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs>(this.grdDataFilled_RowStyle);
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.lblLine);
            this.pnlTools.Controls.Add(this.hlnkView);
            this.pnlTools.Controls.Add(this.btnWithdraw);
            this.pnlTools.Controls.Add(this.btnDelete);
            this.pnlTools.Controls.Add(this.btnView);
            this.pnlTools.Controls.Add(this.btnEdit);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTools.Location = new System.Drawing.Point(2, 23);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(1140, 40);
            this.pnlTools.TabIndex = 7;
            // 
            // lblLine
            // 
            this.lblLine.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Vertical_Line;
            this.lblLine.Appearance.Options.UseImage = true;
            this.lblLine.Location = new System.Drawing.Point(247, 13);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(16, 16);
            this.lblLine.TabIndex = 11;
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
            this.hlnkView.Location = new System.Drawing.Point(347, 11);
            this.hlnkView.Name = "hlnkView";
            this.hlnkView.Size = new System.Drawing.Size(69, 20);
            this.hlnkView.TabIndex = 10;
            this.hlnkView.Text = "查看流程";
            this.hlnkView.Click += new System.EventHandler(this.hlnkView_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Delete;
            this.btnDelete.Location = new System.Drawing.Point(168, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnView
            // 
            this.btnView.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_View;
            this.btnView.Location = new System.Drawing.Point(10, 9);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 6;
            this.btnView.Text = "查看(&V)";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Edit;
            this.btnEdit.Location = new System.Drawing.Point(89, 9);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "编辑(&E)";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // icInstanceState
            // 
            this.icInstanceState.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icInstanceState.ImageStream")));
            this.icInstanceState.Images.SetKeyName(0, "Client_Common_Calendar_1.png");
            this.icInstanceState.Images.SetKeyName(1, "Client_Common_Calendar_2.png");
            this.icInstanceState.Images.SetKeyName(2, "Client_Common_Caption.png");
            this.icInstanceState.Images.SetKeyName(3, "Client_Common_Data_Type.png");
            this.icInstanceState.Images.SetKeyName(4, "Client_Common_Condition_Enable.png");
            this.icInstanceState.Images.SetKeyName(5, "Client_Common_Back.png");
            this.icInstanceState.Images.SetKeyName(6, "Client_Common_Refresh.png");
            // 
            // DataFilledListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 561);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.gcCondition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DataFilledListForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "填报业务名称";
            this.Load += new System.EventHandler(this.DataFilledListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).EndInit();
            this.gcCondition.ResumeLayout(false);
            this.gcCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbInstanceState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTimeTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTimeTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTimeFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTimeFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSteps)).EndInit();
            this.fpSteps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).EndInit();
            this.flyoutPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpComment)).EndInit();
            this.fpComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl1)).EndInit();
            this.flyoutPanelControl1.ResumeLayout(false);
            this.flyoutPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meLastestComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTools)).EndInit();
            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icInstanceState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcCondition;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private AppFramework.WinFormsControls.DevExpressGrid grdDataFilled;
        private DevExpress.XtraEditors.LabelControl lblInstanceState;
        private DevExpress.XtraEditors.TextEdit txtInstanceName;
        private DevExpress.XtraEditors.LabelControl lblInstanceName;
        private DevExpress.XtraEditors.DateEdit deDateTimeTo;
        private DevExpress.XtraEditors.LabelControl lblTimeSumbitted;
        private DevExpress.XtraEditors.DateEdit deTimeFrom;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkClean;
        private DevExpress.Utils.ImageCollection icInstanceState;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbInstanceState;
        private DevExpress.XtraEditors.PanelControl pnlTools;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnView;
        private DevExpress.XtraEditors.SimpleButton btnWithdraw;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkView;
        private DevExpress.XtraEditors.LabelControl lblLine;
        private DevExpress.Utils.FlyoutPanel fpComment;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl1;
        private DevExpress.XtraEditors.MemoEdit meLastestComment;
        private DevExpress.XtraEditors.LabelControl lblLastestComment;
        private DevExpress.Utils.FlyoutPanel fpSteps;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblReviewer;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraGrid.GridControl gcSteps;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSteps;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataFieldAuthority;
    }
}