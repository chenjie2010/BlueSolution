using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.WinFormsControls;
using AppFramework.Reference.WCFLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;
using AppFramework.WinFormsLibrary;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class RoleSettingForm : Form
    {
        #region  私有变量

        private ICommonNodeContract commonNodeDatabaseContract;
        private ICommonNodeContract commonNodeMenuContract;
        private TreeViewHandler<TreeNode> trvDatabaseHandler;
        private TreeViewHandler<TreeNode> trvSystemMenuHandler;
        private TreeViewHandler<TreeNode> trvMenuHandler;
        private Dictionary<decimal, RoleAndDataFieldInfo> dicRoleAndDataFieldInfo;
        private Dictionary<decimal, RoleAndBusinessInfo> dicRoleAndBusinessInfo;
        private List<decimal> lstPrints = null;

        private bool isLoading = false;
        private CustomRoleInfo customRoleInfo = null;
        private Int64 menuAuthority = 0;
        private Int64 menuSubAuthority = 0;
        private Int64 systemAuthority = 0;
        private Int64 systemSubAuthority = 0;

        #endregion

        #region 契约接口

        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomMenuContract customMenuContract;
        private readonly ICustomBusinessContract customBusinessContract;
        private readonly ICustomDatabaseContract customDatabaseContract;
        private readonly ICustomCategoryContract customCategoryContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomPrintContract customPrintContract;

        #endregion

        #region 属性

        /// <summary>
        /// 角色编号
        /// </summary>
        public decimal RoleId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleSettingForm()
        {
            InitializeComponent();
            dicRoleAndDataFieldInfo = new Dictionary<decimal, RoleAndDataFieldInfo>();
            dicRoleAndBusinessInfo = new Dictionary<decimal, RoleAndBusinessInfo>();
            trvDatabaseHandler = new TreeViewHandler<TreeNode>(trvDatabase);
            trvSystemMenuHandler = new TreeViewHandler<TreeNode>(trvSystemMenu);
            trvMenuHandler = new TreeViewHandler<TreeNode>(trvMenu);
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customMenuContract = BusinessChannelFactory.CreateCustomMenuContract();
            customBusinessContract = BusinessChannelFactory.CreateCustomBusinessContract();
            customDatabaseContract = BusinessChannelFactory.CreateCustomDatabaseContract();
            customCategoryContract = BusinessChannelFactory.CreateCustomCategoryContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customPrintContract = BusinessChannelFactory.CreateCustomPrintContract();

            gcTop.Enabled = false;
            gcSystemDataField.Enabled = false;
            gcMain.Enabled = false;
            UserControlHelper.InitImageComboBoxEdit(icmbDataAuthorityType, typeof(DataAuthorityType));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(SystemDataField));
            UserControlHelper.InitImageComboBoxEdit(icmbBatchSetting, typeof(DataFieldAuthority));
            UserControlHelper.InitRepositoryItemImageComboBox(rcmbDataFieldAuthority, typeof(DataFieldAuthority));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbMenuAuthority, typeof(MenuAuthority));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbMenuSubAuthority, typeof(MenuSubAuthority));
        }

        #endregion

        #region 窗体和控件的方法        

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoleSettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                customRoleInfo = customRoleContract.GetModelInfo(RoleId);
                systemAuthority = customRoleInfo.SystemAuthority;
                systemSubAuthority = customRoleInfo.SystemSubAuthority;
                menuAuthority = customRoleInfo.MenuAuthority;
                menuSubAuthority = customRoleInfo.MenuSubAuthority;
                IList<CommonNode> commonNodes = customRoleContract.GetPrintsByRoleId(RoleId);
                btxtPrint.Tag = commonNodes;
                btxtPrint.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
                LoadDatabase();
                LoadSystemMenu();
                LoadBusinessMenu();
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbMenuAuthority, customRoleInfo.MenuAuthority);
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbMenuSubAuthority, customRoleInfo.MenuSubAuthority);
                btnApply.Enabled = false;
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 业务权限类型变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbDataAuthorityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataAuthorityType dataAuthorityType = (DataAuthorityType)Convert.ToByte(icmbDataAuthorityType.EditValue);
            SetTableAuthority(dataAuthorityType);
            if (trvDatabase.SelectedNode != null)
            {
                LoadAuthorityData(trvDatabase.SelectedNode);
            }
        }

        /// <summary>
        /// 字段权限批量设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbBatchSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (icmbBatchSetting.SelectedIndex < 0)
            {
                return;
            }
            for (int i = 0; i < gvDataFields.DataRowCount; i++)
            {
                decimal dataFieldId = DataConvertionHelper.GetDecimal(gvDataFields.GetDataRow(i)["DataFieldId"]);
                byte dataFieldAuthority = DataConvertionHelper.GetConvertedByte(icmbBatchSetting.EditValue);
                gvDataFields.GetDataRow(i)["AuthorityType"] = dataFieldAuthority;
                GridViewCellValueChanged(dataFieldId, dataFieldAuthority);
            }
        }

        /// <summary>
        /// 单元格的值发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDataFields_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal dataFieldId = DataConvertionHelper.GetDecimal(gvDataFields.GetDataRow(e.RowHandle)["DataFieldId"]);
            GridViewCellValueChanged(dataFieldId, DataConvertionHelper.GetByte(e.Value));
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Sumbit();
                Cursor = Cursors.Default;
                this.Close();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Sumbit();
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 节点选择之后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvDatabase_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {
                    LoadAuthorityData(e.Node);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 节点展开之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvDatabase_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetDatabaseCommonNodeContract(GetDatabaseNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 节点展开之后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvDatabase_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (e.Node.IsExpanded)
                {
                    e.Node.ImageIndex = 1;
                }
                else
                {
                    e.Node.ImageIndex = 0;
                }
                if (commonNodeDatabaseContract != null && e.Node.Nodes.Count == 1)
                {
                    CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                    if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                    {
                        CommonNode commonNode = e.Node.Tag as CommonNode;
                        IList<CommonNode> commonNodes = null;
                        if (commonNode.NodeType > 0)
                        {
                            commonNodes = commonNodeDatabaseContract.GetChildNodes(commonNode.NodeId, commonNode.NodeType);
                        }
                        else
                        {
                            commonNodes = commonNodeDatabaseContract.GetChildNodes(commonNode.NodeId);
                        }
                        DatabaseNodeType databaseNodeType = GetDatabaseNodeType(e.Node.Level);
                        if (databaseNodeType == DatabaseNodeType.Category)
                        {
                            foreach (CommonNode node in commonNodes)
                            {
                                node.IsLeaf = true;
                            }
                        }
                        trvDatabaseHandler.LoadPartialNodes(e.Node, commonNodes);
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 菜单节点选择之后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {
                    Cursor = Cursors.WaitCursor;
                    dicRoleAndBusinessInfo.Clear();
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    MenuNodeType combinedNodeType = GetMenuNodeType(e.Node.Level);
                    if (combinedNodeType == MenuNodeType.Class)
                    {
                        gcBusiness.Enabled = true;
                        DataSet ds = customRoleContract.GetBusinessAuthority(RoleId, commonNode.NodeId);
                        gcMenu.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        gcBusiness.Enabled = false;
                    }
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 菜单节点展开之后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvMenu_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (e.Node.IsExpanded)
                {
                    e.Node.ImageIndex = 1;
                }
                else
                {
                    e.Node.ImageIndex = 0;
                }
                if (commonNodeMenuContract != null && e.Node.Nodes.Count == 1)
                {
                    CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                    if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                    {
                        CommonNode commonNode = e.Node.Tag as CommonNode;
                        MenuNodeType treeNodeType = GetMenuNodeType(e.Node.Level);
                        IList<CommonNode> commonNodes = commonNodeMenuContract.GetChildNodes(commonNode.NodeId);
                        if (treeNodeType == MenuNodeType.Catalog)
                        {
                            foreach (CommonNode node in commonNodes)
                            {
                                node.IsLeaf = true;
                            }
                        }
                        trvMenuHandler.LoadPartialNodes(e.Node, commonNodes);
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 菜单节点展开之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvMenu_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetMenuCommonNodeContract(GetMenuNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 系统字段设置发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccmbDataFieldSetting_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                btnApply.Enabled = true;
            }
        }

        /// <summary>
        /// 表的设置发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccmbTableAuthority_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                btnApply.Enabled = true;
            }
        }

        /// <summary>
        /// 业务单元格数据发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBusiness_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow dataRow = gvBusiness.GetDataRow(e.RowHandle);
            decimal businessId = DataConvertionHelper.GetDecimal(dataRow["BusinessId"]);

            RoleAndBusinessInfo roleAndBusinessInfo = new RoleAndBusinessInfo(RoleId, businessId, false, false, DateTime.MinValue, DateTime.MinValue, string.Empty);
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gvBusiness.Columns)
            {
                switch (column.FieldName.ToLower())
                {
                    case "businessenabled":
                        roleAndBusinessInfo.BusinessEnabled = DataConvertionHelper.GetBoolean(dataRow["BusinessEnabled"]);
                        break;

                    case "thirdmodeenabled":
                        roleAndBusinessInfo.ThirdModeEnabled = DataConvertionHelper.GetBoolean(dataRow["ThirdModeEnabled"]);
                        break;

                    case "initializeddate":
                        roleAndBusinessInfo.InitializedDate = DataConvertionHelper.GetDateTime(dataRow["InitializedDate"]);
                        break;

                    case "expireddate":
                        roleAndBusinessInfo.ExpiredDate = DataConvertionHelper.GetDateTime(dataRow["ExpiredDate"]);
                        break;

                    case "notes":
                        roleAndBusinessInfo.Notes = DataConvertionHelper.GetString(dataRow["Notes"]);
                        break;
                }
            }
            if (dicRoleAndBusinessInfo.ContainsKey(businessId))
            {
                roleAndBusinessInfo = dicRoleAndBusinessInfo[businessId];
            }
            else
            {
                dicRoleAndBusinessInfo.Add(businessId, roleAndBusinessInfo);
            }
            btnApply.Enabled = true;
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBusiness_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDataFields_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 系统菜单节点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvSystemMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        /// <summary>
        /// 系统菜单选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvSystemMenu_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.ByMouse)
            {
                return;
            }
            Cursor = Cursors.WaitCursor;
            CommonNode commonNode = e.Node.Tag as CommonNode;
            systemAuthority = AuthorityHelper.GetAuthority(systemAuthority, Convert.ToByte(commonNode.NodeId), e.Node.Checked);
            btnApply.Enabled = true;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 菜单权限变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccmbMenuAuthority_EditValueChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            menuAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbMenuAuthority);
            btnApply.Enabled = true;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 菜单子权限发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccmbMenuSubAuthority_EditValueChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            menuSubAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbMenuSubAuthority);
            btnApply.Enabled = true;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 设置打印权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtPrint_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IList<CommonNode> commonNodes = btxtPrint.Tag as IList<CommonNode>;
                MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
                frmMultiSelectedItems.MultiSelectedItemsHandler = new PrintMultiSelectedItems(customGroupContract, customPrintContract, (byte)GroupType.Print, true);
                frmMultiSelectedItems.Text = "打印选择";
                frmMultiSelectedItems.GetTreeNodeListDelegate = (nodes) =>
                {
                    Cursor = Cursors.WaitCursor;
                    btxtPrint.Tag = nodes;
                    btxtPrint.Text = CommonObjHelper.GetCommonNodeNamesWithComma(nodes);
                    if (lstPrints == null)
                    {
                        lstPrints = new List<decimal>();
                    }
                    else
                    {
                        lstPrints.Clear();
                    }
                    foreach (var node in nodes)
                    {
                        lstPrints.Add(node.NodeId);
                    }
                    btnApply.Enabled = true;
                    Cursor = Cursors.Default;
                };
                frmMultiSelectedItems.OperationTip = "提示：请为该角色选择打印业务。";
                frmMultiSelectedItems.SetTokenEidtValues(commonNodes);
                Cursor = Cursors.Default;
                frmMultiSelectedItems.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置表的权限，如果是审核，则多包含：审核、批量申、完全审核
        /// </summary>
        /// <param name="dataAuthorityType"></param>
        private void SetTableAuthority(DataAuthorityType dataAuthorityType)
        {
            ccmbTableAuthority.Properties.Items.Clear();
            switch (dataAuthorityType)
            {
                case DataAuthorityType.Auditing:
                    IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(GridViewAuthority));
                    ccmbTableAuthority.Properties.Items.AddRange(enumItems.ToArray());
                    break;

                case DataAuthorityType.Business:
                    IList<EnumItem> businessEnumItems = UserEnumHelper.GetEnumItems(typeof(GridViewAuthority));
                    foreach (var enumItem in businessEnumItems)
                    {
                        GridViewAuthority gridViewAuthority = (GridViewAuthority)enumItem.Value;
                        if (gridViewAuthority != GridViewAuthority.Auditing && gridViewAuthority != GridViewAuthority.BatchAuditing
                             && gridViewAuthority != GridViewAuthority.CompletelyAuditing)
                        {
                            ccmbTableAuthority.Properties.Items.Add(enumItem);
                        }
                    }
                    break;

                case DataAuthorityType.Query:
                    IList<EnumItem> queryEnumItems = UserEnumHelper.GetEnumItems(typeof(GridViewAuthority));
                    foreach (var enumItem in queryEnumItems)
                    {
                        GridViewAuthority gridViewAuthority = (GridViewAuthority)enumItem.Value;
                        if (gridViewAuthority != GridViewAuthority.Auditing && gridViewAuthority != GridViewAuthority.BatchAuditing
                             && gridViewAuthority != GridViewAuthority.CompletelyAuditing && gridViewAuthority != GridViewAuthority.Add
                             && gridViewAuthority != GridViewAuthority.Move && gridViewAuthority != GridViewAuthority.Import
                             && gridViewAuthority != GridViewAuthority.Export)
                        {
                            ccmbTableAuthority.Properties.Items.Add(enumItem);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        private void Sumbit()
        {
            foreach (KeyValuePair<decimal, RoleAndBusinessInfo> keyValue in dicRoleAndBusinessInfo)
            {
                if (!DataConvertionHelper.IsNullValue(keyValue.Value.InitializedDate) && !DataConvertionHelper.IsNullValue(keyValue.Value.ExpiredDate))
                {
                    if (keyValue.Value.InitializedDate > keyValue.Value.ExpiredDate)
                    {
                        DataTable dataTable = gvBusiness.GridControl.DataSource as DataTable;
                        DataRow[] dataRows = dataTable.Select(string.Format("BusinessId={0}", keyValue.Value.BusinessId));//查询
                        if (dataRows != null && dataRows.Length > 0)
                        {
                            MessageBox.Show(string.Format("{0}的起始日期不能大于截止日期。", DataConvertionHelper.GetString(dataRows[0]["BusinessName"])), "提示信息",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            throw new ArgumentException("业务名称出错。");
                        }
                        return;
                    }
                }
            }

            if (trvDatabase.SelectedNode != null)
            {
                DatabaseNodeType databaseNodeType = GetDatabaseNodeType(trvDatabase.SelectedNode.Level);
                if (databaseNodeType == DatabaseNodeType.Table)
                {
                    CommonNode commonNode = trvDatabase.SelectedNode.Tag as CommonNode;
                    IList<RoleAndDataFieldInfo> roleAndDataFieldInfos = new List<RoleAndDataFieldInfo>();
                    foreach (KeyValuePair<decimal, RoleAndDataFieldInfo> keyValue in dicRoleAndDataFieldInfo)
                    {
                        roleAndDataFieldInfos.Add(keyValue.Value);
                    }
                    Int64 tableAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbTableAuthority);
                    Int64 systemDataFieldAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDataFieldSetting);
                    customRoleContract.Update(RoleId, commonNode.NodeId, Convert.ToByte(icmbDataAuthorityType.EditValue), tableAuthority, systemDataFieldAuthority, roleAndDataFieldInfos);
                }
            }
            if (trvMenu.SelectedNode != null)
            {
                MenuNodeType combinedNodeType = GetMenuNodeType(trvMenu.SelectedNode.Level);
                if (combinedNodeType == MenuNodeType.Class)
                {
                    CommonNode commonNode = trvMenu.SelectedNode.Tag as CommonNode;
                    IList<RoleAndBusinessInfo> roleAndBusinessInfos = new List<RoleAndBusinessInfo>();
                    foreach (KeyValuePair<decimal, RoleAndBusinessInfo> keyValue in dicRoleAndBusinessInfo)
                    {
                        roleAndBusinessInfos.Add(keyValue.Value);
                    }
                    customRoleContract.Update(RoleId, roleAndBusinessInfos);
                }
            }
            if (menuAuthority != customRoleInfo.MenuAuthority || menuSubAuthority != customRoleInfo.MenuSubAuthority
                || systemAuthority != customRoleInfo.SystemAuthority || systemSubAuthority != customRoleInfo.SystemSubAuthority)
            {
                customRoleContract.Update(RoleId, menuAuthority, menuSubAuthority, systemAuthority, systemSubAuthority);
            }
            if (lstPrints != null)
            {
                customRoleContract.UpdatePrints(RoleId, lstPrints);
            }
            btnApply.Enabled = false;
        }

        /// <summary>
        /// 字段权限单元格的值发生变化
        /// </summary>
        /// <param name="dataAuthority"></param>
        /// <param name="dataFieldId"></param>
        private void GridViewCellValueChanged(decimal dataFieldId, byte dataFieldAuthority)
        {
            RoleAndDataFieldInfo roleAndDataFieldInfo = null;
            if (dicRoleAndDataFieldInfo.ContainsKey(dataFieldId))
            {
                roleAndDataFieldInfo = dicRoleAndDataFieldInfo[dataFieldId];
                roleAndDataFieldInfo.AuthorityType = dataFieldAuthority;
            }
            else
            {
                roleAndDataFieldInfo = new RoleAndDataFieldInfo(dataFieldId, RoleId, Convert.ToByte(icmbDataAuthorityType.EditValue), dataFieldAuthority);
                dicRoleAndDataFieldInfo.Add(dataFieldId, roleAndDataFieldInfo);
            }
            btnApply.Enabled = true;
        }

        /// <summary>
        /// 加载数据库
        /// </summary>                        
        private void LoadDatabase()
        {
            if (!DesignMode)
            {
                IList<CommonNode> commonNodes = new List<CommonNode>();
                IList<EnumItem> dataWarehouses = UserEnumHelper.GetEnumItems(typeof(DataWarehouse));
                foreach (EnumItem enumItem in dataWarehouses)
                {
                    commonNodes.Add(new CommonNode(enumItem.Value, decimal.MinValue, enumItem.Text, string.Empty, false));
                }
                trvDatabaseHandler.InitFirstLevelNodes(commonNodes);
            }
        }

        /// <summary>
        /// 加载菜单
        /// </summary>
        private void LoadBusinessMenu()
        {
            if (!DesignMode)
            {
                IList<CommonNode> commonNodes = new List<CommonNode>();
                commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "业务应用菜单", string.Empty, false, (byte)MenuType.Business));
                trvMenuHandler.InitFirstLevelNodes(commonNodes);
            }
        }

        /// <summary>
        /// 获得数据库节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private DatabaseNodeType GetDatabaseNodeType(int level)
        {
            DatabaseNodeType nodeType = DatabaseNodeType.DataWarehouse;

            /* 第一层为数据仓库 第二层为数据库，第三层为分类，第四层为数据表 */
            switch (level)
            {
                case 0:
                    nodeType = DatabaseNodeType.DataWarehouse;
                    break;

                case 1:
                    nodeType = DatabaseNodeType.Database;
                    break;

                case 2:
                    nodeType = DatabaseNodeType.Category;
                    break;

                case 3:
                    nodeType = DatabaseNodeType.Table;
                    break;

                default:
                    throw new ArgumentException("不支持该数据库节点类型。");
            }

            return nodeType;
        }

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="databaseNode"></param>
        private void SetDatabaseCommonNodeContract(DatabaseNodeType databaseNode)
        {
            /* 第一层为数据仓库 第二层为数据库，第三层为分组，第四层为数据表，第五、六层为字段 */
            switch (databaseNode)
            {
                case DatabaseNodeType.DataWarehouse:
                    commonNodeDatabaseContract = null;
                    break;

                case DatabaseNodeType.Database:
                    commonNodeDatabaseContract = customDatabaseContract;
                    break;

                case DatabaseNodeType.Category:
                    commonNodeDatabaseContract = customCategoryContract;
                    break;

                case DatabaseNodeType.Table:
                    commonNodeDatabaseContract = customTableContract;
                    break;
            }
        }

        /// <summary>
        /// 获得菜单节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private MenuNodeType GetMenuNodeType(int level)
        {
            MenuNodeType menuNodeType = MenuNodeType.Category;

            /* (1) 菜单类型 (2) 菜单目录 (3) 菜单分类 */
            switch (level)
            {
                case 0:
                    menuNodeType = MenuNodeType.Category;
                    break;

                case 1:
                    menuNodeType = MenuNodeType.Catalog;
                    break;

                case 2:
                    menuNodeType = MenuNodeType.Class;
                    break;

                default:
                    throw new ArgumentException("不支持该菜单节点类型");

            }

            return menuNodeType;
        }

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="combinedNodeType"></param>
        private void SetMenuCommonNodeContract(MenuNodeType menuNodeType)
        {
            /* (1) 菜单类型 (2) 菜单目录 (3) 菜单分类 */
            switch (menuNodeType)
            {
                case MenuNodeType.Category:
                    commonNodeMenuContract = null;
                    break;

                case MenuNodeType.Catalog:
                case MenuNodeType.Class:
                    commonNodeMenuContract = customMenuContract;
                    break;

                default:
                    throw new ArgumentException("不支持该菜单节点类型");
            }
        }

        /// <summary>
        /// 加载表与字段的权限数据
        /// </summary>
        /// <param name="node"></param>
        private void LoadAuthorityData(TreeNode node)
        {
            Cursor = Cursors.WaitCursor;
            dicRoleAndDataFieldInfo.Clear();
            icmbBatchSetting.SelectedIndex = -1;
            CommonNode commonNode = node.Tag as CommonNode;
            DatabaseNodeType databaseNodeType = GetDatabaseNodeType(node.Level);
            isLoading = true;
            if (databaseNodeType == DatabaseNodeType.Table)
            {
                gcTop.Enabled = true;
                gcSystemDataField.Enabled = true;
                gcMain.Enabled = true;
                RoleAndTableInfo roleAndTableInfo = customRoleContract.GetRoleAndTableInfo(RoleId, commonNode.NodeId, Convert.ToByte(icmbDataAuthorityType.EditValue));
                if (roleAndTableInfo != null)
                {
                    UserControlHelper.SetCheckedComboBoxEditItems(ccmbTableAuthority, roleAndTableInfo.TableAuthority);
                    UserControlHelper.SetCheckedComboBoxEditItems(ccmbDataFieldSetting, roleAndTableInfo.SystemDataFieldAuthority);
                }
                else
                {
                    UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbTableAuthority);
                    UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataFieldSetting);
                }
                DataSet ds = customRoleContract.GetDataFiledAuthority(RoleId, commonNode.NodeId, Convert.ToByte(icmbDataAuthorityType.EditValue));
                gcDataFields.DataSource = ds.Tables[0];
            }
            else
            {
                gcTop.Enabled = false;
                gcSystemDataField.Enabled = false;
                gcMain.Enabled = false;
                UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbTableAuthority);
                UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataFieldSetting);
            }
            isLoading = false;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 加载系统管理菜单
        /// </summary>
        private void LoadSystemMenu()
        {
            if (!DesignMode)
            {
                List<CommonNode> commonNodes = new List<CommonNode>();
                IList<CommonNode> systemCommonNodes = UserEnumHelper.GetCommonNodes(typeof(SystemMenuCategory));
                foreach (CommonNode node in systemCommonNodes)
                {
                    node.IsLeaf = false;
                    commonNodes.Add(node);
                }
                IList<CommonNode> catalogCommonNodes = UserEnumHelper.GetCommonNodes(typeof(SystemMenuAuthority));
                foreach (var catalogCommonNode in catalogCommonNodes)
                {
                    if (catalogCommonNode.NodeId >= (byte)SystemMenuCategory.SysProcessing)
                    {
                        catalogCommonNode.ParentNodeId = (byte)SystemMenuCategory.SysProcessing;
                    }
                    else if (catalogCommonNode.NodeId >= (byte)SystemMenuCategory.SysData)
                    {
                        catalogCommonNode.ParentNodeId = (byte)SystemMenuCategory.SysData;
                    }
                    else if (catalogCommonNode.NodeId >= (byte)SystemMenuCategory.SysBusiness)
                    {
                        catalogCommonNode.ParentNodeId = (byte)SystemMenuCategory.SysBusiness;
                    }
                    else if (catalogCommonNode.NodeId >= (byte)SystemMenuCategory.SysManagement)
                    {
                        catalogCommonNode.ParentNodeId = (byte)SystemMenuCategory.SysManagement;
                    }
                    else if (catalogCommonNode.NodeId >= (byte)SystemMenuCategory.SysDesigner)
                    {
                        catalogCommonNode.ParentNodeId = (byte)SystemMenuCategory.SysDesigner;
                    }
                    else if (catalogCommonNode.NodeId >= (byte)SystemMenuCategory.SysArchitecture)
                    {
                        catalogCommonNode.ParentNodeId = (byte)SystemMenuCategory.SysArchitecture;
                    }
                    else if (catalogCommonNode.NodeId >= (byte)SystemMenuCategory.SysSetting)
                    {
                        catalogCommonNode.ParentNodeId = (byte)SystemMenuCategory.SysSetting;
                    }
                    commonNodes.Add(catalogCommonNode);
                }
                trvSystemMenuHandler.InitFullTreeNodes(commonNodes);
                trvSystemMenuHandler.SetCheckValues(null, customRoleInfo.SystemAuthority);
            }
        }

        #endregion        
    }
}
