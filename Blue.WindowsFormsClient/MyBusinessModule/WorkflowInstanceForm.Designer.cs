namespace Blue.WindowsFormsClient.MyBusinessModule
{
    partial class WorkflowInstanceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkflowInstanceForm));
            this.icData = new DevExpress.Utils.ImageCollection(this.components);
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.pnlStep = new DevExpress.XtraEditors.PanelControl();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.btnPrevious = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnDraft = new DevExpress.XtraEditors.SimpleButton();
            this.hlnkDownload = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.hlnkReject = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.pnlReview = new DevExpress.XtraEditors.PanelControl();
            this.meLastestComment = new DevExpress.XtraEditors.MemoEdit();
            this.lblComment = new DevExpress.XtraEditors.LabelControl();
            this.lblLastestComment = new DevExpress.XtraEditors.LabelControl();
            this.meComment = new DevExpress.XtraEditors.MemoEdit();
            this.scBottom = new DevExpress.XtraEditors.SeparatorControl();
            this.icStep = new DevExpress.Utils.ImageCollection(this.components);
            this.gcToolTip = new DevExpress.XtraEditors.GroupControl();
            this.meToolTip = new DevExpress.XtraEditors.MemoEdit();
            this.xtcBussiness = new DevExpress.XtraTab.XtraTabControl();
            this.gcBusiness = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.icData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlStep)).BeginInit();
            this.pnlStep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlReview)).BeginInit();
            this.pnlReview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meLastestComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcToolTip)).BeginInit();
            this.gcToolTip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meToolTip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcBussiness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBusiness)).BeginInit();
            this.gcBusiness.SuspendLayout();
            this.SuspendLayout();
            // 
            // icData
            // 
            this.icData.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icData.ImageStream")));
            this.icData.Images.SetKeyName(0, "Clinet_Common_Save.png");
            this.icData.Images.SetKeyName(1, "Clinet_Common_Previous.png");
            this.icData.Images.SetKeyName(2, "Clinet_Common_Next.png");
            this.icData.Images.SetKeyName(3, "Client_Common_Handle.png");
            this.icData.Images.SetKeyName(4, "Client_Common_Download.png");
            this.icData.Images.SetKeyName(5, "Client_Common_Reject.png");
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlStep);
            this.pnlBottom.Controls.Add(this.pnlReview);
            this.pnlBottom.Controls.Add(this.scBottom);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 661);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1187, 113);
            this.pnlBottom.TabIndex = 100;
            // 
            // pnlStep
            // 
            this.pnlStep.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlStep.Controls.Add(this.lblTip);
            this.pnlStep.Controls.Add(this.btnPrevious);
            this.pnlStep.Controls.Add(this.btnNext);
            this.pnlStep.Controls.Add(this.btnDraft);
            this.pnlStep.Controls.Add(this.hlnkDownload);
            this.pnlStep.Controls.Add(this.hlnkReject);
            this.pnlStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStep.Location = new System.Drawing.Point(2, 77);
            this.pnlStep.Margin = new System.Windows.Forms.Padding(0);
            this.pnlStep.Name = "pnlStep";
            this.pnlStep.Size = new System.Drawing.Size(1183, 34);
            this.pnlStep.TabIndex = 37;
            // 
            // lblTip
            // 
            this.lblTip.Location = new System.Drawing.Point(236, 12);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(300, 14);
            this.lblTip.TabIndex = 7;
            this.lblTip.Text = "提示：请先保存为草稿后下载，正式版本在提交后下载。";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Enabled = false;
            this.btnPrevious.ImageIndex = 1;
            this.btnPrevious.ImageList = this.icData;
            this.btnPrevious.Location = new System.Drawing.Point(981, 6);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(89, 23);
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.Text = "上一步(&B)";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.ImageIndex = 2;
            this.btnNext.ImageList = this.icData;
            this.btnNext.Location = new System.Drawing.Point(1082, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(89, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "下一步(&N)";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnDraft
            // 
            this.btnDraft.ImageIndex = 0;
            this.btnDraft.ImageList = this.icData;
            this.btnDraft.Location = new System.Drawing.Point(11, 6);
            this.btnDraft.Name = "btnDraft";
            this.btnDraft.Size = new System.Drawing.Size(113, 23);
            this.btnDraft.TabIndex = 2;
            this.btnDraft.Text = "保存为草稿(&S)";
            this.btnDraft.Click += new System.EventHandler(this.btnDraft_Click);
            // 
            // hlnkDownload
            // 
            this.hlnkDownload.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkDownload.Appearance.ImageIndex = 4;
            this.hlnkDownload.Appearance.ImageList = this.icData;
            this.hlnkDownload.Appearance.Options.UseImageAlign = true;
            this.hlnkDownload.Appearance.Options.UseImageIndex = true;
            this.hlnkDownload.Appearance.Options.UseImageList = true;
            this.hlnkDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkDownload.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkDownload.Location = new System.Drawing.Point(134, 9);
            this.hlnkDownload.Name = "hlnkDownload";
            this.hlnkDownload.Size = new System.Drawing.Size(93, 20);
            this.hlnkDownload.TabIndex = 6;
            this.hlnkDownload.Text = "草稿表格下载";
            // 
            // hlnkReject
            // 
            this.hlnkReject.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.hlnkReject.Appearance.ImageIndex = 5;
            this.hlnkReject.Appearance.ImageList = this.icData;
            this.hlnkReject.Appearance.Options.UseImageAlign = true;
            this.hlnkReject.Appearance.Options.UseImageIndex = true;
            this.hlnkReject.Appearance.Options.UseImageList = true;
            this.hlnkReject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlnkReject.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.hlnkReject.Location = new System.Drawing.Point(905, 8);
            this.hlnkReject.Name = "hlnkReject";
            this.hlnkReject.Size = new System.Drawing.Size(69, 20);
            this.hlnkReject.TabIndex = 8;
            this.hlnkReject.Text = "驳回提交";
            this.hlnkReject.Click += new System.EventHandler(this.hlnkReject_Click);
            // 
            // pnlReview
            // 
            this.pnlReview.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlReview.Controls.Add(this.meLastestComment);
            this.pnlReview.Controls.Add(this.lblComment);
            this.pnlReview.Controls.Add(this.lblLastestComment);
            this.pnlReview.Controls.Add(this.meComment);
            this.pnlReview.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReview.Location = new System.Drawing.Point(2, 2);
            this.pnlReview.Margin = new System.Windows.Forms.Padding(0);
            this.pnlReview.Name = "pnlReview";
            this.pnlReview.Size = new System.Drawing.Size(1183, 75);
            this.pnlReview.TabIndex = 11;
            // 
            // meLastestComment
            // 
            this.meLastestComment.Location = new System.Drawing.Point(113, 6);
            this.meLastestComment.Name = "meLastestComment";
            this.meLastestComment.Properties.ReadOnly = true;
            this.meLastestComment.Size = new System.Drawing.Size(467, 65);
            this.meLastestComment.TabIndex = 11;
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(611, 9);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(84, 14);
            this.lblComment.TabIndex = 35;
            this.lblComment.Text = "本次审核意见：";
            // 
            // lblLastestComment
            // 
            this.lblLastestComment.Location = new System.Drawing.Point(12, 10);
            this.lblLastestComment.Name = "lblLastestComment";
            this.lblLastestComment.Size = new System.Drawing.Size(96, 14);
            this.lblLastestComment.TabIndex = 33;
            this.lblLastestComment.Text = "上一步审核意见：";
            // 
            // meComment
            // 
            this.meComment.Location = new System.Drawing.Point(700, 4);
            this.meComment.Name = "meComment";
            this.meComment.Properties.MaxLength = 512;
            this.meComment.Size = new System.Drawing.Size(467, 67);
            this.meComment.TabIndex = 34;
            // 
            // scBottom
            // 
            this.scBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scBottom.Location = new System.Drawing.Point(2, 32);
            this.scBottom.Name = "scBottom";
            this.scBottom.Size = new System.Drawing.Size(1181, 23);
            this.scBottom.TabIndex = 32;
            // 
            // icStep
            // 
            this.icStep.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icStep.ImageStream")));
            this.icStep.Images.SetKeyName(0, "DataFilled_Draft_State_Empty.png");
            this.icStep.Images.SetKeyName(1, "DataFilled_Draft_State_Filling.png");
            this.icStep.Images.SetKeyName(2, "DataFilled_Draft_State_Completed.png");
            // 
            // gcToolTip
            // 
            this.gcToolTip.Controls.Add(this.meToolTip);
            this.gcToolTip.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcToolTip.Location = new System.Drawing.Point(0, 0);
            this.gcToolTip.Name = "gcToolTip";
            this.gcToolTip.Padding = new System.Windows.Forms.Padding(2);
            this.gcToolTip.Size = new System.Drawing.Size(1187, 42);
            this.gcToolTip.TabIndex = 2;
            this.gcToolTip.Text = "工作流提示信息";
            // 
            // meToolTip
            // 
            this.meToolTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meToolTip.EditValue = "工作流内容测试。";
            this.meToolTip.Location = new System.Drawing.Point(4, 23);
            this.meToolTip.Name = "meToolTip";
            this.meToolTip.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.meToolTip.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.meToolTip.Properties.Appearance.Options.UseBackColor = true;
            this.meToolTip.Properties.Appearance.Options.UseForeColor = true;
            this.meToolTip.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.meToolTip.Properties.ReadOnly = true;
            this.meToolTip.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.meToolTip.Size = new System.Drawing.Size(1179, 15);
            this.meToolTip.TabIndex = 0;
            // 
            // xtcBussiness
            // 
            this.xtcBussiness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtcBussiness.Location = new System.Drawing.Point(2, 21);
            this.xtcBussiness.Name = "xtcBussiness";
            this.xtcBussiness.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.xtcBussiness.Size = new System.Drawing.Size(1183, 596);
            this.xtcBussiness.TabIndex = 1;
            this.xtcBussiness.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtcBussiness_SelectedPageChanged);
            // 
            // gcBusiness
            // 
            this.gcBusiness.Controls.Add(this.xtcBussiness);
            this.gcBusiness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBusiness.Location = new System.Drawing.Point(0, 42);
            this.gcBusiness.Name = "gcBusiness";
            this.gcBusiness.Size = new System.Drawing.Size(1187, 619);
            this.gcBusiness.TabIndex = 4;
            this.gcBusiness.Text = "业务步骤名称";
            // 
            // WorkflowInstanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 774);
            this.Controls.Add(this.gcBusiness);
            this.Controls.Add(this.gcToolTip);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WorkflowInstanceForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工作流填报";
            this.Load += new System.EventHandler(this.DataFilledInstanceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlStep)).EndInit();
            this.pnlStep.ResumeLayout(false);
            this.pnlStep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlReview)).EndInit();
            this.pnlReview.ResumeLayout(false);
            this.pnlReview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meLastestComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcToolTip)).EndInit();
            this.gcToolTip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.meToolTip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtcBussiness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBusiness)).EndInit();
            this.gcBusiness.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection icData;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnDraft;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnPrevious;
        private DevExpress.XtraEditors.GroupControl gcToolTip;
        private DevExpress.XtraEditors.MemoEdit meToolTip;
        private DevExpress.XtraTab.XtraTabControl xtcBussiness;
        private DevExpress.XtraEditors.GroupControl gcBusiness;
        private DevExpress.Utils.ImageCollection icStep;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkDownload;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlnkReject;
        private DevExpress.XtraEditors.MemoEdit meLastestComment;
        private DevExpress.XtraEditors.LabelControl lblComment;
        private DevExpress.XtraEditors.MemoEdit meComment;
        private DevExpress.XtraEditors.LabelControl lblLastestComment;
        private DevExpress.XtraEditors.SeparatorControl scBottom;
        private DevExpress.XtraEditors.PanelControl pnlStep;
        private DevExpress.XtraEditors.PanelControl pnlReview;
    }
}