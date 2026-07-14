namespace Blue.WindowsFormsClient.DataConvertionModule
{
    partial class RemoteDatabaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteDatabaseForm));
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.lblProgress = new DevExpress.XtraEditors.LabelControl();
            this.lblOperation = new DevExpress.XtraEditors.LabelControl();
            this.btxtDatabaseName = new DevExpress.XtraEditors.ButtonEdit();
            this.lblDatabaseName = new DevExpress.XtraEditors.LabelControl();
            this.lblDatabaseNameTip = new DevExpress.XtraEditors.LabelControl();
            this.lblTimerValue = new DevExpress.XtraEditors.LabelControl();
            this.hlnkCancel = new DevExpress.XtraEditors.HyperLinkEdit();
            this.pbcWaittingBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.lblRemoteAddressRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblRemotePwdRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtRemotePasswordConfirmed = new DevExpress.XtraEditors.TextEdit();
            this.txtRemoteAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtRemoteUserName = new DevExpress.XtraEditors.TextEdit();
            this.txtRemotePassword = new DevExpress.XtraEditors.TextEdit();
            this.lblRemotePasswordConfirmed = new DevExpress.XtraEditors.LabelControl();
            this.lblRemoteAddress = new DevExpress.XtraEditors.LabelControl();
            this.lblRemoteUserName = new DevExpress.XtraEditors.LabelControl();
            this.lblRemotePassword = new DevExpress.XtraEditors.LabelControl();
            this.lblRemoteUserNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblRemotePasswordRequired = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btxtDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlnkCancel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbcWaittingBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemotePasswordConfirmed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemoteAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemoteUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemotePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.Controls.Add(this.lblProgress);
            this.gcMain.Controls.Add(this.lblOperation);
            this.gcMain.Controls.Add(this.btxtDatabaseName);
            this.gcMain.Controls.Add(this.lblDatabaseName);
            this.gcMain.Controls.Add(this.lblDatabaseNameTip);
            this.gcMain.Controls.Add(this.lblTimerValue);
            this.gcMain.Controls.Add(this.hlnkCancel);
            this.gcMain.Controls.Add(this.pbcWaittingBar);
            this.gcMain.Controls.Add(this.btnTest);
            this.gcMain.Controls.Add(this.lblRemoteAddressRequired);
            this.gcMain.Controls.Add(this.lblRemotePwdRequired);
            this.gcMain.Controls.Add(this.txtRemotePasswordConfirmed);
            this.gcMain.Controls.Add(this.txtRemoteAddress);
            this.gcMain.Controls.Add(this.txtRemoteUserName);
            this.gcMain.Controls.Add(this.txtRemotePassword);
            this.gcMain.Controls.Add(this.lblRemotePasswordConfirmed);
            this.gcMain.Controls.Add(this.lblRemoteAddress);
            this.gcMain.Controls.Add(this.lblRemoteUserName);
            this.gcMain.Controls.Add(this.lblRemotePassword);
            this.gcMain.Controls.Add(this.lblRemoteUserNameRequired);
            this.gcMain.Controls.Add(this.lblRemotePasswordRequired);
            this.gcMain.Controls.Add(this.separatorControl2);
            this.gcMain.Controls.Add(this.separatorControl1);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(403, 271);
            this.gcMain.TabIndex = 0;
            this.gcMain.Text = "提示：请先输入远程交换信息，再测试链接，成功后选择目标数据库。";
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(51, 207);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(60, 14);
            this.lblProgress.TabIndex = 141;
            this.lblProgress.Text = "测试进度：";
            // 
            // lblOperation
            // 
            this.lblOperation.Location = new System.Drawing.Point(51, 177);
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Size = new System.Drawing.Size(60, 14);
            this.lblOperation.TabIndex = 140;
            this.lblOperation.Text = "测试操作：";
            // 
            // btxtDatabaseName
            // 
            this.btxtDatabaseName.Location = new System.Drawing.Point(117, 242);
            this.btxtDatabaseName.Name = "btxtDatabaseName";
            this.btxtDatabaseName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtDatabaseName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtDatabaseName.Size = new System.Drawing.Size(247, 20);
            this.btxtDatabaseName.TabIndex = 6;
            this.btxtDatabaseName.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtDatabaseName_ButtonPressed);
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.Location = new System.Drawing.Point(38, 244);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(72, 14);
            this.lblDatabaseName.TabIndex = 137;
            this.lblDatabaseName.Text = "目标数据库：";
            // 
            // lblDatabaseNameTip
            // 
            this.lblDatabaseNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblDatabaseNameTip.Appearance.Options.UseForeColor = true;
            this.lblDatabaseNameTip.Location = new System.Drawing.Point(376, 247);
            this.lblDatabaseNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblDatabaseNameTip.Name = "lblDatabaseNameTip";
            this.lblDatabaseNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblDatabaseNameTip.TabIndex = 136;
            this.lblDatabaseNameTip.Text = "*";
            // 
            // lblTimerValue
            // 
            this.lblTimerValue.Location = new System.Drawing.Point(258, 177);
            this.lblTimerValue.Name = "lblTimerValue";
            this.lblTimerValue.Size = new System.Drawing.Size(7, 14);
            this.lblTimerValue.TabIndex = 134;
            this.lblTimerValue.Text = "0";
            this.lblTimerValue.Visible = false;
            // 
            // hlnkCancel
            // 
            this.hlnkCancel.EditValue = "取消...";
            this.hlnkCancel.Enabled = false;
            this.hlnkCancel.Location = new System.Drawing.Point(204, 176);
            this.hlnkCancel.Name = "hlnkCancel";
            this.hlnkCancel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlnkCancel.Properties.Appearance.Options.UseBackColor = true;
            this.hlnkCancel.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlnkCancel.Properties.ReadOnly = true;
            this.hlnkCancel.Size = new System.Drawing.Size(48, 18);
            this.hlnkCancel.TabIndex = 5;
            this.hlnkCancel.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.hlnkCancel_ButtonClick);
            // 
            // pbcWaittingBar
            // 
            this.pbcWaittingBar.EditValue = 0;
            this.pbcWaittingBar.Location = new System.Drawing.Point(117, 205);
            this.pbcWaittingBar.Name = "pbcWaittingBar";
            this.pbcWaittingBar.Properties.MarqueeWidth = 30;
            this.pbcWaittingBar.Properties.Stopped = true;
            this.pbcWaittingBar.Size = new System.Drawing.Size(247, 18);
            this.pbcWaittingBar.TabIndex = 132;
            this.pbcWaittingBar.Visible = false;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(117, 174);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(70, 20);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "测试链接...";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblRemoteAddressRequired
            // 
            this.lblRemoteAddressRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblRemoteAddressRequired.Appearance.Options.UseForeColor = true;
            this.lblRemoteAddressRequired.Location = new System.Drawing.Point(376, 41);
            this.lblRemoteAddressRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblRemoteAddressRequired.Name = "lblRemoteAddressRequired";
            this.lblRemoteAddressRequired.Size = new System.Drawing.Size(7, 14);
            this.lblRemoteAddressRequired.TabIndex = 130;
            this.lblRemoteAddressRequired.Text = "*";
            // 
            // lblRemotePwdRequired
            // 
            this.lblRemotePwdRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblRemotePwdRequired.Appearance.Options.UseForeColor = true;
            this.lblRemotePwdRequired.Location = new System.Drawing.Point(376, 134);
            this.lblRemotePwdRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblRemotePwdRequired.Name = "lblRemotePwdRequired";
            this.lblRemotePwdRequired.Size = new System.Drawing.Size(7, 14);
            this.lblRemotePwdRequired.TabIndex = 129;
            this.lblRemotePwdRequired.Text = "*";
            // 
            // txtRemotePasswordConfirmed
            // 
            this.txtRemotePasswordConfirmed.Location = new System.Drawing.Point(117, 130);
            this.txtRemotePasswordConfirmed.Name = "txtRemotePasswordConfirmed";
            this.txtRemotePasswordConfirmed.Properties.MaxLength = 64;
            this.txtRemotePasswordConfirmed.Properties.PasswordChar = '*';
            this.txtRemotePasswordConfirmed.Size = new System.Drawing.Size(247, 20);
            this.txtRemotePasswordConfirmed.TabIndex = 3;
            // 
            // txtRemoteAddress
            // 
            this.txtRemoteAddress.Location = new System.Drawing.Point(117, 36);
            this.txtRemoteAddress.Name = "txtRemoteAddress";
            this.txtRemoteAddress.Properties.MaxLength = 64;
            this.txtRemoteAddress.Size = new System.Drawing.Size(247, 20);
            this.txtRemoteAddress.TabIndex = 0;
            // 
            // txtRemoteUserName
            // 
            this.txtRemoteUserName.Location = new System.Drawing.Point(117, 67);
            this.txtRemoteUserName.Name = "txtRemoteUserName";
            this.txtRemoteUserName.Properties.MaxLength = 64;
            this.txtRemoteUserName.Size = new System.Drawing.Size(247, 20);
            this.txtRemoteUserName.TabIndex = 1;
            // 
            // txtRemotePassword
            // 
            this.txtRemotePassword.Location = new System.Drawing.Point(117, 98);
            this.txtRemotePassword.Name = "txtRemotePassword";
            this.txtRemotePassword.Properties.MaxLength = 64;
            this.txtRemotePassword.Properties.PasswordChar = '*';
            this.txtRemotePassword.Size = new System.Drawing.Size(247, 20);
            this.txtRemotePassword.TabIndex = 2;
            // 
            // lblRemotePasswordConfirmed
            // 
            this.lblRemotePasswordConfirmed.Location = new System.Drawing.Point(50, 131);
            this.lblRemotePasswordConfirmed.Name = "lblRemotePasswordConfirmed";
            this.lblRemotePasswordConfirmed.Size = new System.Drawing.Size(60, 14);
            this.lblRemotePasswordConfirmed.TabIndex = 128;
            this.lblRemotePasswordConfirmed.Text = "确认密码：";
            // 
            // lblRemoteAddress
            // 
            this.lblRemoteAddress.Location = new System.Drawing.Point(26, 38);
            this.lblRemoteAddress.Name = "lblRemoteAddress";
            this.lblRemoteAddress.Size = new System.Drawing.Size(84, 14);
            this.lblRemoteAddress.TabIndex = 127;
            this.lblRemoteAddress.Text = "远程交换地址：";
            // 
            // lblRemoteUserName
            // 
            this.lblRemoteUserName.Location = new System.Drawing.Point(62, 69);
            this.lblRemoteUserName.Name = "lblRemoteUserName";
            this.lblRemoteUserName.Size = new System.Drawing.Size(48, 14);
            this.lblRemoteUserName.TabIndex = 126;
            this.lblRemoteUserName.Text = "用户名：";
            // 
            // lblRemotePassword
            // 
            this.lblRemotePassword.Location = new System.Drawing.Point(74, 100);
            this.lblRemotePassword.Name = "lblRemotePassword";
            this.lblRemotePassword.Size = new System.Drawing.Size(36, 14);
            this.lblRemotePassword.TabIndex = 125;
            this.lblRemotePassword.Text = "密码：";
            // 
            // lblRemoteUserNameRequired
            // 
            this.lblRemoteUserNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblRemoteUserNameRequired.Appearance.Options.UseForeColor = true;
            this.lblRemoteUserNameRequired.Location = new System.Drawing.Point(376, 72);
            this.lblRemoteUserNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblRemoteUserNameRequired.Name = "lblRemoteUserNameRequired";
            this.lblRemoteUserNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblRemoteUserNameRequired.TabIndex = 124;
            this.lblRemoteUserNameRequired.Text = "*";
            // 
            // lblRemotePasswordRequired
            // 
            this.lblRemotePasswordRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblRemotePasswordRequired.Appearance.Options.UseForeColor = true;
            this.lblRemotePasswordRequired.Location = new System.Drawing.Point(376, 103);
            this.lblRemotePasswordRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblRemotePasswordRequired.Name = "lblRemotePasswordRequired";
            this.lblRemotePasswordRequired.Size = new System.Drawing.Size(7, 14);
            this.lblRemotePasswordRequired.TabIndex = 123;
            this.lblRemotePasswordRequired.Text = "*";
            // 
            // separatorControl2
            // 
            this.separatorControl2.Location = new System.Drawing.Point(7, 221);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Size = new System.Drawing.Size(391, 23);
            this.separatorControl2.TabIndex = 139;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(7, 152);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(391, 23);
            this.separatorControl1.TabIndex = 138;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 271);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(403, 42);
            this.pnlBottom.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(216, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(127, 9);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "确认(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // RemoteDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 313);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RemoteDatabaseForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "远程数据库设置";
            this.Load += new System.EventHandler(this.RemoteDatabaseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            this.gcMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btxtDatabaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlnkCancel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbcWaittingBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemotePasswordConfirmed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemoteAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemoteUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemotePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.LabelControl lblTimerValue;
        private DevExpress.XtraEditors.HyperLinkEdit hlnkCancel;
        private DevExpress.XtraEditors.MarqueeProgressBarControl pbcWaittingBar;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        private DevExpress.XtraEditors.LabelControl lblRemoteAddressRequired;
        private DevExpress.XtraEditors.LabelControl lblRemotePwdRequired;
        private DevExpress.XtraEditors.TextEdit txtRemotePasswordConfirmed;
        private DevExpress.XtraEditors.TextEdit txtRemoteAddress;
        private DevExpress.XtraEditors.TextEdit txtRemoteUserName;
        private DevExpress.XtraEditors.TextEdit txtRemotePassword;
        private DevExpress.XtraEditors.LabelControl lblRemotePasswordConfirmed;
        private DevExpress.XtraEditors.LabelControl lblRemoteAddress;
        private DevExpress.XtraEditors.LabelControl lblRemoteUserName;
        private DevExpress.XtraEditors.LabelControl lblRemotePassword;
        private DevExpress.XtraEditors.LabelControl lblRemoteUserNameRequired;
        private DevExpress.XtraEditors.LabelControl lblRemotePasswordRequired;
        private DevExpress.XtraEditors.ButtonEdit btxtDatabaseName;
        private DevExpress.XtraEditors.LabelControl lblDatabaseName;
        private DevExpress.XtraEditors.LabelControl lblDatabaseNameTip;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LabelControl lblProgress;
        private DevExpress.XtraEditors.LabelControl lblOperation;
    }
}