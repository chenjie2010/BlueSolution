namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class SaveSnapshotForm
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
            this.lblSnapshotName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateExpireDate = new DevExpress.XtraEditors.DateEdit();
            this.lblDataFieldTypeTip = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sbtnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.etxtSnapshotName = new DevExpress.XtraEditors.TextEdit();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExpireDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExpireDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtSnapshotName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSnapshotName
            // 
            this.lblSnapshotName.Location = new System.Drawing.Point(5, 15);
            this.lblSnapshotName.Name = "lblSnapshotName";
            this.lblSnapshotName.Size = new System.Drawing.Size(70, 16);
            this.lblSnapshotName.TabIndex = 21;
            this.lblSnapshotName.Text = "快照名称：";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "快照时间：";
            // 
            // dateExpireDate
            // 
            this.dateExpireDate.EditValue = null;
            this.dateExpireDate.Location = new System.Drawing.Point(72, 40);
            this.dateExpireDate.Name = "dateExpireDate";
            this.dateExpireDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateExpireDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateExpireDate.Properties.LookAndFeel.SkinName = "Blue";
            this.dateExpireDate.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dateExpireDate.Size = new System.Drawing.Size(278, 20);
            this.dateExpireDate.TabIndex = 24;
            // 
            // lblDataFieldTypeTip
            // 
            this.lblDataFieldTypeTip.AutoSize = true;
            this.lblDataFieldTypeTip.ForeColor = System.Drawing.Color.Red;
            this.lblDataFieldTypeTip.Location = new System.Drawing.Point(354, 14);
            this.lblDataFieldTypeTip.Name = "lblDataFieldTypeTip";
            this.lblDataFieldTypeTip.Size = new System.Drawing.Size(11, 12);
            this.lblDataFieldTypeTip.TabIndex = 49;
            this.lblDataFieldTypeTip.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(354, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 50;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(28, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 51;
            this.label3.Text = "备注：";
            // 
            // sbtnConfirm
            // 
            this.sbtnConfirm.Location = new System.Drawing.Point(138, 177);
            this.sbtnConfirm.LookAndFeel.SkinName = "Blue";
            this.sbtnConfirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnConfirm.Name = "sbtnConfirm";
            this.sbtnConfirm.Size = new System.Drawing.Size(65, 23);
            this.sbtnConfirm.TabIndex = 54;
            this.sbtnConfirm.Text = "确定(&O)";
            this.sbtnConfirm.Click += new System.EventHandler(this.sbtnConfirm_Click);
            // 
            // sbtnClose
            // 
            this.sbtnClose.Location = new System.Drawing.Point(210, 177);
            this.sbtnClose.LookAndFeel.SkinName = "Blue";
            this.sbtnClose.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnClose.Name = "sbtnClose";
            this.sbtnClose.Size = new System.Drawing.Size(65, 23);
            this.sbtnClose.TabIndex = 53;
            this.sbtnClose.Text = "关闭(&C)";
            this.sbtnClose.Click += new System.EventHandler(this.sbtnClose_Click);
            // 
            // etxtSnapshotName
            // 
            this.etxtSnapshotName.Location = new System.Drawing.Point(72, 10);
            this.etxtSnapshotName.Name = "etxtSnapshotName";
            this.etxtSnapshotName.Size = new System.Drawing.Size(278, 20);
            this.etxtSnapshotName.TabIndex = 22;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(72, 72);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(278, 96);
            this.txtNotes.TabIndex = 55;
            // 
            // SaveSnapshotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(380, 208);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.sbtnConfirm);
            this.Controls.Add(this.sbtnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDataFieldTypeTip);
            this.Controls.Add(this.dateExpireDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.etxtSnapshotName);
            this.Controls.Add(this.lblSnapshotName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SaveSnapshotForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保存快照";
            this.Load += new System.EventHandler(this.SaveSnapshotForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateExpireDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExpireDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtSnapshotName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit etxtSnapshotName;
        private System.Windows.Forms.Label lblSnapshotName;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit dateExpireDate;
        private System.Windows.Forms.Label lblDataFieldTypeTip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton sbtnConfirm;
        private DevExpress.XtraEditors.SimpleButton sbtnClose;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
    }
}