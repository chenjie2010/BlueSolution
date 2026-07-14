namespace Blue.WindowsFormsClient.DataConvertionModule
{
    partial class DataImportForm
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
            DevExpress.XtraEditors.GroupControl gcSwap;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataImportForm));
            this.label5 = new System.Windows.Forms.Label();
            this.icmbCurrentState = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icCurrentState = new DevExpress.Utils.ImageCollection(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.chkValidationRequired = new System.Windows.Forms.CheckBox();
            this.lblValidationRequired = new System.Windows.Forms.Label();
            this.chkNotExistedUser = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.icmbImportedKeyName = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiLoad = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCellFormat = new DevExpress.XtraBars.BarButtonItem();
            this.bbiVerify = new DevExpress.XtraBars.BarButtonItem();
            this.bbiShowResult = new DevExpress.XtraBars.BarButtonItem();
            this.bbiImport = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.bbiErrorData = new DevExpress.XtraBars.BarButtonItem();
            this.bbiErrorDataImported = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSkippedDataImported = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCloseForm = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.btnItmDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmBatchDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClear = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmReset = new DevExpress.XtraBars.BarButtonItem();
            this.icmbImport = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icmbAudit = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblDataSourceTip = new System.Windows.Forms.Label();
            this.lblAuditTip = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblImportTip = new System.Windows.Forms.Label();
            this.beTable = new DevExpress.XtraEditors.ButtonEdit();
            this.lblImportedKeyName = new System.Windows.Forms.Label();
            this.lblAudit = new System.Windows.Forms.Label();
            this.chkAddtionalType = new System.Windows.Forms.CheckBox();
            this.lblAddtionalType = new System.Windows.Forms.Label();
            this.lblImport = new System.Windows.Forms.Label();
            this.chkAllowNull = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLocalTable = new System.Windows.Forms.Label();
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.gcDataField = new DevExpress.XtraEditors.GroupControl();
            this.gcDataFieldRelation = new DevExpress.XtraGrid.GridControl();
            this.gridViewInput = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColDestination = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColRequiredDataField = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricmbSource = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridColCondition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribtnCondition = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.fpError = new DevExpress.Utils.FlyoutPanel();
            this.flyoutPanelControl2 = new DevExpress.Utils.FlyoutPanelControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblErrorTip = new DevExpress.XtraEditors.LabelControl();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.fpDataImported = new FarPoint.Win.Spread.FpSpread();
            this.fpDataImported_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlStatus = new DevExpress.XtraEditors.PanelControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.lblDataSource = new System.Windows.Forms.Label();
            this.icmbDataSource = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icDataSource = new DevExpress.Utils.ImageCollection(this.components);
            gcSwap = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(gcSwap)).BeginInit();
            gcSwap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbCurrentState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icCurrentState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbImportedKeyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbImport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbAudit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beTable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataField)).BeginInit();
            this.gcDataField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFieldRelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribtnCondition)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.fpDataImported)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDataImported_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlStatus)).BeginInit();
            this.pnlStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gcSwap
            // 
            gcSwap.Controls.Add(this.icmbDataSource);
            gcSwap.Controls.Add(this.label6);
            gcSwap.Controls.Add(this.lblDataSource);
            gcSwap.Controls.Add(this.label5);
            gcSwap.Controls.Add(this.icmbCurrentState);
            gcSwap.Controls.Add(this.label4);
            gcSwap.Controls.Add(this.chkValidationRequired);
            gcSwap.Controls.Add(this.lblValidationRequired);
            gcSwap.Controls.Add(this.chkNotExistedUser);
            gcSwap.Controls.Add(this.label3);
            gcSwap.Controls.Add(this.icmbImportedKeyName);
            gcSwap.Controls.Add(this.icmbImport);
            gcSwap.Controls.Add(this.icmbAudit);
            gcSwap.Controls.Add(this.lblDataSourceTip);
            gcSwap.Controls.Add(this.lblAuditTip);
            gcSwap.Controls.Add(this.label2);
            gcSwap.Controls.Add(this.lblImportTip);
            gcSwap.Controls.Add(this.beTable);
            gcSwap.Controls.Add(this.lblImportedKeyName);
            gcSwap.Controls.Add(this.lblAudit);
            gcSwap.Controls.Add(this.chkAddtionalType);
            gcSwap.Controls.Add(this.lblAddtionalType);
            gcSwap.Controls.Add(this.lblImport);
            gcSwap.Controls.Add(this.chkAllowNull);
            gcSwap.Controls.Add(this.label1);
            gcSwap.Controls.Add(this.lblLocalTable);
            gcSwap.Dock = System.Windows.Forms.DockStyle.Top;
            gcSwap.Location = new System.Drawing.Point(2, 2);
            gcSwap.Name = "gcSwap";
            gcSwap.Size = new System.Drawing.Size(355, 319);
            gcSwap.TabIndex = 0;
            gcSwap.Text = "交换条件";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 14);
            this.label5.TabIndex = 263;
            this.label5.Text = "提示：主从表数据更新条件";
            // 
            // icmbCurrentState
            // 
            this.icmbCurrentState.Enabled = false;
            this.icmbCurrentState.Location = new System.Drawing.Point(92, 180);
            this.icmbCurrentState.Name = "icmbCurrentState";
            this.icmbCurrentState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbCurrentState.Properties.LookAndFeel.SkinName = "Blue";
            this.icmbCurrentState.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.icmbCurrentState.Properties.SmallImages = this.icCurrentState;
            this.icmbCurrentState.Size = new System.Drawing.Size(89, 20);
            this.icmbCurrentState.TabIndex = 4;
            // 
            // icCurrentState
            // 
            this.icCurrentState.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icCurrentState.ImageStream")));
            this.icCurrentState.Images.SetKeyName(0, "Client_CurrentState_History.png");
            this.icCurrentState.Images.SetKeyName(1, "Client_CurrentState_Current.png");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 261;
            this.label4.Text = "更新条件：";
            // 
            // chkValidationRequired
            // 
            this.chkValidationRequired.AutoSize = true;
            this.chkValidationRequired.BackColor = System.Drawing.Color.Transparent;
            this.chkValidationRequired.Checked = true;
            this.chkValidationRequired.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkValidationRequired.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkValidationRequired.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkValidationRequired.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkValidationRequired.Location = new System.Drawing.Point(92, 293);
            this.chkValidationRequired.Name = "chkValidationRequired";
            this.chkValidationRequired.Size = new System.Drawing.Size(105, 16);
            this.chkValidationRequired.TabIndex = 5;
            this.chkValidationRequired.Text = "导入前数据校验";
            this.chkValidationRequired.UseVisualStyleBackColor = false;
            // 
            // lblValidationRequired
            // 
            this.lblValidationRequired.AutoSize = true;
            this.lblValidationRequired.Location = new System.Drawing.Point(10, 294);
            this.lblValidationRequired.Name = "lblValidationRequired";
            this.lblValidationRequired.Size = new System.Drawing.Size(79, 14);
            this.lblValidationRequired.TabIndex = 259;
            this.lblValidationRequired.Text = "导入前操作：";
            // 
            // chkNotExistedUser
            // 
            this.chkNotExistedUser.AutoSize = true;
            this.chkNotExistedUser.BackColor = System.Drawing.Color.Transparent;
            this.chkNotExistedUser.Checked = true;
            this.chkNotExistedUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNotExistedUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkNotExistedUser.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkNotExistedUser.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkNotExistedUser.Location = new System.Drawing.Point(92, 212);
            this.chkNotExistedUser.Name = "chkNotExistedUser";
            this.chkNotExistedUser.Size = new System.Drawing.Size(117, 16);
            this.chkNotExistedUser.TabIndex = 5;
            this.chkNotExistedUser.Text = "用户不存在则忽略";
            this.chkNotExistedUser.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 258;
            this.label3.Text = "用户条件：";
            // 
            // icmbImportedKeyName
            // 
            this.icmbImportedKeyName.Location = new System.Drawing.Point(92, 149);
            this.icmbImportedKeyName.MenuManager = this.barManager;
            this.icmbImportedKeyName.Name = "icmbImportedKeyName";
            this.icmbImportedKeyName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbImportedKeyName.Size = new System.Drawing.Size(236, 20);
            this.icmbImportedKeyName.TabIndex = 3;
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.AllowShowToolbarsPopup = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.CloseButtonAffectAllTabs = false;
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.icTools;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiLoad,
            this.bbiCellFormat,
            this.bbiVerify,
            this.bbiShowResult,
            this.bbiImport,
            this.bbiSaveAs,
            this.bbiErrorData,
            this.bbiErrorDataImported,
            this.bbiCloseForm,
            this.btnItmDelete,
            this.btnItmBatchDelete,
            this.btnItmClear,
            this.btnItmReset,
            this.bbiSkippedDataImported});
            this.barManager.MaxItemId = 14;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bbiLoad, "加载数据...(&I)", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiCellFormat, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiVerify, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiShowResult, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiImport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiSaveAs, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiErrorData, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiErrorDataImported, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.bbiSkippedDataImported, "忽略数据另存...(&K)", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiCloseForm, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bbiLoad
            // 
            this.bbiLoad.Caption = "barButtonItem1";
            this.bbiLoad.Id = 0;
            this.bbiLoad.ImageIndex = 0;
            this.bbiLoad.Name = "bbiLoad";
            this.bbiLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLoad_ItemClick);
            // 
            // bbiCellFormat
            // 
            this.bbiCellFormat.Caption = "设置单元格格式...(&X)";
            this.bbiCellFormat.Id = 1;
            this.bbiCellFormat.ImageIndex = 1;
            this.bbiCellFormat.Name = "bbiCellFormat";
            this.bbiCellFormat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCellFormat_ItemClick);
            // 
            // bbiVerify
            // 
            this.bbiVerify.Caption = "数据校验(&V)";
            this.bbiVerify.Id = 2;
            this.bbiVerify.ImageIndex = 2;
            this.bbiVerify.Name = "bbiVerify";
            this.bbiVerify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiVerify_ItemClick);
            // 
            // bbiShowResult
            // 
            this.bbiShowResult.Caption = "显示校验结果(&E)";
            this.bbiShowResult.Id = 3;
            this.bbiShowResult.ImageIndex = 3;
            this.bbiShowResult.Name = "bbiShowResult";
            this.bbiShowResult.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiShowResult_ItemClick);
            // 
            // bbiImport
            // 
            this.bbiImport.Caption = "导入数据(&D)";
            this.bbiImport.Id = 4;
            this.bbiImport.ImageIndex = 4;
            this.bbiImport.Name = "bbiImport";
            this.bbiImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiImport_ItemClick);
            // 
            // bbiSaveAs
            // 
            this.bbiSaveAs.Caption = "Excel数据另存...(&S)";
            this.bbiSaveAs.Id = 5;
            this.bbiSaveAs.ImageIndex = 5;
            this.bbiSaveAs.Name = "bbiSaveAs";
            this.bbiSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSaveAs_ItemClick);
            // 
            // bbiErrorData
            // 
            this.bbiErrorData.Caption = "校验错误数据另存...(&R)";
            this.bbiErrorData.Id = 6;
            this.bbiErrorData.ImageIndex = 6;
            this.bbiErrorData.Name = "bbiErrorData";
            this.bbiErrorData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiErrorData_ItemClick);
            // 
            // bbiErrorDataImported
            // 
            this.bbiErrorDataImported.Caption = "导入错误数据另存...(&T)";
            this.bbiErrorDataImported.Id = 7;
            this.bbiErrorDataImported.ImageIndex = 7;
            this.bbiErrorDataImported.Name = "bbiErrorDataImported";
            this.bbiErrorDataImported.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiErrorDataImported_ItemClick);
            // 
            // bbiSkippedDataImported
            // 
            this.bbiSkippedDataImported.Caption = "忽略数据另存...(&K)";
            this.bbiSkippedDataImported.Enabled = false;
            this.bbiSkippedDataImported.Id = 13;
            this.bbiSkippedDataImported.ImageIndex = 8;
            this.bbiSkippedDataImported.Name = "bbiSkippedDataImported";
            this.bbiSkippedDataImported.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSkippedDataImported_ItemClick);
            // 
            // bbiCloseForm
            // 
            this.bbiCloseForm.Caption = "关闭(&C)";
            this.bbiCloseForm.Id = 8;
            this.bbiCloseForm.ImageIndex = 9;
            this.bbiCloseForm.Name = "bbiCloseForm";
            this.bbiCloseForm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCloseForm_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(1262, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 555);
            this.barDockControlBottom.Size = new System.Drawing.Size(1262, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 529);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1262, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 529);
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
            this.icTools.Images.SetKeyName(8, "Tools_Skip.png");
            this.icTools.Images.SetKeyName(9, "Common_Close_1.png");
            // 
            // btnItmDelete
            // 
            this.btnItmDelete.Caption = "删除当前行(&D)";
            this.btnItmDelete.Id = 9;
            this.btnItmDelete.Name = "btnItmDelete";
            this.btnItmDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmDelete_ItemClick);
            // 
            // btnItmBatchDelete
            // 
            this.btnItmBatchDelete.Caption = "批量删除选定行(&B)";
            this.btnItmBatchDelete.Id = 10;
            this.btnItmBatchDelete.Name = "btnItmBatchDelete";
            this.btnItmBatchDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmBatchDelete_ItemClick);
            // 
            // btnItmClear
            // 
            this.btnItmClear.Caption = "清除(&C)";
            this.btnItmClear.Id = 11;
            this.btnItmClear.Name = "btnItmClear";
            this.btnItmClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmClear_ItemClick);
            // 
            // btnItmReset
            // 
            this.btnItmReset.Caption = "重置(&R)";
            this.btnItmReset.Id = 12;
            this.btnItmReset.Name = "btnItmReset";
            this.btnItmReset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmReset_ItemClick);
            // 
            // icmbImport
            // 
            this.icmbImport.Location = new System.Drawing.Point(92, 89);
            this.icmbImport.MenuManager = this.barManager;
            this.icmbImport.Name = "icmbImport";
            this.icmbImport.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbImport.Size = new System.Drawing.Size(236, 20);
            this.icmbImport.TabIndex = 1;
            this.icmbImport.SelectedIndexChanged += new System.EventHandler(this.icmbImport_SelectedIndexChanged);
            // 
            // icmbAudit
            // 
            this.icmbAudit.Location = new System.Drawing.Point(92, 119);
            this.icmbAudit.MenuManager = this.barManager;
            this.icmbAudit.Name = "icmbAudit";
            this.icmbAudit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbAudit.Size = new System.Drawing.Size(236, 20);
            this.icmbAudit.TabIndex = 2;
            // 
            // lblDataSourceTip
            // 
            this.lblDataSourceTip.AutoSize = true;
            this.lblDataSourceTip.ForeColor = System.Drawing.Color.Red;
            this.lblDataSourceTip.Location = new System.Drawing.Point(334, 155);
            this.lblDataSourceTip.Name = "lblDataSourceTip";
            this.lblDataSourceTip.Size = new System.Drawing.Size(14, 14);
            this.lblDataSourceTip.TabIndex = 253;
            this.lblDataSourceTip.Text = "*";
            // 
            // lblAuditTip
            // 
            this.lblAuditTip.AutoSize = true;
            this.lblAuditTip.ForeColor = System.Drawing.Color.Red;
            this.lblAuditTip.Location = new System.Drawing.Point(335, 126);
            this.lblAuditTip.Name = "lblAuditTip";
            this.lblAuditTip.Size = new System.Drawing.Size(14, 14);
            this.lblAuditTip.TabIndex = 252;
            this.lblAuditTip.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(334, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 14);
            this.label2.TabIndex = 251;
            this.label2.Text = "*";
            // 
            // lblImportTip
            // 
            this.lblImportTip.AutoSize = true;
            this.lblImportTip.ForeColor = System.Drawing.Color.Red;
            this.lblImportTip.Location = new System.Drawing.Point(334, 95);
            this.lblImportTip.Name = "lblImportTip";
            this.lblImportTip.Size = new System.Drawing.Size(14, 14);
            this.lblImportTip.TabIndex = 250;
            this.lblImportTip.Text = "*";
            // 
            // beTable
            // 
            this.beTable.Location = new System.Drawing.Point(91, 59);
            this.beTable.MenuManager = this.barManager;
            this.beTable.Name = "beTable";
            this.beTable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beTable.Properties.ReadOnly = true;
            this.beTable.Size = new System.Drawing.Size(235, 20);
            this.beTable.TabIndex = 0;
            this.beTable.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beTable_ButtonPressed);
            // 
            // lblImportedKeyName
            // 
            this.lblImportedKeyName.AutoSize = true;
            this.lblImportedKeyName.Location = new System.Drawing.Point(10, 153);
            this.lblImportedKeyName.Name = "lblImportedKeyName";
            this.lblImportedKeyName.Size = new System.Drawing.Size(79, 14);
            this.lblImportedKeyName.TabIndex = 248;
            this.lblImportedKeyName.Text = "第一列数据：";
            // 
            // lblAudit
            // 
            this.lblAudit.AutoSize = true;
            this.lblAudit.Location = new System.Drawing.Point(22, 121);
            this.lblAudit.Name = "lblAudit";
            this.lblAudit.Size = new System.Drawing.Size(67, 14);
            this.lblAudit.TabIndex = 246;
            this.lblAudit.Text = "审核状态：";
            // 
            // chkAddtionalType
            // 
            this.chkAddtionalType.AutoSize = true;
            this.chkAddtionalType.BackColor = System.Drawing.Color.Transparent;
            this.chkAddtionalType.Checked = true;
            this.chkAddtionalType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddtionalType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAddtionalType.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAddtionalType.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkAddtionalType.Location = new System.Drawing.Point(92, 267);
            this.chkAddtionalType.Name = "chkAddtionalType";
            this.chkAddtionalType.Size = new System.Drawing.Size(69, 16);
            this.chkAddtionalType.TabIndex = 7;
            this.chkAddtionalType.Text = "包含标题";
            this.chkAddtionalType.UseVisualStyleBackColor = false;
            // 
            // lblAddtionalType
            // 
            this.lblAddtionalType.AutoSize = true;
            this.lblAddtionalType.Location = new System.Drawing.Point(6, 266);
            this.lblAddtionalType.Name = "lblAddtionalType";
            this.lblAddtionalType.Size = new System.Drawing.Size(83, 14);
            this.lblAddtionalType.TabIndex = 244;
            this.lblAddtionalType.Text = "Excel第一行：";
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Location = new System.Drawing.Point(22, 93);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(67, 14);
            this.lblImport.TabIndex = 242;
            this.lblImport.Text = "导入方式：";
            // 
            // chkAllowNull
            // 
            this.chkAllowNull.AutoSize = true;
            this.chkAllowNull.BackColor = System.Drawing.Color.Transparent;
            this.chkAllowNull.Checked = true;
            this.chkAllowNull.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllowNull.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAllowNull.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAllowNull.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkAllowNull.Location = new System.Drawing.Point(92, 240);
            this.chkAllowNull.Name = "chkAllowNull";
            this.chkAllowNull.Size = new System.Drawing.Size(117, 16);
            this.chkAllowNull.TabIndex = 6;
            this.chkAllowNull.Text = "允许必填字段为空";
            this.chkAllowNull.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 240;
            this.label1.Text = "必填字段：";
            // 
            // lblLocalTable
            // 
            this.lblLocalTable.AutoSize = true;
            this.lblLocalTable.Location = new System.Drawing.Point(10, 61);
            this.lblLocalTable.Name = "lblLocalTable";
            this.lblLocalTable.Size = new System.Drawing.Size(79, 14);
            this.lblLocalTable.TabIndex = 238;
            this.lblLocalTable.Text = "导入数据表：";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gcDataField);
            this.pnlLeft.Controls.Add(gcSwap);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 26);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(359, 529);
            this.pnlLeft.TabIndex = 4;
            // 
            // gcDataField
            // 
            this.gcDataField.Controls.Add(this.gcDataFieldRelation);
            this.gcDataField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDataField.Location = new System.Drawing.Point(2, 321);
            this.gcDataField.Name = "gcDataField";
            this.gcDataField.Size = new System.Drawing.Size(355, 206);
            this.gcDataField.TabIndex = 1;
            this.gcDataField.Text = "字段对应关系";
            // 
            // gcDataFieldRelation
            // 
            this.gcDataFieldRelation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDataFieldRelation.Location = new System.Drawing.Point(2, 21);
            this.gcDataFieldRelation.MainView = this.gridViewInput;
            this.gcDataFieldRelation.MenuManager = this.barManager;
            this.gcDataFieldRelation.Name = "gcDataFieldRelation";
            this.gcDataFieldRelation.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ricmbSource,
            this.ribtnCondition});
            this.gcDataFieldRelation.Size = new System.Drawing.Size(351, 183);
            this.gcDataFieldRelation.TabIndex = 13;
            this.gcDataFieldRelation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewInput});
            // 
            // gridViewInput
            // 
            this.gridViewInput.ActiveFilterEnabled = false;
            this.gridViewInput.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridViewInput.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridViewInput.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridViewInput.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridViewInput.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gridViewInput.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gridViewInput.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gridViewInput.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gridViewInput.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(190)))), ((int)(((byte)(243)))));
            this.gridViewInput.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gridViewInput.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridViewInput.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gridViewInput.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gridViewInput.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gridViewInput.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gridViewInput.Appearance.Empty.Options.UseBackColor = true;
            this.gridViewInput.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.gridViewInput.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridViewInput.Appearance.EvenRow.Options.UseForeColor = true;
            this.gridViewInput.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridViewInput.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridViewInput.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridViewInput.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridViewInput.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gridViewInput.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gridViewInput.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gridViewInput.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gridViewInput.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            this.gridViewInput.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gridViewInput.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gridViewInput.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.gridViewInput.Appearance.FixedLine.Options.UseBackColor = true;
            this.gridViewInput.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gridViewInput.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewInput.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gridViewInput.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.gridViewInput.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridViewInput.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewInput.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridViewInput.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridViewInput.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridViewInput.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridViewInput.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridViewInput.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gridViewInput.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gridViewInput.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridViewInput.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridViewInput.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridViewInput.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.GroupButton.Options.UseBackColor = true;
            this.gridViewInput.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gridViewInput.Appearance.GroupButton.Options.UseForeColor = true;
            this.gridViewInput.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridViewInput.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridViewInput.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gridViewInput.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gridViewInput.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gridViewInput.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gridViewInput.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridViewInput.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gridViewInput.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gridViewInput.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridViewInput.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gridViewInput.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gridViewInput.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridViewInput.Appearance.GroupRow.Options.UseBorderColor = true;
            this.gridViewInput.Appearance.GroupRow.Options.UseFont = true;
            this.gridViewInput.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridViewInput.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridViewInput.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gridViewInput.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gridViewInput.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gridViewInput.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridViewInput.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gridViewInput.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridViewInput.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(228)))));
            this.gridViewInput.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(224)))), ((int)(((byte)(251)))));
            this.gridViewInput.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridViewInput.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridViewInput.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gridViewInput.Appearance.HorzLine.Options.UseBackColor = true;
            this.gridViewInput.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gridViewInput.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.OddRow.Options.UseBackColor = true;
            this.gridViewInput.Appearance.OddRow.Options.UseForeColor = true;
            this.gridViewInput.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gridViewInput.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(185)))));
            this.gridViewInput.Appearance.Preview.Options.UseBackColor = true;
            this.gridViewInput.Appearance.Preview.Options.UseForeColor = true;
            this.gridViewInput.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridViewInput.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.gridViewInput.Appearance.Row.Options.UseBackColor = true;
            this.gridViewInput.Appearance.Row.Options.UseForeColor = true;
            this.gridViewInput.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            this.gridViewInput.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gridViewInput.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(126)))), ((int)(((byte)(217)))));
            this.gridViewInput.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gridViewInput.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridViewInput.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridViewInput.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gridViewInput.Appearance.VertLine.Options.UseBackColor = true;
            this.gridViewInput.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColDestination,
            this.gridColRequiredDataField,
            this.gridColSource,
            this.gridColCondition});
            this.gridViewInput.GridControl = this.gcDataFieldRelation;
            this.gridViewInput.IndicatorWidth = 30;
            this.gridViewInput.Name = "gridViewInput";
            this.gridViewInput.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewInput.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewInput.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewInput.OptionsCustomization.AllowFilter = false;
            this.gridViewInput.OptionsCustomization.AllowGroup = false;
            this.gridViewInput.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewInput.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridViewInput.OptionsFilter.AllowFilterEditor = false;
            this.gridViewInput.OptionsFind.AllowFindPanel = false;
            this.gridViewInput.OptionsMenu.EnableColumnMenu = false;
            this.gridViewInput.OptionsMenu.EnableFooterMenu = false;
            this.gridViewInput.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridViewInput.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridViewInput.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gridViewInput.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridViewInput.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewInput.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewInput.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewInput.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewInput.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewInput.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewInput.OptionsView.ShowGroupPanel = false;
            this.gridViewInput.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewInput_CustomDrawRowIndicator);
            this.gridViewInput.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewInput_CellValueChanged);
            this.gridViewInput.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridViewInput_MouseUp);
            // 
            // gridColDestination
            // 
            this.gridColDestination.Caption = "目的字段";
            this.gridColDestination.FieldName = "DestinationName";
            this.gridColDestination.Name = "gridColDestination";
            this.gridColDestination.OptionsColumn.AllowEdit = false;
            this.gridColDestination.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColDestination.OptionsColumn.ReadOnly = true;
            this.gridColDestination.OptionsFilter.AllowAutoFilter = false;
            this.gridColDestination.OptionsFilter.AllowFilter = false;
            this.gridColDestination.Visible = true;
            this.gridColDestination.VisibleIndex = 0;
            this.gridColDestination.Width = 103;
            // 
            // gridColRequiredDataField
            // 
            this.gridColRequiredDataField.Caption = "必填";
            this.gridColRequiredDataField.FieldName = "RequiredDataField";
            this.gridColRequiredDataField.Name = "gridColRequiredDataField";
            this.gridColRequiredDataField.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColRequiredDataField.OptionsFilter.AllowAutoFilter = false;
            this.gridColRequiredDataField.OptionsFilter.AllowFilter = false;
            this.gridColRequiredDataField.Visible = true;
            this.gridColRequiredDataField.VisibleIndex = 1;
            this.gridColRequiredDataField.Width = 41;
            // 
            // gridColSource
            // 
            this.gridColSource.Caption = "源字段";
            this.gridColSource.ColumnEdit = this.ricmbSource;
            this.gridColSource.FieldName = "SourceId";
            this.gridColSource.Name = "gridColSource";
            this.gridColSource.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColSource.OptionsFilter.AllowAutoFilter = false;
            this.gridColSource.OptionsFilter.AllowFilter = false;
            this.gridColSource.Visible = true;
            this.gridColSource.VisibleIndex = 2;
            this.gridColSource.Width = 99;
            // 
            // ricmbSource
            // 
            this.ricmbSource.AutoHeight = false;
            this.ricmbSource.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ricmbSource.Name = "ricmbSource";
            // 
            // gridColCondition
            // 
            this.gridColCondition.Caption = "目的字段条件";
            this.gridColCondition.ColumnEdit = this.ribtnCondition;
            this.gridColCondition.FieldName = "Condition";
            this.gridColCondition.Name = "gridColCondition";
            this.gridColCondition.Visible = true;
            this.gridColCondition.VisibleIndex = 3;
            this.gridColCondition.Width = 90;
            // 
            // ribtnCondition
            // 
            this.ribtnCondition.AutoHeight = false;
            this.ribtnCondition.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ribtnCondition.Name = "ribtnCondition";
            this.ribtnCondition.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ribtnCondition_ButtonPressed);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.progressPanel);
            this.pnlMain.Controls.Add(this.fpError);
            this.pnlMain.Controls.Add(this.fpDataImported);
            this.pnlMain.Controls.Add(this.pnlStatus);
            this.pnlMain.Controls.Add(this.pnlTop);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(359, 26);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(903, 529);
            this.pnlMain.TabIndex = 5;
            // 
            // progressPanel
            // 
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "数据正在导入中，请稍后......";
            this.progressPanel.Location = new System.Drawing.Point(356, 288);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(213, 50);
            this.progressPanel.TabIndex = 20;
            this.progressPanel.Text = "数据导入中......";
            this.progressPanel.Visible = false;
            // 
            // fpError
            // 
            this.fpError.Controls.Add(this.flyoutPanelControl2);
            this.fpError.Location = new System.Drawing.Point(193, 64);
            this.fpError.Name = "fpError";
            this.fpError.OptionsButtonPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.Utils.PeekFormButton("Button", global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16, false, true, "")});
            this.fpError.OptionsButtonPanel.ShowButtonPanel = true;
            this.fpError.OwnerControl = this.lblTip;
            this.fpError.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.fpError.Size = new System.Drawing.Size(557, 203);
            this.fpError.TabIndex = 19;
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
            // lblTip
            // 
            this.lblTip.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblTip.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Client_Common_Warning_16;
            this.lblTip.Appearance.Options.UseForeColor = true;
            this.lblTip.Appearance.Options.UseImage = true;
            this.lblTip.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblTip.Location = new System.Drawing.Point(10, 9);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(225, 20);
            this.lblTip.TabIndex = 3;
            this.lblTip.Text = "提示：数据导入前请先进行数据校验。";
            // 
            // fpDataImported
            // 
            this.fpDataImported.AccessibleDescription = "";
            this.fpDataImported.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpDataImported.Location = new System.Drawing.Point(2, 40);
            this.fpDataImported.Name = "fpDataImported";
            this.fpDataImported.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpDataImported_Sheet1});
            this.fpDataImported.Size = new System.Drawing.Size(899, 452);
            this.fpDataImported.TabIndex = 1;
            this.fpDataImported.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread_CellClick);
            this.fpDataImported.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpDataImported_MouseUp);
            // 
            // fpDataImported_Sheet1
            // 
            this.fpDataImported_Sheet1.Reset();
            this.fpDataImported_Sheet1.SheetName = "Sheet1";
            // 
            // pnlStatus
            // 
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(2, 492);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(899, 35);
            this.pnlStatus.TabIndex = 18;
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Status_Red;
            this.lblStatus.Appearance.Options.UseForeColor = true;
            this.lblStatus.Appearance.Options.UseImage = true;
            this.lblStatus.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblStatus.Location = new System.Drawing.Point(9, 3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(125, 28);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "未进行数据校验。";
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblTip);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(2, 2);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(899, 38);
            this.pnlTop.TabIndex = 0;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.RestoreDirectory = true;
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmBatchDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmClear),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmReset)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(333, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 14);
            this.label6.TabIndex = 266;
            this.label6.Text = "*";
            // 
            // lblDataSource
            // 
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Location = new System.Drawing.Point(34, 32);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(55, 14);
            this.lblDataSource.TabIndex = 265;
            this.lblDataSource.Text = "数据源：";
            // 
            // icmbDataSource
            // 
            this.icmbDataSource.Location = new System.Drawing.Point(91, 29);
            this.icmbDataSource.MenuManager = this.barManager;
            this.icmbDataSource.Name = "icmbDataSource";
            this.icmbDataSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbDataSource.Properties.SmallImages = this.icDataSource;
            this.icmbDataSource.Size = new System.Drawing.Size(236, 20);
            this.icmbDataSource.TabIndex = 0;
            this.icmbDataSource.SelectedIndexChanged += new System.EventHandler(this.icmbDataSource_SelectedIndexChanged);
            // 
            // icDataSource
            // 
            this.icDataSource.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataSource.ImageStream")));
            this.icDataSource.Images.SetKeyName(0, "Client_Common_Excel.png");
            this.icDataSource.Images.SetKeyName(1, "User_Remote.png");
            // 
            // DataImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 555);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataImportForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据导入";
            this.Load += new System.EventHandler(this.DataImportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(gcSwap)).EndInit();
            gcSwap.ResumeLayout(false);
            gcSwap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbCurrentState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icCurrentState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbImportedKeyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbImport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbAudit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beTable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDataField)).EndInit();
            this.gcDataField.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDataFieldRelation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricmbSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribtnCondition)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.fpDataImported)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpDataImported_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlStatus)).EndInit();
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbDataSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiLoad;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.XtraBars.BarButtonItem bbiCellFormat;
        private DevExpress.XtraBars.BarButtonItem bbiVerify;
        private DevExpress.XtraBars.BarButtonItem bbiShowResult;
        private DevExpress.XtraBars.BarButtonItem bbiImport;
        private DevExpress.XtraBars.BarButtonItem bbiSaveAs;
        private DevExpress.XtraBars.BarButtonItem bbiErrorData;
        private DevExpress.XtraBars.BarButtonItem bbiErrorDataImported;
        private DevExpress.XtraBars.BarButtonItem bbiCloseForm;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private FarPoint.Win.Spread.FpSpread fpDataImported;
        private FarPoint.Win.Spread.SheetView fpDataImported_Sheet1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private DevExpress.XtraEditors.GroupControl gcDataField;
        private DevExpress.XtraEditors.ButtonEdit beTable;
        private System.Windows.Forms.Label lblImportedKeyName;
        private System.Windows.Forms.Label lblAudit;
        private System.Windows.Forms.CheckBox chkAddtionalType;
        private System.Windows.Forms.Label lblAddtionalType;
        private System.Windows.Forms.Label lblImport;
        private System.Windows.Forms.CheckBox chkAllowNull;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLocalTable;
        private System.Windows.Forms.Label lblDataSourceTip;
        private System.Windows.Forms.Label lblAuditTip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblImportTip;
        private DevExpress.XtraGrid.GridControl gcDataFieldRelation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewInput;
        private DevExpress.XtraGrid.Columns.GridColumn gridColDestination;
        private DevExpress.XtraGrid.Columns.GridColumn gridColRequiredDataField;
        private DevExpress.XtraGrid.Columns.GridColumn gridColSource;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ricmbSource;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbImport;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbAudit;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbImportedKeyName;
        private System.Windows.Forms.CheckBox chkNotExistedUser;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.PanelControl pnlStatus;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.Utils.FlyoutPanel fpError;
        private DevExpress.Utils.FlyoutPanelControl flyoutPanelControl2;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblErrorTip;
        private DevExpress.XtraBars.BarButtonItem btnItmDelete;
        private DevExpress.XtraBars.BarButtonItem btnItmBatchDelete;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.XtraBars.BarButtonItem btnItmClear;
        private DevExpress.XtraBars.BarButtonItem btnItmReset;
        private System.Windows.Forms.CheckBox chkValidationRequired;
        private System.Windows.Forms.Label lblValidationRequired;
        private DevExpress.XtraGrid.Columns.GridColumn gridColCondition;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ribtnCondition;
        private DevExpress.XtraBars.BarButtonItem bbiSkippedDataImported;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbCurrentState;
        private System.Windows.Forms.Label label4;
        private DevExpress.Utils.ImageCollection icCurrentState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDataSource;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbDataSource;
        private DevExpress.Utils.ImageCollection icDataSource;
    }
}