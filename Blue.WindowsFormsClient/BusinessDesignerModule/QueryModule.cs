using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class QueryModule : UserControl, ITreeNodeShow
    {
        #region 属性

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

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbDataQueriedType, typeof(DataQueriedType));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbSystemCondition, typeof(SystemCondition));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(SystemDataField));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbGroupCondition, typeof(GroupCondition));
            IList<CommonNode> commonNodes = ShowModeHelper.GetQueriedShowModes();
            cmbQueriedShowMode.SelectedNode = null;
            cmbQueriedShowMode.TreeViewHandler.InitFullTreeNodes(commonNodes);
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataRange, typeof(QueriedDataRange));
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryModule_Load(object sender, EventArgs e)
        {

        }               

        /// <summary>
        /// 选择分组或是视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleTableSetting_OpenLink(object sender, OpenLinkEventArgs e)
        {
            try
            {
                ccmbDataFieldSetting.Properties.Items.Clear();
                DataQueriedType dataQueriedType = (DataQueriedType)Convert.ToByte(icmbDataQueriedType.EditValue);
                switch (dataQueriedType)
                {
                    case DataQueriedType.Table:
                        UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(SystemDataField));
                        DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
                        frmDataTableItems.Text = "查询表选择";
                        frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
                        frmDataTableItems.NodeSelected = delegate (CommonNode node)
                        {
                            if (node != null)
                            {
                                txtTableName.Text = CustomTableContract.GetFullName(node.NodeId);
                                txtTableName.Tag = node;
                            }
                            else
                            {
                                txtTableName.Text = string.Empty;
                                txtTableName.Tag = null;
                            }
                        };
                        frmDataTableItems.ShowDialog();
                        break;

                    case DataQueriedType.View:
                        ViewItemsForm frmViewItems = new ViewItemsForm();
                        frmViewItems.Text = "查询视图选择";
                        frmViewItems.ToolTip = "提示：只能选择视图类型的节点。";
                        frmViewItems.NodeSelected = delegate (CommonNode node)
                        {
                            if (node != null)
                            {
                                LoadSystemDataFields(node.NodeId);
                                txtTableName.Text = CustomViewContract.GetFullName(node.NodeId);
                                txtTableName.Tag = node;
                            }
                            else
                            {
                                UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(SystemDataField));
                                txtTableName.Text = string.Empty;
                                txtTableName.Tag = null;
                            }
                        };
                        frmViewItems.ShowDialog();
                        break;

                    case DataQueriedType.Custom:                        
                        UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(SystemDataField));
                        CustomQueryDefinitionForm frmCustomQuerySetting = new CustomQueryDefinitionForm();
                        frmCustomQuerySetting.CustomQueyContract = CustomQueyContract;
                        frmCustomQuerySetting.CustomQueryName = txtTableName.Text.Trim();
                        if (txtTableName.Tag != null)
                        {
                            CommonNode node = txtTableName.Tag as CommonNode;
                            frmCustomQuerySetting.DataWarehouseId = DataConvertionHelper.GetConvertedByte(node.NodeId);
                            frmCustomQuerySetting.Conditions = node.NodeName;
                        }
                        frmCustomQuerySetting.GetEntity = delegate (CommonNode commonNode)
                        {
                            if (TreeNodeId > 0)
                            {
                                CustomQueyInfo customQueyInfo = CustomQueyContract.GetModelInfo(TreeNodeId);
                                txtTableName.Text = string.Format("[{0}][{1}]", UserEnumHelper.GetEnumText((DataWarehouse)commonNode.NodeId), customQueyInfo.CustomViewName);
                            }
                            else
                            {
                                txtTableName.Text = string.Format("[{0}][查询待创建]", UserEnumHelper.GetEnumText((DataWarehouse)commonNode.NodeId));
                            }
                            txtTableName.Tag = commonNode;
                        };
                        frmCustomQuerySetting.ShowDialog();
                        break;
                }
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
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
                txtDataQueriedCode.Text = value;
            }
            get
            {
                return txtDataQueriedCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtDataQueriedName.ReadOnly = readOnly;
            txtDataQueriedCode.ReadOnly = readOnly;
            if (TreeNodeId > 0)
            {
                icmbDataQueriedType.ReadOnly = true;
            }
            else
            {
                icmbDataQueriedType.ReadOnly = readOnly;
            }
            hleTableSetting.Enabled = !readOnly;
            ccmbDataFieldSetting.ReadOnly = readOnly;
            ccmbSystemCondition.ReadOnly = readOnly;
            ccmbSystemCondition.Properties.ReadOnly = readOnly;
            ccmbGroupCondition.Properties.ReadOnly = readOnly;
            cmbQueriedShowMode.ReadOnly = readOnly;
            ccmbDataRange.Properties.ReadOnly = readOnly;
            txtToolTip.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!txtDataQueriedName.ReadOnly)
            {
                txtDataQueriedName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomQueyInfo customQueyInfo = CustomQueyContract.GetModelInfo(commonNode.NodeId);
            if (customQueyInfo != null)
            {
                txtDataQueriedName.Text = customQueyInfo.DataQueriedName;
                txtDataQueriedCode.Text = customQueyInfo.DataQueriedCode;
                icmbDataQueriedType.EditValue = customQueyInfo.DataQueriedType;
                DataQueriedType dataQueriedType = (DataQueriedType)customQueyInfo.DataQueriedType;
                switch (dataQueriedType)
                {
                    case DataQueriedType.Table:
                        
                        txtTableName.Text = CustomTableContract.GetFullName(customQueyInfo.TableId);
                        txtTableName.Tag = CustomTableContract.GetCommonNode(customQueyInfo.TableId);
                        break;

                    case DataQueriedType.View:
                        LoadSystemDataFields(customQueyInfo.ViewId);                        
                        txtTableName.Text = CustomViewContract.GetFullName(customQueyInfo.ViewId);
                        txtTableName.Tag = CustomViewContract.GetCommonNode(customQueyInfo.ViewId);
                        break;

                    case DataQueriedType.Custom:                       
                        CommonNode node = new CommonNode(customQueyInfo.DataWarehouseId, customQueyInfo.Conditions);
                        txtTableName.Text = string.Format("[{0}][{1}]",
                            UserEnumHelper.GetEnumText((DataWarehouse)customQueyInfo.DataWarehouseId), customQueyInfo.CustomViewName);
                        txtTableName.Tag = node;
                        break;
                }
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbDataFieldSetting, customQueyInfo.SystemDataFields);
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbSystemCondition, customQueyInfo.SystemCondition);
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbGroupCondition, customQueyInfo.GroupCondition);
                cmbQueriedShowMode.SelectedNodeId = customQueyInfo.ShowMode;
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbDataRange, customQueyInfo.DataRange);
                txtToolTip.Text = customQueyInfo.ToolTip;
                txtNotes.Text = customQueyInfo.Notes;
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
            txtDataQueriedName.Text = string.Empty;
            txtDataQueriedCode.Text = string.Empty;
            icmbDataQueriedType.SelectedIndex = 0;
            txtTableName.Text = string.Empty;
            txtTableName.Tag = null;

            ccmbDataFieldSetting.Properties.Items.Clear();
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(SystemDataField));
            ccmbDataFieldSetting.EditValue = null;
            ccmbSystemCondition.EditValue = null;
            ccmbGroupCondition.EditValue = null;
            cmbQueriedShowMode.SelectedNode = null;
            ccmbDataRange.EditValue = null;
            txtDataQueriedCode.Text = string.Empty;
            txtToolTip.Text = string.Empty;
            txtNotes.Text = string.Empty;
            if (!txtDataQueriedName.ReadOnly)
            {
                txtDataQueriedName.Focus();
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

            if (cmbQueriedShowMode.SelectedNode == null)
            {
                warning = "请选择展现模式。";
                result = false;
            }
            if (result)
            {
                CommonNode commonNode = txtTableName.Tag as CommonNode;
                DataQueriedType dataQueriedType = (DataQueriedType)Convert.ToByte(icmbDataQueriedType.EditValue);
                switch (dataQueriedType)
                {
                    case DataQueriedType.Table:
                    case DataQueriedType.View:
                        if (commonNode == null || commonNode.NodeId <= 0)
                        {
                            result = false;
                            if (dataQueriedType == DataQueriedType.Table)
                            {
                                warning = "请先选择表。";
                            }
                            else
                            {
                                warning = "请先选择视图。";
                            }
                        }
                        break;

                    case DataQueriedType.Custom:
                        if (commonNode == null || commonNode.NodeId <= 0 || string.IsNullOrWhiteSpace(commonNode.NodeName))
                        {
                            result = false;
                            warning = "请先设计自定义查询。";
                        }
                        break;
                }
                if (result)
                {
                    CustomQueyInfo customQueyInfo = GetModelInfo();
                    result = ValidationHelper.Validate<CustomQueyInfo>(customQueyInfo, out warning);
                }
            }

            return result;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取查询的信息
        /// </summary>
        /// <returns></returns>
        public CustomQueyInfo GetModelInfo()
        {
            decimal tableId = decimal.MinValue;
            decimal viewId = decimal.MinValue;
            byte dataWarehouseId = 0;
            string conditions = string.Empty;
            string captions = string.Empty;
            CommonNode commonNode = txtTableName.Tag as CommonNode;

            DataQueriedType dataQueriedType = (DataQueriedType)Convert.ToByte(icmbDataQueriedType.EditValue);
            switch (dataQueriedType)
            {
                case DataQueriedType.Table:
                    tableId = commonNode.NodeId;
                    break;

                case DataQueriedType.View:
                    viewId = commonNode.NodeId;
                    break;

                case DataQueriedType.Custom:
                    dataWarehouseId = DataConvertionHelper.GetConvertedByte(commonNode.NodeId, 0);
                    conditions = commonNode.NodeName;
                    captions = commonNode.NodeCode;
                    break;
            }
            CustomQueyInfo customQueyInfo = new CustomQueyInfo()
            {
                DataQueriedId = TreeNodeId,
                DataQueriedName = txtDataQueriedName.Text.Trim(),
                DataQueriedCode = txtDataQueriedCode.Text.Trim(),
                DataWarehouseId = dataWarehouseId,
                Conditions = conditions,
                DataQueriedType = (byte)dataQueriedType,
                SystemDataFields = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDataFieldSetting),
                TableId = tableId,
                ViewId = viewId,
                SystemCondition = UserControlHelper.GetCheckedComboBoxEditItems(ccmbSystemCondition),
                GroupCondition = UserControlHelper.GetCheckedComboBoxEditItems(ccmbGroupCondition),
                ShowMode = Convert.ToByte(cmbQueriedShowMode.SelectedNode.NodeId),
                DataRange = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDataRange),
                ToolTip = txtToolTip.Text.Trim(),
                Notes = txtNotes.Text.Trim()
            };

            return customQueyInfo;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载视图条件下的系统字段
        /// </summary>
        /// <param name="viewId"></param>
        private void LoadSystemDataFields(decimal viewId)
        {
            ccmbDataFieldSetting.Properties.Items.Clear();
            Int64 systemDataFields = CustomViewContract.GetSystemDataFields(viewId);
            List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemDataField));
            foreach (var enumItem in enumItems)
            {
                if (AuthorityHelper.CheckAuthority(systemDataFields, enumItem.Value))
                {
                    ccmbDataFieldSetting.Properties.Items.Add(enumItem);
                }
            }
        }

        #endregion
    }
}
