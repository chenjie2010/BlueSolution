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
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class AssociationModule : UserControl, ITreeNodeShow
    {
        #region 属性

        /// <summary>
        /// 关联类型契约
        /// </summary>
        public ICustomAssociationContract CustomAssociationContract
        {
            set; get;
        }

        #endregion      

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssociationModule()
        {
            InitializeComponent();
            UserControlHelper.InitImageComboBoxEdit(icmbShowMode, typeof(AssociationShowMode));            
            TreeNodeId = 0;
            SetActiveStatesOfControls(true);
        }

        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationModule_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 超大关联
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSuperAssociationEnabled_CheckedChanged(object sender, EventArgs e)
        {
            icmbShowMode.Properties.ReadOnly = chkSuperAssociationEnabled.Checked;
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
                txtCode.Text = value;
            }
            get
            {
                return txtCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtName.ReadOnly = readOnly;
            txtCode.ReadOnly = readOnly;            
            txtNotes.ReadOnly = readOnly;
            icmbShowMode.ReadOnly = readOnly;
            chkSuperAssociationEnabled.ReadOnly = readOnly;            
            if (!readOnly)
            {
                txtName.Focus();
            }
        }

        /// <summary>
        /// 设置关联信息
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            if (commonNode != null)
            {
                TreeNodeId = commonNode.NodeId;
                CustomAssociationInfo customAssociationInfo = CustomAssociationContract.GetModelInfo(commonNode.NodeId);
                if (customAssociationInfo != null)
                {
                    txtName.Text = customAssociationInfo.AssociationName;
                    txtCode.Text = customAssociationInfo.AssociationCode;
                    txtPhysicalName.Text = customAssociationInfo.PhysicalName;
                    icmbShowMode.EditValue = customAssociationInfo.ShowMode;
                    chkSuperAssociationEnabled.Checked = customAssociationInfo.SuperAssociationEnabled;
                    txtNotes.Text = customAssociationInfo.Notes;
                }
                else
                {
                    txtName.Text = commonNode.NodeName;
                    txtCode.Text = commonNode.NodeCode;
                    txtPhysicalName.Text = string.Empty;
                    icmbShowMode.SelectedIndex = 0;
                    chkSuperAssociationEnabled.Checked = false;
                    txtNotes.Text = string.Empty;
                }
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
            txtName.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtPhysicalName.Text = string.Empty;
            icmbShowMode.SelectedIndex = 0;
            chkSuperAssociationEnabled.Checked = false;
            txtNotes.Text = string.Empty;
            txtName.Focus();
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取枚关联信息
        /// </summary>
        /// <returns></returns>
        public CustomAssociationInfo GetModelInfo()
        {
            string associationName = txtName.Text.Trim();
            string associationCode = txtCode.Text.Trim();
            string notes = txtNotes.Text.Trim();
            byte showMode = Convert.ToByte(icmbShowMode.EditValue);
            CustomAssociationInfo customAssociationInfo = new CustomAssociationInfo()
            {
                AssociationId = TreeNodeId,
                AssociationName = associationName,
                AssociationCode = associationCode,
                ShowMode = showMode,
                SuperAssociationEnabled = chkSuperAssociationEnabled.Checked,
                IsLeaf = true,
                Notes = notes
            };

            return customAssociationInfo;
        }

        #endregion        
    }
}
