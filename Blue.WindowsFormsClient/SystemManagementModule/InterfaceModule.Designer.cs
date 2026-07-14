namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class InterfaceModule
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaceModule));
            this.txtInterfaceCode = new DevExpress.XtraEditors.TextEdit();
            this.lblInterfaceIdentifier = new DevExpress.XtraEditors.LabelControl();
            this.lblInterfaceCode = new DevExpress.XtraEditors.LabelControl();
            this.txtInterfaceName = new DevExpress.XtraEditors.TextEdit();
            this.lblInterfaceName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.lblUserNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.lblTableNameTip = new DevExpress.XtraEditors.LabelControl();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lblInterfaceIdentifierTip = new DevExpress.XtraEditors.LabelControl();
            this.lblDepContained = new DevExpress.XtraEditors.LabelControl();
            this.icmbTableType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icTableType = new DevExpress.Utils.ImageCollection(this.components);
            this.lblUserTypeContained = new DevExpress.XtraEditors.LabelControl();
            this.btxtTableName = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btxtUserName = new DevExpress.XtraEditors.ButtonEdit();
            this.lblAuditorRequired = new DevExpress.XtraEditors.LabelControl();
            this.chkUserTypeContained = new DevExpress.XtraEditors.CheckEdit();
            this.chkDepContained = new DevExpress.XtraEditors.CheckEdit();
            this.txtInterfaceIdentifier = new DevExpress.XtraEditors.TextEdit();
            this.chkActived = new DevExpress.XtraEditors.CheckEdit();
            this.lblActived = new DevExpress.XtraEditors.LabelControl();
            this.hlnkAutoGenerate = new DevExpress.XtraEditors.HyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterfaceCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterfaceName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtTableName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserTypeContained.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDepContained.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterfaceIdentifier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActived.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlnkAutoGenerate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtInterfaceCode
            // 
            this.txtInterfaceCode.Location = new System.Drawing.Point(80, 40);
            this.txtInterfaceCode.Name = "txtInterfaceCode";
            this.txtInterfaceCode.Properties.MaxLength = 32;
            this.txtInterfaceCode.Properties.ReadOnly = true;
            this.txtInterfaceCode.Size = new System.Drawing.Size(152, 20);
            this.txtInterfaceCode.TabIndex = 2;
            // 
            // lblInterfaceIdentifier
            // 
            this.lblInterfaceIdentifier.Location = new System.Drawing.Point(25, 105);
            this.lblInterfaceIdentifier.Name = "lblInterfaceIdentifier";
            this.lblInterfaceIdentifier.Size = new System.Drawing.Size(48, 14);
            this.lblInterfaceIdentifier.TabIndex = 17;
            this.lblInterfaceIdentifier.Text = "标识符：";
            // 
            // lblInterfaceCode
            // 
            this.lblInterfaceCode.Location = new System.Drawing.Point(13, 43);
            this.lblInterfaceCode.Name = "lblInterfaceCode";
            this.lblInterfaceCode.Size = new System.Drawing.Size(60, 14);
            this.lblInterfaceCode.TabIndex = 13;
            this.lblInterfaceCode.Text = "接口信息：";
            // 
            // txtInterfaceName
            // 
            this.txtInterfaceName.Location = new System.Drawing.Point(80, 10);
            this.txtInterfaceName.Name = "txtInterfaceName";
            this.txtInterfaceName.Properties.MaxLength = 64;
            this.txtInterfaceName.Size = new System.Drawing.Size(282, 20);
            this.txtInterfaceName.TabIndex = 1;
            // 
            // lblInterfaceName
            // 
            this.lblInterfaceName.Location = new System.Drawing.Point(13, 12);
            this.lblInterfaceName.Name = "lblInterfaceName";
            this.lblInterfaceName.Size = new System.Drawing.Size(60, 14);
            this.lblInterfaceName.TabIndex = 15;
            this.lblInterfaceName.Text = "接口名称：";
            // 
            // lblNameRequired
            // 
            this.lblNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameRequired.Appearance.Options.UseForeColor = true;
            this.lblNameRequired.Location = new System.Drawing.Point(367, 14);
            this.lblNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameRequired.Name = "lblNameRequired";
            this.lblNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblNameRequired.TabIndex = 22;
            this.lblNameRequired.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(80, 258);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(282, 74);
            this.txtNotes.TabIndex = 10;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(37, 259);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // lblUserNameRequired
            // 
            this.lblUserNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblUserNameRequired.Appearance.Options.UseForeColor = true;
            this.lblUserNameRequired.Location = new System.Drawing.Point(367, 203);
            this.lblUserNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblUserNameRequired.Name = "lblUserNameRequired";
            this.lblUserNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblUserNameRequired.TabIndex = 24;
            this.lblUserNameRequired.Text = "*";
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(13, 198);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(60, 14);
            this.lblUserName.TabIndex = 48;
            this.lblUserName.Text = "验证账号：";
            // 
            // lblTableNameTip
            // 
            this.lblTableNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblTableNameTip.Appearance.Options.UseForeColor = true;
            this.lblTableNameTip.Location = new System.Drawing.Point(367, 75);
            this.lblTableNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblTableNameTip.Name = "lblTableNameTip";
            this.lblTableNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblTableNameTip.TabIndex = 50;
            this.lblTableNameTip.Text = "*";
            // 
            // lnkDetailedView
            // 
            this.lnkDetailedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedView.EditValue = "接口详情";
            this.lnkDetailedView.Location = new System.Drawing.Point(77, 338);
            this.lnkDetailedView.Name = "lnkDetailedView";
            this.lnkDetailedView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedView.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedView.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedView.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedView.TabIndex = 10;
            // 
            // lblInterfaceIdentifierTip
            // 
            this.lblInterfaceIdentifierTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblInterfaceIdentifierTip.Appearance.Options.UseForeColor = true;
            this.lblInterfaceIdentifierTip.Location = new System.Drawing.Point(367, 110);
            this.lblInterfaceIdentifierTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblInterfaceIdentifierTip.Name = "lblInterfaceIdentifierTip";
            this.lblInterfaceIdentifierTip.Size = new System.Drawing.Size(7, 14);
            this.lblInterfaceIdentifierTip.TabIndex = 70;
            this.lblInterfaceIdentifierTip.Text = "*";
            // 
            // lblDepContained
            // 
            this.lblDepContained.Location = new System.Drawing.Point(13, 167);
            this.lblDepContained.Name = "lblDepContained";
            this.lblDepContained.Size = new System.Drawing.Size(60, 14);
            this.lblDepContained.TabIndex = 79;
            this.lblDepContained.Text = "单位类型：";
            // 
            // icmbTableType
            // 
            this.icmbTableType.Location = new System.Drawing.Point(236, 40);
            this.icmbTableType.Name = "icmbTableType";
            this.icmbTableType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbTableType.Properties.SmallImages = this.icTableType;
            this.icmbTableType.Size = new System.Drawing.Size(126, 20);
            this.icmbTableType.TabIndex = 3;
            this.icmbTableType.SelectedIndexChanged += new System.EventHandler(this.icmbTableType_SelectedIndexChanged);
            // 
            // icTableType
            // 
            this.icTableType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTableType.ImageStream")));
            this.icTableType.Images.SetKeyName(0, "Business_Table.png");
            this.icTableType.Images.SetKeyName(1, "BarButtonItem_View.png");
            this.icTableType.Images.SetKeyName(2, "Business_System_Table.png");
            // 
            // lblUserTypeContained
            // 
            this.lblUserTypeContained.Location = new System.Drawing.Point(13, 136);
            this.lblUserTypeContained.Name = "lblUserTypeContained";
            this.lblUserTypeContained.Size = new System.Drawing.Size(60, 14);
            this.lblUserTypeContained.TabIndex = 82;
            this.lblUserTypeContained.Text = "用户类型：";
            // 
            // btxtTableName
            // 
            this.btxtTableName.Location = new System.Drawing.Point(80, 70);
            this.btxtTableName.Name = "btxtTableName";
            this.btxtTableName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtTableName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtTableName.Size = new System.Drawing.Size(282, 20);
            this.btxtTableName.TabIndex = 4;
            this.btxtTableName.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtTableName_ButtonPressed);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 74);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 84;
            this.labelControl1.Text = "表格名称：";
            // 
            // btxtUserName
            // 
            this.btxtUserName.Location = new System.Drawing.Point(80, 196);
            this.btxtUserName.Name = "btxtUserName";
            this.btxtUserName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtUserName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtUserName.Size = new System.Drawing.Size(280, 20);
            this.btxtUserName.TabIndex = 8;
            this.btxtUserName.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtUserName_ButtonPressed);
            // 
            // lblAuditorRequired
            // 
            this.lblAuditorRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblAuditorRequired.Appearance.Options.UseForeColor = true;
            this.lblAuditorRequired.Location = new System.Drawing.Point(367, 46);
            this.lblAuditorRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblAuditorRequired.Name = "lblAuditorRequired";
            this.lblAuditorRequired.Size = new System.Drawing.Size(7, 14);
            this.lblAuditorRequired.TabIndex = 101;
            this.lblAuditorRequired.Text = "*";
            this.lblAuditorRequired.Visible = false;
            // 
            // chkUserTypeContained
            // 
            this.chkUserTypeContained.Location = new System.Drawing.Point(80, 134);
            this.chkUserTypeContained.Name = "chkUserTypeContained";
            this.chkUserTypeContained.Properties.Caption = "启用用户类型条件";
            this.chkUserTypeContained.Size = new System.Drawing.Size(124, 19);
            this.chkUserTypeContained.TabIndex = 6;
            // 
            // chkDepContained
            // 
            this.chkDepContained.Location = new System.Drawing.Point(80, 164);
            this.chkDepContained.Name = "chkDepContained";
            this.chkDepContained.Properties.Caption = "启用单位条件";
            this.chkDepContained.Size = new System.Drawing.Size(102, 19);
            this.chkDepContained.TabIndex = 7;
            // 
            // txtInterfaceIdentifier
            // 
            this.txtInterfaceIdentifier.Location = new System.Drawing.Point(80, 104);
            this.txtInterfaceIdentifier.Name = "txtInterfaceIdentifier";
            this.txtInterfaceIdentifier.Properties.MaxLength = 64;
            this.txtInterfaceIdentifier.Properties.ReadOnly = true;
            this.txtInterfaceIdentifier.Size = new System.Drawing.Size(206, 20);
            this.txtInterfaceIdentifier.TabIndex = 5;
            // 
            // chkActived
            // 
            this.chkActived.Location = new System.Drawing.Point(80, 227);
            this.chkActived.Name = "chkActived";
            this.chkActived.Properties.Caption = "启用接口";
            this.chkActived.Size = new System.Drawing.Size(102, 19);
            this.chkActived.TabIndex = 9;
            // 
            // lblActived
            // 
            this.lblActived.Location = new System.Drawing.Point(13, 229);
            this.lblActived.Name = "lblActived";
            this.lblActived.Size = new System.Drawing.Size(60, 14);
            this.lblActived.TabIndex = 103;
            this.lblActived.Text = "接口状态：";
            // 
            // hlnkAutoGenerate
            // 
            this.hlnkAutoGenerate.EditValue = "自动生成...";
            this.hlnkAutoGenerate.Enabled = false;
            this.hlnkAutoGenerate.Location = new System.Drawing.Point(290, 105);
            this.hlnkAutoGenerate.Name = "hlnkAutoGenerate";
            this.hlnkAutoGenerate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hlnkAutoGenerate.Properties.Appearance.Options.UseBackColor = true;
            this.hlnkAutoGenerate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hlnkAutoGenerate.Properties.ReadOnly = true;
            this.hlnkAutoGenerate.Size = new System.Drawing.Size(70, 18);
            this.hlnkAutoGenerate.TabIndex = 5;
            this.hlnkAutoGenerate.Click += new System.EventHandler(this.hlnkAutoGenerate_Click);
            // 
            // InterfaceModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hlnkAutoGenerate);
            this.Controls.Add(this.chkActived);
            this.Controls.Add(this.lblActived);
            this.Controls.Add(this.txtInterfaceIdentifier);
            this.Controls.Add(this.chkDepContained);
            this.Controls.Add(this.chkUserTypeContained);
            this.Controls.Add(this.lblAuditorRequired);
            this.Controls.Add(this.btxtUserName);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btxtTableName);
            this.Controls.Add(this.lblUserTypeContained);
            this.Controls.Add(this.icmbTableType);
            this.Controls.Add(this.lblDepContained);
            this.Controls.Add(this.lblInterfaceIdentifierTip);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.lblTableNameTip);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblUserNameRequired);
            this.Controls.Add(this.lblNameRequired);
            this.Controls.Add(this.txtInterfaceCode);
            this.Controls.Add(this.lblInterfaceIdentifier);
            this.Controls.Add(this.lblInterfaceCode);
            this.Controls.Add(this.txtInterfaceName);
            this.Controls.Add(this.lblInterfaceName);
            this.Name = "InterfaceModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.InterfaceModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtInterfaceCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterfaceName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtTableName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserTypeContained.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDepContained.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterfaceIdentifier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActived.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hlnkAutoGenerate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtInterfaceCode;
        private DevExpress.XtraEditors.LabelControl lblInterfaceCode;
        private DevExpress.XtraEditors.TextEdit txtInterfaceName;
        private DevExpress.XtraEditors.LabelControl lblNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.LabelControl lblUserNameRequired;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.LabelControl lblTableNameTip;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.LabelControl lblInterfaceIdentifierTip;
        private DevExpress.XtraEditors.LabelControl lblDepContained;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbTableType;
        private DevExpress.XtraEditors.LabelControl lblUserTypeContained;
        private DevExpress.XtraEditors.LabelControl lblInterfaceName;
        private DevExpress.XtraEditors.ButtonEdit btxtTableName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.ImageCollection icTableType;
        private DevExpress.XtraEditors.ButtonEdit btxtUserName;
        private DevExpress.XtraEditors.LabelControl lblAuditorRequired;
        private DevExpress.XtraEditors.CheckEdit chkUserTypeContained;
        private DevExpress.XtraEditors.CheckEdit chkDepContained;
        private DevExpress.XtraEditors.LabelControl lblInterfaceIdentifier;
        private DevExpress.XtraEditors.TextEdit txtInterfaceIdentifier;
        private DevExpress.XtraEditors.CheckEdit chkActived;
        private DevExpress.XtraEditors.LabelControl lblActived;
        private DevExpress.XtraEditors.HyperLinkEdit hlnkAutoGenerate;
    }
}
