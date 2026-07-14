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
    public class ThreadResult
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public ThreadResult() : this(false)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="success"></param>
        public ThreadResult(bool success)
        {
            Success = success;
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
