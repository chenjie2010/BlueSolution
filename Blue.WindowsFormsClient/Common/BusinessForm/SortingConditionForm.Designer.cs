namespace Blue.WindowsFormsClient.Common
{
    partial class SortingConditionForm
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
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gcDataFields = new DevExpress.XtraGrid.GridControl();
            this.gvDataFields = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDataFieldName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDataField = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFormat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbDataSorting = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataSorting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gcDataFields);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(442, 247);
            this.pnlMain.TabIndex = 0;
            // 
            // gcDataFields
            // 
            this.gcDataFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDataFields.Location = new System.Drawing.Point(2, 2);
            this.gcDataFields.MainView = this.gvDataFields;
            this.gcDataFields.Name = "gcDataFields";
            this.gcDataFields.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbDataSorting});
            this.gcDataFields.Size = new System.Drawing.Size(438, 243);
            this.gcDataFields.TabIndex = 2;
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
            this.gcDataFieldName,
            this.gcDataField,
            this.gcFormat});
            this.gvDataFields.GridControl = this.gcDataFields;
            this.gvDataFields.IndicatorWidth = 45;
            this.gvDataFields.Name = "gvDataFields";
            this.gvDataFields.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvDataFields.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvDataFields.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvDataFields.OptionsBehavior.AutoPopulateColumns = false;
            this.gvDataFields.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gvDataFields.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gvDataFields.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvDataFields.OptionsBehavior.KeepFocusedRowOnUpdate = false;
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
            // gcDataFieldName
            // 
            this.gcDataFieldName.Caption = "字段物理名称";
            this.gcDataFieldName.FieldName = "NodeCode";
            this.gcDataFieldName.Name = "gcDataFieldName";
            // 
            // gcDataField
            // 
            this.gcDataField.Caption = "字段逻辑名称";
            this.gcDataField.FieldName = "NodeName";
            this.gcDataField.Name = "gcDataField";
            this.gcDataField.OptionsColumn.AllowEdit = false;
            this.gcDataField.OptionsColumn.AllowMove = false;
            this.gcDataField.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcDataField.OptionsColumn.ReadOnly = true;
            this.gcDataField.OptionsFilter.AllowAutoFilter = false;
            this.gcDataField.Visible = true;
            this.gcDataField.VisibleIndex = 0;
            this.gcDataField.Width = 150;
            // 
            // gcFormat
            // 
            this.gcFormat.Caption = "排序方式";
            this.gcFormat.ColumnEdit = this.rcmbDataSorting;
            this.gcFormat.FieldName = "NodeType";
            this.gcFormat.Name = "gcFormat";
            this.gcFormat.OptionsFilter.AllowAutoFilter = false;
            this.gcFormat.Visible = true;
            this.gcFormat.VisibleIndex = 1;
            this.gcFormat.Width = 150;
            // 
            // rcmbDataSorting
            // 
            this.rcmbDataSorting.AutoHeight = false;
            this.rcmbDataSorting.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbDataSorting.Name = "rcmbDataSorting";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 247);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(442, 36);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(214, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(132, 6);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确认(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // SortingConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 283);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SortingConditionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "排序条件";
            this.Load += new System.EventHandler(this.SortingConditionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataSorting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraGrid.GridControl gcDataFields;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDataFields;
        private DevExpress.XtraGrid.Columns.GridColumn gcDataFieldName;
        private DevExpress.XtraGrid.Columns.GridColumn gcDataField;
        private DevExpress.XtraGrid.Columns.GridColumn gcFormat;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataSorting;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
    }
}