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
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WinBusinessLogic.UserReport;

namespace Blue.WindowsFormsClient.Common
{
    public partial class DataFilledInstanceForm : Form
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

        #endregion
         
        #region 私有变量

        /// <summary>
        /// 步骤数
        /// </summary>
        private byte numberOfSteps = 0;

        /// <summary>
        /// 当前步骤，从0开始
        /// </summary>
        private byte currentStep = 0;

        /// <summary>
        /// 组合表与字段
        /// </summary>
        private Dictionary<decimal, IList<CommonNode>> combinedDataFields;

        /// <summary>
        /// 表格
        /// </summary>
        private readonly Dictionary<int, IList<DataTableBusiness>> dicTableBusinesses;

        /// <summary>
        /// 步骤与表格
        /// </summary>
        private readonly Dictionary<int, IList<CustomFormInfo>> currentFormInfos;

        /// <summary>
        /// 保存当前操作人的页面志位
        /// </summary>
        private readonly List<int> pageSavings = null;

        /// <summary>
        /// 业务所属对象
        /// </summary>
        private CommonUserInfo commonUserInfo = null;

        /// <summary>
        /// 保存提交人的页面操作标志
        /// </summary>
        private Int64 pageSign = 0;

        /// <summary>
        /// 加载中...
        /// </summary>
        private bool isLoading = true;

        /// <summary>
        /// /数据改变
        /// </summary>
        private bool dataChanged = false;

