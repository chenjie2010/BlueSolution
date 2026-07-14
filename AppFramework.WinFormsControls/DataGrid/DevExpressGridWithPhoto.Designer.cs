namespace AppFramework.WinFormsControls.DataGrid
{
    partial class DevExpressGridWithPhoto
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
            this.gcPhoto = new DevExpress.XtraEditors.GroupControl();
            this.peUser = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPhoto)).BeginInit();
            this.gcPhoto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Size = new System.Drawing.Size(572, 309);
            // 
            // gcPhoto
            // 
            this.gcPhoto.Controls.Add(this.peUser);
            this.gcPhoto.Dock = System.Windows.Forms.DockStyle.Right;
            this.gcPhoto.Location = new System.Drawing.Point(572, 0);
            this.gcPhoto.Name = "gcPhoto";
            this.gcPhoto.Size = new System.Drawing.Size(116, 309);
            this.gcPhoto.TabIndex = 8;
            this.gcPhoto.Text = "用户照片";
            // 
            // peUser
            // 
            this.peUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.peUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.peUser.Location = new System.Drawing.Point(2, 21);
            this.peUser.Name = "peUser";
            this.peUser.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peUser.Properties.ShowMenu = false;
            this.peUser.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.peUser.Properties.ZoomAccelerationFactor = 1D;
            this.peUser.Size = new System.Drawing.Size(112, 161);
            this.peUser.TabIndex = 10;
            // 
            // DevExpressGridWithPhoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ColumnHeaderTexts = new string[0];
            this.Controls.Add(this.gcPhoto);
            this.DataKeyNames = new string[0];
            this.Name = "DevExpressGridWithPhoto";
            this.Controls.SetChildIndex(this.gcPhoto, 0);
            this.Controls.SetChildIndex(this.panelControlMain, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPhoto)).EndInit();
            this.gcPhoto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcPhoto;
        private DevExpress.XtraEditors.PictureEdit peUser;
    }
}
