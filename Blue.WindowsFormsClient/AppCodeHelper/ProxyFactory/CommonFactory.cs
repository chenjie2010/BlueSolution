//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CommonUtilFactory.cs
// 描述: 与服务器端连接的通用操作工厂类
// 作者：ChenJie 
// 编写日期：2016/7/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 与服务器端连接的通用操作工厂类，不需要用户名和密码验证
    /// </summary>
    public sealed class CommonFactory
    {
        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static CommonFactory()
        {

        }

        #endregion

        #region 静态方法    

        /// <summary>
        /// 根据默认地址来创建验证用户和密码的验证代理对象
        /// </summary>
        /// <returns></returns>
        public static ICommonUtilContract CreateCommonUtilContract()
        {
            ICommonUtilContract commonUtilContract = ServiceProxyFactory.Create<ICommonUtilContract>("CommonUtilService");

            return commonUtilContract;
        }
       
        /// <summary>
        /// 创建系统服务对象
        /// </summary>
        /// <returns></returns>
        public static ISystemPrvoiderContract CreateSystemPrvoiderContract()
        {
            ISystemPrvoiderContract systemPrvoiderContract = ServiceProxyFactory.Create<ISystemPrvoiderContract>("SystemPrvoiderService");

            return systemPrvoiderContract;
        }

        #endregion

    }
}
