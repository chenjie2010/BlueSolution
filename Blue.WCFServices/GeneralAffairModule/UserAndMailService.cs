//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserAndMailService.cs
// 描述：UserAndMail 操作服务类
// 作者：ChenJie 
// 编写日期：2017/9/12
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.Unity;
using Blue.CustomLibrary.EnterpriseLibrary;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.CustomLibrary;
using Blue.Model.GeneralAffairModule;
using Blue.BusinessInterface.GeneralAffairModule;
using Blue.WCFContracts.GeneralAffairModule;

namespace Blue.WCFServices.GeneralAffairModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.UserAndMail.
    /// </summary>
    public class UserAndMailService : IUserAndMailContract
    {
        #region 业务实例
        
        private static readonly IUserAndMailHandler userAndMailHandler = BusinessLogicContainer.Instance.GeneralAffairModuleContainer.Resolve<IUserAndMailHandler>();
        
        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserAndMailService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		
        #endregion
		
		#region 实现自定义接口
		
		#endregion
    }
}
