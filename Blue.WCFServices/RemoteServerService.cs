//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AuthoritiedUserAndDataFieldService.cs
// 描述: AuthoritiedUserAndDataField 操作服务类
// 作者：ChenJie 
// 编写日期：2012/1/7
// Copyright 2012
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.WCFContracts;
using Blue.BusinessInterface.BusinessModule;
using Blue.BusinessInterface.SystemModule;
using Blue.BusinessInterface.UserModule;
using Blue.Model.BusinessModule;

namespace Blue.WCFServices
{
    /// <summary>
    /// 操作服务类
    /// </summary>
    public class RemoteServerService : IRemoteServerContract
    {
        #region 业务实例

        private readonly ISystemConfigHandler systemConfigHandler = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<ISystemConfigHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RemoteServerService()
		{
              
		}
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 获得用户名标签
        /// </summary>
        /// <returns></returns>
        public string GetUserNameLabelInfo()
        {
            ISystemConfigHandler systemConfigHandler = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<ISystemConfigHandler>();
            string userNameLabel = systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.UserNameLabelInfo);
            if (string.IsNullOrWhiteSpace(userNameLabel))
            {
                userNameLabel = AppSettingHelper.UserNameLabelInfo;
            }

            return userNameLabel;
        }

        /// <summary>
        /// 获得指定的字段的附件
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="dataFieldName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetFileData(string userName, string password, string dataFieldName, string fileName)
        {
            ICustomDataFieldHandler customDataFieldHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDataFieldHandler>();
            if (ValidateUser(userName, password))
            {
                return customDataFieldHandler.GetFileData(dataFieldName, fileName);
            }
            return null;
        }

