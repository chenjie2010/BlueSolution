namespace AppFramework.WinFormsControls
{
    partial class SearchLookUpEditWithGrid
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
            this.pnlPages = new DevExpress.XtraEditors.PanelControl();
            this.dataNavigator = new DevExpress.XtraEditors.DataNavigator();
            this.pnlSearch = new DevExpress.XtraEditors.PanelControl();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl)).BeginInit();
            this.popupContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPages)).BeginInit();
            this.pnlPages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSearch)).BeginInit();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
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
            this.popupContainerEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.popupContainerEdit.Size = new System.Drawing.Size(400, 20);
            this.popupContainerEdit.TabIndex = 1;
            this.popupContainerEdit.Popup += new System.EventHandler(this.popupContainerEdit_Popup);
            this.popupContainerEdit.BeforePopup += new System.EventHandler(this.popupContainerEdit_BeforePopup);
            this.popupContainerEdit.EditValueChanged += new System.EventHandler(this.popupContainerEdit_EditValueChanged);
            // 
            // popupContainerControl
            // 
            this.popupContainerControl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.popupContainerControl.Controls.Add(this.gridControl);
            this.popupContainerControl.Controls.Add(this.pnlPages);
            this.popupContainerControl.Controls.Add(this.pnlSearch);
            this.popupContainerControl.Location = new System.Drawing.Point(0, 21);
            this.popupContainerControl.Name = "popupContainerControl";
            this.popupContainerControl.Size = new System.Drawing.Size(400, 250);
            this.popupContainerControl.TabIndex = 2;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 24);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(400, 194);
            this.gridControl.TabIndex = 2;
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
            this.gridView.IndicatorWidth = 25;
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
            this.gridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView_CustomDrawRowIndicator);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // pnlPages
            // 
            this.pnlPages.Controls.Add(this.dataNavigator);
            this.pnlPages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPages.Location = new System.Drawing.Point(0, 218);
            this.pnlPages.Name = "pnlPages";
            this.pnlPages.Size = new System.Drawing.Size(400, 32);
            this.pnlPages.TabIndex = 5;
            // 
            // dataNavigator
            // 
            this.dataNavigator.Buttons.Append.Visible = false;
            this.dataNavigator.Buttons.CancelEdit.Visible = false;
            this.dataNavigator.Buttons.EndEdit.Visible = false;
            this.dataNavigator.Buttons.Next.Visible = false;
            this.dataNavigator.Buttons.Prev.Visible = false;
            this.dataNavigator.Buttons.Remove.Visible = false;
            this.dataNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataNavigator.Location = new System.Drawing.Point(2, 2);
            this.dataNavigator.Name = "dataNavigator";
            this.dataNavigator.Size = new System.Drawing.Size(396, 28);
            this.dataNavigator.TabIndex = 13;
            this.dataNavigator.Text = "翻页";
            this.dataNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.End;
            this.dataNavigator.TextStringFormat = "总记录 0 条，共 0 页。第 0 页，当前页记录 0 条。";
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.searchControl);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(400, 24);
            this.pnlSearch.TabIndex = 4;
            // 
            // searchControl
            // 
            this.searchControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchControl.Location = new System.Drawing.Point(2, 2);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.NullValuePrompt = "请输入查询条件";
            this.searchControl.Properties.ShowDefaultButtonsMode = DevExpress.XtraEditors.Repository.ShowDefaultButtonsMode.Always;
            this.searchControl.Size = new System.Drawing.Size(396, 20);
            this.searchControl.TabIndex = 0;
            this.searchControl.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.searchControl_ButtonClick);
            this.searchControl.EditValueChanged += new System.EventHandler(this.searchControl_EditValueChanged);
            // 
            // SearchLookUpEditWithGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerEdit);
            this.Controls.Add(this.popupContainerControl);
            this.Name = "SearchLookUpEditWithGrid";
            this.Size = new System.Drawing.Size(400, 20);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl)).EndInit();
            this.popupContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPages)).EndInit();
            this.pnlPages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlSearch)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit;
        private DevExpress.XtraEditors.PanelControl pnlSearch;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraEditors.PanelControl pnlPages;
        private DevExpress.XtraEditors.DataNavigator dataNavigator;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl;
    }
}
