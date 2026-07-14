using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using AppFramework.Core;

namespace AppFramework.WinFormsControls
{
    public class ExtendedItemClickEventArgs : ItemClickEventArgs
    {
        #region 内部成员变量

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
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="e"></param>
        /// <param name="movedDriection"></param>
        public ExtendedItemClickEventArgs(ItemClickEventArgs e, MovedDriection movedDriection)
            : base(e.Item, e.Link)
        {
            _movedDriection = movedDriection;
        }

        #endregion
    }
}
