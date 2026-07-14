using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 校验结果
    /// </summary>
    public class ValidatedResult
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public ValidatedResult() : this(false)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="success"></param>
        public ValidatedResult(bool success)
        {
            Success = success;
            UserName = string.Empty;
            UserTypeId = 0;            
            DepId = 0;
            IdentificationType = 0;
            UserIdentity = string.Empty;
            TelephoneNumber = string.Empty;
            EmailAddress = string.Empty;
            GoToURL = string.Empty;
            ErrorMessage = string.Empty;
        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 校验结果
        /// </summary>
        public bool Success
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 用户类型编号
        /// </summary>
        public decimal UserTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 用户单位编号
        /// </summary>
        public decimal DepId
        {
            get;
            set;
        }

        /// <summary>
        /// 证件类型
        /// </summary>
        public decimal IdentificationType
        {
            get;
            set;
        }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string UserIdentity
        {
            get;
            set;
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string TelephoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string EmailAddress
        {
            get;
            set;
        }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string GoToURL
        {
            get;
            set;
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get;
            set;
        }

        #endregion
    }
}
