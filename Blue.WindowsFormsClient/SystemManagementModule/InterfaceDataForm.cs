using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary.EventArgument;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class InterfaceDataForm : Form
    {
        #region 契约接口
        
        private readonly ICustomInterfaceContract customInterfaceContract;
        private readonly ICombinedTableContract combinedTableContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomRoleContract customRoleContract;

        #endregion

        #region 属性

        /// <summary>
        /// 接口编号
        /// </summary>
        public decimal InterfaceId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InterfaceDataForm()
        {
            InitializeComponent();
            customInterfaceContract = SystemChannelFactory.CreateCustomInterfaceContract();
            combinedTableContract = BusinessChannelFactory.CreateCombinedTableContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
        }

        #endregion

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InterfaceDataForm_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 清除条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            deStartTime.EditValue = null;
            deEndTime.EditValue = null;
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            devExpressGrid.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                CustomInterfaceInfo customInterfaceInfo = customInterfaceContract.GetModelInfo(InterfaceId);
                if (customInterfaceInfo == null) return;
                IList<ExtendedCustomDataFieldInfo> authorizedDataFieldInfos = GetAuthorizedDataFieldInfos(customInterfaceInfo);
                Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
                foreach (var extendedCustomDataFieldInfo in authorizedDataFieldInfos)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
                    string expressionText = string.Empty;
                    if (dataFieldProperty == DataFieldProperty.LogicalDataField)
                    {
                        LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                        if (logicalDataFieldType == LogicalDataFieldType.DigitExpression || logicalDataFieldType == LogicalDataFieldType.StringExpression
                            || logicalDataFieldType == LogicalDataFieldType.DateTimeExpression)
                        {
                            expressionText = customDataFieldContract.GetDataFieldLogicalExpression(extendedCustomDataFieldInfo.DataFieldId);
                        }
                    }
                    dataFieldNameRelations.Add(extendedCustomDataFieldInfo.PhysicalName,
                            new CommonDataFieldInfo(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.TableId, extendedCustomDataFieldInfo.PhysicalName, extendedCustomDataFieldInfo.LogicalName,
                            expressionText, dataFieldProperty, extendedCustomDataFieldInfo.DataFieldType));
                }
                IList<WhereConditon> whereConditons = GetWhereConditons(customInterfaceInfo, deStartTime.DateTime, deEndTime.DateTime);
                IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("ModificationTime", CustomSorting.Descending));
                DataSet ds = null;
                FormType formType = (FormType)customInterfaceInfo.TableType;
                int count = 0;
                switch (formType)
                {
                    case FormType.Table:
                        count = customTableContract.GetRecordCount(customInterfaceInfo.TableId, whereConditons);
                        ds = customTableContract.GetTableData(customInterfaceInfo.TableId, dataFieldNameRelations, devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex, devExpressGrid.PageSize, whereConditons, sortingCondtions);
                        break;

                    case FormType.CombinedTable:
                        count = customTableContract.GetRecordCount(customInterfaceInfo.CombinedTableId, whereConditons);
                        ds = combinedTableContract.GetTableData(customInterfaceInfo.CombinedTableId, dataFieldNameRelations, devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex,
                    devExpressGrid.PageSize, whereConditons, sortingCondtions);
                        break;

                    default:
                        throw new ArgumentException("不支持该类型。");
                }
                devExpressGrid.DataSource = ds.Tables[0];
                devExpressGrid.RecordCount = count;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 获得条件
        /// </summary>
        /// <param name="customInterfaceInfo"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        private IList<WhereConditon> GetWhereConditons(CustomInterfaceInfo customInterfaceInfo, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

           
            if (customInterfaceInfo.UserTypeContained)
            {
                IList<CommonNode> userTypeCommonNodes = customInterfaceContract.GetUserTypes(customInterfaceInfo.InterfaceId);
                if (userTypeCommonNodes != null)
                {
                    for (int idx = 0; idx < userTypeCommonNodes.Count; idx++)
                    {
                        if (idx == 0)
                        {
                            if (userTypeCommonNodes.Count == 1)
                            {
                                whereConditons.Add(new WhereConditon("UserTypeId", string.Format("UserTypeId_{0}", idx), DbType.Decimal, userTypeCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                            }
                            else
                            {
                                whereConditons.Add(new WhereConditon("UserTypeId", string.Format("UserTypeId_{0}", idx), DbType.Decimal, userTypeCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                            }
                        }
                        else if (idx == userTypeCommonNodes.Count - 1)
                        {
                            whereConditons.Add(new WhereConditon("UserTypeId", string.Format("UserTypeId_{0}", idx), DbType.Decimal, userTypeCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                        }
                        else
                        {
                            whereConditons.Add(new WhereConditon("UserTypeId", string.Format("UserTypeId_{0}", idx), DbType.Decimal, userTypeCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.Or));
                        }
                    }
                }
            }
            if (customInterfaceInfo.DepContained)
            {
                IList<CommonNode> depCommonNodes = customInterfaceContract.GetDepartments(customInterfaceInfo.InterfaceId);
                if (depCommonNodes != null && depCommonNodes.Count > 0)
                {
                    for (int idx = 0; idx < depCommonNodes.Count; idx++)
                    {
                        if (idx == 0)
                        {
                            if (depCommonNodes.Count == 1)
                            {
                                whereConditons.Add(new WhereConditon("DepId", string.Format("DepId_{0}", idx), DbType.Decimal, depCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                            }
                            else
                            {
                                whereConditons.Add(new WhereConditon("DepId", string.Format("DepId_{0}", idx), DbType.Decimal, depCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                            }
                        }
                        else if (idx == depCommonNodes.Count - 1)
                        {
                            whereConditons.Add(new WhereConditon("DepId", string.Format("DepId_{0}", idx), DbType.Decimal, depCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                        }
                        else
                        {
                            whereConditons.Add(new WhereConditon("DepId", string.Format("DepId_{0}", idx), DbType.Decimal, depCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.Or));
                        }
                    }
                }
            }
            if (!DataConvertionHelper.IsNullValue(fromUpdatedTime))
            {
                whereConditons.Add(new WhereConditon("ModificationTime", "ModificationTime_0", DbType.DateTime, fromUpdatedTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And));
            }
            if (!DataConvertionHelper.IsNullValue(toUpdatedTime))
            {
                whereConditons.Add(new WhereConditon("ModificationTime", "ModificationTime_1", DbType.DateTime, toUpdatedTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And));
            }

            return whereConditons;
        }


        /// <summary>
        /// 获得授权的字段列表
        /// </summary>
        /// <param name="customInterfaceInfo"></param>
        /// <returns></returns>
        private IList<ExtendedCustomDataFieldInfo> GetAuthorizedDataFieldInfos(CustomInterfaceInfo customInterfaceInfo)
        {
            IList<ExtendedCustomDataFieldInfo> authorizedDataFieldInfos = null;

            try
            {
                decimal formId = decimal.MinValue;
                FormType formType = (FormType)customInterfaceInfo.TableType;
                switch (formType)
                {
                    case FormType.Table:
                        formId = customInterfaceInfo.TableId;
                        break;

                    case FormType.CombinedTable:
                        formId = customInterfaceInfo.CombinedTableId;
                        break;

                    default:
                        throw new ArgumentException("不支持该类型。");
                }

                IList<decimal> tableIds = new List<decimal>();
                switch (formType)
                {
                    case FormType.Table:
                        tableIds.Add(formId);
                        authorizedDataFieldInfos = customRoleContract.GetAuthorizedExtendedCustomDataFieldInfos(customInterfaceInfo.UserId, tableIds, DataAuthorityType.Query);
                        break;

                    case FormType.CombinedTable:
                        authorizedDataFieldInfos = new List<ExtendedCustomDataFieldInfo>();
                        IList<CommonNode> commonNodeInfos = combinedTableContract.GetTables(formId);
                        foreach (var commonNodeInfo in commonNodeInfos)
                        {
                            tableIds.Add(commonNodeInfo.NodeId);
                        }
                        List<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = customRoleContract.GetAuthorizedExtendedCustomDataFieldInfos(customInterfaceInfo.UserId, tableIds, DataAuthorityType.Query);
                        List<CommonNode> combinedDataFields = combinedTableContract.GetDataFields(formId);
                        foreach (var combinedDataField in combinedDataFields)
                        {
                            int pos = (extendedCustomDataFieldInfos.FindIndex(extendedCustomDataFieldInfo => extendedCustomDataFieldInfo.DataFieldId == combinedDataField.NodeId));
                            authorizedDataFieldInfos.Add(extendedCustomDataFieldInfos[pos]);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return authorizedDataFieldInfos;
        }

        #endregion
    }
}
