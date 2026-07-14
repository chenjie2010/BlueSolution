//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataValidationResult..cs
// 描述： 数据校验结果类
// 作者：ChenJie 
// 编写日期：2018/07/19
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据校验结果类
    /// </summary>
    public class DataValidationResult
    {
        #region 定义私有变量

        private int _sheetIndex;
        private int _row;
        private int _col;
        private DataImportedError _dataImportedError;

        #endregion

        #region 属性

        /// <summary>
        /// 索引
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
        /// 行
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
        public DataImportedError DataImportedError
        {
            get
            {
                return _dataImportedError;
            }
            set
            {
                _dataImportedError = value;
            }
        }
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DataValidationResult()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public DataValidationResult(int sheetIndex, int row, int col)
        {
            _sheetIndex = sheetIndex;
            _row = row;
            _col = col;
            _dataImportedError = DataImportedError.None;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="dataImportedError"></param>
        public DataValidationResult(int sheetIndex, int row, int col, DataImportedError dataImportedError)
        {
            _sheetIndex = sheetIndex;
            _row = row;
            _col = col;
            _dataImportedError = dataImportedError;
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
