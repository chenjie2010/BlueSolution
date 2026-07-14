//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomMenuContract.cs
// 描述： CustomMenu 契约层接口
// 作者：ChenJie 
// 编写日期：2017/12/14
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.BusinessModule
{
    /// <summary>
    /// CustomMenu 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomMenuContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomMenuContract : ICommonNodeContract, IPrincipalContracts<CustomMenuInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 根据菜单类型获得一级菜单
        /// </summary>
        /// <param name="menuType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCustomMenu")]
        CustomMenuInfo GetCustomMenu(byte menuType);

        /// <summary>
        /// 最大的菜单类型
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetMaxMenuType")]
        byte GetMaxMenuType();

        /// <summary>
        /// 向 CustomMenu 表中插入一条新记录
        /// </summary>
        /// <param name="customMenuInfo">customMenuInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        /// <returns>自动增加的关键字的值</returns>
        [OperationContract(Name = "InsertWithImageData")]
        decimal Insert(CustomMenuInfo customMenuInfo, byte[] imageData);

        /// <summary>
        /// 更新 CustomMenuInfo 对象
        /// </summary>
        /// <param name="customMenuInfo">CustomMenuInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        [OperationContract(Name = "UpdateWithImageData")]
        void Update(CustomMenuInfo customMenuInfo, byte[] imageData);

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileName">下载的图片文件名</param>
        /// <returns></returns>
        [OperationContract(Name = "DownLoadIcons")]
        byte[] DownLoadIcons(string fileName);

        /// <summary>
        /// 获得菜单分类对象列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetMenuClasses")]
        IList<CustomMenuInfo> GetMenuClasses(decimal userId);
        
        #endregion
    }
}