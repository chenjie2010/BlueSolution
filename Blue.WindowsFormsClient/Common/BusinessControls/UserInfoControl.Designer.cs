namespace Blue.WindowsFormsClient.Common.BusinessControls
{
    partial class UserInfoControl
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
            this.pnlUser = new DevExpress.XtraEditors.PanelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.txtUserActualName = new DevExpress.XtraEditors.TextEdit();
            this.lblUserActualName = new DevExpress.XtraEditors.LabelControl();
            this.lblDepartment = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.peUser = new DevExpress.XtraEditors.PictureEdit();
            this.lblRole = new DevExpress.XtraEditors.LabelControl();
            this.txtNewUserName = new DevExpress.XtraEditors.TextEdit();
            this.lblNewUserName = new DevExpress.XtraEditors.LabelControl();
            this.txtNewUserActualName = new DevExpress.XtraEditors.TextEdit();
            this.lblNewUserActualName = new DevExpress.XtraEditors.LabelControl();
            this.lblNewDepartment = new DevExpress.XtraEditors.LabelControl();
            this.txtDepartment = new DevExpress.XtraEditors.TextEdit();
            this.txtUserType = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtRole = new DevExpress.XtraEditors.TextEdit();
            this.btxtNewRole = new DevExpress.XtraEditors.ButtonEdit();
            this.lblNewRole = new DevExpress.XtraEditors.LabelControl();
            this.cmbUserType = new Blue.WindowsFormsClient.TreeDropdownList();
            this.cmbDepartment = new Blue.WindowsFormsClient.TreeDropdownList();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUser)).BeginInit();
            this.pnlUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserActualName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewUserActualName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtNewRole.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlUser
            // 
            this.pnlUser.Controls.Add(this.lblTip);
            this.pnlUser.Controls.Add(this.btxtNewRole);
            this.pnlUser.Controls.Add(this.lblNewRole);
            this.pnlUser.Controls.Add(this.txtRole);
            this.pnlUser.Controls.Add(this.labelControl2);
            this.pnlUser.Controls.Add(this.txtUserType);
            this.pnlUser.Controls.Add(this.txtDepartment);
            this.pnlUser.Controls.Add(this.lblNewDepartment);
            this.pnlUser.Controls.Add(this.txtNewUserActualName);
            this.pnlUser.Controls.Add(this.lblNewUserActualName);
            this.pnlUser.Controls.Add(this.lblNewUserName);
            this.pnlUser.Controls.Add(this.txtNewUserName);
            this.pnlUser.Controls.Add(this.lblRole);
            this.pnlUser.Controls.Add(this.peUser);
            this.pnlUser.Controls.Add(this.labelControl1);
            this.pnlUser.Controls.Add(this.cmbUserType);
            this.pnlUser.Controls.Add(this.cmbDepartment);
            this.pnlUser.Controls.Add(this.lblDepartment);
            this.pnlUser.Controls.Add(this.txtUserActualName);
            this.pnlUser.Controls.Add(this.lblUserActualName);
            this.pnlUser.Controls.Add(this.lblUserName);
            this.pnlUser.Controls.Add(this.txtUserName);
            this.pnlUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUser.Location = new System.Drawing.Point(0, 0);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(788, 203);
            this.pnlUser.TabIndex = 0;
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(21, 16);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(48, 14);
            this.lblUserName.TabIndex = 13;
            this.lblUserName.Text = "用户名：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(73, 13);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(222, 20);
            this.txtUserName.TabIndex = 12;
            // 
            // txtUserActualName
            // 
            this.txtUserActualName.Location = new System.Drawing.Point(73, 46);
            this.txtUserActualName.Name = "txtUserActualName";
            this.txtUserActualName.Properties.ReadOnly = true;
            this.txtUserActualName.Size = new System.Drawing.Size(222, 20);
            this.txtUserActualName.TabIndex = 30;
            // 
            // lblUserActualName
            // 
            this.lblUserActualName.Location = new System.Drawing.Point(33, 49);
            this.lblUserActualName.Name = "lblUserActualName";
            this.lblUserActualName.Size = new System.Drawing.Size(36, 14);
            this.lblUserActualName.TabIndex = 29;
            this.lblUserActualName.Text = "姓名：";
            // 
            // lblDepartment
            // 
            this.lblDepartment.Location = new System.Drawing.Point(9, 82);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(60, 14);
            this.lblDepartment.TabIndex = 37;
            this.lblDepartment.Text = "所属单位：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 115);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 46;
            this.labelControl1.Text = "用户类型：";
            // 
            // peUser
            // 
            this.peUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.peUser.Location = new System.Drawing.Point(658, 8);
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
            // lblRole
            // 
            this.lblRole.Location = new System.Drawing.Point(9, 148);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(60, 14);
            this.lblRole.TabIndex = 48;
            this.lblRole.Text = "用户角色：";
            // 
            // txtNewUserName
            // 
            this.txtNewUserName.Location = new System.Drawing.Point(387, 13);
            this.txtNewUserName.Name = "txtNewUserName";
            this.txtNewUserName.Properties.MaxLength = 32;
            this.txtNewUserName.Size = new System.Drawing.Size(259, 20);
            this.txtNewUserName.TabIndex = 0;
            // 
            // lblNewUserName
            // 
            this.lblNewUserName.Location = new System.Drawing.Point(323, 16);
            this.lblNewUserName.Name = "lblNewUserName";
            this.lblNewUserName.Size = new System.Drawing.Size(60, 14);
            this.lblNewUserName.TabIndex = 54;
            this.lblNewUserName.Text = "新用户名：";
            // 
            // txtNewUserActualName
            // 
            this.txtNewUserActualName.Location = new System.Drawing.Point(387, 46);
            this.txtNewUserActualName.Name = "txtNewUserActualName";
            this.txtNewUserActualName.Properties.MaxLength = 128;
            this.txtNewUserActualName.Size = new System.Drawing.Size(259, 20);
            this.txtNewUserActualName.TabIndex = 1;
            // 
            // lblNewUserActualName
            // 
            this.lblNewUserActualName.Location = new System.Drawing.Point(335, 49);
            this.lblNewUserActualName.Name = "lblNewUserActualName";
            this.lblNewUserActualName.Size = new System.Drawing.Size(48, 14);
            this.lblNewUserActualName.TabIndex = 55;
            this.lblNewUserActualName.Text = "新姓名：";
            // 
            // lblNewDepartment
            // 
            this.lblNewDepartment.Location = new System.Drawing.Point(311, 82);
            this.lblNewDepartment.Name = "lblNewDepartment";
            this.lblNewDepartment.Size = new System.Drawing.Size(72, 14);
            this.lblNewDepartment.TabIndex = 57;
            this.lblNewDepartment.Text = "所属新单位：";
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(73, 79);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Properties.ReadOnly = true;
            this.txtDepartment.Size = new System.Drawing.Size(222, 20);
            this.txtDepartment.TabIndex = 58;
            // 
            // txtUserType
            // 
            this.txtUserType.Location = new System.Drawing.Point(73, 112);
            this.txtUserType.Name = "txtUserType";
            this.txtUserType.Properties.ReadOnly = true;
            this.txtUserType.Size = new System.Drawing.Size(222, 20);
            this.txtUserType.TabIndex = 59;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(311, 115);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 60;
            this.labelControl2.Text = "新用户类型：";
            // 
            // txtRole
            // 
            this.txtRole.Location = new System.Drawing.Point(73, 145);
            this.txtRole.Name = "txtRole";
            this.txtRole.Properties.ReadOnly = true;
            this.txtRole.Size = new System.Drawing.Size(222, 20);
            this.txtRole.TabIndex = 62;
            // 
            // btxtNewRole
            // 
            this.btxtNewRole.Location = new System.Drawing.Point(387, 145);
            this.btxtNewRole.Name = "btxtNewRole";
            this.btxtNewRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtNewRole.Size = new System.Drawing.Size(259, 20);
            this.btxtNewRole.TabIndex = 4;
            this.btxtNewRole.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtNewRole_ButtonPressed);
            // 
            // lblNewRole
            // 
            this.lblNewRole.Location = new System.Drawing.Point(311, 148);
            this.lblNewRole.Name = "lblNewRole";
            this.lblNewRole.Size = new System.Drawing.Size(72, 14);
            this.lblNewRole.TabIndex = 63;
            this.lblNewRole.Text = "新用户角色：";
            // 
            // cmbUserType
            // 
            this.cmbUserType.Location = new System.Drawing.Point(387, 112);
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.OnlySelectedLeaf = true;
            this.cmbUserType.Size = new System.Drawing.Size(259, 20);
            this.cmbUserType.SkinName = "Blue";
            this.cmbUserType.TabIndex = 3;
            this.cmbUserType.TreeDropdownHandler = null;
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.Location = new System.Drawing.Point(387, 79);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.ShowSearch = true;
            this.cmbDepartment.Size = new System.Drawing.Size(259, 20);
            this.cmbDepartment.SkinName = "Blue";
            this.cmbDepartment.TabIndex = 2;
            this.cmbDepartment.TreeDropdownHandler = null;
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
            this.lblTip.Location = new System.Drawing.Point(587, 175);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(189, 20);
            this.lblTip.TabIndex = 64;
            this.lblTip.Text = "提示：不修改的内容为空即可。";
            this.lblTip.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // UserInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlUser);
            this.Name = "UserInfoControl";
            this.Size = new System.Drawing.Size(788, 203);
            this.Load += new System.EventHandler(this.UserInfoControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlUser)).EndInit();
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserActualName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewUserActualName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtNewRole.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlUser;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.TextEdit txtUserActualName;
        private DevExpress.XtraEditors.LabelControl lblUserActualName;
        private TreeDropdownList cmbDepartment;
        private DevExpress.XtraEditors.LabelControl lblDepartment;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private TreeDropdownList cmbUserType;
        private DevExpress.XtraEditors.PictureEdit peUser;
        private DevExpress.XtraEditors.LabelControl lblRole;
        private DevExpress.XtraEditors.TextEdit txtRole;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtUserType;
        private DevExpress.XtraEditors.TextEdit txtDepartment;
        private DevExpress.XtraEditors.LabelControl lblNewDepartment;
        private DevExpress.XtraEditors.TextEdit txtNewUserActualName;
        private DevExpress.XtraEditors.LabelControl lblNewUserActualName;
        private DevExpress.XtraEditors.LabelControl lblNewUserName;
        private DevExpress.XtraEditors.TextEdit txtNewUserName;
        private DevExpress.XtraEditors.ButtonEdit btxtNewRole;
        private DevExpress.XtraEditors.LabelControl lblNewRole;
        private DevExpress.XtraEditors.LabelControl lblTip;
    }
}
