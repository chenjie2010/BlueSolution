namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class OptionShowForm
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
            this.sbtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkZero = new System.Windows.Forms.CheckBox();
            this.chkRowHeader = new System.Windows.Forms.CheckBox();
            this.chkColHeader = new System.Windows.Forms.CheckBox();
            this.chkGridLine = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // sbtnCancel
            // 
            this.sbtnCancel.Location = new System.Drawing.Point(101, 95);
            this.sbtnCancel.LookAndFeel.SkinName = "Money Twins";
            this.sbtnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnCancel.Name = "sbtnCancel";
            this.sbtnCancel.Size = new System.Drawing.Size(64, 23);
            this.sbtnCancel.TabIndex = 19;
            this.sbtnCancel.Text = "取消(&C)";
            this.sbtnCancel.Click += new System.EventHandler(this.sbtnCancel_Click);
            // 
            // sbtnConfirm
            // 
            this.sbtnConfirm.Location = new System.Drawing.Point(30, 95);
            this.sbtnConfirm.LookAndFeel.SkinName = "Money Twins";
            this.sbtnConfirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnConfirm.Name = "sbtnConfirm";
            this.sbtnConfirm.Size = new System.Drawing.Size(64, 23);
            this.sbtnConfirm.TabIndex = 18;
            this.sbtnConfirm.Text = "确定(&O)";
            this.sbtnConfirm.Click += new System.EventHandler(this.sbtnConfirm_Click);
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.label7);
            this.groupControl.Controls.Add(this.label3);
            this.groupControl.Controls.Add(this.label2);
            this.groupControl.Controls.Add(this.label1);
            this.groupControl.Controls.Add(this.chkZero);
            this.groupControl.Controls.Add(this.chkRowHeader);
            this.groupControl.Controls.Add(this.chkColHeader);
            this.groupControl.Controls.Add(this.chkGridLine);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl.Location = new System.Drawing.Point(0, 0);
            this.groupControl.LookAndFeel.SkinName = "Money Twins";
            this.groupControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(205, 87);
            this.groupControl.TabIndex = 20;
            this.groupControl.Text = "选项";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "行表头";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "列表头";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "显示零";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "网格线";
            // 
            // chkZero
            // 
            this.chkZero.AutoSize = true;
            this.chkZero.BackColor = System.Drawing.Color.Transparent;
            this.chkZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkZero.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkZero.ForeColor = System.Drawing.Color.DodgerBlue;
            this.chkZero.Location = new System.Drawing.Point(98, 38);
            this.chkZero.Name = "chkZero";
            this.chkZero.Size = new System.Drawing.Size(12, 11);
            this.chkZero.TabIndex = 7;
            this.chkZero.UseVisualStyleBackColor = false;
            this.chkZero.CheckedChanged += new System.EventHandler(this.chkZero_CheckedChanged);
            // 
            // chkRowHeader
            // 
            this.chkRowHeader.AutoSize = true;
            this.chkRowHeader.BackColor = System.Drawing.Color.Transparent;
            this.chkRowHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRowHeader.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkRowHeader.ForeColor = System.Drawing.Color.DodgerBlue;
            this.chkRowHeader.Location = new System.Drawing.Point(23, 64);
            this.chkRowHeader.Name = "chkRowHeader";
            this.chkRowHeader.Size = new System.Drawing.Size(12, 11);
            this.chkRowHeader.TabIndex = 6;
            this.chkRowHeader.UseVisualStyleBackColor = false;
            this.chkRowHeader.CheckedChanged += new System.EventHandler(this.chkRowHeader_CheckedChanged);
            // 
            // chkColHeader
            // 
            this.chkColHeader.AutoSize = true;
            this.chkColHeader.BackColor = System.Drawing.Color.Transparent;
            this.chkColHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkColHeader.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkColHeader.ForeColor = System.Drawing.Color.DodgerBlue;
            this.chkColHeader.Location = new System.Drawing.Point(23, 39);
            this.chkColHeader.Name = "chkColHeader";
            this.chkColHeader.Size = new System.Drawing.Size(12, 11);
            this.chkColHeader.TabIndex = 5;
            this.chkColHeader.UseVisualStyleBackColor = false;
            this.chkColHeader.CheckedChanged += new System.EventHandler(this.chkColHeader_CheckedChanged);
            // 
            // chkGridLine
            // 
            this.chkGridLine.AutoSize = true;
            this.chkGridLine.BackColor = System.Drawing.Color.Transparent;
            this.chkGridLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkGridLine.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkGridLine.ForeColor = System.Drawing.Color.DodgerBlue;
            this.chkGridLine.Location = new System.Drawing.Point(97, 63);
            this.chkGridLine.Name = "chkGridLine";
            this.chkGridLine.Size = new System.Drawing.Size(12, 11);
            this.chkGridLine.TabIndex = 4;
            this.chkGridLine.UseVisualStyleBackColor = false;
            this.chkGridLine.CheckedChanged += new System.EventHandler(this.chkGridLine_CheckedChanged);
            // 
            // OptionShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(205, 125);
            this.Controls.Add(this.groupControl);
            this.Controls.Add(this.sbtnCancel);
            this.Controls.Add(this.sbtnConfirm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionShowForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "显示选择项";
            this.Load += new System.EventHandler(this.OptionShowForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            this.groupControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.SimpleButton sbtnCancel;
        protected DevExpress.XtraEditors.SimpleButton sbtnConfirm;
        private DevExpress.XtraEditors.GroupControl groupControl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkZero;
        private System.Windows.Forms.CheckBox chkRowHeader;
        private System.Windows.Forms.CheckBox chkColHeader;
        private System.Windows.Forms.CheckBox chkGridLine;
    }
}