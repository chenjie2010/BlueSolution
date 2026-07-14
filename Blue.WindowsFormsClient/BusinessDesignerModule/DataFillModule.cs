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
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class DataFillModule : UserControl, ITreeNodeShow
    {
        #region 私有常量

        /// <summary>
        /// 窗口最大高度
        /// </summary>
        private const int MAX_FORM_HEIGHT = 800;

        /// <summary>
        /// 窗口最小高度
        /// </summary>
        private const int MIN_FORM_HEIGHT = 300;

        #endregion

        #region 属性

        /// <summary>
        /// 数据填报契约
        /// </summary>
        public ICustomDataContract CustomDataContract
        {
            get; set;
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
        /// 数据表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
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

        /// <summary>
        /// 角色契约
        /// </summary>
        public ICustomRoleContract CustomRoleContract
        {
            get;
            set;
        }

        /// <summary>
        /// 字段契约
        /// </summary>
        public ICustomDataFieldContract CustomDataFieldContract
        {
            get; set;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataFillModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            btxtInputReport.ReadOnly = true;
            UserControlHelper.InitImageComboBoxEdit(icmbConditionType, typeof(ConditionType));
            UserControlHelper.InitImageComboBoxEdit(icmbDataFilledType, typeof(DataFilledType));
            UserControlHelper.InitImageComboBoxEdit(icmbDataInputMode, typeof(DataInputMode));            
            UserControlHelper.InitImageComboBoxEdit(icmbDataShowMode, typeof(DataShowMode));
            UserControlHelper.InitImageComboBoxEdit(icmbDataFilledProperty, typeof(DataFilledProperty));
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFillModule_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 关联复表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtInputReport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            ReportItemsForm frmReportItems = new ReportItemsForm();
            frmReportItems.ReportCategory = ReportCategory.Input;
            frmReportItems.Text = "选择复表";
            frmReportItems.ToolTip = "提示：只能选择复表节点。";
            frmReportItems.NodeSelected = delegate (CommonNode node)
            {
                if (node != null)
                {
                    btxtInputReport.Text = CustomReportContract.GetFullName(node.NodeId);
                    btxtInputReport.Tag = node.NodeId;
                }
                else
                {
                    btxtInputReport.Text = string.Empty;
                    btxtInputReport.Tag = null;
                }
            };
            frmReportItems.ShowDialog();

        }

        /// <summary>
        /// 下载报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtReport_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            ReportItemsForm frmReportItems = new ReportItemsForm();
            frmReportItems.ReportCategory = ReportCategory.Query;
            frmReportItems.Text = "选择报表";
            frmReportItems.ToolTip = "提示：只能选择报表节点。";
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

        /// <summary>
        /// 设置指南
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleGuidance_OpenLink(object sender, OpenLinkEventArgs e)
        {
            ControlContentHelper.ShowDialog(TreeNodeId, AttachmentCategory.DataFilled, "数据填报指南内容", txtGuidance, hleGuidance);
            //TextForm frmText = new TextForm();
            //frmText.Text = "数据填报指南内容";
            //frmText.AttachmentCategory = AttachmentCategory.DataFilled;
            //if (txtGuidance.Tag != null)
            //{
            //    frmText.HtmlText = DataConvertionHelper.GetString(txtGuidance.Tag);
            //}
            //else
            //{
            //    frmText.HtmlText = string.Empty;
            //}
            //frmText.TextId = TreeNodeId;
            //frmText.GetRichTextAndAttachments = delegate (string richText, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
            //{
            //    txtGuidance.Text = "[已设置]";
            //    txtGuidance.Tag = richText;
            //    hleGuidance.Tag = upLoadFileInfos;
            //};
            //frmText.ShowDialog();
        }

        /// <summary>
        /// 条件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleConditionContent_OpenLink(object sender, OpenLinkEventArgs e)
        {
            ControlContentHelper.ShowDialog(TreeNodeId, AttachmentCategory.DataFilledCondition, "数据填报条件内容", txtConditionContent, hleConditionContent);
        }

        /// <summary>
        /// 是否启用指南
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableGuidance_CheckedChanged(object sender, EventArgs e)
        {
            lblGuidanceTip.Visible = chkEnableGuidance.Checked;
            hleGuidance.Enabled = chkEnableGuidance.Checked;
        }

        /// <summary>
        /// 设置关联表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleTableSetting_OpenLink(object sender, OpenLinkEventArgs e)
        {
            try
            {
                ConditionType conditionType = (ConditionType)Convert.ToByte(icmbConditionType.EditValue);
                switch (conditionType)
                {
                    case ConditionType.Table:
                        DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
                        frmDataTableItems.TableFilter = TableFilter.System;
                        frmDataTableItems.Text = "条件表选择";
                        frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
                        frmDataTableItems.NodeSelected = delegate (CommonNode node)
                        {
                            if (node != null)
                            {
                                int count = CustomDataFieldContract.GetDataFieldCountUnderSetting(node.NodeId, (byte)DataFieldSetting.RoleUnderCondition);
                                if (count > 0)
                                {
                                    txtDependency.Text = CustomTableContract.GetFullName(node.NodeId);
                                    txtDependency.Tag = node;
                                }
                                else
                                {
                                    txtDependency.Text = string.Empty;
                                    txtDependency.Tag = null;
                                    MessageBox.Show(string.Format("该表[{0}]不包含角色条件字段，请先设置。", node.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
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
                        frmViewItems.Text = "条件视图选择";
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

        /// <summary>
        /// 关联类型更改，清空内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbTableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDependency.Text = string.Empty;
            txtDependency.Tag = null;
        }

        /// <summary>
        /// 启用条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkConditionEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConditionEnabled.Checked)
            {
                icmbConditionType.ReadOnly = false;
                hleConditionContent.Enabled = true;
                hleTableSetting.Enabled = true;
                lblTableType.Enabled = true;
                lblFormCodeRequired.Visible = true;
            }
            else
            {
                icmbConditionType.ReadOnly = true;
                hleConditionContent.Enabled = false;
                hleTableSetting.Enabled = false;
                lblTableType.Enabled = false;
                lblFormCodeRequired.Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkFinalReview_CheckedChanged(object sender, EventArgs e)
        {
            lblFinalReviewTip.Visible = chkFinalReview.Checked;
            hlnkFinalReview.Enabled = chkFinalReview.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsInitReview_CheckedChanged(object sender, EventArgs e)
        {
            btxtInitReviewRole.Enabled = chkIsInitReview.Checked;
            chkEnableManager.ReadOnly = !chkIsInitReview.Checked;
            lblInitReviewTip.Visible = chkIsInitReview.Checked;
        }
       
        /// <summary>
        /// 设置初审角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtInitReviewRole_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            SetRole(true);
        }

        /// <summary>
        /// 设置终审角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkFinalReview_OpenLink(object sender, OpenLinkEventArgs e)
        {
            SetRole(false);
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
                txtDataCode.Text = value;
            }
            get
            {
                return txtDataCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtDataName.ReadOnly = readOnly;
            txtDataCode.ReadOnly = readOnly;            
            icmbDataFilledType.ReadOnly = readOnly;
            icmbDataShowMode.ReadOnly = readOnly;            
            txtClientFormHeight.ReadOnly = readOnly;
            icmbDataInputMode.ReadOnly = readOnly;
            DataInputMode dataInputMode = (DataInputMode)Convert.ToByte(icmbDataInputMode.EditValue);
            if (dataInputMode == DataInputMode.Table)
            {
                btxtInputReport.ReadOnly = true;
            }
            else
            {
                btxtInputReport.ReadOnly = readOnly;
            }            
            txtWebFormHeight.ReadOnly = readOnly;
            chkFinalReview.ReadOnly = readOnly;
            chkIsInitReview.ReadOnly = readOnly;
            if (chkIsInitReview.Checked)
            {
                btxtInitReviewRole.Enabled = !readOnly;
                chkEnableManager.ReadOnly = readOnly;
            }
            else
            {
                btxtInitReviewRole.Enabled = false;
                chkEnableManager.ReadOnly = true;
            }
            if (chkFinalReview.Checked)
            {
                hlnkFinalReview.Enabled = !readOnly;
            }
            else
            {
                hlnkFinalReview.Enabled = false;
            }
            icmbDataFilledProperty.ReadOnly = readOnly;
            chkEnableGuidance.ReadOnly = readOnly;
            btxtReport.Properties.ReadOnly = readOnly;
            if (chkEnableGuidance.Checked)
            {
                hleGuidance.Enabled = !readOnly;
            }
            else
            {
                hleGuidance.Enabled = false;
            }
            chkConditionEnabled.ReadOnly = readOnly;
            if (chkConditionEnabled.Checked)
            {
                icmbConditionType.ReadOnly = readOnly;
                hleTableSetting.Enabled = !readOnly;
                hleConditionContent.Enabled = !readOnly;
                lblTableType.Enabled = true;
            }
            else
            {
                icmbConditionType.ReadOnly = true;
                hleTableSetting.Enabled = false;
                lblTableType.Enabled = false;
                hleConditionContent.Enabled = false;
            }
            txtNotes.ReadOnly = readOnly;
            if (!txtDataName.ReadOnly)
            {
                txtDataName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomDataInfo customDataInfo = CustomDataContract.GetModelInfo(commonNode.NodeId);
            if (customDataInfo != null)
            {
                txtDataName.Text = customDataInfo.DataName;
                txtDataCode.Text = customDataInfo.DataCode;
                icmbDataFilledType.EditValue = customDataInfo.DataFilledType;
                icmbDataShowMode.EditValue = customDataInfo.DataShowMode;
                icmbDataInputMode.EditValue = customDataInfo.DataInputMode;
                txtClientFormHeight.Text = DataConvertionHelper.EndowStringOfInt(customDataInfo.ClientFormHeight);
                txtWebFormHeight.Text = DataConvertionHelper.EndowStringOfInt(customDataInfo.WebFormHeight);
                chkFinalReview.Checked = customDataInfo.IsFinalReview;
                chkIsInitReview.Checked = customDataInfo.IsInitReview;
                if (customDataInfo.IsInitReview)
                {
                    btxtInitReviewRole.Text = CustomRoleContract.GetNodeNameByNodeId(customDataInfo.RoleId);
                    btxtInitReviewRole.Tag = customDataInfo.RoleId;
                    chkEnableManager.Checked = customDataInfo.EnableManager;
                }
                else
                {
                    btxtInitReviewRole.Text = string.Empty;
                    btxtInitReviewRole.Tag = null;
                    chkEnableManager.Checked = false;
                }
                if (customDataInfo.IsFinalReview)
                {
                    txtFinalReview.Text = CustomRoleContract.GetNodeNameByNodeId(customDataInfo.ParentRoleId);
                    txtFinalReview.Tag = customDataInfo.ParentRoleId;
                }
                else
                {
                    txtFinalReview.Text = string.Empty;
                    txtFinalReview.Tag = null;
                }
                icmbDataFilledProperty.EditValue = customDataInfo.DataFilledProperty;
                ControlContentHelper.SetAttachments(chkEnableGuidance, txtGuidance, hleGuidance, customDataInfo.ConditionEnabled, customDataInfo.Guidance);
                ControlContentHelper.SetAttachments(chkConditionEnabled, txtConditionContent, hleConditionContent, customDataInfo.ConditionEnabled, customDataInfo.ConditionContent);
                ConditionType conditionType = (ConditionType)customDataInfo.ConditionType;
                switch (conditionType)
                {
                    case ConditionType.Table:
                        if (customDataInfo.TableId > 0)
                        {
                            txtDependency.Text = CustomTableContract.GetFullName(customDataInfo.TableId);
                            txtDependency.Tag = CustomTableContract.GetCommonNode(customDataInfo.TableId);
                        }
                        else
                        {
                            txtDependency.Text = string.Empty;
                            txtDependency.Tag = null;
                        }
                        break;

                    case ConditionType.View:
                        if (customDataInfo.ViewId > 0)
                        {
                            txtDependency.Text = CustomViewContract.GetFullName(customDataInfo.ViewId);
                            txtDependency.Tag = CustomViewContract.GetCommonNode(customDataInfo.ViewId);
                        }
                        else
                        {
                            txtDependency.Text = string.Empty;
                            txtDependency.Tag = null;
                        }
                        break;
                }
                if (!DataConvertionHelper.IsNullValue(customDataInfo.ReportId))
                {
                    btxtReport.Text = CustomReportContract.GetFullName(customDataInfo.ReportId);
                    btxtReport.Tag = customDataInfo.ReportId;
                }
                else
                {
                    btxtReport.Text = string.Empty;
                    btxtReport.Tag = null;
                }
                if (!DataConvertionHelper.IsNullValue(customDataInfo.ParentReportId))
                {
                    btxtInputReport.Text = CustomReportContract.GetFullName(customDataInfo.ReportId);
                    btxtInputReport.Tag = customDataInfo.ReportId;
                }
                else
                {
                    btxtInputReport.Text = string.Empty;
                    btxtInputReport.Tag = null;
                }
                chkConditionEnabled.Checked = customDataInfo.EnableGuidance;
                icmbConditionType.EditValue = customDataInfo.ConditionType;
                txtNotes.Text = customDataInfo.Notes;
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
            txtDataName.Text = string.Empty;
            txtDataCode.Text = string.Empty;
            icmbDataFilledType.SelectedIndex = 0;
            icmbDataShowMode.SelectedIndex = 0;
            icmbDataInputMode.SelectedIndex = 0;
            txtClientFormHeight.Text = string.Empty;
            txtWebFormHeight.Text = string.Empty;
            chkFinalReview.Checked = false;
            chkIsInitReview.Checked = false;
            btxtInitReviewRole.Text = string.Empty;
            txtFinalReview.Text = string.Empty;
            btxtInitReviewRole.Tag = null;
            txtFinalReview.Tag = null;
            chkEnableManager.Checked = false;
            icmbDataFilledProperty.SelectedIndex = 0;
            chkEnableGuidance.Checked = false;
            hleGuidance.Enabled = false;
            txtGuidance.Text = string.Empty;
            txtGuidance.Tag = null;
            hleGuidance.Tag = null;
            chkConditionEnabled.Checked = false;
            icmbConditionType.SelectedIndex = 0;
            txtDependency.Text = string.Empty;
            txtDependency.Tag = null;
            txtConditionContent.Text = string.Empty;
            txtConditionContent.Tag = null;
            hleConditionContent.Tag = null;
            btxtInputReport.Text = string.Empty;
            btxtInputReport.Tag = null;
            btxtReport.Text = string.Empty;
            btxtReport.Tag = null;
            txtNotes.Text = string.Empty;
            if (!txtDataName.ReadOnly)
            {
                txtDataName.Focus();
            }
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;
            warning = string.Empty;

            if (chkEnableGuidance.Checked && txtGuidance.Tag == null)
            {
                result = false;
                warning = "指南内容未设置。";
            }
            if (result && chkConditionEnabled.Checked)
            {
                if (txtConditionContent.Tag == null)
                {
                    result = false;
                    warning = "条件内容未设置。";
                }
                CommonNode commonNode = txtDependency.Tag as CommonNode;
                if (result && commonNode == null)
                {
                    result = false;
                    warning = "关联内容未设置。";
                }
            }
            if (result && chkIsInitReview.Checked)
            {
                if (btxtInitReviewRole.Tag == null)
                {
                    result = false;
                    warning = "初审角色未设置。";
                }
            }
            if (result && chkFinalReview.Checked)
            {
                if (txtFinalReview.Tag == null)
                {
                    result = false;
                    warning = "终审审角色未设置。";
                }
            }
            if (result)
            {
                int clientFormHeight = int.MinValue;
                string clientFormHeightValue = txtClientFormHeight.Text.Trim();
                if (!string.IsNullOrWhiteSpace(clientFormHeightValue))
                {
                    bool value = UserDataHelper.MatchPositiveInteger(clientFormHeightValue);
                    if (value)
                    {
                        clientFormHeight = Convert.ToInt32(clientFormHeightValue);
                        if (clientFormHeight > MAX_FORM_HEIGHT)
                        {
                            result = false;
                            warning = string.Format("客户端窗体高度不能超过{0}。", MAX_FORM_HEIGHT);
                        }
                        else if (clientFormHeight < MIN_FORM_HEIGHT)
                        {
                            result = false;
                            warning = string.Format("客户端窗体高度不能小于300。{0}。", MIN_FORM_HEIGHT);
                        }
                    }
                    else
                    {
                        result = false;
                        warning = "客户端窗体高度必须是数字。";
                    }
                }
            }
            if (result)
            {
                int webFormHeight = int.MinValue;
                string webFormHeightValue = txtWebFormHeight.Text.Trim();
                if (!string.IsNullOrWhiteSpace(webFormHeightValue))
                {
                    bool value = UserDataHelper.MatchPositiveInteger(webFormHeightValue);
                    if (value)
                    {
                        webFormHeight = Convert.ToInt32(webFormHeightValue);
                        if (webFormHeight > MAX_FORM_HEIGHT)
                        {
                            result = false;
                            warning = string.Format("Web 端窗体高度不能超过{0}。", MAX_FORM_HEIGHT);
                        }
                        else if (webFormHeight < MIN_FORM_HEIGHT)
                        {
                            result = false;
                            warning = string.Format("Web 端窗体高度不能小于{0}。", MIN_FORM_HEIGHT);
                        }
                    }
                    else
                    {
                        result = false;
                        warning = "Web 端窗体高度必须是数字。";
                    }
                }
            }
            DataInputMode dataInputMode = (DataInputMode)Convert.ToByte(icmbDataInputMode.EditValue);
            if (result && dataInputMode == DataInputMode.Report && btxtInputReport.Tag == null)
            {
                result = false;
                warning = "请关联复表。";
            }
            if (result)
            {
                CustomDataInfo customDataInfo = GetModelInfo();
                result = ValidationHelper.Validate<CustomDataInfo>(customDataInfo, out warning);
            }

            return result;
        }

        /// <summary>
        /// 录入模式变化引起提示变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbDataInputMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataInputMode dataInputMode = (DataInputMode)Convert.ToByte(icmbDataInputMode.EditValue);
            if (dataInputMode == DataInputMode.Table)
            {
                btxtInputReport.ReadOnly = true;
                lblReportRequired.Visible = false;
            }
            else
            {
                btxtInputReport.ReadOnly = false;
                lblReportRequired.Visible = true;
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取数据填报的信息
        /// </summary>
        /// <returns></returns>
        public CustomDataInfo GetModelInfo()
        {
            byte conditionTypeValue = Convert.ToByte(icmbConditionType.EditValue);
            ConditionType conditionType = (ConditionType)conditionTypeValue;
            decimal tableId = decimal.MinValue;
            decimal viewId = decimal.MinValue;
            decimal roleId = decimal.MinValue;
            decimal parentRoleId = decimal.MinValue;

            if (chkEnableGuidance.Checked && txtGuidance.Tag == null)
            {
                throw new ArgumentNullException("指南内容未设置。");
            }
            if (chkIsInitReview.Checked && btxtInitReviewRole.Tag == null)
            {
                throw new ArgumentNullException("初审角色未设置。");
            }
            if (chkFinalReview.Checked && txtFinalReview.Tag == null)
            {
                throw new ArgumentNullException("终审角色未设置。");
            }
            roleId = DataConvertionHelper.GetDecimal(btxtInitReviewRole.Tag);
            parentRoleId = DataConvertionHelper.GetDecimal(txtFinalReview.Tag);
            if (chkConditionEnabled.Checked)
            {
                if (txtConditionContent.Tag == null)
                {
                    throw new ArgumentNullException("条件内容未设置。");
                }
                CommonNode commonNode = txtDependency.Tag as CommonNode;
                if (commonNode == null)
                {
                    throw new ArgumentNullException("表与视图未设置。");
                }
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
            int clientFormHeight = int.MinValue;
            string clientFormHeightValue = txtClientFormHeight.Text.Trim();
            if (!string.IsNullOrWhiteSpace(clientFormHeightValue))
            {
                clientFormHeight = Convert.ToInt32(clientFormHeightValue);
            }
            int webFormHeight = int.MinValue;
            string webFormHeightValue = txtWebFormHeight.Text.Trim();
            if (!string.IsNullOrWhiteSpace(webFormHeightValue))
            {
                webFormHeight = Convert.ToInt32(webFormHeightValue);
            }
            decimal reportId = decimal.MinValue;
            if (btxtReport.Tag != null)
            {
                reportId = Convert.ToDecimal(btxtReport.Tag);
            }
            decimal parentReportId = decimal.MinValue;
            if (btxtInputReport.Tag != null)
            {
                parentReportId = Convert.ToDecimal(btxtInputReport.Tag);
            }
            CustomDataInfo customDataInfo = new CustomDataInfo()
            {
                DataId = TreeNodeId,
                TableId = tableId,
                ReportId = reportId,
                RoleId = roleId,
                ParentReportId = parentReportId,
                ParentRoleId = parentRoleId,
                ViewId = viewId,
                DataName = txtDataName.Text.Trim(),
                DataCode = txtDataCode.Text.Trim(),
                DataFilledType = Convert.ToByte(icmbDataFilledType.EditValue),
                DataShowMode = Convert.ToByte(icmbDataShowMode.EditValue),
                ClientFormHeight = clientFormHeight,
                WebFormHeight = webFormHeight,
                IsInitReview = chkIsInitReview.Checked,
                EnableManager = chkEnableManager.Checked,
                IsFinalReview = chkFinalReview.Checked,
                DataFilledProperty = Convert.ToByte(icmbDataFilledProperty.EditValue),
                EnableGuidance = chkEnableGuidance.Checked,
                Guidance = DataConvertionHelper.GetString(txtGuidance.Tag),
                ConditionEnabled = chkConditionEnabled.Checked,
                ConditionContent = DataConvertionHelper.GetString(txtConditionContent.Tag),
                ConditionType = conditionTypeValue,
                Notes = txtNotes.Text.Trim()
            };

            return customDataInfo;
        }

        /// <summary>
        /// 获得指南附件
        /// </summary>
        /// <returns></returns>
        public IList<ExtendedUpLoadFileInfo> GetAttachements()
        {
            IList<ExtendedUpLoadFileInfo> upLoadFileInfos = hleGuidance.Tag as IList<ExtendedUpLoadFileInfo>;

            return upLoadFileInfos;
        }

        /// <summary>
        /// 获得条件内容附件
        /// </summary>
        /// <returns></returns>
        public IList<ExtendedUpLoadFileInfo> GetConditionAttachements()
        {
            IList<ExtendedUpLoadFileInfo> upLoadFileInfos = hleConditionContent.Tag as IList<ExtendedUpLoadFileInfo>;

            return upLoadFileInfos;
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
                        btxtInitReviewRole.Tag = node.NodeId;
                    }
                    else
                    {
                        txtFinalReview.Text = node.NodeName;
                        txtFinalReview.Tag = node.NodeId;
                    }
                }
                else
                {
                    if (init)
                    {
                        btxtInitReviewRole.Text = string.Empty;
                        btxtInitReviewRole.Tag = null;
                        chkEnableManager.Checked = false;
                    }
                    else
                    {
                        txtFinalReview.Text = string.Empty;
                        txtFinalReview.Tag = null;
                    }
                }
            };
            frmRoleItems.ShowDialog();
        }

        #endregion        
    }
}
