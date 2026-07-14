namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class DepartmentModule
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepartmentModule));
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.txtAddFstCode = new DevExpress.XtraEditors.TextEdit();
            this.lblAddFstCode = new DevExpress.XtraEditors.LabelControl();
            this.txtAddScdCode = new DevExpress.XtraEditors.TextEdit();
            this.lblAddScdCode = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.lblCode = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameTip = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblCodeTip = new DevExpress.XtraEditors.LabelControl();
            this.lblDepartmentPorperty = new DevExpress.XtraEditors.LabelControl();
            this.icmbDepartmentPorperty = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icDepartmentPorperty = new DevExpress.Utils.ImageCollection(this.components);
            this.lblDepartmentPorpertyTip = new DevExpress.XtraEditors.LabelControl();
            this.txtDepValue = new DevExpress.XtraEditors.TextEdit();
            this.lblDepValue = new DevExpress.XtraEditors.LabelControl();
            this.lblIsSystemUserType = new DevExpress.XtraEditors.LabelControl();
            this.chkIsSystem = new DevExpress.XtraEditors.CheckEdit();
            this.lblVisibleForInterface = new DevExpress.XtraEditors.LabelControl();
            this.chkVisibleForInterface = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddFstCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddScdCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDepartmentPorperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDepartmentPorperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsSystem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVisibleForInterface.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(35, 270);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 21;
            this.lblNotes.Text = "备注：";
            // 
            // txtAddFstCode
            // 
            this.txtAddFstCode.Location = new System.Drawing.Point(77, 148);
            this.txtAddFstCode.Name = "txtAddFstCode";
            this.txtAddFstCode.Properties.MaxLength = 64;
            this.txtAddFstCode.Size = new System.Drawing.Size(281, 20);
            this.txtAddFstCode.TabIndex = 5;
            // 
            // lblAddFstCode
            // 
            this.lblAddFstCode.Location = new System.Drawing.Point(11, 150);
            this.lblAddFstCode.Name = "lblAddFstCode";
            this.lblAddFstCode.Size = new System.Drawing.Size(60, 14);
            this.lblAddFstCode.TabIndex = 19;
            this.lblAddFstCode.Text = "附加值一：";
            // 
            // txtAddScdCode
            // 
            this.txtAddScdCode.Location = new System.Drawing.Point(77, 184);
            this.txtAddScdCode.Name = "txtAddScdCode";
            this.txtAddScdCode.Properties.MaxLength = 64;
            this.txtAddScdCode.Size = new System.Drawing.Size(281, 20);
            this.txtAddScdCode.TabIndex = 6;
            // 
            // lblAddScdCode
            // 
            this.lblAddScdCode.Location = new System.Drawing.Point(11, 186);
            this.lblAddScdCode.Name = "lblAddScdCode";
            this.lblAddScdCode.Size = new System.Drawing.Size(60, 14);
            this.lblAddScdCode.TabIndex = 17;
            this.lblAddScdCode.Text = "附加值二：";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(77, 50);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 64;
            this.txtCode.Properties.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(281, 20);
            this.txtCode.TabIndex = 2;
            // 
            // lblCode
            // 
            this.lblCode.Location = new System.Drawing.Point(11, 53);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(60, 14);
            this.lblCode.TabIndex = 13;
            this.lblCode.Text = "单位编码：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(77, 16);
            this.txtName.Name = "txtName";
            this.txtName.Properties.MaxLength = 64;
            this.txtName.Size = new System.Drawing.Size(282, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(11, 18);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(60, 14);
            this.lblName.TabIndex = 15;
            this.lblName.Text = "单位名称：";
            // 
            // lblNameTip
            // 
            this.lblNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameTip.Appearance.Options.UseForeColor = true;
            this.lblNameTip.Location = new System.Drawing.Point(364, 19);
            this.lblNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameTip.Name = "lblNameTip";
            this.lblNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblNameTip.TabIndex = 22;
            this.lblNameTip.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(77, 268);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(280, 71);
            this.txtNotes.TabIndex = 9;
            // 
            // lblCodeTip
            // 
            this.lblCodeTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblCodeTip.Appearance.Options.UseForeColor = true;
            this.lblCodeTip.Location = new System.Drawing.Point(364, 55);
            this.lblCodeTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblCodeTip.Name = "lblCodeTip";
            this.lblCodeTip.Size = new System.Drawing.Size(7, 14);
            this.lblCodeTip.TabIndex = 24;
            this.lblCodeTip.Text = "*";
            // 
            // lblDepartmentPorperty
            // 
            this.lblDepartmentPorperty.Location = new System.Drawing.Point(11, 115);
            this.lblDepartmentPorperty.Name = "lblDepartmentPorperty";
            this.lblDepartmentPorperty.Size = new System.Drawing.Size(60, 14);
            this.lblDepartmentPorperty.TabIndex = 25;
            this.lblDepartmentPorperty.Text = "单位性质：";
            // 
            // icmbDepartmentPorperty
            // 
            this.icmbDepartmentPorperty.Location = new System.Drawing.Point(77, 112);
            this.icmbDepartmentPorperty.Name = "icmbDepartmentPorperty";
            this.icmbDepartmentPorperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDepartmentPorperty.Properties.SmallImages = this.icDepartmentPorperty;
            this.icmbDepartmentPorperty.Size = new System.Drawing.Size(281, 20);
            this.icmbDepartmentPorperty.TabIndex = 4;
            // 
            // icDepartmentPorperty
            // 
            this.icDepartmentPorperty.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDepartmentPorperty.ImageStream")));
            this.icDepartmentPorperty.Images.SetKeyName(0, "Common_Drodownlist_One.png");
            this.icDepartmentPorperty.Images.SetKeyName(1, "Common_Drodownlist_Two.png");
            this.icDepartmentPorperty.Images.SetKeyName(2, "Common_Drodownlist_Three.png");
            this.icDepartmentPorperty.Images.SetKeyName(3, "Common_Drodownlist_Four.png");
            this.icDepartmentPorperty.Images.SetKeyName(4, "Common_Drodownlist_Five.png");
            this.icDepartmentPorperty.Images.SetKeyName(5, "Common_Drodownlist_Six.png");
            this.icDepartmentPorperty.Images.SetKeyName(6, "Common_Drodownlist_Seven.png");
            this.icDepartmentPorperty.Images.SetKeyName(7, "Common_Drodownlist_Eight.png");
            this.icDepartmentPorperty.Images.SetKeyName(8, "Common_Drodownlist_Nine.png");
            this.icDepartmentPorperty.Images.SetKeyName(9, "Common_Drodownlist_Ten.png");
            // 
            // lblDepartmentPorpertyTip
            // 
            this.lblDepartmentPorpertyTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblDepartmentPorpertyTip.Appearance.Options.UseForeColor = true;
            this.lblDepartmentPorpertyTip.Location = new System.Drawing.Point(364, 116);
            this.lblDepartmentPorpertyTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblDepartmentPorpertyTip.Name = "lblDepartmentPorpertyTip";
            this.lblDepartmentPorpertyTip.Size = new System.Drawing.Size(7, 14);
            this.lblDepartmentPorpertyTip.TabIndex = 27;
            this.lblDepartmentPorpertyTip.Text = "*";
            // 
            // txtDepValue
            // 
            this.txtDepValue.Location = new System.Drawing.Point(79, 81);
            this.txtDepValue.Name = "txtDepValue";
            this.txtDepValue.Properties.MaxLength = 64;
            this.txtDepValue.Size = new System.Drawing.Size(281, 20);
            this.txtDepValue.TabIndex = 3;
            // 
            // lblDepValue
            // 
            this.lblDepValue.Location = new System.Drawing.Point(13, 84);
            this.lblDepValue.Name = "lblDepValue";
            this.lblDepValue.Size = new System.Drawing.Size(48, 14);
            this.lblDepValue.TabIndex = 29;
            this.lblDepValue.Text = "单位值：";
            // 
            // lblIsSystemUserType
            // 
            this.lblIsSystemUserType.Location = new System.Drawing.Point(13, 216);
            this.lblIsSystemUserType.Name = "lblIsSystemUserType";
            this.lblIsSystemUserType.Size = new System.Drawing.Size(60, 14);
            this.lblIsSystemUserType.TabIndex = 107;
            this.lblIsSystemUserType.Text = "系统类型：";
            // 
            // chkIsSystem
            // 
            this.chkIsSystem.Location = new System.Drawing.Point(79, 214);
            this.chkIsSystem.Name = "chkIsSystem";
            this.chkIsSystem.Properties.Caption = "";
            this.chkIsSystem.Size = new System.Drawing.Size(20, 19);
            this.chkIsSystem.TabIndex = 7;
            // 
            // lblVisibleForInterface
            // 
            this.lblVisibleForInterface.Location = new System.Drawing.Point(13, 242);
            this.lblVisibleForInterface.Name = "lblVisibleForInterface";
            this.lblVisibleForInterface.Size = new System.Drawing.Size(60, 14);
            this.lblVisibleForInterface.TabIndex = 109;
            this.lblVisibleForInterface.Text = "接口可见：";
            // 
            // chkVisibleForInterface
            // 
            this.chkVisibleForInterface.Location = new System.Drawing.Point(79, 240);
            this.chkVisibleForInterface.Name = "chkVisibleForInterface";
            this.chkVisibleForInterface.Properties.Caption = "";
            this.chkVisibleForInterface.Size = new System.Drawing.Size(20, 19);
            this.chkVisibleForInterface.TabIndex = 8;
            // 
            // DepartmentModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblVisibleForInterface);
            this.Controls.Add(this.chkVisibleForInterface);
            this.Controls.Add(this.lblIsSystemUserType);
            this.Controls.Add(this.chkIsSystem);
            this.Controls.Add(this.txtDepValue);
            this.Controls.Add(this.lblDepValue);
            this.Controls.Add(this.lblDepartmentPorpertyTip);
            this.Controls.Add(this.icmbDepartmentPorperty);
            this.Controls.Add(this.lblDepartmentPorperty);
            this.Controls.Add(this.lblCodeTip);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNameTip);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtAddFstCode);
            this.Controls.Add(this.lblAddFstCode);
            this.Controls.Add(this.txtAddScdCode);
            this.Controls.Add(this.lblAddScdCode);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Name = "DepartmentModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.DepartmentModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtAddFstCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddScdCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDepartmentPorperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDepartmentPorperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsSystem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVisibleForInterface.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.TextEdit txtAddFstCode;
        private DevExpress.XtraEditors.LabelControl lblAddFstCode;
        private DevExpress.XtraEditors.TextEdit txtAddScdCode;
        private DevExpress.XtraEditors.LabelControl lblAddScdCode;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl lblCode;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblNameTip;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblCodeTip;
        private DevExpress.XtraEditors.LabelControl lblDepartmentPorperty;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDepartmentPorperty;
        private DevExpress.XtraEditors.LabelControl lblDepartmentPorpertyTip;
        private DevExpress.Utils.ImageCollection icDepartmentPorperty;
        private DevExpress.XtraEditors.TextEdit txtDepValue;
        private DevExpress.XtraEditors.LabelControl lblDepValue;
        private DevExpress.XtraEditors.LabelControl lblIsSystemUserType;
        private DevExpress.XtraEditors.CheckEdit chkIsSystem;
        private DevExpress.XtraEditors.LabelControl lblVisibleForInterface;
        private DevExpress.XtraEditors.CheckEdit chkVisibleForInterface;
    }
}
