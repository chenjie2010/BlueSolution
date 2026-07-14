namespace Blue.WindowsFormsClient.MyAuditingModule
{
    partial class PersonalAuditingControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonalAuditingControl));
            this.imglstTreeview = new System.Windows.Forms.ImageList(this.components);
            this.icDataWarehouse = new DevExpress.Utils.ImageCollection(this.components);
            this.icButtons = new DevExpress.Utils.ImageCollection(this.components);
            this.pnlControls = new DevExpress.XtraEditors.PanelControl();
            this.userListControl = new Blue.WindowsFormsClient.Common.UserListControl();
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).BeginInit();
            this.SuspendLayout();
            // 
            // imglstTreeview
            // 
            this.imglstTreeview.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstTreeview.ImageStream")));
            this.imglstTreeview.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstTreeview.Images.SetKeyName(0, "Common_Nodes_Up.png");
            this.imglstTreeview.Images.SetKeyName(1, "Common_Nodes_Down.png");
            this.imglstTreeview.Images.SetKeyName(2, "Common_Nodes_Selected.png");
            // 
            // icDataWarehouse
            // 
            this.icDataWarehouse.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataWarehouse.ImageStream")));
            this.icDataWarehouse.Images.SetKeyName(0, "Common_Number_First.png");
            this.icDataWarehouse.Images.SetKeyName(1, "Common_Number_Second.png");
            this.icDataWarehouse.Images.SetKeyName(2, "Common_Number_Third.png");
            this.icDataWarehouse.Images.SetKeyName(3, "Common_Number_Fourth.png");
            this.icDataWarehouse.Images.SetKeyName(4, "Common_Number_Fifth.png");
            // 
            // icButtons
            // 
            this.icButtons.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icButtons.ImageStream")));
            this.icButtons.Images.SetKeyName(0, "Auditing_PersonInfo_Auditing.png");
            this.icButtons.Images.SetKeyName(1, "Auditing_PersonInfo_Auditing_Cancelled.png");
            this.icButtons.Images.SetKeyName(2, "Client_Common_Current_State.png");
            // 
            // pnlControls
            // 
            this.pnlControls.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControls.Location = new System.Drawing.Point(0, 237);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1239, 346);
            this.pnlControls.TabIndex = 13;
            // 
            // userListControl
            // 
            this.userListControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.userListControl.IsPhotoShowed = false;
            this.userListControl.IsShowCheckBox = true;
            this.userListControl.Location = new System.Drawing.Point(0, 0);
            this.userListControl.Name = "userListControl";
            this.userListControl.Size = new System.Drawing.Size(1239, 237);
            this.userListControl.TabIndex = 12;
            // 
            // PersonalAuditingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.userListControl);
            this.Name = "PersonalAuditingControl";
            this.Size = new System.Drawing.Size(1239, 583);
            this.Load += new System.EventHandler(this.PersonalAuditingControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icDataWarehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Blue.WindowsFormsClient.Common.UserListControl userListControl;
        private DevExpress.Utils.ImageCollection icDataWarehouse;
        private System.Windows.Forms.ImageList imglstTreeview;
        private DevExpress.Utils.ImageCollection icButtons;
        private DevExpress.XtraEditors.PanelControl pnlControls;
    }
}
