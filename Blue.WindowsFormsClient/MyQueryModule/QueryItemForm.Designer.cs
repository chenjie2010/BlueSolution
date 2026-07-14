namespace Blue.WindowsFormsClient.MyQueryModule
{
    partial class QueryItemForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryItemForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnItmProperty = new DevExpress.XtraBars.BarButtonItem();
            this.blnCtnItmExport = new DevExpress.XtraBars.BarLinkContainerItem();
            this.btnItmExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmPdf = new DevExpress.XtraBars.BarButtonItem();
            this.btnItmAttachment = new DevExpress.XtraBars.BarButtonItem();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.icTools = new DevExpress.Utils.ImageCollection(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.icTools;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnItmProperty,
            this.blnCtnItmExport,
            this.barButtonItem1,
            this.btnItmExcel,
            this.btnItmPdf,
            this.btnItmAttachment});
            this.barManager.MaxItemId = 6;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnItmProperty, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blnCtnItmExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnItmProperty
            // 
            this.btnItmProperty.Caption = "属性设置(&R)";
            this.btnItmProperty.Id = 0;
            this.btnItmProperty.ImageIndex = 0;
            this.btnItmProperty.Name = "btnItmProperty";
            // 
            // blnCtnItmExport
            // 
            this.blnCtnItmExport.Caption = "导出选项(&E)";
            this.blnCtnItmExport.Id = 1;
            this.blnCtnItmExport.ImageIndex = 1;
            this.blnCtnItmExport.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmExcel),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmPdf),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnItmAttachment)});
            this.blnCtnItmExport.Name = "blnCtnItmExport";
            // 
            // btnItmExcel
            // 
            this.btnItmExcel.Caption = "导出Excel(X)";
            this.btnItmExcel.Id = 3;
            this.btnItmExcel.Name = "btnItmExcel";
            // 
            // btnItmPdf
            // 
            this.btnItmPdf.Caption = "导出PDF(P)";
            this.btnItmPdf.Id = 4;
            this.btnItmPdf.Name = "btnItmPdf";
            // 
            // btnItmAttachment
            // 
            this.btnItmAttachment.Caption = "导出附件(A)";
            this.btnItmAttachment.Id = 5;
            this.btnItmAttachment.Name = "btnItmAttachment";
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
            this.barDockControlTop.Size = new System.Drawing.Size(1065, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 727);
            this.barDockControlBottom.Size = new System.Drawing.Size(1065, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 701);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1065, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 701);
            // 
            // icTools
            // 
            this.icTools.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTools.ImageStream")));
            this.icTools.Images.SetKeyName(0, "Query_Data_Properties.png");
            this.icTools.Images.SetKeyName(1, "Query_Data_Export.png");
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 2;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // QueryItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 727);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "QueryItemForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTools)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.Utils.ImageCollection icTools;
        private DevExpress.XtraBars.BarButtonItem btnItmProperty;
        private DevExpress.XtraBars.BarLinkContainerItem blnCtnItmExport;
        private DevExpress.XtraBars.BarButtonItem btnItmExcel;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnItmPdf;
        private DevExpress.XtraBars.BarButtonItem btnItmAttachment;
    }
}