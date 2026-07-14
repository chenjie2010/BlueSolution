//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserAccountService.cs
// 描述: UserAccount 操作服务类
// 作者：ChenJie 
// 编写日期：2016/7/25
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.UserModule;
using Blue.BusinessInterface.UserModule;
using Blue.WCFContracts.UserModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.UserModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.UserAccount.
    /// </summary>
    public class UserAccountService : IUserAccountContract
    {
        #region 业务实例

        private readonly IUserAccountHandler userAccount = BusinessLogicContainer.Instance.UserModuleContainer.Resolve<IUserAccountHandler>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserAccountService()
        {

        }

        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 useraccount 表中插入一条新记录
        /// </summary>
        /// <param name="userAccountInfo"></param>
        /// <returns></returns>
        public decimal Insert(UserAccountInfo userAccountInfo)
        {
            return userAccount.Insert(userAccountInfo);
        }

        /// <summary>
        /// 获得 UserAccountInfo 对象
        /// </summary>
        ///<param name="userId">用户编号</param>
        /// <returns> UserAccountInfo 对象</returns>
        public UserAccountInfo GetModelInfo(decimal userId)
        {
            return userAccount.GetModelInfo(userId);
        }

        /// <summary>
        /// 更新 UserAccountInfo 对象
        /// </summary>
        /// <param name="userAccountInfo">UserAccountInfo 对象</param>
        public void Update(UserAccountInfo userAccountInfo)
        {
            userAccount.Update(userAccountInfo);
        }

        /// <summary>
        /// 删除 UserAccountInfo 对象
        /// </summary>
        ///<param name="userId">用户编号</param>
        /// <returns> UserAccountInfo 对象</returns>
        public void Delete(decimal userId)
        {
            userAccount.Delete(userId);
        }

        /// <summary>
        /// 获得 UserAccountInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserAccountInfo 对象列表</returns>
        public IList<UserAccountInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return userAccount.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 UserAccount 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserAccountInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return userAccount.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得用户数
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetUserCount(IList<WhereConditon> whereConditons)
        {
            return userAccount.GetUserCount(whereConditons);
        }

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, CommonUserInfo> GetCommonUserInfos()
        {
            return userAccount.GetCommonUserInfos();
        }

        /// <summary>
        /// 根据用户名查邮件地址
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>邮件地址</returns>
        public string GetEmailAddressByUserName(string userName)
        {
            return userAccount.GetEmailAddressByUserName(userName);
        }

        /// <summary>
        /// 查询用户邮件地址
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetEmailAddress(decimal userId)
        {
            return userAccount.GetEmailAddress(userId);
        }

        /// <summary>
        /// 更新用户邮件地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="emailAddress"></param>
        public void UpdateEmailAddress(decimal userId, string emailAddress)
        {
            userAccount.UpdateEmailAddress(userId, emailAddress);
        }

        /// <summary>
        /// 根据用户名查用户名和用户实际名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户编号</returns>
        public StringPairValue GetUserNameInfoByUserId(decimal userId)
        {
            return userAccount.GetUserNameInfoByUserId(userId);
        }

        /// <summary>
        /// 根据角色与用户所属单位条件查找用户
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetManagementUsersByUserId(decimal roleId, decimal userId)
        {
            return userAccount.GetManagementUsersByUserId(roleId, userId);
        }

        /// <summary>
        /// 根据角色与单位条件查找用户
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetManagementUsers(decimal roleId, decimal depId)
        {
            return userAccount.GetManagementUsers(roleId, depId);
        }

        /// <summary>
        /// 更新用户的单位编号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="depId"></param>
        public void UpdateDepIdByUserName(string userName, decimal depId)
        {
            userAccount.UpdateDepIdByUserName(userName, depId);
        }

        /// <summary>
        /// 更新用户的用户类型编号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userTypeId"></param>
        public void UpdateUserTypeIdByUserName(string userName, decimal userTypeId)
        {
            userAccount.UpdateUserTypeIdByUserName(userName, userTypeId);
        }

        /// <summary>
        /// 更新用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newUserName"></param>
        public void UpdateUserName(string userName, string newUserName)
        {
            userAccount.UpdateUserName(userName, newUserName);
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
            userAccount.UpdateUserInfo(userToolAction, userName, userData);
        }

        /// <summary>
        /// 获得最后登录时间
        /// </summary>
        /// <param name="userName"></param>
        public DateTime GetLastLogonTime(string userName)
        {
            return userAccount.GetLastLogonTime(userName);
        }

        /// <summary>
        /// 清空重试次数
        /// </summary>
        /// <param name="userName"></param>
        public void ClearRetryTimes(string userName)
        {
            userAccount.ClearRetryTimes(userName);
        }

        /// <summary>
        /// 获得重试次数
        /// </summary>
        /// <param name="userName"></param>
        public int GetRetryTimes(string userName)
        {
            return userAccount.GetRetryTimes(userName);
        }

        /// <summary>
        /// 更新 UserAccountInfo 对象，在导入数据中使用。
        /// </summary>
        /// <param name="userAccountInfo">UserAccountInfo 对象</param>
        public void UpdateUserAccountInfo(UserAccountInfo userAccountInfo)
        {
            userAccount.UpdateUserAccountInfo(userAccountInfo);
        }

        /// <summary>
        /// 根据用户身份证号码查用户用户名
        /// </summary>
        /// <param name="userIdentity">用户身份证号码</param>
        /// <returns>用户名</returns>
        public string GetUserNameByUserIdentity(string userIdentity)
        {
            return userAccount.GetUserNameByUserIdentity(userIdentity);
        }       

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        public IList<int> Delete(Dictionary<int, string> userNames)
        {
            return userAccount.Delete(userNames);
        }

        /// <summary>
        /// 批量插入用户
        /// </summary>
        /// <param name="userAccountInfos"></param>
        /// <returns>插入失败的索引列表</returns>
        public IList<int> Insert(Dictionary<int, UserAccountInfo> userAccountInfos)
        {
            return userAccount.Insert(userAccountInfos);
        }

        /// <summary>
        /// 查询密码
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserPassword(string userName)
        {
            return userAccount.GetUserPassword(userName);
        }

        /// <summary>
        /// 批量删除 UserAccountInfo 对象
        /// </summary>
        /// <param name="userIds">用户编号列表</param>
        public void Delete(IList<decimal> userIds)
        {
            userAccount.Delete(userIds);
        }

        /// <summary>
        /// 锁定与解锁用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="locked"></param>
        public void LockUser(decimal userId, bool locked)
        {
            userAccount.LockUser(userId, locked);
        }

        /// <summary>
        /// 锁定与解锁用户状态
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="locked"></param>
        public void LockUsers(IList<decimal> userIds, bool locked)
        {
            userAccount.LockUsers(userIds, locked);
        }

        /// <summary>
        /// 根据角色编号获取用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<CommonUserInfo> GetCommonUserInfos(decimal roleId)
        {
            return userAccount.GetCommonUserInfos(roleId);
        }

        /// <summary>
        /// 根据角色编号和单位编号获取用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public IList<CommonUserInfo> GetCommonUserInfos(decimal roleId, decimal depId)
        {
            return userAccount.GetCommonUserInfos(roleId, depId);
        }

        /// <summary>
        /// 根据用户编号查询管理的单位属性
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Int64 GetDepartmentAuthority(decimal userId)
        {
            return userAccount.GetDepartmentAuthority(userId);
        }

        /// <summary>
        /// 获得用户常用信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public CommonUserInfo GetCommonUserInfo(string userName)
        {
            return userAccount.GetCommonUserInfo(userName);
        }

        /// <summary>
        /// 获得用户常用信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonUserInfo GetCommonUserInfo(decimal userId)
        {
            return userAccount.GetCommonUserInfo(userId);
        }

        /// <summary>
        /// 根据用户名查用户编号
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户编号</returns>
        public decimal GetUserIdByUserName(string userName)
        {
            return userAccount.GetUserIdByUserName(userName);
        }

        /// <summary>
        /// 获得管理的用户类型
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<decimal> GetUserTypeIds(decimal userId)
        {
            return userAccount.GetUserTypeIds(userId);
        }

        /// <summary>
        /// 获得管理的用户类型
        /// </summary>
        /// <param name="userTypeScope"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetUserTypes(decimal userId)
        {
            return userAccount.GetUserTypes(userId);
        }

        /// <summary>
        /// 获得管理的单位
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<decimal> GetDepartmentIds(decimal userId)
        {
            return userAccount.GetDepartmentIds(userId);
        }

        /// <summary>
        /// 获得管理的单位
        /// </summary>
        /// <param name="departmentScope"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDepartments(decimal userId)
        {
            return userAccount.GetDepartments(userId);
        }

        /// <summary>
        /// 获得用户所拥有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<decimal> GetRoleIds(decimal userId)
        {
            return userAccount.GetRoleIds(userId);
        }

        /// <summary>
        /// 获得用户所拥有的角色
        /// </summary>
        /// <param name="roleAndUser"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRoles(decimal userId)
        {
            return userAccount.GetRoles(userId);
        }

        /// <summary>
        /// 获得 UserAccountInfo 对象
        /// </summary>
        ///<param name="userName">用户名</param>
        /// <returns> UserAccountInfo 对象</returns>
        public UserAccountInfo GetModelInfo(string userName)
        {
            return userAccount.GetModelInfo(userName);
        }

        /// <summary>
        /// 根据用户类型编号查询用户数
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>

        public int GetUserCountByUserTypeId(decimal userTypeId)
        {
            return userAccount.GetUserCountByUserTypeId(userTypeId);
        }

        /// <summary>
        /// 根据单位编号查询用户数
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public int GetUserCountByDepId(decimal depId)
        {
            return userAccount.GetUserCountByDepId(depId);
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
            return userAccount.GetUserList(startPosition, count, whereConditons, ref totalCount);
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
            return userAccount.GetUserInfos(startPosition, count, whereConditons, ref totalCount);
        }

        /// <summary>
        /// 获得用户表中的用户名的分页数据集
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>     
        public DataSet GetUserNames(int startPosition, int count, IList<WhereConditon> whereConditons)
        {
            return userAccount.GetUserNames(startPosition, count, whereConditons);
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
            return userAccount.GetPageRecordOfMultiTables(startPosition, count, whereConditons, ref totalCount);
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
            return userAccount.GetPageRecordOfMultiTablesWithRole(startPosition, count, whereConditons, ref totalCount);
        }

        /// <summary>
        /// 用户名称是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>存在返回 true ,否则返回 false</returns>
        public bool IsExistUserName(string userName)
        {
            return userAccount.IsExistUserName(userName);
        }

        /// <summary>
        /// 用户关键字的值是否存在
        /// </summary>
        /// <param name="validationMode"></param>
        /// <param name="keyValue"></param>
        /// <returns>存在返回 true ,否则返回 false</returns>
        public bool IsExistIdentity(ValidationMode validationMode, string keyValue)
        {
            return userAccount.IsExistIdentity(validationMode, keyValue);
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
            return userAccount.Insert(userAccountInfo, imageData, userTypeIds, departmentIds, roleIds);
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
            userAccount.Update(userName, newPassword, userActualName, EmailAddress, telephoneNumber, imageData, photoSuffixName);
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
            userAccount.Update(userAccountInfo, imageData, userTypeIds, departmentIds, roleIds);
        }

        /// <summary>
        /// 自动获取照片的后缀名
        /// 自动搜索几种常见图片格式的照片，如果存在，则返回该照片的后缀名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string AutoGetPhotoSuffixName(string userName)
        {
            return userAccount.AutoGetPhotoSuffixName(userName);
        }

        /// <summary>
        /// 下载图片
        /// 图片不存在，则自动搜索几种常见图片格式的照片，如果存在则返回该图片
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>图片数据</returns>
        public byte[] DownLoadPhoto(string userName)
        {
            return userAccount.DownLoadPhoto(userName);
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
            userAccount.UpLoadPhoto(userName, photoSuffixName, fileName, imageData);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="imageData">图片数据</param>
        public void UpLoadPhoto(string fileName, byte[] imageData)
        {
            userAccount.UpLoadPhoto(fileName, imageData);
        }

        #endregion
    }
}
