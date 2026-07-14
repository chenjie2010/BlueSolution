//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomQueyInfo.cs
// 描述: CustomQueyInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/8/18
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomQueyInfo 类</para>
    /// <para>数据查询</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomQueyInfo
    {
        #region 内部成员变量

        private decimal _dataQueriedId;
        private decimal _groupId;
        private decimal _tableId;
        private decimal _viewId;
        private string _dataQueriedName = string.Empty;
        private string _dataQueriedCode = string.Empty;
        private byte _dataWarehouseId;
        private string _customViewName = string.Empty;
        private string _conditions = string.Empty;
        private byte _dataQueriedType;
        private long _systemDataFields;
        private long _systemCondition;
        private long _groupCondition;
        private byte _showMode;
        private long _dataRange;
        private string _toolTip = string.Empty;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomQueyInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="dataQueriedId">数据查询编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="tableId">表编号</param>
        ///<param name="viewId">视图编号</param>
        ///<param name="dataQueriedName">数据查询名称</param>
        ///<param name="dataQueriedCode">数据查询编码</param>
        ///<param name="dataWarehouseId">数据仓库编号</param>
        ///<param name="customViewName">自定义视图名称</param>
        ///<param name="conditions">预置条件</param>
        ///<param name="dataQueriedType">查询类型：视图，表</param>
        ///<param name="systemDataFields">系统字段</param>
        ///<param name="systemCondition">系统条件</param>
        ///<param name="groupCondition">系统分组</param>
        ///<param name="showMode">展现模式</param>
        ///<param name="dataRange">查询范围</param>
        ///<param name="toolTip">提示信息</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomQueyInfo(decimal dataQueriedId, decimal groupId, decimal tableId, decimal viewId, string dataQueriedName,
            string dataQueriedCode, byte dataWarehouseId, string customViewName, string conditions, byte dataQueriedType,
            long systemDataFields, long systemCondition, long groupCondition, byte showMode, long dataRange,
            string toolTip, int sorting, string notes)
        {
            _dataQueriedId = dataQueriedId;
            _groupId = groupId;
            _tableId = tableId;
            _viewId = viewId;
            _dataQueriedName = dataQueriedName;
            _dataQueriedCode = dataQueriedCode;
            _dataWarehouseId = dataWarehouseId;
            _customViewName = customViewName;
            _conditions = conditions;
            _dataQueriedType = dataQueriedType;
            _systemDataFields = systemDataFields;
            _systemCondition = systemCondition;
            _groupCondition = groupCondition;
            _showMode = showMode;
            _dataRange = dataRange;
            _toolTip = toolTip;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 数据查询编号
        /// </summary>
        public decimal DataQueriedId
        {
            get
            {
                return _dataQueriedId;
            }
            set
            {
                if (_dataQueriedId == value)
                    return;
                _dataQueriedId = value;
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
        /// 视图编号
        /// </summary>
        public decimal ViewId
        {
            get
            {
                return _viewId;
            }
            set
            {
                if (_viewId == value)
                    return;
                _viewId = value;
            }
        }

        /// <summary>
        /// 数据查询名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 数据查询名称不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "数据查询名称长度范围在1位～256位！")]
        public string DataQueriedName
        {
            get
            {
                return _dataQueriedName;
            }
            set
            {
                if (_dataQueriedName == value)
                    return;
                _dataQueriedName = value;
            }
        }

        /// <summary>
        /// 数据查询编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 数据查询编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "数据查询编码长度范围在1位～32位！")]
        public string DataQueriedCode
        {
            get
            {
                return _dataQueriedCode;
            }
            set
            {
                if (_dataQueriedCode == value)
                    return;
                _dataQueriedCode = value;
            }
        }

        /// <summary>
        /// 数据仓库编号
        /// </summary>
        public byte DataWarehouseId
        {
            get
            {
                return _dataWarehouseId;
            }
            set
            {
                if (_dataWarehouseId == value)
                    return;
                _dataWarehouseId = value;
            }
        }

        /// <summary>
        /// 自定义视图名称
        /// </summary>
        [StringLengthValidator(0, 512, MessageTemplate = "自定义视图名称长度不能超过512位！")]
        public string CustomViewName
        {
            get
            {
                return _customViewName;
            }
            set
            {
                if (_customViewName == value)
                    return;
                _customViewName = value;
            }
        }

        /// <summary>
        /// 预置条件
        /// </summary>
        [StringLengthValidator(0, 4000, MessageTemplate = "预置条件长度不能超过4000位！")]
        public string Conditions
        {
            get
            {
                return _conditions;
            }
            set
            {
                if (_conditions == value)
                    return;
                _conditions = value;
            }
        }

        /// <summary>
        /// 查询类型：视图，表
        /// </summary>
        public byte DataQueriedType
        {
            get
            {
                return _dataQueriedType;
            }
            set
            {
                if (_dataQueriedType == value)
                    return;
                _dataQueriedType = value;
            }
        }

        /// <summary>
        /// 系统字段
        /// </summary>
        public long SystemDataFields
        {
            get
            {
                return _systemDataFields;
            }
            set
            {
                if (_systemDataFields == value)
                    return;
                _systemDataFields = value;
            }
        }

        /// <summary>
        /// 系统条件
        /// </summary>
        public long SystemCondition
        {
            get
            {
                return _systemCondition;
            }
            set
            {
                if (_systemCondition == value)
                    return;
                _systemCondition = value;
            }
        }

        /// <summary>
        /// 系统分组
        /// </summary>
        public long GroupCondition
        {
            get
            {
                return _groupCondition;
            }
            set
            {
                if (_groupCondition == value)
                    return;
                _groupCondition = value;
            }
        }

        /// <summary>
        /// 展现模式
        /// </summary>
        public byte ShowMode
        {
            get
            {
                return _showMode;
            }
            set
            {
                if (_showMode == value)
                    return;
                _showMode = value;
            }
        }

        /// <summary>
        /// 查询范围
        /// </summary>
        public long DataRange
        {
            get
            {
                return _dataRange;
            }
            set
            {
                if (_dataRange == value)
                    return;
                _dataRange = value;
            }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        [StringLengthValidator(0, 256, MessageTemplate = "提示信息长度不能超过256位！")]
        public string ToolTip
        {
            get
            {
                return _toolTip;
            }
            set
            {
                if (_toolTip == value)
                    return;
                _toolTip = value;
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