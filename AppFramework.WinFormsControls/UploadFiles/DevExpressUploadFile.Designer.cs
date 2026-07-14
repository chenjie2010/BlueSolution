namespace AppFramework.WinFormsControls
{
    partial class DevExpressUploadFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevExpressUploadFile));
            this.textEdit = new DevExpress.XtraEditors.TextEdit();
            this.icButton = new DevExpress.Utils.ImageCollection(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageEdit = new DevExpress.XtraEditors.ImageEdit();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.hlnkView = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.fpFileOperation = new DevExpress.Utils.FlyoutPanel();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.sbtnBrowse = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFileOperation)).BeginInit();
            this.fpFileOperation.SuspendLayout();
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
            this.textEdit.Size = new System.Drawing.Size(189, 20);
            this.textEdit.TabIndex = 0;
            this.textEdit.TextChanged += new System.EventHandler(this.textEdit_TextChanged);
            // 
            // icButton
            // 
            this.icButton.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icButton.ImageStream")));
            this.icButton.Images.SetKeyName(0, "Button_Query.png");
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
            this.imageEdit.Size = new System.Drawing.Size(190, 20);
            this.imageEdit.TabIndex = 3;
            this.imageEdit.Visible = false;
            this.imageEdit.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.imageEdit_Closed);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // hlnkView
            // 
            this.hlnkView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlnkView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkView.Location = new System.Drawing.Point(220, 3);
            this.hlnkView.Name = "hlnkView";
            this.hlnkView.Size = new System.Drawing.Size(24, 14);
            this.hlnkView.TabIndex = 4;
            this.hlnkView.Text = "操作";
            this.hlnkView.Click += new System.EventHandler(this.hlnkView_Click);
            // 
            // fpFileOperation
            // 
            this.fpFileOperation.Controls.Add(this.lblTip);
            this.fpFileOperation.Location = new System.Drawing.Point(65, 23);
            this.fpFileOperation.Name = "fpFileOperation";
            this.fpFileOperation.OptionsButtonPanel.ButtonPanelContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.fpFileOperation.OptionsButtonPanel.ButtonPanelLocation = DevExpress.Utils.FlyoutPanelButtonPanelLocation.Bottom;
            this.fpFileOperation.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("查看", global::AppFramework.WinFormsControls.Properties.Resources.Button_View, -1, DevExpress.XtraEditors.ButtonPanel.ImageLocation.Default, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "查看", true, -1, true, null, true, false, true, null, ((byte)(0)), 0, false),
            new DevExpress.Utils.PeekFormButton("下载", global::AppFramework.WinFormsControls.Properties.Resources.Button_Save, -1, DevExpress.XtraEditors.ButtonPanel.ImageLocation.Default, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "保存", true, -1, true, null, true, false, true, null, "1", 1, false),
            new DevExpress.Utils.PeekFormButton("删除", global::AppFramework.WinFormsControls.Properties.Resources.Button_Close, -1, DevExpress.XtraEditors.ButtonPanel.ImageLocation.Default, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "删除", true, -1, true, null, true, false, true, null, ((byte)(2)), 2, false)});
            this.fpFileOperation.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpFileOperation.OwnerControl = this.hlnkView;
            this.fpFileOperation.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
            this.fpFileOperation.Size = new System.Drawing.Size(179, 45);
            this.fpFileOperation.TabIndex = 5;
            this.fpFileOperation.ButtonClick += new DevExpress.Utils.FlyoutPanelButtonClickEventHandler(this.fpFileOperation_ButtonClick);
            // 
            // lblTip
            // 
            this.lblTip.Location = new System.Drawing.Point(3, 2);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(0, 14);
            this.lblTip.TabIndex = 0;
            // 
            // sbtnBrowse
            // 
            this.sbtnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtnBrowse.Image = global::AppFramework.WinFormsControls.Properties.Resources.Button_Search;
            this.sbtnBrowse.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.sbtnBrowse.Location = new System.Drawing.Point(195, 0);
            this.sbtnBrowse.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnBrowse.Name = "sbtnBrowse";
            this.sbtnBrowse.Size = new System.Drawing.Size(16, 16);
            this.sbtnBrowse.TabIndex = 1;
            this.sbtnBrowse.Click += new System.EventHandler(this.sbtnBrowse_Click);
            // 
            // DevExpressUploadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fpFileOperation);
            this.Controls.Add(this.hlnkView);
            this.Controls.Add(this.textEdit);
            this.Controls.Add(this.sbtnBrowse);
            this.Controls.Add(this.imageEdit);
            this.Name = "DevExpressUploadFile";
            this.Size = new System.Drawing.Size(250, 20);
            this.Load += new System.EventHandler(this.DevExpressUploadFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFileOperation)).EndInit();
            this.fpFileOperation.ResumeLayout(false);
            this.fpFileOperation.PerformLayout();
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
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkView;
        private DevExpress.Utils.FlyoutPanel fpFileOperation;
        private DevExpress.XtraEditors.LabelControl lblTip;
    }
}
