//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: WinPlatformHelper.cs
// 描述: Win Forms 平台帮助类
// 作者：ChenJie 
// 编写日期：2018/07/10
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
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
    public static class WinPlatformHelper
    {
        public static string LastestFilePath
        {
            get;
            set;
        }

        static WinPlatformHelper()
        {
            LastestFilePath = string.Empty;
        }

        /// <summary>
        /// 获得文件目录
        /// </summary>
        /// <returns></returns>
        public static string GetFileFloder()
        {
            string path = Application.StartupPath;

            if (!string.IsNullOrWhiteSpace(LastestFilePath) && Directory.Exists(LastestFilePath))
            {
                path = LastestFilePath;
            }
            else
            {
                string desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (Directory.Exists(desktopDir))
                {
                    path = desktopDir;                    
                }
            }

            return path;
        }


    }
}
