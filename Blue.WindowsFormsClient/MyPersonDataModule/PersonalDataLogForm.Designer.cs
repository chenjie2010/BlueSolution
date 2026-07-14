namespace Blue.WindowsFormsClient.MyPersonDataModule
{
    partial class PersonalDataLogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonalDataLogForm));
            this.xtcLog = new DevExpress.XtraTab.XtraTabControl();
            this.xtpAuditingData = new DevExpress.XtraTab.XtraTabPage();
            this.fpSteps = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl2 = new DevExpress.Utils.FlyoutPanelControl();
            this.gcSteps = new DevExpress.XtraGrid.GridControl();
            this.gvSteps = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rcmbDataFieldAuthority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblReviewer = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.degAuditingData = new AppFramework.WinFormsControls.DevExpressGrid();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.hlnkView = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btnWithdraw = new DevExpress.XtraEditors.SimpleButton();
            this.xtpAuditedData = new DevExpress.XtraTab.XtraTabPage();
            this.degAuditedData = new AppFramework.WinFormsControls.DevExpressGrid();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.hlnkViewLog = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.icData = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtcLog)).BeginInit();
            this.xtcLog.SuspendLayout();
            this.xtpAuditingData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSteps)).BeginInit();
            this.fpSteps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).BeginInit();
            this.flyoutPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.xtpAuditedData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).BeginInit();
            this.SuspendLayout();
            // 
            // xtcLog
            // 
            this.xtcLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcLog.Location = new System.Drawing.Point(0, 0);
            this.xtcLog.Name = "xtcLog";
            this.xtcLog.SelectedTabPage = this.xtpAuditingData;
            this.xtcLog.Size = new System.Drawing.Size(746, 462);
            this.xtcLog.TabIndex = 0;
            this.xtcLog.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpAuditingData,
            this.xtpAuditedData});
            // 
            // xtpAuditingData
            // 
            this.xtpAuditingData.Controls.Add(this.fpSteps);
            this.xtpAuditingData.Controls.Add(this.degAuditingData);
            this.xtpAuditingData.Controls.Add(this.pnlTop);
            this.xtpAuditingData.Name = "xtpAuditingData";
            this.xtpAuditingData.Size = new System.Drawing.Size(740, 433);
            this.xtpAuditingData.Text = "信息更新申请审核中";
            // 
            // fpSteps
            // 
            this.fpSteps.Controls.Add(this.flyoutPanelControl2);
            this.fpSteps.Location = new System.Drawing.Point(30, 75);
            this.fpSteps.Name = "fpSteps";
            this.fpSteps.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("Button", global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16, false, true, "")});
            this.fpSteps.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpSteps.OwnerControl = this.xtcLog;
            this.fpSteps.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.fpSteps.Size = new System.Drawing.Size(674, 203);
            this.fpSteps.TabIndex = 16;
            this.fpSteps.ButtonClick += new DevExpress.Utils.FlyoutPanelButtonClickEventHandler(this.fpSteps_ButtonClick);
            // 
            // flyoutPanelControl2
            // 
            this.flyoutPanelControl2.Controls.Add(this.gcSteps);
            this.flyoutPanelControl2.Controls.Add(this.panelControl3);
            this.flyoutPanelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl2.FlyoutPanel = this.fpSteps;
            this.flyoutPanelControl2.Location = new System.Drawing.Point(0, 30);
            this.flyoutPanelControl2.Name = "flyoutPanelControl2";
            this.flyoutPanelControl2.Size = new System.Drawing.Size(674, 173);
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
            this.gvSteps.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvSteps_CustomColumnDisplayText);
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
            // degAuditingData
            // 
            this.degAuditingData.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.degAuditingData.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.degAuditingData.CheckboxColumnCaption = null;
            this.degAuditingData.ColumnHeaderTexts = new string[] {
        "信息更新名称",
        "信息更新描述",
        "更新状态",
        "申请时间"};
            this.degAuditingData.DataKeyNames = new string[] {
        "AuditingLogId",
        "DataAuditingId"};
            this.degAuditingData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.degAuditingData.ExportedExcel = false;
            this.degAuditingData.FootText = null;
            this.degAuditingData.ImportedExcel = false;
            this.degAuditingData.IsMainTable = false;
            this.degAuditingData.IsShowCheckBox = true;
            this.degAuditingData.Location = new System.Drawing.Point(0, 36);
            this.degAuditingData.Name = "degAuditingData";
            this.degAuditingData.PageSize = 50;
            this.degAuditingData.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.degAuditingData.Size = new System.Drawing.Size(740, 397);
            this.degAuditingData.TabIndex = 0;
            this.degAuditingData.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.degAuditingData_OnPageIndexChanged);
            this.degAuditingData.OnRowView += new System.EventHandler<AppFramework.WinFormsControls.RowEvent>(this.degAuditingData_OnRowView);
            this.degAuditingData.OnRowEdit += new System.EventHandler<AppFramework.WinFormsControls.RowEvent>(this.degAuditingData_OnRowEdit);
            this.degAuditingData.OnDeleteClick += new System.EventHandler<DevExpress.XtraBars.ItemClickEventArgs>(this.degAuditingData_OnDeleteClick);
            this.degAuditingData.OnCustomColumnDisplayText += new System.EventHandler<DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs>(this.degAuditingData_OnCustomColumnDisplayText);
            this.degAuditingData.RowCellStyle += new System.EventHandler<DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs>(this.degAuditingData_RowCellStyle);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.hlnkView);
            this.pnlTop.Controls.Add(this.btnWithdraw);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(740, 36);
            this.pnlTop.TabIndex = 1;
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
            this.hlnkView.Location = new System.Drawing.Point(90, 10);
            this.hlnkView.Name = "hlnkView";
            this.hlnkView.Size = new System.Drawing.Size(69, 20);
            this.hlnkView.TabIndex = 12;
            this.hlnkView.Text = "查看流程";
            this.hlnkView.Click += new System.EventHandler(this.hlnkView_Click);
            // 
            // btnWithdraw
            // 
            this.btnWithdraw.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Withdraw;
            this.btnWithdraw.Location = new System.Drawing.Point(10, 8);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.Size = new System.Drawing.Size(75, 23);
            this.btnWithdraw.TabIndex = 11;
            this.btnWithdraw.Text = "撤回(&W)";
            this.btnWithdraw.Click += new System.EventHandler(this.btnWithdraw_Click);
            // 
            // xtpAuditedData
            // 
            this.xtpAuditedData.Controls.Add(this.degAuditedData);
            this.xtpAuditedData.Controls.Add(this.panelControl2);
            this.xtpAuditedData.Name = "xtpAuditedData";
            this.xtpAuditedData.Size = new System.Drawing.Size(740, 433);
            this.xtpAuditedData.Text = "信息更新申请审核完成";
            // 
            // degAuditedData
            // 
            this.degAuditedData.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.degAuditedData.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.degAuditedData.CheckboxColumnCaption = null;
            this.degAuditedData.ColumnHeaderTexts = new string[] {
        "信息更新名称",
        "信息更新描述",
        "更新状态",
        "申请时间"};
            this.degAuditedData.DataKeyNames = new string[] {
        "AuditingLogId",
        "DataAuditingId"};
            this.degAuditedData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.degAuditedData.ExportedExcel = false;
            this.degAuditedData.FootText = null;
            this.degAuditedData.ImportedExcel = false;
            this.degAuditedData.IsMainTable = false;
            this.degAuditedData.Location = new System.Drawing.Point(0, 42);
            this.degAuditedData.Name = "degAuditedData";
            this.degAuditedData.PageSize = 50;
            this.degAuditedData.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.degAuditedData.Size = new System.Drawing.Size(740, 391);
            this.degAuditedData.TabIndex = 3;
            this.degAuditedData.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.degAuditedData_OnPageIndexChanged);
            this.degAuditedData.OnCustomColumnDisplayText += new System.EventHandler<DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs>(this.degAuditedData_OnCustomColumnDisplayText);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.hlnkViewLog);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(740, 42);
            this.panelControl2.TabIndex = 2;
            // 
            // hlnkViewLog
            // 
            this.hlnkViewLog.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Check;
            this.hlnkViewLog.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkViewLog.Appearance.ImageIndex = 1;
            this.hlnkViewLog.Appearance.Options.UseImage = true;
            this.hlnkViewLog.Appearance.Options.UseImageAlign = true;
            this.hlnkViewLog.Appearance.Options.UseImageIndex = true;
            this.hlnkViewLog.Appearance.Options.UseImageList = true;
            this.hlnkViewLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkViewLog.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkViewLog.Location = new System.Drawing.Point(11, 10);
            this.hlnkViewLog.Name = "hlnkViewLog";
            this.hlnkViewLog.Size = new System.Drawing.Size(69, 20);
            this.hlnkViewLog.TabIndex = 13;
            this.hlnkViewLog.Text = "查看流程";
            this.hlnkViewLog.Click += new System.EventHandler(this.hlnkViewLog_Click);
            // 
            // icData
            // 
            this.icData.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icData.ImageStream")));
            this.icData.Images.SetKeyName(0, "Tools_Edit.png");
            this.icData.Images.SetKeyName(1, "Client_Common_Delete.png");
            this.icData.Images.SetKeyName(2, "Client_Common_Table.png");
            this.icData.Images.SetKeyName(3, "Client_Common_Download.png");
            this.icData.Images.SetKeyName(4, "Client_Common_Excel.png");
            this.icData.Images.SetKeyName(5, "Client_Common_Current_State.png");
            // 
            // PersonalDataLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 462);
            this.Controls.Add(this.xtcLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PersonalDataLogForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新记录列表";
            this.Load += new System.EventHandler(this.PersonalDataLogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtcLog)).EndInit();
            this.xtcLog.ResumeLayout(false);
            this.xtpAuditingData.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.xtpAuditedData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcLog;
        private DevExpress.XtraTab.XtraTabPage xtpAuditingData;
        private DevExpress.XtraTab.XtraTabPage xtpAuditedData;
        private AppFramework.WinFormsControls.DevExpressGrid degAuditingData;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private AppFramework.WinFormsControls.DevExpressGrid degAuditedData;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.Utils.ImageCollection icData;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkView;
        private DevExpress.XtraEditors.SimpleButton btnWithdraw;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkViewLog;
        private DevExpress.Utils.FlyoutPanel fpSteps;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl2;
        private DevExpress.XtraGrid.GridControl gcSteps;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSteps;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataFieldAuthority;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblReviewer;
        private DevExpress.XtraEditors.LabelControl lblName;
    }
}