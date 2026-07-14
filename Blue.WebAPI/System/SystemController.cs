//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ApiController.cs
// 描述: 调用API获取系统数据类
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
using Blue.Model.UserModule;
using Blue.Model.SystemModule;
using Blue.BusinessLogic.UserModule;
using Blue.BusinessLogic.SystemModule;

namespace Blue.WebAPI
{
    /// <summary>
    /// 调用API获取系统数据类
    /// </summary>
    [RoutePrefix("System")]
    [Authorize]
    public class SystemController : ApiController
    {
        #region 常量

        /// <summary>
        /// 每次获得的最大分页记录数
        /// </summary>
        private const int MAX_PAGE_SIZE = 1000;

        #endregion

        #region 接口函数

        #region 获得单个对象接口函数

        //[WebApiExceptionFilter]
        //[HttpGet]
        //[ActionName("TestActionName")]
        //public IHttpActionResult GetName(int b)
        //{
        //    int a = 0;

        //    return Json<int>(a);
        //}

        //[WebApiExceptionFilter]
        //[HttpGet]
        //[Route("Login/{b}")]
        //public IHttpActionResult Login(int b)
        //{
        //    int a = 0;

        //    return Json<int>(a);
        //}

        /// <summary>
        /// 获得用户对象
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserInfoById")]
        public IHttpActionResult GetUserInfoById(decimal userId)
        {
            UserInfo userInfo = new UserInfo();

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
                CommonUserInfo commonUserInfo = userAccountHandler.GetCommonUserInfo(userId);
                userInfo.UserName = userAccountInfo.UserName;
                userInfo.UserActualName = userAccountInfo.UserActualName;
                userInfo.UserId = userAccountInfo.UserId;
                userInfo.DepId = userAccountInfo.DepId;
                userInfo.DepName = commonUserInfo.DepName;
                userInfo.UserTypeId = userAccountInfo.UserTypeId;
                userInfo.UserTypeName = commonUserInfo.UserTypeName;
                userInfo.EmailAddress = userAccountInfo.EmailAddress;
                userInfo.IdentificationType = userAccountInfo.IdentificationType;
                userInfo.UserIdentity = userAccountInfo.UserIdentity;
                userInfo.TelephoneNumber = userAccountInfo.TelephoneNumber;
                userInfo.LockedOut = userAccountInfo.LockedOut;
                userInfo.UniqueUserIdentity = userAccountInfo.UniqueUserIdentity;
                userInfo.CreatedTime = userAccountInfo.CreatedTime;
                userInfo.UpdatedTime = userAccountInfo.UpdatedTime;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return Json<UserInfo>(userInfo);
        }

        /// <summary>
        /// 获得用户对象
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserInfoByName")]
        public IHttpActionResult GetUserInfoByName(string userName)
        {
            UserInfo userInfo = new UserInfo();

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
                CommonUserInfo commonUserInfo = userAccountHandler.GetCommonUserInfo(userName);
                userInfo.UserId = userAccountInfo.UserId;
                userInfo.UserName = userAccountInfo.UserName;
                userInfo.UserActualName = userAccountInfo.UserActualName;
                userInfo.DepId = userAccountInfo.DepId;
                userInfo.DepName = commonUserInfo.DepName;
                userInfo.UserTypeId = userAccountInfo.UserTypeId;
                userInfo.UserTypeName = commonUserInfo.UserTypeName;
                userInfo.EmailAddress = userAccountInfo.EmailAddress;
                userInfo.IdentificationType = userAccountInfo.IdentificationType;
                userInfo.UserIdentity = userAccountInfo.UserIdentity;
                userInfo.TelephoneNumber = userAccountInfo.TelephoneNumber;
                userInfo.LockedOut = userAccountInfo.LockedOut;
                userInfo.UniqueUserIdentity = userAccountInfo.UniqueUserIdentity;
                userInfo.CreatedTime = userAccountInfo.CreatedTime;
                userInfo.UpdatedTime = userAccountInfo.UpdatedTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<UserInfo>(userInfo);
        }

