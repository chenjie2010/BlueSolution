namespace Blue.WindowsFormsClient
{
    partial class MultiSelectedItemsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiSelectedItemsForm));
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.txtTip = new DevExpress.XtraEditors.MemoEdit();
            this.scCondition = new DevExpress.XtraEditors.SearchControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.tkeItems = new DevExpress.XtraEditors.TokenEdit();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkeItems.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.txtTip);
            this.pnlTop.Controls.Add(this.scCondition);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(401, 69);
            this.pnlTop.TabIndex = 0;
            // 
            // txtTip
            // 
            this.txtTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTip.Location = new System.Drawing.Point(2, 2);
            this.txtTip.Name = "txtTip";
            this.txtTip.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtTip.Properties.ReadOnly = true;
            this.txtTip.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTip.Size = new System.Drawing.Size(397, 45);
            this.txtTip.TabIndex = 0;
            // 
            // scCondition
            // 
            this.scCondition.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.scCondition.Location = new System.Drawing.Point(2, 47);
            this.scCondition.Name = "scCondition";
            this.scCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.scCondition.Properties.NullValuePrompt = "请输入搜索名称";
            this.scCondition.Properties.ShowDefaultButtonsMode = DevExpress.XtraEditors.Repository.ShowDefaultButtonsMode.AutoShowClear;
            this.scCondition.Size = new System.Drawing.Size(397, 20);
            this.scCondition.TabIndex = 2;
            this.scCondition.ButtonPressed += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.scCondition_ButtonPressed);
            this.scCondition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scCondition_KeyPress);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlMain.Controls.Add(this.treeView);
            this.pnlMain.Controls.Add(this.tkeItems);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 69);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(401, 306);
            this.pnlMain.TabIndex = 1;
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.CheckBoxes = true;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageListTree;
            this.treeView.ItemHeight = 20;
            this.treeView.Location = new System.Drawing.Point(0, 20);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 2;
            this.treeView.Size = new System.Drawing.Size(401, 286);
            this.treeView.TabIndex = 0;
            this.treeView.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeCheck);
            this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCheck);
            this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
            this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "Common_Nodes_Up.png");
            this.imageListTree.Images.SetKeyName(1, "Common_Nodes_Down.png");
            this.imageListTree.Images.SetKeyName(2, "Common_Nodes_Selected.png");
            // 
            // tkeItems
            // 
            this.tkeItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.tkeItems.Location = new System.Drawing.Point(0, 0);
            this.tkeItems.Name = "tkeItems";
            this.tkeItems.Properties.Separators.AddRange(new string[] {
            ","});
            this.tkeItems.Size = new System.Drawing.Size(401, 20);
            this.tkeItems.TabIndex = 3;
            this.tkeItems.EditValueChanged += new System.EventHandler(this.tkeItems_EditValueChanged);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 375);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(401, 52);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(214, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(111, 16);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确定(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // MultiSelectedItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 427);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MultiSelectedItemsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "多项选择";
            this.Load += new System.EventHandler(this.MultiSelectedItemsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tkeItems.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private System.Windows.Forms.TreeView treeView;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.MemoEdit txtTip;
        internal System.Windows.Forms.ImageList imageListTree;
        private DevExpress.XtraEditors.SearchControl scCondition;
        private DevExpress.XtraEditors.TokenEdit tkeItems;
    }
}