namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    partial class WorkflowLogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkflowLogForm));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gcWorkflowLog = new DevExpress.XtraGrid.GridControl();
            this.gvWorkflowLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rcmbDataFieldAuthority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gcSteps = new DevExpress.XtraGrid.GridControl();
            this.gvSteps = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblReviewer = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkflowLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkflowLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 30);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(811, 231);
            this.xtraTabControl1.TabIndex = 6;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gcWorkflowLog);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(805, 202);
            this.xtraTabPage1.Text = "用户流程日志";
            // 
            // gcWorkflowLog
            // 
            this.gcWorkflowLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcWorkflowLog.Location = new System.Drawing.Point(0, 0);
            this.gcWorkflowLog.MainView = this.gvWorkflowLog;
            this.gcWorkflowLog.Name = "gcWorkflowLog";
            this.gcWorkflowLog.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbDataFieldAuthority});
            this.gcWorkflowLog.Size = new System.Drawing.Size(805, 202);
            this.gcWorkflowLog.TabIndex = 2;
            this.gcWorkflowLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWorkflowLog});
            // 
            // gvWorkflowLog
            // 
            this.gvWorkflowLog.Appearance.FocusedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gvWorkflowLog.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvWorkflowLog.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvWorkflowLog.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvWorkflowLog.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvWorkflowLog.Appearance.Row.Options.UseTextOptions = true;
            this.gvWorkflowLog.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvWorkflowLog.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvWorkflowLog.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gvWorkflowLog.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvWorkflowLog.GridControl = this.gcWorkflowLog;
            this.gvWorkflowLog.IndicatorWidth = 25;
            this.gvWorkflowLog.Name = "gvWorkflowLog";
            this.gvWorkflowLog.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvWorkflowLog.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvWorkflowLog.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gvWorkflowLog.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gvWorkflowLog.OptionsBehavior.Editable = false;
            this.gvWorkflowLog.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvWorkflowLog.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gvWorkflowLog.OptionsBehavior.ReadOnly = true;
            this.gvWorkflowLog.OptionsCustomization.AllowColumnMoving = false;
            this.gvWorkflowLog.OptionsCustomization.AllowFilter = false;
            this.gvWorkflowLog.OptionsCustomization.AllowSort = false;
            this.gvWorkflowLog.OptionsFind.AllowFindPanel = false;
            this.gvWorkflowLog.OptionsFind.ShowClearButton = false;
            this.gvWorkflowLog.OptionsFind.ShowCloseButton = false;
            this.gvWorkflowLog.OptionsFind.ShowFindButton = false;
            this.gvWorkflowLog.OptionsMenu.EnableColumnMenu = false;
            this.gvWorkflowLog.OptionsMenu.EnableFooterMenu = false;
            this.gvWorkflowLog.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvWorkflowLog.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gvWorkflowLog.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gvWorkflowLog.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gvWorkflowLog.OptionsMenu.ShowSplitItem = false;
            this.gvWorkflowLog.OptionsView.ShowGroupPanel = false;
            this.gvWorkflowLog.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvWorkflowLog_CustomColumnDisplayText);
            // 
            // rcmbDataFieldAuthority
            // 
            this.rcmbDataFieldAuthority.AutoHeight = false;
            this.rcmbDataFieldAuthority.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbDataFieldAuthority.Name = "rcmbDataFieldAuthority";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gcSteps);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(805, 202);
            this.xtraTabPage2.Text = "当前处理人日志";
            // 
            // gcSteps
            // 
            this.gcSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSteps.Location = new System.Drawing.Point(0, 0);
            this.gcSteps.MainView = this.gvSteps;
            this.gcSteps.Name = "gcSteps";
            this.gcSteps.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.gcSteps.Size = new System.Drawing.Size(805, 202);
            this.gcSteps.TabIndex = 3;
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
            this.gvSteps.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvSteps_CustomColumnDisplayText);
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.lblReviewer);
            this.panelControl2.Controls.Add(this.lblName);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(811, 30);
            this.panelControl2.TabIndex = 5;
            // 
            // lblReviewer
            // 
            this.lblReviewer.Location = new System.Drawing.Point(96, 7);
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
            // WorkflowLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 261);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WorkflowLogForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WorkflowLogForm";
            this.Load += new System.EventHandler(this.WorkflowLogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkflowLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkflowLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraGrid.GridControl gcWorkflowLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWorkflowLog;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataFieldAuthority;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraGrid.GridControl gcSteps;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSteps;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblReviewer;
        private DevExpress.XtraEditors.LabelControl lblName;
    }
}