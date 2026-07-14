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
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.BusinessInterface.BusinessModule
{
/// <summary>
    /// CustomMenu 接口
    /// </summary>
    public interface ICustomMenuHandler: ICommonNodeBusiness, IPrincipalBusiness<CustomMenuInfo>
    {
        #region 接口

        /// <summary>
        /// 检查子菜单权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        bool CheckSubMenuAuthority(decimal userId, decimal menuId);

        /// <summary>
        /// 根据菜单类型获得一级菜单
        /// </summary>
        /// <param name="menuType"></param>
        /// <returns></returns>
        CustomMenuInfo GetCustomMenu(byte menuType);

        /// <summary>
        /// 最大的菜单类型
        /// </summary>
        /// <returns></returns>
        byte GetMaxMenuType();

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileName">下载的图片文件名</param>
        /// <returns></returns>
        byte[] DownLoadIcons(string fileName);

        /// <summary>
        /// 向 CustomMenu 表中插入一条新记录
        /// </summary>
        /// <param name="customMenuInfo">customMenuInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        /// <returns>自动增加的关键字的值</returns>
        decimal Insert(CustomMenuInfo customMenuInfo, byte[] imageData);

        /// <summary>
        /// 更新 CustomMenuInfo 对象
        /// </summary>
        /// <param name="customMenuInfo">CustomMenuInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        void Update(CustomMenuInfo customMenuInfo, byte[] imageData);

        /// <summary>
        /// 获得菜单分类对象列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<CustomMenuInfo> GetMenuClasses(decimal userId);

        #endregion
    }
}
