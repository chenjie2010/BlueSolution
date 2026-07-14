//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ConfirmedEventArgs.cs
// 描述： 确认操作参数
// 作者：ChenJie 
// 编写日期：2016-08-20
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Windows.Forms;
using AppFramework.Core;

namespace AppFramework.WinFormsLibrary.EventArgument
{
    /// <summary>
    /// 确认操作参数
    /// </summary>
    public class TreeNodeEditEventArgs : EventArgs
    {
        #region 定义成员变量

        private TreeNode _treeNode;
        private EditState _editState;
        private bool _cancel;

        #endregion

        #region 定义私有变量

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="state"></param>
        public TreeNodeEditEventArgs(TreeNode treeNode, EditState state)
        {
            _treeNode = treeNode;
            _editState = state;
            _cancel = false;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 数据
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
        /// 确认状态
        /// </summary>
        public EditState EditState
        {
            get
            {
                return _editState;
            }
            set
            {
                _editState = value;
            }
        }

        /// <summary>
        /// 操作是否取消
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
    }

    #endregion
}
