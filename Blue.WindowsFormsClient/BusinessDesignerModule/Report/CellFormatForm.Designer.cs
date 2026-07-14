namespace Blue.WindowsFormsClient.BusinessDesignerModule
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
            this.gpctrlType = new DevExpress.XtraEditors.GroupControl();
            this.lstCellType = new DevExpress.XtraEditors.ListBoxControl();
            this.gpctrlFormat = new DevExpress.XtraEditors.GroupControl();
            this.lstFormat = new DevExpress.XtraEditors.ListBoxControl();
            this.gpctrlExample = new DevExpress.XtraEditors.GroupControl();
            this.txtExample = new DevExpress.XtraEditors.TextEdit();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.sbtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gpctrlType)).BeginInit();
            this.gpctrlType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstCellType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpctrlFormat)).BeginInit();
            this.gpctrlFormat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpctrlExample)).BeginInit();
            this.gpctrlExample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExample.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpctrlType
            // 
            this.gpctrlType.Controls.Add(this.lstCellType);
            this.gpctrlType.Dock = System.Windows.Forms.DockStyle.Left;
            this.gpctrlType.Location = new System.Drawing.Point(0, 0);
            this.gpctrlType.LookAndFeel.SkinName = "Money Twins";
            this.gpctrlType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gpctrlType.Name = "gpctrlType";
            this.gpctrlType.Size = new System.Drawing.Size(197, 157);
            this.gpctrlType.TabIndex = 0;
            this.gpctrlType.Text = "单元格类型";
            // 
            // lstCellType
            // 
            this.lstCellType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCellType.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.lstCellType.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.lstCellType.Location = new System.Drawing.Point(2, 22);
            this.lstCellType.Name = "lstCellType";
            this.lstCellType.Size = new System.Drawing.Size(193, 133);
            this.lstCellType.TabIndex = 1;
            this.lstCellType.SelectedIndexChanged += new System.EventHandler(this.klstCellType_SelectedIndexChanged);
            // 
            // gpctrlFormat
            // 
            this.gpctrlFormat.Controls.Add(this.lstFormat);
            this.gpctrlFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpctrlFormat.Location = new System.Drawing.Point(197, 0);
            this.gpctrlFormat.LookAndFeel.SkinName = "Money Twins";
            this.gpctrlFormat.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gpctrlFormat.Name = "gpctrlFormat";
            this.gpctrlFormat.Size = new System.Drawing.Size(257, 157);
            this.gpctrlFormat.TabIndex = 1;
            this.gpctrlFormat.Text = "格式";
            // 
            // lstFormat
            // 
            this.lstFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFormat.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.lstFormat.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.lstFormat.Location = new System.Drawing.Point(2, 22);
            this.lstFormat.LookAndFeel.SkinName = "Money Twins";
            this.lstFormat.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lstFormat.Name = "lstFormat";
            this.lstFormat.Size = new System.Drawing.Size(253, 133);
            this.lstFormat.TabIndex = 2;
            this.lstFormat.SelectedIndexChanged += new System.EventHandler(this.lstFormat_SelectedIndexChanged);
            // 
            // gpctrlExample
            // 
            this.gpctrlExample.Controls.Add(this.txtExample);
            this.gpctrlExample.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpctrlExample.Location = new System.Drawing.Point(0, 0);
            this.gpctrlExample.LookAndFeel.SkinName = "Money Twins";
            this.gpctrlExample.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gpctrlExample.Name = "gpctrlExample";
            this.gpctrlExample.Size = new System.Drawing.Size(454, 47);
            this.gpctrlExample.TabIndex = 2;
            this.gpctrlExample.Text = "例子";
            // 
            // txtExample
            // 
            this.txtExample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExample.Location = new System.Drawing.Point(2, 22);
            this.txtExample.Name = "txtExample";
            this.txtExample.Properties.LookAndFeel.SkinName = "Blue";
            this.txtExample.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtExample.Properties.ReadOnly = true;
            this.txtExample.Size = new System.Drawing.Size(450, 20);
            this.txtExample.TabIndex = 0;
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlTop.Controls.Add(this.gpctrlFormat);
            this.pnlTop.Controls.Add(this.gpctrlType);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 47);
            this.pnlTop.LookAndFeel.SkinName = "Money Twins";
            this.pnlTop.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(454, 157);
            this.pnlTop.TabIndex = 3;
            // 
            // sbtnCancel
            // 
            this.sbtnCancel.Location = new System.Drawing.Point(383, 210);
            this.sbtnCancel.LookAndFeel.SkinName = "Money Twins";
            this.sbtnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnCancel.Name = "sbtnCancel";
            this.sbtnCancel.Size = new System.Drawing.Size(64, 23);
            this.sbtnCancel.TabIndex = 21;
            this.sbtnCancel.Text = "取消(&C)";
            this.sbtnCancel.Click += new System.EventHandler(this.sbtnCancel_Click);
            // 
            // sbtnConfirm
            // 
            this.sbtnConfirm.Location = new System.Drawing.Point(312, 210);
            this.sbtnConfirm.LookAndFeel.SkinName = "Money Twins";
            this.sbtnConfirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnConfirm.Name = "sbtnConfirm";
            this.sbtnConfirm.Size = new System.Drawing.Size(64, 23);
            this.sbtnConfirm.TabIndex = 20;
            this.sbtnConfirm.Text = "确定(&O)";
            this.sbtnConfirm.Click += new System.EventHandler(this.sbtnConfirm_Click);
            // 
            // CellFormatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(454, 239);
            this.Controls.Add(this.sbtnCancel);
            this.Controls.Add(this.sbtnConfirm);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.gpctrlExample);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CellFormatForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置单元格数据格式";
            this.Load += new System.EventHandler(this.CellFormatForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gpctrlType)).EndInit();
            this.gpctrlType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstCellType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpctrlFormat)).EndInit();
            this.gpctrlFormat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpctrlExample)).EndInit();
            this.gpctrlExample.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtExample.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gpctrlType;
        private DevExpress.XtraEditors.GroupControl gpctrlFormat;
        private DevExpress.XtraEditors.GroupControl gpctrlExample;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        protected DevExpress.XtraEditors.SimpleButton sbtnCancel;
        protected DevExpress.XtraEditors.SimpleButton sbtnConfirm;
        private DevExpress.XtraEditors.TextEdit txtExample;
        private DevExpress.XtraEditors.ListBoxControl lstCellType;
        private DevExpress.XtraEditors.ListBoxControl lstFormat;
    }
}