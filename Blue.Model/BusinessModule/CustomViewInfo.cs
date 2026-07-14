//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomViewInfo.cs
// 描述: CustomViewInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/8/16
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomViewInfo 类</para>
    /// <para>数据视图</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomViewInfo
    {
        #region 内部成员变量

        private decimal _viewId;
        private decimal _tableId;
        private decimal _groupId;
        private string _viewName = string.Empty;
        private string _viewCode = string.Empty;
        private string _physicalName = string.Empty;
        private byte _viewProperty;
        private long _systemDataFields;
        private bool _isLeaf;
        private string _toolTip = string.Empty;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomViewInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="viewId">视图编号</param>
        ///<param name="tableId">表编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="viewName">视图名称</param>
        ///<param name="viewCode">视图编码</param>
        ///<param name="physicalName">物理名称</param>
        ///<param name="viewProperty">视图属性</param>
        ///<param name="systemDataFields">系统字段</param>
        ///<param name="isLeaf">叶子节点</param>
        ///<param name="toolTip">提示信息</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomViewInfo(decimal viewId, decimal tableId, decimal groupId, string viewName, string viewCode,
            string physicalName, byte viewProperty, long systemDataFields, bool isLeaf, string toolTip,
            int sorting, string notes)
        {
            _viewId = viewId;
            _tableId = tableId;
            _groupId = groupId;
            _viewName = viewName;
            _viewCode = viewCode;
            _physicalName = physicalName;
            _viewProperty = viewProperty;
            _systemDataFields = systemDataFields;
            _isLeaf = isLeaf;
            _toolTip = toolTip;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

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
        /// 视图名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 视图名称不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "视图名称长度范围在1位～256位！")]
        public string ViewName
        {
            get
            {
                return _viewName;
            }
            set
            {
                if (_viewName == value)
                    return;
                _viewName = value;
            }
        }

        /// <summary>
        /// 视图编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 视图编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "视图编码长度范围在1位～32位！")]
        public string ViewCode
        {
            get
            {
                return _viewCode;
            }
            set
            {
                if (_viewCode == value)
                    return;
                _viewCode = value;
            }
        }

        /// <summary>
        /// 物理名称
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
        /// 视图属性
        /// </summary>
        public byte ViewProperty
        {
            get
            {
                return _viewProperty;
            }
            set
            {
                if (_viewProperty == value)
                    return;
                _viewProperty = value;
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