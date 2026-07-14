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
    public partial class WorkflowInstanceListForm : Form
    {
        #region 契约接口

        private ICustomWorkflowInstanceContract customWorkflowInstanceContract;
        private ICustomWorkflowContract customWorkflowContract;
        private ICustomDataContract customDataContract;
        private IWorkflowInstanceStepContract workflowInstanceStepContract;

        #endregion

        #region  私有变量

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowInstanceListForm()
        {
            InitializeComponent();
            customWorkflowInstanceContract = BusinessChannelFactory.CreateCustomWorkflowInstanceContract();
            workflowInstanceStepContract = BusinessChannelFactory.CreateWorkflowInstanceStepContract();
            customWorkflowContract = BusinessChannelFactory.CreateCustomWorkflowContract();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbInstanceState, typeof(InstanceStatus));
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowInstanceListForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            LoadData();
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            devWorkflow.CurrentPageIndex = 0;
            LoadData();
        }

        /// <summary>
        /// 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkClear_Click(object sender, EventArgs e)
        {
            txtInstanceName.Text = string.Empty;
            dtStart.EditValue = null;
            dtEnd.EditValue = null;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbInstanceState);
            ceArchived.Checked = false;
            btxtWorkflow.Tag = null;
            devWorkflow.CurrentPageIndex = 0;
            LoadData();
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devWorkflow_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            devWorkflow.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInit_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbort_Click(object sender, EventArgs e)
        {
                      
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkView_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 自定义枚举字段显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devWorkflow_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "InstanceStatus")
            {
                InstanceStatus instanceState = (InstanceStatus)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(instanceState);
            }
        } 

        /// <summary>
        /// 查看工作流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[0]);
                if (instanceId > 0)
                {
                    Cursor = Cursors.WaitCursor;
                    CustomWorkflowInfo customWorkflowInfo = customWorkflowContract.GetCustomWorkflowInfo(instanceId);
                    CustomDataInfo customDataInfo = customDataContract.GetModelInfo(customWorkflowInfo.DataId);
                    CustomWorkflowInstanceInfo customWorkflowInstanceInfo = customWorkflowInstanceContract.GetModelInfo(instanceId);
                    WorkflowInstanceForm frmWorkflowInstance = new WorkflowInstanceForm()
                    {
                        ParentUserId = customWorkflowInstanceInfo.ParentUserId,
                        Text = customWorkflowInfo.WorkflowName,
                        CustomWorkflowInfo = customWorkflowInfo,
                        CustomDataInfo = customDataInfo,
                        FormReadOnly = true,
                        InstanceId = instanceId,
                        StepId = 0
                    };
                    Cursor = Cursors.Default;
                    frmWorkflowInstance.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 将工作流初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbInit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[0]);
                if (instanceId > 0)
                {
                    if (MessageBox.Show("确认终止该工作流实例？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        customWorkflowInstanceContract.InitWorkWorkflowInstance(instanceId);
                        LoadData();
                        MessageBox.Show("该工作流实例初始化成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }            
        }

        /// <summary>
        /// 终止工作流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAbort_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[0]);
                if (instanceId > 0)
                {
                    InstanceStatus instanceState = (InstanceStatus)customWorkflowInstanceContract.GetInstanceStatus(instanceId);
                    if (instanceState != InstanceStatus.Review)
                    {
                        MessageBox.Show(string.Format("该工作流实例处于{0}状态，不能终止。", UserEnumHelper.GetEnumText(instanceState)), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (MessageBox.Show("确认终止该工作流实例？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        customWorkflowInstanceContract.AbortWorkflowInstance(instanceId);
                        LoadData();
                        MessageBox.Show("该工作流实例终止成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiArchive_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[0]);
                if (instanceId > 0)
                {
                    InstanceStatus instanceState = (InstanceStatus)customWorkflowInstanceContract.GetInstanceStatus(instanceId);
                    if (instanceState != InstanceStatus.Completed)
                    {
                        MessageBox.Show(string.Format("该工作流实例处于{0}状态，不能归档。", UserEnumHelper.GetEnumText(instanceState)), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (MessageBox.Show("确认对该工作流实例进行归档？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        customWorkflowInstanceContract.ArchiveWorkflowInstance(instanceId, true, CurrentUser.Instance.UserName, CurrentUser.Instance.UserActualName);
                        LoadData();

                        MessageBox.Show("该工作流实例归档成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// 批量归档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiBatchArchive_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<decimal> instanceIds = new List<decimal>(devWorkflow.MultiSelectedValues.Count);
            foreach (RowEvent rowEvent in devWorkflow.MultiSelectedValues)
            {
                instanceIds.Add(DataConvertionHelper.GetDecimal(rowEvent.Value));
            }
            if (instanceIds.Count > 0)
            {
                if (MessageBox.Show("确认对工作流实例批量归档？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    customWorkflowInstanceContract.ArchiveWorkflowInstance(instanceIds, true, CurrentUser.Instance.UserName, CurrentUser.Instance.UserActualName);
                    LoadData();
                    MessageBox.Show("工作流实例批量归档成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("请先批量选择工作流实例。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 取消归档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCancleArchive_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[0]);
                if (instanceId > 0)
                {
                    if (!customWorkflowInstanceContract.GetInstanceArchivedStatus(instanceId))
                    {
                        MessageBox.Show("该工作流实例处于非归档状态，不能取消归档。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (MessageBox.Show("确认对该工作流实例取消归档？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        customWorkflowInstanceContract.ArchiveWorkflowInstance(instanceId, false, CurrentUser.Instance.UserName, CurrentUser.Instance.UserActualName);
                        LoadData();
                        MessageBox.Show("该工作流实例取消归档成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// 根据查询条件取消归档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAllArchives_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<WhereConditon> whereConditons = GetWhereConditons();
            if (MessageBox.Show("确认对按照条件对工作流实例归档？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                customWorkflowInstanceContract.ArchiveWorkflowInstance(whereConditons, true, CurrentUser.Instance.UserName, CurrentUser.Instance.UserActualName);
                LoadData();
                MessageBox.Show("按照条件对工作流实例归档成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 批量取消归档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiBatchCancelArchive_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<decimal> instanceIds = new List<decimal>(devWorkflow.MultiSelectedValues.Count);
            foreach (RowEvent rowEvent in devWorkflow.MultiSelectedValues)
            {
                instanceIds.Add(DataConvertionHelper.GetDecimal(rowEvent.Value));
            }
            if (instanceIds.Count > 0)
            {
                if (MessageBox.Show("确认对批量取消工作流实例归档？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    customWorkflowInstanceContract.ArchiveWorkflowInstance(instanceIds, true, CurrentUser.Instance.UserName, CurrentUser.Instance.UserActualName);
                    LoadData();
                    MessageBox.Show("工作流实例批量归档取消成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("请先批量选择工作流实例。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 根据查询条件取消归档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCancelAllArchives_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<WhereConditon> whereConditons = GetWhereConditons();
            if (MessageBox.Show("确认对按照条件对工作流实例取消归档？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                customWorkflowInstanceContract.ArchiveWorkflowInstance(whereConditons, true, CurrentUser.Instance.UserName, CurrentUser.Instance.UserActualName);
                LoadData();
                MessageBox.Show("按照条件对工作流实例取消归档成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 查看日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiViewLog_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[0]);
                WorkflowLogForm frmWorkflowLog = new WorkflowLogForm()
                {
                    InstanceId = instanceId,
                    Text = Convert.ToString(devWorkflow.GetRowCellValue(devWorkflow.FocusedRowHandle, "InstanceName"))
                };
                frmWorkflowLog.ShowDialog();
            }
        }
        
        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvWorkflowLog_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
        private void gvSteps_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 工作流选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtWorkflow_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            WorkflowItemsForm frmWorkflowItems = new WorkflowItemsForm();
            frmWorkflowItems.ToolTip = "提示：只能选择工作流节点。";
            frmWorkflowItems.NodeSelected = delegate (CommonNode node)
            {
                if (node != null)
                {
                    btxtWorkflow.Text = customWorkflowContract.GetFullName(node.NodeId);
                    btxtWorkflow.Tag = node;
                }
                else
                {
                    btxtWorkflow.Text = string.Empty;
                    btxtWorkflow.Tag = null;
                }
            };
            frmWorkflowItems.ShowDialog();
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
            devWorkflow.DataSource = customWorkflowInstanceContract.GetWorkflowInstances(devWorkflow.PageSize * devWorkflow.CurrentPageIndex,
                        devWorkflow.PageSize, whereConditons, ref totalCount).Tables[0];
            devWorkflow.RecordCount = totalCount;
            devWorkflow.DevExpressGridView.Columns["InstanceName"].MinWidth = 200;
            devWorkflow.Columns["TimeSumbitted"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            devWorkflow.Columns["TimeSumbitted"].DisplayFormat.FormatString = "f";
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
                whereConditons.Add(new WhereConditon("InstanceName", "InstanceName", DbType.String, string.Format("%{0}%", condition),
                   DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("UserName", "UserName", DbType.String, string.Format("%{0}%", condition),
                  DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("UserActualName", "UserActualName", DbType.String, string.Format("%{0}%", condition),
                  DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            DateTime start = dtStart.DateTime;
            DateTime end = dtEnd.DateTime;
            if (!DataConvertionHelper.IsNullValue(start))
            {
                whereConditons.Add(new WhereConditon("TimeSumbitted", "TimeSumbitted_0", DbType.DateTime, start,
                  DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(end))
            {
                whereConditons.Add(new WhereConditon("TimeSumbitted", "TimeSumbitted_1", DbType.DateTime, end,
                  DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (btxtWorkflow.Tag != null)
            {
                CommonNode node = btxtWorkflow.Tag as CommonNode;
                whereConditons.Add(new WhereConditon("WorkflowId", "WorkflowId", DbType.Decimal, node.NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            UserControlHelper.GetWhereConditons(ccmbInstanceState, whereConditons, "CustomWorkflowInstance", "InstanceStatus");
            whereConditons.Add(new WhereConditon("IsArchived", "IsArchived", DbType.Boolean, ceArchived.Checked,
                  DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

            return whereConditons;
        }

        #endregion
    }
}
