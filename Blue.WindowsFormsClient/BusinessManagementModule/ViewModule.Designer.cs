namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    partial class ViewModule
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
            this.lblTooltip = new DevExpress.XtraEditors.LabelControl();
            this.txtViewCode = new DevExpress.XtraEditors.TextEdit();
            this.txtViewPhysicalName = new DevExpress.XtraEditors.TextEdit();
            this.lblPhysicalName = new DevExpress.XtraEditors.LabelControl();
            this.txtViewLogicalName = new DevExpress.XtraEditors.TextEdit();
            this.lblViewName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtTooltip = new DevExpress.XtraEditors.MemoEdit();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.lblTableCodeRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblMainTable = new DevExpress.XtraEditors.LabelControl();
            this.lblAssociatedTableTip = new DevExpress.XtraEditors.LabelControl();
            this.hleAssociatedTables = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lblAssociatedTables = new DevExpress.XtraEditors.LabelControl();
            this.txtMainTable = new DevExpress.XtraEditors.TextEdit();
            this.hleMainTable = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lblMainTableTip = new DevExpress.XtraEditors.LabelControl();
            this.lstAssociatedTables = new DevExpress.XtraEditors.ListBoxControl();
            this.lblViewProperty = new DevExpress.XtraEditors.LabelControl();
            this.icmbViewProperty = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.ccmbDataFieldSetting = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblSystemDataFields = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtViewCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtViewPhysicalName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtViewLogicalName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTooltip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleAssociatedTables.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMainTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleMainTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstAssociatedTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbViewProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldSetting.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTooltip
            // 
            this.lblTooltip.Location = new System.Drawing.Point(11, 276);
            this.lblTooltip.Name = "lblTooltip";
            this.lblTooltip.Size = new System.Drawing.Size(60, 14);
            this.lblTooltip.TabIndex = 21;
            this.lblTooltip.Text = "用户提示：";
            // 
            // txtViewCode
            // 
            this.txtViewCode.Location = new System.Drawing.Point(181, 45);
            this.txtViewCode.Name = "txtViewCode";
            this.txtViewCode.Properties.MaxLength = 32;
            this.txtViewCode.Properties.ReadOnly = true;
            this.txtViewCode.Size = new System.Drawing.Size(178, 20);
            this.txtViewCode.TabIndex = 3;
            // 
            // txtViewPhysicalName
            // 
            this.txtViewPhysicalName.Location = new System.Drawing.Point(77, 45);
            this.txtViewPhysicalName.Name = "txtViewPhysicalName";
            this.txtViewPhysicalName.Properties.ReadOnly = true;
            this.txtViewPhysicalName.Size = new System.Drawing.Size(98, 20);
            this.txtViewPhysicalName.TabIndex = 2;
            // 
            // lblPhysicalName
            // 
            this.lblPhysicalName.Location = new System.Drawing.Point(11, 48);
            this.lblPhysicalName.Name = "lblPhysicalName";
            this.lblPhysicalName.Size = new System.Drawing.Size(60, 14);
            this.lblPhysicalName.TabIndex = 13;
            this.lblPhysicalName.Text = "视图信息：";
            // 
            // txtViewLogicalName
            // 
            this.txtViewLogicalName.Location = new System.Drawing.Point(77, 14);
            this.txtViewLogicalName.Name = "txtViewLogicalName";
            this.txtViewLogicalName.Properties.MaxLength = 64;
            this.txtViewLogicalName.Size = new System.Drawing.Size(282, 20);
            this.txtViewLogicalName.TabIndex = 1;
            // 
            // lblViewName
            // 
            this.lblViewName.Location = new System.Drawing.Point(11, 16);
            this.lblViewName.Name = "lblViewName";
            this.lblViewName.Size = new System.Drawing.Size(60, 14);
            this.lblViewName.TabIndex = 15;
            this.lblViewName.Text = "视图名称：";
            // 
            // lblNameRequired
            // 
            this.lblNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameRequired.Appearance.Options.UseForeColor = true;
            this.lblNameRequired.Location = new System.Drawing.Point(366, 16);
            this.lblNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameRequired.Name = "lblNameRequired";
            this.lblNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblNameRequired.TabIndex = 22;
            this.lblNameRequired.Text = "*";
            // 
            // txtTooltip
            // 
            this.txtTooltip.EditValue = "";
            this.txtTooltip.Location = new System.Drawing.Point(77, 273);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Properties.MaxLength = 256;
            this.txtTooltip.Size = new System.Drawing.Size(280, 28);
            this.txtTooltip.TabIndex = 9;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(77, 310);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 23);
            this.txtNotes.TabIndex = 10;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(35, 310);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // lblTableCodeRequired
            // 
            this.lblTableCodeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblTableCodeRequired.Appearance.Options.UseForeColor = true;
            this.lblTableCodeRequired.Location = new System.Drawing.Point(366, 51);
            this.lblTableCodeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblTableCodeRequired.Name = "lblTableCodeRequired";
            this.lblTableCodeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblTableCodeRequired.TabIndex = 24;
            this.lblTableCodeRequired.Text = "*";
            // 
            // lblMainTable
            // 
            this.lblMainTable.Location = new System.Drawing.Point(11, 142);
            this.lblMainTable.Name = "lblMainTable";
            this.lblMainTable.Size = new System.Drawing.Size(60, 14);
            this.lblMainTable.TabIndex = 48;
            this.lblMainTable.Text = "主数据表：";
            // 
            // lblAssociatedTableTip
            // 
            this.lblAssociatedTableTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblAssociatedTableTip.Appearance.Options.UseForeColor = true;
            this.lblAssociatedTableTip.Location = new System.Drawing.Point(365, 190);
            this.lblAssociatedTableTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblAssociatedTableTip.Name = "lblAssociatedTableTip";
            this.lblAssociatedTableTip.Size = new System.Drawing.Size(7, 14);
            this.lblAssociatedTableTip.TabIndex = 50;
            this.lblAssociatedTableTip.Text = "*";
            // 
            // hleAssociatedTables
            // 
            this.hleAssociatedTables.EditValue = "设置从数据表...";
            this.hleAssociatedTables.Location = new System.Drawing.Point(267, 250);
            this.hleAssociatedTables.Name = "hleAssociatedTables";
            this.hleAssociatedTables.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleAssociatedTables.Properties.Appearance.Options.UseBackColor = true;
            this.hleAssociatedTables.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleAssociatedTables.Size = new System.Drawing.Size(90, 18);
            this.hleAssociatedTables.TabIndex = 8;
            this.hleAssociatedTables.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hleAssociatedTables_OpenLink);
            // 
            // lnkDetailedView
            // 
            this.lnkDetailedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedView.EditValue = "视图详情";
            this.lnkDetailedView.Location = new System.Drawing.Point(77, 336);
            this.lnkDetailedView.Name = "lnkDetailedView";
            this.lnkDetailedView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedView.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedView.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedView.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedView.TabIndex = 59;
            // 
            // lblAssociatedTables
            // 
            this.lblAssociatedTables.Location = new System.Drawing.Point(11, 188);
            this.lblAssociatedTables.Name = "lblAssociatedTables";
            this.lblAssociatedTables.Size = new System.Drawing.Size(60, 14);
            this.lblAssociatedTables.TabIndex = 62;
            this.lblAssociatedTables.Text = "从数据表：";
            // 
            // txtMainTable
            // 
            this.txtMainTable.Location = new System.Drawing.Point(77, 139);
            this.txtMainTable.Name = "txtMainTable";
            this.txtMainTable.Properties.MaxLength = 32;
            this.txtMainTable.Size = new System.Drawing.Size(280, 20);
            this.txtMainTable.TabIndex = 5;
            // 
            // hleMainTable
            // 
            this.hleMainTable.EditValue = "设置主数据表...";
            this.hleMainTable.Location = new System.Drawing.Point(267, 163);
            this.hleMainTable.Name = "hleMainTable";
            this.hleMainTable.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleMainTable.Properties.Appearance.Options.UseBackColor = true;
            this.hleMainTable.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleMainTable.Size = new System.Drawing.Size(90, 18);
            this.hleMainTable.TabIndex = 6;
            this.hleMainTable.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hleMainTable_OpenLink);
            // 
            // lblMainTableTip
            // 
            this.lblMainTableTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblMainTableTip.Appearance.Options.UseForeColor = true;
            this.lblMainTableTip.Location = new System.Drawing.Point(366, 142);
            this.lblMainTableTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblMainTableTip.Name = "lblMainTableTip";
            this.lblMainTableTip.Size = new System.Drawing.Size(7, 14);
            this.lblMainTableTip.TabIndex = 65;
            this.lblMainTableTip.Text = "*";
            // 
            // lstAssociatedTables
            // 
            this.lstAssociatedTables.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstAssociatedTables.IncrementalSearch = true;
            this.lstAssociatedTables.Location = new System.Drawing.Point(77, 187);
            this.lstAssociatedTables.Name = "lstAssociatedTables";
            this.lstAssociatedTables.Size = new System.Drawing.Size(282, 58);
            this.lstAssociatedTables.TabIndex = 7;
            // 
            // lblViewProperty
            // 
            this.lblViewProperty.Location = new System.Drawing.Point(11, 79);
            this.lblViewProperty.Name = "lblViewProperty";
            this.lblViewProperty.Size = new System.Drawing.Size(60, 14);
            this.lblViewProperty.TabIndex = 90;
            this.lblViewProperty.Text = "视图属性：";
            // 
            // icmbViewProperty
            // 
            this.icmbViewProperty.Location = new System.Drawing.Point(77, 76);
            this.icmbViewProperty.Name = "icmbViewProperty";
            this.icmbViewProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbViewProperty.Size = new System.Drawing.Size(282, 20);
            this.icmbViewProperty.TabIndex = 4;
            // 
            // ccmbDataFieldSetting
            // 
            this.ccmbDataFieldSetting.EditValue = "";
            this.ccmbDataFieldSetting.Location = new System.Drawing.Point(77, 108);
            this.ccmbDataFieldSetting.Name = "ccmbDataFieldSetting";
            this.ccmbDataFieldSetting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbDataFieldSetting.Properties.PopupSizeable = false;
            this.ccmbDataFieldSetting.Properties.SelectAllItemVisible = false;
            this.ccmbDataFieldSetting.Size = new System.Drawing.Size(280, 20);
            this.ccmbDataFieldSetting.TabIndex = 92;
            // 
            // lblSystemDataFields
            // 
            this.lblSystemDataFields.Location = new System.Drawing.Point(11, 111);
            this.lblSystemDataFields.Name = "lblSystemDataFields";
            this.lblSystemDataFields.Size = new System.Drawing.Size(60, 14);
            this.lblSystemDataFields.TabIndex = 91;
            this.lblSystemDataFields.Text = "系统字段：";
            // 
            // ViewModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ccmbDataFieldSetting);
            this.Controls.Add(this.lblSystemDataFields);
            this.Controls.Add(this.lblViewProperty);
            this.Controls.Add(this.icmbViewProperty);
            this.Controls.Add(this.lstAssociatedTables);
            this.Controls.Add(this.lblMainTableTip);
            this.Controls.Add(this.hleMainTable);
            this.Controls.Add(this.txtMainTable);
            this.Controls.Add(this.lblAssociatedTables);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.hleAssociatedTables);
            this.Controls.Add(this.lblAssociatedTableTip);
            this.Controls.Add(this.lblMainTable);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblTableCodeRequired);
            this.Controls.Add(this.txtTooltip);
            this.Controls.Add(this.lblNameRequired);
            this.Controls.Add(this.lblTooltip);
            this.Controls.Add(this.txtViewCode);
            this.Controls.Add(this.txtViewPhysicalName);
            this.Controls.Add(this.lblPhysicalName);
            this.Controls.Add(this.txtViewLogicalName);
            this.Controls.Add(this.lblViewName);
            this.Name = "ViewModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.ViewModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtViewCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtViewPhysicalName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtViewLogicalName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTooltip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleAssociatedTables.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMainTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleMainTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstAssociatedTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbViewProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldSetting.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTooltip;
        private DevExpress.XtraEditors.TextEdit txtViewCode;
        private DevExpress.XtraEditors.TextEdit txtViewPhysicalName;
        private DevExpress.XtraEditors.LabelControl lblPhysicalName;
        private DevExpress.XtraEditors.TextEdit txtViewLogicalName;
        private DevExpress.XtraEditors.LabelControl lblViewName;
        private DevExpress.XtraEditors.LabelControl lblNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtTooltip;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.LabelControl lblTableCodeRequired;
        private DevExpress.XtraEditors.LabelControl lblMainTable;
        private DevExpress.XtraEditors.LabelControl lblAssociatedTableTip;
        private DevExpress.XtraEditors.HyperLinkEdit hleAssociatedTables;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.LabelControl lblAssociatedTables;
        private DevExpress.XtraEditors.TextEdit txtMainTable;
        private DevExpress.XtraEditors.HyperLinkEdit hleMainTable;
        private DevExpress.XtraEditors.LabelControl lblMainTableTip;
        private DevExpress.XtraEditors.ListBoxControl lstAssociatedTables;
        private DevExpress.XtraEditors.LabelControl lblViewProperty;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbViewProperty;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbDataFieldSetting;
        private DevExpress.XtraEditors.LabelControl lblSystemDataFields;
    }
}