        /// <summary>
        /// 实例状态
        /// </summary>
        private InstanceState currentInstanceState = InstanceState.None;

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
        private readonly IBusinessInstanceContract businessInstanceContract;
        private readonly ICustomExpressionContract customExpressionContract;
        private readonly ICustomDataContract customDataContract;
        private readonly IDataAuditingContract dataAuditingContract;
        private ICustomReportContract customReportContract;
        private ICustomSheetContract customSheetContract;
        private ICustomCellContract customCellContract;
        private ISystemConfigContract systemConfigContract;

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
        /// 实例编号
        /// </summary>
        public decimal InstanceId
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
        /// 是否只读
        /// </summary>
        /// 
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
        public DataFilledInstanceForm()
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
            businessInstanceContract = DataFilledChannelFactory.CreateBusinessInstanceContract();
            customExpressionContract = BusinessChannelFactory.CreateCustomExpressionContract();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();
            dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();            
            combinedDataFields = new Dictionary<decimal, IList<CommonNode>>();
            dicTableBusinesses = new Dictionary<int, IList<DataTableBusiness>>();
            currentFormInfos = new Dictionary<int, IList<CustomFormInfo>>();
            pageSavings = new List<int>();
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
                if (CustomDataInfo.ReportId <= 0)
                {
                    hlnkDownload.Visible = false;
                }                
                if (InstanceId > 0)
                {
                    BusinessInstanceInfo businessInstanceInfo = businessInstanceContract.GetModelInfo(InstanceId);
                    if (businessInstanceInfo != null)
                    {
                        commonUserInfo = userAccountContract.GetCommonUserInfo(businessInstanceInfo.ParentUserId);
                        currentInstanceState = (InstanceState)businessInstanceInfo.InstanceState;
                        pageSign = businessInstanceInfo.PageSign;
                        if (FormReadOnly || currentInstanceState == InstanceState.Completed
                            || currentInstanceState == InstanceState.InitReviewAborted || currentInstanceState == InstanceState.FinalReviewAborted)
                        {
                            pnlBottom.Visible = false;
                        }
                        else
                        {
                            if (currentInstanceState == InstanceState.None)
                            {
                                SetControlsVisible(false);
                            }
                            else
                            {
                                SetControlsVisible(true);
                                meLastestComment.Text = businessInstanceContract.GetLastestComment(InstanceId);
                            }
                            if (businessInstanceInfo.IsDraft)
                            {
                                meComment.Text = businessInstanceInfo.TmpComments;
                            }
                        }
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("该填报并不存在。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    commonUserInfo = userAccountContract.GetCommonUserInfo(ParentUserId);
                    SetControlsVisible(false);
                }
                if (currentInstanceState == InstanceState.None || currentInstanceState == InstanceState.InitReview || currentInstanceState == InstanceState.FinalReview)
                {
                    InstanceState nextInstanceState = GetNextInstanceState(currentInstanceState);
                    switch (nextInstanceState)
                    {
                        case InstanceState.InitReview:
                            Dictionary<decimal, string> initReviewers = null;
                            if (CustomDataInfo.EnableManager)
                            {
                                decimal depId = CurrentUser.Instance.DepId;
                                if (ParentUserId != CurrentUser.Instance.UserId)
                                {
                                    initReviewers = userAccountContract.GetManagementUsersByUserId(CustomDataInfo.RoleId, ParentUserId);
                                }
                                else
                                {
                                    initReviewers = userAccountContract.GetManagementUsers(CustomDataInfo.RoleId, depId);
                                }
                            }
                            else
                            {
                                initReviewers = customDataContract.GetInitReviewers(CustomDataInfo.DataId, ParentUserId);
                            }
                            foreach (KeyValuePair<decimal, string> keyValue in initReviewers)
                            {
                                cmbReviewer.Properties.Items.Add(new CommonNode(keyValue.Key, keyValue.Value));
                            }
                            if (initReviewers.Count > 0)
                            {
                                cmbReviewer.SelectedIndex = 0;
                            }
                            break;

                        case InstanceState.FinalReview:
                            Dictionary<decimal, string> finalReviewers = customDataContract.GetFinalReviewers(CustomDataInfo.DataId);
                            foreach (KeyValuePair<decimal, string> keyValue in finalReviewers)
                            {
                                cmbReviewer.Properties.Items.Add(new CommonNode(keyValue.Key, keyValue.Value));
                            }
                            if (finalReviewers.Count > 0)
                            {
                                cmbReviewer.SelectedIndex = 0;
                            }
                            break;

                        case InstanceState.Completed:
                            lblReviewer.Visible = false;
                            cmbReviewer.Visible = false;
                            break;


                        default:
                            throw new ArgumentException("不支持该实例状态。");
                    }
                }
                else
                {
                    lblReviewer.Visible = false;
                    cmbReviewer.Visible = false;
                }
                DataAuthorityType dataAuthorityType = DataAuthorityType.Business;
                if (currentInstanceState != InstanceState.None)
                {
                    dataAuthorityType = DataAuthorityType.Auditing;
                }
                /* 初始化并加载第一步控件 */
                IList<CustomSectionInfo> customSectionInfos = customSectionContract.GetModelInfos(CustomDataInfo.DataId);
                xtcBussiness.Tag = 0;
                foreach (var customSectionInfo in customSectionInfos)
                {
                    IList<CustomFormInfo> customFormInfos = customFormContract.GetModelInfos(customSectionInfo.SectionId);
                    bool visible = false;
                    foreach (var customFormInfo in customFormInfos)
                    {
                        Int64 tableAuthority = 0;
                        FormType formType = (FormType)customFormInfo.FormType;
                        switch (formType)
                        {
                            case FormType.Table:
                                tableAuthority = customRoleContract.GetTableAuthority(CurrentUser.Instance.UserId, customFormInfo.TableId, dataAuthorityType);
                                if (AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.View))
                                {
                                    visible = true;
                                }
                                break;

                            case FormType.CombinedTable:
                                IList<decimal> tableIds = combinedTableContract.GetTableIds(customFormInfo.CombinedTableId);
                                foreach (var tableId in tableIds)
                                {
                                    tableAuthority = customRoleContract.GetTableAuthority(CurrentUser.Instance.UserId, tableId, dataAuthorityType);
                                    if (AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.View))
                                    {
                                        visible = true;
                                        break;
                                    }
                                }
                                break;
                        }
                        if (visible)
                        {
                            break;
                        }
                    }
                    if (visible)
                    {
                        XtraTabPage tabPage = new XtraTabPage()
                        {
                            Text = customSectionInfo.SectionName,
                            AutoScroll = true
                        };
                        xtcBussiness.TabPages.Add(tabPage);
                        currentFormInfos.Add(numberOfSteps++, customFormInfos);
                    }
                }
                xtcBussiness.Tag = 1;
                if (currentFormInfos.Count > 0)
                {
                    CreateControls(currentStep, xtcBussiness.TabPages[currentStep], currentFormInfos[currentStep]);

                }
                /* 只读模式下以卡片方式显示 */
                DataShowMode dataShowMode = DataShowMode.Tab;
                if (!FormReadOnly)
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
                        btnPrevious.Enabled = false;
                        gcBusiness.ShowCaption = true;
                        gcBusiness.Text = customSectionInfos[0].SectionName;
                        gcBusiness.BorderStyle = BorderStyles.NoBorder;
                        xtcBussiness.ShowTabHeader = DefaultBoolean.False;
                        xtcBussiness.BorderStyle = BorderStyles.NoBorder;
                        break;

