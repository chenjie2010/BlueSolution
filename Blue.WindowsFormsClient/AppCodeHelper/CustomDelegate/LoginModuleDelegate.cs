//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: LoginModuleDelegate.cs
// 描述: 登录模块的代理定义
// 作者：ChenJie 
// 编写日期：2016-08-07
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using AppFramework.Core;
using AppFramework.Core.ClientConfig;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 登录时回调委托
    /// </summary>
    /// <param name="asynResult"></param>
    /// <param name="logonStep"></param>
    public delegate void LogonCallbackHandler(AsynResult asynResult, LoginedStep logonStep);

    /// <summary>
    /// 清除登录用户委托
    /// </summary>
    /// <param name="userConfigs"></param>
    public delegate void RemoveUserListCallbackHandler(IList<UserInfoConfig> userConfigs);

    /// <summary>
    /// 定时处理器
    /// </summary>
    /// <param name="millisecond">耗费时间，单位为毫秒</param>
    public delegate void TimerHandler(int millisecond);

    /// <summary>
    /// 异步调用测试连接
    /// </summary>
    /// <param name="serverAddress"></param>
    /// <param name="result"></param>
    public delegate void AsynTestConnectionCaller(string serverAddress, out bool result);

    /// <summary>
    /// 异步调用测试连接
    /// </summary>
    /// <param name="serverAddress"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="result"></param>
    public delegate void AsynTestServiceCaller(string serverAddress, string userName, string password, out bool result);

    /// <summary>
    /// 异步调用测试连接
    /// </summary>
    /// <param name="serverAddress"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="result"></param>
    public delegate void AsynRemoteTestServiceCaller(string userName, string password, out bool result);
    
    /// <summary>
    /// 更新文本代理
    /// </summary>
    /// <param name="text">文本内容</param>
    public delegate void UpdateTextHandler(string text);

}
