using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core.ClientConfig;
using AppFramework.WinFormsLibrary.EventArgument;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.DataConvertionModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.Model.BusinessModule;
using Blue.Model.DataConvertionModule;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class DatatCopyForm : Form
    {
        #region 私有常量

        /// <summary>
        /// 分页大小
        /// </summary>
        private const int PAGE_SIZE = 1000;

        #endregion

        #region 私有变量

        /// <summary>
        /// 私有变量
        /// </summary>
        private ProgressForm frmCommonProgress = null;

        private ConcurrentDictionary<decimal, List<CustomDataFieldInfo>> dicRelation = null;

        private bool validationCompleted = false;

        private static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        private DataRelationInfo dataRelationInfo = null;
        private RemoteDataInfo remoteDataInfo = null;

        private IList<int> checkBoxUnBoundData = new List<int>();

        #endregion

        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ISystemConfigContract systemConfigContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomGroupContract customGroupContract;
        private readonly IDataBussinessContract dataBussinessContract;
        private readonly IDataRelationContract dataRelationContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomTableContract customTableContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly IRemoteDataContract remoteDataContract;
        private readonly IDataAuditingContract dataAuditingContract;
        private IRemoteServerContract remoteServerContract;

        #endregion

        #region 属性

        /// <summary>
        /// 关联编号
        /// </summary>
        public decimal RelationId
        {
            get;
            set;
        }

        /// <summary>
        /// 数据源类型
        /// </summary>
        public DataSourceType DataSourceTypeValue
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DatatCopyForm()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            dataBussinessContract = BusinessChannelFactory.CreateDataBussinessContract();
            dataRelationContract = DataConvertionChannelFactory.CreateDataRelationContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();
            remoteDataContract = DataConvertionChannelFactory.CreateRemoteDataContract();
            dicRelation = new ConcurrentDictionary<decimal, List<CustomDataFieldInfo>>();
            InitErrorDataTable();            
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatatCopyForm_Load(object sender, EventArgs e)
        {
            ImageComboBoxItem item = new ImageComboBoxItem(string.Empty, -1, 0);
            icmbAudit.Properties.Items.Add(item);
            UserControlHelper.InitImageComboBoxEdit(icmbAudit, typeof(AuditedStatus));
            cmbQueriedUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(customGroupContract, userTypeContract);
            cmbQueriedUserType.InitalizeTreeView();

            cmbQueriedDepartment.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
            cmbQueriedDepartment.InitalizeTreeView();

            DataTable dataTable = CreateTableSource();
            gridControlResult.DataSource = dataTable;
            switch (DataSourceTypeValue)
            {
                case DataSourceType.Local:
                    gridResult.Columns[gridResult.Columns.Count - 1].Visible = true;
                    dataRelationInfo = dataRelationContract.GetModelInfo(RelationId);
                    Dictionary<decimal, decimal> tableRelation = dataRelationContract.GetTableRelation(RelationId);
                    foreach (var keyValue in tableRelation)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["SourceId"] = keyValue.Key;
                        dataRow["SourceName"] = customTableContract.GetTableLogicalName(keyValue.Key);
                        dataRow["DestinationId"] = keyValue.Value;
                        dataRow["DestinationName"] = customTableContract.GetTableLogicalName(keyValue.Value);
                        dataRow["RecordCount"] = string.Empty;
                        dataTable.Rows.Add(dataRow);
                    }
                    break;

                case DataSourceType.Remote:
                    gridResult.Columns[gridResult.Columns.Count - 1].Visible = true;
                    remoteDataInfo = remoteDataContract.GetModelInfo(RelationId);
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        remoteServerContract = RemoteChannelFactory.CreateRemoteServerContract(remoteDataInfo.RemoteAddress, CurrentConfig.Instance.Port);
                        if (!remoteServerContract.ValidateUser(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword))
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("远程交换用户名密码错误。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Close();
                        }
                        Cursor = Cursors.Default;
                    }
                    catch (Exception exception)
                    {
                        Cursor = Cursors.Default;
                        //记录日志, 抛出异常, 不包装异常 
                        WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                        this.Close();
                    }
                    Dictionary<decimal, Dictionary<decimal, decimal>> relations = remoteDataContract.GetTableRelation(remoteDataInfo.RemoteDataId);
                    Dictionary<decimal, decimal> tableIds = new Dictionary<decimal, decimal>();
                    foreach (var kvTableId in relations)
                    {
                        decimal dataFieldId = kvTableId.Value.First().Value;
                        decimal tableId = remoteServerContract.GetTableId(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, dataFieldId);
                        if (!tableIds.ContainsKey(tableId))
                        {
                            tableIds.Add(tableId, kvTableId.Key);
                        }
                    }
                    IList<CommonNode> commonNodes = remoteServerContract.GetCommonNodesByDatabaseId(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, remoteDataInfo.RemoteDatabaseId);
                    foreach (CommonNode node in commonNodes)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["SourceId"] = node.NodeId;
                        dataRow["SourceName"] = node.NodeName;
                        if (tableIds.ContainsKey(node.NodeId))
                        {
                            dataRow["DestinationId"] = tableIds[node.NodeId];
                            dataRow["DestinationName"] = customTableContract.GetTableLogicalName(tableIds[node.NodeId]);
                            dataRow["RecordCount"] = string.Empty;
                        }
                        else
                        {
                            dataRow["DestinationId"] = 0;
                            dataRow["RecordCount"] = string.Empty;
                            dataRow["DestinationName"] = string.Empty;
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    break;
            }
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                BackgroundWorker backgroundWorker = new BackgroundWorker()
                {
                    WorkerSupportsCancellation = true,
                    WorkerReportsProgress = true
                };                
                Dictionary<decimal, decimal> tableRelation = dataRelationContract.GetTableRelation(RelationId);
                InitCommonProgress("", 0, tableRelation.Count);
                frmCommonProgress.TaskCancelled = delegate ()
                {
                    backgroundWorker.CancelAsync();
                };
                backgroundWorker.DoWork += (workSender, ea) =>
                {
                    var worker = workSender as BackgroundWorker;
                    dicRelation.Clear();
                    int idx = 0;
                    foreach (var keyValue in tableRelation)
                    {
                        Dictionary<decimal, decimal> dataFieldRelation = dataRelationContract.GetDataFieldRelation(RelationId, keyValue.Key, keyValue.Value);
                        foreach (var dataFieldPair in dataFieldRelation)
                        {
                            if (worker.CancellationPending)
                            {
                                ea.Cancel = true;
                                break;
                            }
                            CustomDataFieldInfo sourceCustomDataFieldInfo = null;
                            switch (DataSourceTypeValue)
                            {
                                case DataSourceType.Local:
                                    sourceCustomDataFieldInfo = customDataFieldContract.GetModelInfo(dataFieldPair.Key);
                                    break;

                                case DataSourceType.Remote:
                                    sourceCustomDataFieldInfo = remoteServerContract.GetModelInfoByDataFieldId(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, dataFieldPair.Key);
                                    break;
                            }
                            CustomDataFieldInfo destCustomDataFieldInfo = customDataFieldContract.GetModelInfo(dataFieldPair.Value);
                            PhysicalDataFieldType destType = (PhysicalDataFieldType)destCustomDataFieldInfo.DataFieldType;
                            BasedDataType sourceBasedDataType = BasedDataType.Boolean;
                            DataFieldProperty dataFieldProperty = (DataFieldProperty)sourceCustomDataFieldInfo.DataFieldProperty;
                            int sourceLength = 0;
                            switch (dataFieldProperty)
                            {
                                case DataFieldProperty.PhysicalDataField:
                                    PhysicalDataFieldType sourceType = (PhysicalDataFieldType)sourceCustomDataFieldInfo.DataFieldType;
                                    if (sourceType == PhysicalDataFieldType.Association || sourceType == PhysicalDataFieldType.PrimaryAssociation || sourceType == PhysicalDataFieldType.SecondaryAssociation)
                                    {
                                        AssociatedDataFieldInfo associatedDataFieldInfo = associatedDataFieldContract.GetModelInfo(sourceCustomDataFieldInfo.AssociatedDataFieldId);
                                        sourceBasedDataType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                                        sourceLength = associatedDataFieldInfo.DataLength;
                                    }
                                    else
                                    {
                                        sourceBasedDataType = DataFieldHelper.GetBasedDataType(sourceType);
                                        sourceLength = sourceCustomDataFieldInfo.DataFieldLength;
                                    }
                                    break;

                                case DataFieldProperty.LogicalDataField:
                                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)sourceCustomDataFieldInfo.DataFieldType;
                                    sourceBasedDataType = DataFieldHelper.GetBasedDataType(logicalDataFieldType);
                                    switch (logicalDataFieldType)
                                    {
                                        case LogicalDataFieldType.StringExpression:
                                            sourceLength = 0;
                                            break;

                                        case LogicalDataFieldType.DigitExpression:
                                            sourceLength = 2;
                                            break;
                                    }
                                    break;
                            }
                            BasedDataType destBasedDataType = BasedDataType.Boolean;
                            int destLength = 0;
                            if (destType == PhysicalDataFieldType.PrimaryAssociation || destType == PhysicalDataFieldType.SecondaryAssociation)
                            {
                                AssociatedDataFieldInfo associatedDataFieldInfo = associatedDataFieldContract.GetModelInfo(destCustomDataFieldInfo.AssociatedDataFieldId);
                                destBasedDataType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                                destLength = associatedDataFieldInfo.DataLength;
                            }
                            else
                            {
                                destBasedDataType = DataFieldHelper.GetBasedDataType(destType);
                                destLength = destCustomDataFieldInfo.DataFieldLength;
                            }
                            if (!DataFieldHelper.CheckCompatibility(sourceBasedDataType, sourceLength, destBasedDataType, destLength))
                            {
                                List<CustomDataFieldInfo> customDataFieldInfos = new List<CustomDataFieldInfo>();
                                if (!dicRelation.ContainsKey(keyValue.Key))
                                {
                                    customDataFieldInfos = new List<CustomDataFieldInfo>();
                                    dicRelation.TryAdd(keyValue.Key, customDataFieldInfos);
                                }
                                else
                                {
                                    customDataFieldInfos = dicRelation[keyValue.Key];
                                }
                                customDataFieldInfos.Add(sourceCustomDataFieldInfo);
                                break;
                            }
                        }                        
                        worker.ReportProgress(++idx);
                        manualResetEvent.WaitOne();
                    }
                };
                backgroundWorker.ProgressChanged += (workSender, ea) =>
                {
                    frmCommonProgress.IncreaseStep();
                    manualResetEvent.Set();
                };
                backgroundWorker.RunWorkerCompleted += (workSender, ea) =>
                {
                    frmCommonProgress.CloseFrom();
                    if (ea.Cancelled)
                    {
                        validationCompleted = false;
                        Cursor = Cursors.Default;
                        MessageBox.Show("已取消表的字段对应关系验证。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        validationCompleted = true;
                        if (dicRelation.Count > 0)
                        {
                            DataTable dataTable = (DataTable)gridControl.DataSource;
                            dataTable.Rows.Clear();
                            foreach (KeyValuePair<decimal, List<CustomDataFieldInfo>> keyValue in dicRelation)
                            {
                                string tableName = customTableContract.GetTableLogicalName(keyValue.Key);
                                foreach (CustomDataFieldInfo dataFieldInfo in keyValue.Value)
                                {
                                    DataRow dataRow = dataTable.NewRow();
                                    dataRow[0] = tableName;
                                    dataRow[1] = dataFieldInfo.LogicalName;
                                    dataRow[2] = dataFieldInfo.PhysicalName;
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                            gridControl.RefreshDataSource();
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}数据表的字段对应关系存在问题。", dicRelation.Count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("表的字段对应关系验证通过。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                };
                backgroundWorker.RunWorkerAsync();
                frmCommonProgress.ShowDialog();
            }
            catch (Exception exception)
            {
                frmCommonProgress.CloseFrom();
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认导入数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }
                if (!validationCompleted)
                {
                    MessageBox.Show("请先完成校验。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dicRelation.Count > 0)
                {
                    MessageBox.Show("表的对应关系中存在字段对应关系不兼容。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                IList<WhereConditon> whereConditons = GetWhereConditons();
                Cursor = Cursors.WaitCursor;
                switch (DataSourceTypeValue)
                {
                    case DataSourceType.Local:
                        dataRelationContract.Import(RelationId, whereConditons);
                        Cursor = Cursors.Default;
                        meResult.Text = "转入成功。";
                        MessageBox.Show("转入成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case DataSourceType.Remote:
                        DataTable dataTable = (DataTable)gridControlResult.DataSource;
                        IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                        sortingCondtions.Add(new SortingCondtion("CreationTime", CustomSorting.Ascending));                        
                        List<string> userNames = new List<string>();
                        Dictionary<decimal, Dictionary<decimal, decimal>> relations = remoteDataContract.GetTableRelation(remoteDataInfo.RemoteDataId);
                        foreach (var relation in relations)
                        {     
                            Dictionary<string, string> dataFieldRelation = new Dictionary<string, string>();
                            DateTime defaultTime = DateTime.Parse(AppSettingHelper.YearMonthDay);
                            IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = new List<ExtendedCustomDataFieldInfo>();
                            decimal dataFieldId = relation.Value.First().Value;
                            decimal tableId = remoteServerContract.GetTableId(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, dataFieldId);
                            DataRow[] drs = dataTable.Select(string.Format("DestinationId={0} AND SourceId={1}", relation.Key, tableId));                                
                            if (drs != null && drs.Length > 0)
                            {
                                int rowIndex = dataTable.Rows.IndexOf(drs[0]);
                                if (rowIndex >= 0 && checkBoxUnBoundData.Contains(rowIndex))
                                {
                                    customTableContract.TruncatedTable(relation.Key);
                                }
                            }
                            /*目标字段编号与远程的源字段对象 */
                            Dictionary<decimal, CustomDataFieldInfo> attachments = new Dictionary<decimal, CustomDataFieldInfo>();
                            foreach (var dataField in relation.Value)
                            {
                                CustomDataFieldInfo customDataFieldInfo = remoteServerContract.GetModelInfoByDataFieldId(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, dataField.Value);
                                if (customDataFieldInfo == null)
                                {
                                    continue;
                                }
                                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                                string name = customDataFieldInfo.PhysicalName;
                                switch (dataFieldProperty)
                                {
                                    case DataFieldProperty.LogicalDataField:
                                        IList<CommonNode> commonNodes = remoteServerContract.GetExpressionCommonNodes(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, customDataFieldInfo.DataFieldId);
                                        customDataFieldInfo.PhysicalName = remoteServerContract.GetExpressionDataFieldName(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, string.Empty, customDataFieldInfo.ExpressionText, commonNodes);
                                        break;

                                    case DataFieldProperty.PhysicalDataField:
                                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                                        if (((physicalDataFieldType == PhysicalDataFieldType.DocAttachment || physicalDataFieldType == PhysicalDataFieldType.PDFAttachment
                                            || physicalDataFieldType == PhysicalDataFieldType.PicAttachment)) && !attachments.ContainsKey(dataField.Key))
                                        {
                                            attachments.Add(dataField.Key, customDataFieldInfo);
                                        }
                                        break;
                                }
                                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = new ExtendedCustomDataFieldInfo(customDataFieldInfo.DataFieldId, customDataFieldInfo.EnumId, customDataFieldInfo.ParentDataFieldId, customDataFieldInfo.AssociatedDataFieldId, customDataFieldInfo.TableId, 
                                    customDataFieldInfo.LogicalName, customDataFieldInfo.PhysicalName, customDataFieldInfo.DataFieldCode, customDataFieldInfo.DataFieldProperty, customDataFieldInfo.DataFieldType, 
                                    customDataFieldInfo.DataFieldLength, customDataFieldInfo.BasedDataType, customDataFieldInfo.RegexExpression, customDataFieldInfo.ExpressionText, customDataFieldInfo.DataFieldSetting, customDataFieldInfo.RequiredDataField, 
                                    customDataFieldInfo.AutoComplete, customDataFieldInfo.IndexCreated, customDataFieldInfo.HelpEnabled, customDataFieldInfo.HelpContent, customDataFieldInfo.Tooltip, 
                                    customDataFieldInfo.Sorting, customDataFieldInfo.Notes, name, 0);
                                extendedCustomDataFieldInfos.Add(extendedCustomDataFieldInfo);
                                string destPhysicalName = customDataFieldContract.GetPhysicalName(dataField.Key);
                                dataFieldRelation.Add(name, destPhysicalName);
                            }
                            string key1 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
                            dataFieldRelation.Add(key1, key1);
                            string key2 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName);
                            dataFieldRelation.Add(key2, key2);
                            string key3 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.DepId);
                            dataFieldRelation.Add(key3, key3);
                            string key4 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserTypeId);
                            dataFieldRelation.Add(key4, key4);
                            string key5 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordSorting);
                            dataFieldRelation.Add(key5, key5);
                            string key6 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.IsDeleted);
                            dataFieldRelation.Add(key6, key6);
                            string key7 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus);
                            dataFieldRelation.Add(key7, key7);
                            string key8 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState);
                            dataFieldRelation.Add(key8, key8);
                            string key9 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime);
                            dataFieldRelation.Add(key9, key9);
                            string key10 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime);
                            dataFieldRelation.Add(key10, key10);
                            int count = remoteServerContract.GetTableRecordCount(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, tableId, whereConditons);
                            int pageSize = count / PAGE_SIZE;
                            if (count % PAGE_SIZE != 0)
                            {
                                pageSize++;
                            }
                            for (int idx = 0; idx < pageSize; idx++)
                            {
                                DataSet ds = remoteServerContract.GetPageRecord(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, tableId, extendedCustomDataFieldInfos,
                                    idx * PAGE_SIZE, PAGE_SIZE, whereConditons, sortingCondtions);
                                if (ds != null && ds.Tables.Count > 0)
                                {
                                    bool delete = false;
                                    ds.Tables[0].Columns.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId), typeof(Decimal));
                                    ds.Tables[0].Columns.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserTypeId), typeof(Decimal));
                                    ds.Tables[0].Columns.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.DepId), typeof(Decimal));
                                    ds.Tables[0].Columns.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordSorting), typeof(Int32));
                                    ds.Tables[0].Columns.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.IsDeleted), typeof(Boolean));
                                    ds.Tables[0].Columns.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime), typeof(DateTime));
                                    ds.Tables[0].Columns.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime), typeof(DateTime));

                                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                                    {
                                        string userName = Convert.ToString(dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName)]);
                                        if (!userNames.Contains(userName))
                                        {
                                            CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(userName);
                                            if (commonUserInfo != null)
                                            {
                                                dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId)] = commonUserInfo.UserId;
                                                dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserTypeId)] = commonUserInfo.UserTypeId;
                                                dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.DepId)] = commonUserInfo.DepId;
                                                dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordSorting)] = 0;
                                                dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.IsDeleted)] = false;
                                                dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime)] = defaultTime;
                                                dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime)] = defaultTime;
                                            }
                                            else
                                            {
                                                userNames.Add(userName);
                                                delete = true;
                                                dataRow.Delete();
                                            }
                                        }
                                        else
                                        {
                                            delete = true;
                                            dataRow.Delete();
                                        }
                                    }
                                    if (delete)
                                    {
                                        ds.Tables[0].AcceptChanges();
                                    }
                                    customTableContract.Import(relation.Key, ds.Tables[0], dataFieldRelation);
                                    foreach (var attachment in attachments)
                                    {
                                        if (ds.Tables[0].Columns.Contains(attachment.Value.PhysicalName))
                                        {
                                            string physicalName = customDataFieldContract.GetPhysicalName(attachment.Key);
                                            foreach (DataRow dr in ds.Tables[0].Rows)
                                            {
                                                string attachmentName = DataConvertionHelper.GetString(dr[attachment.Value.PhysicalName]);
                                                if (!string.IsNullOrWhiteSpace(attachmentName))
                                                {
                                                    byte[] data = remoteServerContract.GetFileData(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, attachment.Value.PhysicalName, attachmentName);
                                                    dataAuditingContract.SaveUploadFiles(new UpLoadFileInfo(attachmentName, string.Empty, data), physicalName);
                                                }
                                            }
                                        }
                                    }
                                }
                            }                            
                        }
                        if (userNames.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("以下用户不存在：");
                            foreach (string name in userNames)
                            {
                                sb.AppendFormat("{0}, ", name);
                            }
                            sb.Remove(sb.Length - 2, 2);
                            sb.Append("。");
                            meResult.Text = sb.ToString();
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            meResult.Text = "转入成功。";
                            MessageBox.Show("转入成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IList<WhereConditon> whereConditons = GetWhereConditons();
                DataTable dataTable = (DataTable)gridControlResult.DataSource;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string destinationName = Convert.ToString(dataRow["DestinationName"]);
                    if (string.IsNullOrWhiteSpace(destinationName))
                    {
                        continue;
                    }
                    decimal tableId = (decimal)dataRow["SourceId"];
                    int count = 0;
                    switch (DataSourceTypeValue)
                    {
                        case DataSourceType.Local:
                            count = dataBussinessContract.GetTableRecordCount(tableId, whereConditons);
                            break;

                        case DataSourceType.Remote:
                            count = remoteServerContract.GetTableRecordCount(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, tableId, whereConditons);
                            break;
                    }
                    dataRow["RecordCount"] = count;
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnClear_Click(object sender, EventArgs e)
        {
            ClearSystemCondition();
            DataTable dataTable = (DataTable)gridControlResult.DataSource;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["RecordCount"] = string.Empty;
            }
        }

        /// <summary>
        /// 显示校验结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiResult_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (validationCompleted)
            {
                int count = GetCellErrorCount();
                if (count == 0)
                {
                    lblErrorTip.Text = "校验正确。";
                }
                else
                {
                    lblErrorTip.Text = string.Format("错误单元格数共有：{0}个。", count);
                }

                if (fpError.FlyoutPanelState.IsActive)
                {
                    return;
                }
                fpError.ShowBeakForm();
            }
            else
            {
                MessageBox.Show("请先进行数据校验。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 隐藏错误提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpError_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            fpError.HideBeakForm();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 创建源表
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTableSource()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SourceId", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("SourceName", Type.GetType("System.String"));
            dataTable.Columns.Add("DestinationId", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("DestinationName", Type.GetType("System.String"));
            dataTable.Columns.Add("RecordCount", Type.GetType("System.String"));
            return dataTable;
        }

        /// <summary>
        /// 获得单元格校验错误总数
        /// </summary>
        /// <returns></returns>
        private int GetCellErrorCount()
        {
            int count = 0;

            foreach (var keyValue in dicRelation)
            {
                count += keyValue.Value.Count;
            }

            return count;
        }

        /// <summary>
        /// 初始化错误结果表
        /// </summary>
        private void InitErrorDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("TableName", Type.GetType("System.String"));
            dataTable.Columns.Add("DataFieldLogicalName", Type.GetType("System.String"));
            dataTable.Columns.Add("DataFieldPhysicalName", Type.GetType("System.String"));
            gridControl.DataSource = dataTable;
            gridView.Columns[0].Caption = "源表名称";
            gridView.Columns[1].Caption = "源字段逻辑名称";
            gridView.Columns[2].Caption = "源字段物理名称";
            gridView.Columns[0].Width = 80;
            gridView.Columns[1].Width = 80;
            gridView.Columns[2].Width = 40;
        }

        /// <summary>
        /// 初始化进度条
        /// </summary>
        /// <param name="text"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        private void InitCommonProgress(string text, int minValue, int maxValue)
        {
            frmCommonProgress = new ProgressForm();
            frmCommonProgress.Text = text;
            frmCommonProgress.MinValue = minValue;
            frmCommonProgress.MaxValue = maxValue;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearSystemCondition()
        {
            txtUserName.Text = string.Empty;
            cmbQueriedUserType.SelectedNode = null;
            cmbQueriedUserType.SelectedNode = null;
            icmbAudit.SelectedIndex = 0;
            deStartTime.EditValue = null;
            deEndTime.EditValue = null;
            deFromTime.EditValue = null;
            deToTime.EditValue = null;
        }

        /// <summary>
        /// 获得查询条件
        /// </summary>
        /// <returns></returns>
        private IList<WhereConditon> GetWhereConditons()
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

            string userName = txtUserName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                whereConditons.Add(new WhereConditon("UserName", "UserName",
                    DbType.String, userName, DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (icmbAudit.SelectedIndex > 0)
            {
                byte auditedStatus = Convert.ToByte(icmbAudit.EditValue);
                whereConditons.Add(new WhereConditon("AuditedStatus", "AuditedStatus", DbType.Byte,
                    auditedStatus, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            /* 用户类型 */
            CommonNode userTypeCommonNode = cmbQueriedUserType.Value as CommonNode;
            if (userTypeCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("UserTypeId", "UserTypeId", DbType.Decimal, userTypeCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            /* 单位类型 */
            CommonNode departmentCommonNode = cmbQueriedDepartment.Value as CommonNode;
            if (departmentCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("DepId", "DepId", DbType.Decimal, departmentCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deStartTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("CreationTime", "CreationTime0", DbType.DateTime,
                    deStartTime.DateTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deEndTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("CreationTime", "CreationTime1", DbType.DateTime,
                    deEndTime.DateTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deFromTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("ModificationTime", "ModificationTime0", DbType.DateTime,
                    deFromTime.DateTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deToTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("ModificationTime", "ModificationTime1", DbType.DateTime,
                    deToTime.DateTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            return whereConditons;
        }

        #endregion

        /// <summary>
        /// 未绑定行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridResult_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                if (checkBoxUnBoundData.Contains(e.ListSourceRowIndex))
                {
                    e.Value = true;
                }
                else
                {
                    e.Value = false;
                }
            }
            else
            {

                if (Convert.ToBoolean(e.Value))
                {
                    if (!checkBoxUnBoundData.Contains(e.ListSourceRowIndex))
                    {
                        checkBoxUnBoundData.Add(e.ListSourceRowIndex);
                    }
                }
                else
                {
                    if (checkBoxUnBoundData.Contains(e.ListSourceRowIndex))
                    {
                        checkBoxUnBoundData.Remove(e.ListSourceRowIndex);
                    }
                }
            }
        }
        
    }
}
