namespace Blue.WindowsFormsClient.MyAuditingModule
{
    partial class TableAuditingControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableAuditingControl));
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.treeView = new System.Windows.Forms.TreeView();
            this.imglstTreeview = new System.Windows.Forms.ImageList(this.components);
            this.icmbDataWarehouse = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icDataWarehouse = new DevExpress.Utils.ImageCollection(this.components);
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gcData = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.pnlOperation = new DevExpress.XtraEditors.PanelControl();
            this.pcContainer = new DevExpress.XtraBars.PopupControlContainer(this.components);
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.hlnkCurrent = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.icButtons = new DevExpress.Utils.ImageCollection(this.components);
            this.chkBlukOperation = new DevExpress.XtraEditors.CheckEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnAudit = new DevExpress.XtraEditors.SimpleButton();
            this.saveAttachmentFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataWarehouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).BeginInit();
            this.gcData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperation)).BeginInit();
            this.pnlOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcContainer)).BeginInit();
            this.pcContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBlukOperation.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.treeView);
            this.groupControl.Controls.Add(this.icmbDataWarehouse);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl.Location = new System.Drawing.Point(0, 0);
            this.groupControl.LookAndFeel.SkinName = "Money Twins";
            this.groupControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(210, 583);
            this.groupControl.TabIndex = 17;
            this.groupControl.Text = "审核表";
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.CheckBoxes = true;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imglstTreeview;
            this.treeView.ItemHeight = 20;
            this.treeView.Location = new System.Drawing.Point(2, 42);
            this.treeView.Margin = new System.Windows.Forms.Padding(5);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 2;
            this.treeView.Size = new System.Drawing.Size(206, 539);
            this.treeView.TabIndex = 3;
            this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
            this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
            // 
            // imglstTreeview
            // 
            this.imglstTreeview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstTreeview.ImageStream")));
            this.imglstTreeview.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstTreeview.Images.SetKeyName(0, "Common_Nodes_Up.png");
            this.imglstTreeview.Images.SetKeyName(1, "Common_Nodes_Down.png");
            this.imglstTreeview.Images.SetKeyName(2, "Common_Nodes_Selected.png");
            // 
            // icmbDataWarehouse
            // 
            this.icmbDataWarehouse.Dock = System.Windows.Forms.DockStyle.Top;
            this.icmbDataWarehouse.Location = new System.Drawing.Point(2, 22);
            this.icmbDataWarehouse.Name = "icmbDataWarehouse";
            this.icmbDataWarehouse.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDataWarehouse.Properties.SmallImages = this.icDataWarehouse;
            this.icmbDataWarehouse.Size = new System.Drawing.Size(206, 20);
            this.icmbDataWarehouse.TabIndex = 59;
            this.icmbDataWarehouse.SelectedIndexChanged += new System.EventHandler(this.icmbDataWarehouse_SelectedIndexChanged);
            // 
            // icDataWarehouse
            // 
            this.icDataWarehouse.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataWarehouse.ImageStream")));
            this.icDataWarehouse.Images.SetKeyName(0, "Common_Number_First.png");
            this.icDataWarehouse.Images.SetKeyName(1, "Common_Number_Second.png");
            this.icDataWarehouse.Images.SetKeyName(2, "Common_Number_Third.png");
            this.icDataWarehouse.Images.SetKeyName(3, "Common_Number_Fourth.png");
            this.icDataWarehouse.Images.SetKeyName(4, "Common_Number_Fifth.png");
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlMain.Controls.Add(this.gcData);
            this.pnlMain.Controls.Add(this.groupControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1239, 583);
            this.pnlMain.TabIndex = 18;
            // 
            // gcData
            // 
            this.gcData.Controls.Add(this.xtraTabControl);
            this.gcData.Controls.Add(this.pnlOperation);
            this.gcData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcData.Location = new System.Drawing.Point(210, 0);
            this.gcData.LookAndFeel.SkinName = "Money Twins";
            this.gcData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcData.Name = "gcData";
            this.gcData.Size = new System.Drawing.Size(1029, 583);
            this.gcData.TabIndex = 11;
            this.gcData.Text = "用户数据";
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.xtraTabControl.Location = new System.Drawing.Point(2, 52);
            this.xtraTabControl.LookAndFeel.SkinName = "Blue";
            this.xtraTabControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.Padding = new System.Windows.Forms.Padding(2);
            this.xtraTabControl.Size = new System.Drawing.Size(1025, 529);
            this.xtraTabControl.TabIndex = 9;
            this.xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl_SelectedPageChanged);
            // 
            // pnlOperation
            // 
            this.pnlOperation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlOperation.Controls.Add(this.pcContainer);
            this.pnlOperation.Controls.Add(this.hlnkCurrent);
            this.pnlOperation.Controls.Add(this.chkBlukOperation);
            this.pnlOperation.Controls.Add(this.btnCancel);
            this.pnlOperation.Controls.Add(this.btnAudit);
            this.pnlOperation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOperation.Location = new System.Drawing.Point(2, 22);
            this.pnlOperation.Name = "pnlOperation";
            this.pnlOperation.Size = new System.Drawing.Size(1025, 30);
            this.pnlOperation.TabIndex = 10;
            // 
            // pcContainer
            // 
            this.pcContainer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcContainer.Controls.Add(this.pictureEdit);
            this.pcContainer.Location = new System.Drawing.Point(429, 9);
            this.pcContainer.Manager = this.barManager1;
            this.pcContainer.Name = "pcContainer";
            this.pcContainer.ShowSizeGrip = true;
            this.pcContainer.Size = new System.Drawing.Size(118, 161);
            this.pcContainer.TabIndex = 19;
            this.pcContainer.Visible = false;
            // 
            // pictureEdit
            // 
            this.pictureEdit.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureEdit.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit.Properties.ZoomAccelerationFactor = 1D;
            this.pictureEdit.Size = new System.Drawing.Size(118, 161);
            this.pictureEdit.TabIndex = 1;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 0;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1239, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 583);
            this.barDockControlBottom.Size = new System.Drawing.Size(1239, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 583);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1239, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 583);
            // 
            // hlnkCurrent
            // 
            this.hlnkCurrent.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkCurrent.Appearance.ImageIndex = 2;
            this.hlnkCurrent.Appearance.ImageList = this.icButtons;
            this.hlnkCurrent.Appearance.Options.UseImageAlign = true;
            this.hlnkCurrent.Appearance.Options.UseImageIndex = true;
            this.hlnkCurrent.Appearance.Options.UseImageList = true;
            this.hlnkCurrent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkCurrent.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkCurrent.Location = new System.Drawing.Point(257, 5);
            this.hlnkCurrent.Name = "hlnkCurrent";
            this.hlnkCurrent.Size = new System.Drawing.Size(98, 20);
            this.hlnkCurrent.TabIndex = 27;
            this.hlnkCurrent.Text = "设置当前/既往";
            this.hlnkCurrent.Click += new System.EventHandler(this.hlnkCurrent_Click);
            // 
            // icButtons
            // 
            this.icButtons.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icButtons.ImageStream")));
            this.icButtons.Images.SetKeyName(0, "Auditing_PersonInfo_Auditing.png");
            this.icButtons.Images.SetKeyName(1, "Auditing_PersonInfo_Auditing_Cancelled.png");
            this.icButtons.Images.SetKeyName(2, "Client_Common_Current_State.png");
            // 
            // chkBlukOperation
            // 
            this.chkBlukOperation.Location = new System.Drawing.Point(184, 7);
            this.chkBlukOperation.Name = "chkBlukOperation";
            this.chkBlukOperation.Properties.Caption = "批量操作";
            this.chkBlukOperation.Size = new System.Drawing.Size(75, 19);
            this.chkBlukOperation.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.ImageIndex = 1;
            this.btnCancel.ImageList = this.icButtons;
            this.btnCancel.Location = new System.Drawing.Point(84, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 20);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消审核(&R)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAudit
            // 
            this.btnAudit.ImageIndex = 0;
            this.btnAudit.ImageList = this.icButtons;
            this.btnAudit.Location = new System.Drawing.Point(6, 5);
            this.btnAudit.Name = "btnAudit";
            this.btnAudit.Size = new System.Drawing.Size(68, 20);
            this.btnAudit.TabIndex = 0;
            this.btnAudit.Text = "审核(&A)";
            this.btnAudit.Click += new System.EventHandler(this.btnAudit_Click);
            // 
            // TableAuditingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "TableAuditingControl";
            this.Size = new System.Drawing.Size(1239, 583);
            this.Load += new System.EventHandler(this.PersonalAuditingControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataWarehouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).EndInit();
            this.gcData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOperation)).EndInit();
            this.pnlOperation.ResumeLayout(false);
            this.pnlOperation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcContainer)).EndInit();
            this.pcContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBlukOperation.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl;
        private System.Windows.Forms.TreeView treeView;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.GroupControl gcData;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDataWarehouse;
        private DevExpress.Utils.ImageCollection icDataWarehouse;
        private System.Windows.Forms.ImageList imglstTreeview;
        private DevExpress.XtraBars.PopupControlContainer pcContainer;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraEditors.PanelControl pnlOperation;
        private DevExpress.XtraEditors.CheckEdit chkBlukOperation;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.Utils.ImageCollection icButtons;
        private DevExpress.XtraEditors.SimpleButton btnAudit;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkCurrent;
        private System.Windows.Forms.SaveFileDialog saveAttachmentFileDialog;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}
