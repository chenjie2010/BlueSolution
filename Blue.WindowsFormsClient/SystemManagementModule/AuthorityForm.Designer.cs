namespace Blue.WindowsFormsClient.SystemManagementModule
{
    partial class AuthorityForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorityForm));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gcRoles = new DevExpress.XtraEditors.GroupControl();
            this.chklstRole = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.gcType = new DevExpress.XtraEditors.GroupControl();
            this.chkOwnDepartment = new DevExpress.XtraEditors.CheckEdit();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnItmAuthority = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmBatchAuthority = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmAllAuthority = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imglstToolBar = new System.Windows.Forms.ImageList(this.components);
            this.ccmbAuthority = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEditDepartment = new DevExpress.XtraEditors.ButtonEdit();
            this.btnEditUserType = new DevExpress.XtraEditors.ButtonEdit();
            this.lblRelatedUserType = new System.Windows.Forms.Label();
            this.lblRelatedDepartment = new System.Windows.Forms.Label();
            this.gcAuthorityMethod = new DevExpress.XtraEditors.GroupControl();
            this.icmbAuthorityMethod = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icAuthorityMethod = new DevExpress.Utils.ImageCollection(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.gcQuery = new DevExpress.XtraEditors.GroupControl();
            this.cmbDepartment = new Blue.WindowsFormsClient.TreeDropdownList();
            this.cmbUserType = new Blue.WindowsFormsClient.TreeDropdownList();
            this.txtCondition = new DevExpress.XtraEditors.TextEdit();
            this.ccmbRole = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lnkbtnClean = new System.Windows.Forms.LinkLabel();
            this.sbtnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.devExpressGrid = new AppFramework.WinFormsControls.DevExpressGrid();
            this.gcUser = new DevExpress.XtraEditors.GroupControl();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRoles)).BeginInit();
            this.gcRoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chklstRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcType)).BeginInit();
            this.gcType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkOwnDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbAuthority.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditUserType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAuthorityMethod)).BeginInit();
            this.gcAuthorityMethod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbAuthorityMethod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAuthorityMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuery)).BeginInit();
            this.gcQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcUser)).BeginInit();
            this.gcUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gcRoles);
            this.groupControl1.Controls.Add(this.gcType);
            this.groupControl1.Controls.Add(this.gcAuthorityMethod);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 32);
            this.groupControl1.LookAndFeel.SkinName = "Blue";
            this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(290, 640);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "角色授权";
            // 
            // gcRoles
            // 
            this.gcRoles.Controls.Add(this.chklstRole);
            this.gcRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRoles.Location = new System.Drawing.Point(2, 208);
            this.gcRoles.LookAndFeel.SkinName = "Money Twins";
            this.gcRoles.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcRoles.Name = "gcRoles";
            this.gcRoles.Size = new System.Drawing.Size(286, 430);
            this.gcRoles.TabIndex = 30;
            this.gcRoles.Text = "选择角色";
            // 
            // chklstRole
            // 
            this.chklstRole.Cursor = System.Windows.Forms.Cursors.Default;
            this.chklstRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chklstRole.Location = new System.Drawing.Point(2, 22);
            this.chklstRole.LookAndFeel.SkinName = "Blue";
            this.chklstRole.LookAndFeel.UseDefaultLookAndFeel = false;
            this.chklstRole.Name = "chklstRole";
            this.chklstRole.Size = new System.Drawing.Size(282, 406);
            this.chklstRole.TabIndex = 4;
            // 
            // gcType
            // 
            this.gcType.Controls.Add(this.chkOwnDepartment);
            this.gcType.Controls.Add(this.ccmbAuthority);
            this.gcType.Controls.Add(this.label3);
            this.gcType.Controls.Add(this.btnEditDepartment);
            this.gcType.Controls.Add(this.btnEditUserType);
            this.gcType.Controls.Add(this.lblRelatedUserType);
            this.gcType.Controls.Add(this.lblRelatedDepartment);
            this.gcType.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcType.Location = new System.Drawing.Point(2, 75);
            this.gcType.LookAndFeel.SkinName = "Money Twins";
            this.gcType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcType.Name = "gcType";
            this.gcType.Size = new System.Drawing.Size(286, 133);
            this.gcType.TabIndex = 29;
            this.gcType.Text = "选择管理范围";
            // 
            // chkOwnDepartment
            // 
            this.chkOwnDepartment.Location = new System.Drawing.Point(68, 107);
            this.chkOwnDepartment.MenuManager = this.barManager;
            this.chkOwnDepartment.Name = "chkOwnDepartment";
            this.chkOwnDepartment.Properties.Caption = "仅管理自己所属的单位";
            this.chkOwnDepartment.Properties.LookAndFeel.SkinName = "Blue";
            this.chkOwnDepartment.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.chkOwnDepartment.Size = new System.Drawing.Size(150, 19);
            this.chkOwnDepartment.TabIndex = 30;
            this.chkOwnDepartment.CheckedChanged += new System.EventHandler(this.chkOwnDepartment_CheckedChanged);
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager.CloseButtonAffectAllTabs = false;
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imglstToolBar;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItmAuthority,
            this.btnItmBatchAuthority,
            this.btnItmAllAuthority});
            this.barManager.MaxItemId = 9;
            // 
            // bar2
            // 
            this.bar2.BarItemHorzIndent = 5;
            this.bar2.BarItemVertIndent = 5;
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmAuthority, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmBatchAuthority, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmAllAuthority, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnItmAuthority
            // 
            this.btnItmAuthority.Caption = "授权(&C)";
            this.btnItmAuthority.Id = 0;
            this.btnItmAuthority.ImageIndex = 0;
            this.btnItmAuthority.Name = "btnItmAuthority";
            this.btnItmAuthority.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmAuthority_ItemClick);
            // 
            // btnItmBatchAuthority
            // 
            this.btnItmBatchAuthority.Caption = "批量授权(&B)";
            this.btnItmBatchAuthority.Id = 1;
            this.btnItmBatchAuthority.ImageIndex = 1;
            this.btnItmBatchAuthority.Name = "btnItmBatchAuthority";
            this.btnItmBatchAuthority.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmBatchAuthority_ItemClick);
            // 
            // btnItmAllAuthority
            // 
            this.btnItmAllAuthority.Caption = "全部授权(&A)";
            this.btnItmAllAuthority.Id = 2;
            this.btnItmAllAuthority.ImageIndex = 2;
            this.btnItmAllAuthority.Name = "btnItmAllAuthority";
            this.btnItmAllAuthority.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmAllAuthority_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(1429, 32);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 672);
            this.barDockControlBottom.Size = new System.Drawing.Size(1429, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 32);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 640);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1429, 32);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 640);
            // 
            // imglstToolBar
            // 
            this.imglstToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstToolBar.ImageStream")));
            this.imglstToolBar.TransparentColor = System.Drawing.Color.Maroon;
            this.imglstToolBar.Images.SetKeyName(0, "System_Authority_Single.png");
            this.imglstToolBar.Images.SetKeyName(1, "System_Authority_Batch.png");
            this.imglstToolBar.Images.SetKeyName(2, "System_Authority_All.png");
            // 
            // ccmbAuthority
            // 
            this.ccmbAuthority.EditValue = "";
            this.ccmbAuthority.Location = new System.Drawing.Point(67, 28);
            this.ccmbAuthority.Name = "ccmbAuthority";
            this.ccmbAuthority.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbAuthority.Properties.LookAndFeel.SkinName = "Blue";
            this.ccmbAuthority.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ccmbAuthority.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.ccmbAuthority.Properties.PopupSizeable = false;
            this.ccmbAuthority.Properties.SelectAllItemVisible = false;
            this.ccmbAuthority.Properties.ShowButtons = false;
            this.ccmbAuthority.Size = new System.Drawing.Size(204, 20);
            this.ccmbAuthority.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 29;
            this.label3.Text = "用户权限：";
            // 
            // btnEditDepartment
            // 
            this.btnEditDepartment.Location = new System.Drawing.Point(67, 84);
            this.btnEditDepartment.Name = "btnEditDepartment";
            this.btnEditDepartment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditDepartment.Properties.LookAndFeel.SkinName = "Blue";
            this.btnEditDepartment.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnEditDepartment.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnEditDepartment.Size = new System.Drawing.Size(204, 20);
            this.btnEditDepartment.TabIndex = 3;
            this.btnEditDepartment.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEditDepartment_ButtonPressed);
            // 
            // btnEditUserType
            // 
            this.btnEditUserType.Location = new System.Drawing.Point(67, 57);
            this.btnEditUserType.Name = "btnEditUserType";
            this.btnEditUserType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditUserType.Properties.LookAndFeel.SkinName = "Blue";
            this.btnEditUserType.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnEditUserType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnEditUserType.Size = new System.Drawing.Size(204, 20);
            this.btnEditUserType.TabIndex = 2;
            this.btnEditUserType.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnEditUserType_ButtonPressed);
            // 
            // lblRelatedUserType
            // 
            this.lblRelatedUserType.AutoSize = true;
            this.lblRelatedUserType.Location = new System.Drawing.Point(5, 61);
            this.lblRelatedUserType.Name = "lblRelatedUserType";
            this.lblRelatedUserType.Size = new System.Drawing.Size(67, 14);
            this.lblRelatedUserType.TabIndex = 25;
            this.lblRelatedUserType.Text = "用户类型：";
            // 
            // lblRelatedDepartment
            // 
            this.lblRelatedDepartment.AutoSize = true;
            this.lblRelatedDepartment.Location = new System.Drawing.Point(5, 88);
            this.lblRelatedDepartment.Name = "lblRelatedDepartment";
            this.lblRelatedDepartment.Size = new System.Drawing.Size(67, 14);
            this.lblRelatedDepartment.TabIndex = 26;
            this.lblRelatedDepartment.Text = "用户单位：";
            // 
            // gcAuthorityMethod
            // 
            this.gcAuthorityMethod.Controls.Add(this.icmbAuthorityMethod);
            this.gcAuthorityMethod.Controls.Add(this.label1);
            this.gcAuthorityMethod.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcAuthorityMethod.Location = new System.Drawing.Point(2, 22);
            this.gcAuthorityMethod.LookAndFeel.SkinName = "Money Twins";
            this.gcAuthorityMethod.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcAuthorityMethod.Name = "gcAuthorityMethod";
            this.gcAuthorityMethod.Size = new System.Drawing.Size(286, 53);
            this.gcAuthorityMethod.TabIndex = 28;
            this.gcAuthorityMethod.Text = "选择授权方式";
            // 
            // icmbAuthorityMethod
            // 
            this.icmbAuthorityMethod.Location = new System.Drawing.Point(68, 27);
            this.icmbAuthorityMethod.MenuManager = this.barManager;
            this.icmbAuthorityMethod.Name = "icmbAuthorityMethod";
            this.icmbAuthorityMethod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbAuthorityMethod.Properties.SmallImages = this.icAuthorityMethod;
            this.icmbAuthorityMethod.Size = new System.Drawing.Size(204, 20);
            this.icmbAuthorityMethod.TabIndex = 0;
            // 
            // icAuthorityMethod
            // 
            this.icAuthorityMethod.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icAuthorityMethod.ImageStream")));
            this.icAuthorityMethod.Images.SetKeyName(0, "Enum_AuthorityMethod_Append.png");
            this.icAuthorityMethod.Images.SetKeyName(1, "Enum_AuthorityMethod_Update.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 26;
            this.label1.Text = "授权方式：";
            // 
            // gcQuery
            // 
            this.gcQuery.Controls.Add(this.cmbDepartment);
            this.gcQuery.Controls.Add(this.cmbUserType);
            this.gcQuery.Controls.Add(this.txtCondition);
            this.gcQuery.Controls.Add(this.ccmbRole);
            this.gcQuery.Controls.Add(this.lnkbtnClean);
            this.gcQuery.Controls.Add(this.sbtnQuery);
            this.gcQuery.Controls.Add(this.lblDepartment);
            this.gcQuery.Controls.Add(this.lblRole);
            this.gcQuery.Controls.Add(this.label2);
            this.gcQuery.Controls.Add(this.labelControl1);
            this.gcQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcQuery.Location = new System.Drawing.Point(290, 32);
            this.gcQuery.LookAndFeel.SkinName = "Blue";
            this.gcQuery.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcQuery.Name = "gcQuery";
            this.gcQuery.Size = new System.Drawing.Size(1139, 59);
            this.gcQuery.TabIndex = 2;
            this.gcQuery.Text = "查询条件";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.Location = new System.Drawing.Point(610, 31);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.ShowSearch = true;
            this.cmbDepartment.Size = new System.Drawing.Size(175, 24);
            this.cmbDepartment.SkinName = "Blue";
            this.cmbDepartment.TabIndex = 7;
            this.cmbDepartment.TreeDropdownHandler = null;
            // 
            // cmbUserType
            // 
            this.cmbUserType.Location = new System.Drawing.Point(391, 31);
            this.cmbUserType.Name = "cmbUserType";
            this.cmbUserType.OnlySelectedLeaf = true;
            this.cmbUserType.Size = new System.Drawing.Size(168, 26);
            this.cmbUserType.SkinName = "Blue";
            this.cmbUserType.TabIndex = 6;
            this.cmbUserType.TreeDropdownHandler = null;
            // 
            // txtCondition
            // 
            this.txtCondition.EditValue = "";
            this.txtCondition.Location = new System.Drawing.Point(72, 30);
            this.txtCondition.MenuManager = this.barManager;
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Properties.MaxLength = 32;
            this.txtCondition.Properties.NullValuePrompt = "请输入用户名、姓名、电子邮件、手机号码或证件号码";
            this.txtCondition.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCondition.Size = new System.Drawing.Size(244, 20);
            this.txtCondition.TabIndex = 5;
            this.txtCondition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCondition_KeyPress);
            // 
            // ccmbRole
            // 
            this.ccmbRole.Location = new System.Drawing.Point(837, 31);
            this.ccmbRole.MenuManager = this.barManager;
            this.ccmbRole.Name = "ccmbRole";
            this.ccmbRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbRole.Properties.LookAndFeel.SkinName = "Blue";
            this.ccmbRole.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ccmbRole.Properties.PopupSizeable = false;
            this.ccmbRole.Properties.SelectAllItemVisible = false;
            this.ccmbRole.Size = new System.Drawing.Size(173, 20);
            this.ccmbRole.TabIndex = 8;
            // 
            // lnkbtnClean
            // 
            this.lnkbtnClean.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lnkbtnClean.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.lnkbtnClean.Location = new System.Drawing.Point(1078, 34);
            this.lnkbtnClean.Name = "lnkbtnClean";
            this.lnkbtnClean.Size = new System.Drawing.Size(52, 12);
            this.lnkbtnClean.TabIndex = 10;
            this.lnkbtnClean.TabStop = true;
            this.lnkbtnClean.Text = "清除...";
            this.lnkbtnClean.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkbtnClean_LinkClicked);
            // 
            // sbtnQuery
            // 
            this.sbtnQuery.Location = new System.Drawing.Point(1016, 27);
            this.sbtnQuery.LookAndFeel.SkinName = "Money Twins";
            this.sbtnQuery.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnQuery.Name = "sbtnQuery";
            this.sbtnQuery.Size = new System.Drawing.Size(60, 23);
            this.sbtnQuery.TabIndex = 9;
            this.sbtnQuery.Text = "查询(&Q)";
            this.sbtnQuery.Click += new System.EventHandler(this.sbtnQuery_Click);
            // 
            // lblDepartment
            // 
            this.lblDepartment.Location = new System.Drawing.Point(565, 33);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(43, 14);
            this.lblDepartment.TabIndex = 72;
            this.lblDepartment.Text = "单位：";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(794, 33);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(43, 14);
            this.lblRole.TabIndex = 70;
            this.lblRole.Text = "角色：";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(322, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 68;
            this.label2.Text = "用户类型：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 64;
            this.labelControl1.Text = "查询条件：";
            // 
            // devExpressGrid
            // 
            this.devExpressGrid.CheckboxColumnCaption = null;
            this.devExpressGrid.ColumnHeaderTexts = new string[] {
        "用户名",
        "用户姓名",
        "用户类型",
        "用户单位"};
            this.devExpressGrid.DataKeyNames = new string[] {
        "UserId"};
            this.devExpressGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devExpressGrid.ExportedExcel = false;
            this.devExpressGrid.FootText = null;
            this.devExpressGrid.ImportedExcel = false;
            this.devExpressGrid.IsMainTable = false;
            this.devExpressGrid.IsShowCheckBox = true;
            this.devExpressGrid.Location = new System.Drawing.Point(2, 22);
            this.devExpressGrid.Name = "devExpressGrid";
            this.devExpressGrid.PageSize = 30;
            this.devExpressGrid.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devExpressGrid.Size = new System.Drawing.Size(1135, 557);
            this.devExpressGrid.TabIndex = 3;
            this.devExpressGrid.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.devExpressGrid_OnPageIndexChanged);
            // 
            // gcUser
            // 
            this.gcUser.Controls.Add(this.progressPanel);
            this.gcUser.Controls.Add(this.devExpressGrid);
            this.gcUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcUser.Location = new System.Drawing.Point(290, 91);
            this.gcUser.LookAndFeel.SkinName = "Blue";
            this.gcUser.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcUser.Name = "gcUser";
            this.gcUser.Size = new System.Drawing.Size(1139, 581);
            this.gcUser.TabIndex = 2;
            this.gcUser.Text = "查询结果";
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "授权操作进行中......";
            this.progressPanel.Location = new System.Drawing.Point(494, 265);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(150, 50);
            this.progressPanel.TabIndex = 9;
            this.progressPanel.Text = "数据加载中......";
            this.progressPanel.Visible = false;
            // 
            // AuthorityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1429, 672);
            this.Controls.Add(this.gcUser);
            this.Controls.Add(this.gcQuery);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AuthorityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "权限管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AuthorityForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRoles)).EndInit();
            this.gcRoles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chklstRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcType)).EndInit();
            this.gcType.ResumeLayout(false);
            this.gcType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkOwnDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbAuthority.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditUserType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAuthorityMethod)).EndInit();
            this.gcAuthorityMethod.ResumeLayout(false);
            this.gcAuthorityMethod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbAuthorityMethod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAuthorityMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuery)).EndInit();
            this.gcQuery.ResumeLayout(false);
            this.gcQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcUser)).EndInit();
            this.gcUser.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl gcQuery;
        private DevExpress.XtraEditors.GroupControl gcUser;
        protected System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lnkbtnClean;
        private DevExpress.XtraEditors.SimpleButton sbtnQuery;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnItmAuthority;
        private DevExpress.XtraBars.BarButtonItem btnItmBatchAuthority;
        private DevExpress.XtraBars.BarButtonItem btnItmAllAuthority;
        private System.Windows.Forms.ImageList imglstToolBar;
        private System.Windows.Forms.Label lblRole;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbRole;
        private DevExpress.XtraEditors.GroupControl gcRoles;
        private DevExpress.XtraEditors.CheckedListBoxControl chklstRole;
        private DevExpress.XtraEditors.GroupControl gcType;
        private DevExpress.XtraEditors.ButtonEdit btnEditDepartment;
        private DevExpress.XtraEditors.ButtonEdit btnEditUserType;
        private System.Windows.Forms.Label lblRelatedUserType;
        private System.Windows.Forms.Label lblRelatedDepartment;
        private DevExpress.XtraEditors.GroupControl gcAuthorityMethod;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.CheckEdit chkOwnDepartment;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbAuthority;
        private System.Windows.Forms.Label label3;        
        protected System.Windows.Forms.Label lblDepartment;
        private AppFramework.WinFormsControls.DevExpressGrid devExpressGrid;
        private DevExpress.XtraEditors.TextEdit txtCondition;
        private TreeDropdownList cmbUserType;
        private TreeDropdownList cmbDepartment;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbAuthorityMethod;
        private DevExpress.Utils.ImageCollection icAuthorityMethod;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
    }
}