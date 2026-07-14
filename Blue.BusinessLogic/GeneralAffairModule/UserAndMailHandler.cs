//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserAndMailHandler.cs
// 描述：UserAndMail 业务处理类
// 作者：ChenJie 
// 编写日期：2017/9/12
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.GeneralAffairModule;
using Blue.Model.GeneralAffairModule;
using Blue.BusinessInterface.GeneralAffairModule;

namespace Blue.BusinessLogic.GeneralAffairModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.UserAndMail.
    /// </summary>
    public class UserAndMailHandler : CorrelatedTableBusiness, IUserAndMailHandler
    {
        #region 工厂类实例
        
        private static readonly IUserAndMail dalUserAndMail = GeneralAffairDataAccessFactory.CreateUserAndMail(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserAndMailHandler() : base(dalUserAndMail)
        {
		}

        #endregion

        #region 默认方法

      
        #endregion

        #region 自定义方法

        #endregion

        #region 私有方法

        #endregion
    }
}
