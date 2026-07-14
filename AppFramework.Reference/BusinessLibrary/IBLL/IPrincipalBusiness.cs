//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IPrincipalBusiness.cs
// 描述： 主表类型的业务接口
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
    /// 主表类型的业务接口
    /// </summary>
    public interface IPrincipalBusiness<T> : IBusinessBase<T> where T : class
    {
        #region 接口

        /// <summary>
        /// 插入 T 类的对象
        /// </summary>
        /// <param name="modeInfo">T 类的对象</param>
        /// <returns>自动增加的关键字的值</returns>
        decimal Insert(T modeInfo);

        /// <summary>
        /// 获得 T 类的对象
        /// </summary>
        ///<param name="modeId">T 类对象的编号</param>
        /// <returns> T 类的对象</returns>
        T GetModelInfo(decimal modeId);

        /// <summary>
        /// 根据编号删除 T 类的对象
        /// </summary>
        /// <param name="modeId">T 类对象的编号</param>
        void Delete(decimal modeId);

        #endregion

    }
}
