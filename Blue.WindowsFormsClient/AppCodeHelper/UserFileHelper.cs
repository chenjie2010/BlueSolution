//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: FileSavedHelper.cs
// 描述: 文件保存帮助类
// 作者：ChenJie 
// 编写日期：2018/01/25
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient.Properties;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public static class UserFileHelper
    {
        #region 私有静态变量

        private static readonly ICustomMenuContract customMenuContract;
        private static readonly ICustomBusinessContract customBusinessContract;
        
        #endregion

        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static UserFileHelper()
        {
            customMenuContract = BusinessChannelFactory.CreateCustomMenuContract();
            customBusinessContract = BusinessChannelFactory.CreateCustomBusinessContract();
        }

        #endregion

        #region 公有静态方法

        /// <summary>
        /// 获得系统图片
        /// </summary>
        /// <param name="menuIcon"></param>
        /// <returns></returns>
        public static Image GetUserIcons(byte menuIcon)
        {            
            return (Image)ResourceIcon.ResourceManager.GetObject(string.Format("SystemIcon_{0}", menuIcon));
        }

        /// <summary>
        /// 获得图片对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Image GetMenuIcons(string fileName)
        {
            Image image = null;

            byte[] data = customMenuContract.DownLoadIcons(fileName);
            if (data != null)
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    image = Image.FromStream(ms);
                }
            }

            return image;
        }

        /// <summary>
        /// 获得图片对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Image GetBusinessIcons(string fileName)
        {
            Image image = null;

            byte[] data = customBusinessContract.DownLoadIcons(fileName);
            if (data != null)
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    image = Image.FromStream(ms);
                }
            }

            return image;
        }

        #endregion

        #region 私有静态方法

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileName">下载的图片文件名</param>
        /// <returns></returns>
        private static byte[] DownLoadImage(string fileName, string relativePath)
        {
            byte[] imageData = null;

            try
            {
                string fullPath = GetRelativeSubDirOfSavedFiles(relativePath);
                StringBuilder sb = new StringBuilder();
                sb.Append(fullPath);
                sb.Append(fileName);
                if (!string.IsNullOrEmpty(sb.ToString()) && File.Exists(sb.ToString()))
                {
                    /* 指定路径的扩展名（包含句点“.”） */
                    string format = Path.GetExtension(fileName).Remove(0, 1).ToUpper();
                    ImageFormat imgFormat = FileFormatHelper.GetImageFormat(format);
                    using (Image img = Image.FromFile(sb.ToString()))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, imgFormat);
                            imageData = ms.ToArray();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return imageData;
        }
                
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="imageData"></param>
        /// <param name="relativePath"></param>
        private static void SaveImage(string fileName, byte[] imageData, string relativePath)
        {
            try
            {
                string fullPath = GetRelativeSubDirOfSavedFiles(relativePath);
                StringBuilder sb = new StringBuilder();
                sb.Append(fullPath);
                if (!Directory.Exists(sb.ToString()))
                {
                    Directory.CreateDirectory(sb.ToString());
                }
                sb.Append(fileName);
                //删除服务器上的原有的存储图片             
                if (File.Exists(sb.ToString()))
                {
                    try
                    {
                        File.Delete(sb.ToString());
                    }
                    catch { }
                }
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    using (Image img = Image.FromStream(ms))
                    {
                        string format = Path.GetExtension(fileName).Remove(0, 1).ToUpper();
                        ImageFormat imageFormat = FileFormatHelper.GetImageFormat(format);
                        img.Save(sb.ToString(), imageFormat);
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="relativePath"></param>
        private static void DeleteImage(string fileName, string relativePath)
        {
            try
            {
                string fullPath = GetRelativeSubDirOfSavedFiles(relativePath);
                StringBuilder sb = new StringBuilder();
                sb.Append(fullPath);                
                sb.Append(fileName);
                //删除服务器上的原有的存储图片             
                if (File.Exists(sb.ToString()))
                {
                    try
                    {
                        File.Delete(sb.ToString());
                    }
                    catch { }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得文件的保存路径
        /// </summary>
        /// <param name="subPath"></param>
        /// <returns></returns>
        private static string GetRelativeSubDirOfSavedFiles(string subPath)
        {
            StringBuilder sb = new StringBuilder();
            string rootDirectory = AppSettingHelper.DefaultRootDirOfSavedFiles;
            sb.Append(rootDirectory);
            if (!rootDirectory.EndsWith(@"\"))
            {
                sb.Append(@"\");
            }
            sb.AppendFormat(@"{0}\", subPath);

            return sb.ToString();
        }

        #endregion
    }
}
