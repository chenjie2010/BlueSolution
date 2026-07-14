//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: WinExceptionHelper.cs
// 描述: Win Forms 异常处理类
// 作者：ChenJie 
// 编写日期：2016/08/07
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Reference.EnterpriseLibrary;

namespace AppFramework.WinFormsLibrary
{
    /// <summary>
    /// Win Forms 异常处理类
    /// </summary>
    public static class WinExceptionHelper
    {
        /// <summary>
        /// 不抛出异常,并发出警告对话框
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void NoExceptionAndAlertPolicyWithLog(Exception ex)
        {
            ExceptionHelper.NoExceptionPolicyWithLog(ex);
            MessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}
