namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    partial class PrintingRecordListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintingRecordListForm));
            this.pnlQuery = new DevExpress.XtraEditors.PanelControl();
            this.btxtRecordList = new DevExpress.XtraEditors.ButtonEdit();
            this.lblWorkflow = new DevExpress.XtraEditors.LabelControl();
            this.dtStart = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.lblTimeSumbitted = new DevExpress.XtraEditors.LabelControl();
            this.dtEnd = new DevExpress.XtraEditors.DateEdit();
            this.txtInstanceName = new DevExpress.XtraEditors.TextEdit();
            this.hlnkClear = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.lblInstanceName = new DevExpress.XtraEditors.LabelControl();
            this.devRecordList = new AppFramework.WinFormsControls.DevExpressGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlQuery)).BeginInit();
            this.pnlQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btxtRecordList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQuery
            // 
            this.pnlQuery.Controls.Add(this.btxtRecordList);
            this.pnlQuery.Controls.Add(this.lblWorkflow);
            this.pnlQuery.Controls.Add(this.dtStart);
            this.pnlQuery.Controls.Add(this.lblTo);
            this.pnlQuery.Controls.Add(this.lblTimeSumbitted);
            this.pnlQuery.Controls.Add(this.dtEnd);
            this.pnlQuery.Controls.Add(this.txtInstanceName);
            this.pnlQuery.Controls.Add(this.hlnkClear);
            this.pnlQuery.Controls.Add(this.btnQuery);
            this.pnlQuery.Controls.Add(this.lblInstanceName);
            this.pnlQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlQuery.Location = new System.Drawing.Point(0, 0);
            this.pnlQuery.Name = "pnlQuery";
            this.pnlQuery.Size = new System.Drawing.Size(1060, 70);
            this.pnlQuery.TabIndex = 2;
            // 
            // btxtRecordList
            // 
            this.btxtRecordList.Location = new System.Drawing.Point(547, 12);
            this.btxtRecordList.Name = "btxtRecordList";
            this.btxtRecordList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtRecordList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtRecordList.Size = new System.Drawing.Size(408, 20);
            this.btxtRecordList.TabIndex = 1;
            this.btxtRecordList.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtRecordList_ButtonClick);
            // 
            // lblWorkflow
            // 
            this.lblWorkflow.Location = new System.Drawing.Point(481, 15);
            this.lblWorkflow.Name = "lblWorkflow";
            this.lblWorkflow.Size = new System.Drawing.Size(60, 14);
            this.lblWorkflow.TabIndex = 41;
            this.lblWorkflow.Text = "打印名称：";
            // 
            // dtStart
            // 
            this.dtStart.EditValue = null;
            this.dtStart.Location = new System.Drawing.Point(77, 45);
            this.dtStart.Name = "dtStart";
            this.dtStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStart.Size = new System.Drawing.Size(182, 20);
            this.dtStart.TabIndex = 2;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(265, 48);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(12, 14);
            this.lblTo.TabIndex = 37;
            this.lblTo.Text = "至";
            // 
            // lblTimeSumbitted
            // 
            this.lblTimeSumbitted.Location = new System.Drawing.Point(10, 47);
            this.lblTimeSumbitted.Name = "lblTimeSumbitted";
            this.lblTimeSumbitted.Size = new System.Drawing.Size(60, 14);
            this.lblTimeSumbitted.TabIndex = 36;
            this.lblTimeSumbitted.Text = "预览时间：";
            // 
            // dtEnd
            // 
            this.dtEnd.EditValue = null;
            this.dtEnd.Location = new System.Drawing.Point(283, 45);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEnd.Size = new System.Drawing.Size(183, 20);
            this.dtEnd.TabIndex = 3;
            // 
            // txtInstanceName
            // 
            this.txtInstanceName.Location = new System.Drawing.Point(77, 13);
            this.txtInstanceName.Name = "txtInstanceName";
            this.txtInstanceName.Properties.MaxLength = 64;
            this.txtInstanceName.Properties.NullValuePrompt = "请输入用户名或者用户姓名";
            this.txtInstanceName.Size = new System.Drawing.Size(391, 20);
            this.txtInstanceName.TabIndex = 0;
            // 
            // hlnkClear
            // 
            this.hlnkClear.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.hlnkClear.Appearance.Options.UseImage = true;
            this.hlnkClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkClear.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkClear.Location = new System.Drawing.Point(967, 41);
            this.hlnkClear.Name = "hlnkClear";
            this.hlnkClear.Size = new System.Drawing.Size(45, 20);
            this.hlnkClear.TabIndex = 5;
            this.hlnkClear.Text = "清除";
            this.hlnkClear.Click += new System.EventHandler(this.hlnkClear_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Image = global::Blue.WindowsFormsClient.Properties.Resources.Buttom_Quer_Small;
            this.btnQuery.Location = new System.Drawing.Point(964, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 20);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblInstanceName
            // 
            this.lblInstanceName.Location = new System.Drawing.Point(12, 15);
            this.lblInstanceName.Name = "lblInstanceName";
            this.lblInstanceName.Size = new System.Drawing.Size(60, 14);
            this.lblInstanceName.TabIndex = 32;
            this.lblInstanceName.Text = "查询条件：";
            // 
            // devRecordList
            // 
            this.devRecordList.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devRecordList.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devRecordList.CheckboxColumnCaption = null;
            this.devRecordList.ColumnHeaderTexts = new string[] {
        "打印名称",
        "用户名",
        "用户姓名",
        "打印时间"};
            this.devRecordList.DataKeyNames = new string[] {
        "PrintRecordId"};
            this.devRecordList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devRecordList.ExportedExcel = false;
            this.devRecordList.FootText = null;
            this.devRecordList.ImportedExcel = false;
            this.devRecordList.IsMainTable = false;
            this.devRecordList.Location = new System.Drawing.Point(0, 70);
            this.devRecordList.Name = "devRecordList";
            this.devRecordList.PageSize = 50;
            this.devRecordList.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devRecordList.Size = new System.Drawing.Size(1060, 449);
            this.devRecordList.TabIndex = 11;
            this.devRecordList.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.devWorkflow_OnPageIndexChanged);
            this.devRecordList.OnExportExcel += new System.EventHandler<DevExpress.XtraBars.ItemClickEventArgs>(this.devWorkflow_OnExportExcel);
            // 
            // PrintingRecordListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 519);
            this.Controls.Add(this.devRecordList);
            this.Controls.Add(this.pnlQuery);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrintingRecordListForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印记录列表";
            this.Load += new System.EventHandler(this.PrintingRecordListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlQuery)).EndInit();
            this.pnlQuery.ResumeLayout(false);
            this.pnlQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btxtRecordList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInstanceName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlQuery;
        private DevExpress.XtraEditors.ButtonEdit btxtRecordList;
        private DevExpress.XtraEditors.LabelControl lblWorkflow;
        private DevExpress.XtraEditors.DateEdit dtStart;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private DevExpress.XtraEditors.LabelControl lblTimeSumbitted;
        private DevExpress.XtraEditors.DateEdit dtEnd;
        private DevExpress.XtraEditors.TextEdit txtInstanceName;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkClear;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.LabelControl lblInstanceName;
        private AppFramework.WinFormsControls.DevExpressGrid devRecordList;
    }
}