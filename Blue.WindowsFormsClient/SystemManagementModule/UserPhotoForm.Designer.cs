namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class UserPhotoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserPhotoForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.btnBrowser = new DevExpress.XtraEditors.SimpleButton();
            this.txtUserPhotoDir = new DevExpress.XtraEditors.TextEdit();
            this.lblUserPhotoDir = new DevExpress.XtraEditors.LabelControl();
            this.gcPhotoList = new DevExpress.XtraEditors.GroupControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpload = new DevExpress.XtraEditors.SimpleButton();
            this.bgwLoadUserPhoto = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserPhotoDir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPhotoList)).BeginInit();
            this.gcPhotoList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.CaptionImageUri.Uri = "Open";
            this.gcMain.Controls.Add(this.btnBrowser);
            this.gcMain.Controls.Add(this.txtUserPhotoDir);
            this.gcMain.Controls.Add(this.lblUserPhotoDir);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(569, 70);
            this.gcMain.TabIndex = 0;
            this.gcMain.Text = "支持用户名或是身份证号码作为用户图片名称，用户图片仅支持JPG、PNG、GIF或者BMP格式。";
            // 
            // btnBrowser
            // 
            this.btnBrowser.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Query;
            this.btnBrowser.Location = new System.Drawing.Point(489, 34);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(69, 23);
            this.btnBrowser.TabIndex = 24;
            this.btnBrowser.Text = "浏览...";
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtUserPhotoDir
            // 
            this.txtUserPhotoDir.Location = new System.Drawing.Point(103, 35);
            this.txtUserPhotoDir.Name = "txtUserPhotoDir";
            this.txtUserPhotoDir.Properties.ReadOnly = true;
            this.txtUserPhotoDir.Size = new System.Drawing.Size(377, 20);
            this.txtUserPhotoDir.TabIndex = 23;
            // 
            // lblUserPhotoDir
            // 
            this.lblUserPhotoDir.Location = new System.Drawing.Point(12, 36);
            this.lblUserPhotoDir.Name = "lblUserPhotoDir";
            this.lblUserPhotoDir.Size = new System.Drawing.Size(84, 14);
            this.lblUserPhotoDir.TabIndex = 22;
            this.lblUserPhotoDir.Text = "上传照片路径：";
            // 
            // gcPhotoList
            // 
            this.gcPhotoList.CaptionImageUri.Uri = "AlignJustify";
            this.gcPhotoList.Controls.Add(this.gridControl);
            this.gcPhotoList.Controls.Add(this.panelControl1);
            this.gcPhotoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPhotoList.Location = new System.Drawing.Point(0, 70);
            this.gcPhotoList.Name = "gcPhotoList";
            this.gcPhotoList.Size = new System.Drawing.Size(569, 257);
            this.gcPhotoList.TabIndex = 1;
            this.gcPhotoList.Text = "用户照片列表";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(2, 21);
            this.gridControl.LookAndFeel.SkinName = "Money Twins";
            this.gridControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(565, 197);
            this.gridControl.TabIndex = 6;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.IndicatorWidth = 50;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView.OptionsFilter.AllowFilterEditor = false;
            this.gridView.OptionsFilter.AllowMRUFilterList = false;
            this.gridView.OptionsFind.AllowFindPanel = false;
            this.gridView.OptionsMenu.EnableColumnMenu = false;
            this.gridView.OptionsMenu.EnableFooterMenu = false;
            this.gridView.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblTip);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnUpload);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(2, 218);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(565, 37);
            this.panelControl1.TabIndex = 0;
            // 
            // lblTip
            // 
            this.lblTip.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Information_16;
            this.lblTip.Appearance.Options.UseImage = true;
            this.lblTip.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblTip.Location = new System.Drawing.Point(10, 12);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(105, 20);
            this.lblTip.TabIndex = 25;
            this.lblTip.Text = "导入提示：无。";
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(479, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Upload_16;
            this.btnUpload.Location = new System.Drawing.Point(397, 8);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "上传(&U)";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // bgwLoadUserPhoto
            // 
            this.bgwLoadUserPhoto.WorkerReportsProgress = true;
            this.bgwLoadUserPhoto.WorkerSupportsCancellation = true;
            this.bgwLoadUserPhoto.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoadUserPhoto_DoWork);
            this.bgwLoadUserPhoto.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwLoadUserPhoto_ProgressChanged);
            this.bgwLoadUserPhoto.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLoadUserPhoto_RunWorkerCompleted);
            // 
            // UserPhotoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 327);
            this.Controls.Add(this.gcPhotoList);
            this.Controls.Add(this.gcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserPhotoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入照片";
            this.Load += new System.EventHandler(this.UserPhotoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            this.gcMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserPhotoDir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPhotoList)).EndInit();
            this.gcPhotoList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraEditors.SimpleButton btnBrowser;
        private DevExpress.XtraEditors.TextEdit txtUserPhotoDir;
        private DevExpress.XtraEditors.LabelControl lblUserPhotoDir;
        private DevExpress.XtraEditors.GroupControl gcPhotoList;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnUpload;
        private System.ComponentModel.BackgroundWorker bgwLoadUserPhoto;
        private DevExpress.XtraEditors.LabelControl lblTip;
    }
}