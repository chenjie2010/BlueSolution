//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserLogHandler.cs
// 描述：UserLog 业务处理类
// 作者：ChenJie 
// 编写日期：2016/8/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.SystemModule;

namespace Blue.BusinessInterface.SystemModule
{
/// <summary>
    /// UserLog 接口
    /// </summary>
    public interface IUserLogHandler: IPrincipalBusiness<UserLogInfo>
    {
        #region 接口

        /// <summary>
        /// 根据条件按月统计日志数量
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        Dictionary<int, int> GetStaticsByMonth(decimal userId, LogTitle logTitle);

        /// <summary>
        /// 按编号批量删除日志
        /// </summary>
        /// <param name="logIds"></param>
        void Delete(IList<decimal> logIds);

        /// <summary>
        /// 按条件删除日志
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        int Delete(IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得以表 UserLog 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        #endregion
    }
}
