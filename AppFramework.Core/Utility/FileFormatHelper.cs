//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ImageFormatHelper.cs
// 描述: 图片格式处理类
// 作者：ChenJie 
// 编写日期：2016-08-22
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;

namespace AppFramework.Core
{
    /// <summary>
    /// 文件格式格式处理类
    /// </summary>
    public sealed class FileFormatHelper
    {
        #region 文档格式        

        /// <summary>
        /// 验证文档式
        /// </summary>
        /// <param name="docPath"></param>
        /// <returns></returns>
        public static bool VerfiyDocFormat(string docPath)
        {
            bool success;

            if (docPath.EndsWith(".doc", true, null) || docPath.EndsWith(".docx", true, null)
                || docPath.EndsWith(".pdf", true, null)
                || docPath.EndsWith(".ppt", true, null) || docPath.EndsWith(".pptx", true, null)
                || docPath.EndsWith(".xls", true, null) || docPath.EndsWith(".xlsx", true, null)
                || docPath.EndsWith(".zip", true, null) || docPath.EndsWith(".rar", true, null))
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }

        #endregion

        #region 图片格式

        /// <summary>
        /// 验证PDF格式
        /// </summary>
        /// <param name="pdfPath"></param>
        /// <returns></returns>
        public static bool VerfiyPDFFormat(string pdfPath)
        {
            bool success;

            if (pdfPath.EndsWith(".PDF", true, null))
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// 验证图片格式
        /// </summary>
        /// <param name="photoPath"></param>
        /// <returns></returns>
        public static bool VerfiyPNGFormat(string photoPath)
        {
            bool success;

            if (photoPath.EndsWith(".PNG", true, null))
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// 验证图片格式
        /// </summary>
        /// <param name="photoPath"></param>
        /// <returns></returns>
        public static bool VerfiyJPGFormat(string photoPath)
        {
            bool success;

            if (photoPath.EndsWith(".JPG", true, null) || photoPath.EndsWith(".JPEG", true, null))
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }
        
        /// <summary>
        /// 验证图片格式
        /// </summary>
        /// <param name="photoPath"></param>
        /// <returns></returns>
        public static bool VerfiyImageFormat(string photoPath)
        {
            bool success;

            if (photoPath.EndsWith(".JPG", true, null) || photoPath.EndsWith(".JPEG", true, null)
                || photoPath.EndsWith(".GIF", true, null) || photoPath.EndsWith(".BMP", true, null) || photoPath.EndsWith(".PNG", true, null))
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// 后缀名
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(string format)
        {
            ImageFormat imageFormat;

            format = format.ToUpper();
            if (format.Equals("JPG") || format.Equals("JPEG"))
            {
                imageFormat = ImageFormat.Jpeg;
            }
            else if (format.Equals("GIF"))
            {
                imageFormat = ImageFormat.Gif;
            }
            else if (format.Equals("BMP"))
            {
                imageFormat = ImageFormat.Bmp;
            }
            else if (format.Equals("PNG"))
            {
                imageFormat = ImageFormat.Png;
            }
            else
            {
                imageFormat = ImageFormat.MemoryBmp;
            }

            return imageFormat;
        }

        #endregion

        #region 文件格式

        /// <summary>
        /// 验证 Excel 文件格式
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool VerfiyExcelFileFormat(string filePath)
        {
            bool success;

            if (filePath.EndsWith(".xls", true, null) || filePath.EndsWith(".xlsx", true, null))
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// 验证文件格式
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool VerfiyFileFormat(string filePath)
        {
            bool success;

            if (filePath.EndsWith(".JPG", true, null) || filePath.EndsWith(".JPEG", true, null)
                || filePath.EndsWith(".GIF", true, null) || filePath.EndsWith(".BMP", true, null) || filePath.EndsWith(".PNG", true, null)
                || filePath.EndsWith(".doc", true, null) || filePath.EndsWith(".docx", true, null)
                || filePath.EndsWith(".xls", true, null) || filePath.EndsWith(".xlsx", true, null)
                || filePath.EndsWith(".pdf", true, null)
                || filePath.EndsWith(".ppt", true, null) || filePath.EndsWith(".pptx", true, null)
                || filePath.EndsWith(".zip", true, null) || filePath.EndsWith(".rar", true, null))
            {
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }

        #endregion
    }
}
