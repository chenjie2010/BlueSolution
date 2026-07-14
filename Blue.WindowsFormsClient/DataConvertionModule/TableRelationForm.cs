using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Core.ClientConfig;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using Blue.Model.DataConvertionModule;
using Blue.Model.BusinessModule;
using Blue.WCFContracts;
using Blue.WCFContracts.DataConvertionModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class TableRelationForm : Form
    {
        #region 私有变量

        private DataRelationInfo dataRelationInfo = null;
        private RemoteDataInfo remoteDataInfo = null;
        private Dictionary<decimal, DataTable> dataFieldRelation;

        #endregion

        #region 契约接口

        private readonly IDataRelationContract dataRelationContract;
        private readonly ICustomDatabaseContract customDatabaseContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly IRemoteDataContract remoteDataContract;
        private IRemoteServerContract remoteServerContract;

        #endregion

        #region 属性

        /// <summary>
        /// 数据字段关系编号
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
        public TableRelationForm()
        {
            InitializeComponent();
            dataRelationContract = DataConvertionChannelFactory.CreateDataRelationContract();
            customDatabaseContract = BusinessChannelFactory.CreateCustomDatabaseContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
            remoteDataContract = DataConvertionChannelFactory.CreateRemoteDataContract();
            dataFieldRelation = new Dictionary<decimal, DataTable>();
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocalTableRelationForm_Load(object sender, EventArgs e)
        {
            switch (DataSourceTypeValue)
            {
                case DataSourceType.Local:
                    dataRelationInfo = dataRelationContract.GetModelInfo(RelationId);
                    txtSourceDatabaseName.Text = customDatabaseContract.GetFullName(dataRelationInfo.ParentDatabaseId);
                    txtDestDatabaseName.Text = customDatabaseContract.GetFullName(dataRelationInfo.DatabaseId);
                    break;

                case DataSourceType.Remote:
                    remoteDataInfo = remoteDataContract.GetModelInfo(RelationId);
                    txtDestDatabaseName.Text = customDatabaseContract.GetFullName(remoteDataInfo.DatabaseId);
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
                        txtSourceDatabaseName.Text = remoteServerContract.GetDatabaseName(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, remoteDataInfo.RemoteDatabaseId);
                        Cursor = Cursors.Default;
                    }
                    catch (Exception exception)
                    {
                        Cursor = Cursors.Default;
                        //记录日志, 抛出异常, 不包装异常 
                        WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                        this.Close();
                    }
                    break;
            }
            LoadData();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            switch (DataSourceTypeValue)
            {
                case DataSourceType.Local:
                    Dictionary<decimal, decimal> tableRelation = dataRelationContract.GetTableRelation(RelationId);
                    SetTableDataRelation(dataRelationInfo.ParentDatabaseId, dataRelationInfo.DatabaseId, tableRelation);
                    break;

                case DataSourceType.Remote:
                    Dictionary<decimal, Dictionary<decimal, decimal>> relations = remoteDataContract.GetTableRelation(remoteDataInfo.RemoteDataId);
                    SetTableDataRelation(remoteDataInfo.RemoteDatabaseId, remoteDataInfo.DatabaseId, relations);
                    break;
            }
            SetActiveStatesOfControls(true);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetActiveStatesOfControls(false);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                List<KeyValueItem> keyValueItems = new List<KeyValueItem>();
                DataTable dataTable = gcTableRelation.DataSource as DataTable;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    decimal sourceTableId = (decimal)dataRow["SourceId"];
                    decimal destinationTableId = (decimal)dataRow["DestinationId"];
                    if (destinationTableId > 0 && dataFieldRelation.ContainsKey(sourceTableId))
                    {
                        IList<CustomDataFieldInfo> sourceCustomDataFieldInfos = null;
                        switch (DataSourceTypeValue)
                        {
                            case DataSourceType.Local:
                                sourceCustomDataFieldInfos = customDataFieldContract.GetModelInfos(sourceTableId, DataFieldFilter.PhysicalFieldAndLogicalField);
                                break;

                            case DataSourceType.Remote:
                                sourceCustomDataFieldInfos = remoteServerContract.GetModelInfosByTableId(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, sourceTableId, DataFieldFilter.PhysicalFieldAndLogicalField);
                                break;
                        }
                        IList<CustomDataFieldInfo> destCustomDataFieldInfos = customDataFieldContract.GetModelInfos(destinationTableId, DataFieldFilter.OnlyPhysicalField);
                        DataTable dt = dataFieldRelation[sourceTableId];
                        var query = from t in dt.AsEnumerable()
                                    group t by new { t1 = t.Field<decimal>("DestinationId")} into m
                                    select new
                                    {
                                        DestinationId = m.Key.t1,
                                        SourceName = m.First().Field<string>("SourceName"),
                                        Rowcount = m.Count()
                                    };
                        int count = 0;
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in query.ToList())
                        {
                            if (item.Rowcount > 1 && item.DestinationId > 0)
                            {
                                sb.AppendFormat("[{0}], ", item.SourceName);
                                count++;
                            }
                        }
                        if (count > 0)
                        {
                            sb.Remove(sb.Length - 2, 2);
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个目的字段有重复，其对应的源字段：{1}。", count, sb), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }                        
                        foreach (DataRow dr in dt.Rows)
                        {
                            decimal sourceId = (decimal)dr["SourceId"];
                            decimal destinationId = (decimal)dr["DestinationId"];
                            if (sourceId > 0 && destinationId > 0)
                            {
                                int sourceIndex = sourceCustomDataFieldInfos.FindIndex(dataFieldInfo => dataFieldInfo.DataFieldId == sourceId);
                                int destIndex = destCustomDataFieldInfos.FindIndex(dataFieldInfo => dataFieldInfo.DataFieldId == destinationId);
                                if (sourceIndex >= 0 && destIndex >= 0)
                                {
                                    keyValueItems.Add(new KeyValueItem(sourceId, destinationId));
                                    CustomDataFieldInfo sourceCustomDataFieldInfo = sourceCustomDataFieldInfos[sourceIndex];
                                    CustomDataFieldInfo destCustomDataFieldInfo = destCustomDataFieldInfos[destIndex];                                   
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
                                        string sourceTableName = (string)dataRow["SourceName"];
                                        string sourceName = (string)dr["SourceName"];
                                        Cursor = Cursors.Default;
                                        MessageBox.Show(string.Format("源数据表[{0}]中字段名[{1}]与其对应目的字段的数据类型不兼容！", sourceTableName, sourceName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                            }
                        }
                        switch (DataSourceTypeValue)
                        {
                            case DataSourceType.Local:
                                dataRelationContract.UpdateDataFieldRelation(RelationId, keyValueItems);
                                break;

                            case DataSourceType.Remote:
                                remoteDataContract.UpdateDataFieldRelation(RelationId, keyValueItems);
                                break;
                        }                        
                    }
                }
                SetActiveStatesOfControls(false);
                MessageBox.Show("保存成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
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
        /// 重置表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetTableDataRelation(false);
        }

        /// <summary>
        /// 清除表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetTableDataRelation(true);
        }

        /// <summary>
        /// 右键点击表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridDataField_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
            {
                popupMenuDataField.ShowPopup(Control.MousePosition);
            }
        }

        /// <summary>
        /// 右键点击字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridTable_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
            {
                popupMenu.ShowPopup(Control.MousePosition);
            }
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridTable_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridDataField_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 表格选择变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridTable_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                decimal sourceTableId = Convert.ToDecimal(gridTable.GetRowCellValue(e.RowHandle, "SourceId"));
                decimal destinationTableId = Convert.ToDecimal(e.Value);
                if (dataFieldRelation.ContainsKey(sourceTableId))
                {
                    dataFieldRelation.Remove(sourceTableId);
                }
                ConstructDataFieldRelation(sourceTableId, destinationTableId);
                ShowDataFieldRelation(sourceTableId, destinationTableId);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 点击行选择表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridTable_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                decimal sourceTableId = Convert.ToDecimal(gridTable.GetFocusedRowCellValue("SourceId"));
                decimal destinationTableId = Convert.ToDecimal(gridTable.GetFocusedRowCellValue("DestinationId"));
                ConstructDataFieldRelation(sourceTableId, destinationTableId);
                ShowDataFieldRelation(sourceTableId, destinationTableId);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 字段选择变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridDataField_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                decimal sourceTableId = Convert.ToDecimal(gridTable.GetFocusedRowCellValue("SourceId"));
                if (dataFieldRelation.ContainsKey(sourceTableId))
                {
                    DataTable dt = dataFieldRelation[sourceTableId];
                    decimal sourceDataFieldId = Convert.ToDecimal(gridDataField.GetFocusedRowCellValue("SourceId"));
                    DataRow[] drs = dt.Select(string.Format("SourceId = {0}", sourceDataFieldId));
                    if (drs != null && drs.Length > 0)
                    {
                        drs[0]["DestinationId"] = e.Value;
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 重置字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmResetDataField_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            decimal sourceTableId = Convert.ToDecimal(gridTable.GetFocusedRowCellValue("SourceId"));
            decimal destinationTableId = Convert.ToDecimal(gridTable.GetFocusedRowCellValue("DestinationId"));
            if (dataFieldRelation.ContainsKey(sourceTableId))
            {
                dataFieldRelation.Remove(sourceTableId);
            }
            ConstructDataFieldRelation(sourceTableId, destinationTableId);
            ShowDataFieldRelation(sourceTableId, destinationTableId);
        }

        /// <summary>
        /// 清除字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmClearDataField_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            decimal sourceTableId = Convert.ToDecimal(gridTable.GetFocusedRowCellValue("SourceId"));
            if (dataFieldRelation.ContainsKey(sourceTableId))
            {
                DataTable dt = dataFieldRelation[sourceTableId];
                foreach (DataRow dr in dt.Rows)
                {
                    dr["DestinationId"] = 0;
                }
            }
        }


        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmReset_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetTableDataRelation(false);
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmClear_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetTableDataRelation(true);
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmResetDataField_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            decimal sourceTableId = Convert.ToDecimal(gridTable.GetFocusedRowCellValue("SourceId"));
            decimal destinationTableId = Convert.ToDecimal(gridTable.GetFocusedRowCellValue("DestinationId"));
            if (dataFieldRelation.ContainsKey(sourceTableId))
            {
                dataFieldRelation.Remove(sourceTableId);
            }
            ConstructDataFieldRelation(sourceTableId, destinationTableId);
            ShowDataFieldRelation(sourceTableId, destinationTableId);
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmClearDataField_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            decimal sourceTableId = Convert.ToDecimal(gridTable.GetFocusedRowCellValue("SourceId"));
            if (dataFieldRelation.ContainsKey(sourceTableId))
            {
                DataTable dt = dataFieldRelation[sourceTableId];
                foreach (DataRow dr in dt.Rows)
                {
                    dr["DestinationId"] = 0;
                }
            }
        }

        #endregion
        
        #region 私有方法

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        private void SetActiveStatesOfControls(bool readOnly)
        {
            gridTable.OptionsBehavior.ReadOnly = readOnly;
            gridDataField.OptionsBehavior.ReadOnly = readOnly;
        }

        /// <summary>
        /// 清除窗体右侧的控件中的数据
        /// </summary>
        private void ClearDataOnControls()
        {
            gcTableRelation.DataSource = CreateTableSource();
            gcDataFieldRelation.DataSource = CreateDataFieldSource();
        }

        /// <summary>
        /// 设置表和字段的对应关系
        /// </summary>
        /// <param name="remoteDatabaseId"></param>
        /// <param name="destinationDatabaseId"></param>
        /// <param name="tableRelation"></param>
        private void SetTableDataRelation(decimal remoteDatabaseId, decimal destinationDatabaseId, Dictionary<decimal, Dictionary<decimal, decimal>> relations)
        {
            /* 源数据库对应的表 */
            DataTable dataTable = CreateTableSource();
            gcTableRelation.DataSource = dataTable;
            IList<CommonNode> commonNodes = remoteServerContract.GetCommonNodesByDatabaseId(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, remoteDatabaseId);
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
            foreach (CommonNode node in commonNodes)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["SourceId"] = node.NodeId;
                dataRow["SourceName"] = node.NodeName;
                if (tableIds.ContainsKey(node.NodeId))
                {                    
                    dataRow["DestinationId"] = tableIds[node.NodeId];
                }
                else
                {
                    dataRow["DestinationId"] = 0;
                }
                dataTable.Rows.Add(dataRow);                
            }

            /* 目的数据库的表 */
            ricmbDestinationTable.Items.Clear();
            ricmbDestinationTable.Items.Add(new ImageComboBoxItem(string.Empty, 0, -1));
            IList<CommonNode> nodes = customTableContract.GetCommonNodesByDatabaseId(destinationDatabaseId);
            foreach (CommonNode node in nodes)
            {
                ImageComboBoxItem imageComboBoxItem = new ImageComboBoxItem(node.NodeName, node.NodeId, 0);
                ricmbDestinationTable.Items.Add(imageComboBoxItem);
            }
            ConstructDataFieldRelation();
        }

        /// <summary>
        /// 设置表和字段的对应关系
        /// </summary>
        /// <param name="sourceDatabaseId"></param>
        /// <param name="destinationDatabaseId"></param>
        /// <param name="tableRelation"></param>
        private void SetTableDataRelation(decimal sourceDatabaseId, decimal destinationDatabaseId, Dictionary<decimal, decimal> tableRelation)
        {
            /* 源数据库对应的表 */
            DataTable dataTable = CreateTableSource();
            gcTableRelation.DataSource = dataTable;
            IList<CommonNode> commonNodes = customTableContract.GetCommonNodesByDatabaseId(sourceDatabaseId);
            foreach (CommonNode node in commonNodes)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["SourceId"] = node.NodeId;
                dataRow["SourceName"] = node.NodeName;
                if (tableRelation.ContainsKey(node.NodeId))
                {
                    dataRow["DestinationId"] = tableRelation[node.NodeId];
                }
                else
                {
                    dataRow["DestinationId"] = 0;
                }
                dataTable.Rows.Add(dataRow);
            }

            /* 目的数据库的表 */
            ricmbDestinationTable.Items.Clear();
            ricmbDestinationTable.Items.Add(new ImageComboBoxItem(string.Empty, 0, -1));
            IList<CommonNode> nodes = customTableContract.GetCommonNodesByDatabaseId(destinationDatabaseId);
            foreach (CommonNode node in nodes)
            {
                ImageComboBoxItem imageComboBoxItem = new ImageComboBoxItem(node.NodeName, node.NodeId, 0);
                ricmbDestinationTable.Items.Add(imageComboBoxItem);
            }
            ConstructDataFieldRelation();
        }
        
        /// <summary>
        /// 设置表的关系
        /// </summary>
        /// <param name="clearDestTable"></param>
        private void SetTableDataRelation(bool clearDestTable)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                /* 源数据库 */
                DataTable dataTable = gcTableRelation.DataSource as DataTable;
                dataTable.Rows.Clear();
                dataFieldRelation.Clear();
                IList<CommonNode> sourceCommonNodes = customTableContract.GetChildNodes(dataRelationInfo.ParentDatabaseId);
                foreach (CommonNode node in sourceCommonNodes)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["SourceId"] = node.NodeId;
                    dataRow["SourceName"] = node.NodeName;
                    dataRow["DestinationId"] = 0;
                    dataTable.Rows.Add(dataRow);
                }

                /* 目的数据库 */
                ricmbDestinationTable.Items.Clear();
                ricmbDestinationTable.Items.Add(new ImageComboBoxItem(string.Empty, 0, -1));
                IList<CommonNode> commonNodes = customTableContract.GetChildNodes(dataRelationInfo.DatabaseId);
                foreach (CommonNode node in commonNodes)
                {
                    ImageComboBoxItem imageComboBoxItem = new ImageComboBoxItem(node.NodeName, node.NodeId, 0);
                    ricmbDestinationTable.Items.Add(imageComboBoxItem);
                }
                int count = dataTable.Rows.Count <= commonNodes.Count ? dataTable.Rows.Count : commonNodes.Count;
                for (int i = 0; i < count; i++)
                {
                    if (clearDestTable)
                    {
                        dataTable.Rows[i]["DestinationId"] = 0;
                    }
                    else
                    {
                        dataTable.Rows[i]["DestinationId"] = commonNodes[i].NodeId;
                    }
                }
                ConstructDataFieldRelation();
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 构造多表的字段对应关系
        /// </summary>
        private void ConstructDataFieldRelation()
        {
            DataTable dataTable = gcTableRelation.DataSource as DataTable;
            foreach (DataRow dr in dataTable.Rows)
            {
                decimal sourceTableId = Convert.ToDecimal(dr["SourceId"]);
                decimal destinationTableId = Convert.ToDecimal(dr["DestinationId"]);
                ConstructDataFieldRelation(sourceTableId, destinationTableId);
            }
            ShowDataFieldRelation();
        }

        /// <summary>
        /// 构造表的字段对应关系
        /// </summary>
        /// <param name="sourceTableId"></param>
        /// <param name="destinationTableId"></param>
        private void ConstructDataFieldRelation(decimal sourceTableId, decimal destinationTableId)
        {
            if (sourceTableId <= 0)
            {
                throw new ArgumentException();
            }
            if (!dataFieldRelation.ContainsKey(sourceTableId))
            {
                DataTable dt = CreateDataFieldSource();
                IList<CommonNode> sourceCommonNodes = null;
                switch (DataSourceTypeValue)
                {
                    case DataSourceType.Local:
                        sourceCommonNodes = customDataFieldContract.GetCommonNodes(sourceTableId, DataFieldFilter.PhysicalFieldAndLogicalField);
                        break;

                    case DataSourceType.Remote:
                        sourceCommonNodes = remoteServerContract.GetCommonNodesByTableId(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, sourceTableId, DataFieldFilter.PhysicalFieldAndLogicalField);
                        break;
                }               
                foreach (CommonNode sourceCommonNode in sourceCommonNodes)
                {
                    DataRow dataRow = dt.NewRow();
                    dataRow["SourceId"] = sourceCommonNode.NodeId;
                    dataRow["SourceName"] = sourceCommonNode.NodeName;
                    dataRow["DestinationId"] = 0;
                    dt.Rows.Add(dataRow);
                }
                if (destinationTableId > 0)
                {
                    IList<CommonNode> destinationCommonNodes = customDataFieldContract.GetCommonNodes(destinationTableId, DataFieldFilter.OnlyPhysicalField);
                    Dictionary<decimal, decimal> relation = null;
                    switch (DataSourceTypeValue)
                    {
                        case DataSourceType.Local:
                            relation = dataRelationContract.GetDataFieldRelation(RelationId, sourceTableId, destinationTableId);
                            break;

                        case DataSourceType.Remote:
                            IList<CustomDataFieldInfo> customDataFieldInfos = remoteServerContract.GetModelInfosByTableId(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, sourceTableId, DataFieldFilter.PhysicalFieldAndLogicalField);
                            Dictionary<decimal, decimal> dataFieldRelations = remoteDataContract.GetDataFieldRelation(RelationId, destinationTableId);
                            relation = new Dictionary<decimal, decimal>();
                            foreach (var dataFieldRelation in dataFieldRelations)
                            {
                                int pos = customDataFieldInfos.FindIndex(customDataFieldInfo => customDataFieldInfo.DataFieldId == dataFieldRelation.Key);
                                if (pos >= 0)
                                {
                                    relation.Add(dataFieldRelation.Key, dataFieldRelation.Value);
                                }                               
                            }
                            break;
                    }            
                    bool hasDestionation = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal dataFieldId = (decimal)dr["SourceId"];
                        if (relation.ContainsKey(dataFieldId))
                        {
                            hasDestionation = true;
                            dr["DestinationId"] = relation[dataFieldId];
                        }                        
                    }
                    if (!hasDestionation)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            decimal dataFieldId = (decimal)dr["SourceId"];
                            if (!relation.ContainsKey(dataFieldId))
                            {
                                string name = Convert.ToString(dr["SourceName"]);
                                int index = destinationCommonNodes.FindIndex(node => node.NodeName.Equals(name));
                                if (index >= 0)
                                {
                                    dr["DestinationId"] = destinationCommonNodes[index].NodeId;
                                }
                                else
                                {
                                    dr["DestinationId"] = 0;
                                }
                            }
                        }
                    }
                }
                dataFieldRelation.Add(sourceTableId, dt);
            }
        }

        /// <summary>
        /// 显示表的字段对应关系
        /// </summary>
        /// <param name="sourceTableId"></param>
        /// <param name="destinationTableId"></param>
        private void ShowDataFieldRelation(decimal sourceTableId, decimal destinationTableId)
        {
            if (dataFieldRelation.ContainsKey(sourceTableId))
            {
                DataTable dt = dataFieldRelation[sourceTableId];
                ricmbDestinationDataField.Items.Clear();
                ricmbDestinationDataField.Items.Add(new ImageComboBoxItem(string.Empty, 0, -1));
                if (destinationTableId > 0)
                {
                    IList<CommonNode> destinationCommonNodes = customDataFieldContract.GetCommonNodes(destinationTableId, DataFieldFilter.OnlyPhysicalField);
                    foreach (CommonNode node in destinationCommonNodes)
                    {
                        ImageComboBoxItem imageComboBoxItem = new ImageComboBoxItem(node.NodeName, node.NodeId, 1);
                        ricmbDestinationDataField.Items.Add(imageComboBoxItem);
                    }
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["DestinationId"] = 0;
                    }
                }
                gcDataFieldRelation.DataSource = dataFieldRelation[sourceTableId];
            }
            else
            {
                gcDataFieldRelation.DataSource = null;
            }
        }

        /// <summary>
        /// 显示第一个表的字段对应关系
        /// </summary>
        private void ShowDataFieldRelation()
        {
            DataTable dataTable = gcTableRelation.DataSource as DataTable;
            if (gridTable.RowCount > 0)
            {
                gridTable.FocusedRowHandle = 0;
                decimal sourceTableId = Convert.ToDecimal(dataTable.Rows[0]["SourceId"]);
                decimal destinationTableId = Convert.ToDecimal(dataTable.Rows[0]["DestinationId"]);
                ShowDataFieldRelation(sourceTableId, destinationTableId);
            }
        }

        /// <summary>
        /// 创建表的数据源
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTableSource()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SourceId", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("SourceName", Type.GetType("System.String"));
            dataTable.Columns.Add("DestinationId", Type.GetType("System.Decimal"));

            return dataTable;
        }

        /// <summary>
        /// 创建字段的数据源
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataFieldSource()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SourceId", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("SourceName", Type.GetType("System.String"));
            dataTable.Columns.Add("DestinationId", Type.GetType("System.Decimal"));

            return dataTable;
        }

        #endregion
    }

}
