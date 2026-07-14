//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CellValidationResult.cs
// 描述： 单元格校验结果类
// 作者：ChenJie 
// 编写日期：2018/07/13
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 单元格校验结果类
    /// </summary>
    public class CellValidationResult
    {
        #region 定义私有变量

        private int _row;
        private int _col;
        private UserToolError _userToolError;

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

        /// <summary>
        /// 校验结果
        /// </summary>
        public UserToolError UserToolError
        {
            get
            {
                return _userToolError;
            }
            set
            {
                _userToolError = value;
            }
        }
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CellValidationResult()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public CellValidationResult(int row, int col)
        {
            _row = row;
            _col = col;
            _userToolError = UserToolError.None;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="userToolError"></param>
        public CellValidationResult(int row, int col, UserToolError userToolError)
        {
            _row = row;
            _col = col;
            _userToolError = userToolError;
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
