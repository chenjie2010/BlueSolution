namespace Blue.WindowsFormsClient
{
    partial class AuditingUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditingUserControl));
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgAudit = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbtUnAuditted = new DevExpress.XtraNavBar.NavBarItem();
            this.nbtAuditted = new DevExpress.XtraNavBar.NavBarItem();
            this.nbtStatistics = new DevExpress.XtraNavBar.NavBarItem();
            this.icLargeImages = new DevExpress.Utils.ImageCollection(this.components);
            this.xtcAudit = new DevExpress.XtraTab.XtraTabControl();
            this.xtpAudit = new DevExpress.XtraTab.XtraTabPage();
            this.gcAudit = new DevExpress.XtraEditors.GroupControl();
            this.fpSteps = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl2 = new DevExpress.Utils.FlyoutPanelControl();
            this.gcSteps = new DevExpress.XtraGrid.GridControl();
            this.gvSteps = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rcmbDataFieldAuthority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblReviewer = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.btnWithdraw = new DevExpress.XtraEditors.SimpleButton();
            this.devAudit = new AppFramework.WinFormsControls.DevExpressGrid();
            this.pnlTools = new DevExpress.XtraEditors.PanelControl();
            this.lblLine = new DevExpress.XtraEditors.LabelControl();
            this.hlnkView = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btnAbort = new DevExpress.XtraEditors.SimpleButton();
            this.btnView = new DevExpress.XtraEditors.SimpleButton();
            this.btnAudit = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cmbUserType = new Blue.WindowsFormsClient.TreeDropdownList();
            this.icmbInstanceState = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icInstanceState = new DevExpress.Utils.ImageCollection(this.components);
            this.lblInstanceState = new DevExpress.XtraEditors.LabelControl();
            this.dtStart = new DevExpress.XtraEditors.DateEdit();
            this.hlnkClear = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblTimeSumbitted = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.dtEnd = new DevExpress.XtraEditors.DateEdit();
            this.txtInstanceName = new DevExpress.XtraEditors.TextEdit();
            this.lblUserType = new DevExpress.XtraEditors.LabelControl();
            this.cmbDepartment = new Blue.WindowsFormsClient.TreeDropdownList();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblInstanceName = new DevExpress.XtraEditors.LabelControl();
            this.xtpStatics = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icLargeImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcAudit)).BeginInit();
            this.xtcAudit.SuspendLayout();
            this.xtpAudit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAudit)).BeginInit();
            this.gcAudit.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbInstanceState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icInstanceState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbgAudit;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgAudit});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbtUnAuditted,
            this.nbtAuditted,
            this.nbtStatistics});
            this.navBarControl1.LargeImages = this.icLargeImages;
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 160;
            this.navBarControl1.OptionsNavPane.ShowOverflowButton = false;
            this.navBarControl1.OptionsNavPane.ShowOverflowPanel = false;
            this.navBarControl1.OptionsNavPane.ShowSplitter = false;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(160, 477);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nbgAudit
            // 
            this.nbgAudit.Caption = "数据填报审核";
            this.nbgAudit.Expanded = true;
            this.nbgAudit.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
            this.nbgAudit.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.nbgAudit.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbtUnAuditted),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbtAuditted),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbtStatistics)});
            this.nbgAudit.Name = "nbgAudit";
            this.nbgAudit.NavigationPaneVisible = false;
            // 
            // nbtUnAuditted
            // 
            this.nbtUnAuditted.Caption = "待审核数据填报";
            this.nbtUnAuditted.LargeImageIndex = 0;
            this.nbtUnAuditted.Name = "nbtUnAuditted";
            this.nbtUnAuditted.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbtUnAuditted_LinkClicked);
            // 
            // nbtAuditted
            // 
            this.nbtAuditted.Caption = "已审核数据填报";
            this.nbtAuditted.LargeImageIndex = 1;
            this.nbtAuditted.Name = "nbtAuditted";
            this.nbtAuditted.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbtAuditted_LinkClicked);
            // 
            // nbtStatistics
            // 
            this.nbtStatistics.Caption = "数据填报统计";
            this.nbtStatistics.LargeImageIndex = 2;
            this.nbtStatistics.Name = "nbtStatistics";
            this.nbtStatistics.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbtStatistics_LinkClicked);
            // 
            // icLargeImages
            // 
            this.icLargeImages.ImageSize = new System.Drawing.Size(32, 32);
            this.icLargeImages.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icLargeImages.ImageStream")));
            this.icLargeImages.Images.SetKeyName(0, "Auditing_Main_UnAudited.png");
            this.icLargeImages.Images.SetKeyName(1, "Auditing_Main_Audited.png");
            this.icLargeImages.Images.SetKeyName(2, "Auditing_Main_Statistics.png");
            // 
            // xtcAudit
            // 
            this.xtcAudit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtcAudit.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtcAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcAudit.HeaderButtons = DevExpress.XtraTab.TabButtons.None;
            this.xtcAudit.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Never;
            this.xtcAudit.Location = new System.Drawing.Point(160, 0);
            this.xtcAudit.Name = "xtcAudit";
            this.xtcAudit.SelectedTabPage = this.xtpAudit;
            this.xtcAudit.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.xtcAudit.Size = new System.Drawing.Size(840, 477);
            this.xtcAudit.TabIndex = 1;
            this.xtcAudit.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpAudit,
            this.xtpStatics});
            // 
            // xtpAudit
            // 
            this.xtpAudit.Controls.Add(this.gcAudit);
            this.xtpAudit.Name = "xtpAudit";
            this.xtpAudit.Size = new System.Drawing.Size(834, 471);
            this.xtpAudit.Text = "审核";
            // 
            // gcAudit
            // 
            this.gcAudit.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("gcAudit.Appearance.Image")));
            this.gcAudit.Appearance.Options.UseImage = true;
            this.gcAudit.AppearanceCaption.Image = ((System.Drawing.Image)(resources.GetObject("gcAudit.AppearanceCaption.Image")));
            this.gcAudit.AppearanceCaption.Options.UseImage = true;
            this.gcAudit.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.Auditing_Main_Head;
            this.gcAudit.Controls.Add(this.fpSteps);
            this.gcAudit.Controls.Add(this.devAudit);
            this.gcAudit.Controls.Add(this.pnlTools);
            this.gcAudit.Controls.Add(this.panelControl1);
            this.gcAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAudit.Location = new System.Drawing.Point(0, 0);
            this.gcAudit.Name = "gcAudit";
            this.gcAudit.Size = new System.Drawing.Size(834, 471);
            this.gcAudit.TabIndex = 6;
            this.gcAudit.Text = "待审核数据填报";
            // 
            // fpSteps
            // 
            this.fpSteps.Controls.Add(this.flyoutPanelControl2);
            this.fpSteps.Location = new System.Drawing.Point(139, 134);
            this.fpSteps.Name = "fpSteps";
            this.fpSteps.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("Button", global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16, false, true, "")});
            this.fpSteps.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpSteps.OwnerControl = this.btnWithdraw;
            this.fpSteps.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.fpSteps.Size = new System.Drawing.Size(557, 203);
            this.fpSteps.TabIndex = 14;
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
            this.gvSteps.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.devAudit_OnCustomColumnDisplayText);
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
            this.btnWithdraw.Location = new System.Drawing.Point(89, 9);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.Size = new System.Drawing.Size(70, 20);
            this.btnWithdraw.TabIndex = 9;
            this.btnWithdraw.Text = "撤回(&W)";
            this.btnWithdraw.Visible = false;
            this.btnWithdraw.Click += new System.EventHandler(this.btnWithdraw_Click);
            // 
            // devAudit
            // 
            this.devAudit.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devAudit.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devAudit.CheckboxColumnCaption = null;
            this.devAudit.ColumnHeaderTexts = new string[0];
            this.devAudit.DataKeyNames = new string[] {
        "InstanceId"};
            this.devAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devAudit.ExportedExcel = false;
            this.devAudit.FootText = null;
            this.devAudit.ImportedExcel = false;
            this.devAudit.IsMainTable = false;
            this.devAudit.Location = new System.Drawing.Point(2, 137);
            this.devAudit.Name = "devAudit";
            this.devAudit.PageSize = 50;
            this.devAudit.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devAudit.Size = new System.Drawing.Size(830, 332);
            this.devAudit.TabIndex = 5;
            this.devAudit.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.devAudit_OnPageIndexChanged);
            this.devAudit.OnFocusedRowChanged += new System.EventHandler<DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs>(this.devAudit_OnFocusedRowChanged);
            this.devAudit.OnCustomColumnDisplayText += new System.EventHandler<DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs>(this.devAudit_OnCustomColumnDisplayText);
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.lblLine);
            this.pnlTools.Controls.Add(this.hlnkView);
            this.pnlTools.Controls.Add(this.btnAbort);
            this.pnlTools.Controls.Add(this.btnView);
            this.pnlTools.Controls.Add(this.btnWithdraw);
            this.pnlTools.Controls.Add(this.btnAudit);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTools.Location = new System.Drawing.Point(2, 97);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(830, 40);
            this.pnlTools.TabIndex = 8;
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
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cmbUserType);
            this.panelControl1.Controls.Add(this.icmbInstanceState);
            this.panelControl1.Controls.Add(this.lblInstanceState);
            this.panelControl1.Controls.Add(this.dtStart);
            this.panelControl1.Controls.Add(this.hlnkClear);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.lblTimeSumbitted);
            this.panelControl1.Controls.Add(this.btnQuery);
            this.panelControl1.Controls.Add(this.dtEnd);
            this.panelControl1.Controls.Add(this.txtInstanceName);
            this.panelControl1.Controls.Add(this.lblUserType);
            this.panelControl1.Controls.Add(this.cmbDepartment);
            this.panelControl1.Controls.Add(this.lblDepartment);
            this.panelControl1.Controls.Add(this.lblInstanceName);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 23);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(830, 74);
            this.panelControl1.TabIndex = 4;
            // 
            // cmbUserType
            // 
            this.cmbUserType.Location = new System.Drawing.Point(63, 43);
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.OnlySelectedLeaf = true;
            this.cmbUserType.Size = new System.Drawing.Size(150, 30);
            this.cmbUserType.SkinName = "Blue";
            this.cmbUserType.TabIndex = 33;
            this.cmbUserType.TreeDropdownHandler = null;
            // 
            // icmbInstanceState
            // 
            this.icmbInstanceState.Location = new System.Drawing.Point(277, 12);
            this.icmbInstanceState.Name = "icmbInstanceState";
            this.icmbInstanceState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbInstanceState.Properties.SmallImages = this.icInstanceState;
            this.icmbInstanceState.Size = new System.Drawing.Size(115, 20);
            this.icmbInstanceState.TabIndex = 31;
            // 
            // icInstanceState
            // 
            this.icInstanceState.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icInstanceState.ImageStream")));
            this.icInstanceState.Images.SetKeyName(0, "Client_Common_All.png");
            this.icInstanceState.Images.SetKeyName(1, "Client_InstanceState_InitReview.png");
            this.icInstanceState.Images.SetKeyName(2, "Client_InstanceState_FinalReview.png");
            this.icInstanceState.Images.SetKeyName(3, "Client_ReviewedAction_Pass.png");
            this.icInstanceState.Images.SetKeyName(4, "Client_ReviewedAction_Reject.png");
            this.icInstanceState.Images.SetKeyName(5, "Client_ReviewedAction_Abort.png");
            // 
            // lblInstanceState
            // 
            this.lblInstanceState.Location = new System.Drawing.Point(221, 15);
            this.lblInstanceState.Name = "lblInstanceState";
            this.lblInstanceState.Size = new System.Drawing.Size(60, 14);
            this.lblInstanceState.TabIndex = 29;
            this.lblInstanceState.Text = "业务状态：";
            // 
            // dtStart
            // 
            this.dtStart.EditValue = null;
            this.dtStart.Location = new System.Drawing.Point(455, 12);
            this.dtStart.Name = "dtStart";
            this.dtStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStart.Size = new System.Drawing.Size(106, 20);
            this.dtStart.TabIndex = 1;
            // 
            // hlnkClear
            // 
            this.hlnkClear.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.hlnkClear.Appearance.Options.UseImage = true;
            this.hlnkClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkClear.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkClear.Location = new System.Drawing.Point(766, 11);
            this.hlnkClear.Name = "hlnkClear";
            this.hlnkClear.Size = new System.Drawing.Size(45, 20);
            this.hlnkClear.TabIndex = 5;
            this.hlnkClear.Text = "清除";
            this.hlnkClear.Click += new System.EventHandler(this.hlnkClear_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(567, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(12, 14);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "至";
            // 
            // lblTimeSumbitted
            // 
            this.lblTimeSumbitted.Location = new System.Drawing.Point(402, 15);
            this.lblTimeSumbitted.Name = "lblTimeSumbitted";
            this.lblTimeSumbitted.Size = new System.Drawing.Size(60, 14);
            this.lblTimeSumbitted.TabIndex = 4;
            this.lblTimeSumbitted.Text = "提交时间：";
            // 
            // btnQuery
            // 
            this.btnQuery.Image = global::Blue.WindowsFormsClient.Properties.Resources.Buttom_Quer_Small;
            this.btnQuery.Location = new System.Drawing.Point(693, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(70, 20);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dtEnd
            // 
            this.dtEnd.EditValue = null;
            this.dtEnd.Location = new System.Drawing.Point(580, 12);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEnd.Size = new System.Drawing.Size(106, 20);
            this.dtEnd.TabIndex = 2;
            // 
            // txtInstanceName
            // 
            this.txtInstanceName.Location = new System.Drawing.Point(63, 12);
            this.txtInstanceName.Name = "txtInstanceName";
            this.txtInstanceName.Properties.MaxLength = 32;
            this.txtInstanceName.Size = new System.Drawing.Size(150, 20);
            this.txtInstanceName.TabIndex = 0;
            // 
            // lblUserType
            // 
            this.lblUserType.Location = new System.Drawing.Point(4, 45);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new System.Drawing.Size(60, 14);
            this.lblUserType.TabIndex = 32;
            this.lblUserType.Text = "用户类型：";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.Location = new System.Drawing.Point(275, 45);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.ShowSearch = true;
            this.cmbDepartment.Size = new System.Drawing.Size(280, 28);
            this.cmbDepartment.SkinName = "Blue";
            this.cmbDepartment.TabIndex = 73;
            this.cmbDepartment.TreeDropdownHandler = null;
            // 
            // lblDepartment
            // 
            this.lblDepartment.Location = new System.Drawing.Point(216, 46);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(79, 14);
            this.lblDepartment.TabIndex = 74;
            this.lblDepartment.Text = "单位名称：";
            // 
            // lblInstanceName
            // 
            this.lblInstanceName.Location = new System.Drawing.Point(5, 15);
            this.lblInstanceName.Name = "lblInstanceName";
            this.lblInstanceName.Size = new System.Drawing.Size(60, 14);
            this.lblInstanceName.TabIndex = 0;
            this.lblInstanceName.Text = "实例名称：";
            // 
            // xtpStatics
            // 
            this.xtpStatics.Name = "xtpStatics";
            this.xtpStatics.Size = new System.Drawing.Size(834, 471);
            this.xtpStatics.Text = "统计";
            // 
            // AuditingUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtcAudit);
            this.Controls.Add(this.navBarControl1);
            this.Name = "AuditingUserControl";
            this.Size = new System.Drawing.Size(1000, 477);
            this.Load += new System.EventHandler(this.AuditingUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icLargeImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcAudit)).EndInit();
            this.xtcAudit.ResumeLayout(false);
            this.xtpAudit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAudit)).EndInit();
            this.gcAudit.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbInstanceState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icInstanceState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbgAudit;
        private DevExpress.XtraNavBar.NavBarItem nbtUnAuditted;
        private DevExpress.XtraNavBar.NavBarItem nbtAuditted;
        private DevExpress.XtraNavBar.NavBarItem nbtStatistics;
        private DevExpress.Utils.ImageCollection icLargeImages;
        private DevExpress.XtraTab.XtraTabControl xtcAudit;
        private DevExpress.XtraTab.XtraTabPage xtpAudit;
        private DevExpress.XtraTab.XtraTabPage xtpStatics;
        private AppFramework.WinFormsControls.DevExpressGrid devAudit;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkClear;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblTimeSumbitted;
        private DevExpress.XtraEditors.LabelControl lblInstanceName;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.DateEdit dtEnd;
        private DevExpress.XtraEditors.TextEdit txtInstanceName;
        private DevExpress.XtraEditors.DateEdit dtStart;
        private DevExpress.XtraEditors.GroupControl gcAudit;
        private DevExpress.XtraEditors.PanelControl pnlTools;
        private DevExpress.XtraEditors.LabelControl lblLine;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkView;
        private DevExpress.XtraEditors.SimpleButton btnWithdraw;
        private DevExpress.XtraEditors.SimpleButton btnAbort;
        private DevExpress.XtraEditors.SimpleButton btnView;
        private DevExpress.XtraEditors.SimpleButton btnAudit;
        private DevExpress.Utils.FlyoutPanel fpSteps;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl2;
        private DevExpress.XtraGrid.GridControl gcSteps;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSteps;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataFieldAuthority;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblReviewer;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblInstanceState;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbInstanceState;
        private DevExpress.Utils.ImageCollection icInstanceState;
        private DevExpress.XtraEditors.LabelControl lblUserType;
        private TreeDropdownList cmbUserType;
        private TreeDropdownList cmbDepartment;
        protected System.Windows.Forms.Label lblDepartment;
    }
}
