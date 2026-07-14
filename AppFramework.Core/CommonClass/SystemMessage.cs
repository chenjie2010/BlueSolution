//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SystemMessage.cs
// 描述： 系统消息
// 作者：ChenJie 
// 编写日期：2016-08-15
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 排序条件
    /// </summary>
    [Serializable]
    public class SystemMessage
    {
        #region 定义成员变量

        private decimal _messageId;
        private string _messageTitle;
        private string _messageContent;
        private DateTime _messageSendTime;
        private MessageType _messageType;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="messageTitle">消息编号</param>
        /// <param name="messageTitle">标题</param>
        /// <param name="messageContent">内容</param>
        /// <param name="messageSendTime">时间</param>
        /// <param name="messageType">时间</param>
        public SystemMessage(decimal messageId, string messageTitle, string messageContent, DateTime messageSendTime, MessageType messageType)
        {
            _messageId = messageId;
            _messageTitle = messageTitle;
            _messageContent = messageContent;
            _messageSendTime = messageSendTime;
            _messageType = messageType;
        }


        #endregion

        #region 属性

        /// <summary>
        /// 消息编号
        /// </summary>
        public decimal MessageId
        {
            get
            {
                return _messageId;
            }
            set
            {
                if (_messageId == value)
                    return;
                _messageId = value;
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string MessageTitle
        {
            get
            {
                return _messageTitle;
            }
            set
            {
                if (_messageTitle == value)
                    return;
                _messageTitle = value;
            }
        }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string MessageContent
        {
            get
            {
                return _messageContent;
            }
            set
            {
                if (_messageContent == value)
                    return;
                _messageContent = value;
            }
        }

        /// <summary>
        /// 消息时间
        /// </summary>
        public DateTime MessageSendTime
        {
            get
            {
                return _messageSendTime;
            }
            set
            {
                if (_messageSendTime == value)
                    return;
                _messageSendTime = value;
            }
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType
        {
            get
            {
                return _messageType;
            }
            set
            {
                if (_messageType == value)
                    return;
                _messageType = value;
            }
        }
        #endregion

    }
}
