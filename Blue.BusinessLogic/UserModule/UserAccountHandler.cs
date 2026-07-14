//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserAccountHandler.cs
// 描述：UserAccount 业务处理类
// 作者：ChenJie 
// 编写日期：2016/8/9
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.UserModule;
using Blue.IDAL.SystemModule;
using Blue.Model.UserModule;
using Blue.BusinessLogic.SystemModule;
using Blue.BusinessInterface.UserModule;
using Blue.BusinessInterface.SystemModule;

namespace Blue.BusinessLogic.UserModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.UserAccount.
    /// </summary>
    public class UserAccountHandler : IUserAccountHandler
    {
        #region 工厂类实例

        private static readonly IUserAccount dalUserAccount = UserDataAccessFactory.CreateUserAccount();
        private static readonly IRoleAndUser dalRoleAndUser = SystemDataAccessFactory.CreateRoleAndUser();
        private static readonly IDepartmentScope dalDepartmentScope = SystemDataAccessFactory.CreateDepartmentScope();
        private static readonly IUserTypeScope dalUserTypeScope = SystemDataAccessFactory.CreateUserTypeScope();

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserAccountHandler()
        {

        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 useraccount 表中插入一条新记录
        /// </summary>
        /// <param name="userAccountInfo"></param>
        /// <returns></returns>
        public decimal Insert(UserAccountInfo userAccountInfo)
        {
            //自动增加的关键字的值
            decimal userAccountId = 0;

            // 验证输入
            if (userAccountInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                userAccountId = dalUserAccount.Insert(userAccountInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userAccountId;
        }

        /// <summary>
        /// 获得 UserAccountInfo 对象
        /// </summary>
        ///<param name="userId">用户编号</param>
        /// <returns> UserAccountInfo 对象</returns>
        public UserAccountInfo GetModelInfo(decimal userId)
        {
            UserAccountInfo userAccountInfo = null;

            // 验证输入
            if (userId < 0)
            {
                return null;
            }

            try
            {
                userAccountInfo = dalUserAccount.GetModelInfo(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userAccountInfo;
        }

        /// <summary>
        /// 更新 UserAccountInfo 对象
        /// </summary>
        /// <param name="userAccountInfo">UserAccountInfo 对象</param>
        public void Update(UserAccountInfo userAccountInfo)
        {
            // 验证输入
            if (userAccountInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalUserAccount.Update(userAccountInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 UserAccountInfo 对象
        /// </summary>
        ///<param name="userId">用户编号</param>
        /// <returns> UserAccountInfo 对象</returns>
        public void Delete(decimal userId)
        {
            // 验证输入
            if (userId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalUserAccount.Delete(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 获得 UserAccountInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserAccountInfo 对象列表</returns>
        public IList<UserAccountInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<UserAccountInfo> userAccountInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                userAccountInfos = dalUserAccount.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userAccountInfos;
        }

        /// <summary>
        /// 获得 UserAccount 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserAccountInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalUserAccount.GetTotalCount(whereConditons);
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

        #region 实现新增接口

        /// <summary>
        /// 根据用户名或者证件号查用户编号
        /// </summary>
        /// <param name="key">用户名或者证件号</param>
        /// <returns>用户编号</returns>
        public decimal GetUserIdByKey(string key)
        {
            decimal userId = decimal.MinValue;

            try
            {
                userId = dalUserAccount.GetUserIdByKey(key);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userId;
        }

        /// <summary>
        /// 根据用户编号查用户编号
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户名</returns>
        public string GetUserNameByUserId(decimal userId)
        {
            string userName = string.Empty;

            try
            {
                userName = dalUserAccount.GetUserNameByUserId(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userName;
        }

        /// <summary>
        /// 获得用户数
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetUserCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                count = dalUserAccount.GetUserCount(whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得用户数量
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public int GetUserCount(DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            int count = 0;

            try
            {
                count = dalUserAccount.GetUserCount(fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得用户分页数据
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public DataTable GetUserData(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            DataTable dt = null;

            try
            {
                dt = dalUserAccount.GetUserData(pos, pageSize, fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dt;
        }

        /// <summary>
        /// 根据用户名查邮件地址
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>邮件地址</returns>
        public string GetEmailAddressByUserName(string userName)
        {
            string address = null;

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户编号不能为空。");
            }

            try
            {
                address = dalUserAccount.GetEmailAddressByUserName(userName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return address;
        }

        /// <summary>
        /// 查询用户邮件地址
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetEmailAddress(decimal userId)
        {
            string address = null;

            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能为空。");
            }

            try
            {
                address = dalUserAccount.GetEmailAddress(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return address;
        }

        /// <summary>
        /// 更新用户邮件地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="emailAddress"></param>
        public void UpdateEmailAddress(decimal userId, string emailAddress)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能为空。");
            }

            try
            {
                dalUserAccount.UpdateEmailAddress(userId, emailAddress);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

        }

        /// <summary>
        /// 根据用户名查用户名和用户实际名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户编号</returns>
        public StringPairValue GetUserNameInfoByUserId(decimal userId)
        {
            StringPairValue stringPairValue = null;

            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能为空。");
            }

            try
            {
                stringPairValue = dalUserAccount.GetUserNameInfoByUserId(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return stringPairValue;
        }

        /// <summary>
        /// 根据角色与用户所属单位条件查找用户
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetManagementUsersByUserId(decimal roleId, decimal userId)
        {
            Dictionary<decimal, string> managementUsers = null;

            // 验证输入
            if (roleId <= 0)
            {
                throw new ArgumentException("角色编号不能为空。");
            }
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能为空。");
            }

            try
            {
                managementUsers = dalUserAccount.GetManagementUsersByUserId(roleId, userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return managementUsers;
        }

        /// <summary>
        /// 根据角色与单位条件查找用户
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetManagementUsers(decimal roleId, decimal depId)
        {
            Dictionary<decimal, string> managementUsers = null;

            // 验证输入
            if (roleId <= 0)
            {
                throw new ArgumentException("角色编号不能为空。");
            }
            if (depId <= 0)
            {
                throw new ArgumentException("单位编号不能为空。");
            }

            try
            {
                managementUsers = dalUserAccount.GetManagementUsers(roleId, depId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return managementUsers;
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>如果提供的用户名和密码有效，则返回 true；否则返回 false</returns>
        public bool ValidateUser(string userName, string password)
        {
            bool validate = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }
            // 验证输入
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("密码不能为空。");
            }

            try
            {
                ValidationMode userValidationType = UserDataHelper.GetUserValidationType(userName);
                validate = dalUserAccount.ValidateUser(userName, password, userValidationType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return validate;
        }

        /// <summary>
        /// 获得字段权限
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Int64 GetDataFieldAuthority(string userName)
        {
            Int64 dataFieldAuthority = 0;

            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }

            try
            {
                dataFieldAuthority = dalUserAccount.GetDataFieldAuthority(userName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldAuthority;
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newPassword"></param>
        public void UpdatePassword(string userName, string newPassword)
        {
            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }
            // 验证输入
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("密码不能为空。");
            }

            try
            {
                dalUserAccount.UpdatePassword(userName, newPassword);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="imageData"></param>
        /// <param name="photoSuffixName"></param>
        public void Update(string userName, string emailAddress, string telephoneNumber, byte[] imageData, string photoSuffixName)
        {
            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }

            try
            {
                dalUserAccount.Update(userName, emailAddress, telephoneNumber, imageData, photoSuffixName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得用户照片路径
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetWebPhotoPath(string userName)
        {
            string filePath = string.Empty;

            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }
            try
            {
                filePath = dalUserAccount.GetWebPhotoPath(userName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return filePath;
        }

        /// <summary>
        /// 更新用户的单位编号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="depId"></param>
        public void UpdateDepIdByUserName(string userName, decimal depId)
        {
            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }

            try
            {
                dalUserAccount.UpdateDepIdByUserName(userName, depId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新用户的用户类型编号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userTypeId"></param>
        public void UpdateUserTypeIdByUserName(string userName, decimal userTypeId)
        {
            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }

            try
            {
                dalUserAccount.UpdateUserTypeIdByUserName(userName, userTypeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newUserName"></param>
        public void UpdateUserName(string userName, string newUserName)
        {
            // 验证输入
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(newUserName))
            {
                throw new ArgumentException("旧用户名或新用户名不能为空。");
            }

            try
            {
                dalUserAccount.UpdateUserName(userName, newUserName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userToolAction"></param>
        /// <param name="userName"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public void UpdateUserInfo(UserToolAction userToolAction, string userName, string userData)
        {
            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }

            try
            {
                dalUserAccount.UpdateUserInfo(userToolAction, userName, userData);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得最后登录时间
        /// </summary>
        /// <param name="userName"></param>
        public DateTime GetLastLogonTime(string userName)
        {
            DateTime dateTime = DateTime.Now;

            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }

            try
            {
                dateTime = dalUserAccount.GetLastLogonTime(userName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dateTime;
        }

        /// <summary>
        /// 清空重试次数
        /// </summary>
        /// <param name="userName"></param>
        public void ClearRetryTimes(string userName)
        {
            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }

            try
            {
                dalUserAccount.ClearRetryTimes(userName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得重试次数
        /// </summary>
        /// <param name="userName"></param>
        public int GetRetryTimes(string userName)
        {
            int retryTimes = 0;

            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }

            try
            {
                retryTimes = dalUserAccount.GetRetryTimes(userName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return retryTimes;
        }

        /// <summary>
        /// 更新 UserAccountInfo 对象，在导入数据中使用。
        /// </summary>
        /// <param name="userAccountInfo">UserAccountInfo 对象</param>
        public void UpdateUserAccountInfo(UserAccountInfo userAccountInfo)
        {
            if (userAccountInfo == null)
            {
                throw new ArgumentNullException("更新对象不能为空。");
            }

            try
            {
                dalUserAccount.UpdateUserAccountInfo(userAccountInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }       

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        public IList<int> Delete(Dictionary<int, string> userNames)
        {
            IList<int> failedRows = null;

            try
            {
                failedRows = dalUserAccount.Delete(userNames);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return failedRows;
        }

        /// <summary>
        /// 批量插入用户
        /// </summary>
        /// <param name="userAccountInfos"></param>
        /// <returns>插入失败的索引列表</returns>
        public IList<int> Insert(Dictionary<int, UserAccountInfo> userAccountInfos)
        {
            IList<int> failedRows = null;

            try
            {
                failedRows = dalUserAccount.Insert(userAccountInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return failedRows;
        }

        /// <summary>
        /// 查询密码
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserPassword(string userName)
        {
            string password = string.Empty;

            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }

            try
            {
                password = dalUserAccount.GetUserPassword(userName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return password;
        }

        /// <summary>
        /// 批量删除 UserAccountInfo 对象
        /// </summary>
        /// <param name="userIds">用户编号列表</param>
        public void Delete(IList<decimal> userIds)
        {
            try
            {
                dalUserAccount.Delete(userIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 锁定与解锁用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="locked"></param>
        public void LockUser(decimal userId, bool locked)
        {
            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于或是等于0。");
            }

            try
            {
                dalUserAccount.LockUser(userId, locked);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 锁定与解锁用户状态
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="locked"></param>
        public void LockUsers(IList<decimal> userIds, bool locked)
        {
            try
            {
                dalUserAccount.LockUsers(userIds, locked);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, CommonUserInfo> GetCommonUserInfos()
        {
            Dictionary<string, CommonUserInfo> commonUserInfos = null;

            try
            {
                commonUserInfos = dalUserAccount.GetCommonUserInfos();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonUserInfos;
        }

        /// <summary>
        /// 根据角色编号获取用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<CommonUserInfo> GetCommonUserInfos(decimal roleId)
        {
            IList<CommonUserInfo> commonUserInfos = new List<CommonUserInfo>();

            try
            {
                commonUserInfos = dalUserAccount.GetCommonUserInfos(roleId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonUserInfos;
        }

        /// <summary>
        /// 根据角色编号和单位编号获取用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public IList<CommonUserInfo> GetCommonUserInfos(decimal roleId, decimal depId)
        {
            IList<CommonUserInfo> commonUserInfos = new List<CommonUserInfo>();

            try
            {
                commonUserInfos = dalUserAccount.GetCommonUserInfos(roleId, depId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonUserInfos;
        }

        /// <summary>
        /// 根据用户编号查询管理的单位属性
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Int64 GetDepartmentAuthority(decimal userId)
        {
            Int64 departmentAuthority = 0;

            try
            {
                departmentAuthority = dalUserAccount.GetDepartmentAuthority(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return departmentAuthority;
        }

        /// <summary>
        /// 获得用户常用信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public CommonUserInfo GetCommonUserInfo(string userName)
        {
            CommonUserInfo commonUserInfo = null;

            try
            {
                commonUserInfo = dalUserAccount.GetCommonUserInfo(userName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonUserInfo;
        }

        /// <summary>
        /// 根据用户身份证号码查用户用户名
        /// </summary>
        /// <param name="userIdentity">用户身份证号码</param>
        /// <returns>用户名</returns>
        public string GetUserNameByUserIdentity(string userIdentity)
        {
            string userName = string.Empty;

            if (string.IsNullOrWhiteSpace(userIdentity))
            {
                throw new ArgumentException("身份证号码不能为空。");
            }

            try
            {
                userName = dalUserAccount.GetUserNameByUserIdentity(userIdentity);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userName;
        }

        /// <summary>
        /// 获得用户常用信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonUserInfo GetCommonUserInfo(decimal userId)
        {
            CommonUserInfo commonUserInfo = null;

            try
            {
                commonUserInfo = dalUserAccount.GetCommonUserInfo(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonUserInfo;
        }

        /// <summary>
        /// 根据用户名查用户编号
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户编号</returns>
        public decimal GetUserIdByUserName(string userName)
        {
            decimal userId = decimal.MinValue;

            try
            {
                // 验证输入
                if (string.IsNullOrWhiteSpace(userName))
                {
                    throw new ArgumentException("用户名不能为空。");
                }

                userId = dalUserAccount.GetUserIdByUserName(userName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userId;
        }

        /// <summary>
        /// 获得管理的用户类型
        /// </summary>
        /// <param name="userTypeScope"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<decimal> GetUserTypeIds(decimal userId)
        {
            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于或是等于0。");
            }

            IList<decimal> userTypeIds = dalUserTypeScope.GetSecondIds(userId);

            return userTypeIds;
        }

        /// <summary>
        /// 获得管理的用户类型
        /// </summary>
        /// <param name="userTypeScope"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetUserTypes(decimal userId)
        {
            IList<decimal> userTypeIds = dalUserTypeScope.GetSecondIds(userId);
            IUserTypeHandler userTypeHandler = new UserTypeHandler();
            IList<CommonNode> userTypes = userTypeHandler.GetCommonNodes(userTypeIds);

            return userTypes;
        }

        /// <summary>
        /// 获得管理的单位
        /// </summary>
        /// <param name="departmentScope"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<decimal> GetDepartmentIds(decimal userId)
        {
            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于或是等于0。");
            }

            IList<decimal> departmentIds = dalDepartmentScope.GetSecondIds(userId);

            return departmentIds;
        }

        /// <summary>
        /// 获得管理的单位
        /// </summary>
        /// <param name="departmentScope"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDepartments(decimal userId)
        {
            IList<decimal> departmentIds = dalDepartmentScope.GetSecondIds(userId);
            ICustomDepartmentHandler customDepartmentHandler = new CustomDepartmentHandler();
            IList<CommonNode> departments = customDepartmentHandler.GetCommonNodes(departmentIds);

            return departments;
        }


        /// <summary>
        /// 获得用户所拥有的角色
        /// </summary>
        /// <param name="roleAndUser"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<decimal> GetRoleIds(decimal userId)
        {
            IList<decimal> roleIds = dalRoleAndUser.GetSecondIds(userId);

            return roleIds;
        }

        /// <summary>
        /// 获得用户所拥有的角色
        /// </summary>
        /// <param name="roleAndUser"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRoles(decimal userId)
        {
            IList<decimal> roleIds = dalRoleAndUser.GetSecondIds(userId);
            ICustomRoleHandler customRoleHandler = new CustomRoleHandler();
            IList<CommonNode> roles = customRoleHandler.GetCommonNodes(roleIds);

            return roles;
        }

        /// <summary>
        /// 向 UserAccount 表中插入一条新记录
        /// </summary>
        /// <param name="userAccountInfo">userAccountInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        /// <param name="userTypeIds">管理的户类型列表</param>
        /// <param name="departmentIds">管理的单位列表</param>
        /// <param name="roleIds">角色列表</param>
        /// <returns></returns>        
        public decimal Insert(UserAccountInfo userAccountInfo, byte[] imageData, IList<decimal> userTypeIds, IList<decimal> departmentIds, IList<decimal> roleIds)
        {
            decimal userAccountId = 0;

            // 验证输入
            if (userAccountInfo == null)
            {
                throw new ArgumentException("不能插入空对象。");
            }

            if (imageData == null || imageData.Length == 0)
            {
                throw new ArgumentException("图片不能为空。");
            }

            try
            {
                userAccountId = dalUserAccount.Insert(userAccountInfo, imageData, userTypeIds, departmentIds, roleIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userAccountId;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newPassword"></param>
        /// <param name="userActualName"></param>
        /// <param name="EmailAddress"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="imageData"></param>
        /// <param name="photoSuffixName"></param>
        public void Update(string userName, string newPassword, string userActualName, string EmailAddress, string telephoneNumber, byte[] imageData, string photoSuffixName)
        {
            try
            {
                dalUserAccount.Update(userName, newPassword, userActualName, EmailAddress, telephoneNumber, imageData, photoSuffixName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 向 UserAccount 表中插入一条新记录
        /// </summary>
        /// <param name="userAccountInfo">userAccountInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        /// <param name="userTypeIds">管理的户类型列表</param>
        /// <param name="departmentIds">管理的单位列表</param>
        /// <param name="roleIds">角色列表</param>
        public void Update(UserAccountInfo userAccountInfo, byte[] imageData, IList<decimal> userTypeIds, IList<decimal> departmentIds, IList<decimal> roleIds)
        {
            // 验证输入
            if (userAccountInfo == null)
            {
                throw new ArgumentException("不能插入空对象。");
            }
            try
            {
                dalUserAccount.Update(userAccountInfo, imageData, userTypeIds, departmentIds, roleIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 UserAccountInfo 对象
        /// </summary>
        ///<param name="keyWord">关键字</param>
        /// <returns> UserAccountInfo 对象</returns>
        public UserAccountInfo GetModelInfo(string keyWord)
        {
            UserAccountInfo userAccountInfo = null;

            // 验证输入
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                return null;
            }

            try
            {
                ValidationMode validationMode = UserDataHelper.GetUserValidationType(keyWord);
                switch (validationMode)
                {
                    case ValidationMode.UserIdentity:
                        userAccountInfo = dalUserAccount.GetModeInfoByUserIdentity(keyWord);
                        break;

                    case ValidationMode.UserName:
                        userAccountInfo = dalUserAccount.GetModelInfo(keyWord);
                        break;
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userAccountInfo;
        }

        /// <summary>
        /// 根据用户类型编号查询用户数
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>

        public int GetUserCountByUserTypeId(decimal userTypeId)
        {
            int count = 0;

            // 验证输入
            if (userTypeId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                count = dalUserAccount.GetUserCountByUserTypeId(userTypeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 根据单位编号查询用户数
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public int GetUserCountByDepId(decimal depId)
        {
            int count = 0;

            // 验证输入
            if (depId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                count = dalUserAccount.GetUserCountByDepId(depId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>  
        /// <returns>数据集</returns>
        public DataSet GetUserList(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            // 验证输入
            if (startPosition < 0)
            {
                throw new ArgumentException("错误.");
            }
            try
            {
                ds = dalUserAccount.GetUserList(startPosition, count, whereConditons, ref totalCount);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>  
        /// <returns>数据集</returns>
        public DataSet GetUserData(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            // 验证输入
            if (startPosition < 0)
            {
                throw new ArgumentException("错误.");
            }
            try
            {
                ds = dalUserAccount.GetUserData(startPosition, count, whereConditons, ref totalCount);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        public DataSet GetUserInfos(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            // 验证输入
            if (startPosition < 0)
            {
                throw new ArgumentException("错误.");
            }
            try
            {
                ds = dalUserAccount.GetUserInfos(startPosition, count, whereConditons, ref totalCount);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得用户表中的用户名的分页数据集
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>   
        public DataSet GetUserNames(int startPosition, int count, IList<WhereConditon> whereConditons)
        {
            DataSet ds = null;

            // 验证输入
            if (startPosition < 0)
            {
                throw new ArgumentException("错误.");
            }
            try
            {
                ds = dalUserAccount.GetUserNames(startPosition, count, whereConditons);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            // 验证输入
            if (startPosition < 0)
            {
                throw new ArgumentException("错误.");
            }
            try
            {
                ds = dalUserAccount.GetPageRecordOfMultiTables(startPosition, count, whereConditons, ref totalCount);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        public DataSet GetPageRecordOfMultiTablesWithRole(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            // 验证输入
            if (startPosition < 0)
            {
                throw new ArgumentException("错误.");
            }
            try
            {
                ds = dalUserAccount.GetPageRecordOfMultiTablesWithRole(startPosition, count, whereConditons, ref totalCount);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 非系统用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsExistOrdinaryUserName(string userName)
        {
            bool result = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名错误.");
            }

            try
            {
                result = dalUserAccount.IsExistOrdinaryUserName(userName);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 用户名称是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>存在返回 true ,否则返回 false</returns>
        public bool IsExistUserName(string userName)
        {
            bool result = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名错误.");
            }

            try
            {
                result = dalUserAccount.IsExistUserName(userName);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 用户关键字的值是否存在
        /// </summary>
        /// <param name="validationMode"></param>
        /// <param name="keyValue"></param>
        /// <returns>存在返回 true ,否则返回 false</returns>
        public bool IsExistIdentity(ValidationMode validationMode, string keyValue)
        { 
            bool result = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                throw new ArgumentException("证件号码错误.");
            }

            try
            {
                result = dalUserAccount.IsExistIdentity(validationMode, keyValue);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 自动获取照片的后缀名
        /// 自动搜索几种常见图片格式的照片，如果存在，则返回该照片的后缀名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string AutoGetPhotoSuffixName(string userName)
        {
            string name = string.Empty;

            try
            {
                name = dalUserAccount.AutoGetPhotoSuffixName(userName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return name;
        }

        /// <summary>
        /// 下载图片
        /// 图片不存在，则自动搜索几种常见图片格式的照片，如果存在则返回该图片
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>图片数据</returns>
        public byte[] DownLoadPhoto(string userName)
        {
            byte[] photoData = null;

            try
            {
                photoData = dalUserAccount.DownLoadPhoto(userName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return photoData;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="photoSuffixName"></param>
        /// <param name="fileName"></param>
        /// <param name="imageData"></param>
        public void UpLoadPhoto(string userName, string photoSuffixName, string fileName, byte[] imageData)
        {
            // 验证输入
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(photoSuffixName) || string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("用户名、文件名或则后缀名不能为空。");
            }

            try
            {
                dalUserAccount.UpLoadPhoto(userName, photoSuffixName, fileName, imageData);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="imageData">图片数据</param>
        public void UpLoadPhoto(string fileName, byte[] imageData)
        {
            // 验证输入
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("文件名错误。");
            }

            try
            {
                dalUserAccount.UpLoadPhoto(fileName, imageData);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion

        #region 私有方法

        #endregion

        #endregion
    }
}
