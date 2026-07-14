//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DepartmentInfo.cs
// 描述: DepartmentInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/4/19
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;

namespace Blue.WebAPI
{
    /// <summary>
    /// 单位信息
    /// </summary>
    [Serializable]
    public class DepartmentInfo
    {
        #region 内部成员变量

        private decimal _depId;
        private string _depName = string.Empty;
        private string _depCode = string.Empty;
        private string _firstCode = string.Empty;
        private string _secondCode = string.Empty;
        private DateTime _createdTime;
        private DateTime _updatedTime;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DepartmentInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="depId">单位编号</param>
        ///<param name="depName">单位名称</param>
        ///<param name="depCode">单位编码</param>
        ///<param name="firstCode">单位附加编码一</param>
        ///<param name="secondCode">单位附加编码二</param>
        ///<param name="createdTime">增加时间</param>
        ///<param name="updatedTime">修改时间</param>
        public DepartmentInfo(decimal depId, string depName, string depCode, string firstCode, string secondCode, DateTime createdTime, DateTime updatedTime)
        {
            _depId = depId;
            _depName = depName;
            _depCode = depCode;
            _firstCode = firstCode;
            _secondCode = secondCode;
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
        /// 单位名称
        /// </summary>
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
        /// 附加编码一
        /// </summary>
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