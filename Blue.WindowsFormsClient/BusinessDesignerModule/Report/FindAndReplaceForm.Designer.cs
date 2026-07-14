namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class FindAndReplaceForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblReplaceTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.etxtFind = new DevExpress.XtraEditors.TextEdit();
            this.sbtnFindNext = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnReplace = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnReplaceAll = new DevExpress.XtraEditors.SimpleButton();
            this.etxtReplace = new DevExpress.XtraEditors.TextEdit();
            this.cmbeSerachType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkEntireCell = new System.Windows.Forms.CheckBox();
            this.chkMatchCase = new System.Windows.Forms.CheckBox();
            this.lblEntireCell = new System.Windows.Forms.Label();
            this.lblMatchCase = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeSerachType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(8, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(100, 16);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "查找：";
            // 
            // lblReplaceTitle
            // 
            this.lblReplaceTitle.Location = new System.Drawing.Point(8, 47);
            this.lblReplaceTitle.Name = "lblReplaceTitle";
            this.lblReplaceTitle.Size = new System.Drawing.Size(100, 16);
            this.lblReplaceTitle.TabIndex = 9;
            this.lblReplaceTitle.Text = "替换：";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "搜索方式：";
            // 
            // etxtFind
            // 
            this.etxtFind.Location = new System.Drawing.Point(8, 22);
            this.etxtFind.Name = "etxtFind";
            this.etxtFind.Size = new System.Drawing.Size(312, 21);
            this.etxtFind.TabIndex = 20;
            this.etxtFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.etxtFind_KeyPress);
            // 
            // sbtnFindNext
            // 
            this.sbtnFindNext.Location = new System.Drawing.Point(328, 5);
            this.sbtnFindNext.LookAndFeel.SkinName = "Blue";
            this.sbtnFindNext.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnFindNext.Name = "sbtnFindNext";
            this.sbtnFindNext.Size = new System.Drawing.Size(111, 23);
            this.sbtnFindNext.TabIndex = 21;
            this.sbtnFindNext.Text = "查找下一个(&F)";
            this.sbtnFindNext.Click += new System.EventHandler(this.sbtnFindNext_Click);
            // 
            // sbtnClose
            // 
            this.sbtnClose.Location = new System.Drawing.Point(328, 37);
            this.sbtnClose.LookAndFeel.SkinName = "Blue";
            this.sbtnClose.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnClose.Name = "sbtnClose";
            this.sbtnClose.Size = new System.Drawing.Size(111, 23);
            this.sbtnClose.TabIndex = 22;
            this.sbtnClose.Text = "关闭(&C)";
            this.sbtnClose.Click += new System.EventHandler(this.sbtnClose_Click);
            // 
            // sbtnReplace
            // 
            this.sbtnReplace.Location = new System.Drawing.Point(328, 69);
            this.sbtnReplace.LookAndFeel.SkinName = "Blue";
            this.sbtnReplace.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnReplace.Name = "sbtnReplace";
            this.sbtnReplace.Size = new System.Drawing.Size(111, 23);
            this.sbtnReplace.TabIndex = 24;
            this.sbtnReplace.Text = "替换(&R)";
            this.sbtnReplace.Click += new System.EventHandler(this.sbtnReplace_Click);
            // 
            // sbtnReplaceAll
            // 
            this.sbtnReplaceAll.Location = new System.Drawing.Point(328, 101);
            this.sbtnReplaceAll.LookAndFeel.SkinName = "Blue";
            this.sbtnReplaceAll.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnReplaceAll.Name = "sbtnReplaceAll";
            this.sbtnReplaceAll.Size = new System.Drawing.Size(111, 23);
            this.sbtnReplaceAll.TabIndex = 25;
            this.sbtnReplaceAll.Text = "全部替换(&A)";
            this.sbtnReplaceAll.Click += new System.EventHandler(this.sbtnReplaceAll_Click);
            // 
            // etxtReplace
            //
            this.etxtReplace.Location = new System.Drawing.Point(8, 63);
            this.etxtReplace.Name = "etxtReplace";
            this.etxtReplace.Size = new System.Drawing.Size(312, 21);
            this.etxtReplace.TabIndex = 26;
            // 
            // cmbeSerachType
            // 
            this.cmbeSerachType.Location = new System.Drawing.Point(69, 94);
            this.cmbeSerachType.Name = "cmbeSerachType";
            this.cmbeSerachType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeSerachType.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbeSerachType.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbeSerachType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbeSerachType.Size = new System.Drawing.Size(98, 21);
            this.cmbeSerachType.TabIndex = 27;
            // 
            // chkEntireCell
            // 
            this.chkEntireCell.AutoSize = true;
            this.chkEntireCell.BackColor = System.Drawing.Color.Transparent;
            this.chkEntireCell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkEntireCell.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkEntireCell.ForeColor = System.Drawing.Color.DodgerBlue;
            this.chkEntireCell.Location = new System.Drawing.Point(185, 114);
            this.chkEntireCell.Name = "chkEntireCell";
            this.chkEntireCell.Size = new System.Drawing.Size(12, 11);
            this.chkEntireCell.TabIndex = 28;
            this.chkEntireCell.UseVisualStyleBackColor = false;
            this.chkEntireCell.CheckedChanged += new System.EventHandler(this.chkEntireCell_CheckedChanged);
            // 
            // chkMatchCase
            // 
            this.chkMatchCase.AutoSize = true;
            this.chkMatchCase.BackColor = System.Drawing.Color.Transparent;
            this.chkMatchCase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkMatchCase.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkMatchCase.ForeColor = System.Drawing.Color.DodgerBlue;
            this.chkMatchCase.Location = new System.Drawing.Point(185, 93);
            this.chkMatchCase.Name = "chkMatchCase";
            this.chkMatchCase.Size = new System.Drawing.Size(12, 11);
            this.chkMatchCase.TabIndex = 29;
            this.chkMatchCase.UseVisualStyleBackColor = false;
            this.chkMatchCase.CheckedChanged += new System.EventHandler(this.chkMatchCase_CheckedChanged);
            // 
            // lblEntireCell
            // 
            this.lblEntireCell.AutoSize = true;
            this.lblEntireCell.Location = new System.Drawing.Point(202, 113);
            this.lblEntireCell.Name = "lblEntireCell";
            this.lblEntireCell.Size = new System.Drawing.Size(53, 12);
            this.lblEntireCell.TabIndex = 30;
            this.lblEntireCell.Text = "完全匹配";
            // 
            // lblMatchCase
            // 
            this.lblMatchCase.AutoSize = true;
            this.lblMatchCase.Location = new System.Drawing.Point(202, 93);
            this.lblMatchCase.Name = "lblMatchCase";
            this.lblMatchCase.Size = new System.Drawing.Size(65, 12);
            this.lblMatchCase.TabIndex = 31;
            this.lblMatchCase.Text = "区分大小写";
            // 
            // FindAndReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(448, 132);
            this.Controls.Add(this.lblMatchCase);
            this.Controls.Add(this.lblEntireCell);
            this.Controls.Add(this.chkMatchCase);
            this.Controls.Add(this.chkEntireCell);
            this.Controls.Add(this.cmbeSerachType);
            this.Controls.Add(this.etxtReplace);
            this.Controls.Add(this.sbtnReplaceAll);
            this.Controls.Add(this.sbtnReplace);
            this.Controls.Add(this.sbtnClose);
            this.Controls.Add(this.sbtnFindNext);
            this.Controls.Add(this.etxtFind);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblReplaceTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindAndReplaceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找/替换";
            this.Activated += new System.EventHandler(this.FindAndReplaceForm_Activated);
            this.Load += new System.EventHandler(this.FindAndReplaceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbeSerachType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblReplaceTitle;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit etxtFind;
        private DevExpress.XtraEditors.SimpleButton sbtnFindNext;
        private DevExpress.XtraEditors.SimpleButton sbtnClose;
        private DevExpress.XtraEditors.SimpleButton sbtnReplace;
        private DevExpress.XtraEditors.SimpleButton sbtnReplaceAll;
        private DevExpress.XtraEditors.TextEdit etxtReplace;
        private DevExpress.XtraEditors.ComboBoxEdit cmbeSerachType;
        private System.Windows.Forms.CheckBox chkEntireCell;
        private System.Windows.Forms.CheckBox chkMatchCase;
        private System.Windows.Forms.Label lblEntireCell;
        private System.Windows.Forms.Label lblMatchCase;
    }
}