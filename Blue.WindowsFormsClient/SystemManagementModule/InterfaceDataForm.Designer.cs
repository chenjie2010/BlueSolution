namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class InterfaceDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaceDataForm));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.label7 = new System.Windows.Forms.Label();
            this.deEndTime = new DevExpress.XtraEditors.DateEdit();
            this.deStartTime = new DevExpress.XtraEditors.DateEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.devExpressGrid = new AppFramework.WinFormsControls.DevExpressGrid();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnClear);
            this.groupControl1.Controls.Add(this.btnQuery);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.deEndTime);
            this.groupControl1.Controls.Add(this.deStartTime);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1361, 58);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "接口数据条件";
            // 
            // btnClear
            // 
            this.btnClear.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.btnClear.Location = new System.Drawing.Point(641, 28);
            this.btnClear.LookAndFeel.SkinName = "Money Twins";
            this.btnClear.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 21);
            this.btnClear.TabIndex = 235;
            this.btnClear.Text = "清除(&R)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Image = global::Blue.WindowsFormsClient.Properties.Resources.Buttom_Quer_Small;
            this.btnQuery.Location = new System.Drawing.Point(563, 28);
            this.btnQuery.LookAndFeel.SkinName = "Money Twins";
            this.btnQuery.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(70, 21);
            this.btnQuery.TabIndex = 234;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(309, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 14);
            this.label7.TabIndex = 233;
            this.label7.Text = "至";
            // 
            // deEndTime
            // 
            this.deEndTime.EditValue = null;
            this.deEndTime.Location = new System.Drawing.Point(334, 28);
            this.deEndTime.Name = "deEndTime";
            this.deEndTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.deEndTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deEndTime.Properties.CalendarTimeProperties.LookAndFeel.SkinName = "Blue";
            this.deEndTime.Properties.CalendarTimeProperties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.deEndTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.deEndTime.Properties.DisplayFormat.FormatString = "g";
            this.deEndTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deEndTime.Properties.EditFormat.FormatString = "g";
            this.deEndTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deEndTime.Properties.LookAndFeel.SkinName = "Blue";
            this.deEndTime.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.deEndTime.Properties.Mask.EditMask = "g";
            this.deEndTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.deEndTime.Size = new System.Drawing.Size(215, 20);
            this.deEndTime.TabIndex = 231;
            // 
            // deStartTime
            // 
            this.deStartTime.EditValue = null;
            this.deStartTime.Location = new System.Drawing.Point(85, 28);
            this.deStartTime.Name = "deStartTime";
            this.deStartTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deStartTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.deStartTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deStartTime.Properties.CalendarTimeProperties.LookAndFeel.SkinName = "Blue";
            this.deStartTime.Properties.CalendarTimeProperties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.deStartTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.deStartTime.Properties.DisplayFormat.FormatString = "g";
            this.deStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deStartTime.Properties.EditFormat.FormatString = "g";
            this.deStartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deStartTime.Properties.LookAndFeel.SkinName = "Blue";
            this.deStartTime.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.deStartTime.Properties.Mask.EditMask = "g";
            this.deStartTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.deStartTime.Size = new System.Drawing.Size(215, 20);
            this.deStartTime.TabIndex = 230;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 14);
            this.label9.TabIndex = 232;
            this.label9.Text = "起止时间：";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.devExpressGrid);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 58);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1361, 647);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "接口数据结果";
            // 
            // devExpressGrid
            // 
            this.devExpressGrid.CheckboxColumnCaption = null;
            this.devExpressGrid.ColumnHeaderTexts = new string[0];
            this.devExpressGrid.DataKeyNames = new string[] {
        "RecordId"};
            this.devExpressGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devExpressGrid.ExportedExcel = false;
            this.devExpressGrid.FootText = null;
            this.devExpressGrid.ImportedExcel = false;
            this.devExpressGrid.IsMainTable = false;
            this.devExpressGrid.Location = new System.Drawing.Point(2, 21);
            this.devExpressGrid.Name = "devExpressGrid";
            this.devExpressGrid.PageSize = 50;
            this.devExpressGrid.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devExpressGrid.Size = new System.Drawing.Size(1357, 624);
            this.devExpressGrid.TabIndex = 0;
            this.devExpressGrid.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.devExpressGrid_OnPageIndexChanged);
            // 
            // InterfaceDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1361, 705);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InterfaceDataForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "接口数据";
            this.Load += new System.EventHandler(this.InterfaceDataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private AppFramework.WinFormsControls.DevExpressGrid devExpressGrid;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.DateEdit deEndTime;
        private DevExpress.XtraEditors.DateEdit deStartTime;
        private System.Windows.Forms.Label label9;
    }
}