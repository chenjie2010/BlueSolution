namespace Blue.WindowsFormsClient.MyAuditingModule
{
    partial class GroupAdutingControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupAdutingControl));
            this.pnlAll = new DevExpress.XtraEditors.PanelControl();
            this.icAuditedStatus = new DevExpress.Utils.ImageCollection(this.components);
            this.groupCondition = new Blue.WindowsFormsClient.MyAuditingModule.GroupConditionControl();
            this.pnlControls = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAll)).BeginInit();
            this.pnlAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icAuditedStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlAll
            // 
            this.pnlAll.Controls.Add(this.pnlControls);
            this.pnlAll.Controls.Add(this.groupCondition);
            this.pnlAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAll.Location = new System.Drawing.Point(0, 0);
            this.pnlAll.Name = "pnlAll";
            this.pnlAll.Size = new System.Drawing.Size(1183, 560);
            this.pnlAll.TabIndex = 1;
            // 
            // icAuditedStatus
            // 
            this.icAuditedStatus.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icAuditedStatus.ImageStream")));
            this.icAuditedStatus.Images.SetKeyName(0, "Client_AuditedStatus_UnAudited.png");
            this.icAuditedStatus.Images.SetKeyName(1, "Client_AuditedStatus_Auditing.png");
            this.icAuditedStatus.Images.SetKeyName(2, "Client_AuditedStatus_Audited.png");
            this.icAuditedStatus.Images.SetKeyName(3, "Client_AuditedStatus_None.png");
            // 
            // groupCondition
            // 
            this.groupCondition.CustomDepartmentContract = null;
            this.groupCondition.CustomGroupContract = null;
            this.groupCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupCondition.Location = new System.Drawing.Point(2, 2);
            this.groupCondition.Name = "groupCondition";
            this.groupCondition.Size = new System.Drawing.Size(1179, 50);
            this.groupCondition.TabIndex = 0;
            this.groupCondition.UserTypeContract = null;
            // 
            // pnlControls
            // 
            this.pnlControls.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControls.Location = new System.Drawing.Point(2, 52);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1179, 506);
            this.pnlControls.TabIndex = 1;
            // 
            // GroupAdutingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlAll);
            this.Name = "GroupAdutingControl";
            this.Size = new System.Drawing.Size(1183, 560);
            this.Load += new System.EventHandler(this.GroupAdutingControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlAll)).EndInit();
            this.pnlAll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.icAuditedStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlAll;
        private DevExpress.Utils.ImageCollection icAuditedStatus;
        private GroupConditionControl groupCondition;
        private DevExpress.XtraEditors.PanelControl pnlControls;
    }
}
