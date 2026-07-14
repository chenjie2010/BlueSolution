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
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.CustomLibrary;
using Blue.WCFContracts.SystemModule;
using Blue.Model.SystemModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class RoleModule : UserControl, ITreeNodeShow
    {
        #region 属性

        /// <summary>
        /// 角色契约
        /// </summary>
        public ICustomRoleContract CustomRoleContract
        {
            get; set;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbRoleProperty, typeof(RoleProperty));
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuModule_Load(object sender, EventArgs e)
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
                txtRoleCode.Text = value;
            }
            get
            {
                return txtRoleCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtRoleName.ReadOnly = readOnly;
            txtRoleCode.ReadOnly = readOnly;
            deInitializedDate.ReadOnly = readOnly;
            deExpiredDate.ReadOnly = readOnly;
            ccmbRoleProperty.ReadOnly = readOnly;
            chkIsSystemRole.ReadOnly = readOnly;
            chkIsLockedOut.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!txtRoleName.ReadOnly)
            {
                txtRoleName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomRoleInfo customRoleInfo = CustomRoleContract.GetModelInfo(commonNode.NodeId);
            if (customRoleInfo != null)
            {
                txtRoleName.Text = customRoleInfo.RoleName;
                txtRoleCode.Text = customRoleInfo.RoleCode;
                deInitializedDate.EditValue = DataConvertionHelper.SetDateTime(customRoleInfo.InitializedDate);
                deExpiredDate.EditValue = DataConvertionHelper.SetDateTime(customRoleInfo.ExpiredDate);
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbRoleProperty, customRoleInfo.RoleProperty);                 
                chkIsSystemRole.Checked = customRoleInfo.IsSystemRole;
                chkIsLockedOut.Checked = customRoleInfo.IsLockedOut;                
                txtNotes.Text = customRoleInfo.Notes;
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
            txtRoleName.Text = string.Empty;
            txtRoleCode.Text = string.Empty;
            deInitializedDate.EditValue = null;
            deExpiredDate.EditValue = null;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbRoleProperty);
            chkIsSystemRole.Checked = false;
            chkIsLockedOut.Checked = false;
            txtNotes.Text = string.Empty;
            if (!txtRoleName.ReadOnly)
            {
                txtRoleName.Focus();
            }
        }

        /// <summary>
        /// 校验工作流步骤对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            warning = string.Empty;

            CustomRoleInfo cstomRoleInfo = GetModelInfo();
            return ValidationHelper.Validate<CustomRoleInfo>(cstomRoleInfo, out warning);
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns></returns>
        public CustomRoleInfo GetModelInfo()
        {
            DateTime initializedDate = DateTime.MinValue;
            DateTime expiredDate = DateTime.MinValue;
            if (deInitializedDate.EditValue != null)
            {
                initializedDate = deInitializedDate.DateTime;
            }
            if (deExpiredDate.EditValue != null)
            {
                expiredDate = deExpiredDate.DateTime;
            }
            CustomRoleInfo customRoleInfo = new CustomRoleInfo()
            {
                RoleId = TreeNodeId,
                RoleName = txtRoleName.Text.Trim(),
                RoleCode = txtRoleCode.Text.Trim(),
                InitializedDate = initializedDate,
                RoleProperty = UserControlHelper.GetCheckedComboBoxEditItems(ccmbRoleProperty),
            ExpiredDate = expiredDate,
                IsSystemRole = chkIsSystemRole.Checked,
                IsLockedOut = chkIsLockedOut.Checked,
                Notes = txtNotes.Text.Trim()
            };

            return customRoleInfo;
        }       

        #endregion       
    }
}