        /// <summary>
        /// 测试用户名密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool TestRemoteConnection(string userName, string password)
        {
            bool result = false;

            ISystemConfigHandler systemConfigHandler = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<ISystemConfigHandler>();

            string userNameConfirmed = systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.RemoteUserName);
            string passwordConfirmed = systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.RemotePassword);
            if (userName.Equals(userNameConfirmed) && password.Equals(passwordConfirmed))
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 获得表达式字段名称
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="tablePhysicalName"></param>
        /// <param name="expressionText"></param>
        /// <param name="commonNodes"></param>
        /// <returns></returns>
        public string GetExpressionDataFieldName(string userName, string password, string tablePhysicalName, string expressionText, IList<CommonNode> commonNodes)
        {
            string expressionDataFieldName = string.Empty;

            ICustomDataFieldHandler customDataFieldHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDataFieldHandler>();
            if (ValidateUser(userName, password))
            {
                expressionDataFieldName = customDataFieldHandler.GetExpressionDataFieldName(tablePhysicalName, expressionText, commonNodes);
            }

            return expressionDataFieldName;
        }

        /// <summary>
        /// 获得表达式字段
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetExpressionCommonNodes(string userName, string password, decimal dataFieldId)
        {
            IList<CommonNode> commonNodes = null;

            ICustomExpressionHandler customExpressionHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomExpressionHandler>();
            if (ValidateUser(userName, password))
            {
                commonNodes = customExpressionHandler.GetCommonNodes(dataFieldId);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得表 CustomTable 的分页数据集
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="extendedCustomDataFieldInfos"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(string userName, string password, decimal tableId, IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos, int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;

            IDataBusinessHandler dataBusinessHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<IDataBusinessHandler>();
            if (ValidateUser(userName, password))
            {
                ds = dataBusinessHandler.GetPageRecord(tableId, extendedCustomDataFieldInfos, startPosition, count, whereConditons, sortingCondtions);
            }

            return ds;
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="tableId"></param>
        /// <param name="where"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetRecordCount(string userName, string password, decimal tableId, string where, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            ICustomTableHandler customTableHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomTableHandler>();
            if (ValidateUser(userName, password))
            {
                count = customTableHandler.GetRecordCount(tableId, where, whereConditons);
            }

            return count;
        }

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetTableRecordCount(string userName, string password, decimal tableId, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            IDataBusinessHandler dataBusinessHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<IDataBusinessHandler>();
            if (ValidateUser(userName, password))
            {
                count = dataBusinessHandler.GetTableRecordCount(tableId, whereConditons);
            }

            return count;
        }

        /// <summary>
        /// 获得表的编号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public decimal GetTableId(string userName, string password, decimal dataFieldId)
        {
            decimal tableId = decimal.MinValue;
            
            ICustomDataFieldHandler customDataFieldHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDataFieldHandler>();
            if (ValidateUser(userName, password))
            {
                tableId = customDataFieldHandler.GetTableId(dataFieldId);
            }
            
            return tableId;
        }
                
        /// <summary>
        /// 根据数据编号获得表的列表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodesByDatabaseId(string userName, string password, decimal sourceDatabaseId)
        {
            IList<CommonNode> commonNodes = null;

            ICustomTableHandler customTableHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomTableHandler>();
            if (ValidateUser(userName, password))
            {
                commonNodes = customTableHandler.GetCommonNodesByDatabaseId(sourceDatabaseId);
                foreach (var commonNode in commonNodes)
                {
                    commonNode.IsLeaf = true;
                }
            }

            return commonNodes;
        }

        /// <summary>
        /// 根据数据库编号获得分类的列表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCategoriesByDatabaseId(string userName, string password, decimal databaseId)
        {
            IList<CommonNode> commonNodes = null;

            ICustomCategoryHandler customCategoryHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomCategoryHandler>();
            if (ValidateUser(userName, password))
            {
                commonNodes = customCategoryHandler.GetChildNodes(databaseId);                
            }

            return commonNodes;
        }

        /// <summary>
        /// 根据分类编号获得表的列表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetTablesByCategoryId(string userName, string password, decimal categoryId)
        {
            IList<CommonNode> commonNodes = null;

            ICustomTableHandler customTableHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomTableHandler>();
            if (ValidateUser(userName, password))
            {
                commonNodes = customTableHandler.GetChildNodes(categoryId);
            }

            return commonNodes;
        }
        
        /// <summary>
        /// 获得字段对象
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public CustomDataFieldInfo GetModelInfoByDataFieldId(string userName, string password, decimal dataFieldId)
        {
            CustomDataFieldInfo customDataFieldInfo = null;

            ICustomDataFieldHandler customDataFieldHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDataFieldHandler>();
            if (ValidateUser(userName, password))
            {
                customDataFieldInfo = customDataFieldHandler.GetModelInfo(dataFieldId);
            }

            return customDataFieldInfo;
        }

        /// <summary>
        /// 根据条件获得字段对象列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetModelInfosByTableId(string userName, string password, decimal tableId, DataFieldFilter dataFieldFilter)
        {
            IList<CustomDataFieldInfo> customDataFieldInfos = null;

            ICustomDataFieldHandler customDataFieldHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDataFieldHandler>();
            if (ValidateUser(userName, password))
            {
                customDataFieldInfos = customDataFieldHandler.GetModelInfos(tableId, dataFieldFilter);
            }

            return customDataFieldInfos;
        }

        /// <summary>
        /// 根据数据编号获得表的列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodesByTableId(string userName, string password, decimal tableId, DataFieldFilter dataFieldFilter)
        {
            IList<CommonNode> commonNodes = null;

            ICustomDataFieldHandler customDataFieldHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDataFieldHandler>();
            if (ValidateUser(userName, password))
            {
                commonNodes = customDataFieldHandler.GetCommonNodes(tableId, dataFieldFilter);
            }

            return commonNodes;
        }

        /// <summary>
        /// 根据数据仓库编号获得数据库列表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDatabases(string userName, string password, byte dataWarehouseId)
        {
            IList<CommonNode> commonNodes = null;
            ICustomDatabaseHandler customDatabaseHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDatabaseHandler>();
            if (ValidateUser(userName, password))
            {
                commonNodes = customDatabaseHandler.GetChildNodes(dataWarehouseId);                
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得数据库对象
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public CommonNode GetDatabase(string userName, string password, decimal databaseId)
        {
            CommonNode commonNode = null;

            if (ValidateUser(userName, password))
            {
                ICustomDatabaseHandler customDatabaseHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDatabaseHandler>();
                commonNode = customDatabaseHandler.GetCommonNode(databaseId);
            }

            return commonNode;
        }

        /// <summary>
        /// 获得数据库名称
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public string GetDatabaseName(string userName, string password, decimal databaseId)
        {
            string databaseName = string.Empty;

            if (ValidateUser(userName, password))
            {
                ICustomDatabaseHandler customDatabaseHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDatabaseHandler>();
                databaseName = customDatabaseHandler.GetDatabaseName(databaseId);
            }

            return databaseName;
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="tableName"></param>
        /// <param name="dataTable"></param>
        public void Import(string userName, string userPwd, string tableName, DataTable dataTable)
        {
            if (ValidateUser(userName, userPwd))
            {
            }
        }

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="tableId"></param>
        /// <param name="dataFieldIds"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string userName, string userPwd, string tableName, IList<WhereConditon> whereConditons)
        {
            if (ValidateUser(userName, userPwd))
            {
                
            }

            return null;            
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateUser(string userName, string password)
        {
            bool result = false;

            string userNameConfirmed = systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.RemoteUserName);
            string passwordConfirmed = systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.RemotePassword);
            if (userName.Equals(userNameConfirmed) && password.Equals(passwordConfirmed))
            {
                result = true;
            }

            return result;
        }
 
        /// <summary>
        /// 获得分组信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public IList<CommonNode> GetGroupCommonNodes(string userName, string userPwd)
        {
            if (ValidateUser(userName, userPwd))
            {
               
            }

            return null;
        }

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="tableId"></param>
        /// <param name="dataFieldIds"></param>
        /// <param name="where"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public DataTable GetTableData(string userName, string userPwd, decimal tableId, IList<decimal> dataFieldIds, string where, IList<WhereConditon> whereConditons)
        {
            if (ValidateUser(userName, userPwd))
            {
                if (ValidateUser(userName, userPwd))
                {
                    ICustomTableHandler customTableHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomTableHandler>();
                    return customTableHandler.GetTableData(tableId, dataFieldIds, where, whereConditons);
                }
                
            }

            return null;            
        }

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>  
        /// <returns>数据集</returns>
        public DataSet GetUserData(string userName, string userPwd, int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            if (ValidateUser(userName, userPwd))
            {
                IUserAccountHandler userAccount = BusinessLogicContainer.Instance.UserModuleContainer.Resolve<IUserAccountHandler>();
                return userAccount.GetUserData(startPosition, count, whereConditons, ref totalCount);
            }

            return null;
        }

        /// <summary>
        /// 获得用户类型列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public IList<CommonNode> GetUserTypes(string userName, string userPwd)
        {
            if (ValidateUser(userName, userPwd))
            {
                IUserTypeHandler userType = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<IUserTypeHandler>();
                return userType.GetCommonNodes();
            }

            return null;
        }

        /// <summary>
        /// 获得用户数
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetUserCount(string userName, string userPwd, IList<WhereConditon> whereConditons)
        {
            if (ValidateUser(userName, userPwd))
            {
                IUserAccountHandler userAccount = BusinessLogicContainer.Instance.UserModuleContainer.Resolve<IUserAccountHandler>();
                return userAccount.GetUserCount(whereConditons);
            }

            return 0;
        }

        /// <summary>
        /// 获得用户照片
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public byte[] DownLoadPhoto(string userName, string userPwd, string user)
        {
            if (ValidateUser(userName, userPwd))
            {
                IUserAccountHandler userAccount = BusinessLogicContainer.Instance.UserModuleContainer.Resolve<IUserAccountHandler>();
                return userAccount.DownLoadPhoto(user);
            }

            return null;
        }

       
        #endregion
		
		#region 实现自定义接口
		
		#endregion
    }
}
