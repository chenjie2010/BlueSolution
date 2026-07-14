//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: TreeDropdownItems.cs
// 描述: 通用的下拉选择项操作类
// 作者：ChenJie 
// 编写日期：2018-01-17
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    ///通用的下拉选择项操作类
    /// </summary>
    public class TreeDropdownItems : ITreeDropdownHandler
    {
        #region 属性

        /// <summary>
        /// 根节点编号
        /// </summary>
        public decimal RootNodeId
        {
            get;
            set;
        }

        /// <summary>
        /// 分组节点类型
        /// </summary>
        public byte NodeType
        {
            get;
            set;
        }      
        
        /// <summary>
        /// 用编号为父节点为前提条件查询
        /// </summary>
        public bool QueryByRootNodeId
        {
            get;
            set;
        }  

        /// <summary>
        /// 通用节点契约
        /// </summary>
        public ICommonNodeContract CommonNodeContract
        {
            get;
            set;
        }

        /// <summary>
        /// 查询节点操作契约
        /// </summary>
        public ICommonNodeContract QueriedCommonNodeContract
        {
            set;
            get;
        }

        /// <summary>
        /// 显示父辈节点名称
        /// </summary>
        public bool ParentNamesShowed
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        /// <param name="rootNodeId"></param>
        /// <param name="nodeType"></param>
        /// <param name="parentNamesShowed"></param>
        public TreeDropdownItems(ICommonNodeContract commonNodeContract)
            : this(commonNodeContract, decimal.MinValue, 0, false)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        /// <param name="rootNodeId"></param>
        public TreeDropdownItems(ICommonNodeContract commonNodeContract, decimal rootNodeId)
            : this(commonNodeContract, rootNodeId, 0, false)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        /// <param name="rootNodeId"></param>
        public TreeDropdownItems(ICommonNodeContract commonNodeContract, decimal rootNodeId, bool parentNamesShowed)
            : this(commonNodeContract, rootNodeId, 0, parentNamesShowed)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        /// <param name="parentNamesShowed"></param>
        public TreeDropdownItems(ICommonNodeContract commonNodeContract, bool parentNamesShowed)
            : this(commonNodeContract, decimal.MinValue, 0, parentNamesShowed)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        /// <param name="nodeType"></param>
        public TreeDropdownItems(ICommonNodeContract commonNodeContract, byte nodeType)
            : this(commonNodeContract, decimal.MinValue, nodeType, false)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        /// <param name="rootNodeId"></param>
        /// <param name="nodeType"></param>
        public TreeDropdownItems(ICommonNodeContract commonNodeContract, decimal rootNodeId, byte nodeType)
            : this(commonNodeContract, rootNodeId, nodeType, false)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        /// <param name="rootNodeId"></param>
        /// <param name="nodeType"></param>
        /// <param name="parentNamesShowed"></param>
        public TreeDropdownItems(ICommonNodeContract commonNodeContract, decimal rootNodeId, byte nodeType, bool parentNamesShowed)
        {
            CommonNodeContract = commonNodeContract;
            QueriedCommonNodeContract = commonNodeContract;
            RootNodeId = rootNodeId;
            NodeType = nodeType;
            ParentNamesShowed = parentNamesShowed;
            QueryByRootNodeId = false;
        }

        #endregion

        #region 接口方法

        /// <summary>
        /// 初始化获得节点
        /// </summary>
        /// <returns></returns>
        public IList<CommonNode> InitTree()
        {
            if (NodeType > 0)
            {
                return CommonNodeContract.GetChildNodes(RootNodeId, NodeType);
            }
            else
            {
                return CommonNodeContract.GetChildNodes(RootNodeId);
            }
        }
                
        /// <summary>
        /// 扩展后获得的节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public IList<CommonNode> AfterExpand(TreeNode treeNode)
        {
            CommonNode commonNode = treeNode.Tag as CommonNode;
            if (NodeType > 0)
            {
                return CommonNodeContract.GetChildNodes(commonNode.NodeId, NodeType);
            }
            else
            {

                return CommonNodeContract.GetChildNodes(commonNode.NodeId);
            }
        }

        /// <summary>
        /// 查询获得的节点
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IList<CommonNode> Query(string content)
        {
            if (QueryByRootNodeId)
            {
                return QueriedCommonNodeContract.GetChildNodes(RootNodeId, string.Format("%{0}%", content));
            }
            else
            {
                return QueriedCommonNodeContract.GetChildNodes(string.Format("%{0}%", content));
            }
        }

        /// <summary>
        /// 获取提示
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public string GetToolTipText(TreeNode treeNode)
        {
            CommonNode commonNode = treeNode.Tag as CommonNode;
            IList<string> names = CommonNodeContract.GetHierarchicalNamesOfNode(commonNode.NodeId);
            StringBuilder sb = new StringBuilder();
            foreach (string name in names)
            {
                sb.AppendFormat("[{0}]", name);
            }

            return sb.ToString();
        }

        #endregion
    }
}
