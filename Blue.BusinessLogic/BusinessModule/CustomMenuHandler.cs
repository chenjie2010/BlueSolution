//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomMenuHandler.cs
// 描述：CustomMenu 业务处理类
// 作者：ChenJie 
// 编写日期：2017/12/14
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
    /// 业务层处理类，对于的表： dbo.CustomMenu.
    /// </summary>
    public class CustomMenuHandler : CommonNodeBusiness, ICustomMenuHandler
    {
        #region 工厂类实例

        private static readonly ICustomMenu dalCustomMenu = BusinessDataAccessFactory.CreateCustomMenu();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomMenuHandler() : base(dalCustomMenu)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 custommenu 表中插入一条新记录
        /// </summary>
        /// <param name="customMenuInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomMenuInfo customMenuInfo)
        {
            return Insert(customMenuInfo, null);
        }

        /// <summary>
        /// 获得 CustomMenuInfo 对象
        /// </summary>
        ///<param name="menuId">菜单编号</param>
        /// <returns> CustomMenuInfo 对象</returns>
        public CustomMenuInfo GetModelInfo(decimal menuId)
        {
            CustomMenuInfo customMenuInfo = null;

            // 验证输入
            if (menuId < 0)
            {
                return null;
            }

            try
            {
                customMenuInfo = dalCustomMenu.GetModelInfo(menuId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customMenuInfo;
        }

        /// <summary>
        /// 更新 CustomMenuInfo 对象
        /// </summary>
        /// <param name="customMenuInfo">CustomMenuInfo 对象</param>
        public void Update(CustomMenuInfo customMenuInfo)
        {
            Update(customMenuInfo, null);
        }

        /// <summary>
        /// 删除 CustomMenuInfo 对象
        /// </summary>
        ///<param name="menuId">菜单编号</param>
        /// <returns> CustomMenuInfo 对象</returns>
        public void Delete(decimal menuId)
        {
            // 验证输入
            if (menuId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomMenu.Delete(menuId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 获得 CustomMenuInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomMenuInfo 对象列表</returns>
        public IList<CustomMenuInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomMenuInfo> customMenuInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customMenuInfos = dalCustomMenu.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customMenuInfos;
        }

        /// <summary>
        /// 获得 CustomMenu 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomMenuInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomMenu.GetTotalCount(whereConditons);
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
        /// 检查子菜单权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool CheckSubMenuAuthority(decimal userId, decimal menuId)
        {
            bool result = false;

            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于或是等于0。");
            }

            if (menuId <= 0)
            {
                throw new ArgumentException("菜单编号不能小于或是等于0。");
            }

            try
            {
                result = dalCustomMenu.CheckSubMenuAuthority(userId, menuId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 根据菜单类型获得一级菜单
        /// </summary>
        /// <param name="menuType"></param>
        /// <returns></returns>
        public CustomMenuInfo GetCustomMenu(byte menuType)
        {
            CustomMenuInfo customMenuInfo = null;

            if (menuType <= 0)
            {
                throw new ArgumentException("菜单类型不能小于或是等于0。");
            }

            try
            {
                customMenuInfo = dalCustomMenu.GetCustomMenu(menuType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customMenuInfo;
        }

        /// <summary>
        /// 最大的菜单类型
        /// </summary>
        /// <returns></returns>
        public byte GetMaxMenuType()
        {
            byte maxMenuType = 0;

            try
            {
                maxMenuType = dalCustomMenu.GetMaxMenuType();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return maxMenuType;
        }

        /// <summary>
        /// 向 CustomMenu 表中插入一条新记录
        /// </summary>
        /// <param name="customMenuInfo">customMenuInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomMenuInfo customMenuInfo, byte[] imageData)
        {
            //自动增加的关键字的值
            decimal customMenuId = 0;

            // 验证输入
            if (customMenuInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customMenuId = dalCustomMenu.Insert(customMenuInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customMenuId;
        }

        /// <summary>
        /// 更新 CustomMenuInfo 对象
        /// </summary>
        /// <param name="customMenuInfo">CustomMenuInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        public void Update(CustomMenuInfo customMenuInfo, byte[] imageData)
        {
            // 验证输入
            if (customMenuInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                if (string.IsNullOrWhiteSpace(customMenuInfo.IconName))
                {
                    string iconName = dalCustomMenu.GetIconName(customMenuInfo.MenuId);
                    if(!string.IsNullOrWhiteSpace(iconName))
                    {
                        FileSavedHelper.DeleteIcons(iconName);
                    }                    
                }
                dalCustomMenu.Update(customMenuInfo, imageData);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
        /// 获得菜单分类对象列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CustomMenuInfo> GetMenuClasses(decimal userId)
        {
            //创建集合对象
            IList<CustomMenuInfo> customMenuInfos = null;

            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于或是等于0。");
            }

            try
            {
                customMenuInfos = dalCustomMenu.GetMenuClasses(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customMenuInfos;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
