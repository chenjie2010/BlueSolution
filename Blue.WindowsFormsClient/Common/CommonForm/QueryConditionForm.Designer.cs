namespace Blue.WindowsFormsClient
{
    partial class QueryConditionForm
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
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.cmbeBoolean = new DevExpress.XtraEditors.ComboBoxEdit();
            this.extDigitCondition = new DevExpress.XtraEditors.TextEdit();
            this.pnlCondition = new DevExpress.XtraEditors.PanelControl();
            this.txtQueryCondition = new DevExpress.XtraEditors.MemoEdit();
            this.chkAbort = new System.Windows.Forms.CheckBox();
            this.sbtnClear = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.lblComment = new System.Windows.Forms.Label();
            this.sbtnReset = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.cmbeOperation = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbeCombination = new DevExpress.XtraEditors.ComboBoxEdit();
            this.etxtPhyscialName = new DevExpress.XtraEditors.TextEdit();
            this.chkRightBracket = new System.Windows.Forms.CheckBox();
            this.chkLeftBracket = new System.Windows.Forms.CheckBox();
            this.lblPhyscialName = new System.Windows.Forms.Label();
            this.lblRightBracket = new System.Windows.Forms.Label();
            this.lblCombination = new System.Windows.Forms.Label();
            this.lblCondition = new System.Windows.Forms.Label();
            this.lblOperation = new System.Windows.Forms.Label();
            this.lblLeftBracket = new System.Windows.Forms.Label();
            this.timeEditCondition = new DevExpress.XtraEditors.TimeEdit();
            this.etxtStringCondition = new DevExpress.XtraEditors.TextEdit();
            this.lookUpEditCondition = new DevExpress.XtraEditors.LookUpEdit();
            this.dateEditCondition = new DevExpress.XtraEditors.DateEdit();
            this.chkembCondition = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.ecmbCondition = new Blue.WindowsFormsClient.TreeDropdownList();
            this.cmbeDataFieldType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDataFieldName = new System.Windows.Forms.Label();
            this.cmbeDataFieldProperty = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDataFieldType = new System.Windows.Forms.Label();
            this.lblDataFieldProperty = new System.Windows.Forms.Label();
            this.etxtDataFieldName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeBoolean.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.extDigitCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).BeginInit();
            this.pnlCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeOperation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeCombination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtPhyscialName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtStringCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditCondition.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkembCondition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeDataFieldType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeDataFieldProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtDataFieldName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.cmbeBoolean);
            this.groupControl.Controls.Add(this.extDigitCondition);
            this.groupControl.Controls.Add(this.pnlCondition);
            this.groupControl.Controls.Add(this.sbtnReset);
            this.groupControl.Controls.Add(this.sbtnAdd);
            this.groupControl.Controls.Add(this.cmbeOperation);
            this.groupControl.Controls.Add(this.cmbeCombination);
            this.groupControl.Controls.Add(this.etxtPhyscialName);
            this.groupControl.Controls.Add(this.chkRightBracket);
            this.groupControl.Controls.Add(this.chkLeftBracket);
            this.groupControl.Controls.Add(this.lblPhyscialName);
            this.groupControl.Controls.Add(this.lblRightBracket);
            this.groupControl.Controls.Add(this.lblCombination);
            this.groupControl.Controls.Add(this.lblCondition);
            this.groupControl.Controls.Add(this.lblOperation);
            this.groupControl.Controls.Add(this.lblLeftBracket);
            this.groupControl.Controls.Add(this.timeEditCondition);
            this.groupControl.Controls.Add(this.etxtStringCondition);
            this.groupControl.Controls.Add(this.lookUpEditCondition);
            this.groupControl.Controls.Add(this.dateEditCondition);
            this.groupControl.Controls.Add(this.chkembCondition);
            this.groupControl.Controls.Add(this.ecmbCondition);
            this.groupControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl.Location = new System.Drawing.Point(0, 88);
            this.groupControl.LookAndFeel.SkinName = "Money Twins";
            this.groupControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(406, 330);
            this.groupControl.TabIndex = 96;
            this.groupControl.Text = "条件设置";
            this.groupControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.groupControl_MouseClick);
            // 
            // cmbeBoolean
            // 
            this.cmbeBoolean.Location = new System.Drawing.Point(79, 137);
            this.cmbeBoolean.Name = "cmbeBoolean";
            this.cmbeBoolean.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeBoolean.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbeBoolean.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbeBoolean.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbeBoolean.Size = new System.Drawing.Size(128, 20);
            this.cmbeBoolean.TabIndex = 120;
            // 
            // extDigitCondition
            // 
            this.extDigitCondition.Location = new System.Drawing.Point(79, 137);
            this.extDigitCondition.Name = "extDigitCondition";
            this.extDigitCondition.Properties.MaxLength = 128;
            this.extDigitCondition.Size = new System.Drawing.Size(200, 20);
            this.extDigitCondition.TabIndex = 118;
            // 
            // pnlCondition
            // 
            this.pnlCondition.Controls.Add(this.txtQueryCondition);
            this.pnlCondition.Controls.Add(this.chkAbort);
            this.pnlCondition.Controls.Add(this.sbtnClear);
            this.pnlCondition.Controls.Add(this.sbtnClose);
            this.pnlCondition.Controls.Add(this.sbtnConfirm);
            this.pnlCondition.Controls.Add(this.lblComment);
            this.pnlCondition.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCondition.Location = new System.Drawing.Point(2, 216);
            this.pnlCondition.LookAndFeel.SkinName = "Money Twins";
            this.pnlCondition.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pnlCondition.Name = "pnlCondition";
            this.pnlCondition.Size = new System.Drawing.Size(402, 112);
            this.pnlCondition.TabIndex = 112;
            this.pnlCondition.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlCondition_MouseClick);
            // 
            // txtQueryCondition
            // 
            this.txtQueryCondition.Location = new System.Drawing.Point(77, 8);
            this.txtQueryCondition.Name = "txtQueryCondition";
            this.txtQueryCondition.Properties.MaxLength = 1024;
            this.txtQueryCondition.Size = new System.Drawing.Size(314, 69);
            this.txtQueryCondition.TabIndex = 123;
            // 
            // chkAbort
            // 
            this.chkAbort.AutoSize = true;
            this.chkAbort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAbort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222)))));
            this.chkAbort.Location = new System.Drawing.Point(5, 91);
            this.chkAbort.Name = "chkAbort";
            this.chkAbort.Size = new System.Drawing.Size(107, 18);
            this.chkAbort.TabIndex = 122;
            this.chkAbort.Text = "不验证查询条件";
            this.chkAbort.UseVisualStyleBackColor = true;
            // 
            // sbtnClear
            // 
            this.sbtnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.sbtnClear.Image = global::Blue.WindowsFormsClient.Properties.Resources.Button_Remove_Small;
            this.sbtnClear.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.sbtnClear.Location = new System.Drawing.Point(241, 83);
            this.sbtnClear.LookAndFeel.SkinName = "Blue";
            this.sbtnClear.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnClear.Name = "sbtnClear";
            this.sbtnClear.Size = new System.Drawing.Size(24, 24);
            this.sbtnClear.TabIndex = 121;
            this.sbtnClear.Click += new System.EventHandler(this.sbtnClear_Click);
            // 
            // sbtnClose
            // 
            this.sbtnClose.Location = new System.Drawing.Point(334, 83);
            this.sbtnClose.LookAndFeel.SkinName = "Blue";
            this.sbtnClose.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnClose.Name = "sbtnClose";
            this.sbtnClose.Size = new System.Drawing.Size(55, 23);
            this.sbtnClose.TabIndex = 113;
            this.sbtnClose.Text = "关闭(&C)";
            this.sbtnClose.Click += new System.EventHandler(this.sbtnClose_Click);
            // 
            // sbtnConfirm
            // 
            this.sbtnConfirm.Location = new System.Drawing.Point(271, 83);
            this.sbtnConfirm.LookAndFeel.SkinName = "Blue";
            this.sbtnConfirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnConfirm.Name = "sbtnConfirm";
            this.sbtnConfirm.Size = new System.Drawing.Size(55, 23);
            this.sbtnConfirm.TabIndex = 112;
            this.sbtnConfirm.Text = "确定(&O)";
            this.sbtnConfirm.Click += new System.EventHandler(this.sbtnConfirm_Click);
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(8, 10);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(67, 14);
            this.lblComment.TabIndex = 103;
            this.lblComment.Text = "查询条件：";
            // 
            // sbtnReset
            // 
            this.sbtnReset.Location = new System.Drawing.Point(140, 188);
            this.sbtnReset.LookAndFeel.SkinName = "Blue";
            this.sbtnReset.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnReset.Name = "sbtnReset";
            this.sbtnReset.Size = new System.Drawing.Size(55, 23);
            this.sbtnReset.TabIndex = 111;
            this.sbtnReset.Text = "重置(&R)";
            this.sbtnReset.Click += new System.EventHandler(this.sbtnReset_Click);
            // 
            // sbtnAdd
            // 
            this.sbtnAdd.Location = new System.Drawing.Point(77, 188);
            this.sbtnAdd.LookAndFeel.SkinName = "Blue";
            this.sbtnAdd.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sbtnAdd.Name = "sbtnAdd";
            this.sbtnAdd.Size = new System.Drawing.Size(55, 23);
            this.sbtnAdd.TabIndex = 109;
            this.sbtnAdd.Text = "添加(&A)";
            this.sbtnAdd.Click += new System.EventHandler(this.sbtnAdd_Click);
            // 
            // cmbeOperation
            // 
            this.cmbeOperation.Location = new System.Drawing.Point(79, 109);
            this.cmbeOperation.Name = "cmbeOperation";
            this.cmbeOperation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeOperation.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbeOperation.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbeOperation.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbeOperation.Size = new System.Drawing.Size(128, 20);
            this.cmbeOperation.TabIndex = 108;
            // 
            // cmbeCombination
            // 
            this.cmbeCombination.Location = new System.Drawing.Point(79, 52);
            this.cmbeCombination.Name = "cmbeCombination";
            this.cmbeCombination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeCombination.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbeCombination.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbeCombination.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbeCombination.Size = new System.Drawing.Size(128, 20);
            this.cmbeCombination.TabIndex = 107;
            // 
            // etxtPhyscialName
            // 
            this.etxtPhyscialName.Location = new System.Drawing.Point(79, 81);
            this.etxtPhyscialName.Name = "etxtPhyscialName";
            this.etxtPhyscialName.Properties.ReadOnly = true;
            this.etxtPhyscialName.Size = new System.Drawing.Size(314, 20);
            this.etxtPhyscialName.TabIndex = 106;
            // 
            // chkRightBracket
            // 
            this.chkRightBracket.AutoSize = true;
            this.chkRightBracket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRightBracket.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222)))));
            this.chkRightBracket.Location = new System.Drawing.Point(79, 169);
            this.chkRightBracket.Name = "chkRightBracket";
            this.chkRightBracket.Size = new System.Drawing.Size(12, 11);
            this.chkRightBracket.TabIndex = 105;
            this.chkRightBracket.UseVisualStyleBackColor = true;
            // 
            // chkLeftBracket
            // 
            this.chkLeftBracket.AutoSize = true;
            this.chkLeftBracket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkLeftBracket.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(193)))), ((int)(((byte)(222)))));
            this.chkLeftBracket.Location = new System.Drawing.Point(79, 30);
            this.chkLeftBracket.Name = "chkLeftBracket";
            this.chkLeftBracket.Size = new System.Drawing.Size(12, 11);
            this.chkLeftBracket.TabIndex = 104;
            this.chkLeftBracket.UseVisualStyleBackColor = true;
            // 
            // lblPhyscialName
            // 
            this.lblPhyscialName.AutoSize = true;
            this.lblPhyscialName.Location = new System.Drawing.Point(10, 85);
            this.lblPhyscialName.Name = "lblPhyscialName";
            this.lblPhyscialName.Size = new System.Drawing.Size(67, 14);
            this.lblPhyscialName.TabIndex = 102;
            this.lblPhyscialName.Text = "物理名称：";
            // 
            // lblRightBracket
            // 
            this.lblRightBracket.AutoSize = true;
            this.lblRightBracket.Location = new System.Drawing.Point(22, 169);
            this.lblRightBracket.Name = "lblRightBracket";
            this.lblRightBracket.Size = new System.Drawing.Size(55, 14);
            this.lblRightBracket.TabIndex = 101;
            this.lblRightBracket.Text = "右括号：";
            // 
            // lblCombination
            // 
            this.lblCombination.AutoSize = true;
            this.lblCombination.Location = new System.Drawing.Point(22, 56);
            this.lblCombination.Name = "lblCombination";
            this.lblCombination.Size = new System.Drawing.Size(55, 14);
            this.lblCombination.TabIndex = 100;
            this.lblCombination.Text = "组合符：";
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Location = new System.Drawing.Point(22, 140);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(55, 14);
            this.lblCondition.TabIndex = 99;
            this.lblCondition.Text = "条件值：";
            // 
            // lblOperation
            // 
            this.lblOperation.AutoSize = true;
            this.lblOperation.Location = new System.Drawing.Point(22, 112);
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Size = new System.Drawing.Size(55, 14);
            this.lblOperation.TabIndex = 98;
            this.lblOperation.Text = "操作符：";
            // 
            // lblLeftBracket
            // 
            this.lblLeftBracket.AutoSize = true;
            this.lblLeftBracket.Location = new System.Drawing.Point(22, 30);
            this.lblLeftBracket.Name = "lblLeftBracket";
            this.lblLeftBracket.Size = new System.Drawing.Size(55, 14);
            this.lblLeftBracket.TabIndex = 97;
            this.lblLeftBracket.Text = "左括号：";
            // 
            // timeEditCondition
            // 
            this.timeEditCondition.EditValue = new System.DateTime(2011, 8, 27, 0, 0, 0, 0);
            this.timeEditCondition.Location = new System.Drawing.Point(79, 137);
            this.timeEditCondition.Name = "timeEditCondition";
            this.timeEditCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEditCondition.Properties.LookAndFeel.SkinName = "Blue";
            this.timeEditCondition.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.timeEditCondition.Size = new System.Drawing.Size(128, 20);
            this.timeEditCondition.TabIndex = 116;
            // 
            // etxtStringCondition
            // 
            this.etxtStringCondition.Location = new System.Drawing.Point(79, 137);
            this.etxtStringCondition.Name = "etxtStringCondition";
            this.etxtStringCondition.Properties.MaxLength = 128;
            this.etxtStringCondition.Size = new System.Drawing.Size(314, 20);
            this.etxtStringCondition.TabIndex = 110;
            // 
            // lookUpEditCondition
            // 
            this.lookUpEditCondition.Location = new System.Drawing.Point(79, 137);
            this.lookUpEditCondition.Name = "lookUpEditCondition";
            this.lookUpEditCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditCondition.Properties.LookAndFeel.SkinName = "Blue";
            this.lookUpEditCondition.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lookUpEditCondition.Size = new System.Drawing.Size(314, 20);
            this.lookUpEditCondition.TabIndex = 117;
            // 
            // dateEditCondition
            // 
            this.dateEditCondition.EditValue = null;
            this.dateEditCondition.Location = new System.Drawing.Point(79, 137);
            this.dateEditCondition.Name = "dateEditCondition";
            this.dateEditCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditCondition.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditCondition.Properties.LookAndFeel.SkinName = "Blue";
            this.dateEditCondition.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dateEditCondition.Size = new System.Drawing.Size(314, 20);
            this.dateEditCondition.TabIndex = 115;
            // 
            // chkembCondition
            // 
            this.chkembCondition.EditValue = "";
            this.chkembCondition.Location = new System.Drawing.Point(79, 137);
            this.chkembCondition.Name = "chkembCondition";
            this.chkembCondition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkembCondition.Properties.LookAndFeel.SkinName = "Blue";
            this.chkembCondition.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.chkembCondition.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.chkembCondition.Properties.PopupSizeable = false;
            this.chkembCondition.Properties.SelectAllItemVisible = false;
            this.chkembCondition.Properties.ShowButtons = false;
            this.chkembCondition.Properties.ShowPopupCloseButton = false;
            this.chkembCondition.Size = new System.Drawing.Size(314, 20);
            this.chkembCondition.TabIndex = 114;
            // 
            // ecmbCondition
            // 
            this.ecmbCondition.Location = new System.Drawing.Point(79, 137);
            this.ecmbCondition.Name = "ecmbCondition";
            this.ecmbCondition.ShowSearch = true;
            this.ecmbCondition.Size = new System.Drawing.Size(314, 21);
            this.ecmbCondition.TabIndex = 121;
            this.ecmbCondition.TreeDropdownHandler = null;
            // 
            // cmbeDataFieldType
            // 
            this.cmbeDataFieldType.Location = new System.Drawing.Point(79, 62);
            this.cmbeDataFieldType.Name = "cmbeDataFieldType";
            this.cmbeDataFieldType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeDataFieldType.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbeDataFieldType.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbeDataFieldType.Properties.ReadOnly = true;
            this.cmbeDataFieldType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbeDataFieldType.Size = new System.Drawing.Size(314, 20);
            this.cmbeDataFieldType.TabIndex = 102;
            // 
            // lblDataFieldName
            // 
            this.lblDataFieldName.AutoSize = true;
            this.lblDataFieldName.Location = new System.Drawing.Point(10, 10);
            this.lblDataFieldName.Name = "lblDataFieldName";
            this.lblDataFieldName.Size = new System.Drawing.Size(65, 12);
            this.lblDataFieldName.TabIndex = 97;
            this.lblDataFieldName.Text = "字段名称：";
            // 
            // cmbeDataFieldProperty
            // 
            this.cmbeDataFieldProperty.Location = new System.Drawing.Point(79, 34);
            this.cmbeDataFieldProperty.Name = "cmbeDataFieldProperty";
            this.cmbeDataFieldProperty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbeDataFieldProperty.Properties.LookAndFeel.SkinName = "Blue";
            this.cmbeDataFieldProperty.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbeDataFieldProperty.Properties.ReadOnly = true;
            this.cmbeDataFieldProperty.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbeDataFieldProperty.Size = new System.Drawing.Size(314, 20);
            this.cmbeDataFieldProperty.TabIndex = 101;
            // 
            // lblDataFieldType
            // 
            this.lblDataFieldType.AutoSize = true;
            this.lblDataFieldType.Location = new System.Drawing.Point(10, 66);
            this.lblDataFieldType.Name = "lblDataFieldType";
            this.lblDataFieldType.Size = new System.Drawing.Size(65, 12);
            this.lblDataFieldType.TabIndex = 98;
            this.lblDataFieldType.Text = "字段类型：";
            // 
            // lblDataFieldProperty
            // 
            this.lblDataFieldProperty.AutoSize = true;
            this.lblDataFieldProperty.Location = new System.Drawing.Point(10, 38);
            this.lblDataFieldProperty.Name = "lblDataFieldProperty";
            this.lblDataFieldProperty.Size = new System.Drawing.Size(65, 12);
            this.lblDataFieldProperty.TabIndex = 99;
            this.lblDataFieldProperty.Text = "字段属性：";
            // 
            // etxtDataFieldName
            // 
            this.etxtDataFieldName.Location = new System.Drawing.Point(79, 7);
            this.etxtDataFieldName.Name = "etxtDataFieldName";
            this.etxtDataFieldName.Properties.ReadOnly = true;
            this.etxtDataFieldName.Size = new System.Drawing.Size(314, 20);
            this.etxtDataFieldName.TabIndex = 100;
            // 
            // QueryConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(406, 418);
            this.Controls.Add(this.cmbeDataFieldType);
            this.Controls.Add(this.lblDataFieldName);
            this.Controls.Add(this.cmbeDataFieldProperty);
            this.Controls.Add(this.lblDataFieldType);
            this.Controls.Add(this.etxtDataFieldName);
            this.Controls.Add(this.lblDataFieldProperty);
            this.Controls.Add(this.groupControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "QueryConditionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置字段的查询条件";
            this.Load += new System.EventHandler(this.QueryConditionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            this.groupControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeBoolean.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.extDigitCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCondition)).EndInit();
            this.pnlCondition.ResumeLayout(false);
            this.pnlCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQueryCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeOperation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeCombination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtPhyscialName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEditCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtStringCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditCondition.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkembCondition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeDataFieldType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbeDataFieldProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etxtDataFieldName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl;
        private DevExpress.XtraEditors.PanelControl pnlCondition;
        private DevExpress.XtraEditors.SimpleButton sbtnClose;
        private DevExpress.XtraEditors.SimpleButton sbtnConfirm;
        private System.Windows.Forms.Label lblComment;
        private DevExpress.XtraEditors.SimpleButton sbtnReset;
        private DevExpress.XtraEditors.TextEdit etxtStringCondition;
        private DevExpress.XtraEditors.SimpleButton sbtnAdd;
        private DevExpress.XtraEditors.ComboBoxEdit cmbeOperation;
        private DevExpress.XtraEditors.ComboBoxEdit cmbeCombination;
        private DevExpress.XtraEditors.TextEdit etxtPhyscialName;
        protected System.Windows.Forms.CheckBox chkRightBracket;
        protected System.Windows.Forms.CheckBox chkLeftBracket;
        private System.Windows.Forms.Label lblPhyscialName;
        private System.Windows.Forms.Label lblRightBracket;
        private System.Windows.Forms.Label lblCombination;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.Label lblOperation;
        private System.Windows.Forms.Label lblLeftBracket;
        private DevExpress.XtraEditors.ComboBoxEdit cmbeDataFieldType;
        private System.Windows.Forms.Label lblDataFieldName;
        private DevExpress.XtraEditors.ComboBoxEdit cmbeDataFieldProperty;
        private System.Windows.Forms.Label lblDataFieldType;
        private DevExpress.XtraEditors.TextEdit etxtDataFieldName;
        private System.Windows.Forms.Label lblDataFieldProperty;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkembCondition;
        private DevExpress.XtraEditors.DateEdit dateEditCondition;
        private DevExpress.XtraEditors.TimeEdit timeEditCondition;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditCondition;
        private DevExpress.XtraEditors.TextEdit extDigitCondition;
        private DevExpress.XtraEditors.ComboBoxEdit cmbeBoolean;
        private DevExpress.XtraEditors.SimpleButton sbtnClear;
        protected System.Windows.Forms.CheckBox chkAbort;
        private DevExpress.XtraEditors.MemoEdit txtQueryCondition;
        private TreeDropdownList ecmbCondition;
    }
}