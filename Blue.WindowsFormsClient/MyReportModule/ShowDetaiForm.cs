using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.MyReportModule
{
    public partial class ShowDetaiForm : Form
    {
        #region 契约接口

        private readonly ICustomCellContract customCellContract;
        private readonly ICustomDataFieldContract customDataFieldContract;

        #endregion

    
        private IDictionary<decimal, string> dicCommonNodes = null;

        #region 属性

        public DataTable DataDetail
        {
            get;
            set;
        }

        public string DepartmentName
        {
            get;
            set;
        }

        /// <summary>
        /// 仓库编号
        /// </summary>
        public byte DataWarehouseId
        {
            get;
            set;
        }

        /// <summary>
        /// 用户权限
        /// </summary>
        public AuthorityCondition AuthorityCondition
        {
            get;
            set;
        }

        /// <summary>
        /// 单元格编号
        /// </summary>
        public decimal CellId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public ShowDetaiForm()
        {
            InitializeComponent();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customCellContract = BusinessDesignerChannelFactory.CreateCustomCellContract();
            dicCommonNodes = new Dictionary<decimal, string>();
            UserControlHelper.InitImageComboBoxEdit(icmbDataWarehouse, typeof(DataWarehouse));
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDetaiForm_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmCloseAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveFileDialog.FileName = string.Format("{0}_数据详情.xlsx", DepartmentName);
            saveFileDialog.Filter = "xls(*.xls,*.xlsx)|*.xls,*.xlsx";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            gcDetai.ExportToXlsx(saveFileDialog.FileName);
        }

        /// <summary>
        /// 行索引
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
        /// 添加项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataFieldItemsForm frmDataFieldItems = new DataFieldItemsForm();
            frmDataFieldItems.MultiNodeAllowed = true;
            frmDataFieldItems.DataFieldShowMode = DataFieldShowMode.DataWarehouse;
            frmDataFieldItems.DataWarehouseId = Convert.ToByte(icmbDataWarehouse.EditValue);
            frmDataFieldItems.DataFieldFilter = DataFieldFilter.PhysicalFieldAndKeyLogicalField;
            frmDataFieldItems.MultiNodeSelected = delegate (IList<CommonNode> commonNodes)
            {
                foreach (CommonNode commonNode in commonNodes)
                {
                    bool add = true;
                    foreach (object obj in lstDataFields.Items)
                    {
                        CommonNode node = obj as CommonNode;
                        if (node.NodeId == commonNode.NodeId && node.NodeType == commonNode.NodeType)
                        {
                            add = false;
                            break;
                        }
                    }
                    if (add)
                    {
                        commonNode.NodeName = customDataFieldContract.GetFullLogicalName(commonNode.NodeId);
                        lstDataFields.Items.Add(commonNode);
                    }
                }
            };
            frmDataFieldItems.ShowDialog();
        }

        /// <summary>
        /// 移除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认清除所选择的字段吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (lstDataFields.SelectedIndex >= 0)
                {
                    lstDataFields.Items.RemoveAt(lstDataFields.SelectedIndex);
                    if (lstDataFields.SelectedIndex > 0)
                    {
                        lstDataFields.SelectedIndex -= 1;
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择需要清除的字段。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 清除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认清除所有选择的字段吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                lstDataFields.Items.Clear();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstDataFields.Items.Count > 0)
            {
                if (DataDetail != null && DataDetail.Rows.Count > 0)
                {
                    IList<decimal> dataFieldIds = new List<decimal>();
                    foreach (object obj in lstDataFields.Items)
                    {
                        CommonNode node = obj as CommonNode;
                        dataFieldIds.Add(node.NodeId);
                    }
                    gridView.OptionsView.ColumnAutoWidth = false;
                    gcDetai.DataSource = customCellContract.GetCellDetail(CellId, DataWarehouseId, dataFieldIds, AuthorityCondition.RelatedUserTypeCommonNodes, AuthorityCondition.RelatedDepartmentCommonNodes);
                    gridView.OptionsView.ColumnAutoWidth = false; gridView.PopulateColumns();

                    //if (!gridView.OptionsView.ColumnAutoWidth)
                    //{
                    //    gridView.BestFitColumns();
                    //}
                }
            }
            else
            {
                LoadUserData();
            }
        }

        #endregion



        #region 私有方法

        /// <summary>
        /// 仅加载用户数据
        /// </summary>
        private void LoadUserData()
        {
            if (DataDetail != null)
            {
                if (!DataDetail.Columns.Contains("DepartmentName"))
                {
                    DataDetail.Columns.Add("DepartmentName", Type.GetType("System.String"));
                }
                foreach (DataRow dr in DataDetail.Rows)
                {
                    dr[DataDetail.Columns.Count - 1] = DepartmentName;
                }
                gcDetai.DataSource = DataDetail;
            }
        }

        #endregion

    }
}
