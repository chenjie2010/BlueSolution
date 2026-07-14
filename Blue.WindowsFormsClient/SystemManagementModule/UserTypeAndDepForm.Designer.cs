namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class UserTypeAndDepForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserTypeAndDepForm));
            this.gcPanel = new DevExpress.XtraEditors.GroupControl();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.btxtDepartmentRange = new DevExpress.XtraEditors.ButtonEdit();
            this.btxtUserTypeRange = new DevExpress.XtraEditors.ButtonEdit();
            this.lblDepartmentRange = new DevExpress.XtraEditors.LabelControl();
            this.lblUserTypeRange = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcPanel)).BeginInit();
            this.gcPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btxtDepartmentRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtUserTypeRange.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcPanel
            // 
            this.gcPanel.Controls.Add(this.btxtDepartmentRange);
            this.gcPanel.Controls.Add(this.btxtUserTypeRange);
            this.gcPanel.Controls.Add(this.lblDepartmentRange);
            this.gcPanel.Controls.Add(this.lblUserTypeRange);
            this.gcPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPanel.Location = new System.Drawing.Point(0, 0);
            this.gcPanel.Name = "gcPanel";
            this.gcPanel.Size = new System.Drawing.Size(537, 110);
            this.gcPanel.TabIndex = 0;
            this.gcPanel.Text = "请选择用户类型和单位";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 110);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(537, 40);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(265, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(180, 8);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确认(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btxtDepartmentRange
            // 
            this.btxtDepartmentRange.Location = new System.Drawing.Point(105, 73);
            this.btxtDepartmentRange.Name = "btxtDepartmentRange";
            this.btxtDepartmentRange.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtDepartmentRange.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtDepartmentRange.Size = new System.Drawing.Size(415, 20);
            this.btxtDepartmentRange.TabIndex = 40;
            this.btxtDepartmentRange.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtDepartmentRange_ButtonPressed);
            // 
            // btxtUserTypeRange
            // 
            this.btxtUserTypeRange.Location = new System.Drawing.Point(105, 37);
            this.btxtUserTypeRange.Name = "btxtUserTypeRange";
            this.btxtUserTypeRange.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtUserTypeRange.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtUserTypeRange.Size = new System.Drawing.Size(415, 20);
            this.btxtUserTypeRange.TabIndex = 39;
            this.btxtUserTypeRange.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtUserTypeRange_ButtonPressed);
            // 
            // lblDepartmentRange
            // 
            this.lblDepartmentRange.Location = new System.Drawing.Point(15, 75);
            this.lblDepartmentRange.Name = "lblDepartmentRange";
            this.lblDepartmentRange.Size = new System.Drawing.Size(84, 14);
            this.lblDepartmentRange.TabIndex = 42;
            this.lblDepartmentRange.Text = "用户单位范围：";
            // 
            // lblUserTypeRange
            // 
            this.lblUserTypeRange.Location = new System.Drawing.Point(15, 40);
            this.lblUserTypeRange.Name = "lblUserTypeRange";
            this.lblUserTypeRange.Size = new System.Drawing.Size(84, 14);
            this.lblUserTypeRange.TabIndex = 41;
            this.lblUserTypeRange.Text = "用户类型范围：";
            // 
            // UserTypeAndDepForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 150);
            this.Controls.Add(this.gcPanel);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserTypeAndDepForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户类型和单位";
            this.Load += new System.EventHandler(this.UserTypeAndDepForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcPanel)).EndInit();
            this.gcPanel.ResumeLayout(false);
            this.gcPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btxtDepartmentRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtUserTypeRange.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcPanel;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.ButtonEdit btxtDepartmentRange;
        private DevExpress.XtraEditors.ButtonEdit btxtUserTypeRange;
        private DevExpress.XtraEditors.LabelControl lblDepartmentRange;
        private DevExpress.XtraEditors.LabelControl lblUserTypeRange;
    }
}