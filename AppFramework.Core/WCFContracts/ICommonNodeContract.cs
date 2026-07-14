//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICommonNodeContract.cs
// 描述: ICommonNodeContract 契约层节点接口
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
using AppFramework.Core.CommonClass;
using AppFramework.Core.CommonEnum;

namespace AppFramework.Core.WCFContracts
{
    /// <summary>
    /// 节点操作契约接口
    /// </summary>
    [ServiceContract(Name = "ICommonNodeContract", Namespace = "http://www.scu.edu.cn/WCFContracts/")]
    public interface ICommonNodeContract
    {
        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary> 
        /// <returns>CommonNode 对象的列表</returns>
        [OperationContract(Name = "GetCommonNodes")]
        IList<CommonNode> GetCommonNodes();

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="name">名称</param>
        /// <returns>CommonNode 对象的列表</returns>
        [OperationContract(Name = "GetCommonNodesByName")]
        IList<CommonNode> GetCommonNodes(string name);

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentId">父节点编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        [OperationContract(Name = "GetCommonNodesByParentId")]
        IList<CommonNode> GetCommonNodes(decimal parentId);

        /// <summary>
        /// 由父节点编号获得下一级子节点的数目
        /// </summary>
        /// <param name="parentNodeId">父节点编号</param>
        /// <returns>下一级子节点的数目</returns>
        [OperationContract(Name = "GetTotalCountOfChildNode")]
        int GetTotalCountOfChildNode(decimal parentNodeId);

        /// <summary>
        /// 获得节点的所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        [OperationContract(Name = "GetParentNamesOfNode")]
        IList<string> GetParentNamesOfNode(decimal nodeId);

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="userTypeId">移动的节点编号</param>
        /// <param name="otherNodeId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        [OperationContract(Name = "UpdateSorting")]
        void UpdateSorting(decimal nodeId, decimal otherNodeId, MovedDriection movedDriectionOfNode);

        /// <summary>
        /// 同一级是否存在相同的名称
        /// </summary>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeName">节点名称</param>        
        /// <returns>是否存在</returns>
        [OperationContract(Name = "IsExistIdenticalName")]
        bool IsExistIdenticalName(decimal parentNodeId, string nodeName);    

    }
}
