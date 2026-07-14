using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Design;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using DevExpress.XtraEditors.Controls;
using AppFramework.CustomModule.ConfigModule.AppSettings;
using AppFramework.CustomSystem.CustomClass;
using AppFramework.CustomSystem.CustomEnum;
using AppFramework.CustomLibrary.CommonLibrary;
using AppFramework.WinFormsLibrary.CustomAssistant;
using AppFramework.WinFormsControls.CustomWindowsControls.CusmtomComboxTreeView;
using Promotion.WindowsFormsClient.Common.CommonForm;
using Promotion.ApplicationLibrary.ApplicationClass;
using Promotion.ApplicationLibrary.ApplicationEnum;
using Promotion.Model.BusinessModule;
using Promotion.Model.DataModule;
using Promotion.Model.AudtingModule;
using Promotion.WindowsFormsClient.BusinessModule.BusinessClass;
using Promotion.WCFContracts;
using Promotion.WCFContracts.BusinessModule;
using Promotion.WCFContracts.AudtingModule;
using Promotion.WCFContracts.DataModule;
using Promotion.WCFContracts.ReportingModule;
using Promotion.WCFContracts.UserModule;
using Promotion.WindowsFormsClient.ReportingModule;

namespace Promotion.WindowsFormsClient.DataModule
{
    public partial class DataConvertionForm : Form
    {
        #region 私有变量

        private readonly DataTableHandler<TreeNode> sourceTableHandler;
        private readonly DataTableHandler<TreeNode> destTableHandler;
        private readonly UserTypeHandler<TreeNode> userTypeHandler;
        private readonly DepartmentHandler<TreeNode> departmentHandler;
        private readonly CompositegTableClassHandler<TreeNode> compositegTableClassHandler;
        private int sourceDataWarehouseId = 0;
        private int destDataWarehouseId = 0;
        private decimal sourceTableId = 0;
        private decimal destTableId = 0;
        private RowColSwap currentRowColSwap;
        private IDictionary<decimal, decimal> dataFieldRelations = new Dictionary<decimal, decimal>();
        private IDictionary<decimal, string> customDataFieldNames = new Dictionary<decimal, string>();
        private string reviewedConclusion = AppSettingHelper.GetAppSettingByName("ZYJS_REVIEWED_CONCLUSION");

        private ProgressForm frmProgress = null;
        
        #endregion

        #region 契约接口

        private readonly ICustomDatabaseContract customDatabaseContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomExpressionContract customExpressionContract;
        private readonly ICustomGroupContract customGroupContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly IDepartmentContract departmentContract;
        private readonly IAuthorizedDataContract authorizedDataContract;
        private readonly ICorrespondingDataFieldContract correspondingDataFieldContract;
        private readonly IUserDataRelationContract userDataRelationContract;
        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomSheetContract customSheetContract;
        private readonly ICustomReportClassContract customReportClassContract;
        private readonly ICustomReportContract customReportContract;
        private ICustomDataContract customDataContract = null;

        #endregion

        #region 构造函数

        public DataConvertionForm()
        {
            InitializeComponent();
            authorizedDataContract = AudtingBusinessChannelFactory.CreateCustomDatabaseContract();
            customDatabaseContract = BusinessChannelFactory.CreateCustomDatabaseContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customExpressionContract = BusinessChannelFactory.CreateRelatedDataFieldContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            userTypeContract = BusinessChannelFactory.CreateUserTypeContract();
            departmentContract = BusinessChannelFactory.CreateDepartmentContract();
            userDataRelationContract = DataChannelFactory.CreateUserDataRelationContract();
            customSheetContract = ReportingChannelFactory.CreateCustomSheetContract();
            correspondingDataFieldContract = DataChannelFactory.CreateCorrespondingDataFieldContract();            
            userAccountContract = UserChannelFactory.CreateUserAccount();
            sourceTableHandler = new DataTableHandler<TreeNode>(customGroupContract, customTableContract, ecmbSourceTable.TreeView);
            destTableHandler = new DataTableHandler<TreeNode>(customGroupContract, customTableContract, ecmbDestTable.TreeView);
            userTypeHandler = new UserTypeHandler<TreeNode>(userTypeContract, ecmbUserType.TreeView);
            departmentHandler = new DepartmentHandler<TreeNode>(departmentContract, ecmbDepartment.TreeView);
            customReportClassContract = BusinessChannelFactory.CreateCustomReportClassContract();
            customReportContract = BusinessChannelFactory.CreateCustomReportContract();
            compositegTableClassHandler = new CompositegTableClassHandler<TreeNode>(customReportClassContract,
                customReportContract, false, false, devExpreesComoboxTreeview.TreeView);
        }

        #endregion

        #region 窗体和控件方法

        private void DataConvertionForm_Load(object sender, EventArgs e)
        {            
            this.WindowState = FormWindowState.Maximized;
            ecmbAudit.Properties.Items.Add(new CommonNode(-1, string.Empty));
            EnumDescription[] auditedStates = EnumDescription.GetFieldTexts(typeof(AuditedState));
            ecmbAudit.Properties.Items.AddRange(auditedStates);
            ecmbAudit.SelectedIndex = 0;
            EnumDescription[] descriptions = EnumDescription.GetFieldTexts(typeof(DataImportType));
            cmbDataImportType.Properties.Items.AddRange(descriptions);
            cmbDataImportType.SelectedIndex = 0;
            EnumDescription[] rowColSwaps = EnumDescription.GetFieldTexts(typeof(RowColSwap));
            cmbImport.Properties.Items.AddRange(rowColSwaps);
            cmbImport.SelectedIndex = 0;
            sourceTableHandler.InitPartialTree();
            destTableHandler.InitPartialTree();
            userTypeHandler.LoadFullTree();
            departmentHandler.LoadPartialTree();

            compositegTableClassHandler.InitPartialTree();

            IList<UserDataRelationInfo> userDataRelationInfos = userDataRelationContract.GetModeInfos(DataRelationType.DataConvertion);
            LoadData(userDataRelationInfos);
            gcDataFieldRelation.DataSource = CreateDataTable();

            ITestConnectionContract testConnectionContract = TestConnectionChannelFacotry.CreateTestConnection();
            SoftwareVersion softwareVersion = testConnectionContract.GetRegisterInfo().SoftwareVersion;
            switch (softwareVersion)
            {
                case SoftwareVersion.Trial:
                case SoftwareVersion.Basical:
                    xtData.TabPages[1].PageVisible = false;
                    break;
            }

            mtxtSource.Text = AppSettingHelper.SourceSQL;
            mtxtDestination.Text = AppSettingHelper.DestinationSQL;
        }

        private void LoadData(IList<UserDataRelationInfo> userDataRelationInfos)
        {
            cmbUserDataRelation.Properties.Items.Clear();            
            if (userDataRelationInfos != null && userDataRelationInfos.Count > 0)
            {
                foreach (UserDataRelationInfo userDataRelationInfo in userDataRelationInfos)
                {
                    CommonNode commonNode = new CommonNode(userDataRelationInfo.UserDataRelationId, userDataRelationInfo.UserDataRelationName);
                    cmbUserDataRelation.Properties.Items.Add(commonNode);
                }
                cmbUserDataRelation.SelectedIndex = 0;                
            }
        }
      
        private void ecmbDepartment_AfterTreeNodeExpand(object sender, TreeViewEventArgs e)
        {
            departmentHandler.TreeNodeExpand(e.Node);
        }

        #endregion

        /// <summary>
        /// 获得
        /// </summary>
        /// <returns></returns>
        private IList<WhereConditon> GetWhereConditons()
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

