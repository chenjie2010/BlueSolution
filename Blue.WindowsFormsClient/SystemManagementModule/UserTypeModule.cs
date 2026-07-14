using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.WinFormsLibrary.Common;
using Blue.Model.SystemModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.Common
{
    /// <summary>
    /// 用户类型窗体类
    /// </summary>
    public partial class UserTypeModule : UserControl, ITreeNodeShow
    {
        #region 属性

        /// <summary>
        /// 用户类型契约
        /// </summary>
        public IUserTypeContract UserTypeContract
        {
            set;get;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserTypeModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            SetActiveStatesOfControls(true);
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeModule_Load(object sender, EventArgs e)
        {

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
            txtAddFstCode.ReadOnly = readOnly;
            txtAddScdCode.ReadOnly = readOnly;
            chkIsSystemUserType.ReadOnly = readOnly;
            chkVisibleForInterface.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!readOnly)
            {
                txtName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            if (commonNode != null && commonNode.NodeId > 0)
            {
                TreeNodeId = commonNode.NodeId;
                UserTypeInfo userTypeInfo = UserTypeContract.GetModelInfo(commonNode.NodeId);
                txtName.Text = userTypeInfo.UserTypeName;
                txtCode.Text = userTypeInfo.UserTypeCode;
                txtAddFstCode.Text = userTypeInfo.FirstCode;
                txtAddScdCode.Text = userTypeInfo.SecondCode;
                chkIsSystemUserType.Checked = userTypeInfo.IsSystemUserType;
                chkVisibleForInterface.Checked = userTypeInfo.IsVisibleForInterface;
                txtNotes.Text = userTypeInfo.Notes;
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
            txtAddFstCode.Text = string.Empty;
            txtAddScdCode.Text = string.Empty;
            chkIsSystemUserType.Checked = false;
            chkVisibleForInterface.Checked = false;
            txtNotes.Text = string.Empty;
            if (!txtName.ReadOnly)
            {
                txtName.Focus();
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取用户类型信息
        /// </summary>
        /// <returns></returns>
        public UserTypeInfo GetModelInfo()
        {
            string name = txtName.Text.Trim();
            string code = txtCode.Text.Trim();
            string addFstCode = txtAddFstCode.Text.Trim();
            string addScdCode = txtAddScdCode.Text.Trim();
            string notes = txtNotes.Text.Trim();
            UserTypeInfo userTypeInfo = new UserTypeInfo()
            {
                UserTypeId = TreeNodeId,
                UserTypeName = name,
                UserTypeCode = code,
                FirstCode = addFstCode,
                SecondCode = addScdCode,
                IsSystemUserType = chkIsSystemUserType.Checked,
                IsVisibleForInterface = chkVisibleForInterface.Checked,
                Notes = notes
            };

            return userTypeInfo;
        }

        #endregion
    }
}
