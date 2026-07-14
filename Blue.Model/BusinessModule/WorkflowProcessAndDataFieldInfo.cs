//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: WorkflowProcessAndDataFieldInfo.cs
// 描述: WorkflowProcessAndDataFieldInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/8/26
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>WorkflowProcessAndDataFieldInfo 类</para>
    /// <para>工作流流程与字段</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class WorkflowProcessAndDataFieldInfo
    {
        #region 内部成员变量

        private decimal _processId;
        private decimal _dataFieldId;
        private byte _fstCondition;
        private byte _scdCondition;
        private bool _boolValue;
        private string _stringValue = string.Empty;
        private int _fstIntegerValue;
        private int _scdIntegerValue;
        private decimal _fstDecimalValue;
        private decimal _scdDecimalValue;
        private DateTime _fstTimeValue;
        private DateTime _scdTimeValue;
        private byte _nextRelation;
        private int _sorting;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public WorkflowProcessAndDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="fstCondition">判断条件一</param>
        ///<param name="scdCondition">判断条件二</param>
        ///<param name="boolValue">布尔类型值</param>
        ///<param name="stringValue">字符串条件值</param>
        ///<param name="fstIntegerValue">整型条件值一</param>
        ///<param name="scdIntegerValue">整型条件值二</param>
        ///<param name="fstDecimalValue">实数条件值一</param>
        ///<param name="scdDecimalValue">实数条件值二</param>
        ///<param name="fstTimeValue">时间条件值一</param>
        ///<param name="scdTimeValue">时间条件值二</param>
        ///<param name="nextRelation">与下一条件关系</param>
        ///<param name="sorting">排序</param>
        public WorkflowProcessAndDataFieldInfo(decimal processId, decimal dataFieldId, byte fstCondition, byte scdCondition, bool boolValue,
            string stringValue, int fstIntegerValue, int scdIntegerValue, decimal fstDecimalValue, decimal scdDecimalValue,
            DateTime fstTimeValue, DateTime scdTimeValue, byte nextRelation, int sorting)
        {
            _processId = processId;
            _dataFieldId = dataFieldId;
            _fstCondition = fstCondition;
            _scdCondition = scdCondition;
            _boolValue = boolValue;
            _stringValue = stringValue;
            _fstIntegerValue = fstIntegerValue;
            _scdIntegerValue = scdIntegerValue;
            _fstDecimalValue = fstDecimalValue;
            _scdDecimalValue = scdDecimalValue;
            _fstTimeValue = fstTimeValue;
            _scdTimeValue = scdTimeValue;
            _nextRelation = nextRelation;
            _sorting = sorting;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 流程编号
        /// </summary>
        public decimal ProcessId
        {
            get
            {
                return _processId;
            }
            set
            {
                if (_processId == value)
                    return;
                _processId = value;
            }
        }

        /// <summary>
        /// 字段编号
        /// </summary>
        public decimal DataFieldId
        {
            get
            {
                return _dataFieldId;
            }
            set
            {
                if (_dataFieldId == value)
                    return;
                _dataFieldId = value;
            }
        }

        /// <summary>
        /// 判断条件一
        /// </summary>
        public byte FstCondition
        {
            get
            {
                return _fstCondition;
            }
            set
            {
                if (_fstCondition == value)
                    return;
                _fstCondition = value;
            }
        }

        /// <summary>
        /// 判断条件二
        /// </summary>
        public byte ScdCondition
        {
            get
            {
                return _scdCondition;
            }
            set
            {
                if (_scdCondition == value)
                    return;
                _scdCondition = value;
            }
        }

        /// <summary>
        /// 布尔类型值
        /// </summary>
        public bool BoolValue
        {
            get
            {
                return _boolValue;
            }
            set
            {
                if (_boolValue == value)
                    return;
                _boolValue = value;
            }
        }

        /// <summary>
        /// 字符串条件值
        /// </summary>
        [StringLengthValidator(0, 512, MessageTemplate = "字符串条件值长度不能超过512位！")]
        public string StringValue
        {
            get
            {
                return _stringValue;
            }
            set
            {
                if (_stringValue == value)
                    return;
                _stringValue = value;
            }
        }

        /// <summary>
        /// 整型条件值一
        /// </summary>
        public int FstIntegerValue
        {
            get
            {
                return _fstIntegerValue;
            }
            set
            {
                if (_fstIntegerValue == value)
                    return;
                _fstIntegerValue = value;
            }
        }

        /// <summary>
        /// 整型条件值二
        /// </summary>
        public int ScdIntegerValue
        {
            get
            {
                return _scdIntegerValue;
            }
            set
            {
                if (_scdIntegerValue == value)
                    return;
                _scdIntegerValue = value;
            }
        }

        /// <summary>
        /// 实数条件值一
        /// </summary>
        public decimal FstDecimalValue
        {
            get
            {
                return _fstDecimalValue;
            }
            set
            {
                if (_fstDecimalValue == value)
                    return;
                _fstDecimalValue = value;
            }
        }

        /// <summary>
        /// 实数条件值二
        /// </summary>
        public decimal ScdDecimalValue
        {
            get
            {
                return _scdDecimalValue;
            }
            set
            {
                if (_scdDecimalValue == value)
                    return;
                _scdDecimalValue = value;
            }
        }

        /// <summary>
        /// 时间条件值一
        /// </summary>
        public DateTime FstTimeValue
        {
            get
            {
                return _fstTimeValue;
            }
            set
            {
                if (_fstTimeValue == value)
                    return;
                _fstTimeValue = value;
            }
        }

        /// <summary>
        /// 时间条件值二
        /// </summary>
        public DateTime ScdTimeValue
        {
            get
            {
                return _scdTimeValue;
            }
            set
            {
                if (_scdTimeValue == value)
                    return;
                _scdTimeValue = value;
            }
        }

        /// <summary>
        /// 与下一条件关系
        /// </summary>
        public byte NextRelation
        {
            get
            {
                return _nextRelation;
            }
            set
            {
                if (_nextRelation == value)
                    return;
                _nextRelation = value;
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

        #endregion

    }
}