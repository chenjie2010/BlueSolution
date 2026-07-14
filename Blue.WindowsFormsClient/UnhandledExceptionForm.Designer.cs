namespace Blue.WindowsFormsClient
{
    partial class UnhandledExceptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnhandledExceptionForm));
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.peWarning = new DevExpress.XtraEditors.PictureEdit();
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.meException = new DevExpress.XtraEditors.MemoEdit();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.peWarning.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meException.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(447, 221);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "退出(&E)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // peWarning
            // 
            this.peWarning.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.peWarning.EditValue = global::Blue.WindowsFormsClient.Properties.Resources.Common_Waring;
            this.peWarning.Location = new System.Drawing.Point(5, 209);
            this.peWarning.Name = "peWarning";
            this.peWarning.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peWarning.Properties.Appearance.Options.UseBackColor = true;
            this.peWarning.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peWarning.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peWarning.Properties.ZoomAccelerationFactor = 1D;
            this.peWarning.Size = new System.Drawing.Size(48, 48);
            this.peWarning.TabIndex = 4;
            // 
            // gcMain
            // 
            this.gcMain.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.Common_Waring;
            this.gcMain.Controls.Add(this.meException);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(530, 205);
            this.gcMain.TabIndex = 1;
            this.gcMain.Text = "异常信息";
            // 
            // meException
            // 
            this.meException.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meException.Location = new System.Drawing.Point(2, 55);
            this.meException.Name = "meException";
            this.meException.Properties.AllowFocused = false;
            this.meException.Properties.ReadOnly = true;
            this.meException.Size = new System.Drawing.Size(526, 148);
            this.meException.TabIndex = 2;
            // 
            // lblTip
            // 
            this.lblTip.Appearance.Font = new System.Drawing.Font("SimSun", 9F);
            this.lblTip.Location = new System.Drawing.Point(59, 226);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(372, 12);
            this.lblTip.TabIndex = 5;
            this.lblTip.Text = "出现了未处理的异常，建议重启程序。我们给您带来的不便表示道歉。";
            // 
            // UnhandledExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(530, 260);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.peWarning);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.gcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnhandledExceptionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发生了未处理的异常";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UnhandledExceptionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.peWarning.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.meException.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.MemoEdit meException;
        private DevExpress.XtraEditors.PictureEdit peWarning;
        private DevExpress.XtraEditors.LabelControl lblTip;
    }
}