//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：BusinessInstanceStepInfo.cs
// 描述：BusinessInstanceStepInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/3/30
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.DataFilledModule
{
    /// <summary>
    /// <para>BusinessInstanceStepInfo 类</para>
    /// <para>填报业务流程</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class BusinessInstanceStepInfo
    {
        #region 内部成员变量

        private decimal _instanceId;
        private int _sorting;
        private decimal _userId;
        private byte _reviewedAction;
        private bool _actionVisible;
        private DateTime _timeReviewed;
        private string _commentReviewed = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public BusinessInstanceStepInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="instanceId">实例编号</param>
        ///<param name="sorting">排序</param>
        ///<param name="userId">用户编号</param>
        ///<param name="reviewedAction">审核状态</param>
        ///<param name="actionVisible">可见性</param>
        ///<param name="timeReviewed">审核时间</param>
        ///<param name="commentReviewed">审核意见</param>
        public BusinessInstanceStepInfo(decimal instanceId, int sorting, decimal userId, byte reviewedAction, bool actionVisible,
            DateTime timeReviewed, string commentReviewed)
        {
            _instanceId = instanceId;
            _sorting = sorting;
            _userId = userId;
            _reviewedAction = reviewedAction;
            _actionVisible = actionVisible;
            _timeReviewed = timeReviewed;
            _commentReviewed = commentReviewed;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 实例编号
        /// </summary>
        public decimal InstanceId
        {
            get
            {
                return _instanceId;
            }
            set
            {
                if (_instanceId == value)
                    return;
                _instanceId = value;
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
        /// 审核状态
        /// </summary>
        public byte ReviewedAction
        {
            get
            {
                return _reviewedAction;
            }
            set
            {
                if (_reviewedAction == value)
                    return;
                _reviewedAction = value;
            }
        }

        /// <summary>
        /// 可见性
        /// </summary>
        public bool ActionVisible
        {
            get
            {
                return _actionVisible;
            }
            set
            {
                if (_actionVisible == value)
                    return;
                _actionVisible = value;
            }
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime TimeReviewed
        {
            get
            {
                return _timeReviewed;
            }
            set
            {
                if (_timeReviewed == value)
                    return;
                _timeReviewed = value;
            }
        }

        /// <summary>
        /// 审核意见
        /// </summary>
        [StringLengthValidator(0, 512, MessageTemplate = "审核意见长度不能超过512位！")]
        public string CommentReviewed
        {
            get
            {
                return _commentReviewed;
            }
            set
            {
                if (_commentReviewed == value)
                    return;
                _commentReviewed = value;
            }
        }

        #endregion

    }
}