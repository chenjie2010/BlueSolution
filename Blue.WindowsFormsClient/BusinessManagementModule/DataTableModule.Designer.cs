namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    partial class DataTableModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTableModule));
            this.txtTableCode = new DevExpress.XtraEditors.TextEdit();
            this.lblTableCode = new DevExpress.XtraEditors.LabelControl();
            this.lblTableType = new DevExpress.XtraEditors.LabelControl();
            this.txtPhysicalName = new DevExpress.XtraEditors.TextEdit();
            this.lblCode = new DevExpress.XtraEditors.LabelControl();
            this.txtLogicalName = new DevExpress.XtraEditors.TextEdit();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.icmbTableType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icTableType = new DevExpress.Utils.ImageCollection(this.components);
            this.lblTableCodeRequired = new DevExpress.XtraEditors.LabelControl();
            this.ccmbTableSetting = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblTableSetting = new DevExpress.XtraEditors.LabelControl();
            this.lblSystemTable = new DevExpress.XtraEditors.LabelControl();
            this.chkSystemTable = new DevExpress.XtraEditors.CheckEdit();
            this.icmbTableProperty = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icTableProperty = new DevExpress.Utils.ImageCollection(this.components);
            this.lblTableProperty = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhysicalName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogicalName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbTableSetting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSystemTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableProperty)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTableCode
            // 
            this.txtTableCode.Location = new System.Drawing.Point(78, 82);
            this.txtTableCode.Name = "txtTableCode";
            this.txtTableCode.Properties.MaxLength = 32;
            this.txtTableCode.Size = new System.Drawing.Size(281, 20);
            this.txtTableCode.TabIndex = 3;
            // 
            // lblTableCode
            // 
            this.lblTableCode.Location = new System.Drawing.Point(12, 85);
            this.lblTableCode.Name = "lblTableCode";
            this.lblTableCode.Size = new System.Drawing.Size(60, 14);
            this.lblTableCode.TabIndex = 19;
            this.lblTableCode.Text = "表的编码：";
            // 
            // lblTableType
            // 
            this.lblTableType.Location = new System.Drawing.Point(12, 149);
            this.lblTableType.Name = "lblTableType";
            this.lblTableType.Size = new System.Drawing.Size(60, 14);
            this.lblTableType.TabIndex = 17;
            this.lblTableType.Text = "表的类型：";
            // 
            // txtPhysicalName
            // 
            this.txtPhysicalName.Location = new System.Drawing.Point(78, 49);
            this.txtPhysicalName.Name = "txtPhysicalName";
            this.txtPhysicalName.Properties.ReadOnly = true;
            this.txtPhysicalName.Size = new System.Drawing.Size(281, 20);
            this.txtPhysicalName.TabIndex = 2;
            // 
            // lblCode
            // 
            this.lblCode.Location = new System.Drawing.Point(12, 52);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(60, 14);
            this.lblCode.TabIndex = 13;
            this.lblCode.Text = "物理名称：";
            // 
            // txtLogicalName
            // 
            this.txtLogicalName.Location = new System.Drawing.Point(78, 16);
            this.txtLogicalName.Name = "txtLogicalName";
            this.txtLogicalName.Properties.MaxLength = 64;
            this.txtLogicalName.Size = new System.Drawing.Size(282, 20);
            this.txtLogicalName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(12, 18);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(60, 14);
            this.lblName.TabIndex = 15;
            this.lblName.Text = "表的名称：";
            // 
            // lblNameRequired
            // 
            this.lblNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameRequired.Appearance.Options.UseForeColor = true;
            this.lblNameRequired.Location = new System.Drawing.Point(364, 23);
            this.lblNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameRequired.Name = "lblNameRequired";
            this.lblNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblNameRequired.TabIndex = 22;
            this.lblNameRequired.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(78, 244);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 95);
            this.txtNotes.TabIndex = 8;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(36, 246);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // icmbTableType
            // 
            this.icmbTableType.Location = new System.Drawing.Point(78, 146);
            this.icmbTableType.Name = "icmbTableType";
            this.icmbTableType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbTableType.Properties.SmallImages = this.icTableType;
            this.icmbTableType.Size = new System.Drawing.Size(281, 20);
            this.icmbTableType.TabIndex = 5;
            // 
            // icTableType
            // 
            this.icTableType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTableType.ImageStream")));
            this.icTableType.Images.SetKeyName(0, "Common_Primary_Table.png");
            this.icTableType.Images.SetKeyName(1, "Common_Assistant_Table.png");
            this.icTableType.Images.SetKeyName(2, "Common_Addtional_Table.png");
            this.icTableType.Images.SetKeyName(3, "Common_Business_Table.png");
            this.icTableType.Images.SetKeyName(4, "Common_Dependent_Business_Table.png");
            this.icTableType.Images.SetKeyName(5, "Common_System_Table.png");
            // 
            // lblTableCodeRequired
            // 
            this.lblTableCodeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblTableCodeRequired.Appearance.Options.UseForeColor = true;
            this.lblTableCodeRequired.Location = new System.Drawing.Point(364, 86);
            this.lblTableCodeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblTableCodeRequired.Name = "lblTableCodeRequired";
            this.lblTableCodeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblTableCodeRequired.TabIndex = 24;
            this.lblTableCodeRequired.Text = "*";
            // 
            // ccmbTableSetting
            // 
            this.ccmbTableSetting.EditValue = "";
            this.ccmbTableSetting.Location = new System.Drawing.Point(78, 209);
            this.ccmbTableSetting.Name = "ccmbTableSetting";
            this.ccmbTableSetting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbTableSetting.Properties.PopupSizeable = false;
            this.ccmbTableSetting.Properties.SelectAllItemVisible = false;
            this.ccmbTableSetting.Size = new System.Drawing.Size(281, 20);
            this.ccmbTableSetting.TabIndex = 7;
            // 
            // lblTableSetting
            // 
            this.lblTableSetting.Location = new System.Drawing.Point(12, 212);
            this.lblTableSetting.Name = "lblTableSetting";
            this.lblTableSetting.Size = new System.Drawing.Size(60, 14);
            this.lblTableSetting.TabIndex = 83;
            this.lblTableSetting.Text = "表的设置：";
            // 
            // lblSystemTable
            // 
            this.lblSystemTable.Location = new System.Drawing.Point(24, 179);
            this.lblSystemTable.Name = "lblSystemTable";
            this.lblSystemTable.Size = new System.Drawing.Size(48, 14);
            this.lblSystemTable.TabIndex = 84;
            this.lblSystemTable.Text = "系统表：";
            // 
            // chkSystemTable
            // 
            this.chkSystemTable.Location = new System.Drawing.Point(78, 177);
            this.chkSystemTable.Name = "chkSystemTable";
            this.chkSystemTable.Properties.Caption = "设置为系统表";
            this.chkSystemTable.Size = new System.Drawing.Size(103, 19);
            this.chkSystemTable.TabIndex = 6;
            // 
            // icmbTableProperty
            // 
            this.icmbTableProperty.Location = new System.Drawing.Point(78, 115);
            this.icmbTableProperty.Name = "icmbTableProperty";
            this.icmbTableProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbTableProperty.Properties.SmallImages = this.icTableProperty;
            this.icmbTableProperty.Size = new System.Drawing.Size(281, 20);
            this.icmbTableProperty.TabIndex = 4;
            // 
            // icTableProperty
            // 
            this.icTableProperty.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTableProperty.ImageStream")));
            this.icTableProperty.Images.SetKeyName(0, "TableProperty_Data.png");
            this.icTableProperty.Images.SetKeyName(1, "TableProperty_Department.png");
            this.icTableProperty.Images.SetKeyName(2, "TableProperty_UserType.png");
            this.icTableProperty.Images.SetKeyName(3, "TableProperty_Business.png");
            // 
            // lblTableProperty
            // 
            this.lblTableProperty.Location = new System.Drawing.Point(12, 117);
            this.lblTableProperty.Name = "lblTableProperty";
            this.lblTableProperty.Size = new System.Drawing.Size(60, 14);
            this.lblTableProperty.TabIndex = 86;
            this.lblTableProperty.Text = "表的属性：";
            // 
            // DataTableModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.icmbTableProperty);
            this.Controls.Add(this.lblTableProperty);
            this.Controls.Add(this.chkSystemTable);
            this.Controls.Add(this.lblSystemTable);
            this.Controls.Add(this.ccmbTableSetting);
            this.Controls.Add(this.lblTableSetting);
            this.Controls.Add(this.icmbTableType);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblTableCodeRequired);
            this.Controls.Add(this.lblNameRequired);
            this.Controls.Add(this.txtTableCode);
            this.Controls.Add(this.lblTableCode);
            this.Controls.Add(this.lblTableType);
            this.Controls.Add(this.txtPhysicalName);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtLogicalName);
            this.Controls.Add(this.lblName);
            this.Name = "DataTableModule";
            this.Size = new System.Drawing.Size(386, 357);
            this.Load += new System.EventHandler(this.DataTableModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtTableCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhysicalName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogicalName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbTableSetting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSystemTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableProperty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtTableCode;
        private DevExpress.XtraEditors.LabelControl lblTableCode;
        private DevExpress.XtraEditors.LabelControl lblTableType;
        private DevExpress.XtraEditors.TextEdit txtPhysicalName;
        private DevExpress.XtraEditors.LabelControl lblCode;
        private DevExpress.XtraEditors.TextEdit txtLogicalName;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbTableType;
        private DevExpress.Utils.ImageCollection icTableType;
        private DevExpress.XtraEditors.LabelControl lblTableCodeRequired;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbTableSetting;
        private DevExpress.XtraEditors.LabelControl lblTableSetting;
        private DevExpress.XtraEditors.LabelControl lblSystemTable;
        private DevExpress.XtraEditors.CheckEdit chkSystemTable;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbTableProperty;
        private DevExpress.XtraEditors.LabelControl lblTableProperty;
        private DevExpress.Utils.ImageCollection icTableProperty;
    }
}
