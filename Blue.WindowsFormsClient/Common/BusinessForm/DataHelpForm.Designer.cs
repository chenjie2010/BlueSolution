namespace Blue.WindowsFormsClient.Common
{
    partial class DataHelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataHelpForm));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.icData = new DevExpress.Utils.ImageCollection();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.richEditControl = new DevExpress.XtraRichEdit.RichEditControl();
            this.pnlAttachment = new DevExpress.XtraEditors.PanelControl();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAttachment)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.lblTitle);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(884, 57);
            this.panelControl2.TabIndex = 10;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("新宋体", 16F);
            this.lblTitle.Appearance.ImageIndex = 0;
            this.lblTitle.Appearance.ImageList = this.icData;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseImageIndex = true;
            this.lblTitle.Appearance.Options.UseImageList = true;
            this.lblTitle.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblTitle.Location = new System.Drawing.Point(355, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(117, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "使用帮助";
            // 
            // icData
            // 
            this.icData.ImageSize = new System.Drawing.Size(24, 24);
            this.icData.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icData.ImageStream")));
            this.icData.Images.SetKeyName(0, "Client_Common_Data_Help.png");
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.richEditControl);
            this.pnlMain.Controls.Add(this.pnlAttachment);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 57);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(884, 624);
            this.pnlMain.TabIndex = 11;
            // 
            // richEditControl
            // 
            this.richEditControl.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl.Location = new System.Drawing.Point(2, 2);
            this.richEditControl.Name = "richEditControl";
            this.richEditControl.ReadOnly = true;
            this.richEditControl.Size = new System.Drawing.Size(880, 549);
            this.richEditControl.TabIndex = 8;
            // 
            // pnlAttachment
            // 
            this.pnlAttachment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAttachment.Location = new System.Drawing.Point(2, 551);
            this.pnlAttachment.Name = "pnlAttachment";
            this.pnlAttachment.Size = new System.Drawing.Size(880, 71);
            this.pnlAttachment.TabIndex = 9;
            // 
            // DataHelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 681);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DataHelpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "使用帮助";
            this.Load += new System.EventHandler(this.DataGuidanceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlAttachment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.Utils.ImageCollection icData;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraRichEdit.RichEditControl richEditControl;
        private DevExpress.XtraEditors.PanelControl pnlAttachment;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}