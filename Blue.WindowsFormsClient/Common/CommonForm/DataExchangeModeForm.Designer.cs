namespace Blue.WindowsFormsClient.Common
{
    partial class DataExchangeModeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataExchangeModeForm));
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.rgDataExchangeMode = new DevExpress.XtraEditors.RadioGroup();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgDataExchangeMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 119);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(496, 42);
            this.pnlBottom.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(275, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(186, 10);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确认(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // gcMain
            // 
            this.gcMain.CaptionImageUri.Uri = "AlignHorizontalTop";
            this.gcMain.Controls.Add(this.progressPanel);
            this.gcMain.Controls.Add(this.rgDataExchangeMode);
            this.gcMain.Controls.Add(this.pnlTop);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.Name = "gcMain";
            this.gcMain.Padding = new System.Windows.Forms.Padding(2);
            this.gcMain.Size = new System.Drawing.Size(496, 119);
            this.gcMain.TabIndex = 4;
            this.gcMain.Text = "当前节点名称：";
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "数据正在导出中......";
            this.progressPanel.Location = new System.Drawing.Point(164, 67);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(174, 40);
            this.progressPanel.TabIndex = 9;
            this.progressPanel.Text = "数据正在导出中......";
            this.progressPanel.Visible = false;
            // 
            // rgDataExchangeMode
            // 
            this.rgDataExchangeMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgDataExchangeMode.Location = new System.Drawing.Point(4, 58);
            this.rgDataExchangeMode.Name = "rgDataExchangeMode";
            this.rgDataExchangeMode.Properties.Columns = 3;
            this.rgDataExchangeMode.Size = new System.Drawing.Size(488, 57);
            this.rgDataExchangeMode.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblTip);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(4, 25);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(488, 33);
            this.pnlTop.TabIndex = 10;
            // 
            // lblTip
            // 
            this.lblTip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTip.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTip.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Information_16;
            this.lblTip.Appearance.Options.UseForeColor = true;
            this.lblTip.Appearance.Options.UseImage = true;
            this.lblTip.Appearance.Options.UseTextOptions = true;
            this.lblTip.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.lblTip.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblTip.Location = new System.Drawing.Point(6, 7);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(477, 20);
            this.lblTip.TabIndex = 2;
            this.lblTip.Text = "导出数据为当前节点的所有子节点数据（不含该节点），导入数据以该节点为父节点。";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // DataExchangeModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 161);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataExchangeModeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据交换";
            this.Load += new System.EventHandler(this.DataExchangeModeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgDataExchangeMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraEditors.RadioGroup rgDataExchangeMode;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.LabelControl lblTip;
    }
}