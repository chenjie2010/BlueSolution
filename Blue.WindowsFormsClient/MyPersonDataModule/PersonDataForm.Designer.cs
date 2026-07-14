namespace Blue.WindowsFormsClient.MyPersonDataModule
{
    partial class PersonDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonDataForm));
            this.meToolTip = new DevExpress.XtraEditors.MemoEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblReviewer = new DevExpress.XtraEditors.LabelControl();
            this.cmbReviewer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.meComment = new DevExpress.XtraEditors.MemoEdit();
            this.lblComment = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.icData = new DevExpress.Utils.ImageCollection(this.components);
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.xscPanel = new DevExpress.XtraEditors.XtraScrollableControl();
            ((System.ComponentModel.ISupportInitialize)(this.meToolTip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReviewer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).BeginInit();
            this.SuspendLayout();
            // 
            // meToolTip
            // 
            this.meToolTip.Dock = System.Windows.Forms.DockStyle.Top;
            this.meToolTip.EditValue = "填报内容测试。";
            this.meToolTip.Location = new System.Drawing.Point(0, 0);
            this.meToolTip.Name = "meToolTip";
            this.meToolTip.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.meToolTip.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.meToolTip.Properties.Appearance.Options.UseBackColor = true;
            this.meToolTip.Properties.Appearance.Options.UseForeColor = true;
            this.meToolTip.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.meToolTip.Properties.ReadOnly = true;
            this.meToolTip.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.meToolTip.Size = new System.Drawing.Size(1184, 17);
            this.meToolTip.TabIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblReviewer);
            this.panelControl1.Controls.Add(this.cmbReviewer);
            this.panelControl1.Controls.Add(this.meComment);
            this.panelControl1.Controls.Add(this.lblComment);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 344);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1184, 46);
            this.panelControl1.TabIndex = 5;
            // 
            // lblReviewer
            // 
            this.lblReviewer.Location = new System.Drawing.Point(751, 17);
            this.lblReviewer.Name = "lblReviewer";
            this.lblReviewer.Size = new System.Drawing.Size(84, 14);
            this.lblReviewer.TabIndex = 39;
            this.lblReviewer.Text = "下一步处理人：";
            // 
            // cmbReviewer
            // 
            this.cmbReviewer.Location = new System.Drawing.Point(839, 14);
            this.cmbReviewer.Name = "cmbReviewer";
            this.cmbReviewer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReviewer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbReviewer.Size = new System.Drawing.Size(147, 20);
            this.cmbReviewer.TabIndex = 1;
            // 
            // meComment
            // 
            this.meComment.Location = new System.Drawing.Point(76, 9);
            this.meComment.Name = "meComment";
            this.meComment.Properties.MaxLength = 255;
            this.meComment.Size = new System.Drawing.Size(667, 28);
            this.meComment.TabIndex = 0;
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(11, 12);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(60, 14);
            this.lblComment.TabIndex = 36;
            this.lblComment.Text = "申请事由：";
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 0;
            this.btnSave.ImageList = this.icData;
            this.btnSave.Location = new System.Drawing.Point(1005, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "提交(&S)";
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
            this.btnCancel.Location = new System.Drawing.Point(1095, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // xscPanel
            // 
            this.xscPanel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.xscPanel.Appearance.Options.UseBackColor = true;
            this.xscPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xscPanel.Location = new System.Drawing.Point(0, 17);
            this.xscPanel.LookAndFeel.SkinName = "Money Twins";
            this.xscPanel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xscPanel.Name = "xscPanel";
            this.xscPanel.Size = new System.Drawing.Size(1184, 327);
            this.xscPanel.TabIndex = 8;
            // 
            // PersonDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 390);
            this.Controls.Add(this.xscPanel);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.meToolTip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PersonDataForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "个人数据";
            this.Load += new System.EventHandler(this.PersonDataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.meToolTip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReviewer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit meToolTip;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.XtraScrollableControl xscPanel;
        private DevExpress.Utils.ImageCollection icData;
        private DevExpress.XtraEditors.LabelControl lblComment;
        private DevExpress.XtraEditors.MemoEdit meComment;
        private DevExpress.XtraEditors.LabelControl lblReviewer;
        private DevExpress.XtraEditors.ComboBoxEdit cmbReviewer;
    }
}