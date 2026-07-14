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
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class ViewModule : UserControl, ITreeNodeShow
    {
        #region 私有变量

        private Dictionary<decimal, CustomViewAndTableInfo> selectedCustomViewAndTableInfos;

        #endregion

        #region 属性

        /// <summary>
        /// 视图契约
        /// </summary>
        public ICustomViewContract CustomViewContract
        {
            get; set;
        }

        /// <summary>
        /// 数据库表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            get; set;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewModule()
        {
            InitializeComponent();
            UserControlHelper.InitImageComboBoxEdit(icmbViewProperty, typeof(ViewProperty));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(SystemDataField));
            TreeNodeId = 0;
            selectedCustomViewAndTableInfos = new Dictionary<decimal, CustomViewAndTableInfo>();
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModule_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 主表选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleMainTable_OpenLink(object sender, OpenLinkEventArgs e)
        {
            DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
            frmDataTableItems.TableFilter = TableFilter.All;
            frmDataTableItems.Text = "主表选择";
            frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
            frmDataTableItems.NodeSelected = delegate (CommonNode node) {
                if (node != null)
                {
                    txtMainTable.Text = CustomTableContract.GetFullName(node.NodeId);
                    txtMainTable.Tag = node;
                }
                else
                {
                    txtMainTable.Text = string.Empty;
                    txtMainTable.Tag = null;
                }
            };
            frmDataTableItems.ShowDialog();
        }

        /// <summary>
        /// 从表选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleAssociatedTables_OpenLink(object sender, OpenLinkEventArgs e)
        {
            if (txtMainTable.Tag == null)
            {
                MessageBox.Show("请先选择主数据表。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CommonNode mainNode = txtMainTable.Tag as CommonNode;
            if (mainNode == null)
            {
                throw new Exception("选择主数据表异常。");
            }
            decimal previousTableId = 0;
            if (lstAssociatedTables.Items.Count > 0)
            {
                CommonNode commonNode = lstAssociatedTables.Items[lstAssociatedTables.Items.Count - 1] as CommonNode;
                previousTableId = commonNode.NodeId;
            }

            CommonListItemsForm frmCommonListItems = new CommonListItemsForm();
            frmCommonListItems.Text = "从表选择";
            frmCommonListItems.ToolTip = "从表项目列表";
            frmCommonListItems.RemoveItem = delegate (decimal tableId)
            {
                if (selectedCustomViewAndTableInfos.ContainsKey(tableId))
                {
                    selectedCustomViewAndTableInfos.Remove(tableId);
                }
            };
            frmCommonListItems.CreateItmes = delegate (ListBoxControl lstItems)
            {
                DataTableShowForm frmDataTableSelectedItems = new DataTableShowForm();
                frmDataTableSelectedItems.TableFilter = TableFilter.All;
                frmDataTableSelectedItems.ViewProperty = (ViewProperty)Convert.ToByte(icmbViewProperty.EditValue);
                frmDataTableSelectedItems.DataWarehouseId = CustomTableContract.GetDataWarehouseId(mainNode.NodeId);
                frmDataTableSelectedItems.Text = "从表选择";
                frmDataTableSelectedItems.ToolTip = "提示：只能选择数据表类型的节点。";
                frmDataTableSelectedItems.MainTableId = mainNode.NodeId;
                frmDataTableSelectedItems.PreviousTableId = previousTableId;
                frmDataTableSelectedItems.GetCustomViewAndTableData = delegate (CustomViewAndTableInfo customViewAndTableInfo)
                {
                    if (customViewAndTableInfo != null)
                    {
                        if (customViewAndTableInfo.TableId == mainNode.NodeId)
                        {
                            MessageBox.Show("该表已选为主数据表，不能再作为从表。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        foreach (object obj in lstItems.Items)
                        {
                            CommonNode node = obj as CommonNode;
                            if (node.NodeId == customViewAndTableInfo.TableId)
                            {
                                MessageBox.Show("不能重复选择数据表。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }                            
                        }
                        customViewAndTableInfo.Sorting = selectedCustomViewAndTableInfos.Count + 1;
                        if (!selectedCustomViewAndTableInfos.ContainsKey(customViewAndTableInfo.TableId))
                        {
                            selectedCustomViewAndTableInfos.Add(customViewAndTableInfo.TableId, customViewAndTableInfo);
                            lstItems.Items.Add(GetCommonNode(customViewAndTableInfo));
                        }
                    }
                };
                frmDataTableSelectedItems.ShowDialog();
            };
            frmCommonListItems.GetItems = delegate (IList<CommonNode> nodes)
            {
                lstAssociatedTables.Items.Clear();
                if (nodes != null && nodes.Count > 0)
                {
                    lstAssociatedTables.Items.AddRange(nodes.ToArray());
                    /* 移除被删除的表节点 */
                    foreach (var obj in nodes)
                    {
                        if (!selectedCustomViewAndTableInfos.ContainsKey(obj.NodeId))
                        {
                            selectedCustomViewAndTableInfos.Remove(obj.NodeId);
                        }
                    }
                }
            };
            if (lstAssociatedTables.Items.Count > 0)
            {
                IList<CommonNode> commonNodes = new List<CommonNode>();
                foreach (var obj in lstAssociatedTables.Items)
                {
                    CommonNode commonNode = obj as CommonNode;
                    commonNodes.Add(commonNode);
                }
                frmCommonListItems.LoadItems(commonNodes);
            }
            frmCommonListItems.ShowDialog();
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
                txtViewCode.Text = value;
            }
            get
            {
                return txtViewCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtViewLogicalName.ReadOnly = readOnly;
            txtViewCode.ReadOnly = readOnly;
            icmbViewProperty.ReadOnly = readOnly;
            ccmbDataFieldSetting.ReadOnly = readOnly;
            txtMainTable.ReadOnly = readOnly;
            lstAssociatedTables.Enabled = !readOnly;
            hleMainTable.Enabled = !readOnly;
            hleAssociatedTables.Enabled = !readOnly;
            txtTooltip.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            hleAssociatedTables.ReadOnly = readOnly;
            if (!txtViewLogicalName.ReadOnly)
            {
                txtViewLogicalName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomViewInfo customViewInfo = CustomViewContract.GetModelInfo(commonNode.NodeId);
            if (customViewInfo != null)
            {
                txtViewLogicalName.Text = customViewInfo.ViewName;
                txtViewPhysicalName.Text = customViewInfo.PhysicalName;
                txtViewCode.Text = customViewInfo.ViewCode;
                icmbViewProperty.EditValue = customViewInfo.ViewProperty;
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbDataFieldSetting, customViewInfo.SystemDataFields);
                txtMainTable.Text = CustomTableContract.GetFullName(customViewInfo.TableId);
                txtMainTable.Tag = CustomTableContract.GetCommonNode(customViewInfo.TableId);
                txtTooltip.Text = customViewInfo.ToolTip;
                txtNotes.Text = customViewInfo.Notes;

                lstAssociatedTables.Items.Clear();
                selectedCustomViewAndTableInfos.Clear();
                IList<CommonNode> commonNodes = new List<CommonNode>();
                IList<CustomViewAndTableInfo> customViewAndTableInfos = CustomViewContract.GetAssociatedTables(commonNode.NodeId);
                foreach (var customViewAndTableInfo in customViewAndTableInfos)
                {
                    selectedCustomViewAndTableInfos.Add(customViewAndTableInfo.TableId, customViewAndTableInfo);
                    commonNodes.Add(GetCommonNode(customViewAndTableInfo));
                }
                lstAssociatedTables.Items.AddRange(commonNodes.ToArray());
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
            txtViewLogicalName.Text = string.Empty;
            txtViewPhysicalName.Text = string.Empty;
            txtViewCode.Text = string.Empty;
            icmbViewProperty.SelectedIndex = 0;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataFieldSetting);
            txtMainTable.Text = string.Empty;
            txtTooltip.Text = string.Empty;
            txtNotes.Text = string.Empty;
            if (!txtViewLogicalName.ReadOnly)
            {
                txtViewLogicalName.Focus();
            }
            lstAssociatedTables.Items.Clear();
            if (selectedCustomViewAndTableInfos != null)
            {
                selectedCustomViewAndTableInfos.Clear();
            }
            TreeNodeId = 0;
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;

            CustomViewInfo customViewInfo = GetModelInfo();
            result = ValidationHelper.Validate<CustomViewInfo>(customViewInfo, out warning);
            if (result)
            {
                CommonNode commonNode = txtMainTable.Tag as CommonNode;
                if (commonNode == null)
                {
                    result = false;
                    warning = "主数据表未设置。";
                }
                if (result && lstAssociatedTables.Items.Count == 0)
                {
                    result = false;
                    warning = "从数据表不能为空。";
                }                
            }

            return result;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取视图的信息
        /// </summary>
        /// <returns></returns>
        public CustomViewInfo GetModelInfo()
        {
            CommonNode commonNode = txtMainTable.Tag as CommonNode;
            if (commonNode == null)
            {
                throw new ArgumentNullException("主数据表未设置。");
            }
            CustomViewInfo customViewInfo = new CustomViewInfo()
            {
                ViewId = TreeNodeId,
                ViewName = txtViewLogicalName.Text.Trim(),
                ViewCode = txtViewCode.Text.Trim(),
                ViewProperty = Convert.ToByte(icmbViewProperty.EditValue),
                SystemDataFields = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDataFieldSetting),
                TableId = commonNode.NodeId,
                ToolTip = txtTooltip.Text.Trim(),
                Notes = txtNotes.Text.Trim()
            };

            return customViewInfo;
        }

        /// <summary>
        /// 获得视图与表
        /// </summary>
        /// <returns></returns>
        public IList<CustomViewAndTableInfo> GetCustomViewAndTableInfos()
        {
            IList<CustomViewAndTableInfo> customViewAndTableInfos = new List<CustomViewAndTableInfo>();
            int index = 0;
            foreach (KeyValuePair<decimal, CustomViewAndTableInfo> keyValue in selectedCustomViewAndTableInfos)
            {
                keyValue.Value.Sorting = index;
                customViewAndTableInfos.Add(keyValue.Value);
                index++;
            }

            return customViewAndTableInfos;
        }

        #endregion

        /// <summary>
        /// 构建表关系对象
        /// </summary>
        /// <param name="customViewAndTableInfo"></param>
        /// <returns></returns>
        private CommonNode GetCommonNode(CustomViewAndTableInfo customViewAndTableInfo)
        {
            string logicalName = CustomTableContract.GetTableLogicalName(customViewAndTableInfo.TableId);
            string nodeName = string.Format("[{0}][{1}][{2}][{0}].[{3}]=[{3}]",
                UserEnumHelper.GetEnumText((TableRelation)customViewAndTableInfo.TableRelation), UserEnumHelper.GetEnumText((TableJoin)customViewAndTableInfo.TableJoin),
                    logicalName, UserEnumHelper.GetEnumText((DataFieldRelation)customViewAndTableInfo.PrimaryDataField));

            CommonNode commonNode = new CommonNode(customViewAndTableInfo.TableId, customViewAndTableInfo.ViewId, nodeName);

            return commonNode;
        }
    }
}
