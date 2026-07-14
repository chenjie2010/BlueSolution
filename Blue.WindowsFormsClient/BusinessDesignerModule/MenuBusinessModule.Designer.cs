namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class MenuBusinessModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuBusinessModule));
            this.txtBusinessCode = new DevExpress.XtraEditors.TextEdit();
            this.txtBusinessName = new DevExpress.XtraEditors.TextEdit();
            this.lblBusinessName = new DevExpress.XtraEditors.LabelControl();
            this.lblBusinessNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.icMenuBusinessType = new DevExpress.Utils.ImageCollection(this.components);
            this.lblBusinessCodeRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblBusinessCode = new DevExpress.XtraEditors.LabelControl();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.chkEnableHelp = new DevExpress.XtraEditors.CheckEdit();
            this.lblGuidanceTip = new DevExpress.XtraEditors.LabelControl();
            this.hleHelp = new DevExpress.XtraEditors.HyperLinkEdit();
            this.txtHelpContent = new DevExpress.XtraEditors.TextEdit();
            this.lblHelpContent = new DevExpress.XtraEditors.LabelControl();
            this.txtBusinessIntro = new DevExpress.XtraEditors.MemoEdit();
            this.lblBusinessIntro = new DevExpress.XtraEditors.LabelControl();
            this.lblMenuBusinessType = new DevExpress.XtraEditors.LabelControl();
            this.icmbMenuBusinessType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblMenuTypeRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblMenuIntroRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblMenuIcon = new DevExpress.XtraEditors.LabelControl();
            this.txtBusinessURL = new DevExpress.XtraEditors.TextEdit();
            this.lblURL = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.icmbIconType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icIconType = new DevExpress.Utils.ImageCollection(this.components);
            this.icSystemIcon = new DevExpress.Utils.ImageCollection(this.components);
            this.lblIconTip = new DevExpress.XtraEditors.LabelControl();
            this.icmbMenuIcon = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbCustomMenuBusinessName = new AppFramework.WinFormsControls.ComoboxTreeview();
            this.devExpressUploadFile = new AppFramework.WinFormsControls.DevExpressUploadFile();
            this.btxtAssociatedBusiness = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icMenuBusinessType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableHelp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleHelp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHelpContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessIntro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbMenuBusinessType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessURL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbIconType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icIconType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icSystemIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbMenuIcon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtAssociatedBusiness.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBusinessCode
            // 
            this.txtBusinessCode.Location = new System.Drawing.Point(77, 36);
            this.txtBusinessCode.Name = "txtBusinessCode";
            this.txtBusinessCode.Properties.MaxLength = 32;
            this.txtBusinessCode.Properties.ReadOnly = true;
            this.txtBusinessCode.Size = new System.Drawing.Size(156, 20);
            this.txtBusinessCode.TabIndex = 2;
            // 
            // txtBusinessName
            // 
            this.txtBusinessName.Location = new System.Drawing.Point(77, 7);
            this.txtBusinessName.Name = "txtBusinessName";
            this.txtBusinessName.Properties.MaxLength = 64;
            this.txtBusinessName.Size = new System.Drawing.Size(282, 20);
            this.txtBusinessName.TabIndex = 1;
            // 
            // lblBusinessName
            // 
            this.lblBusinessName.Location = new System.Drawing.Point(11, 9);
            this.lblBusinessName.Name = "lblBusinessName";
            this.lblBusinessName.Size = new System.Drawing.Size(60, 14);
            this.lblBusinessName.TabIndex = 15;
            this.lblBusinessName.Text = "业务名称：";
            // 
            // lblBusinessNameRequired
            // 
            this.lblBusinessNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblBusinessNameRequired.Appearance.Options.UseForeColor = true;
            this.lblBusinessNameRequired.Location = new System.Drawing.Point(364, 10);
            this.lblBusinessNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblBusinessNameRequired.Name = "lblBusinessNameRequired";
            this.lblBusinessNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblBusinessNameRequired.TabIndex = 22;
            this.lblBusinessNameRequired.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(77, 278);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 56);
            this.txtNotes.TabIndex = 11;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(38, 282);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // icMenuBusinessType
            // 
            this.icMenuBusinessType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icMenuBusinessType.ImageStream")));
            this.icMenuBusinessType.Images.SetKeyName(0, "Common_Menu_Business.png");
            this.icMenuBusinessType.Images.SetKeyName(1, "Common_Menu_DataFilled.png");
            this.icMenuBusinessType.Images.SetKeyName(2, "Common_Menu_Audited.png");
            this.icMenuBusinessType.Images.SetKeyName(3, "Common_Menu_Query.png");
            this.icMenuBusinessType.Images.SetKeyName(4, "Common_Menu_Report.png");
            this.icMenuBusinessType.Images.SetKeyName(5, "Common_Menu_Workflow.png");
            this.icMenuBusinessType.Images.SetKeyName(6, "Common_Menu_Reserved.png");
            this.icMenuBusinessType.Images.SetKeyName(7, "Common_Menu_Data.png");
            this.icMenuBusinessType.Images.SetKeyName(8, "Common_Menu_Custom.png");
            // 
            // lblBusinessCodeRequired
            // 
            this.lblBusinessCodeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblBusinessCodeRequired.Appearance.Options.UseForeColor = true;
            this.lblBusinessCodeRequired.Location = new System.Drawing.Point(364, 41);
            this.lblBusinessCodeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblBusinessCodeRequired.Name = "lblBusinessCodeRequired";
            this.lblBusinessCodeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblBusinessCodeRequired.TabIndex = 24;
            this.lblBusinessCodeRequired.Text = "*";
            // 
            // lblBusinessCode
            // 
            this.lblBusinessCode.Location = new System.Drawing.Point(11, 38);
            this.lblBusinessCode.Name = "lblBusinessCode";
            this.lblBusinessCode.Size = new System.Drawing.Size(60, 14);
            this.lblBusinessCode.TabIndex = 58;
            this.lblBusinessCode.Text = "业务信息：";
            // 
            // lnkDetailedView
            // 
            this.lnkDetailedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedView.EditValue = "业务详情";
            this.lnkDetailedView.Location = new System.Drawing.Point(77, 337);
            this.lnkDetailedView.Name = "lnkDetailedView";
            this.lnkDetailedView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedView.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedView.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedView.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedView.TabIndex = 10;
            // 
            // chkEnableHelp
            // 
            this.chkEnableHelp.Location = new System.Drawing.Point(78, 249);
            this.chkEnableHelp.Name = "chkEnableHelp";
            this.chkEnableHelp.Properties.Caption = "启用帮助";
            this.chkEnableHelp.Size = new System.Drawing.Size(67, 19);
            this.chkEnableHelp.TabIndex = 8;
            this.chkEnableHelp.CheckedChanged += new System.EventHandler(this.chkEnableHelp_CheckedChanged);
            // 
            // lblGuidanceTip
            // 
            this.lblGuidanceTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblGuidanceTip.Appearance.Options.UseForeColor = true;
            this.lblGuidanceTip.Location = new System.Drawing.Point(364, 252);
            this.lblGuidanceTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblGuidanceTip.Name = "lblGuidanceTip";
            this.lblGuidanceTip.Size = new System.Drawing.Size(7, 14);
            this.lblGuidanceTip.TabIndex = 92;
            this.lblGuidanceTip.Text = "*";
            this.lblGuidanceTip.Visible = false;
            // 
            // hleHelp
            // 
            this.hleHelp.EditValue = "设置帮助...";
            this.hleHelp.Location = new System.Drawing.Point(289, 247);
            this.hleHelp.Name = "hleHelp";
            this.hleHelp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleHelp.Properties.Appearance.Options.UseBackColor = true;
            this.hleHelp.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleHelp.Size = new System.Drawing.Size(68, 18);
            this.hleHelp.TabIndex = 9;
            this.hleHelp.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hleHelp_OpenLink);
            // 
            // txtHelpContent
            // 
            this.txtHelpContent.Location = new System.Drawing.Point(151, 248);
            this.txtHelpContent.Name = "txtHelpContent";
            this.txtHelpContent.Properties.MaxLength = 32;
            this.txtHelpContent.Properties.ReadOnly = true;
            this.txtHelpContent.Size = new System.Drawing.Size(133, 20);
            this.txtHelpContent.TabIndex = 89;
            // 
            // lblHelpContent
            // 
            this.lblHelpContent.Location = new System.Drawing.Point(11, 251);
            this.lblHelpContent.Name = "lblHelpContent";
            this.lblHelpContent.Size = new System.Drawing.Size(60, 14);
            this.lblHelpContent.TabIndex = 91;
            this.lblHelpContent.Text = "使用帮助：";
            // 
            // txtBusinessIntro
            // 
            this.txtBusinessIntro.EditValue = "";
            this.txtBusinessIntro.Location = new System.Drawing.Point(77, 96);
            this.txtBusinessIntro.Name = "txtBusinessIntro";
            this.txtBusinessIntro.Properties.MaxLength = 256;
            this.txtBusinessIntro.Size = new System.Drawing.Size(280, 54);
            this.txtBusinessIntro.TabIndex = 7;
            // 
            // lblBusinessIntro
            // 
            this.lblBusinessIntro.Location = new System.Drawing.Point(11, 98);
            this.lblBusinessIntro.Name = "lblBusinessIntro";
            this.lblBusinessIntro.Size = new System.Drawing.Size(60, 14);
            this.lblBusinessIntro.TabIndex = 94;
            this.lblBusinessIntro.Text = "业务介绍：";
            // 
            // lblMenuBusinessType
            // 
            this.lblMenuBusinessType.Location = new System.Drawing.Point(11, 68);
            this.lblMenuBusinessType.Name = "lblMenuBusinessType";
            this.lblMenuBusinessType.Size = new System.Drawing.Size(60, 14);
            this.lblMenuBusinessType.TabIndex = 95;
            this.lblMenuBusinessType.Text = "关联业务：";
            // 
            // icmbMenuBusinessType
            // 
            this.icmbMenuBusinessType.Location = new System.Drawing.Point(236, 36);
            this.icmbMenuBusinessType.Name = "icmbMenuBusinessType";
            this.icmbMenuBusinessType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbMenuBusinessType.Properties.SmallImages = this.icMenuBusinessType;
            this.icmbMenuBusinessType.Size = new System.Drawing.Size(123, 20);
            this.icmbMenuBusinessType.TabIndex = 3;
            this.icmbMenuBusinessType.SelectedIndexChanged += new System.EventHandler(this.icmbMenuType_SelectedIndexChanged);
            // 
            // lblMenuTypeRequired
            // 
            this.lblMenuTypeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblMenuTypeRequired.Appearance.Options.UseForeColor = true;
            this.lblMenuTypeRequired.Location = new System.Drawing.Point(364, 70);
            this.lblMenuTypeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblMenuTypeRequired.Name = "lblMenuTypeRequired";
            this.lblMenuTypeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblMenuTypeRequired.TabIndex = 97;
            this.lblMenuTypeRequired.Text = "*";
            // 
            // lblMenuIntroRequired
            // 
            this.lblMenuIntroRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblMenuIntroRequired.Appearance.Options.UseForeColor = true;
            this.lblMenuIntroRequired.Location = new System.Drawing.Point(364, 98);
            this.lblMenuIntroRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblMenuIntroRequired.Name = "lblMenuIntroRequired";
            this.lblMenuIntroRequired.Size = new System.Drawing.Size(7, 14);
            this.lblMenuIntroRequired.TabIndex = 98;
            this.lblMenuIntroRequired.Text = "*";
            // 
            // lblMenuIcon
            // 
            this.lblMenuIcon.Location = new System.Drawing.Point(11, 192);
            this.lblMenuIcon.Name = "lblMenuIcon";
            this.lblMenuIcon.Size = new System.Drawing.Size(60, 14);
            this.lblMenuIcon.TabIndex = 102;
            this.lblMenuIcon.Text = "业务图标：";
            // 
            // txtBusinessURL
            // 
            this.txtBusinessURL.Location = new System.Drawing.Point(77, 220);
            this.txtBusinessURL.Name = "txtBusinessURL";
            this.txtBusinessURL.Properties.MaxLength = 64;
            this.txtBusinessURL.Size = new System.Drawing.Size(281, 20);
            this.txtBusinessURL.TabIndex = 6;
            // 
            // lblURL
            // 
            this.lblURL.Location = new System.Drawing.Point(14, 222);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(57, 14);
            this.lblURL.TabIndex = 101;
            this.lblURL.Text = "URL地址：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 163);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 106;
            this.labelControl2.Text = "图标类型：";
            // 
            // icmbIconType
            // 
            this.icmbIconType.Location = new System.Drawing.Point(77, 160);
            this.icmbIconType.Name = "icmbIconType";
            this.icmbIconType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbIconType.Properties.SmallImages = this.icIconType;
            this.icmbIconType.Size = new System.Drawing.Size(280, 20);
            this.icmbIconType.TabIndex = 105;
            this.icmbIconType.SelectedIndexChanged += new System.EventHandler(this.icmbIconType_SelectedIndexChanged);
            // 
            // icIconType
            // 
            this.icIconType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icIconType.ImageStream")));
            this.icIconType.Images.SetKeyName(0, "Common_IconType_System.png");
            this.icIconType.Images.SetKeyName(1, "Common_IconType_User.png");
            // 
            // icSystemIcon
            // 
            this.icSystemIcon.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icSystemIcon.ImageStream")));
            this.icSystemIcon.Images.SetKeyName(0, "SystemIcon_0_Small.png");
            this.icSystemIcon.Images.SetKeyName(1, "SystemIcon_1_Small.png");
            this.icSystemIcon.Images.SetKeyName(2, "SystemIcon_2_Small.png");
            // 
            // lblIconTip
            // 
            this.lblIconTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblIconTip.Appearance.Options.UseForeColor = true;
            this.lblIconTip.Location = new System.Drawing.Point(363, 197);
            this.lblIconTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblIconTip.Name = "lblIconTip";
            this.lblIconTip.Size = new System.Drawing.Size(7, 14);
            this.lblIconTip.TabIndex = 108;
            this.lblIconTip.Text = "*";
            // 
            // icmbMenuIcon
            // 
            this.icmbMenuIcon.Location = new System.Drawing.Point(77, 190);
            this.icmbMenuIcon.Name = "icmbMenuIcon";
            this.icmbMenuIcon.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbMenuIcon.Properties.SmallImages = this.icSystemIcon;
            this.icmbMenuIcon.Size = new System.Drawing.Size(280, 20);
            this.icmbMenuIcon.TabIndex = 109;
            // 
            // cmbCustomMenuBusinessName
            // 
            this.cmbCustomMenuBusinessName.Location = new System.Drawing.Point(77, 67);
            this.cmbCustomMenuBusinessName.Name = "cmbCustomMenuBusinessName";
            this.cmbCustomMenuBusinessName.OnlySelectedLeaf = true;
            this.cmbCustomMenuBusinessName.Size = new System.Drawing.Size(282, 21);
            this.cmbCustomMenuBusinessName.TabIndex = 4;
            this.cmbCustomMenuBusinessName.Visible = false;
            // 
            // devExpressUploadFile
            // 
            this.devExpressUploadFile.DocType = AppFramework.WinFormsControls.DocType.PicAttachment;
            this.devExpressUploadFile.Location = new System.Drawing.Point(77, 191);
            this.devExpressUploadFile.Name = "devExpressUploadFile";
            this.devExpressUploadFile.ReadOnly = false;
            this.devExpressUploadFile.Size = new System.Drawing.Size(280, 23);
            this.devExpressUploadFile.TabIndex = 107;
            this.devExpressUploadFile.Visible = false;
            this.devExpressUploadFile.OnViewLinkClicked += new System.EventHandler<System.EventArgs>(this.devExpressUploadFile_OnViewLinkClicked);
            // 
            // btxtAssociatedBusiness
            // 
            this.btxtAssociatedBusiness.Location = new System.Drawing.Point(77, 67);
            this.btxtAssociatedBusiness.Name = "btxtAssociatedBusiness";
            this.btxtAssociatedBusiness.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtAssociatedBusiness.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtAssociatedBusiness.Size = new System.Drawing.Size(282, 20);
            this.btxtAssociatedBusiness.TabIndex = 110;
            this.btxtAssociatedBusiness.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtAssociatedBusiness_ButtonPressed);
            // 
            // MenuBusinessModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btxtAssociatedBusiness);
            this.Controls.Add(this.lblIconTip);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.icmbIconType);
            this.Controls.Add(this.lblMenuIcon);
            this.Controls.Add(this.txtBusinessURL);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.lblMenuIntroRequired);
            this.Controls.Add(this.lblMenuTypeRequired);
            this.Controls.Add(this.icmbMenuBusinessType);
            this.Controls.Add(this.lblMenuBusinessType);
            this.Controls.Add(this.lblBusinessIntro);
            this.Controls.Add(this.chkEnableHelp);
            this.Controls.Add(this.lblGuidanceTip);
            this.Controls.Add(this.hleHelp);
            this.Controls.Add(this.txtHelpContent);
            this.Controls.Add(this.lblHelpContent);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.lblBusinessCode);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblBusinessCodeRequired);
            this.Controls.Add(this.lblBusinessNameRequired);
            this.Controls.Add(this.txtBusinessCode);
            this.Controls.Add(this.txtBusinessName);
            this.Controls.Add(this.lblBusinessName);
            this.Controls.Add(this.icmbMenuIcon);
            this.Controls.Add(this.cmbCustomMenuBusinessName);
            this.Controls.Add(this.txtBusinessIntro);
            this.Controls.Add(this.devExpressUploadFile);
            this.Name = "MenuBusinessModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.MenuModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icMenuBusinessType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableHelp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleHelp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHelpContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessIntro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbMenuBusinessType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBusinessURL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbIconType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icIconType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icSystemIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbMenuIcon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtAssociatedBusiness.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtBusinessCode;
        private DevExpress.XtraEditors.TextEdit txtBusinessName;
        private DevExpress.XtraEditors.LabelControl lblBusinessName;
        private DevExpress.XtraEditors.LabelControl lblBusinessNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.Utils.ImageCollection icMenuBusinessType;
        private DevExpress.XtraEditors.LabelControl lblBusinessCodeRequired;
        private DevExpress.XtraEditors.LabelControl lblBusinessCode;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.CheckEdit chkEnableHelp;
        private DevExpress.XtraEditors.LabelControl lblGuidanceTip;
        private DevExpress.XtraEditors.HyperLinkEdit hleHelp;
        private DevExpress.XtraEditors.TextEdit txtHelpContent;
        private DevExpress.XtraEditors.LabelControl lblHelpContent;
        private DevExpress.XtraEditors.MemoEdit txtBusinessIntro;
        private DevExpress.XtraEditors.LabelControl lblBusinessIntro;
        private DevExpress.XtraEditors.LabelControl lblMenuBusinessType;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbMenuBusinessType;
        private DevExpress.XtraEditors.LabelControl lblMenuTypeRequired;
        private DevExpress.XtraEditors.LabelControl lblMenuIntroRequired;
        private AppFramework.WinFormsControls.ComoboxTreeview cmbCustomMenuBusinessName;
        private DevExpress.XtraEditors.LabelControl lblMenuIcon;
        private DevExpress.XtraEditors.TextEdit txtBusinessURL;
        private DevExpress.XtraEditors.LabelControl lblURL;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbIconType;
        private AppFramework.WinFormsControls.DevExpressUploadFile devExpressUploadFile;
        private DevExpress.Utils.ImageCollection icIconType;
        private DevExpress.Utils.ImageCollection icSystemIcon;
        private DevExpress.XtraEditors.LabelControl lblIconTip;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbMenuIcon;
        private DevExpress.XtraEditors.ButtonEdit btxtAssociatedBusiness;
    }
}
