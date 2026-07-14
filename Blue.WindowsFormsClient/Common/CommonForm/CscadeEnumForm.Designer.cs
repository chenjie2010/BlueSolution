namespace Blue.WindowsFormsClient.Common
{
    partial class CscadeEnumForm
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.cmbFourth = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbThird = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbSecond = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbFirst = new DevExpress.XtraEditors.ComboBoxEdit();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.meToolTip = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFourth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbThird.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSecond.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFirst.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meToolTip.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.meToolTip);
            this.panelControl1.Controls.Add(this.lblName);
            this.panelControl1.Controls.Add(this.btnRemove);
            this.panelControl1.Controls.Add(this.btnConfirm);
            this.panelControl1.Controls.Add(this.cmbFourth);
            this.panelControl1.Controls.Add(this.cmbThird);
            this.panelControl1.Controls.Add(this.cmbSecond);
            this.panelControl1.Controls.Add(this.cmbFirst);
            this.panelControl1.Controls.Add(this.separatorControl2);
            this.panelControl1.Controls.Add(this.separatorControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(567, 109);
            this.panelControl1.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(8, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(48, 14);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "选项值：";
            // 
            // btnRemove
            // 
            this.btnRemove.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.btnRemove.Location = new System.Drawing.Point(289, 79);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "清除(&R)";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(203, 79);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "确定(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // cmbFourth
            // 
            this.cmbFourth.Location = new System.Drawing.Point(428, 44);
            this.cmbFourth.Name = "cmbFourth";
            this.cmbFourth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFourth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFourth.Size = new System.Drawing.Size(135, 20);
            this.cmbFourth.TabIndex = 4;
            this.cmbFourth.SelectedIndexChanged += new System.EventHandler(this.cmbFourth_SelectedIndexChanged);
            // 
            // cmbThird
            // 
            this.cmbThird.Location = new System.Drawing.Point(287, 44);
            this.cmbThird.Name = "cmbThird";
            this.cmbThird.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbThird.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbThird.Size = new System.Drawing.Size(135, 20);
            this.cmbThird.TabIndex = 3;
            this.cmbThird.SelectedIndexChanged += new System.EventHandler(this.cmbThird_SelectedIndexChanged);
            // 
            // cmbSecond
            // 
            this.cmbSecond.Location = new System.Drawing.Point(146, 44);
            this.cmbSecond.Name = "cmbSecond";
            this.cmbSecond.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSecond.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSecond.Size = new System.Drawing.Size(135, 20);
            this.cmbSecond.TabIndex = 2;
            this.cmbSecond.SelectedIndexChanged += new System.EventHandler(this.cmbSecond_SelectedIndexChanged);
            // 
            // cmbFirst
            // 
            this.cmbFirst.Location = new System.Drawing.Point(5, 44);
            this.cmbFirst.Name = "cmbFirst";
            this.cmbFirst.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFirst.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFirst.Size = new System.Drawing.Size(135, 20);
            this.cmbFirst.TabIndex = 1;
            this.cmbFirst.SelectedIndexChanged += new System.EventHandler(this.cmbFirst_SelectedIndexChanged);
            // 
            // separatorControl2
            // 
            this.separatorControl2.Location = new System.Drawing.Point(3, 59);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Size = new System.Drawing.Size(560, 23);
            this.separatorControl2.TabIndex = 5;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(2, 24);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(560, 23);
            this.separatorControl1.TabIndex = 9;
            // 
            // meToolTip
            // 
            this.meToolTip.EditValue = "";
            this.meToolTip.Location = new System.Drawing.Point(58, 9);
            this.meToolTip.Name = "meToolTip";
            this.meToolTip.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.meToolTip.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.meToolTip.Properties.Appearance.Options.UseBackColor = true;
            this.meToolTip.Properties.Appearance.Options.UseForeColor = true;
            this.meToolTip.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.meToolTip.Properties.ReadOnly = true;
            this.meToolTip.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.meToolTip.Size = new System.Drawing.Size(497, 20);
            this.meToolTip.TabIndex = 0;
            // 
            // CscadeEnumForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 109);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "CscadeEnumForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "级联枚举";
            this.Load += new System.EventHandler(this.CscadeEnumForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFourth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbThird.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSecond.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFirst.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meToolTip.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFourth;
        private DevExpress.XtraEditors.ComboBoxEdit cmbThird;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSecond;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFirst;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.MemoEdit meToolTip;
    }
}