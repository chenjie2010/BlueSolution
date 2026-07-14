//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonNodeEventArgs.cs
// 描述： 节点参数类
// 作者：ChenJie 
// 编写日期：2016-08-20
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace AppFramework.WinFormsLibrary.EventArgument
{
    /// <summary>
    /// 点击过程中节点参数
    /// </summary>
    public class TreeNodeItemClickEventArgs : ItemClickEventArgs
    {
        #region 定义成员变量

        private TreeNode _treeNode;
        private bool _cancel;

        #endregion

        #region 定义私有变量

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="cancel"></param>
        /// <param name="item"></param>
        /// <param name="link"></param>
        public TreeNodeItemClickEventArgs(TreeNode treeNode, bool cancel, BarItem item, BarItemLink link) : base(item, link)
        {
            _treeNode = treeNode;
            _cancel = cancel;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="treeNode">节点</param>
        /// <param name="item"></param>
        /// <param name="link"></param>
        public TreeNodeItemClickEventArgs(TreeNode treeNode, BarItem item, BarItemLink link) : base(item, link)
        {
            _treeNode = treeNode;
            _cancel = false;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 节点
        /// </summary>
        public TreeNode TreeNode
        {
            get
            {
                return _treeNode;
            }
            set
            {
                _treeNode = value;
            }
        }

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool Cancel
        {
            get
            {
                return _cancel;
            }
            set
            {
                _cancel = value;
            }
        }
        

        #endregion
    }
}