//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomAssociationHandler.cs
// 描述：CustomAssociation 业务处理类
// 作者：ChenJie 
// 编写日期：2016/10/2
// Copyright 2016
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
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomAssociation.
    /// </summary>
    public class CustomAssociationHandler : CommonNodeBusiness, ICustomAssociationHandler
    {
        #region 工厂类实例

        private static readonly ICustomAssociation dalCustomAssociation = BusinessDataAccessFactory.CreateCustomAssociation();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomAssociationHandler() : base(dalCustomAssociation)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 customassociation 表中插入一条新记录
        /// </summary>
        /// <param name="customAssociationInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomAssociationInfo customAssociationInfo)
        {
            //自动增加的关键字的值
            decimal customAssociationId = 0;

            // 验证输入
            if (customAssociationInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customAssociationId = dalCustomAssociation.Insert(customAssociationInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customAssociationId;
        }

        /// <summary>
        /// 获得 CustomAssociationInfo 对象
        /// </summary>
        ///<param name="associatedDataFieldId">关联编号</param>
        /// <returns> CustomAssociationInfo 对象</returns>
        public CustomAssociationInfo GetModelInfo(decimal associatedDataFieldId)
        {
            CustomAssociationInfo customAssociationInfo = null;

            // 验证输入
            if (associatedDataFieldId < 0)
            {
                return null;
            }

            try
            {
                customAssociationInfo = dalCustomAssociation.GetModelInfo(associatedDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customAssociationInfo;
        }

        /// <summary>
        /// 更新 CustomAssociationInfo 对象
        /// </summary>
        /// <param name="customAssociationInfo">CustomAssociationInfo 对象</param>
        public void Update(CustomAssociationInfo customAssociationInfo)
        {
            // 验证输入
            if (customAssociationInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomAssociation.Update(customAssociationInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CustomAssociationInfo 对象
        /// </summary>
        ///<param name="associatedDataFieldId">关联编号</param>
        /// <returns> CustomAssociationInfo 对象</returns>
        public void Delete(decimal associatedDataFieldId)
        {
            // 验证输入
            if (associatedDataFieldId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomAssociation.Delete(associatedDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 获得 CustomAssociationInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomAssociationInfo 对象列表</returns>
        public IList<CustomAssociationInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomAssociationInfo> customAssociationInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customAssociationInfos = dalCustomAssociation.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customAssociationInfos;
        }

        /// <summary>
        /// 获得 CustomAssociation 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomAssociationInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomAssociation.GetTotalCount(whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        /// <param name="movedDriection"></param>
        public void UpdateRecordSorting(decimal associationId, decimal recordId, MovedDriection movedDriection)
        {
            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("关联编号不能小于或是等于0。");
            }
            
            if (recordId <= 0)
            {
                throw new ArgumentException("记录编号不能小于或是等于0。");
            }

            try
            {
               dalCustomAssociation.UpdateRecordSorting(associationId, recordId, movedDriection);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得指定列数据
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public DataTable GetAssociationColumnData(decimal associatedDataFieldId)
        {
            DataTable data = null;

            // 验证输入
            if (associatedDataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                data = dalCustomAssociation.GetAssociationColumnData(associatedDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return data;
        }

        /// <summary>
        /// 该表中的字段属于物理字段的关联字段类型的个数
        /// </summary>
        /// <param name="associationCode"></param>
        /// <returns></returns>
        public int GetDataFieldCountConnected(string associationCode)
        {
            int count = 0;

            // 验证输入
            if (string.IsNullOrWhiteSpace(associationCode))
            {
                throw new ArgumentException("编码不能为空。");
            }

            try
            {
                count = dalCustomAssociation.GetDataFieldCountConnected(associationCode);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 该表是否有字段属于物理字段的关联字段类型
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public bool HasDataFieldConnected(decimal associationId)
        {
            bool result = false;

            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                result = dalCustomAssociation.HasDataFieldConnected(associationId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 重置关联表
        /// </summary>
        /// <param name="associationId"></param>
        public void ResetTable(decimal associationId)
        {
            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                dalCustomAssociation.ResetTable(associationId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 导入业务数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="dataTable"></param>
        public void ImportDataTable(decimal associationId, DataTable dataTable)
        {
            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                dalCustomAssociation.ImportDataTable(associationId, dataTable);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(IList<decimal> groupIds)
        {
            DataSet ds = null;

            // 验证输入
            if (groupIds == null || groupIds.Count <= 0)
            {
                throw new ArgumentException("分组编号列表不能为空，或者个数不能小于等于0。");
            }

            try
            {
                ds = dalCustomAssociation.GetPageRecord(groupIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetAssociationData(decimal associationId, int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                ds = dalCustomAssociation.GetAssociationData(associationId, startPosition, count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 是否是超大关联
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public bool GetSuperAssociationEnabled(decimal associationId)
        {
            bool superAssociationEnabled = false;

            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                superAssociationEnabled = dalCustomAssociation.GetSuperAssociationEnabled(associationId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return superAssociationEnabled;
        }

        /// <summary>
        /// 在关联表中增加记录
        /// </summary>
        /// <param name="associationId">关联编号</param>
        /// <param name="commonDataFields"></param>
        /// <returns></returns>
        public decimal Insert(decimal associationId, IList<CommonDataField> commonDataFields)
        {
            decimal recordId = 0;

            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                recordId = dalCustomAssociation.Insert(associationId, commonDataFields);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return recordId;
        }

        /// <summary>
        /// 更新关联表中的记录
        /// </summary>
        /// <param name="associationId">关联编号</param>
        /// <param name="recordId">关联表记录编号</param>
        /// <param name="commonDataFields"></param>
        public void Update(decimal associationId, decimal recordId, IList<CommonDataField> commonDataFields)
        {
            // 验证输入
            if (associationId <= 0 || recordId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                dalCustomAssociation.Update(associationId, recordId, commonDataFields);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除关联表的记录
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        public void Delete(decimal associationId, decimal recordId)
        {
            // 验证输入
            if (associationId <= 0 || recordId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                dalCustomAssociation.Delete(associationId, recordId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public DataTable GetAssociationData(decimal associationId)
        {
            DataTable data = null;

            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                data = dalCustomAssociation.GetAssociationData(associationId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return data;
        }

        /// <summary>
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public DataTable GetAssociationDataWithSortingDataField(decimal associationId)
        {
            DataTable data = null;

            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                data = dalCustomAssociation.GetAssociationDataWithSortingDataField(associationId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return data;
        }

        /// <summary>
        /// 根据关联编号对于的表获得相应的记录行
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public DataTable GetAssociationData(decimal associationId, decimal recordId)
        {
            DataTable data = null;

            // 验证输入
            if (associationId <= 0 || recordId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                data = dalCustomAssociation.GetAssociationData(associationId, recordId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return data;

        }

        #endregion

        #region 私有方法

        #endregion
    }
}
