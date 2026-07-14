namespace Blue.WindowsFormsClient
{
    partial class ExportedModeForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportedModeForm));
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.icmbExportedMode = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icExportedMode = new DevExpress.Utils.ImageCollection(this.components);
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbExportedMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icExportedMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.Common_Information_16;
            this.gcMain.Controls.Add(this.icmbExportedMode);
            this.gcMain.Controls.Add(this.lblTip);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(478, 76);
            this.gcMain.TabIndex = 0;
            this.gcMain.Text = "提示：请先选择数据导出方式。";
            // 
            // icmbExportedMode
            // 
            this.icmbExportedMode.Location = new System.Drawing.Point(113, 36);
            this.icmbExportedMode.Name = "icmbExportedMode";
            this.icmbExportedMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbExportedMode.Properties.SmallImages = this.icExportedMode;
            this.icmbExportedMode.Size = new System.Drawing.Size(335, 20);
            this.icmbExportedMode.TabIndex = 1;
            // 
            // icExportedMode
            // 
            this.icExportedMode.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icExportedMode.ImageStream")));
            this.icExportedMode.Images.SetKeyName(0, "Client_Common_Excel.png");
            this.icExportedMode.Images.SetKeyName(1, "Client_Common_PDF.png");
            // 
            // lblTip
            // 
            this.lblTip.Location = new System.Drawing.Point(22, 39);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(84, 14);
            this.lblTip.TabIndex = 0;
            this.lblTip.Text = "数据导出模式：";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 76);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(478, 42);
            this.pnlBottom.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(264, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(175, 10);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确认(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // ExportedModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 118);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportedModeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据导出模式";
            this.Load += new System.EventHandler(this.ExportedModeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            this.gcMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbExportedMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icExportedMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbExportedMode;
        private DevExpress.Utils.ImageCollection icExportedMode;
    }
}