namespace Blue.WindowsFormsClient.Common
{
    partial class TreeLayerForm
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeLayerForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar = new DevExpress.XtraBars.Bar();
            this.bbiCreate = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEdit = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSetting = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCustomItem = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExchange = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icToolItems = new DevExpress.Utils.ImageCollection(this.components);
            this.pbbiCreate = new DevExpress.XtraBars.BarButtonItem();
            this.pbbiEdit = new DevExpress.XtraBars.BarButtonItem();
            this.pbbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.pbbiTop = new DevExpress.XtraBars.BarButtonItem();
            this.pbbiPrevious = new DevExpress.XtraBars.BarButtonItem();
            this.pbbiNext = new DevExpress.XtraBars.BarButtonItem();
            this.pbbiBottom = new DevExpress.XtraBars.BarButtonItem();
            this.trvLayer = new System.Windows.Forms.TreeView();
            this.imglstTreeview = new System.Windows.Forms.ImageList(this.components);
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.gcTreeview = new DevExpress.XtraEditors.GroupControl();
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.pnlCondition = new DevExpress.XtraEditors.PanelControl();
            this.scQuery = new DevExpress.XtraEditors.SearchControl();
            this.lblCondition = new DevExpress.XtraEditors.LabelControl();
            this.pnlList = new DevExpress.XtraEditors.PanelControl();
            this.chkShow = new DevExpress.XtraEditors.CheckEdit();
            this.btnBottom = new DevExpress.XtraEditors.SimpleButton();
            this.icButton = new DevExpress.Utils.ImageCollection(this.components);
            this.btnPrevious = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnTop = new DevExpress.XtraEditors.SimpleButton();
            this.gcDetail = new DevExpress.XtraEditors.GroupControl();
            this.pnlDetail = new DevExpress.XtraEditors.PanelControl();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.chkReturn = new DevExpress.XtraEditors.CheckEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlWarning = new DevExpress.XtraEditors.PanelControl();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.peTip = new DevExpress.XtraEditors.PictureEdit();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.icGroup = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icToolItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTreeview)).BeginInit();
            this.gcTreeview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlList)).BeginInit();
            this.pnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).BeginInit();
            this.gcDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetail)).BeginInit();
            this.pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWarning)).BeginInit();
            this.pnlWarning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peTip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar});
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.icToolItems;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiCreate,
            this.bbiEdit,
            this.bbiDelete,
            this.bbiExchange,
            this.bbiSetting,
            this.pbbiCreate,
            this.pbbiEdit,
            this.pbbiDelete,
            this.pbbiTop,
            this.pbbiPrevious,
            this.pbbiNext,
            this.pbbiBottom,
            this.bbiCustomItem});
            this.barManager.MaxItemId = 13;
            // 
            // bar
            // 
            this.bar.BarName = "Tools";
            this.bar.DockCol = 0;
            this.bar.DockRow = 0;
            this.bar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiCreate, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiSetting, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiCustomItem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiExchange, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar.OptionsBar.AllowQuickCustomization = false;
            this.bar.OptionsBar.DrawDragBorder = false;
            this.bar.OptionsBar.UseWholeRow = true;
            this.bar.Text = "Tools";
            // 
            // bbiCreate
            // 
            this.bbiCreate.Caption = "新建(&N)";
            this.bbiCreate.Id = 0;
            this.bbiCreate.ImageIndex = 0;
            this.bbiCreate.Name = "bbiCreate";
            this.bbiCreate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCreate_ItemClick);
            // 
            // bbiEdit
            // 
            this.bbiEdit.Caption = "编辑(&E)";
            this.bbiEdit.Id = 1;
            this.bbiEdit.ImageIndex = 1;
            this.bbiEdit.Name = "bbiEdit";
            this.bbiEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEdit_ItemClick);
            // 
            // bbiDelete
            // 
            this.bbiDelete.Caption = "删除(&D)";
            this.bbiDelete.Id = 2;
            this.bbiDelete.ImageIndex = 2;
            this.bbiDelete.Name = "bbiDelete";
            this.bbiDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDelete_ItemClick);
            // 
            // bbiSetting
            // 
            this.bbiSetting.Caption = "设置(&S)";
            this.bbiSetting.Id = 4;
            this.bbiSetting.ImageIndex = 3;
            this.bbiSetting.Name = "bbiSetting";
            toolTipTitleItem1.Text = "提示";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.bbiSetting.SuperTip = superToolTip1;
            this.bbiSetting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSetting_ItemClick);
            // 
            // bbiCustomItem
            // 
            this.bbiCustomItem.Caption = "自定义项(&C)";
            this.bbiCustomItem.Id = 12;
            this.bbiCustomItem.ImageIndex = 4;
            this.bbiCustomItem.Name = "bbiCustomItem";
            this.bbiCustomItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiCustomItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCustomItem_ItemClick);
            // 
            // bbiExchange
            // 
            this.bbiExchange.Caption = "导入导出(&O)";
            this.bbiExchange.Id = 3;
            this.bbiExchange.ImageIndex = 5;
            this.bbiExchange.Name = "bbiExchange";
            this.bbiExchange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExchange_ItemClick);
            // 
            // barAndDockingController
            // 
            this.barAndDockingController.LookAndFeel.SkinName = "Money Twins";
            this.barAndDockingController.LookAndFeel.UseDefaultLookAndFeel = false;
            this.barAndDockingController.PropertiesBar.AllowLinkLighting = false;
            this.barAndDockingController.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.barAndDockingController.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(934, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 556);
            this.barDockControlBottom.Size = new System.Drawing.Size(934, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 530);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(934, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 530);
            // 
            // icToolItems
            // 
            this.icToolItems.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icToolItems.ImageStream")));
            this.icToolItems.Images.SetKeyName(0, "Tools_New.png");
            this.icToolItems.Images.SetKeyName(1, "Tools_Edit.png");
            this.icToolItems.Images.SetKeyName(2, "Tools_Delete.png");
            this.icToolItems.Images.SetKeyName(3, "Tool_Advance_Setting.png");
            this.icToolItems.Images.SetKeyName(4, "Tool_Custom_Item_Small.png");
            this.icToolItems.Images.SetKeyName(5, "Tools_Import.png");
            // 
            // pbbiCreate
            // 
            this.pbbiCreate.Caption = "新建(&N)";
            this.pbbiCreate.Id = 5;
            this.pbbiCreate.Name = "pbbiCreate";
            this.pbbiCreate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.pbbiCreate_ItemClick);
            // 
            // pbbiEdit
            // 
            this.pbbiEdit.Caption = "编辑(&E)";
            this.pbbiEdit.Id = 6;
            this.pbbiEdit.Name = "pbbiEdit";
            this.pbbiEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.pbbiEdit_ItemClick);
            // 
            // pbbiDelete
            // 
            this.pbbiDelete.Caption = "删除(&D)";
            this.pbbiDelete.Id = 7;
            this.pbbiDelete.Name = "pbbiDelete";
            this.pbbiDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.pbbiDelete_ItemClick);
            // 
            // pbbiTop
            // 
            this.pbbiTop.Caption = "置顶(&T)";
            this.pbbiTop.Id = 8;
            this.pbbiTop.Name = "pbbiTop";
            this.pbbiTop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.pbbiTop_ItemClick);
            // 
            // pbbiPrevious
            // 
            this.pbbiPrevious.Caption = "上移(&P)";
            this.pbbiPrevious.Id = 9;
            this.pbbiPrevious.Name = "pbbiPrevious";
            this.pbbiPrevious.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.pbbiPrevious_ItemClick);
            // 
            // pbbiNext
            // 
            this.pbbiNext.Caption = "下移(&N)";
            this.pbbiNext.Id = 10;
            this.pbbiNext.Name = "pbbiNext";
            this.pbbiNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.pbbiNext_ItemClick);
            // 
            // pbbiBottom
            // 
            this.pbbiBottom.Caption = "置底(&B)";
            this.pbbiBottom.Id = 11;
            this.pbbiBottom.Name = "pbbiBottom";
            this.pbbiBottom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.pbbiBottom_ItemClick);
            // 
            // trvLayer
            // 
            this.trvLayer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvLayer.ImageIndex = 0;
            this.trvLayer.ImageList = this.imglstTreeview;
            this.trvLayer.ItemHeight = 20;
            this.trvLayer.Location = new System.Drawing.Point(2, 2);
            this.trvLayer.Name = "trvLayer";
            this.barManager.SetPopupContextMenu(this.trvLayer, this.popupMenu);
            this.trvLayer.SelectedImageIndex = 2;
            this.trvLayer.ShowNodeToolTips = true;
            this.trvLayer.Size = new System.Drawing.Size(462, 409);
            this.trvLayer.TabIndex = 0;
            this.trvLayer.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.trvLayer_AfterCollapse);
            this.trvLayer.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvLayer_BeforeExpand);
            this.trvLayer.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.trvLayer_AfterExpand);
            this.trvLayer.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trvLayer_ItemDrag);
            this.trvLayer.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.trvLayer_NodeMouseHover);
            this.trvLayer.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvLayer_BeforeSelect);
            this.trvLayer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvLayer_AfterSelect);
            this.trvLayer.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvLayer_DragDrop);
            this.trvLayer.DragEnter += new System.Windows.Forms.DragEventHandler(this.trvLayer_DragEnter);
            // 
            // imglstTreeview
            // 
            this.imglstTreeview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstTreeview.ImageStream")));
            this.imglstTreeview.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstTreeview.Images.SetKeyName(0, "Common_Nodes_Up.png");
            this.imglstTreeview.Images.SetKeyName(1, "Common_Nodes_Down.png");
            this.imglstTreeview.Images.SetKeyName(2, "Common_Nodes_Selected.png");
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.pbbiCreate),
            new DevExpress.XtraBars.LinkPersistInfo(this.pbbiEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.pbbiDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.pbbiTop, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.pbbiPrevious),
            new DevExpress.XtraBars.LinkPersistInfo(this.pbbiNext),
            new DevExpress.XtraBars.LinkPersistInfo(this.pbbiBottom)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // gcTreeview
            // 
            this.gcTreeview.CaptionImage = ((System.Drawing.Image)(resources.GetObject("gcTreeview.CaptionImage")));
            this.gcTreeview.Controls.Add(this.pnlLeft);
            this.gcTreeview.Controls.Add(this.pnlCondition);
            this.gcTreeview.Controls.Add(this.pnlList);
            this.gcTreeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTreeview.Location = new System.Drawing.Point(0, 26);
            this.gcTreeview.Name = "gcTreeview";
            this.gcTreeview.Padding = new System.Windows.Forms.Padding(2);
            this.gcTreeview.Size = new System.Drawing.Size(474, 530);
            this.gcTreeview.TabIndex = 4;
            this.gcTreeview.Text = "列表名称";
            // 
            // pnlLeft
            // 
            this.pnlLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlLeft.Controls.Add(this.trvLayer);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(4, 68);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(2);
            this.pnlLeft.Size = new System.Drawing.Size(466, 413);
            this.pnlLeft.TabIndex = 3;
            // 
            // pnlCondition
            // 
            this.pnlCondition.Controls.Add(this.scQuery);
            this.pnlCondition.Controls.Add(this.lblCondition);
            this.pnlCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCondition.Location = new System.Drawing.Point(4, 25);
            this.pnlCondition.Name = "pnlCondition";
            this.pnlCondition.Size = new System.Drawing.Size(466, 43);
            this.pnlCondition.TabIndex = 2;
            // 
            // scQuery
            // 
            this.scQuery.Location = new System.Drawing.Point(79, 12);
            this.scQuery.MenuManager = this.barManager;
            this.scQuery.Name = "scQuery";
            this.scQuery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton(),
            new DevExpress.XtraEditors.Repository.MRUButton()});
            this.scQuery.Properties.NullValuePrompt = "请输入查询条件";
            this.scQuery.Properties.ShowDefaultButtonsMode = DevExpress.XtraEditors.Repository.ShowDefaultButtonsMode.AutoShowClear;
            this.scQuery.Properties.ShowMRUButton = true;
            this.scQuery.Properties.ShowNullValuePromptWhenFocused = true;
            this.scQuery.Size = new System.Drawing.Size(365, 20);
            this.scQuery.TabIndex = 0;
            this.scQuery.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.scQuery_ButtonClick);
            this.scQuery.EditValueChanged += new System.EventHandler(this.scQuery_EditValueChanged);
            this.scQuery.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scQuery_KeyPress);
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(10, 13);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(60, 14);
            this.lblCondition.TabIndex = 1;
            this.lblCondition.Text = "查询条件：";
            // 
            // pnlList
            // 
            this.pnlList.Controls.Add(this.chkShow);
            this.pnlList.Controls.Add(this.btnBottom);
            this.pnlList.Controls.Add(this.btnPrevious);
            this.pnlList.Controls.Add(this.btnNext);
            this.pnlList.Controls.Add(this.btnTop);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlList.Location = new System.Drawing.Point(4, 481);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(466, 45);
            this.pnlList.TabIndex = 0;
            // 
            // chkShow
            // 
            this.chkShow.Location = new System.Drawing.Point(343, 13);
            this.chkShow.MenuManager = this.barManager;
            this.chkShow.Name = "chkShow";
            this.chkShow.Properties.Caption = "显示子节点总数";
            this.chkShow.Size = new System.Drawing.Size(116, 19);
            this.chkShow.TabIndex = 4;
            this.chkShow.CheckedChanged += new System.EventHandler(this.chkShow_CheckedChanged);
            // 
            // btnBottom
            // 
            this.btnBottom.ImageIndex = 3;
            this.btnBottom.ImageList = this.icButton;
            this.btnBottom.Location = new System.Drawing.Point(258, 12);
            this.btnBottom.Name = "btnBottom";
            this.btnBottom.Size = new System.Drawing.Size(75, 23);
            this.btnBottom.TabIndex = 3;
            this.btnBottom.Text = "置底(&B)";
            this.btnBottom.Click += new System.EventHandler(this.btnBottom_Click);
            // 
            // icButton
            // 
            this.icButton.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icButton.ImageStream")));
            this.icButton.Images.SetKeyName(0, "Common_Arrow_Top.png");
            this.icButton.Images.SetKeyName(1, "Common_Arrow_Up.png");
            this.icButton.Images.SetKeyName(2, "Common_Arrow_Down.png");
            this.icButton.Images.SetKeyName(3, "Common_Arrow_Bottom.png");
            this.icButton.Images.SetKeyName(4, "Tool_Confirm.png");
            this.icButton.Images.SetKeyName(5, "Tool_Canel.png");
            // 
            // btnPrevious
            // 
            this.btnPrevious.ImageIndex = 1;
            this.btnPrevious.ImageList = this.icButton;
            this.btnPrevious.Location = new System.Drawing.Point(96, 12);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 2;
            this.btnPrevious.Text = "上移(&P)";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.ImageIndex = 2;
            this.btnNext.ImageList = this.icButton;
            this.btnNext.Location = new System.Drawing.Point(177, 12);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下移(&N)";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnTop
            // 
            this.btnTop.ImageIndex = 0;
            this.btnTop.ImageList = this.icButton;
            this.btnTop.Location = new System.Drawing.Point(10, 12);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(75, 23);
            this.btnTop.TabIndex = 0;
            this.btnTop.Text = "置顶(&T)";
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // gcDetail
            // 
            this.gcDetail.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.Tool_Detail;
            this.gcDetail.Controls.Add(this.pnlDetail);
            this.gcDetail.Controls.Add(this.chkReturn);
            this.gcDetail.Controls.Add(this.btnCancel);
            this.gcDetail.Controls.Add(this.pnlWarning);
            this.gcDetail.Controls.Add(this.btnConfirm);
            this.gcDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.gcDetail.Location = new System.Drawing.Point(474, 26);
            this.gcDetail.Name = "gcDetail";
            this.gcDetail.Padding = new System.Windows.Forms.Padding(2);
            this.gcDetail.Size = new System.Drawing.Size(460, 530);
            this.gcDetail.TabIndex = 5;
            this.gcDetail.Text = "详细信息";
            // 
            // pnlDetail
            // 
            this.pnlDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlDetail.Controls.Add(this.progressPanel);
            this.pnlDetail.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetail.Location = new System.Drawing.Point(4, 25);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(2);
            this.pnlDetail.Size = new System.Drawing.Size(452, 415);
            this.pnlDetail.TabIndex = 7;
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "请稍后";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "正在处理中......";
            this.progressPanel.Location = new System.Drawing.Point(157, 187);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.ShowCaption = false;
            this.progressPanel.Size = new System.Drawing.Size(145, 52);
            this.progressPanel.TabIndex = 0;
            this.progressPanel.Text = "数据加载中......";
            // 
            // chkReturn
            // 
            this.chkReturn.EditValue = true;
            this.chkReturn.Location = new System.Drawing.Point(17, 452);
            this.chkReturn.MenuManager = this.barManager;
            this.chkReturn.Name = "chkReturn";
            this.chkReturn.Properties.Caption = "新建成功后返回上一级";
            this.chkReturn.Size = new System.Drawing.Size(148, 19);
            this.chkReturn.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.ImageIndex = 5;
            this.btnCancel.ImageList = this.icButton;
            this.btnCancel.Location = new System.Drawing.Point(268, 451);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlWarning
            // 
            this.pnlWarning.Controls.Add(this.lblTip);
            this.pnlWarning.Controls.Add(this.peTip);
            this.pnlWarning.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlWarning.Location = new System.Drawing.Point(4, 493);
            this.pnlWarning.Name = "pnlWarning";
            this.pnlWarning.Size = new System.Drawing.Size(452, 33);
            this.pnlWarning.TabIndex = 0;
            // 
            // lblTip
            // 
            this.lblTip.Location = new System.Drawing.Point(43, 9);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(48, 14);
            this.lblTip.TabIndex = 2;
            this.lblTip.Text = "提示信息";
            // 
            // peTip
            // 
            this.peTip.Cursor = System.Windows.Forms.Cursors.Default;
            this.peTip.EditValue = global::Blue.WindowsFormsClient.Properties.Resources.Tool_Waring;
            this.peTip.Location = new System.Drawing.Point(5, 1);
            this.peTip.MenuManager = this.barManager;
            this.peTip.Name = "peTip";
            this.peTip.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peTip.Properties.Appearance.Options.UseBackColor = true;
            this.peTip.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peTip.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peTip.Properties.ZoomAccelerationFactor = 1D;
            this.peTip.Size = new System.Drawing.Size(32, 30);
            this.peTip.TabIndex = 0;
            // 
            // btnConfirm
            // 
            this.btnConfirm.ImageIndex = 4;
            this.btnConfirm.ImageList = this.icButton;
            this.btnConfirm.Location = new System.Drawing.Point(177, 451);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "确定(&Q)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // icGroup
            // 
            this.icGroup.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icGroup.ImageStream")));
            this.icGroup.Images.SetKeyName(0, "Tool_Tree.png");
            this.icGroup.Images.SetKeyName(1, "Tool_Detail.png");
            // 
            // TreeLayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 556);
            this.Controls.Add(this.gcTreeview);
            this.Controls.Add(this.gcDetail);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TreeLayerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "树形结构窗体";
            this.Load += new System.EventHandler(this.TreeLayerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icToolItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTreeview)).EndInit();
            this.gcTreeview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlCondition.ResumeLayout(false);
            this.pnlCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlList)).EndInit();
            this.pnlList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).EndInit();
            this.gcDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetail)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkReturn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlWarning)).EndInit();
            this.pnlWarning.ResumeLayout(false);
            this.pnlWarning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peTip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiCreate;
        private DevExpress.XtraBars.BarButtonItem bbiEdit;
        private DevExpress.XtraBars.BarButtonItem bbiDelete;
        private DevExpress.XtraBars.BarButtonItem bbiExchange;
        private DevExpress.Utils.ImageCollection icToolItems;
        private DevExpress.XtraBars.BarButtonItem bbiSetting;
        private DevExpress.XtraEditors.GroupControl gcDetail;
        private DevExpress.XtraEditors.GroupControl gcTreeview;
        private DevExpress.XtraEditors.PanelControl pnlList;
        private DevExpress.XtraEditors.SimpleButton btnBottom;
        private DevExpress.XtraEditors.SimpleButton btnPrevious;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnTop;
        private DevExpress.XtraEditors.PanelControl pnlCondition;
        private DevExpress.XtraEditors.LabelControl lblCondition;
        private DevExpress.XtraEditors.CheckEdit chkShow;
        private DevExpress.Utils.ImageCollection icGroup;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl pnlWarning;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.Utils.ImageCollection icButton;
        private DevExpress.XtraEditors.PictureEdit peTip;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.CheckEdit chkReturn;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem pbbiCreate;
        private DevExpress.XtraBars.BarButtonItem pbbiEdit;
        private DevExpress.XtraBars.BarButtonItem pbbiDelete;
        private DevExpress.XtraBars.BarButtonItem pbbiTop;
        private DevExpress.XtraBars.BarButtonItem pbbiPrevious;
        private DevExpress.XtraBars.BarButtonItem pbbiNext;
        private DevExpress.XtraBars.BarButtonItem pbbiBottom;
        private DevExpress.XtraEditors.PanelControl pnlDetail;
        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private System.Windows.Forms.TreeView trvLayer;
        private System.Windows.Forms.ImageList imglstTreeview;
        private DevExpress.XtraEditors.SearchControl scQuery;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.XtraBars.BarButtonItem bbiCustomItem;
    }
}