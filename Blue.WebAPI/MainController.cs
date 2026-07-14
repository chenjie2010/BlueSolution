//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: MainController.cs
// 描述: 获取主要数据接口
// 作者：ChenJie 
// 编写日期：2017-07-07
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;
using AppFramework.Core;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WebAPI
{
    [SessionValidate]
    public class MainController : ApiController
    {
        #region 业务实例

        private static readonly IUserTypeHandler userTypeHandler = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<IUserTypeHandler>();

        #endregion
        
        /// <summary>
        /// 获得用户类型列表
        /// </summary>
        /// <returns></returns>
        public DataTable  GetUserType()
        {
            DataTable dt = null;

            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            userTypeHandler.GetModelInfos(null, sortingCondtions);


            return dt;
        }

        /// <summary>
        /// 获得单位列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDepartment()
        {
            DataTable dt = null;

            return dt;
        }
    }
}
