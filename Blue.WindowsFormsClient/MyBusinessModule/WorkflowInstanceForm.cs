using System;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraGrid.Columns;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.DataFilledModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.DataFilledModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.MyBusinessModule
{
    public partial class WorkflowInstanceForm : Form
    {
        #region 私有常量

        /// <summary>
        /// 标签字符的最大长度（三列）
        /// </summary>
        private const int COMMON_MAX_LEN_OF_LABEL_TEXT = 8;

        /// <summary>
        /// 标签字符的省略符号长度
        /// </summary>
        private const int COMMON_LEN_OF_OMIT_TEXT = 2;

        /// <summary>
        /// 标签的宽度（三列）
        /// </summary>
        private const int COMMON_WIDTH_OF_LABEL_TEXT = 120;

        /// <summary>
        /// 多行文本框的高度
        /// </summary>
        private const int COMMON_HEIGHT_OF_TEXT = 60;

        /// <summary>
        /// 标签字符的最大长度（双列）
        /// </summary>
        private const int COMMON_MAX_LEN_OF_LABEL_TEXT_MIDDLE = 12;

        /// <summary>
        /// 标签的宽度（双列）
        /// </summary>
        private const int COMMON_WIDTH_OF_LABEL_TEXT_MIDDLE = 180;

        /// <summary>
        /// 标签字符的最大长度（单列）
        /// </summary>
        private const int COMMON_MAX_LEN_OF_LABEL_TEXT_BIG = 24;

        /// <summary>
        /// 标签的宽度（单列）
        /// </summary>
        private const int COMMON_WIDTH_OF_LABEL_TEXT_BIG = 360;        

        /// <summary>
        /// 控件高度
        /// </summary>
        private const int COMMON_HEIGHT_OF_CONTROL = 20;

        /// <summary>
        /// 每行之间的间隙距离
        /// </summary>
        private const int COMMON_HEIGHT_OF_SPACE = 10;
        
        /// <summary>
        ///控件边沿的距离
        /// </summary>
        private const int COMMON_WIDTH_OF_MARGIN_SPACE = 3;

        /// <summary>
        /// 当前窗体边缘宽度
        /// </summary>
        private const int CURRENT_WIDTH_OF_MARGIN_SPACE = 42;

        /// <summary>
        /// 每行表格数量最大数
        /// </summary>
        private const int MAX_NUMBER_PER_LAYER = 2;

        /// <summary>
        /// Group 控件的标题行高度
        /// </summary>
        private const int HEIGHT_OF_GROUP_HEADER = 20;
                        
        /// <summary>
        /// 表格的高度
        /// </summary>
        private const int TBALE_HEIGHT = 300;

        /// <summary>
        /// 动态节点最大审核用户数量
        /// </summary>
        private const int MAX_USER_COUNT_ON_NODE = 5;

        #endregion

        #region 私有变量

        /// <summary>
        /// 步骤数
        /// </summary>
        private int numberOfSteps = 0;

        /// <summary>
        /// 当前步骤
        /// </summary>
        private int currentStep = 0;

        /// <summary>
        /// 工作流实例
        /// </summary>
        private CustomWorkflowInstanceInfo customWorkflowInstanceInfo = null;

        /// <summary>
        /// 视图与字段
        /// </summary>
        private Dictionary<decimal, IList<CommonNode>> combinedDataFields;        

        /// <summary>
        /// 表格
        /// </summary>
        private readonly Dictionary<int, IList<DataTableBusiness>> dicWorkflowTables;

        /// <summary>
        /// 步骤与表格
        /// </summary>
        private readonly Dictionary<int, IList<CustomFormInfo>> currentFormInfos;

        /// <summary>
        /// 当前工作流关联的节点
        /// </summary>
        private CustomWorkflowProcessInfo customWorkflowProcessInfo = null;

        /// <summary>
        /// 业务所属对象
        /// </summary>
        private CommonUserInfo commonUserInfo = null;

        #endregion

        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomSectionContract customSectionContract;
        private readonly ICustomFormContract customFormContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICombinedTableContract combinedTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly ICustomWorkflowInstanceContract customWorkflowInstanceContract;
        private readonly ICustomWorkflowProcessContract customWorkflowProcessContract;
        private readonly ICustomExpressionContract customExpressionContract;
        private readonly ICustomDataContract customDataContract;
        private readonly IBusinessInstanceContract businessInstanceContract;
        private readonly IWorkflowInstanceStepContract workflowInstanceStepContract;

        #endregion

        #region 属性    

        /// <summary>
        /// 数据填充对象
        /// </summary>
        public CustomDataInfo CustomDataInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 数据填充对象
        /// </summary>
        public CustomWorkflowInfo CustomWorkflowInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 实例编号
        /// </summary>
        public decimal InstanceId
        {
            get;
            set;
        }

        /// <summary>
        /// 步骤编号
        /// </summary>
        public decimal StepId
        {
            get;
            set;
        }

        /// <summary>
        /// 与填报的数据关联的用户编号
        /// </summary>
        public decimal ParentUserId
        {
            get;
            set;
        }

        /// <summary>
        /// 当前的编辑状态
        /// </summary>
        public bool CurrentEditedState
        {
            get;
            set;
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool FormReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        public CloseFormDelegate CloseForm
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowInstanceForm()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customSectionContract = BusinessChannelFactory.CreateCustomSectionContract();
            customFormContract = BusinessChannelFactory.CreateCustomFormContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            combinedTableContract = BusinessChannelFactory.CreateCombinedTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
            customWorkflowInstanceContract = BusinessChannelFactory.CreateCustomWorkflowInstanceContract();
            customWorkflowProcessContract = BusinessChannelFactory.CreateCustomWorkflowProcessContract();
            customExpressionContract = BusinessChannelFactory.CreateCustomExpressionContract();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();
            businessInstanceContract = DataFilledChannelFactory.CreateBusinessInstanceContract();
            workflowInstanceStepContract = BusinessChannelFactory.CreateWorkflowInstanceStepContract();
            combinedDataFields = new Dictionary<decimal, IList<CommonNode>>();            
            dicWorkflowTables = new Dictionary<int, IList<DataTableBusiness>>();
            currentFormInfos = new Dictionary<int, IList<CustomFormInfo>>();
            StepId = decimal.MinValue;
            InstanceId = decimal.MinValue;            
            meToolTip.Text = string.Empty;
        }

        #endregion

        #region 窗体加载方法
        
        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFilledInstanceForm_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                InstanceStatus currentInstanceStatus = InstanceStatus.None;
                if (InstanceId > 0)
                {
                    customWorkflowInstanceInfo = customWorkflowInstanceContract.GetModelInfo(InstanceId);
                    if (customWorkflowInstanceInfo != null)
                    {
                        commonUserInfo = userAccountContract.GetCommonUserInfo(customWorkflowInstanceInfo.UserId);
                        currentInstanceStatus = (InstanceStatus)customWorkflowInstanceInfo.InstanceStatus;
                        if (FormReadOnly || currentInstanceStatus == InstanceStatus.Completed || currentInstanceStatus == InstanceStatus.ReviewAborted)
                        {
                            pnlBottom.Visible = false;
                            CurrentEditedState = false;
                        }
                        else
                        {
                            CurrentEditedState = true;
                            if (currentInstanceStatus == InstanceStatus.None)
                            {
                                SetControlsVisible(false);
                            }
                            else
                            {
                                SetControlsVisible(true);                                
                            }
                        }
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("该工作流并不存在。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    CurrentEditedState = true;
                    SetControlsVisible(false);
                }
                /*获得与工作流相关的节点*/
                WorkflowInstanceLogInfo workflowInstanceLogInfo = null;
                if (StepId > 0)
                {
                    WorkflowInstanceStepInfo workflowInstanceStepInfo = customWorkflowInstanceContract.GetWorkflowInstanceStepInfo(StepId);
                    customWorkflowProcessInfo = customWorkflowProcessContract.GetModelInfo(workflowInstanceStepInfo.ProcessId);
                    workflowInstanceLogInfo = customWorkflowInstanceContract.GetDraftLog(customWorkflowProcessInfo.ProcessId, InstanceId, CurrentUser.Instance.UserId);
                    if (workflowInstanceLogInfo != null)
                    {
                        meComment.Text = workflowInstanceLogInfo.Comment;
                    }
                    Dictionary<string, string> comments = customWorkflowInstanceContract.GetComments(StepId);
                    StringBuilder sb = new StringBuilder();
                    foreach (var comment in comments)
                    {
                        if (!string.IsNullOrWhiteSpace(comment.Value))
                        {
                            sb.AppendFormat("{0}的审核意见：{1}；\r\n", comment.Key, comment.Value);
                        }
                    }
                    meLastestComment.Text = sb.ToString();
                }
                else
                {
                    CommonNode commonNode = customWorkflowProcessContract.GetWorkflowRootNode(CustomWorkflowInfo.WorkflowId);
                    if (commonNode != null)
                    {
                        customWorkflowProcessInfo = customWorkflowProcessContract.GetModelInfo(commonNode.NodeId);
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("该工作流未设置流程关系。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }                                
                hlnkReject.Visible = AuthorityHelper.CheckAuthority(customWorkflowProcessInfo.ProcessSetting, (byte)WorkflowProcessSetting.Reject);
                if ((workflowInstanceLogInfo == null) && 
                    AuthorityHelper.CheckAuthority(customWorkflowProcessInfo.ProcessSetting, (byte)WorkflowProcessSetting.Reject))
                {
                    meComment.Text = customWorkflowProcessInfo.ToolTip;
                }

                /* 初始化并加载第一步控件 */
                IList<CustomSectionInfo> customSectionInfos = customSectionContract.GetModelInfos(CustomDataInfo.DataId);
                xtcBussiness.Tag = 0;
                foreach (var customSectionInfo in customSectionInfos)
                {
                    IList<CustomFormInfo> customFormInfos = customFormContract.GetModelInfos(customSectionInfo.SectionId);
                    XtraTabPage tabPage = new XtraTabPage()
                    {
                        Text = customSectionInfo.SectionName,
                        AutoScroll = true
                    };
                    xtcBussiness.TabPages.Add(tabPage);
                    currentFormInfos.Add(numberOfSteps++, customFormInfos);                    
                }                
                xtcBussiness.Tag = 1;
                CreateControls(currentStep, xtcBussiness.TabPages[currentStep], currentFormInfos[currentStep]);
                
                /* 只读模式下以卡片方式显示 */
                DataShowMode dataShowMode = DataShowMode.Tab;
                if (!FormReadOnly && CurrentEditedState)
                {
                    dataShowMode = (DataShowMode)CustomDataInfo.DataShowMode;
                    btnPrevious.Enabled = false;
                    if (numberOfSteps == 1)
                    {
                        btnNext.Text = "提交(&O)";
                        btnNext.ImageIndex = 3;
                    }
                }
                else
                {
                    pnlBottom.Visible = false;
                }
                switch (dataShowMode)
                {
                    case DataShowMode.Step:
                        gcBusiness.ShowCaption = true;
                        gcBusiness.Text = customSectionInfos[0].SectionName;
                        gcBusiness.BorderStyle = BorderStyles.NoBorder;
                        xtcBussiness.ShowTabHeader = DefaultBoolean.False;
                        xtcBussiness.BorderStyle = BorderStyles.NoBorder;
                        break;

                    case DataShowMode.Tab:
                        gcBusiness.ShowCaption = false;
                        gcBusiness.BorderStyle = BorderStyles.Default;
                        xtcBussiness.ShowTabHeader = DefaultBoolean.True;
                        xtcBussiness.SelectedTabPageIndex = 0;
                        xtcBussiness.BorderStyle = BorderStyles.Default;
                        break;
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        ///<summary>
        ///审核意见控件的可见性
        ///</summary>
        private void SetControlsVisible(bool visible)
        {
            pnlReview.Visible = visible;            
            if (!visible)
            {
                pnlBottom.Height = 40;
            }
        }

        /// <summary>
        /// 下一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (currentStep >= 0 && currentStep < numberOfSteps && currentStep < xtcBussiness.TabPages.Count)
                {
                    bool submitted = currentStep == (numberOfSteps - 1);
                    bool result = SubmitBusiness(submitted);
                    if (result)
                    {
                        if (submitted)
                        {
                            CloseForm?.Invoke();
                            MessageBox.Show("提交成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            currentStep++;
                            CreateControls(currentStep, xtcBussiness.TabPages[currentStep], currentFormInfos[currentStep]);
                            xtcBussiness.SelectedTabPageIndex = currentStep;
                            if (currentStep == (numberOfSteps - 1))
                            {
                                btnNext.Text = "提交(&O)";
                                btnNext.ImageIndex = 3;
                            }
                            btnPrevious.Enabled = true;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("下一步系统出错。");
                }

            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkReject_Click(object sender, EventArgs e)
        {
            if (StepId <= 0)
            {
                MessageBox.Show("该工作流处理创建状态，无法驳回。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!AuthorityHelper.CheckAuthority(customWorkflowProcessInfo.ProcessSetting, (byte)WorkflowProcessSetting.Reject))
            {
                MessageBox.Show("该工作流被管理员设置为不可驳回。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (InstanceId > 0)
                {
                    WorkflowInstanceStepInfo workflowInstanceStepInfo = customWorkflowInstanceContract.GetWorkflowInstanceStepInfo(StepId);
                    if ((ReviewedStatus)workflowInstanceStepInfo.ReviewedStatus != ReviewedStatus.Reviewing)
                    {
                        MessageBox.Show("该工作流已经被撤回，无法驳回。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    IList<CommonNode> commonNodes = workflowInstanceStepContract.GetRejectTargets(StepId);
                    IList<decimal> logIds = new List<decimal>();
                    CheckedSelectedItemsForm frmCheckedSelectedItems = new CheckedSelectedItemsForm();
                    frmCheckedSelectedItems.MultiNodeSelected = delegate (IList<CommonNode> selectedNodes)
                    {
                        foreach (var selectedNode in selectedNodes)
                        {
                            logIds.Add(selectedNode.NodeId);
                        }
                    };
                    frmCheckedSelectedItems.LoadAndSetCommonNodes(commonNodes);
                    frmCheckedSelectedItems.ShowDialog();
                    if (logIds.Count == 0)
                    {
                        MessageBox.Show("请先选择驳回对象。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (MessageBox.Show("确认驳回该工作流？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        IList<WorkflowInstanceLogInfo> workflowInstanceLogInfos = new List<WorkflowInstanceLogInfo>(logIds.Count);
                        foreach (var logId in logIds)
                        {
                            WorkflowInstanceLogInfo workflowInstanceLogInfo = new WorkflowInstanceLogInfo()
                            {
                                ProcessId = workflowInstanceStepInfo.ProcessId,
                                InstanceId = InstanceId,
                                UserId = customWorkflowInstanceContract.GetParentUserId(logId),
                                ParentUserId = CurrentUser.Instance.UserId,
                                ReviewedAction = (byte)ReviewedAction.Reject,
                                Comment = meComment.Text.Trim(),
                                TimeReviewed = DateTime.Now
                            };
                        }
                        workflowInstanceStepContract.RejectWorkflowInstance(StepId, logIds, workflowInstanceStepInfo.ProcessId,
                            workflowInstanceStepInfo.InstanceId, workflowInstanceLogInfos);
                        MessageBox.Show("驳回成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="submitted"></param>
        /// <returns></returns>
        private bool SubmitBusiness(bool submitted)
        {
            if (InstanceId > 0)
            {
                InstanceStatus currentInstanceStatus = (InstanceStatus)customWorkflowInstanceInfo.InstanceStatus;
                if (currentInstanceStatus != InstanceStatus.None)
                {
                    if (StepId > 0)
                    {
                        WorkflowInstanceStepInfo workflowInstanceStepInfo = workflowInstanceStepContract.GetModelInfo(StepId);
                        ReviewedStatus reviewedStatus = (ReviewedStatus)workflowInstanceStepInfo.ReviewedStatus;
                        if (reviewedStatus != ReviewedStatus.Reviewing)
                        {
                            MessageBox.Show("该业务已经被撤回，无法审核。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
                else
                {
                    customWorkflowInstanceInfo.InstanceStatus = (byte)(submitted ? InstanceStatus.Review : InstanceStatus.None);
                    if (customWorkflowInstanceInfo.UserId != CurrentUser.Instance.UserId)
                    {
                        MessageBox.Show("该业务不属于当前用户提交，无法修改。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            else
            {
                InstanceStatus instanceStatus = submitted ? InstanceStatus.Review : InstanceStatus.None;
                customWorkflowInstanceInfo = GetCustomWorkflowInstanceInfo(instanceStatus);
            }

            /* 获取业务对象 */
            Dictionary<decimal, List<RecordEntity>> dicRecordEntities = new Dictionary<decimal, List<RecordEntity>>();
            if (dicWorkflowTables.ContainsKey(currentStep))
            {
                IList<DataTableBusiness> tableBusinesses = dicWorkflowTables[currentStep];
                foreach (DataTableBusiness tableBusiness in tableBusinesses)
                {
                    List<RecordEntity> recordEntities = null;
                    if (dicRecordEntities.ContainsKey(tableBusiness.FormId))
                    {
                        recordEntities = dicRecordEntities[tableBusiness.FormId];
                    }
                    else
                    {
                        recordEntities = new List<RecordEntity>();
                        dicRecordEntities.Add(tableBusiness.FormId, recordEntities);
                    }
                    RecordSet recordSet = tableBusiness.GetRecordSet();
                    if (!recordSet.Success)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(recordSet.Warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    foreach (RecordEntity recordEntity in recordSet.RecordEntities)
                    {
                        recordEntities.Add(recordEntity);
                    }
                }
            }

            if (submitted)
            {
                /* 下一步审核人 */
                Dictionary<decimal, Dictionary<decimal, string>> nextReviewers = null;
                IList<decimal> dynamicNextReviewers = null;
                decimal dynamicProcessId = decimal.MinValue;
                int counter = 0;
                WorkflowProcessType workflowProcessType = (WorkflowProcessType)customWorkflowProcessInfo.ProcessType;
                switch (workflowProcessType)
                {
                    case WorkflowProcessType.ParallelBranch:
                    case WorkflowProcessType.SingleBranch:
                        nextReviewers = customWorkflowProcessContract.GetNextReviewers(CurrentUser.Instance.UserId, customWorkflowProcessInfo.ProcessId);
                        break;

                    case WorkflowProcessType.SelectiveBranch:
                        Dictionary<decimal, CommonDataField> commonDataFields = new Dictionary<decimal, CommonDataField>();
                        IList<decimal> childProcessIds = customWorkflowProcessContract.GetChildNodeIds(customWorkflowProcessInfo.ProcessId);
                        foreach (var childProcessId in childProcessIds)
                        {
                            IList<WorkflowProcessAndDataFieldInfo> workflowProcessAndDataFieldInfos = customWorkflowProcessContract.GetModelInfos(childProcessId);
                            foreach (var workflowProcessAndDataFieldInfo in workflowProcessAndDataFieldInfos)
                            {
                                foreach (var keyValue in dicRecordEntities)
                                {
                                    foreach (RecordEntity recordEntity in keyValue.Value)
                                    {
                                        foreach (CommonDataField commonDataField in recordEntity.CommonDataFields)
                                        {
                                            if (commonDataField.DataFieldId == workflowProcessAndDataFieldInfo.DataFieldId)
                                            {
                                                if (!commonDataFields.ContainsKey(commonDataField.DataFieldId))
                                                {
                                                    commonDataFields.Add(commonDataField.DataFieldId, commonDataField);
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        nextReviewers = customWorkflowProcessContract.GetNextReviewers(CurrentUser.Instance.UserId, customWorkflowProcessInfo.ProcessId, commonDataFields);
                        if (nextReviewers.Count == 0)
                        {
                            MessageBox.Show(string.Format("该流程({0})提交后并未找到合适的分支流程，请联系管理员。", customWorkflowProcessContract.GetNodeNameByNodeId(customWorkflowProcessInfo.ProcessId)), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        break;

                    case WorkflowProcessType.DynamicBranch:
                    case WorkflowProcessType.DynamicBranchInDeps:
                    case WorkflowProcessType.DynamicBranchBetweenDeps:
                        IList<decimal> childNodeIds = customWorkflowProcessContract.GetChildNodeIds(customWorkflowProcessInfo.ProcessId);
                        if (childNodeIds != null && childNodeIds.Count == 1)
                        {
                            dynamicProcessId = childNodeIds[0];
                            switch (workflowProcessType)
                            {
                                case WorkflowProcessType.DynamicBranch:
                                    dynamicNextReviewers = customWorkflowProcessContract.GetGlobalNextReviewers(CurrentUser.Instance.UserId, dynamicProcessId, MAX_USER_COUNT_ON_NODE);
                                    break;

                                case WorkflowProcessType.DynamicBranchInDeps:
                                    dynamicNextReviewers = customWorkflowProcessContract.GetNextReviewers(CurrentUser.Instance.UserId, dynamicProcessId, MAX_USER_COUNT_ON_NODE);
                                    break;

                                case WorkflowProcessType.DynamicBranchBetweenDeps:
                                    IList<decimal> depIds = new List<decimal>();
                                    foreach (var keyValue in dicRecordEntities)
                                    {
                                        foreach (RecordEntity recordEntity in keyValue.Value)
                                        {
                                            if (recordEntity.AdditionalData.ContainsKey(AdditionalRecordType.DnamicalDep))
                                            {
                                                IList<CommonDataFieldValue> commonDataFieldValue = recordEntity.AdditionalData[AdditionalRecordType.DnamicalDep];
                                                foreach (var dataFieldValue in commonDataFieldValue)
                                                {
                                                    string depName = Convert.ToString(dataFieldValue.UpdatedDataFieldValue);
                                                    if (!string.IsNullOrWhiteSpace(depName))
                                                    {
                                                        decimal depId = customDepartmentContract.GetDepIdByName(depName);
                                                        if (depId > 0)
                                                        {
                                                            depIds.Add(depId);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    Dictionary<decimal, decimal> dicNextReviewers = customWorkflowProcessContract.GetNextReviewers(dynamicProcessId, depIds);
                                    foreach (var depId in depIds)
                                    {
                                        if (!dicNextReviewers.ContainsKey(depId))
                                        {
                                            MessageBox.Show(string.Format("该节点({0})下没有审核人员，请和管理员联系。", customWorkflowProcessContract.GetNodeNameByNodeId(dynamicProcessId)), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    foreach (var reviewer in dicNextReviewers)
                                    {
                                        dynamicNextReviewers.Add(reviewer.Value);
                                    }
                                    break;
                            }
                            if (dynamicNextReviewers.Count == 0)
                            {
                                MessageBox.Show(string.Format("该节点({0})下没有审核人员，请和管理员联系。", customWorkflowProcessContract.GetNodeNameByNodeId(childNodeIds[0])), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("流向类型为动态节点时，有且仅有一个流向类型为单行节点的子节点。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        break;
                }      
                Dictionary<decimal, decimal> reivewers = new Dictionary<decimal, decimal>();
                if (workflowProcessType != WorkflowProcessType.DynamicBranchInDeps)
                {
                    bool onlyOne = true;
                    foreach (KeyValuePair<decimal, Dictionary<decimal, string>> keyValue in nextReviewers)
                    {
                        if (keyValue.Value.Count <= 0)
                        {
                            MessageBox.Show(string.Format("该节点({0})下没有审核人员，请和管理员联系。", customWorkflowProcessContract.GetNodeNameByNodeId(keyValue.Key)), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        if (keyValue.Value.Count != 1)
                        {
                            onlyOne = false;
                        }
                    }
                    if (onlyOne)
                    {
                        /* 获得节点编号和下一步审核人编号 */
                        foreach (KeyValuePair<decimal, Dictionary<decimal, string>> keyValue in nextReviewers)
                        {
                            foreach (KeyValuePair<decimal, string> reviewers in keyValue.Value)
                            {
                                reivewers.Add(keyValue.Key, reviewers.Key);
                            }
                        }
                    }
                    else
                    {
                        WorkflowHandlerMode workflowHandlerMode = (WorkflowHandlerMode)customWorkflowProcessInfo.HandlerMode;
                        switch (workflowHandlerMode)
                        {
                            case WorkflowHandlerMode.UserSelected:
                                ReviewersForm frmReviewers = new ReviewersForm()
                                {
                                    CustomWorkflowProcessInfo = customWorkflowProcessInfo,
                                    Reviewers = nextReviewers
                                };
                                frmReviewers.GetReviewers = (reivewersSelected) =>
                                {
                                    foreach (KeyValuePair<decimal, decimal> keyValue in reivewersSelected)
                                    {
                                        reivewers.Add(keyValue.Key, keyValue.Value);
                                    }
                                };
                                Cursor = Cursors.Default;
                                frmReviewers.ShowDialog();
                                break;

                            case WorkflowHandlerMode.InTurn:
                            case WorkflowHandlerMode.RemainingWorkflowsAveraged:
                                IList<decimal> reviewerIds = new List<decimal>();
                                foreach (KeyValuePair<decimal, Dictionary<decimal, string>> keyValue in nextReviewers)
                                {
                                    foreach (KeyValuePair<decimal, string> reviewers in keyValue.Value)
                                    {
                                        reviewerIds.Add(reviewers.Key);
                                    }
                                    if (reviewerIds.Count > 1)
                                    {
                                        decimal nextReviewerId = workflowInstanceStepContract.GetNextReviewerId(customWorkflowProcessInfo.ProcessId, reviewerIds, workflowHandlerMode);
                                        reivewers.Add(keyValue.Key, nextReviewerId);                                        
                                    }
                                    else
                                    {
                                        if (reviewerIds.Count == 1)
                                        {
                                            reivewers.Add(keyValue.Key, reviewerIds[0]);
                                        }
                                    }
                                    reviewerIds.Clear();
                                }
                                break;
                        }
                    }
                }

                /* 1. 保存数据 */
                decimal processId = 0;
                ReviewedAction reviewedAction = ReviewedAction.Sumbitted;
                if (StepId > 0)
                {
                    processId = customWorkflowProcessInfo.ProcessId;
                    reviewedAction = ReviewedAction.Pass;
                }
                else
                {
                    CommonNode commonNode = customWorkflowProcessContract.GetWorkflowRootNode(CustomWorkflowInfo.WorkflowId);
                    processId = commonNode.NodeId;
                }
                IList<WorkflowInstanceStepInfo> workflowInstanceStepInfos = new List<WorkflowInstanceStepInfo>();
                IList<WorkflowInstanceLogInfo> workflowInstanceLogInfos = new List<WorkflowInstanceLogInfo>();
                WorkflowProcessCategory workflowProcessCategory = (WorkflowProcessCategory)customWorkflowProcessInfo.ProcessCategory;
                if (workflowProcessCategory == WorkflowProcessCategory.End)
                {
                    WorkflowInstanceLogInfo workflowInstanceLogInfo = new WorkflowInstanceLogInfo()
                    {
                        ProcessId = customWorkflowProcessInfo.ProcessId,
                        InstanceId = customWorkflowInstanceInfo.InstanceId,
                        ParentUserId = CurrentUser.Instance.UserId,
                        UserId = decimal.MinValue,
                        ReviewedAction = (byte)ReviewedAction.Compelete,
                        Comment = meComment.Text.Trim(),
                        TimeReviewed = DateTime.Now,
                        IsDraft = false
                    };
                    customWorkflowInstanceInfo.InstanceStatus = (byte)InstanceStatus.Completed;
                    workflowInstanceLogInfos.Add(workflowInstanceLogInfo);
                }
                else
                {
                    switch (workflowProcessType)
                    {
                        case WorkflowProcessType.SelectiveBranch:
                            counter = reivewers.Count;
                            break;

                        case WorkflowProcessType.DynamicBranchInDeps:
                        case WorkflowProcessType.DynamicBranchBetweenDeps:
                            counter = dynamicNextReviewers.Count;
                            break;

                        default:
                            counter = 0;
                            break;
                    }
                    /* 提交给下一步操作人 */
                    if (workflowProcessType == WorkflowProcessType.DynamicBranchInDeps 
                        || workflowProcessType == WorkflowProcessType.DynamicBranchBetweenDeps)
                    {
                        foreach (var dynamicNextReviewer in dynamicNextReviewers)
                        {
                            WorkflowInstanceStepInfo workflowInstanceStepInfo = new WorkflowInstanceStepInfo()
                            {
                                InstanceId = InstanceId,
                                ProcessId = dynamicProcessId,
                                UserId = dynamicNextReviewer,
                                ReviewedStatus = (byte)ReviewedStatus.Pending,
                                TimeReviewed = DateTime.Now,
                                ProcessCounter = counter
                            };
                            workflowInstanceStepInfos.Add(workflowInstanceStepInfo);
                            /* 操作日志 */
                            WorkflowInstanceLogInfo workflowInstanceLogInfo = new WorkflowInstanceLogInfo()
                            {
                                ProcessId = processId,
                                InstanceId = InstanceId,
                                ParentUserId = CurrentUser.Instance.UserId,
                                UserId = dynamicNextReviewer,
                                ReviewedAction = (byte)reviewedAction,
                                Comment = meComment.Text.Trim(),
                                TimeReviewed = DateTime.Now,
                                IsDraft = false
                            };
                            workflowInstanceLogInfos.Add(workflowInstanceLogInfo);
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<decimal, decimal> reivewer in reivewers)
                        {
                            WorkflowInstanceStepInfo workflowInstanceStepInfo = new WorkflowInstanceStepInfo()
                            {
                                InstanceId = InstanceId,
                                ProcessId = reivewer.Key,
                                UserId = reivewer.Value,
                                ReviewedStatus = (byte)ReviewedStatus.Pending,
                                TimeReviewed = DateTime.Now,
                                ProcessCounter = counter
                            };
                            workflowInstanceStepInfos.Add(workflowInstanceStepInfo);
                            /* 操作日志 */
                            WorkflowInstanceLogInfo workflowInstanceLogInfo = new WorkflowInstanceLogInfo()
                            {
                                ProcessId = processId,
                                InstanceId = InstanceId,
                                ParentUserId = CurrentUser.Instance.UserId,
                                UserId = reivewer.Value,
                                ReviewedAction = (byte)reviewedAction,
                                Comment = meComment.Text.Trim(),
                                TimeReviewed = DateTime.Now,
                                IsDraft = false
                            };
                            workflowInstanceLogInfos.Add(workflowInstanceLogInfo);
                        }
                    }              
                }
                InstanceSet instanceSet = customWorkflowInstanceContract.Process(customWorkflowInstanceInfo, StepId, workflowInstanceLogInfos, workflowInstanceStepInfos, dicRecordEntities);
                InstanceId = instanceSet.InstanceId;
            }
            else
            {
                WorkflowInstanceLogInfo workflowInstanceLogInfo = null;
                if (StepId > 0)
                {
                    workflowInstanceLogInfo = new WorkflowInstanceLogInfo()
                    {
                        ProcessId = customWorkflowProcessInfo.ProcessId,
                        InstanceId = customWorkflowInstanceInfo.InstanceId,
                        ParentUserId = CurrentUser.Instance.UserId,
                        UserId = decimal.MinValue,
                        ReviewedAction = (byte)ReviewedAction.None,
                        Comment = meComment.Text.Trim(),
                        TimeReviewed = DateTime.Now,
                        IsDraft = true
                    };
                }
                InstanceSet instanceSet = customWorkflowInstanceContract.Process(customWorkflowInstanceInfo, workflowInstanceLogInfo, dicRecordEntities);
                InstanceId = instanceSet.InstanceId;
            }
            
            return true;
        }

        /// <summary>
        /// 获得工作流实例对象
        /// </summary>
        /// <param name="instanceStatus"></param>
        /// <returns></returns>
        private CustomWorkflowInstanceInfo GetCustomWorkflowInstanceInfo(InstanceStatus instanceStatus)
        {
            CustomWorkflowInstanceInfo workflowInstanceInfo = new CustomWorkflowInstanceInfo()
            {
                WorkflowId = CustomWorkflowInfo.WorkflowId,
                UserId = CurrentUser.Instance.UserId,
                ParentUserId = CurrentUser.Instance.UserId,
                InstanceName = string.Format("{0}_{1}_{2}_{3}", Text, CurrentUser.Instance.UserName, CurrentUser.Instance.UserActualName, DateTime.Now),
                InstanceStatus = (byte)instanceStatus,
                TimeCreated = DateTime.Now,
                TimeModified = DateTime.Now,
                TimeSumbitted = DateTime.Now
            };

            return workflowInstanceInfo;
        }

        /// <summary>
        /// 上一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentStep > 0)
            {
                currentStep--;
                if (currentStep >= 0 && currentStep < xtcBussiness.TabPages.Count)
                {
                    xtcBussiness.SelectedTabPageIndex = currentStep;
                }
                else
                {
                    throw new ArgumentException("上一步系统出错。");
                }
                if (currentStep < xtcBussiness.TabPages.Count)
                {
                    btnNext.Text = "下一步(&N)";
                    btnNext.ImageIndex = 2;
                }
                if (currentStep == 0)
                {
                    btnPrevious.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 保存草稿
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDraft_Click(object sender, EventArgs e)
        {
            if (currentStep >= 0 && currentStep < xtcBussiness.TabPages.Count)
            {
                SubmitBusiness(false);
                MessageBox.Show("草稿保存成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 判断当前页是否加载完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtcBussiness_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            /* Tag属性等于1的时候表明控件已经加载完毕。*/
            int tag = Convert.ToInt32(xtcBussiness.Tag);
            if (tag == 1 && FormReadOnly && xtcBussiness.SelectedTabPageIndex < currentFormInfos.Count)
            {
                CreateControls(xtcBussiness.SelectedTabPageIndex, e.Page, currentFormInfos[xtcBussiness.SelectedTabPageIndex]);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 动态创建控件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="tabPage"></param>
        /// <param name="customFormInfos"></param>
        private void CreateControls(int index, XtraTabPage tabPage, IList<CustomFormInfo> customFormInfos)
        {
            if (tabPage.Tag != null)
            {
                return;
            }
            Cursor = Cursors.WaitCursor;
            try
            {
                tabPage.Tag = customFormInfos;
                IList<decimal> tableIds = new List<decimal>();
                foreach (CustomFormInfo customFormInfo in customFormInfos)
                {
                    FormType tableType = (FormType)customFormInfo.FormType;
                    switch (tableType)
                    {
                        case FormType.CombinedTable:
                            IList<CommonNode> commonNodeInfos = combinedTableContract.GetTables(customFormInfo.CombinedTableId);
                            if (!combinedDataFields.ContainsKey(customFormInfo.CombinedTableId))
                            {
                                IList<CommonNode> dataFields = combinedTableContract.GetDataFields(customFormInfo.CombinedTableId);
                                combinedDataFields.Add(customFormInfo.CombinedTableId, dataFields);
                            }
                            foreach (var commonNodeInfo in commonNodeInfos)
                            {
                                tableIds.Add(commonNodeInfo.NodeId);
                            }
                            break;

                        case FormType.Table:
                            tableIds.Add(customFormInfo.TableId);
                            break;

                        default:
                            throw new ArgumentException("不支持该表类型。");
                    }
                }
                IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = customRoleContract.GetAuthorizedExtendedCustomDataFieldInfos(CurrentUser.Instance.UserId, tableIds, DataAuthorityType.Business);
                CreateControls(index, xtcBussiness.TabPages[index], customFormInfos, extendedCustomDataFieldInfos);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 动态创建控件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="tabPage"></param>
        /// <param name="customFormInfos"></param>
        /// <param name="extendedCustomDataFieldInfos"></param>
        private void CreateControls(int stepIndex, XtraTabPage tabPage, IList<CustomFormInfo> customFormInfos, IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos)
        {
            int xpos = 0, ypos = COMMON_HEIGHT_OF_SPACE / 2, numberPerLayer = 0, maxHeightPerLayer = 0;
            bool separatorControlCreated = false;
            IList<DataTableBusiness> tableBusinesses = new List<DataTableBusiness>();
            dicWorkflowTables.Add(stepIndex, tableBusinesses);
            for (int index = 0; index < customFormInfos.Count; index++)
            {
                /* 1. 创建控件 */
                int dataFiledIndex = 0, multiTextBoxCount = 0, width = 0;
                bool showTable = false;

                CustomFormInfo customFormInfo = customFormInfos[index];
                FormShowStyle formShowStyle = (FormShowStyle)customFormInfo.ShowMode;
                FormShowStyleSetting formShowStyleSetting = GetFormShowStyleSettings(formShowStyle);
                if (formShowStyleSetting.SeparatorControlCreated)
                {
                    separatorControlCreated = true;
                    numberPerLayer = 0;
                    width = this.Width - CURRENT_WIDTH_OF_MARGIN_SPACE - COMMON_WIDTH_OF_MARGIN_SPACE;
                    xpos = COMMON_WIDTH_OF_MARGIN_SPACE;
                }
                else
                {
                    width = (this.Width - CURRENT_WIDTH_OF_MARGIN_SPACE) / MAX_NUMBER_PER_LAYER - COMMON_WIDTH_OF_MARGIN_SPACE;
                    if ((++numberPerLayer % MAX_NUMBER_PER_LAYER) == 0)
                    {
                        separatorControlCreated = true;
                        xpos = (numberPerLayer - 1) * (width + COMMON_WIDTH_OF_MARGIN_SPACE) + COMMON_WIDTH_OF_MARGIN_SPACE;
                        numberPerLayer = 0;
                    }
                    else
                    {
                        separatorControlCreated = false;
                        xpos = COMMON_WIDTH_OF_MARGIN_SPACE;
                    }
                }
                GroupControl groupControl = new GroupControl()
                {
                    ShowCaption = formShowStyleSetting.FormCompleted,
                    Text = customFormInfo.FormName,
                    Width = width,
                    Tag = customFormInfo
                };
                Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
                Dictionary<string, CommonDataFieldInfo> systemDataFieldNameRelations = DataFieldHelper.GetSystemDataFieldInfo(customFormInfo.DataFieldSetting);
                foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in systemDataFieldNameRelations)
                {
                    dataFieldNameRelations.Add(keyValue.Key, keyValue.Value);
                }                
                FormType tableType = (FormType)customFormInfo.FormType;
                switch (tableType)
                {
                    case FormType.CombinedTable:
                        if (combinedDataFields.ContainsKey(customFormInfo.CombinedTableId))
                        {
                            IList<CommonNode> commonNodes = combinedDataFields[customFormInfo.CombinedTableId];                            
                            IList<ExtendedCustomDataFieldInfo> currentExtendedCustomDataFieldInfos = new List<ExtendedCustomDataFieldInfo>();
                            foreach (CommonNode commonNode in commonNodes)
                            {
                                foreach (ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
                                {
                                    if (extendedCustomDataFieldInfo.DataFieldId == commonNode.NodeId)
                                    {
                                        bool added = AddCommonDataFieldInfo(dataFieldNameRelations, extendedCustomDataFieldInfo);
                                        if (added)
                                        {
                                            currentExtendedCustomDataFieldInfos.Add(extendedCustomDataFieldInfo);
                                        }
                                        break;
                                    }
                                }
                            }                            
                            DataTableBusiness tableBusiness = null;
                            if (customFormInfo.BusinessEnabled)
                            {
                                tableBusiness = new DataTableBusiness(InstanceId, commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                    customEnumContract, customDepartmentContract, groupControl, meToolTip);
                            }
                            else
                            {
                                tableBusiness = new DataTableBusiness(commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                    customEnumContract, customDepartmentContract, groupControl, meToolTip);
                            }
                            /* 加载控件 */
                            tableBusiness.CreateControls(currentExtendedCustomDataFieldInfos, formShowStyleSetting, ref multiTextBoxCount);
                            tableBusinesses.Add(tableBusiness);
                            /* 加载数据 */
                            Dictionary<decimal, DataTable> dataRowValues = combinedTableContract.GetDataFilledData(customFormInfo.CombinedTableId, customFormInfo.BusinessEnabled, dataFieldNameRelations, CurrentUser.Instance.UserId, InstanceId);
                            tableBusiness.LoadDataOnCombinedTables(dataRowValues, false);
                        }
                        else
                        {
                            throw new ArgumentException("视图的字段缓存错误。");
                        }
                        break;

                    case FormType.Table:
                        string tablePhysicalName = customTableContract.GetTablePhysicalName(customFormInfo.TableId);
                        DataTableType dataTableType = customTableContract.GetDataTableType(customFormInfo.TableId);
                        IList<ExtendedCustomDataFieldInfo> currentCustomDataFieldInfos = new List<ExtendedCustomDataFieldInfo>();
                        foreach (ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
                        {
                            if (customFormInfo.TableId == extendedCustomDataFieldInfo.TableId)
                            {
                                bool added = AddCommonDataFieldInfo(dataFieldNameRelations, extendedCustomDataFieldInfo);
                                if (added)
                                {
                                    currentCustomDataFieldInfos.Add(extendedCustomDataFieldInfo);
                                }
                            }
                        }
                        if (currentCustomDataFieldInfos.Count == 0)
                        {
                            continue;
                        }
                        switch (dataTableType)
                        {
                            case DataTableType.MasterSlaveTable:
                            case DataTableType.AssistanTable:
                                showTable = true;
                                DataTableControl dataTableControl = new DataTableControl();
                                dataTableControl.Dock = DockStyle.Fill;
                                dataTableControl.FormReadOnly = FormReadOnly;
                                dataTableControl.CustomRoleContract = customRoleContract;
                                dataTableControl.TableId = customFormInfo.TableId;
                                dataTableControl.SetAuthorityHandler = (devExpressGrid, btnAdd, btnDelete) =>
                                {
                                    Int64 tableAuthority = customRoleContract.GetTableAuthority(CurrentUser.Instance.UserId, customFormInfo.TableId, DataAuthorityType.Business);
                                    Int64 authority = 0;
                                    dataTableControl.AllowDataExported = AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Export);
                                    dataTableControl.AllowDataImported = AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Import);
                                    dataTableControl.AllowStatusSetting = (dataTableType == DataTableType.MasterSlaveTable) && AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.MasterSlave);
                                    if (AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Add))
                                    {
                                        authority |= Convert.ToInt64(GridViewAuthority.Add);
                                        btnAdd.Enabled = true;
                                    }
                                    else
                                    {
                                        btnAdd.Enabled = false;
                                    }
                                    if (AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Edit))
                                    {
                                        authority |= Convert.ToInt64(GridViewAuthority.Edit);
                                    }
                                    if (AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Delete))
                                    {
                                        authority |= Convert.ToInt64(GridViewAuthority.Delete);
                                        btnDelete.Enabled = true;
                                    }
                                    else
                                    {
                                        btnDelete.Enabled = false;
                                    }
                                    authority |= Convert.ToInt64(GridViewAuthority.Move);
                                    devExpressGrid.Authority = authority;
                                };
                                dataTableControl.RefreshSortinHandler = () =>
                                {
                                    if (customFormInfo.BusinessEnabled)
                                    {
                                        customTableContract.UpdateRecordSortingByBusinessId(InstanceId, customFormInfo.TableId);
                                    }
                                    else
                                    {
                                        customTableContract.UpdateRecordSortingByUserId(ParentUserId, customFormInfo.TableId);
                                    }
                                };
                                dataTableControl.LoadDataHanler = (devExpressGrid) =>
                                {
                                    int totalCount = 0;
                                    devExpressGrid.DataKeyNames = new string[] { DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId),
                                        DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId) };
                                    IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                                    sortingCondtions.Add(new SortingCondtion("RecordSorting", CustomSorting.Ascending));
                                    IList<WhereConditon> conditons = new List<WhereConditon>();
                                    if (customFormInfo.BusinessEnabled)
                                    {
                                        conditons.Add(new WhereConditon(tablePhysicalName, "BusinessId", "BusinessId", DbType.Decimal, InstanceId, 
                                            DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                    }
                                    else
                                    {
                                        DataFilledProperty dataFilledProperty = (DataFilledProperty)CustomDataInfo.DataFilledProperty;
                                        if (dataFilledProperty == DataFilledProperty.SingleUser)
                                        {
                                            conditons.Add(new WhereConditon(tablePhysicalName, "UserId", "UserId", DbType.Decimal, ParentUserId,
                                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                        }
                                        else
                                        {
                                            throw new ArgumentException("多用户模式下必须启用业务表。");
                                        }
                                    }
                                    DataTable dt = customTableContract.GetTableData(customFormInfo.TableId, customFormInfo.DataFieldSetting, false, false, dataFieldNameRelations,
                                        devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex, devExpressGrid.PageSize, conditons, sortingCondtions, ref totalCount);
                                    devExpressGrid.RecordCount = totalCount;
                                    devExpressGrid.DataSource = dt;
                                    devExpressGrid.ColumnAutoWidth = false;
                                    devExpressGrid.AppearanceCellHAlignment = HorzAlignment.Center;
                                    devExpressGrid.AppearanceHeaderHAlignment = HorzAlignment.Center;
                                    devExpressGrid.OnCustomColumnDisplayText += (sender, e) =>
                                    {
                                        SystemConfigHelper.SetColumnDisplayText(e);
                                    };
                                    foreach (GridColumn gridColumn in devExpressGrid.Columns)
                                    {
                                        if (gridColumn.VisibleIndex < 2 || !gridColumn.Visible)
                                        {
                                            continue;
                                        }
                                        if (dataFieldNameRelations.ContainsKey(gridColumn.FieldName))
                                        {
                                            SetColumnDisplayText(gridColumn, dataFieldNameRelations[gridColumn.FieldName]);
                                        }
                                    }                                    
                                };
                                dataTableControl.AddHandler = (dataRow) =>
                                {
                                    DataTemplateTableForm frmDataTemplateTable = new DataTemplateTableForm();
                                    frmDataTemplateTable.Text = customFormInfo.FormName;                                    
                                    DataTableBusiness business = null;
                                    if (customFormInfo.BusinessEnabled)
                                    {
                                        business = new DataTableBusiness(InstanceId, commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                            customEnumContract, customDepartmentContract, frmDataTemplateTable.Panel, frmDataTemplateTable.MemoEditToolTip);
                                    }
                                    else
                                    {
                                        business = new DataTableBusiness(commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                            customEnumContract, customDepartmentContract, frmDataTemplateTable.Panel, frmDataTemplateTable.MemoEditToolTip);
                                    }

                                    /* 加载控件 */
                                    dataFiledIndex = business.CreateControls(currentCustomDataFieldInfos, formShowStyleSetting, ref multiTextBoxCount);
                                    frmDataTemplateTable.SumbittedHandler = () =>
                                    {
                                        RecordSet recordSet = business.GetRecordSet();
                                        if (!recordSet.Success)
                                        {
                                            Cursor = Cursors.Default;
                                            MessageBox.Show(recordSet.Warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        if (InstanceId <= 0)
                                        {
                                            customWorkflowInstanceInfo = GetCustomWorkflowInstanceInfo(InstanceStatus.None);
                                        }
                                        InstanceItem instanceItem = customWorkflowInstanceContract.Process(customWorkflowInstanceInfo, null, recordSet.RecordEntities);
                                        InstanceId = instanceItem.InstanceId;
                                        dataTableControl.LoadData();
                                        frmDataTemplateTable.Close();
                                    };
                                    if (dataRow != null)
                                    {
                                        business.LoadDataOnTable(customFormInfo.TableId, dataRow, true);
                                    }
                                    frmDataTemplateTable.ShowDialog();
                                };
                                dataTableControl.EditHandler = (dataRow, readOnly) =>
                                {
                                    if (dataRow != null)
                                    {
                                        DataTemplateTableForm frmDataTemplateTable = new DataTemplateTableForm();
                                        frmDataTemplateTable.Text = customFormInfo.FormName;
                                        frmDataTemplateTable.FormReadOnly = readOnly;
                                        DataTableBusiness business = null;
                                        if (customFormInfo.BusinessEnabled)
                                        {
                                            business = new DataTableBusiness(InstanceId, commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                                customEnumContract, customDepartmentContract, frmDataTemplateTable.Panel, frmDataTemplateTable.MemoEditToolTip);
                                        }
                                        else
                                        {
                                            business = new DataTableBusiness(commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                                customEnumContract, customDepartmentContract, frmDataTemplateTable.Panel, frmDataTemplateTable.MemoEditToolTip);
                                        }
                                        /* 加载控件 */
                                        business.CreateControls(currentCustomDataFieldInfos, formShowStyleSetting, ref multiTextBoxCount);                                        
                                        frmDataTemplateTable.SumbittedHandler = () =>
                                        {
                                            RecordSet recordSet = business.GetRecordSet();
                                            if (!recordSet.Success)
                                            {
                                                Cursor = Cursors.Default;
                                                MessageBox.Show(recordSet.Warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                return;
                                            }
                                            if (InstanceId <= 0)
                                            {
                                                customWorkflowInstanceInfo = GetCustomWorkflowInstanceInfo(InstanceStatus.None);
                                            }
                                            InstanceItem instanceItem = customWorkflowInstanceContract.Process(customWorkflowInstanceInfo, null, recordSet.RecordEntities);
                                            InstanceId = instanceItem.InstanceId;
                                            dataTableControl.LoadData();
                                            frmDataTemplateTable.Close();
                                        };
                                        business.LoadDataOnTable(customFormInfo.TableId, dataRow, false);
                                        frmDataTemplateTable.ShowDialog();
                                    }
                                };
                                dataTableControl.MoveRecordHandler = (userId, tableId, recordId, movedDriection) =>
                                {
                                    customTableContract.MoveRecord(userId, tableId, recordId, movedDriection);
                                };
                                dataTableControl.DeleteHandler = (recordIds) =>
                                {
                                    customTableContract.DeleteRecords(customFormInfo.TableId, recordIds);
                                };                                
                                groupControl.Controls.Add(dataTableControl);
                                break;

                            case DataTableType.PrimaryTable:
                                DataTableBusiness tableBusiness = null;
                                if (customFormInfo.BusinessEnabled)
                                {
                                    tableBusiness = new DataTableBusiness(InstanceId, commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                        customEnumContract, customDepartmentContract, groupControl, meToolTip);
                                }
                                else
                                {
                                    tableBusiness = new DataTableBusiness(commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                        customEnumContract, customDepartmentContract, groupControl, meToolTip);
                                }
                                /* 加载控件 */
                                dataFiledIndex = tableBusiness.CreateControls(currentCustomDataFieldInfos, formShowStyleSetting, ref multiTextBoxCount);
                                
                                /* 加载数据 */
                                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                                if (customFormInfo.BusinessEnabled)
                                {
                                    if (InstanceId > 0)
                                    {
                                        whereConditons.Add(new WhereConditon(tablePhysicalName, "BusinessId", "BusinessId", DbType.Decimal, InstanceId,
                                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                        DataTable dataTable = customTableContract.GetTableData(customFormInfo.TableId, customFormInfo.DataFieldSetting, dataFieldNameRelations, whereConditons);
                                        if (dataTable.Rows.Count > 0)
                                        {
                                            tableBusiness.LoadDataOnTable(customFormInfo.TableId, dataTable.Rows[0], false);
                                        }
                                    }
                                }
                                else
                                {                                    
                                    DataFilledProperty dataFilledProperty = (DataFilledProperty)CustomDataInfo.DataFilledProperty;
                                    if (dataFilledProperty == DataFilledProperty.SingleUser)
                                    {
                                        whereConditons.Add(new WhereConditon(tablePhysicalName, "UserId", "UserId", DbType.Decimal, ParentUserId,
                                            DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                        DataTable dataTable = customTableContract.GetTableData(customFormInfo.TableId, customFormInfo.DataFieldSetting, dataFieldNameRelations, whereConditons);
                                        if (dataTable.Rows.Count > 0)
                                        {
                                            tableBusiness.LoadDataOnTable(customFormInfo.TableId, dataTable.Rows[0], false);
                                        }
                                    }
                                }
                                tableBusinesses.Add(tableBusiness);
                                break;
                        }
                        break;
                }
                int height = 0;
                if (showTable)
                {
                    height = TBALE_HEIGHT;
                }
                else
                {
                    int quotient = dataFiledIndex / formShowStyleSetting.CountForEachRow + ((dataFiledIndex % formShowStyleSetting.CountForEachRow == 0) ? 0 : 1);
                    height = 2 * COMMON_HEIGHT_OF_SPACE + (COMMON_HEIGHT_OF_CONTROL + COMMON_HEIGHT_OF_SPACE) * quotient + COMMON_HEIGHT_OF_TEXT * multiTextBoxCount;
                }
                if (formShowStyleSetting.FormCompleted)
                {
                    height += HEIGHT_OF_GROUP_HEADER;
                }
                groupControl.Height = height;
                groupControl.Location = new Point(xpos, ypos);
                tabPage.Controls.Add(groupControl);

                /* 分隔符 */
                if (separatorControlCreated)
                {
                    ypos += (maxHeightPerLayer > groupControl.Height ? maxHeightPerLayer : groupControl.Height);
                    maxHeightPerLayer = 0;
                    /* 最后一行不需要分割符号 */
                    if (index < (customFormInfos.Count - 1))
                    {
                        SeparatorControl separatorControl = new SeparatorControl()
                        {
                            Size = new Size(this.Width - CURRENT_WIDTH_OF_MARGIN_SPACE - COMMON_WIDTH_OF_MARGIN_SPACE, COMMON_HEIGHT_OF_CONTROL),
                            Location = new Point(COMMON_WIDTH_OF_MARGIN_SPACE / 2, ypos)
                        };
                        tabPage.Controls.Add(separatorControl);
                        ypos += COMMON_HEIGHT_OF_CONTROL;
                    }
                }
                else
                {
                    maxHeightPerLayer = maxHeightPerLayer > groupControl.Height ? maxHeightPerLayer : groupControl.Height;
                }
            }
            if (numberOfSteps == 1 && (ypos + COMMON_HEIGHT_OF_SPACE) < xtcBussiness.Height)
            {
                this.Height -= xtcBussiness.Height - (ypos + COMMON_HEIGHT_OF_SPACE);
                xtcBussiness.Height = ypos + COMMON_HEIGHT_OF_SPACE;
            }
        }

        /// <summary>
        /// 自定义列内容显示
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="commonDataFieldInfo"></param>
        private void SetColumnDisplayText(GridColumn gridColumn, CommonDataFieldInfo commonDataFieldInfo)
        {
            if (gridColumn == null)
            {
                return;
            }
            DataFieldProperty dataFieldProperty = (DataFieldProperty)commonDataFieldInfo.DataFieldProperty;
            if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
            {
                PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)commonDataFieldInfo.DataFieldType;
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                        gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        gridColumn.DisplayFormat.FormatString = "G";
                        break;

                    case PhysicalDataFieldType.YearAndMonthAndDay:
                        gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        gridColumn.DisplayFormat.FormatString = "yyyy-MM-dd";
                        break;

                    case PhysicalDataFieldType.YearAndMonth:
                        gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        gridColumn.DisplayFormat.FormatString = "y";
                        break;

                    case PhysicalDataFieldType.MonthAndDay:
                        gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        gridColumn.DisplayFormat.FormatString = "m";
                        break;

                    case PhysicalDataFieldType.Time:
                        gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        gridColumn.DisplayFormat.FormatString = "HH:mm:ss";
                        break;

                    case PhysicalDataFieldType.PrimaryAssociation:
                    case PhysicalDataFieldType.SecondaryAssociation:
                        if (gridColumn.ColumnType == typeof(DateTime))
                        {
                            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                            gridColumn.DisplayFormat.FormatString = "yyyy-MM-dd";
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 保存字段信息
        /// </summary>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="extendedCustomDataFieldInfo"></param>
        /// <returns></returns>
        private bool AddCommonDataFieldInfo(Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo)
        {
            bool save = true;

            DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
            string expressionText = string.Empty;
            if (dataFieldProperty == DataFieldProperty.LogicalDataField)
            {
                LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                if (logicalDataFieldType == LogicalDataFieldType.Empty || logicalDataFieldType == LogicalDataFieldType.OneDimCode
                    || logicalDataFieldType == LogicalDataFieldType.TwoDimCode || logicalDataFieldType == LogicalDataFieldType.UserName)
                {
                    save = false;
                }
                else
                {
                    expressionText = customDataFieldContract.GetDataFieldLogicalExpression(extendedCustomDataFieldInfo.DataFieldId);
                }
            }
            if (save)
            {
                dataFieldNameRelations.Add(extendedCustomDataFieldInfo.PhysicalName,
                    new CommonDataFieldInfo(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.TableId, extendedCustomDataFieldInfo.PhysicalName, extendedCustomDataFieldInfo.LogicalName,
                    expressionText, dataFieldProperty, extendedCustomDataFieldInfo.DataFieldType));
            }

            return save;
        }
        
        /// <summary>
        /// 获得当前表格样式
        /// </summary>
        /// <param name="formShowStyle"></param>
        /// <returns></returns>
        private FormShowStyleSetting GetFormShowStyleSettings(FormShowStyle formShowStyle)
        {
            bool formCompleted = false;
            int countForEachRow = 1;
            int labelWidth = 0;
            int characterCountOnLabel = 0;
            bool separatorControlCreated = true;
            switch (formShowStyle)
            {
                case FormShowStyle.SingleColumnThreeRanksCompleted:
                    countForEachRow = 3;
                    formCompleted = true;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT;
                    break;

                case FormShowStyle.SingleColumnTwoRanksCompleted:
                    countForEachRow = 2;
                    formCompleted = true;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT_MIDDLE;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT_MIDDLE;
                    break;

                case FormShowStyle.SingleColumnOneRankCompleted:
                    countForEachRow = 1;
                    formCompleted = true;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT_BIG;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT_BIG;
                    break;

                case FormShowStyle.TwoColumnsOneRankCompleted:
                    countForEachRow = 1;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT;
                    formCompleted = true;
                    separatorControlCreated = false;
                    break;

                case FormShowStyle.SingleColumnThreeRanksClean:
                    countForEachRow = 3;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT;
                    break;

                case FormShowStyle.SingleColumnTwoRanksClean:
                    countForEachRow = 2;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT_MIDDLE;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT_MIDDLE;
                    break;

                case FormShowStyle.SingleColumnOneRankClean:
                    countForEachRow = 1;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT_BIG;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT_BIG;
                    break;

                case FormShowStyle.TwoColumnsOneRankClean:
                    countForEachRow = 1;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT;
                    separatorControlCreated = false;
                    break;

                case FormShowStyle.Expand:
                    break;

                case FormShowStyle.Combination:
                    break;
            }

            FormShowStyleSetting formShowStyleSetting = new FormShowStyleSetting(formCompleted, countForEachRow, labelWidth, characterCountOnLabel, separatorControlCreated);

            return formShowStyleSetting;
        }



        #endregion

     
    }
}
