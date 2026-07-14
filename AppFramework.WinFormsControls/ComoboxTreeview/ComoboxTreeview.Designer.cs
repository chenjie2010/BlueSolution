namespace AppFramework.WinFormsControls
{
    partial class ComoboxTreeview
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComoboxTreeview));
            this.popupContainerEdit = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.scCondition = new DevExpress.XtraEditors.SearchControl();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.sbtnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl)).BeginInit();
            this.popupContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // popupContainerEdit
            // 
            this.popupContainerEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.popupContainerEdit.Location = new System.Drawing.Point(0, 0);
            this.popupContainerEdit.Name = "popupContainerEdit";
            this.popupContainerEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)});
            this.popupContainerEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.popupContainerEdit.Properties.PopupControl = this.popupContainerControl;
            this.popupContainerEdit.Properties.PopupSizeable = false;
            this.popupContainerEdit.Properties.ShowPopupCloseButton = false;
            this.popupContainerEdit.Properties.BeforePopup += new System.EventHandler(this.popupContainerEdit_BeforePopup);
            this.popupContainerEdit.Size = new System.Drawing.Size(160, 20);
            this.popupContainerEdit.TabIndex = 0;
            this.popupContainerEdit.Popup += new System.EventHandler(this.popupContainerEdit_Popup);
            this.popupContainerEdit.BeforePopup += new System.EventHandler(this.popupContainerEdit_BeforePopup);
            this.popupContainerEdit.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.popupContainerEdit_Closed);
            this.popupContainerEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.popupContainerEdit_ButtonClick);
            this.popupContainerEdit.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.popupContainerEdit_ButtonPressed);
            this.popupContainerEdit.Leave += new System.EventHandler(this.popupContainerEdit_Leave);
            // 
            // popupContainerControl
            // 
            this.popupContainerControl.Controls.Add(this.treeView);
            this.popupContainerControl.Controls.Add(this.scCondition);
            this.popupContainerControl.Controls.Add(this.panelControl);
            this.popupContainerControl.Location = new System.Drawing.Point(2, 20);
            this.popupContainerControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.popupContainerControl.Name = "popupContainerControl";
            this.popupContainerControl.Size = new System.Drawing.Size(295, 201);
            this.popupContainerControl.TabIndex = 1;
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageListTree;
            this.treeView.ItemHeight = 20;
            this.treeView.Location = new System.Drawing.Point(0, 20);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 2;
            this.treeView.Size = new System.Drawing.Size(295, 156);
            this.treeView.TabIndex = 1;
            this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
            this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
            this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
            this.treeView.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.treeView_NodeMouseHover);
            this.treeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeSelect);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "Common_Nodes_Up.png");
            this.imageListTree.Images.SetKeyName(1, "Common_Nodes_Down.png");
            this.imageListTree.Images.SetKeyName(2, "Common_Nodes_Selected.png");
            // 
            // scCondition
            // 
            this.scCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.scCondition.Location = new System.Drawing.Point(0, 0);
            this.scCondition.Name = "scCondition";
            this.scCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.scCondition.Properties.NullValuePrompt = "请输入搜索条件......";
            this.scCondition.Properties.ShowDefaultButtonsMode = DevExpress.XtraEditors.Repository.ShowDefaultButtonsMode.AutoShowClear;
            this.scCondition.Size = new System.Drawing.Size(295, 20);
            this.scCondition.TabIndex = 3;
            this.scCondition.Visible = false;
            this.scCondition.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.scCondition_ButtonClick);
            this.scCondition.EditValueChanged += new System.EventHandler(this.scCondition_EditValueChanged);
            this.scCondition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scCondition_KeyPress);
            // 
            // panelControl
            // 
            this.panelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl.Controls.Add(this.sbtnRemove);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl.Location = new System.Drawing.Point(0, 176);
            this.panelControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(295, 25);
            this.panelControl.TabIndex = 2;
            // 
            // sbtnRemove
            // 
            this.sbtnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtnRemove.Image = global::AppFramework.WinFormsControls.Properties.Resources.Common_Remove;
            this.sbtnRemove.Location = new System.Drawing.Point(270, 3);
            this.sbtnRemove.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnRemove.Name = "sbtnRemove";
            this.sbtnRemove.Size = new System.Drawing.Size(23, 20);
            this.sbtnRemove.TabIndex = 0;
            this.sbtnRemove.Click += new System.EventHandler(this.sbtnRemove_Click);
            // 
            // ComoboxTreeview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerControl);
            this.Controls.Add(this.popupContainerEdit);
            this.Name = "ComoboxTreeview";
            this.Size = new System.Drawing.Size(160, 21);
            this.Load += new System.EventHandler(this.DevExpreesComoboxTreeview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl)).EndInit();
            this.popupContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl;
        private System.Windows.Forms.TreeView treeView;
        internal System.Windows.Forms.ImageList imageListTree;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.SimpleButton sbtnRemove;
        private DevExpress.XtraEditors.SearchControl scCondition;
        private DevExpress.Utils.ToolTipController toolTipController;
    }
}
