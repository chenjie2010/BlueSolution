namespace Blue.WindowsFormsClient
{
    partial class SplashScreenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreenForm));
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.lblCompany = new DevExpress.XtraEditors.LabelControl();
            this.lblProcessText = new DevExpress.XtraEditors.LabelControl();
            this.peHREMS = new DevExpress.XtraEditors.PictureEdit();
            this.pesScu = new DevExpress.XtraEditors.PictureEdit();
            this.hlnkCancel = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.peDefault = new DevExpress.XtraEditors.PictureEdit();
            this.peSCUHR = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peHREMS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesScu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peSCUHR.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.EditValue = 0;
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(11, 213);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(424, 10);
            this.marqueeProgressBarControl1.TabIndex = 5;
            // 
            // lblCompany
            // 
            this.lblCompany.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblCompany.Location = new System.Drawing.Point(183, 267);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(255, 14);
            this.lblCompany.TabIndex = 6;
            this.lblCompany.Text = "成都家易科技有限公司 Copyright © 2006-2019";
            // 
            // lblProcessText
            // 
            this.lblProcessText.Location = new System.Drawing.Point(13, 190);
            this.lblProcessText.Name = "lblProcessText";
            this.lblProcessText.Size = new System.Drawing.Size(72, 14);
            this.lblProcessText.TabIndex = 7;
            this.lblProcessText.Text = "正在登录中...";
            // 
            // peHREMS
            // 
            this.peHREMS.Cursor = System.Windows.Forms.Cursors.Default;
            this.peHREMS.EditValue = ((object)(resources.GetObject("peHREMS.EditValue")));
            this.peHREMS.Location = new System.Drawing.Point(12, 11);
            this.peHREMS.Name = "peHREMS";
            this.peHREMS.Properties.AllowFocused = false;
            this.peHREMS.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peHREMS.Properties.Appearance.Options.UseBackColor = true;
            this.peHREMS.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peHREMS.Properties.ShowMenu = false;
            this.peHREMS.Properties.ZoomAccelerationFactor = 1D;
            this.peHREMS.Size = new System.Drawing.Size(426, 166);
            this.peHREMS.TabIndex = 9;
            // 
            // pesScu
            // 
            this.pesScu.Cursor = System.Windows.Forms.Cursors.Default;
            this.pesScu.EditValue = ((object)(resources.GetObject("pesScu.EditValue")));
            this.pesScu.Location = new System.Drawing.Point(13, 239);
            this.pesScu.Name = "pesScu";
            this.pesScu.Properties.AllowFocused = false;
            this.pesScu.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pesScu.Properties.Appearance.Options.UseBackColor = true;
            this.pesScu.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pesScu.Properties.ShowMenu = false;
            this.pesScu.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pesScu.Properties.ZoomAccelerationFactor = 1D;
            this.pesScu.Size = new System.Drawing.Size(160, 44);
            this.pesScu.TabIndex = 8;
            this.pesScu.Visible = false;
            // 
            // hlnkCancel
            // 
            this.hlnkCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkCancel.Location = new System.Drawing.Point(403, 243);
            this.hlnkCancel.Name = "hlnkCancel";
            this.hlnkCancel.Size = new System.Drawing.Size(24, 14);
            this.hlnkCancel.TabIndex = 10;
            this.hlnkCancel.Text = "取消";
            this.hlnkCancel.Click += new System.EventHandler(this.hlnkCancel_Click);
            // 
            // peDefault
            // 
            this.peDefault.Cursor = System.Windows.Forms.Cursors.Default;
            this.peDefault.EditValue = ((object)(resources.GetObject("peDefault.EditValue")));
            this.peDefault.Location = new System.Drawing.Point(12, 229);
            this.peDefault.Name = "peDefault";
            this.peDefault.Properties.AllowFocused = false;
            this.peDefault.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peDefault.Properties.Appearance.Options.UseBackColor = true;
            this.peDefault.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peDefault.Properties.ShowMenu = false;
            this.peDefault.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peDefault.Properties.ZoomAccelerationFactor = 1D;
            this.peDefault.Size = new System.Drawing.Size(160, 61);
            this.peDefault.TabIndex = 11;
            // 
            // peSCUHR
            // 
            this.peSCUHR.Cursor = System.Windows.Forms.Cursors.Default;
            this.peSCUHR.EditValue = ((object)(resources.GetObject("peSCUHR.EditValue")));
            this.peSCUHR.Location = new System.Drawing.Point(13, 11);
            this.peSCUHR.Name = "peSCUHR";
            this.peSCUHR.Properties.AllowFocused = false;
            this.peSCUHR.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peSCUHR.Properties.Appearance.Options.UseBackColor = true;
            this.peSCUHR.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peSCUHR.Properties.ShowMenu = false;
            this.peSCUHR.Properties.ZoomAccelerationFactor = 1D;
            this.peSCUHR.Size = new System.Drawing.Size(426, 166);
            this.peSCUHR.TabIndex = 12;
            // 
            // SplashScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 295);
            this.Controls.Add(this.hlnkCancel);
            this.Controls.Add(this.peHREMS);
            this.Controls.Add(this.lblProcessText);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.marqueeProgressBarControl1);
            this.Controls.Add(this.peDefault);
            this.Controls.Add(this.pesScu);
            this.Controls.Add(this.peSCUHR);
            this.Name = "SplashScreenForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peHREMS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesScu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peSCUHR.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private DevExpress.XtraEditors.LabelControl lblCompany;
        private DevExpress.XtraEditors.LabelControl lblProcessText;
        private DevExpress.XtraEditors.PictureEdit pesScu;
        private DevExpress.XtraEditors.PictureEdit peHREMS;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkCancel;
        private DevExpress.XtraEditors.PictureEdit peDefault;
        private DevExpress.XtraEditors.PictureEdit peSCUHR;
    }
}
