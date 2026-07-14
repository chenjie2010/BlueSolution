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
using AppFramework.Core;
using AppFramework.Core.ClientConfig;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.DataConvertionModule;
using Blue.WCFContracts;
using Blue.Model.UserModule;
using Blue.Model.DataConvertionModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class RemoteDataModule : UserControl, ITreeNodeShow
    {
        #region 私有变量

        #endregion

        #region 属性

        /// <summary>
        /// 远程交换契约
        /// </summary>
        public IRemoteDataContract RemoteDataContract
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库契约
        /// </summary>
        public ICustomDatabaseContract CustomDatabaseContract
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RemoteDataModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbRemoteProperty, typeof(RemoteProperty));
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteDataModule_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 远程数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtParentDatabaseName_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            RemoteDatabaseForm frmRemoteDatabase = new RemoteDatabaseForm();
            frmRemoteDatabase.RemoteServerValue = btxtParentDatabaseName.Tag as RemoteServer;
            frmRemoteDatabase.RemoteSereverConfrimed = (remoteServer) =>
                {
                    btxtParentDatabaseName.Tag = remoteServer;
                    CommonNode commonNode = remoteServer.Tag as CommonNode;
                    btxtParentDatabaseName.Text = commonNode.NodeName;
                };
            frmRemoteDatabase.ShowDialog();
        }

        /// <summary>
        /// 本地数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtDatabaseName_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            ExtendedTreeSelectedItemsForm frmExtendedTreeSelectedItems = new ExtendedTreeSelectedItemsForm();
            frmExtendedTreeSelectedItems.Text = "数据库选择";
            frmExtendedTreeSelectedItems.ToolTip = "提示：只能选择数据库类型的节点。";
            frmExtendedTreeSelectedItems.TreeDropdownHandler = new DatabaseDropdownList(CustomDatabaseContract);
            frmExtendedTreeSelectedItems.NodeSelected = delegate (CommonNode node)
            {
                if (node != null)
                {
                    btxtDatabaseName.Text = node.NodeName;
                    btxtDatabaseName.Tag = node;
                }
                else
                {
                    btxtDatabaseName.Text = string.Empty;
                    btxtDatabaseName.Tag = null;
                }
            };
            frmExtendedTreeSelectedItems.ShowDialog();
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
                txtRemoteDataCode.Text = value;
            }
            get
            {
                return txtRemoteDataCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtRemoteDataName.ReadOnly = readOnly;            
            ccmbRemoteProperty.ReadOnly = readOnly;
            btxtDatabaseName.ReadOnly = readOnly;
            btxtParentDatabaseName.ReadOnly = readOnly;           
            txtNotes.ReadOnly = readOnly;            
            if (!txtRemoteDataName.ReadOnly)
            {
                txtRemoteDataName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                TreeNodeId = commonNode.NodeId;
                RemoteDataInfo remoteDataInfo = RemoteDataContract.GetModelInfo(commonNode.NodeId);
                if (remoteDataInfo != null)
                {
                    txtRemoteDataName.Text = remoteDataInfo.RemoteDataName;
                    txtRemoteDataCode.Text = remoteDataInfo.RemoteDataCode;
                    UserControlHelper.SetCheckedComboBoxEditItems(ccmbRemoteProperty, remoteDataInfo.RemoteProperty);
                    CommonNode node = CustomDatabaseContract.GetCommonNode(remoteDataInfo.DatabaseId);
                    btxtDatabaseName.Text = node.NodeName;
                    btxtDatabaseName.Tag = node;
                    txtNotes.Text = remoteDataInfo.Notes;
                    CommonNode databaseNode = null;
                    try
                    {
                        IRemoteServerContract remoteServerContract = RemoteChannelFactory.CreateRemoteServerContract(remoteDataInfo.RemoteAddress, CurrentConfig.Instance.Port);
                        databaseNode = remoteServerContract.GetDatabase(remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, remoteDataInfo.RemoteDatabaseId);
                        btxtParentDatabaseName.Text = databaseNode.NodeName;
                    }
                    catch
                    {
                        btxtParentDatabaseName.Text = "远程服务器链接失败。";
                    }
                    btxtParentDatabaseName.Tag = new RemoteServer(remoteDataInfo.RemoteAddress, remoteDataInfo.RemoteUserName, remoteDataInfo.RemotePassword, databaseNode);
                }
                else
                {
                    ClearModelInfo();
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 清除界面数据
        /// </summary>
        public void ClearModelInfo()
        {
            txtRemoteDataName.Text = string.Empty;
            txtRemoteDataCode.Text = string.Empty;
            btxtParentDatabaseName.Text = string.Empty;
            btxtParentDatabaseName.Tag = null;
            btxtDatabaseName.Text = string.Empty;
            btxtDatabaseName.Tag = null;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbRemoteProperty);
            txtNotes.Text = string.Empty;            
            if (!txtRemoteDataName.ReadOnly)
            {
                txtRemoteDataName.Focus();
            }
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;

            if (btxtParentDatabaseName.Tag == null)
            {
                warning = "请设置远程数据库。";
                return false;
            }
            if (btxtDatabaseName.Tag == null)
            {
                warning = "请设置本地数据库。";
                return false;
            }
            RemoteDataInfo remoteDataInfo = GetModelInfo();
            result = ValidationHelper.Validate<RemoteDataInfo>(remoteDataInfo, out warning);

            return result;
        }
        
        #endregion

        #region 公有方法

        /// <summary>
        /// 获取远程交换的信息
        /// </summary>
        /// <returns></returns>
        public RemoteDataInfo GetModelInfo()
        {
            if (btxtParentDatabaseName.Tag == null)
            {
                throw new ArgumentException("请设置远程数据库。");
            }
            if (btxtDatabaseName.Tag == null)
            {
                throw new ArgumentException("请设置本地数据库。");
            }
            RemoteServer remoteServer = btxtParentDatabaseName.Tag as RemoteServer;
            CommonNode remoteNode = remoteServer.Tag as CommonNode;
            CommonNode node = btxtDatabaseName.Tag as CommonNode;
            RemoteDataInfo remoteDataInfo = new RemoteDataInfo()
            {
                RemoteDataId = TreeNodeId,
                DatabaseId  = node.NodeId,
                RemoteDataName = txtRemoteDataName.Text.Trim(),
                RemoteDataCode = txtRemoteDataCode.Text.Trim(),
                RemoteProperty = UserControlHelper.GetCheckedComboBoxEditItems(ccmbRemoteProperty),
                RemoteAddress = remoteServer.RemoteAddress,
                RemoteUserName = remoteServer.RemoteUserName,
                RemotePassword = remoteServer.RemotePassword,
                RemoteDatabaseId = remoteNode.NodeId,
                Notes = txtNotes.Text.Trim()
            };

            return remoteDataInfo;
        }

        #endregion

        #region 私有方法

        

        #endregion

    }
}
