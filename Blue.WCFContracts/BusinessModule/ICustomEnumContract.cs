//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomEnumContract.cs
// 描述： CustomEnum 契约层接口
// 作者：ChenJie 
// 编写日期：2016/8/20
// Copyright 2016
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
    /// CustomEnum 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomEnumContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomEnumContract : ICommonNodeContract, IPrincipalContracts<CustomEnumInfo>
    {
        #region 自定义接口        

        /// <summary>
        /// 刷新排序
        /// </summary>
        [OperationContract(Name = "RefreshSorting")]
        void RefreshSorting();

        /// <summary>
        /// 根据父节点编号和枚举名称获得枚举数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTreeData")]
        object GetTreeData(decimal parentEnumId, string enumName, PhysicalDataFieldType physicalDataFieldType);

        /// <summary>
        /// 根据父节点编号和枚举名称获得枚举数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="enumName"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDropdownListData")]
        object GetDropdownListData(decimal parentEnumId, string enumName, PhysicalDataFieldType physicalDataFieldType);

        /// <summary>
        /// 获得枚举名称
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="value"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDropdownListEnumName")]
        string GetDropdownListEnumName(decimal parentEnumId, object value, PhysicalDataFieldType physicalDataFieldType);

        /// <summary>
        /// 获得枚举名称
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="value"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTreeEnumName")]
        [ServiceKnownType(typeof(DBNull))]
        string GetTreeEnumName(decimal parentEnumId, object value, PhysicalDataFieldType physicalDataFieldType);

        /// <summary>
        /// 获得枚举数据
        /// </summary>
        /// <param name="enumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetEnumData")]
        object GetEnumData(decimal enumId, PhysicalDataFieldType physicalDataFieldType);

        /// <summary>
        /// 根据枚举编码获得枚举编号
        /// </summary>
        /// <param name="enumCode"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetEnumIdByEnumCode")]
        decimal GetEnumId(string enumCode);

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetTemplateColumnCaptions")]
        IList<string> GetTemplateColumnCaptions();

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPageRecordByParentEnumId")]
        DataSet GetPageRecord(decimal parentEnumId);

        /// <summary>
        /// 获得表 CustomEnum 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        [OperationContract(Name = "GetPageRecord")]
        DataSet GetPageRecord(int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 获得枚举选项列表
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetEnumItems")]
        IList<CustomEnumInfo> GetEnumItems(decimal parentEnumId);

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="enumName"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTreeviewCommonNodes")]
        CommonItemList<decimal, CommonNode> GetTreeviewCommonNodes(decimal parentEnumId, string enumName);

        /// <summary>
        /// 获取枚举的最大层级
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetMaxLevel")]
        int GetMaxLevel(decimal enumId);

        /// <summary>
        /// 是否是超大枚举
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetSuperEnumEnabled")]
        bool GetSuperEnumEnabled(decimal enumId);

        #endregion
    }
}