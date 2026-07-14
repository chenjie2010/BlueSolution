//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ITestConnectionContract.cs
// 描述: 服务器端连接契约类
// 作者：ChenJie 
// 编写日期：2016-07-28
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;

namespace Blue.WCFContracts
{
    /// <summary>
    /// 用户验证契约
    /// </summary>
    [ServiceContract(Name = "ICommonUtilContract", Namespace = "http://www.scu.edu.cn/")]
    public interface ICommonUtilContract
    {
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>如果提供的用户名和密码有效，则返回 true；否则返回 false</returns>
        [OperationContract(Name = "ValidateUser")]
        bool ValidateUser(string userName, string password);

        /// <summary>
        /// 验证客户端版本
        /// </summary>
        /// <param name="clientVersion"></param>
        /// <returns></returns>
        [OperationContract(Name = "ValidateClientVersion")]
        bool ValidateClientVersion(string clientVersion);

        /// <summary>
        /// 测试连接
        /// </summary>
        [OperationContract(Name = "TestConnection")]
        void TestConnection();

        /// <summary>
        /// 获得注册信息
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetRegisterInfo")]
        RegisterInfo GetRegisterInfo();

        /// <summary>
        /// 获得系统时间
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetSystemDataTime")]
        DateTime GetSystemDataTime(string userName);      

        /// <summary>
        /// 获得服务器端关键字
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetDomainName")]
        string GetDomainName();
    }
}
