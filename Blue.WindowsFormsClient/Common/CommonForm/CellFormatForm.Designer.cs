namespace Blue.WindowsFormsClient
{
    partial class CellFormatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellFormatForm));
            this.gcExample = new DevExpress.XtraEditors.GroupControl();
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.gcFormat = new DevExpress.XtraEditors.GroupControl();
            this.txtExample = new DevExpress.XtraEditors.TextEdit();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.lstCellType = new DevExpress.XtraEditors.ListBoxControl();
            this.lstFormat = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcExample)).BeginInit();
            this.gcExample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcFormat)).BeginInit();
            this.gcFormat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExample.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstCellType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstFormat)).BeginInit();
            this.SuspendLayout();
            // 
            // gcExample
            // 
            this.gcExample.Controls.Add(this.txtExample);
            this.gcExample.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcExample.Location = new System.Drawing.Point(2, 2);
            this.gcExample.Name = "gcExample";
            this.gcExample.Size = new System.Drawing.Size(535, 53);
            this.gcExample.TabIndex = 0;
            this.gcExample.Text = "例子";
            // 
            // gcMain
            // 
            this.gcMain.Controls.Add(this.lstFormat);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(261, 55);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(276, 162);
            this.gcMain.TabIndex = 1;
            this.gcMain.Text = "单元格类型";
            // 
            // gcFormat
            // 
            this.gcFormat.Controls.Add(this.lstCellType);
            this.gcFormat.Dock = System.Windows.Forms.DockStyle.Left;
            this.gcFormat.Location = new System.Drawing.Point(2, 55);
            this.gcFormat.Name = "gcFormat";
            this.gcFormat.Size = new System.Drawing.Size(259, 162);
            this.gcFormat.TabIndex = 1;
            this.gcFormat.Text = "格式";
            // 
            // txtExample
            // 
            this.txtExample.Location = new System.Drawing.Point(12, 27);
            this.txtExample.Name = "txtExample";
            this.txtExample.Properties.ReadOnly = true;
            this.txtExample.Size = new System.Drawing.Size(515, 20);
            this.txtExample.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gcMain);
            this.pnlMain.Controls.Add(this.gcFormat);
            this.pnlMain.Controls.Add(this.gcExample);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(539, 219);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 219);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(539, 42);
            this.pnlBottom.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(275, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(186, 10);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确认(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lstCellType
            // 
            this.lstCellType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCellType.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.lstCellType.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.lstCellType.Location = new System.Drawing.Point(2, 21);
            this.lstCellType.Name = "lstCellType";
            this.lstCellType.Size = new System.Drawing.Size(255, 139);
            this.lstCellType.TabIndex = 2;
            this.lstCellType.SelectedIndexChanged += new System.EventHandler(this.lstCellType_SelectedIndexChanged);
            // 
            // lstFormat
            // 
            this.lstFormat.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFormat.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.lstFormat.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.lstFormat.Location = new System.Drawing.Point(2, 21);
            this.lstFormat.LookAndFeel.SkinName = "Money Twins";
            this.lstFormat.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lstFormat.Name = "lstFormat";
            this.lstFormat.Size = new System.Drawing.Size(272, 139);
            this.lstFormat.TabIndex = 3;
            this.lstFormat.SelectedIndexChanged += new System.EventHandler(this.lstFormat_SelectedIndexChanged);
            // 
            // CellFormatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 261);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CellFormatForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置单元格数据格式";
            this.Load += new System.EventHandler(this.CellFormatForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcExample)).EndInit();
            this.gcExample.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcFormat)).EndInit();
            this.gcFormat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtExample.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstCellType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstFormat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcExample;
        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraEditors.GroupControl gcFormat;
        private DevExpress.XtraEditors.TextEdit txtExample;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.ListBoxControl lstFormat;
        private DevExpress.XtraEditors.ListBoxControl lstCellType;
    }
}