namespace Blue.WindowsFormsClient
{
    partial class CommonProgressForm
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
            this.mpbProgress = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.mpbProgress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mpbProgress
            // 
            this.mpbProgress.Dock = System.Windows.Forms.DockStyle.Left;
            this.mpbProgress.EditValue = 0;
            this.mpbProgress.Location = new System.Drawing.Point(0, 0);
            this.mpbProgress.Name = "mpbProgress";
            this.mpbProgress.Properties.MarqueeAnimationSpeed = 30;
            this.mpbProgress.Properties.MarqueeWidth = 30;
            this.mpbProgress.Size = new System.Drawing.Size(376, 28);
            this.mpbProgress.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(384, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 28);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.mpbProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ProgressForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "处理中......";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProgressForm_FormClosed);
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mpbProgress.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl mpbProgress;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}