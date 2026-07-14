using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.DataFieldLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.BusinessDesignerModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class CellConditionSettingForm : Form
    {
        #region 私有变量
      
        private TreeNode previousSelectedTreeNode = null;

        #endregion

        #region 契约接口

        private readonly ICustomDatabaseContract customDatabaseContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomExpressionContract customExpressionContract;
        private readonly ICustomReportContract customReportContract;
        private readonly ICustomSheetContract customSheetContract;
        private readonly ICustomCellContract customCellContract;

        #endregion

        #region 内部成员变量

        private IList<CustomCellInfo> _customCellInfos;
        private QueryReportType _reportType;

        #endregion

        #region 属性

        /// <summary>
        /// 单元格列表
        /// </summary>
        public IList<CustomCellInfo> CustomCellInfos
        {
            set
            {
                _customCellInfos = value;               
            }
            get
            {
                return _customCellInfos;
            }
        }

        /// <summary>
        /// 活跃的表格
        /// </summary>
        public FarPoint.Win.Spread.SheetView ActiveSheetView
        {
            get;
            set;
        }

        /// <summary>
        /// 查询报表格式类型
        /// </summary>
        public QueryReportType ReportType
        {
            set
            {
                _reportType = value;
            }
            get
            {
                return _reportType;
            }
        }
        
        /// <summary>
        /// 设置或是清除背景颜色
        /// </summary>
        public SetBackColorDelegate SetBackColor
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public CellConditionSettingForm()
        {
            InitializeComponent();
            customDatabaseContract = BusinessChannelFactory.CreateCustomDatabaseContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customExpressionContract = BusinessChannelFactory.CreateCustomExpressionContract();
            customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();
            customSheetContract = BusinessDesignerChannelFactory.CreateCustomSheetContract();
            customCellContract = BusinessDesignerChannelFactory.CreateCustomCellContract();
        }

        #endregion

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellConditionSettingForm_Load(object sender, EventArgs e)
        {            
            btnItmSave.Enabled = false;
            switch (ReportType)
            {
                case QueryReportType.Basic:
                    UserControlHelper.InitImageComboBoxEdit(icmbCellConditon, typeof(BasicCellType));
                    break;

                case QueryReportType.Statistics:
                    UserControlHelper.InitImageComboBoxEdit(icmbCellConditon, typeof(StatisticCellType));
                    break;
            }
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldShowProperty, typeof(DataFieldShowProperty));
            icmbCellConditon.SelectedIndex = 0;
            lblCellConditon.Text = "单元格条件类型：";
            grpCellTemplate.Text = "单元格显示设置";
            LoadData();
        }


        private void etxtCondition_TextChanged(object sender, EventArgs e)
        {
            btnItmSave.Enabled = true;
        }

        private void etxtTemplate_TextChanged(object sender, EventArgs e)
        {
            btnItmSave.Enabled = true;
        }

        /// <summary>
        /// 条件类型发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbCellConditon_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ReportType)
            {
                case QueryReportType.Basic:
                    BasicCellType basicCellType = (BasicCellType)Convert.ToByte(icmbCellConditon.EditValue);
                    switch (basicCellType)
                    {
                        case BasicCellType.OnlyData:
                            ccmbDataFieldShowProperty.ReadOnly = false;
                            grpDataFieldShow.Enabled = true;
                            grpCellTemplate.Enabled = true;
                            lblRowOrCol.Visible = false;
                            etxtRowOrCol.Visible = false;
                            lblRowOrColTip.Visible = false;
                            lblConditionTip.Visible = false;
                            etxtRowOrCol.Text = string.Empty;
                            break;

                        case BasicCellType.ExtendRow:
                        case BasicCellType.ExtendRowByCondtion:
                        case BasicCellType.ExtendCol:
                        case BasicCellType.ExtendColByCondtion:
                            if (basicCellType == BasicCellType.ExtendRow || basicCellType == BasicCellType.ExtendRowByCondtion)
                            {
                                lblRowOrCol.Text = "可扩展行数：";
                            }
                            else
                            {
                                lblRowOrCol.Text = "可扩展列数：";
                            }
                            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataFieldShowProperty);
                            ccmbDataFieldShowProperty.ReadOnly = true;
                            lblRowOrCol.Visible = true;
                            etxtRowOrCol.Visible = true;
                            lblRowOrColTip.Visible = true;
                            lblConditionTip.Visible = true;
                            grpDataFieldShow.Enabled = true;
                            grpCellTemplate.Enabled = false;
                            break;
                    }
                    break;

                case QueryReportType.Statistics:
                    StatisticCellType statisticCellType = (StatisticCellType)Convert.ToByte(icmbCellConditon.EditValue);
                    switch (statisticCellType)
                    {
                        case StatisticCellType.OnlyData:
                        case StatisticCellType.OnlyValue:
                        case StatisticCellType.Detail:
                            ccmbDataFieldShowProperty.ReadOnly = false;
                            grpDataFieldShow.Enabled = true;
                            grpCellTemplate.Enabled = true;
                            lblRowOrCol.Visible = false;
                            etxtRowOrCol.Visible = false;
                            lblRowOrColTip.Visible = false;
                            lblConditionTip.Visible = false;
                            etxtRowOrCol.Text = string.Empty;
                            break;                            

                        case StatisticCellType.RowAndColumnCondition:
                        case StatisticCellType.RowCondition:
                        case StatisticCellType.ColumnCondition:
                        case StatisticCellType.GlobalCondition:
                            lstDataFieldShow.Items.Clear();
                            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataFieldShowProperty);
                            ccmbDataFieldShowProperty.ReadOnly = true;
                            lstDataFieldShow.Items.Clear();
                            meTemplate.Text = string.Empty;
                            grpDataFieldShow.Enabled = false;
                            grpCellTemplate.Enabled = false;                            
                            break;
                    }
                    break;
            }
            btnItmSave.Enabled = true;
        }        

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (btxtDataTable.Tag == null)
                {
                    MessageBox.Show("请先选中表！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                string condition = meCondition.Text.Trim();
                int number = 0;
                string tablePhysicalName = customTableContract.GetTablePhysicalName(GetTableId());
                byte cellType = Convert.ToByte(icmbCellConditon.EditValue);                  
                if (!DataFieldHelper.IsDataCell(ReportType, cellType) && string.IsNullOrWhiteSpace(condition))
                {
                    MessageBox.Show("请设置条件。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string expression = GetExpression(tablePhysicalName);
                Cursor = Cursors.WaitCursor;                
                IList<CommonNode> dataFieldConditions = new List<CommonNode>();
                decimal tableId = GetTableId();
                foreach (object item in lstDataFieldCondition.Items)
                {
                    CommonNode commonNode = item as CommonNode;
                    if ((DataFieldProperty)commonNode.NodeType != DataFieldProperty.SystemPhysicalDataField)
                    {
                        if (commonNode.ParentNodeId != tableId)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("条件字段{0}不在选择的表中，请清除重新设置！", commonNode.NodeName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    dataFieldConditions.Add(commonNode);
                }
                IList<CommonNode> dataFieldShows = new List<CommonNode>();
                foreach (object item in lstDataFieldShow.Items)
                {
                    CommonNode commonNode = item as CommonNode;
                    if ((DataFieldProperty)commonNode.NodeType != DataFieldProperty.SystemPhysicalDataField && commonNode.ParentNodeId != tableId)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(string.Format("显示字段{0}不在选择的表中，请清除重新设置！", commonNode.NodeName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    dataFieldShows.Add(commonNode);                    
                }                
                bool success = ValidateCellCondition(tablePhysicalName, expression, condition);
                if (!success)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("条件验证失败！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if ((ReportType == QueryReportType.Basic &&  (BasicCellType)cellType != BasicCellType.OnlyData)
                    || (ReportType == QueryReportType.Statistics && (StatisticCellType)cellType == StatisticCellType.Detail))                  
                {
                    string rowOrCol = etxtRowOrCol.Text.Trim();
                    if (string.IsNullOrWhiteSpace(rowOrCol))
                    {
                        MessageBox.Show("可扩展行数或是列数不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Regex r = new Regex(@"^\d+$");
                    if (r.IsMatch(rowOrCol))
                    {
                        number = Int32.Parse(rowOrCol);
                        if (number <= 0 || number > 2000)
                        {
                            MessageBox.Show("可扩展行数或是列数必须为正整数，且范围在(0~2000)！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("可扩展行数或是列数必须为正整数，且范围在(0~2000)！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (_customCellInfos.Count > 1)
                    {
                        MessageBox.Show("扩展单元格不能批量设置！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (_customCellInfos.Count == 0)
                    {
                        MessageBox.Show("没有选中单元格，请重新选中！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (ReportType == QueryReportType.Basic)
                    {
                        BasicCellType basicCellType = (BasicCellType)cellType;
                        if (basicCellType != BasicCellType.OnlyData)
                        {
                            if (basicCellType == BasicCellType.ExtendRow || basicCellType == BasicCellType.ExtendRowByCondtion)
                            {
                                if (ActiveSheetView.ActiveCell.Column.Index + lstDataFieldShow.Items.Count > ActiveSheetView.ColumnCount)
                                {
                                    MessageBox.Show("显示的字段过多，已超过了表格的列允许的范围！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                for (int rowIndex = 0; rowIndex < number; rowIndex++)
                                {
                                    for (int colIndex = 0; colIndex < lstDataFieldShow.Items.Count; colIndex++)
                                    {
                                        FarPoint.Win.Spread.Cell cell = ActiveSheetView.Cells[ActiveSheetView.ActiveCell.Row.Index + rowIndex, ActiveSheetView.ActiveCell.Column.Index + colIndex];
                                        if (cell.RowSpan > 1)
                                        {
                                            MessageBox.Show("行扩展类型中不允许有行合并的单元格存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (ActiveSheetView.ActiveCell.Row.Index + lstDataFieldShow.Items.Count > ActiveSheetView.RowCount)
                                {
                                    MessageBox.Show("显示的字段过多，已超过了表格的行允许的范围！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                for (int rowIndex = 0; rowIndex < lstDataFieldShow.Items.Count; rowIndex++)
                                {
                                    for (int colIndex = 0; colIndex < number; colIndex++)
                                    {
                                        FarPoint.Win.Spread.Cell cell = ActiveSheetView.Cells[ActiveSheetView.ActiveCell.Row.Index + rowIndex, ActiveSheetView.ActiveCell.Column.Index + colIndex];
                                        if (cell.ColumnSpan > 1)
                                        {
                                            MessageBox.Show("列扩展类型中不允许有列合并的单元格存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        IList<CommonNode> commonNodes = customCellContract.GetCommonNodesByCellId(_customCellInfos[0].CellId, CellCondition.Show);
                        if (SetBackColor != null)
                        {
                            switch (basicCellType)
                            {
                                case BasicCellType.ExtendRow:
                                case BasicCellType.ExtendRowByCondtion:
                                    SetBackColor(_customCellInfos[0].ExtendRows, commonNodes.Count, Color.White, false);
                                    SetBackColor(number, dataFieldShows.Count, Color.LightSteelBlue, true);
                                    break;

                                case BasicCellType.ExtendCol:
                                case BasicCellType.ExtendColByCondtion:
                                    SetBackColor(commonNodes.Count, _customCellInfos[0].ExtendCols, Color.White, false);
                                    SetBackColor(dataFieldShows.Count, number, Color.LightSteelBlue, true);
                                    break;
                            }
                        }
                    }
                }
                foreach (var customCellInfo in _customCellInfos)
                {
                    customCellContract.SaveDataFieldCondition(customCellInfo.CellId, tableId, meCondition.Text.Trim(), meTemplate.Text.Trim(),
                        (byte)cellType, number, dataFieldConditions, dataFieldShows);
                }
                btxtDataTable.Enabled = false;
                icmbCellConditon.Enabled = false;
                btnItmSave.Enabled = false;
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
        /// 清除所有条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("确认要清除所有的条件。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (_customCellInfos.Count == 1 && (ReportType == QueryReportType.Basic) && SetBackColor != null)
                {
                    BasicCellType cellType = (BasicCellType)_customCellInfos[0].CellType;
                    IList<CommonNode> commonNodes = customCellContract.GetCommonNodesByCellId(_customCellInfos[0].CellId, CellCondition.Show);
                    switch (cellType)
                    {
                        case BasicCellType.ExtendRow:
                        case BasicCellType.ExtendRowByCondtion:
                            SetBackColor(_customCellInfos[0].ExtendRows, commonNodes.Count, Color.White, true);
                            break;

                        case BasicCellType.ExtendCol:
                        case BasicCellType.ExtendColByCondtion:
                            SetBackColor(commonNodes.Count, _customCellInfos[0].ExtendCols, Color.White, true);
                            break;
                    }
                }
                foreach (var customCellInfo in _customCellInfos)
                {
                    customCellContract.ClearDataFieldCondition(customCellInfo.CellId);
                    customCellInfo.TableId = decimal.MinValue;
                }
                btxtDataTable.Text = string.Empty;
                btxtDataTable.Tag = null;
                btxtDataTable.Enabled = true;
                icmbCellConditon.Enabled = true;
                etxtRowOrCol.ReadOnly = false;
                ClearDataOnControls();
            }
        }

        /// <summary>
        /// 校验单元格条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string condition = meCondition.Text.Trim();
                if (icmbCellConditon.SelectedItem != null)
                {
                    byte cellType = Convert.ToByte(icmbCellConditon.EditValue);
                    if(!DataFieldHelper.IsDataCell(ReportType, cellType) && string.IsNullOrWhiteSpace(condition))
                    {
                        MessageBox.Show("请设置条件。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                /* 检查表达式中的参数个数与选择字段的对应关系以及个数是否匹配 */
                string tablePhysicalName = customTableContract.GetTablePhysicalName(GetTableId());
                string expression = GetExpression(tablePhysicalName);
                int start = 0;
                int end = 0;
                start = expression.IndexOf('{', 0);
                if (start >= 0)
                {
                    end = expression.IndexOf('}', start + 1);
                    if (end > (start + 1))
                    {

                        string digtal = expression.Substring(start + 1, end - start - 1);
                        if (Regex.IsMatch(digtal, @"^[0-9]*$"))
                        {
                            int data = DataConvertionHelper.GetConvertedInt(digtal);
                            meTemplate.Focus();
                            MessageBox.Show(string.Format("{0}中的{{{1}}}字段并不存在！", grpCellTemplate.Text, data), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                Cursor = Cursors.WaitCursor;
                bool success = ValidateCellCondition(tablePhysicalName, expression, condition);
                Cursor = Cursors.Default;
                if (success)
                {
                    MessageBox.Show("条件验证成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("条件验证失败!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 更换表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataTableDropdownList_AfterTreeNodeSelect(object sender, TreeViewEventArgs e)
        {
            if (previousSelectedTreeNode == null)
            {
                previousSelectedTreeNode = e.Node;
            }
            else
            {
                if (previousSelectedTreeNode != e.Node)
                {
                    if (lstDataFieldCondition.Items.Count > 0 || lstDataFieldShow.Items.Count > 0)
                    {
                        if (MessageBox.Show("确认要更改所选择的数据表?该操作会清除所有的条件。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            previousSelectedTreeNode = e.Node;
                            ClearDataOnControls();
                        }
                    }
                }
            }
            btnItmSave.Enabled = true;
        }

        /// <summary>
        /// 增加字段条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnAddCondition_Click(object sender, EventArgs e)
        {
            ShowSelectedDataFiled(lstDataFieldCondition);
        }

        private void sbtnRemoveCondition_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认移除该字段？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (lstDataFieldCondition.SelectedItem != null)
                {
                    lstDataFieldCondition.Items.Remove(lstDataFieldCondition.SelectedItem);
                    btnItmSave.Enabled = true;
                }
                if (lstDataFieldCondition.Items.Count == 0)
                {
                    meCondition.Text = string.Empty;
                }
                else
                {
                    meCondition.Focus();
                    MessageBox.Show("请在条件中手工删除被移除字段对应的条件！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 设置条件字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnSetting_Click(object sender, EventArgs e)
        {
            if (lstDataFieldCondition.SelectedItem != null)
            {
                decimal tableId = GetTableId();
                CommonNode commonNode = lstDataFieldCondition.SelectedItem as CommonNode;
                CustomDataFieldInfo customDataFieldInfo = null;
                string physicalName = customTableContract.GetTablePhysicalName(tableId);
                if ((DataFieldProperty)commonNode.NodeType == DataFieldProperty.SystemPhysicalDataField)
                {                    
                    customDataFieldInfo = CommonBussinessHelper.GetSystemDataFieldInfo(tableId, physicalName, (SystemDataField)commonNode.NodeId);
                }
                else
                {
                    customDataFieldInfo = customDataFieldContract.GetModelInfo(commonNode.NodeId);
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                    if (dataFieldProperty == DataFieldProperty.LogicalDataField)
                    {
                        customDataFieldInfo.PhysicalName = customDataFieldContract.GetDataFieldLogicalExpression(commonNode.NodeId);
                    }
                }
                QueryConditionForm frmQueryCondition = new QueryConditionForm();
                frmQueryCondition.IsValidate = false;
                frmQueryCondition.TableName = physicalName;
                frmQueryCondition.CustomDataFieldInfo = customDataFieldInfo;
                frmQueryCondition.UpdateTextHandler = (where) =>
                {
                    meCondition.Text = string.Format("{0} {1}", meCondition.Text, where);
                    btnItmSave.Enabled = true;
                };
                frmQueryCondition.ShowDialog();
            }
        }

        /// <summary>
        /// 显示字段设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnShowDataFied_Click(object sender, EventArgs e)
        {
            ShowSelectedDataFiled(lstDataFieldShow);
        }

        /// <summary>
        /// 移除显示字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnRemoveDataFied_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认移除该字段？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (lstDataFieldShow.SelectedItem != null)
                {
                    lstDataFieldShow.Items.Remove(lstDataFieldShow.SelectedItem);
                    btnItmSave.Enabled = true;
                }
                int index = 0;
                foreach (object obj in lstDataFieldShow.Items)
                {
                    CommonNode node = obj as CommonNode;
                    int pos = node.NodeName.LastIndexOf('}');
                    node.NodeName = string.Format("{{{0}}}{1}", index++, node.NodeName.Substring(pos + 1));
                }
                lstDataFieldShow.Refresh();
            }
        }

        /// <summary>
        /// 选择数据表
        /// </summary>
        /// <param name="listBoxControl"></param>
        private void ShowSelectedDataFiled(DevExpress.XtraEditors.ListBoxControl listBoxControl)
        {
            if (btxtDataTable.Tag == null)
            {
                MessageBox.Show("请先选择数据表!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal tableId = decimal.MinValue;
            if (DataConvertionHelper.IsNullValue(_customCellInfos[0].TableId))
            {
                tableId = GetTableId();
            }
            else
            {
                tableId = _customCellInfos[0].TableId;
            }
            IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodes(tableId, DataFieldFilter.All);
            CheckedSelectedItemsForm frmCheckedSelectedItems = new CheckedSelectedItemsForm();
            frmCheckedSelectedItems.MultiNodeSelected = delegate (IList<CommonNode> selectedNodes)
            {
                foreach (CommonNode commonNode in selectedNodes)
                {
                    bool exist = false;
                    foreach (object obj in listBoxControl.Items)
                    {
                        CommonNode node = obj as CommonNode;
                        if (node.NodeId == commonNode.NodeId && node.NodeType == commonNode.NodeType)
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (!exist)
                    {
                        commonNode.NodeName = string.Format("{{{0}}}{1}", listBoxControl.Items.Count, commonNode.NodeName);
                        listBoxControl.Items.Add(commonNode);
                    }
                }
                btnItmSave.Enabled = true;
            };
            frmCheckedSelectedItems.LoadAndSetCommonNodes(commonNodes);
            frmCheckedSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 选择表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtDataTable_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
            frmDataTableItems.TableFilter = TableFilter.All;
            frmDataTableItems.DataWarehouseId = customCellContract.GetDataWarehouseId(_customCellInfos[0].CellId); ;
            frmDataTableItems.Text = "数据表选择";
            frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
            frmDataTableItems.NodeSelected = delegate (CommonNode commonNode) {
                if (commonNode != null)
                {
                    btxtDataTable.Text = customTableContract.GetFullName(commonNode.NodeId);
                    btxtDataTable.Tag = commonNode;
                }
                else
                {
                    btxtDataTable.Text = string.Empty;
                    btxtDataTable.Tag = null;
                }
            };
            frmDataTableItems.ShowDialog();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 校验显示字段和输入字段条件
        /// </summary>
        /// <param name="tablePhysicalName"></param>
        /// <param name="expression"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private bool ValidateCellCondition(string tablePhysicalName, string expression, string condition)
        {
            Dictionary<string, TableLink> systemTableLinks = new Dictionary<string, TableLink>();

            Dictionary<string, TableLink> showedTableLinks = GetSystemTableLinks(tablePhysicalName, lstDataFieldShow);
            foreach (var keyValue in showedTableLinks)
            {
                if (!systemTableLinks.ContainsKey(keyValue.Key))
                {
                    systemTableLinks.Add(keyValue.Key, keyValue.Value);
                }
            }
            Dictionary<string, TableLink> conditionalTableLinks = GetSystemTableLinks(tablePhysicalName, lstDataFieldCondition);
            foreach (var keyValue in showedTableLinks)
            {
                if (!systemTableLinks.ContainsKey(keyValue.Key))
                {
                    systemTableLinks.Add(keyValue.Key, keyValue.Value);
                }
            }

            return customCellContract.VaildateCellCondition(GetTableId(), expression, condition, systemTableLinks.Values.ToList<TableLink>());
        }

        /// <summary>
        /// 获得系统链接
        /// </summary>
        /// <param name="tablePhysicalName"></param>
        /// <param name="listBoxControl"></param>
        /// <returns></returns>
        private Dictionary<string, TableLink> GetSystemTableLinks(string tablePhysicalName, DevExpress.XtraEditors.ListBoxControl listBoxControl)
        {
            Dictionary<string, TableLink> systemTableLinks = new Dictionary<string, TableLink>();

            foreach (object item in lstDataFieldCondition.Items)
            {
                CommonNode commonNode = item as CommonNode;
                if ((DataFieldProperty)commonNode.NodeType == DataFieldProperty.SystemPhysicalDataField)
                {
                    SystemDataField systemLogicalDataField = (SystemDataField)commonNode.NodeId;
                    string systemTablePhysicalName = DataFieldHelper.GetSystemTablePhysicalName(systemLogicalDataField);
                    if (!string.IsNullOrWhiteSpace(systemTablePhysicalName) && !systemTableLinks.ContainsKey(systemTablePhysicalName))
                    {
                        TableLink tableLink = DataFieldHelper.GetTableLink(tablePhysicalName, systemLogicalDataField);
                        if (tableLink != null)
                        {
                            systemTableLinks.Add(systemTablePhysicalName, tableLink);
                        }
                    }
                }
            }

            return systemTableLinks;
        }

        /// <summary>
        /// 获得表达式
        /// </summary>
        /// <returns></returns>
        private string GetExpression(string tablePhysicalName)
        {
            string template = meTemplate.Text.Trim();
            StringBuilder sbExpression = new StringBuilder(template);
            StringBuilder sbReplace = new StringBuilder();
            int index = 0;
            foreach (object item in lstDataFieldShow.Items)
            {
                CommonNode commonNode = item as CommonNode;
                if (commonNode != null)
                {
                    string dataFieldPhysicalName = string.Empty;
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)commonNode.NodeType;
                    switch (dataFieldProperty)
                    {
                        case DataFieldProperty.SystemPhysicalDataField:
                            dataFieldPhysicalName = DataFieldHelper.GetSystemLogicalDataFieldName(tablePhysicalName, (SystemDataField)commonNode.NodeId);
                            break;

                        case DataFieldProperty.PhysicalDataField:
                            dataFieldPhysicalName = customDataFieldContract.GetPhysicalName(commonNode.NodeId);
                            break;

                        case DataFieldProperty.LogicalDataField:
                            dataFieldPhysicalName = customDataFieldContract.GetDataFieldLogicalExpression(commonNode.NodeId);
                            break;
                    }
                    sbReplace.Append("{");
                    sbReplace.Append(index);
                    sbReplace.Append("}");
                    sbExpression.Replace(sbReplace.ToString(), dataFieldPhysicalName);
                    sbReplace.Clear();
                    index++;
                }
            }

            return sbExpression.ToString();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            if (_customCellInfos.Count == 1)
            {
                if (!DataConvertionHelper.IsNullValue(_customCellInfos[0].TableId))
                {
                    btxtDataTable.Text = customTableContract.GetFullName(_customCellInfos[0].TableId);
                    btxtDataTable.Tag = customTableContract.GetCommonNode(_customCellInfos[0].TableId);
                    btxtDataTable.Enabled = false;
                    icmbCellConditon.Enabled = false;
                    etxtRowOrCol.ReadOnly = true;
                }
                else
                {
                    btxtDataTable.Text = string.Empty;
                    btxtDataTable.Tag = null;
                    btxtDataTable.Enabled = true;
                    icmbCellConditon.Enabled = true;
                    etxtRowOrCol.ReadOnly = false;
                }
                IList<CommonNode> commonNodes = customCellContract.GetCommonNodesByCellId(_customCellInfos[0].CellId, CellCondition.Show);
                for (int i = 0; i < commonNodes.Count; i++)
                {
                    commonNodes[i].NodeName = string.Format("{{{0}}}{1}", i, commonNodes[i].NodeName);
                }
                lstDataFieldShow.Items.AddRange(commonNodes.ToArray());
                foreach (CommonNode commonNode in commonNodes)
                {
                    if ((DataFieldProperty)commonNode.NodeType != DataFieldProperty.SystemPhysicalDataField)
                    {
                        if (_customCellInfos[0].TableId != commonNode.ParentNodeId)
                        {
                            MessageBox.Show("该单元格设置有误，请清除后重新设置！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    }
                }
                icmbCellConditon.EditValue = _customCellInfos[0].CellType;
                QueryReportType QueryReportType = (QueryReportType)ReportType;
                switch (ReportType)
                {
                    case QueryReportType.Basic:
                        BasicCellType cellType = (BasicCellType)_customCellInfos[0].CellType;
                        switch (cellType)
                        {
                            case BasicCellType.ExtendRow:
                            case BasicCellType.ExtendRowByCondtion:
                                etxtRowOrCol.Text = _customCellInfos[0].ExtendRows.ToString();
                                break;

                            case BasicCellType.ExtendCol:
                            case BasicCellType.ExtendColByCondtion:
                                etxtRowOrCol.Text = _customCellInfos[0].ExtendCols.ToString();
                                break;

                            default:
                                etxtRowOrCol.Text = string.Empty;
                                break;
                        }
                        break;
                }
                IList<CommonNode> nodes = customCellContract.GetCommonNodesByCellId(_customCellInfos[0].CellId, CellCondition.Condition);
                lstDataFieldCondition.Items.AddRange(nodes.ToArray());
                meCondition.Text = _customCellInfos[0].ConditionText;
                meTemplate.Text = _customCellInfos[0].TemplateText;
            }
        }
        
        /// <summary>
        /// 清除条件
        /// </summary>
        private void ClearDataOnControls()
        {
            lstDataFieldCondition.Items.Clear();
            lstDataFieldShow.Items.Clear();
            icmbCellConditon.SelectedIndex = 0;
            meCondition.Text = string.Empty;
            meTemplate.Text = string.Empty;
        }

        /// <summary>
        /// 获得选择表的编号
        /// </summary>
        /// <returns></returns>
        private decimal GetTableId()
        {
            decimal tableId = decimal.MinValue;

            CommonNode commonNode = btxtDataTable.Tag as CommonNode;
            if (commonNode != null)
            {
                tableId = commonNode.NodeId;
            }

            return tableId;
        }
        #endregion


    }
}
