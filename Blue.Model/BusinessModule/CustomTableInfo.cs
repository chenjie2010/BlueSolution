//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomTableInfo.cs
// 描述: CustomTableInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/6/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomTableInfo 类</para>
    /// <para>自定义表</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomTableInfo
    {
        #region 内部成员变量

        private decimal _tableId;
        private decimal _categoryId;
        private string _logicalName = string.Empty;
        private string _physicalName = string.Empty;
        private string _tableCode = string.Empty;
        private byte _tableProperty;
        private byte _tableType;
        private bool _systemTable;
        private long _tableSetting;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomTableInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="tableId">表编号</param>
        ///<param name="categoryId">分类编号</param>
        ///<param name="logicalName">表逻辑名称</param>
        ///<param name="physicalName">表物理名称</param>
        ///<param name="tableCode">表的编码</param>
        ///<param name="tableProperty">表的属性</param>
        ///<param name="tableType">表类型</param>
        ///<param name="systemTable">是否系统表</param>
        ///<param name="tableSetting">表的设置</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomTableInfo(decimal tableId, decimal categoryId, string logicalName, string physicalName, string tableCode,
            byte tableProperty, byte tableType, bool systemTable, long tableSetting, int sorting,
            string notes)
        {
            _tableId = tableId;
            _categoryId = categoryId;
            _logicalName = logicalName;
            _physicalName = physicalName;
            _tableCode = tableCode;
            _tableProperty = tableProperty;
            _tableType = tableType;
            _systemTable = systemTable;
            _tableSetting = tableSetting;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

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
        /// 分类编号
        /// </summary>
        public decimal CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                if (_categoryId == value)
                    return;
                _categoryId = value;
            }
        }

        /// <summary>
        /// 表逻辑名称
        /// </summary>
        [StringLengthValidator(0, 64, MessageTemplate = "表逻辑名称长度不能超过64位！")]
        public string LogicalName
        {
            get
            {
                return _logicalName;
            }
            set
            {
                if (_logicalName == value)
                    return;
                _logicalName = value;
            }
        }

        /// <summary>
        /// 表物理名称
        /// </summary>
        public string PhysicalName
        {
            get
            {
                return _physicalName;
            }
            set
            {
                if (_physicalName == value)
                    return;
                _physicalName = value;
            }
        }

        /// <summary>
        /// 表的编码
        /// </summary>
        [StringLengthValidator(0, 32, MessageTemplate = "表的编码长度不能超过32位！")]
        public string TableCode
        {
            get
            {
                return _tableCode;
            }
            set
            {
                if (_tableCode == value)
                    return;
                _tableCode = value;
            }
        }

        /// <summary>
        /// 表的属性
        /// </summary>
        public byte TableProperty
        {
            get
            {
                return _tableProperty;
            }
            set
            {
                if (_tableProperty == value)
                    return;
                _tableProperty = value;
            }
        }

        /// <summary>
        /// 表类型
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
        /// 是否系统表
        /// </summary>
        public bool SystemTable
        {
            get
            {
                return _systemTable;
            }
            set
            {
                if (_systemTable == value)
                    return;
                _systemTable = value;
            }
        }

        /// <summary>
        /// 表的设置
        /// </summary>
        public long TableSetting
        {
            get
            {
                return _tableSetting;
            }
            set
            {
                if (_tableSetting == value)
                    return;
                _tableSetting = value;
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