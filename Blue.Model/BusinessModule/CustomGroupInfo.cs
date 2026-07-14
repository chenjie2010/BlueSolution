//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomGroupInfo.cs
// 描述: CustomGroupInfo 实体类
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
    /// <para>CustomGroupInfo 类</para>
    /// <para>自定义分组</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomGroupInfo
    {
        #region 内部成员变量

        private decimal _groupId = decimal.MinValue;
        private decimal _userId = decimal.MinValue;
        private decimal _parentGroupId = decimal.MinValue;
        private string _groupName = string.Empty;
        private string _groupCode = string.Empty;
        private byte _groupType;
        private bool _isLeaf;
        private string _tooltip = string.Empty;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomGroupInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="groupId">分组编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="parentGroupId">分组编号</param>
        ///<param name="groupName">分组名称</param>
        ///<param name="groupCode">分组编码</param>
        ///<param name="groupType">分组类型</param>
        ///<param name="isLeaf">叶子节点</param>
        ///<param name="tooltip">提示信息</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomGroupInfo(decimal groupId, decimal userId, decimal parentGroupId, string groupName, string groupCode,
            byte groupType, bool isLeaf, string tooltip, int sorting, string notes)
        {
            _groupId = groupId;
            _userId = userId;
            _parentGroupId = parentGroupId;
            _groupName = groupName;
            _groupCode = groupCode;
            _groupType = groupType;
            _isLeaf = isLeaf;
            _tooltip = tooltip;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

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
        /// 用户编号
        /// </summary>
        public decimal UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId == value)
                    return;
                _userId = value;
            }
        }

        /// <summary>
        /// 分组编号
        /// </summary>
        public decimal ParentGroupId
        {
            get
            {
                return _parentGroupId;
            }
            set
            {
                if (_parentGroupId == value)
                    return;
                _parentGroupId = value;
            }
        }

        /// <summary>
        /// 分组名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 分组名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "分组名称长度范围在1位～64位！")]
        public string GroupName
        {
            get
            {
                return _groupName;
            }
            set
            {
                if (_groupName == value)
                    return;
                _groupName = value;
            }
        }

        /// <summary>
        /// 分组编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 分组编码不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "分组编码长度范围在1位～64位！")]
        public string GroupCode
        {
            get
            {
                return _groupCode;
            }
            set
            {
                if (_groupCode == value)
                    return;
                _groupCode = value;
            }
        }

        /// <summary>
        /// 分组类型
        /// </summary>
        public byte GroupType
        {
            get
            {
                return _groupType;
            }
            set
            {
                if (_groupType == value)
                    return;
                _groupType = value;
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
        public string Tooltip
        {
            get
            {
                return _tooltip;
            }
            set
            {
                if (_tooltip == value)
                    return;
                _tooltip = value;
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