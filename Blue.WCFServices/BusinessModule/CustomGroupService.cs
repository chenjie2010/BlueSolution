//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomGroupService.cs
// 描述：CustomGroup 操作服务类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomGroup.
    /// </summary>
    public class CustomGroupService : CommonNodeServices, ICustomGroupContract
    {
        #region 业务实例

        private static readonly ICustomGroupHandler customGroupHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomGroupHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomGroupService() : base(customGroupHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 customgroup 表中插入一条新记录
        /// </summary>
        /// <param name="customGroupInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomGroupInfo customGroupInfo)
        {
            return customGroupHandler.Insert(customGroupInfo);
        }

        /// <summary>
        /// 获得 CustomGroupInfo 对象
        /// </summary>
        ///<param name="groupId">分组编号</param>
        /// <returns> CustomGroupInfo 对象</returns>
        public CustomGroupInfo GetModelInfo(decimal groupId)
        {
            return customGroupHandler.GetModelInfo(groupId);
        }

        /// <summary>
        /// 更新 CustomGroupInfo 对象
        /// </summary>
        /// <param name="customGroupInfo">CustomGroupInfo 对象</param>
        public void Update(CustomGroupInfo customGroupInfo)
        {
            customGroupHandler.Update(customGroupInfo);
        }

        /// <summary>
        /// 删除 CustomGroupInfo 对象
        /// </summary>
        ///<param name="groupId">分组编号</param>
        /// <returns> CustomGroupInfo 对象</returns>
        public void Delete(decimal groupId)
        {
            customGroupHandler.Delete(groupId);
        }

        /// <summary>
        /// 获得 CustomGroupInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomGroupInfo 对象列表</returns>
        public IList<CustomGroupInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customGroupHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomGroup 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomGroupInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customGroupHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得数据集(获得节点自身数据)
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal groupId)
        {
            return customGroupHandler.GetPageRecord(groupId);
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="parentGroupId"></param>
        /// <param name="groupType"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal parentGroupId, GroupType groupType)
        {
            return customGroupHandler.GetPageRecord(parentGroupId, groupType);
        }

        /// <summary>
        /// 获得表 CustomEnum 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(int startPosition, int count, byte groupType, ref int totalCount)
        {
            return customGroupHandler.GetPageRecord(startPosition, count, groupType, ref totalCount);
        }

        #endregion
    }
}
