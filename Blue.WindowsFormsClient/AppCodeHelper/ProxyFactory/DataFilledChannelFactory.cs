//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataFilledChannelFactory.cs
// 描述: 数据填报模块类来创建客户端代理对象
// 作者：ChenJie 
// 编写日期：2018/02/18
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts.DataFilledModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 数据填报模块类
    /// </summary>
    public sealed class DataFilledChannelFactory
    {
        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DataFilledChannelFactory()
        {

        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 根据默认地址来创建枚举管理的 IBusinessInstanceContract 代理对象
        /// </summary>
        /// <returns></returns>
        public static IBusinessInstanceContract CreateBusinessInstanceContract()
        {
            IBusinessInstanceContract businessInstanceContract = ServiceProxyFactory.Create<IBusinessInstanceContract>("BusinessInstanceService");

            return businessInstanceContract;
        }
        
        #endregion
    }
}
