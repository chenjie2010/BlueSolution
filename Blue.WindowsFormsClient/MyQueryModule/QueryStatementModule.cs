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
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.MyQueryModule
{
    public partial class QueryStatementModule : UserControl, ITreeNodeShow
    {
        #region 私有变量

        #endregion

        #region 属性
        
        /// <summary>
        /// 用户查询契约
        /// </summary>
        public IUserQueryContract UserQueryContract
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
        

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryStatementModule()
        {
            InitializeComponent();
            icmbRecommendType.Visible = false;
            lblRecommendType.Visible = false;
            //UserControlHelper.InitImageComboBoxEdit(icmbRecommendType, typeof(RecommendType));
            TreeNodeId = 0;
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintModule_Load(object sender, EventArgs e)
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
                txtQueryCode.Text = value;
            }
            get
            {
                return txtQueryCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtQueryame.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            icmbRecommendType.ReadOnly = readOnly;
            if (!txtQueryame.ReadOnly)
            {
                txtQueryame.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            UserQueryInfo userQueryInfo = UserQueryContract.GetModelInfo(commonNode.NodeId);
            if (userQueryInfo != null)
            {
                txtQueryame.Text = userQueryInfo.UserQueryName;
                txtQueryCode.Text = userQueryInfo.UserQueryCode;
                icmbRecommendType.EditValue = userQueryInfo.RecommendType;
                txtNotes.Text = userQueryInfo.Notes;
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
            txtQueryame.Text = string.Empty;
            txtQueryCode.Text = string.Empty;          
            txtNotes.Text = string.Empty;
            icmbRecommendType.SelectedIndex = 0;
            if (!txtQueryame.ReadOnly)
            {
                txtQueryame.Focus();
            }
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;
            
            UserQueryInfo userQueryInfo = GetModelInfo();
            result = ValidationHelper.Validate<UserQueryInfo>(userQueryInfo, out warning);

            return result;
        }
        
        #endregion

        #region 公有方法

        /// <summary>
        /// 获取用户查的信息
        /// </summary>
        /// <returns></returns>
        public UserQueryInfo GetModelInfo()
        {
            UserQueryInfo userQueryInfo = new UserQueryInfo()
            {
                UserQueryId = TreeNodeId,
                UserQueryCode = txtQueryCode.Text.Trim(),
                UserId = CurrentUser.Instance.UserId,
                RecommendType = Convert.ToByte(icmbRecommendType.EditValue),
                UserQueryName = txtQueryame.Text.Trim(),
                Notes = txtNotes.Text.Trim()
            };

            return userQueryInfo;
        }
        
        #endregion
        
    }
}
