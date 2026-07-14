//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IUserAndMail.cs
// 描述：UserAndMail 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/9/12
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.GeneralAffairModule;

namespace Blue.IDAL.GeneralAffairModule
{
    /// <summary>
    /// UserAndMail 接口
    /// </summary>
    public interface IUserAndMail : ICorrelatedTable
    {
        #region 接口

        /// <summary>
		/// 获得 UserAndMailInfo 对象
		/// </summary>
		///<param name="mailId">邮件编号</param>
		///<param name="userId">用户编号</param>
		/// <returns> UserAndMailInfo 对象</returns>
		UserAndMailInfo GetModelInfo(decimal mailId, decimal userId);

        /// <summary>
        /// 更新删除状态
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="userId"></param>
        /// <param name="isDelete"></param>
        void Update(decimal mailId, decimal userId, bool isDelete);

        /// <summary>
        /// 更新删除状态
        /// </summary>
        /// <param name="mailIds"></param>
        /// <param name="userId"></param>
        /// <param name="isDelete"></param>
        void Update(IList<decimal> mailIds, decimal userId, bool isDelete);

        #endregion
    }
}