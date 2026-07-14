//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomInterfaceContract.cs
// 描述: CustomInterface 契约层接口
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.SystemModule;

namespace Blue.WCFContracts.SystemModule
{
    /// <summary>
    /// CustomInterface 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomInterfaceContract", Namespace = "http://www.scu.edu.cn/SystemModule/")]
    public interface ICustomInterfaceContract : ICommonNodeContract, IPrincipalContracts<CustomInterfaceInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 标识符是否已经存在
        /// </summary>
        /// <param name="interfaceIdentifier"></param>
        /// <returns></returns>
        [OperationContract(Name = "IsExistedIdentifier")]
        bool IsExistedIdentifier(string interfaceIdentifier);

        /// <summary>
        /// 更新条件
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <param name="userTypeIds"></param>
        /// <param name="departmentIds"></param>
        [OperationContract(Name = "UpdateConditions")]
        void UpdateConditions(decimal interfaceId, IList<decimal> userTypeIds, IList<decimal> departmentIds);

        /// <summary>
        /// 获得管理的用户类型
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetUserTypes")]
        IList<CommonNode> GetUserTypes(decimal interfaceId);

        /// <summary>
        /// 获得管理的单位
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDepartments")]
        IList<CommonNode> GetDepartments(decimal interfaceId);

        #endregion
    }
}