            string userName = etxtUserName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                whereConditons.Add(new WhereConditon("UserName", "UserName",
                    System.Data.DbType.String, userName, DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (ecmbAudit.SelectedIndex > 0)
            {
                EnumDescription enumDescription = ecmbAudit.SelectedItem as EnumDescription;
                whereConditons.Add(new WhereConditon("Auditing", "Auditing", System.Data.DbType.Byte,
                    (byte)enumDescription.EnumValue, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (ecmbUserType.SelectedNode != null)
            {
                CommonNode commonNode = ecmbUserType.SelectedNode.Tag as CommonNode;
                whereConditons.Add(new WhereConditon("UserTypeId", "UserTypeId", System.Data.DbType.Decimal,
                    commonNode.NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (ecmbDepartment.SelectedNode != null)
            {
                CommonNode commonNode = ecmbDepartment.SelectedNode.Tag as CommonNode;
                whereConditons.Add(new WhereConditon("DepID", "DepID", System.Data.DbType.Decimal,
                    commonNode.NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deStartTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("CreationTime", "CreationTime0", System.Data.DbType.DateTime,
                    deStartTime.DateTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deEndTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("CreationTime", "CreationTime1", System.Data.DbType.DateTime,
                    deEndTime.DateTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deFromTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("ModificationTime", "ModificationTime0", System.Data.DbType.DateTime,
                    deFromTime.DateTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deToTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("ModificationTime", "ModificationTime1", System.Data.DbType.DateTime,
                    deToTime.DateTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            return whereConditons;
        }

        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;                
                IList<WhereConditon> whereConditons = GetWhereConditons();  
                DataTable dataTable = (DataTable)gridControlResult.DataSource;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    decimal tableId = (decimal)dataRow["SourceId"];
                    int count = authorizedDataContract.GetTableRecordCount(tableId, whereConditons);
                    dataRow["RecordCount"] = count;
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicy(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUserDataRelation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUserDataRelation.SelectedItem != null)
            {
                CommonNode commonNode = (CommonNode)cmbUserDataRelation.SelectedItem;
                IList<CorrespondingDataFieldInfo> correspondingDataFieldInfos = correspondingDataFieldContract.GetModeInfos(commonNode.NodeId);
                cmbUserDataRelation.Tag = correspondingDataFieldInfos;
                if (correspondingDataFieldInfos.Count > 0)
                {
                    decimal sourceDatabaseId = customDataFieldContract.GetDatabaseId(correspondingDataFieldInfos[0].DataFieldId);
                    decimal destinationDatabaseId = customDataFieldContract.GetDatabaseId(correspondingDataFieldInfos[0].ParentDataFieldId);
                    CustomDatabaseInfo sourceCustomDatabaseInfo = customDatabaseContract.GetModeInfo(sourceDatabaseId);
                    CustomDatabaseInfo destinationCustomDatabaseInfo = customDatabaseContract.GetModeInfo(destinationDatabaseId);
                    lblConvertion.Text = string.Format("{0} -> {1}", sourceCustomDatabaseInfo.DatabaseName, destinationCustomDatabaseInfo.DatabaseName);
                    IDictionary<decimal, decimal> tableRelation = correspondingDataFieldContract.GetTableRelation(commonNode.NodeId);
                    /* 源数据库对应的表 */
                    DataTable dataTable = CreateTableSource();
                    gridControlResult.DataSource = dataTable;
                    IList<CommonNode> commonNodes = customTableContract.GetCommonNodes(sourceDatabaseId, DataTableShow.Visible);
                    foreach (CommonNode node in commonNodes)
                    {                        
                        if (tableRelation.ContainsKey(node.NodeId))
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["SourceId"] = node.NodeId;
                            dataRow["SourceName"] = node.NodeName;
                            dataRow["DestinationName"] = customTableContract.GetTableLogicalName(tableRelation[node.NodeId]);
                            dataRow["RecordCount"] = string.Empty;
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTableSource()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SourceId", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("SourceName", Type.GetType("System.String"));
            dataTable.Columns.Add("DestinationName", Type.GetType("System.String"));
            dataTable.Columns.Add("RecordCount", Type.GetType("System.String"));            

            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkbtnSetting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SwapRelationForm frmSwapRelationForm = new SwapRelationForm();
            frmSwapRelationForm.RefresehRelation = (userDataRelationInfos) =>
                {
                    int selectedIndex = 0;
                    if (cmbUserDataRelation.Properties.Items.Count > 0)
                    {
                        selectedIndex = cmbUserDataRelation.SelectedIndex;
                    }
                    LoadData(userDataRelationInfos);
                    cmbUserDataRelation.SelectedIndex = selectedIndex;
                };
            frmSwapRelationForm.ShowDialog();
        }

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
        /// 
        /// </summary>
        private void ClearSystemCondition()
        {
            etxtUserName.Text = string.Empty;
            ecmbUserType.SelectedNode = null;
            ecmbDepartment.SelectedNode = null;
            ecmbAudit.SelectedIndex = 0;
            deStartTime.EditValue = null;
            deEndTime.EditValue = null;
            deFromTime.EditValue = null;
            deToTime.EditValue = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridResult_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnVerify_Click(object sender, EventArgs e)
        {
            if (cmbUserDataRelation.Tag == null)
            {
                MessageBox.Show("请先选择关系名称！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Cursor = Cursors.Default;
                IList<CorrespondingDataFieldInfo> correspondingDataFieldInfos = (IList<CorrespondingDataFieldInfo>)cmbUserDataRelation.Tag;
                foreach (CorrespondingDataFieldInfo correspondingDataFieldInfo in correspondingDataFieldInfos)
                {
                    if (!customDataFieldContract.ValidatePhysicalDataFieldCompatibility(correspondingDataFieldInfo.DataFieldId, correspondingDataFieldInfo.ParentDataFieldId))
                    {
                        CustomDataFieldInfo customDataFieldInfo = customDataFieldContract.GetModeInfo(correspondingDataFieldInfo.DataFieldId);
                        string tableName = customTableContract.GetTableLogicalName(customDataFieldInfo.TableId);
                        Cursor = Cursors.Default; 
                        MessageBox.Show(string.Format("源数据表[{0}]中字段名[{1}]与其对应目的字段的数据类型不兼容！", tableName, customDataFieldInfo.LogicalName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                CommonNode commonNode = cmbUserDataRelation.SelectedItem as CommonNode;
                Cursor = Cursors.Default;
                MessageBox.Show(string.Format("{0}中表的字段对应关系验证通过！", commonNode.NodeName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicy(exception);
            }
        }


        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认导入数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }
                if (cmbUserDataRelation.SelectedItem == null || cmbUserDataRelation.Tag == null)
                {
                    MessageBox.Show("请先选择关系名称！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Cursor = Cursors.WaitCursor;
                bool result = true;
                IList<CorrespondingDataFieldInfo> correspondingDataFieldInfos = (IList<CorrespondingDataFieldInfo>)cmbUserDataRelation.Tag;
                foreach (CorrespondingDataFieldInfo correspondingDataFieldInfo in correspondingDataFieldInfos)
                {
                    if (!customDataFieldContract.ValidatePhysicalDataFieldCompatibility(correspondingDataFieldInfo.DataFieldId, correspondingDataFieldInfo.ParentDataFieldId))
                    {
                        result = false;
                        break;
                    }
                }
                if (result)
                {
                    EnumDescription description = (EnumDescription)cmbDataImportType.SelectedItem;
                    CommonNode commonNode = (CommonNode)cmbUserDataRelation.SelectedItem;
                    IList<WhereConditon> whereConditons = GetWhereConditons();
                    userDataRelationContract.Import(commonNode.NodeId, (DataImportType)description.EnumValue, whereConditons);
                    Cursor = Cursors.Default;
                    MessageBox.Show("转入成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("源数据表存在字段与其对应目的字段名称的数据类型不兼容，请验证！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicy(exception);
            }
        }

        private void sbtnRowColQuery_Click(object sender, EventArgs e)
        {
            if (ecmbSourceTable.SelectedNode == null)
            {
                MessageBox.Show("请先选择源表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode sourceCommonNode = ecmbSourceTable.SelectedNode.Tag as CommonNode;
            DatabaseNodeType sourceExportTableType = (DatabaseNodeType)sourceCommonNode.NodeType;
            if (sourceExportTableType != DatabaseNodeType.CustomTable)
            {
                MessageBox.Show("您选择的源表数据节点不是数据表类型！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ecmbDestTable.SelectedNode == null)
            {
                MessageBox.Show("请先选择目的表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode destCommonNode = ecmbDestTable.SelectedNode.Tag as CommonNode;
            DatabaseNodeType destExportTableType = (DatabaseNodeType)destCommonNode.NodeType;
            if (destExportTableType != DatabaseNodeType.CustomTable)
            {
                MessageBox.Show("您选择的目的表数据节点不是数据表类型！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EnumDescription enumDescription = (EnumDescription)cmbImport.SelectedItem;
            RowColSwap rowColSwap = (RowColSwap)enumDescription.EnumValue;
            if (rowColSwap == RowColSwap.DuplicateRow && sourceCommonNode.NodeId == destCommonNode.NodeId)
            {
                MessageBox.Show("行复制时不允许源表和目的表相同！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            currentRowColSwap = rowColSwap;
            try
            {
                //if (sourceDataWarehouseId == destDataWarehouseId && sourceDataWarehouseId > 0)
                //{
                    dataFieldRelations.Clear();
                    customDataFieldNames.Clear();
                    StringBuilder sb = new StringBuilder();
                    DataTable dataTable = (DataTable)gcDataFieldRelation.DataSource;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        decimal sourceId = DataConvertionHelper.GetConvertedDecimal(dr["SourceId"]);
                        decimal destinationId = DataConvertionHelper.GetConvertedDecimal(dr["DestinationId"]);
                        string customDataFiledName = DataConvertionHelper.GetString(dr["CustomDataFieldName"]);
                        if (destinationId > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(customDataFiledName))
                            {
                                customDataFieldNames.Add(destinationId, customDataFiledName);
                            }
                            else
                            {
                                if (sourceId > 0)
                                {
                                    dataFieldRelations.Add(sourceId, destinationId);
                                    if (rowColSwap == RowColSwap.AlternativeCol && sourceId == destinationId)
                                    {
                                        MessageBox.Show("列替换时源字段和目的字段相同！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    string sourceDataField = txtSourceDataField.Text.Trim();
                    if (!string.IsNullOrWhiteSpace(sourceDataField))
                    {
                        sb.AppendFormat("({0})", sourceDataField);
                    }
                    string destDataField = txtDestDataField.Text.Trim();
                    if (rowColSwap == RowColSwap.AlternativeCol && !string.IsNullOrWhiteSpace(destDataField))
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append(" AND ");
                        }
                        sb.AppendFormat("({0})", destDataField);
                    }
                    gridQueriedResult.Tag = sb.ToString();
                    sourceTableId = sourceCommonNode.NodeId;
                    destTableId = destCommonNode.NodeId;
                    Cursor = Cursors.WaitCursor;
                    int totalCount = 0;
                    gridQueriedResult.CurrentPageIndex = 0;
                    DataSet ds = authorizedDataContract.GetQueriedData(sourceDataWarehouseId, destDataWarehouseId, sourceCommonNode.NodeId, destCommonNode.NodeId, rowColSwap, dataFieldRelations,
                    customDataFieldNames, sb.ToString(), gridQueriedResult.PageSize * gridQueriedResult.CurrentPageIndex, gridQueriedResult.PageSize, ref totalCount);
                    gridQueriedResult.DataSource = ds.Tables[0];
                    gridQueriedResult.RecordCount = totalCount;
                    Cursor = Cursors.Default;
            //    }
            //    else
            //    {
            //        MessageBox.Show("行列替换操作必须在同一个数据仓库中进行！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicy(exception);
            }
        }

        private void sbtnRowColSumbit_Click(object sender, EventArgs e)
        {
            if (ecmbSourceTable.SelectedNode == null)
            {
                MessageBox.Show("请先选择源表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode sourceCommonNode = ecmbSourceTable.SelectedNode.Tag as CommonNode;
            DatabaseNodeType sourceExportTableType = (DatabaseNodeType)sourceCommonNode.NodeType;
            if (sourceExportTableType != DatabaseNodeType.CustomTable)
            {
                MessageBox.Show("您选择的源表数据节点不是数据表类型！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ecmbDestTable.SelectedNode == null)
            {
                MessageBox.Show("请先选择目的表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode destCommonNode = ecmbDestTable.SelectedNode.Tag as CommonNode;
            DatabaseNodeType destExportTableType = (DatabaseNodeType)destCommonNode.NodeType;
            if (destExportTableType != DatabaseNodeType.CustomTable)
            {
                MessageBox.Show("您选择的目的表数据节点不是数据表类型！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EnumDescription enumDescription = (EnumDescription)cmbImport.SelectedItem;
            RowColSwap rowColSwap = (RowColSwap)enumDescription.EnumValue;
            if (rowColSwap == RowColSwap.DuplicateRow && sourceCommonNode.NodeId == destCommonNode.NodeId)
            {
                MessageBox.Show("行复制时不允许源表和目的表相同！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                //if (sourceDataWarehouseId == destDataWarehouseId && sourceDataWarehouseId > 0)
                //{
                    IDictionary<decimal, decimal> dataFieldRelations = new Dictionary<decimal, decimal>();
                    IDictionary<decimal, string> customDataFieldNames = new Dictionary<decimal, string>();
                    StringBuilder sb = new StringBuilder();
                    DataTable dataTable = (DataTable)gcDataFieldRelation.DataSource;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        decimal sourceId = DataConvertionHelper.GetConvertedDecimal(dr["SourceId"]);
                        decimal destinationId = DataConvertionHelper.GetConvertedDecimal(dr["DestinationId"]);
                        string customDataFiledName = DataConvertionHelper.GetString(dr["CustomDataFieldName"]);
                        if (destinationId > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(customDataFiledName))
                            {
                                customDataFieldNames.Add(destinationId, customDataFiledName);
                            }
                            else
                            {
                                if (sourceId > 0)
                                {
                                    dataFieldRelations.Add(sourceId, destinationId);
                                    if (rowColSwap == RowColSwap.AlternativeCol && sourceId == destinationId)
                                    {
                                        MessageBox.Show("列替换时源字段和目的字段相同！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    string sourceDataField = txtSourceDataField.Text.Trim();
                    if (!string.IsNullOrWhiteSpace(sourceDataField))
                    {
                        sb.AppendFormat("({0})", sourceDataField);
                    }
                    string destDataField = txtDestDataField.Text.Trim();
                    if(rowColSwap == RowColSwap.AlternativeCol && !string.IsNullOrWhiteSpace(destDataField))
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append(" AND ");
                        }
                        sb.AppendFormat("({0})", destDataField);
                    }
                    int count = 0;
                    Cursor = Cursors.WaitCursor;
                    if (rowColSwap == RowColSwap.DuplicateRow)
                    {
                        count = authorizedDataContract.Import(sourceDataWarehouseId, destDataWarehouseId, sourceCommonNode.NodeId, destCommonNode.NodeId, dataFieldRelations,
                            customDataFieldNames, sb.ToString());
                    }
                    else
                    {
                        count = authorizedDataContract.Update(sourceDataWarehouseId, destDataWarehouseId, sourceCommonNode.NodeId, destCommonNode.NodeId, dataFieldRelations,
                            customDataFieldNames, sb.ToString());
                    }
                    Cursor = Cursors.Default;
                    MessageBox.Show(string.Format("{0}操作成功，共有{1}记录被处理！", cmbImport.Text, count), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    MessageBox.Show("行列替换操作必须在同一个数据仓库中进行！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicy(exception);
            }
        }

        private void sbtnsbtnRowColClear_Click(object sender, EventArgs e)
        {
            ecmbSourceTable.SelectedNode = null;
            ecmbDestTable.SelectedNode = null;
            cmbImport.SelectedIndex = 0;           
        }

        /// <summary>
        /// 创建字段对应关系的表结构
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("DestinationId", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("DestinationName", Type.GetType("System.String"));
            dataTable.Columns.Add("SourceId", Type.GetType("System.Decimal"));
            //dataTable.Columns.Add("DestinationCondition", Type.GetType("System.String"));
            //dataTable.Columns.Add("SourceCondition", Type.GetType("System.String"));            
            dataTable.Columns.Add("CustomDataFieldName", Type.GetType("System.String"));

            return dataTable;
        }

        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void ecmbSourceTable_AfterTreeNodeExpand(object sender, TreeViewEventArgs e)
        {
            sourceTableHandler.TreeNodeExpand(e.Node);
        }

        private void ecmbDestTable_AfterTreeNodeExpand(object sender, TreeViewEventArgs e)
        {
            destTableHandler.TreeNodeExpand(e.Node);
        }

        private void ecmbSourceTable_AfterTreeNodeSelect(object sender, TreeViewEventArgs e)
        {
            LoadSourceDataField(e.Node);
        }

        private void LoadSourceDataField(TreeNode treeNode)
        {
            if (treeNode == null)
            {
                return;
            }
            CommonNode sourceCommonNode = treeNode.Tag as CommonNode;
            DatabaseNodeType sourceExportTableType = (DatabaseNodeType)sourceCommonNode.NodeType;
            if (sourceExportTableType != DatabaseNodeType.CustomTable)
            {
                MessageBox.Show("您选择的源表数据节点不是数据表类型！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodes(sourceCommonNode.NodeId);
            sourceDataWarehouseId = customTableContract.GetDataWarehouseId(sourceCommonNode.NodeId);
            DataTable dataTable = gcDataFieldRelation.DataSource as DataTable;
            ricmbSource.Items.Clear();
            ricmbSource.Items.Add(new ImageComboBoxItem(string.Empty, 0, -1)); /* 第一个选项为空 */
            cmbSourceDataField.Properties.Items.Clear();
            txtSourceDataField.Text = string.Empty;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (i < commonNodes.Count)
                {
                    dataTable.Rows[i]["SourceId"] = commonNodes[i].NodeId;
                }
                else
                {
                    dataTable.Rows[i]["SourceId"] = 0;
                }                
                //dataTable.Rows[i]["SourceCondition"] = string.Empty;
            }
            for (int i = 0; i < commonNodes.Count; i++)
            {
                ImageComboBoxItem imageComboBoxItem = new ImageComboBoxItem(commonNodes[i].NodeName, commonNodes[i].NodeId, 0);
                ricmbSource.Items.Add(imageComboBoxItem);
                cmbSourceDataField.Properties.Items.Add(new CommonNode(commonNodes[i].NodeId, commonNodes[i].NodeName));
            }
        }

        private void ecmbDestTable_AfterTreeNodeSelect(object sender, TreeViewEventArgs e)
        {
            CommonNode destCommonNode = e.Node.Tag as CommonNode;
            DatabaseNodeType destExportTableType = (DatabaseNodeType)destCommonNode.NodeType;
            if (destExportTableType != DatabaseNodeType.CustomTable)
            {
                MessageBox.Show("您选择的目的表数据节点不是数据表类型！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodes(destCommonNode.NodeId, DataFieldShow.OnlyPhyscialField);
            destDataWarehouseId = customTableContract.GetDataWarehouseId(destCommonNode.NodeId);            
            DataTable dataTable = gcDataFieldRelation.DataSource as DataTable;
            if (dataTable.Rows.Count < commonNodes.Count)
            {
                int count = commonNodes.Count - dataTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataTable.Rows.Add(dataRow);
                }
            }
            else
            {
                int count = dataTable.Rows.Count - commonNodes.Count;
                for (int i = 0; i < count; i++)
                {
                    dataTable.Rows.RemoveAt(commonNodes.Count - 1);
                }
            }
            cmbDestDataField.Properties.Items.Clear();
            txtDestDataField.Text = string.Empty;
            for (int i = 0; i < commonNodes.Count; i++)
            {
                dataTable.Rows[i]["DestinationId"] = commonNodes[i].NodeId;
                dataTable.Rows[i]["DestinationName"] = commonNodes[i].NodeName;
                //dataTable.Rows[i]["DestinationCondition"] = string.Empty;
                cmbDestDataField.Properties.Items.Add(new CommonNode(commonNodes[i].NodeId, commonNodes[i].NodeName));
            }
            LoadSourceDataField(ecmbSourceTable.SelectedNode);
        }

        private void SetCondition(decimal dataFieldId, DevExpress.XtraEditors.MemoEdit memoEdit)
        {
            CustomDataFieldInfo customDataFieldInfo = customDataFieldContract.GetModeInfo(dataFieldId);
            QueryConditionForm frmQueryCondition = new QueryConditionForm();
            frmQueryCondition.QueryCondition = memoEdit.Text;
            frmQueryCondition.IsValidate = true;
            frmQueryCondition.TableName = customTableContract.GetTablePhysicalName(customDataFieldInfo.TableId);
            if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.LogicalDataField)
            {
                IList<CustomExpressionInfo> customExpressionInfos = customExpressionContract.GetModeInfos(customDataFieldInfo.DataFieldId);
                customDataFieldInfo.PhysicalName = customDataFieldContract.GetExpressionDataFieldName(frmQueryCondition.TableName, customDataFieldInfo.ExpressionText,
                    customExpressionInfos, (LogicalDataFieldType)customDataFieldInfo.DataFieldType);
            }
            frmQueryCondition.CustomDataFieldInfo = customDataFieldInfo;
            frmQueryCondition.UdateTextCallback = (where) =>
            {
                memoEdit.Text += where;
            };
            frmQueryCondition.ShowDialog();
        }

        private void rsitmCustomValue_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (ecmbSourceTable.SelectedNode != null)
            {
                CommonNode commonNode = ecmbSourceTable.SelectedNode.Tag as CommonNode;
                DatabaseNodeType exportTableType = (DatabaseNodeType)commonNode.NodeType;
                if (exportTableType != DatabaseNodeType.CustomTable)
                {
                    MessageBox.Show("您选择的源表数据节点不是数据表类型！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }               
                CustomDataFieldForm frmCustomDataField = new CustomDataFieldForm();
                frmCustomDataField.Text = customTableContract.GetTableLogicalName(commonNode.NodeId);
                frmCustomDataField.TableId = commonNode.NodeId;
                frmCustomDataField.CustomDataFieldContract = customDataFieldContract;
                if (gridView.FocusedValue != null)
                {
                    frmCustomDataField.CustomDataFieldText = gridView.FocusedValue.ToString();
                }
                else
                {
                    frmCustomDataField.CustomDataFieldText = string.Empty;
                }
                frmCustomDataField.UpdateText = (where) =>
                {
                    gridView.SetFocusedValue(where);
                };                
                frmCustomDataField.ShowDialog();
            }
        }

        private void gridView_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
            {
                popupMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void btnItmReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadSourceDataField(ecmbSourceTable.SelectedNode);
        }

        private void btnItmClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dataTable = gcDataFieldRelation.DataSource as DataTable;
            foreach (DataRow dr in dataTable.Rows)
            {
                dr["SourceId"] = DBNull.Value;
            }
        }

        private void cmbImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnumDescription enumDescription = (EnumDescription)cmbImport.SelectedItem;
            RowColSwap rowColSwap = (RowColSwap)enumDescription.EnumValue;
            if (rowColSwap == RowColSwap.DuplicateRow)
            {
                cmbDestDataField.Enabled = false;
                cmbDestDataField.Enabled = false;
                sbtnDestDataField.Enabled = false;
                txtDestDataField.Enabled = false;
            }
            else
            {
                cmbDestDataField.Enabled = true;
                cmbDestDataField.Enabled = true;
                sbtnDestDataField.Enabled = true;
                txtDestDataField.Enabled = true;
            }
        }

        private void gridDest_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomDevExpress.DevExpressGrid.CustomGridViewPageEventArgs e)
        {
            
        }

        private void sbtnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = AppSettingHelper.DefaultImportedReportingFormat;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                MessageBox.Show("没有选择Excel文件！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!File.Exists(openFileDialog.FileName))
            {
                MessageBox.Show(string.Format("{0}文件不存在，请检查！", Path.GetFileName(openFileDialog.FileName)),
                    "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                fpUserName.OpenExcel(openFileDialog.FileName, FarPoint.Excel.ExcelOpenFlags.TruncateEmptyRowsAndColumns);
                fpUserName.TabStripInsertTab = false;
                fpUserName.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Always;
                Cursor = Cursors.Default;
            }
            catch
            {
                Cursor = Cursors.Default;
                MessageBox.Show("出错可能原因：（1）已打开该Excel文件（2）该文件并不存在（3）该文件内容与格式错误", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void sbtnSave_Click(object sender, EventArgs e)
        {
            if (devExpreesComoboxTreeview.SelectedNode == null)
            {
                MessageBox.Show("请先选中一个复表！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
            //允许在对话框中包括一个新建目录的按钮
            folderBrowserDialog.ShowNewFolderButton = true;
            // 设置对话框的说明信息
            folderBrowserDialog.Description = "请选择输出目录";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    decimal reportId = devExpreesComoboxTreeview.SelectedNodeId;
                    byte[] data = customSheetContract.DownLoadReportFile(reportId, ReportType.ReportInpt);
                    string dirName = "Input";
                    string dir = string.Format(@"{0}\{1}", folderBrowserDialog.SelectedPath, dirName);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    // 在此添加代码,选择的路径为 
                    Cursor = Cursors.WaitCursor;                    
                    using (FarPoint.Win.Spread.FpSpread fpSpread = new FarPoint.Win.Spread.FpSpread())
                    {
                        int count = 0;
                        for (int rowIndex = 0; rowIndex < fpUserName.ActiveSheet.RowCount; rowIndex++)
                        {
                            string userName = fpUserName.ActiveSheet.Cells[rowIndex, 0].Text.Trim();
                            string subDirName = fpUserName.ActiveSheet.Cells[rowIndex, 1].Text.Trim();
                            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(subDirName))
                            {
                                break;
                            }
                            string subDir = string.Format(@"{0}\{1}\{2}", folderBrowserDialog.SelectedPath, dirName, subDirName);
                            if (!Directory.Exists(subDir))
                            {
                                Directory.CreateDirectory(subDir);
                            }                            
                            decimal userId = userAccountContract.GetUserIdByUserName(userName);
                            if (userId <= 0)
                            {
                                break;
                            }                            
                            CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(userId);                           
                            string subDirPath = string.Format(@"{0}\{1}_{2}_{3}_{4}.xls", subDir, commonUserInfo.UserActualName, commonUserInfo.UserName,
                                commonUserInfo.UserDepartmentName, devExpreesComoboxTreeview.SelectedNode.Text);
                            SaveReportingFile(fpSpread, reportId, data, userId, subDirPath);
                            fpSpread.Reset();
                            Application.DoEvents();
                            count++;
                        }
                        Cursor = Cursors.Default;
                        MessageBox.Show(string.Format("共有{0}个保存成功，保存目录为{1}。", count, dir), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }                    
                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 保存复表
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <param name="reportId"></param>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <param name="dir"></param>
        private void SaveReportingFile(FpSpread fsReporting, decimal reportId, byte[] data, decimal userId, string dir)
        {
            try
            {
                Cursor = Cursors.WaitCursor;                
                if (data != null && data.Length > 0)
                {
                    IList<RowAndCol> rowAndCols = customSheetContract.GetRowAndColCount(reportId, ReportType.ReportInpt);
                    ReportHelper.ShowFpSpread(fsReporting, data, rowAndCols);
                    LoadData(fsReporting, reportId, userId);
                    fsReporting.SaveExcel(dir, FarPoint.Excel.ExcelSaveFlags.SaveAsViewed | FarPoint.Excel.ExcelSaveFlags.SaveAsFiltered);
                    Cursor = Cursors.Default;
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("该复表还未设计，请先设计！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicy(exception);
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <param name="reportId"></param>
        /// <param name="userId"></param>
        private void LoadData(FpSpread fsReporting, decimal reportId, decimal userId)
        {
            IList<CommonNode> commonNodes = customSheetContract.GetCommonNodes(reportId, ReportType.ReportInpt);
            int count = (fsReporting.Sheets.Count <= commonNodes.Count) ? fsReporting.Sheets.Count : commonNodes.Count;
            IDictionary<decimal, DataTable> data = new Dictionary<decimal, DataTable>();
            IDictionary<decimal, int> dataCount = new Dictionary<decimal, int>();
            for (int i = 0; i < count; i++)
            {
                IList<CellDataFieldInfo> cellDataFieldInfos = authorizedDataContract.GetAuthorizedCellDataFieldInfos(CurrentUser.Instance.UserId,
                    DataConvertionHelper.GetConvertedByte(DataAuthority.Auditing), commonNodes[i].NodeId);
                IList<CellDataFieldInfo> associationCellDataFieldInfos = new List<CellDataFieldInfo>();
                IList<CellDataFieldInfo> relevancedCellDataFieldInfos = new List<CellDataFieldInfo>();

                IDictionary<decimal, IList<CellDataFieldInfo>> singleCellDataFieldInfoDic = new Dictionary<decimal, IList<CellDataFieldInfo>>();
                IDictionary<decimal, IList<CellDataFieldInfo>> extendedCellDataFieldInfoDic = new Dictionary<decimal, IList<CellDataFieldInfo>>();
                IDictionary<decimal, int> recordCountOfExtendedCellDataFieldDic = new Dictionary<decimal, int>();

                /* 1.单元格分类 */
                foreach (CellDataFieldInfo cellDataFieldInfo in cellDataFieldInfos)
                {
                    if (cellDataFieldInfo.TableId <= 0)
                    {
                        continue;
                    }
                    ReportCellType reportCellType = (ReportCellType)cellDataFieldInfo.CellType;
                    switch (reportCellType)
                    {
                        case ReportCellType.Single:
                            IList<CellDataFieldInfo> singleCellDataFieldInfos = null;
                            if (!singleCellDataFieldInfoDic.ContainsKey(cellDataFieldInfo.TableId))
                            {
                                singleCellDataFieldInfos = new List<CellDataFieldInfo>();
                                singleCellDataFieldInfoDic.Add(cellDataFieldInfo.TableId, singleCellDataFieldInfos);
                            }
                            else
                            {
                                singleCellDataFieldInfos = singleCellDataFieldInfoDic[cellDataFieldInfo.TableId];
                            }
                            singleCellDataFieldInfos.Add(cellDataFieldInfo);
                            break;

                        case ReportCellType.ExtendRow:
                        case ReportCellType.ExtendCol:
                            IList<CellDataFieldInfo> extendedCellDataFieldInfos = null;
                            if (!extendedCellDataFieldInfoDic.ContainsKey(cellDataFieldInfo.TableId))
                            {
                                extendedCellDataFieldInfos = new List<CellDataFieldInfo>();
                                extendedCellDataFieldInfoDic.Add(cellDataFieldInfo.TableId, extendedCellDataFieldInfos);
                            }
                            else
                            {
                                extendedCellDataFieldInfos = extendedCellDataFieldInfoDic[cellDataFieldInfo.TableId];
                            }
                            extendedCellDataFieldInfos.Add(cellDataFieldInfo);
                            int extendedCount = (reportCellType == ReportCellType.ExtendRow) ? cellDataFieldInfo.ExtendRows : cellDataFieldInfo.ExtendCols;
                            if (!recordCountOfExtendedCellDataFieldDic.ContainsKey(cellDataFieldInfo.TableId))
                            {
                                recordCountOfExtendedCellDataFieldDic.Add(cellDataFieldInfo.TableId, extendedCount);
                            }
                            else
                            {
                                if (recordCountOfExtendedCellDataFieldDic[cellDataFieldInfo.TableId] < extendedCount)
                                {
                                    recordCountOfExtendedCellDataFieldDic[cellDataFieldInfo.TableId] = extendedCount;
                                }
                            }
                            break;
                    }
                }

                /* 2. 显示单个单元格 */
                foreach (KeyValuePair<decimal, IList<CellDataFieldInfo>> singleCellDataFieldInfo in singleCellDataFieldInfoDic)
                {
                    DataTable dt = authorizedDataContract.GetAuthorizedData(userId, singleCellDataFieldInfo.Key, singleCellDataFieldInfo.Value, 1).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        decimal recordId = DataConvertionHelper.GetDecimal(dt.Rows[0][SystemDataFieldHelper.GetSystemDataFieldName(SystemDataField.RecordId)]);
                        foreach (CellDataFieldInfo cellDataFieldInfo in singleCellDataFieldInfo.Value)
                        {
                            object value = null;
                            if (dt.Rows.Count > 0)
                            {
                                cellDataFieldInfo.RecordId = recordId;
                                value = dt.Rows[0][cellDataFieldInfo.PhysicalName];
                            }
                            fsReporting.Sheets[i].Cells[cellDataFieldInfo.Row, cellDataFieldInfo.Col].Tag = cellDataFieldInfo;
                            SetCellProperty(fsReporting, i, fsReporting.Sheets[i].Cells[cellDataFieldInfo.Row, cellDataFieldInfo.Col], value);
                        }
                    }
                }

                /* 3. 显示扩展单元格 */
                foreach (KeyValuePair<decimal, IList<CellDataFieldInfo>> extendedCellDataFieldInfo in extendedCellDataFieldInfoDic)
                {
                    int recordCount = recordCountOfExtendedCellDataFieldDic[extendedCellDataFieldInfo.Key];
                    DataTable dt = authorizedDataContract.GetAuthorizedData(userId, extendedCellDataFieldInfo.Key, extendedCellDataFieldInfo.Value, recordCount).Tables[0];
                    foreach (CellDataFieldInfo cellDataFieldInfo in extendedCellDataFieldInfo.Value)
                    {
                        ReportCellType reportCellType = (ReportCellType)cellDataFieldInfo.CellType;
                        int row = 0, col = 0, size = 0;
                        if (reportCellType == ReportCellType.ExtendRow)
                        {
                            size = cellDataFieldInfo.ExtendRows;
                        }
                        else
                        {
                            size = cellDataFieldInfo.ExtendCols;
                        }
                        int currentSpanCellRowCount = 0;
                        for (int index = 0; index < size; index++)
                        {
                            switch (reportCellType)
                            {
                                case ReportCellType.ExtendRow:
                                    row = cellDataFieldInfo.Row + currentSpanCellRowCount;
                                    col = cellDataFieldInfo.Col + cellDataFieldInfo.Position;
                                    break;

                                case ReportCellType.ExtendCol:
                                    row = cellDataFieldInfo.Row + cellDataFieldInfo.Position;
                                    col = cellDataFieldInfo.Col + currentSpanCellRowCount;
                                    break;
                            }
                            object result = null;
                            CellDataFieldInfo tag = (CellDataFieldInfo)cellDataFieldInfo.Clone();
                            if (index < dt.Rows.Count)
                            {
                                tag.RecordId = DataConvertionHelper.GetDecimal(dt.Rows[index][SystemDataFieldHelper.GetSystemDataFieldName(SystemDataField.RecordId)]);
                                result = dt.Rows[index][cellDataFieldInfo.PhysicalName];
                            }
                            tag.Row = row;
                            tag.Col = col;
                            fsReporting.Sheets[i].Cells[row, col].Tag = tag;
                            SetCellProperty(fsReporting, i, fsReporting.Sheets[i].Cells[row, col], result);
                            /* 计算合并的单元格的行列 */
                            CellRange cr = fsReporting.Sheets[i].GetSpanCell(row, col);
                            if (cr != null)
                            {
                                switch (reportCellType)
                                {
                                    case ReportCellType.ExtendRow:
                                        currentSpanCellRowCount += cr.RowCount;
                                        break;

                                    case ReportCellType.ExtendCol:
                                        currentSpanCellRowCount += cr.ColumnCount;
                                        break;
                                }
                            }
                            else
                            {
                                currentSpanCellRowCount++;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置单元格属性
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="value"></param>
        private void SetCellProperty(FpSpread fsReporting, int index, FarPoint.Win.Spread.Cell cell, object value)
        {
            if (cell.Tag != null)
            {
                CellDataFieldInfo cellDataFieldInfo = cell.Tag as CellDataFieldInfo;
                DataFieldProperty dataFieldProperty = (DataFieldProperty)cellDataFieldInfo.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemDataField:
                        SystemDataField systemDataField = (SystemDataField)cellDataFieldInfo.DataFieldId;
                        switch (systemDataField)
                        {
                            case SystemDataField.UserTypeId:
                                decimal userTypeId = DataConvertionHelper.GetConvertedDecimal(value);
                                value = userTypeContract.GetUserTypeName(userTypeId);
                                break;

                            case SystemDataField.DepID:
                                decimal depId = DataConvertionHelper.GetConvertedDecimal(value);
                                value = departmentContract.GetDepartmentName(depId);
                                break;

                            case SystemDataField.Auditing:
                                byte auditedStateId = DataConvertionHelper.GetConvertedByte(value);
                                value = EnumDescription.GetFieldText((AuditedState)auditedStateId);
                                break;

                            case SystemDataField.CreationTime:
                            case SystemDataField.ModificationTime:
                                value = DataConvertionHelper.GetDateTime(value).ToString("yyyy年MM月dd日");
                                break;
                        }
                        break;

                    case DataFieldProperty.PhysicalDataField:
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)cellDataFieldInfo.DataFieldType;
                        switch (physicalDataFieldType)
                        {

                            case PhysicalDataFieldType.ArbitraryString:
                                if (cellDataFieldInfo.PhysicalName.Equals(reviewedConclusion))
                                {
                                    RichTextCellType rtf = new RichTextCellType();
                                    rtf.WordWrap = true;
                                    rtf.Multiline = true;
                                    cell.CellType = rtf;
                                }
                                break;

                            case PhysicalDataFieldType.Boolean:
                            case PhysicalDataFieldType.AssociatedBoolean:
                                cell.CellType = new CheckBoxCellType();
                                break;

                            case PhysicalDataFieldType.Int32:
                            case PhysicalDataFieldType.AssociatedInt32:
                                NumberCellType numberCellType = new NumberCellType();
                                numberCellType.DecimalPlaces = 0;
                                cell.CellType = numberCellType;
                                break;

                            case PhysicalDataFieldType.Decimal:
                            case PhysicalDataFieldType.AssociatedDecimal:
                                NumberCellType decimalNumberCellType = new NumberCellType();
                                decimalNumberCellType.DecimalPlaces = cellDataFieldInfo.DataFieldLength;
                                cell.CellType = decimalNumberCellType;
                                break;

                            case PhysicalDataFieldType.NumeralString:
                                RegularExpressionCellType numeralString = new RegularExpressionCellType();
                                numeralString.RegularExpression = @"^-?\d+$";
                                cell.CellType = numeralString;
                                break;

                            case PhysicalDataFieldType.CharString:
                                RegularExpressionCellType charString = new RegularExpressionCellType();
                                charString.RegularExpression = @"^[A-Za-z]+$";
                                cell.CellType = charString;
                                break;

                            case PhysicalDataFieldType.MixedString:
                                RegularExpressionCellType mixedString = new RegularExpressionCellType();
                                mixedString.RegularExpression = @"^[A-Za-z0-9]+$";
                                cell.CellType = mixedString;
                                break;

                            case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                                DateTimeCellType yearAndMonthAndDayAndTime = new DateTimeCellType();
                                yearAndMonthAndDayAndTime.DateTimeFormat = DateTimeFormat.LongDateWithTime;
                                cell.CellType = yearAndMonthAndDayAndTime;
                                break;

                            case PhysicalDataFieldType.YearAndMonthAndDay:
                            case PhysicalDataFieldType.AssociatedTime:
                                DateTimeCellType yearAndMonthAndDay = new DateTimeCellType();
                                yearAndMonthAndDay.DateTimeFormat = DateTimeFormat.UserDefined;
                                yearAndMonthAndDay.UserDefinedFormat = "yyyy-MM-dd";
                                cell.CellType = yearAndMonthAndDay;
                                break;

                            case PhysicalDataFieldType.YearAndMonth:
                                DateTimeCellType yearAndMonth = new DateTimeCellType();
                                yearAndMonth.DateTimeFormat = DateTimeFormat.UserDefined;
                                yearAndMonth.UserDefinedFormat = "yyyy-MM";
                                cell.CellType = yearAndMonth;
                                break;

                            case PhysicalDataFieldType.MonthAndDay:
                                DateTimeCellType monthAndDay = new DateTimeCellType();
                                monthAndDay.DateTimeFormat = DateTimeFormat.UserDefined;
                                monthAndDay.UserDefinedFormat = "MM-dd";
                                cell.CellType = monthAndDay;
                                break;

                            case PhysicalDataFieldType.Time:
                                DateTimeCellType time = new DateTimeCellType();
                                time.DateTimeFormat = DateTimeFormat.TimeOnly;
                                cell.CellType = time;
                                break;

                            case PhysicalDataFieldType.EnumTreeViewName:
                            case PhysicalDataFieldType.EnumTreeViewVaule:
                            case PhysicalDataFieldType.EnumTreeViewFstAdditionalCode:
                            case PhysicalDataFieldType.EnumTreeViewScdAdditionalCode:
                            case PhysicalDataFieldType.EnumDropDownListName:
                            case PhysicalDataFieldType.EnumDropDownListVaule:
                            case PhysicalDataFieldType.EnumDropDownListFstAdditionalCode:
                            case PhysicalDataFieldType.EnumDropDownListScdAdditionalCode:
                            case PhysicalDataFieldType.DocAttachment:
                            case PhysicalDataFieldType.AssociatedString:
                                cell.CellType = new TextCellType();
                                break;
                        }
                        break;

                    case DataFieldProperty.LogicalDataField:
                        cell.CellType = new TextCellType();
                        LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)cellDataFieldInfo.DataFieldType;
                        switch (logicalDataFieldType)
                        {
                            //case LogicalDataFieldType.DigitExpression:
                            //    cell.CellType = new NumberCellType();
                            //    break;

                            case LogicalDataFieldType.DateTimeExpression:                                
                                DateTimeCellType yearAndMonthAndDay = new DateTimeCellType();
                                yearAndMonthAndDay.DateTimeFormat = DateTimeFormat.UserDefined;
                                yearAndMonthAndDay.UserDefinedFormat = "yyyy-MM-dd";
                                cell.CellType = yearAndMonthAndDay;
                                break;

                            case LogicalDataFieldType.StringExpression:
                                cell.CellType = new TextCellType();
                                break;
                        }
                        break;
                }
                if (dataFieldProperty == DataFieldProperty.PhysicalDataField
                    && (PhysicalDataFieldType)cellDataFieldInfo.DataFieldType == PhysicalDataFieldType.PicAttachment)
                {
                    if (value != null)
                    {
                        string picName = value.ToString();
                        byte[] data = authorizedDataContract.DownLoadPhoto(string.Format("{0}.JPG", picName));                                              
                        if (data != null && cell != null)
                        {
                            ReportHelper.InsertPhoto(fsReporting, cell, data);
                        }
                    }
                    else
                    {
                        cell.Value = null;
                    }
                }
                else
                {
                    cell.Value = value;
                }
            }
        }

        private void devExpreesComoboxTreeview_AfterTreeNodeExpand(object sender, TreeViewEventArgs e)
        {
            compositegTableClassHandler.TreeNodeExpand(e.Node);
        }

        private void sbtnPrint_Click(object sender, EventArgs e)
        {
            if (devExpreesComoboxTreeview.SelectedNode == null)
            {
                MessageBox.Show("请先选中一个复表！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(string.Format("确认批量打印{0}?", devExpreesComoboxTreeview.SelectedNodeText),
                "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            try
            {
                decimal reportId = devExpreesComoboxTreeview.SelectedNodeId;
                byte[] data = customSheetContract.DownLoadReportFile(reportId, ReportType.ReportInpt);
                if (data != null && data.Length > 0)
                {
                    IList<RowAndCol> rowAndCols = customSheetContract.GetRowAndColCount(reportId, ReportType.ReportInpt);
                    IList<CommonNode> commonNodes = customSheetContract.GetCommonNodes(reportId, ReportType.ReportInpt);
                    IList<CustomMargin> customMargins = new List<CustomMargin>();
                    foreach (CommonNode commonNode in commonNodes)
                    {
                        CustomMargin customMargin = customSheetContract.GetMargin(commonNode.NodeId);
                        if (customMargin != null)
                        {
                            customMargins.Add(customMargin);
                        }
                    }
                    ProgressForm frmProgress = new ProgressForm();
                    frmProgress.MinValue = 0;
                    frmProgress.TopMost = true;
                    frmProgress.MaxValue = fpUserName.ActiveSheet.RowCount;
                    frmProgress.Show();
                    bool exit = false;
                    try
                    {
                        for (int rowIndex = 0; rowIndex < fpUserName.ActiveSheet.RowCount; rowIndex++)
                        {
                            decimal userId = 0;
                            string userName = fpUserName.ActiveSheet.Cells[rowIndex, 0].Text.Trim();
                            if (!string.IsNullOrWhiteSpace(userName))
                            {
                                userId = userAccountContract.GetUserIdByUserName(userName);
                            }
                            if (userId <= 0)
                            {
                                MessageBox.Show(string.Format("第{0}行的用户{1}不存在，打印中断！", rowIndex, userName),
                                    "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                exit = true;
                                break;
                            }
                            frmProgress.Text = string.Format("正在打印第{0}个用户...", rowIndex + 1);
                            Application.DoEvents();
                            frmProgress.IncreaseStep();
                            if (frmProgress != null && frmProgress.Cancel)
                            {
                                frmProgress.CloseFrom();
                                return;
                            }
                            using (FarPoint.Win.Spread.FpSpread fpSpread = new FarPoint.Win.Spread.FpSpread())
                            {

                                ReportHelper.ShowFpSpread(fpSpread, data, rowAndCols);
                                LoadData(fpSpread, reportId, userId);
                                for (int i = 0; i < customMargins.Count; i++)
                                {
                                    fpSpread.ActiveSheetIndex = i;
                                    ReportHelper.PrintActiveSheet(fpSpread, customMargins[i]);
                                    if (frmProgress != null && frmProgress.Cancel)
                                    {
                                        frmProgress.CloseFrom();
                                        return;
                                    }
                                    Application.DoEvents();
                                }
                            }
                        }
                        if (frmProgress != null && !frmProgress.IsDisposed)
                        {
                            frmProgress.CloseFrom();
                        }
                        Cursor = Cursors.Default;
                        if (!exit)
                        {
                            MessageBox.Show("打印完成！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception exception)
                    {
                        if (frmProgress != null && !frmProgress.IsDisposed)
                        {
                            frmProgress.CloseFrom();
                        }
                        throw exception;
                    }
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("该复表还未设计，请先设计！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void gridQueriedResult_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomDevExpress.DevExpressGrid.CustomGridViewPageEventArgs e)
        {
            int totalCount = 0;
            gridQueriedResult.CurrentPageIndex = e.NewPageIndex;
            DataSet ds = authorizedDataContract.GetQueriedData(sourceDataWarehouseId, destDataWarehouseId, sourceTableId, destTableId, currentRowColSwap, dataFieldRelations,
                    customDataFieldNames, gridQueriedResult.Tag.ToString(), gridQueriedResult.PageSize * gridQueriedResult.CurrentPageIndex, gridQueriedResult.PageSize, ref totalCount);
            gridQueriedResult.DataSource = ds.Tables[0];
            gridQueriedResult.RecordCount = totalCount;
        }

        private void sbtnSourceDataField_Click(object sender, EventArgs e)
        {
            if (cmbSourceDataField.SelectedItem != null)
            {
                CommonNode commonNode = (CommonNode)cmbSourceDataField.SelectedItem;
                SetCondition(commonNode.NodeId, txtSourceDataField);
            }
        }

        private void sbtnDestDataField_Click(object sender, EventArgs e)
        {
            if (cmbDestDataField.SelectedItem != null)
            {
                CommonNode commonNode = (CommonNode)cmbDestDataField.SelectedItem;
                SetCondition(commonNode.NodeId, txtDestDataField);
            }
        }

        private void sbtnObtain_Click(object sender, EventArgs e)
        {
            string remoteAddress = etxtRemoteAddress.Text.Trim();
            string remoteUserName = etxtRemoteUserName.Text.Trim();
            string remoteUserPwd = etxtRemoteUserPwd.Text.Trim();
            string source = mtxtSource.Text.Trim();
            string dest = mtxtDestination.Text.Trim();
            if (string.IsNullOrWhiteSpace(remoteAddress))
            {
                etxtRemoteAddress.Focus();
                MessageBox.Show("远程地址不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(remoteUserName))
            {
                etxtRemoteUserName.Focus();
                MessageBox.Show("用户名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(remoteUserPwd))
            {
                etxtRemoteUserPwd.Focus();
                MessageBox.Show("密码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(source))
            {
                etxtRemoteUserName.Focus();
                MessageBox.Show("源数据不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(dest))
            {
                etxtRemoteUserName.Focus();
                MessageBox.Show("目标数据不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            source = source.Replace("；", ";");
            if (source.EndsWith(";"))
            {
                source = source.Remove(source.Length - 1);
            }
            dest = dest.Replace("；", ";");
            if (dest.EndsWith(";"))
            {
                dest = dest.Remove(dest.Length - 1);
            }
            string[] sources = source.Split(';');
            string[] dests = dest.Split(';');
            if (sources.Length != dests.Length)
            {
                MessageBox.Show("源数据和目标数据对应不一致，请检查！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                customDataContract = RemoteChannelFactory.CreateCustomDataContract(remoteAddress, CurrentConfig.Instance.Port);
                if (customDataContract.ValidateUser(remoteUserName, remoteUserPwd))
                {                    
                    frmProgress = new ProgressForm();
                    frmProgress.MinValue = 0;
                    frmProgress.TopMost = true;
                    frmProgress.MaxValue = sources.Length + 1;
                    frmProgress.Show();
                    frmProgress.Tip = "正准备导入...";
                    frmProgress.IncreaseStep();
                    Application.DoEvents();

                    int index = 0;
                    bool exit = false;
                    IDictionary<string, string> dataFieldRelation = new Dictionary<string, string>();
                    foreach (string sr in sources)
                    {
                        string[] tableNameAndDataFields = dests[index].Split(',');
                        index++;
                        frmProgress.Tip = string.Format("正在导入第{0}个表的数据...", index);
                        Application.DoEvents();
                        DataSet ds = null;
                        try
                        {
                            int pos = sr.IndexOf(',');
                            string sourceName = sr.Substring(0, pos - 1).Trim();
                            int dataWarehouseId = DataWarehouseHelper.GetDataSourceId(sourceName);
                            string sql = sr.Substring(pos + 1).Trim();
                            ds = customDataContract.GetQueryDataSet(dataWarehouseId, sql);                        
                        }
                        catch
                        {
                            Application.DoEvents();
                            frmProgress.CloseFrom();
                            frmProgress = null;
                            MessageBox.Show(string.Format("第{0}条SQL语句查询异常，请检查！", index),
                                "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (ds == null || ds.Tables.Count == 0)
                        {
                            Application.DoEvents();
                            frmProgress.CloseFrom();
                            frmProgress = null;
                            MessageBox.Show(string.Format("第{0}条SQL语句查询为空，请检查！", index),
                                "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        int dataFieldIndex = 1;
                        if (ds.Tables[0].Columns.Count != tableNameAndDataFields.Length - 1)
                        {
                            Application.DoEvents();
                            frmProgress.CloseFrom();
                            frmProgress = null;
                            MessageBox.Show(string.Format("第{0}条SQL语句的字段对应关系不一致，请检查！", index),
                                "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            dataFieldRelation.Add(dc.ColumnName, tableNameAndDataFields[dataFieldIndex].Trim());
                            dataFieldIndex++;
                        }
                        CustomTableInfo customTableInfo = customTableContract.GetModeInfo(tableNameAndDataFields[0].Trim());
                        if (customTableInfo == null)
                        {
                            Application.DoEvents();
                            frmProgress.CloseFrom();
                            frmProgress = null;
                            MessageBox.Show(string.Format("目标表{0}不存在，请检查！", tableNameAndDataFields[0].Trim()), 
                                "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        authorizedDataContract.Import(customTableInfo.TableId, ds.Tables[0], dataFieldRelation, true);
                        dataFieldRelation.Clear();
                        Application.DoEvents();
                        if (frmProgress != null && frmProgress.Cancel)
                        {
                            exit = true;
                            break; ;
                        }
                        frmProgress.IncreaseStep();
                        if (exit)
                        {
                            frmProgress.CloseFrom();
                            frmProgress = null;
                            return;
                        }                        
                    }
                    frmProgress.CloseFrom();
                    frmProgress = null;
                    Cursor = Cursors.Default;
                    AppSettingHelper.SourceSQL = mtxtSource.Text.Trim();
                    AppSettingHelper.DestinationSQL = mtxtDestination.Text.Trim();

                    MessageBox.Show("导入成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("授权的用户名或是密码错误！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                Cursor = Cursors.Default;
                MessageBox.Show("无法连接到远程服务器，获取失败！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
