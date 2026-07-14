namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class RoleModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleModule));
            this.txtRoleCode = new DevExpress.XtraEditors.TextEdit();
            this.txtRoleName = new DevExpress.XtraEditors.TextEdit();
            this.lblRoleName = new DevExpress.XtraEditors.LabelControl();
            this.lblRoleNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.icMenuBusinessType = new DevExpress.Utils.ImageCollection(this.components);
            this.lblRoleCodeRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblRoleCode = new DevExpress.XtraEditors.LabelControl();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lblExpiredDate = new DevExpress.XtraEditors.LabelControl();
            this.lblInitializedDate = new DevExpress.XtraEditors.LabelControl();
            this.lblIsLockedOut = new DevExpress.XtraEditors.LabelControl();
            this.chkIsLockedOut = new DevExpress.XtraEditors.CheckEdit();
            this.deInitializedDate = new DevExpress.XtraEditors.DateEdit();
            this.dateTimeChartRangeControlClient1 = new DevExpress.XtraEditors.DateTimeChartRangeControlClient();
            this.deExpiredDate = new DevExpress.XtraEditors.DateEdit();
            this.chkIsSystemRole = new DevExpress.XtraEditors.CheckEdit();
            this.lblIsSystemRole = new DevExpress.XtraEditors.LabelControl();
            this.lblRoleProperty = new DevExpress.XtraEditors.LabelControl();
            this.ccmbRoleProperty = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icMenuBusinessType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsLockedOut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInitializedDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInitializedDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeChartRangeControlClient1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deExpiredDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deExpiredDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsSystemRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbRoleProperty.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRoleCode
            // 
            this.txtRoleCode.Location = new System.Drawing.Point(77, 53);
            this.txtRoleCode.Name = "txtRoleCode";
            this.txtRoleCode.Properties.MaxLength = 32;
            this.txtRoleCode.Size = new System.Drawing.Size(281, 20);
            this.txtRoleCode.TabIndex = 2;
            // 
            // txtRoleName
            // 
            this.txtRoleName.Location = new System.Drawing.Point(77, 18);
            this.txtRoleName.Name = "txtRoleName";
            this.txtRoleName.Properties.MaxLength = 64;
            this.txtRoleName.Size = new System.Drawing.Size(282, 20);
            this.txtRoleName.TabIndex = 1;
            // 
            // lblRoleName
            // 
            this.lblRoleName.Location = new System.Drawing.Point(11, 20);
            this.lblRoleName.Name = "lblRoleName";
            this.lblRoleName.Size = new System.Drawing.Size(60, 14);
            this.lblRoleName.TabIndex = 15;
            this.lblRoleName.Text = "角色名称：";
            // 
            // lblRoleNameRequired
            // 
            this.lblRoleNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblRoleNameRequired.Appearance.Options.UseForeColor = true;
            this.lblRoleNameRequired.Location = new System.Drawing.Point(365, 22);
            this.lblRoleNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblRoleNameRequired.Name = "lblRoleNameRequired";
            this.lblRoleNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblRoleNameRequired.TabIndex = 22;
            this.lblRoleNameRequired.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(77, 249);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 72);
            this.txtNotes.TabIndex = 6;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(35, 252);
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
            this.icMenuBusinessType.Images.SetKeyName(5, "Common_Menu_Custom.png");
            this.icMenuBusinessType.Images.SetKeyName(6, "Common_Menu_Reserved.png");
            // 
            // lblRoleCodeRequired
            // 
            this.lblRoleCodeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblRoleCodeRequired.Appearance.Options.UseForeColor = true;
            this.lblRoleCodeRequired.Location = new System.Drawing.Point(365, 58);
            this.lblRoleCodeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblRoleCodeRequired.Name = "lblRoleCodeRequired";
            this.lblRoleCodeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblRoleCodeRequired.TabIndex = 24;
            this.lblRoleCodeRequired.Text = "*";
            // 
            // lblRoleCode
            // 
            this.lblRoleCode.Location = new System.Drawing.Point(11, 55);
            this.lblRoleCode.Name = "lblRoleCode";
            this.lblRoleCode.Size = new System.Drawing.Size(60, 14);
            this.lblRoleCode.TabIndex = 58;
            this.lblRoleCode.Text = "角色编码：";
            // 
            // lnkDetailedView
            // 
            this.lnkDetailedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedView.EditValue = "角色详情";
            this.lnkDetailedView.Location = new System.Drawing.Point(77, 328);
            this.lnkDetailedView.Name = "lnkDetailedView";
            this.lnkDetailedView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedView.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedView.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedView.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedView.TabIndex = 10;
            // 
            // lblExpiredDate
            // 
            this.lblExpiredDate.Location = new System.Drawing.Point(11, 126);
            this.lblExpiredDate.Name = "lblExpiredDate";
            this.lblExpiredDate.Size = new System.Drawing.Size(60, 14);
            this.lblExpiredDate.TabIndex = 94;
            this.lblExpiredDate.Text = "结束时间：";
            // 
            // lblInitializedDate
            // 
            this.lblInitializedDate.Location = new System.Drawing.Point(11, 90);
            this.lblInitializedDate.Name = "lblInitializedDate";
            this.lblInitializedDate.Size = new System.Drawing.Size(60, 14);
            this.lblInitializedDate.TabIndex = 95;
            this.lblInitializedDate.Text = "起始时间：";
            // 
            // lblIsLockedOut
            // 
            this.lblIsLockedOut.Location = new System.Drawing.Point(11, 223);
            this.lblIsLockedOut.Name = "lblIsLockedOut";
            this.lblIsLockedOut.Size = new System.Drawing.Size(60, 14);
            this.lblIsLockedOut.TabIndex = 102;
            this.lblIsLockedOut.Text = "锁定状态：";
            // 
            // chkIsLockedOut
            // 
            this.chkIsLockedOut.Location = new System.Drawing.Point(77, 221);
            this.chkIsLockedOut.Name = "chkIsLockedOut";
            this.chkIsLockedOut.Properties.Caption = "";
            this.chkIsLockedOut.Size = new System.Drawing.Size(20, 19);
            this.chkIsLockedOut.TabIndex = 5;
            // 
            // deInitializedDate
            // 
            this.deInitializedDate.EditValue = null;
            this.deInitializedDate.Location = new System.Drawing.Point(77, 88);
            this.deInitializedDate.Name = "deInitializedDate";
            this.deInitializedDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deInitializedDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deInitializedDate.Size = new System.Drawing.Size(282, 20);
            this.deInitializedDate.TabIndex = 3;
            // 
            // deExpiredDate
            // 
            this.deExpiredDate.EditValue = null;
            this.deExpiredDate.Location = new System.Drawing.Point(77, 123);
            this.deExpiredDate.Name = "deExpiredDate";
            this.deExpiredDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deExpiredDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deExpiredDate.Size = new System.Drawing.Size(282, 20);
            this.deExpiredDate.TabIndex = 4;
            // 
            // chkIsSystemRole
            // 
            this.chkIsSystemRole.Location = new System.Drawing.Point(77, 191);
            this.chkIsSystemRole.Name = "chkIsSystemRole";
            this.chkIsSystemRole.Properties.Caption = "";
            this.chkIsSystemRole.Size = new System.Drawing.Size(20, 19);
            this.chkIsSystemRole.TabIndex = 103;
            // 
            // lblIsSystemRole
            // 
            this.lblIsSystemRole.Location = new System.Drawing.Point(11, 193);
            this.lblIsSystemRole.Name = "lblIsSystemRole";
            this.lblIsSystemRole.Size = new System.Drawing.Size(60, 14);
            this.lblIsSystemRole.TabIndex = 104;
            this.lblIsSystemRole.Text = "系统角色：";
            // 
            // lblRoleProperty
            // 
            this.lblRoleProperty.Location = new System.Drawing.Point(11, 161);
            this.lblRoleProperty.Name = "lblRoleProperty";
            this.lblRoleProperty.Size = new System.Drawing.Size(60, 14);
            this.lblRoleProperty.TabIndex = 105;
            this.lblRoleProperty.Text = "特殊权限：";
            // 
            // ccmbRoleProperty
            // 
            this.ccmbRoleProperty.Location = new System.Drawing.Point(77, 158);
            this.ccmbRoleProperty.Name = "ccmbRoleProperty";
            this.ccmbRoleProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbRoleProperty.Properties.SelectAllItemVisible = false;
            this.ccmbRoleProperty.Size = new System.Drawing.Size(281, 20);
            this.ccmbRoleProperty.TabIndex = 106;
            // 
            // RoleModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ccmbRoleProperty);
            this.Controls.Add(this.lblRoleProperty);
            this.Controls.Add(this.chkIsSystemRole);
            this.Controls.Add(this.lblIsSystemRole);
            this.Controls.Add(this.deExpiredDate);
            this.Controls.Add(this.deInitializedDate);
            this.Controls.Add(this.chkIsLockedOut);
            this.Controls.Add(this.lblIsLockedOut);
            this.Controls.Add(this.lblInitializedDate);
            this.Controls.Add(this.lblExpiredDate);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.lblRoleCode);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblRoleCodeRequired);
            this.Controls.Add(this.lblRoleNameRequired);
            this.Controls.Add(this.txtRoleCode);
            this.Controls.Add(this.txtRoleName);
            this.Controls.Add(this.lblRoleName);
            this.Name = "RoleModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.MenuModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icMenuBusinessType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsLockedOut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInitializedDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInitializedDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeChartRangeControlClient1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deExpiredDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deExpiredDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsSystemRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbRoleProperty.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtRoleCode;
        private DevExpress.XtraEditors.TextEdit txtRoleName;
        private DevExpress.XtraEditors.LabelControl lblRoleName;
        private DevExpress.XtraEditors.LabelControl lblRoleNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.Utils.ImageCollection icMenuBusinessType;
        private DevExpress.XtraEditors.LabelControl lblRoleCodeRequired;
        private DevExpress.XtraEditors.LabelControl lblRoleCode;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.LabelControl lblExpiredDate;
        private DevExpress.XtraEditors.LabelControl lblInitializedDate;
        private DevExpress.XtraEditors.LabelControl lblIsLockedOut;
        private DevExpress.XtraEditors.CheckEdit chkIsLockedOut;
        private DevExpress.XtraEditors.DateEdit deInitializedDate;
        private DevExpress.XtraEditors.DateTimeChartRangeControlClient dateTimeChartRangeControlClient1;
        private DevExpress.XtraEditors.DateEdit deExpiredDate;
        private DevExpress.XtraEditors.CheckEdit chkIsSystemRole;
        private DevExpress.XtraEditors.LabelControl lblIsSystemRole;
        private DevExpress.XtraEditors.LabelControl lblRoleProperty;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbRoleProperty;
    }
}
