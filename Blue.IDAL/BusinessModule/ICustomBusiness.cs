//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomBusiness.cs
// 描述：CustomBusiness 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/12/20
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.IDAL.BusinessModule
{
    /// <summary>
    /// CustomBusiness 接口
    /// </summary>
    public interface ICustomBusiness : ICommonNode, IPrincipalTable<CustomBusinessInfo>
    {
        #region 接口

        /// <summary>
        /// 根据条件查询业务数量
        /// </summary>
        /// <param name="conditionId"></param>
        /// <param name="businessMenu"></param>
        /// <returns></returns>
		int GetTotalCount(decimal conditionId, BusinessMenu businessMenu);

        /// <summary>
        /// 通过数据填报实例编号获取业务名称
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        string GetBusinessNameByInstanceId(decimal instanceId);

        /// <summary>
        /// 获得授权业务
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        IList<ExtendedCustomBusinessInfo> GetBusiness(decimal userId, decimal menuId);

        /// <summary>
        /// 获得授权业务
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<ExtendedCustomBusinessInfo> GetBusiness(decimal userId);

        /// <summary>
        /// 获得菜单的图标名称
        /// </summary>
        ///<param name="businessId">业务编号</param>
        /// <returns> 图标名称</returns>
        string GetIconName(decimal businessId);

        /// <summary>
        /// 更新 CustomBusinessInfo 对象
        /// </summary>
        /// <param name="customBusinessInfo">CustomBusinessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="imageData">图片数据</param>
        void Update(CustomBusinessInfo customBusinessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, byte[] imageData);

        /// <summary>
        /// 向 CustomBusiness 表中插入一条新记录
        /// </summary>
        /// <param name="customBusinessInfo">customBusinessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="imageData">图片数据</param>
        /// <returns>自动增加的关键字的值</returns>
        decimal Insert(CustomBusinessInfo customBusinessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, byte[] imageData);

        #endregion
    }
}
    