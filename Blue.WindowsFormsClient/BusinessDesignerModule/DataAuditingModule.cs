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
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WCFContracts.SystemModule;
using Blue.Model.UserModule;
using Blue.Model.BusinessDesignerModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class DataAuditingModule : UserControl, ITreeNodeShow
    {
        #region 私有变量

        #endregion

        #region 属性

        /// <summary>
        /// 当前数据类型
        /// </summary>
        public GroupType CurrentDataAuditingType
        {
            get;
            set;
        }

        /// <summary>
        /// 组合表契约
        /// </summary>
        public ICombinedTableContract CombinedTableContract
        {
            get;
            set;
        }

        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            get;
            set;
        }

        /// <summary>
        /// 报表契约
        /// </summary>
        public ICustomReportContract CustomReportContract
        {
            get;
            set;
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
        /// 数据审核契约
        /// </summary>
        public IDataAuditingContract DataAuditingContract
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
        public DataAuditingModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbTableType, typeof(FormType));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(SystemDataField));
            //UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataAuditingProperty, typeof(DataAuditingProperty));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbSystemCondition, typeof(SystemCondition));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbProcess, typeof(DataAuditingProcess));
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataAuditingModule_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择表或者组合表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtTableName_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                switch (CurrentDataAuditingType)
                {
                    case GroupType.InfoAudited:
                        FormType tableType = (FormType)Convert.ToByte(icmbTableType.EditValue);
                        switch (tableType)
                        {
                            case FormType.Table:
                                DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
                                frmDataTableItems.Text = "表选择";
                                frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
                                frmDataTableItems.NodeSelected = delegate (CommonNode node)
                                {
                                    if (node != null)
                                    {
                                        btxtTableName.Text = CustomTableContract.GetFullName(node.NodeId);
                                        btxtTableName.Tag = node;
                                    }
                                    else
                                    {
                                        btxtTableName.Text = string.Empty;
                                        btxtTableName.Tag = null;
                                    }
                                };
                                frmDataTableItems.ShowDialog();
                                break;

                            case FormType.CombinedTable:
                                ExtendedTreeSelectedItemsForm frmTreeSelectedItems = new ExtendedTreeSelectedItemsForm();
                                frmTreeSelectedItems.Text = "组合表选择";
                                frmTreeSelectedItems.ToolTip = "提示：只能选择组合表类型的节点。";
                                frmTreeSelectedItems.TreeDropdownHandler = new CombinedTableDropdownList(CustomGroupContract, CombinedTableContract);
                                frmTreeSelectedItems.NodeSelected = delegate (CommonNode node)
                                {
                                    if (node != null)
                                    {
                                        btxtTableName.Text = CombinedTableContract.GetFullName(node.NodeId);
                                        btxtTableName.Tag = node;
                                    }
                                    else
                                    {
                                        btxtTableName.Text = string.Empty;
                                        btxtTableName.Tag = null;
                                    }
                                };
                                frmTreeSelectedItems.ShowDialog();
                                break;

                            default:
                                throw new ArgumentException("不支持该表格类型。");
                        }
                        break;

                    case GroupType.InfoUpdated:
                        ExtendedTreeSelectedItemsForm frmExtendedTreeSelectedItems = new ExtendedTreeSelectedItemsForm();
                        frmExtendedTreeSelectedItems.Text = "个人信息关联选择";
                        frmExtendedTreeSelectedItems.ToolTip = "提示：只能个人信息类型的节点。";
                        frmExtendedTreeSelectedItems.TreeDropdownHandler = new CommonTreeDropdownList(CustomGroupContract, DataAuditingContract, (byte)GroupType.InfoAudited);
                        frmExtendedTreeSelectedItems.NodeSelected = delegate (CommonNode node)
                        {
                            if (node != null)
                                    {
                                        btxtTableName.Text = DataAuditingContract.GetFullName(node.NodeId);
                                        btxtTableName.Tag = node;
                                    }
                                    else
                                    {
                                        btxtTableName.Text = string.Empty;
                                        btxtTableName.Tag = null;
                                    }
                        };
                        frmExtendedTreeSelectedItems.ShowDialog();
                        break;
                }
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 表类型切换后清空设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbTableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btxtTableName.Tag = null;
            btxtTableName.Text = string.Empty;
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
        /// 终审角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtFinalRole_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            SetRole(false);
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
                txtDataAuditingCode.Text = value;
            }
            get
            {
                return txtDataAuditingCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtDataAuditingName.ReadOnly = readOnly;
            icmbTableType.ReadOnly = readOnly;
            btxtTableName.ReadOnly = readOnly;
            btxtReport.Properties.ReadOnly = readOnly;
            ccmbDataFieldSetting.ReadOnly = readOnly;
            ccmbSystemCondition.ReadOnly = readOnly;
            ccmbProcess.Properties.ReadOnly = readOnly;
            btxtInitReviewRole.ReadOnly = readOnly;
            chkEnableManager.ReadOnly = readOnly;
            btxtAuditor.ReadOnly = readOnly;
            btxtFinalRole.ReadOnly = readOnly;
            chkAllowAuditing.ReadOnly = readOnly;
            chkAllowAllocation.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;            
            if (!txtDataAuditingName.ReadOnly)
            {
                txtDataAuditingName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            DataAuditingInfo dataAuditingInfo = DataAuditingContract.GetModelInfo(commonNode.NodeId);
            if (dataAuditingInfo != null)
            {
                txtDataAuditingName.Text = dataAuditingInfo.DataAuditingName;
                txtDataAuditingCode.Text = dataAuditingInfo.DataAuditingCode;
                icmbTableType.EditValue = dataAuditingInfo.TableType;
                switch (CurrentDataAuditingType)
                {
                    case GroupType.InfoAudited:
                        FormType formType = (FormType)dataAuditingInfo.TableType;
                        switch (formType)
                        {
                            case FormType.CombinedTable:
                                btxtTableName.Text = CombinedTableContract.GetFullName(dataAuditingInfo.CombinedTableId);
                                btxtTableName.Tag = CombinedTableContract.GetCommonNode(dataAuditingInfo.CombinedTableId);
                                break;

                            case FormType.Table:
                                btxtTableName.Text = CustomTableContract.GetFullName(dataAuditingInfo.TableId);
                                btxtTableName.Tag = CustomTableContract.GetCommonNode(dataAuditingInfo.TableId);
                                break;
                        }
                        break;

                    case GroupType.InfoUpdated:
                        if (dataAuditingInfo.ParentDataAuditingId > 0)
                        {
                            btxtTableName.Text = DataAuditingContract.GetFullName(dataAuditingInfo.ParentDataAuditingId);
                            btxtTableName.Tag = DataAuditingContract.GetCommonNode(dataAuditingInfo.ParentDataAuditingId);
                        }
                        break;
                }
                if (!DataConvertionHelper.IsNullValue(dataAuditingInfo.ReportId))
                {
                    btxtReport.Text = CustomReportContract.GetFullName(dataAuditingInfo.ReportId);
                    btxtReport.Tag = dataAuditingInfo.ReportId;
                }
                else
                {
                    btxtReport.Text = string.Empty;
                    btxtReport.Tag = null;
                }
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbDataFieldSetting, dataAuditingInfo.SystemDataFieldAuthority);
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbSystemCondition, dataAuditingInfo.SystemCondition);
                foreach (CheckedListBoxItem checkedListBoxItem in ccmbProcess.Properties.Items)
                {                    
                    EnumItem enumItem = checkedListBoxItem.Value as EnumItem;
                    DataAuditingProcess dataAuditingProcess = (DataAuditingProcess)enumItem.Value;
                    switch (dataAuditingProcess)
                    {
                        case DataAuditingProcess.InitReview:
                            if (dataAuditingInfo.InitReviewStatus)
                            {
                                checkedListBoxItem.CheckState = CheckState.Checked;
                            }
                            else
                            {
                                checkedListBoxItem.CheckState = CheckState.Unchecked;
                            }
                            break;

                        case DataAuditingProcess.Allocation:
                            if (dataAuditingInfo.AllowAllocation)
                            {
                                checkedListBoxItem.CheckState = CheckState.Checked;
                            }
                            else
                            {
                                checkedListBoxItem.CheckState = CheckState.Unchecked;
                            }
                            break;

                        case DataAuditingProcess.FinalReview:
                            if (dataAuditingInfo.FinalReviewStatus)
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
                if (!DataConvertionHelper.IsNullValue(dataAuditingInfo.RoleId))
                {
                    CommonNode node = CustomRoleContract.GetCommonNode(dataAuditingInfo.RoleId);
                    btxtInitReviewRole.Text = node.NodeName;
                    btxtInitReviewRole.Tag = node;
                }
                else
                {
                    btxtInitReviewRole.Text = string.Empty;
                    btxtInitReviewRole.Tag = null;
                }
                if (!DataConvertionHelper.IsNullValue(dataAuditingInfo.UserId))
                {
                    UserAccountInfo userAccountInfo = UserAccountContract.GetModelInfo(dataAuditingInfo.UserId);
                    btxtAuditor.Text = GetHander(userAccountInfo.UserActualName, userAccountInfo.UserName);
                    btxtAuditor.Tag = userAccountInfo;
                }
                else
                {
                    btxtAuditor.Text = string.Empty;
                    btxtAuditor.Tag = null;
                }                   
                if (!DataConvertionHelper.IsNullValue(dataAuditingInfo.ParentRoleId))
                {
                    CommonNode node = CustomRoleContract.GetCommonNode(dataAuditingInfo.ParentRoleId);
                    btxtFinalRole.Text = node.NodeName;
                    btxtFinalRole.Tag = node;
                }
                else
                {
                    btxtFinalRole.Text = string.Empty;
                    btxtFinalRole.Tag = null;
                }
                chkEnableManager.Checked = dataAuditingInfo.EnableManager;
                chkAllowAuditing.Checked = dataAuditingInfo.AllowAuditing;
                chkAllowAllocation.Checked = dataAuditingInfo.AllowAllocation;
                txtNotes.Text = dataAuditingInfo.Notes;             
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
            txtDataAuditingName.Text = string.Empty;
            txtDataAuditingCode.Text = string.Empty;
            icmbTableType.SelectedIndex = 0;
            btxtTableName.Text = string.Empty;
            btxtTableName.Tag = null;
            btxtReport.Text = string.Empty;
            btxtReport.Tag = null;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataFieldSetting);
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbSystemCondition);
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbProcess);
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
            if (!txtDataAuditingName.ReadOnly)
            {
                txtDataAuditingName.Focus();
            }
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;

            DataAuditingInfo dataAuditingInfo = GetModelInfo();
            result = ValidationHelper.Validate<DataAuditingInfo>(dataAuditingInfo, out warning);
            if (result)
            {
                if (btxtTableName.Tag == null)
                {
                    if (CurrentDataAuditingType == GroupType.InfoAudited)
                    {
                        warning = "请设置表格名称。";
                    }
                    else
                    {
                        warning = "请设置个人信息名称。";
                    }                    
                    return false;
                }
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

            return result;
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

        #region 公有方法

        /// <summary>
        /// 获取审核的信息
        /// </summary>
        /// <returns></returns>
        public DataAuditingInfo GetModelInfo()
        {
            decimal tableId = decimal.MinValue;
            decimal combinedTableId = decimal.MinValue;            
            if (btxtTableName.Tag == null)
            {
                if (CurrentDataAuditingType == GroupType.InfoAudited)
                {
                    throw new ArgumentException("请设置表格名称。");
                }
                else
                {
                    throw new ArgumentException("请设置个人信息名称。");
                }
            }
            CommonNode commonNode = btxtTableName.Tag as CommonNode;
            long process = UserControlHelper.GetCheckedComboBoxEditItems(ccmbProcess);
            bool initReviewStatus = AuthorityHelper.CheckAuthority(process, (byte)DataAuditingProcess.InitReview);
            bool allocationStatus = AuthorityHelper.CheckAuthority(process, (byte)DataAuditingProcess.Allocation);
            bool finalReviewStatus = AuthorityHelper.CheckAuthority(process, (byte)DataAuditingProcess.FinalReview);
            byte tableTypeValue = Convert.ToByte(icmbTableType.EditValue);
            decimal parentDataAuditingId = decimal.MinValue;
            switch (CurrentDataAuditingType)
            {
                case GroupType.InfoAudited:
                    FormType formType = (FormType)tableTypeValue;
                    switch (formType)
                    {
                        case FormType.CombinedTable:
                            combinedTableId = commonNode.NodeId;
                            break;

                        case FormType.Table:
                            tableId = commonNode.NodeId;
                            break;
                    }
                    break;

                case GroupType.InfoUpdated:
                    parentDataAuditingId = commonNode.NodeId;
                    break;
            }
            decimal reportId = decimal.MinValue;
            if (btxtReport.Tag != null)
            {
                reportId = Convert.ToDecimal(btxtReport.Tag);
            }
            decimal roleId = decimal.MinValue;
            if (btxtInitReviewRole.Tag != null)
            {
                CommonNode role = btxtInitReviewRole.Tag as CommonNode;
                if (role != null)
                {
                    roleId = role.NodeId;
                }
            }
            if (initReviewStatus && DataConvertionHelper.IsNullValue(roleId))
            {
                throw new ArgumentException("请设置单位初审角色。");
            }

            decimal parentRoleId = decimal.MinValue;
            if (btxtFinalRole.Tag != null)
            {
                CommonNode parentRole = btxtFinalRole.Tag as CommonNode;
                if (parentRole != null)
                {
                    parentRoleId = parentRole.NodeId;
                }
            }
            if (finalReviewStatus && DataConvertionHelper.IsNullValue(parentRoleId))
            {
                throw new ArgumentException("请设置终审角色。");
            }
            decimal userId = decimal.MinValue;
            if (btxtAuditor.Tag != null)
            {
                UserAccountInfo userAccountInfo = btxtAuditor.Tag as UserAccountInfo;
                if (userAccountInfo != null)
                {
                    userId = userAccountInfo.UserId;
                }
            }
            if (allocationStatus && DataConvertionHelper.IsNullValue(userId))
            {
                throw new ArgumentException("请设置分配人。");
            }
            DataAuditingInfo dataAuditingInfo = new DataAuditingInfo()
            {
                DataAuditingName = txtDataAuditingName.Text.Trim(),
                DataAuditingCode = txtDataAuditingCode.Text.Trim(),
                RoleId = roleId,
                ReportId = reportId,
                UserId = userId,
                TableId = tableId,
                CombinedTableId = combinedTableId,
                ParentRoleId = parentRoleId,
                ParentDataAuditingId = parentDataAuditingId,
                DataAuditingType = (byte)CurrentDataAuditingType,
                DataAuditingProperty = 0,
                SystemDataFieldAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDataFieldSetting),
                SystemCondition = UserControlHelper.GetCheckedComboBoxEditItems(ccmbSystemCondition),
                InitReviewStatus = initReviewStatus,
                EnableManager = chkEnableManager.Checked,
                AllocationStatus = allocationStatus,
                FinalReviewStatus = finalReviewStatus,
                TableType = tableTypeValue,
                AllowAuditing = chkAllowAuditing.Checked,
                AllowAllocation = chkAllowAllocation.Checked,
                Notes = txtNotes.Text.Trim()
            };

            return dataAuditingInfo;
        }

        /// <summary>
        /// 设置标签名称
        /// </summary>
        public void SetLabelTitle()
        {
            switch (CurrentDataAuditingType)
            {
                case GroupType.InfoAudited:
                    lblDataAuditingName.Text = "信息名称：";
                    lblDataAuditingCode.Text = "信息编码：";
                    lblTableName.Text = "表格名称";
                    icmbTableType.Visible = true;
                    txtDataAuditingCode.Width = 170;
                    break;

                case GroupType.InfoUpdated:
                    lblDataAuditingName.Text = "变更名称：";
                    lblDataAuditingCode.Text = "变更信息：";
                    lblTableName.Text = "个人信息：";
                    icmbTableType.Visible = false;
                    txtDataAuditingCode.Width = 330;
                    break;

                default:
                    throw new ArgumentException("不支持该属性");
            }
        }

        #endregion

        /// <summary>
        /// 设置报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtReport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            ReportItemsForm frmReportItems = new ReportItemsForm();
            frmReportItems.Text = "选择报表";
            frmReportItems.ToolTip = "提示：只能选择报表节点。";
            frmReportItems.ReportCategory = ReportCategory.Query;
            frmReportItems.NodeSelected = delegate (CommonNode node)
            {
                if (node != null)
                {
                    btxtReport.Text = CustomReportContract.GetFullName(node.NodeId);
                    btxtReport.Tag = node.NodeId;
                }
                else
                {
                    btxtReport.Text = string.Empty;
                    btxtReport.Tag = null;
                }
            };
            frmReportItems.ShowDialog();
        }
    }
}
