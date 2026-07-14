//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IUserLogContract.cs
// 描述： UserLog 契约层接口
// 作者：ChenJie 
// 编写日期：2016/8/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.SystemModule;

namespace Blue.WCFContracts.SystemModule
{
    /// <summary>
    /// UserLog 契约接口
    /// </summary>
    [ServiceContract(Name = "IUserLogContract", Namespace = "http://www.scu.edu.cn/SystemModule/")]
    public interface IUserLogContract :  IPrincipalContracts<UserLogInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 按编号批量删除日志
        /// </summary>
        /// <param name="logIds"></param>
        [OperationContract(Name = "BatchDelete")]
        void Delete(IList<decimal> logIds);

        /// <summary>
        /// 按条件删除日志
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeleteByConditions")]
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
        [OperationContract(Name = "GetPageRecordOfMultiTables")]
        DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);


        #endregion
    }
}