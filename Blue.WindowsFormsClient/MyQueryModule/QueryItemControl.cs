using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;
using AppFramework.WinFormsControls;
using AppFramework.Reference.DataFieldLibrary;
using Blue.WindowsFormsClient.Common;
using DevExpress.XtraGrid.Columns;

namespace Blue.WindowsFormsClient.MyQueryModule
{
    public partial class QueryItemControl : UserControl
    {
        #region 私有变量

        private CustomQueyInfo customQueyInfo = null;
        private IList<CommonNode> relatedUserTypeCommonNodes = null;
        private IList<CommonNode> relatedDepartmentCommonNodes = null;
        private IList<CommonNode> departmentPropertyCommonNodes = null;
        private bool isLoading = true;

        #endregion

        #region 私有常量

        /// <summary>
        /// 控件高度
        /// </summary>
        private const int COMMON_HEIGHT_OF_CONTROL = 20;

        /// <summary>
        /// Group 控件的标题行高度
        /// </summary>
        private const int HEIGHT_OF_GROUP_HEADER = 20;

        /// <summary>
        /// 每行之间的间隙距离
        /// </summary>
        private const int COMMON_HEIGHT_OF_SPACE = 10;

        /// <summary>
        /// 每行的控件之间的间隙距离
        /// </summary>
        private const int COMMON_WIDTH_OF_SPACE = 8;

        /// <summary>
        ///控件边沿的距离
        /// </summary>
        private const int COMMON_WIDTH_OF_MARGIN_SPACE = 3;

        /// <summary>
        /// 标签的宽度
        /// </summary>
        private const int COMMON_WIDTH_OF_LABEL_TEXT = 105;

        /// <summary>
        /// 控件的宽度
        /// </summary>
        private const int COMMON_WIDTH_OF_CONTROL = 190;

        /// <summary>
        /// 控件的宽度
        /// </summary>
        private const int COMMON_WIDTH_OF_DATE_CONTROL = 205;

        /// <summary>
        /// 控件的宽度
        /// </summary>
        private const int COMMON_WIDTH_OF_COMBOBOX_CONTROL = 70;

        /// <summary>
        /// 控件的宽度
        /// </summary>
        private const int COMMON_WIDTH_OF_DIGIT_CONTROL = 82;

        /// <summary>
        /// 控件的宽度
        /// </summary>
        private const int COMMON_WIDTH_OF_TEXT_CONTROL = 120;

        #endregion

        #region 属性

        /// <summary>
        /// 业务对象
        /// </summary>
        public ExtendedCustomBusinessInfo ExtendedCustomBusinessInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 自定义表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            get;
            set;
        }

        /// <summary>
        /// 自定义视图契约
        /// </summary>
        public ICustomViewContract CustomViewContract
        {
            get;
            set;
        }

        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 枚举类型契约
        /// </summary>
        public ICustomEnumContract CustomEnumContract
        {
            set; get;
        }

        /// <summary>
        /// 查询契约
        /// </summary>
        public ICustomQueyContract CustomQueyContract
        {
            get; set;
        }

        /// <summary>
        /// 用户类型契约
        /// </summary>
        public IUserTypeContract UserTypeContract
        {
            get; set;
        }

        /// <summary>
        /// 单位契约
        /// </summary>
        public ICustomDepartmentContract CustomDepartmentContract
        {
            set; get;
        }

        /// <summary>
        /// 用户契约
        /// </summary>
        public IUserAccountContract UserAccountContract
        {
            set; get;
        }

        /// <summary>
        /// 关联字段契约
        /// </summary>
        public IAssociatedDataFieldContract AssociatedDataFieldContract
        {
            set; get;
        }

        /// <summary>
        /// 字段契约
        /// </summary>
        public ICustomDataFieldContract CustomDataFieldContract
        {
            set; get;
        }

        /// <summary>
        /// 每行的控件个数
        /// </summary>
        public int RowCount
        {
            set; get;
        }

        /// <summary>
        /// 返回主界面
        /// </summary>
        public GoBackDelegate GoBack
        {
            get;
            set;
        }

        /// <summary>
        /// 返回按钮可见
        /// </summary>
        public bool BackVisible
        {
            get
            {
                return hlnkBack.Visible;
            }
            set
            {
                hlnkBack.Visible = value;
            }
        }

