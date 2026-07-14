//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: SystemCallBackHandler.cs
// 描述: 回调处理类
// 作者：ChenJie 
// 编写日期：2018-08-15
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using AppFramework.Core;

namespace AppFramework.Reference.WCFLibrary
{ 
    /// <summary>
    /// 回调处理类
    /// </summary>
    public class DuplexSystemCallBackHandler : IDuplexChannelCallBackContract
    {       
        #region 私有变量

        private readonly PopupMessageDelegate popupMessageDelegate = null;

        #endregion

        #region 属性


        #endregion

        #region 构造函数


        public DuplexSystemCallBackHandler(PopupMessageDelegate popupMessageDelegate)
        {
            this.popupMessageDelegate = popupMessageDelegate;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 消息弹出处理函数
        /// </summary>
        /// <param name="message"></param>
        public void PopupMessage(SystemMessage message)
        {
            if (popupMessageDelegate != null)
            {
                popupMessageDelegate(message);
            }            
        }

        #endregion
    }
}
