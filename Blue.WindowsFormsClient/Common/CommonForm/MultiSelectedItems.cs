//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: MultiSelectedItems.cs
// 描述: 通用的多项选择
// 作者：ChenJie 
// 编写日期：2018-01-16
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
    ///通用的多项选择
    /// </summary>
    public class MultiSelectedItems : IMultiSelectedItemsHandler
    {
        /// <summary>
        /// 只选择叶子节点
        /// </summary>
        public bool OnlySelectedLeaf
        {
            get;
            set;
        }

        /// <summary>
        /// 节点操作契约
        /// </summary>
        public ICommonNodeContract CommonNodeContract
        {
            set;
            get;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        public MultiSelectedItems(ICommonNodeContract commonNodeContract, bool onlySelectedLeaf)
        {
            CommonNodeContract = commonNodeContract;
            OnlySelectedLeaf = onlySelectedLeaf;
        }

        /// <summary>
        /// 初始化获得节点
        /// </summary>
        /// <returns></returns>
        public IList<CommonNode> InitTree()
        {
            return CommonNodeContract.GetChildNodes(decimal.MinValue);
        }

        /// <summary>
        /// 扩展后获得的节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public IList<CommonNode> AfterExpand(TreeNode treeNode)
        {
            CommonNode commonNode = treeNode.Tag as CommonNode;

            return CommonNodeContract.GetChildNodes(commonNode.NodeId);
        }

        /// <summary>
        /// 查询获得的节点
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IList<CommonNode> Query(string content)
        {
            return CommonNodeContract.GetChildNodes(string.Format("%{0}%", content));
        }

        /// <summary>
        /// 获得被选择的单位列表
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="departmentIdList"></param>
        public void GetNodeList(TreeNodeCollection tc, IList<CommonNode> commonNodes)
        {
            foreach (TreeNode tn in tc)
            {
                if (tn.Checked && ((OnlySelectedLeaf && tn.Nodes.Count == 0) || !OnlySelectedLeaf))
                {
                    commonNodes.Add(tn.Tag as CommonNode);
                }
                GetNodeList(tn.Nodes, commonNodes);
            }
        }

        /// <summary>
        /// 判断是否是叶子节点
        /// </summary>
        /// <param name="tn"></param>
        /// <returns></returns>
        public bool IsLeafNode(TreeNode tn)
        {
            return tn.Nodes.Count == 0;
        }
    }
}
