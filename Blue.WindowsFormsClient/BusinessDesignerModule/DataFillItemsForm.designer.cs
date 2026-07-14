namespace Blue.WindowsFormsClient
{
    partial class DataFillItemsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataFillItemsForm));
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.icTableRelation = new DevExpress.Utils.ImageCollection(this.components);
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.dataFillDropdownList = new Blue.WindowsFormsClient.DataFillDropdownList();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icTableRelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.Controls.Add(this.dataFillDropdownList);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.Name = "gcMain";
            this.gcMain.Padding = new System.Windows.Forms.Padding(2);
            this.gcMain.Size = new System.Drawing.Size(375, 47);
            this.gcMain.TabIndex = 0;
            this.gcMain.Text = "选择项";
            // 
            // icTableRelation
            // 
            this.icTableRelation.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTableRelation.ImageStream")));
            this.icTableRelation.Images.SetKeyName(0, "Common_Table_Link_InnerJoin.png");
            this.icTableRelation.Images.SetKeyName(1, "Common_Table_Link_LeftOuterJoin.png");
            this.icTableRelation.Images.SetKeyName(2, "Common_Table_Link_RightOuterJoin.png");
            this.icTableRelation.Images.SetKeyName(3, "Common_Table_Link_FullOuterJoin.png");
            this.icTableRelation.Images.SetKeyName(4, "Common_Table_Link_InnerJoin_1.png");
            this.icTableRelation.Images.SetKeyName(5, "Common_Table_Link_LeftOuterJoin_1.png");
            this.icTableRelation.Images.SetKeyName(6, "Common_Table_Link_RightOuterJoin_1.png");
            this.icTableRelation.Images.SetKeyName(7, "Common_Table_Link_FullOuterJoin_1.png");
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 47);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(375, 39);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(202, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(105, 8);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "确定(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dataFillDropdownList
            // 
            this.dataFillDropdownList.CustomDataContract = null;
            this.dataFillDropdownList.CustomGroupContract = null;
            this.dataFillDropdownList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataFillDropdownList.Location = new System.Drawing.Point(4, 23);
            this.dataFillDropdownList.Name = "dataFillDropdownList";
            this.dataFillDropdownList.OnlySelectedLeaf = true;
            this.dataFillDropdownList.Size = new System.Drawing.Size(367, 20);
            this.dataFillDropdownList.TabIndex = 0;
            // 
            // DataFillItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 86);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataFillItemsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据填报选择";
            this.Load += new System.EventHandler(this.DataTableSelectedItemsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.icTableRelation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.Utils.ImageCollection icTableRelation;
        private DataFillDropdownList dataFillDropdownList;
    }
}