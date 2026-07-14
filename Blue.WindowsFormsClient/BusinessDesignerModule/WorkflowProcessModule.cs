using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.Model.UserModule;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class WorkflowProcessModule : UserControl, ITreeNodeShow
    {
        #region 私有变量
        

        #endregion

        #region 属性

        /// <summary>
        /// 工作流契约
        /// </summary>
        public ICustomWorkflowContract CustomWorkflowContract
        {
            get; set;
        }

        /// <summary>
        /// 工作流步骤契约
        /// </summary>
        public ICustomWorkflowProcessContract CustomWorkflowProcessContract
        {
            get; set;
        }

        /// <summary>
        /// 角色契约
        /// </summary>
        public ICustomRoleContract CustomRoleContract
        {
            get;set;
        }

        /// <summary>
        /// 用户契约
        /// </summary>
        public IUserAccountContract UserAccountContract
        {
            get;
            set;
        }

        /// <summary>
        /// 数据表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            get; set;
        }

        /// <summary>
        /// 分组
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 查询契约
        /// </summary>
        public ICustomQueyContract CustomQueyContract
        {
            get; set;
        }

        /// <summary>
        /// 视图契约
        /// </summary>
        public ICustomViewContract CustomViewContract
        {
            get;
            set;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowProcessModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbWorkflowProcessCategory, typeof(WorkflowProcessCategory));
            UserControlHelper.InitImageComboBoxEdit(icmbNextProcessType, typeof(WorkflowProcessType));
            UserControlHelper.InitImageComboBoxEdit(icmbHandlerType, typeof(WorkflowHandlerType));
            UserControlHelper.InitImageComboBoxEdit(icmbHandlerMode, typeof(WorkflowHandlerMode));            
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbProcessSetting, typeof(WorkflowProcessSetting));
            UserControlHelper.InitImageComboBoxEdit(icmbConditionType, typeof(ConditionType));
            UserControlHelper.InitImageComboBoxEdit(icmbDataInputMode, typeof(DataInputMode));
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowProcessModule_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 处理人类型设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleHandlerType_OpenLink(object sender, OpenLinkEventArgs e)
        {
            try
            {
                WorkflowProcessType workflowProcessType = (WorkflowProcessType)Convert.ToByte(icmbNextProcessType.EditValue);
                if (workflowProcessType == WorkflowProcessType.DynamicBranchInDeps)
                {
                    SetRole();
                }
                else
                {
                    byte handlerType = Convert.ToByte(icmbHandlerType.EditValue);
                    WorkflowHandlerType workflowHandlerType = (WorkflowHandlerType)handlerType;
                    switch (workflowHandlerType)
                    {
                        case WorkflowHandlerType.Role:
                        case WorkflowHandlerType.DepRole:
                        case WorkflowHandlerType.ManagementRole:
                            SetRole();
                            break;

                        case WorkflowHandlerType.User:
                            UserListForm frmUserList = new UserListForm();
                            frmUserList.GetIdentifier = (userId) =>
                            {
                                if (userId > 0)
                                {
                                    UserAccountInfo userAccountInfo = UserAccountContract.GetModelInfo(userId);
                                    if (userAccountInfo != null)
                                    {
                                        txtHandlerType.Text = GetHander(userAccountInfo.UserActualName, userAccountInfo.UserName);
                                        txtHandlerType.Tag = userAccountInfo;
                                    }
                                }
                            };
                            frmUserList.ShowDialog();
                            break;
                    }
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
        /// 是否启用帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableHelp_CheckedChanged(object sender, EventArgs e)
        {
            lblGuidanceTip.Visible = chkEnableHelp.Checked;
        }

        /// <summary>
        /// 设置帮助内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleHelp_OpenLink(object sender, OpenLinkEventArgs e)
        {
            TextForm frmText = new TextForm();
            frmText.Text = "帮助";
            frmText.AttachmentCategory = AttachmentCategory.WorkflowProcess;
            if (txtHelpContent.Tag != null)
            {
                frmText.HtmlText = DataConvertionHelper.GetString(txtHelpContent.Tag);
            }
            else
            {
                frmText.HtmlText = string.Empty;
            }
            frmText.TextId = TreeNodeId;
            frmText.GetRichTextAndAttachments = (richText, upLoadFileInfos) =>
            {
                txtHelpContent.Text = "[已设置]";
                txtHelpContent.Tag = richText;
                hleHelp.Tag = upLoadFileInfos;
            };
            frmText.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbHandlerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte handlerType = Convert.ToByte(icmbHandlerType.EditValue);
            WorkflowHandlerType workflowHandlerType = (WorkflowHandlerType)handlerType;
            if (workflowHandlerType == WorkflowHandlerType.Sponsor)
            {
                lblHandlerTypeRequired.Visible = false;
                hleHandlerType.Enabled = false;
                lblHandler.Enabled = false;
            }
            else
            {
                lblHandlerTypeRequired.Visible = true;
                hleHandlerType.Enabled = true;
                lblHandler.Enabled = true;
            }
            if (workflowHandlerType == WorkflowHandlerType.Sponsor || workflowHandlerType == WorkflowHandlerType.User)
            {
                icmbHandlerMode.SelectedIndex = 0;
                icmbHandlerMode.ReadOnly = true;
                UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbProcessSetting);
                ccmbProcessSetting.ReadOnly = true;
                lblProcessSetting.Enabled = false;
                lblLeftDays.Enabled = false;
                txtLeftDays.ReadOnly = true;
                txtLeftDays.Text = string.Empty;
                lblEnableGuidance.Enabled = false;
            }
            else
            {
                icmbHandlerMode.ReadOnly = false;
                ccmbProcessSetting.ReadOnly = false;
                lblProcessSetting.Enabled = true;
                lblLeftDays.Enabled = true;
                txtLeftDays.ReadOnly = false;
                lblEnableGuidance.Enabled = true;
            }
            txtHandlerType.Text = string.Empty;
            txtHandlerType.Tag = null;
        }

        /// <summary>
        /// 节点类型变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbProcessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            WorkflowProcessType workflowProcessType = (WorkflowProcessType)Convert.ToByte(icmbNextProcessType.EditValue);
            if (workflowProcessType == WorkflowProcessType.DynamicBranchInDeps || workflowProcessType == WorkflowProcessType.DynamicBranch)
            {
                icmbHandlerType.Visible = false;
                icmbHandlerMode.Visible = false;
                lblHandlerType.Text = "处理关联：";
                txtDependency.Visible = true;
                hleDependency.Visible = true;
                icmbConditionType.Visible = true;
                hleHandlerType.Enabled = true;
                lblHandler.Enabled = true;
            }
            else
            {
                icmbHandlerType.Visible = true;
                icmbHandlerMode.Visible = true;
                if (workflowProcessType == WorkflowProcessType.DynamicBranchBetweenDeps)
                {
                    icmbHandlerType.Enabled = false;
                    icmbHandlerType.EditValue = (byte)WorkflowHandlerType.Role;
                }
                else
                {
                    icmbHandlerType.Enabled = true;
                    icmbHandlerType.SelectedIndex = 0;
                }
                icmbHandlerMode.Enabled = true;
                lblHandlerType.Text = "处理信息：";
                txtDependency.Visible = false;
                hleDependency.Visible = false;
                icmbConditionType.Visible = false;
                icmbHandlerType.ReadOnly = false;
            }
        }

        /// <summary>
        /// 设置关联系统表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleDependency_OpenLink(object sender, OpenLinkEventArgs e)
        {
            WorkflowProcessType workflowProcessType = (WorkflowProcessType)Convert.ToByte(icmbNextProcessType.EditValue);
            if (workflowProcessType == WorkflowProcessType.DynamicBranch || workflowProcessType == WorkflowProcessType.DynamicBranchInDeps ||
                workflowProcessType == WorkflowProcessType.DynamicBranchBetweenDeps)
            {
                try
                {
                    ConditionType conditionType = (ConditionType)Convert.ToByte(icmbConditionType.EditValue);
                    switch (conditionType)
                    {
                        case ConditionType.Table:
                            DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
                            frmDataTableItems.TableFilter = TableFilter.System;
                            frmDataTableItems.Text = "请选择系统表";
                            frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
                            frmDataTableItems.NodeSelected = delegate (CommonNode node)
                            {
                                if (node != null)
                                {
                                    txtDependency.Text = CustomTableContract.GetFullName(node.NodeId);
                                    txtDependency.Tag = node;
                                }
                                else
                                {
                                    txtDependency.Text = string.Empty;
                                    txtDependency.Tag = null;
                                }
                            };
                            frmDataTableItems.ShowDialog();
                            break;

                        case ConditionType.View:
                            ViewItemsForm frmViewItems = new ViewItemsForm();
                            frmViewItems.Text = "请选择查询视图";
                            frmViewItems.ToolTip = "提示：只能选择视图类型的节点。";
                            frmViewItems.NodeSelected = delegate (CommonNode node)
                            {
                                if (node != null)
                                {
                                    txtDependency.Text = CustomViewContract.GetFullName(node.NodeId);
                                    txtDependency.Tag = node;
                                }
                                else
                                {
                                    txtDependency.Text = string.Empty;
                                    txtDependency.Tag = null;
                                }
                            };
                            frmViewItems.ShowDialog();
                            break;
                    }
                }
                catch (Exception exception)
                {
                    //记录日志, 不抛出异常, 包装异常
                    WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                }
            }
        }

        /// <summary>
        /// 类型变动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDependency.Text = string.Empty;
            txtDependency.Tag = null;
        }

        /// <summary>
        /// 流程类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmWorkflowProcessCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            WorkflowProcessCategory workflowProcessCategory = (WorkflowProcessCategory)Convert.ToByte(icmbWorkflowProcessCategory.EditValue);
            switch (workflowProcessCategory)
            {
                case WorkflowProcessCategory.Begin:
                    icmbHandlerType.SelectedIndex = 0;
                    icmbHandlerType.Properties.ReadOnly = true;
                    icmbNextProcessType.Properties.ReadOnly = false;
                    break;

                case WorkflowProcessCategory.Middle:
                    icmbHandlerType.Properties.ReadOnly = false;
                    icmbNextProcessType.Properties.ReadOnly = false;
                    break;

                case WorkflowProcessCategory.End:
                    icmbNextProcessType.SelectedIndex = 0;                    
                    icmbHandlerType.Properties.ReadOnly = false;
                    icmbNextProcessType.Properties.ReadOnly = true;
                    break;
            }
        }

        #endregion

        #region 实现 ITreeNodeShow 接口

        /// <summary>
        /// 节点编号
        /// </summary>
        public decimal TreeNodeId
        {
            get;
            set;
        }

        /// <summary>
        /// 默认编码
        /// </summary>
        public string DefaultCode
        {
            set
            {
                txtProcessCode.Text = value;
            }
            get
            {
                return txtProcessCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtProcessName.ReadOnly = readOnly;
            icmbWorkflowProcessCategory.ReadOnly = readOnly;
            icmbNextProcessType.ReadOnly = readOnly;
            icmbDataInputMode.ReadOnly = readOnly;
            WorkflowProcessCategory workflowProcessCategory = (WorkflowProcessCategory)Convert.ToByte(icmbWorkflowProcessCategory.EditValue);
            if (workflowProcessCategory == WorkflowProcessCategory.Begin)
            {
                icmbHandlerType.Properties.ReadOnly = true;
            }
            else
            {
                icmbHandlerType.ReadOnly = readOnly;
            }
            WorkflowHandlerType workflowHandlerType = (WorkflowHandlerType)Convert.ToByte(icmbHandlerType.EditValue);
            switch (workflowHandlerType)
            {
                case WorkflowHandlerType.Sponsor:
                    lblHandlerTypeRequired.Visible = false;
                    hleHandlerType.Enabled = false;
                    lblHandler.Enabled = false;

                    icmbHandlerMode.SelectedIndex = 0;
                    icmbHandlerMode.ReadOnly = true;
                    UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbProcessSetting);
                    ccmbProcessSetting.ReadOnly = true;
                    lblProcessSetting.Enabled = false;
                    lblLeftDays.Enabled = false;
                    txtLeftDays.ReadOnly = true;
                    txtLeftDays.Text = string.Empty;
                    lblEnableGuidance.Enabled = false;
                    break;

                //case WorkflowHandlerType.CustomUser:
                //    lblHandlerTypeRequired.Visible = false;
                //    hleHandlerType.Enabled = false;
                //    lblHandler.Enabled = false;

                //    icmbHandlerMode.ReadOnly = readOnly;
                //    ccmbProcessSetting.ReadOnly = readOnly;
                //    lblProcessSetting.Enabled = true;
                //    lblLeftDays.Enabled = true; ;
                //    txtLeftDays.ReadOnly = readOnly;
                //    lblEnableGuidance.Enabled = true;

                //    icmbHandlerMode.SelectedIndex = 0;
                //    icmbHandlerMode.ReadOnly = true;
                //    break;

                case WorkflowHandlerType.User:
                    lblHandlerTypeRequired.Visible = true;
                    hleHandlerType.Enabled = !readOnly;
                    lblHandler.Enabled = true;

                    icmbHandlerMode.ReadOnly = readOnly;
                    ccmbProcessSetting.ReadOnly = readOnly;
                    lblProcessSetting.Enabled = true;
                    lblLeftDays.Enabled = true; ;
                    txtLeftDays.ReadOnly = readOnly;
                    lblEnableGuidance.Enabled = true;

                    icmbHandlerMode.SelectedIndex = 0;
                    icmbHandlerMode.ReadOnly = true;
                    break;

                default:
                    lblHandlerTypeRequired.Visible = true;
                    hleHandlerType.Enabled = !readOnly;
                    lblHandler.Enabled = true;

                    icmbHandlerMode.ReadOnly = readOnly;
                    ccmbProcessSetting.ReadOnly = readOnly;
                    lblProcessSetting.Enabled = true;
                    lblLeftDays.Enabled = true; ;
                    txtLeftDays.ReadOnly = readOnly;
                    lblEnableGuidance.Enabled = true;

                    icmbHandlerMode.ReadOnly = readOnly;
                    ccmbProcessSetting.ReadOnly = readOnly;
                    break;

            }
            icmbConditionType.ReadOnly = readOnly;
            hleHelp.Enabled = !readOnly;
            chkEnableHelp.ReadOnly = readOnly;
            txtTooltip.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!txtProcessName.ReadOnly)
            {
                txtProcessName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomWorkflowProcessInfo customWorkflowProcessInfo = CustomWorkflowProcessContract.GetModelInfo(commonNode.NodeId);
            if (customWorkflowProcessInfo != null)
            {
                txtProcessName.Text = customWorkflowProcessInfo.ProcessName;
                txtProcessCode.Text = customWorkflowProcessInfo.ProcessCode;
                icmbWorkflowProcessCategory.EditValue = customWorkflowProcessInfo.ProcessCategory;
                icmbNextProcessType.EditValue = customWorkflowProcessInfo.ProcessType;
                icmbHandlerType.EditValue = customWorkflowProcessInfo.HandlerType;
                icmbHandlerMode.EditValue = customWorkflowProcessInfo.HandlerMode;
                
                WorkflowProcessType workflowProcessType = (WorkflowProcessType)Convert.ToByte(icmbNextProcessType.EditValue);
                if (workflowProcessType == WorkflowProcessType.DynamicBranchInDeps)
                {
                    CommonNode roleCommonNode = CustomRoleContract.GetCommonNode(customWorkflowProcessInfo.RoleId);
                    txtHandlerType.Text = roleCommonNode.NodeName;
                    txtHandlerType.Tag = roleCommonNode;
                    if (workflowProcessType == WorkflowProcessType.DynamicBranchInDeps)
                    {
                        txtDependency.Text = CustomTableContract.GetFullName(customWorkflowProcessInfo.TableId);
                        txtDependency.Tag = CustomTableContract.GetCommonNode(customWorkflowProcessInfo.TableId);
                        icmbConditionType.EditValue = customWorkflowProcessInfo.ConditionType;
                        ConditionType conditionType = (ConditionType)customWorkflowProcessInfo.ConditionType;
                        switch (conditionType)
                        {
                            case ConditionType.Table:
                                txtDependency.Text = CustomTableContract.GetFullName(commonNode.NodeId);
                                txtDependency.Tag = CustomTableContract.GetCommonNode(commonNode.NodeId);

                                break;

                            case ConditionType.View:
                                txtDependency.Text = CustomViewContract.GetFullName(commonNode.NodeId);
                                txtDependency.Tag = CustomViewContract.GetCommonNode(commonNode.NodeId);
                                break;
                        }
                    }
                }
                else
                {
                    WorkflowHandlerType workflowHandlerType = (WorkflowHandlerType)customWorkflowProcessInfo.HandlerType;
                    switch (workflowHandlerType)
                    {
                        case WorkflowHandlerType.DepRole:
                        case WorkflowHandlerType.Role:
                        case WorkflowHandlerType.ManagementRole:
                            CommonNode roleCommonNode = CustomRoleContract.GetCommonNode(customWorkflowProcessInfo.RoleId);
                            txtHandlerType.Text = roleCommonNode.NodeName;
                            txtHandlerType.Tag = roleCommonNode;
                            break;

                        case WorkflowHandlerType.User:
                            UserAccountInfo userAccountInfo = UserAccountContract.GetModelInfo(customWorkflowProcessInfo.UserId);
                            txtHandlerType.Text = GetHander(userAccountInfo.UserActualName, userAccountInfo.UserName);
                            txtHandlerType.Tag = userAccountInfo;
                            break;

                        default:
                            txtHandlerType.Text = string.Empty;
                            txtHandlerType.Tag = null;
                            break;
                    }
                    txtDependency.Text = string.Empty;
                    txtDependency.Tag = null;
                }           
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbProcessSetting, customWorkflowProcessInfo.ProcessSetting);
                chkEnableHelp.Checked = customWorkflowProcessInfo.EnableHelp;
                if (!string.IsNullOrWhiteSpace(customWorkflowProcessInfo.HelpContent))
                {
                    txtHelpContent.Text = "[已设置]";
                }
                txtHelpContent.Tag = customWorkflowProcessInfo.HelpContent;
                /* null 表示帮助内容的附件不变化 */
                hleHelp.Tag = null;
                if (customWorkflowProcessInfo.LeftDays >= 0)
                {
                    txtLeftDays.Text = customWorkflowProcessInfo.LeftDays.ToString();
                }
                txtTooltip.Text = customWorkflowProcessInfo.ToolTip;
                txtNotes.Text = customWorkflowProcessInfo.Notes;                
            }
            else
            {
                ClearModelInfo();
            }
        }

        /// <summary>
        /// 获得处理人的信息
        /// </summary>
        /// <param name="userActualName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetHander(string userActualName, string userName)
        {
            return string.Format("{0}({1})", userActualName, userName);
        }

        /// <summary>
        /// 清除界面数据
        /// </summary>
        public void ClearModelInfo()
        {
            txtProcessName.Text = string.Empty;
            txtProcessCode.Text = string.Empty;
            icmbWorkflowProcessCategory.SelectedIndex = 0;
            icmbNextProcessType.SelectedIndex = 0;
            icmbHandlerType.SelectedIndex = 0;
            icmbHandlerMode.SelectedIndex = 0;
            icmbDataInputMode.SelectedIndex = 0;
            txtHandlerType.Text = string.Empty;
            txtHandlerType.Tag = null;
            icmbConditionType.SelectedIndex = 0;
            txtDependency.Text = string.Empty;
            txtDependency.Tag = null;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbProcessSetting);
            chkEnableHelp.Checked = false;
            txtHelpContent.Text = string.Empty;
            txtHelpContent.Tag = null;
            hleHelp.Tag = null;
            txtTooltip.Text = string.Empty;
            txtNotes.Text = string.Empty;
            if (!txtProcessName.ReadOnly)
            {
                txtProcessName.Focus();
            }
            TreeNodeId = 0;
        }

        /// <summary>
        /// 校验工作流步骤对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;
            warning = string.Empty;

            WorkflowProcessType workflowProcessType = (WorkflowProcessType)Convert.ToByte(icmbNextProcessType.EditValue);
            byte handlerType = Convert.ToByte(icmbHandlerType.EditValue);
            WorkflowHandlerType workflowHandlerType = (WorkflowHandlerType)handlerType;
            if (((workflowHandlerType == WorkflowHandlerType.Role) || (workflowHandlerType == WorkflowHandlerType.ManagementRole)
                || (workflowHandlerType == WorkflowHandlerType.DepRole)) && txtHandlerType.Tag == null)
            {
                result = false;
                warning = "处理信息中的处理人未设置。";
            }
            if (result && workflowProcessType == WorkflowProcessType.DynamicBranchInDeps && txtDependency.Tag == null)
            {
                result = false;
                warning = "关联的系统表未设置。";
            }
            CustomWorkflowProcessInfo customWorkflowProcessInfo = GetModelInfo();
            result = ValidationHelper.Validate<CustomWorkflowProcessInfo>(customWorkflowProcessInfo, out warning);
            if (result && chkEnableHelp.Checked && txtHelpContent.Tag == null)
            {
                result = false;
                warning = "帮助内容未设置。";
            }

            return result;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取工作流的信息
        /// </summary>
        /// <returns></returns>
        public CustomWorkflowProcessInfo GetModelInfo()
        {
            byte processType = Convert.ToByte(icmbNextProcessType.EditValue);
            WorkflowProcessType workflowProcessType = (WorkflowProcessType)processType;
            byte workflowProcessCategory = Convert.ToByte(icmbWorkflowProcessCategory.EditValue);
            byte handlerType = Convert.ToByte(icmbHandlerType.EditValue);
            byte dataInputMode = Convert.ToByte(icmbDataInputMode.EditValue);
            WorkflowHandlerType workflowHandlerType = (WorkflowHandlerType)handlerType;
            if (((workflowHandlerType == WorkflowHandlerType.Role) || (workflowHandlerType == WorkflowHandlerType.ManagementRole)
                || (workflowHandlerType == WorkflowHandlerType.DepRole)) && txtHandlerType.Tag == null)
            {
                throw new ArgumentNullException("处理信息中的处理人未设置。");
            }
            decimal userId = decimal.MinValue;
            decimal roleId = decimal.MinValue;
            decimal tableId = decimal.MinValue;
            decimal viewId = decimal.MinValue;

            byte conditionTypeValue = Convert.ToByte(icmbConditionType.EditValue);
            ConditionType conditionType = (ConditionType)conditionTypeValue;
            if (workflowProcessType == WorkflowProcessType.DynamicBranchInDeps)
            {
                CommonNode roleCommonNode = txtHandlerType.Tag as CommonNode;
                roleId = roleCommonNode.NodeId;
                if (workflowProcessType == WorkflowProcessType.DynamicBranchInDeps)
                {
                    CommonNode commonNode = txtDependency.Tag as CommonNode;
                    switch (conditionType)
                    {
                        case ConditionType.Table:
                            tableId = commonNode.NodeId;
                            break;

                        case ConditionType.View:
                            viewId = commonNode.NodeId;
                            break;
                    }
                }
            }
            else
            {
                switch (workflowHandlerType)
                {
                    case WorkflowHandlerType.DepRole:
                    case WorkflowHandlerType.Role:
                    case WorkflowHandlerType.ManagementRole:
                        CommonNode roleCommonNode = txtHandlerType.Tag as CommonNode;
                        roleId = roleCommonNode.NodeId;
                        break;

                    case WorkflowHandlerType.User:
                        UserAccountInfo userAccountInfo = txtHandlerType.Tag as UserAccountInfo;
                        userId = userAccountInfo.UserId;
                        break;
                }
            }

            int leftDays = DataConvertionHelper.GetConvertedInt(txtLeftDays.Text.Trim());            
            CustomWorkflowProcessInfo customWorkflowProcessInfo = new CustomWorkflowProcessInfo()
            {
                ProcessId = TreeNodeId,
                UserId = userId,
                RoleId = roleId,
                TableId =  tableId,
                ViewId = viewId,
                ProcessName = txtProcessName.Text.Trim(),
                ProcessCode = txtProcessCode.Text.Trim(),
                DataInputMode = dataInputMode,
                ProcessCategory = workflowProcessCategory,
                ProcessType = processType,                
                HandlerType = handlerType,
                HandlerMode = Convert.ToByte(icmbHandlerMode.EditValue),
                ConditionType = conditionTypeValue,
                ProcessSetting = UserControlHelper.GetCheckedComboBoxEditItems(ccmbProcessSetting),                
                EnableHelp = chkEnableHelp.Checked,
                HelpContent = DataConvertionHelper.GetString(txtHelpContent.Tag),
                LeftDays = leftDays,
                ToolTip = txtTooltip.Text.Trim(),
                Notes = txtNotes.Text.Trim()
            };

            return customWorkflowProcessInfo;
        }

        /// <summary>
        /// 获得附件
        /// </summary>
        /// <returns></returns>
        public IList<ExtendedUpLoadFileInfo> GetAttachements()
        {
            IList<ExtendedUpLoadFileInfo> upLoadFileInfos = hleHelp.Tag as IList<ExtendedUpLoadFileInfo>;

            return upLoadFileInfos;
        }

        #endregion               

        #region 私有方法

        /// <summary>
        /// 设置角色
        /// </summary>
        private void SetRole()
        {
            RoleItemsForm frmRoleItems = new RoleItemsForm();
            frmRoleItems.Text = "角色选择";
            frmRoleItems.ToolTip = "提示：只能选择角色类型的节点。";
            frmRoleItems.NodeSelected = (node) =>
            {
                if (node != null)
                {
                    txtHandlerType.Text = node.NodeName;
                    txtHandlerType.Tag = node;
                }
                else
                {
                    txtHandlerType.Text = string.Empty;
                    txtHandlerType.Tag = null;
                }
            };
            frmRoleItems.ShowDialog();
        }

        #endregion        
    }
}
