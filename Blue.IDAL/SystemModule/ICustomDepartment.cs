//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomDepartment.cs
// 描述：CustomDepartment 数据访问层接口
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.SystemModule;

namespace Blue.IDAL.SystemModule
{
    /// <summary>
    /// CustomDepartment 接口
    /// </summary>
    public interface ICustomDepartment: ICommonNode, IPrincipalTable<CustomDepartmentInfo>
    {
        #region 接口

        /// <summary>
        /// 获得单位数量
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        int GetDepartmentCount(DateTime fromUpdatedTime, DateTime toUpdatedTime);

        /// <summary>
        /// 获得单位分页数据
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        DataTable GetDepartmentData(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime);

        /// <summary>
        /// 获得系统接口标记位
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        bool GetIsVisibleForInterface(decimal depId);

        /// <summary>
        /// 获得系统标记位
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        bool GetIsSystemDepartment(decimal depId);

        /// <summary>
        /// 获得所有的单位信息
        /// </summary>
        /// <returns></returns>
        IList<CustomDepartmentInfo> GetCustomDepartmentInfos();

        /// <summary>
        /// 获得单位文本值
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        string GetDepartmentText(decimal depId);

        /// <summary>
        /// 获得单位编号
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        decimal GetDepIdByName(string depName);

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        IList<string> GetTemplateColumnCaptions();

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="parentDepId"></param>
        /// <returns></returns>
        DataSet GetPageRecord(decimal parentDepId);

        /// <summary>
        /// 获得表 CustomDepartment 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecord(int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 获得单位编码
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        string GetDepCode(decimal depId);

        /// <summary>
        /// 获得用户单位编号和用户单位名称的对应集合
        /// </summary>
        /// <returns></returns>
        Dictionary<decimal, string> GetDepIdAndNames();

        /// <summary>
        /// 获得单位名称与编号集合
        /// </summary>
        /// <returns></returns>
        Dictionary<string, decimal> GetNameAndIds();
        
        /// <summary>
        /// 获得单位对象
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        CommonNode GetCommonNode(string depName);

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        CommonItemList<decimal, CommonNode> GetTreeviewCommonNodesWithRoot(string depName);

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        CommonItemList<decimal, CommonNode> GetTreeviewCommonNodes(string depName);

        /// <summary>
        /// 通过根节点单位信息
        /// </summary>
        CustomDepartmentInfo GetRootDepartmentInfo();

        #endregion
    }
}