//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MovedNodeEventArgs.cs
// 描述： 节点移动事件
// 作者：ChenJie 
// 编写日期：2016-08-18
// 版权所有 (C) 四川大学 2016
using System;
using AppFramework.Core;

namespace AppFramework.WinFormsLibrary
{
    /// <summary>
    /// 节点移动事件
    /// </summary>
    public class MovedItemEventArgs : EventArgs
    {
        #region 内部成员变量

        /// <summary>
        /// 移动方向
        /// </summary>
        private MovedDriection _movedDriection;

        #endregion

        #region 属性

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

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="movedDriection"></param>
        public MovedItemEventArgs(MovedDriection movedDriection)
            : base()
        {
            _movedDriection = movedDriection;
        }

        #endregion
    }
}
