//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserAccountHandler.cs
// 描述: UserAccount 业务处理类
// 作者：ChenJie 
// 编写日期：2016/7/25
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.IDAL.SystemModule;
using Blue.Model.UserModule;

namespace Blue.BusinessInterface.UserModule
{
/// <summary>
    /// UserAccount 接口
    /// </summary>
    public interface IUserAccountHandler: IPrincipalBusiness<UserAccountInfo>
    {
        #region 接口

        /// <summary>
        /// 获得用户表中的用户名的分页数据集
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>   
        DataSet GetUserNames(int startPosition, int count, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 根据用户编号查用户编号
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户名</returns>
        string GetUserNameByUserId(decimal userId);

        /// <summary>
        /// 根据用户名或者证件号查用户编号
        /// </summary>
        /// <param name="key">用户名或者证件号</param>
        /// <returns>用户编号</returns>
        decimal GetUserIdByKey(string key);

        /// <summary>
        /// 获得用户数
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        int GetUserCount(IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得用户数量
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        int GetUserCount(DateTime fromUpdatedTime, DateTime toUpdatedTime);

        /// <summary>
        /// 获得用户分页数据
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        DataTable GetUserData(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime);

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <returns></returns>
        Dictionary<string, CommonUserInfo> GetCommonUserInfos();

        /// <summary>
        /// 根据用户名查邮件地址
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>邮件地址</returns>
        string GetEmailAddressByUserName(string userName);

        /// <summary>
        /// 查询用户邮件地址
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string GetEmailAddress(decimal userId);
        
        /// <summary>
        /// 更新用户邮件地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="emailAddress"></param>
        void UpdateEmailAddress(decimal userId, string emailAddress);

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        DataSet GetUserInfos(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 根据用户名查用户名和用户实际名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户编号</returns>
        StringPairValue GetUserNameInfoByUserId(decimal userId);

        /// <summary>
        /// 根据角色与用户所属单位条件查找用户
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        Dictionary<decimal, string> GetManagementUsersByUserId(decimal roleId, decimal userId);

        /// <summary>
        /// 根据角色与单位条件查找用户
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        Dictionary<decimal, string> GetManagementUsers(decimal roleId, decimal depId);

        /// <summary>
        /// 获得字段权限
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Int64 GetDataFieldAuthority(string userName);

        /// <summary>
        /// 获得用户照片路径
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string GetWebPhotoPath(string userName);

        /// <summary>
        /// 更新用户的单位编号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="depId"></param>
        void UpdateDepIdByUserName(string userName, decimal depId);

        /// <summary>
        /// 更新用户的用户类型编号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userTypeId"></param>
        void UpdateUserTypeIdByUserName(string userName, decimal userTypeId);

        /// <summary>
        /// 更新用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newUserName"></param>
        void UpdateUserName(string userName, string newUserName);


        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userToolAction"></param>
        /// <param name="userName"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        void UpdateUserInfo(UserToolAction userToolAction, string userName, string userData);

        /// <summary>
        /// 获得最后登录时间
        /// </summary>
        /// <param name="userName"></param>
        DateTime GetLastLogonTime(string userName);

        /// <summary>
        /// 清空重试次数
        /// </summary>
        /// <param name="userName"></param>
        void ClearRetryTimes(string userName);


        /// <summary>
        /// 获得重试次数
        /// </summary>
        /// <param name="userName"></param>
        int GetRetryTimes(string userName);

        /// <summary>
        /// 更新 UserAccountInfo 对象，在导入数据中使用。
        /// </summary>
        /// <param name="userAccountInfo">UserAccountInfo 对象</param>
        void UpdateUserAccountInfo(UserAccountInfo userAccountInfo);

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        IList<int> Delete(Dictionary<int, string> userNames);

        /// <summary>
        /// 批量插入用户
        /// </summary>
        /// <param name="userAccountInfos"></param>
        /// <returns>插入失败的索引列表</returns>
        IList<int> Insert(Dictionary<int, UserAccountInfo> userAccountInfos);

        /// <summary>
        /// 查询密码
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string GetUserPassword(string userName);

        /// <summary>
        /// 批量删除 UserAccountInfo 对象
        /// </summary>
        /// <param name="userIds">用户编号列表</param>
        void Delete(IList<decimal> userIds);

        /// <summary>
        /// 锁定与解锁用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="locked"></param>
        void LockUser(decimal userId, bool locked);

        /// <summary>
        /// 锁定与解锁用户状态
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="locked"></param>
        void LockUsers(IList<decimal> userIds, bool locked);

        /// <summary>
        /// 根据角色编号获取用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IList<CommonUserInfo> GetCommonUserInfos(decimal roleId);

        /// <summary>
        /// 根据角色编号和单位编号获取用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        IList<CommonUserInfo> GetCommonUserInfos(decimal roleId, decimal depId);

        /// <summary>
        /// 根据用户编号查询管理的单位属性
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Int64 GetDepartmentAuthority(decimal userId);

        /// <summary>
        /// 获得用户常用信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        CommonUserInfo GetCommonUserInfo(string userName);

        /// <summary>
        /// 获得用户常用信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        CommonUserInfo GetCommonUserInfo(decimal userId);

        /// <summary>
        /// 根据用户身份证号码查用户用户名
        /// </summary>
        /// <param name="userIdentity">用户身份证号码</param>
        /// <returns>用户名</returns>
        string GetUserNameByUserIdentity(string userIdentity);

        /// <summary>
        /// 根据用户名查用户编号
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户编号</returns>
        decimal GetUserIdByUserName(string userName);

        /// <summary>
        /// 获得管理的用户类型
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<decimal> GetUserTypeIds(decimal userId);

        /// <summary>
        /// 获得管理的用户类型
        /// </summary>
        /// <param name="userTypeScope"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<CommonNode> GetUserTypes(decimal userId);

        /// <summary>
        /// 获得管理的单位
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<decimal> GetDepartmentIds(decimal userId);

        /// <summary>
        /// 获得管理的单位
        /// </summary>
        /// <param name="departmentScope"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<CommonNode> GetDepartments(decimal userId);
        
        /// <summary>
        /// 获得用户所拥有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<decimal> GetRoleIds(decimal userId);

        /// <summary>
        /// 获得用户所拥有的角色
        /// </summary>
        /// <param name="roleAndUser"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<CommonNode> GetRoles(decimal userId);

        /// <summary>
        /// 向 UserAccount 表中插入一条新记录
        /// </summary>
        /// <param name="userAccountInfo">userAccountInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        /// <param name="userTypeIds">管理的户类型列表</param>
        /// <param name="departmentIds">管理的单位列表</param>
        /// <param name="roleIds">角色列表</param>
        /// <returns></returns>        
        decimal Insert(UserAccountInfo userAccountInfo, byte[] imageData, IList<decimal> userTypeIds, IList<decimal> departmentIds, IList<decimal> roleIds);

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
        void Update(string userName, string newPassword, string userActualName, string EmailAddress, string telephoneNumber, byte[] imageData, string photoSuffixName);

        /// <summary>
        /// 向 UserAccount 表中插入一条新记录
        /// </summary>
        /// <param name="userAccountInfo">userAccountInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        /// <param name="userTypeIds">管理的户类型列表</param>
        /// <param name="departmentIds">管理的单位列表</param>
        /// <param name="roleIds">角色列表</param>
        void Update(UserAccountInfo userAccountInfo, byte[] imageData, IList<decimal> userTypeIds, IList<decimal> departmentIds, IList<decimal> roleIds);

        /// <summary>
        /// 获得 UserAccountInfo 对象
        /// </summary>
        ///<param name="keyWord">关键字</param>
        /// <returns> UserAccountInfo 对象</returns>
        UserAccountInfo GetModelInfo(string keyWord);

        /// <summary>
        /// 根据用户类型编号查询用户数
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>

        int GetUserCountByUserTypeId(decimal userTypeId);

        /// <summary>
        /// 根据单位编号查询用户数
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        int GetUserCountByDepId(decimal depId);

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>   
        /// <returns>数据集</returns>
        DataSet GetUserList(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>  
        /// <returns>数据集</returns>
        DataSet GetUserData(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        DataSet GetPageRecordOfMultiTablesWithRole(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 用户名称是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>存在返回 true ,否则返回 false</returns>
        bool IsExistUserName(string userName);

        /// <summary>
        /// 非系统用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool IsExistOrdinaryUserName(string userName);

        /// <summary>
        /// 用户关键字的值是否存在
        /// </summary>
        /// <param name="validationMode"></param>
        /// <param name="keyValue"></param>
        /// <returns>存在返回 true ,否则返回 false</returns>
        bool IsExistIdentity(ValidationMode validationMode, string keyValue);

        /// <summary>
        /// 自动获取照片的后缀名
        /// 如果用户照片不存在，则自动搜索几种常见图片格式的照片，如果存在，则返回该照片的后缀名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string AutoGetPhotoSuffixName(string userName);

        /// <summary>
        /// 下载图片
        /// 图片不存在，则自动搜索几种常见图片格式的照片，如果存在则返回该图片
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>图片数据</returns>
        byte[] DownLoadPhoto(string userName);

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="photoSuffixName"></param>
        /// <param name="fileName"></param>
        /// <param name="imageData"></param>
        void UpLoadPhoto(string userName, string photoSuffixName, string fileName, byte[] imageData);

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="imageData">图片数据</param>
        void UpLoadPhoto(string fileName, byte[] imageData);

        #endregion
    }
}
