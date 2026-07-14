namespace AppFramework.WinFormsControls
{
    partial class DevExpressUpdatedUploadFile
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevExpressUpdatedUploadFile));
            this.textEdit = new DevExpress.XtraEditors.TextEdit();
            this.icButton = new DevExpress.Utils.ImageCollection(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageEdit = new DevExpress.XtraEditors.ImageEdit();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.sbtnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.lnkSave = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.lnkDelete = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.lnkView = new DevExpress.XtraEditors.HyperlinkLabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEdit
            // 
            this.textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEdit.Location = new System.Drawing.Point(0, 0);
            this.textEdit.Name = "textEdit";
            this.textEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.textEdit.Properties.MaxLength = 1024;
            this.textEdit.Properties.ReadOnly = true;
            this.textEdit.Size = new System.Drawing.Size(185, 20);
            this.textEdit.TabIndex = 0;
            this.textEdit.TextChanged += new System.EventHandler(this.textEdit_TextChanged);
            // 
            // icButton
            // 
            this.icButton.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icButton.ImageStream")));
            this.icButton.Images.SetKeyName(0, "Button_Query.png");
            this.icButton.Images.SetKeyName(1, "Button_View.png");
            this.icButton.Images.SetKeyName(2, "Button_Save.png");
            this.icButton.Images.SetKeyName(3, "Button_Close.png");
            // 
            // openFileDialog
            // 
            this.openFileDialog.RestoreDirectory = true;
            // 
            // imageEdit
            // 
            this.imageEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageEdit.Location = new System.Drawing.Point(0, 0);
            this.imageEdit.Name = "imageEdit";
            this.imageEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageEdit.Properties.LookAndFeel.SkinName = "Blue";
            this.imageEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.imageEdit.Properties.ShowMenu = false;
            this.imageEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.imageEdit.Size = new System.Drawing.Size(150, 20);
            this.imageEdit.TabIndex = 3;
            this.imageEdit.Visible = false;
            this.imageEdit.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.imageEdit_Closed);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // sbtnBrowse
            // 
            this.sbtnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtnBrowse.Image = global::AppFramework.WinFormsControls.Properties.Resources.Button_Search;
            this.sbtnBrowse.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.sbtnBrowse.Location = new System.Drawing.Point(188, 0);
            this.sbtnBrowse.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnBrowse.Name = "sbtnBrowse";
            this.sbtnBrowse.Size = new System.Drawing.Size(16, 16);
            this.sbtnBrowse.TabIndex = 1;
            this.sbtnBrowse.Click += new System.EventHandler(this.sbtnBrowse_Click);
            // 
            // lblTip
            // 
            this.lblTip.Location = new System.Drawing.Point(10, 58);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(0, 14);
            this.lblTip.TabIndex = 6;
            // 
            // lnkSave
            // 
            this.lnkSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkSave.Appearance.ImageIndex = 2;
            this.lnkSave.Appearance.ImageList = this.icButton;
            this.lnkSave.Appearance.Options.UseImageIndex = true;
            this.lnkSave.Appearance.Options.UseImageList = true;
            this.lnkSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkSave.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lnkSave.Location = new System.Drawing.Point(69, 24);
            this.lnkSave.Name = "lnkSave";
            this.lnkSave.Size = new System.Drawing.Size(45, 20);
            this.lnkSave.TabIndex = 7;
            this.lnkSave.Text = "下载";
            this.lnkSave.Click += new System.EventHandler(this.lnkSave_Click);
            // 
            // lnkDelete
            // 
            this.lnkDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDelete.Appearance.ImageIndex = 3;
            this.lnkDelete.Appearance.ImageList = this.icButton;
            this.lnkDelete.Appearance.Options.UseImageIndex = true;
            this.lnkDelete.Appearance.Options.UseImageList = true;
            this.lnkDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkDelete.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lnkDelete.Location = new System.Drawing.Point(124, 24);
            this.lnkDelete.Name = "lnkDelete";
            this.lnkDelete.Size = new System.Drawing.Size(45, 20);
            this.lnkDelete.TabIndex = 8;
            this.lnkDelete.Text = "删除";
            this.lnkDelete.Click += new System.EventHandler(this.lnkDelete_Click);
            // 
            // lnkView
            // 
            this.lnkView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkView.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkView.Appearance.ImageIndex = 1;
            this.lnkView.Appearance.ImageList = this.icButton;
            this.lnkView.Appearance.Options.UseImageAlign = true;
            this.lnkView.Appearance.Options.UseImageIndex = true;
            this.lnkView.Appearance.Options.UseImageList = true;
            this.lnkView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkView.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lnkView.Location = new System.Drawing.Point(14, 24);
            this.lnkView.Name = "lnkView";
            this.lnkView.Size = new System.Drawing.Size(45, 20);
            this.lnkView.TabIndex = 9;
            this.lnkView.Text = "查看";
            this.lnkView.Click += new System.EventHandler(this.lnkView_Click);
            // 
            // DevExpressNewUploadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkView);
            this.Controls.Add(this.lnkDelete);
            this.Controls.Add(this.lnkSave);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.textEdit);
            this.Controls.Add(this.sbtnBrowse);
            this.Controls.Add(this.imageEdit);
            this.Name = "DevExpressNewUploadFile";
            this.Size = new System.Drawing.Size(210, 49);
            this.Load += new System.EventHandler(this.DevExpressUploadFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEdit;
        private DevExpress.XtraEditors.SimpleButton sbtnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private DevExpress.XtraEditors.ImageEdit imageEdit;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private DevExpress.Utils.ImageCollection icButton;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.HyperlinkLabelControl lnkSave;
        private DevExpress.XtraEditors.HyperlinkLabelControl lnkDelete;
        private DevExpress.XtraEditors.HyperlinkLabelControl lnkView;
    }
}
