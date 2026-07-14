namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class SnapshotForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SnapshotForm));
            this.lbcCoverList = new DevExpress.XtraEditors.ListBoxControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.etxtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnItmConfirm = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmClose = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imglstEdit = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDataFieldTypeTip = new System.Windows.Forms.Label();
            this.dateExpireDate = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSnapshotName = new DevExpress.XtraEditors.TextEdit();
            this.lblSnapshotName = new System.Windows.Forms.Label();
            this.bar1 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.lbcCoverList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etxtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExpireDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExpireDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSnapshotName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbcCoverList
            // 
            this.lbcCoverList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbcCoverList.Location = new System.Drawing.Point(2, 22);
            this.lbcCoverList.LookAndFeel.SkinName = "Blue";
            this.lbcCoverList.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lbcCoverList.Name = "lbcCoverList";
            this.lbcCoverList.Size = new System.Drawing.Size(205, 238);
            this.lbcCoverList.TabIndex = 0;
            this.lbcCoverList.SelectedIndexChanged += new System.EventHandler(this.lbcCoverList_SelectedIndexChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lbcCoverList);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 32);
            this.groupControl1.LookAndFeel.SkinName = "Money Twins";
            this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(209, 262);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "快照列表";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.etxtNotes);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.label2);
            this.groupControl2.Controls.Add(this.lblDataFieldTypeTip);
            this.groupControl2.Controls.Add(this.dateExpireDate);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.txtSnapshotName);
            this.groupControl2.Controls.Add(this.lblSnapshotName);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(209, 32);
            this.groupControl2.LookAndFeel.SkinName = "Money Twins";
            this.groupControl2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(370, 262);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "快照详情";
            // 
            // etxtNotes
            // 
            this.etxtNotes.Location = new System.Drawing.Point(72, 96);
            this.etxtNotes.MenuManager = this.barManager;
            this.etxtNotes.Name = "etxtNotes";
            this.etxtNotes.Size = new System.Drawing.Size(278, 154);
            this.etxtNotes.TabIndex = 2;
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imglstEdit;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItmConfirm,
            this.btnItmClose,
            this.btnItmDelete});
            this.barManager.MaxItemId = 3;
            // 
            // bar2
            // 
            this.bar2.BarItemHorzIndent = 5;
            this.bar2.BarItemVertIndent = 5;
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmConfirm, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Tools";
            // 
            // btnItmConfirm
            // 
            this.btnItmConfirm.Caption = "确定(&O)";
            this.btnItmConfirm.Id = 0;
            this.btnItmConfirm.ImageIndex = 0;
            this.btnItmConfirm.Name = "btnItmConfirm";
            this.btnItmConfirm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmConfirm_ItemClick);
            // 
            // btnItmClose
            // 
            this.btnItmClose.Caption = "关闭(&C)";
            this.btnItmClose.Id = 1;
            this.btnItmClose.ImageIndex = 1;
            this.btnItmClose.Name = "btnItmClose";
            this.btnItmClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmClose_ItemClick);
            // 
            // btnItmDelete
            // 
            this.btnItmDelete.Caption = "删除(&D)";
            this.btnItmDelete.Id = 2;
            this.btnItmDelete.ImageIndex = 2;
            this.btnItmDelete.Name = "btnItmDelete";
            this.btnItmDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnItmDelete_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(579, 32);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 294);
            this.barDockControlBottom.Size = new System.Drawing.Size(579, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 32);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 262);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(579, 32);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 262);
            // 
            // imglstEdit
            // 
            this.imglstEdit.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstEdit.ImageStream")));
            this.imglstEdit.TransparentColor = System.Drawing.Color.Maroon;
            this.imglstEdit.Images.SetKeyName(0, "");
            this.imglstEdit.Images.SetKeyName(1, "");
            this.imglstEdit.Images.SetKeyName(2, "Report_Sheet_Clear.png");
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(32, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 59;
            this.label3.Text = "备注：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(354, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 14);
            this.label2.TabIndex = 58;
            this.label2.Text = "*";
            // 
            // lblDataFieldTypeTip
            // 
            this.lblDataFieldTypeTip.AutoSize = true;
            this.lblDataFieldTypeTip.ForeColor = System.Drawing.Color.Red;
            this.lblDataFieldTypeTip.Location = new System.Drawing.Point(354, 38);
            this.lblDataFieldTypeTip.Name = "lblDataFieldTypeTip";
            this.lblDataFieldTypeTip.Size = new System.Drawing.Size(14, 14);
            this.lblDataFieldTypeTip.TabIndex = 57;
            this.lblDataFieldTypeTip.Text = "*";
            // 
            // dateExpireDate
            // 
            this.dateExpireDate.EditValue = null;
            this.dateExpireDate.Location = new System.Drawing.Point(72, 64);
            this.dateExpireDate.Name = "dateExpireDate";
            this.dateExpireDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateExpireDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateExpireDate.Properties.LookAndFeel.SkinName = "Blue";
            this.dateExpireDate.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dateExpireDate.Size = new System.Drawing.Size(278, 20);
            this.dateExpireDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 55;
            this.label1.Text = "快照时间：";
            // 
            // txtSnapshotName
            // 
            this.txtSnapshotName.Location = new System.Drawing.Point(72, 34);
            this.txtSnapshotName.Name = "txtSnapshotName";
            this.txtSnapshotName.Size = new System.Drawing.Size(278, 20);
            this.txtSnapshotName.TabIndex = 0;
            // 
            // lblSnapshotName
            // 
            this.lblSnapshotName.Location = new System.Drawing.Point(6, 37);
            this.lblSnapshotName.Name = "lblSnapshotName";
            this.lblSnapshotName.Size = new System.Drawing.Size(70, 16);
            this.lblSnapshotName.TabIndex = 53;
            this.lblSnapshotName.Text = "快照名称：";
            // 
            // bar1
            // 
            this.bar1.BarItemHorzIndent = 5;
            this.bar1.BarItemVertIndent = 5;
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // SnapshotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(579, 294);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SnapshotForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "快照列表";
            this.Load += new System.EventHandler(this.SnapshotForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lbcCoverList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etxtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExpireDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateExpireDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSnapshotName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lbcCoverList;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDataFieldTypeTip;
        private DevExpress.XtraEditors.DateEdit dateExpireDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtSnapshotName;
        private System.Windows.Forms.Label lblSnapshotName;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnItmConfirm;
        private DevExpress.XtraBars.BarButtonItem btnItmClose;
        private DevExpress.XtraBars.BarButtonItem btnItmDelete;
        private System.Windows.Forms.ImageList imglstEdit;
        private DevExpress.XtraEditors.MemoEdit etxtNotes;
    }
}