        /// <summary>
        /// 最大化按钮可见
        /// </summary>
        public bool MaxmizeVisible
        {
            get
            {
                return hlnkMaxmize.Visible;
            }
            set
            {
                hlnkMaxmize.Visible = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryItemControl()
        {
            InitializeComponent();
            RowCount = 2;
        }

        #endregion

        #region 窗体和控件加载方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryItemControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 翻页操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            devExpressGrid.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 排序字段设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beDataFieldSorting_Click(object sender, EventArgs e)
        {
            SortingConditionForm frnSortingCondition = new SortingConditionForm();
            frnSortingCondition.CustomQueyContract = CustomQueyContract;
            frnSortingCondition.CustomTableContract = CustomTableContract;
            frnSortingCondition.CustomViewContract = CustomViewContract;
            frnSortingCondition.DataQueriedId = customQueyInfo.DataQueriedId;
            frnSortingCondition.SystemDataFieldCondition = customQueyInfo.SystemCondition;
            frnSortingCondition.GetSortingCondtions = (sortingCondtions, description) =>
            {
                beDataFieldSorting.Tag = sortingCondtions;
                beDataFieldSorting.Text = description;
            };
            frnSortingCondition.ShowDialog();
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 清除按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkClean_Click(object sender, EventArgs e)
        {
            ClearDataOnControls();
        }
        
        /// <summary>
        /// 最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkMaxmize_Click(object sender, EventArgs e)
        {
            QueryItemControl queryItemControl = new QueryItemControl()
            {
                Tag = ExtendedCustomBusinessInfo,
                ExtendedCustomBusinessInfo = ExtendedCustomBusinessInfo,
                CustomGroupContract = CustomGroupContract,
                UserAccountContract = UserAccountContract,
                CustomQueyContract = CustomQueyContract,
                UserTypeContract = UserTypeContract,
                CustomEnumContract = CustomEnumContract,
                CustomDepartmentContract = CustomDepartmentContract,
                AssociatedDataFieldContract = AssociatedDataFieldContract,
                CustomTableContract = CustomTableContract,
                CustomViewContract = CustomViewContract,
                CustomDataFieldContract = CustomDataFieldContract,
                RowCount = 5,
                BackVisible = false,
                MaxmizeVisible = false,
                Dock = DockStyle.Fill
            };
            QueryItemForm frmQueryItem = new QueryItemForm();
            frmQueryItem.Controls.Add(queryItemControl);
            queryItemControl.LoadDataFields();
            frmQueryItem.ShowDialog();
        }

        /// <summary>
        /// 回到主窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkBack_Click(object sender, EventArgs e)
        {
            GoBack?.Invoke();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            int totalCount = 0;
            string warning = string.Empty;
            string tableName = string.Empty;
            DataQueriedType tableType = (DataQueriedType)customQueyInfo.DataQueriedType;

            switch (tableType)
            {
                case DataQueriedType.Custom:
                    tableName = customQueyInfo.CustomViewName;
                    break;

                case DataQueriedType.Table:
                    tableName = CustomTableContract.GetTablePhysicalName(customQueyInfo.TableId);
                    break;

                case DataQueriedType.View:
                    tableName = CustomViewContract.GetViewPhysicalName(customQueyInfo.ViewId);
                    break;
            }
            bool hasUserAccount = false;
            bool hasDepartmentProperty = false;
            IList<WhereConditon> whereConditons = GetWhereConditons(tableName, ref hasUserAccount, ref hasDepartmentProperty, ref warning);
            if (whereConditons == null)
            {
                MessageBox.Show(warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<SortingCondtion> sortingCondtions = beDataFieldSorting.Tag as List<SortingCondtion>;
            Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
            Dictionary<string, CommonDataFieldInfo> systemDataFieldNameRelations = DataFieldHelper.GetSystemDataFieldInfo(customQueyInfo.SystemDataFields);
            foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in systemDataFieldNameRelations)
            {
                dataFieldNameRelations.Add(keyValue.Key, keyValue.Value);
            }
            IList<CustomDataFieldInfo> customDataFieldInfos = CustomQueyContract.GetCustomDataFieldInfos(customQueyInfo.DataQueriedId);
            foreach (CustomDataFieldInfo customDataFieldInfo in customDataFieldInfos)
            {
                AddCommonDataFieldInfo(dataFieldNameRelations, customDataFieldInfo);
            }
            DataTable dt = null;
            switch (tableType)
            {
                case DataQueriedType.Custom:
                    devExpressGrid.DataKeyNames = new string[] { DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId) };
                    dt = CustomQueyContract.GetQueriedData(customQueyInfo.DataQueriedId, devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex, devExpressGrid.PageSize, whereConditons, sortingCondtions, ref totalCount);
                    break;

                case DataQueriedType.Table:
                    devExpressGrid.DataKeyNames = new string[] { DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId) };
                    dt = CustomTableContract.GetTableData(customQueyInfo.TableId, customQueyInfo.SystemDataFields, hasUserAccount, hasDepartmentProperty, dataFieldNameRelations,
                                       devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex, devExpressGrid.PageSize, whereConditons, sortingCondtions, ref totalCount);
                    break;

                case DataQueriedType.View:
                    IList<CommonNode> commonNodes = CustomViewContract.GetTablesByViewId(customQueyInfo.ViewId);
                    IList<string> keyNames = new List<string>();
                    string tablePhysicalName = CustomViewContract.GetTablePhysicalName(customQueyInfo.ViewId);
                    DataTableType dataTableType = CustomViewContract.GetMainTableType(customQueyInfo.ViewId);
                    //if (dataTableType == DataTableType.PrimaryBusiness || dataTableType == DataTableType.AssistantBusiness)
                    //{
                    //    keyNames.Add(string.Format("{0}_{1}", tablePhysicalName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId)));
                    //}
                    //keyNames.Add(string.Format("{0}_{1}", tablePhysicalName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId)));
                    //foreach (CommonNode commonNode in commonNodes)
                    //{
                    //    keyNames.Add(string.Format("{0}_{1}", commonNode.NodeCode, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId)));
                    //    dataTableType = CustomTableContract.GetDataTableType(commonNode.ParentNodeId);
                    //    if (dataTableType == DataTableType.PrimaryBusiness || dataTableType == DataTableType.AssistantBusiness)
                    //    {
                    //        keyNames.Add(string.Format("{0}_{1}", commonNode.NodeCode, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId)));
                    //    }
                    //}
                    devExpressGrid.DataKeyNames = keyNames.ToArray();
                    dt = CustomViewContract.GetViewData(customQueyInfo.ViewId, customQueyInfo.SystemDataFields, hasUserAccount, hasDepartmentProperty, dataFieldNameRelations,
                                       devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex, devExpressGrid.PageSize, whereConditons, sortingCondtions, ref totalCount);
                    break;
            }
            devExpressGrid.DataSource = dt;
            devExpressGrid.RecordCount = totalCount;
            bool currentState = AuthorityHelper.CheckAuthority(customQueyInfo.SystemDataFields, (byte)SystemDataField.CurrentState);
            if (currentState)
            {
                IList<EnumItem> currentStates = UserEnumHelper.GetEnumItems(typeof(CurrentState));
                devExpressGrid.Columns["CurrentState"].ColumnEdit = UserControlHelper.GetImageComboBoxOnColumnEdit(currentStates, icCurrentState);
            }
            bool auditedStatus = AuthorityHelper.CheckAuthority(customQueyInfo.SystemDataFields, (byte)SystemDataField.AuditedStatus);
            if (auditedStatus)
            {
                IList<EnumItem> auditedItems = UserEnumHelper.GetEnumItems(typeof(AuditedStatus));
                devExpressGrid.Columns["AuditedStatus"].ColumnEdit = UserControlHelper.GetImageComboBoxOnColumnEdit(auditedItems, icAuditedStatus);
            }
            foreach (GridColumn gridColumn in devExpressGrid.Columns)
            {
                if (!gridColumn.Visible)
                {
                    continue;
                }
                if (dataFieldNameRelations.ContainsKey(gridColumn.FieldName))
                {
                    SetColumnDisplayText(gridColumn, dataFieldNameRelations[gridColumn.FieldName]);
                }
            }
        }
        
        /// <summary>
        /// 清除查询条件
        /// </summary>
        private void ClearDataOnControls()
        {
            foreach (Control control in gcCondition.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }

                if (control is TreeDropdownList)
                {
                    TreeDropdownList treeDropdownList = (TreeDropdownList)control;
                    treeDropdownList.RemoveSelectedNodes();
                }
                else if (control is TreeMultipleList)
                {
                    TreeMultipleList treeMultipleList = (TreeMultipleList)control;
                    treeMultipleList.RemoveSelectedNodes();
                }
                else if (control is ComboBoxEdit)
                {
                    ComboBoxEdit comboBoxEdit = (ComboBoxEdit)control;
                    comboBoxEdit.SelectedIndex = 0;
                }
                else if (control is CheckedComboBoxEdit)
                {
                    CheckedComboBoxEdit chkEdit = (CheckedComboBoxEdit)control;
                    if (chkEdit.EditValue != null)
                    {
                        foreach (CheckedListBoxItem checkedListBoxItem in chkEdit.Properties.Items)
                        {
                            checkedListBoxItem.CheckState = CheckState.Unchecked;
                        }
                    }
                }
                else if (control is CheckEdit)
                {
                    ((CheckEdit)control).Checked = false;
                }
                else if (control is TextEdit)
                {
                    ((TextEdit)control).Text = string.Empty;
                }
                else if (control is DateEdit)
                {
                    DateEdit dateEdit = (DateEdit)control;
                    dateEdit.EditValue = null;
                }
                else if (control is TimeEdit)
                {
                    TimeEdit timeEdit = (TimeEdit)control;
                    timeEdit.EditValue = null;
                }
            }
            beDataFieldSorting.Tag = null;
            beDataFieldSorting.Text = string.Empty;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbStaticDataField);
        }

        /// <summary>
        /// 保存字段信息
        /// </summary>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="extendedCustomDataFieldInfo"></param>
        /// <returns></returns>
        private bool AddCommonDataFieldInfo(Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, CustomDataFieldInfo customDataFieldInfo)
        {
            bool save = true;

            DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
            string expressionText = string.Empty;
            if (dataFieldProperty == DataFieldProperty.LogicalDataField)
            {
                LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)customDataFieldInfo.DataFieldType;
                if (logicalDataFieldType == LogicalDataFieldType.Empty || logicalDataFieldType == LogicalDataFieldType.OneDimCode
                    || logicalDataFieldType == LogicalDataFieldType.TwoDimCode || logicalDataFieldType == LogicalDataFieldType.UserName)
                {
                    save = false;
                }
                else
                {
                    expressionText = CustomDataFieldContract.GetDataFieldLogicalExpression(customDataFieldInfo.DataFieldId);
                }
            }
            if (save)
            {
                dataFieldNameRelations.Add(customDataFieldInfo.PhysicalName,
                    new CommonDataFieldInfo(customDataFieldInfo.DataFieldId, customDataFieldInfo.TableId, customDataFieldInfo.PhysicalName, customDataFieldInfo.LogicalName,
                    expressionText, dataFieldProperty, customDataFieldInfo.DataFieldType));
            }

