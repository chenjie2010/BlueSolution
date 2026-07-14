namespace Blue.WindowsFormsClient.Common
{
    partial class UserListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserListControl));
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.pnlUser = new DevExpress.XtraEditors.PanelControl();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.grdUsers = new AppFramework.WinFormsControls.DataGrid.DevExpressGridWithPhoto();
            this.gcCondition = new DevExpress.XtraEditors.GroupControl();
            this.cmbQueriedDepartment = new Blue.WindowsFormsClient.TreeDropdownList();
            this.cmbQueriedUserType = new Blue.WindowsFormsClient.TreeDropdownList();
            this.lblQueriedDepartment = new DevExpress.XtraEditors.LabelControl();
            this.lblQueriedUserType = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txtCondition = new DevExpress.XtraEditors.TextEdit();
            this.lblCondition = new DevExpress.XtraEditors.LabelControl();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icPhoto = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlUser)).BeginInit();
            this.pnlUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).BeginInit();
            this.gcCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlMain.Controls.Add(this.pnlUser);
            this.pnlMain.Controls.Add(this.gcCondition);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(860, 274);
            this.pnlMain.TabIndex = 16;
            // 
            // pnlUser
            // 
            this.pnlUser.Controls.Add(this.progressPanel);
            this.pnlUser.Controls.Add(this.grdUsers);
            this.pnlUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUser.Location = new System.Drawing.Point(0, 50);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(860, 224);
            this.pnlUser.TabIndex = 15;
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "数据正在加载......";
            this.progressPanel.Location = new System.Drawing.Point(403, 73);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(150, 50);
            this.progressPanel.TabIndex = 8;
            this.progressPanel.Text = "数据加载中......";
            this.progressPanel.Visible = false;
            // 
            // grdUsers
            // 
            this.grdUsers.CheckboxColumnCaption = null;
            this.grdUsers.ColumnHeaderTexts = new string[] {
        "用户名",
        "用户姓名",
        "用户类型名称",
        "单位名称",
        "证件号",
        "电话号码"};
            this.grdUsers.DataKeyNames = new string[] {
        "UserId"};
            this.grdUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUsers.ExportedExcel = false;
            this.grdUsers.FootText = null;
            this.grdUsers.ImportedExcel = false;
            this.grdUsers.IsMainTable = false;
            this.grdUsers.IsPhotoShowed = true;
            this.grdUsers.Location = new System.Drawing.Point(2, 2);
            this.grdUsers.Name = "grdUsers";
            this.grdUsers.PageSize = 50;
            this.grdUsers.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.grdUsers.Size = new System.Drawing.Size(856, 220);
            this.grdUsers.TabIndex = 9;
            this.grdUsers.UserPhoto = null;
            this.grdUsers.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.grdUsers_OnPageIndexChanged);
            this.grdUsers.OnRowClick += new System.EventHandler<AppFramework.WinFormsControls.RowEvent>(this.grdUsers_OnRowClick);
            this.grdUsers.OnFocusedRowChanged += new System.EventHandler<DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs>(this.grdUsers_OnFocusedRowChanged);
            this.grdUsers.OnFocusedColumnChanged += new System.EventHandler<DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs>(this.grdUsers_OnFocusedColumnChanged);
            // 
            // gcCondition
            // 
            this.gcCondition.CaptionImage = ((System.Drawing.Image)(resources.GetObject("gcCondition.CaptionImage")));
            this.gcCondition.Controls.Add(this.cmbQueriedDepartment);
            this.gcCondition.Controls.Add(this.cmbQueriedUserType);
            this.gcCondition.Controls.Add(this.lblQueriedDepartment);
            this.gcCondition.Controls.Add(this.lblQueriedUserType);
            this.gcCondition.Controls.Add(this.btnClear);
            this.gcCondition.Controls.Add(this.btnQuery);
            this.gcCondition.Controls.Add(this.txtCondition);
            this.gcCondition.Controls.Add(this.lblCondition);
            this.gcCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcCondition.Location = new System.Drawing.Point(0, 0);
            this.gcCondition.Name = "gcCondition";
            this.gcCondition.Size = new System.Drawing.Size(860, 50);
            this.gcCondition.TabIndex = 13;
            this.gcCondition.Text = "用户查询";
            // 
            // cmbQueriedDepartment
            // 
            this.cmbQueriedDepartment.Location = new System.Drawing.Point(530, 26);
            this.cmbQueriedDepartment.Name = "cmbQueriedDepartment";
            this.cmbQueriedDepartment.ShowSearch = true;
            this.cmbQueriedDepartment.Size = new System.Drawing.Size(170, 21);
            this.cmbQueriedDepartment.SkinName = "Blue";
            this.cmbQueriedDepartment.TabIndex = 21;
            this.cmbQueriedDepartment.TreeDropdownHandler = null;
            // 
            // cmbQueriedUserType
            // 
            this.cmbQueriedUserType.Location = new System.Drawing.Point(289, 26);
            this.cmbQueriedUserType.Name = "cmbQueriedUserType";
            this.cmbQueriedUserType.OnlySelectedLeaf = true;
            this.cmbQueriedUserType.Size = new System.Drawing.Size(170, 21);
            this.cmbQueriedUserType.SkinName = "Blue";
            this.cmbQueriedUserType.TabIndex = 22;
            this.cmbQueriedUserType.TreeDropdownHandler = null;
            // 
            // lblQueriedDepartment
            // 
            this.lblQueriedDepartment.Location = new System.Drawing.Point(467, 28);
            this.lblQueriedDepartment.Name = "lblQueriedDepartment";
            this.lblQueriedDepartment.Size = new System.Drawing.Size(60, 14);
            this.lblQueriedDepartment.TabIndex = 9;
            this.lblQueriedDepartment.Text = "所属单位：";
            // 
            // lblQueriedUserType
            // 
            this.lblQueriedUserType.Location = new System.Drawing.Point(227, 29);
            this.lblQueriedUserType.Name = "lblQueriedUserType";
            this.lblQueriedUserType.Size = new System.Drawing.Size(60, 14);
            this.lblQueriedUserType.TabIndex = 7;
            this.lblQueriedUserType.Text = "用户类型：";
            // 
            // btnClear
            // 
            this.btnClear.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.btnClear.ImageIndex = 1;
            this.btnClear.Location = new System.Drawing.Point(780, 25);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 20);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "清除(&R)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Image = global::Blue.WindowsFormsClient.Properties.Resources.Buttom_Quer_Small;
            this.btnQuery.ImageIndex = 0;
            this.btnQuery.Location = new System.Drawing.Point(705, 25);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(70, 20);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtCondition
            // 
            this.txtCondition.EditValue = "";
            this.txtCondition.Location = new System.Drawing.Point(63, 26);
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Properties.MaxLength = 32;
            this.txtCondition.Properties.NullValuePrompt = "请输入用户名、姓名或证件号码";
            this.txtCondition.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCondition.Size = new System.Drawing.Size(160, 20);
            this.txtCondition.TabIndex = 20;
            this.txtCondition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCondition_KeyPress);
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(6, 28);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(60, 14);
            this.lblCondition.TabIndex = 0;
            this.lblCondition.Text = "查询条件：";
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.MaxItemId = 0;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(860, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 274);
            this.barDockControlBottom.Size = new System.Drawing.Size(860, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 274);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(860, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 274);
            // 
            // icPhoto
            // 
            this.icPhoto.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icPhoto.ImageStream")));
            this.icPhoto.Images.SetKeyName(0, "Common_Boolean_No.png");
            this.icPhoto.Images.SetKeyName(1, "Common_Boolean_Yes.png");
            // 
            // UserListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UserListControl";
            this.Size = new System.Drawing.Size(860, 274);
            this.Load += new System.EventHandler(this.UserListControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlUser)).EndInit();
            this.pnlUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).EndInit();
            this.gcCondition.ResumeLayout(false);
            this.gcCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.XtraEditors.GroupControl gcCondition;
        private TreeDropdownList cmbQueriedDepartment;
        private TreeDropdownList cmbQueriedUserType;
        private DevExpress.XtraEditors.LabelControl lblQueriedDepartment;
        private DevExpress.XtraEditors.LabelControl lblQueriedUserType;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.TextEdit txtCondition;
        private DevExpress.XtraEditors.LabelControl lblCondition;
        private DevExpress.Utils.ImageCollection icPhoto;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.PanelControl pnlUser;
        private AppFramework.WinFormsControls.DataGrid.DevExpressGridWithPhoto grdUsers;
    }
}
