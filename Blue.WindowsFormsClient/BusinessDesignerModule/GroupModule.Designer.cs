namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class GroupModule
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
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.lblCode = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameTip = new DevExpress.XtraEditors.LabelControl();
            this.txtTooltip = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTooltip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(35, 210);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 21;
            this.lblNotes.Text = "提示：";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(77, 50);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 64;
            this.txtCode.Size = new System.Drawing.Size(281, 20);
            this.txtCode.TabIndex = 2;
            // 
            // lblCode
            // 
            this.lblCode.Location = new System.Drawing.Point(11, 53);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(60, 14);
            this.lblCode.TabIndex = 13;
            this.lblCode.Text = "分组编码：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(77, 16);
            this.txtName.Name = "txtName";
            this.txtName.Properties.MaxLength = 64;
            this.txtName.Size = new System.Drawing.Size(282, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(11, 18);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(60, 14);
            this.lblName.TabIndex = 15;
            this.lblName.Text = "分组名称：";
            // 
            // lblNameTip
            // 
            this.lblNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameTip.Appearance.Options.UseForeColor = true;
            this.lblNameTip.Location = new System.Drawing.Point(364, 19);
            this.lblNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameTip.Name = "lblNameTip";
            this.lblNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblNameTip.TabIndex = 22;
            this.lblNameTip.Text = "*";
            // 
            // txtTooltip
            // 
            this.txtTooltip.Location = new System.Drawing.Point(79, 207);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Properties.MaxLength = 256;
            this.txtTooltip.Size = new System.Drawing.Size(280, 69);
            this.txtTooltip.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(364, 55);
            this.labelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(7, 14);
            this.labelControl1.TabIndex = 24;
            this.labelControl1.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(77, 285);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 67);
            this.txtNotes.TabIndex = 25;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(35, 288);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 26;
            this.labelControl2.Text = "备注：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 86);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 27;
            this.labelControl3.Text = "关联报表：";
            // 
            // listBoxControl1
            // 
            this.listBoxControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBoxControl1.Location = new System.Drawing.Point(79, 86);
            this.listBoxControl1.Name = "listBoxControl1";
            this.listBoxControl1.Size = new System.Drawing.Size(278, 108);
            this.listBoxControl1.TabIndex = 28;
            // 
            // GroupModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxControl1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtTooltip);
            this.Controls.Add(this.lblNameTip);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Name = "GroupModule";
            this.Size = new System.Drawing.Size(386, 363);
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTooltip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl lblCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblNameTip;
        private DevExpress.XtraEditors.MemoEdit txtTooltip;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
    }
}
