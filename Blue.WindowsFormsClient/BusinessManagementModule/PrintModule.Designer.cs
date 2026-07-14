namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    partial class PrintModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintModule));
            this.txtPrintCode = new DevExpress.XtraEditors.TextEdit();
            this.lblSystemDataField = new DevExpress.XtraEditors.LabelControl();
            this.lblPrintCode = new DevExpress.XtraEditors.LabelControl();
            this.txtPrintName = new DevExpress.XtraEditors.TextEdit();
            this.lblPrintName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.lblTableNameTip = new DevExpress.XtraEditors.LabelControl();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.icTableType = new DevExpress.Utils.ImageCollection(this.components);
            this.btxtTableName = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblAuditorRequired = new DevExpress.XtraEditors.LabelControl();
            this.chkPrintVisible = new DevExpress.XtraEditors.CheckEdit();
            this.lblPrintVisible = new DevExpress.XtraEditors.LabelControl();
            this.ccmbDataFieldSetting = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.icmbTableType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblTableType = new DevExpress.XtraEditors.LabelControl();
            this.lblTableTypeTip = new DevExpress.XtraEditors.LabelControl();
            this.fpRoleList = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl2 = new DevExpress.Utils.FlyoutPanelControl();
            this.lstRoles = new DevExpress.XtraEditors.ListBoxControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lnkRecord = new DevExpress.XtraEditors.HyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtTableName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintVisible.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldSetting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRoleList)).BeginInit();
            this.fpRoleList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).BeginInit();
            this.flyoutPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lnkRecord.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPrintCode
            // 
            this.txtPrintCode.Location = new System.Drawing.Point(80, 41);
            this.txtPrintCode.Name = "txtPrintCode";
            this.txtPrintCode.Properties.MaxLength = 32;
            this.txtPrintCode.Properties.ReadOnly = true;
            this.txtPrintCode.Size = new System.Drawing.Size(282, 20);
            this.txtPrintCode.TabIndex = 2;
            // 
            // lblSystemDataField
            // 
            this.lblSystemDataField.Location = new System.Drawing.Point(13, 74);
            this.lblSystemDataField.Name = "lblSystemDataField";
            this.lblSystemDataField.Size = new System.Drawing.Size(60, 14);
            this.lblSystemDataField.TabIndex = 17;
            this.lblSystemDataField.Text = "系统字段：";
            // 
            // lblPrintCode
            // 
            this.lblPrintCode.Location = new System.Drawing.Point(13, 43);
            this.lblPrintCode.Name = "lblPrintCode";
            this.lblPrintCode.Size = new System.Drawing.Size(60, 14);
            this.lblPrintCode.TabIndex = 13;
            this.lblPrintCode.Text = "打印编码：";
            // 
            // txtPrintName
            // 
            this.txtPrintName.Location = new System.Drawing.Point(80, 10);
            this.txtPrintName.Name = "txtPrintName";
            this.txtPrintName.Properties.MaxLength = 64;
            this.txtPrintName.Size = new System.Drawing.Size(282, 20);
            this.txtPrintName.TabIndex = 1;
            // 
            // lblPrintName
            // 
            this.lblPrintName.Location = new System.Drawing.Point(13, 12);
            this.lblPrintName.Name = "lblPrintName";
            this.lblPrintName.Size = new System.Drawing.Size(60, 14);
            this.lblPrintName.TabIndex = 15;
            this.lblPrintName.Text = "打印名称：";
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
            this.txtNotes.Location = new System.Drawing.Point(80, 199);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(282, 101);
            this.txtNotes.TabIndex = 5;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(37, 206);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // lblTableNameTip
            // 
            this.lblTableNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblTableNameTip.Appearance.Options.UseForeColor = true;
            this.lblTableNameTip.Location = new System.Drawing.Point(367, 143);
            this.lblTableNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblTableNameTip.Name = "lblTableNameTip";
            this.lblTableNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblTableNameTip.TabIndex = 50;
            this.lblTableNameTip.Text = "*";
            // 
            // lnkDetailedView
            // 
            this.lnkDetailedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedView.EditValue = "打印详情";
            this.lnkDetailedView.Location = new System.Drawing.Point(77, 309);
            this.lnkDetailedView.Name = "lnkDetailedView";
            this.lnkDetailedView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedView.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedView.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedView.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedView.TabIndex = 10;
            this.lnkDetailedView.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.lnkDetailedView_OpenLink);
            // 
            // icTableType
            // 
            this.icTableType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTableType.ImageStream")));
            this.icTableType.Images.SetKeyName(0, "Business_Table.png");
            this.icTableType.Images.SetKeyName(1, "BarButtonItem_View.png");
            this.icTableType.Images.SetKeyName(2, "Business_System_Table.png");
            // 
            // btxtTableName
            // 
            this.btxtTableName.Location = new System.Drawing.Point(80, 136);
            this.btxtTableName.Name = "btxtTableName";
            this.btxtTableName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtTableName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtTableName.Size = new System.Drawing.Size(282, 20);
            this.btxtTableName.TabIndex = 3;
            this.btxtTableName.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtTableName_ButtonPressed);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 138);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 84;
            this.labelControl1.Text = "表格名称：";
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
            // chkPrintVisible
            // 
            this.chkPrintVisible.Location = new System.Drawing.Point(80, 173);
            this.chkPrintVisible.Name = "chkPrintVisible";
            this.chkPrintVisible.Properties.Caption = "启用打印";
            this.chkPrintVisible.Size = new System.Drawing.Size(282, 19);
            this.chkPrintVisible.TabIndex = 4;
            // 
            // lblPrintVisible
            // 
            this.lblPrintVisible.Location = new System.Drawing.Point(13, 175);
            this.lblPrintVisible.Name = "lblPrintVisible";
            this.lblPrintVisible.Size = new System.Drawing.Size(60, 14);
            this.lblPrintVisible.TabIndex = 103;
            this.lblPrintVisible.Text = "打印状态：";
            // 
            // ccmbDataFieldSetting
            // 
            this.ccmbDataFieldSetting.EditValue = "";
            this.ccmbDataFieldSetting.Location = new System.Drawing.Point(80, 72);
            this.ccmbDataFieldSetting.Name = "ccmbDataFieldSetting";
            this.ccmbDataFieldSetting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbDataFieldSetting.Properties.PopupSizeable = false;
            this.ccmbDataFieldSetting.Properties.SelectAllItemVisible = false;
            this.ccmbDataFieldSetting.Size = new System.Drawing.Size(282, 20);
            this.ccmbDataFieldSetting.TabIndex = 2;
            // 
            // icmbTableType
            // 
            this.icmbTableType.Location = new System.Drawing.Point(80, 104);
            this.icmbTableType.Name = "icmbTableType";
            this.icmbTableType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbTableType.Properties.SmallImages = this.icTableType;
            this.icmbTableType.Size = new System.Drawing.Size(282, 20);
            this.icmbTableType.TabIndex = 104;
            this.icmbTableType.SelectedIndexChanged += new System.EventHandler(this.icmbTableType_SelectedIndexChanged);
            // 
            // lblTableType
            // 
            this.lblTableType.Location = new System.Drawing.Point(13, 107);
            this.lblTableType.Name = "lblTableType";
            this.lblTableType.Size = new System.Drawing.Size(60, 14);
            this.lblTableType.TabIndex = 105;
            this.lblTableType.Text = "表格类型：";
            // 
            // lblTableTypeTip
            // 
            this.lblTableTypeTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblTableTypeTip.Appearance.Options.UseForeColor = true;
            this.lblTableTypeTip.Location = new System.Drawing.Point(367, 110);
            this.lblTableTypeTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblTableTypeTip.Name = "lblTableTypeTip";
            this.lblTableTypeTip.Size = new System.Drawing.Size(7, 14);
            this.lblTableTypeTip.TabIndex = 106;
            this.lblTableTypeTip.Text = "*";
            // 
            // fpRoleList
            // 
            this.fpRoleList.Controls.Add(this.flyoutPanelControl2);
            this.fpRoleList.Location = new System.Drawing.Point(41, 86);
            this.fpRoleList.Name = "fpRoleList";
            this.fpRoleList.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("Button", global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16, false, true, "")});
            this.fpRoleList.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpRoleList.OwnerControl = this.lnkDetailedView;
            this.fpRoleList.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.fpRoleList.Size = new System.Drawing.Size(320, 230);
            this.fpRoleList.TabIndex = 107;
            this.fpRoleList.ButtonClick += new DevExpress.Utils.FlyoutPanelButtonClickEventHandler(this.fpRoleList_ButtonClick);
            // 
            // flyoutPanelControl2
            // 
            this.flyoutPanelControl2.Controls.Add(this.lstRoles);
            this.flyoutPanelControl2.Controls.Add(this.panelControl3);
            this.flyoutPanelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl2.FlyoutPanel = this.fpRoleList;
            this.flyoutPanelControl2.Location = new System.Drawing.Point(0, 30);
            this.flyoutPanelControl2.Name = "flyoutPanelControl2";
            this.flyoutPanelControl2.Size = new System.Drawing.Size(320, 200);
            this.flyoutPanelControl2.TabIndex = 0;
            // 
            // lstRoles
            // 
            this.lstRoles.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRoles.Location = new System.Drawing.Point(2, 32);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(316, 166);
            this.lstRoles.TabIndex = 4;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.lblName);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(2, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(316, 30);
            this.panelControl3.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(12, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(48, 14);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "角色列表";
            // 
            // lnkRecord
            // 
            this.lnkRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkRecord.EditValue = "打印记录列表";
            this.lnkRecord.Location = new System.Drawing.Point(77, 337);
            this.lnkRecord.Name = "lnkRecord";
            this.lnkRecord.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkRecord.Properties.Appearance.Options.UseBackColor = true;
            this.lnkRecord.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkRecord.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_View;
            this.lnkRecord.Size = new System.Drawing.Size(280, 22);
            this.lnkRecord.TabIndex = 11;
            this.lnkRecord.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.lnkRecord_OpenLink);
            // 
            // PrintModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkRecord);
            this.Controls.Add(this.fpRoleList);
            this.Controls.Add(this.lblTableTypeTip);
            this.Controls.Add(this.lblTableType);
            this.Controls.Add(this.icmbTableType);
            this.Controls.Add(this.ccmbDataFieldSetting);
            this.Controls.Add(this.chkPrintVisible);
            this.Controls.Add(this.lblPrintVisible);
            this.Controls.Add(this.lblAuditorRequired);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btxtTableName);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.lblTableNameTip);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNameRequired);
            this.Controls.Add(this.txtPrintCode);
            this.Controls.Add(this.lblSystemDataField);
            this.Controls.Add(this.lblPrintCode);
            this.Controls.Add(this.txtPrintName);
            this.Controls.Add(this.lblPrintName);
            this.Name = "PrintModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.PrintModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtTableName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintVisible.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldSetting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRoleList)).EndInit();
            this.fpRoleList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).EndInit();
            this.flyoutPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lnkRecord.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtPrintCode;
        private DevExpress.XtraEditors.LabelControl lblPrintCode;
        private DevExpress.XtraEditors.TextEdit txtPrintName;
        private DevExpress.XtraEditors.LabelControl lblNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.LabelControl lblTableNameTip;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.LabelControl lblPrintName;
        private DevExpress.XtraEditors.ButtonEdit btxtTableName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.ImageCollection icTableType;
        private DevExpress.XtraEditors.LabelControl lblAuditorRequired;
        private DevExpress.XtraEditors.LabelControl lblSystemDataField;
        private DevExpress.XtraEditors.CheckEdit chkPrintVisible;
        private DevExpress.XtraEditors.LabelControl lblPrintVisible;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbDataFieldSetting;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbTableType;
        private DevExpress.XtraEditors.LabelControl lblTableType;
        private DevExpress.XtraEditors.LabelControl lblTableTypeTip;
        private DevExpress.Utils.FlyoutPanel fpRoleList;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.ListBoxControl lstRoles;
        private DevExpress.XtraEditors.HyperLinkEdit lnkRecord;
    }
}
