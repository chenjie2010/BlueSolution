//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： RowAndCol.cs
// 描述： 行列类
// 作者：ChenJie 
// 编写日期：2018/07/09
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 行列
    /// </summary>
    public class RowAndCol
    {
        #region 定义私有变量

        private int _row;
        private int _col;
        
        #endregion

        #region 属性

        /// <summary>
        /// 值
        /// </summary>
        public int Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        /// <summary>
        /// 列
        /// </summary>
        public int Col
        {
            get
            {
                return _col;
            }
            set
            {
                _col = value;
            }
        }        
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RowAndCol()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public RowAndCol(int row, int col)
        {
            _row = row;
            _col = col;
        }

        #endregion

        #region 重载方法

        /// <summary>
        /// 获得名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}_{1}", _row, _col);
        }

        #endregion
    }
}
