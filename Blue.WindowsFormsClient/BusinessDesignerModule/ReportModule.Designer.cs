namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class ReportModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportModule));
            this.lblTooltip = new DevExpress.XtraEditors.LabelControl();
            this.txtReportCode = new DevExpress.XtraEditors.TextEdit();
            this.lblReportType = new DevExpress.XtraEditors.LabelControl();
            this.lblReportCode = new DevExpress.XtraEditors.LabelControl();
            this.txtReportName = new DevExpress.XtraEditors.TextEdit();
            this.lblReportName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtTooltip = new DevExpress.XtraEditors.MemoEdit();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.lblReportCodeRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblDataWarehouse = new DevExpress.XtraEditors.LabelControl();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lblMainTableTip = new DevExpress.XtraEditors.LabelControl();
            this.icmbReportType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icmbDataWarehouse = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icDataWarehouse = new DevExpress.Utils.ImageCollection(this.components);
            this.icReportType = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtReportCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTooltip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbReportType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icReportType)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTooltip
            // 
            this.lblTooltip.Location = new System.Drawing.Point(11, 153);
            this.lblTooltip.Name = "lblTooltip";
            this.lblTooltip.Size = new System.Drawing.Size(60, 14);
            this.lblTooltip.TabIndex = 21;
            this.lblTooltip.Text = "用户提示：";
            // 
            // txtReportCode
            // 
            this.txtReportCode.Location = new System.Drawing.Point(77, 50);
            this.txtReportCode.Name = "txtReportCode";
            this.txtReportCode.Properties.MaxLength = 32;
            this.txtReportCode.Size = new System.Drawing.Size(282, 20);
            this.txtReportCode.TabIndex = 2;
            // 
            // lblReportType
            // 
            this.lblReportType.Location = new System.Drawing.Point(11, 85);
            this.lblReportType.Name = "lblReportType";
            this.lblReportType.Size = new System.Drawing.Size(60, 14);
            this.lblReportType.TabIndex = 17;
            this.lblReportType.Text = "报表类型：";
            // 
            // lblReportCode
            // 
            this.lblReportCode.Location = new System.Drawing.Point(11, 52);
            this.lblReportCode.Name = "lblReportCode";
            this.lblReportCode.Size = new System.Drawing.Size(60, 14);
            this.lblReportCode.TabIndex = 13;
            this.lblReportCode.Text = "报表编码：";
            // 
            // txtReportName
            // 
            this.txtReportName.Location = new System.Drawing.Point(77, 17);
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.Properties.MaxLength = 64;
            this.txtReportName.Size = new System.Drawing.Size(282, 20);
            this.txtReportName.TabIndex = 1;
            // 
            // lblReportName
            // 
            this.lblReportName.Location = new System.Drawing.Point(11, 19);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(60, 14);
            this.lblReportName.TabIndex = 15;
            this.lblReportName.Text = "报表名称：";
            // 
            // lblNameRequired
            // 
            this.lblNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameRequired.Appearance.Options.UseForeColor = true;
            this.lblNameRequired.Location = new System.Drawing.Point(366, 19);
            this.lblNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameRequired.Name = "lblNameRequired";
            this.lblNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblNameRequired.TabIndex = 22;
            this.lblNameRequired.Text = "*";
            // 
            // txtTooltip
            // 
            this.txtTooltip.EditValue = "";
            this.txtTooltip.Location = new System.Drawing.Point(77, 151);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Properties.MaxLength = 256;
            this.txtTooltip.Size = new System.Drawing.Size(280, 89);
            this.txtTooltip.TabIndex = 5;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(77, 252);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 72);
            this.txtNotes.TabIndex = 6;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(35, 254);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // lblReportCodeRequired
            // 
            this.lblReportCodeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblReportCodeRequired.Appearance.Options.UseForeColor = true;
            this.lblReportCodeRequired.Location = new System.Drawing.Point(366, 52);
            this.lblReportCodeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblReportCodeRequired.Name = "lblReportCodeRequired";
            this.lblReportCodeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblReportCodeRequired.TabIndex = 24;
            this.lblReportCodeRequired.Text = "*";
            // 
            // lblDataWarehouse
            // 
            this.lblDataWarehouse.Location = new System.Drawing.Point(11, 118);
            this.lblDataWarehouse.Name = "lblDataWarehouse";
            this.lblDataWarehouse.Size = new System.Drawing.Size(60, 14);
            this.lblDataWarehouse.TabIndex = 48;
            this.lblDataWarehouse.Text = "数据仓库：";
            // 
            // lnkDetailedView
            // 
            this.lnkDetailedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedView.EditValue = "报表详情";
            this.lnkDetailedView.Location = new System.Drawing.Point(77, 332);
            this.lnkDetailedView.Name = "lnkDetailedView";
            this.lnkDetailedView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedView.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedView.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedView.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedView.TabIndex = 59;
            // 
            // lblMainTableTip
            // 
            this.lblMainTableTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblMainTableTip.Appearance.Options.UseForeColor = true;
            this.lblMainTableTip.Location = new System.Drawing.Point(366, 123);
            this.lblMainTableTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblMainTableTip.Name = "lblMainTableTip";
            this.lblMainTableTip.Size = new System.Drawing.Size(7, 14);
            this.lblMainTableTip.TabIndex = 65;
            this.lblMainTableTip.Text = "*";
            // 
            // icmbReportType
            // 
            this.icmbReportType.Location = new System.Drawing.Point(77, 83);
            this.icmbReportType.Name = "icmbReportType";
            this.icmbReportType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbReportType.Properties.SmallImages = this.icReportType;
            this.icmbReportType.Size = new System.Drawing.Size(280, 20);
            this.icmbReportType.TabIndex = 3;
            // 
            // icmbDataWarehouse
            // 
            this.icmbDataWarehouse.Location = new System.Drawing.Point(77, 116);
            this.icmbDataWarehouse.Name = "icmbDataWarehouse";
            this.icmbDataWarehouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDataWarehouse.Properties.SmallImages = this.icDataWarehouse;
            this.icmbDataWarehouse.Size = new System.Drawing.Size(280, 20);
            this.icmbDataWarehouse.TabIndex = 4;
            // 
            // icDataWarehouse
            // 
            this.icDataWarehouse.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataWarehouse.ImageStream")));
            this.icDataWarehouse.Images.SetKeyName(0, "Common_Number_First.png");
            this.icDataWarehouse.Images.SetKeyName(1, "Common_Number_Second.png");
            this.icDataWarehouse.Images.SetKeyName(2, "Common_Number_Third.png");
            this.icDataWarehouse.Images.SetKeyName(3, "Common_Number_Fourth.png");
            this.icDataWarehouse.Images.SetKeyName(4, "Common_Number_Fifth.png");
            // 
            // icReportType
            // 
            this.icReportType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icReportType.ImageStream")));
            this.icReportType.Images.SetKeyName(0, "Enum_ReportType_Basic.png");
            this.icReportType.Images.SetKeyName(1, "Enum_ReportType_Statistics.png");
            this.icReportType.Images.SetKeyName(2, "Enum_ReportType_Common.png");
            // 
            // ReportModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.icmbDataWarehouse);
            this.Controls.Add(this.icmbReportType);
            this.Controls.Add(this.lblMainTableTip);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.lblDataWarehouse);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblReportCodeRequired);
            this.Controls.Add(this.txtTooltip);
            this.Controls.Add(this.lblNameRequired);
            this.Controls.Add(this.lblTooltip);
            this.Controls.Add(this.txtReportCode);
            this.Controls.Add(this.lblReportType);
            this.Controls.Add(this.lblReportCode);
            this.Controls.Add(this.txtReportName);
            this.Controls.Add(this.lblReportName);
            this.Name = "ReportModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.ReportModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtReportCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReportName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTooltip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbReportType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icReportType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTooltip;
        private DevExpress.XtraEditors.TextEdit txtReportCode;
        private DevExpress.XtraEditors.LabelControl lblReportType;
        private DevExpress.XtraEditors.LabelControl lblReportCode;
        private DevExpress.XtraEditors.TextEdit txtReportName;
        private DevExpress.XtraEditors.LabelControl lblReportName;
        private DevExpress.XtraEditors.LabelControl lblNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtTooltip;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.LabelControl lblReportCodeRequired;
        private DevExpress.XtraEditors.LabelControl lblDataWarehouse;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.LabelControl lblMainTableTip;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbReportType;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDataWarehouse;
        private DevExpress.Utils.ImageCollection icDataWarehouse;
        private DevExpress.Utils.ImageCollection icReportType;
    }
}
