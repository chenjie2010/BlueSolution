namespace Blue.WindowsFormsClient.Common
{
    partial class CustomDataFieldForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstDataField = new DevExpress.XtraEditors.ListBoxControl();
            this.gcDataFieldList = new DevExpress.XtraEditors.GroupControl();
            this.gcDataFieldDetail = new DevExpress.XtraEditors.GroupControl();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPhyscialName = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sbtnVerify = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.lblComment = new System.Windows.Forms.Label();
            this.cmbeDataFieldType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDataFieldName = new System.Windows.Forms.Label();
            this.cmbeDataFieldProperty = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDataFieldType = new System.Windows.Forms.Label();
            this.txtDataFieldName = new DevExpress.XtraEditors.TextEdit();
            this.lblDataFieldProperty = new System.Windows.Forms.Label();
            this.etxtCustomDataField = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lstDataField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFieldList)).BeginInit();
            this.gcDataFieldList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFieldDetail)).BeginInit();
            this.gcDataFieldDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhyscialName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeDataFieldType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeDataFieldProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFieldName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtCustomDataField.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lstDataField
            // 
            this.lstDataField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDataField.Location = new System.Drawing.Point(2, 22);
            this.lstDataField.LookAndFeel.SkinName = "Money Twins";
            this.lstDataField.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lstDataField.Name = "lstDataField";
            this.lstDataField.Size = new System.Drawing.Size(196, 280);
            this.lstDataField.TabIndex = 0;
            this.lstDataField.SelectedIndexChanged += new System.EventHandler(this.lstDataField_SelectedIndexChanged);
            // 
            // gcDataFieldList
            // 
            this.gcDataFieldList.Controls.Add(this.lstDataField);
            this.gcDataFieldList.Dock = System.Windows.Forms.DockStyle.Left;
            this.gcDataFieldList.Location = new System.Drawing.Point(0, 0);
            this.gcDataFieldList.LookAndFeel.SkinName = "Money Twins";
            this.gcDataFieldList.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcDataFieldList.Name = "gcDataFieldList";
            this.gcDataFieldList.Size = new System.Drawing.Size(200, 304);
            this.gcDataFieldList.TabIndex = 1;
            this.gcDataFieldList.Text = "物理字段列表";
            // 
            // gcDataFieldDetail
            // 
            this.gcDataFieldDetail.Controls.Add(this.label1);
            this.gcDataFieldDetail.Controls.Add(this.txtPhyscialName);
            this.gcDataFieldDetail.Controls.Add(this.panelControl1);
            this.gcDataFieldDetail.Controls.Add(this.cmbeDataFieldType);
            this.gcDataFieldDetail.Controls.Add(this.lblDataFieldName);
            this.gcDataFieldDetail.Controls.Add(this.cmbeDataFieldProperty);
            this.gcDataFieldDetail.Controls.Add(this.lblDataFieldType);
            this.gcDataFieldDetail.Controls.Add(this.txtDataFieldName);
            this.gcDataFieldDetail.Controls.Add(this.lblDataFieldProperty);
            this.gcDataFieldDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDataFieldDetail.Location = new System.Drawing.Point(200, 0);
            this.gcDataFieldDetail.LookAndFeel.SkinName = "Money Twins";
            this.gcDataFieldDetail.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcDataFieldDetail.Name = "gcDataFieldDetail";
            this.gcDataFieldDetail.Size = new System.Drawing.Size(419, 304);
            this.gcDataFieldDetail.TabIndex = 2;
            this.gcDataFieldDetail.Text = "字段详情";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 114;
            this.label1.Text = "物理名称：";
            // 
            // txtPhyscialName
            // 
            this.txtPhyscialName.Location = new System.Drawing.Point(87, 60);
            this.txtPhyscialName.Name = "txtPhyscialName";
            this.txtPhyscialName.Properties.ReadOnly = true;
            this.txtPhyscialName.Size = new System.Drawing.Size(314, 20);
            this.txtPhyscialName.TabIndex = 115;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.etxtCustomDataField);
            this.panelControl1.Controls.Add(this.sbtnVerify);
            this.panelControl1.Controls.Add(this.sbtnClear);
            this.panelControl1.Controls.Add(this.sbtnClose);
            this.panelControl1.Controls.Add(this.sbtnConfirm);
            this.panelControl1.Controls.Add(this.lblComment);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(2, 150);
            this.panelControl1.LookAndFeel.SkinName = "Money Twins";
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(415, 152);
            this.panelControl1.TabIndex = 113;
            // 
            // sbtnVerify
            // 
            this.sbtnVerify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.sbtnVerify.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Apply_Small;
            this.sbtnVerify.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.sbtnVerify.Location = new System.Drawing.Point(239, 117);
            this.sbtnVerify.LookAndFeel.SkinName = "Blue";
            this.sbtnVerify.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnVerify.Name = "sbtnVerify";
            this.sbtnVerify.Size = new System.Drawing.Size(24, 24);
            this.sbtnVerify.TabIndex = 122;
            this.sbtnVerify.ToolTip = "校验";
            this.sbtnVerify.Click += new System.EventHandler(this.sbtnVerify_Click);
            // 
            // sbtnClear
            // 
            this.sbtnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.sbtnClear.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.sbtnClear.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.sbtnClear.Location = new System.Drawing.Point(207, 117);
            this.sbtnClear.LookAndFeel.SkinName = "Blue";
            this.sbtnClear.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnClear.Name = "sbtnClear";
            this.sbtnClear.Size = new System.Drawing.Size(24, 24);
            this.sbtnClear.TabIndex = 121;
            this.sbtnClear.ToolTip = "清除";
            this.sbtnClear.Click += new System.EventHandler(this.sbtnClear_Click);
            // 
            // sbtnClose
            // 
            this.sbtnClose.Location = new System.Drawing.Point(332, 118);
            this.sbtnClose.LookAndFeel.SkinName = "Blue";
            this.sbtnClose.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnClose.Name = "sbtnClose";
            this.sbtnClose.Size = new System.Drawing.Size(55, 23);
            this.sbtnClose.TabIndex = 113;
            this.sbtnClose.Text = "关闭(&C)";
            this.sbtnClose.Click += new System.EventHandler(this.sbtnClose_Click);
            // 
            // sbtnConfirm
            // 
            this.sbtnConfirm.Location = new System.Drawing.Point(269, 118);
            this.sbtnConfirm.LookAndFeel.SkinName = "Blue";
            this.sbtnConfirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnConfirm.Name = "sbtnConfirm";
            this.sbtnConfirm.Size = new System.Drawing.Size(55, 23);
            this.sbtnConfirm.TabIndex = 112;
            this.sbtnConfirm.Text = "确定(&O)";
            this.sbtnConfirm.Click += new System.EventHandler(this.sbtnConfirm_Click);
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(13, 13);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(67, 14);
            this.lblComment.TabIndex = 103;
            this.lblComment.Text = "自定义值：";
            // 
            // cmbeDataFieldType
            // 
            this.cmbeDataFieldType.Location = new System.Drawing.Point(87, 120);
            this.cmbeDataFieldType.Name = "cmbeDataFieldType";
            this.cmbeDataFieldType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeDataFieldType.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbeDataFieldType.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbeDataFieldType.Properties.ReadOnly = true;
            this.cmbeDataFieldType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbeDataFieldType.Size = new System.Drawing.Size(314, 20);
            this.cmbeDataFieldType.TabIndex = 108;
            // 
            // lblDataFieldName
            // 
            this.lblDataFieldName.AutoSize = true;
            this.lblDataFieldName.Location = new System.Drawing.Point(12, 34);
            this.lblDataFieldName.Name = "lblDataFieldName";
            this.lblDataFieldName.Size = new System.Drawing.Size(67, 14);
            this.lblDataFieldName.TabIndex = 103;
            this.lblDataFieldName.Text = "字段名称：";
            // 
            // cmbeDataFieldProperty
            // 
            this.cmbeDataFieldProperty.Location = new System.Drawing.Point(87, 92);
            this.cmbeDataFieldProperty.Name = "cmbeDataFieldProperty";
            this.cmbeDataFieldProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeDataFieldProperty.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbeDataFieldProperty.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbeDataFieldProperty.Properties.ReadOnly = true;
            this.cmbeDataFieldProperty.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbeDataFieldProperty.Size = new System.Drawing.Size(314, 20);
            this.cmbeDataFieldProperty.TabIndex = 107;
            // 
            // lblDataFieldType
            // 
            this.lblDataFieldType.AutoSize = true;
            this.lblDataFieldType.Location = new System.Drawing.Point(12, 124);
            this.lblDataFieldType.Name = "lblDataFieldType";
            this.lblDataFieldType.Size = new System.Drawing.Size(67, 14);
            this.lblDataFieldType.TabIndex = 104;
            this.lblDataFieldType.Text = "字段类型：";
            // 
            // txtDataFieldName
            // 
            this.txtDataFieldName.Location = new System.Drawing.Point(87, 31);
            this.txtDataFieldName.Name = "txtDataFieldName";
            this.txtDataFieldName.Properties.ReadOnly = true;
            this.txtDataFieldName.Size = new System.Drawing.Size(314, 20);
            this.txtDataFieldName.TabIndex = 106;
            // 
            // lblDataFieldProperty
            // 
            this.lblDataFieldProperty.AutoSize = true;
            this.lblDataFieldProperty.Location = new System.Drawing.Point(12, 96);
            this.lblDataFieldProperty.Name = "lblDataFieldProperty";
            this.lblDataFieldProperty.Size = new System.Drawing.Size(67, 14);
            this.lblDataFieldProperty.TabIndex = 105;
            this.lblDataFieldProperty.Text = "字段属性：";
            // 
            // etxtCustomDataField
            // 
            this.etxtCustomDataField.Location = new System.Drawing.Point(87, 9);
            this.etxtCustomDataField.Name = "etxtCustomDataField";
            this.etxtCustomDataField.Size = new System.Drawing.Size(314, 96);
            this.etxtCustomDataField.TabIndex = 123;
            // 
            // CustomDataFieldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 304);
            this.Controls.Add(this.gcDataFieldDetail);
            this.Controls.Add(this.gcDataFieldList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "CustomDataFieldForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义字段";
            this.Load += new System.EventHandler(this.CustomDataFieldForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lstDataField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFieldList)).EndInit();
            this.gcDataFieldList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFieldDetail)).EndInit();
            this.gcDataFieldDetail.ResumeLayout(false);
            this.gcDataFieldDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhyscialName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeDataFieldType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeDataFieldProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFieldName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtCustomDataField.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lstDataField;
        private DevExpress.XtraEditors.GroupControl gcDataFieldList;
        private DevExpress.XtraEditors.GroupControl gcDataFieldDetail;
        private DevExpress.XtraEditors.ComboBoxEdit cmbeDataFieldType;
        private System.Windows.Forms.Label lblDataFieldName;
        private DevExpress.XtraEditors.ComboBoxEdit cmbeDataFieldProperty;
        private System.Windows.Forms.Label lblDataFieldType;
        private DevExpress.XtraEditors.TextEdit txtDataFieldName;
        private System.Windows.Forms.Label lblDataFieldProperty;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton sbtnClear;
        private DevExpress.XtraEditors.SimpleButton sbtnClose;
        private DevExpress.XtraEditors.SimpleButton sbtnConfirm;
        private System.Windows.Forms.Label lblComment;
        private DevExpress.XtraEditors.SimpleButton sbtnVerify;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtPhyscialName;
        private DevExpress.XtraEditors.MemoEdit etxtCustomDataField;
    }
}