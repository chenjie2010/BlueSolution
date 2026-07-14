namespace Blue.WindowsFormsClient.Common
{
    partial class DataTableControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTableControl));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkCopy = new DevExpress.XtraEditors.CheckEdit();
            this.hlnkCurrent = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.icData = new DevExpress.Utils.ImageCollection(this.components);
            this.hlnkTemplate = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkDownload = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.devExpressGrid = new AppFramework.WinFormsControls.DevExpressGrid();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCopy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkCopy);
            this.panelControl1.Controls.Add(this.hlnkCurrent);
            this.panelControl1.Controls.Add(this.hlnkTemplate);
            this.panelControl1.Controls.Add(this.hlnkDownload);
            this.panelControl1.Controls.Add(this.btnImport);
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(698, 34);
            this.panelControl1.TabIndex = 0;
            // 
            // chkCopy
            // 
            this.chkCopy.EditValue = true;
            this.chkCopy.Location = new System.Drawing.Point(322, 11);
            this.chkCopy.Name = "chkCopy";
            this.chkCopy.Properties.Caption = "自动填充";
            this.chkCopy.Size = new System.Drawing.Size(70, 19);
            this.chkCopy.TabIndex = 27;
            this.chkCopy.ToolTip = "增加时自动填充当前记录";
            // 
            // hlnkCurrent
            // 
            this.hlnkCurrent.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkCurrent.Appearance.ImageIndex = 5;
            this.hlnkCurrent.Appearance.ImageList = this.icData;
            this.hlnkCurrent.Appearance.Options.UseImageAlign = true;
            this.hlnkCurrent.Appearance.Options.UseImageIndex = true;
            this.hlnkCurrent.Appearance.Options.UseImageList = true;
            this.hlnkCurrent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkCurrent.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkCurrent.Location = new System.Drawing.Point(219, 10);
            this.hlnkCurrent.Name = "hlnkCurrent";
            this.hlnkCurrent.Size = new System.Drawing.Size(98, 20);
            this.hlnkCurrent.TabIndex = 26;
            this.hlnkCurrent.Text = "设置当前/既往";
            this.hlnkCurrent.Click += new System.EventHandler(this.hlnkCurrent_Click);
            // 
            // icData
            // 
            this.icData.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icData.ImageStream")));
            this.icData.Images.SetKeyName(0, "Button_Add.png");
            this.icData.Images.SetKeyName(1, "Client_Common_Delete.png");
            this.icData.Images.SetKeyName(2, "Client_Common_Table.png");
            this.icData.Images.SetKeyName(3, "Client_Common_Download.png");
            this.icData.Images.SetKeyName(4, "Client_Common_Excel.png");
            this.icData.Images.SetKeyName(5, "Client_Common_Current_State.png");
            // 
            // hlnkTemplate
            // 
            this.hlnkTemplate.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkTemplate.Appearance.ImageIndex = 4;
            this.hlnkTemplate.Appearance.ImageList = this.icData;
            this.hlnkTemplate.Appearance.Options.UseImageAlign = true;
            this.hlnkTemplate.Appearance.Options.UseImageIndex = true;
            this.hlnkTemplate.Appearance.Options.UseImageList = true;
            this.hlnkTemplate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkTemplate.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkTemplate.Location = new System.Drawing.Point(395, 10);
            this.hlnkTemplate.Name = "hlnkTemplate";
            this.hlnkTemplate.Size = new System.Drawing.Size(69, 20);
            this.hlnkTemplate.TabIndex = 25;
            this.hlnkTemplate.Text = "下载模板";
            this.hlnkTemplate.HyperlinkClick += new DevExpress.Utils.HyperlinkClickEventHandler(this.hlnkTemplate_HyperlinkClick);
            // 
            // hlnkDownload
            // 
            this.hlnkDownload.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkDownload.Appearance.ImageIndex = 3;
            this.hlnkDownload.Appearance.ImageList = this.icData;
            this.hlnkDownload.Appearance.Options.UseImageAlign = true;
            this.hlnkDownload.Appearance.Options.UseImageIndex = true;
            this.hlnkDownload.Appearance.Options.UseImageList = true;
            this.hlnkDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkDownload.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkDownload.Location = new System.Drawing.Point(468, 10);
            this.hlnkDownload.Name = "hlnkDownload";
            this.hlnkDownload.Size = new System.Drawing.Size(69, 20);
            this.hlnkDownload.TabIndex = 24;
            this.hlnkDownload.Text = "导出数据";
            this.hlnkDownload.HyperlinkClick += new DevExpress.Utils.HyperlinkClickEventHandler(this.hlnkDownload_HyperlinkClick);
            // 
            // btnImport
            // 
            this.btnImport.ImageIndex = 2;
            this.btnImport.ImageList = this.icData;
            this.btnImport.Location = new System.Drawing.Point(148, 8);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(66, 20);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "导入(&I)";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ImageIndex = 1;
            this.btnDelete.ImageList = this.icData;
            this.btnDelete.Location = new System.Drawing.Point(77, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(66, 20);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.ImageList = this.icData;
            this.btnAdd.Location = new System.Drawing.Point(6, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(66, 20);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "增加(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // devExpressGrid
            // 
            this.devExpressGrid.CheckboxColumnCaption = null;
            this.devExpressGrid.ColumnAutoWidth = false;
            this.devExpressGrid.ColumnHeaderTexts = new string[0];
            this.devExpressGrid.DataKeyNames = new string[0];
            this.devExpressGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devExpressGrid.ExportedExcel = false;
            this.devExpressGrid.FootText = null;
            this.devExpressGrid.ImportedExcel = false;
            this.devExpressGrid.IsMainTable = false;
            this.devExpressGrid.IsShowCheckBox = true;
            this.devExpressGrid.Location = new System.Drawing.Point(0, 34);
            this.devExpressGrid.Name = "devExpressGrid";
            this.devExpressGrid.PageSize = 50;
            this.devExpressGrid.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devExpressGrid.Size = new System.Drawing.Size(698, 268);
            this.devExpressGrid.TabIndex = 1;
            this.devExpressGrid.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.devExpressGrid_OnPageIndexChanged);
            this.devExpressGrid.OnRowDoubleClick += new System.EventHandler<AppFramework.WinFormsControls.RowEvent>(this.devExpressGrid_OnRowDoubleClick);
            this.devExpressGrid.OnRecordSortingChanged += new System.EventHandler<AppFramework.WinFormsControls.ExtendedItemClickEventArgs>(this.devExpressGrid_OnRecordSortingChanged);
            this.devExpressGrid.OnAddClick += new System.EventHandler<DevExpress.XtraBars.ItemClickEventArgs>(this.devExpressGrid_OnAddClick);
            this.devExpressGrid.OnEditClick += new System.EventHandler<DevExpress.XtraBars.ItemClickEventArgs>(this.devExpressGrid_OnEditClick);
            this.devExpressGrid.OnRowEdit += new System.EventHandler<AppFramework.WinFormsControls.RowEvent>(this.devExpressGrid_OnRowEdit);
            this.devExpressGrid.OnDeleteClick += new System.EventHandler<DevExpress.XtraBars.ItemClickEventArgs>(this.devExpressGrid_OnDeleteClick);
            this.devExpressGrid.OnDataSourceChanged += new System.EventHandler<System.EventArgs>(this.devExpressGrid_OnDataSourceChanged);
            this.devExpressGrid.OnRefresh += new System.EventHandler<DevExpress.XtraBars.ItemClickEventArgs>(this.devExpressGrid_OnRefresh);
            this.devExpressGrid.RowCellStyle += new System.EventHandler<DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs>(this.devExpressGrid_RowCellStyle);
            // 
            // DataTableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.devExpressGrid);
            this.Controls.Add(this.panelControl1);
            this.Name = "DataTableControl";
            this.Size = new System.Drawing.Size(698, 302);
            this.Load += new System.EventHandler(this.DataTableControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCopy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private AppFramework.WinFormsControls.DevExpressGrid devExpressGrid;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkTemplate;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkDownload;
        private DevExpress.Utils.ImageCollection icData;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkCurrent;
        private DevExpress.XtraEditors.CheckEdit chkCopy;
    }
}
