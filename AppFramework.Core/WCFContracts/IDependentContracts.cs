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
    /// 依赖表类型的契约接口的父接口
    /// </summary>
    [ServiceContract(Name = "IDependentContracts", Namespace = "http://www.scu.edu.cn/WCFContracts/")]
    public interface IDependentContracts<T> : IContractsBase<T> where T : class
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
        ///<param name="foreignKey">被关联表的外键值</param>
        ///<param name="modeId">T 类的对象编号</param>
        /// <returns> T 类的对象</returns>
        [OperationContract(Name = "GetModeInfo")]
        T GetModeInfo(decimal foreignKey, int modeId);

        /// <summary>
        /// 根据编号删除 T 类的对象
        /// </summary>
        /// <param name="foreignKey">被关联表的外键值</param>
        /// <param name="modeId">T 类的对象编号</param>    
        [OperationContract(Name = "Delete")]
        void Delete(decimal foreignKey, int modeId);
        #endregion
    }
}
