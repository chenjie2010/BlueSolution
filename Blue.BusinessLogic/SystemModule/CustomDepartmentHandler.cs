//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDepartmentHandler.cs
// 描述：CustomDepartment 业务处理类
// 作者：ChenJie 
// 编写日期：2016/8/19
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
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;

namespace Blue.BusinessLogic.SystemModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomDepartment.
    /// </summary>
    public class CustomDepartmentHandler : CommonNodeBusiness, ICustomDepartmentHandler
    {
        #region 工厂类实例

        private static readonly ICustomDepartment dalCustomDepartment = SystemDataAccessFactory.CreateCustomDepartment();
        private static readonly IDepartmentScope dalDepartmentScope = SystemDataAccessFactory.CreateDepartmentScope();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomDepartmentHandler() : base(dalCustomDepartment)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 customdepartment 表中插入一条新记录
        /// </summary>
        /// <param name="customDepartmentInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomDepartmentInfo customDepartmentInfo)
        {
            //自动增加的关键字的值
            decimal customDepartmentId = 0;

            // 验证输入
            if (customDepartmentInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customDepartmentId = dalCustomDepartment.Insert(customDepartmentInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDepartmentId;
        }

        /// <summary>
        /// 获得 CustomDepartmentInfo 对象
        /// </summary>
        ///<param name="depId">部门编号</param>
        /// <returns> CustomDepartmentInfo 对象</returns>
        public CustomDepartmentInfo GetModelInfo(decimal depId)
        {
            CustomDepartmentInfo customDepartmentInfo = null;

            // 验证输入
            if (depId < 0)
            {
                return null;
            }

            try
            {
                customDepartmentInfo = dalCustomDepartment.GetModelInfo(depId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDepartmentInfo;
        }

        /// <summary>
        /// 更新 CustomDepartmentInfo 对象
        /// </summary>
        /// <param name="customDepartmentInfo">CustomDepartmentInfo 对象</param>
        public void Update(CustomDepartmentInfo customDepartmentInfo)
        {
            // 验证输入
            if (customDepartmentInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomDepartment.Update(customDepartmentInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CustomDepartmentInfo 对象
        /// </summary>
        ///<param name="depId">部门编号</param>
        /// <returns> CustomDepartmentInfo 对象</returns>
        public void Delete(decimal depId)
        {
            // 验证输入
            if (depId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomDepartment.Delete(depId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 获得 CustomDepartmentInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDepartmentInfo 对象列表</returns>
        public IList<CustomDepartmentInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomDepartmentInfo> customDepartmentInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customDepartmentInfos = dalCustomDepartment.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDepartmentInfos;
        }

        /// <summary>
        /// 获得 CustomDepartment 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomDepartmentInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomDepartment.GetTotalCount(whereConditons);
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
        /// 获得单位数量
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public int GetDepartmentCount(DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            int count = 0;

            try
            {
                count = dalCustomDepartment.GetDepartmentCount(fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得单位分页数据
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public DataTable GetDepartmentData(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            DataTable dt = null;

            try
            {
                dt = dalCustomDepartment.GetDepartmentData(pos, pageSize, fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dt;
        }

        /// <summary>
        /// 获得系统接口标记位
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public bool GetIsVisibleForInterface(decimal depId)
        {
            bool isVisibleForInterface = false;

            // 验证输入
            if (depId <= 0)
            {
                throw new ArgumentException("单位编号不能为空。");
            }

            try
            {
                isVisibleForInterface = dalCustomDepartment.GetIsVisibleForInterface(depId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isVisibleForInterface;
        }

        /// <summary>
        /// 获得系统标记位
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public bool GetIsSystemDepartment(decimal depId)
        {
            bool isSystemDepartment = false;

            // 验证输入
            if (depId <= 0)
            {
                throw new ArgumentException("单位编号不能为空。");
            }

            try
            {
                isSystemDepartment = dalCustomDepartment.GetIsSystemDepartment(depId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isSystemDepartment;
        }

        /// <summary>
        /// 获得所有的单位信息
        /// </summary>
        /// <returns></returns>
        public IList<CustomDepartmentInfo> GetCustomDepartmentInfos()
        {
            IList<CustomDepartmentInfo> customDepartmentInfos = null;

            try
            {
                customDepartmentInfos = dalCustomDepartment.GetCustomDepartmentInfos();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDepartmentInfos;
        }

        /// <summary>
        /// 获得单位文本值
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public string GetDepartmentText(decimal depId)
        {
            string departmentText = string.Empty;

            // 验证输入
            if (depId <= 0)
            {
                throw new ArgumentException("单位编号不能为空。");
            }

            try
            {
                departmentText = dalCustomDepartment.GetDepartmentText(depId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return departmentText;
        }

        /// <summary>
        /// 获得单位编号
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public decimal GetDepIdByName(string depName)
        {
            decimal depId = decimal.MinValue;

            // 验证输入
            if (string.IsNullOrWhiteSpace(depName))
            {
                throw new ArgumentException("单位名称不能为空。");
            }

            try
            {
                depId = dalCustomDepartment.GetDepIdByName(depName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return depId;
        }

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        public IList<string> GetTemplateColumnCaptions()
        {
            IList<string> columnCaptions = null;

            try
            {
                columnCaptions = dalCustomDepartment.GetTemplateColumnCaptions();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return columnCaptions;
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="parentDepId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal parentDepId)
        {
            DataSet ds = null;

            // 验证输入
            if (parentDepId <= 0)
            {
                throw new ArgumentException("单位父节点编号不能小于或是等于0。");
            }

            try
            {
                ds = dalCustomDepartment.GetPageRecord(parentDepId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
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
            DataSet ds = null;

            try
            {
                ds = dalCustomDepartment.GetPageRecord(startPosition, count, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得单位编码
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public string GetDepCode(decimal depId)
        {
            string depCode = string.Empty;

            // 验证输入
            if (depId <= 0)
            {
                throw new ArgumentException("单位编码不能小于或是等于0。");
            }

            try
            {
                depCode = dalCustomDepartment.GetDepCode(depId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return depCode;
        }

        /// <summary>
        /// 获得用户单位编号和用户单位名称的对应集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<decimal, string> GetDepIdAndNames()
        {
            Dictionary<decimal, string> depIdAndNames = null;

            try
            {
                depIdAndNames = dalCustomDepartment.GetDepIdAndNames();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return depIdAndNames;
        }

        /// <summary>
        /// 获得单位名称与编号集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, decimal> GetNameAndIds()
        {
            Dictionary<string, decimal> nameAndIds = null;

            try
            {
                nameAndIds = dalCustomDepartment.GetNameAndIds();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nameAndIds;
        }

        /// <summary>
        /// 通过用户编号获得管理单位节点列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal userId)
        {
            //创建集合对象
            IList<CommonNode> commonNodes = null;

            if (userId <= 0)
            {
                throw new ArgumentException("单位名称不能为空。");
            }

            try
            {
                commonNodes = dalDepartmentScope.GetCommonNodes(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;

        }

        /// <summary>
        /// 获得单位对象
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public CommonNode GetCommonNode(string depName)
        {
            CommonNode commonNode = null;

            // 验证输入
            if (string.IsNullOrWhiteSpace(depName))
            {
                throw new ArgumentException("单位名称不能为空。");
            }

            try
            {
                commonNode = dalCustomDepartment.GetCommonNode(depName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNode;
        }

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public CommonItemList<decimal, CommonNode> GetTreeviewCommonNodesWithRoot(string depName)
        {
            CommonItemList<decimal, CommonNode> commonItemList = null;

            // 验证输入
            if (string.IsNullOrWhiteSpace(depName))
            {
                throw new ArgumentException("单位名称不能为空。");
            }

            try
            {
                commonItemList = dalCustomDepartment.GetTreeviewCommonNodesWithRoot(depName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonItemList;
        }

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public CommonItemList<decimal, CommonNode> GetTreeviewCommonNodes(string depName)
        {
            CommonItemList<decimal, CommonNode> commonItemList = null;

            // 验证输入
            if (string.IsNullOrWhiteSpace(depName))
            {
                throw new ArgumentException("单位名称不能为空。");
            }

            try
            {
                commonItemList = dalCustomDepartment.GetTreeviewCommonNodes(depName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonItemList;
        }
        
        /// <summary>
        /// 通过根节点单位信息
        /// </summary>
        public CustomDepartmentInfo GetRootDepartmentInfo()
        {
            CustomDepartmentInfo customDepartmentInfo = null;

            try
            {
                customDepartmentInfo = dalCustomDepartment.GetRootDepartmentInfo();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDepartmentInfo;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
