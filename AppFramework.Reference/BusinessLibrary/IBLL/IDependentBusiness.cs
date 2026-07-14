//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IDependentBusiness.cs
// 描述： 依赖表类型业务接口
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
    /// 依赖表类型业务接口
    /// </summary>
    public interface IDependentBusiness<T> : IBusinessBase<T> where T : class
    {
        #region 接口

        /// <summary>
        /// 插入 T 类的对象
        /// </summary>
        /// <param name="modeInfo">T 类的对象</param>
        void Insert(T modeInfo);

        /// <summary>
        /// 获得 T 类的对象
        /// </summary>        
        ///<param name="foreignKey">被关联表的外键值</param>
        ///<param name="modeId">T 类的对象编号</param>
        /// <returns> T 类的对象</returns>
        T GetModelInfo(decimal foreignKey, int modeId);

        /// <summary>
        /// 根据编号删除 T 类的对象
        /// </summary>
        /// <param name="foreignKey">被关联表的外键值</param>
        /// <param name="modeId">T 类的对象编号</param>
        void Delete(decimal foreignKey, int modeId);

        #endregion
    }
}
