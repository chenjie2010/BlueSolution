//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDualTableContracts.cs
// 描述: IDualTableContracts 契约层接口
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
    /// 联系表类型的契约接口的父接口
    /// </summary>
    [ServiceContract(Name = "ICorrelatedContracts", Namespace = "http://www.scu.edu.cn/WCFContracts/")]
    public interface ICorrelatedContracts<T> : IContractsBase<T> where T : class
    {
        #region 接口

        /// <summary>
        /// 插入 T 类的对象
        /// </summary>
        /// <param name="modeInfo">T 类的对象</param>
        [OperationContract(Name = "Insert")]
        void Insert(T modeInfo);

        /// <summary>
        /// 获得 T 类的对象
        /// </summary>
        ///<param name="foreignKey">其中一个被关联表的外键值</param>
        ///<param name="otherForeignKey">另外一个被关联表的外键值</param>
        /// <returns> T 类的对象</returns>
        [OperationContract(Name = "GetModeInfo")]
        T GetModeInfo(decimal foreignKey, decimal otherForeignKey);

        /// <summary>
        /// 根据编号删除 T 类的对象
        /// </summary>
        /// <param name="foreignKey">其中一个被关联表的外键值</param>
        /// <param name="otherForeignKey">另外一个被关联表的外键值</param>
        [OperationContract(Name = "Delete")]
        void Delete(decimal foreignKey, decimal otherForeignKey);        

        /*
        /// <summary>
        /// 根据被关联表的外键值来删除满足条件的 T 类的对象
        /// </summary>
        /// <param name="foreignKey">被关联表的外键值</param>
        /// <returns>返回删除的记录数目数目</returns>
        [OperationContract(Name = "DeleteByForeignKey")]
        int Delete(decimal foreignKey);*/

        #endregion
    }
}
