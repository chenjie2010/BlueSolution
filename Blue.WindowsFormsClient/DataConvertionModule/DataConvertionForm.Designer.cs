namespace Blue.WindowsFormsClient.DataConvertionModule
{
    partial class DataConvertionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataConvertionForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnItmClear = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmReset = new DevExpress.XtraBars.BarButtonItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblSheetNames = new DevExpress.XtraEditors.LabelControl();
            this.chkSheetNames = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.sbtnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.label26 = new System.Windows.Forms.Label();
            this.fpUserName = new FarPoint.Win.Spread.FpSpread();
            this.fpUserName_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSheetNames.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUserName_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItmClear,
            this.btnItmReset});
            this.barManager.MaxItemId = 4;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1346, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 583);
            this.barDockControlBottom.Size = new System.Drawing.Size(1346, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 583);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1346, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 583);
            // 
            // btnItmClear
            // 
            this.btnItmClear.Id = 2;
            this.btnItmClear.Name = "btnItmClear";
            // 
            // btnItmReset
            // 
            this.btnItmReset.Id = 3;
            this.btnItmReset.Name = "btnItmReset";
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // gridView2
            // 
            this.gridView2.Name = "gridView2";
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmReset),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmClear)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // openFileDialog
            // 
            this.openFileDialog.RestoreDirectory = true;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblSheetNames);
            this.panelControl1.Controls.Add(this.chkSheetNames);
            this.panelControl1.Controls.Add(this.sbtnPrint);
            this.panelControl1.Controls.Add(this.sbtnLoad);
            this.panelControl1.Controls.Add(this.sbtnSave);
            this.panelControl1.Controls.Add(this.label26);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.LookAndFeel.SkinName = "Money Twins";
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1346, 37);
            this.panelControl1.TabIndex = 254;
            // 
            // lblSheetNames
            // 
            this.lblSheetNames.Location = new System.Drawing.Point(121, 12);
            this.lblSheetNames.Name = "lblSheetNames";
            this.lblSheetNames.Size = new System.Drawing.Size(108, 14);
            this.lblSheetNames.TabIndex = 255;
            this.lblSheetNames.Text = "打印样本名称列表：";
            // 
            // chkSheetNames
            // 
            this.chkSheetNames.Location = new System.Drawing.Point(231, 9);
            this.chkSheetNames.MenuManager = this.barManager;
            this.chkSheetNames.Name = "chkSheetNames";
            this.chkSheetNames.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkSheetNames.Size = new System.Drawing.Size(433, 20);
            this.chkSheetNames.TabIndex = 254;
            // 
            // sbtnPrint
            // 
            this.sbtnPrint.Location = new System.Drawing.Point(734, 8);
            this.sbtnPrint.LookAndFeel.SkinName = "Money Twins";
            this.sbtnPrint.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnPrint.Name = "sbtnPrint";
            this.sbtnPrint.Size = new System.Drawing.Size(82, 23);
            this.sbtnPrint.TabIndex = 253;
            this.sbtnPrint.Text = "批量打印...";
            this.sbtnPrint.Click += new System.EventHandler(this.sbtnPrint_Click);
            // 
            // sbtnLoad
            // 
            this.sbtnLoad.Location = new System.Drawing.Point(6, 6);
            this.sbtnLoad.LookAndFeel.SkinName = "Money Twins";
            this.sbtnLoad.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnLoad.Name = "sbtnLoad";
            this.sbtnLoad.Size = new System.Drawing.Size(100, 23);
            this.sbtnLoad.TabIndex = 243;
            this.sbtnLoad.Text = "加载Excel文件...";
            this.sbtnLoad.Click += new System.EventHandler(this.sbtnLoad_Click);
            // 
            // sbtnSave
            // 
            this.sbtnSave.Location = new System.Drawing.Point(672, 8);
            this.sbtnSave.LookAndFeel.SkinName = "Money Twins";
            this.sbtnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnSave.Name = "sbtnSave";
            this.sbtnSave.Size = new System.Drawing.Size(56, 23);
            this.sbtnSave.TabIndex = 244;
            this.sbtnSave.Text = "保存...";
            this.sbtnSave.Click += new System.EventHandler(this.sbtnSave_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(824, 12);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(343, 14);
            this.label26.TabIndex = 249;
            this.label26.Text = "提示：第一列为用户名，不用列标题；第二列为分类目录名称。";
            // 
            // fpUserName
            // 
            this.fpUserName.AccessibleDescription = "";
            this.fpUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpUserName.Location = new System.Drawing.Point(0, 37);
            this.fpUserName.Name = "fpUserName";
            this.fpUserName.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpUserName_Sheet1});
            this.fpUserName.Size = new System.Drawing.Size(1346, 546);
            this.fpUserName.TabIndex = 255;
            this.fpUserName.TabStripInsertTab = false;
            this.fpUserName.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            // 
            // fpUserName_Sheet1
            // 
            this.fpUserName_Sheet1.Reset();
            this.fpUserName_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpUserName_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpUserName_Sheet1.ColumnFooterSheetCornerStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpUserName_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.fpUserName_Sheet1.ColumnFooterSheetCornerStyle.Parent = "CornerFooterDefaultEnhanced";
            this.fpUserName_Sheet1.ColumnFooterSheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpUserName_Sheet1.FilterBarHeaderStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpUserName_Sheet1.FilterBarHeaderStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.fpUserName_Sheet1.FilterBarHeaderStyle.Parent = "RowHeaderDefaultEnhanced";
            this.fpUserName_Sheet1.FilterBarHeaderStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpUserName_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // DataConvertionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1346, 583);
            this.Controls.Add(this.fpUserName);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataConvertionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量打印报表...";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DataConvertionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSheetNames.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUserName_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnItmClear;
        private DevExpress.XtraBars.BarButtonItem btnItmReset;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private FarPoint.Win.Spread.FpSpread fpUserName;
        private FarPoint.Win.Spread.SheetView fpUserName_Sheet1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        protected DevExpress.XtraEditors.SimpleButton sbtnPrint;
        protected DevExpress.XtraEditors.SimpleButton sbtnLoad;
        protected DevExpress.XtraEditors.SimpleButton sbtnSave;
        protected System.Windows.Forms.Label label26;
        private DevExpress.XtraEditors.LabelControl lblSheetNames;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkSheetNames;
    }
}