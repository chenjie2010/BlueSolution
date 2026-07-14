namespace Blue.WindowsFormsClient.Common
{
    partial class MyMailForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyMailForm));
            this.nbcMail = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgFirst = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiInBox = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiOutBox = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiDraft = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgSecond = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiRecycleBin = new DevExpress.XtraNavBar.NavBarItem();
            this.icNav = new DevExpress.Utils.ImageCollection(this.components);
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiWrite = new DevExpress.XtraBars.BarButtonItem();
            this.bbiReply = new DevExpress.XtraBars.BarButtonItem();
            this.bbiTransfer = new DevExpress.XtraBars.BarButtonItem();
            this.blcMark = new DevExpress.XtraBars.BarLinkContainerItem();
            this.bbiOld = new DevExpress.XtraBars.BarButtonItem();
            this.bbiNew = new DevExpress.XtraBars.BarButtonItem();
            this.bbiPending = new DevExpress.XtraBars.BarButtonItem();
            this.bbiComplete = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRecovery = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.blcMove = new DevExpress.XtraBars.BarLinkContainerItem();
            this.bbiInBox = new DevExpress.XtraBars.BarButtonItem();
            this.bbiOutBox = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDraft = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.gcQuery = new DevExpress.XtraEditors.GroupControl();
            this.icmbBox = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icBoxes = new DevExpress.Utils.ImageCollection(this.components);
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txtCondition = new DevExpress.XtraEditors.TextEdit();
            this.lblCondition = new DevExpress.XtraEditors.LabelControl();
            this.gcList = new DevExpress.XtraEditors.GroupControl();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.drgMail = new AppFramework.WinFormsControls.DevExpressGrid();
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.gcDetail = new DevExpress.XtraEditors.GroupControl();
            this.mailControl = new Blue.WindowsFormsClient.Common.CommonControls.MailControl();
            this.defaultBarAndDockingController = new DevExpress.XtraBars.DefaultBarAndDockingController(this.components);
            this.icPriority = new DevExpress.Utils.ImageCollection(this.components);
            this.icMailDeliveryMode = new DevExpress.Utils.ImageCollection(this.components);
            this.icMailState = new DevExpress.Utils.ImageCollection(this.components);
            this.icAttach = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nbcMail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icNav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuery)).BeginInit();
            this.gcQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icBoxes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            this.gcList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).BeginInit();
            this.gcDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController.Controller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icMailDeliveryMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icMailState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAttach)).BeginInit();
            this.SuspendLayout();
            // 
            // nbcMail
            // 
            this.nbcMail.ActiveGroup = this.nbgFirst;
            this.nbcMail.Dock = System.Windows.Forms.DockStyle.Left;
            this.nbcMail.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgFirst,
            this.nbgSecond});
            this.nbcMail.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbiInBox,
            this.nbiOutBox,
            this.nbiDraft,
            this.nbiRecycleBin});
            this.nbcMail.LargeImages = this.icNav;
            this.nbcMail.Location = new System.Drawing.Point(0, 26);
            this.nbcMail.Name = "nbcMail";
            this.nbcMail.OptionsNavPane.ExpandedWidth = 151;
            this.nbcMail.OptionsNavPane.ShowOverflowButton = false;
            this.nbcMail.OptionsNavPane.ShowOverflowPanel = false;
            this.nbcMail.Size = new System.Drawing.Size(151, 699);
            this.nbcMail.TabIndex = 0;
            this.nbcMail.Text = "导航栏";
            // 
            // nbgFirst
            // 
            this.nbgFirst.Caption = "我的邮件";
            this.nbgFirst.Expanded = true;
            this.nbgFirst.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            this.nbgFirst.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiInBox),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiOutBox),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiDraft)});
            this.nbgFirst.Name = "nbgFirst";
            this.nbgFirst.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbgFirst.SmallImage")));
            // 
            // nbiInBox
            // 
            this.nbiInBox.Caption = "收件箱";
            this.nbiInBox.LargeImageIndex = 0;
            this.nbiInBox.Name = "nbiInBox";
            this.nbiInBox.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiInBox_LinkClicked);
            // 
            // nbiOutBox
            // 
            this.nbiOutBox.Caption = "发件箱";
            this.nbiOutBox.LargeImageIndex = 1;
            this.nbiOutBox.Name = "nbiOutBox";
            this.nbiOutBox.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiOutBox_LinkClicked);
            // 
            // nbiDraft
            // 
            this.nbiDraft.Caption = "草稿箱";
            this.nbiDraft.LargeImageIndex = 2;
            this.nbiDraft.Name = "nbiDraft";
            this.nbiDraft.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiDraft_LinkClicked);
            // 
            // nbgSecond
            // 
            this.nbgSecond.Caption = "其他设置";
            this.nbgSecond.Expanded = true;
            this.nbgSecond.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.nbgSecond.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiRecycleBin)});
            this.nbgSecond.LargeImage = ((System.Drawing.Image)(resources.GetObject("nbgSecond.LargeImage")));
            this.nbgSecond.Name = "nbgSecond";
            // 
            // nbiRecycleBin
            // 
            this.nbiRecycleBin.Caption = "回收站";
            this.nbiRecycleBin.LargeImageIndex = 3;
            this.nbiRecycleBin.Name = "nbiRecycleBin";
            this.nbiRecycleBin.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiRecycleBin_LinkClicked);
            // 
            // icNav
            // 
            this.icNav.ImageSize = new System.Drawing.Size(32, 32);
            this.icNav.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icNav.ImageStream")));
            this.icNav.Images.SetKeyName(0, "Mail_InBox.png");
            this.icNav.Images.SetKeyName(1, "Mail_OutBox.png");
            this.icNav.Images.SetKeyName(2, "Mail_Draft.png");
            this.icNav.Images.SetKeyName(3, "Mail_Recycle_Bin.png");
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "Mail_Write.png");
            this.icTools.Images.SetKeyName(1, "Mail_Reply.png");
            this.icTools.Images.SetKeyName(2, "Mail_Transfer.png");
            this.icTools.Images.SetKeyName(3, "Mail_Move.png");
            this.icTools.Images.SetKeyName(4, "Mail_Mark.png");
            this.icTools.Images.SetKeyName(5, "Mail_Recycle.png");
            this.icTools.Images.SetKeyName(6, "Mail_Refresh.png");
            this.icTools.Images.SetKeyName(7, "Mail_State_Open.png");
            this.icTools.Images.SetKeyName(8, "Mail_State_New.png");
            this.icTools.Images.SetKeyName(9, "Mail_State_Pending.png");
            this.icTools.Images.SetKeyName(10, "Mail_State_Completed.png");
            this.icTools.Images.SetKeyName(11, "Mail_Recovery.png");
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.icTools;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiWrite,
            this.bbiReply,
            this.bbiDelete,
            this.bbiRefresh,
            this.blcMove,
            this.blcMark,
            this.bbiTransfer,
            this.bbiInBox,
            this.bbiOutBox,
            this.bbiDraft,
            this.bbiOld,
            this.bbiNew,
            this.barButtonItem1,
            this.bbiPending,
            this.bbiComplete,
            this.bbiRecovery});
            this.barManager.LargeImages = this.icNav;
            this.barManager.MaxItemId = 16;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiWrite, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiReply, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiTransfer, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blcMark, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiRecovery, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bbiWrite
            // 
            this.bbiWrite.Caption = "写新邮件(&W)";
            this.bbiWrite.Id = 0;
            this.bbiWrite.ImageIndex = 0;
            this.bbiWrite.Name = "bbiWrite";
            this.bbiWrite.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiWrite_ItemClick);
            // 
            // bbiReply
            // 
            this.bbiReply.Caption = "回复邮件(&R)";
            this.bbiReply.Id = 1;
            this.bbiReply.ImageIndex = 1;
            this.bbiReply.Name = "bbiReply";
            this.bbiReply.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiReply_ItemClick);
            // 
            // bbiTransfer
            // 
            this.bbiTransfer.Caption = "转发邮件(&T)";
            this.bbiTransfer.Id = 6;
            this.bbiTransfer.ImageIndex = 2;
            this.bbiTransfer.Name = "bbiTransfer";
            this.bbiTransfer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiTransfer_ItemClick);
            // 
            // blcMark
            // 
            this.blcMark.Caption = "标记为...(&M)";
            this.blcMark.Id = 5;
            this.blcMark.ImageIndex = 4;
            this.blcMark.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiOld),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiPending),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiComplete)});
            this.blcMark.Name = "blcMark";
            // 
            // bbiOld
            // 
            this.bbiOld.Caption = "已读(&R)";
            this.bbiOld.Id = 10;
            this.bbiOld.ImageIndex = 7;
            this.bbiOld.Name = "bbiOld";
            this.bbiOld.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiOld_ItemClick);
            // 
            // bbiNew
            // 
            this.bbiNew.Caption = "未读(&N)";
            this.bbiNew.Id = 11;
            this.bbiNew.ImageIndex = 8;
            this.bbiNew.Name = "bbiNew";
            this.bbiNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNew_ItemClick);
            // 
            // bbiPending
            // 
            this.bbiPending.Caption = "待办(&P)";
            this.bbiPending.Id = 13;
            this.bbiPending.ImageIndex = 9;
            this.bbiPending.Name = "bbiPending";
            this.bbiPending.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPending_ItemClick);
            // 
            // bbiComplete
            // 
            this.bbiComplete.Caption = "完成(&C)";
            this.bbiComplete.Id = 14;
            this.bbiComplete.ImageIndex = 10;
            this.bbiComplete.Name = "bbiComplete";
            this.bbiComplete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiComplete_ItemClick);
            // 
            // bbiDelete
            // 
            this.bbiDelete.Caption = "删除(&D)";
            this.bbiDelete.Id = 2;
            this.bbiDelete.ImageIndex = 5;
            this.bbiDelete.Name = "bbiDelete";
            this.bbiDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDelete_ItemClick);
            // 
            // bbiRecovery
            // 
            this.bbiRecovery.Caption = "还原(&C)";
            this.bbiRecovery.Id = 15;
            this.bbiRecovery.ImageIndex = 11;
            this.bbiRecovery.Name = "bbiRecovery";
            this.bbiRecovery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRecovery_ItemClick);
            // 
            // bbiRefresh
            // 
            this.bbiRefresh.Caption = "刷新(&R)";
            this.bbiRefresh.Id = 3;
            this.bbiRefresh.ImageIndex = 6;
            this.bbiRefresh.Name = "bbiRefresh";
            this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRefresh_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1314, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 725);
            this.barDockControlBottom.Size = new System.Drawing.Size(1314, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 699);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1314, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 699);
            // 
            // blcMove
            // 
            this.blcMove.Caption = "移动到...(&T)";
            this.blcMove.Id = 4;
            this.blcMove.ImageIndex = 3;
            this.blcMove.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiInBox),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiOutBox),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDraft),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.blcMove.Name = "blcMove";
            // 
            // bbiInBox
            // 
            this.bbiInBox.Caption = "收件箱(&I)";
            this.bbiInBox.Id = 7;
            this.bbiInBox.ImageIndex = 7;
            this.bbiInBox.Name = "bbiInBox";
            // 
            // bbiOutBox
            // 
            this.bbiOutBox.Caption = "发件箱(&O)";
            this.bbiOutBox.Id = 8;
            this.bbiOutBox.ImageIndex = 8;
            this.bbiOutBox.Name = "bbiOutBox";
            // 
            // bbiDraft
            // 
            this.bbiDraft.Caption = "草稿箱(&D)";
            this.bbiDraft.Id = 9;
            this.bbiDraft.ImageIndex = 9;
            this.bbiDraft.Name = "bbiDraft";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "垃圾箱(&R)";
            this.barButtonItem1.Id = 12;
            this.barButtonItem1.ImageIndex = 10;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // gcQuery
            // 
            this.gcQuery.CaptionImage = ((System.Drawing.Image)(resources.GetObject("gcQuery.CaptionImage")));
            this.gcQuery.Controls.Add(this.icmbBox);
            this.gcQuery.Controls.Add(this.btnClear);
            this.gcQuery.Controls.Add(this.btnQuery);
            this.gcQuery.Controls.Add(this.txtCondition);
            this.gcQuery.Controls.Add(this.lblCondition);
            this.gcQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcQuery.Location = new System.Drawing.Point(151, 26);
            this.gcQuery.Name = "gcQuery";
            this.gcQuery.Size = new System.Drawing.Size(1163, 61);
            this.gcQuery.TabIndex = 5;
            this.gcQuery.Text = "邮件查询";
            // 
            // icmbBox
            // 
            this.icmbBox.Location = new System.Drawing.Point(559, 32);
            this.icmbBox.Name = "icmbBox";
            this.icmbBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbBox.Properties.SmallImages = this.icBoxes;
            this.icmbBox.Size = new System.Drawing.Size(204, 20);
            this.icmbBox.TabIndex = 1;
            // 
            // icBoxes
            // 
            this.icBoxes.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icBoxes.ImageStream")));
            this.icBoxes.Images.SetKeyName(0, "Mail_InBox.png");
            this.icBoxes.Images.SetKeyName(1, "Mail_OutBox.png");
            this.icBoxes.Images.SetKeyName(2, "Mail_Draft.png");
            this.icBoxes.Images.SetKeyName(3, "Mail_Recycle_Bin.png");
            // 
            // btnClear
            // 
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageIndex = 0;
            this.btnClear.Location = new System.Drawing.Point(859, 30);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "清除(&R)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageIndex = 0;
            this.btnQuery.Location = new System.Drawing.Point(776, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtCondition
            // 
            this.txtCondition.EditValue = "";
            this.txtCondition.Location = new System.Drawing.Point(82, 32);
            this.txtCondition.MenuManager = this.barManager;
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Properties.MaxLength = 32;
            this.txtCondition.Properties.NullValuePrompt = "请输入邮件标题";
            this.txtCondition.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCondition.Size = new System.Drawing.Size(468, 20);
            this.txtCondition.TabIndex = 0;
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(17, 34);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(60, 14);
            this.lblCondition.TabIndex = 21;
            this.lblCondition.Text = "查询条件：";
            // 
            // gcList
            // 
            this.gcList.CaptionImage = ((System.Drawing.Image)(resources.GetObject("gcList.CaptionImage")));
            this.gcList.Controls.Add(this.progressPanel);
            this.gcList.Controls.Add(this.drgMail);
            this.gcList.Controls.Add(this.splitterControl1);
            this.gcList.Controls.Add(this.gcDetail);
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.Location = new System.Drawing.Point(151, 87);
            this.gcList.Name = "gcList";
            this.gcList.Size = new System.Drawing.Size(1163, 638);
            this.gcList.TabIndex = 6;
            this.gcList.Text = "邮件列表";
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "数据正在加载......";
            this.progressPanel.Location = new System.Drawing.Point(385, 283);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(150, 50);
            this.progressPanel.TabIndex = 9;
            this.progressPanel.Text = "数据加载中......";
            // 
            // drgMail
            // 
            this.drgMail.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.drgMail.CheckboxColumnCaption = "多选项";
            this.drgMail.ColumnHeaderTexts = new string[0];
            this.drgMail.DataKeyNames = new string[] {
        "MailId"};
            this.drgMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drgMail.FootText = null;
            this.drgMail.ExportedExcel = false;
            this.drgMail.IsMainTable = false;
            this.drgMail.IsShowCheckBox = true;
            this.drgMail.Location = new System.Drawing.Point(2, 23);
            this.drgMail.Name = "drgMail";
            this.drgMail.PageSize = 50;
            this.drgMail.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.drgMail.Size = new System.Drawing.Size(638, 613);
            this.drgMail.TabIndex = 0;
            this.drgMail.ToolTipController = this.toolTipController;
            this.drgMail.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.drgMail_OnPageIndexChanged);
            this.drgMail.OnRowClick += new System.EventHandler<AppFramework.WinFormsControls.RowEvent>(this.drgMail_OnRowClick);
            this.drgMail.OnRowDoubleClick += new System.EventHandler<AppFramework.WinFormsControls.RowEvent>(this.drgMail_OnRowDoubleClick);
            this.drgMail.OnGridMouseMove += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.drgMail_OnGridMouseMove);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl1.Location = new System.Drawing.Point(640, 23);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 613);
            this.splitterControl1.TabIndex = 2;
            this.splitterControl1.TabStop = false;
            // 
            // gcDetail
            // 
            this.gcDetail.CaptionImage = ((System.Drawing.Image)(resources.GetObject("gcDetail.CaptionImage")));
            this.gcDetail.Controls.Add(this.mailControl);
            this.gcDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.gcDetail.Location = new System.Drawing.Point(645, 23);
            this.gcDetail.Name = "gcDetail";
            this.gcDetail.Size = new System.Drawing.Size(516, 613);
            this.gcDetail.TabIndex = 3;
            this.gcDetail.Text = "邮件内容";
            // 
            // mailControl
            // 
            this.mailControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mailControl.Location = new System.Drawing.Point(2, 23);
            this.mailControl.Name = "mailControl";
            this.mailControl.PriavteAttachmentContract = null;
            this.mailControl.PrivateMailContract = null;
            this.mailControl.Size = new System.Drawing.Size(512, 588);
            this.mailControl.TabIndex = 0;
            this.mailControl.UserAccountContract = null;
            // 
            // defaultBarAndDockingController
            // 
            this.defaultBarAndDockingController.Controller.LookAndFeel.SkinName = "Money Twins";
            this.defaultBarAndDockingController.Controller.LookAndFeel.UseDefaultLookAndFeel = false;
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // icPriority
            // 
            this.icPriority.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icPriority.ImageStream")));
            this.icPriority.Images.SetKeyName(0, "Mail_Priority_Normal.png");
            this.icPriority.Images.SetKeyName(1, "Mail_Priority_Importance.png");
            this.icPriority.Images.SetKeyName(2, "Mail_Priority_Critical.png");
            // 
            // icMailDeliveryMode
            // 
            this.icMailDeliveryMode.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icMailDeliveryMode.ImageStream")));
            this.icMailDeliveryMode.Images.SetKeyName(0, "Mail_Mode_Delivery.png");
            this.icMailDeliveryMode.Images.SetKeyName(1, "Mail_Mode_Copy.png");
            this.icMailDeliveryMode.Images.SetKeyName(2, "Mail_Mode_Blind.png");
            // 
            // icMailState
            // 
            this.icMailState.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icMailState.ImageStream")));
            this.icMailState.Images.SetKeyName(0, "Mail_State_New.png");
            this.icMailState.Images.SetKeyName(1, "Mail_State_Open.png");
            this.icMailState.Images.SetKeyName(2, "Mail_State_Pending.png");
            this.icMailState.Images.SetKeyName(3, "Mail_State_Completed.png");
            // 
            // icAttach
            // 
            this.icAttach.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icAttach.ImageStream")));
            this.icAttach.Images.SetKeyName(0, "Mail_Attach_No.png");
            this.icAttach.Images.SetKeyName(1, "Mail_Attachment.png");
            // 
            // MyMailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 725);
            this.Controls.Add(this.gcList);
            this.Controls.Add(this.gcQuery);
            this.Controls.Add(this.nbcMail);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyMailForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "邮件管理";
            this.Load += new System.EventHandler(this.MyMailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nbcMail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icNav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuery)).EndInit();
            this.gcQuery.ResumeLayout(false);
            this.gcQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icBoxes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            this.gcList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).EndInit();
            this.gcDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController.Controller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icMailDeliveryMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icMailState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAttach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl nbcMail;
        private DevExpress.XtraNavBar.NavBarGroup nbgFirst;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.Utils.ImageCollection icNav;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraNavBar.NavBarItem nbiInBox;
        private DevExpress.XtraNavBar.NavBarItem nbiOutBox;
        private DevExpress.XtraNavBar.NavBarItem nbiDraft;
        private DevExpress.XtraNavBar.NavBarGroup nbgSecond;
        private DevExpress.XtraNavBar.NavBarItem nbiRecycleBin;
        private DevExpress.XtraBars.BarButtonItem bbiWrite;
        private DevExpress.XtraBars.BarButtonItem bbiReply;
        private DevExpress.XtraBars.BarButtonItem bbiDelete;
        private DevExpress.XtraBars.BarButtonItem bbiRefresh;
        private DevExpress.XtraEditors.GroupControl gcList;
        private AppFramework.WinFormsControls.DevExpressGrid drgMail;
        private DevExpress.XtraEditors.GroupControl gcQuery;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraBars.DefaultBarAndDockingController defaultBarAndDockingController;
        private DevExpress.XtraEditors.GroupControl gcDetail;
        private DevExpress.XtraEditors.TextEdit txtCondition;
        private DevExpress.XtraEditors.LabelControl lblCondition;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraBars.BarLinkContainerItem blcMove;
        private DevExpress.XtraBars.BarLinkContainerItem blcMark;
        private DevExpress.XtraBars.BarButtonItem bbiTransfer;
        private DevExpress.XtraBars.BarButtonItem bbiInBox;
        private DevExpress.XtraBars.BarButtonItem bbiOutBox;
        private DevExpress.XtraBars.BarButtonItem bbiDraft;
        private DevExpress.XtraBars.BarButtonItem bbiOld;
        private DevExpress.XtraBars.BarButtonItem bbiNew;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.Utils.ImageCollection icPriority;
        private DevExpress.Utils.ImageCollection icMailDeliveryMode;
        private DevExpress.Utils.ImageCollection icMailState;
        private DevExpress.Utils.ImageCollection icAttach;
        private DevExpress.Utils.ToolTipController toolTipController;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbBox;
        private DevExpress.Utils.ImageCollection icBoxes;
        private DevExpress.XtraBars.BarButtonItem bbiPending;
        private DevExpress.XtraBars.BarButtonItem bbiComplete;
        private DevExpress.XtraBars.BarButtonItem bbiRecovery;
        private CommonControls.MailControl mailControl;
    }
}