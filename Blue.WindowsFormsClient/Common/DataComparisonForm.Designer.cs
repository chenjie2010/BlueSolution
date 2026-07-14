namespace Blue.WindowsFormsClient.Common
{
    partial class DataComparisonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataComparisonForm));
            this.xscPanel = new DevExpress.XtraEditors.XtraScrollableControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtUserTypeName = new DevExpress.XtraEditors.TextEdit();
            this.txtDepName = new DevExpress.XtraEditors.TextEdit();
            this.txtUserActualName = new DevExpress.XtraEditors.TextEdit();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.lblUserTypeName = new DevExpress.XtraEditors.LabelControl();
            this.lblDepName = new DevExpress.XtraEditors.LabelControl();
            this.lblUserActualName = new DevExpress.XtraEditors.LabelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.chkShow = new DevExpress.XtraEditors.CheckEdit();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.gcSource = new DevExpress.XtraEditors.GroupControl();
            this.gcDest = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserTypeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserActualName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xscPanel
            // 
            this.xscPanel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.xscPanel.Appearance.Options.UseBackColor = true;
            this.xscPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xscPanel.Location = new System.Drawing.Point(0, 56);
            this.xscPanel.LookAndFeel.SkinName = "Money Twins";
            this.xscPanel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xscPanel.Name = "xscPanel";
            this.xscPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.xscPanel.Size = new System.Drawing.Size(940, 410);
            this.xscPanel.TabIndex = 9;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtUserTypeName);
            this.panelControl1.Controls.Add(this.txtDepName);
            this.panelControl1.Controls.Add(this.txtUserActualName);
            this.panelControl1.Controls.Add(this.txtUserName);
            this.panelControl1.Controls.Add(this.lblUserTypeName);
            this.panelControl1.Controls.Add(this.lblDepName);
            this.panelControl1.Controls.Add(this.lblUserActualName);
            this.panelControl1.Controls.Add(this.lblUserName);
            this.panelControl1.Controls.Add(this.chkShow);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(940, 35);
            this.panelControl1.TabIndex = 10;
            // 
            // txtUserTypeName
            // 
            this.txtUserTypeName.Location = new System.Drawing.Point(665, 8);
            this.txtUserTypeName.Name = "txtUserTypeName";
            this.txtUserTypeName.Properties.ReadOnly = true;
            this.txtUserTypeName.Size = new System.Drawing.Size(128, 20);
            this.txtUserTypeName.TabIndex = 8;
            // 
            // txtDepName
            // 
            this.txtDepName.Location = new System.Drawing.Point(411, 8);
            this.txtDepName.Name = "txtDepName";
            this.txtDepName.Properties.ReadOnly = true;
            this.txtDepName.Size = new System.Drawing.Size(182, 20);
            this.txtDepName.TabIndex = 7;
            // 
            // txtUserActualName
            // 
            this.txtUserActualName.Location = new System.Drawing.Point(247, 8);
            this.txtUserActualName.Name = "txtUserActualName";
            this.txtUserActualName.Properties.ReadOnly = true;
            this.txtUserActualName.Size = new System.Drawing.Size(116, 20);
            this.txtUserActualName.TabIndex = 6;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(64, 8);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(116, 20);
            this.txtUserName.TabIndex = 5;
            // 
            // lblUserTypeName
            // 
            this.lblUserTypeName.Location = new System.Drawing.Point(602, 10);
            this.lblUserTypeName.Name = "lblUserTypeName";
            this.lblUserTypeName.Size = new System.Drawing.Size(60, 14);
            this.lblUserTypeName.TabIndex = 4;
            this.lblUserTypeName.Text = "用户类型：";
            // 
            // lblDepName
            // 
            this.lblDepName.Location = new System.Drawing.Point(371, 10);
            this.lblDepName.Name = "lblDepName";
            this.lblDepName.Size = new System.Drawing.Size(36, 14);
            this.lblDepName.TabIndex = 3;
            this.lblDepName.Text = "单位：";
            // 
            // lblUserActualName
            // 
            this.lblUserActualName.Location = new System.Drawing.Point(186, 10);
            this.lblUserActualName.Name = "lblUserActualName";
            this.lblUserActualName.Size = new System.Drawing.Size(60, 14);
            this.lblUserActualName.TabIndex = 2;
            this.lblUserActualName.Text = "用户姓名：";
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(12, 10);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(48, 14);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "用户名：";
            // 
            // chkShow
            // 
            this.chkShow.Location = new System.Drawing.Point(806, 9);
            this.chkShow.Name = "chkShow";
            this.chkShow.Properties.Caption = "仅显示不同数据对比";
            this.chkShow.Size = new System.Drawing.Size(129, 19);
            this.chkShow.TabIndex = 0;
            this.chkShow.Visible = false;
            this.chkShow.CheckedChanged += new System.EventHandler(this.chkShow_CheckedChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 466);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(940, 15);
            this.pnlBottom.TabIndex = 11;
            // 
            // gcSource
            // 
            this.gcSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.gcSource.Location = new System.Drawing.Point(0, 0);
            this.gcSource.Name = "gcSource";
            this.gcSource.Size = new System.Drawing.Size(470, 21);
            this.gcSource.TabIndex = 12;
            this.gcSource.Text = "源数据";
            // 
            // gcDest
            // 
            this.gcDest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDest.Location = new System.Drawing.Point(470, 0);
            this.gcDest.Name = "gcDest";
            this.gcDest.Size = new System.Drawing.Size(470, 21);
            this.gcDest.TabIndex = 13;
            this.gcDest.Text = "更新后的数据";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gcDest);
            this.panelControl2.Controls.Add(this.gcSource);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 35);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(940, 21);
            this.panelControl2.TabIndex = 14;
            // 
            // DataComparisonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 481);
            this.Controls.Add(this.xscPanel);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DataComparisonForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据对比";
            this.Load += new System.EventHandler(this.DataComparisonForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserTypeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserActualName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xscPanel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit chkShow;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.TextEdit txtUserTypeName;
        private DevExpress.XtraEditors.TextEdit txtDepName;
        private DevExpress.XtraEditors.TextEdit txtUserActualName;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.LabelControl lblUserTypeName;
        private DevExpress.XtraEditors.LabelControl lblDepName;
        private DevExpress.XtraEditors.LabelControl lblUserActualName;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.GroupControl gcSource;
        private DevExpress.XtraEditors.GroupControl gcDest;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}