//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserTypeMultiSelectedItems.cs
// 描述: UserTypeMultiSelectedItems
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
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 枚举多选操作
    /// </summary>
    public class EnumMultiSelectedItems : IMultiSelectedItemsHandler
    {

        #region 契约接口
        
        private readonly ICustomEnumContract customEnumContract;

        #endregion       

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
            get;
            set;
        }

        #endregion       

        #region  构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customEnumContract"></param>
        /// <param name="onlySelectedLeaf"></param>
        public EnumMultiSelectedItems(ICustomEnumContract customEnumContract, decimal rootNodeId, bool onlySelectedLeaf)
        {
            RootNodeId = rootNodeId;
            CommonNodeContract = customEnumContract;
            this.customEnumContract = customEnumContract;
        }

        #endregion

        #region  受保护方法

        /// <summary>
        /// 判断是否是叶子节点
        /// </summary>
        /// <param name="tn"></param>
        /// <returns></returns>
        public bool IsLeafNode(TreeNode tn)
        {
            return tn.Nodes.Count == 0;
        }

        #endregion

        #region  受保护方法

        /// <summary>
        /// 初始化获得节点
        /// </summary>
        /// <returns></returns>
        public IList<CommonNode> InitTree()
        {
            return customEnumContract.GetChildNodes(RootNodeId);
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
        /// <param name="text"></param>
        /// <returns></returns>
        public IList<CommonNode> Query(string text)
        {
            return CommonNodeContract.GetChildNodes(string.Format("%{0}%", text));
        }

        /// <summary>
        /// 获得被选择的列表
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="departmentIdList"></param>
        public void GetNodeList(TreeNodeCollection tc, IList<CommonNode> commonNodes)
        {
            foreach (TreeNode tn in tc)
            {
                if (tn.Checked)
                {
                    commonNodes.Add(tn.Tag as CommonNode);
                }
                GetNodeList(tn.Nodes, commonNodes);
            }
        }


        #endregion
    }
}
