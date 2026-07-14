namespace Blue.WindowsFormsClient
{
    partial class UserSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSettingForm));
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.xtcUserSetting = new DevExpress.XtraTab.XtraTabControl();
            this.xtpNetworkSetting = new DevExpress.XtraTab.XtraTabPage();
            this.peNetworkSetting = new DevExpress.XtraEditors.PictureEdit();
            this.pnlNetwork = new DevExpress.XtraEditors.PanelControl();
            this.pbcWaittingBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.lblStateValue = new DevExpress.XtraEditors.LabelControl();
            this.lblTimerValue = new DevExpress.XtraEditors.LabelControl();
            this.btnTestConnection = new DevExpress.XtraEditors.SimpleButton();
            this.lblServerAddress = new DevExpress.XtraEditors.LabelControl();
            this.lblState = new DevExpress.XtraEditors.LabelControl();
            this.txtServerAddress = new DevExpress.XtraEditors.TextEdit();
            this.lblTimerName = new DevExpress.XtraEditors.LabelControl();
            this.lblWaittingBar = new DevExpress.XtraEditors.LabelControl();
            this.xtpUserRemoval = new DevExpress.XtraTab.XtraTabPage();
            this.peUser = new DevExpress.XtraEditors.PictureEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.clstUser = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.chkSelect = new DevExpress.XtraEditors.CheckEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtcUserSetting)).BeginInit();
            this.xtcUserSetting.SuspendLayout();
            this.xtpNetworkSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peNetworkSetting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlNetwork)).BeginInit();
            this.pnlNetwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbcWaittingBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerAddress.Properties)).BeginInit();
            this.xtpUserRemoval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clstUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelect.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblVersion);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnConfirm);
            this.pnlMain.Controls.Add(this.xtcUserSetting);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(439, 261);
            this.pnlMain.TabIndex = 0;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(6, 235);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(72, 14);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "客户端版本号";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(357, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(274, 230);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确定(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // xtcUserSetting
            // 
            this.xtcUserSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtcUserSetting.Location = new System.Drawing.Point(2, 2);
            this.xtcUserSetting.Name = "xtcUserSetting";
            this.xtcUserSetting.SelectedTabPage = this.xtpNetworkSetting;
            this.xtcUserSetting.Size = new System.Drawing.Size(435, 225);
            this.xtcUserSetting.TabIndex = 1;
            this.xtcUserSetting.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtpNetworkSetting,
            this.xtpUserRemoval});
            // 
            // xtpNetworkSetting
            // 
            this.xtpNetworkSetting.Controls.Add(this.peNetworkSetting);
            this.xtpNetworkSetting.Controls.Add(this.pnlNetwork);
            this.xtpNetworkSetting.Name = "xtpNetworkSetting";
            this.xtpNetworkSetting.Padding = new System.Windows.Forms.Padding(3);
            this.xtpNetworkSetting.Size = new System.Drawing.Size(429, 196);
            this.xtpNetworkSetting.Text = "网络设置";
            // 
            // peNetworkSetting
            // 
            this.peNetworkSetting.Dock = System.Windows.Forms.DockStyle.Left;
            this.peNetworkSetting.EditValue = global::Blue.WindowsFormsClient.Properties.Resources.Login_NetworkSetting;
            this.peNetworkSetting.Location = new System.Drawing.Point(3, 3);
            this.peNetworkSetting.Name = "peNetworkSetting";
            this.peNetworkSetting.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peNetworkSetting.Properties.ZoomAccelerationFactor = 1D;
            this.peNetworkSetting.Size = new System.Drawing.Size(132, 190);
            this.peNetworkSetting.TabIndex = 6;
            // 
            // pnlNetwork
            // 
            this.pnlNetwork.Controls.Add(this.pbcWaittingBar);
            this.pnlNetwork.Controls.Add(this.lblStateValue);
            this.pnlNetwork.Controls.Add(this.lblTimerValue);
            this.pnlNetwork.Controls.Add(this.btnTestConnection);
            this.pnlNetwork.Controls.Add(this.lblServerAddress);
            this.pnlNetwork.Controls.Add(this.lblState);
            this.pnlNetwork.Controls.Add(this.txtServerAddress);
            this.pnlNetwork.Controls.Add(this.lblTimerName);
            this.pnlNetwork.Controls.Add(this.lblWaittingBar);
            this.pnlNetwork.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlNetwork.Location = new System.Drawing.Point(137, 3);
            this.pnlNetwork.Name = "pnlNetwork";
            this.pnlNetwork.Size = new System.Drawing.Size(289, 190);
            this.pnlNetwork.TabIndex = 5;
            // 
            // pbcWaittingBar
            // 
            this.pbcWaittingBar.EditValue = 0;
            this.pbcWaittingBar.Location = new System.Drawing.Point(84, 55);
            this.pbcWaittingBar.Name = "pbcWaittingBar";
            this.pbcWaittingBar.Properties.MarqueeWidth = 30;
            this.pbcWaittingBar.Properties.Stopped = true;
            this.pbcWaittingBar.Size = new System.Drawing.Size(198, 18);
            this.pbcWaittingBar.TabIndex = 7;
            // 
            // lblStateValue
            // 
            this.lblStateValue.Location = new System.Drawing.Point(84, 127);
            this.lblStateValue.Name = "lblStateValue";
            this.lblStateValue.Size = new System.Drawing.Size(48, 14);
            this.lblStateValue.TabIndex = 6;
            this.lblStateValue.Text = "未知状态";
            // 
            // lblTimerValue
            // 
            this.lblTimerValue.Location = new System.Drawing.Point(84, 92);
            this.lblTimerValue.Name = "lblTimerValue";
            this.lblTimerValue.Size = new System.Drawing.Size(7, 14);
            this.lblTimerValue.TabIndex = 5;
            this.lblTimerValue.Text = "0";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(207, 158);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(75, 23);
            this.btnTestConnection.TabIndex = 4;
            this.btnTestConnection.Text = "测试...(&T)";
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // lblServerAddress
            // 
            this.lblServerAddress.Location = new System.Drawing.Point(9, 19);
            this.lblServerAddress.Name = "lblServerAddress";
            this.lblServerAddress.Size = new System.Drawing.Size(72, 14);
            this.lblServerAddress.TabIndex = 0;
            this.lblServerAddress.Text = "服务器地址：";
            // 
            // lblState
            // 
            this.lblState.Location = new System.Drawing.Point(21, 127);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(60, 14);
            this.lblState.TabIndex = 4;
            this.lblState.Text = "连接状态：";
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.Location = new System.Drawing.Point(84, 17);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(198, 20);
            this.txtServerAddress.TabIndex = 1;
            this.txtServerAddress.EditValueChanged += new System.EventHandler(this.txtServerAddress_EditValueChanged);
            // 
            // lblTimerName
            // 
            this.lblTimerName.Location = new System.Drawing.Point(21, 91);
            this.lblTimerName.Name = "lblTimerName";
            this.lblTimerName.Size = new System.Drawing.Size(60, 14);
            this.lblTimerName.TabIndex = 3;
            this.lblTimerName.Text = "测试耗时：";
            // 
            // lblWaittingBar
            // 
            this.lblWaittingBar.Location = new System.Drawing.Point(21, 55);
            this.lblWaittingBar.Name = "lblWaittingBar";
            this.lblWaittingBar.Size = new System.Drawing.Size(60, 14);
            this.lblWaittingBar.TabIndex = 2;
            this.lblWaittingBar.Text = "测试状态：";
            // 
            // xtpUserRemoval
            // 
            this.xtpUserRemoval.Controls.Add(this.peUser);
            this.xtpUserRemoval.Controls.Add(this.panelControl1);
            this.xtpUserRemoval.Name = "xtpUserRemoval";
            this.xtpUserRemoval.Padding = new System.Windows.Forms.Padding(3);
            this.xtpUserRemoval.Size = new System.Drawing.Size(429, 196);
            this.xtpUserRemoval.Text = "用户清除";
            // 
            // peUser
            // 
            this.peUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.peUser.Dock = System.Windows.Forms.DockStyle.Left;
            this.peUser.EditValue = global::Blue.WindowsFormsClient.Properties.Resources.Login_UserSetting;
            this.peUser.Location = new System.Drawing.Point(3, 3);
            this.peUser.Name = "peUser";
            this.peUser.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peUser.Properties.ZoomAccelerationFactor = 1D;
            this.peUser.Size = new System.Drawing.Size(135, 190);
            this.peUser.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.clstUser);
            this.panelControl1.Controls.Add(this.chkSelect);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(140, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(286, 190);
            this.panelControl1.TabIndex = 0;
            // 
            // clstUser
            // 
            this.clstUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.clstUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.clstUser.Location = new System.Drawing.Point(2, 2);
            this.clstUser.Name = "clstUser";
            this.clstUser.Size = new System.Drawing.Size(282, 150);
            this.clstUser.TabIndex = 6;
            // 
            // chkSelect
            // 
            this.chkSelect.Location = new System.Drawing.Point(6, 158);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Properties.Caption = "全选";
            this.chkSelect.Size = new System.Drawing.Size(75, 19);
            this.chkSelect.TabIndex = 5;
            this.chkSelect.CheckedChanged += new System.EventHandler(this.chkSelect_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(203, 158);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "清除(&R)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // UserSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 261);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserSettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户设置";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UserSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtcUserSetting)).EndInit();
            this.xtcUserSetting.ResumeLayout(false);
            this.xtpNetworkSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peNetworkSetting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlNetwork)).EndInit();
            this.pnlNetwork.ResumeLayout(false);
            this.pnlNetwork.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbcWaittingBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerAddress.Properties)).EndInit();
            this.xtpUserRemoval.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clstUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelect.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraTab.XtraTabControl xtcUserSetting;
        private DevExpress.XtraTab.XtraTabPage xtpNetworkSetting;
        private DevExpress.XtraTab.XtraTabPage xtpUserRemoval;
        private DevExpress.XtraEditors.PictureEdit peNetworkSetting;
        private DevExpress.XtraEditors.PanelControl pnlNetwork;
        private DevExpress.XtraEditors.SimpleButton btnTestConnection;
        private DevExpress.XtraEditors.LabelControl lblServerAddress;
        private DevExpress.XtraEditors.LabelControl lblState;
        private DevExpress.XtraEditors.TextEdit txtServerAddress;
        private DevExpress.XtraEditors.LabelControl lblTimerName;
        private DevExpress.XtraEditors.LabelControl lblWaittingBar;
        private DevExpress.XtraEditors.LabelControl lblStateValue;
        private DevExpress.XtraEditors.LabelControl lblTimerValue;
        private DevExpress.XtraEditors.MarqueeProgressBarControl pbcWaittingBar;
        private DevExpress.XtraEditors.PictureEdit peUser;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.CheckEdit chkSelect;
        private DevExpress.XtraEditors.CheckedListBoxControl clstUser;
        private DevExpress.XtraEditors.LabelControl lblVersion;
    }
}