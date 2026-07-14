namespace Blue.WindowsFormsClient.MyQueryModule
{
    partial class QueryManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryManagementForm));
            this.SuspendLayout();
            // 
            // QueryManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 556);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QueryManagementForm";
            this.Text = "查询语句管理";
            this.OnBeoreCreatedClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeItemClickEventArgs>(this.QueryManagementForm_OnBeoreCreatedClick);
            this.OnDeleteClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeItemClickEventArgs>(this.QueryManagementForm_OnDeleteClick);
            this.OnBeforeSelectOfTreeView += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.QueryManagementForm_OnBeforeSelectOfTreeView);
            this.OnBeforeTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.QueryManagementForm_OnBeforeTreeNodeExpand);
            this.OnBeoreQueryClick += new System.EventHandler<System.EventArgs>(this.QueryManagementForm_OnBeoreQueryClick);
            this.OnConfirmClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeEditEventArgs>(this.QueryManagementForm_OnConfirmClick);
            this.OnCancelClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeEditEventArgs>(this.QueryManagementForm_OnCancelClick);
            this.Load += new System.EventHandler(this.QueryManagementForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}