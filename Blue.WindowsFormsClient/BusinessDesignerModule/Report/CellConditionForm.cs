using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.BusinessDesignerModule;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class CellConditionForm : Form
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

        private CustomCellInfo _customCellInfo;
        private InputReportType _reportType;

        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public CustomCellInfo CustomCellInfo
        {
            set
            {
                _customCellInfo = value;               
            }
            get
            {
                return _customCellInfo;
            }
        }

        /// <summary>
        /// 活跃的单元格
        /// </summary>
        public FarPoint.Win.Spread.SheetView ActiveSheetView
        {
            get;
            set;
        }

        /// <summary>
        /// 报表格式类型
        /// </summary>
        public InputReportType ReportType
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

        public CellConditionForm()
        {
            InitializeComponent();
            customDatabaseContract = BusinessChannelFactory.CreateCustomDatabaseContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customExpressionContract = BusinessChannelFactory.CreateCustomExpressionContract();
            customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();
            customSheetContract = BusinessDesignerChannelFactory.CreateCustomSheetContract();
            customCellContract = BusinessDesignerChannelFactory.CreateCustomCellContract();
            dataTableDropdownList.CustomDatabaseContract = customDatabaseContract;
            dataTableDropdownList.CustomCategoryContract = BusinessChannelFactory.CreateCustomCategoryContract();
            dataTableDropdownList.CustomTableContract = customTableContract;
            UserControlHelper.InitImageComboBoxEdit(icmbCellConditon, typeof(InputCellType));
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellConditionSettingForm_Load(object sender, EventArgs e)
        {
            dataTableDropdownList.DataWarehouseId = customCellContract.GetDataWarehouseId(_customCellInfo.CellId);
            dataTableDropdownList.LoadData();
            LoadData();
            btnItmSave.Enabled = false;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            if (!DataConvertionHelper.IsNullValue(_customCellInfo.TableId))
            {
                dataTableDropdownList.SelectedNodeId = _customCellInfo.TableId;
                dataTableDropdownList.ReadOnly = true;
            }
            else
            {
                dataTableDropdownList.SelectedNode = null;
                dataTableDropdownList.ReadOnly = false;
            }
            icmbCellConditon.EditValue = _customCellInfo.CellType;
            InputCellType inputCellType = (InputCellType)_customCellInfo.CellType;
            switch (inputCellType)
            {
                case InputCellType.ExtendRow:
                case InputCellType.ExtendRowByCondtion:
                    etxtRowOrCol.Text = _customCellInfo.ExtendRows.ToString();
                    break;

                case InputCellType.ExtendCol:
                case InputCellType.ExtendColByCondtion:
                    etxtRowOrCol.Text = _customCellInfo.ExtendCols.ToString();
                    break;

                default:
                    etxtRowOrCol.Text = string.Empty;
                    break;
            }
            IList<CommonNode> nodes = customCellContract.GetCommonNodesByCellId(_customCellInfo.CellId, CellCondition.Show);
            lstDataFieldCondition.Items.AddRange(nodes.ToArray());
            //if (customDataFieldAndCellStyleInfos[0].IsSystemSystemDataField)
            //{
            //    etxtCondition.Text = DataFieldHelper.GetSystemDataFieldCaption((SystemDataField)customDataFieldAndCellStyleInfos[0].SystemDataFieldId);
            //}
            //else
            //{
            //    etxtCondition.Text = customDataFieldContract.GetDataFieldLogicalName(customDataFieldAndCellStyleInfos[0].DataFieldId);
            //}       
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dataTableDropdownList.SelectedNodeId <= 0)
            {
                MessageBox.Show("请先选择数据表!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            InputCellType inputCellType = (InputCellType)Convert.ToByte(icmbCellConditon.EditValue);
            IList<CommonNode> dataFieldConditions = new List<CommonNode>();
            int number = Int32.MinValue;
            switch (inputCellType)
            {
                case InputCellType.Single:
                    if (etxtCondition.Tag == null)
                    {
                        btnItmSave.Enabled = false;
                        MessageBox.Show("单元格字段未发生改变，不用保存！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CommonNode node = etxtCondition.Tag as CommonNode;
                    /* 当前位置 */
                    node.ParentNodeId = 0;
                    dataFieldConditions.Add(node);                    
                    break;

                case InputCellType.ExtendRow:
                case InputCellType.ExtendCol:
                    if (lstDataFieldCondition.Items.Count == 0)
                    {
                        MessageBox.Show("字段列表不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string condition  = etxtRowOrCol.Text.Trim();
                    if(string.IsNullOrWhiteSpace(condition))
                    {
                        MessageBox.Show("可扩展行数或是列数不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Regex r = new Regex(@"^\d+$");
                    if (r.IsMatch(condition))
                    {
                        number = Int32.Parse(condition);
                        if (number <= 0 || number > 2000)
                        {
                            MessageBox.Show("可扩展行数或是列数必须为正整数，且范围在(0~2000)!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("可扩展行数或是列数必须为正整数，且范围在(0~2000)!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (inputCellType == InputCellType.ExtendRow)
                    {
                        if (ActiveSheetView.ActiveCell.Column.Index + lstDataFieldCondition.Items.Count > ActiveSheetView.ColumnCount)
                        {
                            MessageBox.Show("显示的字段过多，已超过了表格的列允许的范围！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        for (int rowIndex = 0; rowIndex < number; rowIndex++)
                        {
                            for (int colIndex = 0; colIndex < lstDataFieldCondition.Items.Count; colIndex++)
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
                        if (ActiveSheetView.ActiveCell.Row.Index + lstDataFieldCondition.Items.Count > ActiveSheetView.RowCount)
                        {
                            MessageBox.Show("显示的字段过多，已超过了表格的行允许的范围！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        for (int rowIndex = 0; rowIndex < lstDataFieldCondition.Items.Count; rowIndex++)
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
                    int postion = 0;
                    FarPoint.Win.Spread.Cell currentCell = ActiveSheetView.ActiveCell;
                    for (int index = 0; index < lstDataFieldCondition.Items.Count; index++)
                    {
                        CommonNode dataFieldCommonNode = lstDataFieldCondition.Items[index] as CommonNode;
                        dataFieldCommonNode.ParentNodeId = postion;
                        dataFieldConditions.Add(dataFieldCommonNode); 
                        if (inputCellType == InputCellType.ExtendRow)
                        {
                            postion += currentCell.ColumnSpan;
                            if (currentCell.Column.Index + currentCell.ColumnSpan < ActiveSheetView.ColumnCount)
                            {
                                currentCell = ActiveSheetView.Cells[currentCell.Row.Index, currentCell.Column.Index + currentCell.ColumnSpan];
                            }
                        }
                        else
                        {
                            postion += currentCell.RowSpan;
                            if (currentCell.Row.Index + currentCell.RowSpan < ActiveSheetView.RowCount)
                            {
                                currentCell = ActiveSheetView.Cells[currentCell.Row.Index + currentCell.RowSpan, currentCell.Column.Index];
                            }
                        }                        
                    }
                    IList<CommonNode> commonNodes = customCellContract.GetCommonNodesByCellId(_customCellInfo.CellId, CellCondition.Show);
                    if (SetBackColor != null)
                    {
                        switch (inputCellType)
                        {
                            case InputCellType.ExtendRow:
                                SetBackColor(_customCellInfo.ExtendRows, commonNodes.Count, Color.White, false);
                                SetBackColor(number, dataFieldConditions.Count, Color.LightSteelBlue, true);
                                break;

                            case InputCellType.ExtendCol:
                                SetBackColor(commonNodes.Count, _customCellInfo.ExtendCols, Color.White, false);
                                SetBackColor(dataFieldConditions.Count, number, Color.LightSteelBlue, true);
                                break;
                        }
                    }
                    sbtnSetting.Enabled = false;
                    break;
            }
            customCellContract.SaveDataFieldCondition(_customCellInfo.CellId, dataTableDropdownList.SelectedNodeId, 
                (byte)inputCellType, number, dataFieldConditions);
            
            btnItmSave.Enabled = false;
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("确认要清除所有的条件。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                IList<CommonNode> commonNodes = customCellContract.GetCommonNodesByCellId(_customCellInfo.CellId, CellCondition.Show);
                customCellContract.ClearDataFieldCondition(_customCellInfo.CellId);
                etxtCondition.Text = string.Empty;
                sbtnSetting.Enabled = true;
                dataTableDropdownList.ReadOnly = false;
                icmbCellConditon.Properties.ReadOnly = false;                
                ClearDataOnControls();
                InputCellType inputCellType = (InputCellType)_customCellInfo.CellType;
                if (SetBackColor != null && commonNodes.Count > 0)
                {
                    switch (inputCellType)
                    {
                        case InputCellType.ExtendRow:
                            SetBackColor(_customCellInfo.ExtendRows, commonNodes.Count, Color.White, true);
                            break;

                        case InputCellType.ExtendCol:
                            SetBackColor(commonNodes.Count, _customCellInfo.ExtendCols, Color.White, true);
                            break;
                    }
                }
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
        /// 增加显示字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnAddCondition_Click(object sender, EventArgs e)
        {
            ShowSelectedDataFiled();
        }          

        /// <summary>
        /// 移除显示字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnRemoveCondition_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认移除该字段？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (lstDataFieldCondition.SelectedItem != null)
                {
                    lstDataFieldCondition.Items.Remove(lstDataFieldCondition.SelectedItem);
                    btnItmSave.Enabled = true;
                }
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listBoxControl"></param>
        private void ShowSelectedDataFiled()
        {
            if (dataTableDropdownList.SelectedNode == null || dataTableDropdownList.SelectedNodeId <= 0)
            {
                MessageBox.Show("请先选择数据表!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodes(dataTableDropdownList.SelectedNodeId, DataFieldFilter.All);
            CheckedSelectedItemsForm frmCheckedSelectedItems = new CheckedSelectedItemsForm();
            frmCheckedSelectedItems.MultiNodeSelected = delegate (IList<CommonNode> selectedNodes)
            {
                foreach (CommonNode commonNode in selectedNodes)
                {
                    bool exist = false;
                    foreach (object obj in lstDataFieldCondition.Items)
                    {
                        CommonNode node = obj as CommonNode;
                        if (node.NodeId == commonNode.NodeId && node.NodeType == commonNode.NodeType)
                        {
                            exist = true;
                            break; ;
                        }

                    }
                    if (!exist)
                    {
                        commonNode.NodeName = string.Format("{{{0}}}{1}", lstDataFieldCondition.Items.Count, commonNode.NodeName);
                        lstDataFieldCondition.Items.Add(commonNode);
                    }
                }
                btnItmSave.Enabled = true;
            };
            frmCheckedSelectedItems.LoadAndSetCommonNodes(commonNodes);
            frmCheckedSelectedItems.ShowDialog();
        }

        private void ClearDataOnControls()
        {
            etxtCondition.Text = string.Empty;
            lstDataFieldCondition.Items.Clear();
            etxtRowOrCol.Text = string.Empty;
        }

        private void etxtRowOrCol_TextChanged(object sender, EventArgs e)
        {
            btnItmSave.Enabled = true;
        }
        
        private void sbtnSetting_Click(object sender, EventArgs e)
        {
            ShowSelectedDataFiled();
        }

        private void ecmbTable_OnNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (previousSelectedTreeNode == null)
            {
                previousSelectedTreeNode = e.Node;
            }
            else
            {
                if (previousSelectedTreeNode != e.Node)
                {
                    if (lstDataFieldCondition.Items.Count > 0)
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
        /// 字段类型变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbCellConditon_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputCellType reportCellType = (InputCellType)Convert.ToByte(icmbCellConditon.EditValue);
            switch (reportCellType)
            {
                case InputCellType.Single:
                    pnlDataField.Visible = true;
                    pnlExtendDataField.Visible = false;
                    break;

                case InputCellType.ExtendRow:
                case InputCellType.ExtendCol:
                    pnlDataField.Visible = false;
                    pnlExtendDataField.Visible = true;
                    if (reportCellType == InputCellType.ExtendRow)
                    {
                        lblRowOrCol.Text = "可扩展行数：";
                    }
                    else
                    {
                        lblRowOrCol.Text = "可扩展列数：";
                    }
                    break;
            }
        }
    }
}
