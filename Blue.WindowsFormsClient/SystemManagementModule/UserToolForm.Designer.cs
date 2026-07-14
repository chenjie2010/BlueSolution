namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class UserToolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserToolForm));
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiLoad = new DevExpress.XtraBars.BarButtonItem();
            this.biiCellFormat = new DevExpress.XtraBars.BarButtonItem();
            this.bbiVerify = new DevExpress.XtraBars.BarButtonItem();
            this.bbiShowResult = new DevExpress.XtraBars.BarButtonItem();
            this.bbiImport = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.bbiErrorData = new DevExpress.XtraBars.BarButtonItem();
            this.bbiErrorDataImported = new DevExpress.XtraBars.BarButtonItem();
            this.bbiUserTemplate = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.icImportedMode = new DevExpress.Utils.ImageCollection(this.components);
            this.defaultBarAndDockingController = new DevExpress.XtraBars.DefaultBarAndDockingController(this.components);
            this.nbcData = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgUserInfo = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiUsersImported = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiUserDeleted = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgExChange = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiUserName = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiUserActualName = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiUserIdentity = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiTelephone = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiUserType = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiUserDepartment = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiPassword = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiEmailAddress = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarSeparatorItem1 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.navBarSeparatorItem2 = new DevExpress.XtraNavBar.NavBarSeparatorItem();
            this.icUser = new DevExpress.Utils.ImageCollection(this.components);
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.lblToolInfo = new DevExpress.XtraEditors.LabelControl();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.fpError = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl2 = new DevExpress.Utils.FlyoutPanelControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblErrorTip = new DevExpress.XtraEditors.LabelControl();
            this.fpUserTool = new FarPoint.Win.Spread.FpSpread();
            this.fpUserTool_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.mtxtTip = new DevExpress.XtraEditors.MemoEdit();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icImportedMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController.Controller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbcData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpError)).BeginInit();
            this.fpError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).BeginInit();
            this.flyoutPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpUserTool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUserTool_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtxtTip.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.icTools;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiLoad,
            this.bbiVerify,
            this.bbiClose,
            this.bbiImport,
            this.bbiErrorData,
            this.bbiShowResult,
            this.bbiSaveAs,
            this.bbiErrorDataImported,
            this.biiCellFormat,
            this.bbiUserTemplate});
            this.barManager.MaxItemId = 12;
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiLoad, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.biiCellFormat, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bbiVerify, "数据校验(&V)", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiShowResult, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bbiImport, "导入数据(&D)", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiSaveAs, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bbiErrorData, "校验错误数据另存...(&R)", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bbiErrorDataImported, "导入错误数据另存...(&T)", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiUserTemplate, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Tools";
            // 
            // bbiLoad
            // 
            this.bbiLoad.Caption = "加载Excel...(&I)";
            this.bbiLoad.Id = 0;
            this.bbiLoad.ImageIndex = 0;
            this.bbiLoad.Name = "bbiLoad";
            this.bbiLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLoad_ItemClick);
            // 
            // biiCellFormat
            // 
            this.biiCellFormat.Caption = "设置单元格格式...(&X)";
            this.biiCellFormat.Id = 10;
            this.biiCellFormat.ImageIndex = 1;
            this.biiCellFormat.Name = "biiCellFormat";
            this.biiCellFormat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biiCellFormat_ItemClick);
            // 
            // bbiVerify
            // 
            this.bbiVerify.Caption = "barButtonItem2";
            this.bbiVerify.Id = 1;
            this.bbiVerify.ImageIndex = 2;
            this.bbiVerify.Name = "bbiVerify";
            this.bbiVerify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiVerify_ItemClick);
            // 
            // bbiShowResult
            // 
            this.bbiShowResult.Caption = "显示校验结果(&E)";
            this.bbiShowResult.Id = 5;
            this.bbiShowResult.ImageIndex = 3;
            this.bbiShowResult.Name = "bbiShowResult";
            this.bbiShowResult.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiShowResult_ItemClick);
            // 
            // bbiImport
            // 
            this.bbiImport.Caption = "导入数据(&E)";
            this.bbiImport.Id = 3;
            this.bbiImport.ImageIndex = 4;
            this.bbiImport.Name = "bbiImport";
            this.bbiImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiImport_ItemClick);
            // 
            // bbiSaveAs
            // 
            this.bbiSaveAs.Caption = "Excel数据另存...(&S)";
            this.bbiSaveAs.Id = 6;
            this.bbiSaveAs.ImageIndex = 5;
            this.bbiSaveAs.Name = "bbiSaveAs";
            this.bbiSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSaveAs_ItemClick);
            // 
            // bbiErrorData
            // 
            this.bbiErrorData.Caption = "校验错误数据另存...(&R)";
            this.bbiErrorData.Id = 4;
            this.bbiErrorData.ImageIndex = 6;
            this.bbiErrorData.Name = "bbiErrorData";
            this.bbiErrorData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiErrorData_ItemClick);
            // 
            // bbiErrorDataImported
            // 
            this.bbiErrorDataImported.Caption = "导入错误数据另存(&T)";
            this.bbiErrorDataImported.Id = 7;
            this.bbiErrorDataImported.ImageIndex = 7;
            this.bbiErrorDataImported.Name = "bbiErrorDataImported";
            this.bbiErrorDataImported.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiErrorDataImported_ItemClick);
            // 
            // bbiUserTemplate
            // 
            this.bbiUserTemplate.Caption = "批量用户模板(&U)";
            this.bbiUserTemplate.Id = 11;
            this.bbiUserTemplate.ImageIndex = 8;
            this.bbiUserTemplate.Name = "bbiUserTemplate";
            this.bbiUserTemplate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiUserTemplate_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.Caption = "关闭(&C)";
            this.bbiClose.Id = 2;
            this.bbiClose.ImageIndex = 9;
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1288, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 683);
            this.barDockControlBottom.Size = new System.Drawing.Size(1288, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 657);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1288, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 657);
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "Tools_Import.png");
            this.icTools.Images.SetKeyName(1, "Tool_Text.png");
            this.icTools.Images.SetKeyName(2, "Tools_Check.png");
            this.icTools.Images.SetKeyName(3, "Tools_Display.png");
            this.icTools.Images.SetKeyName(4, "Tools_Save.png");
            this.icTools.Images.SetKeyName(5, "Tool_Save_AS.png");
            this.icTools.Images.SetKeyName(6, "Tools_Tips.png");
            this.icTools.Images.SetKeyName(7, "Tools_Alternative_Tips.png");
            this.icTools.Images.SetKeyName(8, "User_Tool_Template.png");
            this.icTools.Images.SetKeyName(9, "Common_Close_1.png");
            // 
            // icImportedMode
            // 
            this.icImportedMode.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icImportedMode.ImageStream")));
            this.icImportedMode.Images.SetKeyName(0, "ImportedMode_Append.png");
            this.icImportedMode.Images.SetKeyName(1, "ImportedMode_UpdateAndInsert.png");
            this.icImportedMode.Images.SetKeyName(2, "ImportedMode_UpdateAndNotInsert.png");
            this.icImportedMode.Images.SetKeyName(3, "ImportedMode_NotUpdateAndInsert.png");
            // 
            // defaultBarAndDockingController
            // 
            this.defaultBarAndDockingController.Controller.LookAndFeel.SkinName = "Money Twins";
            this.defaultBarAndDockingController.Controller.LookAndFeel.UseDefaultLookAndFeel = false;
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // nbcData
            // 
            this.nbcData.ActiveGroup = this.nbgUserInfo;
            this.nbcData.AllowDrop = false;
            this.nbcData.Dock = System.Windows.Forms.DockStyle.Left;
            this.nbcData.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.None;
            this.nbcData.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgUserInfo,
            this.nbgExChange});
            this.nbcData.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarSeparatorItem1,
            this.navBarSeparatorItem2,
            this.nbiUserName,
            this.nbiUserActualName,
            this.nbiUserIdentity,
            this.nbiUserType,
            this.nbiUserDepartment,
            this.nbiTelephone,
            this.nbiPassword,
            this.nbiUsersImported,
            this.nbiUserDeleted,
            this.nbiEmailAddress});
            this.nbcData.Location = new System.Drawing.Point(0, 26);
            this.nbcData.Name = "nbcData";
            this.nbcData.OptionsNavPane.ExpandedWidth = 208;
            this.nbcData.OptionsNavPane.ShowExpandButton = false;
            this.nbcData.OptionsNavPane.ShowGroupImageInHeader = true;
            this.nbcData.OptionsNavPane.ShowOverflowButton = false;
            this.nbcData.OptionsNavPane.ShowOverflowPanel = false;
            this.nbcData.OptionsNavPane.ShowSplitter = false;
            this.nbcData.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.nbcData.Size = new System.Drawing.Size(208, 657);
            this.nbcData.SmallImages = this.icUser;
            this.nbcData.StoreDefaultPaintStyleName = true;
            this.nbcData.TabIndex = 0;
            this.nbcData.Text = "navBarControl1";
            // 
            // nbgUserInfo
            // 
            this.nbgUserInfo.Caption = "用户信息处理";
            this.nbgUserInfo.Expanded = true;
            this.nbgUserInfo.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiUsersImported),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiUserDeleted)});
            this.nbgUserInfo.Name = "nbgUserInfo";
            this.nbgUserInfo.SmallImageIndex = 0;
            // 
            // nbiUsersImported
            // 
            this.nbiUsersImported.Caption = "批量用户导入";
            this.nbiUsersImported.Name = "nbiUsersImported";
            this.nbiUsersImported.SmallImageIndex = 13;
            this.nbiUsersImported.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiUsersImported_LinkClicked);
            // 
            // nbiUserDeleted
            // 
            this.nbiUserDeleted.Caption = "批量用户删除";
            this.nbiUserDeleted.Name = "nbiUserDeleted";
            this.nbiUserDeleted.SmallImageIndex = 14;
            this.nbiUserDeleted.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiUserDeleted_LinkClicked);
            // 
            // nbgExChange
            // 
            this.nbgExChange.Caption = "用户信息修改";
            this.nbgExChange.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiUserName),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiUserActualName),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiUserIdentity),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiTelephone),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiUserType),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiUserDepartment),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiPassword),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiEmailAddress)});
            this.nbgExChange.Name = "nbgExChange";
            this.nbgExChange.SmallImageIndex = 1;
            // 
            // nbiUserName
            // 
            this.nbiUserName.Caption = "用户名更新";
            this.nbiUserName.Name = "nbiUserName";
            this.nbiUserName.SmallImageIndex = 2;
            this.nbiUserName.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiUserName_LinkClicked);
            // 
            // nbiUserActualName
            // 
            this.nbiUserActualName.Caption = "用户姓名更新";
            this.nbiUserActualName.Name = "nbiUserActualName";
            this.nbiUserActualName.SmallImageIndex = 3;
            this.nbiUserActualName.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiUserActualName_LinkClicked);
            // 
            // nbiUserIdentity
            // 
            this.nbiUserIdentity.Caption = "身份证号码更新";
            this.nbiUserIdentity.Name = "nbiUserIdentity";
            this.nbiUserIdentity.SmallImageIndex = 4;
            this.nbiUserIdentity.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiUserIdentity_LinkClicked);
            // 
            // nbiTelephone
            // 
            this.nbiTelephone.Caption = "手机号码更新";
            this.nbiTelephone.Name = "nbiTelephone";
            this.nbiTelephone.SmallImageIndex = 5;
            this.nbiTelephone.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiTelephone_LinkClicked);
            // 
            // nbiUserType
            // 
            this.nbiUserType.Caption = "用户类型更新";
            this.nbiUserType.Name = "nbiUserType";
            this.nbiUserType.SmallImageIndex = 6;
            this.nbiUserType.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiUserType_LinkClicked);
            // 
            // nbiUserDepartment
            // 
            this.nbiUserDepartment.Caption = "用户单位更新";
            this.nbiUserDepartment.Name = "nbiUserDepartment";
            this.nbiUserDepartment.SmallImageIndex = 7;
            this.nbiUserDepartment.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiUserDepartment_LinkClicked);
            // 
            // nbiPassword
            // 
            this.nbiPassword.Caption = "用户密码更新";
            this.nbiPassword.Name = "nbiPassword";
            this.nbiPassword.SmallImageIndex = 12;
            this.nbiPassword.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiPassword_LinkClicked);
            // 
            // nbiEmailAddress
            // 
            this.nbiEmailAddress.Caption = "用户邮件地址更新";
            this.nbiEmailAddress.Name = "nbiEmailAddress";
            this.nbiEmailAddress.SmallImageIndex = 15;
            this.nbiEmailAddress.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiEmailAddress_LinkClicked);
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
            // icUser
            // 
            this.icUser.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icUser.ImageStream")));
            this.icUser.Images.SetKeyName(0, "User_Tool_Users.png");
            this.icUser.Images.SetKeyName(1, "User_Tools_1.png");
            this.icUser.Images.SetKeyName(2, "User_Tools_2.png");
            this.icUser.Images.SetKeyName(3, "User_Tools_3.png");
            this.icUser.Images.SetKeyName(4, "User_Tools_Identity.png");
            this.icUser.Images.SetKeyName(5, "User_Tools_Telephone.png");
            this.icUser.Images.SetKeyName(6, "User_Tools_4.png");
            this.icUser.Images.SetKeyName(7, "User_Tools_5.png");
            this.icUser.Images.SetKeyName(8, "User_Tools_6.png");
            this.icUser.Images.SetKeyName(9, "User_Tools_7.png");
            this.icUser.Images.SetKeyName(10, "User_Tools_8.png");
            this.icUser.Images.SetKeyName(11, "User_Tools_Id.png");
            this.icUser.Images.SetKeyName(12, "User_Tool_Password.png");
            this.icUser.Images.SetKeyName(13, "User_Tool_Users_Imported.png");
            this.icUser.Images.SetKeyName(14, "User_Tool_User_Deleted.png");
            this.icUser.Images.SetKeyName(15, "User_Tool_Email.png");
            // 
            // gcMain
            // 
            this.gcMain.CaptionImageUri.Uri = "CustomizeGrid";
            this.gcMain.Controls.Add(this.lblToolInfo);
            this.gcMain.Controls.Add(this.lblTip);
            this.gcMain.Controls.Add(this.pnlMain);
            this.gcMain.Controls.Add(this.pnlTop);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(208, 26);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(1080, 657);
            this.gcMain.TabIndex = 9;
            this.gcMain.Text = "批量用户导入";
            // 
            // lblToolInfo
            // 
            this.lblToolInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToolInfo.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblToolInfo.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Tip_Message;
            this.lblToolInfo.Appearance.Options.UseForeColor = true;
            this.lblToolInfo.Appearance.Options.UseImage = true;
            this.lblToolInfo.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblToolInfo.Location = new System.Drawing.Point(444, 131);
            this.lblToolInfo.Name = "lblToolInfo";
            this.lblToolInfo.Size = new System.Drawing.Size(117, 20);
            this.lblToolInfo.TabIndex = 4;
            this.lblToolInfo.Text = "未进行数据校验。";
            // 
            // lblTip
            // 
            this.lblTip.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblTip.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Client_Common_Warning_16;
            this.lblTip.Appearance.Options.UseForeColor = true;
            this.lblTip.Appearance.Options.UseImage = true;
            this.lblTip.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblTip.Location = new System.Drawing.Point(10, 131);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(337, 20);
            this.lblTip.TabIndex = 3;
            this.lblTip.Text = "提示：Excel文件第一行作为标题行，请勿作为数据行使用。";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.fpError);
            this.pnlMain.Controls.Add(this.fpUserTool);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMain.Location = new System.Drawing.Point(2, 158);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1076, 497);
            this.pnlMain.TabIndex = 1;
            // 
            // fpError
            // 
            this.fpError.Controls.Add(this.flyoutPanelControl2);
            this.fpError.Location = new System.Drawing.Point(186, 175);
            this.fpError.Name = "fpError";
            this.fpError.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("Button", global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16, false, true, "")});
            this.fpError.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpError.OwnerControl = this.fpUserTool;
            this.fpError.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.fpError.Size = new System.Drawing.Size(557, 203);
            this.fpError.TabIndex = 15;
            this.fpError.ButtonClick += new DevExpress.Utils.FlyoutPanelButtonClickEventHandler(this.fpError_ButtonClick);
            // 
            // flyoutPanelControl2
            // 
            this.flyoutPanelControl2.Controls.Add(this.gridControl);
            this.flyoutPanelControl2.Controls.Add(this.panelControl2);
            this.flyoutPanelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flyoutPanelControl2.FlyoutPanel = this.fpError;
            this.flyoutPanelControl2.Location = new System.Drawing.Point(0, 30);
            this.flyoutPanelControl2.Name = "flyoutPanelControl2";
            this.flyoutPanelControl2.Size = new System.Drawing.Size(557, 173);
            this.flyoutPanelControl2.TabIndex = 0;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(2, 32);
            this.gridControl.LookAndFeel.SkinName = "Money Twins";
            this.gridControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(553, 139);
            this.gridControl.TabIndex = 5;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.IndicatorWidth = 40;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView.OptionsFilter.AllowFilterEditor = false;
            this.gridView.OptionsFilter.AllowMRUFilterList = false;
            this.gridView.OptionsFind.AllowFindPanel = false;
            this.gridView.OptionsMenu.EnableColumnMenu = false;
            this.gridView.OptionsMenu.EnableFooterMenu = false;
            this.gridView.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView_RowClick);
            this.gridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.lblErrorTip);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(553, 30);
            this.panelControl2.TabIndex = 3;
            // 
            // lblErrorTip
            // 
            this.lblErrorTip.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Information_16;
            this.lblErrorTip.Appearance.Options.UseImage = true;
            this.lblErrorTip.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblErrorTip.Location = new System.Drawing.Point(6, 5);
            this.lblErrorTip.Name = "lblErrorTip";
            this.lblErrorTip.Size = new System.Drawing.Size(69, 20);
            this.lblErrorTip.TabIndex = 0;
            this.lblErrorTip.Text = "校验结果";
            // 
            // fpUserTool
            // 
            this.fpUserTool.AccessibleDescription = "";
            this.fpUserTool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpUserTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpUserTool.Location = new System.Drawing.Point(2, 2);
            this.fpUserTool.Name = "fpUserTool";
            this.fpUserTool.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpUserTool_Sheet1});
            this.fpUserTool.Size = new System.Drawing.Size(1072, 493);
            this.fpUserTool.TabIndex = 2;
            this.fpUserTool.TabStripInsertTab = false;
            this.fpUserTool.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            // 
            // fpUserTool_Sheet1
            // 
            this.fpUserTool_Sheet1.Reset();
            this.fpUserTool_Sheet1.SheetName = "Sheet1";
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.mtxtTip);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(2, 23);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(2, 4, 0, 0);
            this.pnlTop.Size = new System.Drawing.Size(1076, 102);
            this.pnlTop.TabIndex = 0;
            // 
            // mtxtTip
            // 
            this.mtxtTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxtTip.EditValue = resources.GetString("mtxtTip.EditValue");
            this.mtxtTip.Location = new System.Drawing.Point(4, 6);
            this.mtxtTip.MenuManager = this.barManager;
            this.mtxtTip.Name = "mtxtTip";
            this.mtxtTip.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.mtxtTip.Properties.Appearance.ForeColor = System.Drawing.SystemColors.Highlight;
            this.mtxtTip.Properties.Appearance.Options.UseBackColor = true;
            this.mtxtTip.Properties.Appearance.Options.UseForeColor = true;
            this.mtxtTip.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.mtxtTip.Properties.ReadOnly = true;
            this.mtxtTip.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtTip.Size = new System.Drawing.Size(1070, 94);
            this.mtxtTip.TabIndex = 2;
            this.mtxtTip.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.mtxtTip.ToolTipTitle = "提示";
            // 
            // openFileDialog
            // 
            this.openFileDialog.RestoreDirectory = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // UserToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 683);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.nbcData);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserToolForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户工具";
            this.Load += new System.EventHandler(this.UserToolForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icImportedMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController.Controller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbcData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            this.gcMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpError)).EndInit();
            this.fpError.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutPanelControl2)).EndInit();
            this.flyoutPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpUserTool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpUserTool_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mtxtTip.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem bbiLoad;
        private DevExpress.XtraBars.BarButtonItem bbiVerify;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.XtraBars.DefaultBarAndDockingController defaultBarAndDockingController;
        private DevExpress.XtraBars.BarButtonItem bbiImport;
        private DevExpress.XtraBars.BarButtonItem bbiErrorData;
        private DevExpress.XtraNavBar.NavBarControl nbcData;
        private DevExpress.XtraNavBar.NavBarGroup nbgExChange;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem1;
        private DevExpress.XtraNavBar.NavBarSeparatorItem navBarSeparatorItem2;
        private DevExpress.Utils.ImageCollection icUser;
        private DevExpress.XtraNavBar.NavBarItem nbiUserName;
        private DevExpress.XtraNavBar.NavBarItem nbiUserActualName;
        private DevExpress.XtraNavBar.NavBarItem nbiUserIdentity;
        private DevExpress.XtraNavBar.NavBarItem nbiTelephone;
        private DevExpress.XtraNavBar.NavBarItem nbiUserType;
        private DevExpress.XtraNavBar.NavBarItem nbiUserDepartment;
        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.MemoEdit mtxtTip;
        private FarPoint.Win.Spread.FpSpread fpUserTool;
        private FarPoint.Win.Spread.SheetView fpUserTool_Sheet1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private DevExpress.Utils.FlyoutPanel fpError;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblErrorTip;
        private DevExpress.XtraBars.BarButtonItem bbiShowResult;
        private DevExpress.XtraNavBar.NavBarItem nbiPassword;
        private DevExpress.XtraNavBar.NavBarGroup nbgUserInfo;
        private DevExpress.XtraNavBar.NavBarItem nbiUsersImported;
        private DevExpress.XtraNavBar.NavBarItem nbiUserDeleted;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraBars.BarButtonItem bbiSaveAs;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraNavBar.NavBarItem nbiEmailAddress;
        private DevExpress.XtraEditors.LabelControl lblToolInfo;
        private DevExpress.XtraBars.BarButtonItem bbiErrorDataImported;
        private DevExpress.Utils.ImageCollection icImportedMode;
        private DevExpress.XtraBars.BarButtonItem biiCellFormat;
        private DevExpress.XtraBars.BarButtonItem bbiUserTemplate;
    }
}