namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class DataAuditingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataAuditingForm));
            this.SuspendLayout();
            // 
            // DataAuditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 556);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataAuditingForm";
            this.Text = "数据审核设计";
            this.OnBeoreCreatedClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeItemClickEventArgs>(this.DataAuditingForm_OnBeoreCreatedClick);
            this.OnDeleteClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeItemClickEventArgs>(this.DataAuditingForm_OnDeleteClick);
            this.OnSettingClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeItemClickEventArgs>(this.DataAuditingForm_OnSettingClick);
            this.OnBeforeSelectOfTreeView += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.DataAuditingForm_OnBeforeSelectOfTreeView);
            this.OnBeforeTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.DataAuditingForm_OnBeforeTreeNodeExpand);
            this.OnBeoreQueryClick += new System.EventHandler<System.EventArgs>(this.DataAuditingForm_OnBeoreQueryClick);
            this.OnConfirmClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeEditEventArgs>(this.DataAuditingForm_OnConfirmClick);
            this.OnCancelClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeEditEventArgs>(this.DataAuditingForm_OnCancelClick);
            this.Load += new System.EventHandler(this.DataAuditingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}