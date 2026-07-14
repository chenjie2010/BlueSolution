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
    public partial class CombinedTableModule : UserControl, ITreeNodeShow
    {
        #region 私有变量
        
        #endregion

        #region 属性

        /// <summary>
        /// 视图契约
        /// </summary>
        public ICombinedTableContract CombinedTableContract
        {
            get; set;
        }

        /// <summary>
        /// 数据库表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            get;set;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public CombinedTableModule()
        {
            InitializeComponent();           
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbDataWarehouse, typeof(DataWarehouse));
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
        /// 表选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleAssociatedTables_OpenLink(object sender, OpenLinkEventArgs e)
        {            
            CommonListItemsForm frmCommonListItems = new CommonListItemsForm();
            frmCommonListItems.Text = "数据表选择";
            frmCommonListItems.ToolTip = "请选择数据表";
            frmCommonListItems.CreateItmes = delegate (ListBoxControl lstItems)
            {
                DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
                frmDataTableItems.TableFilter = TableFilter.MasterSlaveTable;
                frmDataTableItems.DataWarehouseId = Convert.ToByte(icmbDataWarehouse.EditValue);
                frmDataTableItems.Text = "数据表选择";
                frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
                frmDataTableItems.NodeSelected = delegate (CommonNode commonNode) {
                    if (commonNode != null)
                    {
                        foreach (object obj in lstItems.Items)
                        {
                            CommonNode node = obj as CommonNode;
                            if (node.NodeId == commonNode.NodeId)
                            {
                                MessageBox.Show("不能重复选择数据表。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        CommonNode newCommonNode = new CommonNode(commonNode.NodeId, TreeNodeId, CustomTableContract.GetFullName(commonNode.NodeId));
                        if (lstItems.Items.Count == 0)
                        {
                            DataTableType dataTableType = (DataTableType)CustomTableContract.GetDataTableType(commonNode.NodeId);
                            if (dataTableType != DataTableType.PrimaryTable && dataTableType != DataTableType.MasterSlaveTable)
                            {
                                MessageBox.Show("组合表的第一个表必须是主表或者主从表。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        lstItems.Items.Add(newCommonNode);
                    }
                };
                frmDataTableItems.ShowDialog();                
            };
            frmCommonListItems.GetItems = delegate (IList<CommonNode> nodes)
            {
                lstAssociatedTables.Items.Clear();
                if (nodes != null && nodes.Count > 0)
                {
                    lstAssociatedTables.Items.AddRange(nodes.ToArray());                                   
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

        /// <summary>
        /// 切换数据仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbDataWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstAssociatedTables.Items.Clear();
            if (TreeNodeId > 0)
            {
                IList<CommonNode> commonNodes = CombinedTableContract.GetTables(TreeNodeId);
                lstAssociatedTables.Items.AddRange(commonNodes.ToArray());
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
                txtCombinedTableCode.Text = value;
            }
            get
            {
                return txtCombinedTableCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtCombinedTableName.ReadOnly = readOnly;
            txtCombinedTableCode.ReadOnly = readOnly;
            icmbDataWarehouse.ReadOnly = readOnly;
            lstAssociatedTables.Enabled = !readOnly;
            hleAssociatedTables.Enabled = !readOnly;
            txtTooltip.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            hleAssociatedTables.ReadOnly = readOnly;
            if (!txtCombinedTableName.ReadOnly)
            {
                txtCombinedTableName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CombinedTableInfo combinedTableInfo = CombinedTableContract.GetModelInfo(commonNode.NodeId);
            if (combinedTableInfo != null)
            {
                txtCombinedTableName.Text = combinedTableInfo.CombinedTableName;               
                txtCombinedTableCode.Text = combinedTableInfo.CombinedTableCode;
                icmbDataWarehouse.EditValue = combinedTableInfo.DataWarehouseId;
                txtTooltip.Text = combinedTableInfo.ToolTip;
                txtNotes.Text = combinedTableInfo.Notes;
                lstAssociatedTables.Items.Clear();
                IList<CommonNode> commonNodes = CombinedTableContract.GetTables(commonNode.NodeId);
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
            txtCombinedTableName.Text = string.Empty;
            txtCombinedTableCode.Text = string.Empty;
            icmbDataWarehouse.SelectedIndex = 0;
            txtTooltip.Text = string.Empty;
            txtNotes.Text = string.Empty;
            if (!txtCombinedTableName.ReadOnly)
            {
                txtCombinedTableName.Focus();
            }
            lstAssociatedTables.Items.Clear();            
            TreeNodeId = 0;
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;

            CombinedTableInfo combinedTableInfo = GetModelInfo();            
            result = ValidationHelper.Validate<CombinedTableInfo>(combinedTableInfo, out warning);
            if (result)
            {
                if (lstAssociatedTables.Items.Count == 0)
                {
                    result = false;
                    warning = "数据表不能为空。";
                }
                CommonNode commonNode = lstAssociatedTables.Items[0] as CommonNode;
                DataTableType dataTableType = (DataTableType)CustomTableContract.GetDataTableType(commonNode.NodeId);
                if (dataTableType != DataTableType.PrimaryTable && dataTableType != DataTableType.MasterSlaveTable)
                {
                    result = false;
                    warning = "组合表的第一个表必须是主表或者主从表。";
                }
            }

            return result;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取的信息
        /// </summary>
        /// <returns></returns>
        public CombinedTableInfo GetModelInfo()
        {
            CombinedTableInfo combinedTableInfo = new CombinedTableInfo()
            {
                CombinedTableId = TreeNodeId,
                CombinedTableName = txtCombinedTableName.Text.Trim(),
                CombinedTableCode = txtCombinedTableCode.Text.Trim(),
                DataWarehouseId = Convert.ToByte(icmbDataWarehouse.EditValue),
                IsLeaf = false,
                ToolTip = txtTooltip.Text.Trim(),
                Notes = txtNotes.Text.Trim()
            };
            
            return combinedTableInfo;
        }

        /// <summary>
        /// 获得组合表
        /// </summary>
        /// <returns></returns>
        public IList<CombinedTableRelationInfo> GetCombinedTableRelationInfos()
        {
            int sorting = 0;
            IList<CombinedTableRelationInfo> combinedTableRelationInfos = new List<CombinedTableRelationInfo>();
            foreach (var obj in lstAssociatedTables.Items)
            {
                CommonNode commonNode = obj as CommonNode;
                combinedTableRelationInfos.Add(new CombinedTableRelationInfo(TreeNodeId, commonNode.NodeId, sorting++));
            }
            
            return combinedTableRelationInfos;
        }

        #endregion        
    }
}
