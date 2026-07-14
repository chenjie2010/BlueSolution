//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomPrintInfo.cs
// 描述: CustomPrintInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/8/11
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomPrintInfo 类</para>
    /// <para>数据打印</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomPrintInfo
    {
        #region 内部成员变量

        private decimal _printId;
        private decimal _combinedTableId;
        private decimal _groupId;
        private decimal _tableId;
        private string _printName = string.Empty;
        private string _printCode = string.Empty;
        private byte _tableType;
        private long _systemDataField;
        private string _printContent = string.Empty;
        private bool _printVisible;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomPrintInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="printId">数据打印编号</param>
        ///<param name="combinedTableId">组合字段编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="tableId">表编号</param>
        ///<param name="printName">数据打印名称</param>
        ///<param name="printCode">数据打印编码</param>
        ///<param name="tableType">表格类型</param>
        ///<param name="systemDataField">系统字段</param>
        ///<param name="printContent">数据打印内容</param>
        ///<param name="printVisible">可见性</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomPrintInfo(decimal printId, decimal combinedTableId, decimal groupId, decimal tableId, string printName,
            string printCode, byte tableType, long systemDataField, string printContent, bool printVisible,
            int sorting, string notes)
        {
            _printId = printId;
            _combinedTableId = combinedTableId;
            _groupId = groupId;
            _tableId = tableId;
            _printName = printName;
            _printCode = printCode;
            _tableType = tableType;
            _systemDataField = systemDataField;
            _printContent = printContent;
            _printVisible = printVisible;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 数据打印编号
        /// </summary>
        public decimal PrintId
        {
            get
            {
                return _printId;
            }
            set
            {
                if (_printId == value)
                    return;
                _printId = value;
            }
        }

        /// <summary>
        /// 组合字段编号
        /// </summary>
        public decimal CombinedTableId
        {
            get
            {
                return _combinedTableId;
            }
            set
            {
                if (_combinedTableId == value)
                    return;
                _combinedTableId = value;
            }
        }

        /// <summary>
        /// 分组编号
        /// </summary>
        public decimal GroupId
        {
            get
            {
                return _groupId;
            }
            set
            {
                if (_groupId == value)
                    return;
                _groupId = value;
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
        /// 数据打印名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 数据打印名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "数据打印名称长度范围在1位～64位！")]
        public string PrintName
        {
            get
            {
                return _printName;
            }
            set
            {
                if (_printName == value)
                    return;
                _printName = value;
            }
        }

        /// <summary>
        /// 数据打印编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 数据打印编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "数据打印编码长度范围在1位～32位！")]
        public string PrintCode
        {
            get
            {
                return _printCode;
            }
            set
            {
                if (_printCode == value)
                    return;
                _printCode = value;
            }
        }

        /// <summary>
        /// 表格类型
        /// </summary>
        public byte TableType
        {
            get
            {
                return _tableType;
            }
            set
            {
                if (_tableType == value)
                    return;
                _tableType = value;
            }
        }

        /// <summary>
        /// 系统字段
        /// </summary>
        public long SystemDataField
        {
            get
            {
                return _systemDataField;
            }
            set
            {
                if (_systemDataField == value)
                    return;
                _systemDataField = value;
            }
        }

        /// <summary>
        /// 数据打印内容
        /// </summary>
        [NotNullValidator(MessageTemplate = " 数据打印内容不能为空")]
        [StringLengthValidator(1, 2147483647, MessageTemplate = "数据打印内容长度超过范围限制(2147483647字符)！")]
        public string PrintContent
        {
            get
            {
                return _printContent;
            }
            set
            {
                if (_printContent == value)
                    return;
                _printContent = value;
            }
        }

        /// <summary>
        /// 可见性
        /// </summary>
        public bool PrintVisible
        {
            get
            {
                return _printVisible;
            }
            set
            {
                if (_printVisible == value)
                    return;
                _printVisible = value;
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