                    case DataShowMode.Tab:
                        btnPrevious.Enabled = true;
                        btnPrevious.Text = "保存(&S)";
                        btnPrevious.ImageIndex = 6;
                        btnNext.Text = "提交(&O)";
                        btnNext.ImageIndex = 3;
                        gcBusiness.ShowCaption = false;
                        gcBusiness.BorderStyle = BorderStyles.Default;
                        xtcBussiness.ShowTabHeader = DefaultBoolean.True;
                        xtcBussiness.SelectedTabPageIndex = 0;
                        xtcBussiness.BorderStyle = BorderStyles.Default;
                        break;
                }
                isLoading = false;
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
        /// 选项卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtcBussiness_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            /* Tag属性等于1的时候表明控件已经加载完毕。*/
            int tag = Convert.ToInt32(xtcBussiness.Tag);
            if (tag == 1 && xtcBussiness.SelectedTabPageIndex < currentFormInfos.Count)
            {
                currentStep = Convert.ToByte(xtcBussiness.SelectedTabPageIndex);
                isLoading = true;
                CreateControls(xtcBussiness.SelectedTabPageIndex, e.Page, currentFormInfos[xtcBussiness.SelectedTabPageIndex]);
                isLoading = false;
                btnPrevious.ImageIndex = 6;
            }
        }

        /// <summary>
        /// 下一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            bool result = false;
            DataShowMode dataShowMode = (DataShowMode)CustomDataInfo.DataShowMode;
            switch (dataShowMode)
            {
                case DataShowMode.Tab:
                    if (numberOfSteps > 1)
                    {
                        StringBuilder sb = new StringBuilder();
                        int index = 1;
                        for (byte idx = 0; idx < numberOfSteps; idx++)
                        {
                            if (!AuthorityHelper.CheckAuthority(pageSign, idx))
                            {
                                sb.AppendFormat("({0}){1}, ", index++, xtcBussiness.TabPages[idx].Text);
                            }
                        }
                        if (sb.Length > 0)
                        {
                            sb.Remove(sb.Length - 2, 2);
                            MessageBox.Show(string.Format("以下页面未填入数据：{0}。", sb), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    result = SubmitBusiness(true);
                    if (result)
                    {
                        CloseForm?.Invoke();
                        MessageBox.Show("提交成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    break;

                case DataShowMode.Step:
                    if (currentStep >= 0 && currentStep < numberOfSteps && currentStep < xtcBussiness.TabPages.Count)
                    {
                        bool submitted = currentStep == (numberOfSteps - 1);
                        result = SubmitBusiness(submitted);
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
                    break;
            }
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkReject_Click(object sender, EventArgs e)
        {
            if (InstanceId > 0)
            {
                if (MessageBox.Show("确认驳回该业务？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    BusinessInstanceInfo instanceInfo = businessInstanceContract.GetModelInfo(InstanceId);
                    if (instanceInfo.ReviewerId != CurrentUser.Instance.UserId)
                    {
                        MessageBox.Show("该业务已经被撤回，无法驳回。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    InstanceState currentInstanceState = (InstanceState)instanceInfo.InstanceState;
                    InstanceState previousInstanceState = GetPreviousInstanceState(currentInstanceState);
                    decimal reviewerId = CurrentUser.Instance.UserId;
                    if (previousInstanceState != InstanceState.None)
                    {
                        reviewerId = businessInstanceContract.GetLastestReviewerId(InstanceId);
                    }
                    BusinessInstanceInfo businessInstanceInfo = GetBusinessInstanceInfo(previousInstanceState, ParentUserId, instanceInfo.UserId,
                        reviewerId, false, string.Empty, false, currentStep);
                    businessInstanceContract.Update(businessInstanceInfo);                    
                    ReviewedAction reviewedAction = GetCurrentReviewedAction(currentInstanceState, false);
                    BusinessInstanceStepInfo businessInstanceStepInfo = new BusinessInstanceStepInfo()
                    {
                        InstanceId = InstanceId,
                        UserId = CurrentUser.Instance.UserId,
                        ReviewedAction = (byte)reviewedAction,
                        ActionVisible = true,
                        CommentReviewed = meComment.Text.Trim()
                    };
                    businessInstanceContract.Insert(businessInstanceStepInfo);
                }
                CloseForm?.Invoke();
                MessageBox.Show("驳回成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        /// <summary>
        /// 上一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            DataShowMode dataShowMode = (DataShowMode)CustomDataInfo.DataShowMode;
            switch (dataShowMode)
            {
                case DataShowMode.Tab:
                    SubmitBusiness(false);
                    btnPrevious.ImageIndex = 7;
                    break;

                case DataShowMode.Step:
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
                    break;
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
        /// 自动保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtcBussiness_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
        {
            if (isLoading || !dataChanged)
            {
                return;
            }
            DataShowMode dataShowMode = (DataShowMode)CustomDataInfo.DataShowMode;
            if (dataShowMode == DataShowMode.Tab)
            {
                e.Cancel = !SubmitBusiness(false);
            }
        }

        /// <summary>
        /// 下载报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkDownload_Click(object sender, EventArgs e)
        {
            if (customReportContract == null)
            {
                customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();
                customSheetContract = BusinessDesignerChannelFactory.CreateCustomSheetContract();
                customCellContract = BusinessDesignerChannelFactory.CreateCustomCellContract();
                systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
            }
            if (CustomDataInfo.ReportId > 0)
            {
                CustomReport customReport = new CustomReport(customReportContract, customSheetContract, customCellContract,
                    customTableContract, customDataFieldContract, systemConfigContract);
                customReport.GenerateExcel(CustomDataInfo.ReportId, ParentUserId);
            }
            else
            {
                MessageBox.Show("该业务不提供报表下载。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 更新提交状态
        /// </summary>
        private void UpdateSumbittedStatus()
        {
            if (!isLoading)
            {
                dataChanged = true;
                btnPrevious.ImageIndex = 6;
            }
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="submitted"></param>
        /// <returns></returns>
        private bool SubmitBusiness(bool submitted)
        {
            /* 获取业务对象 */
            Dictionary<decimal, List<RecordEntity>> dicRecordEntities = new Dictionary<decimal, List<RecordEntity>>();
            if (dicTableBusinesses.ContainsKey(currentStep))
            {
                IList<DataTableBusiness> tableBusinesses = dicTableBusinesses[currentStep];
                foreach (DataTableBusiness tableBusiness in tableBusinesses)
                {
                    List<RecordEntity>  recordEntities = null;
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
                        if (pageSavings.Contains(currentStep))
                        {
                            pageSavings.Remove(currentStep);
                        }
                        MessageBox.Show(recordSet.Warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    foreach (RecordEntity recordEntity in recordSet.RecordEntities)
                    {
                        if (recordEntity != null)
                        {
                            recordEntities.Add(recordEntity);
                        }
                    }                   
                }
            }
            if (!pageSavings.Contains(currentStep))
            {
                pageSavings.Add(currentStep);
            }     

            /* 下一步状态 */
            InstanceState currentInstanceState = InstanceState.None;
            BusinessInstanceInfo instanceInfo = null;
            decimal userId = CurrentUser.Instance.UserId;
            decimal reviewerId = decimal.MinValue;
            if (InstanceId > 0)
            {
                instanceInfo = businessInstanceContract.GetModelInfo(InstanceId);
                currentInstanceState = (InstanceState)instanceInfo.InstanceState;
                userId = instanceInfo.UserId;
                reviewerId = instanceInfo.ReviewerId;
                if (currentInstanceState != InstanceState.None)
                {
                    if (instanceInfo.ReviewerId != CurrentUser.Instance.UserId)
                    {
                        MessageBox.Show("该业务已经被撤回，无法审核。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    if (instanceInfo.UserId != CurrentUser.Instance.UserId)
                    {
                        MessageBox.Show("该业务不属于当前用户提交，无法修改。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            InstanceState nextInstanceState = GetNextInstanceState(currentInstanceState);
            if (nextInstanceState == InstanceState.InitReview || nextInstanceState == InstanceState.FinalReview)
            {
                if (cmbReviewer.Properties.Items.Count == 0)
                {
                    MessageBox.Show("该单位并未设置下一步审核人，请与管理员联系。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (submitted)
                {
                    CommonNode commonNode = cmbReviewer.EditValue as CommonNode;
                    reviewerId = commonNode.NodeId;
                }
            }
            BusinessInstanceInfo businessInstanceInfo = null;
            if (submitted)
            {
                businessInstanceInfo = GetBusinessInstanceInfo(nextInstanceState, ParentUserId, userId, reviewerId, 
                    false, string.Empty, true, currentStep);
                if (currentInstanceState == InstanceState.None)
                {
                    businessInstanceInfo.TimeSumbitted = DateTime.Now;
                    businessInstanceInfo.TimeModified = DateTime.Now;
                }
                else
                {
                    if (instanceInfo != null)
                    {
                        businessInstanceInfo.TimeSumbitted = instanceInfo.TimeSumbitted;
                        businessInstanceInfo.TimeModified = instanceInfo.TimeModified;
                    }
                }
            }
            else
            {
                string tmpComments = meComment.Text.Trim();
                businessInstanceInfo = GetBusinessInstanceInfo(currentInstanceState, ParentUserId, userId, reviewerId, true, tmpComments, true, currentStep);
                if (instanceInfo != null)
                {
                    businessInstanceInfo.TimeSumbitted = instanceInfo.TimeSumbitted;
                    businessInstanceInfo.TimeModified = instanceInfo.TimeModified;
                }
            }

            /* 1. 保存数据 */
            BusinessInstanceStepInfo businessInstanceStepInfo = null;
            if (submitted)
            {
                ReviewedAction reviewedAction = GetCurrentReviewedAction(currentInstanceState, true);
                businessInstanceStepInfo = new BusinessInstanceStepInfo()
                {
                    InstanceId = InstanceId,
                    UserId = CurrentUser.Instance.UserId,
                    ReviewedAction = (byte)reviewedAction,
                    ActionVisible = true,
                    CommentReviewed = meComment.Text.Trim()
                };
            }
            InstanceSet instanceItem = businessInstanceContract.Process(businessInstanceInfo, businessInstanceStepInfo, dicRecordEntities);
            InstanceId = instanceItem.InstanceId;
            if (dicTableBusinesses.ContainsKey(currentStep))
            {
                IList<DataTableBusiness> tableBusinesses = dicTableBusinesses[currentStep];
                foreach (DataTableBusiness tableBusiness in tableBusinesses)
                {
                    if (instanceItem.RecordItems.ContainsKey(tableBusiness.FormId))
                    {
                        tableBusiness.RecordRelation = instanceItem.RecordItems[tableBusiness.FormId];
                    }
                }
            }
            dataChanged = true;
            
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentInstanceState"></param>
        /// <param name="nextStep"></param>
        /// <returns></returns>
        public ReviewedAction GetCurrentReviewedAction(InstanceState currentInstanceState, bool nextStep)
        {
            ReviewedAction reviewedAction = ReviewedAction.Sumbitted;
            switch (currentInstanceState)
            {
                case InstanceState.None:
                    if (nextStep)
                    {
                        reviewedAction = ReviewedAction.Sumbitted;
                    }
                    else
                    {
                        throw new ArgumentException("参数异常：草稿状态不能驳回。");
                    }
                    break;

                case InstanceState.InitReview:
                    if (nextStep)
                    {
                        reviewedAction = ReviewedAction.Pass;
                    }
                    else
                    {
                        reviewedAction = ReviewedAction.Reject;
                    }
                    break;

                case InstanceState.FinalReview:
                    if (nextStep)
                    {
                        reviewedAction = ReviewedAction.Pass;
                    }
                    else
                    {
                        reviewedAction = ReviewedAction.Reject;
                    }
                    break;

                default:
                    throw new ArgumentException("参数异常:实例状态枚举异常。");
            }

            return reviewedAction;
        }

        ///<summary>
        ///审核意见控件的可见性
        ///</summary>
        private void SetControlsVisible(bool visible)
        {
            //pnlStep.Visible = !visible;
            pnlReview.Visible = visible;
            hlnkReject.Visible = visible;
            //lblLastestComment.Visible = visible;
            //meLastestComment.Visible = visible;
            //lblComment.Visible = visible;
            //meComment.Visible = visible;
            //scBottom.Visible = visible;
            if (!visible)
            {
                pnlBottom.Height = 40;
            }
        }

        /// <summary>
        /// 通过当前状态获得上一个状态
        /// </summary>
        /// <param name="currentInstanceState"></param>
        /// <returns></returns>
        private InstanceState GetPreviousInstanceState(InstanceState currentInstanceState)
        {
            InstanceState previousInstanceState = InstanceState.None;

            switch (currentInstanceState)
            {
                case InstanceState.FinalReview:
                    if (CustomDataInfo.IsInitReview)
                    {
                        previousInstanceState = InstanceState.InitReview;
                    }
                    else
                    {
                        previousInstanceState = InstanceState.None;
                    }
                    break;

                case InstanceState.InitReview:
                    previousInstanceState = InstanceState.None;
                    break;

                case InstanceState.Completed:
                case InstanceState.None:
                    throw new ArgumentException("参数异常。");

            }

            return previousInstanceState;
        }

        /// <summary>
        /// 通过当前状态获得下一个状态
        /// </summary>
        /// <param name="currentInstanceState"></param>
        /// <returns></returns>
        private InstanceState GetNextInstanceState(InstanceState currentInstanceState)
        {
            InstanceState nextInstanceState = InstanceState.Completed;

            switch (currentInstanceState)
            {
                case InstanceState.None:
                    if (CustomDataInfo.IsInitReview)
                    {
                        nextInstanceState = InstanceState.InitReview;
                    }
                    else
                    {
                        if (CustomDataInfo.IsFinalReview)
                        {
                            nextInstanceState = InstanceState.FinalReview;
                        }
                    }
                    break;

                case InstanceState.InitReview:
                    if (CustomDataInfo.IsFinalReview)
                    {
                        nextInstanceState = InstanceState.FinalReview;
                    }
                    break;
            }

            return nextInstanceState;
        }

        /// <summary>
        /// 获得实例对象
        /// </summary>
        /// <param name="instanceState"></param>
        /// <param name="parentUserId"></param>
        /// <param name="userId"></param>
        /// <param name="reviwerId"></param>
        /// <param name="isDraft"></param>
        /// <param name="tmpComments"></param>
        /// <param name="audittedStatus"></param>
        /// <param name="stepIndex"></param>
        /// <returns></returns>
        private BusinessInstanceInfo GetBusinessInstanceInfo(InstanceState instanceState, decimal parentUserId, decimal userId,
            decimal reviwerId, bool isDraft, string tmpComments, bool audittedStatus, byte stepIndex)
        {            
            pageSign = pageSign | AuthorityHelper.GetShiftedValue(pageSign, stepIndex);
            BusinessInstanceInfo businessInstanceInfo = new BusinessInstanceInfo()
            {
                InstanceId = InstanceId,
                DataId = CustomDataInfo.DataId,
                UserId = userId,
                ParentUserId = parentUserId,
                InstanceName = string.Format("{0}_{1}_{2}_{3}", Text, commonUserInfo.UserName,
                commonUserInfo.UserActualName, DateTime.Now),
                InstanceState = (byte)instanceState,
                ReviewerId = reviwerId,
                IsDraft = isDraft,
                TmpComments = tmpComments,
                AudittedStatus = audittedStatus,
                IsArchived = false,
                PageSign = pageSign
            };

            return businessInstanceInfo;
        }

        /// <summary>
        /// 动态创建控件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="tabPage"></param>
        /// <param name="customFormInfos"></param>
        private void CreateControls(int index, XtraTabPage tabPage, IList<CustomFormInfo> customFormInfos)
        {
            if (tabPage.Tag != null || customFormInfos.Count == 0)
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
                            IList<CommonNode> combinedTableInfos = combinedTableContract.GetTables(customFormInfo.CombinedTableId);
                            if (!combinedDataFields.ContainsKey(customFormInfo.CombinedTableId))
                            {
                                IList<CommonNode> dataFields = combinedTableContract.GetDataFields(customFormInfo.CombinedTableId);
                                combinedDataFields.Add(customFormInfo.CombinedTableId, dataFields);
                            }
                            foreach (var combinedTableInfo in combinedTableInfos)
                            {
                                tableIds.Add(combinedTableInfo.NodeId);
                            }
                            break;

                        case FormType.Table:
                            tableIds.Add(customFormInfo.TableId);
                            break;

                        default:
                            throw new ArgumentException("不支持该表类型。");
                    }
                }
                DataAuthorityType dataAuthorityType = DataAuthorityType.Business;
                if (currentInstanceState != InstanceState.None)
                {
                    dataAuthorityType = DataAuthorityType.Auditing;
                }
                IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = customRoleContract.GetAuthorizedExtendedCustomDataFieldInfos(CurrentUser.Instance.UserId, tableIds, dataAuthorityType);
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
            dicTableBusinesses.Add(stepIndex, tableBusinesses);
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
                            DataTableBusiness dataTableBusiness = null;
                            if (customFormInfo.BusinessEnabled)
                            {
                                dataTableBusiness = new DataTableBusiness(InstanceId, customFormInfo.FormId, commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                    customEnumContract, customDepartmentContract, groupControl, meToolTip);
                            }
                            else
                            {
                                dataTableBusiness = new DataTableBusiness(customFormInfo.FormId, commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                    customEnumContract, customDepartmentContract, groupControl, meToolTip);
                            }

                            /* 加载控件 */
                            dataFiledIndex = dataTableBusiness.CreateControls(currentExtendedCustomDataFieldInfos, formShowStyleSetting, ref multiTextBoxCount, UpdateSumbittedStatus);
                            tableBusinesses.Add(dataTableBusiness);
                            /* 加载数据 */
                            Dictionary<decimal, DataTable> dataRowValues = combinedTableContract.GetDataFilledData(customFormInfo.CombinedTableId, customFormInfo.BusinessEnabled, dataFieldNameRelations, ParentUserId, InstanceId);
                            if (dataRowValues.Count > 0)
                            {
                                pageSign = pageSign | AuthorityHelper.GetShiftedValue(pageSign, currentStep);
                            }
                            dataTableBusiness.LoadDataOnCombinedTables(dataRowValues, false);
                        }
                        else
                        {
                            throw new ArgumentException("组合表的字段缓存错误。");
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
                        switch (dataTableType)
                        {
                            case DataTableType.AssistanTable:
                            case DataTableType.MasterSlaveTable:
                                showTable = true;
                                DataTableControl dataTableControl = new DataTableControl();
                                dataTableControl.Dock = DockStyle.Fill;
                                dataTableControl.FormReadOnly = FormReadOnly;
                                dataTableControl.CustomRoleContract = customRoleContract;
                                dataTableControl.TableId = customFormInfo.TableId;
                                dataTableControl.SetAuthorityHandler = (devExpressGrid, btnAdd, btnDelete) =>
                                {
                                    DataAuthorityType dataAuthorityType = DataAuthorityType.Business;
                                    if (currentInstanceState != InstanceState.None)
                                    {
                                        dataAuthorityType = DataAuthorityType.Auditing;
                                    }
                                    Int64 tableAuthority = customRoleContract.GetTableAuthority(CurrentUser.Instance.UserId, customFormInfo.TableId, dataAuthorityType);
                                    Int64 authority = 0;
                                    dataTableControl.AllowDataExported = AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Export);
                                    dataTableControl.AllowDataImported = AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Import);
                                    dataTableControl.AllowStatusSetting = (dataTableType == DataTableType.MasterSlaveTable) && AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.MasterSlave);
                                    if (AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Add))
                                    {
                                        authority |= AuthorityHelper.GetShiftedValue(0, (byte)GridViewAuthority.Add);
                                        btnAdd.Enabled = true;
                                    }
                                    else
                                    {
                                        btnAdd.Enabled = false;
                                    }
                                    if (AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Edit))
                                    {
                                        authority |= AuthorityHelper.GetShiftedValue(0, (byte)GridViewAuthority.Edit);
                                    }
                                    if (AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Delete))
                                    {
                                        authority |= AuthorityHelper.GetShiftedValue(0, (byte)GridViewAuthority.Delete);
                                        btnDelete.Enabled = true;
                                    }
                                    else
                                    {
                                        btnDelete.Enabled = false;
                                    }
                                    authority |= AuthorityHelper.GetShiftedValue(0, (byte)GridViewAuthority.Move);
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
                                    if (dataTableType == DataTableType.MasterSlaveTable)
                                    {
                                        sortingCondtions.Add(new SortingCondtion("CurrentState", CustomSorting.Descending));
                                    }
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
                                    if (dt.Rows.Count > 0)
                                    {
                                        pageSign = pageSign | AuthorityHelper.GetShiftedValue(pageSign, currentStep);
                                    }
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
                                            UserControlHelper.SetColumnDisplayText(gridColumn, dataFieldNameRelations[gridColumn.FieldName]);
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
                                        business = new DataTableBusiness(InstanceId, customFormInfo.FormId, commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                            customEnumContract, customDepartmentContract, frmDataTemplateTable.Panel, frmDataTemplateTable.MemoEditToolTip);
                                    }
                                    else
                                    {
                                        business = new DataTableBusiness(customFormInfo.FormId, commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                            customEnumContract, customDepartmentContract, frmDataTemplateTable.Panel, frmDataTemplateTable.MemoEditToolTip);
                                    }
                                    /* 加载控件 */
                                    business.CreateControls(currentCustomDataFieldInfos, formShowStyleSetting, ref multiTextBoxCount);
                                    decimal parentUserId = ParentUserId;
                                    DataFilledProperty dataFilledProperty = (DataFilledProperty)CustomDataInfo.DataFilledProperty;
                                    if (dataFilledProperty == DataFilledProperty.MultiUser)
                                    {
                                        UserNameForm frmUserName = new UserNameForm();
                                        frmUserName.GetCommonUserInfo = (userInfo) =>
                                        {
                                            parentUserId = userInfo.UserId;
                                        };
                                        frmUserName.ShowDialog();
                                    }
                                    frmDataTemplateTable.SumbittedHandler = () =>
                                    {
                                        RecordSet recordSet = business.GetRecordSet();
                                        if (!recordSet.Success)
                                        {
                                            MessageBox.Show(recordSet.Warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        InstanceState currentInstanceState = InstanceState.None;
                                        if (InstanceId > 0)
                                        {
                                            currentInstanceState = businessInstanceContract.GetInstanceState(InstanceId);
                                        }
                                        InstanceState nextInstanceState = GetNextInstanceState(currentInstanceState);
                                        if (nextInstanceState == InstanceState.InitReview || nextInstanceState == InstanceState.FinalReview)
                                        {
                                            if (cmbReviewer.Properties.Items.Count == 0)
                                            {
                                                MessageBox.Show("该单位并未设置下一步审核人，请与管理员联系。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                return;
                                            }
                                        }
                                        decimal userId = CurrentUser.Instance.UserId;
                                        decimal reviewerId = decimal.MinValue;
                                        if (InstanceId > 0)
                                        {
                                            BusinessInstanceInfo instanceInfo = businessInstanceContract.GetModelInfo(InstanceId);
                                            currentInstanceState = (InstanceState)instanceInfo.InstanceState;
                                            userId = instanceInfo.UserId;
                                            reviewerId = instanceInfo.ReviewerId;
                                        }
                                        BusinessInstanceInfo businessInstanceInfo = GetBusinessInstanceInfo(currentInstanceState, parentUserId,
                                            userId, reviewerId, false, string.Empty, true, currentStep);
                                        InstanceItem instanceItem = businessInstanceContract.Process(businessInstanceInfo, null, recordSet.RecordEntities);
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
                                        business.CreateControls(currentCustomDataFieldInfos, formShowStyleSetting, ref multiTextBoxCount);
                                        frmDataTemplateTable.SumbittedHandler = () =>
                                        {
                                            RecordSet recordSet = business.GetRecordSet();
                                            if (!recordSet.Success)
                                            {
                                                MessageBox.Show(recordSet.Warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                return;
                                            }
                                            InstanceState currentInstanceState = InstanceState.None;
                                            decimal userId = CurrentUser.Instance.UserId;
                                            decimal reviewerId = decimal.MinValue;
                                            if (InstanceId > 0)
                                            {
                                                BusinessInstanceInfo instanceInfo = businessInstanceContract.GetModelInfo(InstanceId);
                                                currentInstanceState = (InstanceState)instanceInfo.InstanceState;
                                                userId = instanceInfo.UserId;
                                                reviewerId = instanceInfo.ReviewerId;
                                            }
                                            InstanceState nextInstanceState = GetNextInstanceState(currentInstanceState);
                                            if (nextInstanceState == InstanceState.InitReview || nextInstanceState == InstanceState.FinalReview)
                                            {
                                                if (cmbReviewer.Properties.Items.Count == 0)
                                                {
                                                    MessageBox.Show("该单位并未设置下一步审核人，请与管理员联系。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    return;
                                                }
                                            }                                            
                                            BusinessInstanceInfo businessInstanceInfo = GetBusinessInstanceInfo(currentInstanceState, ParentUserId,
                                                userId, reviewerId, false, string.Empty, true, currentStep);
                                            InstanceItem instanceItem = businessInstanceContract.Process(businessInstanceInfo, null, recordSet.RecordEntities);
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
                                dataTableControl.UpdateCurretStateHandler = (recordId) =>
                                {                                    
                                    if (customFormInfo.BusinessEnabled)
                                    {
                                        dataAuditingContract.UpdateCurretStateByInstanceId(customFormInfo.TableId, recordId, InstanceId);
                                    }
                                    else
                                    {
                                        dataAuditingContract.UpdateCurretStateByUserId(customFormInfo.TableId, recordId, ParentUserId);
                                    }
                                };
                                dataTableControl.SetAuthority();
                                dataTableControl.LoadData();                                
                                groupControl.Controls.Add(dataTableControl);
                                break;

                            case DataTableType.PrimaryTable:
                                DataTableBusiness tableBusiness = null;
                                if (customFormInfo.BusinessEnabled)
                                {
                                    tableBusiness = new DataTableBusiness(InstanceId, customFormInfo.FormId, commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                        customEnumContract, customDepartmentContract, groupControl, meToolTip);
                                }
                                else
                                {
                                    tableBusiness = new DataTableBusiness(customFormInfo.FormId, commonUserInfo, FormReadOnly, customFormInfo.DataFieldSetting, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                                        customEnumContract, customDepartmentContract, groupControl, meToolTip);
                                }                                

                                /* 加载控件 */
                                dataFiledIndex = tableBusiness.CreateControls(currentCustomDataFieldInfos, formShowStyleSetting, ref multiTextBoxCount, UpdateSumbittedStatus);
                                /* 加载数据 */
                                //DataTable dataTable = GetDataTable(customFormInfo, tablePhysicalName, dataFieldNameRelations);
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
                                            pageSign = pageSign | AuthorityHelper.GetShiftedValue(pageSign, currentStep);
                                            tableBusiness.LoadDataOnTable(customFormInfo.TableId, dataTable.Rows[0], false);
                                        }
                                    }
                                }
                                else
                                {
                                    whereConditons.Add(new WhereConditon(tablePhysicalName, "UserId", "UserId", DbType.Decimal, ParentUserId,
                                            DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                                    DataTable dataTable = customTableContract.GetTableData(customFormInfo.TableId, customFormInfo.DataFieldSetting, dataFieldNameRelations, whereConditons);
                                    if (dataTable != null && dataTable.Rows.Count > 0)
                                    {
                                        pageSign = pageSign | AuthorityHelper.GetShiftedValue(pageSign, currentStep);
                                        tableBusiness.LoadDataOnTable(customFormInfo.TableId, dataTable.Rows[0], false);
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
