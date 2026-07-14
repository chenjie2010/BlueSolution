namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    partial class AssociatedDataFieldModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssociatedDataFieldModule));
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFieldCode = new DevExpress.XtraEditors.TextEdit();
            this.lblDataFieldCode = new DevExpress.XtraEditors.LabelControl();
            this.lblBasedDatType = new DevExpress.XtraEditors.LabelControl();
            this.txtPhysicalName = new DevExpress.XtraEditors.TextEdit();
            this.lblPhysicalName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameTip = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.lnkDataFieldList = new DevExpress.XtraEditors.HyperLinkEdit();
            this.icmbBasedDatType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icBasedDatType = new DevExpress.Utils.ImageCollection(this.components);
            this.lblBasedDatTypeTip = new DevExpress.XtraEditors.LabelControl();
            this.lblDataFieldCodeTip = new DevExpress.XtraEditors.LabelControl();
            this.txtMaxLength = new DevExpress.XtraEditors.TextEdit();
            this.lblIsHierarchal = new DevExpress.XtraEditors.LabelControl();
            this.lblDataFieldCategory = new DevExpress.XtraEditors.LabelControl();
            this.ceIsHierarchal = new DevExpress.XtraEditors.CheckEdit();
            this.icmbDataFieldCategory = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icDataFieldCategory = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFieldCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhysicalName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDataFieldList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbBasedDatType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icBasedDatType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxLength.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceIsHierarchal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataFieldCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldCategory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(35, 209);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 21;
            this.lblNotes.Text = "备注：";
            // 
            // txtDataFieldCode
            // 
            this.txtDataFieldCode.Location = new System.Drawing.Point(77, 80);
            this.txtDataFieldCode.Name = "txtDataFieldCode";
            this.txtDataFieldCode.Size = new System.Drawing.Size(281, 20);
            this.txtDataFieldCode.TabIndex = 3;
            // 
            // lblDataFieldCode
            // 
            this.lblDataFieldCode.Location = new System.Drawing.Point(11, 83);
            this.lblDataFieldCode.Name = "lblDataFieldCode";
            this.lblDataFieldCode.Size = new System.Drawing.Size(60, 14);
            this.lblDataFieldCode.TabIndex = 19;
            this.lblDataFieldCode.Text = "字段编码：";
            // 
            // lblBasedDatType
            // 
            this.lblBasedDatType.Location = new System.Drawing.Point(11, 116);
            this.lblBasedDatType.Name = "lblBasedDatType";
            this.lblBasedDatType.Size = new System.Drawing.Size(60, 14);
            this.lblBasedDatType.TabIndex = 17;
            this.lblBasedDatType.Text = "字段类型：";
            // 
            // txtPhysicalName
            // 
            this.txtPhysicalName.Location = new System.Drawing.Point(77, 48);
            this.txtPhysicalName.Name = "txtPhysicalName";
            this.txtPhysicalName.Properties.ReadOnly = true;
            this.txtPhysicalName.Size = new System.Drawing.Size(281, 20);
            this.txtPhysicalName.TabIndex = 2;
            // 
            // lblPhysicalName
            // 
            this.lblPhysicalName.Location = new System.Drawing.Point(11, 50);
            this.lblPhysicalName.Name = "lblPhysicalName";
            this.lblPhysicalName.Size = new System.Drawing.Size(60, 14);
            this.lblPhysicalName.TabIndex = 13;
            this.lblPhysicalName.Text = "物理名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(77, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(282, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(11, 18);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(60, 14);
            this.lblName.TabIndex = 15;
            this.lblName.Text = "字段名称：";
            // 
            // lblNameTip
            // 
            this.lblNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameTip.Appearance.Options.UseForeColor = true;
            this.lblNameTip.Location = new System.Drawing.Point(364, 19);
            this.lblNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameTip.Name = "lblNameTip";
            this.lblNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblNameTip.TabIndex = 22;
            this.lblNameTip.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(77, 208);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(280, 103);
            this.txtNotes.TabIndex = 5;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(77, 311);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(280, 23);
            this.separatorControl1.TabIndex = 27;
            // 
            // lnkDataFieldList
            // 
            this.lnkDataFieldList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDataFieldList.EditValue = "该关联字段与{0}个字段关联";
            this.lnkDataFieldList.Location = new System.Drawing.Point(119, 334);
            this.lnkDataFieldList.Name = "lnkDataFieldList";
            this.lnkDataFieldList.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDataFieldList.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDataFieldList.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDataFieldList.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDataFieldList.Size = new System.Drawing.Size(263, 22);
            this.lnkDataFieldList.TabIndex = 29;
            this.lnkDataFieldList.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.lnkDataFieldList_OpenLink);
            this.lnkDataFieldList.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.lnkDataFieldList_ButtonClick);
            // 
            // icmbBasedDatType
            // 
            this.icmbBasedDatType.Location = new System.Drawing.Point(77, 115);
            this.icmbBasedDatType.Name = "icmbBasedDatType";
            this.icmbBasedDatType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbBasedDatType.Properties.SmallImages = this.icBasedDatType;
            this.icmbBasedDatType.Size = new System.Drawing.Size(226, 20);
            this.icmbBasedDatType.TabIndex = 36;
            this.icmbBasedDatType.SelectedIndexChanged += new System.EventHandler(this.icmbBasedDatType_SelectedIndexChanged);
            // 
            // icBasedDatType
            // 
            this.icBasedDatType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icBasedDatType.ImageStream")));
            this.icBasedDatType.Images.SetKeyName(0, "Database_DataType_Bool.png");
            this.icBasedDatType.Images.SetKeyName(1, "Database_DataType_Number.png");
            this.icBasedDatType.Images.SetKeyName(2, "Database_DataType_Decimal.png");
            this.icBasedDatType.Images.SetKeyName(3, "Database_DataType_String.png");
            this.icBasedDatType.Images.SetKeyName(4, "Database_DataType_Date.png");
            this.icBasedDatType.Images.SetKeyName(5, "Database_DataType_6.png");
            this.icBasedDatType.Images.SetKeyName(6, "Database_DataType_7.png");
            this.icBasedDatType.Images.SetKeyName(7, "Database_DataType_8.png");
            // 
            // lblBasedDatTypeTip
            // 
            this.lblBasedDatTypeTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblBasedDatTypeTip.Appearance.Options.UseForeColor = true;
            this.lblBasedDatTypeTip.Location = new System.Drawing.Point(364, 117);
            this.lblBasedDatTypeTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblBasedDatTypeTip.Name = "lblBasedDatTypeTip";
            this.lblBasedDatTypeTip.Size = new System.Drawing.Size(7, 14);
            this.lblBasedDatTypeTip.TabIndex = 37;
            this.lblBasedDatTypeTip.Text = "*";
            // 
            // lblDataFieldCodeTip
            // 
            this.lblDataFieldCodeTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblDataFieldCodeTip.Appearance.Options.UseForeColor = true;
            this.lblDataFieldCodeTip.Location = new System.Drawing.Point(364, 90);
            this.lblDataFieldCodeTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblDataFieldCodeTip.Name = "lblDataFieldCodeTip";
            this.lblDataFieldCodeTip.Size = new System.Drawing.Size(7, 14);
            this.lblDataFieldCodeTip.TabIndex = 38;
            this.lblDataFieldCodeTip.Text = "*";
            // 
            // txtMaxLength
            // 
            this.txtMaxLength.Location = new System.Drawing.Point(306, 115);
            this.txtMaxLength.Name = "txtMaxLength";
            this.txtMaxLength.Properties.MaxLength = 4;
            this.txtMaxLength.Properties.NullValuePrompt = "字符长度";
            this.txtMaxLength.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMaxLength.Size = new System.Drawing.Size(51, 20);
            this.txtMaxLength.TabIndex = 48;
            this.txtMaxLength.Visible = false;
            // 
            // lblIsHierarchal
            // 
            this.lblIsHierarchal.Location = new System.Drawing.Point(11, 179);
            this.lblIsHierarchal.Name = "lblIsHierarchal";
            this.lblIsHierarchal.Size = new System.Drawing.Size(60, 14);
            this.lblIsHierarchal.TabIndex = 49;
            this.lblIsHierarchal.Text = "级联字段：";
            // 
            // lblDataFieldCategory
            // 
            this.lblDataFieldCategory.Location = new System.Drawing.Point(11, 150);
            this.lblDataFieldCategory.Name = "lblDataFieldCategory";
            this.lblDataFieldCategory.Size = new System.Drawing.Size(60, 14);
            this.lblDataFieldCategory.TabIndex = 50;
            this.lblDataFieldCategory.Text = "字段类别：";
            // 
            // ceIsHierarchal
            // 
            this.ceIsHierarchal.Location = new System.Drawing.Point(77, 177);
            this.ceIsHierarchal.Name = "ceIsHierarchal";
            this.ceIsHierarchal.Properties.Caption = "";
            this.ceIsHierarchal.Size = new System.Drawing.Size(25, 19);
            this.ceIsHierarchal.TabIndex = 52;
            // 
            // icmbDataFieldCategory
            // 
            this.icmbDataFieldCategory.Location = new System.Drawing.Point(77, 148);
            this.icmbDataFieldCategory.Name = "icmbDataFieldCategory";
            this.icmbDataFieldCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDataFieldCategory.Properties.SmallImages = this.icDataFieldCategory;
            this.icmbDataFieldCategory.Size = new System.Drawing.Size(280, 20);
            this.icmbDataFieldCategory.TabIndex = 62;
            // 
            // icDataFieldCategory
            // 
            this.icDataFieldCategory.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataFieldCategory.ImageStream")));
            this.icDataFieldCategory.Images.SetKeyName(0, "Common_Primary_Item_Small.png");
            this.icDataFieldCategory.Images.SetKeyName(1, "Common_Addition_Item_Small.png");
            // 
            // AssociatedDataFieldModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.icmbDataFieldCategory);
            this.Controls.Add(this.ceIsHierarchal);
            this.Controls.Add(this.lblDataFieldCategory);
            this.Controls.Add(this.lblIsHierarchal);
            this.Controls.Add(this.txtMaxLength);
            this.Controls.Add(this.lblDataFieldCodeTip);
            this.Controls.Add(this.lblBasedDatTypeTip);
            this.Controls.Add(this.icmbBasedDatType);
            this.Controls.Add(this.lnkDataFieldList);
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNameTip);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtDataFieldCode);
            this.Controls.Add(this.lblDataFieldCode);
            this.Controls.Add(this.lblBasedDatType);
            this.Controls.Add(this.txtPhysicalName);
            this.Controls.Add(this.lblPhysicalName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Name = "AssociatedDataFieldModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.AssociatedDataFieldModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFieldCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhysicalName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDataFieldList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbBasedDatType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icBasedDatType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxLength.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceIsHierarchal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataFieldCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldCategory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.TextEdit txtDataFieldCode;
        private DevExpress.XtraEditors.LabelControl lblDataFieldCode;
        private DevExpress.XtraEditors.LabelControl lblBasedDatType;
        private DevExpress.XtraEditors.TextEdit txtPhysicalName;
        private DevExpress.XtraEditors.LabelControl lblPhysicalName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblNameTip;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDataFieldList;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbBasedDatType;
        private DevExpress.XtraEditors.LabelControl lblBasedDatTypeTip;
        private DevExpress.XtraEditors.LabelControl lblDataFieldCodeTip;
        private DevExpress.XtraEditors.TextEdit txtMaxLength;
        private DevExpress.Utils.ImageCollection icBasedDatType;
        private DevExpress.XtraEditors.LabelControl lblIsHierarchal;
        private DevExpress.XtraEditors.LabelControl lblDataFieldCategory;
        private DevExpress.XtraEditors.CheckEdit ceIsHierarchal;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDataFieldCategory;
        private DevExpress.Utils.ImageCollection icDataFieldCategory;
    }
}