        /// <summary>
        /// 获得单位对象
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("DepartmentInfo")]
        public IHttpActionResult GetDepartmentInfo(decimal depId)
        {
            DepartmentInfo departmentInfo = new DepartmentInfo();

            try
            {
                if (depId <= 0)
                {
                    throw new ArgumentException();
                }
                CustomDepartmentHandler customDepartmentHandler = new CustomDepartmentHandler();
                CustomDepartmentInfo customDepartmentInfo = customDepartmentHandler.GetModelInfo(depId);

                if (customDepartmentInfo == null)
                {
                    throw new NullReferenceException();
                }
                if (!customDepartmentInfo.IsVisibleForInterface)
                {
                    throw new NullReferenceException();
                }
                departmentInfo.DepId = customDepartmentInfo.DepId;
                departmentInfo.DepName = customDepartmentInfo.DepName;
                departmentInfo.DepCode = customDepartmentInfo.DepCode;
                departmentInfo.FirstCode = customDepartmentInfo.FirstCode;
                departmentInfo.SecondCode = customDepartmentInfo.SecondCode;
                departmentInfo.CreatedTime = customDepartmentInfo.CreatedTime;
                departmentInfo.UpdatedTime = customDepartmentInfo.UpdatedTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<DepartmentInfo>(departmentInfo);
        }

        /// <summary>
        /// 获得用户类型对象
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserTypeInfo")]
        public IHttpActionResult GetUserTypeInfo(decimal userTypeId)
        {
            CurrentUserTypeInfo currentUserTypeInfo = new CurrentUserTypeInfo();

            try
            {
                if (userTypeId <= 0)
                {
                    throw new ArgumentException();
                }
                UserTypeHandler userTypeHandler = new UserTypeHandler();
                UserTypeInfo userTypeInfo = userTypeHandler.GetModelInfo(userTypeId);
                if (userTypeInfo == null)
                {
                    throw new NullReferenceException();
                }
                if (!userTypeInfo.IsVisibleForInterface)
                {
                    throw new NullReferenceException();
                }
                currentUserTypeInfo.UserTypeId = userTypeInfo.UserTypeId;
                currentUserTypeInfo.UserTypeName = userTypeInfo.UserTypeName;
                currentUserTypeInfo.UserTypeCode = userTypeInfo.UserTypeCode;
                currentUserTypeInfo.CreatedTime = userTypeInfo.CreatedTime;
                currentUserTypeInfo.UpdatedTime = userTypeInfo.UpdatedTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<CurrentUserTypeInfo>(currentUserTypeInfo);
        }

        #endregion

        #region 批量获得用户接口函数

        /// <summary>
        /// 获得用户记录数
        /// </summary>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserCount")]
        public IHttpActionResult GetUserCount()
        {
            int count = 0;

            try
            {
                UserAccountHandler userAccountHandler = new UserAccountHandler();
                count = userAccountHandler.GetUserCount(DateTime.MinValue, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 批量获得用户对象
        /// </summary>
        /// <param name="pos">从0开始</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserInfos")]
        public IHttpActionResult GetUserInfos(int pos, int pageSize)
        {
            List<UserInfo> userInfos = null;

            try
            {
                if (pos < 0 || pageSize <= 0 || pageSize > MAX_PAGE_SIZE)
                {
                    throw new ArgumentException();
                }
                userInfos = GetUserInfosByCondition(pos, pageSize, DateTime.MinValue, DateTime.MaxValue);            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<List<UserInfo>>(userInfos);
        }

        /// <summary>
        /// 获得用户记录数
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserCount")]
        public IHttpActionResult GetUserCount(DateTime fromUpdatedTime)
        {
            int count = 0;

            try
            {
                if (DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    throw new ArgumentException();
                }
                UserAccountHandler userAccountHandler = new UserAccountHandler();
                count = userAccountHandler.GetUserCount(fromUpdatedTime, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 批量获得用户对象
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserInfos")]
        public IHttpActionResult GetUserInfos(int pos, int pageSize, DateTime fromUpdatedTime)
        {
            List<UserInfo> userInfos = null;

            try
            {
                if (pos < 0 || pageSize <= 0 || pageSize > MAX_PAGE_SIZE || DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    throw new ArgumentException();
                }
                userInfos = GetUserInfosByCondition(pos, pageSize, fromUpdatedTime, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<List<UserInfo>>(userInfos);
        }

        /// <summary>
        /// 获得用户记录数
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserCount")]
        public IHttpActionResult GetUserCount(DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            int count = 0;

            try
            {
                if (DataConvertionHelper.IsNullValue(fromUpdatedTime) || DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    throw new ArgumentException();
                }
                UserAccountHandler userAccountHandler = new UserAccountHandler();
                count = userAccountHandler.GetUserCount(fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 批量获得用户对象
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserInfos")]
        public IHttpActionResult GetUserInfos(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            List<UserInfo> userInfos = null;

            try
            {
                if (pos < 0 || pageSize <= 0 || pageSize > MAX_PAGE_SIZE || DataConvertionHelper.IsNullValue(fromUpdatedTime) || DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    throw new ArgumentException();
                }
                userInfos = GetUserInfosByCondition(pos, pageSize, fromUpdatedTime, toUpdatedTime);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<List<UserInfo>>(userInfos);
        }

        #region 私有方法

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        private List<UserInfo> GetUserInfosByCondition(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            List<UserInfo> userInfos = new List<UserInfo>(pageSize > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : pageSize);

            try
            {
                UserAccountHandler userAccountHandler = new UserAccountHandler();
                DataTable dt = userAccountHandler.GetUserData(pos, pageSize, fromUpdatedTime, toUpdatedTime);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UserInfo userInfo = new UserInfo()
                        {
                            UserId = DataConvertionHelper.GetDecimal(dr["UserId"]),
                            UserName = DataConvertionHelper.GetString(dr["UserName"]),
                            UserActualName = DataConvertionHelper.GetString(dr["UserActualName"]),
                            DepId = DataConvertionHelper.GetDecimal(dr["DepId"]),
                            DepName = DataConvertionHelper.GetString(dr["DepName"]),
                            UserTypeId = DataConvertionHelper.GetDecimal(dr["UserTypeId"]),
                            UserTypeName = DataConvertionHelper.GetString(dr["UserTypeName"]),
                            EmailAddress = DataConvertionHelper.GetString(dr["EmailAddress"]),
                            IdentificationType = DataConvertionHelper.GetByte(dr["IdentificationType"]),
                            UserIdentity = DataConvertionHelper.GetString(dr["UserIdentity"]),
                            TelephoneNumber = DataConvertionHelper.GetString(dr["TelephoneNumber"]),
                            UniqueUserIdentity = (Guid)dr["UniqueUserIdentity"],
                            CreatedTime = DataConvertionHelper.GetDateTime(dr["CreatedTime"]),
                            UpdatedTime = DataConvertionHelper.GetDateTime(dr["UpdatedTime"])
                        };
                        userInfos.Add(userInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userInfos;
        }

        #endregion

        #endregion

        #region 批量获得单位接口函数

        /// <summary>
        /// 获得单位记录数
        /// </summary>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("DepartmentCount")]
        public IHttpActionResult GetDepartmentCount()
        {
            int count = 0;

            try
            {
                CustomDepartmentHandler customDepartmentHandler = new CustomDepartmentHandler();
                count = customDepartmentHandler.GetDepartmentCount(DateTime.MinValue, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 批量获得单位对象
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("DepartmentInfos")]
        public IHttpActionResult GetDepartmentInfos(int pos, int pageSize)
        {
            List<DepartmentInfo> departmentInfos = null;

            try
            {
                if (pos < 0 || pageSize <= 0 || pageSize > MAX_PAGE_SIZE)
                {
                    throw new ArgumentException();
                }
                departmentInfos = GetDepartmentsByCondition(pos, pageSize, DateTime.MinValue, DateTime.MaxValue);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<List<DepartmentInfo>>(departmentInfos);
        }

        /// <summary>
        /// 获得单位记录数
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("DepartmentCount")]
        public IHttpActionResult GetDepartmentCount(DateTime fromUpdatedTime)
        {
            int count = 0;

            try
            {
                if (DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    throw new ArgumentException();
                }
                CustomDepartmentHandler customDepartmentHandler = new CustomDepartmentHandler();
                count = customDepartmentHandler.GetDepartmentCount(fromUpdatedTime, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 批量获得单位对象
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("DepartmentInfos")]
        public IHttpActionResult GetDepartmentInfos(int pos, int pageSize, DateTime fromUpdatedTime)
        {
            List<DepartmentInfo> departmentInfos = null;

            try
            {
                if (pos < 0 || pageSize <= 0 || pageSize > MAX_PAGE_SIZE || DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    throw new ArgumentException();
                }
                departmentInfos = GetDepartmentsByCondition(pos, pageSize, fromUpdatedTime, DateTime.MaxValue);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<List<DepartmentInfo>>(departmentInfos);
        }

        /// <summary>
        /// 获得单位记录数
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("DepartmentCount")]
        public IHttpActionResult GetDepartmentCount(DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            int count = 0;

            try
            {
                if (DataConvertionHelper.IsNullValue(fromUpdatedTime) || DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    throw new ArgumentException();
                }
                CustomDepartmentHandler customDepartmentHandler = new CustomDepartmentHandler();
                count = customDepartmentHandler.GetDepartmentCount(fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 批量获得单位对象
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("DepartmentInfos")]
        public IHttpActionResult GetDepartmentInfos(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            List<DepartmentInfo> departmentInfos = null;

            try
            {
                if (pos < 0 || pageSize <= 0 || pageSize > MAX_PAGE_SIZE || DataConvertionHelper.IsNullValue(fromUpdatedTime) || DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    throw new ArgumentException();
                }
                departmentInfos = GetDepartmentsByCondition(pos, pageSize, fromUpdatedTime, toUpdatedTime);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return Json<List<DepartmentInfo>>(departmentInfos);
        }
                 
        #region 私有方法

        /// <summary>
        /// 获得单位列表
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        private List<DepartmentInfo> GetDepartmentsByCondition(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            List<DepartmentInfo> departmentInfos = new List<DepartmentInfo>(pageSize > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : pageSize);

            try
            {
                CustomDepartmentHandler customDepartmentHandler = new CustomDepartmentHandler();
                DataTable dt = customDepartmentHandler.GetDepartmentData(pos, pageSize, fromUpdatedTime, toUpdatedTime);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DepartmentInfo departmentInfo = new DepartmentInfo()
                        {
                            DepId = DataConvertionHelper.GetDecimal(dr["DepId"]),
                            DepName = DataConvertionHelper.GetString(dr["DepName"]),
                            DepCode = DataConvertionHelper.GetString(dr["DepCode"]),
                            FirstCode = DataConvertionHelper.GetString(dr["FirstCode"]),
                            SecondCode = DataConvertionHelper.GetString(dr["SecondCode"]),
                            CreatedTime = DataConvertionHelper.GetDateTime(dr["CreatedTime"]),
                            UpdatedTime = DataConvertionHelper.GetDateTime(dr["UpdatedTime"])
                        };
                        departmentInfos.Add(departmentInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return departmentInfos;
        }

        #endregion

        #endregion

        #region 批量获得用户类型接口函数

        /// <summary>
        /// 获得用户类型记录数
        /// </summary>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserTypeCount")]
        public IHttpActionResult GetUserTypeCount()
        {
            int count = 0;

            try
            {                
                UserTypeHandler userTypeHandler = new UserTypeHandler();
                count = userTypeHandler.GetUserTypeCount(DateTime.MinValue, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 批量获得用户类型对象
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserTypeInfos")]
        public IHttpActionResult GetUserTypeInfos(int pos, int pageSize)
        {
            List<UserTypeInfo> userTypeInfos = null;

            try
            {
                if (pos < 0 || pageSize <= 0 || pageSize > MAX_PAGE_SIZE)
                {
                    throw new ArgumentException();
                }
                userTypeInfos = GetUserTypeInfosByCondition(pos, pageSize, DateTime.MinValue, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<List<UserTypeInfo>>(userTypeInfos);
        }

        /// <summary>
        /// 获得用户类型记录数
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserTypeCount")]
        public IHttpActionResult GetUserTypeCount(DateTime fromUpdatedTime)
        {
            int count = 0;

            try
            {
                if (DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    throw new ArgumentException();
                }
                UserTypeHandler userTypeHandler = new UserTypeHandler();
                count = userTypeHandler.GetUserTypeCount(fromUpdatedTime, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 批量获得用户类型对象
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserTypeInfos")]
        public IHttpActionResult GetUserTypeInfos(int pos, int pageSize, DateTime fromUpdatedTime)
        {
            List<UserTypeInfo> userTypeInfos = null;

            try
            {
                if (pos < 0 || pageSize <= 0 || pageSize > MAX_PAGE_SIZE || DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    throw new ArgumentException();
                }
                userTypeInfos = GetUserTypeInfosByCondition(pos, pageSize, fromUpdatedTime, DateTime.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<List<UserTypeInfo>>(userTypeInfos);
        }

        /// <summary>
        /// 获得用户类型记录数
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserTypeCount")]
        public IHttpActionResult GetUserTypeCount(DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            int count = 0;

            try
            {
                if (DataConvertionHelper.IsNullValue(fromUpdatedTime) || DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    throw new ArgumentException();
                }
                UserTypeHandler userTypeHandler = new UserTypeHandler();
                count = userTypeHandler.GetUserTypeCount(fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<Int32>(count);
        }

        /// <summary>
        /// 批量获得用户类型对象
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("UserTypeInfos")]
        public IHttpActionResult GetUserTypeInfos(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            List<UserTypeInfo> userTypeInfos = null;

            try
            {
                if (pos < 0 || pageSize <= 0 || pageSize > MAX_PAGE_SIZE || DataConvertionHelper.IsNullValue(fromUpdatedTime) || DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    throw new ArgumentException();
                }
                userTypeInfos = GetUserTypeInfosByCondition(pos, pageSize, fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<List<UserTypeInfo>>(userTypeInfos);
        }    

        #region 私有方法

        /// <summary>
        /// 获得单位列表
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        private List<UserTypeInfo> GetUserTypeInfosByCondition(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            List<UserTypeInfo> userTypeInfos = new List<UserTypeInfo>(pageSize > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : pageSize);

            try
            {
                UserTypeHandler userTypeHandler = new UserTypeHandler();
                DataTable dt = userTypeHandler.GetUserTypeData(pos, pageSize, fromUpdatedTime, toUpdatedTime);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UserTypeInfo userTypeInfo = new UserTypeInfo()
                        {
                            UserTypeId = DataConvertionHelper.GetDecimal(dr["UserTypeId"]),
                            UserTypeName = DataConvertionHelper.GetString(dr["UserTypeName"]),
                            UserTypeCode = DataConvertionHelper.GetString(dr["UserTypeCode"]),
                            CreatedTime = DataConvertionHelper.GetDateTime(dr["CreatedTime"]),
                            UpdatedTime = DataConvertionHelper.GetDateTime(dr["UpdatedTime"])
                        };
                        userTypeInfos.Add(userTypeInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userTypeInfos;
        }

        #endregion

        #endregion

        #endregion
    }
}
