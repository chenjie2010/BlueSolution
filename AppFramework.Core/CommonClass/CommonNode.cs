//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonNode.cs
// 描述： CommonNode 操作服务类
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 在构成树形节点过程中使用的通用类
    /// </summary>
    [Serializable]
    public class CommonNode
    {
        #region 内部成员变量

        private decimal _nodeId;
        private decimal _parentNodeId;
        private string _nodeName;        
        private string _nodeCode;
        private bool _isLeaf;
        private byte _nodeType;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CommonNode()
        {
            _nodeId = decimal.MinValue;
            _parentNodeId = decimal.MinValue;
            _nodeName = string.Empty;
            _isLeaf = false;
            _nodeCode = string.Empty;
            _nodeType = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="nodeName">节点名称</param>
        public CommonNode(decimal nodeId, string nodeName)
            : this(nodeId, decimal.MinValue, nodeName, string.Empty, true, 0)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="isLeaf">是否叶子节点</param>
        /// <param name="nodeCode">节点值</param>
        public CommonNode(decimal nodeId, string nodeName, string nodeCode)
            : this(nodeId, decimal.MinValue, nodeName, nodeCode, true, 0)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeName">节点编号</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeType">节点类型</param>
        public CommonNode(decimal nodeId, string nodeName, byte nodeType)
            : this(nodeId, decimal.MinValue, nodeName, string.Empty, true, nodeType)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeName">节点名称</param>
        public CommonNode(decimal nodeId, decimal parentNodeId, string nodeName)
            : this(nodeId, parentNodeId, nodeName, string.Empty, true, 0)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="isLeaf">是否叶子节点</param>
        public CommonNode(decimal nodeId, decimal parentNodeId, string nodeName, bool isLeaf)
            : this(nodeId, parentNodeId, nodeName, string.Empty, isLeaf, 0)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="isLeaf">是否叶子节点</param>
        /// <param name="nodeCode">节点值</param>
        public CommonNode(decimal nodeId, decimal parentNodeId, string nodeName, string nodeCode)
            : this(nodeId, parentNodeId, nodeName, nodeCode, true, 0)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeCode">节点的值</param>
        /// <param name="isLeaf">是否叶子节点</param>
        public CommonNode(decimal nodeId, decimal parentNodeId, string nodeName, string nodeCode, bool isLeaf)
            : this(nodeId, parentNodeId, nodeName, nodeCode, isLeaf, 0)
        {
                       
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeCode">节点的值</param>
        /// <param name="nodeType">节点类型</param>
        public CommonNode(decimal nodeId, decimal parentNodeId, string nodeName, string nodeCode, byte nodeType)
           : this(nodeId, parentNodeId, nodeName, nodeCode, true, nodeType)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeCode">节点的值</param>
        /// <param name="isLeaf">是否叶子节点</param>
        /// <param name="nodeType">节点类型</param>
        public CommonNode(decimal nodeId, decimal parentNodeId, string nodeName, string nodeCode, bool isLeaf, byte nodeType)
        {
            _nodeId = nodeId;
            _parentNodeId = parentNodeId;
            _nodeName = nodeName;
            _nodeCode = nodeCode;
            _isLeaf = isLeaf;
            _nodeType = nodeType;
        }        

        #endregion

        #region 属性

        /// <summary>
        /// 节点编号
        /// </summary>
        public decimal NodeId
        {
            get
            {
                return _nodeId;
            }
            set
            {
                _nodeId = value;
            }
        }

        /// <summary>
        /// 父节点编号
        /// </summary>
        public decimal ParentNodeId
        {
            get
            {
                return _parentNodeId;
            }
            set
            {
                _parentNodeId = value;
            }
        }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName
        {
            get
            {
                return _nodeName;
            }
            set
            {
                _nodeName = value;
            }
        }


        /// <summary>
        /// 是否是叶子节点
        /// </summary>
        public Boolean IsLeaf
        {
            get
            {
                return _isLeaf;
            }
            set
            {
                _isLeaf = value;
            }
        }

        /// <summary>
        /// 节点的值
        /// </summary>
        public string NodeCode
        {
            get
            {
                return _nodeCode;
            }
            set
            {
                _nodeCode = value;
            }
        }


        /// <summary>
        /// 节点类型
        /// </summary>
        public byte NodeType
        {
            get
            {
                return _nodeType;
            }
            set
            {
                _nodeType = value;
            }
        }

        #endregion

        #region 重载方法

        /// <summary>
        /// 获得名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _nodeName;
        }

        #endregion

    }
}
