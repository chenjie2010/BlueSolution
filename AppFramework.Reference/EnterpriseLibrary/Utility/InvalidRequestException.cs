//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: InvalidRequestException.cs
// 描述: 非法的请求异常类
// 作者：ChenJie 
// 编写日期：2016-07-02
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Runtime.Serialization;

namespace AppFramework.Reference.EnterpriseLibrary
{
    /// <summary>
    /// 非法的请求异常类
    /// </summary>
    [Serializable]
    public class InvalidRequestException : ApplicationException
    {
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public InvalidRequestException()
        {

        }

        /// <summary>
        /// 序列化信息
        /// </summary>
        /// <param name="message">信息</param>
        public InvalidRequestException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 序列化错误信息，内部异常产生的原因.
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="exception">引起当前的异常</param>
        public InvalidRequestException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// 序列化数据
        /// </summary>
        /// <param name="info">序列化的数据对象</param>
        /// <param name="context">描述.</param>
        protected InvalidRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
