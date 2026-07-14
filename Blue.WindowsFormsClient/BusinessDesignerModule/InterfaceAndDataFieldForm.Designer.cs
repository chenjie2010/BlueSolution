namespace Blue.WindowsFormsClient.SystemDesignerModule
{
    partial class InterfaceAndDataFieldForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaceAndDataFieldForm));
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.icDataFieldAuthority = new DevExpress.Utils.ImageCollection(this.components);
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.pnlRight = new DevExpress.XtraEditors.PanelControl();
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.gcDataFields = new DevExpress.XtraGrid.GridControl();
            this.gvDataFields = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDataFieldId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDataField = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFormat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbDataFieldAuthority = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.icmbBatchSetting = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gcTop = new DevExpress.XtraEditors.GroupControl();
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.gcTables = new DevExpress.XtraEditors.GroupControl();
            this.lstTable = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldAuthority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlRight)).BeginInit();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbBatchSetting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTables)).BeginInit();
            this.gcTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstTable)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnApply);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 507);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(745, 37);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Apply_Small;
            this.btnApply.Location = new System.Drawing.Point(660, 6);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "应用(&S)";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(578, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(496, 6);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "确认(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // icDataFieldAuthority
            // 
            this.icDataFieldAuthority.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataFieldAuthority.ImageStream")));
            this.icDataFieldAuthority.Images.SetKeyName(0, "Common_DataField_InVisible.png");
            this.icDataFieldAuthority.Images.SetKeyName(1, "Common_DataField_ReadOnly.png");
            this.icDataFieldAuthority.Images.SetKeyName(2, "Common_DataField_Edit.png");
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlRight);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(745, 507);
            this.pnlMain.TabIndex = 4;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.gcMain);
            this.pnlRight.Controls.Add(this.gcTop);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(202, 2);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(541, 503);
            this.pnlRight.TabIndex = 6;
            // 
            // gcMain
            // 
            this.gcMain.Controls.Add(this.gcDataFields);
            this.gcMain.Controls.Add(this.icmbBatchSetting);
            this.gcMain.Controls.Add(this.labelControl2);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 62);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(537, 439);
            this.gcMain.TabIndex = 8;
            this.gcMain.Text = "字段权限";
            // 
            // gcDataFields
            // 
            this.gcDataFields.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gcDataFields.Location = new System.Drawing.Point(2, 52);
            this.gcDataFields.MainView = this.gvDataFields;
            this.gcDataFields.Name = "gcDataFields";
            this.gcDataFields.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbDataFieldAuthority});
            this.gcDataFields.Size = new System.Drawing.Size(533, 385);
            this.gcDataFields.TabIndex = 1;
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
            this.gcDataFieldId,
            this.gcDataField,
            this.gcFormat});
            this.gvDataFields.GridControl = this.gcDataFields;
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
            this.gvDataFields.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvDataFields_CellValueChanged);
            // 
            // gcDataFieldId
            // 
            this.gcDataFieldId.Caption = "字段编号";
            this.gcDataFieldId.FieldName = "DataFieldId";
            this.gcDataFieldId.Name = "gcDataFieldId";
            // 
            // gcDataField
            // 
            this.gcDataField.Caption = "字段名";
            this.gcDataField.FieldName = "LogicalName";
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
            this.gcFormat.Caption = "字段权限";
            this.gcFormat.ColumnEdit = this.rcmbDataFieldAuthority;
            this.gcFormat.FieldName = "DataFieldAuthority";
            this.gcFormat.Name = "gcFormat";
            this.gcFormat.OptionsFilter.AllowAutoFilter = false;
            this.gcFormat.Visible = true;
            this.gcFormat.VisibleIndex = 1;
            this.gcFormat.Width = 150;
            // 
            // rcmbDataFieldAuthority
            // 
            this.rcmbDataFieldAuthority.AutoHeight = false;
            this.rcmbDataFieldAuthority.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbDataFieldAuthority.Name = "rcmbDataFieldAuthority";
            this.rcmbDataFieldAuthority.SmallImages = this.icDataFieldAuthority;
            // 
            // icmbBatchSetting
            // 
            this.icmbBatchSetting.Location = new System.Drawing.Point(392, 26);
            this.icmbBatchSetting.Name = "icmbBatchSetting";
            this.icmbBatchSetting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbBatchSetting.Properties.SmallImages = this.icDataFieldAuthority;
            this.icmbBatchSetting.Size = new System.Drawing.Size(141, 20);
            this.icmbBatchSetting.TabIndex = 95;
            this.icmbBatchSetting.SelectedIndexChanged += new System.EventHandler(this.icmbBatchSetting_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(280, 29);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(108, 14);
            this.labelControl2.TabIndex = 94;
            this.labelControl2.Text = "字段权限批量设置：";
            // 
            // gcTop
            // 
            this.gcTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcTop.Location = new System.Drawing.Point(2, 2);
            this.gcTop.Name = "gcTop";
            this.gcTop.Size = new System.Drawing.Size(537, 60);
            this.gcTop.TabIndex = 3;
            this.gcTop.Text = "日期条件";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gcTables);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(2, 2);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(200, 503);
            this.pnlLeft.TabIndex = 5;
            // 
            // gcTables
            // 
            this.gcTables.Controls.Add(this.lstTable);
            this.gcTables.Dock = System.Windows.Forms.DockStyle.Left;
            this.gcTables.Location = new System.Drawing.Point(2, 2);
            this.gcTables.Name = "gcTables";
            this.gcTables.Size = new System.Drawing.Size(200, 499);
            this.gcTables.TabIndex = 0;
            this.gcTables.Text = "工作流程对应表";
            // 
            // lstTable
            // 
            this.lstTable.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTable.Location = new System.Drawing.Point(2, 21);
            this.lstTable.Name = "lstTable";
            this.lstTable.Size = new System.Drawing.Size(196, 476);
            this.lstTable.TabIndex = 0;
            this.lstTable.SelectedIndexChanged += new System.EventHandler(this.lstTable_SelectedIndexChanged);
            // 
            // WorkflowDataFieldsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 544);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WorkflowDataFieldsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工作流流程字段权限设置";
            this.Load += new System.EventHandler(this.InterfaceAndDataFieldForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldAuthority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlRight)).EndInit();
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            this.gcMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldAuthority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbBatchSetting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTables)).EndInit();
            this.gcTables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.Utils.ImageCollection icDataFieldAuthority;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlRight;
        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbBatchSetting;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gcDataFields;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDataFields;
        private DevExpress.XtraGrid.Columns.GridColumn gcDataFieldId;
        private DevExpress.XtraGrid.Columns.GridColumn gcDataField;
        private DevExpress.XtraGrid.Columns.GridColumn gcFormat;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataFieldAuthority;
        private DevExpress.XtraEditors.GroupControl gcTop;
        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private DevExpress.XtraEditors.GroupControl gcTables;
        private DevExpress.XtraEditors.ListBoxControl lstTable;
        private DevExpress.XtraEditors.SimpleButton btnApply;
    }
}