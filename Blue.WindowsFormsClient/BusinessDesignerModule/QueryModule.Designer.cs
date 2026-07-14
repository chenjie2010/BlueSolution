namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class QueryModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryModule));
            this.lblTooltip = new DevExpress.XtraEditors.LabelControl();
            this.txtDataQueriedCode = new DevExpress.XtraEditors.TextEdit();
            this.txtDataQueriedName = new DevExpress.XtraEditors.TextEdit();
            this.lblDataQueriedName = new DevExpress.XtraEditors.LabelControl();
            this.lblDataQueriedNameTip = new DevExpress.XtraEditors.LabelControl();
            this.txtToolTip = new DevExpress.XtraEditors.MemoEdit();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.icDataQueriedType = new DevExpress.Utils.ImageCollection(this.components);
            this.lblTableCodeRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblDataRange = new DevExpress.XtraEditors.LabelControl();
            this.lblCode = new DevExpress.XtraEditors.LabelControl();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.hleTableSetting = new DevExpress.XtraEditors.HyperLinkEdit();
            this.txtTableName = new DevExpress.XtraEditors.TextEdit();
            this.lblMainTable = new DevExpress.XtraEditors.LabelControl();
            this.lblSystemCondition = new DevExpress.XtraEditors.LabelControl();
            this.ccmbSystemCondition = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.ccmbGroupCondition = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblGroupCondition = new DevExpress.XtraEditors.LabelControl();
            this.lblQueriedShowMode = new DevExpress.XtraEditors.LabelControl();
            this.ccmbDataRange = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.icmbDataQueriedType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cmbQueriedShowMode = new AppFramework.WinFormsControls.ComoboxTreeview();
            this.lblQueriedShowModeTip = new DevExpress.XtraEditors.LabelControl();
            this.ccmbDataFieldSetting = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblSystemDataFields = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataQueriedCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataQueriedName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToolTip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataQueriedType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleTableSetting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbSystemCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbGroupCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataQueriedType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldSetting.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTooltip
            // 
            this.lblTooltip.Location = new System.Drawing.Point(11, 259);
            this.lblTooltip.Name = "lblTooltip";
            this.lblTooltip.Size = new System.Drawing.Size(60, 14);
            this.lblTooltip.TabIndex = 21;
            this.lblTooltip.Text = "用户提示：";
            // 
            // txtDataQueriedCode
            // 
            this.txtDataQueriedCode.Location = new System.Drawing.Point(77, 46);
            this.txtDataQueriedCode.Name = "txtDataQueriedCode";
            this.txtDataQueriedCode.Properties.MaxLength = 32;
            this.txtDataQueriedCode.Size = new System.Drawing.Size(144, 20);
            this.txtDataQueriedCode.TabIndex = 3;
            // 
            // txtDataQueriedName
            // 
            this.txtDataQueriedName.Location = new System.Drawing.Point(77, 16);
            this.txtDataQueriedName.Name = "txtDataQueriedName";
            this.txtDataQueriedName.Properties.MaxLength = 64;
            this.txtDataQueriedName.Size = new System.Drawing.Size(282, 20);
            this.txtDataQueriedName.TabIndex = 1;
            // 
            // lblDataQueriedName
            // 
            this.lblDataQueriedName.Location = new System.Drawing.Point(11, 18);
            this.lblDataQueriedName.Name = "lblDataQueriedName";
            this.lblDataQueriedName.Size = new System.Drawing.Size(60, 14);
            this.lblDataQueriedName.TabIndex = 15;
            this.lblDataQueriedName.Text = "查询名称：";
            // 
            // lblDataQueriedNameTip
            // 
            this.lblDataQueriedNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblDataQueriedNameTip.Appearance.Options.UseForeColor = true;
            this.lblDataQueriedNameTip.Location = new System.Drawing.Point(364, 20);
            this.lblDataQueriedNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblDataQueriedNameTip.Name = "lblDataQueriedNameTip";
            this.lblDataQueriedNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblDataQueriedNameTip.TabIndex = 22;
            this.lblDataQueriedNameTip.Text = "*";
            // 
            // txtToolTip
            // 
            this.txtToolTip.EditValue = "";
            this.txtToolTip.Location = new System.Drawing.Point(77, 256);
            this.txtToolTip.Name = "txtToolTip";
            this.txtToolTip.Properties.MaxLength = 256;
            this.txtToolTip.Size = new System.Drawing.Size(280, 37);
            this.txtToolTip.TabIndex = 8;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(77, 303);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 29);
            this.txtNotes.TabIndex = 9;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(35, 305);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // icDataQueriedType
            // 
            this.icDataQueriedType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataQueriedType.ImageStream")));
            this.icDataQueriedType.Images.SetKeyName(0, "Business_Table.png");
            this.icDataQueriedType.Images.SetKeyName(1, "BarButtonItem_View.png");
            this.icDataQueriedType.Images.SetKeyName(2, "Common_Query_Custom.png");
            // 
            // lblTableCodeRequired
            // 
            this.lblTableCodeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblTableCodeRequired.Appearance.Options.UseForeColor = true;
            this.lblTableCodeRequired.Location = new System.Drawing.Point(363, 50);
            this.lblTableCodeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblTableCodeRequired.Name = "lblTableCodeRequired";
            this.lblTableCodeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblTableCodeRequired.TabIndex = 24;
            this.lblTableCodeRequired.Text = "*";
            // 
            // lblDataRange
            // 
            this.lblDataRange.Location = new System.Drawing.Point(11, 228);
            this.lblDataRange.Name = "lblDataRange";
            this.lblDataRange.Size = new System.Drawing.Size(60, 14);
            this.lblDataRange.TabIndex = 46;
            this.lblDataRange.Text = "查询范围：";
            // 
            // lblCode
            // 
            this.lblCode.Location = new System.Drawing.Point(11, 48);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(60, 14);
            this.lblCode.TabIndex = 58;
            this.lblCode.Text = "查询信息：";
            // 
            // lnkDetailedView
            // 
            this.lnkDetailedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedView.EditValue = "查询详情";
            this.lnkDetailedView.Location = new System.Drawing.Point(77, 335);
            this.lnkDetailedView.Name = "lnkDetailedView";
            this.lnkDetailedView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedView.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedView.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedView.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedView.TabIndex = 59;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(362, 80);
            this.labelControl2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(7, 14);
            this.labelControl2.TabIndex = 50;
            this.labelControl2.Text = "*";
            // 
            // hleTableSetting
            // 
            this.hleTableSetting.EditValue = "设置...";
            this.hleTableSetting.Location = new System.Drawing.Point(312, 77);
            this.hleTableSetting.Name = "hleTableSetting";
            this.hleTableSetting.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleTableSetting.Properties.Appearance.Options.UseBackColor = true;
            this.hleTableSetting.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleTableSetting.Size = new System.Drawing.Size(44, 18);
            this.hleTableSetting.TabIndex = 3;
            this.hleTableSetting.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hleTableSetting_OpenLink);
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(77, 76);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Properties.MaxLength = 32;
            this.txtTableName.Properties.ReadOnly = true;
            this.txtTableName.Size = new System.Drawing.Size(230, 20);
            this.txtTableName.TabIndex = 67;
            // 
            // lblMainTable
            // 
            this.lblMainTable.Location = new System.Drawing.Point(11, 78);
            this.lblMainTable.Name = "lblMainTable";
            this.lblMainTable.Size = new System.Drawing.Size(60, 14);
            this.lblMainTable.TabIndex = 66;
            this.lblMainTable.Text = "表与视图：";
            // 
            // lblSystemCondition
            // 
            this.lblSystemCondition.Location = new System.Drawing.Point(11, 167);
            this.lblSystemCondition.Name = "lblSystemCondition";
            this.lblSystemCondition.Size = new System.Drawing.Size(60, 14);
            this.lblSystemCondition.TabIndex = 48;
            this.lblSystemCondition.Text = "系统条件：";
            // 
            // ccmbSystemCondition
            // 
            this.ccmbSystemCondition.EditValue = "";
            this.ccmbSystemCondition.Location = new System.Drawing.Point(77, 165);
            this.ccmbSystemCondition.Name = "ccmbSystemCondition";
            this.ccmbSystemCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbSystemCondition.Properties.PopupSizeable = false;
            this.ccmbSystemCondition.Properties.SelectAllItemVisible = false;
            this.ccmbSystemCondition.Size = new System.Drawing.Size(280, 20);
            this.ccmbSystemCondition.TabIndex = 4;
            // 
            // ccmbGroupCondition
            // 
            this.ccmbGroupCondition.EditValue = "";
            this.ccmbGroupCondition.Location = new System.Drawing.Point(77, 195);
            this.ccmbGroupCondition.Name = "ccmbGroupCondition";
            this.ccmbGroupCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbGroupCondition.Properties.PopupSizeable = false;
            this.ccmbGroupCondition.Properties.SelectAllItemVisible = false;
            this.ccmbGroupCondition.Size = new System.Drawing.Size(280, 20);
            this.ccmbGroupCondition.TabIndex = 5;
            // 
            // lblGroupCondition
            // 
            this.lblGroupCondition.Location = new System.Drawing.Point(11, 197);
            this.lblGroupCondition.Name = "lblGroupCondition";
            this.lblGroupCondition.Size = new System.Drawing.Size(60, 14);
            this.lblGroupCondition.TabIndex = 70;
            this.lblGroupCondition.Text = "系统分组：";
            // 
            // lblQueriedShowMode
            // 
            this.lblQueriedShowMode.Location = new System.Drawing.Point(10, 108);
            this.lblQueriedShowMode.Name = "lblQueriedShowMode";
            this.lblQueriedShowMode.Size = new System.Drawing.Size(60, 14);
            this.lblQueriedShowMode.TabIndex = 72;
            this.lblQueriedShowMode.Text = "展现模式：";
            // 
            // ccmbDataRange
            // 
            this.ccmbDataRange.EditValue = "";
            this.ccmbDataRange.Location = new System.Drawing.Point(77, 226);
            this.ccmbDataRange.Name = "ccmbDataRange";
            this.ccmbDataRange.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbDataRange.Properties.PopupSizeable = false;
            this.ccmbDataRange.Properties.SelectAllItemVisible = false;
            this.ccmbDataRange.Size = new System.Drawing.Size(280, 20);
            this.ccmbDataRange.TabIndex = 7;
            // 
            // icmbDataQueriedType
            // 
            this.icmbDataQueriedType.Location = new System.Drawing.Point(227, 46);
            this.icmbDataQueriedType.Name = "icmbDataQueriedType";
            this.icmbDataQueriedType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDataQueriedType.Properties.SmallImages = this.icDataQueriedType;
            this.icmbDataQueriedType.Size = new System.Drawing.Size(132, 20);
            this.icmbDataQueriedType.TabIndex = 2;
            // 
            // cmbQueriedShowMode
            // 
            this.cmbQueriedShowMode.Location = new System.Drawing.Point(76, 106);
            this.cmbQueriedShowMode.Name = "cmbQueriedShowMode";
            this.cmbQueriedShowMode.OnlySelectedLeaf = true;
            this.cmbQueriedShowMode.Size = new System.Drawing.Size(280, 21);
            this.cmbQueriedShowMode.TabIndex = 6;
            // 
            // lblQueriedShowModeTip
            // 
            this.lblQueriedShowModeTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblQueriedShowModeTip.Appearance.Options.UseForeColor = true;
            this.lblQueriedShowModeTip.Location = new System.Drawing.Point(362, 111);
            this.lblQueriedShowModeTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblQueriedShowModeTip.Name = "lblQueriedShowModeTip";
            this.lblQueriedShowModeTip.Size = new System.Drawing.Size(7, 14);
            this.lblQueriedShowModeTip.TabIndex = 78;
            this.lblQueriedShowModeTip.Text = "*";
            // 
            // ccmbDataFieldSetting
            // 
            this.ccmbDataFieldSetting.EditValue = "";
            this.ccmbDataFieldSetting.Location = new System.Drawing.Point(77, 136);
            this.ccmbDataFieldSetting.Name = "ccmbDataFieldSetting";
            this.ccmbDataFieldSetting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbDataFieldSetting.Properties.PopupSizeable = false;
            this.ccmbDataFieldSetting.Properties.SelectAllItemVisible = false;
            this.ccmbDataFieldSetting.Size = new System.Drawing.Size(280, 20);
            this.ccmbDataFieldSetting.TabIndex = 80;
            // 
            // lblSystemDataFields
            // 
            this.lblSystemDataFields.Location = new System.Drawing.Point(11, 139);
            this.lblSystemDataFields.Name = "lblSystemDataFields";
            this.lblSystemDataFields.Size = new System.Drawing.Size(60, 14);
            this.lblSystemDataFields.TabIndex = 79;
            this.lblSystemDataFields.Text = "系统字段：";
            // 
            // QueryModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ccmbDataFieldSetting);
            this.Controls.Add(this.lblSystemDataFields);
            this.Controls.Add(this.lblQueriedShowModeTip);
            this.Controls.Add(this.cmbQueriedShowMode);
            this.Controls.Add(this.icmbDataQueriedType);
            this.Controls.Add(this.ccmbDataRange);
            this.Controls.Add(this.lblQueriedShowMode);
            this.Controls.Add(this.ccmbGroupCondition);
            this.Controls.Add(this.lblGroupCondition);
            this.Controls.Add(this.ccmbSystemCondition);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.lblMainTable);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.hleTableSetting);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lblDataRange);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblTableCodeRequired);
            this.Controls.Add(this.txtToolTip);
            this.Controls.Add(this.lblDataQueriedNameTip);
            this.Controls.Add(this.lblTooltip);
            this.Controls.Add(this.txtDataQueriedCode);
            this.Controls.Add(this.txtDataQueriedName);
            this.Controls.Add(this.lblDataQueriedName);
            this.Controls.Add(this.lblSystemCondition);
            this.Name = "QueryModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.QueryModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDataQueriedCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataQueriedName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToolTip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataQueriedType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleTableSetting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbSystemCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbGroupCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataQueriedType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataFieldSetting.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTooltip;
        private DevExpress.XtraEditors.TextEdit txtDataQueriedCode;
        private DevExpress.XtraEditors.TextEdit txtDataQueriedName;
        private DevExpress.XtraEditors.LabelControl lblDataQueriedName;
        private DevExpress.XtraEditors.LabelControl lblDataQueriedNameTip;
        private DevExpress.XtraEditors.MemoEdit txtToolTip;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.Utils.ImageCollection icDataQueriedType;
        private DevExpress.XtraEditors.LabelControl lblTableCodeRequired;
        private DevExpress.XtraEditors.LabelControl lblDataRange;
        private DevExpress.XtraEditors.LabelControl lblCode;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.HyperLinkEdit hleTableSetting;
        private DevExpress.XtraEditors.TextEdit txtTableName;
        private DevExpress.XtraEditors.LabelControl lblMainTable;
        private DevExpress.XtraEditors.LabelControl lblSystemCondition;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbSystemCondition;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbGroupCondition;
        private DevExpress.XtraEditors.LabelControl lblGroupCondition;
        private DevExpress.XtraEditors.LabelControl lblQueriedShowMode;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbDataRange;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDataQueriedType;
        private AppFramework.WinFormsControls.ComoboxTreeview cmbQueriedShowMode;
        private DevExpress.XtraEditors.LabelControl lblQueriedShowModeTip;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbDataFieldSetting;
        private DevExpress.XtraEditors.LabelControl lblSystemDataFields;
    }
}
