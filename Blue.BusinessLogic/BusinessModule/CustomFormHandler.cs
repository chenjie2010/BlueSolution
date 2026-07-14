//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomFormHandler.cs
// 描述：CustomForm 业务处理类
// 作者：ChenJie 
// 编写日期：2017/11/27
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
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomForm.
    /// </summary>
    public class CustomFormHandler : CommonNodeBusiness, ICustomFormHandler
    {
        #region 工厂类实例

        private static readonly ICustomForm dalCustomForm = BusinessDataAccessFactory.CreateCustomForm();
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomFormHandler() : base(dalCustomForm)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 customform 表中插入一条新记录
        /// </summary>
        /// <param name="customFormInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomFormInfo customFormInfo)
        {
            //自动增加的关键字的值
            decimal customFormId = 0;

            // 验证输入
            if (customFormInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customFormId = dalCustomForm.Insert(customFormInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customFormId;
        }

        /// <summary>
        /// 获得 CustomFormInfo 对象
        /// </summary>
        ///<param name="formId">业务编号</param>
        /// <returns> CustomFormInfo 对象</returns>
        public CustomFormInfo GetModelInfo(decimal formId)
        {
            CustomFormInfo customFormInfo = null;

            // 验证输入
            if (formId < 0)
            {
                return null;
            }

            try
            {
                customFormInfo = dalCustomForm.GetModelInfo(formId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customFormInfo;
        }

        /// <summary>
        /// 更新 CustomFormInfo 对象
        /// </summary>
        /// <param name="customFormInfo">CustomFormInfo 对象</param>
        public void Update(CustomFormInfo customFormInfo)
        {
            // 验证输入
            if (customFormInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomForm.Update(customFormInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CustomFormInfo 对象
        /// </summary>
        ///<param name="formId">业务编号</param>
        /// <returns> CustomFormInfo 对象</returns>
        public void Delete(decimal formId)
        {
            // 验证输入
            if (formId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomForm.Delete(formId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 CustomFormInfo 对象的列表
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public IList<CustomFormInfo> GetModelInfos(decimal sectionId)
        {
            //创建集合对象
            IList<CustomFormInfo> customFormInfos = null;

            try
            {
                customFormInfos = dalCustomForm.GetModelInfos(sectionId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customFormInfos;
        }

        /// <summary>
		/// 获得 CustomFormInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomFormInfo 对象列表</returns>
		public IList<CustomFormInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomFormInfo> customFormInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customFormInfos = dalCustomForm.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customFormInfos;
        }

        /// <summary>
        /// 获得 CustomForm 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomFormInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomForm.GetTotalCount(whereConditons);
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
        /// 通过组合表编号查询数据填报的窗体数量
        /// </summary>
        /// <param name="combinedTableId">组合表编号</param>
        /// <returns>记录数目</returns>
        public int GetTotalCountByCombinedTableId(decimal combinedTableId)
        {
            int count = 0;

            if (combinedTableId <= 0)
            {
                throw new ArgumentException("组合表编号不能小于或是等于0。");
            }
            try
            {
                count = dalCustomForm.GetTotalCountByCombinedTableId(combinedTableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 向 CustomForm 表中插入一条新记录
        /// </summary>
        /// <param name="customFormInfo">customFormInfo 对象</param>
        /// <param name="upLoadFileInfos">附件列表</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomFormInfo customFormInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //自动增加的关键字的值
            decimal dataId = 0;

            // 验证输入
            if (customFormInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                dataId = dalCustomForm.Insert(customFormInfo, upLoadFileInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataId;
        }

        /// <summary>
        /// 更新数据表格和附件信息
        /// </summary>
        /// <param name="customFormInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        public void Update(CustomFormInfo customFormInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            // 验证输入
            if (customFormInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }

            try
            {
                dalCustomForm.Update(customFormInfo, upLoadFileInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        #endregion

        #region 私有方法

        #endregion
    }
}
