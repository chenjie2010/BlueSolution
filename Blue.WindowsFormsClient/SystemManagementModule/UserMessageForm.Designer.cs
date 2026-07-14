namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class UserMessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserMessageForm));
            this.devExpressGrid = new AppFramework.WinFormsControls.DevExpressGrid();
            this.SuspendLayout();
            // 
            // devExpressGrid
            // 
            this.devExpressGrid.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devExpressGrid.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.devExpressGrid.CheckboxColumnCaption = null;
            this.devExpressGrid.ColumnHeaderTexts = new string[] {
        "标题",
        "内容类型",
        "附件",
        "发件人",
        "发送时间"};
            this.devExpressGrid.DataKeyNames = new string[] {
        "MessageId"};
            this.devExpressGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devExpressGrid.ExportedExcel = false;
            this.devExpressGrid.FootText = null;
            this.devExpressGrid.ImportedExcel = false;
            this.devExpressGrid.IsMainTable = false;
            this.devExpressGrid.Location = new System.Drawing.Point(0, 0);
            this.devExpressGrid.Name = "devExpressGrid";
            this.devExpressGrid.PageSize = 30;
            this.devExpressGrid.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devExpressGrid.Size = new System.Drawing.Size(1139, 563);
            this.devExpressGrid.TabIndex = 5;
            this.devExpressGrid.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.devExpressGrid_OnPageIndexChanged);
            this.devExpressGrid.OnRowDoubleClick += new System.EventHandler<AppFramework.WinFormsControls.RowEvent>(this.devExpressGrid_OnRowDoubleClick);
            this.devExpressGrid.OnCustomColumnDisplayText += new System.EventHandler<DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs>(this.devExpressGrid_OnCustomColumnDisplayText);
            // 
            // UserMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 563);
            this.Controls.Add(this.devExpressGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserMessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户通知与消息";
            this.Load += new System.EventHandler(this.UserMessageForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AppFramework.WinFormsControls.DevExpressGrid devExpressGrid;
    }
}