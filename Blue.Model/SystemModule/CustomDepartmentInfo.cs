//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomDepartmentInfo.cs
// 描述: CustomDepartmentInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/4/19
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.SystemModule
{
    /// <summary>
    /// <para>CustomDepartmentInfo 类</para>
    /// <para>单位信息</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomDepartmentInfo
    {
        #region 内部成员变量

        private decimal _depId;
        private decimal _parentDepId;
        private string _depName = string.Empty;
        private string _depCode = string.Empty;
        private string _depValue = string.Empty;
        private string _firstCode = string.Empty;
        private string _secondCode = string.Empty;
        private byte _departmentProperty;
        private bool _isLeaf;
        private bool _isSystemDepartment;
        private bool _isVisibleForInterface;
        private int _sorting;
        private string _notes = string.Empty;
        private DateTime _createdTime;
        private DateTime _updatedTime;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomDepartmentInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="depId">单位编号</param>
        ///<param name="parentDepId">单位编号</param>
        ///<param name="depName">单位名称</param>
        ///<param name="depCode">单位编码</param>
        ///<param name="depValue">单位值</param>
        ///<param name="firstCode">附加编码一</param>
        ///<param name="secondCode">附加编码二</param>
        ///<param name="departmentProperty">单位性质</param>
        ///<param name="isLeaf">叶子节点</param>
        ///<param name="isSystemDepartment">系统单位</param>
        ///<param name="isVisibleForInterface">是否接口可见</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        ///<param name="createdTime">增加时间</param>
        ///<param name="updatedTime">修改时间</param>
        public CustomDepartmentInfo(decimal depId, decimal parentDepId, string depName, string depCode, string depValue,
            string firstCode, string secondCode, byte departmentProperty, bool isLeaf, bool isSystemDepartment, bool isVisibleForInterface,
            int sorting, string notes, DateTime createdTime, DateTime updatedTime)
        {
            _depId = depId;
            _parentDepId = parentDepId;
            _depName = depName;
            _depCode = depCode;
            _depValue = depValue;
            _firstCode = firstCode;
            _secondCode = secondCode;
            _departmentProperty = departmentProperty;
            _isLeaf = isLeaf;
            _isSystemDepartment = isSystemDepartment;
            _isVisibleForInterface = isVisibleForInterface;
            _sorting = sorting;
            _notes = notes;
            _createdTime = createdTime;
            _updatedTime = updatedTime;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 单位编号
        /// </summary>
        public decimal DepId
        {
            get
            {
                return _depId;
            }
            set
            {
                if (_depId == value)
                    return;
                _depId = value;
            }
        }

        /// <summary>
        /// 单位编号
        /// </summary>
        public decimal ParentDepId
        {
            get
            {
                return _parentDepId;
            }
            set
            {
                if (_parentDepId == value)
                    return;
                _parentDepId = value;
            }
        }

        /// <summary>
        /// 单位名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 单位名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "单位名称长度范围在1位～64位！")]
        public string DepName
        {
            get
            {
                return _depName;
            }
            set
            {
                if (_depName == value)
                    return;
                _depName = value;
            }
        }

        /// <summary>
        /// 单位编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 单位编码不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "单位编码长度范围在1位～64位！")]
        public string DepCode
        {
            get
            {
                return _depCode;
            }
            set
            {
                if (_depCode == value)
                    return;
                _depCode = value;
            }
        }

        /// <summary>
        /// 单位值
        /// </summary>
        [StringLengthValidator(0, 64, MessageTemplate = "单位值长度不能超过64位！")]
        public string DepValue
        {
            get
            {
                return _depValue;
            }
            set
            {
                if (_depValue == value)
                    return;
                _depValue = value;
            }
        }

        /// <summary>
        /// 附加编码一
        /// </summary>
        [StringLengthValidator(0, 64, MessageTemplate = "附加编码一长度不能超过64位！")]
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
        /// 附加编码二
        /// </summary>
        [StringLengthValidator(0, 64, MessageTemplate = "附加编码二长度不能超过64位！")]
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
        /// 单位性质
        /// </summary>
        public byte DepartmentProperty
        {
            get
            {
                return _departmentProperty;
            }
            set
            {
                if (_departmentProperty == value)
                    return;
                _departmentProperty = value;
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
        /// 系统单位
        /// </summary>
        public bool IsSystemDepartment
        {
            get
            {
                return _isSystemDepartment;
            }
            set
            {
                if (_isSystemDepartment == value)
                    return;
                _isSystemDepartment = value;
            }
        }

        /// <summary>
        /// 是否接口可见
        /// </summary>
        public bool IsVisibleForInterface
        {
            get
            {
                return _isVisibleForInterface;
            }
            set
            {
                if (_isVisibleForInterface == value)
                    return;
                _isVisibleForInterface = value;
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

        /// <summary>
        /// 增加时间
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                return _createdTime;
            }
            set
            {
                if (_createdTime == value)
                    return;
                _createdTime = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdatedTime
        {
            get
            {
                return _updatedTime;
            }
            set
            {
                if (_updatedTime == value)
                    return;
                _updatedTime = value;
            }
        }

        #endregion

    }
}