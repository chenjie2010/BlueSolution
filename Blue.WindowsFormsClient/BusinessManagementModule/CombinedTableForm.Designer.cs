namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    partial class CombinedTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CombinedTableForm));
            this.SuspendLayout();
            // 
            // CombinedTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 556);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CombinedTableForm";
            this.Text = "组合表管理";
            this.OnBeoreCreatedClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeItemClickEventArgs>(this.CombinedTableForm_OnBeoreCreatedClick);
            this.OnDeleteClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeItemClickEventArgs>(this.CombinedTableForm_OnDeleteClick);
            this.OnSettingClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeItemClickEventArgs>(this.CombinedTableForm_OnSettingClick);
            this.OnBeforeSelectOfTreeView += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.CombinedTableForm_OnBeforeSelectOfTreeView);
            this.OnBeforeTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.CombinedTableForm_OnBeforeTreeNodeExpand);
            this.OnBeoreQueryClick += new System.EventHandler<System.EventArgs>(this.CombinedTableForm_OnBeoreQueryClick);
            this.OnConfirmClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeEditEventArgs>(this.CombinedTableForm_OnConfirmClick);
            this.OnCancelClick += new System.EventHandler<AppFramework.WinFormsLibrary.EventArgument.TreeNodeEditEventArgs>(this.CombinedTableForm_OnCancelClick);
            this.Load += new System.EventHandler(this.CombinedTableForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}