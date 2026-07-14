namespace Blue.WindowsFormsClient.Common
{
    partial class DataTemplateTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTemplateTableForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.icData = new DevExpress.Utils.ImageCollection(this.components);
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.meToolTip = new DevExpress.XtraEditors.MemoEdit();
            this.xscPanel = new DevExpress.XtraEditors.XtraScrollableControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meToolTip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(2, 563);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1180, 46);
            this.panelControl1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 0;
            this.btnSave.ImageList = this.icData;
            this.btnSave.Location = new System.Drawing.Point(519, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // icData
            // 
            this.icData.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icData.ImageStream")));
            this.icData.Images.SetKeyName(0, "Clinet_Common_Save.png");
            this.icData.Images.SetKeyName(1, "Client_Common_Cancel.png");
            // 
            // btnCancel
            // 
            this.btnCancel.ImageIndex = 1;
            this.btnCancel.ImageList = this.icData;
            this.btnCancel.Location = new System.Drawing.Point(609, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // meToolTip
            // 
            this.meToolTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meToolTip.EditValue = "填报内容测试。";
            this.meToolTip.Location = new System.Drawing.Point(2, 2);
            this.meToolTip.Name = "meToolTip";
            this.meToolTip.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.meToolTip.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.meToolTip.Properties.Appearance.Options.UseBackColor = true;
            this.meToolTip.Properties.Appearance.Options.UseForeColor = true;
            this.meToolTip.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.meToolTip.Properties.ReadOnly = true;
            this.meToolTip.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.meToolTip.Size = new System.Drawing.Size(1176, 17);
            this.meToolTip.TabIndex = 3;
            // 
            // xscPanel
            // 
            this.xscPanel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.xscPanel.Appearance.Options.UseBackColor = true;
            this.xscPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xscPanel.Location = new System.Drawing.Point(2, 23);
            this.xscPanel.LookAndFeel.SkinName = "Money Twins";
            this.xscPanel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xscPanel.Name = "xscPanel";
            this.xscPanel.Size = new System.Drawing.Size(1180, 540);
            this.xscPanel.TabIndex = 7;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.meToolTip);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1180, 21);
            this.panelControl2.TabIndex = 8;
            // 
            // DataTemplateTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 611);
            this.Controls.Add(this.xscPanel);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataTemplateTableForm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataTemplateTableForm";
            this.Load += new System.EventHandler(this.DataTemplateTableForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.icData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meToolTip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.Utils.ImageCollection icData;
        private DevExpress.XtraEditors.MemoEdit meToolTip;
        private DevExpress.XtraEditors.XtraScrollableControl xscPanel;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}