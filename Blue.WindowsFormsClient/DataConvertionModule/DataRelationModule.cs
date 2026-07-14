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
using Blue.WCFContracts.DataConvertionModule;
using Blue.WCFContracts.SystemModule;
using Blue.Model.UserModule;
using Blue.Model.DataConvertionModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class DataRelationModule : UserControl, ITreeNodeShow
    {
        #region 私有变量

        #endregion

        #region 属性
        
        /// <summary>
        /// 转表契约
        /// </summary>
        public IDataRelationContract DataRelationContract
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
        public DataRelationModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbDataRelationType, typeof(DataRelationType));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataRelationProperty, typeof(DataRelationProperty));
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataRelationModule_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 源数据库选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtParentDatabaseName_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            SetDatabaseName(btxtParentDatabaseName);
        }

        /// <summary>
        /// 目标数据库选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtDatabaseName_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            SetDatabaseName(btxtDatabaseName);
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
                txtRelationCode.Text = value;
            }
            get
            {
                return txtRelationCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtRelationName.ReadOnly = readOnly;
            icmbDataRelationType.ReadOnly = readOnly;
            ccmbDataRelationProperty.ReadOnly = readOnly;
            btxtParentDatabaseName.ReadOnly = readOnly;            
            btxtDatabaseName.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;            
            if (!txtRelationName.ReadOnly)
            {
                txtRelationName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            DataRelationInfo dataRelationInfo = DataRelationContract.GetModelInfo(commonNode.NodeId);
            if (dataRelationInfo != null)
            {
                txtRelationName.Text = dataRelationInfo.RelationName;
                txtRelationCode.Text = dataRelationInfo.RelationCode;
                icmbDataRelationType.EditValue = dataRelationInfo.DataRelationType;
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbDataRelationProperty, dataRelationInfo.DataRelationProperty);
                CommonNode parentCommonNode = CustomDatabaseContract.GetCommonNode(dataRelationInfo.ParentDatabaseId);
                btxtParentDatabaseName.Text = parentCommonNode.NodeName;
                btxtParentDatabaseName.Tag = parentCommonNode;
                CommonNode node = CustomDatabaseContract.GetCommonNode(dataRelationInfo.DatabaseId);
                btxtDatabaseName.Text = node.NodeName;
                btxtDatabaseName.Tag = node;
                txtNotes.Text = dataRelationInfo.Notes;
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
            txtRelationName.Text = string.Empty;
            txtRelationCode.Text = string.Empty;
            icmbDataRelationType.SelectedIndex = 0;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataRelationProperty);            
            btxtParentDatabaseName.Text = string.Empty;
            btxtParentDatabaseName.Tag = null;            
            btxtDatabaseName.Text = string.Empty;
            btxtDatabaseName.Tag = null;
            txtNotes.Text = string.Empty;            
            if (!txtRelationName.ReadOnly)
            {
                txtRelationName.Focus();
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
                warning = "请设置源数据库名称。";
                return false;
            }
            if (btxtDatabaseName.Tag == null)
            {
                warning = "请设置目标数据库名称。";
                return false;
            }
            DataRelationInfo dataRelationInfo = GetModelInfo();
            result = ValidationHelper.Validate<DataRelationInfo>(dataRelationInfo, out warning);

            return result;
        }
        
        #endregion

        #region 公有方法

        /// <summary>
        /// 获取接口的信息
        /// </summary>
        /// <returns></returns>
        public DataRelationInfo GetModelInfo()
        {            
            if (btxtParentDatabaseName.Tag == null)
            {
                throw new ArgumentException("请设置源数据库名称。");
            }
            if (btxtDatabaseName.Tag == null)
            {
                throw new ArgumentException("请设置目标数据库名称。");
            }
            CommonNode commonNode = btxtParentDatabaseName.Tag as CommonNode;
            CommonNode node = btxtDatabaseName.Tag as CommonNode;
            DataRelationInfo dataAuditingInfo = new DataRelationInfo()
            {
                RelationId = TreeNodeId,
                RelationName = txtRelationName.Text.Trim(),
                RelationCode = txtRelationCode.Text.Trim(),
                DataRelationType = Convert.ToByte(icmbDataRelationType.EditValue),
                DataRelationProperty = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDataRelationProperty),
                DatabaseId = node.NodeId,
                ParentDatabaseId = commonNode.NodeId,
                Notes = txtNotes.Text.Trim()
            };

            return dataAuditingInfo;
        }


        #endregion        

        #region 私有方法

        /// <summary>
        /// 设置数据库
        /// </summary>
        /// <param name="buttonEdit"></param>
        private void SetDatabaseName(DevExpress.XtraEditors.ButtonEdit buttonEdit)
        {
            ExtendedTreeSelectedItemsForm frmExtendedTreeSelectedItems = new ExtendedTreeSelectedItemsForm();
            frmExtendedTreeSelectedItems.Text = "数据库选择";
            frmExtendedTreeSelectedItems.ToolTip = "提示：只能选择数据库类型的节点。";
            frmExtendedTreeSelectedItems.TreeDropdownHandler = new DatabaseDropdownList(CustomDatabaseContract);
            frmExtendedTreeSelectedItems.NodeSelected = delegate (CommonNode node)
            {
                if (node != null)
                {
                    buttonEdit.Text = node.NodeName;
                    buttonEdit.Tag = node;
                }
                else
                {
                    buttonEdit.Text = string.Empty;
                    buttonEdit.Tag = null;
                }
            };
            frmExtendedTreeSelectedItems.ShowDialog();
        }

        #endregion
    }
}
