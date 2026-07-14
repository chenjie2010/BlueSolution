//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomDataContract.cs
// 描述: CustomData 契约层接口
// 作者：ChenJie 
// 编写日期：2012/1/7
// Copyright 2012
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts
{
    /// <summary>
    /// CustomData 契约接口
    /// </summary>
    [ServiceContract(Name = "IRemoteServerContract", Namespace = "http://www.scu.edu.cn/DataModule/")]
    public interface IRemoteServerContract
    {
        /// <summary>
        /// 获得用户名标签
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetUserNameLabelInfo")]
        string GetUserNameLabelInfo();

        /// <summary>
        /// 获得指定的字段的附件
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="dataFieldName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetFileData")]
        byte[] GetFileData(string userName, string password, string dataFieldName, string fileName);

        /// <summary>
        /// 测试用户名密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract(Name = "TestRemoteConnection")]
        bool TestRemoteConnection(string userName, string password);

        /// <summary>
        /// 获得表达式字段名称
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="tablePhysicalName"></param>
        /// <param name="expressionText"></param>
        /// <param name="commonNodes"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetExpressionDataFieldName")]
        string GetExpressionDataFieldName(string userName, string password, string tablePhysicalName, string expressionText, IList<CommonNode> commonNodes);

        /// <summary>
        /// 获得表达式字段
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetExpressionCommonNodes")]
        IList<CommonNode> GetExpressionCommonNodes(string userName, string password, decimal dataFieldId);

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
        [OperationContract(Name = "GetPageRecord")]
        DataSet GetPageRecord(string userName, string password, decimal tableId, IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos, int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions);

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="tableId"></param>
        /// <param name="where"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTableRecordCountByCondition")]
        int GetRecordCount(string userName, string password, decimal tableId, string where, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTableRecordCount")]
        int GetTableRecordCount(string userName, string password, decimal tableId, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得字段对象
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfoByDataFieldId")]
        CustomDataFieldInfo GetModelInfoByDataFieldId(string userName, string password, decimal dataFieldId);

        /// <summary>
        /// 根据条件获得字段对象列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfosByTableId")]
        IList<CustomDataFieldInfo> GetModelInfosByTableId(string userName, string password, decimal tableId, DataFieldFilter dataFieldFilter);

        /// <summary>
        /// 根据数据编号获得表的列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonNodesByTableId")]
        IList<CommonNode> GetCommonNodesByTableId(string userName, string password, decimal tableId, DataFieldFilter dataFieldFilter);

        /// <summary>
        /// 获得表的编号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTableId")]
        decimal GetTableId(string userName, string password, decimal dataFieldId);

        /// <summary>
        /// 根据数据编号获得表的列表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonNodesByDatabaseId")]
        IList<CommonNode> GetCommonNodesByDatabaseId(string userName, string password, decimal sourceDatabaseId);

        /// <summary>
        /// 根据数据仓库编号获得数据库列表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDatabases")]
        IList<CommonNode> GetDatabases(string userName, string password, byte dataWarehouseId);

        /// <summary>
        /// 根据分类编号获得表的列表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTablesByCategoryId")]
        IList<CommonNode> GetTablesByCategoryId(string userName, string password, decimal categoryId);

        /// <summary>
        /// 根据数据库编号获得分类的列表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCategoriesByDatabaseId")]
        IList<CommonNode> GetCategoriesByDatabaseId(string userName, string password, decimal databaseId);
        
        /// <summary>
        /// 获得数据库名称
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDatabase")]
        CommonNode GetDatabase(string userName, string password, decimal databaseId);

        /// <summary>
        /// 获得数据库名称
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDatabaseName")]
        string GetDatabaseName(string userName, string password, decimal databaseId);

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="tableName"></param>
        /// <param name="dataTable"></param>
        [OperationContract(Name = "Import")]
        void Import(string userName, string userPwd, string tableName, DataTable dataTable);

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="tableId"></param>
        /// <param name="dataFieldIds"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataTableByTableName")]
        DataTable GetDataTable(string userName, string userPwd, string tableName, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得用户照片
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract(Name = "DownLoadPhoto")]
        byte[] DownLoadPhoto(string userName, string userPwd, string user);

         /// <summary>
        /// 获得用户数
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetUserCount")]
        int GetUserCount(string userName, string userPwd, IList<WhereConditon> whereConditons);
        
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
        [OperationContract(Name = "GetUserData")]
        DataSet GetUserData(string userName, string userPwd, int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        [OperationContract(Name = "ValidateUser")]
        bool ValidateUser(string userName, string userPwd);

        /// <summary>
        /// 获得分组信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetGroupCommonNodes")]
        IList<CommonNode> GetGroupCommonNodes(string userName, string userPwd);

        /// <summary>
        /// 获得用户类型列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetUserTypes")]
        IList<CommonNode> GetUserTypes(string userName, string userPwd);

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="tableId"></param>
        /// <param name="dataFieldIds"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>

        [OperationContract(Name = "GetTableData")]
        DataTable GetTableData(string userName, string userPwd, decimal tableId, IList<decimal> dataFieldIds, string where, IList<WhereConditon> whereConditons);

    }
}
