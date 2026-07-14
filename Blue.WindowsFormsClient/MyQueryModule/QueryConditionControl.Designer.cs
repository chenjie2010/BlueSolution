namespace Blue.WindowsFormsClient.MyQueryModule
{
    partial class QueryConditionControl
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
            this.gcMain = new DevExpress.XtraEditors.GroupControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.queryItemControl1 = new Blue.WindowsFormsClient.MyQueryModule.QueryItemControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            this.gcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.CaptionImage = global::Blue.WindowsFormsClient.Properties.Resources.Client_Query_Item;
            this.gcMain.Controls.Add(this.pnlMain);
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(691, 416);
            this.gcMain.TabIndex = 3;
            this.gcMain.Text = "数据查询状态主界面";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.queryItemControl1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(2, 21);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(687, 393);
            this.pnlMain.TabIndex = 34;
            // 
            // queryItemControl1
            // 
            this.queryItemControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queryItemControl1.GoBack = null;
            this.queryItemControl1.Location = new System.Drawing.Point(2, 2);
            this.queryItemControl1.Name = "queryItemControl1";
            this.queryItemControl1.Size = new System.Drawing.Size(683, 389);
            this.queryItemControl1.TabIndex = 0;
            // 
            // QueryConditionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Name = "QueryConditionControl";
            this.Size = new System.Drawing.Size(691, 416);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            this.gcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcMain;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private QueryItemControl queryItemControl1;
    }
}
