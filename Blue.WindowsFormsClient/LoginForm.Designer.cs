namespace Blue.WindowsFormsClient
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.peUser = new DevExpress.XtraEditors.PictureEdit();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imgLoginedStatus = new DevExpress.Utils.ImageCollection(this.components);
            this.btnItmOnline = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmBusy = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmDoNotDisturb = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmLeave = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmHidden = new DevExpress.XtraBars.BarButtonItem();
            this.cmbUserName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ddbStatus = new DevExpress.XtraEditors.DropDownButton();
            this.ppmStatus = new DevExpress.XtraBars.PopupMenu(this.components);
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.chkAutoLogin = new DevExpress.XtraEditors.CheckEdit();
            this.chkRemeber = new DevExpress.XtraEditors.CheckEdit();
            this.hleRegister = new DevExpress.XtraEditors.HyperLinkEdit();
            this.hleRecoverPassword = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.peBanner = new DevExpress.XtraEditors.PictureEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetting = new DevExpress.XtraEditors.SimpleButton();
            this.imgSetting = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLoginedStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppmStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemeber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleRegister.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleRecoverPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peBanner.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.peUser);
            this.pnlMain.Controls.Add(this.cmbUserName);
            this.pnlMain.Controls.Add(this.ddbStatus);
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Controls.Add(this.chkAutoLogin);
            this.pnlMain.Controls.Add(this.chkRemeber);
            this.pnlMain.Controls.Add(this.hleRegister);
            this.pnlMain.Controls.Add(this.hleRecoverPassword);
            this.pnlMain.Controls.Add(this.lblPassword);
            this.pnlMain.Controls.Add(this.lblUserName);
            this.pnlMain.Controls.Add(this.txtPassword);
            this.pnlMain.Controls.Add(this.peBanner);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(351, 189);
            this.pnlMain.TabIndex = 0;
            // 
            // peUser
            // 
            this.peUser.EditValue = global::Blue.WindowsFormsClient.Properties.Resources.Login_Default_User;
            this.peUser.Location = new System.Drawing.Point(5, 83);
            this.peUser.MenuManager = this.barManager;
            this.peUser.Name = "peUser";
            this.peUser.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peUser.Properties.ZoomAccelerationFactor = 1D;
            this.peUser.Size = new System.Drawing.Size(82, 92);
            this.peUser.TabIndex = 20;
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imgLoginedStatus;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItmOnline,
            this.btnItmBusy,
            this.btnItmDoNotDisturb,
            this.btnItmLeave,
            this.btnItmHidden});
            this.barManager.MaxItemId = 5;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(351, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 223);
            this.barDockControlBottom.Size = new System.Drawing.Size(351, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 223);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(351, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 223);
            // 
            // imgLoginedStatus
            // 
            this.imgLoginedStatus.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgLoginedStatus.ImageStream")));
            this.imgLoginedStatus.Images.SetKeyName(0, "Main_State_Online.png");
            this.imgLoginedStatus.Images.SetKeyName(1, "Main_State_Busy.png");
            this.imgLoginedStatus.Images.SetKeyName(2, "Main_State_Leave.png");
            this.imgLoginedStatus.Images.SetKeyName(3, "Main_State_DoNotDisturb.png");
            this.imgLoginedStatus.Images.SetKeyName(4, "Main_State_Hidden.png");
            // 
            // btnItmOnline
            // 
            this.btnItmOnline.Caption = "在线";
            this.btnItmOnline.Id = 0;
            this.btnItmOnline.ImageIndex = 0;
            this.btnItmOnline.Name = "btnItmOnline";
            this.btnItmOnline.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmOnline_ItemClick);
            // 
            // btnItmBusy
            // 
            this.btnItmBusy.Caption = "忙碌";
            this.btnItmBusy.GroupIndex = 1;
            this.btnItmBusy.Id = 1;
            this.btnItmBusy.ImageIndex = 2;
            this.btnItmBusy.Name = "btnItmBusy";
            this.btnItmBusy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmBusy_ItemClick);
            // 
            // btnItmDoNotDisturb
            // 
            this.btnItmDoNotDisturb.Caption = "勿扰";
            this.btnItmDoNotDisturb.GroupIndex = 1;
            this.btnItmDoNotDisturb.Id = 2;
            this.btnItmDoNotDisturb.ImageIndex = 3;
            this.btnItmDoNotDisturb.Name = "btnItmDoNotDisturb";
            this.btnItmDoNotDisturb.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmDoNotDisturb_ItemClick);
            // 
            // btnItmLeave
            // 
            this.btnItmLeave.Caption = "离开";
            this.btnItmLeave.Id = 3;
            this.btnItmLeave.ImageIndex = 1;
            this.btnItmLeave.Name = "btnItmLeave";
            this.btnItmLeave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmLeave_ItemClick);
            // 
            // btnItmHidden
            // 
            this.btnItmHidden.Caption = "隐身";
            this.btnItmHidden.GroupIndex = 2;
            this.btnItmHidden.Id = 4;
            this.btnItmHidden.ImageIndex = 4;
            this.btnItmHidden.Name = "btnItmHidden";
            this.btnItmHidden.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmHidden_ItemClick);
            // 
            // cmbUserName
            // 
            this.cmbUserName.Location = new System.Drawing.Point(141, 83);
            this.cmbUserName.MenuManager = this.barManager;
            this.cmbUserName.Name = "cmbUserName";
            this.cmbUserName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUserName.Properties.MaxLength = 18;
            this.cmbUserName.Size = new System.Drawing.Size(146, 20);
            this.cmbUserName.TabIndex = 0;
            this.cmbUserName.SelectedIndexChanged += new System.EventHandler(this.cmbUserName_SelectedIndexChanged);
            this.cmbUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUserName_KeyPress);
            // 
            // ddbStatus
            // 
            this.ddbStatus.AllowFocus = false;
            this.ddbStatus.DropDownControl = this.ppmStatus;
            this.ddbStatus.ImageIndex = 0;
            this.ddbStatus.ImageList = this.imgLoginedStatus;
            this.ddbStatus.Location = new System.Drawing.Point(142, 155);
            this.ddbStatus.Name = "ddbStatus";
            this.ddbStatus.Size = new System.Drawing.Size(40, 23);
            this.ddbStatus.TabIndex = 4;
            // 
            // ppmStatus
            // 
            this.ppmStatus.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmOnline),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmLeave),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmBusy, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmDoNotDisturb),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmHidden, true)});
            this.ppmStatus.Manager = this.barManager;
            this.ppmStatus.Name = "ppmStatus";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(93, 158);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 14);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "状   态：";
            // 
            // chkAutoLogin
            // 
            this.chkAutoLogin.Location = new System.Drawing.Point(271, 157);
            this.chkAutoLogin.Name = "chkAutoLogin";
            this.chkAutoLogin.Properties.Caption = "自动登录";
            this.chkAutoLogin.Size = new System.Drawing.Size(75, 19);
            this.chkAutoLogin.TabIndex = 6;
            // 
            // chkRemeber
            // 
            this.chkRemeber.Location = new System.Drawing.Point(194, 157);
            this.chkRemeber.Name = "chkRemeber";
            this.chkRemeber.Properties.Caption = "记住密码";
            this.chkRemeber.Size = new System.Drawing.Size(75, 19);
            this.chkRemeber.TabIndex = 5;
            // 
            // hleRegister
            // 
            this.hleRegister.EditValue = "用户注册";
            this.hleRegister.Location = new System.Drawing.Point(289, 84);
            this.hleRegister.Name = "hleRegister";
            this.hleRegister.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleRegister.Properties.Appearance.Options.UseBackColor = true;
            this.hleRegister.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleRegister.Size = new System.Drawing.Size(63, 18);
            this.hleRegister.TabIndex = 8;
            // 
            // hleRecoverPassword
            // 
            this.hleRecoverPassword.EditValue = "找回密码";
            this.hleRecoverPassword.Location = new System.Drawing.Point(289, 120);
            this.hleRecoverPassword.Name = "hleRecoverPassword";
            this.hleRecoverPassword.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleRecoverPassword.Properties.Appearance.Options.UseBackColor = true;
            this.hleRecoverPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleRecoverPassword.Size = new System.Drawing.Size(63, 18);
            this.hleRecoverPassword.TabIndex = 9;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(92, 120);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(48, 14);
            this.lblPassword.TabIndex = 14;
            this.lblPassword.Text = "密   码：";
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(93, 84);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(48, 14);
            this.lblUserName.TabIndex = 13;
            this.lblUserName.Text = "用户名：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(140, 119);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.MaxLength = 32;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(147, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // peBanner
            // 
            this.peBanner.Cursor = System.Windows.Forms.Cursors.Default;
            this.peBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.peBanner.EditValue = global::Blue.WindowsFormsClient.Properties.Resources.Login_Banner;
            this.peBanner.Location = new System.Drawing.Point(2, 2);
            this.peBanner.Name = "peBanner";
            this.peBanner.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peBanner.Properties.ZoomAccelerationFactor = 1D;
            this.peBanner.Size = new System.Drawing.Size(347, 65);
            this.peBanner.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(271, 195);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(188, 195);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "登录(&O)";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.ImageIndex = 0;
            this.btnSetting.ImageList = this.imgSetting;
            this.btnSetting.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSetting.Location = new System.Drawing.Point(4, 195);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 7;
            this.btnSetting.Text = "设置(&S)";
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // imgSetting
            // 
            this.imgSetting.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSetting.ImageStream")));
            this.imgSetting.Images.SetKeyName(0, "Login_Setting.png");
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(351, 223);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLoginedStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppmStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemeber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleRegister.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleRecoverPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peBanner.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSetting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.HyperLinkEdit hleRecoverPassword;
        private DevExpress.XtraEditors.LabelControl lblPassword;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.PictureEdit peBanner;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.SimpleButton btnSetting;
        private DevExpress.XtraEditors.HyperLinkEdit hleRegister;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.CheckEdit chkAutoLogin;
        private DevExpress.XtraEditors.CheckEdit chkRemeber;
        private DevExpress.XtraEditors.DropDownButton ddbStatus;
        private DevExpress.Utils.ImageCollection imgLoginedStatus;
        private DevExpress.XtraBars.PopupMenu ppmStatus;
        private DevExpress.XtraBars.BarButtonItem btnItmOnline;
        private DevExpress.XtraBars.BarButtonItem btnItmLeave;
        private DevExpress.XtraBars.BarButtonItem btnItmBusy;
        private DevExpress.XtraBars.BarButtonItem btnItmDoNotDisturb;
        private DevExpress.XtraBars.BarButtonItem btnItmHidden;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imgSetting;
        private DevExpress.XtraEditors.ComboBoxEdit cmbUserName;
        private DevExpress.XtraEditors.PictureEdit peUser;
    }
}