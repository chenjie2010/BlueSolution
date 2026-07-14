using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class DataExchangeForm : Form
    {
        #region 契约接口

        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomExpressionContract customExpressionContract;
        private readonly IDataBussinessContract dataBussinessContract;

        #endregion

        #region 私有变量

        private RowColCopyType currentRowColCopyType = RowColCopyType.DuplicateRow;
        private IDictionary<decimal, decimal> dataFieldRelations = new Dictionary<decimal, decimal>();
        private IDictionary<decimal, string> customDataFieldNames = new Dictionary<decimal, string>();
        private decimal sourceTableId = 0;
        private decimal destTableId = 0;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataExchangeForm()
        {
            InitializeComponent();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customExpressionContract = BusinessChannelFactory.CreateCustomExpressionContract();
            dataBussinessContract = BusinessChannelFactory.CreateDataBussinessContract();
            UserControlHelper.InitImageComboBoxEdit(icmbImport, typeof(RowColCopyType));
        }

        #endregion

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataExchangeForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            gcDataFieldRelation.DataSource = CreateDataTable();
        }

        /// <summary>
        /// 清除条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRowColClear_Click(object sender, EventArgs e)
        {
            beSourceTable.Text = string.Empty;
            beSourceTable.Text = null;
            beDestTable.Text = string.Empty;
            beDestTable.Tag = null;
            icmbImport.SelectedIndex = 0;
        }

        /// <summary>
        /// 选择源表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beSourceTable_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SelectTable(beSourceTable, true);
        }

        /// <summary>
        /// 选择目标表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beDestTable_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SelectTable(beDestTable, false);
        }

        /// <summary>
        /// 设置源字段条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSourceDataField_Click(object sender, EventArgs e)
        {
            if (beSourceTable.Tag != null && cmbSourceDataField.SelectedItem != null)
            {
                CommonNode tableNode = beSourceTable.Tag as CommonNode;
                CommonNode commonNode = (CommonNode)cmbSourceDataField.SelectedItem;
                SetCondition(tableNode.NodeId, commonNode.NodeId, txtSourceDataField);
            }
        }

        /// <summary>
        /// 设置目标字段条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDestDataField_Click(object sender, EventArgs e)
        {
            if (beDestTable.Tag != null && cmbDestDataField.SelectedItem != null)
            {
                CommonNode tableNode = beDestTable.Tag as CommonNode;
                CommonNode commonNode = (CommonNode)cmbDestDataField.SelectedItem;
                SetCondition(tableNode.NodeId, commonNode.NodeId, txtDestDataField);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRowColQuery_Click(object sender, EventArgs e)
        {
            if (beSourceTable.Tag == null)
            {
                MessageBox.Show("请先选择源表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode sourceCommonNode = beSourceTable.Tag as CommonNode;
            if (beDestTable.Tag == null)
            {
                MessageBox.Show("请先选择目的表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode destCommonNode = beDestTable.Tag as CommonNode;
            RowColCopyType rowColCopyType = (RowColCopyType)Convert.ToByte(icmbImport.EditValue);
            if (rowColCopyType == RowColCopyType.DuplicateRow && sourceCommonNode.NodeId == destCommonNode.NodeId)
            {
                MessageBox.Show("行复制时不允许源表和目的表相同！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            currentRowColCopyType = rowColCopyType;
            try
            {
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
                            if (sourceId > 0 && !dataFieldRelations.ContainsKey(sourceId))
                            {
                                dataFieldRelations.Add(sourceId, destinationId);
                                if (rowColCopyType == RowColCopyType.AlternativeCol && sourceId == destinationId)
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
                if (rowColCopyType == RowColCopyType.AlternativeCol && !string.IsNullOrWhiteSpace(destDataField))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" AND ");
                    }
                    sb.AppendFormat("({0})", destDataField);
                }
                devExpressGrid.Tag = sb.ToString();
                sourceTableId = sourceCommonNode.NodeId;
                destTableId = destCommonNode.NodeId;
                Cursor = Cursors.WaitCursor;
                int totalCount = 0;
                devExpressGrid.CurrentPageIndex = 0;
                DataSet ds = dataBussinessContract.GetQueriedData(sourceCommonNode.NodeId, destCommonNode.NodeId, rowColCopyType, dataFieldRelations,
                customDataFieldNames, sb.ToString(), devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex, devExpressGrid.PageSize, ref totalCount);
                devExpressGrid.DataSource = ds.Tables[0];
                devExpressGrid.RecordCount = totalCount;
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRowColSumbit_Click(object sender, EventArgs e)
        {
            if (beSourceTable.Tag == null)
            {
                MessageBox.Show("请先选择源表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode sourceCommonNode = beSourceTable.Tag as CommonNode;
            if (beDestTable.Tag == null)
            {
                MessageBox.Show("请先选择目的表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode destCommonNode = beDestTable.Tag as CommonNode;
            RowColCopyType rowColCopyType = (RowColCopyType)Convert.ToByte(icmbImport.EditValue);
            if (rowColCopyType == RowColCopyType.DuplicateRow && sourceCommonNode.NodeId == destCommonNode.NodeId)
            {
                MessageBox.Show("行复制时不允许源表和目的表相同！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
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
                                if (rowColCopyType == RowColCopyType.AlternativeCol && sourceId == destinationId)
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
                if (rowColCopyType == RowColCopyType.AlternativeCol && !string.IsNullOrWhiteSpace(destDataField))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" AND ");
                    }
                    sb.AppendFormat("({0})", destDataField);
                }
                int count = 0;
                Cursor = Cursors.WaitCursor;
                if (rowColCopyType == RowColCopyType.DuplicateRow)
                {
                    count = dataBussinessContract.Import(sourceCommonNode.NodeId, destCommonNode.NodeId, dataFieldRelations,
                        customDataFieldNames, sb.ToString());
                }
                else
                {
                    count = dataBussinessContract.Update(sourceCommonNode.NodeId, destCommonNode.NodeId, dataFieldRelations,
                        customDataFieldNames, sb.ToString());
                }
                Cursor = Cursors.Default;
                MessageBox.Show(string.Format("{0}操作成功，共有{1}记录被处理！", icmbImport.Text, count), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            int totalCount = 0;
            devExpressGrid.CurrentPageIndex = e.NewPageIndex;
            DataSet ds = dataBussinessContract.GetQueriedData(sourceTableId, destTableId, currentRowColCopyType, dataFieldRelations,
                    customDataFieldNames, devExpressGrid.Tag.ToString(), devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex, devExpressGrid.PageSize, ref totalCount);
            devExpressGrid.DataSource = ds.Tables[0];
            devExpressGrid.RecordCount = totalCount;
        }
        
        /// <summary>
        /// 设置源值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rsitmCustomValue_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (beSourceTable.Tag != null)
            {
                CommonNode commonNode = beSourceTable.Tag as CommonNode;
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

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CommonNode commonNode = beSourceTable.Tag as CommonNode;
            LoadSourceDataField(commonNode);
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dataTable = gcDataFieldRelation.DataSource as DataTable;
            foreach (DataRow dr in dataTable.Rows)
            {
                dr["SourceId"] = DBNull.Value;
            }
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
            {
                popupMenu.ShowPopup(Control.MousePosition);
            }
        }

        #endregion

        #region 私有方法

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
            dataTable.Columns.Add("CustomDataFieldName", Type.GetType("System.String"));

            return dataTable;
        }

        /// <summary>
        /// 加载源字段
        /// </summary>
        /// <param name="commonNode"></param>
        private void LoadSourceDataField(CommonNode commonNode)
        {
            if (commonNode == null)
            {
                return;
            }
            IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodes(commonNode.NodeId, DataFieldFilter.PhysicalFieldAndLogicalField);
            byte sourceDataWarehouseId = customTableContract.GetDataWarehouseId(commonNode.NodeId);
            DataTable dataTable = gcDataFieldRelation.DataSource as DataTable;
            ricmbSource.Items.Clear();
            ricmbSource.Items.Add(new ImageComboBoxItem(string.Empty, 0, -1)); /* 第一个选项为空 */
            cmbSourceDataField.Properties.Items.Clear();
            txtSourceDataField.Text = string.Empty;

            foreach (DataRow dr in dataTable.Rows)
            {
                string name = Convert.ToString(dr["DestinationName"]);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    int index = commonNodes.FindIndex(node => node.NodeName.Equals(name));
                    if (index >= 0)
                    {
                        dr["SourceId"] = commonNodes[index].NodeId;
                    }
                    else
                    {
                        dr["SourceId"] = 0;
                    }
                }
            }
        

            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    if (i < commonNodes.Count)
            //    {
            //        dataTable.Rows[i]["SourceId"] = commonNodes[i].NodeId;
            //    }
            //    else
            //    {
            //        dataTable.Rows[i]["SourceId"] = 0;
            //    }
            //    //dataTable.Rows[i]["SourceCondition"] = string.Empty;
            //}
            IList<CommonNode> systemCommonNodes = DataFieldHelper.GetSystemDataFieldCommonNodes();
            for (int i = 0; i < systemCommonNodes.Count; i++)
            {
                ImageComboBoxItem imageComboBoxItem = new ImageComboBoxItem(systemCommonNodes[i].NodeName, systemCommonNodes[i].NodeId * -1, 0);
                ricmbSource.Items.Add(imageComboBoxItem);
                cmbSourceDataField.Properties.Items.Add(new CommonNode(systemCommonNodes[i].NodeId * -1, systemCommonNodes[i].NodeName));
            }
            for (int i = 0; i < commonNodes.Count; i++)
            {
                ImageComboBoxItem imageComboBoxItem = new ImageComboBoxItem(commonNodes[i].NodeName, commonNodes[i].NodeId, 0);
                ricmbSource.Items.Add(imageComboBoxItem);
                cmbSourceDataField.Properties.Items.Add(new CommonNode(commonNodes[i].NodeId, commonNodes[i].NodeName));
            }
        }

        /// <summary>
        /// 加载目标字段
        /// </summary>
        /// <param name="commonNode"></param>
        private void LoadDestDataField(CommonNode commonNode)
        {
            IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodes(commonNode.NodeId, DataFieldFilter.OnlyPhysicalField);
            byte destDataWarehouseId = customTableContract.GetDataWarehouseId(commonNode.NodeId);
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
            IList<CommonNode> systemCommonNodes = DataFieldHelper.GetSystemDataFieldCommonNodes();
            for (int i = 0; i < systemCommonNodes.Count; i++)
            {
                cmbDestDataField.Properties.Items.Add(new CommonNode(systemCommonNodes[i].NodeId * -1, systemCommonNodes[i].NodeName));
            }
            for (int i = 0; i < commonNodes.Count; i++)
            {
                dataTable.Rows[i]["DestinationId"] = commonNodes[i].NodeId;
                dataTable.Rows[i]["DestinationName"] = commonNodes[i].NodeName;
                cmbDestDataField.Properties.Items.Add(new CommonNode(commonNodes[i].NodeId, commonNodes[i].NodeName));
            }
            CommonNode node = beSourceTable.Tag as CommonNode;
            LoadSourceDataField(node);
        }

        /// <summary>
        /// 选择表
        /// </summary>
        /// <param name="buttonEdit"></param>
        /// <param name="source"></param>
        private void SelectTable(ButtonEdit buttonEdit, bool source)
        {
            DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
            frmDataTableItems.TableFilter = TableFilter.All;
            frmDataTableItems.DataWarehouseId = 0;
            frmDataTableItems.Text = "数据表选择";
            frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
            frmDataTableItems.NodeSelected = delegate (CommonNode commonNode)
            {
                if (commonNode != null)
                {
                    buttonEdit.Text = customTableContract.GetFullName(commonNode.NodeId);
                    buttonEdit.Tag = commonNode;
                    if (source)
                    {
                        LoadSourceDataField(commonNode);
                    }
                    else
                    {
                        LoadDestDataField(commonNode);
                    }
                }
                else
                {
                    buttonEdit.Text = string.Empty;
                    buttonEdit.Tag = null;
                }
            };
            frmDataTableItems.ShowDialog();
        }

        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="memoEdit"></param>
        private void SetCondition(decimal tableId, decimal dataFieldId, MemoEdit memoEdit)
        {
            CustomDataFieldInfo customDataFieldInfo = null;
            QueryConditionForm frmQueryCondition = new QueryConditionForm();
            frmQueryCondition.QueryCondition = memoEdit.Text;
            frmQueryCondition.IsValidate = true;
            if (dataFieldId > 0)
            {
                customDataFieldInfo = customDataFieldContract.GetModelInfo(dataFieldId);
                frmQueryCondition.TableName = customTableContract.GetTablePhysicalName(customDataFieldInfo.TableId);
                if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.LogicalDataField)
                {
                    IList<CommonNode> commonNodes = customExpressionContract.GetCommonNodes(customDataFieldInfo.DataFieldId);
                    customDataFieldInfo.PhysicalName = customDataFieldContract.GetExpressionDataFieldName(frmQueryCondition.TableName, customDataFieldInfo.ExpressionText, commonNodes);
                }
            }
            else
            {
                frmQueryCondition.TableName = customTableContract.GetTablePhysicalName(tableId);
                SystemDataField systemDataField = (SystemDataField)Convert.ToByte(dataFieldId * -1);
                customDataFieldInfo = CommonBussinessHelper.GetExtendedCustomDataFieldInfo(tableId, frmQueryCondition.TableName, systemDataField);
            }
            frmQueryCondition.CustomDataFieldInfo = customDataFieldInfo;
            frmQueryCondition.UpdateTextHandler = (where) =>
            {
                memoEdit.Text = where;
            };
            frmQueryCondition.ShowDialog();
        }

        #endregion
    }
}