namespace Blue.WindowsFormsClient
{
    partial class DataTableModeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTableModeForm));
            this.gcData = new DevExpress.XtraGrid.GridControl();
            this.gvDataFields = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rcmbDataFieldAuthority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).BeginInit();
            this.SuspendLayout();
            // 
            // gcData
            // 
            this.gcData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcData.Location = new System.Drawing.Point(0, 0);
            this.gcData.MainView = this.gvDataFields;
            this.gcData.Name = "gcData";
            this.gcData.Size = new System.Drawing.Size(624, 261);
            this.gcData.TabIndex = 2;
            this.gcData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
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
            this.gvDataFields.GridControl = this.gcData;
            this.gvDataFields.IndicatorWidth = 45;
            this.gvDataFields.Name = "gvDataFields";
            this.gvDataFields.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvDataFields.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvDataFields.OptionsBehavior.AutoExpandAllGroups = true;
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
            this.gvDataFields.OptionsView.ShowGroupPanel = false;
            this.gvDataFields.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvDataFields_CustomDrawRowIndicator);
            // 
            // rcmbDataFieldAuthority
            // 
            this.rcmbDataFieldAuthority.AutoHeight = false;
            this.rcmbDataFieldAuthority.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbDataFieldAuthority.Name = "rcmbDataFieldAuthority";
            // 
            // DataTableModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 261);
            this.Controls.Add(this.gcData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataTableModeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据展示";
            this.Load += new System.EventHandler(this.DataTableShowForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcData;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDataFields;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataFieldAuthority;
    }
}