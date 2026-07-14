//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserQueryInfo.cs
// 描述: UserQueryInfo 实体类
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
    /// <para>UserQueryInfo 类</para>
    /// <para>用户查询</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class UserQueryInfo
    {
        #region 内部成员变量

        private decimal _userQueryId;
        private decimal _groupId;
        private decimal _userId;
        private string _userQueryName = string.Empty;
        private string _userQueryCode = string.Empty;
        private byte _queryShowType;
        private byte _recommendType;
        private long _tableNameRelation;
        private bool _isGroup;
        private bool _isDistinct;
        private bool _eneableCondition;
        private string _notes = string.Empty;
        private DateTime _createdTime;
        private int _sorting;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserQueryInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="userQueryId">查询编号</param>
        ///<param name="groupId"></param>
        ///<param name="userId">用户编号</param>
        ///<param name="userQueryName">查询名称</param>
        ///<param name="userQueryCode">查询编码</param>
        ///<param name="queryShowType">查询显示类型</param>
        ///<param name="recommendType">推荐类型</param>
        ///<param name="tableNameRelation">表集合关系</param>
        ///<param name="isGroup">分组</param>
        ///<param name="isDistinct">清除相同记录</param>
        ///<param name="eneableCondition">条件是否有效</param>
        ///<param name="notes">备注</param>
        ///<param name="createdTime">创建时间</param>
        ///<param name="sorting">排序</param>
        public UserQueryInfo(decimal userQueryId, decimal groupId, decimal userId, string name, string userQueryCode,
            byte queryShowType, byte recommendType, long tableNameRelation, bool isGroup, bool isDistinct,
            bool eneableCondition, string notes, DateTime createdTime, int sorting)
        {
            _userQueryId = userQueryId;
            _groupId = groupId;
            _userId = userId;
            _userQueryName = name;
            _userQueryCode = userQueryCode;
            _queryShowType = queryShowType;
            _recommendType = recommendType;
            _tableNameRelation = tableNameRelation;
            _isGroup = isGroup;
            _isDistinct = isDistinct;
            _eneableCondition = eneableCondition;
            _notes = notes;
            _createdTime = createdTime;
            _sorting = sorting;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 查询编号
        /// </summary>
        public decimal UserQueryId
        {
            get
            {
                return _userQueryId;
            }
            set
            {
                if (_userQueryId == value)
                    return;
                _userQueryId = value;
            }
        }

        /// <summary>
        /// 
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
        /// 查询名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 查询名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "查询名称长度范围在1位～64位！")]
        public string UserQueryName
        {
            get
            {
                return _userQueryName;
            }
            set
            {
                if (_userQueryName == value)
                    return;
                _userQueryName = value;
            }
        }

        /// <summary>
        /// 查询编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 查询编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "查询编码长度范围在1位～32位！")]
        public string UserQueryCode
        {
            get
            {
                return _userQueryCode;
            }
            set
            {
                if (_userQueryCode == value)
                    return;
                _userQueryCode = value;
            }
        }

        /// <summary>
        /// 查询显示类型
        /// </summary>
        public byte QueryShowType
        {
            get
            {
                return _queryShowType;
            }
            set
            {
                if (_queryShowType == value)
                    return;
                _queryShowType = value;
            }
        }

        /// <summary>
        /// 推荐类型
        /// </summary>
        public byte RecommendType
        {
            get
            {
                return _recommendType;
            }
            set
            {
                if (_recommendType == value)
                    return;
                _recommendType = value;
            }
        }

        /// <summary>
        /// 表集合关系
        /// </summary>
        public long TableNameRelation
        {
            get
            {
                return _tableNameRelation;
            }
            set
            {
                if (_tableNameRelation == value)
                    return;
                _tableNameRelation = value;
            }
        }

        /// <summary>
        /// 分组
        /// </summary>
        public bool IsGroup
        {
            get
            {
                return _isGroup;
            }
            set
            {
                if (_isGroup == value)
                    return;
                _isGroup = value;
            }
        }

        /// <summary>
        /// 清除相同记录
        /// </summary>
        public bool IsDistinct
        {
            get
            {
                return _isDistinct;
            }
            set
            {
                if (_isDistinct == value)
                    return;
                _isDistinct = value;
            }
        }

        /// <summary>
        /// 条件是否有效
        /// </summary>
        public bool EneableCondition
        {
            get
            {
                return _eneableCondition;
            }
            set
            {
                if (_eneableCondition == value)
                    return;
                _eneableCondition = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLengthValidator(0, 64, MessageTemplate = "备注长度不能超过64位！")]
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
        /// 创建时间
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