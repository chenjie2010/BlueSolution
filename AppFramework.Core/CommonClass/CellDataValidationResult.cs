//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CellDataValidationResult.cs
// 描述： 数据单元格校验结果类
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
    public class CellDataValidationResult
    {
        #region 定义私有变量

        private int _sheetIndex;
        private int _row;
        private int _col;
        private ErrorDataFormat _errorDataFormat;

        #endregion

        #region 属性

        /// <summary>
        /// 页面索引
        /// </summary>
        public int SheetIndex
        {
            get
            {
                return _sheetIndex;
            }
            set
            {
                _sheetIndex = value;
            }
        }

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
        public ErrorDataFormat ErrorDataFormat
        {
            get
            {
                return _errorDataFormat;
            }
            set
            {
                _errorDataFormat = value;
            }
        }
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CellDataValidationResult()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public CellDataValidationResult(int row, int col)
        {
            _sheetIndex = -1;
            _row = row;
            _col = col;
            _errorDataFormat = ErrorDataFormat.None;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public CellDataValidationResult(int sheetIndex, int row, int col)
        {
            _sheetIndex = sheetIndex;
            _row = row;
            _col = col;
            _errorDataFormat = ErrorDataFormat.None;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="errorDataFormat"></param>
        public CellDataValidationResult(int row, int col, ErrorDataFormat errorDataFormat)
        {
            _sheetIndex = -1;
            _row = row;
            _col = col;
            _errorDataFormat = errorDataFormat;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="errorDataFormat"></param>
        public CellDataValidationResult(int sheetIndex, int row, int col, ErrorDataFormat errorDataFormat)
        {
            _sheetIndex = sheetIndex;
            _row = row;
            _col = col;
            _errorDataFormat = errorDataFormat;
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
