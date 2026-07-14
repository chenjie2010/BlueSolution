namespace Blue.WindowsFormsClient
{
    partial class MyWorkUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyWorkUserControl));
            this.nbcMyWork = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgCategories = new DevExpress.XtraNavBar.NavBarGroup();
            this.mbiProcessing = new DevExpress.XtraNavBar.NavBarItem();
            this.mbiProcessed = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiWorkflowStatistics = new DevExpress.XtraNavBar.NavBarItem();
            this.icLargeImages = new DevExpress.Utils.ImageCollection(this.components);
            this.pnlQuery = new DevExpress.XtraEditors.PanelControl();
            this.dtStart = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.lblTimeSumbitted = new DevExpress.XtraEditors.LabelControl();
            this.dtEnd = new DevExpress.XtraEditors.DateEdit();
            this.txtInstanceName = new DevExpress.XtraEditors.TextEdit();
            this.hlnkClear = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.lblInstanceName = new DevExpress.XtraEditors.LabelControl();
            this.icInstanceStatus = new DevExpress.Utils.ImageCollection(this.components);
            this.groupModule1 = new Blue.WindowsFormsClient.BusinessDesignerModule.GroupModule();
            this.gcWorkflow = new DevExpress.XtraEditors.GroupControl();
            this.fpSteps = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl2 = new DevExpress.Utils.FlyoutPanelControl();
            this.gcSteps = new DevExpress.XtraGrid.GridControl();
            this.gvSteps = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rcmbDataFieldAuthority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblReviewer = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.btnWithdraw = new DevExpress.XtraEditors.SimpleButton();
            this.devWorkflow = new AppFramework.WinFormsControls.DevExpressGrid();
            this.pnlTools = new DevExpress.XtraEditors.PanelControl();
            this.lblLine = new DevExpress.XtraEditors.LabelControl();
            this.hlnkView = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btnAbort = new DevExpress.XtraEditors.SimpleButton();
            this.btnView = new DevExpress.XtraEditors.SimpleButton();
            this.btnAudit = new DevExpress.XtraEditors.SimpleButton();
            this.xtcWorkflow = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.nbcMyWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icLargeImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlQuery)).BeginInit();
            this.pnlQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icInstanceStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkflow)).BeginInit();
            this.gcWorkflow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSteps)).BeginInit();
            this.fpSteps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).BeginInit();
            this.flyoutPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTools)).BeginInit();
            this.pnlTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtcWorkflow)).BeginInit();
            this.xtcWorkflow.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nbcMyWork
            // 
            this.nbcMyWork.ActiveGroup = this.nbgCategories;
            this.nbcMyWork.Dock = System.Windows.Forms.DockStyle.Left;
            this.nbcMyWork.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgCategories});
            this.nbcMyWork.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.mbiProcessing,
            this.mbiProcessed,
            this.nbiWorkflowStatistics});
            this.nbcMyWork.LargeImages = this.icLargeImages;
            this.nbcMyWork.Location = new System.Drawing.Point(0, 0);
            this.nbcMyWork.Name = "nbcMyWork";
            this.nbcMyWork.OptionsNavPane.ExpandedWidth = 140;
            this.nbcMyWork.OptionsNavPane.ShowOverflowButton = false;
            this.nbcMyWork.OptionsNavPane.ShowOverflowPanel = false;
            this.nbcMyWork.OptionsNavPane.ShowSplitter = false;
            this.nbcMyWork.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.nbcMyWork.Size = new System.Drawing.Size(140, 477);
            this.nbcMyWork.TabIndex = 0;
            this.nbcMyWork.Text = "navBarControl1";
            // 
            // nbgCategories
            // 
            this.nbgCategories.Caption = "工作分类";
            this.nbgCategories.Expanded = true;
            this.nbgCategories.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            this.nbgCategories.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.nbgCategories.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.mbiProcessing),
            new DevExpress.XtraNavBar.NavBarItemLink(this.mbiProcessed),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiWorkflowStatistics)});
            this.nbgCategories.LargeImage = global::Blue.WindowsFormsClient.Properties.Resources.SystemIcon_Workflow;
            this.nbgCategories.Name = "nbgCategories";
            this.nbgCategories.NavigationPaneVisible = false;
            // 
            // mbiProcessing
            // 
            this.mbiProcessing.Caption = "待处理工作流";
            this.mbiProcessing.LargeImageIndex = 0;
            this.mbiProcessing.Name = "mbiProcessing";
            this.mbiProcessing.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.mbiProcessing_LinkClicked);
            // 
            // mbiProcessed
            // 
            this.mbiProcessed.Caption = "已处理工作流";
            this.mbiProcessed.LargeImageIndex = 1;
            this.mbiProcessed.Name = "mbiProcessed";
            this.mbiProcessed.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.mbiProcessed_LinkClicked);
            // 
            // nbiWorkflowStatistics
            // 
            this.nbiWorkflowStatistics.Caption = "工作流信息统计";
            this.nbiWorkflowStatistics.LargeImageIndex = 2;
            this.nbiWorkflowStatistics.Name = "nbiWorkflowStatistics";
            this.nbiWorkflowStatistics.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiWorkflowStatistics_LinkClicked);
            // 
            // icLargeImages
            // 
            this.icLargeImages.ImageSize = new System.Drawing.Size(32, 32);
            this.icLargeImages.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icLargeImages.ImageStream")));
            this.icLargeImages.Images.SetKeyName(0, "MyWork_Processing.png");
            this.icLargeImages.Images.SetKeyName(1, "MyWork_Processed.png");
            this.icLargeImages.Images.SetKeyName(2, "MyWork_Sumbit.png");
            // 
            // pnlQuery
            // 
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
            this.pnlQuery.Size = new System.Drawing.Size(850, 41);
            this.pnlQuery.TabIndex = 1;
            // 
            // dtStart
            // 
            this.dtStart.EditValue = null;
            this.dtStart.Location = new System.Drawing.Point(477, 11);
            this.dtStart.Name = "dtStart";
            this.dtStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStart.Size = new System.Drawing.Size(106, 20);
            this.dtStart.TabIndex = 34;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(587, 12);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(12, 14);
            this.lblTo.TabIndex = 37;
            this.lblTo.Text = "至";
            // 
            // lblTimeSumbitted
            // 
            this.lblTimeSumbitted.Location = new System.Drawing.Point(423, 13);
            this.lblTimeSumbitted.Name = "lblTimeSumbitted";
            this.lblTimeSumbitted.Size = new System.Drawing.Size(60, 14);
            this.lblTimeSumbitted.TabIndex = 36;
            this.lblTimeSumbitted.Text = "提交时间：";
            // 
            // dtEnd
            // 
            this.dtEnd.EditValue = null;
            this.dtEnd.Location = new System.Drawing.Point(602, 11);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEnd.Size = new System.Drawing.Size(106, 20);
            this.dtEnd.TabIndex = 35;
            // 
            // txtInstanceName
            // 
            this.txtInstanceName.Location = new System.Drawing.Point(60, 11);
            this.txtInstanceName.Name = "txtInstanceName";
            this.txtInstanceName.Properties.MaxLength = 64;
            this.txtInstanceName.Properties.NullValuePrompt = "请输入实例名称或者其所属用户名";
            this.txtInstanceName.Size = new System.Drawing.Size(348, 20);
            this.txtInstanceName.TabIndex = 33;
            // 
            // hlnkClear
            // 
            this.hlnkClear.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.hlnkClear.Appearance.Options.UseImage = true;
            this.hlnkClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkClear.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkClear.Location = new System.Drawing.Point(780, 10);
            this.hlnkClear.Name = "hlnkClear";
            this.hlnkClear.Size = new System.Drawing.Size(45, 20);
            this.hlnkClear.TabIndex = 13;
            this.hlnkClear.Text = "清除";
            this.hlnkClear.Click += new System.EventHandler(this.hlnkClear_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Image = global::Blue.WindowsFormsClient.Properties.Resources.Buttom_Quer_Small;
            this.btnQuery.Location = new System.Drawing.Point(714, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(65, 20);
            this.btnQuery.TabIndex = 12;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblInstanceName
            // 
            this.lblInstanceName.Location = new System.Drawing.Point(5, 13);
            this.lblInstanceName.Name = "lblInstanceName";
            this.lblInstanceName.Size = new System.Drawing.Size(60, 14);
            this.lblInstanceName.TabIndex = 32;
            this.lblInstanceName.Text = "查询条件：";
            // 
            // icInstanceStatus
            // 
            this.icInstanceStatus.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icInstanceStatus.ImageStream")));
            this.icInstanceStatus.Images.SetKeyName(0, "Client_Common_All.png");
            this.icInstanceStatus.Images.SetKeyName(1, "Client_ReviewedAction_Pass.png");
            this.icInstanceStatus.Images.SetKeyName(2, "Client_ReviewedAction_Reject.png");
            this.icInstanceStatus.Images.SetKeyName(3, "Client_ReviewedAction_Abort.png");
            // 
            // groupModule1
            // 
            this.groupModule1.CustomGroupContract = null;
            this.groupModule1.DefaultCode = "";
            this.groupModule1.Location = new System.Drawing.Point(310, 183);
            this.groupModule1.Name = "groupModule1";
            this.groupModule1.Size = new System.Drawing.Size(8, 8);
            this.groupModule1.TabIndex = 2;
            this.groupModule1.TreeNodeId = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // gcWorkflow
            // 
            this.gcWorkflow.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.MyWork_Main_Head1;
            this.gcWorkflow.Controls.Add(this.fpSteps);
            this.gcWorkflow.Controls.Add(this.devWorkflow);
            this.gcWorkflow.Controls.Add(this.pnlTools);
            this.gcWorkflow.Controls.Add(this.pnlQuery);
            this.gcWorkflow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcWorkflow.Location = new System.Drawing.Point(0, 0);
            this.gcWorkflow.Name = "gcWorkflow";
            this.gcWorkflow.Size = new System.Drawing.Size(854, 471);
            this.gcWorkflow.TabIndex = 3;
            this.gcWorkflow.Text = "待处理工作流";
            // 
            // fpSteps
            // 
            this.fpSteps.Controls.Add(this.flyoutPanelControl2);
            this.fpSteps.Location = new System.Drawing.Point(62, 182);
            this.fpSteps.Name = "fpSteps";
            this.fpSteps.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("Button", global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16, false, true, "")});
            this.fpSteps.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpSteps.OwnerControl = this.btnWithdraw;
            this.fpSteps.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.fpSteps.Size = new System.Drawing.Size(748, 203);
            this.fpSteps.TabIndex = 16;
            // 
            // flyoutPanelControl2
            // 
            this.flyoutPanelControl2.Controls.Add(this.gcSteps);
            this.flyoutPanelControl2.Controls.Add(this.panelControl2);
            this.flyoutPanelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl2.FlyoutPanel = this.fpSteps;
            this.flyoutPanelControl2.Location = new System.Drawing.Point(0, 30);
            this.flyoutPanelControl2.Name = "flyoutPanelControl2";
            this.flyoutPanelControl2.Size = new System.Drawing.Size(748, 173);
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
            this.gcSteps.Size = new System.Drawing.Size(744, 139);
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
            this.gvSteps.IndicatorWidth = 25;
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
            this.gvSteps.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvSteps_CustomDrawRowIndicator);
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
            this.panelControl2.Size = new System.Drawing.Size(744, 30);
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
            this.btnWithdraw.Location = new System.Drawing.Point(89, 9);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.Size = new System.Drawing.Size(70, 20);
            this.btnWithdraw.TabIndex = 9;
            this.btnWithdraw.Text = "撤回(&W)";
            this.btnWithdraw.Visible = false;
            this.btnWithdraw.Click += new System.EventHandler(this.btnWithdraw_Click);
            // 
            // devWorkflow
            // 
            this.devWorkflow.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devWorkflow.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devWorkflow.CheckboxColumnCaption = null;
            this.devWorkflow.ColumnHeaderTexts = new string[0];
            this.devWorkflow.DataKeyNames = new string[] {
        "StepId",
        "InstanceId"};
            this.devWorkflow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devWorkflow.ExportedExcel = false;
            this.devWorkflow.FootText = null;
            this.devWorkflow.IsMainTable = false;
            this.devWorkflow.Location = new System.Drawing.Point(2, 104);
            this.devWorkflow.Name = "devWorkflow";
            this.devWorkflow.PageSize = 50;
            this.devWorkflow.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devWorkflow.Size = new System.Drawing.Size(850, 365);
            this.devWorkflow.TabIndex = 10;
            this.devWorkflow.OnFocusedRowChanged += new System.EventHandler<DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs>(this.devWorkflow_OnFocusedRowChanged);
            this.devWorkflow.OnCustomColumnDisplayText += new System.EventHandler<DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs>(this.devWorkflow_OnCustomColumnDisplayText);
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.lblLine);
            this.pnlTools.Controls.Add(this.hlnkView);
            this.pnlTools.Controls.Add(this.btnAbort);
            this.pnlTools.Controls.Add(this.btnView);
            this.pnlTools.Controls.Add(this.btnAudit);
            this.pnlTools.Controls.Add(this.btnWithdraw);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTools.Location = new System.Drawing.Point(2, 64);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(850, 40);
            this.pnlTools.TabIndex = 9;
            // 
            // lblLine
            // 
            this.lblLine.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Vertical_Line;
            this.lblLine.Appearance.Options.UseImage = true;
            this.lblLine.Location = new System.Drawing.Point(721, 11);
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
            this.hlnkView.Location = new System.Drawing.Point(739, 9);
            this.hlnkView.Name = "hlnkView";
            this.hlnkView.Size = new System.Drawing.Size(69, 20);
            this.hlnkView.TabIndex = 10;
            this.hlnkView.Text = "查看流程";
            this.hlnkView.Click += new System.EventHandler(this.hlnkView_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Enabled = false;
            this.btnAbort.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Abort;
            this.btnAbort.Location = new System.Drawing.Point(168, 9);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(70, 20);
            this.btnAbort.TabIndex = 8;
            this.btnAbort.Text = "终止(&B)";
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnView
            // 
            this.btnView.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_View;
            this.btnView.Location = new System.Drawing.Point(10, 9);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(70, 20);
            this.btnView.TabIndex = 6;
            this.btnView.Text = "查看(&V)";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnAudit
            // 
            this.btnAudit.Enabled = false;
            this.btnAudit.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Audit;
            this.btnAudit.Location = new System.Drawing.Point(89, 9);
            this.btnAudit.Name = "btnAudit";
            this.btnAudit.Size = new System.Drawing.Size(70, 20);
            this.btnAudit.TabIndex = 7;
            this.btnAudit.Text = "审核(&A)";
            this.btnAudit.Click += new System.EventHandler(this.btnAudit_Click);
            // 
            // xtcWorkflow
            // 
            this.xtcWorkflow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcWorkflow.Location = new System.Drawing.Point(140, 0);
            this.xtcWorkflow.Name = "xtcWorkflow";
            this.xtcWorkflow.SelectedTabPage = this.xtraTabPage1;
            this.xtcWorkflow.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.xtcWorkflow.Size = new System.Drawing.Size(860, 477);
            this.xtcWorkflow.TabIndex = 4;
            this.xtcWorkflow.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gcWorkflow);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(854, 471);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(854, 471);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // MyWorkUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtcWorkflow);
            this.Controls.Add(this.groupModule1);
            this.Controls.Add(this.nbcMyWork);
            this.Name = "MyWorkUserControl";
            this.Size = new System.Drawing.Size(1000, 477);
            this.Load += new System.EventHandler(this.MyWorkUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nbcMyWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icLargeImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlQuery)).EndInit();
            this.pnlQuery.ResumeLayout(false);
            this.pnlQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icInstanceStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkflow)).EndInit();
            this.gcWorkflow.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.pnlTools)).EndInit();
            this.pnlTools.ResumeLayout(false);
            this.pnlTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtcWorkflow)).EndInit();
            this.xtcWorkflow.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl nbcMyWork;
        private DevExpress.XtraNavBar.NavBarGroup nbgCategories;
        private DevExpress.XtraNavBar.NavBarItem mbiProcessing;
        private DevExpress.XtraNavBar.NavBarItem mbiProcessed;
        private DevExpress.XtraNavBar.NavBarItem nbiWorkflowStatistics;
        private DevExpress.Utils.ImageCollection icLargeImages;
        private DevExpress.XtraEditors.PanelControl pnlQuery;
        private BusinessDesignerModule.GroupModule groupModule1;
        private DevExpress.XtraEditors.GroupControl gcWorkflow;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkClear;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.DateEdit dtStart;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private DevExpress.XtraEditors.LabelControl lblTimeSumbitted;
        private DevExpress.XtraEditors.LabelControl lblInstanceName;
        private DevExpress.XtraEditors.DateEdit dtEnd;
        private DevExpress.XtraEditors.TextEdit txtInstanceName;
        private DevExpress.XtraEditors.PanelControl pnlTools;
        private DevExpress.XtraEditors.LabelControl lblLine;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkView;
        private DevExpress.XtraEditors.SimpleButton btnAbort;
        private DevExpress.XtraEditors.SimpleButton btnView;
        private DevExpress.XtraEditors.SimpleButton btnAudit;
        private DevExpress.XtraEditors.SimpleButton btnWithdraw;
        private AppFramework.WinFormsControls.DevExpressGrid devWorkflow;
        private DevExpress.XtraTab.XtraTabControl xtcWorkflow;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.Utils.ImageCollection icInstanceStatus;
        private DevExpress.Utils.FlyoutPanel fpSteps;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl2;
        private DevExpress.XtraGrid.GridControl gcSteps;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSteps;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataFieldAuthority;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblReviewer;
        private DevExpress.XtraEditors.LabelControl lblName;
    }
}
