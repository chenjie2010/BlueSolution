namespace Blue.WindowsFormsClient.MyQueryModule
{
    partial class QueryItemControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryItemControl));
            this.pnlStaticDataField = new DevExpress.XtraEditors.PanelControl();
            this.beDataFieldSorting = new DevExpress.XtraEditors.ButtonEdit();
            this.hlnkMaxmize = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.lblStaticDataField = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.hlnkClean = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.lblDataFieldSorting = new DevExpress.XtraEditors.LabelControl();
            this.ccmbStaticDataField = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gcResult = new DevExpress.XtraEditors.GroupControl();
            this.devExpressGrid = new AppFramework.WinFormsControls.DevExpressGrid();
            this.gcCondition = new DevExpress.XtraEditors.GroupControl();
            this.hlnkBack = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.icCurrentState = new DevExpress.Utils.ImageCollection(this.components);
            this.icAuditedStatus = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlStaticDataField)).BeginInit();
            this.pnlStaticDataField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beDataFieldSorting.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbStaticDataField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcResult)).BeginInit();
            this.gcResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).BeginInit();
            this.gcCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icCurrentState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAuditedStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlStaticDataField
            // 
            this.pnlStaticDataField.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlStaticDataField.Controls.Add(this.beDataFieldSorting);
            this.pnlStaticDataField.Controls.Add(this.hlnkMaxmize);
            this.pnlStaticDataField.Controls.Add(this.lblStaticDataField);
            this.pnlStaticDataField.Controls.Add(this.btnQuery);
            this.pnlStaticDataField.Controls.Add(this.hlnkClean);
            this.pnlStaticDataField.Controls.Add(this.lblDataFieldSorting);
            this.pnlStaticDataField.Controls.Add(this.ccmbStaticDataField);
            this.pnlStaticDataField.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStaticDataField.Location = new System.Drawing.Point(2, 68);
            this.pnlStaticDataField.Name = "pnlStaticDataField";
            this.pnlStaticDataField.Size = new System.Drawing.Size(880, 30);
            this.pnlStaticDataField.TabIndex = 35;
            // 
            // beDataFieldSorting
            // 
            this.beDataFieldSorting.Location = new System.Drawing.Point(91, 5);
            this.beDataFieldSorting.Name = "beDataFieldSorting";
            this.beDataFieldSorting.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beDataFieldSorting.Size = new System.Drawing.Size(180, 20);
            this.beDataFieldSorting.TabIndex = 99;
            this.beDataFieldSorting.Click += new System.EventHandler(this.beDataFieldSorting_Click);
            // 
            // hlnkMaxmize
            // 
            this.hlnkMaxmize.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Maxmize;
            this.hlnkMaxmize.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkMaxmize.Appearance.ImageIndex = 1;
            this.hlnkMaxmize.Appearance.Options.UseImage = true;
            this.hlnkMaxmize.Appearance.Options.UseImageAlign = true;
            this.hlnkMaxmize.Appearance.Options.UseImageIndex = true;
            this.hlnkMaxmize.Appearance.Options.UseImageList = true;
            this.hlnkMaxmize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkMaxmize.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkMaxmize.Location = new System.Drawing.Point(713, 5);
            this.hlnkMaxmize.Name = "hlnkMaxmize";
            this.hlnkMaxmize.Size = new System.Drawing.Size(69, 20);
            this.hlnkMaxmize.TabIndex = 98;
            this.hlnkMaxmize.Text = "最大化...";
            this.hlnkMaxmize.Click += new System.EventHandler(this.hlnkMaxmize_Click);
            // 
            // lblStaticDataField
            // 
            this.lblStaticDataField.Location = new System.Drawing.Point(288, 6);
            this.lblStaticDataField.Name = "lblStaticDataField";
            this.lblStaticDataField.Size = new System.Drawing.Size(60, 14);
            this.lblStaticDataField.TabIndex = 97;
            this.lblStaticDataField.Text = "统计字段：";
            // 
            // btnQuery
            // 
            this.btnQuery.Image = global::Blue.WindowsFormsClient.Properties.Resources.Buttom_Quer_Small;
            this.btnQuery.ImageIndex = 0;
            this.btnQuery.Location = new System.Drawing.Point(589, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(67, 22);
            this.btnQuery.TabIndex = 95;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // hlnkClean
            // 
            this.hlnkClean.Appearance.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.hlnkClean.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkClean.Appearance.ImageIndex = 1;
            this.hlnkClean.Appearance.Options.UseImage = true;
            this.hlnkClean.Appearance.Options.UseImageAlign = true;
            this.hlnkClean.Appearance.Options.UseImageIndex = true;
            this.hlnkClean.Appearance.Options.UseImageList = true;
            this.hlnkClean.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkClean.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkClean.Location = new System.Drawing.Point(659, 5);
            this.hlnkClean.Name = "hlnkClean";
            this.hlnkClean.Size = new System.Drawing.Size(57, 20);
            this.hlnkClean.TabIndex = 94;
            this.hlnkClean.Text = "清除...";
            this.hlnkClean.Click += new System.EventHandler(this.hlnkClean_Click);
            // 
            // lblDataFieldSorting
            // 
            this.lblDataFieldSorting.Location = new System.Drawing.Point(36, 6);
            this.lblDataFieldSorting.Name = "lblDataFieldSorting";
            this.lblDataFieldSorting.Size = new System.Drawing.Size(60, 14);
            this.lblDataFieldSorting.TabIndex = 93;
            this.lblDataFieldSorting.Text = "排序字段：";
            // 
            // ccmbStaticDataField
            // 
            this.ccmbStaticDataField.EditValue = "";
            this.ccmbStaticDataField.Location = new System.Drawing.Point(347, 5);
            this.ccmbStaticDataField.Margin = new System.Windows.Forms.Padding(2);
            this.ccmbStaticDataField.Name = "ccmbStaticDataField";
            this.ccmbStaticDataField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbStaticDataField.Properties.SelectAllItemVisible = false;
            this.ccmbStaticDataField.Size = new System.Drawing.Size(236, 20);
            this.ccmbStaticDataField.TabIndex = 92;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gcResult);
            this.pnlMain.Controls.Add(this.gcCondition);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(888, 466);
            this.pnlMain.TabIndex = 37;
            // 
            // gcResult
            // 
            this.gcResult.Controls.Add(this.devExpressGrid);
            this.gcResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcResult.Location = new System.Drawing.Point(2, 102);
            this.gcResult.Name = "gcResult";
            this.gcResult.Size = new System.Drawing.Size(884, 362);
            this.gcResult.TabIndex = 37;
            this.gcResult.Text = "查询结果";
            // 
            // devExpressGrid
            // 
            this.devExpressGrid.AppearanceCellHAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.devExpressGrid.AppearanceHeaderHAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.devExpressGrid.CheckboxColumnCaption = null;
            this.devExpressGrid.ColumnAutoWidth = false;
            this.devExpressGrid.ColumnHeaderTexts = new string[0];
            this.devExpressGrid.DataKeyNames = new string[0];
            this.devExpressGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devExpressGrid.ExportedExcel = false;
            this.devExpressGrid.FootText = null;
            this.devExpressGrid.IsMainTable = false;
            this.devExpressGrid.Location = new System.Drawing.Point(2, 21);
            this.devExpressGrid.Name = "devExpressGrid";
            this.devExpressGrid.PageSize = 50;
            this.devExpressGrid.SelectionMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.devExpressGrid.Size = new System.Drawing.Size(880, 339);
            this.devExpressGrid.TabIndex = 34;
            this.devExpressGrid.OnPageIndexChanged += new System.EventHandler<AppFramework.WinFormsControls.CustomGridViewPageEventArgs>(this.devExpressGrid_OnPageIndexChanged);
            // 
            // gcCondition
            // 
            this.gcCondition.Controls.Add(this.hlnkBack);
            this.gcCondition.Controls.Add(this.pnlStaticDataField);
            this.gcCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcCondition.Location = new System.Drawing.Point(2, 2);
            this.gcCondition.Name = "gcCondition";
            this.gcCondition.Size = new System.Drawing.Size(884, 100);
            this.gcCondition.TabIndex = 36;
            this.gcCondition.Text = "查询条件";
            // 
            // hlnkBack
            // 
            this.hlnkBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlnkBack.Appearance.ImageIndex = 5;
            this.hlnkBack.Appearance.Options.UseImageIndex = true;
            this.hlnkBack.Appearance.Options.UseImageList = true;
            this.hlnkBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkBack.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkBack.Location = new System.Drawing.Point(845, 1);
            this.hlnkBack.Name = "hlnkBack";
            this.hlnkBack.Size = new System.Drawing.Size(36, 14);
            this.hlnkBack.TabIndex = 99;
            this.hlnkBack.Text = "返回...";
            this.hlnkBack.Click += new System.EventHandler(this.hlnkBack_Click);
            // 
            // icCurrentState
            // 
            this.icCurrentState.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icCurrentState.ImageStream")));
            this.icCurrentState.Images.SetKeyName(0, "Client_CurrentState_History.png");
            this.icCurrentState.Images.SetKeyName(1, "Client_CurrentState_Current.png");
            // 
            // icAuditedStatus
            // 
            this.icAuditedStatus.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icAuditedStatus.ImageStream")));
            this.icAuditedStatus.Images.SetKeyName(0, "Client_AuditedStatus_None.png");
            this.icAuditedStatus.Images.SetKeyName(1, "Client_AuditedStatus_UnAudited.png");
            this.icAuditedStatus.Images.SetKeyName(2, "Client_AuditedStatus_Auditing.png");
            this.icAuditedStatus.Images.SetKeyName(3, "Client_AuditedStatus_Audited.png");
            // 
            // QueryItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "QueryItemControl";
            this.Size = new System.Drawing.Size(888, 466);
            this.Load += new System.EventHandler(this.QueryItemControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlStaticDataField)).EndInit();
            this.pnlStaticDataField.ResumeLayout(false);
            this.pnlStaticDataField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beDataFieldSorting.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbStaticDataField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcResult)).EndInit();
            this.gcResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).EndInit();
            this.gcCondition.ResumeLayout(false);
            this.gcCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icCurrentState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icAuditedStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlStaticDataField;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkClean;
        private DevExpress.XtraEditors.LabelControl lblDataFieldSorting;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbStaticDataField;
        private AppFramework.WinFormsControls.DevExpressGrid devExpressGrid;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.GroupControl gcResult;
        private DevExpress.XtraEditors.GroupControl gcCondition;
        private DevExpress.XtraEditors.LabelControl lblStaticDataField;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkMaxmize;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkBack;
        private DevExpress.XtraEditors.ButtonEdit beDataFieldSorting;
        private DevExpress.Utils.ImageCollection icCurrentState;
        private DevExpress.Utils.ImageCollection icAuditedStatus;
    }
}
