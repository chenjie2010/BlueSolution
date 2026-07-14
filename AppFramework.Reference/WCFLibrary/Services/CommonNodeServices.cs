//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CommonNodeServices.cs
// 描述：节点服务类
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;

namespace AppFramework.Reference.WCFLibrary
{
    /// <summary>
    /// 节点服务类
    /// </summary>
    public abstract class CommonNodeServices : ICommonNodeContract
    {
        #region 私有变量

        private readonly ICommonNodeBusiness commonNodeBusiness;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="business"></param>
        public CommonNodeServices(ICommonNodeBusiness business)
        {
            commonNodeBusiness = business;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获得完整名称
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public string GetFullName(decimal nodeId)
        {
            return commonNodeBusiness.GetFullName(nodeId);
        }

        /// <summary>
        /// 获得 CommonNode 对象
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public CommonNode GetCommonNode(decimal nodeId)
        {
            return commonNodeBusiness.GetCommonNode(nodeId);
        }

        /// <summary>
        /// 获得扩展的 CommonNode
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public ExtendedCommonNode GetExtendedCommonNode(decimal nodeId)
        {
            return commonNodeBusiness.GetExtendedCommonNode(nodeId);
        }

        /// <summary>
        /// 节点名称由父辈节点名称和自身名称一起组成
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public CommonNode GetCommonNodeWithParentNames(decimal nodeId)
        {
            return commonNodeBusiness.GetCommonNodeWithParentNames(nodeId);
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary> 
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetCommonNodes()
        {
            return commonNodeBusiness.GetCommonNodes();
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="nodeName">单位名称</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(string nodeName, IList<string> nodeCodes)
        {
            return commonNodeBusiness.GetChildNodes(nodeName, nodeCodes);
        }

        /// <summary>
        /// 根据节点名称或是编号查询 CommonNode 对象的列表
        /// </summary>	
        /// <param name="condition">条件</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(string condition)
        {
            return commonNodeBusiness.GetChildNodes(condition);
        }

        /// <summary>
        /// 根据节点名称或是编号查询 CommonNode 对象的列表
        /// </summary>	
        /// <param name="rootNodeId">根节点编号</param>
        /// <param name="condition">条件</param>
        /// <returns>CommonNode 对象的列表</returns>        
        public IList<CommonNode> GetChildNodes(decimal rootNodeId, string condition)
        {
            return commonNodeBusiness.GetChildNodes(rootNodeId, condition);
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(decimal parentNodeId)
        {
            return commonNodeBusiness.GetChildNodes(parentNodeId);
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="keyId">关键字编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(decimal parentNodeId, decimal keyId)
        {
            return commonNodeBusiness.GetChildNodes(parentNodeId, keyId);
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeType">父节点编号</param>
        /// <param name="keyId">关键字编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(decimal parentNodeId, byte nodeType, decimal keyId)
        {
            return commonNodeBusiness.GetChildNodes(parentNodeId, nodeType, keyId);
        }
        
        /// <summary>
        /// 由父节点编号获得下一级子节点的数目
        /// </summary>
        /// <param name="parentNodeId">父节点编号</param>
        /// <returns>下一级子节点的数目</returns>
        public int GetTotalCountOfChildNode(decimal parentNodeId)
        {
            return commonNodeBusiness.GetTotalCountOfChildNode(parentNodeId);
        }

        /// <summary>
        /// 获得节点的所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            return commonNodeBusiness.GetHierarchicalNamesOfNode(nodeId);
        }

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="nodeId">移动的节点编号</param>
        /// <param name="otherNodeId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateSorting(decimal nodeId, decimal otherNodeId, MovedDriection movedDriectionOfNode)
        {
            commonNodeBusiness.UpdateSorting(nodeId, otherNodeId, movedDriectionOfNode);
        }

        /// <summary>
        /// 同一级是否存在相同的名称
        /// </summary>
        /// <param name="parentNodeId">节点父编号</param>
        /// <param name="nodeName">节点名称</param>        
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalName(decimal parentNodeId, string nodeName)
        {
            return commonNodeBusiness.IsExistIdenticalName(parentNodeId, nodeName);
        }

        /// <summary>
        /// 同一级是否存在相同的名称
        /// </summary>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalName(decimal parentNodeId, string nodeName, byte nodeType)
        {
            return commonNodeBusiness.IsExistIdenticalName(parentNodeId, nodeName, nodeType);
        }

        /// <summary>
        /// 是否存在相同的名称
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalName(string nodeName)
        {
            return commonNodeBusiness.IsExistIdenticalName(nodeName);
        }

        /// <summary>
        /// 是否存在相同的编码
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalCode(string nodeCode)
        {
            return commonNodeBusiness.IsExistIdenticalCode(nodeCode);
        }

        /// <summary>
        /// 是否存在相同的名称
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalName(string nodeName, byte nodeType)
        {
            return commonNodeBusiness.IsExistIdenticalName(nodeName, nodeType);
        }

        /// <summary>
        /// 是否存在相同的编码
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalCode(string nodeCode, byte nodeType)
        {
            return commonNodeBusiness.IsExistIdenticalCode(nodeCode, nodeType);
        }

        /// <summary>
        /// 更新节点的父编号
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="newCode">节点的新编码</param>
        public void UpdateParentNodeId(decimal nodeId, decimal parentNodeId, string newCode)
        {
            commonNodeBusiness.UpdateParentNodeId(nodeId, parentNodeId, newCode);
        }

        /// <summary>
        /// 获得父节点编号
        /// </summary>
        ///<param name="nodeId">编号</param>
        /// <returns> 父节点编号 </returns>
        public decimal GetParentNodeId(decimal nodeId)
        {
            return commonNodeBusiness.GetParentNodeId(nodeId);
        }

        /// <summary>
        /// 获得子节点编码列表
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>编码</returns>
        public IList<string> GetChildNodeCodes(decimal parentNodeId)
        {
            return commonNodeBusiness.GetChildNodeCodes(parentNodeId);
        }

        /// <summary>
        /// 获得子节点编码列表
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>编码</returns>
        public IList<string> GetChildNodeCodes(decimal parentNodeId, byte nodeType)
        {
            return commonNodeBusiness.GetChildNodeCodes(parentNodeId, nodeType);
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(decimal parentNodeId, byte nodeType)
        {
            return commonNodeBusiness.GetChildNodes(parentNodeId, nodeType);
        }

        /// <summary>
        /// 获得节点名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public string GetNodeNameByNodeId(decimal nodeId)
        {
            return commonNodeBusiness.GetNodeNameByNodeId(nodeId);
        }

        /// <summary>
        /// 通过节点编码获得节点编号
        /// </summary>
        ///<param name="nodeCode">节点编码</param>
        /// <returns> 编号 </returns>
        public decimal GetNodeIdByNodeCode(string nodeCode)
        {
            return commonNodeBusiness.GetNodeIdByNodeCode(nodeCode);
        }

        /// <summary>
        /// 通过节点编码获得节点编号
        /// </summary>
        ///<param name="nodeCode">节点编码</param>
        ///<param name="nodeType">节点类型</param>
        /// <returns> 编号 </returns>
        public decimal GetNodeIdByNodeCode(string nodeCode, byte nodeType)
        {
            return commonNodeBusiness.GetNodeIdByNodeCode(nodeCode, nodeType);
        }

        /// <summary>
        /// 获得节点编码
        /// </summary>
        ///<param name="nodeId">编号</param>
        /// <returns> 节点编码 </returns>
        public string GetNodeCodeByNodeId(decimal nodeId)
        {
            return commonNodeBusiness.GetNodeCodeByNodeId(nodeId);
        }

        /// <summary>
        /// 刷新所有子节点编码
        /// </summary>
        /// <param name="parentNodeId"></param>
        /// <returns></returns>
        public bool RefreshCode(decimal parentNodeId)
        {
            return commonNodeBusiness.RefreshCode(parentNodeId);
        }

        /// <summary>
        /// 获得子节点编号列表
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>子节点编号列表</returns>
        public IList<decimal> GetChildNodeIds(decimal parentNodeId)
        {
            return commonNodeBusiness.GetChildNodeIds(parentNodeId);
        }

        /// <summary>
        /// 获得编号与编码
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>编码</returns>
        public IList<KeyValueInfo> GetChildNodeIdAndCodes(decimal parentNodeId)
        {
            return commonNodeBusiness.GetChildNodeIdAndCodes(parentNodeId);
        }

        #endregion
    }

}
