namespace Blue.WindowsFormsClient.MyAuditingModule
{
    partial class GroupConditionControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupConditionControl));
            this.gcCondition = new DevExpress.XtraEditors.GroupControl();
            this.icAuditedStatus = new DevExpress.Utils.ImageCollection(this.components);
            this.lblAuditedStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblQueriedDepartment = new DevExpress.XtraEditors.LabelControl();
            this.lblQueriedUserType = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txtCondition = new DevExpress.XtraEditors.TextEdit();
            this.lblCondition = new DevExpress.XtraEditors.LabelControl();
            this.cmbQueriedDepartment = new Blue.WindowsFormsClient.TreeDropdownList();
            this.cmbQueriedUserType = new Blue.WindowsFormsClient.TreeDropdownList();
            this.ccmbAuditedStatus = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).BeginInit();
            this.gcCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icAuditedStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbAuditedStatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcCondition
            // 
            this.gcCondition.CaptionImage = ((System.Drawing.Image)(resources.GetObject("gcCondition.CaptionImage")));
            this.gcCondition.Controls.Add(this.ccmbAuditedStatus);
            this.gcCondition.Controls.Add(this.cmbQueriedDepartment);
            this.gcCondition.Controls.Add(this.lblAuditedStatus);
            this.gcCondition.Controls.Add(this.cmbQueriedUserType);
            this.gcCondition.Controls.Add(this.lblQueriedDepartment);
            this.gcCondition.Controls.Add(this.lblQueriedUserType);
            this.gcCondition.Controls.Add(this.btnClear);
            this.gcCondition.Controls.Add(this.btnQuery);
            this.gcCondition.Controls.Add(this.txtCondition);
            this.gcCondition.Controls.Add(this.lblCondition);
            this.gcCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcCondition.Location = new System.Drawing.Point(0, 0);
            this.gcCondition.Name = "gcCondition";
            this.gcCondition.Size = new System.Drawing.Size(965, 50);
            this.gcCondition.TabIndex = 15;
            this.gcCondition.Text = "用户查询";
            // 
            // icAuditedStatus
            // 
            this.icAuditedStatus.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icAuditedStatus.ImageStream")));
            this.icAuditedStatus.Images.SetKeyName(0, "Client_AuditedStatus_UnAudited.png");
            this.icAuditedStatus.Images.SetKeyName(1, "Client_AuditedStatus_Auditing.png");
            this.icAuditedStatus.Images.SetKeyName(2, "Client_AuditedStatus_Audited.png");
            this.icAuditedStatus.Images.SetKeyName(3, "Client_AuditedStatus_None.png");
            // 
            // lblAuditedStatus
            // 
            this.lblAuditedStatus.Location = new System.Drawing.Point(680, 28);
            this.lblAuditedStatus.Name = "lblAuditedStatus";
            this.lblAuditedStatus.Size = new System.Drawing.Size(60, 14);
            this.lblAuditedStatus.TabIndex = 60;
            this.lblAuditedStatus.Text = "审核状态：";
            // 
            // lblQueriedDepartment
            // 
            this.lblQueriedDepartment.Location = new System.Drawing.Point(456, 28);
            this.lblQueriedDepartment.Name = "lblQueriedDepartment";
            this.lblQueriedDepartment.Size = new System.Drawing.Size(60, 14);
            this.lblQueriedDepartment.TabIndex = 9;
            this.lblQueriedDepartment.Text = "所属单位：";
            // 
            // lblQueriedUserType
            // 
            this.lblQueriedUserType.Location = new System.Drawing.Point(233, 28);
            this.lblQueriedUserType.Name = "lblQueriedUserType";
            this.lblQueriedUserType.Size = new System.Drawing.Size(60, 14);
            this.lblQueriedUserType.TabIndex = 7;
            this.lblQueriedUserType.Text = "用户类型：";
            // 
            // btnClear
            // 
            this.btnClear.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.btnClear.ImageIndex = 1;
            this.btnClear.Location = new System.Drawing.Point(892, 26);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 20);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "清除(&R)";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Image = global::Blue.WindowsFormsClient.Properties.Resources.Buttom_Quer_Small;
            this.btnQuery.ImageIndex = 0;
            this.btnQuery.Location = new System.Drawing.Point(818, 26);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(70, 20);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtCondition
            // 
            this.txtCondition.EditValue = "";
            this.txtCondition.Location = new System.Drawing.Point(65, 26);
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Properties.MaxLength = 32;
            this.txtCondition.Properties.NullValuePrompt = "请输入用户名、姓名或证件号码";
            this.txtCondition.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCondition.Size = new System.Drawing.Size(163, 20);
            this.txtCondition.TabIndex = 0;
            this.txtCondition.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCondition_KeyPress);
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(8, 28);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(60, 14);
            this.lblCondition.TabIndex = 0;
            this.lblCondition.Text = "查询条件：";
            // 
            // cmbQueriedDepartment
            // 
            this.cmbQueriedDepartment.Location = new System.Drawing.Point(510, 26);
            this.cmbQueriedDepartment.Name = "cmbQueriedDepartment";
            this.cmbQueriedDepartment.ShowSearch = true;
            this.cmbQueriedDepartment.Size = new System.Drawing.Size(161, 21);
            this.cmbQueriedDepartment.SkinName = "Blue";
            this.cmbQueriedDepartment.TabIndex = 2;
            this.cmbQueriedDepartment.TreeDropdownHandler = null;
            // 
            // cmbQueriedUserType
            // 
            this.cmbQueriedUserType.Location = new System.Drawing.Point(289, 26);
            this.cmbQueriedUserType.Name = "cmbQueriedUserType";
            this.cmbQueriedUserType.OnlySelectedLeaf = true;
            this.cmbQueriedUserType.Size = new System.Drawing.Size(161, 21);
            this.cmbQueriedUserType.SkinName = "Blue";
            this.cmbQueriedUserType.TabIndex = 1;
            this.cmbQueriedUserType.TreeDropdownHandler = null;
            // 
            // ccmbAuditedStatus
            // 
            this.ccmbAuditedStatus.EditValue = "";
            this.ccmbAuditedStatus.Location = new System.Drawing.Point(736, 26);
            this.ccmbAuditedStatus.Name = "ccmbAuditedStatus";
            this.ccmbAuditedStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbAuditedStatus.Properties.PopupSizeable = false;
            this.ccmbAuditedStatus.Properties.SelectAllItemVisible = false;
            this.ccmbAuditedStatus.Size = new System.Drawing.Size(76, 20);
            this.ccmbAuditedStatus.TabIndex = 3;
            // 
            // GroupConditionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcCondition);
            this.Name = "GroupConditionControl";
            this.Size = new System.Drawing.Size(965, 50);
            this.Load += new System.EventHandler(this.GroupConditionControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcCondition)).EndInit();
            this.gcCondition.ResumeLayout(false);
            this.gcCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icAuditedStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbAuditedStatus.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl gcCondition;
        private TreeDropdownList cmbQueriedDepartment;
        private TreeDropdownList cmbQueriedUserType;
        private DevExpress.XtraEditors.LabelControl lblQueriedDepartment;
        private DevExpress.XtraEditors.LabelControl lblQueriedUserType;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.TextEdit txtCondition;
        private DevExpress.XtraEditors.LabelControl lblCondition;
        private DevExpress.XtraEditors.LabelControl lblAuditedStatus;
        private DevExpress.Utils.ImageCollection icAuditedStatus;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbAuditedStatus;
    }
}