            return save;
        }

        /// <summary>
        /// 增加 where 条件
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="commonNodes"></param>
        /// <param name="commonNodeProperty"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFieldName"></param>
        /// <param name="paramDataFieldName"></param>
        /// <param name="dbType"></param>
        private void AddWhereCondition(IList<WhereConditon> whereConditons, IList<CommonNode> commonNodes, CommonNodeProperty commonNodeProperty, string tableName, string dataFieldName, string paramDataFieldName, DbType dbType)
        {
            for (int i = 0; i < commonNodes.Count; i++)
            {
                object value = null;
                switch (commonNodeProperty)
                {
                    case CommonNodeProperty.Id:
                        value = commonNodes[i].NodeId;
                        break;

                    case CommonNodeProperty.Name:
                        value = commonNodes[i].NodeName;
                        break;

                    case CommonNodeProperty.Value:
                        value = commonNodes[i].NodeCode;
                        break;
                }
                if (i == 0)
                {
                    if (commonNodes.Count == 1)
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}_{1}", paramDataFieldName, i), dbType,
                            value, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    }
                    else
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}_{1}", paramDataFieldName, i), dbType,
                            value, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    }
                }
                else if (i == commonNodes.Count - 1)
                {

                    whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}_{1}", paramDataFieldName, i), dbType,
                        value, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                }
                else
                {
                    whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}_{1}", paramDataFieldName, i), dbType,
                        value, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                }
            }
        }

        /// <summary>
        /// 获得查询条件
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="hasUserAccount"></param>
        /// <param name="warning"></param>
        /// <returns></returns>
        private IList<WhereConditon> GetWhereConditons(string tableName, ref bool hasUserAccount, ref bool hasDepartmentProperty, ref string warning)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

            warning = string.Empty;
            hasUserAccount = false;
            hasDepartmentProperty = false;
            IList<decimal> dataFieldIds = new List<decimal>();
            DataFieldCondition dataFieldCondition = DataFieldCondition.Like;

            foreach (Control control in gcCondition.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }

                CustomDataFieldInfo customDataFieldInfo = (CustomDataFieldInfo)control.Tag;
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                        SystemPhysicalDataField systemPhysicalDataField = (SystemPhysicalDataField)Convert.ToByte(customDataFieldInfo.DataFieldId);
                        switch (systemPhysicalDataField)
                        {
                            case SystemPhysicalDataField.UserId:
                                if (control is TextEdit)
                                {
                                    /* 用户条件 */
                                    TextEdit textEdit = (TextEdit)control;
                                    string condition = textEdit.Text;
                                    if (!string.IsNullOrWhiteSpace(condition))
                                    {
                                        string userName = Regex.Replace(condition, " {1,}", "%");
                                        whereConditons.Add(new WhereConditon("UserAccount", "UserName", "UserName", DbType.String, string.Format("{0}%", userName),
                                           DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                                        whereConditons.Add(new WhereConditon("UserAccount", "UserActualName", "UserActualName", DbType.String, string.Format("{0}%", userName),
                                           DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                                        hasUserAccount = true;
                                    }
                                }
                                break;

                            case SystemPhysicalDataField.UserTypeId:
                                bool userTypeExclusive = AuthorityHelper.CheckAuthority(customQueyInfo.DataRange, (byte)QueriedDataRange.UserTypeExclusive);
                                if (relatedUserTypeCommonNodes != null)
                                {
                                    if (relatedUserTypeCommonNodes.Count > 0 && !userTypeExclusive)
                                    {
                                        if (relatedUserTypeCommonNodes.Count > 1)
                                        {
                                            CheckedComboBoxEdit checkedComboBoxEdit = (CheckedComboBoxEdit)control;
                                            IList<CommonNode> commonNodes = new List<CommonNode>();
                                            foreach (CheckedListBoxItem checkedListBoxItem in checkedComboBoxEdit.Properties.Items)
                                            {
                                                if (checkedListBoxItem.CheckState == CheckState.Unchecked)
                                                {
                                                    continue;
                                                }
                                                CommonNode commonNode = checkedListBoxItem.Value as CommonNode;
                                                commonNodes.Add(commonNode);
                                            }
                                            if (commonNodes.Count > 0)
                                            {
                                                AddWhereCondition(whereConditons, commonNodes, CommonNodeProperty.Id, tableName, "UserTypeId", "UserTypeId", DbType.Decimal);
                                            }
                                            else
                                            {
                                                AddWhereCondition(whereConditons, relatedUserTypeCommonNodes, CommonNodeProperty.Id, tableName, "UserTypeId", "UserTypeId", DbType.Decimal);
                                            }
                                        }
                                        else
                                        {
                                            whereConditons.Add(new WhereConditon(tableName, "UserTypeId", "UserTypeId", DbType.Decimal, relatedUserTypeCommonNodes[0].NodeId,
                                          DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                                        }
                                    }
                                    else
                                    {
                                        TreeDropdownList treeDropdownList = (TreeDropdownList)control;
                                        if (treeDropdownList.CheckedTreeNodes.Count > 0)
                                        {
                                            IList<CommonNode> commonNodes = new List<CommonNode>();
                                            foreach (TreeNode treeNode in treeDropdownList.CheckedTreeNodes)
                                            {
                                                CommonNode commonNode = treeNode.Tag as CommonNode;
                                                commonNodes.Add(commonNode);
                                            }
                                            AddWhereCondition(whereConditons, commonNodes, CommonNodeProperty.Id, tableName, "UserTypeId", "UserTypeId", DbType.Decimal);
                                        }
                                    }
                                }
                                break;

                            case SystemPhysicalDataField.DepId:
                                bool depExclusive = AuthorityHelper.CheckAuthority(customQueyInfo.DataRange, (byte)QueriedDataRange.DepExclusive);
                                if (relatedDepartmentCommonNodes != null)
                                {
                                    if (relatedDepartmentCommonNodes.Count > 0 && !depExclusive)
                                    {
                                        if (relatedDepartmentCommonNodes.Count > 1)
                                        {
                                            TreeMultipleList treeMultipleList = (TreeMultipleList)control;
                                            IList<CommonNode> commonNodes = new List<CommonNode>();
                                            foreach (TreeNode treeNode in treeMultipleList.CheckedTreeNodes)
                                            {
                                                CommonNode commonNode = treeNode.Tag as CommonNode;
                                                commonNodes.Add(commonNode);
                                            }
                                            if (commonNodes.Count > 0)
                                            {
                                                AddWhereCondition(whereConditons, commonNodes, CommonNodeProperty.Id, tableName, "DepId", "DepId", DbType.Decimal);
                                            }
                                            else
                                            {
                                                AddWhereCondition(whereConditons, relatedDepartmentCommonNodes, CommonNodeProperty.Id, tableName, "DepId", "DepId", DbType.Decimal);
                                            }
                                        }
                                        else
                                        {
                                            whereConditons.Add(new WhereConditon(tableName, "DepId", "DepId", DbType.Decimal, relatedDepartmentCommonNodes[0].NodeId,
                                          DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                                        }
                                    }
                                    else
                                    {
                                        TreeDropdownList treeDropdownList = (TreeDropdownList)control;
                                        if (treeDropdownList.CheckedTreeNodes.Count > 0)
                                        {
                                            IList<CommonNode> commonNodes = new List<CommonNode>();
                                            foreach (TreeNode treeNode in treeDropdownList.CheckedTreeNodes)
                                            {
                                                CommonNode commonNode = treeNode.Tag as CommonNode;
                                                commonNodes.Add(commonNode);
                                            }
                                            AddWhereCondition(whereConditons, commonNodes, CommonNodeProperty.Id, tableName, "DepId", "DepId", DbType.Decimal);
                                        }
                                    }
                                }
                                break;

                            case SystemPhysicalDataField.RecordSorting:
                                bool depProperty = AuthorityHelper.CheckAuthority(customQueyInfo.DataRange, (byte)QueriedDataRange.DepProperty);
                                hasDepartmentProperty = true;
                                CheckedComboBoxEdit comboBoxEdit = (CheckedComboBoxEdit)control;
                                IList<CommonNode> nodes = new List<CommonNode>();
                                foreach (CheckedListBoxItem checkedListBoxItem in comboBoxEdit.Properties.Items)
                                {
                                    if (checkedListBoxItem.CheckState == CheckState.Unchecked)
                                    {
                                        continue;
                                    }
                                    CommonNode commonNode = checkedListBoxItem.Value as CommonNode;
                                    nodes.Add(commonNode);
                                }
                                if (nodes.Count > 0)
                                {
                                    AddWhereCondition(whereConditons, nodes, CommonNodeProperty.Id, "CustomDepartment", "DepartmentProperty", "DepartmentProperty", DbType.Decimal);
                                }
                                else
                                {
                                    if (departmentPropertyCommonNodes.Count>0)
                                    {
                                        AddWhereCondition(whereConditons, departmentPropertyCommonNodes, CommonNodeProperty.Id, "CustomDepartment", "DepartmentProperty", "DepartmentProperty", DbType.Decimal);
                                    }
                                }
                                break;
                        }
                        break;

                    case DataFieldProperty.PhysicalDataField:
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                        if (physicalDataFieldType == PhysicalDataFieldType.PrimaryAssociation || physicalDataFieldType == PhysicalDataFieldType.SecondaryAssociation)
                        {
                            BasedDataType basedDataType = AssociatedDataFieldContract.GetBasedDataType(customDataFieldInfo.AssociatedDataFieldId);
                            switch (basedDataType)
                            {
                                case BasedDataType.Boolean:
                                    physicalDataFieldType = PhysicalDataFieldType.Boolean;
                                    break;

                                case BasedDataType.DateTime:
                                    physicalDataFieldType = PhysicalDataFieldType.YearAndMonthAndDay;
                                    break;

                                case BasedDataType.Decimal:
                                    physicalDataFieldType = PhysicalDataFieldType.Decimal;
                                    break;

                                case BasedDataType.Int32:
                                    physicalDataFieldType = PhysicalDataFieldType.Int32;
                                    break;

                                case BasedDataType.String:
                                    physicalDataFieldType = PhysicalDataFieldType.ArbitraryString;
                                    break;
                            }
                        }
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.Boolean:
                                whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, DbType.Boolean,
                                            ((CheckEdit)control).Checked, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                break;

                            case PhysicalDataFieldType.Int32:
                            case PhysicalDataFieldType.FstAdditionalInteger:
                            case PhysicalDataFieldType.ScdAdditionalInteger:
                                string textInt32 = ((TextEdit)control).Text.Trim();
                                if (!string.IsNullOrEmpty(textInt32))
                                {
                                    //整数 
                                    Regex regexInt32 = new Regex(@"^-?\d+$");
                                    if (!regexInt32.IsMatch(textInt32))
                                    {
                                        control.Focus();
                                        warning = string.Format("{0}不能为非整数。", customDataFieldInfo.LogicalName);
                                        return null;
                                    }
                                    //超过范围转换失败                                
                                    if (DataConvertionHelper.IsNullValue(DataConvertionHelper.GetConvertedInt(textInt32)))
                                    {
                                        control.Focus();
                                        warning = string.Format("{0}的整数值的超出了整数限制范围(-2147483648~2147483647)。", customDataFieldInfo.LogicalName);
                                        return null;
                                    }
                                    int dataFieldValue = DataConvertionHelper.GetConvertedInt(textInt32);
                                    if (dataFieldIds.Contains(customDataFieldInfo.DataFieldId))
                                    {
                                        whereConditons[whereConditons.Count - 1].DataFieldBracketType = DataFieldBracket.LeftBracket;
                                        whereConditons[whereConditons.Count - 1].DataFieldBracketCount = 1;
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, string.Format("{0}_1", customDataFieldInfo.PhysicalName), DbType.Int32,
                                                                                                              dataFieldValue, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                                    }
                                    else
                                    {
                                        dataFieldIds.Add(customDataFieldInfo.DataFieldId);
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, DbType.Int32,
                                                                      dataFieldValue, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.Decimal:
                            case PhysicalDataFieldType.FstAdditionalDecimal:
                            case PhysicalDataFieldType.ScdAdditionalDecimal:
                                string textDecimal = ((TextEdit)control).Text.Trim();
                                if (!string.IsNullOrEmpty(textDecimal))
                                {
                                    //浮点数 
                                    Regex regexDecimal = new Regex(@"^(-?\d+)(\.\d+)?$");
                                    if (!regexDecimal.IsMatch(textDecimal))
                                    {
                                        control.Focus();
                                        warning = string.Format("{0}不能为非实数。", customDataFieldInfo.LogicalName);
                                        return null;
                                    }
                                    int pos = textDecimal.IndexOf('.');
                                    if ((pos > 0 && (textDecimal.Length - pos - 1) > customDataFieldInfo.DataFieldLength) || textDecimal.Length > 12)
                                    {
                                        control.Focus();
                                        warning = string.Format("{0}的实数长度限制的范围(0~12位)，小数位长度不能超过{1}。", customDataFieldInfo.LogicalName, customDataFieldInfo.DataFieldLength);
                                        return null;
                                    }
                                    decimal dataFieldValue = DataConvertionHelper.GetConvertedDecimal(textDecimal);
                                    if (dataFieldIds.Contains(customDataFieldInfo.DataFieldId))
                                    {
                                        whereConditons[whereConditons.Count - 1].DataFieldBracketType = DataFieldBracket.LeftBracket;
                                        whereConditons[whereConditons.Count - 1].DataFieldBracketCount = 1;
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, string.Format("{0}_1", customDataFieldInfo.PhysicalName), DbType.Decimal,
                                                                                                              dataFieldValue, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.RightBracket, 1));
                                    }
                                    else
                                    {
                                        dataFieldIds.Add(customDataFieldInfo.DataFieldId);
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, DbType.Decimal,
                                                                       dataFieldValue, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.ArbitraryString:
                            case PhysicalDataFieldType.ExtendedArbitraryString:
                            case PhysicalDataFieldType.NumeralString:
                            case PhysicalDataFieldType.CharString:
                            case PhysicalDataFieldType.MixedString:
                            case PhysicalDataFieldType.EncryptedString:
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
                            case PhysicalDataFieldType.DepartmentValue:
                            case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                            case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                                if (control is ComboBoxEdit)
                                {
                                    ComboBoxEdit comboBoxEdit = (ComboBoxEdit)control;
                                    CommonNode commonNode = (CommonNode)comboBoxEdit.EditValue;
                                    MatchingMode matchingMode = (MatchingMode)commonNode.NodeId;
                                    if (matchingMode == MatchingMode.Equal)
                                    {
                                        dataFieldCondition = DataFieldCondition.Equal;
                                    }
                                    else
                                    {
                                        dataFieldCondition = DataFieldCondition.Like;
                                    }
                                }
                                else
                                {
                                    string textString = ((TextEdit)control).Text.Trim();
                                    if (!string.IsNullOrEmpty(textString))
                                    {
                                        switch (physicalDataFieldType)
                                        {
                                            case PhysicalDataFieldType.NumeralString:
                                                if (!string.IsNullOrEmpty(textString))
                                                {
                                                    //整数 
                                                    Regex numeralRegex = new Regex(@"^-?\d+$");
                                                    if (!numeralRegex.IsMatch(textString))
                                                    {
                                                        control.Focus();
                                                        warning = string.Format("{0}只能为数字组成的字符串。", customDataFieldInfo.LogicalName);
                                                        return null;
                                                    }
                                                }
                                                break;

                                            case PhysicalDataFieldType.CharString:
                                                if (!string.IsNullOrEmpty(textString))
                                                {
                                                    //由26个英文字母组成的字符串 
                                                    Regex charRegex = new Regex(@"^[A-Za-z]+$");
                                                    if (!charRegex.IsMatch(textString))
                                                    {
                                                        control.Focus();
                                                        warning = string.Format("{0}只能为由26个英文字母组成的字符串。", customDataFieldInfo.LogicalName);
                                                        return null;
                                                    }
                                                }
                                                break;

                                            case PhysicalDataFieldType.MixedString:
                                                if (!string.IsNullOrEmpty(textString))
                                                {
                                                    //由数字和26个英文字母组成的字符串  
                                                    Regex mixedRegex = new Regex(@"^[A-Za-z0-9]+$");
                                                    if (!mixedRegex.IsMatch(textString))
                                                    {
                                                        control.Focus();
                                                        warning = string.Format("{0}只能为由数字和26个英文字母组成的字符串。", customDataFieldInfo.LogicalName);
                                                        return null;
                                                    }
                                                }
                                                break;

                                            case PhysicalDataFieldType.ArbitraryString:
                                                if (!string.IsNullOrWhiteSpace(customDataFieldInfo.RegexExpression))
                                                {
                                                    Regex regex = new Regex(customDataFieldInfo.RegexExpression);
                                                    if (!regex.IsMatch(textString))
                                                    {
                                                        control.Focus();
                                                        warning = string.Format("{0}不符合要求的格式。", customDataFieldInfo.LogicalName);
                                                        return null;
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(textString))
                                    {
                                        if (dataFieldCondition == DataFieldCondition.Like)
                                        {
                                            textString = string.Format("%{0}%", textString);
                                        }
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, DbType.String,
                                            textString, dataFieldCondition, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.Time:
                                TimeEdit timeEdit = (TimeEdit)control;
                                if (timeEdit.EditValue != null)
                                {
                                    DateTime dataFieldValue = DateTime.Parse(string.Format("{0} {1}", AppSettingHelper.YearMonthDay, timeEdit.Time.ToLongTimeString()));
                                    if (dataFieldIds.Contains(customDataFieldInfo.DataFieldId))
                                    {
                                        whereConditons[whereConditons.Count - 1].DataFieldBracketType = DataFieldBracket.LeftBracket;
                                        whereConditons[whereConditons.Count - 1].DataFieldBracketCount = 1;
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, string.Format("{0}_1", customDataFieldInfo.PhysicalName), DbType.DateTime,
                                                                                                              dataFieldValue, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.RightBracket, 1));
                                    }
                                    else
                                    {
                                        dataFieldIds.Add(customDataFieldInfo.DataFieldId);
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, DbType.DateTime,
                                                                       dataFieldValue, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                            case PhysicalDataFieldType.YearAndMonthAndDay:
                            case PhysicalDataFieldType.YearAndMonth:
                            case PhysicalDataFieldType.MonthAndDay:
                                DateEdit dateEdit = (DateEdit)control;
                                if (dateEdit.EditValue != null)
                                {
                                    DateTime dateTimeValue = dateEdit.DateTime;
                                    if (!DataConvertionHelper.IsNullValue(dateTimeValue))
                                    {
                                        string result = string.Empty;
                                        DateTime dataFieldValue = DateTime.MinValue;
                                        switch (physicalDataFieldType)
                                        {
                                            case PhysicalDataFieldType.YearAndMonth:
                                                result = string.Format("{0}-{1}-01", dateEdit.DateTime.Year, dateEdit.DateTime.Month);
                                                dataFieldValue = DateTime.Parse(result);
                                                break;

                                            case PhysicalDataFieldType.YearAndMonthAndDay:
                                                result = dateEdit.DateTime.ToShortDateString();
                                                dataFieldValue = DateTime.Parse(result);
                                                break;

                                            case PhysicalDataFieldType.MonthAndDay:
                                                result = string.Format("{0}-{1}-{2}", AppSettingHelper.Year, dateEdit.DateTime.Month, dateEdit.DateTime.Day);
                                                dataFieldValue = DateTime.Parse(result);
                                                break;
                                        }
                                        if (dataFieldIds.Contains(customDataFieldInfo.DataFieldId))
                                        {
                                            whereConditons[whereConditons.Count - 1].DataFieldBracketType = DataFieldBracket.LeftBracket;
                                            whereConditons[whereConditons.Count - 1].DataFieldBracketCount = 1;
                                            whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, string.Format("{0}_1", customDataFieldInfo.PhysicalName), DbType.DateTime,
                                                                                                                  dataFieldValue, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.RightBracket, 1));
                                        }
                                        else
                                        {
                                            dataFieldIds.Add(customDataFieldInfo.DataFieldId);
                                            whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, DbType.DateTime,
                                                                            dataFieldValue, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                        }
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.TreeViewEnum:
                            case PhysicalDataFieldType.TreeViewEnumValue:
                            case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                            case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                            case PhysicalDataFieldType.DepartmentTreeViewEnum:
                            case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                                TreeDropdownList treeDropdownList = (TreeDropdownList)control;
                                if (treeDropdownList.CheckedTreeNodes.Count > 0)
                                {
                                    IList<CommonNode> commonNodes = new List<CommonNode>();
                                    foreach (TreeNode treeNode in treeDropdownList.CheckedTreeNodes)
                                    {
                                        CommonNode commonNode = treeNode.Tag as CommonNode;
                                        commonNodes.Add(commonNode);
                                    }
                                    AddWhereCondition(whereConditons, commonNodes, CommonNodeProperty.Name, tableName, customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, DbType.String);
                                }
                                break;

                            case PhysicalDataFieldType.DropdownListEnum:
                            case PhysicalDataFieldType.MultiSelectedEnum:
                            case PhysicalDataFieldType.DepartmentDropdownListEnum:
                                CheckedComboBoxEdit chkEdit = (CheckedComboBoxEdit)control;
                                if (chkEdit.EditValue != null)
                                {
                                    IList<CommonNode> commonNodes = new List<CommonNode>();
                                    foreach (CheckedListBoxItem checkedListBoxItem in chkEdit.Properties.Items)
                                    {
                                        if (checkedListBoxItem.CheckState == CheckState.Unchecked)
                                        {
                                            continue;
                                        }
                                        string name = string.Empty;
                                        CommonNode commonNode = checkedListBoxItem.Value as CommonNode;
                                        commonNodes.Add(commonNode);
                                    }
                                    AddWhereCondition(whereConditons, commonNodes, CommonNodeProperty.Name, tableName, customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, DbType.String);
                                }
                                break;

                            case PhysicalDataFieldType.DropdownListEnumValue:
                            case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                            case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                                CheckedComboBoxEdit checkedComboBoxEdit = (CheckedComboBoxEdit)control;
                                DbType dbType = DbType.String;
                                if (checkedComboBoxEdit.EditValue != null)
                                {
                                    IList<CommonNode> commonNodes = new List<CommonNode>();
                                    foreach (CheckedListBoxItem checkedListBoxItem in checkedComboBoxEdit.Properties.Items)
                                    {
                                        if (checkedListBoxItem.CheckState == CheckState.Unchecked)
                                        {
                                            continue;
                                        }
                                        string name = string.Empty;
                                        CommonNode commonNode = checkedListBoxItem.Value as CommonNode;
                                        commonNodes.Add(commonNode);
                                    }
                                    AddWhereCondition(whereConditons, commonNodes, CommonNodeProperty.Name, tableName, customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, dbType);
                                }
                                break;

                        }
                        break;

                    case DataFieldProperty.LogicalDataField:
                        LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)customDataFieldInfo.DataFieldType;
                        switch (logicalDataFieldType)
                        {
                            case LogicalDataFieldType.StringExpression:
                                if (control is ComboBoxEdit)
                                {
                                    ComboBoxEdit comboBoxEdit = (ComboBoxEdit)control;
                                    CommonNode commonNode = (CommonNode)comboBoxEdit.EditValue;
                                    MatchingMode matchingMode = (MatchingMode)commonNode.NodeId;
                                    if (matchingMode == MatchingMode.Equal)
                                    {
                                        dataFieldCondition = DataFieldCondition.Equal;
                                    }
                                    else
                                    {
                                        dataFieldCondition = DataFieldCondition.Like;
                                    }
                                }
                                else
                                {
                                    string textString = ((TextEdit)control).Text.Trim();
                                    if (!string.IsNullOrWhiteSpace(textString))
                                    {
                                        if (dataFieldCondition == DataFieldCondition.Like)
                                        {
                                            textString = string.Format("%{0}%", textString);
                                        }
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, DbType.String,
                                            textString, dataFieldCondition, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                    }
                                }
                                break;

                            case LogicalDataFieldType.DigitExpression:
                                string textDecimal = ((TextEdit)control).Text.Trim();
                                if (!string.IsNullOrEmpty(textDecimal))
                                {
                                    //浮点数 
                                    Regex regexDecimal = new Regex(@"^(-?\d+)(\.\d+)?$");
                                    if (!regexDecimal.IsMatch(textDecimal))
                                    {
                                        control.Focus();
                                        warning = string.Format("{0}不能为非实数。", customDataFieldInfo.LogicalName);
                                        return null;
                                    }
                                    int pos = textDecimal.IndexOf('.');
                                    if ((pos > 0 && (textDecimal.Length - pos - 1) > customDataFieldInfo.DataFieldLength) || textDecimal.Length > 12)
                                    {
                                        control.Focus();
                                        warning = string.Format("{0}的实数长度限制的范围(0~12位)，小数位长度不能超过{1}。", customDataFieldInfo.LogicalName, customDataFieldInfo.DataFieldLength);
                                        return null;
                                    }
                                    decimal dataFieldValue = DataConvertionHelper.GetConvertedDecimal(textDecimal);
                                    if (dataFieldIds.Contains(customDataFieldInfo.DataFieldId))
                                    {
                                        whereConditons[whereConditons.Count - 1].DataFieldBracketType = DataFieldBracket.LeftBracket;
                                        whereConditons[whereConditons.Count - 1].DataFieldBracketCount = 1;
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, string.Format("{0}_1", customDataFieldInfo.PhysicalName), DbType.Decimal,
                                                                                                              dataFieldValue, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.RightBracket, 1));
                                    }
                                    else
                                    {
                                        dataFieldIds.Add(customDataFieldInfo.DataFieldId);
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, DbType.Decimal,
                                                                       dataFieldValue, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                    }
                                }
                                break;

                            case LogicalDataFieldType.DateTimeExpression:
                                DateEdit dateEdit = (DateEdit)control;
                                if (dateEdit.EditValue != null)
                                {
                                    string result = dateEdit.DateTime.ToShortDateString();
                                    DateTime dataFieldValue = DateTime.Parse(result);
                                    if (dataFieldIds.Contains(customDataFieldInfo.DataFieldId))
                                    {
                                        whereConditons[whereConditons.Count - 1].DataFieldBracketType = DataFieldBracket.LeftBracket;
                                        whereConditons[whereConditons.Count - 1].DataFieldBracketCount = 1;
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, string.Format("{0}_1", customDataFieldInfo.PhysicalName), DbType.DateTime,
                                                                                                              dataFieldValue, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.RightBracket, 1));
                                    }
                                    else
                                    {
                                        dataFieldIds.Add(customDataFieldInfo.DataFieldId);
                                        whereConditons.Add(new WhereConditon(customDataFieldInfo.PhysicalName, customDataFieldInfo.PhysicalName, DbType.DateTime,
                                                                       dataFieldValue, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                    }
                                }
                                break;
                        }
                        break;
                }
            }

            return whereConditons;
        }

        #endregion

        #region 公有函数

        /// <summary>
        /// 加载查询的条件和排序字段
        /// </summary>
        public void LoadDataFields()
        {
            if (isLoading)
            {
                customQueyInfo = CustomQueyContract.GetModelInfo(ExtendedCustomBusinessInfo.DataQueriedId);

                /* 1. 管理的单位 */
                relatedDepartmentCommonNodes = CustomDepartmentContract.GetCommonNodes(CurrentUser.Instance.UserId);

                /* 2. 管理的用户类型 */
                relatedUserTypeCommonNodes = UserTypeContract.GetCommonNodes(CurrentUser.Instance.UserId);

                IList<CommonNode> commonNodes = CustomQueyContract.GetDigitDataFields(CurrentUser.Instance.UserId);
                ccmbStaticDataField.Properties.Items.AddRange(commonNodes.ToArray());

                LoadConditionControls();
                isLoading = false;
            }

        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 设置页的显示文字
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="commonDataFieldInfo"></param>
        private void SetColumnDisplayText(GridColumn gridColumn, CommonDataFieldInfo commonDataFieldInfo)
        {
            if (gridColumn == null)
            {
                return;
            }
            DataFieldProperty dataFieldProperty = (DataFieldProperty)commonDataFieldInfo.DataFieldProperty;
            if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
            {
                PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)commonDataFieldInfo.DataFieldType;
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                        gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        gridColumn.DisplayFormat.FormatString = "G";
                        break;

                    case PhysicalDataFieldType.YearAndMonthAndDay:
                        gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        gridColumn.DisplayFormat.FormatString = "yyyy-MM-dd";
                        break;

                    case PhysicalDataFieldType.YearAndMonth:
                        gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        gridColumn.DisplayFormat.FormatString = "y";
                        break;

                    case PhysicalDataFieldType.MonthAndDay:
                        gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        gridColumn.DisplayFormat.FormatString = "m";
                        break;

                    case PhysicalDataFieldType.Time:
                        gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        gridColumn.DisplayFormat.FormatString = "HH:mm:ss";
                        break;

                    case PhysicalDataFieldType.Association:
                    case PhysicalDataFieldType.PrimaryAssociation:
                    case PhysicalDataFieldType.SecondaryAssociation:
                        if (gridColumn.ColumnType == typeof(DateTime))
                        {
                            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                            gridColumn.DisplayFormat.FormatString = "yyyy-MM-dd";
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 计算当前函数
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        private Point CalculateRows(int tabIndex)
        {
            int x = 0, y = 0;
            int rows = tabIndex / RowCount;
            if (tabIndex % RowCount != 0)
            {
                rows++;
            }

            /* 1. 横坐标 */
            int remainder = tabIndex % RowCount;
            if (remainder == 0)
            {
                x = COMMON_WIDTH_OF_MARGIN_SPACE;
            }
            else
            {
                x = (COMMON_WIDTH_OF_CONTROL + COMMON_WIDTH_OF_LABEL_TEXT + COMMON_WIDTH_OF_MARGIN_SPACE) * remainder;
            }

            /* 2 纵坐标 */
            int quotient = tabIndex / RowCount;
            y = COMMON_HEIGHT_OF_SPACE + HEIGHT_OF_GROUP_HEADER + (COMMON_HEIGHT_OF_CONTROL + COMMON_HEIGHT_OF_SPACE) * quotient;

            return new Point(x, y);
        }

        /// <summary>
        /// 创建用户类型控件
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <param name="controlX"></param>
        /// <param name="point"></param>
        /// <param name="customDataFieldInfo"></param>
        private void CreateUserTypeControl(int tabIndex, int controlX, Point point, CustomDataFieldInfo customDataFieldInfo)
        {
            int bias = 15 + (tabIndex % RowCount) * 40;
            TreeDropdownList cmbUserType = new TreeDropdownList();
            cmbUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(CustomGroupContract, UserTypeContract);
            cmbUserType.InitalizeTreeView();
            cmbUserType.CheckBoxes = true;
            cmbUserType.Width = 170;
            cmbUserType.TabIndex = tabIndex;
            cmbUserType.Location = new Point(controlX - bias, point.Y - 4);
            cmbUserType.SkinName = "Blue";
            cmbUserType.OnlySelectedLeaf = true;
            cmbUserType.Tag = customDataFieldInfo;
            gcCondition.Controls.Add(cmbUserType);
        }

        /// <summary>
        /// 创建单位控件
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <param name="controlX"></param>
        /// <param name="point"></param>
        /// <param name="bias"></param>
        /// <param name="customDataFieldInfo"></param>
        private void CreateDepControl(int tabIndex, int controlX, Point point, int bias, CustomDataFieldInfo customDataFieldInfo)
        {
            TreeDropdownList cmbDep = new TreeDropdownList();
            cmbDep.TreeDropdownHandler = new TreeDropdownItems(CustomDepartmentContract);
            cmbDep.InitalizeTreeView();
            cmbDep.Width = 170;
            cmbDep.TabIndex = tabIndex;
            cmbDep.CheckBoxes = true;
            cmbDep.Location = new Point(controlX - bias, point.Y - 4);
            cmbDep.SkinName = "Blue";
            cmbDep.OnlySelectedLeaf = true;
            cmbDep.Tag = customDataFieldInfo;
            gcCondition.Controls.Add(cmbDep);
        }

        /// <summary>
        /// 加载条件控件
        /// </summary>
        private void LoadConditionControls()
        {
            int controlX = 0, tabIndex = 0;

            /*1.用户条件*/
            bool user = AuthorityHelper.CheckAuthority(customQueyInfo.SystemCondition, (byte)SystemCondition.User);
            if (user)
            {
                Point point = CalculateRows(tabIndex);
                CustomDataFieldInfo customDataFieldInfo = CommonBussinessHelper.GetSystemDataFieldInfo(decimal.MinValue, SystemPhysicalDataField.UserId);
                AddLabelControl("用户条件：", point.X, point.Y);
                controlX = point.X + COMMON_WIDTH_OF_LABEL_TEXT;
                AddTextEditControl(controlX, point.Y, COMMON_WIDTH_OF_CONTROL, tabIndex, customDataFieldInfo, "请输入用户名、姓名");
                tabIndex++;
            }

            /*2.用户类型条件*/
            bool userType = AuthorityHelper.CheckAuthority(customQueyInfo.SystemCondition, (byte)SystemCondition.UserTypeId);
            bool userTypeExclusive = AuthorityHelper.CheckAuthority(customQueyInfo.DataRange, (byte)QueriedDataRange.UserTypeExclusive);
            if (userType)
            {
                Point point = CalculateRows(tabIndex);
                CustomDataFieldInfo customDataFieldInfo = CommonBussinessHelper.GetSystemDataFieldInfo(decimal.MinValue, SystemPhysicalDataField.UserTypeId);
                AddLabelControl(string.Format("{0}：", customDataFieldInfo.LogicalName), point.X, point.Y);
                controlX = point.X + COMMON_WIDTH_OF_LABEL_TEXT;
                if (userTypeExclusive)
                {
                    CreateUserTypeControl(tabIndex, controlX, point, customDataFieldInfo);
                }
                else
                {
                    if ((relatedUserTypeCommonNodes == null || relatedUserTypeCommonNodes.Count != 1))
                    {
                        if (relatedUserTypeCommonNodes != null)
                        {
                            if (relatedUserTypeCommonNodes.Count > 1)
                            {
                                AddCheckedComboBoxEditControl(controlX, point.Y - 4, COMMON_WIDTH_OF_CONTROL, tabIndex, customDataFieldInfo, relatedUserTypeCommonNodes);
                            }
                            else
                            {
                                CreateUserTypeControl(tabIndex, controlX, point, customDataFieldInfo);
                            }
                        }
                    }
                }
                tabIndex++;
            }

            /*3.单位条件*/
            bool dep = AuthorityHelper.CheckAuthority(customQueyInfo.SystemCondition, (byte)SystemCondition.DepId);
            bool depExclusive = AuthorityHelper.CheckAuthority(customQueyInfo.DataRange, (byte)QueriedDataRange.DepExclusive);
            if (dep)
            {
                Point point = CalculateRows(tabIndex);
                CustomDataFieldInfo customDataFieldInfo = CommonBussinessHelper.GetSystemDataFieldInfo(decimal.MinValue, SystemPhysicalDataField.DepId);
                AddLabelControl(string.Format("{0}：", customDataFieldInfo.LogicalName), point.X, point.Y);
                controlX = point.X + COMMON_WIDTH_OF_LABEL_TEXT;
                int bias = 15 + (tabIndex % RowCount) * 40;
                if (depExclusive)
                {
                    CreateDepControl(tabIndex, controlX, point, bias, customDataFieldInfo);
                }
                else
                {
                    if (relatedDepartmentCommonNodes.Count != 1)
                    {
                        if (relatedDepartmentCommonNodes.Count > 1)
                        {
                            TreeMultipleList treeMultipleList = new TreeMultipleList();
                            treeMultipleList.ShowSearch = true;
                            treeMultipleList.CommonNodeContract = CustomDepartmentContract;
                            treeMultipleList.InitalizeTreeView(relatedDepartmentCommonNodes);
                            treeMultipleList.Width = 170;
                            treeMultipleList.CheckBoxes = true;
                            treeMultipleList.Location = new Point(controlX - bias, point.Y - 4);
                            treeMultipleList.SkinName = "Blue";
                            treeMultipleList.OnlySelectedLeaf = true;
                            treeMultipleList.Tag = customDataFieldInfo;
                            gcCondition.Controls.Add(treeMultipleList);

                        }
                        else
                        {
                            CreateDepControl(tabIndex, controlX, point, bias, customDataFieldInfo);
                        }
                    }
                    tabIndex++;
                }
            }

            /*4.单位属性条件*/
            bool departmentProperty = AuthorityHelper.CheckAuthority(customQueyInfo.SystemCondition, (byte)SystemCondition.DepartmentProperty);
            bool depProperty = AuthorityHelper.CheckAuthority(customQueyInfo.DataRange, (byte)QueriedDataRange.DepProperty);
            if (departmentProperty)
            {
                departmentPropertyCommonNodes = new List<CommonNode>();
                IList<CommonNode> departmentPropertyNodes = new List<CommonNode>();
                IList<EnumItem> departmentProperties = SystemConfigHelper.GetDepartmentPorperty();
                Int64 departmentAuthority = UserAccountContract.GetDepartmentAuthority(CurrentUser.Instance.UserId);
                foreach (EnumItem enumItem in departmentProperties)
                {
                    if (departmentAuthority > 0 && !depProperty)
                    {
                        bool result = AuthorityHelper.CheckAuthority(departmentAuthority, enumItem.Value);
                        if (result)
                        {
                            departmentPropertyCommonNodes.Add(new CommonNode(enumItem.Value, decimal.MinValue, enumItem.Text));
                        }
                    }
                    else
                    {
                        departmentPropertyNodes.Add(new CommonNode(enumItem.Value, decimal.MinValue, enumItem.Text));
                    }
                }
                Point point = CalculateRows(tabIndex);
                CustomDataFieldInfo customDataFieldInfo = CommonBussinessHelper.GetSystemDataFieldInfo(decimal.MinValue, SystemPhysicalDataField.RecordSorting);
                AddLabelControl("单位属性：", point.X, point.Y);
                controlX = point.X + COMMON_WIDTH_OF_LABEL_TEXT;
                if (departmentPropertyCommonNodes.Count > 1)
                {
                    AddCheckedComboBoxEditControl(controlX, point.Y, COMMON_WIDTH_OF_CONTROL, tabIndex, customDataFieldInfo, departmentPropertyCommonNodes);
                }
                else
                {
                    if (departmentPropertyCommonNodes.Count == 0)
                    {
                        AddCheckedComboBoxEditControl(controlX, point.Y, COMMON_WIDTH_OF_CONTROL, tabIndex, customDataFieldInfo, departmentPropertyNodes);
                    }
                }
                tabIndex++;
            }
            IList<CustomDataFieldInfo> customDataFieldInfos = CustomQueyContract.GetConditionalCustomDataFieldInfos(customQueyInfo.DataQueriedId);
            int tabCount = LoadConditionControls(tabIndex, customDataFieldInfos);
            gcCondition.Height = pnlStaticDataField.Height + HEIGHT_OF_GROUP_HEADER + COMMON_HEIGHT_OF_SPACE + (tabCount / RowCount + ((tabCount % RowCount) == 0 ? 0 : 1)) * (COMMON_HEIGHT_OF_SPACE + COMMON_HEIGHT_OF_CONTROL);
        }
        
        /// <summary>
        /// 加载查询条件
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <param name="customDataFieldInfos"></param>
        /// <returns></returns>
        private int LoadConditionControls(int tabIndex, IList<CustomDataFieldInfo> customDataFieldInfos)
        {
            int controlX = 0;

            foreach (CustomDataFieldInfo customDataFieldInfo in customDataFieldInfos)
            {
                Point point = CalculateRows(tabIndex);
                tabIndex++;
                AddLabelControl(string.Format("{0}：", customDataFieldInfo.LogicalName), point.X, point.Y);
                controlX = point.X + COMMON_WIDTH_OF_LABEL_TEXT;
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.PhysicalDataField:
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                        if (physicalDataFieldType == PhysicalDataFieldType.PrimaryAssociation || physicalDataFieldType == PhysicalDataFieldType.SecondaryAssociation)
                        {
                            BasedDataType basedDataType = AssociatedDataFieldContract.GetBasedDataType(customDataFieldInfo.AssociatedDataFieldId);
                            switch (basedDataType)
                            {
                                case BasedDataType.Boolean:
                                    physicalDataFieldType = PhysicalDataFieldType.Boolean;
                                    break;

                                case BasedDataType.DateTime:
                                    physicalDataFieldType = PhysicalDataFieldType.YearAndMonthAndDay;
                                    break;

                                case BasedDataType.Decimal:
                                    physicalDataFieldType = PhysicalDataFieldType.Decimal;
                                    break;

                                case BasedDataType.Int32:
                                    physicalDataFieldType = PhysicalDataFieldType.Int32;
                                    break;

                                case BasedDataType.String:
                                    physicalDataFieldType = PhysicalDataFieldType.ArbitraryString;
                                    break;

                            }
                        }
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.Boolean:
                                CheckEdit checkEdit = new CheckEdit();
                                checkEdit.Properties.Caption = string.Empty;
                                checkEdit.Location = new Point(controlX, point.Y);
                                checkEdit.TabIndex = tabIndex;
                                checkEdit.Tag = customDataFieldInfo;
                                gcCondition.Controls.Add(checkEdit);
                                break;

                            case PhysicalDataFieldType.Int32:
                            case PhysicalDataFieldType.Decimal:
                            case PhysicalDataFieldType.FstAdditionalInteger:
                            case PhysicalDataFieldType.ScdAdditionalInteger:
                            case PhysicalDataFieldType.FstAdditionalDecimal:
                            case PhysicalDataFieldType.ScdAdditionalDecimal:
                                AddTextEditControl(controlX, point.Y, COMMON_WIDTH_OF_DIGIT_CONTROL, tabIndex, customDataFieldInfo, string.Empty);
                                controlX += COMMON_WIDTH_OF_DIGIT_CONTROL + 5;

                                AddToTextControl(controlX, point.Y);
                                controlX += 22;

                                AddTextEditControl(controlX, point.Y, COMMON_WIDTH_OF_DIGIT_CONTROL, tabIndex, customDataFieldInfo, string.Empty);
                                break;

                            case PhysicalDataFieldType.ArbitraryString:
                            case PhysicalDataFieldType.ExtendedArbitraryString:
                            case PhysicalDataFieldType.NumeralString:
                            case PhysicalDataFieldType.CharString:
                            case PhysicalDataFieldType.MixedString:
                            case PhysicalDataFieldType.EncryptedString:
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
                            case PhysicalDataFieldType.DepartmentValue:
                            case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                            case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                                AddComboBoxEditContolWithMatchingMode(controlX, point.Y, COMMON_WIDTH_OF_COMBOBOX_CONTROL, tabIndex, customDataFieldInfo);
                                controlX += 75;
                                AddTextEditControl(controlX, point.Y, COMMON_WIDTH_OF_TEXT_CONTROL, tabIndex, customDataFieldInfo, string.Empty);
                                break;

                            case PhysicalDataFieldType.Time:
                                AddTimeEditControl(controlX, point.Y, COMMON_WIDTH_OF_DATE_CONTROL, tabIndex, customDataFieldInfo);
                                controlX += COMMON_WIDTH_OF_DATE_CONTROL + 6;

                                AddToTextControl(controlX, point.Y);
                                controlX += 23;

                                AddTimeEditControl(controlX, point.Y, COMMON_WIDTH_OF_DATE_CONTROL, tabIndex, customDataFieldInfo);
                                break;

                            case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                            case PhysicalDataFieldType.YearAndMonthAndDay:
                            case PhysicalDataFieldType.YearAndMonth:
                            case PhysicalDataFieldType.MonthAndDay:
                                string special = "G";
                                switch (physicalDataFieldType)
                                {
                                    case PhysicalDataFieldType.YearAndMonthAndDay:
                                        special = "d";
                                        break;

                                    case PhysicalDataFieldType.YearAndMonth:
                                        special = "y";
                                        break;

                                    case PhysicalDataFieldType.MonthAndDay:
                                        special = "m";
                                        break;
                                }
                                AddDateEditControl(controlX, point.Y, COMMON_WIDTH_OF_DATE_CONTROL, tabIndex, customDataFieldInfo, special);
                                controlX += COMMON_WIDTH_OF_DATE_CONTROL + 6;

                                AddToTextControl(controlX, point.Y);
                                controlX += 23;

                                AddDateEditControl(controlX, point.Y, COMMON_WIDTH_OF_DATE_CONTROL, tabIndex, customDataFieldInfo, special);
                                if (MaxmizeVisible)
                                {
                                    tabIndex++;
                                }
                                break;

                            case PhysicalDataFieldType.TreeViewEnum:
                            case PhysicalDataFieldType.TreeViewEnumValue:
                            case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                            case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                                AddTreeviewControl(controlX, point.Y, COMMON_WIDTH_OF_CONTROL, tabIndex, customDataFieldInfo);
                                break;

                            case PhysicalDataFieldType.DropdownListEnum:
                            case PhysicalDataFieldType.DropdownListEnumValue:
                            case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                            case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                            case PhysicalDataFieldType.MultiSelectedEnum:
                                AddCheckedComboBoxEditControl(controlX, point.Y, COMMON_WIDTH_OF_CONTROL, tabIndex, customDataFieldInfo);
                                break;

                            case PhysicalDataFieldType.DepartmentDropdownListEnum:
                                IList<CommonNode> depCommonNodes = CustomDepartmentContract.GetChildNodes(decimal.One);
                                AddCheckedComboBoxEditControl(controlX, point.Y, COMMON_WIDTH_OF_CONTROL, tabIndex, customDataFieldInfo, depCommonNodes);
                                break;

                            case PhysicalDataFieldType.DepartmentTreeViewEnum:
                            case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                                TreeDropdownList ddlDepartmentTreeView = new TreeDropdownList();
                                ddlDepartmentTreeView.Location = new Point(controlX, point.Y);
                                ddlDepartmentTreeView.CheckBoxes = true;
                                ddlDepartmentTreeView.SkinName = "Blue";
                                ddlDepartmentTreeView.TabIndex = tabIndex;
                                ddlDepartmentTreeView.Width = COMMON_WIDTH_OF_CONTROL;
                                ddlDepartmentTreeView.OnlySelectedLeaf = false;
                                ddlDepartmentTreeView.Tag = customDataFieldInfo;
                                if (physicalDataFieldType == PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot)
                                {
                                    ddlDepartmentTreeView.TreeDropdownHandler = new TreeDropdownItems(CustomDepartmentContract);
                                }
                                else
                                {
                                    ddlDepartmentTreeView.TreeDropdownHandler = new TreeDropdownItems(CustomDepartmentContract, decimal.One);
                                }
                                gcCondition.Controls.Add(ddlDepartmentTreeView);
                                break;
                        }
                        break;

                    case DataFieldProperty.LogicalDataField:
                        LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)customDataFieldInfo.DataFieldType;
                        switch (logicalDataFieldType)
                        {
                            case LogicalDataFieldType.StringExpression:
                                AddComboBoxEditContolWithMatchingMode(controlX, point.Y, COMMON_WIDTH_OF_COMBOBOX_CONTROL, tabIndex, customDataFieldInfo);
                                controlX += 75;

                                AddTextEditControl(controlX, point.Y, COMMON_WIDTH_OF_TEXT_CONTROL, tabIndex, customDataFieldInfo, string.Empty);
                                break;

                            case LogicalDataFieldType.DigitExpression:
                                AddDigitalTextEditControl(controlX, point.Y, COMMON_WIDTH_OF_DIGIT_CONTROL, tabIndex, customDataFieldInfo);
                                controlX += COMMON_WIDTH_OF_DIGIT_CONTROL + 4;

                                AddToTextControl(controlX, point.Y);

                                AddDigitalTextEditControl(controlX, point.Y, COMMON_WIDTH_OF_DIGIT_CONTROL, tabIndex, customDataFieldInfo);
                                break;

                            case LogicalDataFieldType.DateTimeExpression:
                                AddDateEditControl(controlX, point.Y, COMMON_WIDTH_OF_TEXT_CONTROL, tabIndex, customDataFieldInfo, "d");
                                controlX += COMMON_WIDTH_OF_TEXT_CONTROL + 6;

                                AddToTextControl(controlX, point.Y);
                                controlX += 23;

                                AddDateEditControl(controlX, point.Y, COMMON_WIDTH_OF_TEXT_CONTROL, tabIndex, customDataFieldInfo, "d");
                                break;
                        }
                        break;
                }
            }

            return tabIndex;
        }

        private void AddDateEditControl(int controlX, int controlY, int width, int index, CustomDataFieldInfo customDataFieldInfo, string special)
        {
            DateEdit dateEdit = new DateEdit();
            dateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            if (special.Equals("G"))
            {
                dateEdit.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
                dateEdit.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            }
            dateEdit.Properties.DisplayFormat.FormatString = special;
            dateEdit.Properties.EditFormat.FormatString = special;
            dateEdit.Properties.EditMask = special;
            dateEdit.Properties.NullText = string.Empty;
            dateEdit.EditValue = null;
            dateEdit.Location = new Point(controlX, controlY);
            dateEdit.TabIndex = index;
            dateEdit.Width = width;
            dateEdit.Tag = customDataFieldInfo;
            gcCondition.Controls.Add(dateEdit);
        }

        private void AddTimeEditControl(int controlX, int controlY, int width, int index, CustomDataFieldInfo customDataFieldInfo)
        {
            TimeEdit timeEdit = new TimeEdit();
            timeEdit.Properties.Mask.EditMask = "HH:mm:ss";
            timeEdit.Properties.NullText = string.Empty;
            timeEdit.EditValue = null;
            timeEdit.Location = new Point(controlX, controlY);
            timeEdit.TabIndex = index;
            timeEdit.Width = width;
            timeEdit.Tag = customDataFieldInfo;
            gcCondition.Controls.Add(timeEdit);
        }

        private void AddToTextControl(int controlX, int controlY)
        {
            Label lblDateEdit = new Label();
            lblDateEdit.Text = "至";
            lblDateEdit.Size = new System.Drawing.Size(15, 21);
            lblDateEdit.Location = new Point(controlX, controlY + 2);
            gcCondition.Controls.Add(lblDateEdit);
        }

        private void AddDigitalTextEditControl(int controlX, int controlY, int controlWidth, int index, CustomDataFieldInfo customDataFieldInfo)
        {
            TextEdit digitalTextEdit = new TextEdit();
            digitalTextEdit.Properties.MaxLength = 11;
            digitalTextEdit.Location = new Point(controlX, controlY);
            digitalTextEdit.Size = new System.Drawing.Size(controlWidth, 21);
            digitalTextEdit.TabIndex = index;
            digitalTextEdit.Tag = customDataFieldInfo;
            gcCondition.Controls.Add(digitalTextEdit);
        }

        private void AddComboBoxEditContolWithMatchingMode(int controlX, int controlY, int width, int index, CustomDataFieldInfo customDataFieldInfo)
        {
            ComboBoxEdit cmbSelect = new ComboBoxEdit();
            cmbSelect.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            cmbSelect.Location = new Point(controlX, controlY);
            cmbSelect.TabIndex = index;
            cmbSelect.Width = width;
            cmbSelect.Tag = customDataFieldInfo;
            IList<CommonNode> commonNodes = UserEnumHelper.GetCommonNodes(typeof(MatchingMode));
            cmbSelect.Properties.Items.AddRange(commonNodes.ToArray());
            cmbSelect.SelectedIndex = 0;
            gcCondition.Controls.Add(cmbSelect);
        }

        private void AddTreeviewControl(int controlX, int controlY, int width, int index, CustomDataFieldInfo customDataFieldInfo)
        {
            TreeDropdownList treeDropdownList = new TreeDropdownList();
            treeDropdownList.TreeDropdownHandler = new TreeDropdownItems(CustomEnumContract, customDataFieldInfo.EnumId);
            treeDropdownList.CheckBoxes = true;
            treeDropdownList.TabIndex = index;
            treeDropdownList.InitalizeTreeView();
            treeDropdownList.Width = 170;
            treeDropdownList.Location = new Point(controlX, controlY);
            treeDropdownList.SkinName = "Blue";
            treeDropdownList.OnlySelectedLeaf = true;
            gcCondition.Controls.Add(treeDropdownList);
        }

        /// <summary>
        /// 增加标签
        /// </summary>
        /// <param name="text"></param>
        /// <param name="controlX"></param>
        /// <param name="controlY"></param>
        private void AddLabelControl(string text, int controlX, int controlY)
        {
            Label lblName = new Label();
            lblName.Text = text;
            lblName.TextAlign = ContentAlignment.MiddleRight;
            lblName.Width = COMMON_WIDTH_OF_LABEL_TEXT;
            lblName.Location = new Point(controlX, controlY - 2);
            lblName.TabIndex = 0;
            gcCondition.Controls.Add(lblName);
        }


        /// <summary>
        /// 文本框
        /// </summary>
        /// <param name="controlX"></param>
        /// <param name="controlY"></param>
        /// <param name="controlWidth"></param>
        /// <param name="index"></param>
        /// <param name="customDataFieldInfo"></param>
        private void AddTextEditControl(int controlX, int controlY, int controlWidth, int index, CustomDataFieldInfo customDataFieldInfo, string nullValuePrompt)
        {
            TextEdit textEdit = new TextEdit();
            textEdit.Size = new System.Drawing.Size(controlWidth, 21);
            textEdit.Properties.MaxLength = customDataFieldInfo.DataFieldLength;
            textEdit.Properties.NullValuePrompt = nullValuePrompt;
            textEdit.Location = new Point(controlX, controlY);
            textEdit.TabIndex = index;
            textEdit.Properties.MaxLength = 32;
            textEdit.Tag = customDataFieldInfo;
            gcCondition.Controls.Add(textEdit);
        }

        /// <summary>
        /// 多选框
        /// </summary>
        /// <param name="controlX"></param>
        /// <param name="controlY"></param>
        /// <param name="width"></param>
        /// <param name="index"></param>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="commonNodes"></param>
        private void AddCheckedComboBoxEditControl(int controlX, int controlY, int width, int index, CustomDataFieldInfo customDataFieldInfo, IList<CommonNode> commonNodes)
        {
            CheckedComboBoxEdit checkedComboBoxEdit = new CheckedComboBoxEdit();
            checkedComboBoxEdit.Location = new Point(controlX, controlY);
            checkedComboBoxEdit.TabIndex = index;
            checkedComboBoxEdit.Width = width;
            checkedComboBoxEdit.Properties.SelectAllItemVisible = false;
            checkedComboBoxEdit.Properties.ShowButtons = false;
            checkedComboBoxEdit.Properties.PopupSizeable = false;
            checkedComboBoxEdit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            checkedComboBoxEdit.Tag = customDataFieldInfo;
            checkedComboBoxEdit.Properties.Items.AddRange(commonNodes.ToArray());
            gcCondition.Controls.Add(checkedComboBoxEdit);
        }

        /// <summary>
        /// 多选框
        /// </summary>
        /// <param name="controlX"></param>
        /// <param name="controlY"></param>
        /// <param name="width"></param>
        /// <param name="index"></param>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="commonNodes"></param>
        private void AddCheckedComboBoxEditControl(int controlX, int controlY, int width, int index, CustomDataFieldInfo customDataFieldInfo)
        {
            CheckedComboBoxEdit checkedComboBoxEdit = new CheckedComboBoxEdit();
            checkedComboBoxEdit.Location = new Point(controlX, controlY);
            checkedComboBoxEdit.TabIndex = index;
            checkedComboBoxEdit.Width = width;
            checkedComboBoxEdit.Properties.SelectAllItemVisible = false;
            checkedComboBoxEdit.Properties.ShowButtons = false;
            checkedComboBoxEdit.Properties.PopupSizeable = false;
            checkedComboBoxEdit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            checkedComboBoxEdit.Tag = customDataFieldInfo;
            IList<CommonNode> commonNodes = CustomEnumContract.GetChildNodes(customDataFieldInfo.EnumId);
            checkedComboBoxEdit.Properties.Items.AddRange(commonNodes.ToArray());
            gcCondition.Controls.Add(checkedComboBoxEdit);
        }

        #endregion
        
    }
}

