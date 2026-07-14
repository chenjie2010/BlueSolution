//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CurrentUserTypeInfo.cs
// 描述: CurrentUserTypeInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/4/19
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;

namespace Blue.WebAPI
{
    /// <summary>
    /// 用户类型
    /// </summary>
    [Serializable]
    public class CurrentUserTypeInfo
    {
        #region 内部成员变量

        private decimal _userTypeId;
        private string _userTypeName = string.Empty;
        private string _userTypeCode = string.Empty;
        private DateTime _createdTime;
        private DateTime _updatedTime;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CurrentUserTypeInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="userTypeId">用户类型编号</param>
        ///<param name="userTypeName">用户类型名称</param>
        ///<param name="userTypeCode">用户类型编码</param>
        ///<param name="createdTime">增加时间</param>
        ///<param name="updatedTime">修改时间</param>
        public CurrentUserTypeInfo(decimal userTypeId, string name, string userType, DateTime createdTime,
            DateTime updatedTime)
        {
            _userTypeId = userTypeId;
            _userTypeName = name;
            _userTypeCode = userType;
            _createdTime = createdTime;
            _updatedTime = updatedTime;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 用户类型编号
        /// </summary>
        public decimal UserTypeId
        {
            get
            {
                return _userTypeId;
            }
            set
            {
                if (_userTypeId == value)
                    return;
                _userTypeId = value;
            }
        }

        /// <summary>
        /// 用户类型名称
        /// </summary>
       public string UserTypeName
        {
            get
            {
                return _userTypeName;
            }
            set
            {
                if (_userTypeName == value)
                    return;
                _userTypeName = value;
            }
        }

        /// <summary>
        /// 用户类型编码
        /// </summary>
        public string UserTypeCode
        {
            get
            {
                return _userTypeCode;
            }
            set
            {
                if (_userTypeCode == value)
                    return;
                _userTypeCode = value;
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