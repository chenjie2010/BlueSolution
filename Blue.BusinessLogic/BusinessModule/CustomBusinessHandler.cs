//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomBusinessHandler.cs
// 描述：CustomBusiness 业务处理类
// 作者：ChenJie 
// 编写日期：2017/12/20
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
    /// 业务层处理类，对于的表： dbo.CustomBusiness.
    /// </summary>
    public class CustomBusinessHandler : CommonNodeBusiness, ICustomBusinessHandler
    {
        #region 工厂类实例

        private static readonly ICustomBusiness dalCustomBusiness = BusinessDataAccessFactory.CreateCustomBusiness();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomBusinessHandler() : base(dalCustomBusiness)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 custombusiness 表中插入一条新记录
        /// </summary>
        /// <param name="customBusinessInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomBusinessInfo customBusinessInfo)
        {
            //自动增加的关键字的值
            decimal customBusinessId = 0;

            // 验证输入
            if (customBusinessInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customBusinessId = dalCustomBusiness.Insert(customBusinessInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customBusinessId;
        }

        /// <summary>
        /// 获得 CustomBusinessInfo 对象
        /// </summary>
        ///<param name="businessId">业务编号</param>
        /// <returns> CustomBusinessInfo 对象</returns>
        public CustomBusinessInfo GetModelInfo(decimal businessId)
        {
            CustomBusinessInfo customBusinessInfo = null;

            // 验证输入
            if (businessId < 0)
            {
                return null;
            }

            try
            {
                customBusinessInfo = dalCustomBusiness.GetModelInfo(businessId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customBusinessInfo;
        }

        /// <summary>
        /// 更新 CustomBusinessInfo 对象
        /// </summary>
        /// <param name="customBusinessInfo">CustomBusinessInfo 对象</param>
        public void Update(CustomBusinessInfo customBusinessInfo)
        {
            // 验证输入
            if (customBusinessInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomBusiness.Update(customBusinessInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CustomBusinessInfo 对象
        /// </summary>
        ///<param name="businessId">业务编号</param>
        /// <returns> CustomBusinessInfo 对象</returns>
        public void Delete(decimal businessId)
        {
            // 验证输入
            if (businessId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomBusiness.Delete(businessId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 获得 CustomBusinessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomBusinessInfo 对象列表</returns>
        public IList<CustomBusinessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomBusinessInfo> customBusinessInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customBusinessInfos = dalCustomBusiness.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customBusinessInfos;
        }

        /// <summary>
        /// 获得 CustomBusiness 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomBusinessInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomBusiness.GetTotalCount(whereConditons);
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
        /// 根据条件查询业务数量
        /// </summary>
        /// <param name="conditionId"></param>
        /// <param name="businessMenu"></param>
        /// <returns></returns>
		public int GetTotalCount(decimal conditionId, BusinessMenu businessMenu)
        {
            int count = 0;

            // 验证输入
            if (conditionId <= 0)
            {
                throw new ArgumentException("参数异常。编号参数不能小于或等于0");
            }
            try
            {
                count = dalCustomBusiness.GetTotalCount(conditionId, businessMenu);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 通过数据填报编号获取业务名称
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public string GetBusinessNameByInstanceId(decimal instanceId)
        {
            string businessName = string.Empty;

            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("参数异常。数据填报编号参数不能小于或等于0");
            }
            try
            {
                businessName = dalCustomBusiness.GetBusinessNameByInstanceId(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return businessName;
        }

        /// <summary>
        /// 获得授权业务
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public IList<ExtendedCustomBusinessInfo> GetBusiness(decimal userId, decimal menuId)
        {
            return GetBusinessByCondition(userId, menuId);
        }

        /// <summary>
        /// 获得授权业务
        /// 如果一个业务获得多个授权，取最大范围权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<ExtendedCustomBusinessInfo> GetBusiness(decimal userId)
        {
            return GetBusinessByCondition(userId, 0);    
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileName">下载的图片文件名</param>
        /// <returns></returns>
        public byte[] DownLoadIcons(string fileName)
        {
            return FileSavedHelper.DownLoadIcons(fileName);
        }

        /// <summary>
        /// 向 CustomBusiness 表中插入一条新记录
        /// </summary>
        /// <param name="customBusinessInfo">customBusinessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="imageData">图片数据</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomBusinessInfo customBusinessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, byte[] imageData)
        {
            //自动增加的关键字的值
            decimal customBusinessId = 0;

            // 验证输入
            if (customBusinessInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customBusinessId = dalCustomBusiness.Insert(customBusinessInfo, upLoadFileInfos, imageData);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customBusinessId;
        }

        /// <summary>
        /// 更新 CustomBusinessInfo 对象
        /// </summary>
        /// <param name="customBusinessInfo">CustomBusinessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="imageData">图片数据</param>
        public void Update(CustomBusinessInfo customBusinessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, byte[] imageData)
        {
            // 验证输入
            if (customBusinessInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                if (string.IsNullOrWhiteSpace(customBusinessInfo.IconName))
                {
                    string iconName = dalCustomBusiness.GetIconName(customBusinessInfo.BusinessId);
                    if (!string.IsNullOrWhiteSpace(iconName))
                    {
                        FileSavedHelper.DeleteIcons(iconName);
                    }
                }
                dalCustomBusiness.Update(customBusinessInfo, upLoadFileInfos, imageData);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion

        #region 私有方法
        
        /// <summary>
        /// 获得授权业务，如果一个业务获得多个授权，取最大范围权限
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<ExtendedCustomBusinessInfo> GetBusinessByCondition(decimal userId,decimal menuId)
        {
            IList<ExtendedCustomBusinessInfo> extendedCustomBusinessInfos = new List<ExtendedCustomBusinessInfo>();

            try
            {
                Dictionary<decimal, ExtendedCustomBusinessInfo> dicExtendedCustomBusinessInfos = new Dictionary<decimal, ExtendedCustomBusinessInfo>();
                IList<ExtendedCustomBusinessInfo> customBusinessInfos = null;
                if (menuId > 0)
                {
                    customBusinessInfos = dalCustomBusiness.GetBusiness(userId, menuId);
                }
                else
                {
                    customBusinessInfos = dalCustomBusiness.GetBusiness(userId);
                }
                foreach (ExtendedCustomBusinessInfo extendedCustomBusinessInfo in customBusinessInfos)
                {
                    ExtendedCustomBusinessInfo customBusinessInfo = null;
                    if (dicExtendedCustomBusinessInfos.ContainsKey(extendedCustomBusinessInfo.BusinessId))
                    {
                        customBusinessInfo = dicExtendedCustomBusinessInfos[extendedCustomBusinessInfo.BusinessId];
                        if (extendedCustomBusinessInfo.BusinessEnabled)
                        {
                            if (customBusinessInfo.BusinessEnabled)
                            {
                                if (!DataConvertionHelper.IsNullValue(customBusinessInfo.InitializedDate) &&
                                    ((DataConvertionHelper.IsNullValue(extendedCustomBusinessInfo.InitializedDate)
                                    || (extendedCustomBusinessInfo.InitializedDate < customBusinessInfo.InitializedDate))))
                                {
                                    customBusinessInfo.InitializedDate = extendedCustomBusinessInfo.InitializedDate;
                                }
                                if (!DataConvertionHelper.IsNullValue(customBusinessInfo.ExpiredDate) &&
                                    ((DataConvertionHelper.IsNullValue(extendedCustomBusinessInfo.ExpiredDate)
                                    || (extendedCustomBusinessInfo.ExpiredDate < customBusinessInfo.ExpiredDate))))
                                {
                                    customBusinessInfo.ExpiredDate = extendedCustomBusinessInfo.ExpiredDate;
                                }
                            }
                            else
                            {
                                customBusinessInfo.BusinessEnabled = extendedCustomBusinessInfo.BusinessEnabled;
                                customBusinessInfo.InitializedDate = extendedCustomBusinessInfo.InitializedDate;
                                customBusinessInfo.ExpiredDate = extendedCustomBusinessInfo.ExpiredDate;
                            }
                        }
                    }
                    else
                    {
                        dicExtendedCustomBusinessInfos.Add(extendedCustomBusinessInfo.BusinessId, extendedCustomBusinessInfo);
                    }
                }
                foreach (KeyValuePair<decimal, ExtendedCustomBusinessInfo> keyValue in dicExtendedCustomBusinessInfos)
                {
                    extendedCustomBusinessInfos.Add(keyValue.Value);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return extendedCustomBusinessInfos;
        }
        
        #endregion
    }
}
