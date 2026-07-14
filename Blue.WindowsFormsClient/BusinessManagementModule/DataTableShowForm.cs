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
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient
{
    public partial class DataTableShowForm : Form
    {
        #region 契约接口

        private readonly ICustomTableContract customTableContract;

        #endregion

        #region 属性

        /// <summary>
        /// 获得视图中的表的关系对象
        /// </summary>
        public GetCustomViewAndTableDataDelegate GetCustomViewAndTableData
        {
            set;
            get;
        }

        /// <summary>
        /// 数据仓库编号
        /// </summary>
        public byte DataWarehouseId
        {
            set
            {
                dataTableDropdownList.DataWarehouseId = value;
            }
            get
            {
                return dataTableDropdownList.DataWarehouseId;
            }
        }

        /// <summary>
        /// 主表编号
        /// </summary>
        public decimal MainTableId
        {
            get;
            set;
        }

        /// <summary>
        /// 上一个表编号
        /// </summary>
        public decimal PreviousTableId
        {
            get;
            set;
        }

        /// <summary>
        /// 表类型过滤
        /// </summary>
        public TableFilter TableFilter
        {
            set
            {
                dataTableDropdownList.TableFilter = value;
            }
            get
            {
                return dataTableDropdownList.TableFilter;
            }
        }

        /// <summary>
        /// 提示
        /// </summary>
        public string ToolTip
        {
            get
            {
                return gcMain.Text;
            }
            set
            {
                gcMain.Text = value;
            }
        }

        /// <summary>
        /// 视图属性
        /// </summary>
        public ViewProperty ViewProperty
        {
            get;
            set;
        }

        #endregion

        #region 控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataTableShowForm()
        {
            InitializeComponent();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            dataTableDropdownList.CustomDatabaseContract = BusinessChannelFactory.CreateCustomDatabaseContract();
            dataTableDropdownList.CustomCategoryContract = BusinessChannelFactory.CreateCustomCategoryContract();
            dataTableDropdownList.CustomTableContract = BusinessChannelFactory.CreateCustomTableContract();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTableSelectedItemsForm_Load(object sender, EventArgs e)
        {
            UserControlHelper.InitImageComboBoxEdit(icmbTableRelation, typeof(TableRelation));
            UserControlHelper.InitImageComboBoxEdit(icmbTableJoin, typeof(TableJoin));
            UserControlHelper.InitImageComboBoxEdit(icmbPrimaryDataField, typeof(DataFieldRelation));
            icmbTableJoin.EditValue = (byte)TableJoin.InnerJoin;
            dataTableDropdownList.LoadData();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (dataTableDropdownList.SelectedNode == null)
            {
                dataTableDropdownList.Focus();
                MessageBox.Show("请选择从表名称。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (GetCustomViewAndTableData != null)
            {
                byte tableRelation = Convert.ToByte(icmbTableRelation.EditValue);
                byte tableJoin = Convert.ToByte(icmbTableJoin.EditValue);
                byte primaryDataField = Convert.ToByte(icmbPrimaryDataField.EditValue);
                CustomViewAndTableInfo customViewAndTableInfo = new CustomViewAndTableInfo(0, dataTableDropdownList.SelectedNode.NodeId, tableRelation, tableJoin,
                    primaryDataField, 0);
                GetCustomViewAndTableData(customViewAndTableInfo);
            }
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
