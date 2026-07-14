//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICorrelatedBusiness.cs
// 描述： 联系表类型的业务接口
// 作者：ChenJie 
// 编写日期：2016/07/25
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AppFramework.Reference.BusinessLibrary
{
    /// <summary>
    /// 联系表类型的业务接口
    /// </summary>
    public interface ICorrelatedBusiness
    {
        #region 接口

        /// <summary>
        /// 根据用户编号获得用户类型
        /// </summary>
        /// <param name="firstForeignKey"></param>
        /// <returns></returns>
        IList<decimal> GetSecondIds(decimal firstForeignKey);

        #endregion
    }
}
