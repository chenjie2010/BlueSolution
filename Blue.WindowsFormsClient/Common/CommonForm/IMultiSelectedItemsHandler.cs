using System;
//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IMultiSelectedItemsHandler.cs
// 描述: 多选处理接口
// 作者：ChenJie 
// 编写日期：2018-01-16
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
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
    /// 多选处理接口
    /// </summary>
    public interface IMultiSelectedItemsHandler
    {
        /// <summary>
        /// 只选择叶子节点
        /// </summary>
        bool OnlySelectedLeaf
        {
            get;
            set;
        }

        /// <summary>
        /// 节点操作契约
        /// </summary>
        ICommonNodeContract CommonNodeContract
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化获得节点
        /// </summary>
        /// <returns></returns>
        IList<CommonNode> InitTree();        

        /// <summary>
        /// 扩展后获得的节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        IList<CommonNode> AfterExpand(TreeNode treeNode);

        /// <summary>
        /// 查询获得的节点
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IList<CommonNode> Query(string text);

        /// <summary>
        /// 获得被选择的列表
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="departmentIdList"></param>
        void GetNodeList(TreeNodeCollection tc, IList<CommonNode> commonNodes);

        /// <summary>
        /// 判断是否是叶子节点
        /// </summary>
        /// <param name="tn"></param>
        /// <returns></returns>
        bool IsLeafNode(TreeNode tn);

    }
}
