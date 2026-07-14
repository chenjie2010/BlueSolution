namespace Blue.WindowsFormsClient.DataConvertionModule
{
    partial class RemoteDataModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteDataModule));
            this.txtRemoteDataCode = new DevExpress.XtraEditors.TextEdit();
            this.lblRemoteProperty = new DevExpress.XtraEditors.LabelControl();
            this.lblRemoteDataCode = new DevExpress.XtraEditors.LabelControl();
            this.txtRemoteDataName = new DevExpress.XtraEditors.TextEdit();
            this.lblRemoteDataName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.icDataRelationType = new DevExpress.Utils.ImageCollection(this.components);
            this.lblDataRelationTypeRequired = new DevExpress.XtraEditors.LabelControl();
            this.ccmbRemoteProperty = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblRemoteDataCodeReuqired = new DevExpress.XtraEditors.LabelControl();
            this.btxtDatabaseName = new DevExpress.XtraEditors.ButtonEdit();
            this.btxtParentDatabaseName = new DevExpress.XtraEditors.ButtonEdit();
            this.lblParentDatabaseName = new DevExpress.XtraEditors.LabelControl();
            this.lblDatabaseName = new DevExpress.XtraEditors.LabelControl();
            this.lblParentDatabaseNameTip = new DevExpress.XtraEditors.LabelControl();
            this.lblDatabaseNameTip = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemoteDataCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemoteDataName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataRelationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbRemoteProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtParentDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRemoteDataCode
            // 
            this.txtRemoteDataCode.Location = new System.Drawing.Point(82, 42);
            this.txtRemoteDataCode.Name = "txtRemoteDataCode";
            this.txtRemoteDataCode.Properties.MaxLength = 32;
            this.txtRemoteDataCode.Properties.ReadOnly = true;
            this.txtRemoteDataCode.Size = new System.Drawing.Size(282, 20);
            this.txtRemoteDataCode.TabIndex = 2;
            // 
            // lblRemoteProperty
            // 
            this.lblRemoteProperty.Location = new System.Drawing.Point(19, 75);
            this.lblRemoteProperty.Name = "lblRemoteProperty";
            this.lblRemoteProperty.Size = new System.Drawing.Size(60, 14);
            this.lblRemoteProperty.TabIndex = 17;
            this.lblRemoteProperty.Text = "交换属性：";
            // 
            // lblRemoteDataCode
            // 
            this.lblRemoteDataCode.Location = new System.Drawing.Point(19, 44);
            this.lblRemoteDataCode.Name = "lblRemoteDataCode";
            this.lblRemoteDataCode.Size = new System.Drawing.Size(60, 14);
            this.lblRemoteDataCode.TabIndex = 13;
            this.lblRemoteDataCode.Text = "交换编码：";
            // 
            // txtRemoteDataName
            // 
            this.txtRemoteDataName.Location = new System.Drawing.Point(82, 11);
            this.txtRemoteDataName.Name = "txtRemoteDataName";
            this.txtRemoteDataName.Properties.MaxLength = 64;
            this.txtRemoteDataName.Size = new System.Drawing.Size(282, 20);
            this.txtRemoteDataName.TabIndex = 1;
            // 
            // lblRemoteDataName
            // 
            this.lblRemoteDataName.Location = new System.Drawing.Point(19, 13);
            this.lblRemoteDataName.Name = "lblRemoteDataName";
            this.lblRemoteDataName.Size = new System.Drawing.Size(60, 14);
            this.lblRemoteDataName.TabIndex = 15;
            this.lblRemoteDataName.Text = "交换名称：";
            // 
            // lblNameRequired
            // 
            this.lblNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameRequired.Appearance.Options.UseForeColor = true;
            this.lblNameRequired.Location = new System.Drawing.Point(369, 16);
            this.lblNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameRequired.Name = "lblNameRequired";
            this.lblNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblNameRequired.TabIndex = 22;
            this.lblNameRequired.Text = "*";
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(43, 168);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
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
            // icDataRelationType
            // 
            this.icDataRelationType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataRelationType.ImageStream")));
            this.icDataRelationType.Images.SetKeyName(0, "Business_Table.png");
            this.icDataRelationType.Images.SetKeyName(1, "BarButtonItem_View.png");
            this.icDataRelationType.Images.SetKeyName(2, "Business_System_Table.png");
            // 
            // lblDataRelationTypeRequired
            // 
            this.lblDataRelationTypeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblDataRelationTypeRequired.Appearance.Options.UseForeColor = true;
            this.lblDataRelationTypeRequired.Location = new System.Drawing.Point(369, 78);
            this.lblDataRelationTypeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblDataRelationTypeRequired.Name = "lblDataRelationTypeRequired";
            this.lblDataRelationTypeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblDataRelationTypeRequired.TabIndex = 101;
            this.lblDataRelationTypeRequired.Text = "*";
            this.lblDataRelationTypeRequired.Visible = false;
            // 
            // ccmbRemoteProperty
            // 
            this.ccmbRemoteProperty.Location = new System.Drawing.Point(82, 73);
            this.ccmbRemoteProperty.Name = "ccmbRemoteProperty";
            this.ccmbRemoteProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbRemoteProperty.Size = new System.Drawing.Size(282, 20);
            this.ccmbRemoteProperty.TabIndex = 3;
            // 
            // lblRemoteDataCodeReuqired
            // 
            this.lblRemoteDataCodeReuqired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblRemoteDataCodeReuqired.Appearance.Options.UseForeColor = true;
            this.lblRemoteDataCodeReuqired.Location = new System.Drawing.Point(369, 47);
            this.lblRemoteDataCodeReuqired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblRemoteDataCodeReuqired.Name = "lblRemoteDataCodeReuqired";
            this.lblRemoteDataCodeReuqired.Size = new System.Drawing.Size(7, 14);
            this.lblRemoteDataCodeReuqired.TabIndex = 106;
            this.lblRemoteDataCodeReuqired.Text = "*";
            this.lblRemoteDataCodeReuqired.Visible = false;
            // 
            // btxtDatabaseName
            // 
            this.btxtDatabaseName.Location = new System.Drawing.Point(82, 135);
            this.btxtDatabaseName.Name = "btxtDatabaseName";
            this.btxtDatabaseName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtDatabaseName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtDatabaseName.Size = new System.Drawing.Size(282, 20);
            this.btxtDatabaseName.TabIndex = 108;
            this.btxtDatabaseName.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtDatabaseName_ButtonPressed);
            // 
            // btxtParentDatabaseName
            // 
            this.btxtParentDatabaseName.Location = new System.Drawing.Point(82, 104);
            this.btxtParentDatabaseName.Name = "btxtParentDatabaseName";
            this.btxtParentDatabaseName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtParentDatabaseName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtParentDatabaseName.Size = new System.Drawing.Size(282, 20);
            this.btxtParentDatabaseName.TabIndex = 107;
            this.btxtParentDatabaseName.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtParentDatabaseName_ButtonPressed);
            // 
            // lblParentDatabaseName
            // 
            this.lblParentDatabaseName.Location = new System.Drawing.Point(7, 106);
            this.lblParentDatabaseName.Name = "lblParentDatabaseName";
            this.lblParentDatabaseName.Size = new System.Drawing.Size(72, 14);
            this.lblParentDatabaseName.TabIndex = 112;
            this.lblParentDatabaseName.Text = "远程数据库：";
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.Location = new System.Drawing.Point(7, 137);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(72, 14);
            this.lblDatabaseName.TabIndex = 111;
            this.lblDatabaseName.Text = "本地数据库：";
            // 
            // lblParentDatabaseNameTip
            // 
            this.lblParentDatabaseNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblParentDatabaseNameTip.Appearance.Options.UseForeColor = true;
            this.lblParentDatabaseNameTip.Location = new System.Drawing.Point(369, 110);
            this.lblParentDatabaseNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblParentDatabaseNameTip.Name = "lblParentDatabaseNameTip";
            this.lblParentDatabaseNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblParentDatabaseNameTip.TabIndex = 110;
            this.lblParentDatabaseNameTip.Text = "*";
            // 
            // lblDatabaseNameTip
            // 
            this.lblDatabaseNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblDatabaseNameTip.Appearance.Options.UseForeColor = true;
            this.lblDatabaseNameTip.Location = new System.Drawing.Point(369, 138);
            this.lblDatabaseNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblDatabaseNameTip.Name = "lblDatabaseNameTip";
            this.lblDatabaseNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblDatabaseNameTip.TabIndex = 109;
            this.lblDatabaseNameTip.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(82, 166);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(282, 161);
            this.txtNotes.TabIndex = 8;
            // 
            // RemoteDataModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btxtDatabaseName);
            this.Controls.Add(this.btxtParentDatabaseName);
            this.Controls.Add(this.lblParentDatabaseName);
            this.Controls.Add(this.lblDatabaseName);
            this.Controls.Add(this.lblParentDatabaseNameTip);
            this.Controls.Add(this.lblDatabaseNameTip);
            this.Controls.Add(this.lblRemoteDataCodeReuqired);
            this.Controls.Add(this.ccmbRemoteProperty);
            this.Controls.Add(this.lblDataRelationTypeRequired);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNameRequired);
            this.Controls.Add(this.txtRemoteDataCode);
            this.Controls.Add(this.lblRemoteProperty);
            this.Controls.Add(this.lblRemoteDataCode);
            this.Controls.Add(this.txtRemoteDataName);
            this.Controls.Add(this.lblRemoteDataName);
            this.Name = "RemoteDataModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.RemoteDataModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtRemoteDataCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemoteDataName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataRelationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbRemoteProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtDatabaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtParentDatabaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtRemoteDataCode;
        private DevExpress.XtraEditors.LabelControl lblRemoteDataCode;
        private DevExpress.XtraEditors.TextEdit txtRemoteDataName;
        private DevExpress.XtraEditors.LabelControl lblNameRequired;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.LabelControl lblRemoteDataName;
        private DevExpress.Utils.ImageCollection icDataRelationType;
        private DevExpress.XtraEditors.LabelControl lblDataRelationTypeRequired;
        private DevExpress.XtraEditors.LabelControl lblRemoteProperty;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbRemoteProperty;
        private DevExpress.XtraEditors.LabelControl lblRemoteDataCodeReuqired;
        private DevExpress.XtraEditors.ButtonEdit btxtDatabaseName;
        private DevExpress.XtraEditors.ButtonEdit btxtParentDatabaseName;
        private DevExpress.XtraEditors.LabelControl lblParentDatabaseName;
        private DevExpress.XtraEditors.LabelControl lblDatabaseName;
        private DevExpress.XtraEditors.LabelControl lblParentDatabaseNameTip;
        private DevExpress.XtraEditors.LabelControl lblDatabaseNameTip;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
    }
}
