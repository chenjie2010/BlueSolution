namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class MenuModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuModule));
            this.lblToolTip = new DevExpress.XtraEditors.LabelControl();
            this.txtMenuCode = new DevExpress.XtraEditors.TextEdit();
            this.lblMenuCode = new DevExpress.XtraEditors.LabelControl();
            this.txtMenuName = new DevExpress.XtraEditors.TextEdit();
            this.lblMenuName = new DevExpress.XtraEditors.LabelControl();
            this.lblMenuNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtToolTip = new DevExpress.XtraEditors.MemoEdit();
            this.lblMenuCodeRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.lblURL = new DevExpress.XtraEditors.LabelControl();
            this.txtMenuURL = new DevExpress.XtraEditors.TextEdit();
            this.lblMenuIcon = new DevExpress.XtraEditors.LabelControl();
            this.icmbIconType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icIconType = new DevExpress.Utils.ImageCollection(this.components);
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.icmbMenuIcon = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icSystemIcon = new DevExpress.Utils.ImageCollection(this.components);
            this.devExpressUploadFile = new AppFramework.WinFormsControls.DevExpressUploadFile();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.lblIconTip = new DevExpress.XtraEditors.LabelControl();
            this.lnkWebIcon = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.txtMenuIconName = new DevExpress.XtraEditors.TextEdit();
            this.lblMenuIconName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtMenuCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMenuName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToolTip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMenuURL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbIconType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icIconType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbMenuIcon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icSystemIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMenuIconName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblToolTip
            // 
            this.lblToolTip.Location = new System.Drawing.Point(35, 244);
            this.lblToolTip.Name = "lblToolTip";
            this.lblToolTip.Size = new System.Drawing.Size(36, 14);
            this.lblToolTip.TabIndex = 21;
            this.lblToolTip.Text = "提示：";
            // 
            // txtMenuCode
            // 
            this.txtMenuCode.Location = new System.Drawing.Point(77, 51);
            this.txtMenuCode.Name = "txtMenuCode";
            this.txtMenuCode.Properties.MaxLength = 64;
            this.txtMenuCode.Properties.ReadOnly = true;
            this.txtMenuCode.Size = new System.Drawing.Size(281, 20);
            this.txtMenuCode.TabIndex = 2;
            // 
            // lblMenuCode
            // 
            this.lblMenuCode.Location = new System.Drawing.Point(11, 53);
            this.lblMenuCode.Name = "lblMenuCode";
            this.lblMenuCode.Size = new System.Drawing.Size(60, 14);
            this.lblMenuCode.TabIndex = 13;
            this.lblMenuCode.Text = "菜单编码：";
            // 
            // txtMenuName
            // 
            this.txtMenuName.Location = new System.Drawing.Point(77, 16);
            this.txtMenuName.Name = "txtMenuName";
            this.txtMenuName.Properties.MaxLength = 64;
            this.txtMenuName.Size = new System.Drawing.Size(282, 20);
            this.txtMenuName.TabIndex = 1;
            // 
            // lblMenuName
            // 
            this.lblMenuName.Location = new System.Drawing.Point(11, 18);
            this.lblMenuName.Name = "lblMenuName";
            this.lblMenuName.Size = new System.Drawing.Size(60, 14);
            this.lblMenuName.TabIndex = 15;
            this.lblMenuName.Text = "菜单名称：";
            // 
            // lblMenuNameRequired
            // 
            this.lblMenuNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblMenuNameRequired.Appearance.Options.UseForeColor = true;
            this.lblMenuNameRequired.Location = new System.Drawing.Point(364, 19);
            this.lblMenuNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblMenuNameRequired.Name = "lblMenuNameRequired";
            this.lblMenuNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblMenuNameRequired.TabIndex = 22;
            this.lblMenuNameRequired.Text = "*";
            // 
            // txtToolTip
            // 
            this.txtToolTip.Location = new System.Drawing.Point(77, 240);
            this.txtToolTip.Name = "txtToolTip";
            this.txtToolTip.Properties.MaxLength = 256;
            this.txtToolTip.Size = new System.Drawing.Size(280, 66);
            this.txtToolTip.TabIndex = 5;
            // 
            // lblMenuCodeRequired
            // 
            this.lblMenuCodeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblMenuCodeRequired.Appearance.Options.UseForeColor = true;
            this.lblMenuCodeRequired.Location = new System.Drawing.Point(364, 55);
            this.lblMenuCodeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblMenuCodeRequired.Name = "lblMenuCodeRequired";
            this.lblMenuCodeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblMenuCodeRequired.TabIndex = 24;
            this.lblMenuCodeRequired.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(77, 319);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 36);
            this.txtNotes.TabIndex = 6;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(35, 322);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // lblURL
            // 
            this.lblURL.Location = new System.Drawing.Point(14, 177);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(57, 14);
            this.lblURL.TabIndex = 27;
            this.lblURL.Text = "URL地址：";
            // 
            // txtMenuURL
            // 
            this.txtMenuURL.Location = new System.Drawing.Point(77, 175);
            this.txtMenuURL.Name = "txtMenuURL";
            this.txtMenuURL.Properties.MaxLength = 64;
            this.txtMenuURL.Size = new System.Drawing.Size(281, 20);
            this.txtMenuURL.TabIndex = 4;
            // 
            // lblMenuIcon
            // 
            this.lblMenuIcon.Location = new System.Drawing.Point(11, 123);
            this.lblMenuIcon.Name = "lblMenuIcon";
            this.lblMenuIcon.Size = new System.Drawing.Size(60, 14);
            this.lblMenuIcon.TabIndex = 29;
            this.lblMenuIcon.Text = "菜单图标：";
            // 
            // icmbIconType
            // 
            this.icmbIconType.Location = new System.Drawing.Point(77, 86);
            this.icmbIconType.Name = "icmbIconType";
            this.icmbIconType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbIconType.Properties.SmallImages = this.icIconType;
            this.icmbIconType.Size = new System.Drawing.Size(280, 20);
            this.icmbIconType.TabIndex = 31;
            this.icmbIconType.SelectedIndexChanged += new System.EventHandler(this.icmbIconType_SelectedIndexChanged);
            // 
            // icIconType
            // 
            this.icIconType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icIconType.ImageStream")));
            this.icIconType.Images.SetKeyName(0, "Common_IconType_System.png");
            this.icIconType.Images.SetKeyName(1, "Common_IconType_User.png");
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 89);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 33;
            this.labelControl2.Text = "图标类型：";
            // 
            // icmbMenuIcon
            // 
            this.icmbMenuIcon.Location = new System.Drawing.Point(77, 120);
            this.icmbMenuIcon.Name = "icmbMenuIcon";
            this.icmbMenuIcon.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbMenuIcon.Properties.SmallImages = this.icSystemIcon;
            this.icmbMenuIcon.Size = new System.Drawing.Size(280, 20);
            this.icmbMenuIcon.TabIndex = 34;
            // 
            // icSystemIcon
            // 
            this.icSystemIcon.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icSystemIcon.ImageStream")));
            this.icSystemIcon.Images.SetKeyName(0, "SystemIcon_0_Small.png");
            this.icSystemIcon.Images.SetKeyName(1, "SystemIcon_1_Small.png");
            this.icSystemIcon.Images.SetKeyName(2, "SystemIcon_2_Small.png");
            // 
            // devExpressUploadFile
            // 
            this.devExpressUploadFile.DocType = AppFramework.WinFormsControls.DocType.PicAttachment;
            this.devExpressUploadFile.Location = new System.Drawing.Point(78, 119);
            this.devExpressUploadFile.Name = "devExpressUploadFile";
            this.devExpressUploadFile.ReadOnly = false;
            this.devExpressUploadFile.Size = new System.Drawing.Size(280, 23);
            this.devExpressUploadFile.TabIndex = 30;
            this.devExpressUploadFile.Visible = false;
            this.devExpressUploadFile.OnViewLinkClicked += new System.EventHandler<System.EventArgs>(this.devExpressUploadFile_OnViewLinkClicked);
            // 
            // lblTip
            // 
            this.lblTip.Location = new System.Drawing.Point(78, 149);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(238, 14);
            this.lblTip.TabIndex = 35;
            this.lblTip.Text = "提示：上传图片要求PNG格式，大小32*32。";
            // 
            // lblIconTip
            // 
            this.lblIconTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblIconTip.Appearance.Options.UseForeColor = true;
            this.lblIconTip.Location = new System.Drawing.Point(364, 126);
            this.lblIconTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblIconTip.Name = "lblIconTip";
            this.lblIconTip.Size = new System.Drawing.Size(7, 14);
            this.lblIconTip.TabIndex = 36;
            this.lblIconTip.Text = "*";
            // 
            // lnkWebIcon
            // 
            this.lnkWebIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkWebIcon.Location = new System.Drawing.Point(297, 209);
            this.lnkWebIcon.Name = "lnkWebIcon";
            this.lnkWebIcon.Size = new System.Drawing.Size(48, 14);
            this.lnkWebIcon.TabIndex = 45;
            this.lnkWebIcon.Text = "图标网址";
            this.lnkWebIcon.HyperlinkClick += new DevExpress.Utils.HyperlinkClickEventHandler(this.lnkWebIcon_HyperlinkClick);
            // 
            // txtMenuIconName
            // 
            this.txtMenuIconName.Location = new System.Drawing.Point(77, 207);
            this.txtMenuIconName.Name = "txtMenuIconName";
            this.txtMenuIconName.Properties.MaxLength = 64;
            this.txtMenuIconName.Properties.NullValuePrompt = "格式：fa-{name}";
            this.txtMenuIconName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMenuIconName.Size = new System.Drawing.Size(208, 20);
            this.txtMenuIconName.TabIndex = 43;
            // 
            // lblMenuIconName
            // 
            this.lblMenuIconName.Location = new System.Drawing.Point(9, 210);
            this.lblMenuIconName.Name = "lblMenuIconName";
            this.lblMenuIconName.Size = new System.Drawing.Size(62, 14);
            this.lblMenuIconName.TabIndex = 44;
            this.lblMenuIconName.Text = "Web图标：";
            // 
            // MenuModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkWebIcon);
            this.Controls.Add(this.txtMenuIconName);
            this.Controls.Add(this.lblMenuIconName);
            this.Controls.Add(this.lblIconTip);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.icmbIconType);
            this.Controls.Add(this.lblMenuIcon);
            this.Controls.Add(this.txtMenuURL);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblMenuCodeRequired);
            this.Controls.Add(this.txtToolTip);
            this.Controls.Add(this.lblMenuNameRequired);
            this.Controls.Add(this.lblToolTip);
            this.Controls.Add(this.txtMenuCode);
            this.Controls.Add(this.lblMenuCode);
            this.Controls.Add(this.txtMenuName);
            this.Controls.Add(this.lblMenuName);
            this.Controls.Add(this.icmbMenuIcon);
            this.Controls.Add(this.devExpressUploadFile);
            this.Name = "MenuModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.MenuModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMenuCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMenuName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToolTip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMenuURL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbIconType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icIconType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbMenuIcon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icSystemIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMenuIconName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblToolTip;
        private DevExpress.XtraEditors.TextEdit txtMenuCode;
        private DevExpress.XtraEditors.LabelControl lblMenuCode;
        private DevExpress.XtraEditors.TextEdit txtMenuName;
        private DevExpress.XtraEditors.LabelControl lblMenuName;
        private DevExpress.XtraEditors.LabelControl lblMenuNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtToolTip;
        private DevExpress.XtraEditors.LabelControl lblMenuCodeRequired;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.LabelControl lblURL;
        private DevExpress.XtraEditors.TextEdit txtMenuURL;
        private DevExpress.XtraEditors.LabelControl lblMenuIcon;
        private AppFramework.WinFormsControls.DevExpressUploadFile devExpressUploadFile;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbIconType;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbMenuIcon;
        private DevExpress.Utils.ImageCollection icIconType;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.LabelControl lblIconTip;
        private DevExpress.Utils.ImageCollection icSystemIcon;
        private DevExpress.XtraEditors.HyperlinkLabelControl lnkWebIcon;
        private DevExpress.XtraEditors.TextEdit txtMenuIconName;
        private DevExpress.XtraEditors.LabelControl lblMenuIconName;
    }
}
