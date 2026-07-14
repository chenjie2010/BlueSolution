namespace Blue.WindowsFormsServer
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnRestart = new DevExpress.XtraEditors.SimpleButton();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.xtbMain = new DevExpress.XtraTab.XtraTabControl();
            this.tbGeneral = new DevExpress.XtraTab.XtraTabPage();
            this.pnlConfigRight = new DevExpress.XtraEditors.PanelControl();
            this.lblBackupTime = new DevExpress.XtraEditors.LabelControl();
            this.txtBackupTime = new DevExpress.XtraEditors.TextEdit();
            this.chklInterface = new DevExpress.XtraEditors.CheckEdit();
            this.lblInterface = new DevExpress.XtraEditors.LabelControl();
            this.lblServerName = new DevExpress.XtraEditors.LabelControl();
            this.meWarning = new DevExpress.XtraEditors.MemoEdit();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblWarning = new DevExpress.XtraEditors.LabelControl();
            this.lblStartedTime = new DevExpress.XtraEditors.LabelControl();
            this.txtRunningTime = new DevExpress.XtraEditors.TextEdit();
            this.lblRunningTime = new DevExpress.XtraEditors.LabelControl();
            this.txtStatus = new DevExpress.XtraEditors.TextEdit();
            this.txtServerName = new DevExpress.XtraEditors.TextEdit();
            this.txtStartedTime = new DevExpress.XtraEditors.TextEdit();
            this.pnlConfigLeft = new DevExpress.XtraEditors.PanelControl();
            this.peMain = new DevExpress.XtraEditors.PictureEdit();
            this.tbConfig = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.gcDatabase = new DevExpress.XtraEditors.GroupControl();
            this.lblPasswordTip = new DevExpress.XtraEditors.LabelControl();
            this.lblUserNameTip = new DevExpress.XtraEditors.LabelControl();
            this.lblAddressTip = new DevExpress.XtraEditors.LabelControl();
            this.txtConnectionStatus = new DevExpress.XtraEditors.TextEdit();
            this.lblConnectionStatus = new DevExpress.XtraEditors.LabelControl();
            this.btnSaveSetting = new DevExpress.XtraEditors.SimpleButton();
            this.btnTestDatabase = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.txtAddress = new DevExpress.XtraEditors.TextEdit();
            this.lblAddress = new DevExpress.XtraEditors.LabelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.gcWebPath = new DevExpress.XtraEditors.GroupControl();
            this.btnBackupDir = new DevExpress.XtraEditors.SimpleButton();
            this.lblBackupDir = new DevExpress.XtraEditors.LabelControl();
            this.txtBackupDir = new DevExpress.XtraEditors.TextEdit();
            this.btnKeyName = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtKeyName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnBrowser = new DevExpress.XtraEditors.SimpleButton();
            this.txtPath = new DevExpress.XtraEditors.TextEdit();
            this.pnlFirst = new DevExpress.XtraEditors.PanelControl();
            this.peFirst = new DevExpress.XtraEditors.PictureEdit();
            this.tbRegister = new DevExpress.XtraTab.XtraTabPage();
            this.pnlMainRegister = new DevExpress.XtraEditors.PanelControl();
            this.gcRegister = new DevExpress.XtraEditors.GroupControl();
            this.fpnlRegisteredCode = new DevExpress.Utils.FlyoutPanel();
            this.fpcRegisteredCode = new DevExpress.Utils.FlyoutPanelControl();
            this.btnSumbit = new DevExpress.XtraEditors.SimpleButton();
            this.lblRegisteredTip = new DevExpress.XtraEditors.LabelControl();
            this.meRegisteredCode = new DevExpress.XtraEditors.MemoEdit();
            this.hleRegister = new DevExpress.XtraEditors.HyperLinkEdit();
            this.fpnlMachineCode = new DevExpress.Utils.FlyoutPanel();
            this.fpcMachineCode = new DevExpress.Utils.FlyoutPanelControl();
            this.meMachineCode = new DevExpress.XtraEditors.MemoEdit();
            this.txtRegisteredInfo = new DevExpress.XtraEditors.MemoEdit();
            this.btnRegister = new DevExpress.XtraEditors.SimpleButton();
            this.pnlSecond = new DevExpress.XtraEditors.PanelControl();
            this.peSecond = new DevExpress.XtraEditors.PictureEdit();
            this.tbAboutUs = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl8 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblCompany = new DevExpress.XtraEditors.LabelControl();
            this.lblExcepiton = new DevExpress.XtraEditors.LabelControl();
            this.meException = new DevExpress.XtraEditors.MemoEdit();
            this.lbcClientVersions = new DevExpress.XtraEditors.ListBoxControl();
            this.txtReleasedDate = new DevExpress.XtraEditors.TextEdit();
            this.lblClientVersions = new DevExpress.XtraEditors.LabelControl();
            this.lblReleasedDate = new DevExpress.XtraEditors.LabelControl();
            this.txtServerVersion = new DevExpress.XtraEditors.TextEdit();
            this.lblServerVersion = new DevExpress.XtraEditors.LabelControl();
            this.pnlThird = new DevExpress.XtraEditors.PanelControl();
            this.peThird = new DevExpress.XtraEditors.PictureEdit();
            this.tmUpdate = new System.Windows.Forms.Timer();
            this.bgwConnection = new System.ComponentModel.BackgroundWorker();
            this.bgwMachineCode = new System.ComponentModel.BackgroundWorker();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.bgwServerDataShow = new System.ComponentModel.BackgroundWorker();
            this.bgwEnbaleWindowsService = new System.ComponentModel.BackgroundWorker();
            this.lblKeyTip = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtbMain)).BeginInit();
            this.xtbMain.SuspendLayout();
            this.tbGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlConfigRight)).BeginInit();
            this.pnlConfigRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chklInterface.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meWarning.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRunningTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartedTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlConfigLeft)).BeginInit();
            this.pnlConfigLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peMain.Properties)).BeginInit();
            this.tbConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatabase)).BeginInit();
            this.gcDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConnectionStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcWebPath)).BeginInit();
            this.gcWebPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupDir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFirst)).BeginInit();
            this.pnlFirst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peFirst.Properties)).BeginInit();
            this.tbRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMainRegister)).BeginInit();
            this.pnlMainRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRegister)).BeginInit();
            this.gcRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpnlRegisteredCode)).BeginInit();
            this.fpnlRegisteredCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpcRegisteredCode)).BeginInit();
            this.fpcRegisteredCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meRegisteredCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleRegister.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpnlMachineCode)).BeginInit();
            this.fpnlMachineCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpcMachineCode)).BeginInit();
            this.fpcMachineCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meMachineCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegisteredInfo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSecond)).BeginInit();
            this.pnlSecond.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peSecond.Properties)).BeginInit();
            this.tbAboutUs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl8)).BeginInit();
            this.panelControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meException.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbcClientVersions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReleasedDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlThird)).BeginInit();
            this.pnlThird.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peThird.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 9);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "启动(&S)";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(172, 9);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(75, 23);
            this.btnRestart.TabIndex = 9;
            this.btnRestart.Text = "重启(&R)";
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(89, 9);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "停止(&C)";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(255, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "退出(&E)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnExit);
            this.panelControl1.Controls.Add(this.btnStart);
            this.panelControl1.Controls.Add(this.btnRestart);
            this.panelControl1.Controls.Add(this.btnStop);
            this.panelControl1.Location = new System.Drawing.Point(37, 236);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(337, 41);
            this.panelControl1.TabIndex = 5;
            // 
            // xtbMain
            // 
            this.xtbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtbMain.Location = new System.Drawing.Point(0, 0);
            this.xtbMain.Name = "xtbMain";
            this.xtbMain.SelectedTabPage = this.tbGeneral;
            this.xtbMain.Size = new System.Drawing.Size(544, 315);
            this.xtbMain.TabIndex = 6;
            this.xtbMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tbGeneral,
            this.tbConfig,
            this.tbRegister,
            this.tbAboutUs});
            // 
            // tbGeneral
            // 
            this.tbGeneral.Controls.Add(this.pnlConfigRight);
            this.tbGeneral.Controls.Add(this.pnlConfigLeft);
            this.tbGeneral.Name = "tbGeneral";
            this.tbGeneral.Size = new System.Drawing.Size(538, 286);
            this.tbGeneral.Text = "服务器端状态";
            // 
            // pnlConfigRight
            // 
            this.pnlConfigRight.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlConfigRight.Appearance.Options.UseBackColor = true;
            this.pnlConfigRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlConfigRight.Controls.Add(this.lblBackupTime);
            this.pnlConfigRight.Controls.Add(this.txtBackupTime);
            this.pnlConfigRight.Controls.Add(this.chklInterface);
            this.pnlConfigRight.Controls.Add(this.lblInterface);
            this.pnlConfigRight.Controls.Add(this.lblServerName);
            this.pnlConfigRight.Controls.Add(this.panelControl1);
            this.pnlConfigRight.Controls.Add(this.meWarning);
            this.pnlConfigRight.Controls.Add(this.lblStatus);
            this.pnlConfigRight.Controls.Add(this.lblWarning);
            this.pnlConfigRight.Controls.Add(this.lblStartedTime);
            this.pnlConfigRight.Controls.Add(this.txtRunningTime);
            this.pnlConfigRight.Controls.Add(this.lblRunningTime);
            this.pnlConfigRight.Controls.Add(this.txtStatus);
            this.pnlConfigRight.Controls.Add(this.txtServerName);
            this.pnlConfigRight.Controls.Add(this.txtStartedTime);
            this.pnlConfigRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConfigRight.Location = new System.Drawing.Point(148, 0);
            this.pnlConfigRight.Name = "pnlConfigRight";
            this.pnlConfigRight.Size = new System.Drawing.Size(390, 286);
            this.pnlConfigRight.TabIndex = 4;
            // 
            // lblBackupTime
            // 
            this.lblBackupTime.Location = new System.Drawing.Point(13, 130);
            this.lblBackupTime.Name = "lblBackupTime";
            this.lblBackupTime.Size = new System.Drawing.Size(84, 14);
            this.lblBackupTime.TabIndex = 20;
            this.lblBackupTime.Text = "最新备份时间：";
            // 
            // txtBackupTime
            // 
            this.txtBackupTime.EditValue = "无";
            this.txtBackupTime.Location = new System.Drawing.Point(102, 129);
            this.txtBackupTime.Name = "txtBackupTime";
            this.txtBackupTime.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.txtBackupTime.Properties.Appearance.Options.UseBackColor = true;
            this.txtBackupTime.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtBackupTime.Properties.ReadOnly = true;
            this.txtBackupTime.Size = new System.Drawing.Size(274, 18);
            this.txtBackupTime.TabIndex = 19;
            // 
            // chklInterface
            // 
            this.chklInterface.Location = new System.Drawing.Point(102, 206);
            this.chklInterface.Name = "chklInterface";
            this.chklInterface.Properties.Caption = "启用数据接口。（设置后重启生效）";
            this.chklInterface.Size = new System.Drawing.Size(221, 19);
            this.chklInterface.TabIndex = 6;
            this.chklInterface.CheckedChanged += new System.EventHandler(this.chklInterface_CheckedChanged);
            // 
            // lblInterface
            // 
            this.lblInterface.Location = new System.Drawing.Point(37, 208);
            this.lblInterface.Name = "lblInterface";
            this.lblInterface.Size = new System.Drawing.Size(60, 14);
            this.lblInterface.TabIndex = 17;
            this.lblInterface.Text = "接口信息：";
            // 
            // lblServerName
            // 
            this.lblServerName.Location = new System.Drawing.Point(23, 16);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(72, 14);
            this.lblServerName.TabIndex = 6;
            this.lblServerName.Text = "服务器名称：";
            // 
            // meWarning
            // 
            this.meWarning.Location = new System.Drawing.Point(102, 159);
            this.meWarning.Name = "meWarning";
            this.meWarning.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.meWarning.Properties.Appearance.Options.UseBackColor = true;
            this.meWarning.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.meWarning.Properties.ReadOnly = true;
            this.meWarning.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.meWarning.Size = new System.Drawing.Size(273, 41);
            this.meWarning.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(25, 45);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(72, 14);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "服务器状态：";
            // 
            // lblWarning
            // 
            this.lblWarning.Location = new System.Drawing.Point(37, 159);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(60, 14);
            this.lblWarning.TabIndex = 16;
            this.lblWarning.Text = "提示信息：";
            // 
            // lblStartedTime
            // 
            this.lblStartedTime.Location = new System.Drawing.Point(37, 73);
            this.lblStartedTime.Name = "lblStartedTime";
            this.lblStartedTime.Size = new System.Drawing.Size(60, 14);
            this.lblStartedTime.TabIndex = 8;
            this.lblStartedTime.Text = "开启时间：";
            // 
            // txtRunningTime
            // 
            this.txtRunningTime.EditValue = "0天0小时0分0秒";
            this.txtRunningTime.Location = new System.Drawing.Point(102, 101);
            this.txtRunningTime.Name = "txtRunningTime";
            this.txtRunningTime.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.txtRunningTime.Properties.Appearance.Options.UseBackColor = true;
            this.txtRunningTime.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtRunningTime.Properties.ReadOnly = true;
            this.txtRunningTime.Size = new System.Drawing.Size(274, 18);
            this.txtRunningTime.TabIndex = 3;
            // 
            // lblRunningTime
            // 
            this.lblRunningTime.Location = new System.Drawing.Point(37, 103);
            this.lblRunningTime.Name = "lblRunningTime";
            this.lblRunningTime.Size = new System.Drawing.Size(60, 14);
            this.lblRunningTime.TabIndex = 9;
            this.lblRunningTime.Text = "运行时间：";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(102, 43);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.txtStatus.Properties.Appearance.Options.UseBackColor = true;
            this.txtStatus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtStatus.Properties.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(274, 18);
            this.txtStatus.TabIndex = 1;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(102, 15);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.txtServerName.Properties.Appearance.Options.UseBackColor = true;
            this.txtServerName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtServerName.Properties.ReadOnly = true;
            this.txtServerName.Size = new System.Drawing.Size(274, 18);
            this.txtServerName.TabIndex = 0;
            // 
            // txtStartedTime
            // 
            this.txtStartedTime.Location = new System.Drawing.Point(102, 72);
            this.txtStartedTime.Name = "txtStartedTime";
            this.txtStartedTime.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.txtStartedTime.Properties.Appearance.Options.UseBackColor = true;
            this.txtStartedTime.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtStartedTime.Properties.ReadOnly = true;
            this.txtStartedTime.Size = new System.Drawing.Size(274, 18);
            this.txtStartedTime.TabIndex = 2;
            // 
            // pnlConfigLeft
            // 
            this.pnlConfigLeft.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlConfigLeft.Appearance.Options.UseBackColor = true;
            this.pnlConfigLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlConfigLeft.Controls.Add(this.peMain);
            this.pnlConfigLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlConfigLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlConfigLeft.Name = "pnlConfigLeft";
            this.pnlConfigLeft.Padding = new System.Windows.Forms.Padding(3);
            this.pnlConfigLeft.Size = new System.Drawing.Size(148, 286);
            this.pnlConfigLeft.TabIndex = 19;
            // 
            // peMain
            // 
            this.peMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.peMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peMain.EditValue = global::Blue.WindowsFormsServer.Properties.Resources.Main;
            this.peMain.Location = new System.Drawing.Point(3, 3);
            this.peMain.Name = "peMain";
            this.peMain.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peMain.Properties.Appearance.Options.UseBackColor = true;
            this.peMain.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peMain.Properties.ZoomAccelerationFactor = 1D;
            this.peMain.Size = new System.Drawing.Size(142, 280);
            this.peMain.TabIndex = 21;
            // 
            // tbConfig
            // 
            this.tbConfig.Controls.Add(this.panelControl3);
            this.tbConfig.Controls.Add(this.pnlFirst);
            this.tbConfig.Name = "tbConfig";
            this.tbConfig.Size = new System.Drawing.Size(538, 286);
            this.tbConfig.Text = "系统设置";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.gcDatabase);
            this.panelControl3.Controls.Add(this.gcWebPath);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(140, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(398, 286);
            this.panelControl3.TabIndex = 1;
            // 
            // gcDatabase
            // 
            this.gcDatabase.Controls.Add(this.lblPasswordTip);
            this.gcDatabase.Controls.Add(this.lblUserNameTip);
            this.gcDatabase.Controls.Add(this.lblAddressTip);
            this.gcDatabase.Controls.Add(this.txtConnectionStatus);
            this.gcDatabase.Controls.Add(this.lblConnectionStatus);
            this.gcDatabase.Controls.Add(this.btnSaveSetting);
            this.gcDatabase.Controls.Add(this.btnTestDatabase);
            this.gcDatabase.Controls.Add(this.txtPassword);
            this.gcDatabase.Controls.Add(this.lblPassword);
            this.gcDatabase.Controls.Add(this.txtAddress);
            this.gcDatabase.Controls.Add(this.lblAddress);
            this.gcDatabase.Controls.Add(this.lblUserName);
            this.gcDatabase.Controls.Add(this.txtUserName);
            this.gcDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDatabase.Location = new System.Drawing.Point(2, 140);
            this.gcDatabase.Name = "gcDatabase";
            this.gcDatabase.Size = new System.Drawing.Size(394, 144);
            this.gcDatabase.TabIndex = 17;
            this.gcDatabase.Text = "数据库设置";
            // 
            // lblPasswordTip
            // 
            this.lblPasswordTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblPasswordTip.Appearance.Options.UseForeColor = true;
            this.lblPasswordTip.Location = new System.Drawing.Point(368, 88);
            this.lblPasswordTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblPasswordTip.Name = "lblPasswordTip";
            this.lblPasswordTip.Size = new System.Drawing.Size(7, 14);
            this.lblPasswordTip.TabIndex = 25;
            this.lblPasswordTip.Text = "*";
            // 
            // lblUserNameTip
            // 
            this.lblUserNameTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblUserNameTip.Appearance.Options.UseForeColor = true;
            this.lblUserNameTip.Location = new System.Drawing.Point(205, 88);
            this.lblUserNameTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblUserNameTip.Name = "lblUserNameTip";
            this.lblUserNameTip.Size = new System.Drawing.Size(7, 14);
            this.lblUserNameTip.TabIndex = 24;
            this.lblUserNameTip.Text = "*";
            // 
            // lblAddressTip
            // 
            this.lblAddressTip.Appearance.ForeColor = System.Drawing.Color.Tomato;
            this.lblAddressTip.Appearance.Options.UseForeColor = true;
            this.lblAddressTip.Location = new System.Drawing.Point(368, 58);
            this.lblAddressTip.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblAddressTip.Name = "lblAddressTip";
            this.lblAddressTip.Size = new System.Drawing.Size(7, 14);
            this.lblAddressTip.TabIndex = 23;
            this.lblAddressTip.Text = "*";
            // 
            // txtConnectionStatus
            // 
            this.txtConnectionStatus.EditValue = "未知状态";
            this.txtConnectionStatus.Location = new System.Drawing.Point(99, 27);
            this.txtConnectionStatus.Name = "txtConnectionStatus";
            this.txtConnectionStatus.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.txtConnectionStatus.Properties.Appearance.Options.UseBackColor = true;
            this.txtConnectionStatus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtConnectionStatus.Properties.ReadOnly = true;
            this.txtConnectionStatus.Size = new System.Drawing.Size(290, 18);
            this.txtConnectionStatus.TabIndex = 17;
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.Location = new System.Drawing.Point(10, 29);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(84, 14);
            this.lblConnectionStatus.TabIndex = 16;
            this.lblConnectionStatus.Text = "当前连接状态：";
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.Location = new System.Drawing.Point(220, 112);
            this.btnSaveSetting.Name = "btnSaveSetting";
            this.btnSaveSetting.Size = new System.Drawing.Size(90, 23);
            this.btnSaveSetting.TabIndex = 15;
            this.btnSaveSetting.Text = "保存设置(&S)";
            this.btnSaveSetting.Click += new System.EventHandler(this.btnSaveSetting_Click);
            // 
            // btnTestDatabase
            // 
            this.btnTestDatabase.Location = new System.Drawing.Point(112, 112);
            this.btnTestDatabase.Name = "btnTestDatabase";
            this.btnTestDatabase.Size = new System.Drawing.Size(90, 23);
            this.btnTestDatabase.TabIndex = 14;
            this.btnTestDatabase.Text = "测试连接...(&T)";
            this.btnTestDatabase.Click += new System.EventHandler(this.btnTestDatabase_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(260, 84);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(102, 20);
            this.txtPassword.TabIndex = 13;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(220, 86);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(36, 14);
            this.lblPassword.TabIndex = 12;
            this.lblPassword.Text = "密码：";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(100, 56);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(262, 20);
            this.txtAddress.TabIndex = 8;
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(22, 58);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(72, 14);
            this.lblAddress.TabIndex = 9;
            this.lblAddress.Text = "数据库地址：";
            // 
            // lblUserName
            // 
            this.lblUserName.Location = new System.Drawing.Point(46, 87);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(48, 14);
            this.lblUserName.TabIndex = 11;
            this.lblUserName.Text = "用户名：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(100, 84);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(102, 20);
            this.txtUserName.TabIndex = 10;
            // 
            // gcWebPath
            // 
            this.gcWebPath.Controls.Add(this.lblKeyTip);
            this.gcWebPath.Controls.Add(this.btnBackupDir);
            this.gcWebPath.Controls.Add(this.lblBackupDir);
            this.gcWebPath.Controls.Add(this.txtBackupDir);
            this.gcWebPath.Controls.Add(this.btnKeyName);
            this.gcWebPath.Controls.Add(this.labelControl2);
            this.gcWebPath.Controls.Add(this.txtKeyName);
            this.gcWebPath.Controls.Add(this.labelControl1);
            this.gcWebPath.Controls.Add(this.btnBrowser);
            this.gcWebPath.Controls.Add(this.txtPath);
            this.gcWebPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcWebPath.Location = new System.Drawing.Point(2, 2);
            this.gcWebPath.Name = "gcWebPath";
            this.gcWebPath.Size = new System.Drawing.Size(394, 138);
            this.gcWebPath.TabIndex = 16;
            this.gcWebPath.Text = "系统设置";
            // 
            // btnBackupDir
            // 
            this.btnBackupDir.Location = new System.Drawing.Point(330, 57);
            this.btnBackupDir.Name = "btnBackupDir";
            this.btnBackupDir.Size = new System.Drawing.Size(59, 23);
            this.btnBackupDir.TabIndex = 17;
            this.btnBackupDir.Text = "浏览...";
            this.btnBackupDir.Click += new System.EventHandler(this.btnBackupDir_Click);
            // 
            // lblBackupDir
            // 
            this.lblBackupDir.Location = new System.Drawing.Point(9, 62);
            this.lblBackupDir.Name = "lblBackupDir";
            this.lblBackupDir.Size = new System.Drawing.Size(60, 14);
            this.lblBackupDir.TabIndex = 31;
            this.lblBackupDir.Text = "备份目录：";
            // 
            // txtBackupDir
            // 
            this.txtBackupDir.Location = new System.Drawing.Point(72, 59);
            this.txtBackupDir.Name = "txtBackupDir";
            this.txtBackupDir.Properties.ReadOnly = true;
            this.txtBackupDir.Size = new System.Drawing.Size(250, 20);
            this.txtBackupDir.TabIndex = 16;
            // 
            // btnKeyName
            // 
            this.btnKeyName.Location = new System.Drawing.Point(330, 88);
            this.btnKeyName.Name = "btnKeyName";
            this.btnKeyName.Size = new System.Drawing.Size(59, 23);
            this.btnKeyName.TabIndex = 26;
            this.btnKeyName.Text = "保存(&W)";
            this.btnKeyName.Click += new System.EventHandler(this.btnKeyName_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(7, 32);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 14);
            this.labelControl2.TabIndex = 28;
            this.labelControl2.Text = "Web目录：";
            // 
            // txtKeyName
            // 
            this.txtKeyName.Location = new System.Drawing.Point(72, 89);
            this.txtKeyName.Name = "txtKeyName";
            this.txtKeyName.Properties.MaxLength = 64;
            this.txtKeyName.Size = new System.Drawing.Size(250, 20);
            this.txtKeyName.TabIndex = 26;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 91);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 27;
            this.labelControl1.Text = "关键字：";
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(330, 28);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(59, 23);
            this.btnBrowser.TabIndex = 8;
            this.btnBrowser.Text = "浏览...";
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(72, 29);
            this.txtPath.Name = "txtPath";
            this.txtPath.Properties.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(250, 20);
            this.txtPath.TabIndex = 0;
            // 
            // pnlFirst
            // 
            this.pnlFirst.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlFirst.Appearance.Options.UseBackColor = true;
            this.pnlFirst.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFirst.Controls.Add(this.peFirst);
            this.pnlFirst.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFirst.Location = new System.Drawing.Point(0, 0);
            this.pnlFirst.Name = "pnlFirst";
            this.pnlFirst.Padding = new System.Windows.Forms.Padding(3);
            this.pnlFirst.Size = new System.Drawing.Size(140, 286);
            this.pnlFirst.TabIndex = 0;
            // 
            // peFirst
            // 
            this.peFirst.Cursor = System.Windows.Forms.Cursors.Default;
            this.peFirst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peFirst.EditValue = global::Blue.WindowsFormsServer.Properties.Resources.First;
            this.peFirst.Location = new System.Drawing.Point(3, 3);
            this.peFirst.Name = "peFirst";
            this.peFirst.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peFirst.Properties.Appearance.Options.UseBackColor = true;
            this.peFirst.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peFirst.Properties.ZoomAccelerationFactor = 1D;
            this.peFirst.Size = new System.Drawing.Size(134, 280);
            this.peFirst.TabIndex = 22;
            // 
            // tbRegister
            // 
            this.tbRegister.Controls.Add(this.pnlMainRegister);
            this.tbRegister.Controls.Add(this.pnlSecond);
            this.tbRegister.Name = "tbRegister";
            this.tbRegister.Size = new System.Drawing.Size(538, 286);
            this.tbRegister.Text = "软件注册";
            // 
            // pnlMainRegister
            // 
            this.pnlMainRegister.Controls.Add(this.gcRegister);
            this.pnlMainRegister.Controls.Add(this.hleRegister);
            this.pnlMainRegister.Controls.Add(this.btnRegister);
            this.pnlMainRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainRegister.Location = new System.Drawing.Point(140, 0);
            this.pnlMainRegister.Name = "pnlMainRegister";
            this.pnlMainRegister.Size = new System.Drawing.Size(398, 286);
            this.pnlMainRegister.TabIndex = 19;
            // 
            // gcRegister
            // 
            this.gcRegister.Controls.Add(this.fpnlRegisteredCode);
            this.gcRegister.Controls.Add(this.fpnlMachineCode);
            this.gcRegister.Controls.Add(this.txtRegisteredInfo);
            this.gcRegister.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcRegister.Location = new System.Drawing.Point(2, 2);
            this.gcRegister.Name = "gcRegister";
            this.gcRegister.Size = new System.Drawing.Size(394, 237);
            this.gcRegister.TabIndex = 20;
            this.gcRegister.Text = "注册信息";
            // 
            // fpnlRegisteredCode
            // 
            this.fpnlRegisteredCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpnlRegisteredCode.Controls.Add(this.fpcRegisteredCode);
            this.fpnlRegisteredCode.Location = new System.Drawing.Point(5, 56);
            this.fpnlRegisteredCode.Name = "fpnlRegisteredCode";
            this.fpnlRegisteredCode.OptionsButtonPanel.ButtonPanelContentAlignment = System.Drawing.ContentAlignment.TopRight;
            this.fpnlRegisteredCode.OptionsButtonPanel.ButtonPanelHeight = 25;
            toolTipTitleItem1.Text = "关闭";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.fpnlRegisteredCode.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("", global::Blue.WindowsFormsServer.Properties.Resources.Common_Close_1, -1, DevExpress.XtraEditors.ButtonPanel.ImageLocation.Default, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "关闭", true, -1, true, superToolTip1, true, false, true, null, null, -1, false)});
            this.fpnlRegisteredCode.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpnlRegisteredCode.OwnerControl = this.hleRegister;
            this.fpnlRegisteredCode.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.fpnlRegisteredCode.Size = new System.Drawing.Size(338, 138);
            this.fpnlRegisteredCode.TabIndex = 23;
            this.fpnlRegisteredCode.ButtonClick += new DevExpress.Utils.FlyoutPanelButtonClickEventHandler(this.fpnlRegisteredCode_ButtonClick);
            // 
            // fpcRegisteredCode
            // 
            this.fpcRegisteredCode.Controls.Add(this.btnSumbit);
            this.fpcRegisteredCode.Controls.Add(this.lblRegisteredTip);
            this.fpcRegisteredCode.Controls.Add(this.meRegisteredCode);
            this.fpcRegisteredCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpcRegisteredCode.FlyoutPanel = null;
            this.fpcRegisteredCode.Location = new System.Drawing.Point(0, 25);
            this.fpcRegisteredCode.Name = "fpcRegisteredCode";
            this.fpcRegisteredCode.Padding = new System.Windows.Forms.Padding(1);
            this.fpcRegisteredCode.Size = new System.Drawing.Size(336, 111);
            this.fpcRegisteredCode.TabIndex = 0;
            // 
            // btnSumbit
            // 
            this.btnSumbit.Location = new System.Drawing.Point(273, 83);
            this.btnSumbit.Name = "btnSumbit";
            this.btnSumbit.Size = new System.Drawing.Size(56, 23);
            this.btnSumbit.TabIndex = 21;
            this.btnSumbit.Text = "确定(&C)";
            this.btnSumbit.Click += new System.EventHandler(this.btnSumbit_Click);
            // 
            // lblRegisteredTip
            // 
            this.lblRegisteredTip.Location = new System.Drawing.Point(4, 4);
            this.lblRegisteredTip.Name = "lblRegisteredTip";
            this.lblRegisteredTip.Size = new System.Drawing.Size(84, 14);
            this.lblRegisteredTip.TabIndex = 20;
            this.lblRegisteredTip.Text = "请输入注册码：";
            // 
            // meRegisteredCode
            // 
            this.meRegisteredCode.Location = new System.Drawing.Point(94, 4);
            this.meRegisteredCode.Name = "meRegisteredCode";
            this.meRegisteredCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.meRegisteredCode.Properties.Appearance.Options.UseBackColor = true;
            this.meRegisteredCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.meRegisteredCode.Properties.MaxLength = 512;
            this.meRegisteredCode.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.meRegisteredCode.Size = new System.Drawing.Size(236, 73);
            this.meRegisteredCode.TabIndex = 19;
            // 
            // hleRegister
            // 
            this.hleRegister.EditValue = "获取机器码";
            this.hleRegister.Location = new System.Drawing.Point(18, 256);
            this.hleRegister.Name = "hleRegister";
            this.hleRegister.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hleRegister.Properties.Appearance.Options.UseBackColor = true;
            this.hleRegister.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hleRegister.Size = new System.Drawing.Size(75, 18);
            this.hleRegister.TabIndex = 19;
            this.hleRegister.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hleRegister_OpenLink);
            // 
            // fpnlMachineCode
            // 
            this.fpnlMachineCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpnlMachineCode.Controls.Add(this.fpcMachineCode);
            this.fpnlMachineCode.Location = new System.Drawing.Point(51, 24);
            this.fpnlMachineCode.Name = "fpnlMachineCode";
            this.fpnlMachineCode.OptionsButtonPanel.ButtonPanelContentAlignment = System.Drawing.ContentAlignment.TopRight;
            this.fpnlMachineCode.OptionsButtonPanel.ButtonPanelHeight = 25;
            toolTipTitleItem2.Text = "关闭";
            superToolTip2.Items.Add(toolTipTitleItem2);
            this.fpnlMachineCode.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("", global::Blue.WindowsFormsServer.Properties.Resources.Common_Close_1, -1, DevExpress.XtraEditors.ButtonPanel.ImageLocation.Default, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "关闭", true, -1, true, superToolTip2, true, false, true, null, null, -1, false)});
            this.fpnlMachineCode.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpnlMachineCode.OwnerControl = this.hleRegister;
            this.fpnlMachineCode.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.fpnlMachineCode.Size = new System.Drawing.Size(338, 76);
            this.fpnlMachineCode.TabIndex = 22;
            this.fpnlMachineCode.ButtonClick += new DevExpress.Utils.FlyoutPanelButtonClickEventHandler(this.fpnlMachineCode_ButtonClick);
            // 
            // fpcMachineCode
            // 
            this.fpcMachineCode.Controls.Add(this.meMachineCode);
            this.fpcMachineCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpcMachineCode.FlyoutPanel = null;
            this.fpcMachineCode.Location = new System.Drawing.Point(0, 25);
            this.fpcMachineCode.Name = "fpcMachineCode";
            this.fpcMachineCode.Padding = new System.Windows.Forms.Padding(1);
            this.fpcMachineCode.Size = new System.Drawing.Size(336, 49);
            this.fpcMachineCode.TabIndex = 0;
            // 
            // meMachineCode
            // 
            this.meMachineCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meMachineCode.Location = new System.Drawing.Point(3, 3);
            this.meMachineCode.Name = "meMachineCode";
            this.meMachineCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.meMachineCode.Properties.Appearance.Options.UseBackColor = true;
            this.meMachineCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.meMachineCode.Properties.ReadOnly = true;
            this.meMachineCode.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.meMachineCode.Size = new System.Drawing.Size(330, 43);
            this.meMachineCode.TabIndex = 19;
            // 
            // txtRegisteredInfo
            // 
            this.txtRegisteredInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRegisteredInfo.Location = new System.Drawing.Point(2, 21);
            this.txtRegisteredInfo.Name = "txtRegisteredInfo";
            this.txtRegisteredInfo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtRegisteredInfo.Properties.ReadOnly = true;
            this.txtRegisteredInfo.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtRegisteredInfo.Size = new System.Drawing.Size(390, 214);
            this.txtRegisteredInfo.TabIndex = 18;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(315, 254);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(73, 23);
            this.btnRegister.TabIndex = 18;
            this.btnRegister.Text = "注册...(&R)";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // pnlSecond
            // 
            this.pnlSecond.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlSecond.Appearance.Options.UseBackColor = true;
            this.pnlSecond.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlSecond.Controls.Add(this.peSecond);
            this.pnlSecond.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSecond.Location = new System.Drawing.Point(0, 0);
            this.pnlSecond.Name = "pnlSecond";
            this.pnlSecond.Padding = new System.Windows.Forms.Padding(3);
            this.pnlSecond.Size = new System.Drawing.Size(140, 286);
            this.pnlSecond.TabIndex = 18;
            // 
            // peSecond
            // 
            this.peSecond.Cursor = System.Windows.Forms.Cursors.Default;
            this.peSecond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peSecond.EditValue = global::Blue.WindowsFormsServer.Properties.Resources.Second;
            this.peSecond.Location = new System.Drawing.Point(3, 3);
            this.peSecond.Name = "peSecond";
            this.peSecond.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peSecond.Properties.Appearance.Options.UseBackColor = true;
            this.peSecond.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peSecond.Properties.ZoomAccelerationFactor = 1D;
            this.peSecond.Size = new System.Drawing.Size(134, 280);
            this.peSecond.TabIndex = 23;
            // 
            // tbAboutUs
            // 
            this.tbAboutUs.Controls.Add(this.panelControl8);
            this.tbAboutUs.Name = "tbAboutUs";
            this.tbAboutUs.Size = new System.Drawing.Size(538, 286);
            this.tbAboutUs.Text = "关于本系统";
            // 
            // panelControl8
            // 
            this.panelControl8.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl8.Appearance.Options.UseBackColor = true;
            this.panelControl8.Controls.Add(this.panelControl2);
            this.panelControl8.Controls.Add(this.pnlThird);
            this.panelControl8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl8.Location = new System.Drawing.Point(0, 0);
            this.panelControl8.Name = "panelControl8";
            this.panelControl8.Size = new System.Drawing.Size(538, 286);
            this.panelControl8.TabIndex = 20;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.lblCompany);
            this.panelControl2.Controls.Add(this.lblExcepiton);
            this.panelControl2.Controls.Add(this.meException);
            this.panelControl2.Controls.Add(this.lbcClientVersions);
            this.panelControl2.Controls.Add(this.txtReleasedDate);
            this.panelControl2.Controls.Add(this.lblClientVersions);
            this.panelControl2.Controls.Add(this.lblReleasedDate);
            this.panelControl2.Controls.Add(this.txtServerVersion);
            this.panelControl2.Controls.Add(this.lblServerVersion);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(142, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(394, 282);
            this.panelControl2.TabIndex = 20;
            // 
            // lblCompany
            // 
            this.lblCompany.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblCompany.Location = new System.Drawing.Point(115, 258);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(255, 14);
            this.lblCompany.TabIndex = 18;
            this.lblCompany.Text = "成都家易科技有限公司 Copyright © 2006-2019";
            // 
            // lblExcepiton
            // 
            this.lblExcepiton.Location = new System.Drawing.Point(69, 182);
            this.lblExcepiton.Name = "lblExcepiton";
            this.lblExcepiton.Size = new System.Drawing.Size(84, 14);
            this.lblExcepiton.TabIndex = 17;
            this.lblExcepiton.Text = "系统异常信息：";
            // 
            // meException
            // 
            this.meException.EditValue = "无";
            this.meException.Location = new System.Drawing.Point(159, 179);
            this.meException.Name = "meException";
            this.meException.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.meException.Properties.Appearance.Options.UseBackColor = true;
            this.meException.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.meException.Properties.ReadOnly = true;
            this.meException.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.meException.Size = new System.Drawing.Size(211, 69);
            this.meException.TabIndex = 6;
            // 
            // lbcClientVersions
            // 
            this.lbcClientVersions.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbcClientVersions.Location = new System.Drawing.Point(159, 77);
            this.lbcClientVersions.Name = "lbcClientVersions";
            this.lbcClientVersions.Size = new System.Drawing.Size(211, 92);
            this.lbcClientVersions.TabIndex = 5;
            // 
            // txtReleasedDate
            // 
            this.txtReleasedDate.Location = new System.Drawing.Point(159, 44);
            this.txtReleasedDate.Name = "txtReleasedDate";
            this.txtReleasedDate.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.txtReleasedDate.Properties.Appearance.Options.UseBackColor = true;
            this.txtReleasedDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtReleasedDate.Properties.ReadOnly = true;
            this.txtReleasedDate.Size = new System.Drawing.Size(211, 18);
            this.txtReleasedDate.TabIndex = 4;
            // 
            // lblClientVersions
            // 
            this.lblClientVersions.Location = new System.Drawing.Point(21, 78);
            this.lblClientVersions.Name = "lblClientVersions";
            this.lblClientVersions.Size = new System.Drawing.Size(132, 14);
            this.lblClientVersions.TabIndex = 3;
            this.lblClientVersions.Text = "支持的客户端版本列表：";
            // 
            // lblReleasedDate
            // 
            this.lblReleasedDate.Location = new System.Drawing.Point(93, 46);
            this.lblReleasedDate.Name = "lblReleasedDate";
            this.lblReleasedDate.Size = new System.Drawing.Size(60, 14);
            this.lblReleasedDate.TabIndex = 2;
            this.lblReleasedDate.Text = "发布日期：";
            // 
            // txtServerVersion
            // 
            this.txtServerVersion.Location = new System.Drawing.Point(159, 12);
            this.txtServerVersion.Name = "txtServerVersion";
            this.txtServerVersion.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.txtServerVersion.Properties.Appearance.Options.UseBackColor = true;
            this.txtServerVersion.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtServerVersion.Properties.ReadOnly = true;
            this.txtServerVersion.Size = new System.Drawing.Size(211, 18);
            this.txtServerVersion.TabIndex = 1;
            // 
            // lblServerVersion
            // 
            this.lblServerVersion.Location = new System.Drawing.Point(57, 14);
            this.lblServerVersion.Name = "lblServerVersion";
            this.lblServerVersion.Size = new System.Drawing.Size(96, 14);
            this.lblServerVersion.TabIndex = 0;
            this.lblServerVersion.Text = "服务器端版本号：";
            // 
            // pnlThird
            // 
            this.pnlThird.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnlThird.Appearance.Options.UseBackColor = true;
            this.pnlThird.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlThird.Controls.Add(this.peThird);
            this.pnlThird.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlThird.Location = new System.Drawing.Point(2, 2);
            this.pnlThird.Name = "pnlThird";
            this.pnlThird.Padding = new System.Windows.Forms.Padding(3);
            this.pnlThird.Size = new System.Drawing.Size(140, 282);
            this.pnlThird.TabIndex = 19;
            // 
            // peThird
            // 
            this.peThird.Cursor = System.Windows.Forms.Cursors.Default;
            this.peThird.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peThird.EditValue = global::Blue.WindowsFormsServer.Properties.Resources.Third;
            this.peThird.Location = new System.Drawing.Point(3, 3);
            this.peThird.Name = "peThird";
            this.peThird.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.peThird.Properties.Appearance.Options.UseBackColor = true;
            this.peThird.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.peThird.Properties.ZoomAccelerationFactor = 1D;
            this.peThird.Size = new System.Drawing.Size(134, 276);
            this.peThird.TabIndex = 23;
            // 
            // tmUpdate
            // 
            this.tmUpdate.Interval = 1000;
            this.tmUpdate.Tick += new System.EventHandler(this.tmUpdate_Tick);
            // 
            // bgwConnection
            // 
            this.bgwConnection.WorkerSupportsCancellation = true;
            this.bgwConnection.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwConnection_DoWork);
            this.bgwConnection.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwConnection_RunWorkerCompleted);
            // 
            // bgwMachineCode
            // 
            this.bgwMachineCode.WorkerSupportsCancellation = true;
            this.bgwMachineCode.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwMachineCode_DoWork);
            this.bgwMachineCode.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwMachineCode_RunWorkerCompleted);
            // 
            // bgwServerDataShow
            // 
            this.bgwServerDataShow.WorkerReportsProgress = true;
            this.bgwServerDataShow.WorkerSupportsCancellation = true;
            this.bgwServerDataShow.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwServerDataShow_DoWork);
            this.bgwServerDataShow.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwServerDataShow_RunWorkerCompleted);
            // 
            // bgwEnbaleWindowsService
            // 
            this.bgwEnbaleWindowsService.WorkerSupportsCancellation = true;
            this.bgwEnbaleWindowsService.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwEnbaleWindowsService_DoWork);
            this.bgwEnbaleWindowsService.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwEnbaleWindowsService_RunWorkerCompleted);
            // 
            // lblKeyTip
            // 
            this.lblKeyTip.Location = new System.Drawing.Point(73, 116);
            this.lblKeyTip.Name = "lblKeyTip";
            this.lblKeyTip.Size = new System.Drawing.Size(242, 14);
            this.lblKeyTip.TabIndex = 32;
            this.lblKeyTip.Text = "提示：服务器端和Web端共享同一个关键字。";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 315);
            this.Controls.Add(this.xtbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务器端管理工具";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtbMain)).EndInit();
            this.xtbMain.ResumeLayout(false);
            this.tbGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlConfigRight)).EndInit();
            this.pnlConfigRight.ResumeLayout(false);
            this.pnlConfigRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chklInterface.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meWarning.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRunningTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartedTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlConfigLeft)).EndInit();
            this.pnlConfigLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peMain.Properties)).EndInit();
            this.tbConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDatabase)).EndInit();
            this.gcDatabase.ResumeLayout(false);
            this.gcDatabase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConnectionStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcWebPath)).EndInit();
            this.gcWebPath.ResumeLayout(false);
            this.gcWebPath.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupDir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFirst)).EndInit();
            this.pnlFirst.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peFirst.Properties)).EndInit();
            this.tbRegister.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMainRegister)).EndInit();
            this.pnlMainRegister.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRegister)).EndInit();
            this.gcRegister.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpnlRegisteredCode)).EndInit();
            this.fpnlRegisteredCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpcRegisteredCode)).EndInit();
            this.fpcRegisteredCode.ResumeLayout(false);
            this.fpcRegisteredCode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meRegisteredCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hleRegister.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpnlMachineCode)).EndInit();
            this.fpnlMachineCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpcMachineCode)).EndInit();
            this.fpcMachineCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.meMachineCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegisteredInfo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSecond)).EndInit();
            this.pnlSecond.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peSecond.Properties)).EndInit();
            this.tbAboutUs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl8)).EndInit();
            this.panelControl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meException.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbcClientVersions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReleasedDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlThird)).EndInit();
            this.pnlThird.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peThird.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.SimpleButton btnRestart;
        private DevExpress.XtraEditors.SimpleButton btnStop;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraTab.XtraTabControl xtbMain;
        private DevExpress.XtraTab.XtraTabPage tbGeneral;
        private DevExpress.XtraEditors.TextEdit txtRunningTime;
        private DevExpress.XtraEditors.TextEdit txtStatus;
        private DevExpress.XtraEditors.TextEdit txtStartedTime;
        private DevExpress.XtraEditors.TextEdit txtServerName;
        private DevExpress.XtraEditors.LabelControl lblRunningTime;
        private DevExpress.XtraEditors.LabelControl lblStartedTime;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.LabelControl lblServerName;
        private DevExpress.XtraEditors.MemoEdit meWarning;
        private DevExpress.XtraEditors.LabelControl lblWarning;
        private System.Windows.Forms.Timer tmUpdate;
        private DevExpress.XtraTab.XtraTabPage tbRegister;
        private DevExpress.XtraEditors.PanelControl pnlConfigRight;
        private DevExpress.XtraTab.XtraTabPage tbConfig;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.GroupControl gcDatabase;
        private DevExpress.XtraEditors.SimpleButton btnSaveSetting;
        private DevExpress.XtraEditors.SimpleButton btnTestDatabase;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl lblPassword;
        private DevExpress.XtraEditors.TextEdit txtAddress;
        private DevExpress.XtraEditors.LabelControl lblAddress;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.GroupControl gcWebPath;
        private DevExpress.XtraEditors.SimpleButton btnBrowser;
        private DevExpress.XtraEditors.TextEdit txtPath;
        private DevExpress.XtraEditors.PanelControl pnlFirst;
        private DevExpress.XtraEditors.PanelControl pnlSecond;
        private DevExpress.XtraEditors.PanelControl pnlConfigLeft;
        private DevExpress.XtraEditors.PanelControl pnlMainRegister;
        private DevExpress.XtraEditors.TextEdit txtConnectionStatus;
        private DevExpress.XtraEditors.LabelControl lblConnectionStatus;
        private DevExpress.XtraEditors.SimpleButton btnRegister;
        private DevExpress.XtraEditors.HyperLinkEdit hleRegister;
        private DevExpress.XtraTab.XtraTabPage tbAboutUs;
        private DevExpress.XtraEditors.PanelControl panelControl8;
        private System.ComponentModel.BackgroundWorker bgwConnection;
        private System.ComponentModel.BackgroundWorker bgwMachineCode;
        private DevExpress.XtraEditors.PictureEdit peMain;
        private DevExpress.XtraEditors.PictureEdit peFirst;
        private DevExpress.XtraEditors.PictureEdit peSecond;
        private DevExpress.XtraEditors.PanelControl pnlThird;
        private DevExpress.XtraEditors.PictureEdit peThird;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private DevExpress.XtraEditors.LabelControl lblAddressTip;
        private DevExpress.XtraEditors.LabelControl lblPasswordTip;
        private DevExpress.XtraEditors.LabelControl lblUserNameTip;
        private System.ComponentModel.BackgroundWorker bgwServerDataShow;
        private DevExpress.XtraEditors.GroupControl gcRegister;
        private DevExpress.XtraEditors.MemoEdit txtRegisteredInfo;
        private DevExpress.Utils.FlyoutPanel fpnlMachineCode;
        private DevExpress.Utils.FlyoutPanelControl fpcMachineCode;
        private DevExpress.XtraEditors.MemoEdit meMachineCode;
        private DevExpress.Utils.FlyoutPanel fpnlRegisteredCode;
        private DevExpress.Utils.FlyoutPanelControl fpcRegisteredCode;
        private DevExpress.XtraEditors.MemoEdit meRegisteredCode;
        private DevExpress.XtraEditors.SimpleButton btnSumbit;
        private DevExpress.XtraEditors.LabelControl lblRegisteredTip;
        private DevExpress.XtraEditors.ListBoxControl lbcClientVersions;
        private DevExpress.XtraEditors.TextEdit txtReleasedDate;
        private DevExpress.XtraEditors.LabelControl lblClientVersions;
        private DevExpress.XtraEditors.LabelControl lblReleasedDate;
        private DevExpress.XtraEditors.TextEdit txtServerVersion;
        private DevExpress.XtraEditors.LabelControl lblServerVersion;
        private DevExpress.XtraEditors.SimpleButton btnKeyName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtKeyName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.ComponentModel.BackgroundWorker bgwEnbaleWindowsService;
        private DevExpress.XtraEditors.CheckEdit chklInterface;
        private DevExpress.XtraEditors.LabelControl lblInterface;
        private DevExpress.XtraEditors.LabelControl lblBackupDir;
        private DevExpress.XtraEditors.SimpleButton btnBackupDir;
        private DevExpress.XtraEditors.TextEdit txtBackupDir;
        private DevExpress.XtraEditors.MemoEdit meException;
        private DevExpress.XtraEditors.LabelControl lblExcepiton;
        private DevExpress.XtraEditors.LabelControl lblCompany;
        private DevExpress.XtraEditors.LabelControl lblBackupTime;
        private DevExpress.XtraEditors.TextEdit txtBackupTime;
        private DevExpress.XtraEditors.LabelControl lblKeyTip;
    }
}

