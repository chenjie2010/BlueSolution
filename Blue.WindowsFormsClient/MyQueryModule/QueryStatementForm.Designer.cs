namespace Blue.WindowsFormsClient.MyQueryModule
{
    partial class QueryStatementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryStatementForm));
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.rtxtNotes = new System.Windows.Forms.RichTextBox();
            this.lblQueryName = new System.Windows.Forms.Label();
            this.lblQueryCategoryTip = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.userQueryDropdownList = new Blue.WindowsFormsClient.UserQueryDropdownList();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnClose.Location = new System.Drawing.Point(218, 171);
            this.btnClose.LookAndFeel.SkinName = "Blue";
            this.btnClose.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Apply_Small;
            this.btnConfirm.Location = new System.Drawing.Point(132, 171);
            this.btnConfirm.LookAndFeel.SkinName = "Blue";
            this.btnConfirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确定(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "2.jpg");
            this.imageListTree.Images.SetKeyName(1, "1.jpg");
            this.imageListTree.Images.SetKeyName(2, "3.jpg");
            // 
            // rtxtNotes
            // 
            this.rtxtNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.rtxtNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtNotes.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rtxtNotes.Location = new System.Drawing.Point(99, 39);
            this.rtxtNotes.MaxLength = 4000;
            this.rtxtNotes.Name = "rtxtNotes";
            this.rtxtNotes.ReadOnly = true;
            this.rtxtNotes.Size = new System.Drawing.Size(235, 125);
            this.rtxtNotes.TabIndex = 244;
            this.rtxtNotes.Text = "";
            // 
            // lblQueryName
            // 
            this.lblQueryName.AutoSize = true;
            this.lblQueryName.Location = new System.Drawing.Point(7, 13);
            this.lblQueryName.Name = "lblQueryName";
            this.lblQueryName.Size = new System.Drawing.Size(89, 12);
            this.lblQueryName.TabIndex = 249;
            this.lblQueryName.Text = "查询语句名称：";
            // 
            // lblQueryCategoryTip
            // 
            this.lblQueryCategoryTip.AutoSize = true;
            this.lblQueryCategoryTip.ForeColor = System.Drawing.Color.Red;
            this.lblQueryCategoryTip.Location = new System.Drawing.Point(336, 12);
            this.lblQueryCategoryTip.Name = "lblQueryCategoryTip";
            this.lblQueryCategoryTip.Size = new System.Drawing.Size(11, 12);
            this.lblQueryCategoryTip.TabIndex = 251;
            this.lblQueryCategoryTip.Text = "*";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(7, 41);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(89, 12);
            this.lblNotes.TabIndex = 252;
            this.lblNotes.Text = "查询语句备注：";
            // 
            // userQueryDropdownList
            // 
            this.userQueryDropdownList.CustomGroupContract = null;
            this.userQueryDropdownList.Location = new System.Drawing.Point(97, 8);
            this.userQueryDropdownList.Name = "userQueryDropdownList";
            this.userQueryDropdownList.OnlySelectedLeaf = true;
            this.userQueryDropdownList.Size = new System.Drawing.Size(233, 21);
            this.userQueryDropdownList.TabIndex = 0;
            this.userQueryDropdownList.UserQueryContract = null;
            this.userQueryDropdownList.AfterTreeNodeSelect += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.userQueryDropdownList_AfterTreeNodeSelect);
            // 
            // QueryStatementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(349, 203);
            this.Controls.Add(this.userQueryDropdownList);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblQueryCategoryTip);
            this.Controls.Add(this.lblQueryName);
            this.Controls.Add(this.rtxtNotes);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConfirm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryStatementForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打开查询语句";
            this.Load += new System.EventHandler(this.OpenSentenceForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private System.Windows.Forms.RichTextBox rtxtNotes;
        internal System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.Label lblQueryName;
        private System.Windows.Forms.Label lblQueryCategoryTip;
        private System.Windows.Forms.Label lblNotes;
        private UserQueryDropdownList userQueryDropdownList;
    }
}