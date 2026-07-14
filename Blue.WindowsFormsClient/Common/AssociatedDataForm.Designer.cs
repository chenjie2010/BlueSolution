namespace Blue.WindowsFormsClient.Common
{
    partial class AssociatedDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssociatedDataForm));
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.hlnkClear = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.scCondition = new DevExpress.XtraEditors.SearchControl();
            this.grdAssociation = new AppFramework.WinFormsControls.DevExpressGrid();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scCondition.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.hlnkClear);
            this.pnlMain.Controls.Add(this.progressPanel);
            this.pnlMain.Controls.Add(this.scCondition);
            this.pnlMain.Controls.Add(this.grdAssociation);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnConfirm);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(665, 369);
            this.pnlMain.TabIndex = 1;
            // 
            // hlnkClear
            // 
            this.hlnkClear.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.hlnkClear.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkClear.Appearance.ImageIndex = 4;
            this.hlnkClear.Appearance.Options.UseImage = true;
            this.hlnkClear.Appearance.Options.UseImageAlign = true;
            this.hlnkClear.Appearance.Options.UseImageIndex = true;
            this.hlnkClear.Appearance.Options.UseImageList = true;
            this.hlnkClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkClear.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkClear.Location = new System.Drawing.Point(12, 342);
            this.hlnkClear.Name = "hlnkClear";
            this.hlnkClear.Size = new System.Drawing.Size(69, 20);
            this.hlnkClear.TabIndex = 11;
            this.hlnkClear.Text = "清除数据";
            this.hlnkClear.Click += new System.EventHandler(this.hlnkClear_Click);
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "数据正在加载......";
            this.progressPanel.Location = new System.Drawing.Point(260, 130);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(150, 50);
            this.progressPanel.TabIndex = 10;
            this.progressPanel.Text = "数据加载中......";
            this.progressPanel.Visible = false;
            // 
            // scCondition
            // 
            this.scCondition.Location = new System.Drawing.Point(5, 12);
            this.scCondition.Name = "scCondition";
            this.scCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.scCondition.Size = new System.Drawing.Size(652, 20);
            this.scCondition.TabIndex = 0;
            this.scCondition.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.scCondition_ButtonClick);
            this.scCondition.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.scCondition_ButtonPressed);
            this.scCondition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scCondition_KeyPress);
            // 
            // grdAssociation
            // 
            this.grdAssociation.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdAssociation.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.grdAssociation.CheckboxColumnCaption = "选择项";
            this.grdAssociation.ColumnHeaderTexts = new string[0];
            this.grdAssociation.DataKeyNames = new string[0];
            this.grdAssociation.ExportedExcel = false;
            this.grdAssociation.FootText = null;
            this.grdAssociation.IsMainTable = false;
            this.grdAssociation.IsShowCheckBox = true;
            this.grdAssociation.Location = new System.Drawing.Point(5, 40);
            this.grdAssociation.Name = "grdAssociation";
            this.grdAssociation.PageSize = 50;
            this.grdAssociation.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grdAssociation.Size = new System.Drawing.Size(652, 289);
            this.grdAssociation.TabIndex = 8;
            this.grdAssociation.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.grdAssociation_OnPageIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(578, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(494, 338);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确认(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // AssociatedDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 369);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AssociatedDataForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关联数据";
            this.Load += new System.EventHandler(this.UserListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scCondition.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.SearchControl scCondition;
        private AppFramework.WinFormsControls.DevExpressGrid grdAssociation;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkClear;
    }
}