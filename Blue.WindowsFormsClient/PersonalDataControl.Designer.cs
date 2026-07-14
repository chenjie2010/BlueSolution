namespace Blue.WindowsFormsClient
{
    partial class PersonalDataControl
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
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.ncAudting = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarSeparatorItem1 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.navBarSeparatorItem2 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ncAudting)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(177, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(674, 474);
            this.pnlMain.TabIndex = 4;
            // 
            // ncAudting
            // 
            this.ncAudting.ActiveGroup = null;
            this.ncAudting.AllowDrop = false;
            this.ncAudting.Dock = System.Windows.Forms.DockStyle.Left;
            this.ncAudting.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.None;
            this.ncAudting.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarSeparatorItem1,
            this.navBarSeparatorItem2});
            this.ncAudting.Location = new System.Drawing.Point(0, 0);
            this.ncAudting.Name = "ncAudting";
            this.ncAudting.OptionsNavPane.ExpandedWidth = 177;
            this.ncAudting.OptionsNavPane.ShowOverflowButton = false;
            this.ncAudting.OptionsNavPane.ShowOverflowPanel = false;
            this.ncAudting.OptionsNavPane.ShowSplitter = false;
            this.ncAudting.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.ncAudting.Size = new System.Drawing.Size(177, 474);
            this.ncAudting.StoreDefaultPaintStyleName = true;
            this.ncAudting.TabIndex = 3;
            this.ncAudting.Text = "navBarControl1";
            // 
            // navBarSeparatorItem1
            // 
            this.navBarSeparatorItem1.CanDrag = false;
            this.navBarSeparatorItem1.Enabled = false;
            this.navBarSeparatorItem1.Hint = null;
            this.navBarSeparatorItem1.LargeImageIndex = 0;
            this.navBarSeparatorItem1.LargeImageSize = new System.Drawing.Size(0, 0);
            this.navBarSeparatorItem1.Name = "navBarSeparatorItem1";
            this.navBarSeparatorItem1.SmallImageIndex = 0;
            this.navBarSeparatorItem1.SmallImageSize = new System.Drawing.Size(0, 0);
            // 
            // navBarSeparatorItem2
            // 
            this.navBarSeparatorItem2.CanDrag = false;
            this.navBarSeparatorItem2.Enabled = false;
            this.navBarSeparatorItem2.Hint = null;
            this.navBarSeparatorItem2.LargeImageIndex = 0;
            this.navBarSeparatorItem2.LargeImageSize = new System.Drawing.Size(0, 0);
            this.navBarSeparatorItem2.Name = "navBarSeparatorItem2";
            this.navBarSeparatorItem2.SmallImageIndex = 0;
            this.navBarSeparatorItem2.SmallImageSize = new System.Drawing.Size(0, 0);
            // 
            // PersonalDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.ncAudting);
            this.Name = "PersonalDataControl";
            this.Size = new System.Drawing.Size(851, 474);
            this.Load += new System.EventHandler(this.PersonalDataControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ncAudting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraNavBar.NavBarControl ncAudting;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem1;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem2;
    }
}
