namespace Blue.WindowsFormsClient.DataConvertionModule
{
    partial class DataRelationModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataRelationModule));
            this.txtRelationCode = new DevExpress.XtraEditors.TextEdit();
            this.lblDataRelationProperty = new DevExpress.XtraEditors.LabelControl();
            this.lblRelationCode = new DevExpress.XtraEditors.LabelControl();
            this.txtRelationName = new DevExpress.XtraEditors.TextEdit();
            this.lblRelationName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.lblDatabaseNameTip = new DevExpress.XtraEditors.LabelControl();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lblParentDatabaseNameTip = new DevExpress.XtraEditors.LabelControl();
            this.lblDatabaseName = new DevExpress.XtraEditors.LabelControl();
            this.icDataRelationType = new DevExpress.Utils.ImageCollection(this.components);
            this.lblParentDatabaseName = new DevExpress.XtraEditors.LabelControl();
            this.btxtParentDatabaseName = new DevExpress.XtraEditors.ButtonEdit();
            this.lblDataRelationType = new DevExpress.XtraEditors.LabelControl();
            this.btxtDatabaseName = new DevExpress.XtraEditors.ButtonEdit();
            this.lblDataRelationTypeRequired = new DevExpress.XtraEditors.LabelControl();
            this.ccmbDataRelationProperty = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.icmbDataRelationType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblRelationCodeReuqired = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtRelationCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRelationName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataRelationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtParentDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataRelationProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataRelationType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRelationCode
            // 
            this.txtRelationCode.Location = new System.Drawing.Point(82, 41);
            this.txtRelationCode.Name = "txtRelationCode";
            this.txtRelationCode.Properties.MaxLength = 32;
            this.txtRelationCode.Properties.ReadOnly = true;
            this.txtRelationCode.Size = new System.Drawing.Size(282, 20);
            this.txtRelationCode.TabIndex = 2;
            // 
            // lblDataRelationProperty
            // 
            this.lblDataRelationProperty.Location = new System.Drawing.Point(19, 105);
            this.lblDataRelationProperty.Name = "lblDataRelationProperty";
            this.lblDataRelationProperty.Size = new System.Drawing.Size(60, 14);
            this.lblDataRelationProperty.TabIndex = 17;
            this.lblDataRelationProperty.Text = "交换属性：";
            // 
            // lblRelationCode
            // 
            this.lblRelationCode.Location = new System.Drawing.Point(19, 43);
            this.lblRelationCode.Name = "lblRelationCode";
            this.lblRelationCode.Size = new System.Drawing.Size(60, 14);
            this.lblRelationCode.TabIndex = 13;
            this.lblRelationCode.Text = "交换编码：";
            // 
            // txtRelationName
            // 
            this.txtRelationName.Location = new System.Drawing.Point(82, 10);
            this.txtRelationName.Name = "txtRelationName";
            this.txtRelationName.Properties.MaxLength = 64;
            this.txtRelationName.Size = new System.Drawing.Size(282, 20);
            this.txtRelationName.TabIndex = 1;
            // 
            // lblRelationName
            // 
            this.lblRelationName.Location = new System.Drawing.Point(19, 12);
            this.lblRelationName.Name = "lblRelationName";
            this.lblRelationName.Size = new System.Drawing.Size(60, 14);
            this.lblRelationName.TabIndex = 15;
            this.lblRelationName.Text = "交换名称：";
            // 
            // lblNameRequired
            // 
            this.lblNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameRequired.Appearance.Options.UseForeColor = true;
            this.lblNameRequired.Location = new System.Drawing.Point(369, 14);
            this.lblNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameRequired.Name = "lblNameRequired";
            this.lblNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblNameRequired.TabIndex = 22;
            this.lblNameRequired.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(82, 196);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(282, 125);
            this.txtNotes.TabIndex = 7;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(43, 198);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // lblDatabaseNameTip
            // 
            this.lblDatabaseNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblDatabaseNameTip.Appearance.Options.UseForeColor = true;
            this.lblDatabaseNameTip.Location = new System.Drawing.Point(369, 168);
            this.lblDatabaseNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblDatabaseNameTip.Name = "lblDatabaseNameTip";
            this.lblDatabaseNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblDatabaseNameTip.TabIndex = 24;
            this.lblDatabaseNameTip.Text = "*";
            // 
            // lnkDetailedView
            // 
            this.lnkDetailedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedView.EditValue = "交换详情";
            this.lnkDetailedView.Location = new System.Drawing.Point(77, 333);
            this.lnkDetailedView.Name = "lnkDetailedView";
            this.lnkDetailedView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedView.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedView.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedView.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedView.TabIndex = 10;
            // 
            // lblParentDatabaseNameTip
            // 
            this.lblParentDatabaseNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblParentDatabaseNameTip.Appearance.Options.UseForeColor = true;
            this.lblParentDatabaseNameTip.Location = new System.Drawing.Point(369, 140);
            this.lblParentDatabaseNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblParentDatabaseNameTip.Name = "lblParentDatabaseNameTip";
            this.lblParentDatabaseNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblParentDatabaseNameTip.TabIndex = 70;
            this.lblParentDatabaseNameTip.Text = "*";
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.Location = new System.Drawing.Point(7, 167);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(72, 14);
            this.lblDatabaseName.TabIndex = 79;
            this.lblDatabaseName.Text = "目标数据库：";
            // 
            // icDataRelationType
            // 
            this.icDataRelationType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataRelationType.ImageStream")));
            this.icDataRelationType.Images.SetKeyName(0, "Business_Table.png");
            this.icDataRelationType.Images.SetKeyName(1, "BarButtonItem_View.png");
            this.icDataRelationType.Images.SetKeyName(2, "Business_System_Table.png");
            // 
            // lblParentDatabaseName
            // 
            this.lblParentDatabaseName.Location = new System.Drawing.Point(19, 136);
            this.lblParentDatabaseName.Name = "lblParentDatabaseName";
            this.lblParentDatabaseName.Size = new System.Drawing.Size(60, 14);
            this.lblParentDatabaseName.TabIndex = 82;
            this.lblParentDatabaseName.Text = "源数据库：";
            // 
            // btxtParentDatabaseName
            // 
            this.btxtParentDatabaseName.Location = new System.Drawing.Point(82, 134);
            this.btxtParentDatabaseName.Name = "btxtParentDatabaseName";
            this.btxtParentDatabaseName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtParentDatabaseName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtParentDatabaseName.Size = new System.Drawing.Size(282, 20);
            this.btxtParentDatabaseName.TabIndex = 5;
            this.btxtParentDatabaseName.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtParentDatabaseName_ButtonPressed);
            // 
            // lblDataRelationType
            // 
            this.lblDataRelationType.Location = new System.Drawing.Point(19, 74);
            this.lblDataRelationType.Name = "lblDataRelationType";
            this.lblDataRelationType.Size = new System.Drawing.Size(60, 14);
            this.lblDataRelationType.TabIndex = 84;
            this.lblDataRelationType.Text = "交换关系：";
            // 
            // btxtDatabaseName
            // 
            this.btxtDatabaseName.Location = new System.Drawing.Point(82, 165);
            this.btxtDatabaseName.Name = "btxtDatabaseName";
            this.btxtDatabaseName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtDatabaseName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtDatabaseName.Size = new System.Drawing.Size(282, 20);
            this.btxtDatabaseName.TabIndex = 6;
            this.btxtDatabaseName.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtDatabaseName_ButtonPressed);
            // 
            // lblDataRelationTypeRequired
            // 
            this.lblDataRelationTypeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblDataRelationTypeRequired.Appearance.Options.UseForeColor = true;
            this.lblDataRelationTypeRequired.Location = new System.Drawing.Point(369, 79);
            this.lblDataRelationTypeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblDataRelationTypeRequired.Name = "lblDataRelationTypeRequired";
            this.lblDataRelationTypeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblDataRelationTypeRequired.TabIndex = 101;
            this.lblDataRelationTypeRequired.Text = "*";
            this.lblDataRelationTypeRequired.Visible = false;
            // 
            // ccmbDataRelationProperty
            // 
            this.ccmbDataRelationProperty.Location = new System.Drawing.Point(82, 103);
            this.ccmbDataRelationProperty.Name = "ccmbDataRelationProperty";
            this.ccmbDataRelationProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbDataRelationProperty.Size = new System.Drawing.Size(282, 20);
            this.ccmbDataRelationProperty.TabIndex = 4;
            // 
            // icmbDataRelationType
            // 
            this.icmbDataRelationType.Location = new System.Drawing.Point(82, 72);
            this.icmbDataRelationType.Name = "icmbDataRelationType";
            this.icmbDataRelationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDataRelationType.Properties.SmallImages = this.icDataRelationType;
            this.icmbDataRelationType.Size = new System.Drawing.Size(282, 20);
            this.icmbDataRelationType.TabIndex = 3;
            // 
            // lblRelationCodeReuqired
            // 
            this.lblRelationCodeReuqired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblRelationCodeReuqired.Appearance.Options.UseForeColor = true;
            this.lblRelationCodeReuqired.Location = new System.Drawing.Point(369, 47);
            this.lblRelationCodeReuqired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblRelationCodeReuqired.Name = "lblRelationCodeReuqired";
            this.lblRelationCodeReuqired.Size = new System.Drawing.Size(7, 14);
            this.lblRelationCodeReuqired.TabIndex = 106;
            this.lblRelationCodeReuqired.Text = "*";
            this.lblRelationCodeReuqired.Visible = false;
            // 
            // DataRelationModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblRelationCodeReuqired);
            this.Controls.Add(this.icmbDataRelationType);
            this.Controls.Add(this.ccmbDataRelationProperty);
            this.Controls.Add(this.lblDataRelationTypeRequired);
            this.Controls.Add(this.btxtDatabaseName);
            this.Controls.Add(this.lblDataRelationType);
            this.Controls.Add(this.btxtParentDatabaseName);
            this.Controls.Add(this.lblParentDatabaseName);
            this.Controls.Add(this.lblDatabaseName);
            this.Controls.Add(this.lblParentDatabaseNameTip);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblDatabaseNameTip);
            this.Controls.Add(this.lblNameRequired);
            this.Controls.Add(this.txtRelationCode);
            this.Controls.Add(this.lblDataRelationProperty);
            this.Controls.Add(this.lblRelationCode);
            this.Controls.Add(this.txtRelationName);
            this.Controls.Add(this.lblRelationName);
            this.Name = "DataRelationModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.DataRelationModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtRelationCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRelationName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataRelationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtParentDatabaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtDatabaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataRelationProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataRelationType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtRelationCode;
        private DevExpress.XtraEditors.LabelControl lblRelationCode;
        private DevExpress.XtraEditors.TextEdit txtRelationName;
        private DevExpress.XtraEditors.LabelControl lblNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.LabelControl lblDatabaseNameTip;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.LabelControl lblParentDatabaseNameTip;
        private DevExpress.XtraEditors.LabelControl lblDatabaseName;
        private DevExpress.XtraEditors.LabelControl lblParentDatabaseName;
        private DevExpress.XtraEditors.LabelControl lblRelationName;
        private DevExpress.XtraEditors.ButtonEdit btxtParentDatabaseName;
        private DevExpress.XtraEditors.LabelControl lblDataRelationType;
        private DevExpress.Utils.ImageCollection icDataRelationType;
        private DevExpress.XtraEditors.ButtonEdit btxtDatabaseName;
        private DevExpress.XtraEditors.LabelControl lblDataRelationTypeRequired;
        private DevExpress.XtraEditors.LabelControl lblDataRelationProperty;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbDataRelationProperty;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDataRelationType;
        private DevExpress.XtraEditors.LabelControl lblRelationCodeReuqired;
    }
}
