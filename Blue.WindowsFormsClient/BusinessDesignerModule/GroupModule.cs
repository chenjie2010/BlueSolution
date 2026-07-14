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
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.CustomLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class GroupModule : UserControl, ITreeNodeShow
    {
        #region 属性       
        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            set; get;
        }
        
        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public GroupModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            SetActiveStatesOfControls(true);
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
            txtTooltip.ReadOnly = readOnly;
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
            if (commonNode != null)
            {
                TreeNodeId = commonNode.NodeId;
                CustomGroupInfo customGroupInfo = CustomGroupContract.GetModelInfo(commonNode.NodeId);
                txtName.Text = customGroupInfo.GroupName;
                txtCode.Text = customGroupInfo.GroupCode;             
                txtNotes.Text = customGroupInfo.Notes;
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
            txtTooltip.Text = string.Empty;
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
        public CustomGroupInfo GetModelInfo()
        {
            string name = txtName.Text.Trim();
            string code = txtCode.Text.Trim();
            string tooltip = txtTooltip.Text.Trim();
            string notes = txtNotes.Text.Trim();
            CustomGroupInfo customGroupInfo = new CustomGroupInfo()
            {
                GroupId = TreeNodeId,
                GroupName = name,
                GroupCode = code,
                Notes = notes
            };

            return customGroupInfo;
        }

        #endregion
    }
}
