namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class SingleUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleUserForm));
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.sbtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.gcRight = new DevExpress.XtraEditors.GroupControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtNewTelephoneNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtTelephoneNumber = new DevExpress.XtraEditors.TextEdit();
            this.icmbIdentificationType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtNewUserIdentity = new DevExpress.XtraEditors.TextEdit();
            this.txtNewEmailAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtEmailAddress = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtUserIdentity = new DevExpress.XtraEditors.TextEdit();
            this.txtDepValue = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.upfPhoto = new AppFramework.WinFormsControls.DevExpressUploadFile();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.cmbUserType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.btxtNewRole = new DevExpress.XtraEditors.ButtonEdit();
            this.peUser = new DevExpress.XtraEditors.PictureEdit();
            this.lblNewRole = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.txtRole = new DevExpress.XtraEditors.TextEdit();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblUserActualName = new DevExpress.XtraEditors.LabelControl();
            this.txtUserType = new DevExpress.XtraEditors.TextEdit();
            this.txtUserActualName = new DevExpress.XtraEditors.TextEdit();
            this.txtDepartment = new DevExpress.XtraEditors.TextEdit();
            this.lblDepartment = new DevExpress.XtraEditors.LabelControl();
            this.lblNewDepartment = new DevExpress.XtraEditors.LabelControl();
            this.cmbDepartment = new Blue.WindowsFormsClient.TreeDropdownList();
            this.txtNewUserActualName = new DevExpress.XtraEditors.TextEdit();
            this.lblNewUserActualName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblNewUserName = new DevExpress.XtraEditors.LabelControl();
            this.lblRole = new DevExpress.XtraEditors.LabelControl();
            this.txtNewUserName = new DevExpress.XtraEditors.TextEdit();
            this.icIdentificationType = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRight)).BeginInit();
            this.gcRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewTelephoneNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephoneNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbIdentificationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewUserIdentity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewEmailAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserIdentity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtNewRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserActualName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewUserActualName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icIdentificationType)).BeginInit();
            this.SuspendLayout();
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(25, 329);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(810, 23);
            this.separatorControl1.TabIndex = 35;
            // 
            // sbtnCancel
            // 
            this.sbtnCancel.Location = new System.Drawing.Point(391, 355);
            this.sbtnCancel.Name = "sbtnCancel";
            this.sbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.sbtnCancel.TabIndex = 21;
            this.sbtnCancel.Text = "取消(&C)";
            this.sbtnCancel.Click += new System.EventHandler(this.sbtnCancel_Click);
            // 
            // sbtnConfirm
            // 
            this.sbtnConfirm.Location = new System.Drawing.Point(310, 355);
            this.sbtnConfirm.Name = "sbtnConfirm";
            this.sbtnConfirm.Size = new System.Drawing.Size(75, 23);
            this.sbtnConfirm.TabIndex = 20;
            this.sbtnConfirm.Text = "确定(&O)";
            this.sbtnConfirm.Click += new System.EventHandler(this.sbtnConfirm_Click);
            // 
            // gcRight
            // 
            this.gcRight.Controls.Add(this.labelControl8);
            this.gcRight.Controls.Add(this.labelControl9);
            this.gcRight.Controls.Add(this.txtNewTelephoneNumber);
            this.gcRight.Controls.Add(this.txtTelephoneNumber);
            this.gcRight.Controls.Add(this.icmbIdentificationType);
            this.gcRight.Controls.Add(this.labelControl6);
            this.gcRight.Controls.Add(this.labelControl5);
            this.gcRight.Controls.Add(this.labelControl4);
            this.gcRight.Controls.Add(this.txtNewUserIdentity);
            this.gcRight.Controls.Add(this.txtNewEmailAddress);
            this.gcRight.Controls.Add(this.txtEmailAddress);
            this.gcRight.Controls.Add(this.labelControl3);
            this.gcRight.Controls.Add(this.txtUserIdentity);
            this.gcRight.Controls.Add(this.txtDepValue);
            this.gcRight.Controls.Add(this.labelControl7);
            this.gcRight.Controls.Add(this.upfPhoto);
            this.gcRight.Controls.Add(this.labelControl10);
            this.gcRight.Controls.Add(this.cmbUserType);
            this.gcRight.Controls.Add(this.lblTip);
            this.gcRight.Controls.Add(this.btxtNewRole);
            this.gcRight.Controls.Add(this.sbtnConfirm);
            this.gcRight.Controls.Add(this.peUser);
            this.gcRight.Controls.Add(this.sbtnCancel);
            this.gcRight.Controls.Add(this.lblNewRole);
            this.gcRight.Controls.Add(this.txtUserName);
            this.gcRight.Controls.Add(this.txtRole);
            this.gcRight.Controls.Add(this.lblUserName);
            this.gcRight.Controls.Add(this.labelControl2);
            this.gcRight.Controls.Add(this.lblUserActualName);
            this.gcRight.Controls.Add(this.txtUserType);
            this.gcRight.Controls.Add(this.txtUserActualName);
            this.gcRight.Controls.Add(this.txtDepartment);
            this.gcRight.Controls.Add(this.lblDepartment);
            this.gcRight.Controls.Add(this.lblNewDepartment);
            this.gcRight.Controls.Add(this.cmbDepartment);
            this.gcRight.Controls.Add(this.txtNewUserActualName);
            this.gcRight.Controls.Add(this.lblNewUserActualName);
            this.gcRight.Controls.Add(this.labelControl1);
            this.gcRight.Controls.Add(this.lblNewUserName);
            this.gcRight.Controls.Add(this.lblRole);
            this.gcRight.Controls.Add(this.txtNewUserName);
            this.gcRight.Controls.Add(this.separatorControl1);
            this.gcRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRight.Location = new System.Drawing.Point(0, 0);
            this.gcRight.Name = "gcRight";
            this.gcRight.Size = new System.Drawing.Size(833, 389);
            this.gcRight.TabIndex = 10;
            this.gcRight.Text = "用户详细信息";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(13, 173);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(60, 14);
            this.labelControl8.TabIndex = 82;
            this.labelControl8.Text = "手机号码：";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(315, 170);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(72, 14);
            this.labelControl9.TabIndex = 81;
            this.labelControl9.Text = "新手机号码：";
            // 
            // txtNewTelephoneNumber
            // 
            this.txtNewTelephoneNumber.Location = new System.Drawing.Point(391, 167);
            this.txtNewTelephoneNumber.Name = "txtNewTelephoneNumber";
            this.txtNewTelephoneNumber.Properties.MaxLength = 128;
            this.txtNewTelephoneNumber.Size = new System.Drawing.Size(259, 20);
            this.txtNewTelephoneNumber.TabIndex = 15;
            // 
            // txtTelephoneNumber
            // 
            this.txtTelephoneNumber.Location = new System.Drawing.Point(77, 169);
            this.txtTelephoneNumber.Name = "txtTelephoneNumber";
            this.txtTelephoneNumber.Properties.ReadOnly = true;
            this.txtTelephoneNumber.Size = new System.Drawing.Size(222, 20);
            this.txtTelephoneNumber.TabIndex = 5;
            // 
            // icmbIdentificationType
            // 
            this.icmbIdentificationType.Location = new System.Drawing.Point(391, 104);
            this.icmbIdentificationType.Name = "icmbIdentificationType";
            this.icmbIdentificationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbIdentificationType.Properties.SmallImages = this.icIdentificationType;
            this.icmbIdentificationType.Size = new System.Drawing.Size(105, 20);
            this.icmbIdentificationType.TabIndex = 12;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(13, 141);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.TabIndex = 76;
            this.labelControl6.Text = "电子邮件：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(315, 138);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 14);
            this.labelControl5.TabIndex = 75;
            this.labelControl5.Text = "新电子邮件：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(315, 105);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 14);
            this.labelControl4.TabIndex = 74;
            this.labelControl4.Text = "新证件号码：";
            // 
            // txtNewUserIdentity
            // 
            this.txtNewUserIdentity.Location = new System.Drawing.Point(502, 104);
            this.txtNewUserIdentity.Name = "txtNewUserIdentity";
            this.txtNewUserIdentity.Properties.MaxLength = 128;
            this.txtNewUserIdentity.Size = new System.Drawing.Size(148, 20);
            this.txtNewUserIdentity.TabIndex = 13;
            // 
            // txtNewEmailAddress
            // 
            this.txtNewEmailAddress.Location = new System.Drawing.Point(391, 135);
            this.txtNewEmailAddress.Name = "txtNewEmailAddress";
            this.txtNewEmailAddress.Properties.MaxLength = 128;
            this.txtNewEmailAddress.Size = new System.Drawing.Size(259, 20);
            this.txtNewEmailAddress.TabIndex = 14;
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Location = new System.Drawing.Point(77, 137);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Properties.ReadOnly = true;
            this.txtEmailAddress.Size = new System.Drawing.Size(222, 20);
            this.txtEmailAddress.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 107);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 69;
            this.labelControl3.Text = "证件号码：";
            // 
            // txtUserIdentity
            // 
            this.txtUserIdentity.Location = new System.Drawing.Point(77, 104);
            this.txtUserIdentity.Name = "txtUserIdentity";
            this.txtUserIdentity.Properties.ReadOnly = true;
            this.txtUserIdentity.Size = new System.Drawing.Size(222, 20);
            this.txtUserIdentity.TabIndex = 3;
            // 
            // txtDepValue
            // 
            this.txtDepValue.Location = new System.Drawing.Point(77, 232);
            this.txtDepValue.Name = "txtDepValue";
            this.txtDepValue.Properties.MaxLength = 11;
            this.txtDepValue.Properties.ReadOnly = true;
            this.txtDepValue.Size = new System.Drawing.Size(222, 20);
            this.txtDepValue.TabIndex = 7;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(25, 234);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(48, 14);
            this.labelControl7.TabIndex = 68;
            this.labelControl7.Text = "单位值：";
            // 
            // upfPhoto
            // 
            this.upfPhoto.DocType = AppFramework.WinFormsControls.DocType.PicAttachment;
            this.upfPhoto.Location = new System.Drawing.Point(391, 296);
            this.upfPhoto.Name = "upfPhoto";
            this.upfPhoto.ReadOnly = false;
            this.upfPhoto.ShowView = false;
            this.upfPhoto.Size = new System.Drawing.Size(259, 24);
            this.upfPhoto.TabIndex = 19;
            this.upfPhoto.OnBrowseClick += new System.EventHandler<System.EventArgs>(this.upfPhoto_OnBrowseClick);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(327, 298);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(60, 14);
            this.labelControl10.TabIndex = 66;
            this.labelControl10.Text = "上传照片：";
            // 
            // cmbUserType
            // 
            this.cmbUserType.Location = new System.Drawing.Point(391, 234);
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUserType.Size = new System.Drawing.Size(259, 20);
            this.cmbUserType.TabIndex = 17;
            // 
            // lblTip
            // 
            this.lblTip.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblTip.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Client_Common_Alert;
            this.lblTip.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTip.Appearance.Options.UseForeColor = true;
            this.lblTip.Appearance.Options.UseImage = true;
            this.lblTip.Appearance.Options.UseImageAlign = true;
            this.lblTip.Appearance.Options.UseTextOptions = true;
            this.lblTip.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTip.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblTip.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblTip.Location = new System.Drawing.Point(633, 353);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(189, 20);
            this.lblTip.TabIndex = 64;
            this.lblTip.Text = "提示：不修改的内容为空即可。";
            this.lblTip.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // btxtNewRole
            // 
            this.btxtNewRole.Location = new System.Drawing.Point(391, 266);
            this.btxtNewRole.Name = "btxtNewRole";
            this.btxtNewRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtNewRole.Size = new System.Drawing.Size(259, 20);
            this.btxtNewRole.TabIndex = 18;
            this.btxtNewRole.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtNewRole_ButtonPressed);
            // 
            // peUser
            // 
            this.peUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.peUser.Location = new System.Drawing.Point(687, 89);
            this.peUser.Name = "peUser";
            this.peUser.Properties.AllowScrollOnMouseWheel = DevExpress.Utils.DefaultBoolean.False;
            this.peUser.Properties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.False;
            this.peUser.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peUser.Properties.ShowMenu = false;
            this.peUser.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peUser.Properties.ZoomAccelerationFactor = 1D;
            this.peUser.Size = new System.Drawing.Size(118, 161);
            this.peUser.TabIndex = 47;
            // 
            // lblNewRole
            // 
            this.lblNewRole.Location = new System.Drawing.Point(315, 269);
            this.lblNewRole.Name = "lblNewRole";
            this.lblNewRole.Size = new System.Drawing.Size(72, 14);
            this.lblNewRole.TabIndex = 63;
            this.lblNewRole.Text = "新用户角色：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(77, 39);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(222, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // txtRole
            // 
            this.txtRole.Location = new System.Drawing.Point(77, 298);
            this.txtRole.Name = "txtRole";
            this.txtRole.Properties.ReadOnly = true;
            this.txtRole.Size = new System.Drawing.Size(222, 20);
            this.txtRole.TabIndex = 9;
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(25, 42);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(48, 14);
            this.lblUserName.TabIndex = 13;
            this.lblUserName.Text = "用户名：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(315, 236);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 60;
            this.labelControl2.Text = "新用户类型：";
            // 
            // lblUserActualName
            // 
            this.lblUserActualName.Location = new System.Drawing.Point(37, 75);
            this.lblUserActualName.Name = "lblUserActualName";
            this.lblUserActualName.Size = new System.Drawing.Size(36, 14);
            this.lblUserActualName.TabIndex = 29;
            this.lblUserActualName.Text = "姓名：";
            // 
            // txtUserType
            // 
            this.txtUserType.Location = new System.Drawing.Point(77, 265);
            this.txtUserType.Name = "txtUserType";
            this.txtUserType.Properties.ReadOnly = true;
            this.txtUserType.Size = new System.Drawing.Size(222, 20);
            this.txtUserType.TabIndex = 8;
            // 
            // txtUserActualName
            // 
            this.txtUserActualName.Location = new System.Drawing.Point(77, 72);
            this.txtUserActualName.Name = "txtUserActualName";
            this.txtUserActualName.Properties.ReadOnly = true;
            this.txtUserActualName.Size = new System.Drawing.Size(222, 20);
            this.txtUserActualName.TabIndex = 1;
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(77, 200);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Properties.ReadOnly = true;
            this.txtDepartment.Size = new System.Drawing.Size(222, 20);
            this.txtDepartment.TabIndex = 6;
            // 
            // lblDepartment
            // 
            this.lblDepartment.Location = new System.Drawing.Point(13, 203);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(60, 14);
            this.lblDepartment.TabIndex = 37;
            this.lblDepartment.Text = "所属单位：";
            // 
            // lblNewDepartment
            // 
            this.lblNewDepartment.Location = new System.Drawing.Point(315, 203);
            this.lblNewDepartment.Name = "lblNewDepartment";
            this.lblNewDepartment.Size = new System.Drawing.Size(72, 14);
            this.lblNewDepartment.TabIndex = 57;
            this.lblNewDepartment.Text = "所属新单位：";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.Location = new System.Drawing.Point(391, 200);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.ShowSearch = true;
            this.cmbDepartment.Size = new System.Drawing.Size(259, 23);
            this.cmbDepartment.SkinName = "Blue";
            this.cmbDepartment.TabIndex = 16;
            this.cmbDepartment.TreeDropdownHandler = null;
            // 
            // txtNewUserActualName
            // 
            this.txtNewUserActualName.Location = new System.Drawing.Point(391, 72);
            this.txtNewUserActualName.Name = "txtNewUserActualName";
            this.txtNewUserActualName.Properties.MaxLength = 128;
            this.txtNewUserActualName.Size = new System.Drawing.Size(259, 20);
            this.txtNewUserActualName.TabIndex = 11;
            // 
            // lblNewUserActualName
            // 
            this.lblNewUserActualName.Location = new System.Drawing.Point(339, 75);
            this.lblNewUserActualName.Name = "lblNewUserActualName";
            this.lblNewUserActualName.Size = new System.Drawing.Size(48, 14);
            this.lblNewUserActualName.TabIndex = 55;
            this.lblNewUserActualName.Text = "新姓名：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 268);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 46;
            this.labelControl1.Text = "用户类型：";
            // 
            // lblNewUserName
            // 
            this.lblNewUserName.Location = new System.Drawing.Point(327, 42);
            this.lblNewUserName.Name = "lblNewUserName";
            this.lblNewUserName.Size = new System.Drawing.Size(60, 14);
            this.lblNewUserName.TabIndex = 54;
            this.lblNewUserName.Text = "新用户名：";
            // 
            // lblRole
            // 
            this.lblRole.Location = new System.Drawing.Point(13, 301);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(60, 14);
            this.lblRole.TabIndex = 48;
            this.lblRole.Text = "用户角色：";
            // 
            // txtNewUserName
            // 
            this.txtNewUserName.Location = new System.Drawing.Point(391, 39);
            this.txtNewUserName.Name = "txtNewUserName";
            this.txtNewUserName.Properties.MaxLength = 32;
            this.txtNewUserName.Size = new System.Drawing.Size(259, 20);
            this.txtNewUserName.TabIndex = 10;
            // 
            // icIdentificationType
            // 
            this.icIdentificationType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icIdentificationType.ImageStream")));
            this.icIdentificationType.Images.SetKeyName(0, "Control_Identity_One.jpg");
            this.icIdentificationType.Images.SetKeyName(1, "Control_Identity_Two.jpg");
            this.icIdentificationType.Images.SetKeyName(2, "Control_Identity_Other.png");
            // 
            // SingleUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 389);
            this.Controls.Add(this.gcRight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SingleUserForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户信息";
            this.Load += new System.EventHandler(this.SingleUserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRight)).EndInit();
            this.gcRight.ResumeLayout(false);
            this.gcRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewTelephoneNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephoneNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbIdentificationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewUserIdentity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewEmailAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserIdentity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtNewRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserActualName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewUserActualName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icIdentificationType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton sbtnCancel;
        private DevExpress.XtraEditors.SimpleButton sbtnConfirm;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.GroupControl gcRight;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.ButtonEdit btxtNewRole;
        private DevExpress.XtraEditors.PictureEdit peUser;
        private DevExpress.XtraEditors.LabelControl lblNewRole;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.TextEdit txtRole;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblUserActualName;
        private DevExpress.XtraEditors.TextEdit txtUserType;
        private DevExpress.XtraEditors.TextEdit txtUserActualName;
        private DevExpress.XtraEditors.TextEdit txtDepartment;
        private DevExpress.XtraEditors.LabelControl lblDepartment;
        private DevExpress.XtraEditors.LabelControl lblNewDepartment;
        private TreeDropdownList cmbDepartment;
        private DevExpress.XtraEditors.TextEdit txtNewUserActualName;
        private DevExpress.XtraEditors.LabelControl lblNewUserActualName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblNewUserName;
        private DevExpress.XtraEditors.LabelControl lblRole;
        private DevExpress.XtraEditors.TextEdit txtNewUserName;
        private DevExpress.XtraEditors.ComboBoxEdit cmbUserType;
        private AppFramework.WinFormsControls.DevExpressUploadFile upfPhoto;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtDepValue;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtEmailAddress;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtUserIdentity;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtNewUserIdentity;
        private DevExpress.XtraEditors.TextEdit txtNewEmailAddress;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbIdentificationType;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit txtNewTelephoneNumber;
        private DevExpress.XtraEditors.TextEdit txtTelephoneNumber;
        private DevExpress.Utils.ImageCollection icIdentificationType;
    }
}