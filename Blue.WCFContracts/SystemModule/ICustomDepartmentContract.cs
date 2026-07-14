//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomDepartmentContract.cs
// 描述： CustomDepartment 契约层接口
// 作者：ChenJie 
// 编写日期：2016/8/19
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
using Blue.Model.SystemModule;

namespace Blue.WCFContracts.SystemModule
{
    /// <summary>
    /// CustomDepartment 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomDepartmentContract", Namespace = "http://www.scu.edu.cn/SystemModule/")]
    public interface ICustomDepartmentContract : ICommonNodeContract, IPrincipalContracts<CustomDepartmentInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得所有的单位信息
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetCustomDepartmentInfos")]
        IList<CustomDepartmentInfo> GetCustomDepartmentInfos();

        /// <summary>
        /// 获得单位编号
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDepIdByName")]
        decimal GetDepIdByName(string depName);

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetTemplateColumnCaptions")]
        IList<string> GetTemplateColumnCaptions();

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="parentDepId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPageRecordById")]
        DataSet GetPageRecord(decimal parentDepId);

        /// <summary>
        /// 获得表 CustomDepartment 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        [OperationContract(Name = "GetPageRecord")]
        DataSet GetPageRecord(int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 获得单位编码
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDepCode")]
        string GetDepCode(decimal depId);

        /// <summary>
        /// 获得用户单位编号和用户单位名称的对应集合
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetDepIdAndNames")]
        Dictionary<decimal, string> GetDepIdAndNames();

        /// <summary>
        /// 获得单位名称与编号集合
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetNameAndIds")]
        Dictionary<string, decimal> GetNameAndIds();

        /// <summary>
        /// 通过用户编号获得管理单位节点列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonNodesByUserId")]
        IList<CommonNode> GetCommonNodes(decimal userId);

        /// <summary>
        /// 获得单位对象
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonNodeByDepName")]
        CommonNode GetCommonNode(string depName);

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTreeviewCommonNodesWithRoot")]
        CommonItemList<decimal, CommonNode> GetTreeviewCommonNodesWithRoot(string depName);

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTreeviewCommonNodes")]
        [ServiceKnownType(typeof(CommonNode))]
        CommonItemList<decimal, CommonNode> GetTreeviewCommonNodes(string depName);

        /// <summary>
        /// 通过根节点单位信息
        /// </summary>
        [OperationContract(Name = "GetRootDepartmentInfo")]
        CustomDepartmentInfo GetRootDepartmentInfo();

        #endregion
    }
}