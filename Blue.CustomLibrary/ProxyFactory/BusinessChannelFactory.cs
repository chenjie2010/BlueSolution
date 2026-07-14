//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: BusinessChannelFactory.cs
// 描述: 业务管理模块类来创建客户端代理对象
// 作者：ChenJie 
// 编写日期：2016/08/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts.BusinessModule;

namespace Blue.CustomLibrary
{
    /// <summary>
    /// 业务管理模块类
    /// </summary>
    public sealed class BusinessChannelFactory
    {
        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static BusinessChannelFactory()
        {

        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 根据默认地址来创建枚举管理的 ICustomEnumContract 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomEnumContract CreateCustomEnumContract()
        {
            ICustomEnumContract customEnumContract = ServiceProxyFactory.Create<ICustomEnumContract>("CustomEnumService");

            return customEnumContract;
        }

        #endregion
    }
}
