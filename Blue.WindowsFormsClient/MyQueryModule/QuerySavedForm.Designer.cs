namespace Blue.WindowsFormsClient.MyQueryModule
{
    partial class QuerySavedForm
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
            this.lblQueryName = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.txtQueryName = new DevExpress.XtraEditors.TextEdit();
            this.lblQueryCategory = new System.Windows.Forms.Label();
            this.lblQueryCategoryTip = new System.Windows.Forms.Label();
            this.lblQueryNameTip = new System.Windows.Forms.Label();
            this.cmbQueryCategory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.hlnkUserQuery = new DevExpress.XtraEditors.HyperlinkLabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQueryCategory.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblQueryName
            // 
            this.lblQueryName.AutoSize = true;
            this.lblQueryName.Location = new System.Drawing.Point(8, 63);
            this.lblQueryName.Name = "lblQueryName";
            this.lblQueryName.Size = new System.Drawing.Size(89, 12);
            this.lblQueryName.TabIndex = 25;
            this.lblQueryName.Text = "查询语句名称：";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(8, 96);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(89, 12);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "查询语句备注：";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(96, 92);
            this.txtNote.Name = "txtNote";
            this.txtNote.Properties.LookAndFeel.SkinName = "Blue";
            this.txtNote.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtNote.Properties.MaxLength = 255;
            this.txtNote.Size = new System.Drawing.Size(235, 73);
            this.txtNote.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnClose.Location = new System.Drawing.Point(226, 181);
            this.btnClose.LookAndFeel.SkinName = "Blue";
            this.btnClose.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Apply_Small;
            this.btnConfirm.Location = new System.Drawing.Point(140, 181);
            this.btnConfirm.LookAndFeel.SkinName = "Blue";
            this.btnConfirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确定(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtQueryName
            // 
            this.txtQueryName.Location = new System.Drawing.Point(96, 60);
            this.txtQueryName.Name = "txtQueryName";
            this.txtQueryName.Properties.LookAndFeel.SkinName = "Money Twins";
            this.txtQueryName.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtQueryName.Properties.MaxLength = 64;
            this.txtQueryName.Size = new System.Drawing.Size(235, 20);
            this.txtQueryName.TabIndex = 2;
            // 
            // lblQueryCategory
            // 
            this.lblQueryCategory.AutoSize = true;
            this.lblQueryCategory.Location = new System.Drawing.Point(8, 14);
            this.lblQueryCategory.Name = "lblQueryCategory";
            this.lblQueryCategory.Size = new System.Drawing.Size(89, 12);
            this.lblQueryCategory.TabIndex = 27;
            this.lblQueryCategory.Text = "查询语句分类：";
            // 
            // lblQueryCategoryTip
            // 
            this.lblQueryCategoryTip.AutoSize = true;
            this.lblQueryCategoryTip.ForeColor = System.Drawing.Color.Red;
            this.lblQueryCategoryTip.Location = new System.Drawing.Point(341, 11);
            this.lblQueryCategoryTip.Name = "lblQueryCategoryTip";
            this.lblQueryCategoryTip.Size = new System.Drawing.Size(11, 12);
            this.lblQueryCategoryTip.TabIndex = 83;
            this.lblQueryCategoryTip.Text = "*";
            // 
            // lblQueryNameTip
            // 
            this.lblQueryNameTip.AutoSize = true;
            this.lblQueryNameTip.ForeColor = System.Drawing.Color.Red;
            this.lblQueryNameTip.Location = new System.Drawing.Point(341, 63);
            this.lblQueryNameTip.Name = "lblQueryNameTip";
            this.lblQueryNameTip.Size = new System.Drawing.Size(11, 12);
            this.lblQueryNameTip.TabIndex = 84;
            this.lblQueryNameTip.Text = "*";
            // 
            // cmbQueryCategory
            // 
            this.cmbQueryCategory.Location = new System.Drawing.Point(96, 10);
            this.cmbQueryCategory.Name = "cmbQueryCategory";
            this.cmbQueryCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbQueryCategory.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbQueryCategory.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbQueryCategory.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbQueryCategory.Size = new System.Drawing.Size(235, 20);
            this.cmbQueryCategory.TabIndex = 0;
            // 
            // hlnkUserQuery
            // 
            this.hlnkUserQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkUserQuery.Location = new System.Drawing.Point(232, 38);
            this.hlnkUserQuery.Name = "hlnkUserQuery";
            this.hlnkUserQuery.Size = new System.Drawing.Size(96, 14);
            this.hlnkUserQuery.TabIndex = 1;
            this.hlnkUserQuery.Text = "查询语句分类管理";
            this.hlnkUserQuery.Click += new System.EventHandler(this.hlnkUserQuery_Click);
            // 
            // SaveSentenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(359, 216);
            this.Controls.Add(this.hlnkUserQuery);
            this.Controls.Add(this.cmbQueryCategory);
            this.Controls.Add(this.lblQueryNameTip);
            this.Controls.Add(this.lblQueryCategoryTip);
            this.Controls.Add(this.lblQueryCategory);
            this.Controls.Add(this.txtQueryName);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblQueryName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SaveSentenceForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询语句";
            this.Load += new System.EventHandler(this.QuerySavedForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQueryCategory.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQueryName;
        private System.Windows.Forms.Label lblNotes;
        private DevExpress.XtraEditors.MemoEdit txtNote;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.TextEdit txtQueryName;
        private System.Windows.Forms.Label lblQueryCategory;
        private System.Windows.Forms.Label lblQueryCategoryTip;
        private System.Windows.Forms.Label lblQueryNameTip;
        private DevExpress.XtraEditors.ComboBoxEdit cmbQueryCategory;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkUserQuery;
    }
}