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
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class InterfaceModule : UserControl, ITreeNodeShow
    {
        #region 私有常量

        /// <summary>
        /// 最大标识符长度
        /// </summary>
        private const int MAX_INTERFACE_INDENTFIER_LENGTH = 32;

        #endregion

        #region 属性

        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 接口契约
        /// </summary>
        public ICustomInterfaceContract CustomInterfaceContract
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
        /// 表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
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

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InterfaceModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbTableType, typeof(FormType));
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InterfaceModule_Load(object sender, EventArgs e)
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
                        ExtendedTreeSelectedItemsForm frmExtendedTreeSelectedItems = new ExtendedTreeSelectedItemsForm();
                        frmExtendedTreeSelectedItems.Text = "组合表选择";
                        frmExtendedTreeSelectedItems.ToolTip = "提示：只能选择组合表类型的节点。";
                        frmExtendedTreeSelectedItems.TreeDropdownHandler = new CombinedTableDropdownList(CustomGroupContract, CombinedTableContract);
                        frmExtendedTreeSelectedItems.NodeSelected = delegate (CommonNode node)
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
                        frmExtendedTreeSelectedItems.ShowDialog();
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
        /// 自动生成标识符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkAutoGenerate_Click(object sender, EventArgs e)
        {
            GenerateIdentifier();
        }
        
        /// <summary>
        /// 验证者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtUserName_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            UserListForm frmUserList = new UserListForm();
            frmUserList.GetIdentifier = (userId) =>
            {
                if (userId > 0)
                {
                    CommonUserInfo commonUserInfo = UserAccountContract.GetCommonUserInfo(userId);
                    if (commonUserInfo != null)
                    {
                        btxtUserName.Text = GetHander(commonUserInfo.UserActualName, commonUserInfo.UserName);
                        btxtUserName.Tag = commonUserInfo;
                    }
                    else
                    {
                        btxtUserName.Text = string.Empty;
                        btxtUserName.Tag = null;
                    }
                }
            };
            frmUserList.ShowDialog();
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
                txtInterfaceCode.Text = value;
            }
            get
            {
                return txtInterfaceCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtInterfaceName.ReadOnly = readOnly;
            icmbTableType.ReadOnly = readOnly;
            btxtTableName.ReadOnly = readOnly;
            btxtUserName.ReadOnly = readOnly;            
            chkUserTypeContained.ReadOnly = readOnly;
            chkDepContained.ReadOnly = readOnly;
            chkActived.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;            
            if (!txtInterfaceName.ReadOnly)
            {
                txtInterfaceName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomInterfaceInfo customInterfaceInfo = CustomInterfaceContract.GetModelInfo(commonNode.NodeId);
            if (customInterfaceInfo != null)
            {
                txtInterfaceName.Text = customInterfaceInfo.InterfaceName;
                txtInterfaceCode.Text = customInterfaceInfo.InterfaceCode;
                icmbTableType.EditValue = customInterfaceInfo.TableType;
                FormType formType = (FormType)customInterfaceInfo.TableType;
                switch (formType)
                {
                    case FormType.CombinedTable:
                        btxtTableName.Text = CombinedTableContract.GetFullName(customInterfaceInfo.CombinedTableId);
                        btxtTableName.Tag = CombinedTableContract.GetCommonNode(customInterfaceInfo.CombinedTableId);
                        break;

                    case FormType.Table:
                        btxtTableName.Text = CustomTableContract.GetFullName(customInterfaceInfo.TableId);
                        btxtTableName.Tag = CustomTableContract.GetCommonNode(customInterfaceInfo.TableId);
                        break;
                }
                txtInterfaceIdentifier.Text = customInterfaceInfo.InterfaceIdentifier;
                if (!DataConvertionHelper.IsNullValue(customInterfaceInfo.UserId))
                {
                    CommonUserInfo commonUserInfo = UserAccountContract.GetCommonUserInfo(customInterfaceInfo.UserId);
                    btxtUserName.Text = GetHander(commonUserInfo.UserActualName, commonUserInfo.UserName);
                    btxtUserName.Tag = commonUserInfo;
                }
                chkUserTypeContained.Checked = customInterfaceInfo.UserTypeContained;
                chkDepContained.Checked = customInterfaceInfo.DepContained;
                chkActived.Checked = customInterfaceInfo.Actived;
                txtNotes.Text = customInterfaceInfo.Notes;
                hlnkAutoGenerate.Enabled = false;
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
            txtInterfaceName.Text = string.Empty;
            txtInterfaceCode.Text = string.Empty;
            icmbTableType.SelectedIndex = 0;
            btxtTableName.Text = string.Empty;
            btxtTableName.Tag = null;            
            btxtUserName.Text = string.Empty;
            btxtUserName.Tag = null;
            chkUserTypeContained.Checked = false;
            chkDepContained.Checked = false;
            chkActived.Checked = false;
            txtNotes.Text = string.Empty;
            hlnkAutoGenerate.Enabled = true;
            GenerateIdentifier();
            if (!txtInterfaceName.ReadOnly)
            {
                txtInterfaceName.Focus();
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
            if (btxtUserName.Tag == null)
            {
                warning = "请设置验证账号。";
                return false;
            }
            CustomInterfaceInfo customInterfaceInfo = GetModelInfo();
            result = ValidationHelper.Validate<CustomInterfaceInfo>(customInterfaceInfo, out warning);

            return result;
        }
        
        #endregion

        #region 公有方法

        /// <summary>
        /// 获取接口的信息
        /// </summary>
        /// <returns></returns>
        public CustomInterfaceInfo GetModelInfo()
        {
            decimal tableId = decimal.MinValue;
            decimal combinedTableId = decimal.MinValue;            
            if (btxtTableName.Tag == null)
            {
                throw new ArgumentException("请设置表格名称。");
            }
            CommonNode commonNode = btxtTableName.Tag as CommonNode;
            byte tableTypeValue = Convert.ToByte(icmbTableType.EditValue);
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
            decimal userId = decimal.MinValue;
            if (btxtUserName.Tag != null)
            {
                CommonUserInfo commonUserInfo = btxtUserName.Tag as CommonUserInfo;
                if (commonUserInfo != null)
                {
                    userId = commonUserInfo.UserId;
                }
            }
            if (DataConvertionHelper.IsNullValue(userId))
            {
                throw new ArgumentException("请设置验证账号。");
            }
            CustomInterfaceInfo dataAuditingInfo = new CustomInterfaceInfo()
            {
                InterfaceId = TreeNodeId,
                InterfaceName = txtInterfaceName.Text.Trim(),
                InterfaceCode = txtInterfaceCode.Text.Trim(),
                InterfaceIdentifier = txtInterfaceIdentifier.Text.Trim(),
                UserId = userId,
                TableId = tableId,
                CombinedTableId = combinedTableId,                
                TableType = tableTypeValue,
                UserTypeContained = chkUserTypeContained.Checked,
                DepContained = chkDepContained.Checked,
                Actived = chkActived.Checked,
                Notes = txtNotes.Text.Trim()
            };

            return dataAuditingInfo;
        }
       
        /// <summary>
        /// 获得标识符
        /// </summary>
        private void GenerateIdentifier()
        {
            string id = Guid.NewGuid().ToString("D");
            int pos = id.LastIndexOf('-');
            if (pos > MAX_INTERFACE_INDENTFIER_LENGTH)
            {
                pos = MAX_INTERFACE_INDENTFIER_LENGTH;
            }
            string identifier = id.Substring(0, pos);
            bool result = false;
            do
            {
                result = CustomInterfaceContract.IsExistedIdentifier(identifier);
                if (!result)
                {
                    txtInterfaceIdentifier.Text = identifier;
                }
            } while (result);
        }

        #endregion

    }
}
