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
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.MyBusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class PrintingRecordListForm : Form
    {

        #region 契约接口

        private readonly ICustomPrintContract customPrintContract;

        #endregion

        #region 属性

        /// <summary>
        /// 打印编号
        /// </summary>
        public decimal PrintId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public PrintingRecordListForm()
        {
            InitializeComponent();
            customPrintContract = BusinessChannelFactory.CreateCustomPrintContract();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintingRecordListForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            if (PrintId > 0)
            {
                CommonNode commonNode = customPrintContract.GetCommonNode(PrintId);
                btxtRecordList.Tag = commonNode;
                btxtRecordList.Text = commonNode.NodeName;
            }
            LoadData();
        }

        /// <summary>
        /// 选择打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtRecordList_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            PrintItemsForm frmPrintItems = new PrintItemsForm();
            frmPrintItems.ToolTip = "提示：只能选择打印节点。";
            frmPrintItems.NodeSelected = delegate (CommonNode node)
            {
                if (node != null)
                {
                    CommonNode commonNode = customPrintContract.GetCommonNode(node.NodeId);
                    btxtRecordList.Text = commonNode.NodeName;
                    btxtRecordList.Tag = node;
                }
                else
                {
                    btxtRecordList.Text = string.Empty;
                    btxtRecordList.Tag = null;
                }
            };
            frmPrintItems.ShowDialog();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            devRecordList.CurrentPageIndex = 0;
            LoadData();            
        }

        private void hlnkClear_Click(object sender, EventArgs e)
        {
            txtInstanceName.Text = string.Empty;
            dtStart.EditValue = null;
            dtEnd.EditValue = null;
            btxtRecordList.Tag = null;
            devRecordList.CurrentPageIndex = 0;
            LoadData();
        }

        private void devWorkflow_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            devRecordList.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        private void devWorkflow_OnExportExcel(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载工作流实例数据
        /// </summary>
        private void LoadData()
        {
            int totalCount = 0;

            IList<WhereConditon> whereConditons = GetWhereConditons();
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("PrintTime", CustomSorting.Descending));
            devRecordList.DataSource = customPrintContract.GetPageRecordOfMultiTables(devRecordList.PageSize * devRecordList.CurrentPageIndex,
                        devRecordList.PageSize, whereConditons, sortingCondtions, ref totalCount).Tables[0];
            devRecordList.RecordCount = totalCount;
            devRecordList.DevExpressGridView.Columns["PrintName"].MinWidth = 200;
            devRecordList.Columns["PrintTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            devRecordList.Columns["PrintTime"].DisplayFormat.FormatString = "f";
        }

        /// <summary>
        /// 获得查询条件
        /// </summary>
        /// <returns></returns>
        private IList<WhereConditon> GetWhereConditons()
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

            string condition = txtInstanceName.Text.Trim();
            /* 实例名称查询 */
            if (!string.IsNullOrWhiteSpace(condition))
            {
                whereConditons.Add(new WhereConditon("UserName", "UserName", DbType.String, string.Format("%{0}%", condition),
                  DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.LeftBracket, 1));
                whereConditons.Add(new WhereConditon("UserActualName", "UserActualName", DbType.String, string.Format("%{0}%", condition),
                  DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
            }
            DateTime start = dtStart.DateTime;
            DateTime end = dtEnd.DateTime;
            if (!DataConvertionHelper.IsNullValue(start))
            {
                whereConditons.Add(new WhereConditon("PrintTime", "PrintTime_0", DbType.DateTime, start,
                  DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(end))
            {
                whereConditons.Add(new WhereConditon("PrintTime", "PrintTime_1", DbType.DateTime, end,
                  DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (btxtRecordList.Tag != null)
            {
                CommonNode node = btxtRecordList.Tag as CommonNode;
                whereConditons.Add(new WhereConditon("PrintRecord", "PrintId", "PrintId", DbType.Decimal, node.NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            return whereConditons;
        }


        #endregion

    }
}
