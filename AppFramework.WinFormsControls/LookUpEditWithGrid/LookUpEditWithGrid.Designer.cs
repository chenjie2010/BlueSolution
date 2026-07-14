namespace AppFramework.WinFormsControls
{
    partial class LookUpEditWithGrid
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
            this.popupContainerEdit = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl = new DevExpress.XtraEditors.PopupContainerControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rcmbDataFieldMode = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.hlnkClear = new DevExpress.XtraEditors.HyperlinkLabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl)).BeginInit();
            this.popupContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // popupContainerEdit
            // 
            this.popupContainerEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.popupContainerEdit.Location = new System.Drawing.Point(0, 0);
            this.popupContainerEdit.Name = "popupContainerEdit";
            this.popupContainerEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popupContainerEdit.Properties.PopupControl = this.popupContainerControl;
            this.popupContainerEdit.Properties.PopupSizeable = false;
            this.popupContainerEdit.Size = new System.Drawing.Size(504, 20);
            this.popupContainerEdit.TabIndex = 0;
            this.popupContainerEdit.Popup += new System.EventHandler(this.popupContainerEdit_Popup);
            this.popupContainerEdit.BeforePopup += new System.EventHandler(this.popupContainerEdit_BeforePopup);
            this.popupContainerEdit.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.popupContainerEdit_Closed);
            this.popupContainerEdit.EditValueChanged += new System.EventHandler(this.popupContainerEdit_EditValueChanged);
            this.popupContainerEdit.Leave += new System.EventHandler(this.popupContainerEdit_Leave);
            // 
            // popupContainerControl
            // 
            this.popupContainerControl.Controls.Add(this.gridControl);
            this.popupContainerControl.Controls.Add(this.panelControl1);
            this.popupContainerControl.Location = new System.Drawing.Point(2, 20);
            this.popupContainerControl.Name = "popupContainerControl";
            this.popupContainerControl.Size = new System.Drawing.Size(503, 298);
            this.popupContainerControl.TabIndex = 1;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbDataFieldMode});
            this.gridControl.Size = new System.Drawing.Size(503, 273);
            this.gridControl.TabIndex = 1;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Appearance.FocusedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridView.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gridView.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridView.OptionsBehavior.AutoPopulateColumns = false;
            this.gridView.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gridView.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gridView.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridView.OptionsBehavior.ReadOnly = true;
            this.gridView.OptionsCustomization.AllowColumnMoving = false;
            this.gridView.OptionsCustomization.AllowFilter = false;
            this.gridView.OptionsCustomization.AllowSort = false;
            this.gridView.OptionsFind.AllowFindPanel = false;
            this.gridView.OptionsFind.FindNullPrompt = "";
            this.gridView.OptionsFind.ShowClearButton = false;
            this.gridView.OptionsFind.ShowCloseButton = false;
            this.gridView.OptionsMenu.EnableColumnMenu = false;
            this.gridView.OptionsMenu.EnableFooterMenu = false;
            this.gridView.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridView.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gridView.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridView.OptionsMenu.ShowSplitItem = false;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView_RowClick);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.gridView_CustomColumnSort);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // rcmbDataFieldMode
            // 
            this.rcmbDataFieldMode.AutoHeight = false;
            this.rcmbDataFieldMode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbDataFieldMode.Name = "rcmbDataFieldMode";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.hlnkClear);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 273);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(503, 25);
            this.panelControl1.TabIndex = 2;
            // 
            // hlnkClear
            // 
            this.hlnkClear.Appearance.Image = global::AppFramework.WinFormsControls.Properties.Resources.Button_Remove_Small;
            this.hlnkClear.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkClear.Appearance.ImageIndex = 4;
            this.hlnkClear.Appearance.Options.UseImage = true;
            this.hlnkClear.Appearance.Options.UseImageAlign = true;
            this.hlnkClear.Appearance.Options.UseImageIndex = true;
            this.hlnkClear.Appearance.Options.UseImageList = true;
            this.hlnkClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkClear.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkClear.Location = new System.Drawing.Point(5, 3);
            this.hlnkClear.Name = "hlnkClear";
            this.hlnkClear.Size = new System.Drawing.Size(69, 20);
            this.hlnkClear.TabIndex = 12;
            this.hlnkClear.Text = "清除数据";
            this.hlnkClear.Click += new System.EventHandler(this.hlnkClear_Click);
            // 
            // LookUpEditWithGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerEdit);
            this.Controls.Add(this.popupContainerControl);
            this.Name = "LookUpEditWithGrid";
            this.Size = new System.Drawing.Size(504, 20);
            this.Load += new System.EventHandler(this.LookUpEditWithGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl)).EndInit();
            this.popupContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbDataFieldMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbDataFieldMode;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkClear;
    }
}
