//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomAssociationInfo.cs
// 描述：CustomAssociationInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/2/20
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomAssociationInfo 类</para>
    /// <para>自定义关联</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomAssociationInfo
    {
        #region 内部成员变量

        private decimal _associationId;
        private decimal _groupId;
        private string _associationName = string.Empty;
        private string _associationCode = string.Empty;
        private string _physicalName = string.Empty;
        private byte _showMode;
        private bool _superAssociationEnabled;
        private bool _isLeaf;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomAssociationInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="associationId">关联编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="associationName">关联名称</param>
        ///<param name="associationCode">关联编码</param>
        ///<param name="physicalName">物理名称</param>
        ///<param name="showMode">展现模式</param>
        ///<param name="superAssociationEnabled">超大关联</param>
        ///<param name="isLeaf">叶子节点</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomAssociationInfo(decimal associationId, decimal groupId, string associationName, string associationCode, string physicalName,
            byte showMode, bool superAssociationEnabled, bool isLeaf, int sorting, string notes)
        {
            _associationId = associationId;
            _groupId = groupId;
            _associationName = associationName;
            _associationCode = associationCode;
            _physicalName = physicalName;
            _showMode = showMode;
            _superAssociationEnabled = superAssociationEnabled;
            _isLeaf = isLeaf;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 关联编号
        /// </summary>
        public decimal AssociationId
        {
            get
            {
                return _associationId;
            }
            set
            {
                if (_associationId == value)
                    return;
                _associationId = value;
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
        /// 关联名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 关联名称不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "关联名称长度范围在1位～32位！")]
        public string AssociationName
        {
            get
            {
                return _associationName;
            }
            set
            {
                if (_associationName == value)
                    return;
                _associationName = value;
            }
        }

        /// <summary>
        /// 关联编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 关联编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "关联编码长度范围在1位～32位！")]
        public string AssociationCode
        {
            get
            {
                return _associationCode;
            }
            set
            {
                if (_associationCode == value)
                    return;
                _associationCode = value;
            }
        }

        /// <summary>
        /// 物理名称
        /// </summary>
        [StringLengthValidator(0, 32, MessageTemplate = "物理名称长度不能超过32位！")]
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
        /// 超大关联
        /// </summary>
        public bool SuperAssociationEnabled
        {
            get
            {
                return _superAssociationEnabled;
            }
            set
            {
                if (_superAssociationEnabled == value)
                    return;
                _superAssociationEnabled = value;
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