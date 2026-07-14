namespace Blue.WindowsFormsClient.MyQueryModule
{
    partial class QueryStatementModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryStatementModule));
            this.txtQueryCode = new DevExpress.XtraEditors.TextEdit();
            this.lblQueryCode = new DevExpress.XtraEditors.LabelControl();
            this.txtQueryame = new DevExpress.XtraEditors.TextEdit();
            this.lblQueryName = new DevExpress.XtraEditors.LabelControl();
            this.lblNameRequired = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.lnkDetailedView = new DevExpress.XtraEditors.HyperLinkEdit();
            this.lblAuditorRequired = new DevExpress.XtraEditors.LabelControl();
            this.lblRecommendType = new DevExpress.XtraEditors.LabelControl();
            this.icmbRecommendType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icRecommendType = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryame.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbRecommendType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icRecommendType)).BeginInit();
            this.SuspendLayout();
            // 
            // txtQueryCode
            // 
            this.txtQueryCode.Location = new System.Drawing.Point(80, 41);
            this.txtQueryCode.Name = "txtQueryCode";
            this.txtQueryCode.Properties.MaxLength = 32;
            this.txtQueryCode.Properties.ReadOnly = true;
            this.txtQueryCode.Size = new System.Drawing.Size(282, 20);
            this.txtQueryCode.TabIndex = 2;
            // 
            // lblQueryCode
            // 
            this.lblQueryCode.Location = new System.Drawing.Point(13, 43);
            this.lblQueryCode.Name = "lblQueryCode";
            this.lblQueryCode.Size = new System.Drawing.Size(60, 14);
            this.lblQueryCode.TabIndex = 13;
            this.lblQueryCode.Text = "查询编码：";
            // 
            // txtQueryame
            // 
            this.txtQueryame.Location = new System.Drawing.Point(80, 10);
            this.txtQueryame.Name = "txtQueryame";
            this.txtQueryame.Properties.MaxLength = 64;
            this.txtQueryame.Size = new System.Drawing.Size(282, 20);
            this.txtQueryame.TabIndex = 1;
            // 
            // lblQueryName
            // 
            this.lblQueryName.Location = new System.Drawing.Point(13, 12);
            this.lblQueryName.Name = "lblQueryName";
            this.lblQueryName.Size = new System.Drawing.Size(60, 14);
            this.lblQueryName.TabIndex = 15;
            this.lblQueryName.Text = "查询名称：";
            // 
            // lblNameRequired
            // 
            this.lblNameRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameRequired.Appearance.Options.UseForeColor = true;
            this.lblNameRequired.Location = new System.Drawing.Point(367, 14);
            this.lblNameRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameRequired.Name = "lblNameRequired";
            this.lblNameRequired.Size = new System.Drawing.Size(7, 14);
            this.lblNameRequired.TabIndex = 22;
            this.lblNameRequired.Text = "*";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(79, 74);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(282, 247);
            this.txtNotes.TabIndex = 5;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(37, 77);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 26;
            this.lblNotes.Text = "备注：";
            // 
            // lnkDetailedView
            // 
            this.lnkDetailedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDetailedView.EditValue = "查询详情";
            this.lnkDetailedView.Location = new System.Drawing.Point(77, 331);
            this.lnkDetailedView.Name = "lnkDetailedView";
            this.lnkDetailedView.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkDetailedView.Properties.Appearance.Options.UseBackColor = true;
            this.lnkDetailedView.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkDetailedView.Properties.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lnkDetailedView.Size = new System.Drawing.Size(280, 22);
            this.lnkDetailedView.TabIndex = 10;
            // 
            // lblAuditorRequired
            // 
            this.lblAuditorRequired.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblAuditorRequired.Appearance.Options.UseForeColor = true;
            this.lblAuditorRequired.Location = new System.Drawing.Point(367, 46);
            this.lblAuditorRequired.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblAuditorRequired.Name = "lblAuditorRequired";
            this.lblAuditorRequired.Size = new System.Drawing.Size(7, 14);
            this.lblAuditorRequired.TabIndex = 101;
            this.lblAuditorRequired.Text = "*";
            this.lblAuditorRequired.Visible = false;
            // 
            // lblRecommendType
            // 
            this.lblRecommendType.Location = new System.Drawing.Point(27, 271);
            this.lblRecommendType.Name = "lblRecommendType";
            this.lblRecommendType.Size = new System.Drawing.Size(60, 14);
            this.lblRecommendType.TabIndex = 104;
            this.lblRecommendType.Text = "推荐类型：";
            // 
            // icmbRecommendType
            // 
            this.icmbRecommendType.Location = new System.Drawing.Point(93, 270);
            this.icmbRecommendType.Name = "icmbRecommendType";
            this.icmbRecommendType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbRecommendType.Properties.SmallImages = this.icRecommendType;
            this.icmbRecommendType.Size = new System.Drawing.Size(281, 20);
            this.icmbRecommendType.TabIndex = 103;
            // 
            // icRecommendType
            // 
            this.icRecommendType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icRecommendType.ImageStream")));
            this.icRecommendType.Images.SetKeyName(0, "RecommendType_Private.png");
            this.icRecommendType.Images.SetKeyName(1, "RecommendType_Shared.png");
            // 
            // QueryStatementModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblRecommendType);
            this.Controls.Add(this.icmbRecommendType);
            this.Controls.Add(this.lblAuditorRequired);
            this.Controls.Add(this.lnkDetailedView);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNameRequired);
            this.Controls.Add(this.txtQueryCode);
            this.Controls.Add(this.lblQueryCode);
            this.Controls.Add(this.txtQueryame);
            this.Controls.Add(this.lblQueryName);
            this.Name = "QueryStatementModule";
            this.Size = new System.Drawing.Size(386, 363);
            this.Load += new System.EventHandler(this.PrintModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryame.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkDetailedView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbRecommendType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icRecommendType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtQueryCode;
        private DevExpress.XtraEditors.LabelControl lblQueryCode;
        private DevExpress.XtraEditors.TextEdit txtQueryame;
        private DevExpress.XtraEditors.LabelControl lblNameRequired;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.HyperLinkEdit lnkDetailedView;
        private DevExpress.XtraEditors.LabelControl lblQueryName;
        private DevExpress.XtraEditors.LabelControl lblAuditorRequired;
        private DevExpress.XtraEditors.LabelControl lblRecommendType;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbRecommendType;
        private DevExpress.Utils.ImageCollection icRecommendType;
    }
}
