namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class SheetModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SheetModule));
            this.lblSheetDescription = new DevExpress.XtraEditors.LabelControl();
            this.txtSheetCode = new DevExpress.XtraEditors.TextEdit();
            this.lblSheetCode = new DevExpress.XtraEditors.LabelControl();
            this.txtSheetName = new DevExpress.XtraEditors.TextEdit();
            this.lblSheetName = new DevExpress.XtraEditors.LabelControl();
            this.lblSheetNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtSheetDescription = new DevExpress.XtraEditors.MemoEdit();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.lblSheetCodeRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblApprovalNumber = new DevExpress.XtraEditors.LabelControl();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lbApprovalNumberTip = new DevExpress.XtraEditors.LabelControl();
            this.icReportType = new DevExpress.Utils.ImageCollection(this.components);
            this.icDataWarehouse = new DevExpress.Utils.ImageCollection(this.components);
            this.txtApprovalNumber = new DevExpress.XtraEditors.TextEdit();
            this.lblSheetDescriptionRequired = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icReportType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApprovalNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSheetDescription
            // 
            this.lblSheetDescription.Location = new System.Drawing.Point(11, 114);
            this.lblSheetDescription.Name = "lblSheetDescription";
            this.lblSheetDescription.Size = new System.Drawing.Size(60, 14);
            this.lblSheetDescription.TabIndex = 21;
            this.lblSheetDescription.Text = "样表描述：";
            // 
            // txtSheetCode
            // 
            this.txtSheetCode.Location = new System.Drawing.Point(77, 50);
            this.txtSheetCode.Name = "txtSheetCode";
            this.txtSheetCode.Properties.MaxLength = 32;
            this.txtSheetCode.Properties.ReadOnly = true;
            this.txtSheetCode.Size = new System.Drawing.Size(282, 20);
            this.txtSheetCode.TabIndex = 2;
            // 
            // lblSheetCode
            // 
            this.lblSheetCode.Location = new System.Drawing.Point(11, 52);
            this.lblSheetCode.Name = "lblSheetCode";
            this.lblSheetCode.Size = new System.Drawing.Size(60, 14);
            this.lblSheetCode.TabIndex = 13;
            this.lblSheetCode.Text = "样表编码：";
            // 
            // txtSheetName
            // 
            this.txtSheetName.Location = new System.Drawing.Point(77, 17);
            this.txtSheetName.Name = "txtSheetName";
            this.txtSheetName.Properties.MaxLength = 64;
            this.txtSheetName.Size = new System.Drawing.Size(282, 20);
            this.txtSheetName.TabIndex = 1;
            // 
            // lblSheetName
            // 
            this.lblSheetName.Location = new System.Drawing.Point(11, 19);
            this.lblSheetName.Name = "lblSheetName";
            this.lblSheetName.Size = new System.Drawing.Size(60, 14);
            this.lblSheetName.TabIndex = 15;
            this.lblSheetName.Text = "样表名称：";
            // 
            // lblSheetNameRequired
            // 
            this.lblSheetNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblSheetNameRequired.Appearance.Options.UseForeColor = true;
            this.lblSheetNameRequired.Location = new System.Drawing.Point(366, 19);
            this.lblSheetNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblSheetNameRequired.Name = "lblSheetNameRequired";
            this.lblSheetNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblSheetNameRequired.TabIndex = 22;
            this.lblSheetNameRequired.Text = "*";
            // 
            // txtSheetDescription
            // 
            this.txtSheetDescription.EditValue = "";
            this.txtSheetDescription.Location = new System.Drawing.Point(77, 114);
            this.txtSheetDescription.Name = "txtSheetDescription";
            this.txtSheetDescription.Properties.MaxLength = 256;
            this.txtSheetDescription.Size = new System.Drawing.Size(280, 127);
            this.txtSheetDescription.TabIndex = 4;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(77, 253);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 72);
            this.txtNotes.TabIndex = 5;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(35, 255);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // lblSheetCodeRequired
            // 
            this.lblSheetCodeRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblSheetCodeRequired.Appearance.Options.UseForeColor = true;
            this.lblSheetCodeRequired.Location = new System.Drawing.Point(366, 52);
            this.lblSheetCodeRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblSheetCodeRequired.Name = "lblSheetCodeRequired";
            this.lblSheetCodeRequired.Size = new System.Drawing.Size(7, 14);
            this.lblSheetCodeRequired.TabIndex = 24;
            this.lblSheetCodeRequired.Text = "*";
            // 
            // lblApprovalNumber
            // 
            this.lblApprovalNumber.Location = new System.Drawing.Point(11, 83);
            this.lblApprovalNumber.Name = "lblApprovalNumber";
            this.lblApprovalNumber.Size = new System.Drawing.Size(60, 14);
            this.lblApprovalNumber.TabIndex = 48;
            this.lblApprovalNumber.Text = "批文编号：";
            // 
            // lnkDetailedView
            // 
            this.lnkDetailedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedView.EditValue = "样表详情";
            this.lnkDetailedView.Location = new System.Drawing.Point(77, 332);
            this.lnkDetailedView.Name = "lnkDetailedView";
            this.lnkDetailedView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedView.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedView.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedView.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedView.TabIndex = 6;
            // 
            // lbApprovalNumberTip
            // 
            this.lbApprovalNumberTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lbApprovalNumberTip.Appearance.Options.UseForeColor = true;
            this.lbApprovalNumberTip.Location = new System.Drawing.Point(366, 85);
            this.lbApprovalNumberTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lbApprovalNumberTip.Name = "lbApprovalNumberTip";
            this.lbApprovalNumberTip.Size = new System.Drawing.Size(7, 14);
            this.lbApprovalNumberTip.TabIndex = 65;
            this.lbApprovalNumberTip.Text = "*";
            // 
            // icReportType
            // 
            this.icReportType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icReportType.ImageStream")));
            this.icReportType.Images.SetKeyName(0, "Enum_ReportType_Basic.png");
            this.icReportType.Images.SetKeyName(1, "Enum_ReportType_Statistics.png");
            this.icReportType.Images.SetKeyName(2, "Enum_ReportType_Common.png");
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
            // txtApprovalNumber
            // 
            this.txtApprovalNumber.Location = new System.Drawing.Point(77, 82);
            this.txtApprovalNumber.Name = "txtApprovalNumber";
            this.txtApprovalNumber.Properties.MaxLength = 32;
            this.txtApprovalNumber.Properties.NullValuePrompt = "填写一个数字即可，建议默认值为1";
            this.txtApprovalNumber.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtApprovalNumber.Properties.ShowNullValuePromptWhenFocused = true;
            this.txtApprovalNumber.Size = new System.Drawing.Size(282, 20);
            this.txtApprovalNumber.TabIndex = 3;
            // 
            // lblSheetDescriptionRequired
            // 
            this.lblSheetDescriptionRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblSheetDescriptionRequired.Appearance.Options.UseForeColor = true;
            this.lblSheetDescriptionRequired.Location = new System.Drawing.Point(363, 116);
            this.lblSheetDescriptionRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblSheetDescriptionRequired.Name = "lblSheetDescriptionRequired";
            this.lblSheetDescriptionRequired.Size = new System.Drawing.Size(7, 14);
            this.lblSheetDescriptionRequired.TabIndex = 66;
            this.lblSheetDescriptionRequired.Text = "*";
            // 
            // SheetModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSheetDescriptionRequired);
            this.Controls.Add(this.txtApprovalNumber);
            this.Controls.Add(this.lbApprovalNumberTip);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.lblApprovalNumber);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblSheetCodeRequired);
            this.Controls.Add(this.txtSheetDescription);
            this.Controls.Add(this.lblSheetNameRequired);
            this.Controls.Add(this.lblSheetDescription);
            this.Controls.Add(this.txtSheetCode);
            this.Controls.Add(this.lblSheetCode);
            this.Controls.Add(this.txtSheetName);
            this.Controls.Add(this.lblSheetName);
            this.Name = "SheetModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.ReportModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icReportType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApprovalNumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblSheetDescription;
        private DevExpress.XtraEditors.TextEdit txtSheetCode;
        private DevExpress.XtraEditors.LabelControl lblSheetCode;
        private DevExpress.XtraEditors.TextEdit txtSheetName;
        private DevExpress.XtraEditors.LabelControl lblSheetName;
        private DevExpress.XtraEditors.LabelControl lblSheetNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtSheetDescription;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.LabelControl lblSheetCodeRequired;
        private DevExpress.XtraEditors.LabelControl lblApprovalNumber;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.LabelControl lbApprovalNumberTip;
        private DevExpress.Utils.ImageCollection icDataWarehouse;
        private DevExpress.Utils.ImageCollection icReportType;
        private DevExpress.XtraEditors.TextEdit txtApprovalNumber;
        private DevExpress.XtraEditors.LabelControl lblSheetDescriptionRequired;
    }
}
