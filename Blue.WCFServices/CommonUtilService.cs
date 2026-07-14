//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CommonUtilService.cs
// 描述: 服务器端通用操作服务类
// 作者：ChenJie 
// 编写日期：2016-07-28
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.MembershipProvider;
using Blue.CustomLibrary;
using Blue.WCFContracts;
using Blue.BusinessLogic;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices
{
    /// <summary>
    /// 服务器端通用操作服务类，不需要验证用户名和密码
    /// </summary>
    public class CommonUtilService : ICommonUtilContract
    {
        #region 业务实例

        private readonly SoftwareVersionValidator softwareVersionValidator = null;
        private readonly IUserNamePasswordValidator userNamePasswordValidator = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CommonUtilService()
        {
            softwareVersionValidator = new SoftwareVersionValidator();
            userNamePasswordValidator = BusinessLogicContainer.Instance.LoginModuleContainer.Resolve<IUserNamePasswordValidator>();
        }

        #endregion

        #region 实现契约接口

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>如果提供的用户名和密码有效，则返回 true；否则返回 false</returns>
        public bool ValidateUser(string userName, string password)
        {
            return userNamePasswordValidator.ValidateUser(userName, password);
        }


        /// <summary>
        /// 是否是最新版本
        /// </summary>
        /// <param name="clientVersion"></param>
        /// <returns></returns>
        public bool IsLastestVersion(string clientVersion)
        {
            return softwareVersionValidator.IsLastestVersion(clientVersion);
        }

        /// <summary>
        /// 验证客户端版本
        /// </summary>
        /// <param name="clientVersion"></param>
        /// <returns></returns>
        public bool ValidateClientVersion(string clientVersion)
        {
            return softwareVersionValidator.ValidateClientVersion(clientVersion);
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        public void TestConnection()
        {
            /* 测试连接，执行空操作 */
        }

        /// <summary>
        /// 获得注册信息
        /// </summary>
        /// <returns></returns>
        public RegisterInfo GetRegisterInfo()
        {
            return softwareVersionValidator.GetRegisterInfo();
        }

        /// <summary>
        /// 获得系统时间
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DateTime GetSystemDataTime(string userName)
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 获得服务器端关键字
        /// </summary>
        /// <returns></returns>
        public string GetDomainName()
        {
            return AppSettingHelper.DomainName;
        }

        #endregion
    }
}
