//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataController.cs
// 描述: 调用API获取自定义数据类
// 作者：ChenJie 
// 编写日期：2019-04-19
// 版权所有 (C) 四川大学 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using Blue.BusinessLogic.SystemModule;
using Blue.BusinessLogic.BusinessModule;
using Blue.BusinessLogic.UserModule;
using Blue.Model.SystemModule;
using Blue.Model.UserModule;
using Blue.Model.BusinessModule;

namespace Blue.WebAPI
{
    /// <summary>
    /// 调用API获取自定义数据类
    /// </summary>
    [RoutePrefix("Business")]
    [Authorize]
    public class DataController : ApiController
    {
        /// <summary>
        /// 最大1000条记录
        /// </summary>
        private const int MAX_PAGE_SIZE = 1000;

        #region 接口函数

        #region 获得数据结构接口函数

        /// <summary>
        /// 获得数据结构
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("DataStruct")]
        public IHttpActionResult GetDataStruct(string key)
        {
            List<UserDataField> userDataFields = new List<UserDataField>();

            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentException();
                }
                CustomInterfaceHandler customInterfaceHandler = new CustomInterfaceHandler();
                CustomInterfaceInfo customInterfaceInfo = customInterfaceHandler.GetModelInfo(key);
                if (customInterfaceInfo == null)
                {
                    throw new NullReferenceException();
                }
                if (!customInterfaceInfo.Actived)
                {
                    throw new NotImplementedException();
                }
                IList<ExtendedCustomDataFieldInfo> authorizedDataFieldInfos = GetAuthorizedDataFieldInfos(customInterfaceInfo);                
                foreach (var authorizedDataFieldInfo in authorizedDataFieldInfos)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)authorizedDataFieldInfo.DataFieldProperty;
                    DbType dataFieldType = DataFieldHelper.GetDbType(dataFieldProperty, authorizedDataFieldInfo.DataFieldType);
                    UserDataField userDataField = new UserDataField()
                    {
                        DataFieldName = authorizedDataFieldInfo.PhysicalName,
                        DataFieldCaption = authorizedDataFieldInfo.LogicalName,
                        DataFieldType = (byte)dataFieldType,
                        DataFieldLength = authorizedDataFieldInfo.DataFieldLength,
                        Notes = authorizedDataFieldInfo.Notes
                    };
                    userDataFields.Add(userDataField);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json <List<UserDataField>>(userDataFields);
        }

        #endregion

        #region 获得数据接口函数

        /// <summary>
        /// 获得用户业务数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("DataByUserId")]
        public IHttpActionResult GetDataByUserId(string key, decimal userId)
        {
            DataTable table = null;
            
            try
            {
                if (userId <= 0)
                {
                    throw new ArgumentException();
                }
                UserAccountHandler userAccountHandler = new UserAccountHandler();
                UserAccountInfo userAccountInfo = userAccountHandler.GetModelInfo(userId);
                if (userAccountInfo == null)
                {
                    throw new NullReferenceException();
                }
                CustomDepartmentHandler customDepartmentHandler = new CustomDepartmentHandler();
                if (!customDepartmentHandler.GetIsVisibleForInterface(userAccountInfo.DepId))
                {
                    throw new NullReferenceException();
                }
                UserTypeHandler userTypeHandler = new UserTypeHandler();
                if (!userTypeHandler.GetIsVisibleForInterface(userAccountInfo.UserTypeId))
                {
                    throw new NullReferenceException();
                }
                CustomInterfaceHandler customInterfaceHandler = new CustomInterfaceHandler();
                CustomInterfaceInfo customInterfaceInfo = customInterfaceHandler.GetModelInfo(key);
                if (customInterfaceInfo == null)
                {
                    throw new NullReferenceException();
                }
                if (!customInterfaceInfo.Actived)
                {
                    throw new NotImplementedException();
                }
                CustomTableHandler customTableHandler = new CustomTableHandler();
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                FormType formType = (FormType)customInterfaceInfo.TableType;
                switch (formType)
                {
                    case FormType.Table:
                        whereConditons.Add(new WhereConditon("UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal));
                        break;

                    case FormType.CombinedTable:
                        CombinedTableHandler combinedTableHandler = new CombinedTableHandler();
                        string tablePhysicalName = customTableHandler.GetTablePhysicalName(customInterfaceInfo.TableId);
                        whereConditons.Add(new WhereConditon(tablePhysicalName, "UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal));
                        break;

                    default:
                        throw new ArgumentException("不支持该类型。");
                }
                table = GetDataByCondition(key, whereConditons);                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<DataTable>(table);
        }

        /// <summary>
        /// 获得用户业务数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("DataByUserName")]
        public IHttpActionResult GetDataByUserName(string key, string userName)
        {
            DataTable table = null;

            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new ArgumentException();
                }
                UserAccountHandler userAccountHandler = new UserAccountHandler();
                UserAccountInfo userAccountInfo = userAccountHandler.GetModelInfo(userName);
                if (userAccountInfo == null)
                {
                    throw new NullReferenceException();
                }
                CustomDepartmentHandler customDepartmentHandler = new CustomDepartmentHandler();
                if (!customDepartmentHandler.GetIsVisibleForInterface(userAccountInfo.DepId))
                {
                    throw new NullReferenceException();
                }
                UserTypeHandler userTypeHandler = new UserTypeHandler();
                if (!userTypeHandler.GetIsVisibleForInterface(userAccountInfo.UserTypeId))
                {
                    throw new NullReferenceException();
                }
                CustomInterfaceHandler customInterfaceHandler = new CustomInterfaceHandler();
                CustomInterfaceInfo customInterfaceInfo = customInterfaceHandler.GetModelInfo(key);
                if (customInterfaceInfo == null)
                {
                    throw new NullReferenceException();
                }
                if (!customInterfaceInfo.Actived)
                {
                    throw new NotImplementedException();
                }
                CustomTableHandler customTableHandler = new CustomTableHandler();
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                FormType formType = (FormType)customInterfaceInfo.TableType;
                switch (formType)
                {
                    case FormType.Table:
                        whereConditons.Add(new WhereConditon("UserName", "UserName", DbType.String, userName, DataFieldCondition.Equal));
                        break;

                    case FormType.CombinedTable:
                        CombinedTableHandler combinedTableHandler = new CombinedTableHandler();
                        string tablePhysicalName = customTableHandler.GetTablePhysicalName(customInterfaceInfo.TableId);
                        whereConditons.Add(new WhereConditon(tablePhysicalName, "UserName", "UserName", DbType.String, userName, DataFieldCondition.Equal));
                        break;

                    default:
                        throw new ArgumentException("不支持该类型。");
                }
                table = GetDataByCondition(key, whereConditons);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<DataTable>(table);
        }

        /// <summary>
        /// 根据用户编号获得记录数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("CountByUserId")]
        public IHttpActionResult GetRecordCountById(string key, decimal userId)
        {
            int count = 0;

            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentException();
                }
                CustomInterfaceHandler customInterfaceHandler = new CustomInterfaceHandler();
                CustomInterfaceInfo customInterfaceInfo = customInterfaceHandler.GetModelInfo(key);
                if (customInterfaceInfo == null)
                {
                    throw new NullReferenceException();
                }
                if (!customInterfaceInfo.Actived)
                {
                    throw new NotImplementedException();
                }
                CustomTableHandler customTableHandler = new CustomTableHandler();
                IList<WhereConditon> whereConditons = new List<WhereConditon>();                
                FormType formType = (FormType)customInterfaceInfo.TableType;
                switch (formType)
                {
                    case FormType.Table:                        
                        whereConditons.Add(new WhereConditon("UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal));
                        count = customTableHandler.GetRecordCount(customInterfaceInfo.TableId, whereConditons);
                        break;

                    case FormType.CombinedTable:
                        CombinedTableHandler combinedTableHandler = new CombinedTableHandler();
                        string tablePhysicalName = customTableHandler.GetTablePhysicalName(customInterfaceInfo.TableId);
                        whereConditons.Add(new WhereConditon(tablePhysicalName, "UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal));
                        count = combinedTableHandler.GetRecordCount(customInterfaceInfo.TableId, whereConditons);
                        break;

                    default:
                        throw new ArgumentException("不支持该类型。");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            

            return Json<Int32>(count);
        }

        /// <summary>
        /// 根据用户编号获得记录数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("CountByUserName")]
        public IHttpActionResult GetRecordCountByUserName(string key, string userName)
        {
            int count = 0;

            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentException();
                }
                CustomInterfaceHandler customInterfaceHandler = new CustomInterfaceHandler();
                CustomInterfaceInfo customInterfaceInfo = customInterfaceHandler.GetModelInfo(key);
                if (customInterfaceInfo == null)
                {
                    throw new NullReferenceException();
                }
                if (!customInterfaceInfo.Actived)
                {
                    throw new NotImplementedException();
                }
                CustomTableHandler customTableHandler = new CustomTableHandler();
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                FormType formType = (FormType)customInterfaceInfo.TableType;
                switch (formType)
                {
                    case FormType.Table:
                        whereConditons.Add(new WhereConditon("UserName", "UserName", DbType.String, userName, DataFieldCondition.Equal));
                        count = customTableHandler.GetRecordCount(customInterfaceInfo.TableId, whereConditons);
                        break;

                    case FormType.CombinedTable:
                        CombinedTableHandler combinedTableHandler = new CombinedTableHandler();
                        string tablePhysicalName = customTableHandler.GetTablePhysicalName(customInterfaceInfo.TableId);
                        whereConditons.Add(new WhereConditon(tablePhysicalName, "UserName", "UserName", DbType.String, userName, DataFieldCondition.Equal));
                        count = combinedTableHandler.GetRecordCount(customInterfaceInfo.TableId, whereConditons);
                        break;

                    default:
                        throw new ArgumentException("不支持该类型。");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 获取完整列的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("Data")]
        public IHttpActionResult GetData(string key, int pos, int pageSize)
        {
            DataTable table = null;

            try
            {
                table = GetDataByCondition(key, pos, pageSize, DateTime.MinValue, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<DataTable>(table);
        }

        /// <summary>
        /// 获得记录数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("RecordCount")]
        public IHttpActionResult GetRecordCount(string key)
        {
            int count = 0;

            try
            {
                count = GetRecordCountByCondition(key, DateTime.MinValue, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 获取关键列的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("EssentialData")]
        public IHttpActionResult GetEssentialData(string key, int pos, int pageSize)
        {
            DataTable table = null;

            try
            {
                table = GetEssentialDataByCondition(key, pos, pageSize, DateTime.MinValue, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<DataTable>(table);
        }

        /// <summary>
        /// 获取完整列的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime">大于等于更新时间</param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("Data")]
        public IHttpActionResult GetData(string key, int pos, int pageSize, DateTime fromUpdatedTime)
        {
            DataTable table = null;

            try
            {
                table = GetDataByCondition(key, pos, pageSize, fromUpdatedTime, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<DataTable>(table);
        }

        /// <summary>
        /// 获得记录数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("RecordCount")]
        public IHttpActionResult GetRecordCount(string key, DateTime fromUpdatedTime)
        {
            int count = 0;

            try
            {
                count = GetRecordCountByCondition(key, fromUpdatedTime, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 获取关键列的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("EssentialData")]
        public IHttpActionResult GetEssentialData(string key, int pos, int pageSize, DateTime fromUpdatedTime)
        {
            DataTable table = null;

            try
            {
                table = GetEssentialDataByCondition(key, pos, pageSize, fromUpdatedTime, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<DataTable>(table);
        }

        /// <summary>
        /// 获取完整列的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime">大于等于更新时间</param>
        /// <param name="toUpdatedTime">小于等于更新时间</param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("Data")]
        public IHttpActionResult GetData(string key, int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            DataTable table = null;

            try
            {
                table = GetDataByCondition(key, pos, pageSize, fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<DataTable>(table);
        }

        /// <summary>
        /// 获得记录数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("RecordCount")]
        public IHttpActionResult GetRecordCount(string key, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            int count = 0;

            try
            {
                count = GetRecordCountByCondition(key, fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 获取关键列的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("EssentialData")]
        public IHttpActionResult GetEssentialData(string key, int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            DataTable table = null;

            try
            {                
                table = GetEssentialDataByCondition(key, pos, pageSize, fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<DataTable>(table);
        }

        #endregion

        #endregion

        #region 私有方法

        /// <summary>
        /// 根据条件获取完整列的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        private DataTable GetDataByCondition(string key, IList<WhereConditon> whereConditons)
        {
            DataTable table = null;

            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentException();
                }
                CustomInterfaceHandler customInterfaceHandler = new CustomInterfaceHandler();
                CustomInterfaceInfo customInterfaceInfo = customInterfaceHandler.GetModelInfo(key);
                if (customInterfaceInfo == null)
                {
                    throw new NullReferenceException();
                }
                if (!customInterfaceInfo.Actived)
                {
                    throw new NotImplementedException();
                }
                IList<ExtendedCustomDataFieldInfo> authorizedDataFieldInfos = GetAuthorizedDataFieldInfos(customInterfaceInfo);
                CustomDataFieldHandler customDataFieldHandler = new CustomDataFieldHandler();
                Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
                foreach (var extendedCustomDataFieldInfo in authorizedDataFieldInfos)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
                    string expressionText = string.Empty;
                    if (dataFieldProperty == DataFieldProperty.LogicalDataField)
                    {
                        LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                        if (logicalDataFieldType == LogicalDataFieldType.DigitExpression || logicalDataFieldType == LogicalDataFieldType.StringExpression
                            || logicalDataFieldType == LogicalDataFieldType.DateTimeExpression)
                        {
                            expressionText = customDataFieldHandler.GetDataFieldLogicalExpression(extendedCustomDataFieldInfo.DataFieldId);
                        }
                    }
                    dataFieldNameRelations.Add(extendedCustomDataFieldInfo.PhysicalName,
                            new CommonDataFieldInfo(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.TableId, extendedCustomDataFieldInfo.PhysicalName, extendedCustomDataFieldInfo.LogicalName,
                            expressionText, dataFieldProperty, extendedCustomDataFieldInfo.DataFieldType));
                }                
                IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("RecordId", CustomSorting.Ascending));
                DataSet ds = null;
                FormType formType = (FormType)customInterfaceInfo.TableType;
                switch (formType)
                {
                    case FormType.Table:
                        CustomTableHandler customTableHandler = new CustomTableHandler();
                        ds = customTableHandler.GetTableData(customInterfaceInfo.TableId, dataFieldNameRelations, 0, MAX_PAGE_SIZE, whereConditons, sortingCondtions);
                        break;

                    case FormType.CombinedTable:
                        CombinedTableHandler combinedTableHandler = new CombinedTableHandler();
                        ds = combinedTableHandler.GetTableData(customInterfaceInfo.CombinedTableId, dataFieldNameRelations, 0, MAX_PAGE_SIZE, whereConditons, sortingCondtions);
                        break;

                    default:
                        throw new ArgumentException("不支持该类型。");
                }
                table = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return table;
        }

        /// <summary>
        /// 获取完整列的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime">大于等于更新时间</param>
        /// <param name="toUpdatedTime">小于等于更新时间</param>
        /// <returns></returns>
        private DataTable GetDataByCondition(string key, int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            DataTable table = null;

            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentException();
                }
                CustomInterfaceHandler customInterfaceHandler = new CustomInterfaceHandler();
                CustomInterfaceInfo customInterfaceInfo = customInterfaceHandler.GetModelInfo(key);
                if (customInterfaceInfo == null)
                {
                    throw new NullReferenceException();
                }
                if (!customInterfaceInfo.Actived)
                {
                    throw new NotImplementedException();
                }
                IList<ExtendedCustomDataFieldInfo> authorizedDataFieldInfos = GetAuthorizedDataFieldInfos(customInterfaceInfo);
                CustomDataFieldHandler customDataFieldHandler = new CustomDataFieldHandler();
                Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
                foreach (var extendedCustomDataFieldInfo in authorizedDataFieldInfos)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
                    string expressionText = string.Empty;
                    if (dataFieldProperty == DataFieldProperty.LogicalDataField)
                    {
                        LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                        if (logicalDataFieldType == LogicalDataFieldType.DigitExpression || logicalDataFieldType == LogicalDataFieldType.StringExpression
                            || logicalDataFieldType == LogicalDataFieldType.DateTimeExpression)
                        {
                            expressionText = customDataFieldHandler.GetDataFieldLogicalExpression(extendedCustomDataFieldInfo.DataFieldId);
                        }
                    }
                    dataFieldNameRelations.Add(extendedCustomDataFieldInfo.PhysicalName,
                            new CommonDataFieldInfo(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.TableId, extendedCustomDataFieldInfo.PhysicalName, extendedCustomDataFieldInfo.LogicalName,
                            expressionText, dataFieldProperty, extendedCustomDataFieldInfo.DataFieldType));
                }
                IList<WhereConditon> whereConditons = GetWhereConditons(customInterfaceInfo, fromUpdatedTime, toUpdatedTime);
                IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("RecordId", CustomSorting.Ascending));
                DataSet ds = null;
                FormType formType = (FormType)customInterfaceInfo.TableType;
                switch (formType)
                {
                    case FormType.Table:
                        CustomTableHandler customTableHandler = new CustomTableHandler();                        
                        ds = customTableHandler.GetTableData(customInterfaceInfo.TableId, dataFieldNameRelations, pos, pageSize, whereConditons, sortingCondtions);
                        break;

                    case FormType.CombinedTable:
                        CombinedTableHandler combinedTableHandler = new CombinedTableHandler();
                        ds = combinedTableHandler.GetTableData(customInterfaceInfo.CombinedTableId, dataFieldNameRelations, pos, pageSize, whereConditons, sortingCondtions);
                        break;

                    default:
                        throw new ArgumentException("不支持该类型。");
                }
                table = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return table;
        }

        /// <summary>
        /// 获得记录数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        private int GetRecordCountByCondition(string key, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            int count = 0;

            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentException();
                }
                CustomInterfaceHandler customInterfaceHandler = new CustomInterfaceHandler();
                CustomInterfaceInfo customInterfaceInfo = customInterfaceHandler.GetModelInfo(key);
                if (customInterfaceInfo == null)
                {
                    throw new NullReferenceException();
                }
                if (!customInterfaceInfo.Actived)
                {
                    throw new NotImplementedException();
                }
                IList<WhereConditon> whereConditons = GetWhereConditons(customInterfaceInfo, fromUpdatedTime, toUpdatedTime);
                FormType formType = (FormType)customInterfaceInfo.TableType;
                switch (formType)
                {
                    case FormType.Table:
                        CustomTableHandler customTableHandler = new CustomTableHandler();
                        count = customTableHandler.GetRecordCount(customInterfaceInfo.TableId, whereConditons);
                        break;

                    case FormType.CombinedTable:
                        CombinedTableHandler combinedTableHandler = new CombinedTableHandler();
                        count = combinedTableHandler.GetRecordCount(customInterfaceInfo.TableId, whereConditons);
                        break;

                    default:
                        throw new ArgumentException("不支持该类型。");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return count;
        }

        /// <summary>
        /// 获得关键列数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        private DataTable GetEssentialDataByCondition(string key, int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            DataTable table = null;

            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    throw new ArgumentException();
                }
                CustomInterfaceHandler customInterfaceHandler = new CustomInterfaceHandler();
                CustomInterfaceInfo customInterfaceInfo = customInterfaceHandler.GetModelInfo(key);
                if (customInterfaceInfo == null)
                {
                    throw new NullReferenceException();
                }
                if (!customInterfaceInfo.Actived)
                {
                    throw new NotImplementedException();
                }
                IList<WhereConditon> whereConditons = GetWhereConditons(customInterfaceInfo, fromUpdatedTime, toUpdatedTime);
                IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("ModificationTime", CustomSorting.Descending));
                DataSet ds = null;
                FormType formType = (FormType)customInterfaceInfo.TableType;
                switch (formType)
                {
                    case FormType.Table:
                        CustomTableHandler customTableHandler = new CustomTableHandler();
                        ds = customTableHandler.GetTableData(customInterfaceInfo.TableId, null, pos, pageSize, whereConditons, sortingCondtions);
                        break;

                    case FormType.CombinedTable:
                        CombinedTableHandler combinedTableHandler = new CombinedTableHandler();
                        ds = combinedTableHandler.GetTableData(customInterfaceInfo.CombinedTableId, null, pos, pageSize, whereConditons, sortingCondtions);
                        break;

                    default:
                        throw new ArgumentException("不支持该类型。");
                }
                table = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return table;
        }

        /// <summary>
        /// 获得条件
        /// </summary>
        /// <param name="customInterfaceInfo"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        private IList<WhereConditon> GetWhereConditons(CustomInterfaceInfo customInterfaceInfo, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

            CustomInterfaceHandler customInterfaceHandler = new CustomInterfaceHandler();
            if (customInterfaceInfo.UserTypeContained)
            {
                IList<CommonNode> userTypeCommonNodes = customInterfaceHandler.GetUserTypes(customInterfaceInfo.InterfaceId);
                if (userTypeCommonNodes != null)
                {
                    for (int idx = 0; idx < userTypeCommonNodes.Count; idx++)
                    {
                        if (idx == 0)
                        {
                            if (userTypeCommonNodes.Count == 1)
                            {
                                whereConditons.Add(new WhereConditon("UserTypeId", string.Format("UserTypeId_{0}", idx), DbType.Decimal, userTypeCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                            }
                            else
                            {
                                whereConditons.Add(new WhereConditon("UserTypeId", string.Format("UserTypeId_{0}", idx), DbType.Decimal, userTypeCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                            }
                        }
                        else if (idx == userTypeCommonNodes.Count - 1)
                        {
                            whereConditons.Add(new WhereConditon("UserTypeId", string.Format("UserTypeId_{0}", idx), DbType.Decimal, userTypeCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                        }
                        else
                        {
                            whereConditons.Add(new WhereConditon("UserTypeId", string.Format("UserTypeId_{0}", idx), DbType.Decimal, userTypeCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.Or));
                        }
                    }
                }
            }
            //else
            //{
            //    whereConditons.Add(new WhereConditon("UserType", "IsVisibleForInterface", "IsVisibleForInterface_0", DbType.Boolean, true, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            //}
            if (customInterfaceInfo.DepContained)
            {
                IList<CommonNode> depCommonNodes = customInterfaceHandler.GetDepartments(customInterfaceInfo.InterfaceId);
                if (depCommonNodes != null && depCommonNodes.Count > 0)
                {
                    for (int idx = 0; idx < depCommonNodes.Count; idx++)
                    {
                        if (idx == 0)
                        {
                            if (depCommonNodes.Count == 1)
                            {
                                whereConditons.Add(new WhereConditon("DepId", string.Format("DepId_{0}", idx), DbType.Decimal, depCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                            }
                            else
                            {
                                whereConditons.Add(new WhereConditon("DepId", string.Format("DepId_{0}", idx), DbType.Decimal, depCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                            }
                        }
                        else if (idx == depCommonNodes.Count - 1)
                        {
                            whereConditons.Add(new WhereConditon("DepId", string.Format("DepId_{0}", idx), DbType.Decimal, depCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                        }
                        else
                        {
                            whereConditons.Add(new WhereConditon("DepId", string.Format("DepId_{0}", idx), DbType.Decimal, depCommonNodes[idx].NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.Or));
                        }
                    }
                }
            }
            //else
            //{
            //    whereConditons.Add(new WhereConditon("CustomDepartment", "IsVisibleForInterface", "IsVisibleForInterface_1", DbType.Boolean, true, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            //}
            if (!DataConvertionHelper.IsNullValue(fromUpdatedTime))
            {
                whereConditons.Add(new WhereConditon("ModificationTime", "ModificationTime_0", DbType.DateTime, fromUpdatedTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And));
            }
            if (!DataConvertionHelper.IsNullValue(toUpdatedTime))
            {
                whereConditons.Add(new WhereConditon("ModificationTime", "ModificationTime_1", DbType.DateTime, toUpdatedTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And));
            }

            return whereConditons;
        }

        /// <summary>
        /// 获得授权的字段列表
        /// </summary>
        /// <param name="customInterfaceInfo"></param>
        /// <returns></returns>
        private IList<ExtendedCustomDataFieldInfo> GetAuthorizedDataFieldInfos(CustomInterfaceInfo customInterfaceInfo)
        {            
            IList<ExtendedCustomDataFieldInfo> authorizedDataFieldInfos = null;

            try
            {
                decimal formId = decimal.MinValue;
                FormType formType = (FormType)customInterfaceInfo.TableType;
                switch (formType)
                {
                    case FormType.Table:
                        formId = customInterfaceInfo.TableId;
                        break;

                    case FormType.CombinedTable:
                        formId = customInterfaceInfo.CombinedTableId;
                        break;

                    default:
                        throw new ArgumentException("不支持该类型。");
                }

                CustomRoleHandler customRoleHandler = new CustomRoleHandler();
                IList<decimal> tableIds = new List<decimal>();
                switch (formType)
                {
                    case FormType.Table:
                        tableIds.Add(formId);
                        authorizedDataFieldInfos = customRoleHandler.GetAuthorizedExtendedCustomDataFieldInfos(customInterfaceInfo.UserId, tableIds, DataAuthorityType.Query);
                        break;

                    case FormType.CombinedTable:
                        authorizedDataFieldInfos = new List<ExtendedCustomDataFieldInfo>();
                        CombinedTableHandler combinedTableHandler = new CombinedTableHandler();
                        IList<CommonNode> commonNodeInfos = combinedTableHandler.GetTables(formId);
                        foreach (var commonNodeInfo in commonNodeInfos)
                        {
                            tableIds.Add(commonNodeInfo.NodeId);
                        }
                        List<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = customRoleHandler.GetAuthorizedExtendedCustomDataFieldInfos(customInterfaceInfo.UserId, tableIds, DataAuthorityType.Query);
                        List<CommonNode> combinedDataFields = combinedTableHandler.GetDataFields(formId);
                        foreach (var combinedDataField in combinedDataFields)
                        {
                            int pos = (extendedCustomDataFieldInfos.FindIndex(extendedCustomDataFieldInfo => extendedCustomDataFieldInfo.DataFieldId == combinedDataField.NodeId));
                            authorizedDataFieldInfos.Add(extendedCustomDataFieldInfos[pos]);
                        }
                        //foreach (var extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
                        //{
                        //    if ((combinedDataFields.FindIndex(dataField => dataField.NodeId == extendedCustomDataFieldInfo.DataFieldId) < 0))
                        //    {
                        //        continue;
                        //    }
                        //    authorizedDataFieldInfos.Add(extendedCustomDataFieldInfo);
                        //}
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return authorizedDataFieldInfos;
        }

        #endregion
    }
}
