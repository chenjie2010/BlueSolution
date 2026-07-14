namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    partial class CombinedTableModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CombinedTableModule));
            this.lblTooltip = new DevExpress.XtraEditors.LabelControl();
            this.txtCombinedTableCode = new DevExpress.XtraEditors.TextEdit();
            this.lblCombinedTableCode = new DevExpress.XtraEditors.LabelControl();
            this.txtCombinedTableName = new DevExpress.XtraEditors.TextEdit();
            this.lblCombinedTableName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtTooltip = new DevExpress.XtraEditors.MemoEdit();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.lblTableCodeRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblAssociatedTableTip = new DevExpress.XtraEditors.LabelControl();
            this.hleAssociatedTables = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lnkDetailedTable = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lblAssociatedTables = new DevExpress.XtraEditors.LabelControl();
            this.lstAssociatedTables = new DevExpress.XtraEditors.ListBoxControl();
            this.lblDataWarehouse = new DevExpress.XtraEditors.LabelControl();
            this.icmbDataWarehouse = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblDataWarehouseTip = new DevExpress.XtraEditors.LabelControl();
            this.icDataWarehouse = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtCombinedTableCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCombinedTableName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTooltip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleAssociatedTables.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstAssociatedTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTooltip
            // 
            this.lblTooltip.Location = new System.Drawing.Point(22, 252);
            this.lblTooltip.Name = "lblTooltip";
            this.lblTooltip.Size = new System.Drawing.Size(60, 14);
            this.lblTooltip.TabIndex = 21;
            this.lblTooltip.Text = "用户提示：";
            // 
            // txtCombinedTableCode
            // 
            this.txtCombinedTableCode.Location = new System.Drawing.Point(85, 48);
            this.txtCombinedTableCode.Name = "txtCombinedTableCode";
            this.txtCombinedTableCode.Properties.MaxLength = 32;
            this.txtCombinedTableCode.Size = new System.Drawing.Size(282, 20);
            this.txtCombinedTableCode.TabIndex = 2;
            // 
            // lblCombinedTableCode
            // 
            this.lblCombinedTableCode.Location = new System.Drawing.Point(10, 51);
            this.lblCombinedTableCode.Name = "lblCombinedTableCode";
            this.lblCombinedTableCode.Size = new System.Drawing.Size(72, 14);
            this.lblCombinedTableCode.TabIndex = 13;
            this.lblCombinedTableCode.Text = "组合表编码：";
            // 
            // txtCombinedTableName
            // 
            this.txtCombinedTableName.Location = new System.Drawing.Point(85, 16);
            this.txtCombinedTableName.Name = "txtCombinedTableName";
            this.txtCombinedTableName.Properties.MaxLength = 64;
            this.txtCombinedTableName.Size = new System.Drawing.Size(282, 20);
            this.txtCombinedTableName.TabIndex = 1;
            // 
            // lblCombinedTableName
            // 
            this.lblCombinedTableName.Location = new System.Drawing.Point(10, 18);
            this.lblCombinedTableName.Name = "lblCombinedTableName";
            this.lblCombinedTableName.Size = new System.Drawing.Size(72, 14);
            this.lblCombinedTableName.TabIndex = 15;
            this.lblCombinedTableName.Text = "组合表名称：";
            // 
            // lblNameRequired
            // 
            this.lblNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameRequired.Appearance.Options.UseForeColor = true;
            this.lblNameRequired.Location = new System.Drawing.Point(374, 18);
            this.lblNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameRequired.Name = "lblNameRequired";
            this.lblNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblNameRequired.TabIndex = 22;
            this.lblNameRequired.Text = "*";
            // 
            // txtTooltip
            // 
            this.txtTooltip.EditValue = "";
            this.txtTooltip.Location = new System.Drawing.Point(85, 251);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Properties.MaxLength = 256;
            this.txtTooltip.Size = new System.Drawing.Size(280, 39);
            this.txtTooltip.TabIndex = 6;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(85, 301);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 26);
            this.txtNotes.TabIndex = 7;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(46, 304);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // lblTableCodeRequired
            // 
            this.lblTableCodeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblTableCodeRequired.Appearance.Options.UseForeColor = true;
            this.lblTableCodeRequired.Location = new System.Drawing.Point(374, 54);
            this.lblTableCodeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblTableCodeRequired.Name = "lblTableCodeRequired";
            this.lblTableCodeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblTableCodeRequired.TabIndex = 24;
            this.lblTableCodeRequired.Text = "*";
            // 
            // lblAssociatedTableTip
            // 
            this.lblAssociatedTableTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblAssociatedTableTip.Appearance.Options.UseForeColor = true;
            this.lblAssociatedTableTip.Location = new System.Drawing.Point(373, 122);
            this.lblAssociatedTableTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblAssociatedTableTip.Name = "lblAssociatedTableTip";
            this.lblAssociatedTableTip.Size = new System.Drawing.Size(7, 14);
            this.lblAssociatedTableTip.TabIndex = 50;
            this.lblAssociatedTableTip.Text = "*";
            // 
            // hleAssociatedTables
            // 
            this.hleAssociatedTables.EditValue = "设置数据表...";
            this.hleAssociatedTables.Location = new System.Drawing.Point(275, 227);
            this.hleAssociatedTables.Name = "hleAssociatedTables";
            this.hleAssociatedTables.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleAssociatedTables.Properties.Appearance.Options.UseBackColor = true;
            this.hleAssociatedTables.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleAssociatedTables.Size = new System.Drawing.Size(90, 18);
            this.hleAssociatedTables.TabIndex = 5;
            this.hleAssociatedTables.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hleAssociatedTables_OpenLink);
            // 
            // lnkDetailedTable
            // 
            this.lnkDetailedTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedTable.EditValue = "组合表详情";
            this.lnkDetailedTable.Location = new System.Drawing.Point(78, 332);
            this.lnkDetailedTable.Name = "lnkDetailedTable";
            this.lnkDetailedTable.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedTable.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedTable.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedTable.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedTable.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedTable.TabIndex = 8;
            // 
            // lblAssociatedTables
            // 
            this.lblAssociatedTables.Location = new System.Drawing.Point(34, 115);
            this.lblAssociatedTables.Name = "lblAssociatedTables";
            this.lblAssociatedTables.Size = new System.Drawing.Size(48, 14);
            this.lblAssociatedTables.TabIndex = 62;
            this.lblAssociatedTables.Text = "数据表：";
            // 
            // lstAssociatedTables
            // 
            this.lstAssociatedTables.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstAssociatedTables.IncrementalSearch = true;
            this.lstAssociatedTables.Location = new System.Drawing.Point(85, 113);
            this.lstAssociatedTables.Name = "lstAssociatedTables";
            this.lstAssociatedTables.Size = new System.Drawing.Size(282, 108);
            this.lstAssociatedTables.TabIndex = 4;
            // 
            // lblDataWarehouse
            // 
            this.lblDataWarehouse.Location = new System.Drawing.Point(22, 83);
            this.lblDataWarehouse.Name = "lblDataWarehouse";
            this.lblDataWarehouse.Size = new System.Drawing.Size(60, 14);
            this.lblDataWarehouse.TabIndex = 63;
            this.lblDataWarehouse.Text = "数据仓库：";
            // 
            // icmbDataWarehouse
            // 
            this.icmbDataWarehouse.Location = new System.Drawing.Point(85, 82);
            this.icmbDataWarehouse.Name = "icmbDataWarehouse";
            this.icmbDataWarehouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDataWarehouse.Size = new System.Drawing.Size(282, 20);
            this.icmbDataWarehouse.TabIndex = 3;
            this.icmbDataWarehouse.SelectedIndexChanged += new System.EventHandler(this.icmbDataWarehouse_SelectedIndexChanged);
            // 
            // lblDataWarehouseTip
            // 
            this.lblDataWarehouseTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblDataWarehouseTip.Appearance.Options.UseForeColor = true;
            this.lblDataWarehouseTip.Location = new System.Drawing.Point(374, 85);
            this.lblDataWarehouseTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblDataWarehouseTip.Name = "lblDataWarehouseTip";
            this.lblDataWarehouseTip.Size = new System.Drawing.Size(7, 14);
            this.lblDataWarehouseTip.TabIndex = 65;
            this.lblDataWarehouseTip.Text = "*";
            // 
            // icDataWarehouse
            // 
            this.icDataWarehouse.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataWarehouse.ImageStream")));
            this.icDataWarehouse.Images.SetKeyName(0, "Common_Digit_One.png");
            this.icDataWarehouse.Images.SetKeyName(1, "Common_Digit_Two.png");
            this.icDataWarehouse.Images.SetKeyName(2, "Common_Digit_Three.png");
            this.icDataWarehouse.Images.SetKeyName(3, "Common_Digit_Four.png");
            this.icDataWarehouse.Images.SetKeyName(4, "Common_Digit_Five.png");
            // 
            // CombinedTableModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDataWarehouseTip);
            this.Controls.Add(this.icmbDataWarehouse);
            this.Controls.Add(this.lblDataWarehouse);
            this.Controls.Add(this.lstAssociatedTables);
            this.Controls.Add(this.lblAssociatedTables);
            this.Controls.Add(this.lnkDetailedTable);
            this.Controls.Add(this.hleAssociatedTables);
            this.Controls.Add(this.lblAssociatedTableTip);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblTableCodeRequired);
            this.Controls.Add(this.txtTooltip);
            this.Controls.Add(this.lblNameRequired);
            this.Controls.Add(this.lblTooltip);
            this.Controls.Add(this.txtCombinedTableCode);
            this.Controls.Add(this.lblCombinedTableCode);
            this.Controls.Add(this.txtCombinedTableName);
            this.Controls.Add(this.lblCombinedTableName);
            this.Name = "CombinedTableModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.ViewModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCombinedTableCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCombinedTableName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTooltip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleAssociatedTables.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstAssociatedTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTooltip;
        private DevExpress.XtraEditors.TextEdit txtCombinedTableCode;
        private DevExpress.XtraEditors.LabelControl lblCombinedTableCode;
        private DevExpress.XtraEditors.TextEdit txtCombinedTableName;
        private DevExpress.XtraEditors.LabelControl lblCombinedTableName;
        private DevExpress.XtraEditors.LabelControl lblNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtTooltip;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.LabelControl lblTableCodeRequired;
        private DevExpress.XtraEditors.LabelControl lblAssociatedTableTip;
        private DevExpress.XtraEditors.HyperLinkEdit hleAssociatedTables;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedTable;
        private DevExpress.XtraEditors.LabelControl lblAssociatedTables;
        private DevExpress.XtraEditors.ListBoxControl lstAssociatedTables;
        private DevExpress.XtraEditors.LabelControl lblDataWarehouse;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDataWarehouse;
        private DevExpress.XtraEditors.LabelControl lblDataWarehouseTip;
        private DevExpress.Utils.ImageCollection icDataWarehouse;
    }
}
