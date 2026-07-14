namespace Blue.WindowsFormsClient
{
    partial class QueryUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryUserControl));
            this.ncQuery = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarSeparatorItem1 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.navBarSeparatorItem2 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.icDataQueryLarge = new DevExpress.Utils.ImageCollection(this.components);
            this.icDataQuerySmall = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ncQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataQueryLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataQuerySmall)).BeginInit();
            this.SuspendLayout();
            // 
            // ncQuery
            // 
            this.ncQuery.ActiveGroup = null;
            this.ncQuery.AllowDrop = false;
            this.ncQuery.Dock = System.Windows.Forms.DockStyle.Left;
            this.ncQuery.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.None;
            this.ncQuery.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarSeparatorItem1,
            this.navBarSeparatorItem2});
            this.ncQuery.LargeImages = this.icDataQueryLarge;
            this.ncQuery.Location = new System.Drawing.Point(0, 0);
            this.ncQuery.Name = "ncQuery";
            this.ncQuery.OptionsNavPane.ExpandedWidth = 177;
            this.ncQuery.OptionsNavPane.ShowOverflowButton = false;
            this.ncQuery.OptionsNavPane.ShowOverflowPanel = false;
            this.ncQuery.OptionsNavPane.ShowSplitter = false;
            this.ncQuery.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.ncQuery.Size = new System.Drawing.Size(177, 477);
            this.ncQuery.SmallImages = this.icDataQuerySmall;
            this.ncQuery.StoreDefaultPaintStyleName = true;
            this.ncQuery.TabIndex = 1;
            this.ncQuery.Text = "navBarControl1";
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
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(177, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(823, 477);
            this.pnlMain.TabIndex = 2;
            // 
            // icDataQueryLarge
            // 
            this.icDataQueryLarge.ImageSize = new System.Drawing.Size(32, 32);
            this.icDataQueryLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataQueryLarge.ImageStream")));
            this.icDataQueryLarge.Images.SetKeyName(0, "MyAuditingModule_PersonalAduting.png");
            this.icDataQueryLarge.Images.SetKeyName(1, "MyAuditingModule_GroupAduting.png");
            // 
            // icDataQuerySmall
            // 
            this.icDataQuerySmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataQuerySmall.ImageStream")));
            this.icDataQuerySmall.Images.SetKeyName(0, "MyAuditingModule_Auditing_Caption.png");
            // 
            // QueryUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.ncQuery);
            this.Name = "QueryUserControl";
            this.Size = new System.Drawing.Size(1000, 477);
            this.Load += new System.EventHandler(this.QueryUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ncQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataQueryLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataQuerySmall)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl ncQuery;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem1;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem2;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.Utils.ImageCollection icDataQueryLarge;
        private DevExpress.Utils.ImageCollection icDataQuerySmall;
    }
}
