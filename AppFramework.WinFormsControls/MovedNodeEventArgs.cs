using System;
using System.Windows.Forms;
using AppFramework.Core;

namespace AppFramework.WinFormsControls
{
    public class MovedNodeEventArgs : EventArgs
    {
        private TreeNode _treeNode;
        private MovedDriection _movedDriection;
        private int _previousNodeIndex;

        /// <summary>
        /// 移动方向
        /// </summary>
        public MovedDriection MovedDriection
        {
            get
            {
                return _movedDriection;
            }
            set
            {
                _movedDriection = value;
            }
        }

        /// <summary>
        /// 移动节点
        /// </summary>
        public TreeNode MovedTreeNode
        {
            get { return _treeNode; }
        }

        /// <summary>
        /// 移动前的索引
        /// </summary>
        public int PreviousNodeIndex
        {
            get
            {
                return _previousNodeIndex;
            }
        }
        public MovedNodeEventArgs(TreeNode node, int previousNodeIndex, MovedDriection movedDriection)
            : base()
        {
            _treeNode = node;
            _previousNodeIndex = previousNodeIndex;
            _movedDriection = movedDriection;
        }

    }
}
