namespace Blue.WindowsFormsClient.MyWorkModule
{
    partial class ReviewedCommentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReviewedCommentForm));
            this.meComment = new DevExpress.XtraEditors.MemoEdit();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.gcComment = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.meComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcComment)).BeginInit();
            this.gcComment.SuspendLayout();
            this.SuspendLayout();
            // 
            // meComment
            // 
            this.meComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meComment.Location = new System.Drawing.Point(2, 21);
            this.meComment.Name = "meComment";
            this.meComment.Properties.MaxLength = 512;
            this.meComment.Size = new System.Drawing.Size(391, 155);
            this.meComment.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 178);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(395, 42);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(200, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(118, 9);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确认(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // gcComment
            // 
            this.gcComment.Controls.Add(this.meComment);
            this.gcComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcComment.Location = new System.Drawing.Point(0, 0);
            this.gcComment.Name = "gcComment";
            this.gcComment.Size = new System.Drawing.Size(395, 178);
            this.gcComment.TabIndex = 2;
            this.gcComment.Text = "本次审核意见";
            // 
            // ReviewedCommentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 220);
            this.Controls.Add(this.gcComment);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ReviewedCommentForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "审核意见";
            this.Load += new System.EventHandler(this.ReviewedCommentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.meComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcComment)).EndInit();
            this.gcComment.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.MemoEdit meComment;
        private DevExpress.XtraEditors.GroupControl gcComment;
    }
}