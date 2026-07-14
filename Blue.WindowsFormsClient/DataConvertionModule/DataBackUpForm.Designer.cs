namespace Blue.WindowsFormsClient.DataConvertionModule
{
    partial class DataBackupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataBackupForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.gcVerifyTable = new DevExpress.XtraEditors.GroupControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.trvTable = new System.Windows.Forms.TreeView();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.chklstSystemTable = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.sbtnExplorer = new DevExpress.XtraEditors.SimpleButton();
            this.txtBackupDir = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBackupName = new DevExpress.XtraEditors.TextEdit();
            this.lblUserName = new System.Windows.Forms.Label();
            this.chkFullBackup = new System.Windows.Forms.CheckBox();
            this.sbtnBackup = new DevExpress.XtraEditors.SimpleButton();
            this.label6 = new System.Windows.Forms.Label();
            this.gcAutoBackup = new DevExpress.XtraEditors.GroupControl();
            this.meWarning = new DevExpress.XtraEditors.MemoEdit();
            this.lblWarning = new DevExpress.XtraEditors.LabelControl();
            this.txtBackupTime = new DevExpress.XtraEditors.TextEdit();
            this.lblBackupTime = new System.Windows.Forms.Label();
            this.dtBackupDateTime = new DevExpress.XtraEditors.DateEdit();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.lblBackupDateTime = new System.Windows.Forms.Label();
            this.icmbPeriod = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblDataRange = new System.Windows.Forms.Label();
            this.ccmbDataRange = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.chkAutoBackup = new DevExpress.XtraEditors.CheckEdit();
            this.lblAutoBackup = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.separatorControl3 = new DevExpress.XtraEditors.SeparatorControl();
            this.panel5 = new System.Windows.Forms.Panel();
            this.fpSpreadView = new FarPoint.Win.Spread.FpSpread();
            this.fpSpreadView_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.sbtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnView = new DevExpress.XtraEditors.SimpleButton();
            this.txtExcel = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lnlClearException = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcVerifyTable)).BeginInit();
            this.gcVerifyTable.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chklstSystemTable)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupDir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAutoBackup)).BeginInit();
            this.gcAutoBackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meWarning.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBackupDateTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBackupDateTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoBackup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadView_Sheet1)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExcel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gcVerifyTable);
            this.panel1.Controls.Add(this.gcAutoBackup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 589);
            this.panel1.TabIndex = 0;
            // 
            // gcVerifyTable
            // 
            this.gcVerifyTable.Controls.Add(this.panel3);
            this.gcVerifyTable.Controls.Add(this.panel4);
            this.gcVerifyTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcVerifyTable.Location = new System.Drawing.Point(0, 294);
            this.gcVerifyTable.LookAndFeel.SkinName = "Money Twins";
            this.gcVerifyTable.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcVerifyTable.Name = "gcVerifyTable";
            this.gcVerifyTable.Padding = new System.Windows.Forms.Padding(4);
            this.gcVerifyTable.Size = new System.Drawing.Size(404, 295);
            this.gcVerifyTable.TabIndex = 243;
            this.gcVerifyTable.Text = "手工备份操作(提示：手工备份数据文件在本地)";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.chklstSystemTable);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(6, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(392, 137);
            this.panel3.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.trvTable);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 103);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.panel7.Size = new System.Drawing.Size(392, 34);
            this.panel7.TabIndex = 90;
            // 
            // trvTable
            // 
            this.trvTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvTable.CheckBoxes = true;
            this.trvTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTable.ImageIndex = 0;
            this.trvTable.ImageList = this.imageListTree;
            this.trvTable.ItemHeight = 20;
            this.trvTable.Location = new System.Drawing.Point(0, 6);
            this.trvTable.Name = "trvTable";
            this.trvTable.SelectedImageIndex = 0;
            this.trvTable.Size = new System.Drawing.Size(392, 28);
            this.trvTable.TabIndex = 89;
            this.trvTable.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvTable_AfterCheck);
            this.trvTable.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvTable_BeforeExpand);
            this.trvTable.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.trvTable_AfterExpand);
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "2.jpg");
            this.imageListTree.Images.SetKeyName(1, "1.jpg");
            this.imageListTree.Images.SetKeyName(2, "3.jpg");
            // 
            // chklstSystemTable
            // 
            this.chklstSystemTable.Cursor = System.Windows.Forms.Cursors.Default;
            this.chklstSystemTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.chklstSystemTable.Location = new System.Drawing.Point(0, 0);
            this.chklstSystemTable.LookAndFeel.SkinName = "Blue";
            this.chklstSystemTable.LookAndFeel.UseDefaultLookAndFeel = false;
            this.chklstSystemTable.Name = "chklstSystemTable";
            this.chklstSystemTable.Size = new System.Drawing.Size(392, 103);
            this.chklstSystemTable.TabIndex = 89;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.sbtnExplorer);
            this.panel4.Controls.Add(this.txtBackupDir);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.txtBackupName);
            this.panel4.Controls.Add(this.lblUserName);
            this.panel4.Controls.Add(this.chkFullBackup);
            this.panel4.Controls.Add(this.sbtnBackup);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(6, 163);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(392, 126);
            this.panel4.TabIndex = 1;
            // 
            // sbtnExplorer
            // 
            this.sbtnExplorer.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_File_Explore;
            this.sbtnExplorer.Location = new System.Drawing.Point(305, 38);
            this.sbtnExplorer.LookAndFeel.SkinName = "Money Twins";
            this.sbtnExplorer.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnExplorer.Name = "sbtnExplorer";
            this.sbtnExplorer.Size = new System.Drawing.Size(74, 21);
            this.sbtnExplorer.TabIndex = 247;
            this.sbtnExplorer.Text = "浏览...";
            this.sbtnExplorer.Click += new System.EventHandler(this.sbtnExplorer_Click);
            // 
            // txtBackupDir
            // 
            this.txtBackupDir.Enabled = false;
            this.txtBackupDir.Location = new System.Drawing.Point(75, 39);
            this.txtBackupDir.Name = "txtBackupDir";
            this.txtBackupDir.Properties.MaxLength = 512;
            this.txtBackupDir.Size = new System.Drawing.Size(220, 20);
            this.txtBackupDir.TabIndex = 246;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 14);
            this.label5.TabIndex = 244;
            this.label5.Text = "备份目录：";
            // 
            // txtBackupName
            // 
            this.txtBackupName.Location = new System.Drawing.Point(75, 12);
            this.txtBackupName.Name = "txtBackupName";
            this.txtBackupName.Properties.MaxLength = 256;
            this.txtBackupName.Size = new System.Drawing.Size(307, 20);
            this.txtBackupName.TabIndex = 0;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(4, 14);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(67, 14);
            this.lblUserName.TabIndex = 243;
            this.lblUserName.Text = "备份名称：";
            // 
            // chkFullBackup
            // 
            this.chkFullBackup.AutoSize = true;
            this.chkFullBackup.BackColor = System.Drawing.Color.Transparent;
            this.chkFullBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkFullBackup.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkFullBackup.ForeColor = System.Drawing.Color.SteelBlue;
            this.chkFullBackup.Location = new System.Drawing.Point(75, 70);
            this.chkFullBackup.Name = "chkFullBackup";
            this.chkFullBackup.Size = new System.Drawing.Size(69, 16);
            this.chkFullBackup.TabIndex = 1;
            this.chkFullBackup.Text = "完全备份";
            this.chkFullBackup.UseVisualStyleBackColor = false;
            this.chkFullBackup.CheckedChanged += new System.EventHandler(this.chkFullBackup_CheckedChanged);
            // 
            // sbtnBackup
            // 
            this.sbtnBackup.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Confirm_16;
            this.sbtnBackup.Location = new System.Drawing.Point(266, 93);
            this.sbtnBackup.LookAndFeel.SkinName = "Money Twins";
            this.sbtnBackup.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnBackup.Name = "sbtnBackup";
            this.sbtnBackup.Size = new System.Drawing.Size(112, 21);
            this.sbtnBackup.TabIndex = 2;
            this.sbtnBackup.Text = "开始备份...(&S)";
            this.sbtnBackup.Click += new System.EventHandler(this.sbtnBackup_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 248;
            this.label6.Text = "备份方式：";
            // 
            // gcAutoBackup
            // 
            this.gcAutoBackup.Controls.Add(this.lnlClearException);
            this.gcAutoBackup.Controls.Add(this.meWarning);
            this.gcAutoBackup.Controls.Add(this.lblWarning);
            this.gcAutoBackup.Controls.Add(this.txtBackupTime);
            this.gcAutoBackup.Controls.Add(this.lblBackupTime);
            this.gcAutoBackup.Controls.Add(this.dtBackupDateTime);
            this.gcAutoBackup.Controls.Add(this.btnApply);
            this.gcAutoBackup.Controls.Add(this.lblBackupDateTime);
            this.gcAutoBackup.Controls.Add(this.icmbPeriod);
            this.gcAutoBackup.Controls.Add(this.lblDataRange);
            this.gcAutoBackup.Controls.Add(this.ccmbDataRange);
            this.gcAutoBackup.Controls.Add(this.lblPeriod);
            this.gcAutoBackup.Controls.Add(this.chkAutoBackup);
            this.gcAutoBackup.Controls.Add(this.lblAutoBackup);
            this.gcAutoBackup.Controls.Add(this.lblTip);
            this.gcAutoBackup.Controls.Add(this.separatorControl3);
            this.gcAutoBackup.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcAutoBackup.Location = new System.Drawing.Point(0, 0);
            this.gcAutoBackup.Name = "gcAutoBackup";
            this.gcAutoBackup.Size = new System.Drawing.Size(404, 294);
            this.gcAutoBackup.TabIndex = 244;
            this.gcAutoBackup.Text = "自动备份设置(提示：自动备份数据文件在服务器端)";
            // 
            // meWarning
            // 
            this.meWarning.EditValue = "无";
            this.meWarning.Location = new System.Drawing.Point(107, 216);
            this.meWarning.Name = "meWarning";
            this.meWarning.Properties.ReadOnly = true;
            this.meWarning.Size = new System.Drawing.Size(283, 43);
            this.meWarning.TabIndex = 257;
            // 
            // lblWarning
            // 
            this.lblWarning.Location = new System.Drawing.Point(42, 220);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(60, 14);
            this.lblWarning.TabIndex = 256;
            this.lblWarning.Text = "异常信息：";
            // 
            // txtBackupTime
            // 
            this.txtBackupTime.EditValue = "未进行备份";
            this.txtBackupTime.Location = new System.Drawing.Point(107, 186);
            this.txtBackupTime.Name = "txtBackupTime";
            this.txtBackupTime.Properties.ReadOnly = true;
            this.txtBackupTime.Size = new System.Drawing.Size(283, 20);
            this.txtBackupTime.TabIndex = 254;
            // 
            // lblBackupTime
            // 
            this.lblBackupTime.AutoSize = true;
            this.lblBackupTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBackupTime.Location = new System.Drawing.Point(10, 188);
            this.lblBackupTime.Name = "lblBackupTime";
            this.lblBackupTime.Size = new System.Drawing.Size(91, 14);
            this.lblBackupTime.TabIndex = 253;
            this.lblBackupTime.Text = "最后备份时间：";
            this.lblBackupTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtBackupDateTime
            // 
            this.dtBackupDateTime.EditValue = null;
            this.dtBackupDateTime.Location = new System.Drawing.Point(107, 57);
            this.dtBackupDateTime.Name = "dtBackupDateTime";
            this.dtBackupDateTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtBackupDateTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.dtBackupDateTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtBackupDateTime.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.dtBackupDateTime.Properties.DisplayFormat.FormatString = "G";
            this.dtBackupDateTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtBackupDateTime.Properties.EditFormat.FormatString = "G";
            this.dtBackupDateTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtBackupDateTime.Properties.Mask.EditMask = "G";
            this.dtBackupDateTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.dtBackupDateTime.Size = new System.Drawing.Size(283, 20);
            this.dtBackupDateTime.TabIndex = 1;
            this.dtBackupDateTime.EditValueChanged += new System.EventHandler(this.dtBackupDateTime_EditValueChanged);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_Apply_Small;
            this.btnApply.Location = new System.Drawing.Point(317, 143);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "应用(&A)";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblBackupDateTime
            // 
            this.lblBackupDateTime.AutoSize = true;
            this.lblBackupDateTime.Location = new System.Drawing.Point(10, 59);
            this.lblBackupDateTime.Name = "lblBackupDateTime";
            this.lblBackupDateTime.Size = new System.Drawing.Size(91, 14);
            this.lblBackupDateTime.TabIndex = 250;
            this.lblBackupDateTime.Text = "首次备份时间：";
            // 
            // icmbPeriod
            // 
            this.icmbPeriod.Location = new System.Drawing.Point(107, 86);
            this.icmbPeriod.Name = "icmbPeriod";
            this.icmbPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.icmbPeriod.Size = new System.Drawing.Size(283, 20);
            this.icmbPeriod.TabIndex = 1;
            this.icmbPeriod.SelectedIndexChanged += new System.EventHandler(this.icmbPeriod_SelectedIndexChanged);
            // 
            // lblDataRange
            // 
            this.lblDataRange.AutoSize = true;
            this.lblDataRange.Location = new System.Drawing.Point(34, 119);
            this.lblDataRange.Name = "lblDataRange";
            this.lblDataRange.Size = new System.Drawing.Size(67, 14);
            this.lblDataRange.TabIndex = 248;
            this.lblDataRange.Text = "备份范围：";
            // 
            // ccmbDataRange
            // 
            this.ccmbDataRange.Location = new System.Drawing.Point(107, 116);
            this.ccmbDataRange.Name = "ccmbDataRange";
            this.ccmbDataRange.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ccmbDataRange.Size = new System.Drawing.Size(283, 20);
            this.ccmbDataRange.TabIndex = 3;
            this.ccmbDataRange.EditValueChanged += new System.EventHandler(this.ccmbDataRange_EditValueChanged);
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(34, 88);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(67, 14);
            this.lblPeriod.TabIndex = 246;
            this.lblPeriod.Text = "备份周期：";
            // 
            // chkAutoBackup
            // 
            this.chkAutoBackup.Location = new System.Drawing.Point(107, 27);
            this.chkAutoBackup.Name = "chkAutoBackup";
            this.chkAutoBackup.Properties.Caption = "启用自动备份(每个表备份记录数不超过两百万条)";
            this.chkAutoBackup.Size = new System.Drawing.Size(291, 19);
            this.chkAutoBackup.TabIndex = 0;
            this.chkAutoBackup.CheckedChanged += new System.EventHandler(this.chkAutoBackup_CheckedChanged);
            // 
            // lblAutoBackup
            // 
            this.lblAutoBackup.AutoSize = true;
            this.lblAutoBackup.Location = new System.Drawing.Point(34, 30);
            this.lblAutoBackup.Name = "lblAutoBackup";
            this.lblAutoBackup.Size = new System.Drawing.Size(67, 14);
            this.lblAutoBackup.TabIndex = 244;
            this.lblAutoBackup.Text = "设置备份：";
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTip.Location = new System.Drawing.Point(35, 148);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(259, 14);
            this.lblTip.TabIndex = 251;
            this.lblTip.Text = "提示：服务器端的自动数据备份路径不能为空。";
            this.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // separatorControl3
            // 
            this.separatorControl3.Location = new System.Drawing.Point(11, 165);
            this.separatorControl3.Name = "separatorControl3";
            this.separatorControl3.Size = new System.Drawing.Size(381, 23);
            this.separatorControl3.TabIndex = 252;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.fpSpreadView);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(404, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(575, 589);
            this.panel5.TabIndex = 244;
            // 
            // fpSpreadView
            // 
            this.fpSpreadView.AccessibleDescription = "";
            this.fpSpreadView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpreadView.Location = new System.Drawing.Point(0, 38);
            this.fpSpreadView.Name = "fpSpreadView";
            this.fpSpreadView.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpreadView_Sheet1});
            this.fpSpreadView.Size = new System.Drawing.Size(575, 551);
            this.fpSpreadView.TabIndex = 245;
            this.fpSpreadView.TabStripInsertTab = false;
            this.fpSpreadView.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            // 
            // fpSpreadView_Sheet1
            // 
            this.fpSpreadView_Sheet1.Reset();
            this.fpSpreadView_Sheet1.SheetName = "Sheet1";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.panel6.Controls.Add(this.sbtnClear);
            this.panel6.Controls.Add(this.sbtnView);
            this.panel6.Controls.Add(this.txtExcel);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(575, 38);
            this.panel6.TabIndex = 0;
            // 
            // sbtnClear
            // 
            this.sbtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtnClear.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.sbtnClear.Location = new System.Drawing.Point(493, 9);
            this.sbtnClear.LookAndFeel.SkinName = "Money Twins";
            this.sbtnClear.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnClear.Name = "sbtnClear";
            this.sbtnClear.Size = new System.Drawing.Size(74, 21);
            this.sbtnClear.TabIndex = 251;
            this.sbtnClear.Text = "清除(&C)";
            this.sbtnClear.Click += new System.EventHandler(this.sbtnClear_Click);
            // 
            // sbtnView
            // 
            this.sbtnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtnView.Image = global::Blue.WindowsFormsClient.Properties.Resources.Common_File_Explore;
            this.sbtnView.Location = new System.Drawing.Point(412, 10);
            this.sbtnView.LookAndFeel.SkinName = "Money Twins";
            this.sbtnView.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnView.Name = "sbtnView";
            this.sbtnView.Size = new System.Drawing.Size(74, 21);
            this.sbtnView.TabIndex = 250;
            this.sbtnView.Text = "浏览...";
            this.sbtnView.Click += new System.EventHandler(this.sbtnView_Click);
            // 
            // txtExcel
            // 
            this.txtExcel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcel.Enabled = false;
            this.txtExcel.Location = new System.Drawing.Point(101, 10);
            this.txtExcel.Name = "txtExcel";
            this.txtExcel.Properties.MaxLength = 512;
            this.txtExcel.Size = new System.Drawing.Size(305, 20);
            this.txtExcel.TabIndex = 249;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 248;
            this.label1.Text = "备份文件浏览：";
            // 
            // openFileDialog
            // 
            this.openFileDialog.RestoreDirectory = true;
            // 
            // lnlClearException
            // 
            this.lnlClearException.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnlClearException.Location = new System.Drawing.Point(336, 268);
            this.lnlClearException.Name = "lnlClearException";
            this.lnlClearException.Size = new System.Drawing.Size(48, 14);
            this.lnlClearException.TabIndex = 90;
            this.lnlClearException.Text = "清除异常";
            this.lnlClearException.Click += new System.EventHandler(this.lnlClearException_Click);
            // 
            // DataBackupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 589);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataBackupForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据备份";
            this.Load += new System.EventHandler(this.DataBackupForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcVerifyTable)).EndInit();
            this.gcVerifyTable.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chklstSystemTable)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupDir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAutoBackup)).EndInit();
            this.gcAutoBackup.ResumeLayout(false);
            this.gcAutoBackup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meWarning.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBackupTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBackupDateTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtBackupDateTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmbPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ccmbDataRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoBackup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl3)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpreadView_Sheet1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExcel.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.GroupControl gcVerifyTable;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraEditors.SimpleButton sbtnBackup;
        private DevExpress.XtraEditors.CheckedListBoxControl chklstSystemTable;
        private System.Windows.Forms.CheckBox chkFullBackup;
        private DevExpress.XtraEditors.TextEdit txtBackupName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Panel panel5;
        private FarPoint.Win.Spread.FpSpread fpSpreadView;
        private FarPoint.Win.Spread.SheetView fpSpreadView_Sheet1;
        private System.Windows.Forms.Panel panel6;
        internal System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TreeView trvTable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private DevExpress.XtraEditors.TextEdit txtBackupDir;
        private DevExpress.XtraEditors.SimpleButton sbtnExplorer;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.SimpleButton sbtnView;
        private DevExpress.XtraEditors.TextEdit txtExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private DevExpress.XtraEditors.SimpleButton sbtnClear;
        private DevExpress.XtraEditors.GroupControl gcAutoBackup;
        private System.Windows.Forms.Label lblDataRange;
        private DevExpress.XtraEditors.CheckedComboBoxEdit ccmbDataRange;
        private System.Windows.Forms.Label lblPeriod;
        private DevExpress.XtraEditors.CheckEdit chkAutoBackup;
        private System.Windows.Forms.Label lblAutoBackup;
        private DevExpress.XtraEditors.ImageComboBoxEdit icmbPeriod;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private System.Windows.Forms.Label lblBackupDateTime;
        private System.Windows.Forms.Label lblTip;
        private DevExpress.XtraEditors.DateEdit dtBackupDateTime;
        private DevExpress.XtraEditors.TextEdit txtBackupTime;
        private System.Windows.Forms.Label lblBackupTime;
        private DevExpress.XtraEditors.SeparatorControl separatorControl3;
        private DevExpress.XtraEditors.MemoEdit meWarning;
        private DevExpress.XtraEditors.LabelControl lblWarning;
        private DevExpress.XtraEditors.HyperlinkLabelControl lnlClearException;
    }
}