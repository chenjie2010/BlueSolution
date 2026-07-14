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
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class WorkflowMapForm : Form
    {
        #region 私有变量
        #endregion

        #region 契约接口

        private readonly ICustomWorkflowProcessContract customWorkflowProcessContract;

        #endregion

        #region 属性

        /// <summary>
        /// 工作流编号
        /// </summary>
        public decimal WorkflowId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowMapForm()
        {
            InitializeComponent();
            customWorkflowProcessContract = BusinessChannelFactory.CreateCustomWorkflowProcessContract();
        }

        #endregion

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowMapForm_Load(object sender, EventArgs e)
        {
            IList<CommonNode> commonNodes = customWorkflowProcessContract.GetCommonNodes(WorkflowId, (byte)WorkflowProcessCategory.Begin);
            cmbWorkflowNodes.Properties.Items.AddRange(commonNodes.ToArray());
            cmbWorkflowNodes.EditValue = customWorkflowProcessContract.GetWorkflowRootNode(WorkflowId);
            LoadData();
            btnConfirm.Enabled = false;
        }

        /// <summary>
        /// 添加单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                WorkflowEdgeForm frmWorkflowEdge = new WorkflowEdgeForm();
                frmWorkflowEdge.WorkflowId = WorkflowId;
                frmWorkflowEdge.SetWorkflowProcess = (processRelationship) =>
                {
                    Cursor = Cursors.WaitCursor;
                    IList<CustomWorkflowMapInfo> customWorkflowMapInfos = new List<CustomWorkflowMapInfo>();
                    int sorting = 0;
                    foreach (KeyValueItem keyValueItem in processRelationship)
                    {
                        customWorkflowMapInfos.Add(new CustomWorkflowMapInfo(keyValueItem.Key, keyValueItem.Value, sorting++));
                    }
                    customWorkflowProcessContract.InsertCustomWorkflowMapInfos(customWorkflowMapInfos);
                    LoadData();
                    Cursor = Cursors.Default;
                };
                frmWorkflowEdge.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 删除单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认删除该节点关系吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    if (gvProcess.FocusedRowHandle >= 0)
                    {
                        decimal parentProcessId = Convert.ToDecimal(gvProcess.GetFocusedDataRow()["ParentProcessId"]);
                        decimal processId = Convert.ToDecimal(gvProcess.GetFocusedDataRow()["ProcessId"]);
                        customWorkflowProcessContract.DeleteCustomWorkflowMapInfo(parentProcessId, processId);
                        LoadData();
                    }
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvProcess_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="movedDriection"></param>
        private void UpdateNodeSorting(MovedDriection movedDriection)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (gvProcess.FocusedRowHandle >= 0)
                {
                    decimal parentProcessId = Convert.ToDecimal(gvProcess.GetFocusedDataRow()["ParentProcessId"]);
                    decimal processId = Convert.ToDecimal(gvProcess.GetFocusedDataRow()["ProcessId"]);
                    customWorkflowProcessContract.UpdateMapSorting(parentProcessId, processId, movedDriection);
                    LoadData();
                }
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
        /// 置顶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmTop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Top);
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmPrevious_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Previous);
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Next);
        }

        /// <summary>
        /// 置底
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmBottom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Bottom);
        }

        /// <summary>
        /// 根节点发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWorkflowNodes_SelectedIndexChanged(object sender, EventArgs e)
        {            
            btnConfirm.Enabled = true;
        }

        /// <summary>
        /// 更新根节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbWorkflowNodes.SelectedIndex >= 0)
            {
                CommonNode commonNode = cmbWorkflowNodes.SelectedItem as CommonNode;
                customWorkflowProcessContract.UpdateWorkflowRootNode(commonNode.NodeId);
                btnConfirm.Enabled = false;
            }
        }

        #endregion

        #region 公有函数
        #endregion

        #region 私有函数

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            gcProcess.DataSource = customWorkflowProcessContract.GetProcessMap(WorkflowId).Tables[0];
        }


        #endregion
        
    }
}
