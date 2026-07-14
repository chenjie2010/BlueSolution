namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class RowAndColSettingForm
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
            this.nudRowOrCol = new System.Windows.Forms.NumericUpDown();
            this.lblTip = new System.Windows.Forms.Label();
            this.sbtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.nudRowOrCol)).BeginInit();
            this.SuspendLayout();
            // 
            // nudRowOrCol
            // 
            this.nudRowOrCol.DecimalPlaces = 1;
            this.nudRowOrCol.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudRowOrCol.Location = new System.Drawing.Point(113, 8);
            this.nudRowOrCol.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudRowOrCol.Name = "nudRowOrCol";
            this.nudRowOrCol.Size = new System.Drawing.Size(96, 21);
            this.nudRowOrCol.TabIndex = 10;
            this.nudRowOrCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(24, 11);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(89, 12);
            this.lblTip.TabIndex = 11;
            this.lblTip.Text = "设置具体行高：";
            this.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sbtnCancel
            // 
            this.sbtnCancel.Location = new System.Drawing.Point(116, 42);
            this.sbtnCancel.LookAndFeel.SkinName = "Money Twins";
            this.sbtnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnCancel.Name = "sbtnCancel";
            this.sbtnCancel.Size = new System.Drawing.Size(64, 23);
            this.sbtnCancel.TabIndex = 15;
            this.sbtnCancel.Text = "取消(&C)";
            this.sbtnCancel.Click += new System.EventHandler(this.sbtnCancel_Click);
            // 
            // sbtnConfirm
            // 
            this.sbtnConfirm.Location = new System.Drawing.Point(45, 42);
            this.sbtnConfirm.LookAndFeel.SkinName = "Money Twins";
            this.sbtnConfirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnConfirm.Name = "sbtnConfirm";
            this.sbtnConfirm.Size = new System.Drawing.Size(64, 23);
            this.sbtnConfirm.TabIndex = 14;
            this.sbtnConfirm.Text = "确定(&O)";
            this.sbtnConfirm.Click += new System.EventHandler(this.sbtnConfirm_Click);
            // 
            // RowAndColSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(236, 76);
            this.Controls.Add(this.sbtnCancel);
            this.Controls.Add(this.sbtnConfirm);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.nudRowOrCol);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RowAndColSettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置行/列高";
            this.Load += new System.EventHandler(this.RowColSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRowOrCol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudRowOrCol;
        private System.Windows.Forms.Label lblTip;
        protected DevExpress.XtraEditors.SimpleButton sbtnCancel;
        protected DevExpress.XtraEditors.SimpleButton sbtnConfirm;
    }
}