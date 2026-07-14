namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    partial class DataFieldModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataFieldModule));
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFieldCode = new DevExpress.XtraEditors.TextEdit();
            this.lblDataFieldType = new DevExpress.XtraEditors.LabelControl();
            this.txtPhysicalName = new DevExpress.XtraEditors.TextEdit();
            this.lblInformation = new DevExpress.XtraEditors.LabelControl();
            this.txtLogicalName = new DevExpress.XtraEditors.TextEdit();
            this.lblLogicalName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameTip = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.txtToolTip = new DevExpress.XtraEditors.MemoEdit();
            this.lblToolTip = new DevExpress.XtraEditors.LabelControl();
            this.lnkDataFieldList = new DevExpress.XtraEditors.HyperLinkEdit();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.lblCondition = new DevExpress.XtraEditors.LabelControl();
            this.lblDataFieldAttributes = new DevExpress.XtraEditors.LabelControl();
            this.icmbDataFieldProperty = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icDataFieldProperty = new DevExpress.Utils.ImageCollection(this.components);
            this.lblInformationTip = new DevExpress.XtraEditors.LabelControl();
            this.lblConditionTip = new DevExpress.XtraEditors.LabelControl();
            this.ccmbDataFieldAttributes = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtConditionValue = new DevExpress.XtraEditors.TextEdit();
            this.txtMaxLength = new DevExpress.XtraEditors.TextEdit();
            this.icDataFieldType = new DevExpress.Utils.ImageCollection(this.components);
            this.hleDataFieldSetting = new DevExpress.XtraEditors.HyperLinkEdit();
            this.icImages = new DevExpress.Utils.ImageCollection(this.components);
            this.lblMaxLengthTip = new DevExpress.XtraEditors.LabelControl();
            this.ceAutoComplete = new DevExpress.XtraEditors.CheckEdit();
            this.cmbAssociatedDataField = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ccmbDataFieldSetting = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblDataFieldSetting = new DevExpress.XtraEditors.LabelControl();
            this.hleHelpContent = new DevExpress.XtraEditors.HyperLinkEdit();
            this.txtHelpContent = new DevExpress.XtraEditors.TextEdit();
            this.chkHelpEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.lblHelpEnabled = new DevExpress.XtraEditors.LabelControl();
            this.cmdDataFieldType = new AppFramework.WinFormsControls.ComoboxTreeview();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFieldCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhysicalName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogicalName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToolTip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDataFieldList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataFieldProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldAttributes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConditionValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxLength.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleDataFieldSetting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceAutoComplete.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAssociatedDataField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldSetting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleHelpContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHelpContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHelpEnabled.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(28, 297);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 21;
            this.lblNotes.Text = "备注：";
            // 
            // txtDataFieldCode
            // 
            this.txtDataFieldCode.Location = new System.Drawing.Point(237, 41);
            this.txtDataFieldCode.Name = "txtDataFieldCode";
            this.txtDataFieldCode.Properties.MaxLength = 32;
            this.txtDataFieldCode.Properties.NullValuePrompt = "字段编码";
            this.txtDataFieldCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtDataFieldCode.Properties.ReadOnly = true;
            this.txtDataFieldCode.Size = new System.Drawing.Size(133, 20);
            this.txtDataFieldCode.TabIndex = 3;
            // 
            // lblDataFieldType
            // 
            this.lblDataFieldType.Location = new System.Drawing.Point(6, 75);
            this.lblDataFieldType.Name = "lblDataFieldType";
            this.lblDataFieldType.Size = new System.Drawing.Size(60, 14);
            this.lblDataFieldType.TabIndex = 17;
            this.lblDataFieldType.Text = "字段类型：";
            // 
            // txtPhysicalName
            // 
            this.txtPhysicalName.Location = new System.Drawing.Point(72, 41);
            this.txtPhysicalName.Name = "txtPhysicalName";
            this.txtPhysicalName.Properties.ReadOnly = true;
            this.txtPhysicalName.Size = new System.Drawing.Size(159, 20);
            this.txtPhysicalName.TabIndex = 2;
            // 
            // lblInformation
            // 
            this.lblInformation.Location = new System.Drawing.Point(6, 43);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(60, 14);
            this.lblInformation.TabIndex = 13;
            this.lblInformation.Text = "字段信息：";
            // 
            // txtLogicalName
            // 
            this.txtLogicalName.Location = new System.Drawing.Point(72, 11);
            this.txtLogicalName.Name = "txtLogicalName";
            this.txtLogicalName.Properties.MaxLength = 64;
            this.txtLogicalName.Size = new System.Drawing.Size(299, 20);
            this.txtLogicalName.TabIndex = 1;
            // 
            // lblLogicalName
            // 
            this.lblLogicalName.Location = new System.Drawing.Point(6, 13);
            this.lblLogicalName.Name = "lblLogicalName";
            this.lblLogicalName.Size = new System.Drawing.Size(60, 14);
            this.lblLogicalName.TabIndex = 15;
            this.lblLogicalName.Text = "字段名称：";
            // 
            // lblNameTip
            // 
            this.lblNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameTip.Appearance.Options.UseForeColor = true;
            this.lblNameTip.Location = new System.Drawing.Point(377, 16);
            this.lblNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameTip.Name = "lblNameTip";
            this.lblNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblNameTip.TabIndex = 22;
            this.lblNameTip.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(72, 295);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(298, 26);
            this.txtNotes.TabIndex = 14;
            // 
            // txtToolTip
            // 
            this.txtToolTip.EditValue = "";
            this.txtToolTip.Location = new System.Drawing.Point(73, 226);
            this.txtToolTip.Name = "txtToolTip";
            this.txtToolTip.Properties.MaxLength = 256;
            this.txtToolTip.Size = new System.Drawing.Size(298, 58);
            this.txtToolTip.TabIndex = 13;
            // 
            // lblToolTip
            // 
            this.lblToolTip.Location = new System.Drawing.Point(28, 228);
            this.lblToolTip.Name = "lblToolTip";
            this.lblToolTip.Size = new System.Drawing.Size(36, 14);
            this.lblToolTip.TabIndex = 26;
            this.lblToolTip.Text = "提示：";
            // 
            // lnkDataFieldList
            // 
            this.lnkDataFieldList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDataFieldList.EditValue = "提示信息";
            this.lnkDataFieldList.Location = new System.Drawing.Point(70, 336);
            this.lnkDataFieldList.Name = "lnkDataFieldList";
            this.lnkDataFieldList.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDataFieldList.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDataFieldList.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDataFieldList.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDataFieldList.Size = new System.Drawing.Size(280, 22);
            this.lnkDataFieldList.TabIndex = 30;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(46, 318);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(280, 23);
            this.separatorControl1.TabIndex = 31;
            // 
            // lblCondition
            // 
            this.lblCondition.Enabled = false;
            this.lblCondition.Location = new System.Drawing.Point(6, 105);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(60, 14);
            this.lblCondition.TabIndex = 32;
            this.lblCondition.Text = "字段条件：";
            // 
            // lblDataFieldAttributes
            // 
            this.lblDataFieldAttributes.Location = new System.Drawing.Point(6, 136);
            this.lblDataFieldAttributes.Name = "lblDataFieldAttributes";
            this.lblDataFieldAttributes.Size = new System.Drawing.Size(60, 14);
            this.lblDataFieldAttributes.TabIndex = 33;
            this.lblDataFieldAttributes.Text = "字段性质：";
            // 
            // icmbDataFieldProperty
            // 
            this.icmbDataFieldProperty.Location = new System.Drawing.Point(72, 72);
            this.icmbDataFieldProperty.Name = "icmbDataFieldProperty";
            this.icmbDataFieldProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDataFieldProperty.Properties.SmallImages = this.icDataFieldProperty;
            this.icmbDataFieldProperty.Size = new System.Drawing.Size(80, 20);
            this.icmbDataFieldProperty.TabIndex = 4;
            this.icmbDataFieldProperty.SelectedIndexChanged += new System.EventHandler(this.icmbDataFieldProperty_SelectedIndexChanged);
            // 
            // icDataFieldProperty
            // 
            this.icDataFieldProperty.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataFieldProperty.ImageStream")));
            this.icDataFieldProperty.Images.SetKeyName(0, "Database_Node_0.png");
            this.icDataFieldProperty.Images.SetKeyName(1, "Database_Node_1.png");
            this.icDataFieldProperty.Images.SetKeyName(2, "Database_Node_2.png");
            this.icDataFieldProperty.Images.SetKeyName(3, "Database_Node_3.png");
            // 
            // lblInformationTip
            // 
            this.lblInformationTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblInformationTip.Appearance.Options.UseForeColor = true;
            this.lblInformationTip.Location = new System.Drawing.Point(376, 46);
            this.lblInformationTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblInformationTip.Name = "lblInformationTip";
            this.lblInformationTip.Size = new System.Drawing.Size(7, 14);
            this.lblInformationTip.TabIndex = 42;
            this.lblInformationTip.Text = "*";
            // 
            // lblConditionTip
            // 
            this.lblConditionTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblConditionTip.Appearance.Options.UseForeColor = true;
            this.lblConditionTip.Enabled = false;
            this.lblConditionTip.Location = new System.Drawing.Point(376, 108);
            this.lblConditionTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblConditionTip.Name = "lblConditionTip";
            this.lblConditionTip.Size = new System.Drawing.Size(7, 14);
            this.lblConditionTip.TabIndex = 43;
            this.lblConditionTip.Text = "*";
            // 
            // ccmbDataFieldAttributes
            // 
            this.ccmbDataFieldAttributes.EditValue = "";
            this.ccmbDataFieldAttributes.Location = new System.Drawing.Point(72, 133);
            this.ccmbDataFieldAttributes.Name = "ccmbDataFieldAttributes";
            this.ccmbDataFieldAttributes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbDataFieldAttributes.Properties.PopupSizeable = false;
            this.ccmbDataFieldAttributes.Properties.SelectAllItemVisible = false;
            this.ccmbDataFieldAttributes.Properties.ShowButtons = false;
            this.ccmbDataFieldAttributes.Size = new System.Drawing.Size(193, 20);
            this.ccmbDataFieldAttributes.TabIndex = 7;
            // 
            // txtConditionValue
            // 
            this.txtConditionValue.Location = new System.Drawing.Point(72, 102);
            this.txtConditionValue.Name = "txtConditionValue";
            this.txtConditionValue.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtConditionValue.Properties.ReadOnly = true;
            this.txtConditionValue.Size = new System.Drawing.Size(254, 20);
            this.txtConditionValue.TabIndex = 48;
            // 
            // txtMaxLength
            // 
            this.txtMaxLength.Location = new System.Drawing.Point(311, 72);
            this.txtMaxLength.Name = "txtMaxLength";
            this.txtMaxLength.Properties.MaxLength = 4;
            this.txtMaxLength.Properties.NullValuePrompt = "长度";
            this.txtMaxLength.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMaxLength.Size = new System.Drawing.Size(57, 20);
            this.txtMaxLength.TabIndex = 6;
            this.txtMaxLength.Visible = false;
            // 
            // icDataFieldType
            // 
            this.icDataFieldType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataFieldType.ImageStream")));
            // 
            // hleDataFieldSetting
            // 
            this.hleDataFieldSetting.EditValue = "设置...";
            this.hleDataFieldSetting.Enabled = false;
            this.hleDataFieldSetting.Location = new System.Drawing.Point(330, 102);
            this.hleDataFieldSetting.Name = "hleDataFieldSetting";
            this.hleDataFieldSetting.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleDataFieldSetting.Properties.Appearance.Options.UseBackColor = true;
            this.hleDataFieldSetting.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleDataFieldSetting.Size = new System.Drawing.Size(44, 18);
            this.hleDataFieldSetting.TabIndex = 51;
            this.hleDataFieldSetting.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hleDataFieldSetting_MouseClick);
            // 
            // icImages
            // 
            this.icImages.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icImages.ImageStream")));
            this.icImages.Images.SetKeyName(0, "Common_Close_1.png");
            this.icImages.Images.SetKeyName(1, "Common_Close_2.png");
            // 
            // lblMaxLengthTip
            // 
            this.lblMaxLengthTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblMaxLengthTip.Appearance.Options.UseForeColor = true;
            this.lblMaxLengthTip.Enabled = false;
            this.lblMaxLengthTip.Location = new System.Drawing.Point(376, 75);
            this.lblMaxLengthTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblMaxLengthTip.Name = "lblMaxLengthTip";
            this.lblMaxLengthTip.Size = new System.Drawing.Size(7, 14);
            this.lblMaxLengthTip.TabIndex = 61;
            this.lblMaxLengthTip.Text = "*";
            this.lblMaxLengthTip.Visible = false;
            // 
            // ceAutoComplete
            // 
            this.ceAutoComplete.Location = new System.Drawing.Point(271, 133);
            this.ceAutoComplete.Name = "ceAutoComplete";
            this.ceAutoComplete.Properties.Caption = "自动完成";
            this.ceAutoComplete.Size = new System.Drawing.Size(74, 19);
            this.ceAutoComplete.TabIndex = 8;
            // 
            // cmbAssociatedDataField
            // 
            this.cmbAssociatedDataField.Location = new System.Drawing.Point(72, 133);
            this.cmbAssociatedDataField.Name = "cmbAssociatedDataField";
            this.cmbAssociatedDataField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAssociatedDataField.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbAssociatedDataField.Size = new System.Drawing.Size(254, 20);
            this.cmbAssociatedDataField.TabIndex = 62;
            // 
            // ccmbDataFieldSetting
            // 
            this.ccmbDataFieldSetting.EditValue = "";
            this.ccmbDataFieldSetting.Location = new System.Drawing.Point(72, 164);
            this.ccmbDataFieldSetting.Name = "ccmbDataFieldSetting";
            this.ccmbDataFieldSetting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbDataFieldSetting.Properties.PopupSizeable = false;
            this.ccmbDataFieldSetting.Properties.SelectAllItemVisible = false;
            this.ccmbDataFieldSetting.Size = new System.Drawing.Size(281, 20);
            this.ccmbDataFieldSetting.TabIndex = 10;
            // 
            // lblDataFieldSetting
            // 
            this.lblDataFieldSetting.Location = new System.Drawing.Point(6, 166);
            this.lblDataFieldSetting.Name = "lblDataFieldSetting";
            this.lblDataFieldSetting.Size = new System.Drawing.Size(60, 14);
            this.lblDataFieldSetting.TabIndex = 85;
            this.lblDataFieldSetting.Text = "字段设置：";
            // 
            // hleHelpContent
            // 
            this.hleHelpContent.EditValue = "设置...";
            this.hleHelpContent.Location = new System.Drawing.Point(332, 194);
            this.hleHelpContent.Name = "hleHelpContent";
            this.hleHelpContent.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleHelpContent.Properties.Appearance.Options.UseBackColor = true;
            this.hleHelpContent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleHelpContent.Size = new System.Drawing.Size(42, 18);
            this.hleHelpContent.TabIndex = 12;
            this.hleHelpContent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hleHelpContent_MouseClick);
            // 
            // txtHelpContent
            // 
            this.txtHelpContent.Location = new System.Drawing.Point(147, 194);
            this.txtHelpContent.Name = "txtHelpContent";
            this.txtHelpContent.Properties.MaxLength = 32;
            this.txtHelpContent.Properties.ReadOnly = true;
            this.txtHelpContent.Size = new System.Drawing.Size(179, 20);
            this.txtHelpContent.TabIndex = 87;
            // 
            // chkHelpEnabled
            // 
            this.chkHelpEnabled.Location = new System.Drawing.Point(70, 194);
            this.chkHelpEnabled.Name = "chkHelpEnabled";
            this.chkHelpEnabled.Properties.Caption = "启用帮助";
            this.chkHelpEnabled.Size = new System.Drawing.Size(71, 19);
            this.chkHelpEnabled.TabIndex = 11;
            // 
            // lblHelpEnabled
            // 
            this.lblHelpEnabled.Location = new System.Drawing.Point(6, 196);
            this.lblHelpEnabled.Name = "lblHelpEnabled";
            this.lblHelpEnabled.Size = new System.Drawing.Size(60, 14);
            this.lblHelpEnabled.TabIndex = 89;
            this.lblHelpEnabled.Text = "字段帮助：";
            // 
            // cmdDataFieldType
            // 
            this.cmdDataFieldType.Location = new System.Drawing.Point(154, 72);
            this.cmdDataFieldType.Name = "cmdDataFieldType";
            this.cmdDataFieldType.OnlySelectedLeaf = true;
            this.cmdDataFieldType.Size = new System.Drawing.Size(155, 21);
            this.cmdDataFieldType.TabIndex = 5;
            this.cmdDataFieldType.AfterTreeNodeSelect += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.cmdDataFieldType_AfterTreeNodeSelect);
            // 
            // DataFieldModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblHelpEnabled);
            this.Controls.Add(this.hleHelpContent);
            this.Controls.Add(this.txtHelpContent);
            this.Controls.Add(this.chkHelpEnabled);
            this.Controls.Add(this.ccmbDataFieldSetting);
            this.Controls.Add(this.lblDataFieldSetting);
            this.Controls.Add(this.lblMaxLengthTip);
            this.Controls.Add(this.cmdDataFieldType);
            this.Controls.Add(this.hleDataFieldSetting);
            this.Controls.Add(this.txtConditionValue);
            this.Controls.Add(this.txtMaxLength);
            this.Controls.Add(this.ccmbDataFieldAttributes);
            this.Controls.Add(this.lblConditionTip);
            this.Controls.Add(this.lblInformationTip);
            this.Controls.Add(this.icmbDataFieldProperty);
            this.Controls.Add(this.lblDataFieldAttributes);
            this.Controls.Add(this.lblCondition);
            this.Controls.Add(this.lnkDataFieldList);
            this.Controls.Add(this.txtToolTip);
            this.Controls.Add(this.lblToolTip);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNameTip);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtDataFieldCode);
            this.Controls.Add(this.lblDataFieldType);
            this.Controls.Add(this.txtPhysicalName);
            this.Controls.Add(this.lblInformation);
            this.Controls.Add(this.txtLogicalName);
            this.Controls.Add(this.lblLogicalName);
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.cmbAssociatedDataField);
            this.Controls.Add(this.ceAutoComplete);
            this.Name = "DataFieldModule";
            this.Size = new System.Drawing.Size(393, 368);
            this.Load += new System.EventHandler(this.DataFieldModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFieldCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhysicalName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogicalName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToolTip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDataFieldList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataFieldProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldAttributes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConditionValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxLength.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleDataFieldSetting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceAutoComplete.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAssociatedDataField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldSetting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleHelpContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHelpContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHelpEnabled.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.TextEdit txtDataFieldCode;
        private DevExpress.XtraEditors.LabelControl lblDataFieldType;
        private DevExpress.XtraEditors.TextEdit txtPhysicalName;
        private DevExpress.XtraEditors.LabelControl lblInformation;
        private DevExpress.XtraEditors.TextEdit txtLogicalName;
        private DevExpress.XtraEditors.LabelControl lblLogicalName;
        private DevExpress.XtraEditors.LabelControl lblNameTip;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.MemoEdit txtToolTip;
        private DevExpress.XtraEditors.LabelControl lblToolTip;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDataFieldList;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LabelControl lblCondition;
        private DevExpress.XtraEditors.LabelControl lblDataFieldAttributes;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDataFieldProperty;
        private DevExpress.XtraEditors.LabelControl lblInformationTip;
        private DevExpress.XtraEditors.LabelControl lblConditionTip;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbDataFieldAttributes;
        private DevExpress.XtraEditors.TextEdit txtConditionValue;
        private DevExpress.XtraEditors.TextEdit txtMaxLength;
        private DevExpress.Utils.ImageCollection icDataFieldProperty;
        private DevExpress.Utils.ImageCollection icDataFieldType;
        private DevExpress.XtraEditors.HyperLinkEdit hleDataFieldSetting;
        private AppFramework.WinFormsControls.ComoboxTreeview cmdDataFieldType;
        private DevExpress.Utils.ImageCollection icImages;
        private DevExpress.XtraEditors.LabelControl lblMaxLengthTip;
        private DevExpress.XtraEditors.CheckEdit ceAutoComplete;
        private DevExpress.XtraEditors.ComboBoxEdit cmbAssociatedDataField;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbDataFieldSetting;
        private DevExpress.XtraEditors.LabelControl lblDataFieldSetting;
        private DevExpress.XtraEditors.HyperLinkEdit hleHelpContent;
        private DevExpress.XtraEditors.TextEdit txtHelpContent;
        private DevExpress.XtraEditors.CheckEdit chkHelpEnabled;
        private DevExpress.XtraEditors.LabelControl lblHelpEnabled;
    }
}
