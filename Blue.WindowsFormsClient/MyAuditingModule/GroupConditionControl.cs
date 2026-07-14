using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.MyAuditingModule
{
    public partial class GroupConditionControl : UserControl
    {
        #region 私有变量
        
        private AuthorityCondition authorityCondition = null;

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
        /// 用户类型契约
        /// </summary>
        public IUserTypeContract UserTypeContract
        {
            get;
            set;
        }

        /// <summary>
        /// 单位契约
        /// </summary>
        public ICustomDepartmentContract CustomDepartmentContract
        {
            get;
            set;
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        [
        Description("设置查询条件"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public SetWhereConditonDelegate SetWhereConditonHandler
        {
            get;
            set;
        }

        #endregion               

        #region 构造函数

        /// <summary>
        /// 契约接口
        /// </summary>
        public GroupConditionControl()
        {
            InitializeComponent();
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbAuditedStatus, typeof(AuditedStatus));
        }

        #endregion

        #region 控件加载方法

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupConditionControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                authorityCondition = new AuthorityCondition(CustomDepartmentContract, UserTypeContract);
                IAsyncResult result = this.BeginInvoke(new MethodInvoker(() =>
                {
                    /* 1. 管理的用户类型 */                    
                    if (authorityCondition.RelatedUserTypeCommonNodes != null && authorityCondition.RelatedUserTypeCommonNodes.Count > 0)
                    {
                        cmbQueriedUserType.TreeViewHandler.InitFirstLevelNodes(authorityCondition.RelatedUserTypeCommonNodes);
                    }
                    else
                    {
                        cmbQueriedUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(CustomGroupContract, UserTypeContract);
                        cmbQueriedUserType.InitalizeTreeView();
                    }

                    /* 2. 管理的单位 */
                    if (authorityCondition.RelatedDepartmentCommonNodes != null && authorityCondition.RelatedDepartmentCommonNodes.Count > 0)
                    {
                        cmbQueriedDepartment.ShowSearch = false;
                        cmbQueriedDepartment.TreeViewHandler.InitFirstLevelNodes(authorityCondition.RelatedDepartmentCommonNodes);
                    }
                    else
                    {
                        cmbQueriedDepartment.TreeDropdownHandler = new TreeDropdownItems(CustomDepartmentContract);
                        cmbQueriedDepartment.InitalizeTreeView();
                    }
                }));
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Query();
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCondition.Text = string.Empty;
            cmbQueriedUserType.Value = null;
            cmbQueriedDepartment.Value = null;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbAuditedStatus);
            Query();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获得 WHERE 查询条件
        /// </summary>
        /// <returns></returns>
        private IList<WhereConditon> GetWhereConditons()
        {
            List<WhereConditon> whereConditons = new List<WhereConditon>();
            string condition = txtCondition.Text;
            if (!string.IsNullOrWhiteSpace(condition))
            {
                string userName = Regex.Replace(condition, " {1,}", "%");
                whereConditons.Add(new WhereConditon("UserAccount", "UserName", "UserName", DbType.String, userName,
                   DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                whereConditons.Add(new WhereConditon("UserAccount", "EmailAddress", "EmailAddress", DbType.String, userName,
                      DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("UserAccount", "UserIdentity", "UserIdentity", DbType.String, userName,
                      DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                if (UserDataHelper.StartWithMobilePhoneNumber(condition))
                {
                    whereConditons.Add(new WhereConditon("UserAccount", "TelephoneNumber", "TelephoneNumber", DbType.String, userName,
                      DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                }
                string userActualName = Regex.Replace(condition, " {1,}", "%");
                whereConditons.Add(new WhereConditon("UserAccount", "UserActualName", "UserActualName", DbType.String, userActualName,
                   DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
            }

            /* 用户类型 单位类型 */             
            List<WhereConditon> userWhereConditons = authorityCondition.GetWhereConditons(cmbQueriedUserType.Value as CommonNode, cmbQueriedDepartment.Value as CommonNode);
            if (userWhereConditons != null)
            {
                whereConditons.AddRange(userWhereConditons);
            }
                        
            /* 审核条件 */
            UserControlHelper.GetWhereConditons(ccmbAuditedStatus, whereConditons, string.Empty, "AuditedStatus");

            return whereConditons;
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            IList<WhereConditon> whereConditons = GetWhereConditons();
            SetWhereConditonHandler?.Invoke(whereConditons);
        }

        #endregion        
    }
}
