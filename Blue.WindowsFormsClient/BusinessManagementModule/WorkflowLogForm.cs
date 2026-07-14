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
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class WorkflowLogForm : Form
    {
        #region 契约接口

        private ICustomWorkflowInstanceContract customWorkflowInstanceContract;
        private IWorkflowInstanceStepContract workflowInstanceStepContract;

        #endregion

        #region 属性

        /// <summary>
        /// 工作流实例编号
        /// </summary>
        public decimal InstanceId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowLogForm()
        {
            InitializeComponent();
            customWorkflowInstanceContract = BusinessChannelFactory.CreateCustomWorkflowInstanceContract();
            workflowInstanceStepContract = BusinessChannelFactory.CreateWorkflowInstanceStepContract();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowLogForm_Load(object sender, EventArgs e)
        {
            DataSet ds = customWorkflowInstanceContract.GetPageRecord(InstanceId);
            gcWorkflowLog.DataSource = ds.Tables[0];
            gvWorkflowLog.Columns["LogId"].Visible = false;
            gvWorkflowLog.Columns["TimeReviewed"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gvWorkflowLog.Columns["TimeReviewed"].DisplayFormat.FormatString = "f";
            gvWorkflowLog.BestFitColumns();
            gvWorkflowLog.Columns["Comment"].MinWidth = 150;
            CustomWorkflowInstanceInfo customWorkflowInstanceInfo = customWorkflowInstanceContract.GetModelInfo(InstanceId);
            InstanceStatus instanceStatus = (InstanceStatus)customWorkflowInstanceInfo.InstanceStatus;
            switch (instanceStatus)
            {
                case InstanceStatus.None:
                    lblName.Text = "草稿状态待提交。";
                    lblReviewer.Visible = false;
                    break;

                case InstanceStatus.Review:
                    Dictionary<decimal, string> lastestReviewers = customWorkflowInstanceContract.GetLastestReviewers(customWorkflowInstanceInfo.InstanceId);
                    StringBuilder sb = new StringBuilder();
                    foreach (var lastestReviewer in lastestReviewers)
                    {
                        sb.AppendFormat("{0}, ", lastestReviewer.Value);
                    }
                    if (sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 2, 2);
                        sb.AppendFormat("。");
                    }
                    lblReviewer.Text = sb.ToString();
                    lblName.Text = "下一步审核人：";
                    lblReviewer.Visible = true;
                    break;

                case InstanceStatus.Completed:
                    lblName.Text = "审核已完成。";
                    lblReviewer.Visible = false;
                    break;

                case InstanceStatus.ReviewAborted:
                    lblName.Text = "审核时终止。";
                    lblReviewer.Visible = false;
                    break;
            }
            DataSet dsStep = workflowInstanceStepContract.GetPageRecord(InstanceId);
            gcSteps.DataSource = dsStep.Tables[0];
            gvSteps.Columns["TimeReviewed"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gvSteps.Columns["TimeReviewed"].DisplayFormat.FormatString = "f";
        }

        /// <summary>
        /// 自定义枚举显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvWorkflowLog_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ReviewedAction")
            {
                ReviewedAction reviewedAction = (ReviewedAction)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(reviewedAction);
            }
        }

        /// <summary>
        /// 自定义枚举显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSteps_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ReviewedStatus")
            {
                ReviewedStatus reviewedStatus = (ReviewedStatus)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(reviewedStatus);
            }
        }

        #endregion

    }
}
