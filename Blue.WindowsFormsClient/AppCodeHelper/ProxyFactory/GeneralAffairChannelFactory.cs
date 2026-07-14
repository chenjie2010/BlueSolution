//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: GeneralAffairChannelFactory.cs
// 描述: 通用业务工厂类
// 作者：ChenJie 
// 编写日期：2017/09/14
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts.GeneralAffairModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 与服务器端连接的通用操作工厂类，不需要用户名和密码验证
    /// </summary>
    public sealed class GeneralAffairChannelFactory
    {
        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static GeneralAffairChannelFactory()
        {

        }

        #endregion

        #region 静态方法    

        /// <summary>
        /// 创建 IPriavteAttachmentContract 代理对象
        /// </summary>
        /// <returns></returns>
        public static IPriavteAttachmentContract CreatePriavteAttachmentContract()
        {
            IPriavteAttachmentContract priavteAttachmentContract = ServiceProxyFactory.Create<IPriavteAttachmentContract>("PriavteAttachmentService");

            return priavteAttachmentContract;
        }

        /// <summary>
        /// 创建 IPrivateMailContract 对象
        /// </summary>
        /// <returns></returns>
        public static IPrivateMailContract CreatePrivateMailContract()
        {
            IPrivateMailContract privateMailContract = ServiceProxyFactory.Create<IPrivateMailContract>("PrivateMailService");

            return privateMailContract;
        }

        #endregion

    }
}
