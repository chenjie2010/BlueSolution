using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient
{
    public partial class QueryConditionForm : Form
    {
        #region 契约接口

        private IUserTypeContract userTypeContract;
        private ICustomDepartmentContract departmentContract;
        private ICustomEnumContract customEnumContract;
        private ICustomAssociationContract customAssociationContract;
        private ICustomDataFieldContract customDataFieldContract;
        private IAssociatedDataFieldContract associatedDataFieldContract;
        private ICustomGroupContract customGroupContract;

        #endregion

        #region 私有变量


        #endregion

        #region 内部成员变量

        private CustomDataFieldInfo _customDataFieldInfo;
        
        #endregion

        #region 属性

        /// <summary>
        /// 字段所属的表名
        /// </summary>
        public string TableName
        {
            get;
            set;
        }
       
        /// <summary>
        /// 字段信息
        /// </summary>
        public CustomDataFieldInfo CustomDataFieldInfo
        {
            set
            {
                _customDataFieldInfo = value;                
            }
        }

        /// <summary>
        /// 更新字段的 WHERE 条件
        /// </summary>
        public UpdateTextHandler UpdateTextHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 查询条件
        /// </summary>       
        public string QueryCondition
        {
            set
            {
                txtQueryCondition.Text = value;
            }
        }

        /// <summary>
        /// 是否验证条件
        /// </summary>
        public bool IsValidate
        {
            get;
            set;
        }

        #endregion 

        #region 构造函数
        
        public QueryConditionForm()
        {
            InitializeComponent();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            departmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
        }

        #endregion

        #region 窗体方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryConditionForm_Load(object sender, EventArgs e)
        {
            cmbeCombination.Properties.Items.Add(string.Empty);
            cmbeCombination.Properties.Items.Add("And ");
            cmbeCombination.Properties.Items.Add("Or ");
            cmbeCombination.Properties.Items.Add("And (");
            cmbeCombination.Properties.Items.Add("Or (");

            if (_customDataFieldInfo != null)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)_customDataFieldInfo.DataFieldProperty;
                cmbeDataFieldProperty.Properties.Items.Add(UserEnumHelper.GetEnumText(dataFieldProperty));
                cmbeDataFieldProperty.SelectedIndex = 0;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.LogicalDataField:
                        cmbeDataFieldType.Properties.Items.Add(UserEnumHelper.GetEnumText((LogicalDataFieldType)_customDataFieldInfo.DataFieldType));
                        etxtPhyscialName.Text = _customDataFieldInfo.PhysicalName;
                        break;

                    case DataFieldProperty.PhysicalDataField:
                        cmbeDataFieldType.Properties.Items.Add(UserEnumHelper.GetEnumText((PhysicalDataFieldType)_customDataFieldInfo.DataFieldType));
                        etxtPhyscialName.Text = _customDataFieldInfo.PhysicalName;
                        break;

                    case DataFieldProperty.SystemPhysicalDataField:
                        SystemDataField systemDataField = (SystemDataField)Convert.ToByte(_customDataFieldInfo.DataFieldId);
                        if (systemDataField == SystemDataField.DepName)
                        {
                            etxtPhyscialName.Text = string.Format("{0}.{1}", TableName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.DepId));
                        }
                        else if (systemDataField == SystemDataField.UserTypeName)
                        {
                            etxtPhyscialName.Text = string.Format("{0}.{1}", TableName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserTypeId));
                        }
                        else
                        {
                            etxtPhyscialName.Text = _customDataFieldInfo.PhysicalName;
                        }
                        cmbeDataFieldType.Properties.Items.Add(UserEnumHelper.GetEnumText(DataFieldHelper.GetBasedDataType(systemDataField)));
                        break;
                }           
                cmbeDataFieldType.SelectedIndex = 0;
                InitDropdownListProperty(_customDataFieldInfo);
                etxtDataFieldName.Text = _customDataFieldInfo.LogicalName;                
            }            
        }
        
        /// <summary>
        /// 关闭下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlCondition_MouseClick(object sender, MouseEventArgs e)
        {
            if (ecmbCondition.Visible)
            {
                ecmbCondition.ClosePopup();
            }
        }

        /// <summary>
        /// 关闭下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (ecmbCondition.Visible)
            {
                ecmbCondition.ClosePopup();
            }
        }

        /// <summary>
        /// 增加条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnAdd_Click(object sender, EventArgs e)
        {
            IList<string> conditions = GetWhereCondition();
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(txtQueryCondition.Text))
            {
                sb.Append(" ");
            }
            if (chkLeftBracket.Checked)
            {
                sb.Append("(");
            }
            if (cmbeCombination.SelectedIndex >= 0)
            {
                sb.Append(cmbeCombination.Text);
            }
            if (conditions.Count == 0)
            {
                sb.Append(etxtPhyscialName.Text);
                string operation = cmbeOperation.Text.Trim();
                if (operation.Equals("="))
                {
                    sb.Append(" IS NULL ");
                }
                else if (operation.Equals("!="))
                {
                    sb.Append(" IS NOT NULL ");
                }
                else
                {
                    MessageBox.Show("条件值不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {                
                if (cmbeOperation.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择操作符！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string operation = cmbeOperation.Text.Trim();
                StringBuilder sbCondition = new StringBuilder();
                foreach (string condition in conditions)
                { 
                    if (sbCondition.Length > 0)
                    {
                        if (operation.Equals("=") || operation.Equals("LIKE"))
                        {
                            sbCondition.Append(" OR ");
                        }
                        else
                        {
                            sbCondition.Append(" AND ");
                        }
                    }
                    sbCondition.Append(etxtPhyscialName.Text);
                    sbCondition.Append(cmbeOperation.Text);
                    sbCondition.Append(condition);               
                }
                sb.Append(sbCondition);
            }
            if (chkRightBracket.Checked)
            {
                sb.Append(")");
            }
            if (sb.Length > 10240)
            {
                MessageBox.Show("对不起，查询条件字符长度超出范围（10240位）！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtQueryCondition.Text = txtQueryCondition.Text + sb.ToString();
            ClearDataOnControls();
        }

        /// <summary>
        /// 重置条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnReset_Click(object sender, EventArgs e)
        {
            ClearDataOnControls();
        }        

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnConfirm_Click(object sender, EventArgs e)
        {
            if (_customDataFieldInfo != null && IsValidate)
            {
                if (!chkAbort.Checked)
                {
                    bool success = customDataFieldContract.ValidateWhereExpression((CustomDataFieldInfo)_customDataFieldInfo, txtQueryCondition.Text.Trim());
                    if (!success)
                    {
                        MessageBox.Show("查询条件不正确，请检查！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            UpdateTextHandler?.Invoke(txtQueryCondition.Text.Trim());
            this.Close();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认清除查询条件吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                txtQueryCondition.Text = string.Empty;
                ClearDataOnControls();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 增加操作符
        /// </summary>
        /// <param name="comboBoxEdit"></param>
        /// <param name="dbType"></param>
        private void AddItems(DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit, DbType dbType)
        {
            switch (dbType)
            {
                case DbType.Boolean:
                    comboBoxEdit.Properties.Items.Add(" = ");
                    comboBoxEdit.Properties.Items.Add(" != ");
                    break;

                case DbType.DateTime:
                case DbType.DateTime2:
                case DbType.Int32:
                case DbType.Decimal:
                    comboBoxEdit.Properties.Items.Add(" = ");
                    comboBoxEdit.Properties.Items.Add(" > ");
                    comboBoxEdit.Properties.Items.Add(" >= ");
                    comboBoxEdit.Properties.Items.Add(" < ");
                    comboBoxEdit.Properties.Items.Add(" <= ");
                    comboBoxEdit.Properties.Items.Add(" != ");
                    break;

                case DbType.String:
                    comboBoxEdit.Properties.Items.Add(" LIKE ");
                    comboBoxEdit.Properties.Items.Add(" = ");                    
                    comboBoxEdit.Properties.Items.Add(" != ");
                    break;

                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength:
                    comboBoxEdit.Properties.Items.Add(" = ");
                    comboBoxEdit.Properties.Items.Add(" != ");
                    break;
            }
            comboBoxEdit.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化条件属性
        /// </summary>
        /// <param name="dbType"></param>
        private void InitConditionProperty(DbType dbType)
        {
            switch (dbType)
            {
                case DbType.Boolean:
                    cmbeBoolean.Visible = true;
                    etxtStringCondition.Visible = false;
                    extDigitCondition.Visible = false;
                    ecmbCondition.Visible = false;
                    chkembCondition.Visible = false;
                    dateEditCondition.Visible = false;
                    timeEditCondition.Visible = false;
                    lookUpEditCondition.Visible = false;
                    break;

                case DbType.String:
                    cmbeBoolean.Visible = false;
                    etxtStringCondition.Visible = true;
                    extDigitCondition.Visible = false;
                    ecmbCondition.Visible = false;
                    chkembCondition.Visible = false;
                    dateEditCondition.Visible = false;
                    timeEditCondition.Visible = false;
                    lookUpEditCondition.Visible = false;
                    break;

                case DbType.Int32:
                case DbType.Decimal:
                    cmbeBoolean.Visible = false;
                    etxtStringCondition.Visible = false;
                    extDigitCondition.Visible = true;
                    ecmbCondition.Visible = false;
                    chkembCondition.Visible = false;
                    dateEditCondition.Visible = false;
                    timeEditCondition.Visible = false;
                    lookUpEditCondition.Visible = false;
                    break;

                case DbType.DateTime:
                    cmbeBoolean.Visible = false;
                    etxtStringCondition.Visible = false;
                    extDigitCondition.Visible = false;
                    ecmbCondition.Visible = false;
                    chkembCondition.Visible = false;
                    dateEditCondition.Visible = true;
                    timeEditCondition.Visible = false;
                    lookUpEditCondition.Visible = false;
                    break;

                case DbType.DateTime2:
                    cmbeBoolean.Visible = false;
                    etxtStringCondition.Visible = false;
                    extDigitCondition.Visible = false;
                    ecmbCondition.Visible = false;
                    chkembCondition.Visible = false;
                    dateEditCondition.Visible = false;
                    timeEditCondition.Visible = true;
                    lookUpEditCondition.Visible = false;
                    break;

                case DbType.AnsiStringFixedLength:/* 树形枚举结构 */
                    cmbeBoolean.Visible = false;
                    etxtStringCondition.Visible = false;
                    extDigitCondition.Visible = false;
                    ecmbCondition.Visible = true;
                    chkembCondition.Visible = false;
                    dateEditCondition.Visible = false;
                    timeEditCondition.Visible = false;
                    lookUpEditCondition.Visible = false;
                    break;

                case DbType.StringFixedLength:/* 下拉型枚举结构 */
                    cmbeBoolean.Visible = false;
                    etxtStringCondition.Visible = false;
                    extDigitCondition.Visible = false;
                    ecmbCondition.Visible = false;
                    chkembCondition.Visible = true;
                    dateEditCondition.Visible = false;
                    timeEditCondition.Visible = false;
                    lookUpEditCondition.Visible = false;
                    break;

                case DbType.Xml: /* 关联类型 */
                    cmbeBoolean.Visible = false;
                    etxtStringCondition.Visible = false;
                    extDigitCondition.Visible = false;
                    ecmbCondition.Visible = false;
                    chkembCondition.Visible = false;
                    dateEditCondition.Visible = false;
                    timeEditCondition.Visible = false;
                    lookUpEditCondition.Visible = true;
                    break;

                case DbType.Object: /* 附件类型 */
                    cmbeBoolean.Visible = false;
                    etxtStringCondition.Visible = true;
                    extDigitCondition.Visible = false;
                    ecmbCondition.Visible = false;
                    chkembCondition.Visible = false;
                    dateEditCondition.Visible = false;
                    timeEditCondition.Visible = false;
                    lookUpEditCondition.Visible = false;
                    break;

            }
        }

        /// <summary>
        /// 初始化属性
        /// </summary>
        /// <param name="customDataFieldInfo"></param>
        private void InitDropdownListProperty(CustomDataFieldInfo customDataFieldInfo)
        {
            DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
            byte dataFieldType = customDataFieldInfo.DataFieldType;
            switch (dataFieldProperty)
            {
                case DataFieldProperty.LogicalDataField:
                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)dataFieldType;
                    switch (logicalDataFieldType)
                    {
                        case LogicalDataFieldType.StringExpression:
                            AddItems(cmbeOperation, DbType.String);
                            InitConditionProperty(DbType.String);
                            break;

                        case LogicalDataFieldType.DigitExpression:
                            AddItems(cmbeOperation, DbType.Decimal);
                            InitConditionProperty(DbType.Decimal);
                            break;

                        case LogicalDataFieldType.DateTimeExpression:
                            AddItems(cmbeOperation, DbType.DateTime);
                            InitConditionProperty(DbType.DateTime);
                            dateEditCondition.Properties.DisplayFormat.FormatString = "d";
                            dateEditCondition.Properties.EditFormat.FormatString = "d";
                            dateEditCondition.Properties.EditMask = "d";
                            break;
                    }
                    break;

                case DataFieldProperty.PhysicalDataField:
                    CustomDataFieldInfo parentCustomDataFieldInfo = null;
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)dataFieldType;
                    if (customDataFieldInfo.ParentDataFieldId > 0)
                    {
                        parentCustomDataFieldInfo = customDataFieldContract.GetModelInfo(customDataFieldInfo.ParentDataFieldId);
                        physicalDataFieldType = (PhysicalDataFieldType)parentCustomDataFieldInfo.DataFieldType;
                    }
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.Boolean:
                            AddItems(cmbeOperation, DbType.Boolean);
                            InitConditionProperty(DbType.Boolean);
                            cmbeBoolean.Properties.Items.Add(new CommonItem<int>("空", -1));
                            cmbeBoolean.Properties.Items.Add(new CommonItem<int>("否", 0));
                            cmbeBoolean.Properties.Items.Add(new CommonItem<int>("是", 1));
                            break;

                        case PhysicalDataFieldType.Int32:
                        case PhysicalDataFieldType.Decimal:
                            AddItems(cmbeOperation, DbType.Int32);
                            InitConditionProperty(DbType.Int32);
                            break;
                                                   
                        case PhysicalDataFieldType.ArbitraryString:
                        case PhysicalDataFieldType.ExtendedArbitraryString:
                        case PhysicalDataFieldType.NumeralString:
                        case PhysicalDataFieldType.CharString:
                        case PhysicalDataFieldType.MixedString:
                            AddItems(cmbeOperation, DbType.String);
                            InitConditionProperty(DbType.String);
                            break;

                        case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                            AddItems(cmbeOperation, DbType.DateTime);
                            InitConditionProperty(DbType.DateTime);
                            dateEditCondition.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
                            dateEditCondition.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
                            dateEditCondition.Properties.DisplayFormat.FormatString = "G";
                            dateEditCondition.Properties.EditFormat.FormatString = "G";
                            dateEditCondition.Properties.EditMask = "G";
                            break;

                        case PhysicalDataFieldType.YearAndMonthAndDay:
                            AddItems(cmbeOperation, DbType.DateTime);
                            InitConditionProperty(DbType.DateTime);
                            dateEditCondition.Properties.DisplayFormat.FormatString = "d";
                            dateEditCondition.Properties.EditFormat.FormatString = "d";
                            dateEditCondition.Properties.EditMask = "d";
                            break;

                        case PhysicalDataFieldType.YearAndMonth:
                            AddItems(cmbeOperation, DbType.DateTime);
                            InitConditionProperty(DbType.DateTime);
                            dateEditCondition.Properties.DisplayFormat.FormatString = "y";
                            dateEditCondition.Properties.EditFormat.FormatString = "y";
                            dateEditCondition.Properties.EditMask = "y";
                            break;

                        case PhysicalDataFieldType.MonthAndDay:
                            AddItems(cmbeOperation, DbType.DateTime);
                            InitConditionProperty(DbType.DateTime);
                            dateEditCondition.Properties.DisplayFormat.FormatString = "m";
                            dateEditCondition.Properties.EditFormat.FormatString = "m";
                            dateEditCondition.Properties.EditMask = "m";
                            break;

                        case PhysicalDataFieldType.Time:
                            AddItems(cmbeOperation, DbType.DateTime2);
                            InitConditionProperty(DbType.DateTime2);
                            timeEditCondition.Properties.Mask.EditMask = "HH:mm:ss";
                            timeEditCondition.Properties.NullText = string.Empty;
                            break;

                        case PhysicalDataFieldType.PrimaryAssociation:
                        case PhysicalDataFieldType.SecondaryAssociation:
                            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
                            customAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
                            string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                            decimal associationId = decimal.MinValue;
                            decimal associatedDataFieldId = decimal.MinValue;
                            DataTable data = null;
                            if (physicalDataFieldType == PhysicalDataFieldType.PrimaryAssociation)
                            {
                                associatedDataFieldId = customDataFieldInfo.AssociatedDataFieldId;
                                associationId = associatedDataFieldContract.GetAssociationId(associatedDataFieldId);
                            }
                            else
                            {
                                associatedDataFieldId = parentCustomDataFieldInfo.AssociatedDataFieldId;
                                associationId = associatedDataFieldContract.GetAssociationId(associatedDataFieldId);
                            }
                            if (associationId > 0)
                            {
                                BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(associatedDataFieldId);
                                switch (basedDataType)
                                {
                                    case BasedDataType.Boolean:
                                        AddItems(cmbeOperation, DbType.Boolean);
                                        InitConditionProperty(DbType.Boolean);
                                        break;

                                    case BasedDataType.Decimal:
                                    case BasedDataType.Int32:
                                        AddItems(cmbeOperation, DbType.Int32);
                                        InitConditionProperty(DbType.Int32);
                                        break;

                                    case BasedDataType.String:
                                        AddItems(cmbeOperation, DbType.String);
                                        InitConditionProperty(DbType.String);
                                        break;

                                    case BasedDataType.DateTime:
                                        AddItems(cmbeOperation, DbType.DateTime);
                                        InitConditionProperty(DbType.DateTime);
                                        break;
                                }
                                data = customAssociationContract.GetAssociationData(associationId);
                                string physicalName = associatedDataFieldContract.GetPhysicalName(customDataFieldInfo.AssociatedDataFieldId);
                                foreach (DataColumn dataColumn in data.Columns)
                                {
                                    LookUpColumnInfo lookUpColumnInfo = new LookUpColumnInfo(dataColumn.ColumnName, dataColumn.Caption);
                                    if (dataColumn.DataType == typeof(DateTime))
                                    {
                                        lookUpColumnInfo.FormatType = FormatType.DateTime;
                                        lookUpColumnInfo.FormatString = "d";
                                    }
                                    lookUpEditCondition.Properties.Columns.Add(lookUpColumnInfo);
                                }
                                lookUpEditCondition.Properties.DataSource = data;
                                lookUpEditCondition.Properties.Columns[key].Visible = false;
                                lookUpEditCondition.Properties.DisplayMember = physicalName;
                                lookUpEditCondition.Properties.ValueMember = physicalName;
                            }
                            break;

                        case PhysicalDataFieldType.DropdownListEnum:
                        case PhysicalDataFieldType.MultiSelectedEnum:
                        case PhysicalDataFieldType.DepartmentDropdownListEnum:
                        case PhysicalDataFieldType.DropdownListEnumValue:
                        case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                        case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                            AddItems(cmbeOperation, DbType.StringFixedLength);
                            InitConditionProperty(DbType.StringFixedLength);
                            IList<CommonNode> enumCommonNodes = null;
                            if (physicalDataFieldType == PhysicalDataFieldType.DepartmentDropdownListEnum)
                            {
                                enumCommonNodes = departmentContract.GetChildNodes(decimal.One);
                            }
                            else
                            {
                                customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
                                if (parentCustomDataFieldInfo == null)
                                {
                                    enumCommonNodes = customEnumContract.GetChildNodes(customDataFieldInfo.EnumId);
                                }
                                else
                                {
                                    enumCommonNodes = customEnumContract.GetChildNodes(parentCustomDataFieldInfo.EnumId);
                                }
                            }
                            chkembCondition.Properties.Items.AddRange(enumCommonNodes.ToArray());
                            break;

                        case PhysicalDataFieldType.CscadeEnum:
                        case PhysicalDataFieldType.TreeViewEnum:
                        case PhysicalDataFieldType.TreeViewEnumValue:
                        case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                        case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                            AddItems(cmbeOperation, DbType.AnsiStringFixedLength);
                            InitConditionProperty(DbType.AnsiStringFixedLength);
                            ecmbCondition.CheckBoxes = true;
                            if (parentCustomDataFieldInfo == null)
                            {
                                ecmbCondition.TreeDropdownHandler = new TreeDropdownItems(customEnumContract, customDataFieldInfo.EnumId);
                            }
                            else
                            {
                                ecmbCondition.TreeDropdownHandler = new TreeDropdownItems(customEnumContract, parentCustomDataFieldInfo.EnumId);
                            }
                            ecmbCondition.InitalizeTreeView();
                            break;

                        case PhysicalDataFieldType.DepartmentTreeViewEnum:
                        case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                            AddItems(cmbeOperation, DbType.AnsiStringFixedLength);
                            InitConditionProperty(DbType.AnsiStringFixedLength);
                            ecmbCondition.CheckBoxes = true;
                            if (physicalDataFieldType == PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot)
                            {
                                ecmbCondition.TreeDropdownHandler = new TreeDropdownItems(departmentContract);
                            }
                            else
                            {
                                ecmbCondition.TreeDropdownHandler = new TreeDropdownItems(departmentContract, decimal.One);
                            }
                            ecmbCondition.InitalizeTreeView();
                            break;

                        case PhysicalDataFieldType.DocAttachment:
                        case PhysicalDataFieldType.PicAttachment:
                        case PhysicalDataFieldType.PDFAttachment:
                            AddItems(cmbeOperation, DbType.String);
                            InitConditionProperty(DbType.Object);
                            break;
                    }
                    break;

                case DataFieldProperty.SystemPhysicalDataField:
                    SystemDataField systemDataField = (SystemDataField)customDataFieldInfo.DataFieldId;                    
                    AuthorityCondition authorityCondition = new AuthorityCondition(departmentContract, userTypeContract);
                    switch (systemDataField)
                    {
                        case SystemDataField.UserName:
                        case SystemDataField.UserActualName:
                            AddItems(cmbeOperation, DbType.String);
                            InitConditionProperty(DbType.String);
                            break;

                        case SystemDataField.UserTypeName:
                        case SystemDataField.UserTypeCode:
                            if (systemDataField == SystemDataField.UserTypeCode)
                            {
                                AddItems(cmbeOperation, DbType.String);
                            }
                            else
                            {
                                AddItems(cmbeOperation, DbType.AnsiStringFixedLength);
                            }
                            InitConditionProperty(DbType.AnsiStringFixedLength);
                            ecmbCondition.CheckBoxes = true;
                            /* 1. 管理的用户类型 */
                            if (authorityCondition.RelatedUserTypeCommonNodes != null && authorityCondition.RelatedUserTypeCommonNodes.Count > 0)
                            {
                                ecmbCondition.TreeViewHandler.InitFirstLevelNodes(authorityCondition.RelatedUserTypeCommonNodes);
                            }
                            else
                            {
                                ecmbCondition.TreeDropdownHandler = new UserTypeTreeDropdownList(customGroupContract, userTypeContract);
                                ecmbCondition.InitalizeTreeView();
                            }
                            break;

                        case SystemDataField.DepName:
                        case SystemDataField.DepCode:
                        case SystemDataField.DepValue:
                        case SystemDataField.DepFstAdditionalCode:
                        case SystemDataField.DepScdAdditionalCode:
                            if (systemDataField == SystemDataField.DepName)
                            {
                                AddItems(cmbeOperation, DbType.AnsiStringFixedLength);
                            }
                            else
                            {
                                AddItems(cmbeOperation, DbType.String);
                            }                            
                            InitConditionProperty(DbType.AnsiStringFixedLength);
                            ecmbCondition.CheckBoxes = true;
                            if (authorityCondition.RelatedDepartmentCommonNodes != null && authorityCondition.RelatedDepartmentCommonNodes.Count > 0)
                            {
                                ecmbCondition.ShowSearch = false;
                                ecmbCondition.TreeViewHandler.InitFirstLevelNodes(authorityCondition.RelatedDepartmentCommonNodes);
                            }
                            else
                            {
                                ecmbCondition.ShowSearch = true;
                                ecmbCondition.TreeDropdownHandler = new TreeDropdownItems(departmentContract);
                                ecmbCondition.InitalizeTreeView();
                            }
                            break;
                            
                        case SystemDataField.DepProperty:
                            AddItems(cmbeOperation, DbType.StringFixedLength);
                            InitConditionProperty(DbType.StringFixedLength);
                            IList<EnumItem> enumItems = SystemConfigHelper.GetDepartmentPorperty();
                            chkembCondition.Properties.Items.AddRange(enumItems.ToArray());
                            break;

                        case SystemDataField.CreationTime:
                        case SystemDataField.ModificationTime:
                            AddItems(cmbeOperation, DbType.DateTime);
                            InitConditionProperty(DbType.DateTime);
                            dateEditCondition.Properties.VistaDisplayMode = DefaultBoolean.True;
                            dateEditCondition.Properties.VistaEditTime = DefaultBoolean.True;
                            dateEditCondition.Properties.DisplayFormat.FormatString = "G";
                            dateEditCondition.Properties.EditFormat.FormatString = "G";
                            dateEditCondition.Properties.EditMask = "G";
                            break;

                        case SystemDataField.CurrentState:
                            AddItems(cmbeOperation, DbType.StringFixedLength);
                            InitConditionProperty(DbType.StringFixedLength);
                            UserControlHelper.InitCheckedComboBoxEditItems(chkembCondition, typeof(CurrentState));
                            break;

                        case SystemDataField.AuditedStatus:
                            AddItems(cmbeOperation, DbType.StringFixedLength);
                            InitConditionProperty(DbType.StringFixedLength);
                            UserControlHelper.InitCheckedComboBoxEditItems(chkembCondition, typeof(AuditedStatus));
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 获得查询字段串
        /// </summary>
        /// <returns></returns>
        private IList<string> GetWhereCondition()
        {
            IList<string> conditions = new List<string>();

            if (cmbeBoolean.Visible)
            {
                if (cmbeBoolean.SelectedIndex > 0)
                {
                    CommonItem<int> item = cmbeBoolean.SelectedItem as CommonItem<int>;
                    conditions.Add(string.Format("'{0}'", item.Value));
                }
            }
            else if (etxtStringCondition.Visible)
            {
                if (!string.IsNullOrWhiteSpace(etxtStringCondition.Text.Trim()))
                {
                    if (cmbeOperation.Text.Trim().ToLower().Equals("like"))
                    {
                        conditions.Add(string.Format("'%{0}%'", etxtStringCondition.Text));
                    }
                    else
                    {
                        conditions.Add(string.Format("'{0}'", etxtStringCondition.Text));
                    }
                }
            }
            else if (extDigitCondition.Visible)
            {
                if (!string.IsNullOrWhiteSpace(extDigitCondition.Text.Trim()))
                {
                    conditions.Add(extDigitCondition.Text.Trim());
                }
            }
            else if (ecmbCondition.Visible)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)_customDataFieldInfo.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.PhysicalDataField:
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)_customDataFieldInfo.DataFieldType;
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.TreeViewEnum:
                            case PhysicalDataFieldType.CscadeEnum:
                            case PhysicalDataFieldType.DepartmentTreeViewEnum:
                            case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                                StringBuilder sb = new StringBuilder();
                                foreach (TreeNode tn in ecmbCondition.CheckedTreeNodes)
                                {
                                    conditions.Add(string.Format("'{0}'", tn.Text));
                                }
                                break;

                            case PhysicalDataFieldType.TreeViewEnumValue:
                            case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                            case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                                foreach (TreeNode tn in ecmbCondition.CheckedTreeNodes)
                                {
                                    CommonNode commonNode = tn.Tag as CommonNode;
                                    string value = DataConvertionHelper.GetString(customEnumContract.GetEnumData(commonNode.NodeId, physicalDataFieldType));
                                    conditions.Add(string.Format("'{0}'", value));
                                }
                                break;
                                
                            case PhysicalDataFieldType.EnumValue:
                            case PhysicalDataFieldType.EnumNameDependency:
                            case PhysicalDataFieldType.FstAdditionalCode:
                            case PhysicalDataFieldType.ScdAdditionalCode:
                            case PhysicalDataFieldType.FstAdditionalString:
                            case PhysicalDataFieldType.ScdAdditionalString:
                            case PhysicalDataFieldType.TrdAdditionalString:
                            case PhysicalDataFieldType.FourthAdditionalString:
                            case PhysicalDataFieldType.FifthAdditionalString:
                            case PhysicalDataFieldType.SixthAdditionalString:
                            case PhysicalDataFieldType.FstAdditionalInteger:
                            case PhysicalDataFieldType.ScdAdditionalInteger:
                            case PhysicalDataFieldType.FstAdditionalDecimal:
                            case PhysicalDataFieldType.ScdAdditionalDecimal:
                                foreach (TreeNode tn in ecmbCondition.CheckedTreeNodes)
                                {
                                    CommonNode commonNode = tn.Tag as CommonNode;
                                    if (physicalDataFieldType == PhysicalDataFieldType.EnumNameDependency)
                                    {
                                        conditions.Add(string.Format("'{0}'", commonNode.NodeName));
                                    }
                                    else
                                    {
                                        CustomEnumInfo customEnumInfo = customEnumContract.GetModelInfo(commonNode.NodeId);
                                        switch (physicalDataFieldType)
                                        {
                                            case PhysicalDataFieldType.EnumValue:
                                                conditions.Add(string.Format("'{0}'", customEnumInfo.EnumValue));
                                                break;

                                            case PhysicalDataFieldType.FstAdditionalCode:
                                                conditions.Add(string.Format("'{0}'", customEnumInfo.FirstCode));
                                                break;

                                            case PhysicalDataFieldType.ScdAdditionalCode:
                                                conditions.Add(string.Format("'{0}'", customEnumInfo.SecondCode));
                                                break;

                                            case PhysicalDataFieldType.FstAdditionalString:
                                                conditions.Add(string.Format("'{0}'", customEnumInfo.FstAdditionalString));
                                                break;

                                            case PhysicalDataFieldType.ScdAdditionalString:
                                                conditions.Add(string.Format("'{0}'", customEnumInfo.ScdAdditionalString));
                                                break;

                                            case PhysicalDataFieldType.TrdAdditionalString:
                                                conditions.Add(string.Format("'{0}'", customEnumInfo.TrdAdditionalString));
                                                break;

                                            case PhysicalDataFieldType.FourthAdditionalString:
                                                conditions.Add(string.Format("'{0}'", customEnumInfo.FourthAdditionalString));
                                                break;

                                            case PhysicalDataFieldType.FifthAdditionalString:
                                                conditions.Add(string.Format("'{0}'", customEnumInfo.FifthAdditionalString));
                                                break;

                                            case PhysicalDataFieldType.SixthAdditionalString:
                                                conditions.Add(string.Format("'{0}'", customEnumInfo.SixthAdditionalString));
                                                break;

                                            case PhysicalDataFieldType.FstAdditionalInteger:
                                                conditions.Add(string.Format("{0}", customEnumInfo.FstAdditionalInteger));
                                                break;

                                            case PhysicalDataFieldType.ScdAdditionalInteger:
                                                conditions.Add(string.Format("{0}", customEnumInfo.ScdAdditionalInteger));
                                                break;

                                            case PhysicalDataFieldType.FstAdditionalDecimal:
                                                conditions.Add(string.Format("{0}", customEnumInfo.FstAdditionalDecimal));
                                                break;

                                            case PhysicalDataFieldType.ScdAdditionalDecimal:
                                                conditions.Add(string.Format("{0}", customEnumInfo.ScdAdditionalDecimal));
                                                break;
                                        }
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.DepartmentValue:
                            case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                            case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                                foreach (TreeNode tn in ecmbCondition.CheckedTreeNodes)
                                {
                                    CommonNode commonNode = tn.Tag as CommonNode;
                                    CustomDepartmentInfo customDepartmentInfo = departmentContract.GetModelInfo(commonNode.NodeId);
                                    if (physicalDataFieldType == PhysicalDataFieldType.DepartmentValue)
                                    {
                                        conditions.Add(string.Format("'{0}'", customDepartmentInfo.DepValue));
                                    }
                                    else if (physicalDataFieldType == PhysicalDataFieldType.DepartmentFstAdditionalCode)
                                    {
                                        conditions.Add(string.Format("'{0}'", customDepartmentInfo.FirstCode));
                                    }
                                    else
                                    {
                                        conditions.Add(string.Format("'{0}'", customDepartmentInfo.SecondCode));
                                    }
                                }
                                break;

                            default:
                                throw new Exception("错误路径");
                        }
                        break;

                    case DataFieldProperty.SystemPhysicalDataField:
                        SystemDataField systemDataField = (SystemDataField)_customDataFieldInfo.DataFieldId;
                        switch (systemDataField)
                        {
                            case SystemDataField.DepName:
                            case SystemDataField.UserTypeName:
                                foreach (TreeNode tn in ecmbCondition.CheckedTreeNodes)
                                {
                                    CommonNode commonNode = tn.Tag as CommonNode;
                                    conditions.Add(commonNode.NodeId.ToString());
                                }
                                break;

                            case SystemDataField.DepCode:
                            case SystemDataField.UserTypeCode:
                                foreach (TreeNode tn in ecmbCondition.CheckedTreeNodes)
                                {
                                    CommonNode commonNode = tn.Tag as CommonNode;
                                    conditions.Add(string.Format("'{0}%'", commonNode.NodeCode));
                                }
                                break;

                            case SystemDataField.DepValue:
                            case SystemDataField.DepFstAdditionalCode:
                            case SystemDataField.DepScdAdditionalCode:
                                foreach (TreeNode tn in ecmbCondition.CheckedTreeNodes)
                                {
                                    CommonNode commonNode = tn.Tag as CommonNode;
                                    CustomDepartmentInfo customDepartmentInfo = departmentContract.GetModelInfo(commonNode.NodeId);
                                    if (systemDataField == SystemDataField.DepValue)
                                    {
                                        conditions.Add(string.Format("'{0}%'", customDepartmentInfo.DepValue));
                                    }
                                    else if (systemDataField == SystemDataField.DepFstAdditionalCode)
                                    {
                                        conditions.Add(string.Format("'{0}%'", customDepartmentInfo.FirstCode));
                                    }
                                    else
                                    {
                                        conditions.Add(string.Format("'{0}%'", customDepartmentInfo.SecondCode));
                                    }
                                }
                                break;

                            case SystemDataField.DepProperty:
                                foreach (TreeNode tn in ecmbCondition.CheckedTreeNodes)
                                {
                                    CommonNode commonNode = tn.Tag as CommonNode;
                                    conditions.Add(string.Format("{0}'", commonNode.NodeId));
                                }
                                break;
                        }
                        break;
                }
            }
            else if (chkembCondition.Visible)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)_customDataFieldInfo.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.PhysicalDataField:
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)_customDataFieldInfo.DataFieldType;
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.DropdownListEnum:
                            case PhysicalDataFieldType.DepartmentDropdownListEnum:
                                foreach (CheckedListBoxItem checkedListBoxItem in chkembCondition.Properties.Items)
                                {
                                    if (checkedListBoxItem.CheckState == CheckState.Checked)
                                    {
                                        conditions.Add(string.Format("'{0}'", checkedListBoxItem.ToString()));
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.DropdownListEnumValue:
                            case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                            case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                                foreach (CheckedListBoxItem checkedListBoxItem in chkembCondition.Properties.Items)
                                {
                                    if (checkedListBoxItem.CheckState == CheckState.Checked)
                                    {
                                        CommonNode commonNode = checkedListBoxItem.Value as CommonNode;
                                        string value = DataConvertionHelper.GetString(customEnumContract.GetEnumData(commonNode.NodeId, physicalDataFieldType));
                                        conditions.Add(string.Format("'{0}'", value));
                                    }
                                }
                                break;
                                
                            case PhysicalDataFieldType.DepartmentValue:
                                foreach (CheckedListBoxItem checkedListBoxItem in chkembCondition.Properties.Items)
                                {
                                    if (checkedListBoxItem.CheckState == CheckState.Checked)
                                    {
                                        CommonNode commonNode = checkedListBoxItem.Value as CommonNode;
                                        conditions.Add(string.Format("'{0}'", commonNode.NodeCode));
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                            case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                                foreach (CheckedListBoxItem checkedListBoxItem in chkembCondition.Properties.Items)
                                {
                                    if (checkedListBoxItem.CheckState == CheckState.Checked)
                                    {
                                        CommonNode commonNode = checkedListBoxItem.Value as CommonNode;
                                        CustomDepartmentInfo customDepartmentInfo = departmentContract.GetModelInfo(commonNode.NodeId);
                                        if (physicalDataFieldType == PhysicalDataFieldType.DepartmentFstAdditionalCode)
                                        {
                                            conditions.Add(string.Format("'{0}%'", customDepartmentInfo.FirstCode));
                                        }
                                        else
                                        {
                                            conditions.Add(string.Format("'{0}%'", customDepartmentInfo.SecondCode));
                                        }
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.MultiSelectedEnum:
                                foreach (CheckedListBoxItem checkedListBoxItem in chkembCondition.Properties.Items)
                                {
                                    if (checkedListBoxItem.CheckState == CheckState.Checked)
                                    {
                                        conditions.Add(string.Format("'%{0}%'", checkedListBoxItem.ToString()));
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.EnumValue:
                            case PhysicalDataFieldType.EnumNameDependency:
                            case PhysicalDataFieldType.FstAdditionalCode:
                            case PhysicalDataFieldType.ScdAdditionalCode:
                            case PhysicalDataFieldType.FstAdditionalString:
                            case PhysicalDataFieldType.ScdAdditionalString:
                            case PhysicalDataFieldType.TrdAdditionalString:
                            case PhysicalDataFieldType.FourthAdditionalString:
                            case PhysicalDataFieldType.FifthAdditionalString:
                            case PhysicalDataFieldType.SixthAdditionalString:
                            case PhysicalDataFieldType.FstAdditionalInteger:
                            case PhysicalDataFieldType.ScdAdditionalInteger:
                            case PhysicalDataFieldType.FstAdditionalDecimal:
                            case PhysicalDataFieldType.ScdAdditionalDecimal:
                                foreach (CheckedListBoxItem checkedListBoxItem in chkembCondition.Properties.Items)
                                {
                                    if (checkedListBoxItem.CheckState == CheckState.Checked)
                                    {
                                        CommonNode commonNode = checkedListBoxItem.Value as CommonNode;
                                        if (physicalDataFieldType == PhysicalDataFieldType.EnumNameDependency)
                                        {
                                            conditions.Add(string.Format("'{0}'", commonNode.NodeName));
                                        }
                                        else
                                        {
                                            CustomEnumInfo customEnumInfo = customEnumContract.GetModelInfo(commonNode.NodeId);
                                            switch (physicalDataFieldType)
                                            {
                                                case PhysicalDataFieldType.EnumValue:
                                                    conditions.Add(string.Format("'{0}'", customEnumInfo.EnumCode));
                                                    break;

                                                case PhysicalDataFieldType.FstAdditionalCode:
                                                    conditions.Add(string.Format("'{0}'", customEnumInfo.EnumCode));
                                                    break;

                                                case PhysicalDataFieldType.ScdAdditionalCode:
                                                    conditions.Add(string.Format("'{0}'", customEnumInfo.EnumCode));
                                                    break;

                                                case PhysicalDataFieldType.FstAdditionalString:
                                                    conditions.Add(string.Format("'{0}'", customEnumInfo.FstAdditionalString));
                                                    break;

                                                case PhysicalDataFieldType.ScdAdditionalString:
                                                    conditions.Add(string.Format("'{0}'", customEnumInfo.ScdAdditionalString));
                                                    break;

                                                case PhysicalDataFieldType.TrdAdditionalString:
                                                    conditions.Add(string.Format("'{0}'", customEnumInfo.TrdAdditionalString));
                                                    break;

                                                case PhysicalDataFieldType.FourthAdditionalString:
                                                    conditions.Add(string.Format("'{0}'", customEnumInfo.FourthAdditionalString));
                                                    break;

                                                case PhysicalDataFieldType.FifthAdditionalString:
                                                    conditions.Add(string.Format("'{0}'", customEnumInfo.FifthAdditionalString));
                                                    break;

                                                case PhysicalDataFieldType.SixthAdditionalString:
                                                    conditions.Add(string.Format("'{0}'", customEnumInfo.SixthAdditionalString));
                                                    break;

                                                case PhysicalDataFieldType.FstAdditionalInteger:
                                                    conditions.Add(string.Format("{0}", customEnumInfo.FstAdditionalInteger));
                                                    break;

                                                case PhysicalDataFieldType.ScdAdditionalInteger:
                                                    conditions.Add(string.Format("{0}", customEnumInfo.ScdAdditionalInteger));
                                                    break;

                                                case PhysicalDataFieldType.FstAdditionalDecimal:
                                                    conditions.Add(string.Format("{0}", customEnumInfo.FstAdditionalDecimal));
                                                    break;

                                                case PhysicalDataFieldType.ScdAdditionalDecimal:
                                                    conditions.Add(string.Format("{0}", customEnumInfo.ScdAdditionalDecimal));
                                                    break;
                                            }
                                        }
                                    }
                                }
                                break;

                            default:
                                throw new Exception("错误路径");
                        }
                        break;

                    case DataFieldProperty.SystemPhysicalDataField:
                        SystemDataField systemDataField = (SystemDataField)_customDataFieldInfo.DataFieldId;
                        if (systemDataField == SystemDataField.AuditedStatus || systemDataField == SystemDataField.CurrentState
                            || systemDataField == SystemDataField.DepProperty || systemDataField == SystemDataField.DepFstAdditionalCode
                            || systemDataField == SystemDataField.DepScdAdditionalCode || systemDataField == SystemDataField.UserTypeCode)
                        {
                            foreach (CheckedListBoxItem checkedListBoxItem in chkembCondition.Properties.Items)
                            {
                                if (checkedListBoxItem.CheckState == CheckState.Checked)
                                {
                                    EnumItem enumItem = checkedListBoxItem.Value as EnumItem;
                                    conditions.Add(enumItem.Value.ToString());
                                }
                            }
                        }
                        break;
                }
            }
            else if (dateEditCondition.Visible)
            {
                if (!DataConvertionHelper.IsNullValue(dateEditCondition.DateTime))
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)_customDataFieldInfo.DataFieldProperty;
                    switch (dataFieldProperty)
                    {
                        case DataFieldProperty.PhysicalDataField:
                            PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)_customDataFieldInfo.DataFieldType;
                            switch (physicalDataFieldType)
                            {
                                case PhysicalDataFieldType.YearAndMonth:
                                    conditions.Add(string.Format("'{0}-{1}-01'", dateEditCondition.DateTime.Year, dateEditCondition.DateTime.Month));
                                    break;

                                case PhysicalDataFieldType.YearAndMonthAndDay:
                                    conditions.Add(string.Format("'{0}'", dateEditCondition.DateTime.ToShortDateString()));
                                    break;

                                case PhysicalDataFieldType.MonthAndDay:
                                    conditions.Add(string.Format("'{0}-{1}-{2}'", AppSettingHelper.Year, dateEditCondition.DateTime.Month, dateEditCondition.DateTime.Day));
                                    break;

                                default:
                                    conditions.Add(string.Format("'{0}'", dateEditCondition.DateTime));
                                    break;
                            }
                            break;

                        case DataFieldProperty.LogicalDataField:
                            LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)_customDataFieldInfo.DataFieldType;
                            if (logicalDataFieldType == LogicalDataFieldType.DateTimeExpression)
                            {
                                conditions.Add(string.Format("'{0}'", dateEditCondition.DateTime.ToShortDateString()));
                            }
                            break;

                        default:
                            conditions.Add(string.Format("'{0}'", dateEditCondition.DateTime));
                            break;
                    }
                }
            }
            else if (timeEditCondition.Visible)
            {
                if (!string.IsNullOrWhiteSpace(timeEditCondition.Text))
                {
                    conditions.Add(string.Format("'{0} {1}'", AppSettingHelper.YearMonthDay, timeEditCondition.Text));
                }
            }
            else if (lookUpEditCondition.Visible)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)_customDataFieldInfo.DataFieldProperty;
                if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                {
                    if (!string.IsNullOrWhiteSpace(lookUpEditCondition.Text))
                    {
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)_customDataFieldInfo.DataFieldType;
                        decimal associatedDataFieldId = decimal.MinValue;
                        if (physicalDataFieldType == PhysicalDataFieldType.PrimaryAssociation)
                        {
                            associatedDataFieldId = _customDataFieldInfo.AssociatedDataFieldId;
                        }
                        else
                        {
                            CustomDataFieldInfo parentCustomDataFieldInfo = customDataFieldContract.GetModelInfo(_customDataFieldInfo.ParentDataFieldId);
                            associatedDataFieldId = parentCustomDataFieldInfo.AssociatedDataFieldId;
                        }
                        if (associatedDataFieldId > 0)
                        {
                            BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(associatedDataFieldId);
                            switch (basedDataType)
                            {
                                case BasedDataType.Boolean:
                                case BasedDataType.String:
                                case BasedDataType.DateTime:
                                    conditions.Add(string.Format("'{0}'", lookUpEditCondition.Text));
                                    break;

                                case BasedDataType.Decimal:
                                case BasedDataType.Int32:
                                    conditions.Add(lookUpEditCondition.Text);
                                    break;
                            }

                        }
                    }
                }
            }

            return conditions;
        }
        
        /// <summary>
        /// 清空控件内的输入数据
        /// </summary>
        private void ClearDataOnControls()
        {
            chkLeftBracket.Checked = false;
            chkRightBracket.Checked = false;
            cmbeCombination.SelectedIndex = -1;
            cmbeOperation.SelectedIndex = 0;
            etxtStringCondition.Text = string.Empty;
            extDigitCondition.Text = string.Empty;
            ecmbCondition.SelectedNode = null;
            chkembCondition.EditValue = null;
            dateEditCondition.EditValue = null;
            timeEditCondition.EditValue = null;
            lookUpEditCondition.EditValue = null;
        }

        #endregion
    }
}
