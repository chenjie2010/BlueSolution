//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomReportInfo.cs
// 描述: CustomReportInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/10/4
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessDesignerModule
{
    /// <summary>
    /// <para>CustomReportInfo 类</para>
    /// <para>报表</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomReportInfo
    {
        #region 内部成员变量

        private decimal _reportId;
        private decimal _groupId;
        private string _reportName = string.Empty;
        private string _reportCode = string.Empty;
        private byte _reportCategory;
        private byte _reportType;
        private byte _dataWarehouseId;
        private long _systemDataFields;
        private string _toolTip = string.Empty;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomReportInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="reportId">报表编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="reportName">报表名称</param>
        ///<param name="reportCode">报表编码</param>
        ///<param name="reportCategory">报表分类</param>
        ///<param name="reportType">报表类型</param>
        ///<param name="dataWarehouseId">数据仓库编号</param>
        ///<param name="systemDataFields">系统字段条件</param>
        ///<param name="toolTip">提示信息</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomReportInfo(decimal reportId, decimal groupId, string reportName, string reportCode, byte reportCategory,
            byte reportType, byte dataWarehouseId, long systemDataFields, string toolTip, int sorting,
            string notes)
        {
            _reportId = reportId;
            _groupId = groupId;
            _reportName = reportName;
            _reportCode = reportCode;
            _reportCategory = reportCategory;
            _reportType = reportType;
            _dataWarehouseId = dataWarehouseId;
            _systemDataFields = systemDataFields;
            _toolTip = toolTip;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 报表编号
        /// </summary>
        public decimal ReportId
        {
            get
            {
                return _reportId;
            }
            set
            {
                if (_reportId == value)
                    return;
                _reportId = value;
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
        /// 报表名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 报表名称不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "报表名称长度范围在1位～256位！")]
        public string ReportName
        {
            get
            {
                return _reportName;
            }
            set
            {
                if (_reportName == value)
                    return;
                _reportName = value;
            }
        }

        /// <summary>
        /// 报表编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 报表编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "报表编码长度范围在1位～32位！")]
        public string ReportCode
        {
            get
            {
                return _reportCode;
            }
            set
            {
                if (_reportCode == value)
                    return;
                _reportCode = value;
            }
        }

        /// <summary>
        /// 报表分类
        /// </summary>
        public byte ReportCategory
        {
            get
            {
                return _reportCategory;
            }
            set
            {
                if (_reportCategory == value)
                    return;
                _reportCategory = value;
            }
        }

        /// <summary>
        /// 报表类型
        /// </summary>
        public byte ReportType
        {
            get
            {
                return _reportType;
            }
            set
            {
                if (_reportType == value)
                    return;
                _reportType = value;
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
        /// 系统字段条件
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