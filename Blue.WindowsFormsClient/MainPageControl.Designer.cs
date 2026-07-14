namespace Blue.WindowsFormsClient
{
    partial class MainPageControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPageControl));
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.lblUserInfo = new DevExpress.XtraEditors.LabelControl();
            this.icData = new DevExpress.Utils.ImageCollection(this.components);
            this.hlnkMail = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkAccount = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.pnlSpace = new DevExpress.XtraEditors.PanelControl();
            this.pnlUser = new DevExpress.XtraEditors.PanelControl();
            this.hlnkDataSwap = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkUserManagement = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkRefresh = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkMessage = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkSetting = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.lblLoginedTimeValue = new DevExpress.XtraEditors.LabelControl();
            this.lblServerAddressValue = new DevExpress.XtraEditors.LabelControl();
            this.lblDepNameValue = new DevExpress.XtraEditors.LabelControl();
            this.lblUserActualNameValue = new DevExpress.XtraEditors.LabelControl();
            this.lblUserNameValue = new DevExpress.XtraEditors.LabelControl();
            this.peUser = new DevExpress.XtraEditors.PictureEdit();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lblLoginedTime = new DevExpress.XtraEditors.LabelControl();
            this.lblServerAddress = new DevExpress.XtraEditors.LabelControl();
            this.lblDepName = new DevExpress.XtraEditors.LabelControl();
            this.lblUserActualName = new DevExpress.XtraEditors.LabelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUser)).BeginInit();
            this.pnlUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Appearance.Options.UseBackColor = true;
            this.pnlMain.ContentImage = ((System.Drawing.Image)(resources.GetObject("pnlMain.ContentImage")));
            this.pnlMain.ContentImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(736, 556);
            this.pnlMain.TabIndex = 0;
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblUserInfo.Appearance.ImageIndex = 0;
            this.lblUserInfo.Appearance.ImageList = this.icData;
            this.lblUserInfo.Appearance.Options.UseImageAlign = true;
            this.lblUserInfo.Appearance.Options.UseImageIndex = true;
            this.lblUserInfo.Appearance.Options.UseImageList = true;
            this.lblUserInfo.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblUserInfo.Location = new System.Drawing.Point(86, 14);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(69, 20);
            this.lblUserInfo.TabIndex = 6;
            this.lblUserInfo.Text = "用户信息";
            // 
            // icData
            // 
            this.icData.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icData.ImageStream")));
            this.icData.Images.SetKeyName(0, "Common_User_Info.png");
            this.icData.Images.SetKeyName(1, "Common_User_Name.png");
            this.icData.Images.SetKeyName(2, "Common_User_Actual_Name.png");
            this.icData.Images.SetKeyName(3, "Common_User_Group.png");
            this.icData.Images.SetKeyName(4, "Common_System.png");
            this.icData.Images.SetKeyName(5, "Common_Server_Address.png");
            this.icData.Images.SetKeyName(6, "Common_Server_Time.png");
            this.icData.Images.SetKeyName(7, "Common_User_Data.png");
            this.icData.Images.SetKeyName(8, "Common_User_Setting.png");
            this.icData.Images.SetKeyName(9, "Common_User_Notice.png");
            this.icData.Images.SetKeyName(10, "Common_Mail.png");
            this.icData.Images.SetKeyName(11, "Common_Button_Refresh.png");
            this.icData.Images.SetKeyName(12, "Common_User_Group.png");
            this.icData.Images.SetKeyName(13, "BarButtonItem_Swap.png");
            this.icData.Images.SetKeyName(14, "BarButtonItem_Print.png");
            // 
            // hlnkMail
            // 
            this.hlnkMail.Appearance.ImageIndex = 10;
            this.hlnkMail.Appearance.ImageList = this.icData;
            this.hlnkMail.Appearance.Options.UseImageIndex = true;
            this.hlnkMail.Appearance.Options.UseImageList = true;
            this.hlnkMail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkMail.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkMail.Location = new System.Drawing.Point(31, 531);
            this.hlnkMail.Name = "hlnkMail";
            this.hlnkMail.Size = new System.Drawing.Size(69, 20);
            this.hlnkMail.TabIndex = 4;
            this.hlnkMail.Text = "邮件系统";
            this.hlnkMail.Click += new System.EventHandler(this.hlnkMail_Click);
            // 
            // hlnkAccount
            // 
            this.hlnkAccount.Appearance.ImageIndex = 7;
            this.hlnkAccount.Appearance.ImageList = this.icData;
            this.hlnkAccount.Appearance.Options.UseImageIndex = true;
            this.hlnkAccount.Appearance.Options.UseImageList = true;
            this.hlnkAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkAccount.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkAccount.Location = new System.Drawing.Point(31, 444);
            this.hlnkAccount.Name = "hlnkAccount";
            this.hlnkAccount.Size = new System.Drawing.Size(69, 20);
            this.hlnkAccount.TabIndex = 0;
            this.hlnkAccount.Text = "个人资料";
            this.hlnkAccount.Click += new System.EventHandler(this.hlnkAccount_Click);
            // 
            // pnlSpace
            // 
            this.pnlSpace.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlSpace.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSpace.Location = new System.Drawing.Point(739, 3);
            this.pnlSpace.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSpace.Name = "pnlSpace";
            this.pnlSpace.Size = new System.Drawing.Size(4, 556);
            this.pnlSpace.TabIndex = 2;
            // 
            // pnlUser
            // 
            this.pnlUser.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlUser.Appearance.Options.UseBackColor = true;
            this.pnlUser.Controls.Add(this.hlnkDataSwap);
            this.pnlUser.Controls.Add(this.hlnkUserManagement);
            this.pnlUser.Controls.Add(this.hlnkRefresh);
            this.pnlUser.Controls.Add(this.hlnkMessage);
            this.pnlUser.Controls.Add(this.lblUserInfo);
            this.pnlUser.Controls.Add(this.hlnkSetting);
            this.pnlUser.Controls.Add(this.hlnkMail);
            this.pnlUser.Controls.Add(this.hlnkAccount);
            this.pnlUser.Controls.Add(this.lblLoginedTimeValue);
            this.pnlUser.Controls.Add(this.lblServerAddressValue);
            this.pnlUser.Controls.Add(this.lblDepNameValue);
            this.pnlUser.Controls.Add(this.lblUserActualNameValue);
            this.pnlUser.Controls.Add(this.lblUserNameValue);
            this.pnlUser.Controls.Add(this.peUser);
            this.pnlUser.Controls.Add(this.separatorControl1);
            this.pnlUser.Controls.Add(this.labelControl7);
            this.pnlUser.Controls.Add(this.lblLoginedTime);
            this.pnlUser.Controls.Add(this.lblServerAddress);
            this.pnlUser.Controls.Add(this.lblDepName);
            this.pnlUser.Controls.Add(this.lblUserActualName);
            this.pnlUser.Controls.Add(this.lblUserName);
            this.pnlUser.Controls.Add(this.progressPanel);
            this.pnlUser.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlUser.Location = new System.Drawing.Point(743, 3);
            this.pnlUser.Margin = new System.Windows.Forms.Padding(0);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(254, 556);
            this.pnlUser.TabIndex = 1;
            // 
            // hlnkDataSwap
            // 
            this.hlnkDataSwap.Appearance.ImageIndex = 13;
            this.hlnkDataSwap.Appearance.ImageList = this.icData;
            this.hlnkDataSwap.Appearance.Options.UseImageIndex = true;
            this.hlnkDataSwap.Appearance.Options.UseImageList = true;
            this.hlnkDataSwap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkDataSwap.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkDataSwap.Location = new System.Drawing.Point(166, 473);
            this.hlnkDataSwap.Name = "hlnkDataSwap";
            this.hlnkDataSwap.Size = new System.Drawing.Size(69, 20);
            this.hlnkDataSwap.TabIndex = 6;
            this.hlnkDataSwap.Text = "数据转表";
            this.hlnkDataSwap.Click += new System.EventHandler(this.hlnkDataSwap_Click);
            // 
            // hlnkUserManagement
            // 
            this.hlnkUserManagement.Appearance.ImageIndex = 12;
            this.hlnkUserManagement.Appearance.ImageList = this.icData;
            this.hlnkUserManagement.Appearance.Options.UseImageIndex = true;
            this.hlnkUserManagement.Appearance.Options.UseImageList = true;
            this.hlnkUserManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkUserManagement.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkUserManagement.Location = new System.Drawing.Point(166, 444);
            this.hlnkUserManagement.Name = "hlnkUserManagement";
            this.hlnkUserManagement.Size = new System.Drawing.Size(69, 20);
            this.hlnkUserManagement.TabIndex = 5;
            this.hlnkUserManagement.Text = "用户管理";
            this.hlnkUserManagement.Click += new System.EventHandler(this.hlnkUserManagement_Click);
            // 
            // hlnkRefresh
            // 
            this.hlnkRefresh.Appearance.ImageIndex = 11;
            this.hlnkRefresh.Appearance.ImageList = this.icData;
            this.hlnkRefresh.Appearance.Options.UseImageIndex = true;
            this.hlnkRefresh.Appearance.Options.UseImageList = true;
            this.hlnkRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkRefresh.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkRefresh.Location = new System.Drawing.Point(202, 4);
            this.hlnkRefresh.Name = "hlnkRefresh";
            this.hlnkRefresh.Size = new System.Drawing.Size(45, 20);
            this.hlnkRefresh.TabIndex = 0;
            this.hlnkRefresh.Text = "刷新";
            this.hlnkRefresh.Click += new System.EventHandler(this.hlnkRefresh_Click);
            // 
            // hlnkMessage
            // 
            this.hlnkMessage.Appearance.ImageIndex = 9;
            this.hlnkMessage.Appearance.ImageList = this.icData;
            this.hlnkMessage.Appearance.Options.UseImageIndex = true;
            this.hlnkMessage.Appearance.Options.UseImageList = true;
            this.hlnkMessage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkMessage.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkMessage.Location = new System.Drawing.Point(31, 502);
            this.hlnkMessage.Name = "hlnkMessage";
            this.hlnkMessage.Size = new System.Drawing.Size(105, 20);
            this.hlnkMessage.TabIndex = 3;
            this.hlnkMessage.Text = "用户通知与消息";
            this.hlnkMessage.Click += new System.EventHandler(this.hlnkMessage_Click);
            // 
            // hlnkSetting
            // 
            this.hlnkSetting.Appearance.ImageIndex = 8;
            this.hlnkSetting.Appearance.ImageList = this.icData;
            this.hlnkSetting.Appearance.Options.UseImageIndex = true;
            this.hlnkSetting.Appearance.Options.UseImageList = true;
            this.hlnkSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkSetting.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkSetting.Location = new System.Drawing.Point(31, 473);
            this.hlnkSetting.Name = "hlnkSetting";
            this.hlnkSetting.Size = new System.Drawing.Size(69, 20);
            this.hlnkSetting.TabIndex = 2;
            this.hlnkSetting.Text = "用户设置";
            this.hlnkSetting.Click += new System.EventHandler(this.hlnkSetting_Click);
            // 
            // lblLoginedTimeValue
            // 
            this.lblLoginedTimeValue.Location = new System.Drawing.Point(132, 413);
            this.lblLoginedTimeValue.Name = "lblLoginedTimeValue";
            this.lblLoginedTimeValue.Size = new System.Drawing.Size(48, 14);
            this.lblLoginedTimeValue.TabIndex = 32;
            this.lblLoginedTimeValue.Text = "用户名：";
            // 
            // lblServerAddressValue
            // 
            this.lblServerAddressValue.Location = new System.Drawing.Point(132, 383);
            this.lblServerAddressValue.Name = "lblServerAddressValue";
            this.lblServerAddressValue.Size = new System.Drawing.Size(48, 14);
            this.lblServerAddressValue.TabIndex = 31;
            this.lblServerAddressValue.Text = "用户名：";
            // 
            // lblDepNameValue
            // 
            this.lblDepNameValue.Location = new System.Drawing.Point(106, 287);
            this.lblDepNameValue.Name = "lblDepNameValue";
            this.lblDepNameValue.Size = new System.Drawing.Size(48, 14);
            this.lblDepNameValue.TabIndex = 30;
            this.lblDepNameValue.Text = "用户名：";
            // 
            // lblUserActualNameValue
            // 
            this.lblUserActualNameValue.Location = new System.Drawing.Point(106, 254);
            this.lblUserActualNameValue.Name = "lblUserActualNameValue";
            this.lblUserActualNameValue.Size = new System.Drawing.Size(48, 14);
            this.lblUserActualNameValue.TabIndex = 29;
            this.lblUserActualNameValue.Text = "用户名：";
            // 
            // lblUserNameValue
            // 
            this.lblUserNameValue.Location = new System.Drawing.Point(106, 222);
            this.lblUserNameValue.Name = "lblUserNameValue";
            this.lblUserNameValue.Size = new System.Drawing.Size(48, 14);
            this.lblUserNameValue.TabIndex = 28;
            this.lblUserNameValue.Text = "用户名：";
            // 
            // peUser
            // 
            this.peUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.peUser.Location = new System.Drawing.Point(62, 46);
            this.peUser.Name = "peUser";
            this.peUser.Properties.AllowScrollOnMouseWheel = DevExpress.Utils.DefaultBoolean.False;
            this.peUser.Properties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.False;
            this.peUser.Properties.NullText = "用户照片";
            this.peUser.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peUser.Properties.ShowMenu = false;
            this.peUser.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peUser.Properties.ZoomAccelerationFactor = 1D;
            this.peUser.Size = new System.Drawing.Size(118, 161);
            this.peUser.TabIndex = 27;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(8, 314);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(239, 23);
            this.separatorControl1.TabIndex = 8;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.ImageIndex = 4;
            this.labelControl7.Appearance.ImageList = this.icData;
            this.labelControl7.Appearance.Options.UseImageIndex = true;
            this.labelControl7.Appearance.Options.UseImageList = true;
            this.labelControl7.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControl7.Location = new System.Drawing.Point(89, 345);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(69, 20);
            this.labelControl7.TabIndex = 7;
            this.labelControl7.Text = "系统信息";
            // 
            // lblLoginedTime
            // 
            this.lblLoginedTime.Appearance.ImageIndex = 6;
            this.lblLoginedTime.Appearance.ImageList = this.icData;
            this.lblLoginedTime.Appearance.Options.UseImageIndex = true;
            this.lblLoginedTime.Appearance.Options.UseImageList = true;
            this.lblLoginedTime.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblLoginedTime.Location = new System.Drawing.Point(31, 410);
            this.lblLoginedTime.Name = "lblLoginedTime";
            this.lblLoginedTime.Size = new System.Drawing.Size(93, 20);
            this.lblLoginedTime.TabIndex = 5;
            this.lblLoginedTime.Text = "登 录 时 间：";
            // 
            // lblServerAddress
            // 
            this.lblServerAddress.Appearance.ImageIndex = 5;
            this.lblServerAddress.Appearance.ImageList = this.icData;
            this.lblServerAddress.Appearance.Options.UseImageIndex = true;
            this.lblServerAddress.Appearance.Options.UseImageList = true;
            this.lblServerAddress.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblServerAddress.Location = new System.Drawing.Point(31, 380);
            this.lblServerAddress.Name = "lblServerAddress";
            this.lblServerAddress.Size = new System.Drawing.Size(93, 20);
            this.lblServerAddress.TabIndex = 4;
            this.lblServerAddress.Text = "服务器地址：";
            // 
            // lblDepName
            // 
            this.lblDepName.Appearance.ImageIndex = 3;
            this.lblDepName.Appearance.ImageList = this.icData;
            this.lblDepName.Appearance.Options.UseImageIndex = true;
            this.lblDepName.Appearance.Options.UseImageList = true;
            this.lblDepName.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblDepName.Location = new System.Drawing.Point(33, 284);
            this.lblDepName.Name = "lblDepName";
            this.lblDepName.Size = new System.Drawing.Size(69, 20);
            this.lblDepName.TabIndex = 2;
            this.lblDepName.Text = "单   位：";
            // 
            // lblUserActualName
            // 
            this.lblUserActualName.Appearance.ImageIndex = 2;
            this.lblUserActualName.Appearance.ImageList = this.icData;
            this.lblUserActualName.Appearance.Options.UseImageIndex = true;
            this.lblUserActualName.Appearance.Options.UseImageList = true;
            this.lblUserActualName.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblUserActualName.Location = new System.Drawing.Point(33, 251);
            this.lblUserActualName.Name = "lblUserActualName";
            this.lblUserActualName.Size = new System.Drawing.Size(69, 20);
            this.lblUserActualName.TabIndex = 1;
            this.lblUserActualName.Text = "姓   名：";
            // 
            // lblUserName
            // 
            this.lblUserName.Appearance.ImageIndex = 1;
            this.lblUserName.Appearance.ImageList = this.icData;
            this.lblUserName.Appearance.Options.UseImageIndex = true;
            this.lblUserName.Appearance.Options.UseImageList = true;
            this.lblUserName.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblUserName.Location = new System.Drawing.Point(33, 219);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(69, 20);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "用户名：";
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "刷新中......";
            this.progressPanel.Location = new System.Drawing.Point(166, 23);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(83, 22);
            this.progressPanel.TabIndex = 0;
            this.progressPanel.Text = "刷新中......";
            this.progressPanel.Visible = false;
            // 
            // MainPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSpace);
            this.Controls.Add(this.pnlUser);
            this.Name = "MainPageControl";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(1000, 562);
            this.Load += new System.EventHandler(this.MainPageControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUser)).EndInit();
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlSpace;
        private DevExpress.XtraEditors.PanelControl pnlUser;
        private DevExpress.XtraEditors.LabelControl lblLoginedTime;
        private DevExpress.XtraEditors.LabelControl lblServerAddress;
        private DevExpress.XtraEditors.LabelControl lblDepName;
        private DevExpress.XtraEditors.LabelControl lblUserActualName;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl lblUserInfo;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkMail;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkAccount;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.PictureEdit peUser;
        private DevExpress.XtraEditors.LabelControl lblUserNameValue;
        private DevExpress.XtraEditors.LabelControl lblDepNameValue;
        private DevExpress.XtraEditors.LabelControl lblUserActualNameValue;
        private DevExpress.XtraEditors.LabelControl lblLoginedTimeValue;
        private DevExpress.XtraEditors.LabelControl lblServerAddressValue;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkSetting;
        private DevExpress.Utils.ImageCollection icData;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkMessage;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkRefresh;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkDataSwap;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkUserManagement;
    }
}
