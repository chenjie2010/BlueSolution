namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    partial class WorkflowEdgeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkflowEdgeForm));
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.icmbNodeRelationship = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icNodeRelationship = new DevExpress.Utils.ImageCollection(this.components);
            this.lblNodeRelationShip = new DevExpress.XtraEditors.LabelControl();
            this.lblNode = new DevExpress.XtraEditors.LabelControl();
            this.lblParentNode = new DevExpress.XtraEditors.LabelControl();
            this.beNode = new DevExpress.XtraEditors.ButtonEdit();
            this.beParentNode = new DevExpress.XtraEditors.ButtonEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbNodeRelationship.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icNodeRelationship)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beNode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beParentNode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.icmbNodeRelationship);
            this.pnlBottom.Controls.Add(this.lblNodeRelationShip);
            this.pnlBottom.Controls.Add(this.lblNode);
            this.pnlBottom.Controls.Add(this.lblParentNode);
            this.pnlBottom.Controls.Add(this.beNode);
            this.pnlBottom.Controls.Add(this.beParentNode);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(0, 0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(407, 114);
            this.pnlBottom.TabIndex = 0;
            // 
            // icmbNodeRelationship
            // 
            this.icmbNodeRelationship.Location = new System.Drawing.Point(79, 15);
            this.icmbNodeRelationship.Name = "icmbNodeRelationship";
            this.icmbNodeRelationship.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbNodeRelationship.Properties.SmallImages = this.icNodeRelationship;
            this.icmbNodeRelationship.Size = new System.Drawing.Size(316, 20);
            this.icmbNodeRelationship.TabIndex = 76;
            this.icmbNodeRelationship.SelectedIndexChanged += new System.EventHandler(this.icmbNodeRelationship_SelectedIndexChanged);
            // 
            // icNodeRelationship
            // 
            this.icNodeRelationship.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icNodeRelationship.ImageStream")));
            this.icNodeRelationship.Images.SetKeyName(0, "Node_Relationship_OneToOne.png");
            this.icNodeRelationship.Images.SetKeyName(1, "Node_Relationship_OneToMany.png");
            this.icNodeRelationship.Images.SetKeyName(2, "Node_Relationship_ManyToOne.png");
            // 
            // lblNodeRelationShip
            // 
            this.lblNodeRelationShip.Location = new System.Drawing.Point(14, 17);
            this.lblNodeRelationShip.Name = "lblNodeRelationShip";
            this.lblNodeRelationShip.Size = new System.Drawing.Size(60, 14);
            this.lblNodeRelationShip.TabIndex = 77;
            this.lblNodeRelationShip.Text = "流程类型：";
            // 
            // lblNode
            // 
            this.lblNode.Location = new System.Drawing.Point(25, 84);
            this.lblNode.Name = "lblNode";
            this.lblNode.Size = new System.Drawing.Size(48, 14);
            this.lblNode.TabIndex = 2;
            this.lblNode.Text = "子节点：";
            // 
            // lblParentNode
            // 
            this.lblParentNode.Location = new System.Drawing.Point(25, 51);
            this.lblParentNode.Name = "lblParentNode";
            this.lblParentNode.Size = new System.Drawing.Size(48, 14);
            this.lblParentNode.TabIndex = 0;
            this.lblParentNode.Text = "父节点：";
            // 
            // beNode
            // 
            this.beNode.Location = new System.Drawing.Point(79, 81);
            this.beNode.Name = "beNode";
            this.beNode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beNode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.beNode.Size = new System.Drawing.Size(316, 20);
            this.beNode.TabIndex = 3;
            this.beNode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beNode_ButtonClick);
            // 
            // beParentNode
            // 
            this.beParentNode.Location = new System.Drawing.Point(79, 48);
            this.beParentNode.Name = "beParentNode";
            this.beParentNode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beParentNode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.beParentNode.Size = new System.Drawing.Size(316, 20);
            this.beParentNode.TabIndex = 1;
            this.beParentNode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.beParentNode_ButtonClick);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnConfirm);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 114);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(407, 38);
            this.panelControl2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Cancel_16;
            this.btnCancel.Location = new System.Drawing.Point(207, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.btnConfirm.Location = new System.Drawing.Point(125, 7);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "确认(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // WorkflowEdgeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 152);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "WorkflowEdgeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工作流程节点关系";
            this.Load += new System.EventHandler(this.WorkflowEdgeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbNodeRelationship.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icNodeRelationship)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beNode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beParentNode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.ButtonEdit beParentNode;
        private DevExpress.XtraEditors.ButtonEdit beNode;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.LabelControl lblNode;
        private DevExpress.XtraEditors.LabelControl lblParentNode;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbNodeRelationship;
        private DevExpress.XtraEditors.LabelControl lblNodeRelationShip;
        private DevExpress.Utils.ImageCollection icNodeRelationship;
    }
}