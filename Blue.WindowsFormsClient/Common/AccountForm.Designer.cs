namespace Blue.WindowsFormsClient.Common
{
    partial class AccountForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountForm));
            this.gcAccount = new DevExpress.XtraEditors.GroupControl();
            this.lblPasswordValidation = new DevExpress.XtraEditors.LabelControl();
            this.lblPasswordTip = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.txtUserActualName = new DevExpress.XtraEditors.TextEdit();
            this.lblUserActualName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblNameTip = new DevExpress.XtraEditors.LabelControl();
            this.upfPhoto = new AppFramework.WinFormsControls.DevExpressUploadFile();
            this.lblPhoto = new DevExpress.XtraEditors.LabelControl();
            this.txtTelephoneNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtEmailAddress = new DevExpress.XtraEditors.TextEdit();
            this.lblEmailAddress = new DevExpress.XtraEditors.LabelControl();
            this.lblMobilePhone = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.txtConfirmedPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtNewPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblUserPwd = new DevExpress.XtraEditors.LabelControl();
            this.lblConfirmedUserPwd = new DevExpress.XtraEditors.LabelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.gcPhoto = new DevExpress.XtraEditors.GroupControl();
            this.pnlWarning = new DevExpress.XtraEditors.PanelControl();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.peTip = new DevExpress.XtraEditors.PictureEdit();
            this.peUser = new DevExpress.XtraEditors.PictureEdit();
            this.tlpPhoto = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcAccount)).BeginInit();
            this.gcAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserActualName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephoneNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmedPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPhoto)).BeginInit();
            this.gcPhoto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWarning)).BeginInit();
            this.pnlWarning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peTip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcAccount
            // 
            this.gcAccount.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.Account_PersonalInfo_16;
            this.gcAccount.Controls.Add(this.lblPasswordValidation);
            this.gcAccount.Controls.Add(this.lblPasswordTip);
            this.gcAccount.Controls.Add(this.btnCancel);
            this.gcAccount.Controls.Add(this.btnConfirm);
            this.gcAccount.Controls.Add(this.txtUserActualName);
            this.gcAccount.Controls.Add(this.lblUserActualName);
            this.gcAccount.Controls.Add(this.labelControl3);
            this.gcAccount.Controls.Add(this.lblNameTip);
            this.gcAccount.Controls.Add(this.upfPhoto);
            this.gcAccount.Controls.Add(this.lblPhoto);
            this.gcAccount.Controls.Add(this.txtTelephoneNumber);
            this.gcAccount.Controls.Add(this.txtEmailAddress);
            this.gcAccount.Controls.Add(this.lblEmailAddress);
            this.gcAccount.Controls.Add(this.lblMobilePhone);
            this.gcAccount.Controls.Add(this.txtPassword);
            this.gcAccount.Controls.Add(this.lblPassword);
            this.gcAccount.Controls.Add(this.txtConfirmedPassword);
            this.gcAccount.Controls.Add(this.txtNewPassword);
            this.gcAccount.Controls.Add(this.lblUserPwd);
            this.gcAccount.Controls.Add(this.lblConfirmedUserPwd);
            this.gcAccount.Controls.Add(this.lblUserName);
            this.gcAccount.Controls.Add(this.txtUserName);
            this.gcAccount.Controls.Add(this.separatorControl1);
            this.gcAccount.Controls.Add(this.separatorControl2);
            this.gcAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcAccount.Location = new System.Drawing.Point(246, 0);
            this.gcAccount.Name = "gcAccount";
            this.gcAccount.Size = new System.Drawing.Size(428, 356);
            this.gcAccount.TabIndex = 1;
            this.gcAccount.Text = "账户信息";
            // 
            // lblPasswordValidation
            // 
            this.lblPasswordValidation.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Client_Common_Warning_16;
            this.lblPasswordValidation.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPasswordValidation.Appearance.Options.UseImage = true;
            this.lblPasswordValidation.Appearance.Options.UseImageAlign = true;
            this.lblPasswordValidation.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblPasswordValidation.Location = new System.Drawing.Point(17, 284);
            this.lblPasswordValidation.Name = "lblPasswordValidation";
            this.lblPasswordValidation.Size = new System.Drawing.Size(406, 20);
            this.lblPasswordValidation.TabIndex = 61;
            this.lblPasswordValidation.Text = "提示：新密码必须由数字、字母和特殊符号构成，长度范围为8位-16位。";
            // 
            // lblPasswordTip
            // 
            this.lblPasswordTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblPasswordTip.Appearance.Options.UseForeColor = true;
            this.lblPasswordTip.Location = new System.Drawing.Point(398, 69);
            this.lblPasswordTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblPasswordTip.Name = "lblPasswordTip";
            this.lblPasswordTip.Size = new System.Drawing.Size(7, 14);
            this.lblPasswordTip.TabIndex = 60;
            this.lblPasswordTip.Text = "*";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(228, 323);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(137, 323);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "确定(&Q)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtUserActualName
            // 
            this.txtUserActualName.Location = new System.Drawing.Point(83, 166);
            this.txtUserActualName.Name = "txtUserActualName";
            this.txtUserActualName.Properties.MaxLength = 128;
            this.txtUserActualName.Size = new System.Drawing.Size(309, 20);
            this.txtUserActualName.TabIndex = 4;
            this.txtUserActualName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtUserActualName_MouseDown);
            // 
            // lblUserActualName
            // 
            this.lblUserActualName.Location = new System.Drawing.Point(44, 169);
            this.lblUserActualName.Name = "lblUserActualName";
            this.lblUserActualName.Size = new System.Drawing.Size(36, 14);
            this.lblUserActualName.TabIndex = 56;
            this.lblUserActualName.Text = "姓名：";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(231, 166);
            this.labelControl3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(7, 14);
            this.labelControl3.TabIndex = 48;
            this.labelControl3.Text = "*";
            // 
            // lblNameTip
            // 
            this.lblNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameTip.Appearance.Options.UseForeColor = true;
            this.lblNameTip.Location = new System.Drawing.Point(400, 39);
            this.lblNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameTip.Name = "lblNameTip";
            this.lblNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblNameTip.TabIndex = 23;
            this.lblNameTip.Text = "*";
            // 
            // upfPhoto
            // 
            this.upfPhoto.DocType = AppFramework.WinFormsControls.DocType.PicAttachment;
            this.upfPhoto.Location = new System.Drawing.Point(83, 254);
            this.upfPhoto.Name = "upfPhoto";
            this.upfPhoto.ReadOnly = false;
            this.upfPhoto.ShowView = false;
            this.upfPhoto.Size = new System.Drawing.Size(320, 27);
            this.upfPhoto.SkinName = "Blue";
            this.upfPhoto.TabIndex = 7;
            this.upfPhoto.OnBrowseClick += new System.EventHandler<System.EventArgs>(this.upfPhoto_OnBrowseClick);
            this.upfPhoto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.upfPhoto_MouseDown);
            // 
            // lblPhoto
            // 
            this.lblPhoto.Location = new System.Drawing.Point(20, 257);
            this.lblPhoto.Name = "lblPhoto";
            this.lblPhoto.Size = new System.Drawing.Size(60, 14);
            this.lblPhoto.TabIndex = 43;
            this.lblPhoto.Text = "上传照片：";
            // 
            // txtTelephoneNumber
            // 
            this.txtTelephoneNumber.Location = new System.Drawing.Point(83, 225);
            this.txtTelephoneNumber.Name = "txtTelephoneNumber";
            this.txtTelephoneNumber.Properties.MaxLength = 16;
            this.txtTelephoneNumber.Size = new System.Drawing.Size(309, 20);
            this.txtTelephoneNumber.TabIndex = 6;
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Location = new System.Drawing.Point(83, 196);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Properties.MaxLength = 64;
            this.txtEmailAddress.Size = new System.Drawing.Size(309, 20);
            this.txtEmailAddress.TabIndex = 5;
            // 
            // lblEmailAddress
            // 
            this.lblEmailAddress.Location = new System.Drawing.Point(20, 198);
            this.lblEmailAddress.Name = "lblEmailAddress";
            this.lblEmailAddress.Size = new System.Drawing.Size(60, 14);
            this.lblEmailAddress.TabIndex = 39;
            this.lblEmailAddress.Text = "电子邮件：";
            // 
            // lblMobilePhone
            // 
            this.lblMobilePhone.Location = new System.Drawing.Point(20, 228);
            this.lblMobilePhone.Name = "lblMobilePhone";
            this.lblMobilePhone.Size = new System.Drawing.Size(60, 14);
            this.lblMobilePhone.TabIndex = 38;
            this.lblMobilePhone.Text = "手机号码：";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(83, 65);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.MaxLength = 16;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(309, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(44, 68);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(36, 14);
            this.lblPassword.TabIndex = 36;
            this.lblPassword.Text = "密码：";
            // 
            // txtConfirmedPassword
            // 
            this.txtConfirmedPassword.Location = new System.Drawing.Point(83, 136);
            this.txtConfirmedPassword.Name = "txtConfirmedPassword";
            this.txtConfirmedPassword.Properties.MaxLength = 16;
            this.txtConfirmedPassword.Properties.PasswordChar = '*';
            this.txtConfirmedPassword.Size = new System.Drawing.Size(309, 20);
            this.txtConfirmedPassword.TabIndex = 3;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(83, 104);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Properties.MaxLength = 16;
            this.txtNewPassword.Properties.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(309, 20);
            this.txtNewPassword.TabIndex = 2;
            // 
            // lblUserPwd
            // 
            this.lblUserPwd.Location = new System.Drawing.Point(32, 107);
            this.lblUserPwd.Name = "lblUserPwd";
            this.lblUserPwd.Size = new System.Drawing.Size(48, 14);
            this.lblUserPwd.TabIndex = 33;
            this.lblUserPwd.Text = "新密码：";
            // 
            // lblConfirmedUserPwd
            // 
            this.lblConfirmedUserPwd.Location = new System.Drawing.Point(8, 139);
            this.lblConfirmedUserPwd.Name = "lblConfirmedUserPwd";
            this.lblConfirmedUserPwd.Size = new System.Drawing.Size(72, 14);
            this.lblConfirmedUserPwd.TabIndex = 32;
            this.lblConfirmedUserPwd.Text = "新确认密码：";
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(32, 38);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(48, 14);
            this.lblUserName.TabIndex = 31;
            this.lblUserName.Text = "用户名：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(83, 37);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtUserName.Properties.Appearance.Options.UseBackColor = true;
            this.txtUserName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtUserName.Properties.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(309, 18);
            this.txtUserName.TabIndex = 10;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(17, 83);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(386, 23);
            this.separatorControl1.TabIndex = 45;
            // 
            // separatorControl2
            // 
            this.separatorControl2.Location = new System.Drawing.Point(37, 303);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Size = new System.Drawing.Size(386, 23);
            this.separatorControl2.TabIndex = 46;
            // 
            // gcPhoto
            // 
            this.gcPhoto.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.Common_Photo_16;
            this.gcPhoto.Controls.Add(this.pnlWarning);
            this.gcPhoto.Controls.Add(this.peUser);
            this.gcPhoto.Dock = System.Windows.Forms.DockStyle.Left;
            this.gcPhoto.Location = new System.Drawing.Point(0, 0);
            this.gcPhoto.Name = "gcPhoto";
            this.gcPhoto.Size = new System.Drawing.Size(246, 356);
            this.gcPhoto.TabIndex = 0;
            this.gcPhoto.Text = "用户照片";
            // 
            // pnlWarning
            // 
            this.pnlWarning.Controls.Add(this.lblTip);
            this.pnlWarning.Controls.Add(this.peTip);
            this.pnlWarning.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlWarning.Location = new System.Drawing.Point(2, 294);
            this.pnlWarning.Name = "pnlWarning";
            this.pnlWarning.Size = new System.Drawing.Size(242, 60);
            this.pnlWarning.TabIndex = 61;
            // 
            // lblTip
            // 
            this.lblTip.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lblTip.LineVisible = true;
            this.lblTip.Location = new System.Drawing.Point(43, 8);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(192, 42);
            this.lblTip.TabIndex = 2;
            this.lblTip.Text = "提示信息：修改个人信息需要输入密\r\n码进行验证；如不改动密码，新密码\r\n和新确认密码为空即可。";
            // 
            // peTip
            // 
            this.peTip.Cursor = System.Windows.Forms.Cursors.Default;
            this.peTip.EditValue = global::Blue.WindowsFormsClient.Properties.Resources.Tool_Waring;
            this.peTip.Location = new System.Drawing.Point(5, 6);
            this.peTip.Name = "peTip";
            this.peTip.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peTip.Properties.Appearance.Options.UseBackColor = true;
            this.peTip.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peTip.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peTip.Properties.ZoomAccelerationFactor = 1D;
            this.peTip.Size = new System.Drawing.Size(32, 30);
            this.peTip.TabIndex = 0;
            // 
            // peUser
            // 
            this.peUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.peUser.Location = new System.Drawing.Point(63, 66);
            this.peUser.Name = "peUser";
            this.peUser.Properties.AllowScrollOnMouseWheel = DevExpress.Utils.DefaultBoolean.False;
            this.peUser.Properties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.False;
            this.peUser.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peUser.Properties.ShowMenu = false;
            this.peUser.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peUser.Properties.ZoomAccelerationFactor = 1D;
            this.peUser.Size = new System.Drawing.Size(118, 161);
            this.peUser.TabIndex = 0;
            // 
            // tlpPhoto
            // 
            this.tlpPhoto.Rounded = true;
            this.tlpPhoto.ShowBeak = true;
            // 
            // AccountForm
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(674, 356);
            this.Controls.Add(this.gcAccount);
            this.Controls.Add(this.gcPhoto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "AccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "个人信息";
            this.Load += new System.EventHandler(this.AccountForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AccountForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.gcAccount)).EndInit();
            this.gcAccount.ResumeLayout(false);
            this.gcAccount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserActualName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephoneNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmedPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPhoto)).EndInit();
            this.gcPhoto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlWarning)).EndInit();
            this.pnlWarning.ResumeLayout(false);
            this.pnlWarning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peTip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcPhoto;
        private DevExpress.XtraEditors.GroupControl gcAccount;
        private DevExpress.XtraEditors.TextEdit txtConfirmedPassword;
        private DevExpress.XtraEditors.TextEdit txtNewPassword;
        private DevExpress.XtraEditors.LabelControl lblUserPwd;
        private DevExpress.XtraEditors.LabelControl lblConfirmedUserPwd;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl lblPassword;
        private DevExpress.XtraEditors.TextEdit txtTelephoneNumber;
        private DevExpress.XtraEditors.LabelControl lblEmailAddress;
        private DevExpress.XtraEditors.LabelControl lblMobilePhone;
        private AppFramework.WinFormsControls.DevExpressUploadFile upfPhoto;
        private DevExpress.XtraEditors.LabelControl lblPhoto;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblNameTip;
        private DevExpress.XtraEditors.TextEdit txtUserActualName;
        private DevExpress.XtraEditors.LabelControl lblUserActualName;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.LabelControl lblPasswordTip;
        private DevExpress.XtraEditors.TextEdit txtEmailAddress;
        private DevExpress.XtraEditors.PictureEdit peUser;
        private DevExpress.XtraEditors.PanelControl pnlWarning;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.PictureEdit peTip;
        private DevExpress.Utils.ToolTipController tlpPhoto;
        private DevExpress.XtraEditors.LabelControl lblPasswordValidation;
    }
}