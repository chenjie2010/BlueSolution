namespace Blue.WindowsFormsClient
{
    partial class CommonListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonListControl));
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.btnTop = new DevExpress.XtraEditors.SimpleButton();
            this.icButtons = new DevExpress.Utils.ImageCollection(this.components);
            this.pnlButton = new DevExpress.XtraEditors.PanelControl();
            this.btnBottom = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrevious = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.lstItems = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).BeginInit();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstItems)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRemove
            // 
            this.btnRemove.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.btnRemove.Location = new System.Drawing.Point(145, 131);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(93, 23);
            this.btnRemove.TabIndex = 53;
            this.btnRemove.Text = "移除(&R)";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnTop
            // 
            this.btnTop.ImageIndex = 0;
            this.btnTop.ImageList = this.icButtons;
            this.btnTop.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnTop.Location = new System.Drawing.Point(5, 5);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(26, 23);
            this.btnTop.TabIndex = 57;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // icButtons
            // 
            this.icButtons.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icButtons.ImageStream")));
            this.icButtons.Images.SetKeyName(0, "Common_Arrow_Top.png");
            this.icButtons.Images.SetKeyName(1, "Common_Arrow_Up.png");
            this.icButtons.Images.SetKeyName(2, "Common_Arrow_Down.png");
            this.icButtons.Images.SetKeyName(3, "Common_Arrow_Bottom.png");
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnBottom);
            this.pnlButton.Controls.Add(this.btnNext);
            this.pnlButton.Controls.Add(this.btnPrevious);
            this.pnlButton.Controls.Add(this.btnTop);
            this.pnlButton.Location = new System.Drawing.Point(249, 5);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(36, 120);
            this.pnlButton.TabIndex = 58;
            // 
            // btnBottom
            // 
            this.btnBottom.ImageIndex = 3;
            this.btnBottom.ImageList = this.icButtons;
            this.btnBottom.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBottom.Location = new System.Drawing.Point(5, 92);
            this.btnBottom.Name = "btnBottom";
            this.btnBottom.Size = new System.Drawing.Size(26, 23);
            this.btnBottom.TabIndex = 61;
            this.btnBottom.Click += new System.EventHandler(this.btnBottom_Click);
            // 
            // btnNext
            // 
            this.btnNext.ImageIndex = 2;
            this.btnNext.ImageList = this.icButtons;
            this.btnNext.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNext.Location = new System.Drawing.Point(5, 63);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(26, 23);
            this.btnNext.TabIndex = 60;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.ImageIndex = 1;
            this.btnPrevious.ImageList = this.icButtons;
            this.btnPrevious.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnPrevious.Location = new System.Drawing.Point(5, 34);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(26, 23);
            this.btnPrevious.TabIndex = 58;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Add;
            this.btnAdd.Location = new System.Drawing.Point(46, 131);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(93, 23);
            this.btnAdd.TabIndex = 52;
            this.btnAdd.Text = "添加...(&F)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstItems
            // 
            this.lstItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstItems.IncrementalSearch = true;
            this.lstItems.Location = new System.Drawing.Point(3, 5);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(241, 120);
            this.lstItems.TabIndex = 67;
            this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
            // 
            // CommonListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstItems);
            this.Controls.Add(this.pnlButton);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Name = "CommonListControl";
            this.Size = new System.Drawing.Size(290, 160);
            this.Load += new System.EventHandler(this.ListControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).EndInit();
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraEditors.SimpleButton btnTop;
        private DevExpress.XtraEditors.PanelControl pnlButton;
        private DevExpress.Utils.ImageCollection icButtons;
        private DevExpress.XtraEditors.SimpleButton btnBottom;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnPrevious;
        private DevExpress.XtraEditors.ListBoxControl lstItems;
    }
}
