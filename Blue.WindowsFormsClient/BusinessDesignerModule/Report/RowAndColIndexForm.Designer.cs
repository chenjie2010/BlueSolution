namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class RowAndColIndexForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblRowTip = new System.Windows.Forms.Label();
            this.lblColTip = new System.Windows.Forms.Label();
            this.sbtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.etxtRow = new DevExpress.XtraEditors.TextEdit();
            this.etxtCol = new DevExpress.XtraEditors.TextEdit();
            this.SuspendLayout();
            // 
            // lblRowTip
            // 
            this.lblRowTip.Location = new System.Drawing.Point(9, 14);
            this.lblRowTip.Name = "lblRowTip";
            this.lblRowTip.Size = new System.Drawing.Size(66, 14);
            this.lblRowTip.TabIndex = 3;
            this.lblRowTip.Text = "行数：";
            this.lblRowTip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblColTip
            // 
            this.lblColTip.Location = new System.Drawing.Point(9, 42);
            this.lblColTip.Name = "lblColTip";
            this.lblColTip.Size = new System.Drawing.Size(66, 14);
            this.lblColTip.TabIndex = 2;
            this.lblColTip.Text = "列数：";
            this.lblColTip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sbtnCancel
            // 
            this.sbtnCancel.Location = new System.Drawing.Point(99, 75);
            this.sbtnCancel.LookAndFeel.SkinName = "Money Twins";
            this.sbtnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnCancel.Name = "sbtnCancel";
            this.sbtnCancel.Size = new System.Drawing.Size(64, 23);
            this.sbtnCancel.TabIndex = 17;
            this.sbtnCancel.Text = "取消(&C)";
            this.sbtnCancel.Click += new System.EventHandler(this.sbtnCancel_Click);
            // 
            // sbtnConfirm
            // 
            this.sbtnConfirm.Location = new System.Drawing.Point(28, 75);
            this.sbtnConfirm.LookAndFeel.SkinName = "Money Twins";
            this.sbtnConfirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnConfirm.Name = "sbtnConfirm";
            this.sbtnConfirm.Size = new System.Drawing.Size(64, 23);
            this.sbtnConfirm.TabIndex = 16;
            this.sbtnConfirm.Text = "确定(&O)";
            this.sbtnConfirm.Click += new System.EventHandler(this.sbtnConfirm_Click);
            // 
            // etxtRow
            //
            this.etxtRow.Location = new System.Drawing.Point(75, 10);
            this.etxtRow.Name = "etxtRow";
            this.etxtRow.Size = new System.Drawing.Size(100, 21);
            this.etxtRow.TabIndex = 18;
            // 
            // etxtCol
            //
            this.etxtCol.Location = new System.Drawing.Point(75, 40);
            this.etxtCol.Name = "etxtCol";
            this.etxtCol.Size = new System.Drawing.Size(100, 21);
            this.etxtCol.TabIndex = 19;
            // 
            // RowAndColIndexForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(195, 107);
            this.Controls.Add(this.etxtCol);
            this.Controls.Add(this.etxtRow);
            this.Controls.Add(this.sbtnCancel);
            this.Controls.Add(this.sbtnConfirm);
            this.Controls.Add(this.lblRowTip);
            this.Controls.Add(this.lblColTip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RowAndColIndexForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "最大行列/固定行列";
            this.Load += new System.EventHandler(this.MaxOrFixRowColForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRowTip;
        private System.Windows.Forms.Label lblColTip;
        protected DevExpress.XtraEditors.SimpleButton sbtnCancel;
        protected DevExpress.XtraEditors.SimpleButton sbtnConfirm;
        private DevExpress.XtraEditors.TextEdit etxtRow;
        private DevExpress.XtraEditors.TextEdit etxtCol;
    }
}