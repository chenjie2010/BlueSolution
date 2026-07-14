//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDepartmentService.cs
// 描述：CustomDepartment 操作服务类
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.CustomLibrary;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;
using Blue.WCFContracts.SystemModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.SystemModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomDepartment.
    /// </summary>
    public class CustomDepartmentService : CommonNodeServices, ICustomDepartmentContract
    {
        #region 业务实例

        private static readonly ICustomDepartmentHandler customDepartmentHandler = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<ICustomDepartmentHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomDepartmentService() : base(customDepartmentHandler)
        {
        }

        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 customdepartment 表中插入一条新记录
        /// </summary>
        /// <param name="customDepartmentInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomDepartmentInfo customDepartmentInfo)
        {
            return customDepartmentHandler.Insert(customDepartmentInfo);
        }

        /// <summary>
        /// 获得 CustomDepartmentInfo 对象
        /// </summary>
        ///<param name="depId">部门编号</param>
        /// <returns> CustomDepartmentInfo 对象</returns>
        public CustomDepartmentInfo GetModelInfo(decimal depId)
        {
            return customDepartmentHandler.GetModelInfo(depId);
        }

        /// <summary>
        /// 更新 CustomDepartmentInfo 对象
        /// </summary>
        /// <param name="customDepartmentInfo">CustomDepartmentInfo 对象</param>
        public void Update(CustomDepartmentInfo customDepartmentInfo)
        {
            customDepartmentHandler.Update(customDepartmentInfo);
        }

        /// <summary>
        /// 删除 CustomDepartmentInfo 对象
        /// </summary>
        ///<param name="depId">部门编号</param>
        /// <returns> CustomDepartmentInfo 对象</returns>
        public void Delete(decimal depId)
        {
            customDepartmentHandler.Delete(depId);
        }

        /// <summary>
        /// 获得 CustomDepartmentInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDepartmentInfo 对象列表</returns>
        public IList<CustomDepartmentInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customDepartmentHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomDepartment 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomDepartmentInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customDepartmentHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得所有的单位信息
        /// </summary>
        /// <returns></returns>
        public IList<CustomDepartmentInfo> GetCustomDepartmentInfos()
        {
            return customDepartmentHandler.GetCustomDepartmentInfos();
        }

        /// <summary>
        /// 获得单位编号
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public decimal GetDepIdByName(string depName)
        {
            return customDepartmentHandler.GetDepIdByName(depName);
        }

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        public IList<string> GetTemplateColumnCaptions()
        {
            return customDepartmentHandler.GetTemplateColumnCaptions();
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="parentDepId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal parentDepId)
        {
            return customDepartmentHandler.GetPageRecord(parentDepId);
        }

        /// <summary>
        /// 获得表 CustomDepartment 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(int startPosition, int count, ref int totalCount)
        {
            return customDepartmentHandler.GetPageRecord(startPosition, count, ref totalCount);
        }

        /// <summary>
        /// 获得单位编码
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public string GetDepCode(decimal depId)
        {
            return customDepartmentHandler.GetDepCode(depId);
        }

        /// <summary>
        /// 获得用户单位编号和用户单位名称的对应集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<decimal, string> GetDepIdAndNames()
        {
            return customDepartmentHandler.GetDepIdAndNames();
        }

        /// <summary>
        /// 获得单位名称与编号集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, decimal> GetNameAndIds()
        {
            return customDepartmentHandler.GetNameAndIds();
        }

        /// <summary>
        /// 通过用户编号获得管理单位节点列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal userId)
        {
            return customDepartmentHandler.GetCommonNodes(userId);
        }

        /// <summary>
        /// 获得单位对象
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public CommonNode GetCommonNode(string depName)
        {
            return customDepartmentHandler.GetCommonNode(depName);
        }

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public CommonItemList<decimal, CommonNode> GetTreeviewCommonNodes(string depName)
        {
            return customDepartmentHandler.GetTreeviewCommonNodes(depName);
        }

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public CommonItemList<decimal, CommonNode> GetTreeviewCommonNodesWithRoot(string depName)
        {
            return customDepartmentHandler.GetTreeviewCommonNodesWithRoot(depName);
        }

        /// <summary>
        /// 通过根节点单位信息
        /// </summary>
        public CustomDepartmentInfo GetRootDepartmentInfo()
        {
            return customDepartmentHandler.GetRootDepartmentInfo();
        }

        #endregion
    }
}
