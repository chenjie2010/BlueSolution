//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CommonNodeBusiness.cs
// 描述: 处理节点的操作类
// 作者：ChenJie 
// 编写日期：2016/09/23
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.EnterpriseLibrary;

namespace AppFramework.Reference.BusinessLibrary
{
    /// <summary>
    /// 处理节点的操作类
    /// </summary>
    public class CommonNodeBusiness : ICommonNodeBusiness
    {
        #region 工厂类实例

        private ICommonNode dalNode;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dalNode"></param>
        public CommonNodeBusiness(ICommonNode dalNode)
        {
            this.dalNode = dalNode;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得完整名称
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public string GetFullName(decimal nodeId)
        {
            StringBuilder sb = new StringBuilder();

            IList<string> names = GetHierarchicalNamesOfNode(nodeId);
            foreach (var name in names)
            {
                sb.AppendFormat("[{0}]", name);
            }

            return sb.ToString();
        }
        
        /// <summary>
        /// 节点名称由父辈节点名称和自身名称一起组成
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public CommonNode GetCommonNodeWithParentNames(decimal nodeId)
        {
            CommonNode commonNode = null;
            try
            {
                commonNode = GetCommonNode(nodeId);
                StringBuilder sb = new StringBuilder();
                IList<string> names = GetHierarchicalNamesOfNode(nodeId);
                foreach (string name in names)
                {
                    sb.AppendFormat("[0]", name);
                }
                commonNode.NodeName = sb.ToString();
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNode;
        }

        /// <summary>
        /// 获得扩展的 CommonNode
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public ExtendedCommonNode GetExtendedCommonNode(decimal nodeId)
        {
            ExtendedCommonNode extendedCommonNode = null;

            if (nodeId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                extendedCommonNode = dalNode.GetExtendedCommonNode(nodeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return extendedCommonNode;
        }        

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary> 
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetCommonNodes()
        {
            IList<CommonNode> commonNodes = null;

            try
            {
                commonNodes = dalNode.GetCommonNodes();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="nodeName">单位名称</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(string nodeName, IList<string> nodeCodes)
        {
            IList<CommonNode> extendedNodes = null;

            try
            {
                extendedNodes = dalNode.GetChildNodes(nodeName, nodeCodes);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return extendedNodes;
        }

        /// <summary>
        /// 根据节点名称或是编号查询 CommonNode 对象的列表
        /// </summary>	
        /// <param name="rootNodeId">根节点编号</param>
        /// <param name="condition">条件</param>
        /// <returns>CommonNode 对象的列表</returns>        
        public IList<CommonNode> GetChildNodes(decimal rootNodeId, string condition)
        {
            IList<CommonNode> nodes = null;

            try
            {
                nodes = dalNode.GetChildNodes(rootNodeId, condition);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodes;
        }

        /// <summary>
        /// 根据节点名称或是编号查询 CommonNode 对象的列表
        /// </summary>	
        /// <param name="condition">条件</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(string condition)
        {
            IList<CommonNode> nodes = null;

            try
            {
                nodes = dalNode.GetChildNodes(condition);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodes;
        }        

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(decimal parentNodeId, byte nodeType)
        {
            IList<CommonNode> commonNodes = null;

            try
            {
                commonNodes = dalNode.GetChildNodes(parentNodeId, nodeType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(IList<decimal> nodeIds)
        {
            IList<CommonNode> extendedNodes = null;

            try
            {
                extendedNodes = dalNode.GetCommonNodes(nodeIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return extendedNodes;
        }

        /// <summary>
        /// 由父节点编号获得下一级子节点的数目
        /// </summary>
        /// <param name="parentNodeId">父节点编号</param>
        /// <returns>下一级子节点的数目</returns>
        public int GetTotalCountOfChildNode(decimal parentNodeId)
        {
            int count = 0;

            try
            {
                count = dalNode.GetTotalCountOfChildNode(parentNodeId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得节点的所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> parentNames = null;

            try
            {
                parentNames = dalNode.GetHierarchicalNamesOfNode(nodeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return parentNames;
        }

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="userTypeId">移动的节点编号</param>
        /// <param name="otherNodeId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateSorting(decimal nodeId, decimal otherNodeId, MovedDriection movedDriectionOfNode)
        {
            // 验证输入
            if (nodeId < 0 || otherNodeId < 0)
            {
                throw new ArgumentException("编号错误.");
            }

            try
            {
                dalNode.UpdateSorting(nodeId, otherNodeId, movedDriectionOfNode);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 同一级是否存在相同的名称
        /// </summary>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeName">节点名称</param>       
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalName(decimal parentNodeId, string nodeName)
        {
            bool exist = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                throw new ArgumentException("节点名称不能为空.");
            }

            try
            {
                exist = dalNode.IsExistIdenticalName(parentNodeId, nodeName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
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
            bool exist = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                throw new ArgumentException("节点名称不能为空.");
            }

            try
            {
                exist = dalNode.IsExistIdenticalName(parentNodeId, nodeName, nodeType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 是否存在相同的名称
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalName(string nodeName)
        {
            bool exist = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                throw new ArgumentException("节点名称不能为空.");
            }

            try
            {
                exist = dalNode.IsExistIdenticalName(nodeName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 是否存在相同的编码
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalCode(string nodeCode)
        {
            bool exist = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(nodeCode))
            {
                throw new ArgumentException("节点编码不能为空.");
            }

            try
            {
                exist = dalNode.IsExistIdenticalCode(nodeCode);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 是否存在相同的名称
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalName(string nodeName, byte nodeType)
        {
            bool exist = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                throw new ArgumentException("节点名称不能为空.");
            }

            try
            {
                exist = dalNode.IsExistIdenticalName(nodeName, nodeType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 是否存在相同的编码
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalCode(string nodeCode, byte nodeType)
        {
            bool exist = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(nodeCode))
            {
                throw new ArgumentException("节点编码不能为空.");
            }

            try
            {
                exist = dalNode.IsExistIdenticalCode(nodeCode, nodeType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }        

        /// <summary>
        /// 更新节点的父编号
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="newCode">节点的新编码</param>
        public void UpdateParentNodeId(decimal nodeId, decimal parentNodeId, string newCode)
        {
            try
            {
                dalNode.UpdateParentNodeId(nodeId, parentNodeId, newCode);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得父节点编号
        /// </summary>
        ///<param name="nodeId">编号</param>
        /// <returns> 父节点编号 </returns>
        public decimal GetParentNodeId(decimal nodeId)
        {
            decimal parentNodeId = decimal.MinValue;

            try
            {
                parentNodeId = dalNode.GetParentNodeId(nodeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return parentNodeId;
        }

        /// <summary>
        /// 获得子节点编码列表
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>编码</returns>
        public IList<string> GetChildNodeCodes(decimal parentNodeId)
        {
            //创建集合对象
            IList<string> childNodeCodes = null;

            try
            {
                childNodeCodes = dalNode.GetChildNodeCodes(parentNodeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return childNodeCodes;
        }

        /// <summary>
        /// 获得子节点编码列表
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>编码</returns>
        public IList<string> GetChildNodeCodes(decimal parentNodeId, byte nodeType)
        {
            //创建集合对象
            IList<string> childNodeCodes = null;

            try
            {
                childNodeCodes = dalNode.GetChildNodeCodes(parentNodeId, nodeType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return childNodeCodes;
        }

        /// <summary>
        /// 获得节点名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public string GetNodeNameByNodeId(decimal nodeId)
        {
            string nodeName = string.Empty;

            try
            {
                nodeName = dalNode.GetNodeNameByNodeId(nodeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodeName;
        }

        /// <summary>
        /// 通过节点编码获得节点编号
        /// </summary>
        ///<param name="nodeCode">节点编码</param>
        /// <returns> 编号 </returns>
        public decimal GetNodeIdByNodeCode(string nodeCode)
        {
            decimal nodeId = decimal.MinValue;

            try
            {
                nodeId = dalNode.GetNodeIdByNodeCode(nodeCode);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodeId;
        }

        /// <summary>
        /// 通过节点编码获得节点编号
        /// </summary>
        ///<param name="nodeCode">节点编码</param>
        ///<param name="nodeType">节点类型</param>
        /// <returns> 编号 </returns>
        public decimal GetNodeIdByNodeCode(string nodeCode, byte nodeType)
        {
            decimal nodeId = decimal.MinValue;

            try
            {
                nodeId = dalNode.GetNodeIdByNodeCode(nodeCode, nodeType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodeId;
        }

        /// <summary>
        /// 获得节点编码
        /// </summary>
        ///<param name="nodeId">编号</param>
        /// <returns> 节点编码 </returns>
        public string GetNodeCodeByNodeId(decimal nodeId)
        {
            string nodeCode = string.Empty;

            try
            {
                nodeCode = dalNode.GetNodeCodeByNodeId(nodeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodeCode;
        }

        /// <summary>
        /// 刷新所有子节点编码
        /// </summary>
        /// <param name="parentNodeId"></param>
        /// <returns></returns>
        public bool RefreshCode(decimal parentNodeId)
        {
            bool result = true;

            try
            {
                result = dalNode.RefreshCode(parentNodeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 获得子节点编号列表
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>子节点编号列表</returns>
        public IList<decimal> GetChildNodeIds(decimal parentNodeId)
        {
            //创建集合对象
            IList<decimal> childNodeIds = null;

            try
            {
                childNodeIds = dalNode.GetChildNodeIds(parentNodeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return childNodeIds;

        }

        /// <summary>
        /// 获得编号与编码
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>编码</returns>
        public IList<KeyValueInfo> GetChildNodeIdAndCodes(decimal parentNodeId)
        {
            //集合对象
            IList<KeyValueInfo> childNodeIdAndCodes = null;

            try
            {
                childNodeIdAndCodes = dalNode.GetChildNodeIdAndCodes(parentNodeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return childNodeIdAndCodes;
        }


        #endregion

        #region 虚拟方法

        /// <summary>
        /// 获得 CommonNode 对象
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public virtual CommonNode GetCommonNode(decimal nodeId)
        {
            CommonNode commonNode = null;

            if (nodeId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonNode = dalNode.GetCommonNode(nodeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNode;
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentId">父节点编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        public virtual IList<CommonNode> GetChildNodes(decimal parentId)
        {
            IList<CommonNode> extendedNodes = null;

            try
            {
                extendedNodes = dalNode.GetChildNodes(parentId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return extendedNodes;
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeType">父节点编号</param>
        /// <param name="keyId">关键字编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        public virtual IList<CommonNode> GetChildNodes(decimal parentNodeId, byte nodeType, decimal keyId)
        {
            IList<CommonNode> extendedNodes = null;

            try
            {
                extendedNodes = dalNode.GetChildNodes(parentNodeId, nodeType, keyId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return extendedNodes;
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="keyId">关键字编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        public virtual IList<CommonNode> GetChildNodes(decimal parentNodeId, decimal keyId)
        {
            IList<CommonNode> extendedNodes = null;

            try
            {
                extendedNodes = dalNode.GetChildNodes(parentNodeId, keyId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return extendedNodes;
        }

        #endregion

    }
}
