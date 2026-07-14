namespace Blue.WindowsFormsClient.Common
{
    partial class ClientSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientSettingForm));
            this.xtcUserSetting = new DevExpress.XtraTab.XtraTabControl();
            this.xtbCommonSetting = new DevExpress.XtraTab.XtraTabPage();
            this.chkWorkflow = new DevExpress.XtraEditors.CheckEdit();
            this.chkEmailTip = new DevExpress.XtraEditors.CheckEdit();
            this.xtbPersonalSetting = new DevExpress.XtraTab.XtraTabPage();
            this.chkAutoLogin = new DevExpress.XtraEditors.CheckEdit();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtcUserSetting)).BeginInit();
            this.xtcUserSetting.SuspendLayout();
            this.xtbCommonSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkWorkflow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmailTip.Properties)).BeginInit();
            this.xtbPersonalSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtcUserSetting
            // 
            this.xtcUserSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcUserSetting.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtcUserSetting.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtcUserSetting.Location = new System.Drawing.Point(2, 2);
            this.xtcUserSetting.Name = "xtcUserSetting";
            this.xtcUserSetting.SelectedTabPage = this.xtbCommonSetting;
            this.xtcUserSetting.Size = new System.Drawing.Size(454, 215);
            this.xtcUserSetting.TabIndex = 0;
            this.xtcUserSetting.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtbCommonSetting,
            this.xtbPersonalSetting});
            // 
            // xtbCommonSetting
            // 
            this.xtbCommonSetting.Controls.Add(this.chkWorkflow);
            this.xtbCommonSetting.Controls.Add(this.chkEmailTip);
            this.xtbCommonSetting.Name = "xtbCommonSetting";
            this.xtbCommonSetting.Size = new System.Drawing.Size(386, 209);
            this.xtbCommonSetting.Text = "常规设置";
            // 
            // chkWorkflow
            // 
            this.chkWorkflow.Location = new System.Drawing.Point(23, 38);
            this.chkWorkflow.Name = "chkWorkflow";
            this.chkWorkflow.Properties.Caption = "工作流提示";
            this.chkWorkflow.Size = new System.Drawing.Size(88, 19);
            this.chkWorkflow.TabIndex = 1;
            this.chkWorkflow.CheckedChanged += new System.EventHandler(this.chkWorkflow_CheckedChanged);
            // 
            // chkEmailTip
            // 
            this.chkEmailTip.Location = new System.Drawing.Point(23, 11);
            this.chkEmailTip.Name = "chkEmailTip";
            this.chkEmailTip.Properties.Caption = "邮件提示";
            this.chkEmailTip.Size = new System.Drawing.Size(75, 19);
            this.chkEmailTip.TabIndex = 0;
            this.chkEmailTip.CheckedChanged += new System.EventHandler(this.chkEmailTip_CheckedChanged);
            // 
            // xtbPersonalSetting
            // 
            this.xtbPersonalSetting.Controls.Add(this.chkAutoLogin);
            this.xtbPersonalSetting.Name = "xtbPersonalSetting";
            this.xtbPersonalSetting.Size = new System.Drawing.Size(386, 209);
            this.xtbPersonalSetting.Text = "个人设置";
            // 
            // chkAutoLogin
            // 
            this.chkAutoLogin.Location = new System.Drawing.Point(22, 9);
            this.chkAutoLogin.Name = "chkAutoLogin";
            this.chkAutoLogin.Properties.Caption = "自动登录提示";
            this.chkAutoLogin.Size = new System.Drawing.Size(105, 19);
            this.chkAutoLogin.TabIndex = 1;
            this.chkAutoLogin.CheckedChanged += new System.EventHandler(this.chkAutoLogin_CheckedChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnApply);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 219);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(458, 42);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Apply_Small;
            this.btnApply.Location = new System.Drawing.Point(375, 10);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "应用(&S)";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(294, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Enabled = false;
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(213, 10);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确认(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.xtcUserSetting);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(458, 219);
            this.pnlMain.TabIndex = 3;
            // 
            // ClientSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 261);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClientSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户设置";
            this.Load += new System.EventHandler(this.ClientSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtcUserSetting)).EndInit();
            this.xtcUserSetting.ResumeLayout(false);
            this.xtbCommonSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkWorkflow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmailTip.Properties)).EndInit();
            this.xtbPersonalSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtcUserSetting;
        private DevExpress.XtraTab.XtraTabPage xtbCommonSetting;
        private DevExpress.XtraTab.XtraTabPage xtbPersonalSetting;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.CheckEdit chkWorkflow;
        private DevExpress.XtraEditors.CheckEdit chkEmailTip;
        private DevExpress.XtraEditors.CheckEdit chkAutoLogin;
    }
}