//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomEnumInfo.cs
// 描述：CustomEnumInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/3/7
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomEnumInfo 类</para>
    /// <para>自定义枚举</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomEnumInfo
    {
        #region 内部成员变量

        private decimal _enumId;
        private decimal _parentEnumId;
        private string _enumName = string.Empty;
        private string _enumCode = string.Empty;
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
        private bool _superEnumEnabled;
        private bool _isLeaf;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomEnumInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="enumId">枚举编号</param>
        ///<param name="parentEnumId">枚举编号</param>
        ///<param name="enumName">枚举名称</param>
        ///<param name="enumCode">枚举编码</param>
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
        ///<param name="superEnumEnabled">超大枚举</param>
        ///<param name="isLeaf">叶子节点</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomEnumInfo(decimal enumId, decimal parentEnumId, string enumName, string enumCode, string enumValue,
            string firstCode, string secondCode, string fstAdditionalString, string scdAdditionalString, string trdAdditionalString,
            string fourthAdditionalString, string fifthAdditionalString, string sixthAdditionalString, int fstAdditionalInteger, int scdAdditionalInteger,
            decimal fstAdditionalDecimal, decimal scdAdditionalDecimal, bool superEnumEnabled, bool isLeaf, int sorting,
            string notes)
        {
            _enumId = enumId;
            _parentEnumId = parentEnumId;
            _enumName = enumName;
            _enumCode = enumCode;
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
            _superEnumEnabled = superEnumEnabled;
            _isLeaf = isLeaf;
            _sorting = sorting;
            _notes = notes;

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
        [NotNullValidator(MessageTemplate = " 枚举名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "枚举名称长度范围在1位～64位！")]
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
        /// 枚举编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 枚举编码不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "枚举编码长度范围在1位～64位！")]
        public string EnumCode
        {
            get
            {
                return _enumCode;
            }
            set
            {
                if (_enumCode == value)
                    return;
                _enumCode = value;
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
        [StringLengthValidator(0, 64, MessageTemplate = "附枚举值一长度不能超过64位！")]
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
        [StringLengthValidator(0, 64, MessageTemplate = "附枚举值二长度不能超过64位！")]
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
        [StringLengthValidator(0, 64, MessageTemplate = "附加值字符串一依赖长度不能超过64位！")]
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
        [StringLengthValidator(0, 64, MessageTemplate = "附加值字符串二依赖长度不能超过64位！")]
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
        [StringLengthValidator(0, 64, MessageTemplate = "附加值字符串三依赖长度不能超过64位！")]
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
        [StringLengthValidator(0, 64, MessageTemplate = "附加值字符串四依赖长度不能超过64位！")]
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
        [StringLengthValidator(0, 64, MessageTemplate = "附加值字符串五依赖长度不能超过64位！")]
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
        [StringLengthValidator(0, 64, MessageTemplate = "附加值字符串六依赖长度不能超过64位！")]
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

        /// <summary>
        /// 超大枚举
        /// </summary>
        public bool SuperEnumEnabled
        {
            get
            {
                return _superEnumEnabled;
            }
            set
            {
                if (_superEnumEnabled == value)
                    return;
                _superEnumEnabled = value;
            }
        }

        /// <summary>
        /// 叶子节点
        /// </summary>
        public bool IsLeaf
        {
            get
            {
                return _isLeaf;
            }
            set
            {
                if (_isLeaf == value)
                    return;
                _isLeaf = value;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sorting
        {
            get
            {
                return _sorting;
            }
            set
            {
                if (_sorting == value)
                    return;
                _sorting = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLengthValidator(0, 256, MessageTemplate = "备注长度不能超过256位！")]
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                if (_notes == value)
                    return;
                _notes = value;
            }
        }

        #endregion

    }
}