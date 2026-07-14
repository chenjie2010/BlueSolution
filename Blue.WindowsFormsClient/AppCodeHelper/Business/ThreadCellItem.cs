//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CellItem.cs
// 描述： 单元格项类
// 作者：ChenJie 
// 编写日期：2018/10/16
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using AppFramework.Core;
using FarPoint.Win.Spread.CellType;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    ///  线程单元格项类
    /// </summary>
    public class ThreadCellItem
    {
        #region 定义私有变量

        private int _threadIndex;
        private int _sheetIndex;
        private int _row;
        private int _col;
        private string _cellText;
        private object _cellValue;
        private CustomBool _valueMode;
        private ICellType _cellType;

        #endregion

        #region 属性

        /// <summary>
        /// 线程索引
        /// </summary>
        public int ThreadIndex
        {
            get
            {
                return _threadIndex;
            }
            set
            {
                _threadIndex = value;
            }
        }
        
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
        /// 单元格文本
        /// </summary>
        public string  CellText
        {
            get
            {
                return _cellText;
            }
            set
            {
                _cellText = value;
            }
        }

       /// <summary>
       /// 单元格值
       /// </summary>
        public object CellValue
        {
            get
            {
                return _cellValue;
            }
            set
            {
                _cellValue = value;
            }            
        }

        /// <summary>
        /// 是否是值模式
        /// </summary>
        public CustomBool ValueMode
        {
            get
            {
                return _valueMode;
            }
        }

        /// <summary>
        /// 单元格类型
        /// </summary>
        public ICellType CellType
        {
            get
            {
                return _cellType;
            }
            set
            {
                _cellType = value;
            }
        }
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ThreadCellItem()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public ThreadCellItem(int threadIndex, int sheetIndex, int row, int col)
        {
            _threadIndex = threadIndex;
            _row = row;
            _col = col;
            _sheetIndex = sheetIndex;
            _cellText = string.Empty; ;
            _cellValue = null;
            _valueMode = CustomBool.None;
            _cellType = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="threadIndex"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="cellText"></param>
        public ThreadCellItem(int threadIndex, int sheetIndex, int row, int col, string cellText)
        {
            _threadIndex = threadIndex;
            _row = row;
            _col = col;
            _sheetIndex = sheetIndex;
            _cellText = cellText;
            _cellValue = null;
            _valueMode = CustomBool.False;
            _cellType = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="threadIndex"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="cellValue"></param>
        public ThreadCellItem(int threadIndex, int sheetIndex, int row, int col, object cellValue)
        {
            _threadIndex = threadIndex;
            _row = row;
            _col = col;
            _sheetIndex = sheetIndex;
            _cellText = string.Empty;
            _cellValue = cellValue;
            _valueMode = CustomBool.True;
            _cellType = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="threadIndex"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="cellValue"></param>
        /// <param name="cellType"></param>
        public ThreadCellItem(int threadIndex, int sheetIndex, int row, int col, object cellValue, ICellType cellType)
        {
            _threadIndex = threadIndex;
            _row = row;
            _col = col;
            _sheetIndex = sheetIndex;
            _cellText = string.Empty;
            _cellValue = cellValue;
            _valueMode = CustomBool.True;
            _cellType = cellType;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="threadIndex"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="cellText"></param>
        /// <param name="cellValue"></param>
        public ThreadCellItem(int threadIndex, int sheetIndex, int row, int col, string cellText, object cellValue)
        {
            _threadIndex = threadIndex;
            _row = row;
            _col = col;
            _sheetIndex = sheetIndex;
            _cellText = cellText;
            _cellValue = cellValue;
            _valueMode = CustomBool.None;
            _cellType = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="threadIndex"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="cellText"></param>
        /// <param name="cellType"></param>
        public ThreadCellItem(int threadIndex, int sheetIndex, int row, int col, string cellText, ICellType cellType)
        {
            _threadIndex = threadIndex;
            _row = row;
            _col = col;
            _sheetIndex = sheetIndex;
            _cellText = cellText;
            _valueMode = CustomBool.False;
            _cellType = cellType;
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
