namespace Blue.WindowsFormsClient.MyQueryModule
{
    partial class QueryPropertiesForm
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.sbtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.txtPageSize = new DevExpress.XtraEditors.TextEdit();
            this.chkDistinct = new DevExpress.XtraEditors.CheckEdit();
            this.lblDistinct = new System.Windows.Forms.Label();
            this.lblPageSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDistinct.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.sbtnClose);
            this.panelControl1.Controls.Add(this.sbtnConfirm);
            this.panelControl1.Controls.Add(this.txtPageSize);
            this.panelControl1.Controls.Add(this.chkDistinct);
            this.panelControl1.Controls.Add(this.lblDistinct);
            this.panelControl1.Controls.Add(this.lblPageSize);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.LookAndFeel.SkinName = "Money Twins";
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(361, 111);
            this.panelControl1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(191, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "提示：范围在[10,1000]之间";
            // 
            // sbtnClose
            // 
            this.sbtnClose.Location = new System.Drawing.Point(193, 74);
            this.sbtnClose.LookAndFeel.SkinName = "Blue";
            this.sbtnClose.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnClose.Name = "sbtnClose";
            this.sbtnClose.Size = new System.Drawing.Size(75, 23);
            this.sbtnClose.TabIndex = 4;
            this.sbtnClose.Text = "关闭(&C)";
            this.sbtnClose.Click += new System.EventHandler(this.sbtnClose_Click);
            // 
            // sbtnConfirm
            // 
            this.sbtnConfirm.Location = new System.Drawing.Point(107, 74);
            this.sbtnConfirm.LookAndFeel.SkinName = "Blue";
            this.sbtnConfirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnConfirm.Name = "sbtnConfirm";
            this.sbtnConfirm.Size = new System.Drawing.Size(75, 23);
            this.sbtnConfirm.TabIndex = 3;
            this.sbtnConfirm.Text = "确定(&O)";
            this.sbtnConfirm.Click += new System.EventHandler(this.sbtnConfirm_Click);
            // 
            // txtPageSize
            // 
            this.txtPageSize.Location = new System.Drawing.Point(99, 41);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Properties.LookAndFeel.SkinName = "Money Twins";
            this.txtPageSize.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtPageSize.Size = new System.Drawing.Size(83, 21);
            this.txtPageSize.TabIndex = 2;
            // 
            // chkDistinct
            // 
            this.chkDistinct.Location = new System.Drawing.Point(97, 14);
            this.chkDistinct.Name = "chkDistinct";
            this.chkDistinct.Properties.Caption = "";
            this.chkDistinct.Properties.LookAndFeel.SkinName = "Blue";
            this.chkDistinct.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.chkDistinct.Size = new System.Drawing.Size(23, 19);
            this.chkDistinct.TabIndex = 0;
            // 
            // lblDistinct
            // 
            this.lblDistinct.AutoSize = true;
            this.lblDistinct.Location = new System.Drawing.Point(12, 18);
            this.lblDistinct.Name = "lblDistinct";
            this.lblDistinct.Size = new System.Drawing.Size(89, 12);
            this.lblDistinct.TabIndex = 25;
            this.lblDistinct.Text = "清除相同记录：";
            // 
            // lblPageSize
            // 
            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Location = new System.Drawing.Point(24, 46);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new System.Drawing.Size(77, 12);
            this.lblPageSize.TabIndex = 24;
            this.lblPageSize.Text = "每页记录数：";
            // 
            // QueryPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 111);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryPropertiesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "属性设置";
            this.Load += new System.EventHandler(this.QueryPropertiesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDistinct.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton sbtnClose;
        private DevExpress.XtraEditors.SimpleButton sbtnConfirm;
        private DevExpress.XtraEditors.TextEdit txtPageSize;
        private DevExpress.XtraEditors.CheckEdit chkDistinct;
        private System.Windows.Forms.Label lblDistinct;
        private System.Windows.Forms.Label lblPageSize;
    }
}