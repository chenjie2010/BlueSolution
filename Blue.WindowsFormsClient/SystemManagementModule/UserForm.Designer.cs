namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class UserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barTools = new DevExpress.XtraBars.Bar();
            this.bbiCreate = new DevExpress.XtraBars.BarButtonItem();
            this.bbiEdit = new DevExpress.XtraBars.BarButtonItem();
            this.blcDelete = new DevExpress.XtraBars.BarLinkContainerItem();
            this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBatchDelete = new DevExpress.XtraBars.BarButtonItem();
            this.blcLock = new DevExpress.XtraBars.BarLinkContainerItem();
            this.bbiLock = new DevExpress.XtraBars.BarButtonItem();
            this.bbiUnLock = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBatchLock = new DevExpress.XtraBars.BarButtonItem();
            this.bbiBatchUnLock = new DevExpress.XtraBars.BarButtonItem();
            this.bbiImportPhoto = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExchange = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRemoteUser = new DevExpress.XtraBars.BarButtonItem();
            this.bbiExportedUser = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.defaultBarAndDockingController = new DevExpress.XtraBars.DefaultBarAndDockingController(this.components);
            this.gcCondition = new DevExpress.XtraEditors.GroupControl();
            this.txtNewNotes = new DevExpress.XtraEditors.TextEdit();
            this.lblNewLocked = new DevExpress.XtraEditors.LabelControl();
            this.lblNewNotes = new DevExpress.XtraEditors.LabelControl();
            this.btxtNewRole = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.cmbQueriedDepartment = new Blue.WindowsFormsClient.TreeDropdownList();
            this.chkNewLocked = new DevExpress.XtraEditors.CheckEdit();
            this.cmbQueriedUserType = new Blue.WindowsFormsClient.TreeDropdownList();
            this.lblQueriedDepartment = new DevExpress.XtraEditors.LabelControl();
            this.lblQueriedUserType = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.icButtons = new DevExpress.Utils.ImageCollection(this.components);
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txtCondition = new DevExpress.XtraEditors.TextEdit();
            this.lblCondition = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.grdUsers = new AppFramework.WinFormsControls.DevExpressGrid();
            this.gcRight = new DevExpress.XtraEditors.GroupControl();
            this.upfPhoto = new AppFramework.WinFormsControls.DevExpressUploadFile();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblNameTip = new DevExpress.XtraEditors.LabelControl();
            this.cmbDepartment = new Blue.WindowsFormsClient.TreeDropdownList();
            this.lblNotes = new DevExpress.XtraEditors.LabelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.gcAuthority = new DevExpress.XtraEditors.GroupControl();
            this.lblQueryAttributes = new DevExpress.XtraEditors.LabelControl();
            this.ccmbDepartmentRange = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.btxtRole = new DevExpress.XtraEditors.ButtonEdit();
            this.ccmbAuthority = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.btxtDepartmentRange = new DevExpress.XtraEditors.ButtonEdit();
            this.btxtUserTypeRange = new DevExpress.XtraEditors.ButtonEdit();
            this.lblRole = new DevExpress.XtraEditors.LabelControl();
            this.lblAuthority = new DevExpress.XtraEditors.LabelControl();
            this.lblDepartmentRange = new DevExpress.XtraEditors.LabelControl();
            this.lblUserTypeRange = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.chkLocked = new DevExpress.XtraEditors.CheckEdit();
            this.cmbUserType = new Blue.WindowsFormsClient.TreeDropdownList();
            this.txtUserIdentity = new DevExpress.XtraEditors.TextEdit();
            this.txtTelephoneNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtConfirmedUserPwd = new DevExpress.XtraEditors.TextEdit();
            this.txtUserActualName = new DevExpress.XtraEditors.TextEdit();
            this.txtEmailAddress = new DevExpress.XtraEditors.TextEdit();
            this.peUser = new DevExpress.XtraEditors.PictureEdit();
            this.txtUserPwd = new DevExpress.XtraEditors.TextEdit();
            this.lblLocked = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.lblDepartment = new DevExpress.XtraEditors.LabelControl();
            this.icmbIdentificationType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icIdentificationType = new DevExpress.Utils.ImageCollection(this.components);
            this.lblEmailAddress = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lblUserType = new DevExpress.XtraEditors.LabelControl();
            this.lblUserPwd = new DevExpress.XtraEditors.LabelControl();
            this.lblConfirmedUserPwd = new DevExpress.XtraEditors.LabelControl();
            this.lblUserActualName = new DevExpress.XtraEditors.LabelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.sbtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.peTip = new DevExpress.XtraEditors.PictureEdit();
            this.scSecond = new DevExpress.XtraEditors.SeparatorControl();
            this.scFirst = new DevExpress.XtraEditors.SeparatorControl();
            this.icUserProperty = new DevExpress.Utils.ImageCollection(this.components);
            this.saveAttachmentFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController.Controller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).BeginInit();
            this.gcCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtNewRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNewLocked.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRight)).BeginInit();
            this.gcRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAuthority)).BeginInit();
            this.gcAuthority.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDepartmentRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbAuthority.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtDepartmentRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtUserTypeRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLocked.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserIdentity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephoneNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmedUserPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserActualName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbIdentificationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icIdentificationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peTip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icUserProperty)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.AllowShowToolbarsPopup = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTools});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.icTools;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiCreate,
            this.bbiEdit,
            this.barButtonItem3,
            this.blcDelete,
            this.bbiDelete,
            this.bbiBatchDelete,
            this.blcLock,
            this.bbiLock,
            this.bbiUnLock,
            this.bbiBatchLock,
            this.bbiBatchUnLock,
            this.bbiExchange,
            this.bbiImportPhoto,
            this.bbiRemoteUser,
            this.bbiExportedUser});
            this.barManager.MainMenu = this.barTools;
            this.barManager.MaxItemId = 17;
            this.barManager.OptionsLayout.AllowAddNewItems = false;
            // 
            // barTools
            // 
            this.barTools.BarName = "Tools";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 0;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.FloatLocation = new System.Drawing.Point(60, 140);
            this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bbiCreate, "增加(&Y)", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.blcDelete, "删除...(&C)", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.blcLock, "冻结...(&G)", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bbiImportPhoto, "导入照片(&P)", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiExchange, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiRemoteUser, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiExportedUser, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barTools.OptionsBar.AllowQuickCustomization = false;
            this.barTools.OptionsBar.DrawDragBorder = false;
            this.barTools.OptionsBar.MultiLine = true;
            this.barTools.OptionsBar.UseWholeRow = true;
            this.barTools.Text = "Tools";
            // 
            // bbiCreate
            // 
            this.bbiCreate.Caption = "barButtonItem1";
            this.bbiCreate.Id = 0;
            this.bbiCreate.ImageIndex = 0;
            this.bbiCreate.Name = "bbiCreate";
            this.bbiCreate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCreate_ItemClick);
            // 
            // bbiEdit
            // 
            this.bbiEdit.Caption = "编辑(&E)";
            this.bbiEdit.Enabled = false;
            this.bbiEdit.Id = 1;
            this.bbiEdit.ImageIndex = 1;
            this.bbiEdit.Name = "bbiEdit";
            this.bbiEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiEdit_ItemClick);
            // 
            // blcDelete
            // 
            this.blcDelete.Caption = "删除";
            this.blcDelete.Enabled = false;
            this.blcDelete.Id = 5;
            this.blcDelete.ImageIndex = 2;
            this.blcDelete.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.bbiDelete, "删除(&K)"),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiBatchDelete)});
            this.blcDelete.Name = "blcDelete";
            // 
            // bbiDelete
            // 
            this.bbiDelete.Caption = "barButtonItem1";
            this.bbiDelete.Id = 6;
            this.bbiDelete.ImageIndex = 3;
            this.bbiDelete.Name = "bbiDelete";
            this.bbiDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDelete_ItemClick);
            // 
            // bbiBatchDelete
            // 
            this.bbiBatchDelete.Caption = "批量删除(&M)";
            this.bbiBatchDelete.Id = 7;
            this.bbiBatchDelete.ImageIndex = 4;
            this.bbiBatchDelete.Name = "bbiBatchDelete";
            this.bbiBatchDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiBatchDelete_ItemClick);
            // 
            // blcLock
            // 
            this.blcLock.Caption = "冻结..";
            this.blcLock.Enabled = false;
            this.blcLock.Id = 8;
            this.blcLock.ImageIndex = 5;
            this.blcLock.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.bbiLock, "冻结(&O)"),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.bbiUnLock, "解冻(&U)"),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bbiBatchLock, "批量冻结(&F)", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, this.bbiBatchUnLock, "批量解冻(&B)")});
            this.blcLock.Name = "blcLock";
            // 
            // bbiLock
            // 
            this.bbiLock.Caption = "barButtonItem4";
            this.bbiLock.Id = 9;
            this.bbiLock.ImageIndex = 6;
            this.bbiLock.Name = "bbiLock";
            this.bbiLock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLock_ItemClick);
            // 
            // bbiUnLock
            // 
            this.bbiUnLock.Caption = "barButtonItem6";
            this.bbiUnLock.Id = 10;
            this.bbiUnLock.ImageIndex = 7;
            this.bbiUnLock.Name = "bbiUnLock";
            this.bbiUnLock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiUnLock_ItemClick);
            // 
            // bbiBatchLock
            // 
            this.bbiBatchLock.Caption = "barButtonItem7";
            this.bbiBatchLock.Id = 11;
            this.bbiBatchLock.ImageIndex = 8;
            this.bbiBatchLock.Name = "bbiBatchLock";
            this.bbiBatchLock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiBatchLock_ItemClick);
            // 
            // bbiBatchUnLock
            // 
            this.bbiBatchUnLock.Caption = "barButtonItem8";
            this.bbiBatchUnLock.Id = 12;
            this.bbiBatchUnLock.ImageIndex = 9;
            this.bbiBatchUnLock.Name = "bbiBatchUnLock";
            this.bbiBatchUnLock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiBatchUnLock_ItemClick);
            // 
            // bbiImportPhoto
            // 
            this.bbiImportPhoto.Caption = "导入照片(&P)";
            this.bbiImportPhoto.Id = 14;
            this.bbiImportPhoto.ImageIndex = 11;
            this.bbiImportPhoto.Name = "bbiImportPhoto";
            this.bbiImportPhoto.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiImportPhoto_ItemClick);
            // 
            // bbiExchange
            // 
            this.bbiExchange.Caption = "用户工具(&I)";
            this.bbiExchange.Id = 13;
            this.bbiExchange.ImageIndex = 10;
            this.bbiExchange.Name = "bbiExchange";
            this.bbiExchange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExchange_ItemClick);
            // 
            // bbiRemoteUser
            // 
            this.bbiRemoteUser.Caption = "远程用户同步(&R)";
            this.bbiRemoteUser.Id = 15;
            this.bbiRemoteUser.ImageIndex = 12;
            this.bbiRemoteUser.Name = "bbiRemoteUser";
            this.bbiRemoteUser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRemoteUser_ItemClick);
            // 
            // bbiExportedUser
            // 
            this.bbiExportedUser.Caption = "导出用户(&X)";
            this.bbiExportedUser.Id = 16;
            this.bbiExportedUser.ImageIndex = 13;
            this.bbiExportedUser.Name = "bbiExportedUser";
            this.bbiExportedUser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiExportedUser_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1324, 28);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 688);
            this.barDockControlBottom.Size = new System.Drawing.Size(1324, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 28);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 660);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1324, 28);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 660);
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "User_Create.png");
            this.icTools.Images.SetKeyName(1, "User_Edit.png");
            this.icTools.Images.SetKeyName(2, "User_Delete.png");
            this.icTools.Images.SetKeyName(3, "User_Delete_Only.png");
            this.icTools.Images.SetKeyName(4, "User_Delete_Batch.png");
            this.icTools.Images.SetKeyName(5, "User_Freeze.png");
            this.icTools.Images.SetKeyName(6, "User_Freeze_Only.png");
            this.icTools.Images.SetKeyName(7, "User_Unlock.png");
            this.icTools.Images.SetKeyName(8, "User_Freeze_Batch.png");
            this.icTools.Images.SetKeyName(9, "User_BacthUnlock.png");
            this.icTools.Images.SetKeyName(10, "User_Exchange.png");
            this.icTools.Images.SetKeyName(11, "User_Photo.png");
            this.icTools.Images.SetKeyName(12, "User_Remote.png");
            this.icTools.Images.SetKeyName(13, "User_Export.png");
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "删除(&D)";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.ImageIndex = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // defaultBarAndDockingController
            // 
            this.defaultBarAndDockingController.Controller.LookAndFeel.SkinName = "Money Twins";
            this.defaultBarAndDockingController.Controller.LookAndFeel.UseDefaultLookAndFeel = false;
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
            this.defaultBarAndDockingController.Controller.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
            // 
            // gcCondition
            // 
            this.gcCondition.CaptionImage = ((System.Drawing.Image)(resources.GetObject("gcCondition.CaptionImage")));
            this.gcCondition.Controls.Add(this.txtNewNotes);
            this.gcCondition.Controls.Add(this.lblNewLocked);
            this.gcCondition.Controls.Add(this.lblNewNotes);
            this.gcCondition.Controls.Add(this.btxtNewRole);
            this.gcCondition.Controls.Add(this.labelControl11);
            this.gcCondition.Controls.Add(this.cmbQueriedDepartment);
            this.gcCondition.Controls.Add(this.chkNewLocked);
            this.gcCondition.Controls.Add(this.cmbQueriedUserType);
            this.gcCondition.Controls.Add(this.lblQueriedDepartment);
            this.gcCondition.Controls.Add(this.lblQueriedUserType);
            this.gcCondition.Controls.Add(this.btnClear);
            this.gcCondition.Controls.Add(this.btnQuery);
            this.gcCondition.Controls.Add(this.txtCondition);
            this.gcCondition.Controls.Add(this.lblCondition);
            this.gcCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcCondition.Location = new System.Drawing.Point(2, 2);
            this.gcCondition.Name = "gcCondition";
            this.gcCondition.Size = new System.Drawing.Size(925, 87);
            this.gcCondition.TabIndex = 13;
            this.gcCondition.Text = "用户查询";
            // 
            // txtNewNotes
            // 
            this.txtNewNotes.Location = new System.Drawing.Point(706, 59);
            this.txtNewNotes.MenuManager = this.barManager;
            this.txtNewNotes.Name = "txtNewNotes";
            this.txtNewNotes.Properties.MaxLength = 32;
            this.txtNewNotes.Size = new System.Drawing.Size(131, 20);
            this.txtNewNotes.TabIndex = 24;
            // 
            // lblNewLocked
            // 
            this.lblNewLocked.Location = new System.Drawing.Point(667, 30);
            this.lblNewLocked.Name = "lblNewLocked";
            this.lblNewLocked.Size = new System.Drawing.Size(36, 14);
            this.lblNewLocked.TabIndex = 53;
            this.lblNewLocked.Text = "冻结：";
            // 
            // lblNewNotes
            // 
            this.lblNewNotes.Location = new System.Drawing.Point(667, 61);
            this.lblNewNotes.Name = "lblNewNotes";
            this.lblNewNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNewNotes.TabIndex = 52;
            this.lblNewNotes.Text = "备注：";
            // 
            // btxtNewRole
            // 
            this.btxtNewRole.Location = new System.Drawing.Point(451, 58);
            this.btxtNewRole.MenuManager = this.barManager;
            this.btxtNewRole.Name = "btxtNewRole";
            this.btxtNewRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtNewRole.Size = new System.Drawing.Size(208, 20);
            this.btxtNewRole.TabIndex = 23;
            this.btxtNewRole.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtNewRole_ButtonPressed);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(387, 61);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(60, 14);
            this.labelControl11.TabIndex = 47;
            this.labelControl11.Text = "用户角色：";
            // 
            // cmbQueriedDepartment
            // 
            this.cmbQueriedDepartment.Location = new System.Drawing.Point(70, 59);
            this.cmbQueriedDepartment.Name = "cmbQueriedDepartment";
            this.cmbQueriedDepartment.ShowSearch = true;
            this.cmbQueriedDepartment.Size = new System.Drawing.Size(309, 21);
            this.cmbQueriedDepartment.SkinName = "Blue";
            this.cmbQueriedDepartment.TabIndex = 21;
            this.cmbQueriedDepartment.TreeDropdownHandler = null;
            // 
            // chkNewLocked
            // 
            this.chkNewLocked.Location = new System.Drawing.Point(706, 28);
            this.chkNewLocked.MenuManager = this.barManager;
            this.chkNewLocked.Name = "chkNewLocked";
            this.chkNewLocked.Properties.Caption = "";
            this.chkNewLocked.Size = new System.Drawing.Size(24, 19);
            this.chkNewLocked.TabIndex = 25;
            // 
            // cmbQueriedUserType
            // 
            this.cmbQueriedUserType.Location = new System.Drawing.Point(451, 28);
            this.cmbQueriedUserType.Name = "cmbQueriedUserType";
            this.cmbQueriedUserType.OnlySelectedLeaf = true;
            this.cmbQueriedUserType.Size = new System.Drawing.Size(208, 21);
            this.cmbQueriedUserType.SkinName = "Blue";
            this.cmbQueriedUserType.TabIndex = 22;
            this.cmbQueriedUserType.TreeDropdownHandler = null;
            // 
            // lblQueriedDepartment
            // 
            this.lblQueriedDepartment.Location = new System.Drawing.Point(7, 61);
            this.lblQueriedDepartment.Name = "lblQueriedDepartment";
            this.lblQueriedDepartment.Size = new System.Drawing.Size(60, 14);
            this.lblQueriedDepartment.TabIndex = 9;
            this.lblQueriedDepartment.Text = "所属单位：";
            // 
            // lblQueriedUserType
            // 
            this.lblQueriedUserType.Location = new System.Drawing.Point(387, 31);
            this.lblQueriedUserType.Name = "lblQueriedUserType";
            this.lblQueriedUserType.Size = new System.Drawing.Size(60, 14);
            this.lblQueriedUserType.TabIndex = 7;
            this.lblQueriedUserType.Text = "用户类型：";
            // 
            // btnClear
            // 
            this.btnClear.ImageIndex = 1;
            this.btnClear.ImageList = this.icButtons;
            this.btnClear.Location = new System.Drawing.Point(845, 57);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "清除(&R)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // icButtons
            // 
            this.icButtons.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icButtons.ImageStream")));
            this.icButtons.Images.SetKeyName(0, "Button_Query.png");
            this.icButtons.Images.SetKeyName(1, "Button_Remove_Small.png");
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 0;
            this.btnQuery.ImageList = this.icButtons;
            this.btnQuery.Location = new System.Drawing.Point(845, 28);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtCondition
            // 
            this.txtCondition.EditValue = "";
            this.txtCondition.Location = new System.Drawing.Point(70, 29);
            this.txtCondition.MenuManager = this.barManager;
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Properties.MaxLength = 32;
            this.txtCondition.Properties.NullValuePrompt = "请输入用户名、姓名、电子邮件、手机号码或证件号码";
            this.txtCondition.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCondition.Size = new System.Drawing.Size(309, 20);
            this.txtCondition.TabIndex = 20;
            this.txtCondition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCondition_KeyPress);
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(7, 32);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(60, 14);
            this.lblCondition.TabIndex = 0;
            this.lblCondition.Text = "查询条件：";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gcMain);
            this.pnlMain.Controls.Add(this.gcCondition);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(395, 28);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(929, 660);
            this.pnlMain.TabIndex = 15;
            // 
            // gcMain
            // 
            this.gcMain.CaptionImage = ((System.Drawing.Image)(resources.GetObject("gcMain.CaptionImage")));
            this.gcMain.Controls.Add(this.progressPanel);
            this.gcMain.Controls.Add(this.grdUsers);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 89);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(925, 569);
            this.gcMain.TabIndex = 15;
            this.gcMain.Text = "用户列表";
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "数据正在加载......";
            this.progressPanel.Location = new System.Drawing.Point(437, 260);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(150, 50);
            this.progressPanel.TabIndex = 8;
            this.progressPanel.Text = "数据加载中......";
            this.progressPanel.Visible = false;
            // 
            // grdUsers
            // 
            this.grdUsers.CheckboxColumnCaption = "多选项";
            this.grdUsers.ColumnHeaderTexts = new string[] {
        "用户名",
        "用户姓名",
        "用户类型名称",
        "单位名称",
        "证件类型",
        "身份证号",
        "是否锁定"};
            this.grdUsers.DataKeyNames = new string[] {
        "UserId"};
            this.grdUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUsers.ExportedExcel = false;
            this.grdUsers.FootText = null;
            this.grdUsers.ImportedExcel = false;
            this.grdUsers.IsMainTable = false;
            this.grdUsers.IsShowCheckBox = true;
            this.grdUsers.Location = new System.Drawing.Point(2, 23);
            this.grdUsers.Name = "grdUsers";
            this.grdUsers.PageSize = 50;
            this.grdUsers.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grdUsers.Size = new System.Drawing.Size(921, 544);
            this.grdUsers.TabIndex = 7;
            this.grdUsers.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.grdUsers_OnPageIndexChanged);
            this.grdUsers.OnRowClick += new System.EventHandler<AppFramework.WinFormsControls.RowEvent>(this.grdUsers_OnRowClick);
            // 
            // gcRight
            // 
            this.gcRight.CaptionImage = ((System.Drawing.Image)(resources.GetObject("gcRight.CaptionImage")));
            this.gcRight.Controls.Add(this.upfPhoto);
            this.gcRight.Controls.Add(this.labelControl9);
            this.gcRight.Controls.Add(this.labelControl8);
            this.gcRight.Controls.Add(this.labelControl5);
            this.gcRight.Controls.Add(this.labelControl4);
            this.gcRight.Controls.Add(this.labelControl2);
            this.gcRight.Controls.Add(this.labelControl1);
            this.gcRight.Controls.Add(this.lblNameTip);
            this.gcRight.Controls.Add(this.cmbDepartment);
            this.gcRight.Controls.Add(this.lblNotes);
            this.gcRight.Controls.Add(this.txtNotes);
            this.gcRight.Controls.Add(this.gcAuthority);
            this.gcRight.Controls.Add(this.chkLocked);
            this.gcRight.Controls.Add(this.cmbUserType);
            this.gcRight.Controls.Add(this.txtUserIdentity);
            this.gcRight.Controls.Add(this.txtTelephoneNumber);
            this.gcRight.Controls.Add(this.txtConfirmedUserPwd);
            this.gcRight.Controls.Add(this.txtUserActualName);
            this.gcRight.Controls.Add(this.txtEmailAddress);
            this.gcRight.Controls.Add(this.peUser);
            this.gcRight.Controls.Add(this.txtUserPwd);
            this.gcRight.Controls.Add(this.lblLocked);
            this.gcRight.Controls.Add(this.labelControl10);
            this.gcRight.Controls.Add(this.lblDepartment);
            this.gcRight.Controls.Add(this.icmbIdentificationType);
            this.gcRight.Controls.Add(this.lblEmailAddress);
            this.gcRight.Controls.Add(this.labelControl7);
            this.gcRight.Controls.Add(this.labelControl6);
            this.gcRight.Controls.Add(this.lblUserType);
            this.gcRight.Controls.Add(this.lblUserPwd);
            this.gcRight.Controls.Add(this.lblConfirmedUserPwd);
            this.gcRight.Controls.Add(this.lblUserActualName);
            this.gcRight.Controls.Add(this.lblUserName);
            this.gcRight.Controls.Add(this.txtUserName);
            this.gcRight.Controls.Add(this.sbtnCancel);
            this.gcRight.Controls.Add(this.sbtnConfirm);
            this.gcRight.Controls.Add(this.panelControl3);
            this.gcRight.Controls.Add(this.scSecond);
            this.gcRight.Controls.Add(this.scFirst);
            this.gcRight.Dock = System.Windows.Forms.DockStyle.Left;
            this.gcRight.Location = new System.Drawing.Point(0, 28);
            this.gcRight.Name = "gcRight";
            this.gcRight.Size = new System.Drawing.Size(395, 660);
            this.gcRight.TabIndex = 9;
            this.gcRight.Text = "用户详细信息";
            // 
            // upfPhoto
            // 
            this.upfPhoto.DocType = AppFramework.WinFormsControls.DocType.PicAttachment;
            this.upfPhoto.Location = new System.Drawing.Point(76, 285);
            this.upfPhoto.Name = "upfPhoto";
            this.upfPhoto.ReadOnly = false;
            this.upfPhoto.ShowView = false;
            this.upfPhoto.Size = new System.Drawing.Size(291, 21);
            this.upfPhoto.TabIndex = 10;
            this.upfPhoto.OnBrowseClick += new System.EventHandler<System.EventArgs>(this.upfPhoto_OnBrowseClick);
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.Location = new System.Drawing.Point(373, 286);
            this.labelControl9.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(7, 14);
            this.labelControl9.TabIndex = 50;
            this.labelControl9.Text = "*";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Location = new System.Drawing.Point(372, 258);
            this.labelControl8.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(7, 14);
            this.labelControl8.TabIndex = 49;
            this.labelControl8.Text = "*";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(372, 230);
            this.labelControl5.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(7, 14);
            this.labelControl5.TabIndex = 48;
            this.labelControl5.Text = "*";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(372, 201);
            this.labelControl4.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(7, 14);
            this.labelControl4.TabIndex = 47;
            this.labelControl4.Text = "*";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(253, 143);
            this.labelControl2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(7, 14);
            this.labelControl2.TabIndex = 45;
            this.labelControl2.Text = "*";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(253, 115);
            this.labelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(7, 14);
            this.labelControl1.TabIndex = 44;
            this.labelControl1.Text = "*";
            // 
            // lblNameTip
            // 
            this.lblNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblNameTip.Appearance.Options.UseForeColor = true;
            this.lblNameTip.Location = new System.Drawing.Point(253, 32);
            this.lblNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblNameTip.Name = "lblNameTip";
            this.lblNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblNameTip.TabIndex = 43;
            this.lblNameTip.Text = "*";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.Location = new System.Drawing.Point(76, 255);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.ShowSearch = true;
            this.cmbDepartment.Size = new System.Drawing.Size(291, 24);
            this.cmbDepartment.SkinName = "Blue";
            this.cmbDepartment.TabIndex = 9;
            this.cmbDepartment.TreeDropdownHandler = null;
            // 
            // lblNotes
            // 
            this.lblNotes.Location = new System.Drawing.Point(17, 543);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(36, 14);
            this.lblNotes.TabIndex = 40;
            this.lblNotes.Text = "备注：";
            // 
            // txtNotes
            // 
            this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNotes.Location = new System.Drawing.Point(58, 542);
            this.txtNotes.MenuManager = this.barManager;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Properties.MaxLength = 256;
            this.txtNotes.Size = new System.Drawing.Size(322, 35);
            this.txtNotes.TabIndex = 17;
            // 
            // gcAuthority
            // 
            this.gcAuthority.CaptionImage = ((System.Drawing.Image)(resources.GetObject("gcAuthority.CaptionImage")));
            this.gcAuthority.Controls.Add(this.lblQueryAttributes);
            this.gcAuthority.Controls.Add(this.ccmbDepartmentRange);
            this.gcAuthority.Controls.Add(this.btxtRole);
            this.gcAuthority.Controls.Add(this.ccmbAuthority);
            this.gcAuthority.Controls.Add(this.btxtDepartmentRange);
            this.gcAuthority.Controls.Add(this.btxtUserTypeRange);
            this.gcAuthority.Controls.Add(this.lblRole);
            this.gcAuthority.Controls.Add(this.lblAuthority);
            this.gcAuthority.Controls.Add(this.lblDepartmentRange);
            this.gcAuthority.Controls.Add(this.lblUserTypeRange);
            this.gcAuthority.Controls.Add(this.separatorControl2);
            this.gcAuthority.Controls.Add(this.separatorControl1);
            this.gcAuthority.Location = new System.Drawing.Point(8, 349);
            this.gcAuthority.Name = "gcAuthority";
            this.gcAuthority.Size = new System.Drawing.Size(378, 187);
            this.gcAuthority.TabIndex = 39;
            this.gcAuthority.Text = "用户角色与权限";
            // 
            // lblQueryAttributes
            // 
            this.lblQueryAttributes.Location = new System.Drawing.Point(13, 160);
            this.lblQueryAttributes.Name = "lblQueryAttributes";
            this.lblQueryAttributes.Size = new System.Drawing.Size(60, 14);
            this.lblQueryAttributes.TabIndex = 45;
            this.lblQueryAttributes.Text = "查询属性：";
            // 
            // ccmbDepartmentRange
            // 
            this.ccmbDepartmentRange.Location = new System.Drawing.Point(79, 158);
            this.ccmbDepartmentRange.MenuManager = this.barManager;
            this.ccmbDepartmentRange.Name = "ccmbDepartmentRange";
            this.ccmbDepartmentRange.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbDepartmentRange.Properties.PopupSizeable = false;
            this.ccmbDepartmentRange.Properties.SelectAllItemVisible = false;
            this.ccmbDepartmentRange.Properties.ShowButtons = false;
            this.ccmbDepartmentRange.Size = new System.Drawing.Size(292, 20);
            this.ccmbDepartmentRange.TabIndex = 16;
            // 
            // btxtRole
            // 
            this.btxtRole.Location = new System.Drawing.Point(79, 27);
            this.btxtRole.MenuManager = this.barManager;
            this.btxtRole.Name = "btxtRole";
            this.btxtRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtRole.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtRole.Size = new System.Drawing.Size(292, 20);
            this.btxtRole.TabIndex = 12;
            this.btxtRole.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtRole_ButtonPressed);
            // 
            // ccmbAuthority
            // 
            this.ccmbAuthority.Location = new System.Drawing.Point(79, 56);
            this.ccmbAuthority.MenuManager = this.barManager;
            this.ccmbAuthority.Name = "ccmbAuthority";
            this.ccmbAuthority.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbAuthority.Properties.PopupSizeable = false;
            this.ccmbAuthority.Properties.SelectAllItemVisible = false;
            this.ccmbAuthority.Properties.ShowButtons = false;
            this.ccmbAuthority.Size = new System.Drawing.Size(292, 20);
            this.ccmbAuthority.TabIndex = 13;
            // 
            // btxtDepartmentRange
            // 
            this.btxtDepartmentRange.Location = new System.Drawing.Point(79, 121);
            this.btxtDepartmentRange.MenuManager = this.barManager;
            this.btxtDepartmentRange.Name = "btxtDepartmentRange";
            this.btxtDepartmentRange.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtDepartmentRange.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtDepartmentRange.Size = new System.Drawing.Size(292, 20);
            this.btxtDepartmentRange.TabIndex = 15;
            this.btxtDepartmentRange.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtDepartmentRange_ButtonPressed);
            // 
            // btxtUserTypeRange
            // 
            this.btxtUserTypeRange.Location = new System.Drawing.Point(79, 93);
            this.btxtUserTypeRange.MenuManager = this.barManager;
            this.btxtUserTypeRange.Name = "btxtUserTypeRange";
            this.btxtUserTypeRange.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btxtUserTypeRange.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btxtUserTypeRange.Size = new System.Drawing.Size(292, 20);
            this.btxtUserTypeRange.TabIndex = 14;
            this.btxtUserTypeRange.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btxtUserTypeRange_ButtonPressed);
            // 
            // lblRole
            // 
            this.lblRole.Location = new System.Drawing.Point(13, 31);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(60, 14);
            this.lblRole.TabIndex = 39;
            this.lblRole.Text = "用户角色：";
            // 
            // lblAuthority
            // 
            this.lblAuthority.Location = new System.Drawing.Point(13, 57);
            this.lblAuthority.Name = "lblAuthority";
            this.lblAuthority.Size = new System.Drawing.Size(60, 14);
            this.lblAuthority.TabIndex = 36;
            this.lblAuthority.Text = "用户权限：";
            // 
            // lblDepartmentRange
            // 
            this.lblDepartmentRange.Location = new System.Drawing.Point(13, 124);
            this.lblDepartmentRange.Name = "lblDepartmentRange";
            this.lblDepartmentRange.Size = new System.Drawing.Size(60, 14);
            this.lblDepartmentRange.TabIndex = 38;
            this.lblDepartmentRange.Text = "管理单位：";
            // 
            // lblUserTypeRange
            // 
            this.lblUserTypeRange.Location = new System.Drawing.Point(13, 96);
            this.lblUserTypeRange.Name = "lblUserTypeRange";
            this.lblUserTypeRange.Size = new System.Drawing.Size(60, 14);
            this.lblUserTypeRange.TabIndex = 37;
            this.lblUserTypeRange.Text = "管理类型：";
            // 
            // separatorControl2
            // 
            this.separatorControl2.Location = new System.Drawing.Point(4, 73);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Size = new System.Drawing.Size(370, 23);
            this.separatorControl2.TabIndex = 44;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(5, 138);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(370, 23);
            this.separatorControl1.TabIndex = 46;
            // 
            // chkLocked
            // 
            this.chkLocked.Location = new System.Drawing.Point(76, 312);
            this.chkLocked.MenuManager = this.barManager;
            this.chkLocked.Name = "chkLocked";
            this.chkLocked.Properties.Caption = "";
            this.chkLocked.Size = new System.Drawing.Size(17, 19);
            this.chkLocked.TabIndex = 11;
            // 
            // cmbUserType
            // 
            this.cmbUserType.Location = new System.Drawing.Point(76, 225);
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.OnlySelectedLeaf = true;
            this.cmbUserType.Size = new System.Drawing.Size(291, 24);
            this.cmbUserType.SkinName = "Blue";
            this.cmbUserType.TabIndex = 8;
            this.cmbUserType.TreeDropdownHandler = null;
            // 
            // txtUserIdentity
            // 
            this.txtUserIdentity.Location = new System.Drawing.Point(187, 196);
            this.txtUserIdentity.MenuManager = this.barManager;
            this.txtUserIdentity.Name = "txtUserIdentity";
            this.txtUserIdentity.Properties.MaxLength = 64;
            this.txtUserIdentity.Size = new System.Drawing.Size(180, 20);
            this.txtUserIdentity.TabIndex = 7;
            // 
            // txtTelephoneNumber
            // 
            this.txtTelephoneNumber.Location = new System.Drawing.Point(76, 166);
            this.txtTelephoneNumber.MenuManager = this.barManager;
            this.txtTelephoneNumber.Name = "txtTelephoneNumber";
            this.txtTelephoneNumber.Properties.MaxLength = 11;
            this.txtTelephoneNumber.Size = new System.Drawing.Size(173, 20);
            this.txtTelephoneNumber.TabIndex = 5;
            // 
            // txtConfirmedUserPwd
            // 
            this.txtConfirmedUserPwd.Location = new System.Drawing.Point(76, 83);
            this.txtConfirmedUserPwd.MenuManager = this.barManager;
            this.txtConfirmedUserPwd.Name = "txtConfirmedUserPwd";
            this.txtConfirmedUserPwd.Properties.MaxLength = 32;
            this.txtConfirmedUserPwd.Properties.PasswordChar = '*';
            this.txtConfirmedUserPwd.Size = new System.Drawing.Size(173, 20);
            this.txtConfirmedUserPwd.TabIndex = 2;
            // 
            // txtUserActualName
            // 
            this.txtUserActualName.Location = new System.Drawing.Point(76, 111);
            this.txtUserActualName.MenuManager = this.barManager;
            this.txtUserActualName.Name = "txtUserActualName";
            this.txtUserActualName.Properties.MaxLength = 128;
            this.txtUserActualName.Size = new System.Drawing.Size(173, 20);
            this.txtUserActualName.TabIndex = 3;
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Location = new System.Drawing.Point(76, 139);
            this.txtEmailAddress.MenuManager = this.barManager;
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Properties.MaxLength = 64;
            this.txtEmailAddress.Size = new System.Drawing.Size(173, 20);
            this.txtEmailAddress.TabIndex = 4;
            // 
            // peUser
            // 
            this.peUser.Cursor = System.Windows.Forms.Cursors.Default;
            this.peUser.Location = new System.Drawing.Point(266, 26);
            this.peUser.MenuManager = this.barManager;
            this.peUser.Name = "peUser";
            this.peUser.Properties.AllowScrollOnMouseWheel = DevExpress.Utils.DefaultBoolean.False;
            this.peUser.Properties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.False;
            this.peUser.Properties.NullText = "用户照片";
            this.peUser.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.peUser.Properties.ReadOnly = true;
            this.peUser.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peUser.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.peUser.Properties.ZoomAccelerationFactor = 1D;
            this.peUser.Size = new System.Drawing.Size(118, 161);
            this.peUser.TabIndex = 26;
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.Location = new System.Drawing.Point(76, 55);
            this.txtUserPwd.MenuManager = this.barManager;
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.Properties.PasswordChar = '*';
            this.txtUserPwd.Size = new System.Drawing.Size(173, 20);
            this.txtUserPwd.TabIndex = 1;
            // 
            // lblLocked
            // 
            this.lblLocked.Location = new System.Drawing.Point(34, 314);
            this.lblLocked.Name = "lblLocked";
            this.lblLocked.Size = new System.Drawing.Size(36, 14);
            this.lblLocked.TabIndex = 22;
            this.lblLocked.Text = "冻结：";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(10, 287);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(60, 14);
            this.labelControl10.TabIndex = 21;
            this.labelControl10.Text = "上传照片：";
            // 
            // lblDepartment
            // 
            this.lblDepartment.Location = new System.Drawing.Point(10, 258);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(60, 14);
            this.lblDepartment.TabIndex = 20;
            this.lblDepartment.Text = "所属单位：";
            // 
            // icmbIdentificationType
            // 
            this.icmbIdentificationType.Location = new System.Drawing.Point(76, 196);
            this.icmbIdentificationType.MenuManager = this.barManager;
            this.icmbIdentificationType.Name = "icmbIdentificationType";
            this.icmbIdentificationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbIdentificationType.Properties.SmallImages = this.icIdentificationType;
            this.icmbIdentificationType.Size = new System.Drawing.Size(105, 20);
            this.icmbIdentificationType.TabIndex = 6;
            // 
            // icIdentificationType
            // 
            this.icIdentificationType.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icIdentificationType.ImageStream")));
            this.icIdentificationType.Images.SetKeyName(0, "Control_Identity_One.jpg");
            this.icIdentificationType.Images.SetKeyName(1, "Control_Identity_Two.jpg");
            this.icIdentificationType.Images.SetKeyName(2, "Control_Identity_Other.png");
            // 
            // lblEmailAddress
            // 
            this.lblEmailAddress.Location = new System.Drawing.Point(10, 142);
            this.lblEmailAddress.Name = "lblEmailAddress";
            this.lblEmailAddress.Size = new System.Drawing.Size(60, 14);
            this.lblEmailAddress.TabIndex = 18;
            this.lblEmailAddress.Text = "电子邮件：";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(10, 170);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(60, 14);
            this.labelControl7.TabIndex = 17;
            this.labelControl7.Text = "手机号码：";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(10, 198);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "证件号码：";
            // 
            // lblUserType
            // 
            this.lblUserType.Location = new System.Drawing.Point(10, 228);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new System.Drawing.Size(60, 14);
            this.lblUserType.TabIndex = 15;
            this.lblUserType.Text = "用户类型：";
            // 
            // lblUserPwd
            // 
            this.lblUserPwd.Location = new System.Drawing.Point(34, 58);
            this.lblUserPwd.Name = "lblUserPwd";
            this.lblUserPwd.Size = new System.Drawing.Size(36, 14);
            this.lblUserPwd.TabIndex = 14;
            this.lblUserPwd.Text = "密码：";
            // 
            // lblConfirmedUserPwd
            // 
            this.lblConfirmedUserPwd.Location = new System.Drawing.Point(10, 86);
            this.lblConfirmedUserPwd.Name = "lblConfirmedUserPwd";
            this.lblConfirmedUserPwd.Size = new System.Drawing.Size(60, 14);
            this.lblConfirmedUserPwd.TabIndex = 13;
            this.lblConfirmedUserPwd.Text = "确认密码：";
            // 
            // lblUserActualName
            // 
            this.lblUserActualName.Location = new System.Drawing.Point(34, 114);
            this.lblUserActualName.Name = "lblUserActualName";
            this.lblUserActualName.Size = new System.Drawing.Size(36, 14);
            this.lblUserActualName.TabIndex = 12;
            this.lblUserActualName.Text = "姓名：";
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(22, 30);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(48, 14);
            this.lblUserName.TabIndex = 11;
            this.lblUserName.Text = "用户名：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(76, 27);
            this.txtUserName.MenuManager = this.barManager;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.MaxLength = 32;
            this.txtUserName.Size = new System.Drawing.Size(173, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // sbtnCancel
            // 
            this.sbtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sbtnCancel.Image = ((System.Drawing.Image)(resources.GetObject("sbtnCancel.Image")));
            this.sbtnCancel.Location = new System.Drawing.Point(208, 595);
            this.sbtnCancel.Name = "sbtnCancel";
            this.sbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.sbtnCancel.TabIndex = 19;
            this.sbtnCancel.Text = "取消(&C)";
            this.sbtnCancel.Click += new System.EventHandler(this.sbtnCancel_Click);
            // 
            // sbtnConfirm
            // 
            this.sbtnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sbtnConfirm.Image = ((System.Drawing.Image)(resources.GetObject("sbtnConfirm.Image")));
            this.sbtnConfirm.Location = new System.Drawing.Point(127, 595);
            this.sbtnConfirm.Name = "sbtnConfirm";
            this.sbtnConfirm.Size = new System.Drawing.Size(75, 23);
            this.sbtnConfirm.TabIndex = 18;
            this.sbtnConfirm.Text = "确定(&O)";
            this.sbtnConfirm.Click += new System.EventHandler(this.sbtnConfirm_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.lblTip);
            this.panelControl3.Controls.Add(this.peTip);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(2, 625);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(391, 33);
            this.panelControl3.TabIndex = 7;
            // 
            // lblTip
            // 
            this.lblTip.Appearance.Options.UseTextOptions = true;
            this.lblTip.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTip.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblTip.Location = new System.Drawing.Point(37, 10);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(359, 14);
            this.lblTip.TabIndex = 4;
            this.lblTip.Text = "建议不要删除用户而是冻结用户，以保留用户数据在查询时使用。";
            this.lblTip.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // peTip
            // 
            this.peTip.Cursor = System.Windows.Forms.Cursors.Default;
            this.peTip.EditValue = ((object)(resources.GetObject("peTip.EditValue")));
            this.peTip.Location = new System.Drawing.Point(3, 1);
            this.peTip.MenuManager = this.barManager;
            this.peTip.Name = "peTip";
            this.peTip.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peTip.Properties.Appearance.Options.UseBackColor = true;
            this.peTip.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.peTip.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peTip.Properties.ZoomAccelerationFactor = 1D;
            this.peTip.Size = new System.Drawing.Size(32, 30);
            this.peTip.TabIndex = 3;
            // 
            // scSecond
            // 
            this.scSecond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.scSecond.Location = new System.Drawing.Point(0, 575);
            this.scSecond.Name = "scSecond";
            this.scSecond.Size = new System.Drawing.Size(395, 23);
            this.scSecond.TabIndex = 51;
            // 
            // scFirst
            // 
            this.scFirst.Location = new System.Drawing.Point(4, 328);
            this.scFirst.Name = "scFirst";
            this.scFirst.Size = new System.Drawing.Size(386, 23);
            this.scFirst.TabIndex = 35;
            // 
            // icUserProperty
            // 
            this.icUserProperty.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icUserProperty.ImageStream")));
            this.icUserProperty.Images.SetKeyName(0, "Control_UserProperty_One.png");
            this.icUserProperty.Images.SetKeyName(1, "Control_UserProperty_Two.png");
            this.icUserProperty.Images.SetKeyName(2, "Control_UserProperty_Three.png");
            this.icUserProperty.Images.SetKeyName(3, "Control_UserProperty_Four.png");
            this.icUserProperty.Images.SetKeyName(4, "Control_UserProperty_Five.png");
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1324, 688);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.gcRight);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户管理";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defaultBarAndDockingController.Controller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).EndInit();
            this.gcCondition.ResumeLayout(false);
            this.gcCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtNewRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNewLocked.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRight)).EndInit();
            this.gcRight.ResumeLayout(false);
            this.gcRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAuthority)).EndInit();
            this.gcAuthority.ResumeLayout(false);
            this.gcAuthority.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDepartmentRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbAuthority.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtDepartmentRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btxtUserTypeRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLocked.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserIdentity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephoneNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmedUserPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserActualName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmailAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbIdentificationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icIdentificationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peTip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icUserProperty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barTools;
        private DevExpress.XtraBars.BarButtonItem bbiCreate;
        private DevExpress.XtraBars.BarButtonItem bbiEdit;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.XtraBars.BarLinkContainerItem blcDelete;
        private DevExpress.XtraBars.BarButtonItem bbiDelete;
        private DevExpress.XtraBars.BarButtonItem bbiBatchDelete;
        private DevExpress.XtraBars.BarLinkContainerItem blcLock;
        private DevExpress.XtraBars.BarButtonItem bbiLock;
        private DevExpress.XtraBars.BarButtonItem bbiUnLock;
        private DevExpress.XtraBars.BarButtonItem bbiBatchLock;
        private DevExpress.XtraBars.BarButtonItem bbiBatchUnLock;
        private DevExpress.XtraBars.BarButtonItem bbiExchange;
        private DevExpress.XtraBars.DefaultBarAndDockingController defaultBarAndDockingController;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.GroupControl gcMain;
        private AppFramework.WinFormsControls.DevExpressGrid grdUsers;
        private DevExpress.XtraEditors.GroupControl gcRight;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.PictureEdit peTip;
        private DevExpress.XtraEditors.GroupControl gcCondition;
        private DevExpress.XtraEditors.LabelControl lblCondition;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.TextEdit txtCondition;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.Utils.ImageCollection icButtons;
        private DevExpress.XtraEditors.LabelControl lblQueriedUserType;
        private DevExpress.XtraEditors.LabelControl lblQueriedDepartment;
        private TreeDropdownList cmbQueriedUserType;
        private DevExpress.XtraEditors.SimpleButton sbtnCancel;
        private DevExpress.XtraEditors.SimpleButton sbtnConfirm;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.TextEdit txtUserPwd;
        private DevExpress.XtraEditors.LabelControl lblLocked;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl lblDepartment;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbIdentificationType;
        private DevExpress.XtraEditors.LabelControl lblEmailAddress;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl lblUserType;
        private DevExpress.XtraEditors.LabelControl lblUserPwd;
        private DevExpress.XtraEditors.LabelControl lblConfirmedUserPwd;
        private DevExpress.XtraEditors.LabelControl lblUserActualName;
        private TreeDropdownList cmbUserType;
        private DevExpress.XtraEditors.TextEdit txtUserIdentity;
        private DevExpress.XtraEditors.TextEdit txtTelephoneNumber;
        private DevExpress.XtraEditors.TextEdit txtConfirmedUserPwd;
        private DevExpress.XtraEditors.TextEdit txtUserActualName;
        private DevExpress.XtraEditors.TextEdit txtEmailAddress;
        private DevExpress.XtraEditors.PictureEdit peUser;
        private DevExpress.XtraEditors.CheckEdit chkLocked;
        private DevExpress.XtraEditors.GroupControl gcAuthority;
        private DevExpress.XtraEditors.LabelControl lblRole;
        private DevExpress.XtraEditors.LabelControl lblAuthority;
        private DevExpress.XtraEditors.LabelControl lblDepartmentRange;
        private DevExpress.XtraEditors.LabelControl lblUserTypeRange;
        private DevExpress.XtraEditors.SeparatorControl scFirst;
        private DevExpress.XtraEditors.ButtonEdit btxtUserTypeRange;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.LabelControl lblNotes;
        private DevExpress.XtraEditors.ButtonEdit btxtDepartmentRange;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbAuthority;
        private DevExpress.XtraEditors.CheckEdit chkNewLocked;
        private DevExpress.Utils.ImageCollection icUserProperty;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.XtraEditors.ButtonEdit btxtRole;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbDepartmentRange;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private TreeDropdownList cmbQueriedDepartment;
        private TreeDropdownList cmbDepartment;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblNameTip;
        private DevExpress.XtraEditors.ButtonEdit btxtNewRole;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.SeparatorControl scSecond;
        private DevExpress.XtraEditors.TextEdit txtNewNotes;
        private DevExpress.XtraEditors.LabelControl lblNewLocked;
        private DevExpress.XtraEditors.LabelControl lblNewNotes;
        private DevExpress.Utils.ImageCollection icIdentificationType;
        private AppFramework.WinFormsControls.DevExpressUploadFile upfPhoto;
        private DevExpress.XtraBars.BarButtonItem bbiImportPhoto;
        private DevExpress.XtraEditors.LabelControl lblQueryAttributes;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraBars.BarButtonItem bbiRemoteUser;
        private DevExpress.XtraBars.BarButtonItem bbiExportedUser;
        private System.Windows.Forms.SaveFileDialog saveAttachmentFileDialog;
    }
}