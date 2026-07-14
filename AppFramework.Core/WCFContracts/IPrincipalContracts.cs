//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IPrincipalContracts.cs
// 描述: IPrincipalContracts 契约层接口
// 作者：ChenJie 
// 编写日期：2016/07/20
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace AppFramework.Core.WCFContracts
{
    /// <summary>
    /// 主表类型的契约接口的父接口
    /// </summary>
    [ServiceContract(Name = "IPrincipalContracts", Namespace = "http://www.scu.edu.cn/WCFContracts/")]
    public interface IPrincipalContracts<T> : IContractsBase<T> where T : class
    {
        #region 接口

        /// <summary>
        /// 插入 T 类的对象
        /// </summary>
        /// <param name="modeInfo">T 类的对象</param>
        /// <returns>自动增加的关键字的值</returns>
        [OperationContract(Name = "Insert")]
        decimal Insert(T modeInfo);

        /// <summary>
        /// 获得 T 类的对象
        /// </summary>
        ///<param name="modeId">T 类对象的编号</param>
        /// <returns> T 类的对象</returns>
        [OperationContract(Name = "GetModeInfo")]
        T GetModeInfo(decimal modeId);

        /// <summary>
        /// 根据编号删除 T 类的对象
        /// </summary>
        /// <param name="modeId">T 类对象的编号</param>
        [OperationContract(Name = "Delete")]
        void Delete(decimal modeId);

        #endregion

    }
}
