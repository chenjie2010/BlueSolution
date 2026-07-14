//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomGroupContract.cs
// 描述： CustomGroup 契约层接口
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.BusinessModule
{
    /// <summary>
    /// CustomGroup 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomGroupContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomGroupContract : ICommonNodeContract, IPrincipalContracts<CustomGroupInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得数据集(获得节点自身数据)
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPageRecordById")]
        DataSet GetPageRecord(decimal groupId);

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="parentGroupId"></param>
        /// <param name="groupType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPageRecordByGroupId")]
        DataSet GetPageRecord(decimal parentGroupId, GroupType groupType);

        /// <summary>
        /// 获得表 CustomEnum 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        [OperationContract(Name = "GetPageRecord")]
        DataSet GetPageRecord(int startPosition, int count, byte groupType, ref int totalCount);


        #endregion
    }
}