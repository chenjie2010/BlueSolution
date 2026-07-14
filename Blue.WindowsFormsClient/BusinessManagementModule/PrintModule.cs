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
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class PrintModule : UserControl, ITreeNodeShow
    {
        #region 私有变量

        #endregion

        #region 属性
        
        /// <summary>
        /// 打印契约
        /// </summary>
        public ICustomPrintContract CustomPrintContract
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
        /// 角色契约
        /// </summary>
        public ICustomRoleContract CustomRoleContract
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PrintModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbTableType, typeof(FormType));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(SystemDataField));
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintModule_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtTableName_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            try
            {
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
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }
        
        /// <summary>
        /// 表类型切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbTableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btxtTableName.Text = string.Empty;
            btxtTableName.Tag = null;
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkDetailedView_OpenLink(object sender, OpenLinkEventArgs e)
        {
            if (TreeNodeId > 0)
            {
                IList<CommonNode> commonNodes = CustomRoleContract.GetRolesByPrintId(TreeNodeId);
                lstRoles.DataSource = commonNodes;
                lstRoles.DisplayMember = "NodeName";
                lstRoles.ValueMember = "NodeId";
                if (fpRoleList.FlyoutPanelState.IsActive)
                {
                    return;
                }
                fpRoleList.ShowBeakForm();
            }
        }

        /// <summary>
        /// 隐藏弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpRoleList_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            fpRoleList.HideBeakForm();
        }

        /// <summary>
        /// 查看预览记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkRecord_OpenLink(object sender, OpenLinkEventArgs e)
        {
            PrintingRecordListForm frmPrintingRecordList = new PrintingRecordListForm();
            frmPrintingRecordList.PrintId = TreeNodeId;
            frmPrintingRecordList.ShowDialog();
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
                txtPrintCode.Text = value;
            }
            get
            {
                return txtPrintCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtPrintName.ReadOnly = readOnly;
            icmbTableType.ReadOnly = readOnly;
            btxtTableName.ReadOnly = readOnly;
            ccmbDataFieldSetting.ReadOnly = readOnly;
            chkPrintVisible.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;            
            if (!txtPrintName.ReadOnly)
            {
                txtPrintName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomPrintInfo customPrintInfo = CustomPrintContract.GetModelInfo(commonNode.NodeId);
            if (customPrintInfo != null)
            {
                txtPrintName.Text = customPrintInfo.PrintName;
                txtPrintCode.Text = customPrintInfo.PrintCode;
                icmbTableType.EditValue = customPrintInfo.TableType;
                FormType formType = (FormType)customPrintInfo.TableType;
                switch (formType)
                {
                    case FormType.CombinedTable:
                        btxtTableName.Text = CombinedTableContract.GetFullName(customPrintInfo.CombinedTableId);
                        btxtTableName.Tag = CombinedTableContract.GetCommonNode(customPrintInfo.CombinedTableId);
                        break;

                    case FormType.Table:
                        btxtTableName.Text = CustomTableContract.GetFullName(customPrintInfo.TableId);
                        btxtTableName.Tag = CustomTableContract.GetCommonNode(customPrintInfo.TableId);
                        break;
                }
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbDataFieldSetting, customPrintInfo.SystemDataField);
                chkPrintVisible.Checked = customPrintInfo.PrintVisible;
                txtNotes.Text = customPrintInfo.Notes;
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
            txtPrintName.Text = string.Empty;
            txtPrintCode.Text = string.Empty;
            btxtTableName.Text = string.Empty;
            btxtTableName.Tag = null;
            icmbTableType.SelectedIndex = 0;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataFieldSetting);
            chkPrintVisible.Checked = false;
            txtNotes.Text = string.Empty;            
            if (!txtPrintName.ReadOnly)
            {
                txtPrintName.Focus();
            }
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;

            if (btxtTableName.Tag == null)
            {
                warning = "请设置表格名称。";
                return false;
            }
            CustomPrintInfo customPrintInfo = GetModelInfo();
            result = ValidationHelper.Validate<CustomPrintInfo>(customPrintInfo, out warning);

            return result;
        }
        
        #endregion

        #region 公有方法

        /// <summary>
        /// 获取打印的信息
        /// </summary>
        /// <returns></returns>
        public CustomPrintInfo GetModelInfo()
        {                     
            if (btxtTableName.Tag == null)
            {
                throw new ArgumentException("请设置表格名称。");
            }
            decimal tableId = decimal.MinValue;
            decimal combinedTableId = decimal.MinValue;
            byte tableTypeValue = Convert.ToByte(icmbTableType.EditValue);
            CommonNode commonNode = btxtTableName.Tag as CommonNode;
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
            CustomPrintInfo customPrintInfo = new CustomPrintInfo()
            {
                PrintId = TreeNodeId,
                PrintName = txtPrintName.Text.Trim(),
                PrintCode = txtPrintCode.Text.Trim(),
                TableType = tableTypeValue,
                CombinedTableId = combinedTableId,                
                SystemDataField = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDataFieldSetting),              
                TableId = tableId,    
                PrintVisible = chkPrintVisible.Checked,
                PrintContent = "暂未设置",
                Notes = txtNotes.Text.Trim()
            };

            return customPrintInfo;
        }


        #endregion

       
    }
}
