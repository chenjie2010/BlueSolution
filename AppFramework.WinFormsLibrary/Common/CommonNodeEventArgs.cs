//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonNodeEventArgs.cs
// 描述： 节点移动事件
// 作者：ChenJie 
// 编写日期：2017-10-30
// 版权所有 (C) 四川大学 2017
using System;
using AppFramework.Core;

namespace AppFramework.WinFormsLibrary
{
    /// <summary>
    /// 节点事件
    /// </summary>
    public class CommonNodeEventArgs : EventArgs
    {
        #region 内部成员变量

        /// <summary>
        /// 通用节点
        /// </summary>
        private CommonNode _commonNode;

        #endregion

        #region 属性

        /// <summary>
        /// 移动方向
        /// </summary>
        public CommonNode CommonNode
        {
            get
            {
                return _commonNode;
            }
            set
            {
                _commonNode = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="node"></param>
        public CommonNodeEventArgs(CommonNode node)
            : base()
        {
            _commonNode = node;
        }

        #endregion
    }
}
