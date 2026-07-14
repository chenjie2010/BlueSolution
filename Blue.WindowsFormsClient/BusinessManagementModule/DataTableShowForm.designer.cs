namespace Blue.WindowsFormsClient
{
    partial class DataTableShowForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTableShowForm));
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.icmbTableJoin = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icTableJoin = new DevExpress.Utils.ImageCollection(this.components);
            this.icmbPrimaryDataField = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icDataFieldRelation = new DevExpress.Utils.ImageCollection(this.components);
            this.lblPrimaryDataField = new DevExpress.XtraEditors.LabelControl();
            this.lblPrimaryTable = new DevExpress.XtraEditors.LabelControl();
            this.lblTableJoin = new DevExpress.XtraEditors.LabelControl();
            this.lblTableRelation = new DevExpress.XtraEditors.LabelControl();
            this.icmbTableRelation = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.icTableRelation = new DevExpress.Utils.ImageCollection(this.components);
            this.dataTableDropdownList = new Blue.WindowsFormsClient.DataTableDropdownList();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableJoin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableJoin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbPrimaryDataField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldRelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableRelation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableRelation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.Controls.Add(this.icmbTableJoin);
            this.gcMain.Controls.Add(this.icmbPrimaryDataField);
            this.gcMain.Controls.Add(this.lblPrimaryDataField);
            this.gcMain.Controls.Add(this.lblPrimaryTable);
            this.gcMain.Controls.Add(this.lblTableJoin);
            this.gcMain.Controls.Add(this.lblTableRelation);
            this.gcMain.Controls.Add(this.icmbTableRelation);
            this.gcMain.Controls.Add(this.dataTableDropdownList);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.Name = "gcMain";
            this.gcMain.Padding = new System.Windows.Forms.Padding(2);
            this.gcMain.Size = new System.Drawing.Size(365, 165);
            this.gcMain.TabIndex = 0;
            this.gcMain.Text = "选择项";
            // 
            // icmbTableJoin
            // 
            this.icmbTableJoin.Location = new System.Drawing.Point(78, 68);
            this.icmbTableJoin.Name = "icmbTableJoin";
            this.icmbTableJoin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbTableJoin.Properties.SmallImages = this.icTableJoin;
            this.icmbTableJoin.Size = new System.Drawing.Size(273, 20);
            this.icmbTableJoin.TabIndex = 24;
            // 
            // icTableJoin
            // 
            this.icTableJoin.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTableJoin.ImageStream")));
            this.icTableJoin.Images.SetKeyName(0, "Common_Table_Link_InnerJoin.png");
            this.icTableJoin.Images.SetKeyName(1, "Common_Table_Link_LeftOuterJoin.png");
            this.icTableJoin.Images.SetKeyName(2, "Common_Table_Link_RightOuterJoin.png");
            this.icTableJoin.Images.SetKeyName(3, "Common_Table_Link_FullOuterJoin.png");
            this.icTableJoin.Images.SetKeyName(4, "Common_Table_Link_InnerJoin_1.png");
            this.icTableJoin.Images.SetKeyName(5, "Common_Table_Link_LeftOuterJoin_1.png");
            this.icTableJoin.Images.SetKeyName(6, "Common_Table_Link_RightOuterJoin_1.png");
            this.icTableJoin.Images.SetKeyName(7, "Common_Table_Link_FullOuterJoin_1.png");
            // 
            // icmbPrimaryDataField
            // 
            this.icmbPrimaryDataField.Location = new System.Drawing.Point(78, 136);
            this.icmbPrimaryDataField.Name = "icmbPrimaryDataField";
            this.icmbPrimaryDataField.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbPrimaryDataField.Properties.SmallImages = this.icDataFieldRelation;
            this.icmbPrimaryDataField.Size = new System.Drawing.Size(273, 20);
            this.icmbPrimaryDataField.TabIndex = 22;
            // 
            // icDataFieldRelation
            // 
            this.icDataFieldRelation.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icDataFieldRelation.ImageStream")));
            this.icDataFieldRelation.Images.SetKeyName(0, "Common_DataFieldRelation_Business.png");
            this.icDataFieldRelation.Images.SetKeyName(1, "Common_DataFieldRelation_BusinessForeignId.png");
            this.icDataFieldRelation.Images.SetKeyName(2, "Common_DataFieldRelation_BusinessAlternativeId.png");
            this.icDataFieldRelation.Images.SetKeyName(3, "Common_DataFieldRelation_User.png");
            this.icDataFieldRelation.Images.SetKeyName(4, "Common_DataFieldRelation_Department.png");
            this.icDataFieldRelation.Images.SetKeyName(5, "Common_DataFieldRelation_UserType.png");
            // 
            // lblPrimaryDataField
            // 
            this.lblPrimaryDataField.Location = new System.Drawing.Point(12, 137);
            this.lblPrimaryDataField.Name = "lblPrimaryDataField";
            this.lblPrimaryDataField.Size = new System.Drawing.Size(60, 14);
            this.lblPrimaryDataField.TabIndex = 20;
            this.lblPrimaryDataField.Text = "关系字段：";
            // 
            // lblPrimaryTable
            // 
            this.lblPrimaryTable.Location = new System.Drawing.Point(12, 103);
            this.lblPrimaryTable.Name = "lblPrimaryTable";
            this.lblPrimaryTable.Size = new System.Drawing.Size(60, 14);
            this.lblPrimaryTable.TabIndex = 18;
            this.lblPrimaryTable.Text = "从表名称：";
            // 
            // lblTableJoin
            // 
            this.lblTableJoin.Location = new System.Drawing.Point(12, 69);
            this.lblTableJoin.Name = "lblTableJoin";
            this.lblTableJoin.Size = new System.Drawing.Size(60, 14);
            this.lblTableJoin.TabIndex = 17;
            this.lblTableJoin.Text = "链接方式：";
            // 
            // lblTableRelation
            // 
            this.lblTableRelation.Location = new System.Drawing.Point(12, 35);
            this.lblTableRelation.Name = "lblTableRelation";
            this.lblTableRelation.Size = new System.Drawing.Size(60, 14);
            this.lblTableRelation.TabIndex = 16;
            this.lblTableRelation.Text = "关系选择：";
            // 
            // icmbTableRelation
            // 
            this.icmbTableRelation.Location = new System.Drawing.Point(78, 34);
            this.icmbTableRelation.Name = "icmbTableRelation";
            this.icmbTableRelation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbTableRelation.Properties.SmallImages = this.icTableRelation;
            this.icmbTableRelation.Size = new System.Drawing.Size(273, 20);
            this.icmbTableRelation.TabIndex = 5;
            // 
            // icTableRelation
            // 
            this.icTableRelation.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icTableRelation.ImageStream")));
            this.icTableRelation.Images.SetKeyName(0, "Common_TableRelation_Primary.png");
            this.icTableRelation.Images.SetKeyName(1, "Common_TableRelation_Previous.png");
            // 
            // dataTableDropdownList
            // 
            this.dataTableDropdownList.CustomCategoryContract = null;
            this.dataTableDropdownList.CustomDatabaseContract = null;
            this.dataTableDropdownList.CustomTableContract = null;
            this.dataTableDropdownList.DataWarehouseId = ((byte)(0));
            this.dataTableDropdownList.Location = new System.Drawing.Point(78, 102);
            this.dataTableDropdownList.Name = "dataTableDropdownList";
            this.dataTableDropdownList.OnlySelectedLeaf = true;
            this.dataTableDropdownList.Size = new System.Drawing.Size(273, 20);
            this.dataTableDropdownList.SkinName = "Blue";
            this.dataTableDropdownList.TabIndex = 0;
            this.dataTableDropdownList.TableFilter = AppFramework.Core.TableFilter.All;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 165);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(365, 39);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(202, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(105, 9);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "确定(&O)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // DataTableShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 204);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataTableShowForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "树形选择";
            this.Load += new System.EventHandler(this.DataTableSelectedItemsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            this.gcMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableJoin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icTableJoin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbPrimaryDataField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icDataFieldRelation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbTableRelation.Properties)).EndInit();
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
        private DataTableDropdownList dataTableDropdownList;
        private DevExpress.Utils.ImageCollection icTableJoin;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbTableRelation;
        private DevExpress.XtraEditors.LabelControl lblPrimaryDataField;
        private DevExpress.XtraEditors.LabelControl lblPrimaryTable;
        private DevExpress.XtraEditors.LabelControl lblTableJoin;
        private DevExpress.XtraEditors.LabelControl lblTableRelation;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbTableJoin;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbPrimaryDataField;
        private DevExpress.Utils.ImageCollection icTableRelation;
        private DevExpress.Utils.ImageCollection icDataFieldRelation;
    }
}