//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: SystemChannelFactory.cs
// 描述: 系统模块类来创建客户端代理对象
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
using Blue.WCFContracts.DataConvertionModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 系统模块类
    /// </summary>
    public sealed class DataConvertionChannelFactory
    {
        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DataConvertionChannelFactory()
        {

        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 根据默认地址来创建 DataRelationService 代理对象
        /// </summary>
        /// <returns></returns>
        public static IDataRelationContract CreateDataRelationContract()
        {
            IDataRelationContract dataRelationContract = ServiceProxyFactory.Create<IDataRelationContract>("DataRelationService");

            return dataRelationContract;
        }

        /// <summary>
        /// 根据默认地址来创建 RemoteDataService 代理对象
        /// </summary>
        /// <returns></returns>
        public static IRemoteDataContract CreateRemoteDataContract()
        {
            IRemoteDataContract remoteDataContract = ServiceProxyFactory.Create<IRemoteDataContract>("RemoteDataService");

            return remoteDataContract;
        }

        #endregion
    }
}
