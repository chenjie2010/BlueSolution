namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class DataFormModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataFormModule));
            this.txtFormCode = new DevExpress.XtraEditors.TextEdit();
            this.lblTableName = new DevExpress.XtraEditors.LabelControl();
            this.lblFormCode = new DevExpress.XtraEditors.LabelControl();
            this.txtFormName = new DevExpress.XtraEditors.TextEdit();
            this.lblFormName = new DevExpress.XtraEditors.LabelControl();
            this.lblFormNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.lblFormCodeRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblAssociatedTableTip = new DevExpress.XtraEditors.LabelControl();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.hleTableSetting = new DevExpress.XtraEditors.HyperLinkEdit();
            this.icmbTableType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icTableType = new DevExpress.Utils.ImageCollection(this.components);
            this.txtTableName = new DevExpress.XtraEditors.TextEdit();
            this.lblTableNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblShowMode = new DevExpress.XtraEditors.LabelControl();
            this.chkEnableHelp = new DevExpress.XtraEditors.CheckEdit();
            this.lblGuidanceTip = new DevExpress.XtraEditors.LabelControl();
            this.hleHelpContent = new DevExpress.XtraEditors.HyperLinkEdit();
            this.txtHelpContent = new DevExpress.XtraEditors.TextEdit();
            this.lblHelpContent = new DevExpress.XtraEditors.LabelControl();
            this.lblFormProperty = new DevExpress.XtraEditors.LabelControl();
            this.ccmbFormProperty = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.ccmbDataFieldSetting = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblDataFieldSetting = new DevExpress.XtraEditors.LabelControl();
            this.lblBusinessEnabled = new DevExpress.XtraEditors.LabelControl();
            this.chkBusinessEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.lblBusinessEnabledTip = new DevExpress.XtraEditors.LabelControl();
            this.icmbSystemFormType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icSystemFormType = new DevExpress.Utils.ImageCollection(this.components);
            this.cmbShowMode = new AppFramework.WinFormsControls.ComoboxTreeview();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleTableSetting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableHelp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleHelpContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHelpContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbFormProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldSetting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBusinessEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbSystemFormType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icSystemFormType)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFormCode
            // 
            this.txtFormCode.Location = new System.Drawing.Point(77, 49);
            this.txtFormCode.Name = "txtFormCode";
            this.txtFormCode.Properties.MaxLength = 32;
            this.txtFormCode.Properties.ReadOnly = true;
            this.txtFormCode.Size = new System.Drawing.Size(144, 20);
            this.txtFormCode.TabIndex = 2;
            // 
            // lblTableName
            // 
            this.lblTableName.Location = new System.Drawing.Point(23, 83);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(48, 14);
            this.lblTableName.TabIndex = 17;
            this.lblTableName.Text = "数据表：";
            // 
            // lblFormCode
            // 
            this.lblFormCode.Location = new System.Drawing.Point(11, 49);
            this.lblFormCode.Name = "lblFormCode";
            this.lblFormCode.Size = new System.Drawing.Size(60, 14);
            this.lblFormCode.TabIndex = 13;
            this.lblFormCode.Text = "表格信息：";
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(77, 17);
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Properties.MaxLength = 64;
            this.txtFormName.Size = new System.Drawing.Size(282, 20);
            this.txtFormName.TabIndex = 1;
            // 
            // lblFormName
            // 
            this.lblFormName.Location = new System.Drawing.Point(11, 19);
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.Size = new System.Drawing.Size(60, 14);
            this.lblFormName.TabIndex = 15;
            this.lblFormName.Text = "表格名称：";
            // 
            // lblFormNameRequired
            // 
            this.lblFormNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblFormNameRequired.Appearance.Options.UseForeColor = true;
            this.lblFormNameRequired.Location = new System.Drawing.Point(366, 21);
            this.lblFormNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblFormNameRequired.Name = "lblFormNameRequired";
            this.lblFormNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblFormNameRequired.TabIndex = 22;
            this.lblFormNameRequired.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(77, 294);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 34);
            this.txtNotes.TabIndex = 12;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(35, 294);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // lblFormCodeRequired
            // 
            this.lblFormCodeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblFormCodeRequired.Appearance.Options.UseForeColor = true;
            this.lblFormCodeRequired.Location = new System.Drawing.Point(366, 54);
            this.lblFormCodeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblFormCodeRequired.Name = "lblFormCodeRequired";
            this.lblFormCodeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblFormCodeRequired.TabIndex = 24;
            this.lblFormCodeRequired.Text = "*";
            // 
            // lblAssociatedTableTip
            // 
            this.lblAssociatedTableTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblAssociatedTableTip.Appearance.Options.UseForeColor = true;
            this.lblAssociatedTableTip.Location = new System.Drawing.Point(366, 141);
            this.lblAssociatedTableTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblAssociatedTableTip.Name = "lblAssociatedTableTip";
            this.lblAssociatedTableTip.Size = new System.Drawing.Size(7, 14);
            this.lblAssociatedTableTip.TabIndex = 50;
            this.lblAssociatedTableTip.Text = "*";
            // 
            // lnkDetailedView
            // 
            this.lnkDetailedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedView.EditValue = "表格详情";
            this.lnkDetailedView.Location = new System.Drawing.Point(77, 334);
            this.lnkDetailedView.Name = "lnkDetailedView";
            this.lnkDetailedView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedView.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedView.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedView.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedView.TabIndex = 13;
            // 
            // hleTableSetting
            // 
            this.hleTableSetting.EditValue = "设置...";
            this.hleTableSetting.Location = new System.Drawing.Point(310, 107);
            this.hleTableSetting.Name = "hleTableSetting";
            this.hleTableSetting.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleTableSetting.Properties.Appearance.Options.UseBackColor = true;
            this.hleTableSetting.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleTableSetting.Size = new System.Drawing.Size(47, 18);
            this.hleTableSetting.TabIndex = 5;
            this.hleTableSetting.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hleTableSetting_OpenLink);
            // 
            // icmbTableType
            // 
            this.icmbTableType.Location = new System.Drawing.Point(227, 49);
            this.icmbTableType.Name = "icmbTableType";
            this.icmbTableType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbTableType.Properties.SmallImages = this.icTableType;
            this.icmbTableType.Size = new System.Drawing.Size(132, 20);
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
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(77, 81);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Properties.MaxLength = 32;
            this.txtTableName.Properties.ReadOnly = true;
            this.txtTableName.Size = new System.Drawing.Size(280, 20);
            this.txtTableName.TabIndex = 4;
            // 
            // lblTableNameRequired
            // 
            this.lblTableNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblTableNameRequired.Appearance.Options.UseForeColor = true;
            this.lblTableNameRequired.Location = new System.Drawing.Point(366, 85);
            this.lblTableNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblTableNameRequired.Name = "lblTableNameRequired";
            this.lblTableNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblTableNameRequired.TabIndex = 68;
            this.lblTableNameRequired.Text = "*";
            // 
            // lblShowMode
            // 
            this.lblShowMode.Location = new System.Drawing.Point(11, 136);
            this.lblShowMode.Name = "lblShowMode";
            this.lblShowMode.Size = new System.Drawing.Size(60, 14);
            this.lblShowMode.TabIndex = 74;
            this.lblShowMode.Text = "展现模式：";
            // 
            // chkEnableHelp
            // 
            this.chkEnableHelp.Location = new System.Drawing.Point(79, 260);
            this.chkEnableHelp.Name = "chkEnableHelp";
            this.chkEnableHelp.Properties.Caption = "启用";
            this.chkEnableHelp.Size = new System.Drawing.Size(53, 19);
            this.chkEnableHelp.TabIndex = 10;
            // 
            // lblGuidanceTip
            // 
            this.lblGuidanceTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblGuidanceTip.Appearance.Options.UseForeColor = true;
            this.lblGuidanceTip.Location = new System.Drawing.Point(366, 264);
            this.lblGuidanceTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblGuidanceTip.Name = "lblGuidanceTip";
            this.lblGuidanceTip.Size = new System.Drawing.Size(7, 14);
            this.lblGuidanceTip.TabIndex = 82;
            this.lblGuidanceTip.Text = "*";
            this.lblGuidanceTip.Visible = false;
            // 
            // hleHelpContent
            // 
            this.hleHelpContent.EditValue = "设置帮助...";
            this.hleHelpContent.Location = new System.Drawing.Point(292, 261);
            this.hleHelpContent.Name = "hleHelpContent";
            this.hleHelpContent.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleHelpContent.Properties.Appearance.Options.UseBackColor = true;
            this.hleHelpContent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleHelpContent.Size = new System.Drawing.Size(66, 18);
            this.hleHelpContent.TabIndex = 11;
            this.hleHelpContent.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hleHelpContent_OpenLink);
            // 
            // txtHelpContent
            // 
            this.txtHelpContent.Location = new System.Drawing.Point(141, 260);
            this.txtHelpContent.Name = "txtHelpContent";
            this.txtHelpContent.Properties.MaxLength = 32;
            this.txtHelpContent.Properties.ReadOnly = true;
            this.txtHelpContent.Size = new System.Drawing.Size(148, 20);
            this.txtHelpContent.TabIndex = 10;
            // 
            // lblHelpContent
            // 
            this.lblHelpContent.Location = new System.Drawing.Point(13, 262);
            this.lblHelpContent.Name = "lblHelpContent";
            this.lblHelpContent.Size = new System.Drawing.Size(60, 14);
            this.lblHelpContent.TabIndex = 79;
            this.lblHelpContent.Text = "帮助内容：";
            // 
            // lblFormProperty
            // 
            this.lblFormProperty.Location = new System.Drawing.Point(12, 194);
            this.lblFormProperty.Name = "lblFormProperty";
            this.lblFormProperty.Size = new System.Drawing.Size(60, 14);
            this.lblFormProperty.TabIndex = 87;
            this.lblFormProperty.Text = "表格属性：";
            // 
            // ccmbFormProperty
            // 
            this.ccmbFormProperty.EditValue = "";
            this.ccmbFormProperty.Location = new System.Drawing.Point(79, 193);
            this.ccmbFormProperty.Name = "ccmbFormProperty";
            this.ccmbFormProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbFormProperty.Properties.PopupSizeable = false;
            this.ccmbFormProperty.Properties.SelectAllItemVisible = false;
            this.ccmbFormProperty.Size = new System.Drawing.Size(281, 20);
            this.ccmbFormProperty.TabIndex = 8;
            // 
            // ccmbDataFieldSetting
            // 
            this.ccmbDataFieldSetting.EditValue = "";
            this.ccmbDataFieldSetting.Location = new System.Drawing.Point(79, 225);
            this.ccmbDataFieldSetting.Name = "ccmbDataFieldSetting";
            this.ccmbDataFieldSetting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbDataFieldSetting.Size = new System.Drawing.Size(281, 20);
            this.ccmbDataFieldSetting.TabIndex = 9;
            // 
            // lblDataFieldSetting
            // 
            this.lblDataFieldSetting.Location = new System.Drawing.Point(12, 227);
            this.lblDataFieldSetting.Name = "lblDataFieldSetting";
            this.lblDataFieldSetting.Size = new System.Drawing.Size(60, 14);
            this.lblDataFieldSetting.TabIndex = 89;
            this.lblDataFieldSetting.Text = "系统字段：";
            // 
            // lblBusinessEnabled
            // 
            this.lblBusinessEnabled.Location = new System.Drawing.Point(23, 166);
            this.lblBusinessEnabled.Name = "lblBusinessEnabled";
            this.lblBusinessEnabled.Size = new System.Drawing.Size(48, 14);
            this.lblBusinessEnabled.TabIndex = 91;
            this.lblBusinessEnabled.Text = "业务表：";
            // 
            // chkBusinessEnabled
            // 
            this.chkBusinessEnabled.Location = new System.Drawing.Point(79, 165);
            this.chkBusinessEnabled.Name = "chkBusinessEnabled";
            this.chkBusinessEnabled.Properties.Caption = "启用业务模式";
            this.chkBusinessEnabled.Size = new System.Drawing.Size(100, 19);
            this.chkBusinessEnabled.TabIndex = 7;
            // 
            // lblBusinessEnabledTip
            // 
            this.lblBusinessEnabledTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblBusinessEnabledTip.Appearance.Options.UseForeColor = true;
            this.lblBusinessEnabledTip.Location = new System.Drawing.Point(366, 170);
            this.lblBusinessEnabledTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblBusinessEnabledTip.Name = "lblBusinessEnabledTip";
            this.lblBusinessEnabledTip.Size = new System.Drawing.Size(7, 14);
            this.lblBusinessEnabledTip.TabIndex = 92;
            this.lblBusinessEnabledTip.Text = "*";
            // 
            // icmbSystemFormType
            // 
            this.icmbSystemFormType.Location = new System.Drawing.Point(79, 81);
            this.icmbSystemFormType.Name = "icmbSystemFormType";
            this.icmbSystemFormType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbSystemFormType.Properties.SmallImages = this.icSystemFormType;
            this.icmbSystemFormType.Size = new System.Drawing.Size(278, 20);
            this.icmbSystemFormType.TabIndex = 93;
            this.icmbSystemFormType.Visible = false;
            // 
            // icSystemFormType
            // 
            this.icSystemFormType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icSystemFormType.ImageStream")));
            this.icSystemFormType.Images.SetKeyName(0, "SystemFormType_None.png");
            this.icSystemFormType.Images.SetKeyName(1, "SystemFormType_CommonUserInfo.png");
            // 
            // cmbShowMode
            // 
            this.cmbShowMode.Location = new System.Drawing.Point(79, 135);
            this.cmbShowMode.Name = "cmbShowMode";
            this.cmbShowMode.OnlySelectedLeaf = true;
            this.cmbShowMode.Size = new System.Drawing.Size(280, 21);
            this.cmbShowMode.TabIndex = 6;
            // 
            // DataFormModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblBusinessEnabledTip);
            this.Controls.Add(this.lblBusinessEnabled);
            this.Controls.Add(this.chkBusinessEnabled);
            this.Controls.Add(this.lblDataFieldSetting);
            this.Controls.Add(this.ccmbDataFieldSetting);
            this.Controls.Add(this.ccmbFormProperty);
            this.Controls.Add(this.lblFormProperty);
            this.Controls.Add(this.lblGuidanceTip);
            this.Controls.Add(this.hleHelpContent);
            this.Controls.Add(this.txtHelpContent);
            this.Controls.Add(this.lblHelpContent);
            this.Controls.Add(this.cmbShowMode);
            this.Controls.Add(this.lblShowMode);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.lblTableNameRequired);
            this.Controls.Add(this.icmbTableType);
            this.Controls.Add(this.hleTableSetting);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.lblAssociatedTableTip);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblFormCodeRequired);
            this.Controls.Add(this.lblFormNameRequired);
            this.Controls.Add(this.txtFormCode);
            this.Controls.Add(this.lblTableName);
            this.Controls.Add(this.lblFormCode);
            this.Controls.Add(this.txtFormName);
            this.Controls.Add(this.lblFormName);
            this.Controls.Add(this.chkEnableHelp);
            this.Controls.Add(this.icmbSystemFormType);
            this.Name = "DataFormModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.DataFormModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtFormCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleTableSetting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableHelp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleHelpContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHelpContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbFormProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldSetting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBusinessEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbSystemFormType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icSystemFormType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtFormCode;
        private DevExpress.XtraEditors.LabelControl lblTableName;
        private DevExpress.XtraEditors.LabelControl lblFormCode;
        private DevExpress.XtraEditors.TextEdit txtFormName;
        private DevExpress.XtraEditors.LabelControl lblFormName;
        private DevExpress.XtraEditors.LabelControl lblFormNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.LabelControl lblFormCodeRequired;
        private DevExpress.XtraEditors.LabelControl lblAssociatedTableTip;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.HyperLinkEdit hleTableSetting;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbTableType;
        private DevExpress.XtraEditors.TextEdit txtTableName;
        private DevExpress.XtraEditors.LabelControl lblTableNameRequired;
        private AppFramework.WinFormsControls.ComoboxTreeview cmbShowMode;
        private DevExpress.XtraEditors.LabelControl lblShowMode;
        private DevExpress.XtraEditors.CheckEdit chkEnableHelp;
        private DevExpress.XtraEditors.LabelControl lblGuidanceTip;
        private DevExpress.XtraEditors.HyperLinkEdit hleHelpContent;
        private DevExpress.XtraEditors.TextEdit txtHelpContent;
        private DevExpress.XtraEditors.LabelControl lblHelpContent;
        private DevExpress.Utils.ImageCollection icTableType;
        private DevExpress.XtraEditors.LabelControl lblFormProperty;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbFormProperty;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbDataFieldSetting;
        private DevExpress.XtraEditors.LabelControl lblDataFieldSetting;
        private DevExpress.XtraEditors.LabelControl lblBusinessEnabled;
        private DevExpress.XtraEditors.CheckEdit chkBusinessEnabled;
        private DevExpress.XtraEditors.LabelControl lblBusinessEnabledTip;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbSystemFormType;
        private DevExpress.Utils.ImageCollection icSystemFormType;
    }
}
