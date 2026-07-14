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
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.Model.UserModule;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class WorkflowModule : UserControl, ITreeNodeShow
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
        /// 数据填报契约
        /// </summary>
        public ICustomDataContract CustomDataContract
        {
            get; set;
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
        /// 角色契约
        /// </summary>
        public ICustomRoleContract CustomRoleContract
        {
            get;
            set;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbWorkflowType, typeof(WorkflowType));
            UserControlHelper.InitImageComboBoxEdit(icmbStructCategory, typeof(StructCategory));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbInstanceNameRule, typeof(WorkflowInstanceNameRule));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbProcess, typeof(DataAuditingProcess));
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowModule_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 设置数据填报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtFillData_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            DataFillItemsForm frmDataFillItems = new DataFillItemsForm();
            frmDataFillItems.ToolTip = "提示：只能选择数据填报节点。";
            frmDataFillItems.NodeSelected = delegate (CommonNode node) {
                if (node != null)
                {
                    btxtFillData.Text = CustomDataContract.GetFullName(node.NodeId);
                    btxtFillData.Tag = node;
                }
                else
                {
                    btxtFillData.Text = string.Empty;
                    btxtFillData.Tag = null;
                }
            };
            frmDataFillItems.ShowDialog();
        }
        

        /// <summary>
        /// 是否启用指南
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableGuidance_CheckedChanged(object sender, EventArgs e)
        {
            lblGuidanceTip.Visible = chkEnableGuidance.Checked;
        }

        /// <summary>
        /// 设置指南内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleGuidance_OpenLink(object sender, OpenLinkEventArgs e)
        {
            TextForm frmText = new TextForm();
            frmText.Text = "工作流指南内容";
            frmText.AttachmentCategory = AttachmentCategory.DataFilled;
            if (txtGuidance.Tag != null)
            {
                frmText.HtmlText = DataConvertionHelper.GetString(txtGuidance.Tag);
            }
            else
            {
                frmText.HtmlText = string.Empty;
            }
            frmText.TextId = TreeNodeId;
            frmText.GetRichTextAndAttachments = delegate (string richText, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
            {
                txtGuidance.Text = "[已设置]";
                txtGuidance.Tag = richText;
                hleGuidance.Tag = upLoadFileInfos;
            };
            frmText.ShowDialog();
        }


        /// <summary>
        /// 设置初复审控件属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbStructCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            StructCategory structCategory = (StructCategory)Convert.ToByte(icmbStructCategory.EditValue);
            if (structCategory == StructCategory.Auditing)
            {
                SetControlsReadOnlyProperties(false);
                SetControlsEnableProperties(true);                
            }
            else
            {
                SetControlsEnableProperties(false);
            }
        }


        /// <summary>
        /// 初审角色设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtInitReviewRole_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            SetRole(true);
        }

        /// <summary>
        /// 分配人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtAuditor_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            UserListForm frmUserList = new UserListForm();
            frmUserList.GetIdentifier = (userId) =>
            {
                if (userId > 0)
                {
                    UserAccountInfo userAccountInfo = UserAccountContract.GetModelInfo(userId);
                    if (userAccountInfo != null)
                    {
                        btxtAuditor.Text = GetHander(userAccountInfo.UserActualName, userAccountInfo.UserName);
                        btxtAuditor.Tag = userAccountInfo;
                    }
                    else
                    {
                        btxtAuditor.Text = string.Empty;
                        btxtAuditor.Tag = null;
                    }
                }
            };
            frmUserList.ShowDialog();

        }

        /// <summary>
        /// 设置终审角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtFinalRole_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            SetRole(false);
        }


        /// <summary>
        /// 流程设置变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccmbProcess_EditValueChanged(object sender, EventArgs e)
        {
            foreach (CheckedListBoxItem checkedListBoxItem in ccmbProcess.Properties.Items)
            {
                bool visible = checkedListBoxItem.CheckState == CheckState.Checked;
                EnumItem enumItem = checkedListBoxItem.Value as EnumItem;
                DataAuditingProcess dataAuditingProcess = (DataAuditingProcess)enumItem.Value;
                switch (dataAuditingProcess)
                {
                    case DataAuditingProcess.InitReview:
                        lblInitReviewRoleRequired.Visible = visible;
                        break;

                    case DataAuditingProcess.Allocation:
                        lblAuditorRequired.Visible = visible;
                        break;

                    case DataAuditingProcess.FinalReview:
                        lblFinalRoleRequired.Visible = visible;
                        break;
                }
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
                txtWorkflowCode.Text = value;
            }
            get
            {
                return txtWorkflowCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtWorkflowName.ReadOnly = readOnly;
            icmbWorkflowType.ReadOnly = readOnly;
            icmbStructCategory.ReadOnly = readOnly;
            StructCategory structCategory = (StructCategory)Convert.ToByte(icmbStructCategory.EditValue);
            if (structCategory == StructCategory.Auditing)
            {
                SetControlsEnableProperties(true);
                SetControlsReadOnlyProperties(readOnly);
            }
            else
            {
                SetControlsEnableProperties(false);
            }
            btxtFillData.ReadOnly = readOnly;
            ccmbInstanceNameRule.ReadOnly = readOnly;
            txtInstanceNameFormat.ReadOnly = readOnly;
            hleGuidance.Enabled = !readOnly;
            chkEnableGuidance.ReadOnly = readOnly;
            chkWorkflowEnabled.ReadOnly = readOnly;            
            txtNotes.ReadOnly = readOnly;
            if (!txtWorkflowName.ReadOnly)
            {
                txtWorkflowName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomWorkflowInfo customWorkflowInfo = CustomWorkflowContract.GetModelInfo(commonNode.NodeId);
            if (customWorkflowInfo != null)
            {
                txtWorkflowName.Text = customWorkflowInfo.WorkflowName;
                txtWorkflowCode.Text = customWorkflowInfo.WorkflowCode;
                icmbWorkflowType.EditValue = customWorkflowInfo.WorkflowType;
                icmbStructCategory.EditValue = customWorkflowInfo.StructCategory;
                btxtFillData.Text = CustomDataContract.GetFullName(customWorkflowInfo.DataId);
                btxtFillData.Tag = CustomDataContract.GetCommonNode(customWorkflowInfo.DataId);
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbInstanceNameRule, customWorkflowInfo.InstanceNameRule);
                txtInstanceNameFormat.Text = customWorkflowInfo.InstanceNameFormat;
                chkEnableGuidance.Checked = customWorkflowInfo.EnableGuidance;
                if (!string.IsNullOrWhiteSpace(customWorkflowInfo.Guidance))
                {
                    txtGuidance.Text = "[已设置]";
                }
                txtGuidance.Tag = customWorkflowInfo.Guidance;
                /* null 表示帮助内容的附件不变化 */
                hleGuidance.Tag = null;
                foreach (CheckedListBoxItem checkedListBoxItem in ccmbProcess.Properties.Items)
                {
                    EnumItem enumItem = checkedListBoxItem.Value as EnumItem;
                    DataAuditingProcess dataAuditingProcess = (DataAuditingProcess)enumItem.Value;
                    switch (dataAuditingProcess)
                    {
                        case DataAuditingProcess.InitReview:
                            if (customWorkflowInfo.InitReviewStatus)
                            {
                                checkedListBoxItem.CheckState = CheckState.Checked;
                            }
                            else
                            {
                                checkedListBoxItem.CheckState = CheckState.Unchecked;
                            }
                            break;

                        case DataAuditingProcess.Allocation:
                            if (customWorkflowInfo.AllowAllocation)
                            {
                                checkedListBoxItem.CheckState = CheckState.Checked;
                            }
                            else
                            {
                                checkedListBoxItem.CheckState = CheckState.Unchecked;
                            }
                            break;

                        case DataAuditingProcess.FinalReview:
                            if (customWorkflowInfo.FinalReviewStatus)
                            {
                                checkedListBoxItem.CheckState = CheckState.Checked;
                            }
                            else
                            {
                                checkedListBoxItem.CheckState = CheckState.Unchecked;
                            }
                            break;
                    }
                }
                if (!DataConvertionHelper.IsNullValue(customWorkflowInfo.RoleId))
                {
                    CommonNode node = CustomRoleContract.GetCommonNode(customWorkflowInfo.RoleId);
                    btxtInitReviewRole.Text = node.NodeName;
                    btxtInitReviewRole.Tag = node;
                }
                else
                {
                    btxtInitReviewRole.Text = string.Empty;
                    btxtInitReviewRole.Tag = null;
                }
                if (!DataConvertionHelper.IsNullValue(customWorkflowInfo.UserId))
                {
                    UserAccountInfo userAccountInfo = UserAccountContract.GetModelInfo(customWorkflowInfo.UserId);
                    btxtAuditor.Text = GetHander(userAccountInfo.UserActualName, userAccountInfo.UserName);
                    btxtAuditor.Tag = userAccountInfo;
                }
                else
                {
                    btxtAuditor.Text = string.Empty;
                    btxtAuditor.Tag = null;
                }
                if (!DataConvertionHelper.IsNullValue(customWorkflowInfo.ParentRoleId))
                {
                    CommonNode node = CustomRoleContract.GetCommonNode(customWorkflowInfo.ParentRoleId);
                    btxtFinalRole.Text = node.NodeName;
                    btxtFinalRole.Tag = node;
                }
                else
                {
                    btxtFinalRole.Text = string.Empty;
                    btxtFinalRole.Tag = null;
                }
                chkEnableManager.Checked = customWorkflowInfo.EnableManager;
                chkAllowAuditing.Checked = customWorkflowInfo.AllowAuditing;
                chkAllowAllocation.Checked = customWorkflowInfo.AllowAllocation;
                chkWorkflowEnabled.Checked = customWorkflowInfo.WorkflowEnabled;
                txtNotes.Text = customWorkflowInfo.Notes;                
            }
            else
            {
                ClearModelInfo();
            }
        }

        /// <summary>
        /// 清除界面数据
        /// </summary>
        public void ClearModelInfo()
        {
            txtWorkflowName.Text = string.Empty;
            icmbWorkflowType.SelectedIndex = 0;
            icmbStructCategory.SelectedIndex = 0;
            txtWorkflowCode.Text = string.Empty;
            btxtFillData.Text = string.Empty;
            btxtFillData.Tag = null;
            chkWorkflowEnabled.Checked = false;
            txtInstanceNameFormat.Text = string.Empty;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbProcess);
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbInstanceNameRule);
            chkEnableGuidance.Checked = false;
            txtGuidance.Text = string.Empty;
            txtGuidance.Tag = null;
            hleGuidance.Tag = null;
            btxtInitReviewRole.Text = string.Empty;
            btxtInitReviewRole.Tag = null;
            btxtAuditor.Text = string.Empty;
            btxtAuditor.Tag = null;
            btxtFinalRole.Text = string.Empty;
            btxtFinalRole.Tag = null;
            chkEnableManager.Checked = false;
            chkAllowAuditing.Checked = false;
            chkAllowAllocation.Checked = false;           
            txtNotes.Text = string.Empty;
            if (!txtWorkflowName.ReadOnly)
            {
                txtWorkflowName.Focus();
            }
            TreeNodeId = 0;
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;
            warning = string.Empty;

            CommonNode commonNode = btxtFillData.Tag as CommonNode;
            if (commonNode == null)
            {
                result = false;
                warning = "数据填报未设置。";
            }
            if (result && icmbWorkflowType.EditValue == null)
            {
                result = false;
                warning = "请选择工作流类型。";
            }
            if (result)
            {
                StructCategory structCategory = (StructCategory)Convert.ToByte(icmbStructCategory.EditValue);
                if (structCategory == StructCategory.Auditing)
                {
                    long process = UserControlHelper.GetCheckedComboBoxEditItems(ccmbProcess);
                    bool initReviewStatus = AuthorityHelper.CheckAuthority(process, (byte)DataAuditingProcess.InitReview);
                    bool allocationStatus = AuthorityHelper.CheckAuthority(process, (byte)DataAuditingProcess.Allocation);
                    bool finalReviewStatus = AuthorityHelper.CheckAuthority(process, (byte)DataAuditingProcess.FinalReview);
                    if (initReviewStatus && btxtInitReviewRole.Tag == null)
                    {
                        warning = "请设置初审角色。";
                        return false;
                    }
                    if (allocationStatus && btxtAuditor.Tag == null)
                    {
                        warning = "请设置分配人。";
                        return false;
                    }
                    if (finalReviewStatus && btxtFinalRole.Tag == null)
                    {
                        warning = "请设置终审角色。";
                        return false;
                    }
                }
                CustomWorkflowInfo customWorkflowInfo = GetModelInfo();
                result = ValidationHelper.Validate<CustomWorkflowInfo>(customWorkflowInfo, out warning);
            }

            return result;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取工作流的信息
        /// </summary>
        /// <returns></returns>
        public CustomWorkflowInfo GetModelInfo()
        {
            CommonNode commonNode = btxtFillData.Tag as CommonNode;
            if (commonNode == null)
            {
                throw new ArgumentNullException("数据填报未设置。");
            }
            decimal roleId = decimal.MinValue;
            decimal parentRoleId = decimal.MinValue;
            decimal userId = decimal.MinValue;
            bool initReviewStatus = false;
            bool allocationStatus = false;
            bool finalReviewStatus = false;
            StructCategory structCategory = (StructCategory)Convert.ToByte(icmbStructCategory.EditValue);
            if (structCategory == StructCategory.Auditing)
            {
                long process = UserControlHelper.GetCheckedComboBoxEditItems(ccmbProcess);
                initReviewStatus = AuthorityHelper.CheckAuthority(process, (byte)DataAuditingProcess.InitReview);
                allocationStatus = AuthorityHelper.CheckAuthority(process, (byte)DataAuditingProcess.Allocation);
                finalReviewStatus = AuthorityHelper.CheckAuthority(process, (byte)DataAuditingProcess.FinalReview);
                if (btxtInitReviewRole.Tag != null)
                {
                    CommonNode role = btxtInitReviewRole.Tag as CommonNode;
                    if (role != null)
                    {
                        roleId = role.NodeId;
                    }
                }
                if (btxtFinalRole.Tag != null)
                {
                    CommonNode parentRole = btxtFinalRole.Tag as CommonNode;
                    if (parentRole != null)
                    {
                        parentRoleId = parentRole.NodeId;
                    }
                }
                if (btxtAuditor.Tag != null)
                {
                    UserAccountInfo userAccountInfo = btxtAuditor.Tag as UserAccountInfo;
                    if (userAccountInfo != null)
                    {
                        userId = userAccountInfo.UserId;
                    }
                }
            }
            CustomWorkflowInfo customWorkflowInfo = new CustomWorkflowInfo()
            {
                WorkflowId = TreeNodeId,
                UserId = userId,
                DataId = commonNode.NodeId,
                RoleId = roleId,
                ParentRoleId = parentRoleId,
                WorkflowName = txtWorkflowName.Text.Trim(),
                WorkflowCode = txtWorkflowCode.Text.Trim(),
                WorkflowType = Convert.ToByte(icmbWorkflowType.EditValue),
                StructCategory = Convert.ToByte(icmbStructCategory.EditValue),
                InstanceNameRule = UserControlHelper.GetCheckedComboBoxEditItems(ccmbInstanceNameRule),
                InstanceNameFormat = txtInstanceNameFormat.Text.Trim(),
                EnableGuidance = chkEnableGuidance.Checked,
                Guidance = DataConvertionHelper.GetString(txtGuidance.Tag),
                InitReviewStatus = initReviewStatus,
                EnableManager = chkEnableManager.Checked,
                AllocationStatus = allocationStatus,
                FinalReviewStatus = finalReviewStatus,
                AllowAuditing = chkAllowAuditing.Checked,
                AllowAllocation = chkAllowAllocation.Checked,
                WorkflowEnabled = chkWorkflowEnabled.Checked,
                Notes = txtNotes.Text.Trim()
            };

            return customWorkflowInfo;
        }

        /// <summary>
        /// 获得附件
        /// </summary>
        /// <returns></returns>
        public IList<ExtendedUpLoadFileInfo> GetAttachements()
        {
            IList<ExtendedUpLoadFileInfo> upLoadFileInfos = hleGuidance.Tag as IList<ExtendedUpLoadFileInfo>;

            return upLoadFileInfos;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置控件属性
        /// </summary>
        /// <param name="readOnly"></param>
        private void SetControlsReadOnlyProperties(bool readOnly)
        {
            ccmbProcess.ReadOnly = readOnly;
            btxtInitReviewRole.ReadOnly = readOnly;
            chkEnableManager.ReadOnly = readOnly;
            btxtAuditor.ReadOnly = readOnly;
            chkAllowAuditing.ReadOnly = readOnly;
            btxtFinalRole.ReadOnly = readOnly;
            chkAllowAllocation.ReadOnly = readOnly;
        }

        /// <summary>
        /// 设置控件属性
        /// </summary>
        /// <param name="enable"></param>
        private void SetControlsEnableProperties(bool enable)
        {
            lblProcess.Enabled = enable;
            ccmbProcess.Enabled = enable;
            lblInitReviewRole.Enabled = enable;
            btxtInitReviewRole.Enabled = enable;
            chkEnableManager.Enabled = enable;
            lblAuditor.Enabled = enable;
            btxtAuditor.Enabled = enable;
            chkAllowAuditing.Enabled = enable;
            lblAuditorRequired.Enabled = enable;
            lblFinalRole.Enabled = enable;
            btxtFinalRole.Enabled = enable;
            chkAllowAllocation.Enabled = enable;
            lblFinalRoleRequired.Enabled = enable;
        }

        /// <summary>
        /// 获得处理人的信息
        /// </summary>
        /// <param name="userActualName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private string GetHander(string userActualName, string userName)
        {
            return string.Format("{0}({1})", userActualName, userName);
        }

        /// <summary>
        /// 设置角色
        /// </summary>
        private void SetRole(bool init)
        {
            RoleItemsForm frmRoleItems = new RoleItemsForm();
            frmRoleItems.Text = "角色选择";
            frmRoleItems.ToolTip = "提示：只能选择角色类型的节点。";
            frmRoleItems.NodeSelected = (node) =>
            {
                if (node != null)
                {
                    if (init)
                    {
                        btxtInitReviewRole.Text = node.NodeName;
                        btxtInitReviewRole.Tag = node;
                    }
                    else
                    {
                        btxtFinalRole.Text = node.NodeName;
                        btxtFinalRole.Tag = node;
                    }
                }
                else
                {
                    if (init)
                    {
                        btxtInitReviewRole.Text = string.Empty;
                        btxtInitReviewRole.Tag = null;
                    }
                    else
                    {
                        btxtFinalRole.Text = string.Empty;
                        btxtFinalRole.Tag = null;
                    }
                }
            };
            frmRoleItems.ShowDialog();
        }

        #endregion

    }
}
