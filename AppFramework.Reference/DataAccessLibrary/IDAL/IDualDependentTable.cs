//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IDualDependentTable.cs
// 描述： IDualDependentTable 数据访问层接口
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Reference.DataAccessLibrary
{
    /// <summary>
    /// IDualDependentTable 接口，双依赖表类型：它的主键由自身的主键与其它两个表的主键共同构成
    /// </summary>
    public interface IDualDependentTable<T> : ITableBase<T> where T : class
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
        ///<param name="otherForeignKey">另外一个被关联表的外键值</param>
        ///<param name="modeId">T 类的对象编号</param>
        /// <returns> T 类的对象</returns>
        T GetModelInfo(decimal foreignKey, decimal otherForeignKey, decimal modeId);

        /// <summary>
        /// 根据编号删除 T 类的对象
        /// </summary>
        /// <param name="foreignKey">被关联表的外键值</param>
        ///<param name="otherForeignKey">另外一个被关联表的外键值</param>
        /// <param name="modeId">T 类的对象编号</param>       
        void Delete(decimal foreignKey, decimal otherForeignKey, decimal modeId);

        #endregion
    }
}
