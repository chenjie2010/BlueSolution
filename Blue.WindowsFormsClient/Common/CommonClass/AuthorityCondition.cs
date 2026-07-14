using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.Common
{
    public class AuthorityCondition
    {
        #region 私有变量

        private IList<CommonNode> relatedUserTypeCommonNodes = null;
        private IList<CommonNode> relatedDepartmentCommonNodes = null;

        #endregion

        #region 契约接口

        private readonly ICustomDepartmentContract customDepartmentContract = null;
        private readonly IUserTypeContract userTypeContract = null;

        #endregion

        #region 属性

        /// <summary>
        /// 用户类型范围
        /// </summary>
        public IList<CommonNode> RelatedUserTypeCommonNodes
        {
            get
            {
                return relatedUserTypeCommonNodes;
            }
        }

        /// <summary>
        /// 单位类型范围
        /// </summary>
        public IList<CommonNode> RelatedDepartmentCommonNodes
        {
            get
            {
                return relatedDepartmentCommonNodes;
            }
        }

        #endregion

        #region 构造函数 

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthorityCondition(ICustomDepartmentContract departmentContract, IUserTypeContract customUserTypeContract)
        {
            customDepartmentContract = departmentContract;
            userTypeContract = customUserTypeContract;
            relatedUserTypeCommonNodes = userTypeContract.GetCommonNodes(CurrentUser.Instance.UserId);
            relatedDepartmentCommonNodes = customDepartmentContract.GetCommonNodes(CurrentUser.Instance.UserId);
        }

        #endregion

        #region 公有方法 

        /// <summary>
        /// 获得查询条件
        /// </summary>
        /// <param name="userTypeCommonNode"></param>
        /// <param name="departmentCommonNode"></param>
        /// <returns></returns>
        public List<WhereConditon> GetWhereConditons(CommonNode userTypeCommonNode, CommonNode departmentCommonNode)
        {
            List<WhereConditon> whereConditons = new List<WhereConditon>();

            /* 用户类型 */
            if (userTypeCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("UserAccount", "UserTypeId", "UserTypeId", System.Data.DbType.Decimal, userTypeCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            else
            {
                if (relatedUserTypeCommonNodes != null && relatedUserTypeCommonNodes.Count > 0)
                {
                    whereConditons.AddRange(DataAccessHandler.GetWhereConditons(relatedUserTypeCommonNodes, "UserAccount", "UserTypeId"));
                }
            }

            /* 单位类型 */
            if (departmentCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("UserAccount", "DepId", "DepId", DbType.Decimal, departmentCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            else
            {
                if (relatedDepartmentCommonNodes != null && relatedDepartmentCommonNodes.Count > 0)
                {
                    whereConditons.AddRange(DataAccessHandler.GetWhereConditons(relatedDepartmentCommonNodes, "UserAccount", "DepId"));
                }
            }

            return whereConditons;
        }

        #endregion
    }
}
