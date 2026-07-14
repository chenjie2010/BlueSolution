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
using AppFramework.Core;

namespace AppFramework.Reference.WCFLibrary
{
    /// <summary>
    /// 节点操作契约接口
    /// </summary>
    [ServiceContract(Name = "ICommonNodeContract", Namespace = "http://www.scu.edu.cn/WCFContracts/")]
    public interface ICommonNodeContract
    {
        /// <summary>
        /// 获得完整名称
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetFullName")]
        string GetFullName(decimal nodeId);

        /// <summary>
        /// 获得 CommonNode 对象
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonNode")]
        CommonNode GetCommonNode(decimal nodeId);

        /// <summary>
        /// 获得扩展的 CommonNode
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetExtendedCommonNode")]
        ExtendedCommonNode GetExtendedCommonNode(decimal nodeId);

        /// <summary>
        /// 节点名称由父辈节点名称和自身名称一起组成
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonNodeWithParentNames")]
        CommonNode GetCommonNodeWithParentNames(decimal nodeId);

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary> 
        /// <returns>CommonNode 对象的列表</returns>
        [OperationContract(Name = "GetCommonNodes")]
        IList<CommonNode> GetCommonNodes();

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="nodeName">单位名称</param>
        /// <returns>CommonNode 对象的列表</returns>
        [OperationContract(Name = "GetChildNodesByNameAndCode")]
        IList<CommonNode> GetChildNodes(string nodeName, IList<string> nodeCodes);

        

        /// <summary>
        /// 根据节点名称或是编号查询 CommonNode 对象的列表
        /// </summary>	
        /// <param name="condition">条件</param>
        /// <returns>CommonNode 对象的列表</returns>
        [OperationContract(Name = "GetChildNodesByCondition")]
        IList<CommonNode> GetChildNodes(string condition);

        /// <summary>
        /// 根据节点名称或是编号查询 CommonNode 对象的列表
        /// </summary>	
        /// <param name="rootNodeId">根节点编号</param>
        /// <param name="condition">条件</param>
        /// <returns>CommonNode 对象的列表</returns>
        [OperationContract(Name = "GetChildNodesByConditions")]
        IList<CommonNode> GetChildNodes(decimal rootNodeId, string condition);

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentId">父节点编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        [OperationContract(Name = "GetChildNodesByParentId")]
        IList<CommonNode> GetChildNodes(decimal parentId);

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="keyId">关键字编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        [OperationContract(Name = "GetChildNodesByKeyId")]
        IList<CommonNode> GetChildNodes(decimal parentNodeId, decimal keyId);

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeType">父节点编号</param>
        /// <param name="keyId">关键字编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        [OperationContract(Name = "GetChildNodesByNodeTypeAndKeyId")]
        IList<CommonNode> GetChildNodes(decimal parentNodeId, byte nodeType, decimal keyId);

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>CommonNode 对象的列表</returns>
        [OperationContract(Name = "GetChildNodesByParentIdAndNodeType")]
        IList<CommonNode> GetChildNodes(decimal parentNodeId, byte nodeType);

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
        [OperationContract(Name = "GetHierarchicalNamesOfNode")]
        IList<string> GetHierarchicalNamesOfNode(decimal nodeId);

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
        [OperationContract(Name = "IsExistIdenticalNameOnLevel")]
        bool IsExistIdenticalName(decimal parentNodeId, string nodeName);

        /// <summary>
        /// 同一级是否存在相同的名称
        /// </summary>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否存在</returns>
        [OperationContract(Name = "IsExistIdenticalNameOnLevelWithNodeType")]
        bool IsExistIdenticalName(decimal parentNodeId, string nodeName, byte nodeType);

        /// <summary>
        /// 是否存在相同的名称
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <returns>是否存在</returns>
        [OperationContract(Name = "IsExistIdenticalName")]
        bool IsExistIdenticalName(string nodeName);

        /// <summary>
        /// 是否存在相同的编码
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <returns>是否存在</returns>
        [OperationContract(Name = "IsExistIdenticalCode")]
        bool IsExistIdenticalCode(string nodeCode);

        /// <summary>
        /// 是否存在相同的名称
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否存在</returns>
        [OperationContract(Name = "IsExistIdenticalNameUnderNodeType")]
        bool IsExistIdenticalName(string nodeName, byte nodeType);

        /// <summary>
        /// 是否存在相同的编码
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否存在</returns>
        [OperationContract(Name = "IsExistIdenticalCodeUnderNodeType")]
        bool IsExistIdenticalCode(string nodeCode, byte nodeType);

        /// <summary>
        /// 更新节点的父编号
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="newCode">节点的新编码</param>
        [OperationContract(Name = "UpdateParentNodeId")]
        void UpdateParentNodeId(decimal nodeId, decimal parentNodeId, string newNodeCode);

        /// <summary>
        /// 获得父节点编号
        /// </summary>
        ///<param name="nodeId">编号</param>
        /// <returns> 父节点编号 </returns>
        [OperationContract(Name = "GetParentNodeId")]
        decimal GetParentNodeId(decimal nodeId);

        /// <summary>
        /// 获得子节点编码列表
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>编码</returns>
        [OperationContract(Name = "GetChildNodeCodes")]
        IList<string> GetChildNodeCodes(decimal parentNodeId);

        /// <summary>
        /// 获得子节点编码列表
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>编码</returns>
        [OperationContract(Name = "GetChildNodeCodesWithNodeType")]
        IList<string> GetChildNodeCodes(decimal parentNodeId, byte nodeType);

        /// <summary>
        /// 获得节点名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        [OperationContract(Name = "GetNodeNameByNodeId")]
        string GetNodeNameByNodeId(decimal nodeId);

        /// <summary>
        /// 通过节点编码获得节点编号
        /// </summary>
        ///<param name="nodeCode">节点编码</param>
        /// <returns> 编号 </returns>
        [OperationContract(Name = "GetNodeIdByNodeCode")]
        decimal GetNodeIdByNodeCode(string nodeCode);

        /// <summary>
        /// 通过节点编码获得节点编号
        /// </summary>
        ///<param name="nodeCode">节点编码</param>
        ///<param name="nodeType">节点类型</param>
        /// <returns> 编号 </returns>
        [OperationContract(Name = "GetNodeIdByNodeCodeAndType")]
        decimal GetNodeIdByNodeCode(string nodeCode, byte nodeType);

        /// <summary>
        /// 获得节点编码
        /// </summary>
        ///<param name="nodeId">编号</param>
        /// <returns> 节点编码 </returns>
        [OperationContract(Name = "GetNodeCodeByNodeId")]
        string GetNodeCodeByNodeId(decimal nodeId);

        /// <summary>
        /// 刷新所有子节点编码
        /// </summary>
        /// <param name="parentNodeId"></param>
        /// <returns></returns>
        [OperationContract(Name = "RefreshCode")]
        bool RefreshCode(decimal parentNodeId);

        /// <summary>
        /// 获得子节点编号列表
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>子节点编号列表</returns>
        [OperationContract(Name = "GetChildNodeIds")]
        IList<decimal> GetChildNodeIds(decimal parentNodeId);

        /// <summary>
        /// 获得编号与编码
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>编码</returns>
        [OperationContract(Name = "GetChildNodeIdAndCodes")]
        IList<KeyValueInfo> GetChildNodeIdAndCodes(decimal parentNodeId);

    }
}
