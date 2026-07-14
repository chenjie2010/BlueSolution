//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IAppointmentBusiness.cs
// 描述: AppointmentBusiness 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.BusinessDesignerModule;

namespace Blue.IDAL.BusinessDesignerModule
{
    /// <summary>
    /// AppointmentBusiness 接口
    /// </summary>
    public interface IAppointmentBusiness : ICommonNode, IPrincipalTable<AppointmentBusinessInfo>
    {
		#region 接口
		

        #endregion
    }
}