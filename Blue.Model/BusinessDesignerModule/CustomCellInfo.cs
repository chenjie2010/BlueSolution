//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomCellInfo.cs
// 描述: CustomCellInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/10/2
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessDesignerModule
{
    /// <summary>
    /// <para>CustomCellInfo 类</para>
    /// <para>样表单元格</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomCellInfo
    {
        #region 内部成员变量

        private decimal _cellId;
        private decimal _tableId;
        private decimal _sheetId;
        private int _rowIndex;
        private int _colIndex;
        private byte _cellType;
        private long _cellProperty;
        private string _conditionText = string.Empty;
        private string _templateText = string.Empty;
        private int _extendRows;
        private int _extendCols;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomCellInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="cellId">CellId</param>
        ///<param name="tableId">表编号</param>
        ///<param name="sheetId">样表编号</param>
        ///<param name="rowIndex">行索引</param>
        ///<param name="colIndex">列索引</param>
        ///<param name="cellType">单元格类型</param>
        ///<param name="cellProperty">单元格属性</param>
        ///<param name="conditionText">条件文本</param>
        ///<param name="templateText">模板文本</param>
        ///<param name="extendRows">扩展行数</param>
        ///<param name="extendCols">扩展列数</param>
        public CustomCellInfo(decimal cellId, decimal tableId, decimal sheetId, int rowIndex, int colIndex,
            byte cellType, long cellProperty, string conditionText, string templateText, int extendRows,
            int extendCols)
        {
            _cellId = cellId;
            _tableId = tableId;
            _sheetId = sheetId;
            _rowIndex = rowIndex;
            _colIndex = colIndex;
            _cellType = cellType;
            _cellProperty = cellProperty;
            _conditionText = conditionText;
            _templateText = templateText;
            _extendRows = extendRows;
            _extendCols = extendCols;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// CellId
        /// </summary>
        public decimal CellId
        {
            get
            {
                return _cellId;
            }
            set
            {
                if (_cellId == value)
                    return;
                _cellId = value;
            }
        }

        /// <summary>
        /// 表编号
        /// </summary>
        public decimal TableId
        {
            get
            {
                return _tableId;
            }
            set
            {
                if (_tableId == value)
                    return;
                _tableId = value;
            }
        }

        /// <summary>
        /// 样表编号
        /// </summary>
        public decimal SheetId
        {
            get
            {
                return _sheetId;
            }
            set
            {
                if (_sheetId == value)
                    return;
                _sheetId = value;
            }
        }

        /// <summary>
        /// 行索引
        /// </summary>
        public int RowIndex
        {
            get
            {
                return _rowIndex;
            }
            set
            {
                if (_rowIndex == value)
                    return;
                _rowIndex = value;
            }
        }

        /// <summary>
        /// 列索引
        /// </summary>
        public int ColIndex
        {
            get
            {
                return _colIndex;
            }
            set
            {
                if (_colIndex == value)
                    return;
                _colIndex = value;
            }
        }

        /// <summary>
        /// 单元格类型
        /// </summary>
        public byte CellType
        {
            get
            {
                return _cellType;
            }
            set
            {
                if (_cellType == value)
                    return;
                _cellType = value;
            }
        }

        /// <summary>
        /// 单元格属性
        /// </summary>
        public long CellProperty
        {
            get
            {
                return _cellProperty;
            }
            set
            {
                if (_cellProperty == value)
                    return;
                _cellProperty = value;
            }
        }

        /// <summary>
        /// 条件文本
        /// </summary>
        [StringLengthValidator(0, 256, MessageTemplate = "条件文本长度不能超过256位！")]
        public string ConditionText
        {
            get
            {
                return _conditionText;
            }
            set
            {
                if (_conditionText == value)
                    return;
                _conditionText = value;
            }
        }

        /// <summary>
        /// 模板文本
        /// </summary>
        [StringLengthValidator(0, 128, MessageTemplate = "模板文本长度不能超过128位！")]
        public string TemplateText
        {
            get
            {
                return _templateText;
            }
            set
            {
                if (_templateText == value)
                    return;
                _templateText = value;
            }
        }

        /// <summary>
        /// 扩展行数
        /// </summary>
        public int ExtendRows
        {
            get
            {
                return _extendRows;
            }
            set
            {
                if (_extendRows == value)
                    return;
                _extendRows = value;
            }
        }

        /// <summary>
        /// 扩展列数
        /// </summary>
        public int ExtendCols
        {
            get
            {
                return _extendCols;
            }
            set
            {
                if (_extendCols == value)
                    return;
                _extendCols = value;
            }
        }

        #endregion

    }
}