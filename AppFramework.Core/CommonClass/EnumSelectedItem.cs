//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： EnumSelectedItem.cs
// 描述： 枚举选项
// 作者：ChenJie 
// 编写日期：2018/03/02
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core.CommonClass
{
    /// <summary>
    /// 枚举选项
    /// </summary>
    public class EnumSelectedItem
    {
        #region 内部成员变量

        private decimal _enumId;
        private decimal _parentEnumId;
        private string _enumName = string.Empty;
        private string _enumValue = string.Empty;
        private string _firstCode = string.Empty;
        private string _secondCode = string.Empty;
        private string _fstAdditionalString = string.Empty;
        private string _scdAdditionalString = string.Empty;
        private string _trdAdditionalString = string.Empty;
        private string _fourthAdditionalString = string.Empty;
        private string _fifthAdditionalString = string.Empty;
        private string _sixthAdditionalString = string.Empty;
        private int _fstAdditionalInteger;
        private int _scdAdditionalInteger;
        private decimal _fstAdditionalDecimal;
        private decimal _scdAdditionalDecimal;       

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public EnumSelectedItem()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="enumId">枚举编号</param>
        ///<param name="parentEnumId">枚举编号</param>
        ///<param name="enumName">枚举名称</param>
        ///<param name="enumValue">枚举值</param>
        ///<param name="firstCode">附枚举值一</param>
        ///<param name="secondCode">附枚举值二</param>
        ///<param name="fstAdditionalString">附加值字符串一依赖</param>
        ///<param name="scdAdditionalString">附加值字符串二依赖</param>
        ///<param name="trdAdditionalString">附加值字符串三依赖</param>
        ///<param name="fourthAdditionalString">附加值字符串四依赖</param>
        ///<param name="fifthAdditionalString">附加值字符串五依赖</param>
        ///<param name="sixthAdditionalString">附加值字符串六依赖</param>
        ///<param name="fstAdditionalInteger">附加值整形一依赖</param>
        ///<param name="scdAdditionalInteger">附加值整形二依赖</param>
        ///<param name="fstAdditionalDecimal">附加值实数一依赖</param>
        ///<param name="scdAdditionalDecimal">附加值实数二依赖</param>
        public EnumSelectedItem(decimal enumId, decimal parentEnumId, string enumName, string enumValue, string firstCode, string secondCode, string fstAdditionalString, 
            string scdAdditionalString, string trdAdditionalString, string fourthAdditionalString, string fifthAdditionalString, string sixthAdditionalString, 
            int fstAdditionalInteger, int scdAdditionalInteger, decimal fstAdditionalDecimal, decimal scdAdditionalDecimal)
        {
            _enumId = enumId;
            _parentEnumId = parentEnumId;
            _enumName = enumName;
            _enumValue = enumValue;
            _firstCode = firstCode;
            _secondCode = secondCode;
            _fstAdditionalString = fstAdditionalString;
            _scdAdditionalString = scdAdditionalString;
            _trdAdditionalString = trdAdditionalString;
            _fourthAdditionalString = fourthAdditionalString;
            _fifthAdditionalString = fifthAdditionalString;
            _sixthAdditionalString = sixthAdditionalString;
            _fstAdditionalInteger = fstAdditionalInteger;
            _scdAdditionalInteger = scdAdditionalInteger;
            _fstAdditionalDecimal = fstAdditionalDecimal;
            _scdAdditionalDecimal = scdAdditionalDecimal;
        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 枚举编号
        /// </summary>
        public decimal EnumId
        {
            get
            {
                return _enumId;
            }
            set
            {
                if (_enumId == value)
                    return;
                _enumId = value;
            }
        }

        /// <summary>
        /// 枚举编号
        /// </summary>
        public decimal ParentEnumId
        {
            get
            {
                return _parentEnumId;
            }
            set
            {
                if (_parentEnumId == value)
                    return;
                _parentEnumId = value;
            }
        }

        /// <summary>
        /// 枚举名称
        /// </summary>       
        public string EnumName
        {
            get
            {
                return _enumName;
            }
            set
            {
                if (_enumName == value)
                    return;
                _enumName = value;
            }
        }

        /// <summary>
        /// 枚举值
        /// </summary>        
        public string EnumValue
        {
            get
            {
                return _enumValue;
            }
            set
            {
                if (_enumValue == value)
                    return;
                _enumValue = value;
            }
        }

        /// <summary>
        /// 附枚举值一
        /// </summary>       
        public string FirstCode
        {
            get
            {
                return _firstCode;
            }
            set
            {
                if (_firstCode == value)
                    return;
                _firstCode = value;
            }
        }

        /// <summary>
        /// 附枚举值二
        /// </summary>        
        public string SecondCode
        {
            get
            {
                return _secondCode;
            }
            set
            {
                if (_secondCode == value)
                    return;
                _secondCode = value;
            }
        }

        /// <summary>
        /// 附加值字符串一依赖
        /// </summary>        
        public string FstAdditionalString
        {
            get
            {
                return _fstAdditionalString;
            }
            set
            {
                if (_fstAdditionalString == value)
                    return;
                _fstAdditionalString = value;
            }
        }

        /// <summary>
        /// 附加值字符串二依赖
        /// </summary>        
        public string ScdAdditionalString
        {
            get
            {
                return _scdAdditionalString;
            }
            set
            {
                if (_scdAdditionalString == value)
                    return;
                _scdAdditionalString = value;
            }
        }

        /// <summary>
        /// 附加值字符串三依赖
        /// </summary>        
        public string TrdAdditionalString
        {
            get
            {
                return _trdAdditionalString;
            }
            set
            {
                if (_trdAdditionalString == value)
                    return;
                _trdAdditionalString = value;
            }
        }

        /// <summary>
        /// 附加值字符串四依赖
        /// </summary>        
        public string FourthAdditionalString
        {
            get
            {
                return _fourthAdditionalString;
            }
            set
            {
                if (_fourthAdditionalString == value)
                    return;
                _fourthAdditionalString = value;
            }
        }

        /// <summary>
        /// 附加值字符串五依赖
        /// </summary>        
        public string FifthAdditionalString
        {
            get
            {
                return _fifthAdditionalString;
            }
            set
            {
                if (_fifthAdditionalString == value)
                    return;
                _fifthAdditionalString = value;
            }
        }

        /// <summary>
        /// 附加值字符串六依赖
        /// </summary>      
        public string SixthAdditionalString
        {
            get
            {
                return _sixthAdditionalString;
            }
            set
            {
                if (_sixthAdditionalString == value)
                    return;
                _sixthAdditionalString = value;
            }
        }

        /// <summary>
        /// 附加值整形一依赖
        /// </summary>
        public int FstAdditionalInteger
        {
            get
            {
                return _fstAdditionalInteger;
            }
            set
            {
                if (_fstAdditionalInteger == value)
                    return;
                _fstAdditionalInteger = value;
            }
        }

        /// <summary>
        /// 附加值整形二依赖
        /// </summary>
        public int ScdAdditionalInteger
        {
            get
            {
                return _scdAdditionalInteger;
            }
            set
            {
                if (_scdAdditionalInteger == value)
                    return;
                _scdAdditionalInteger = value;
            }
        }

        /// <summary>
        /// 附加值实数一依赖
        /// </summary>
        public decimal FstAdditionalDecimal
        {
            get
            {
                return _fstAdditionalDecimal;
            }
            set
            {
                if (_fstAdditionalDecimal == value)
                    return;
                _fstAdditionalDecimal = value;
            }
        }

        /// <summary>
        /// 附加值实数二依赖
        /// </summary>
        public decimal ScdAdditionalDecimal
        {
            get
            {
                return _scdAdditionalDecimal;
            }
            set
            {
                if (_scdAdditionalDecimal == value)
                    return;
                _scdAdditionalDecimal = value;
            }
        }

        #endregion
    }
}
