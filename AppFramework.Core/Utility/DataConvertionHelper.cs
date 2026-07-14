//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataConvertionHelper.cs
// 描述： DataConvertionHelper 操作服务类
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据转换类
    /// </summary>
    public sealed class DataConvertionHelper
    {
        #region 私有静态只读变量

        private readonly static DateTime SQLServerMinValueDateTime;
        private readonly static DateTime SQLServerMaxValueDateTime;
        private readonly static Regex regexEmail = null;
        private readonly static Regex regexInt32 = null;
        private readonly static Regex regexDecimal = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        static DataConvertionHelper()
        {
            SQLServerMinValueDateTime = DateTime.Parse("1900-1-1");
            SQLServerMaxValueDateTime = DateTime.Parse("2079-6-6");
            regexEmail = new Regex(@"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$");
            regexInt32 = new Regex(@"^-?\d+$");
            regexDecimal = new Regex(@"^(-?\d+)(\.\d+)?$");
        }
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        private DataConvertionHelper()
        {

        }
        #endregion

        #region 获取数据 用途:(1) 处理常见的数据类型转换, (2)从数据库中获得数据, (3)从文本框中获得数据

        #region String类型

        /// <summary>
        /// 转换字符串类型
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>字符串</returns>
        public static string GetString(object obj)
        {
            return GetString(obj, false, string.Empty);
        }

        /// <summary>
        /// 转换字符串类型
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>字符串</returns>
        public static string GetString(object obj, string defaultValue)
        {
            return GetString(obj, false, defaultValue);
        }


        /// <summary>
        /// 强制转换字符串类型
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>字符串</returns>
        public static string GetConvertedString(object obj)
        {
            return GetString(obj, true, string.Empty);
        }

        /// <summary>
        /// 强制转换字符串类型
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字符串</returns>
        public static string GetConvertedString(object obj, string defaultValue)
        {
            return GetString(obj, true, defaultValue);
        }

        /// <summary>
        /// 转换字符串类型
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="ifConvert">是否强制转换类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字符串</returns>
        public static string GetString(object obj, bool ifConvert, string defaultValue)
        {
            string result = defaultValue;
            if ((obj != null) || (obj != DBNull.Value))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToString(obj);
                    }
                    catch { }
                }
                else
                {
                    result = obj as string;
                    if (result == null)
                    {
                        result = defaultValue;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Decimal类型

        /// <summary>
        /// 转换数值类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>数值</returns>
        public static decimal GetDecimal(object obj)
        {
            return GetDecimal(obj, false, decimal.MinValue);
        }

        /// <summary>
        /// 转换数值类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>数值</returns>
        public static decimal GetDecimal(object obj, decimal defaultValue)
        {
            return GetDecimal(obj, false, defaultValue);
        }

        /// <summary>
        /// 强制转换数值类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>数值</returns>
        public static decimal GetConvertedDecimal(object obj)
        {
            return GetDecimal(obj, true, decimal.MinValue);
        }

        /// <summary>
        /// 强制转换数值类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>数值</returns>
        public static decimal GetConvertedDecimal(object obj, decimal defaultValue)
        {
            return GetDecimal(obj, true, defaultValue);
        }

        /// <summary>
        /// 是相同类型，则转换数值类型数据，不是则强制转换数值类型数据
        /// </summary>
        /// <param name="obj">类型的值</param>
        /// <returns>数值</returns>
        public static decimal GetRightDecimal(object obj)
        {
            return GetRightDecimal(obj, 0);
        }
        /// <summary>
        /// 是相同类型，则转换数值类型数据，不是则强制转换数值类型数据
        /// </summary>
        /// <param name="obj">类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>数值</returns>
        public static decimal GetRightDecimal(object obj, decimal defaultValue)
        {
            decimal result = defaultValue;
            if ((obj != null) || (obj != DBNull.Value))
            {
                if (obj is decimal)
                {
                    result = (decimal)obj;
                }
                else
                {
                    try
                    {
                        result = Convert.ToDecimal(obj);
                    }
                    catch { }
                }
            }

            return result;
        }

        /// <summary>
        /// 转换数值类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="ifConvert">是否强制转换类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>数值</returns>
        public static decimal GetDecimal(object obj, bool ifConvert, decimal defaultValue)
        {
            decimal result = defaultValue;
            if ((obj != null) || (obj != DBNull.Value))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToDecimal(obj);
                    }
                    catch { }
                }
                else
                {
                    if (obj is decimal)
                    {
                        result = (decimal)obj;
                    }
                }
            }

            return result;
        }
        #endregion

        #region Byte类型

        /// <summary>
        /// 转换字节类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>字节类型的值</returns>
        public static Byte GetByte(object obj)
        {
            return GetByte(obj, false, Byte.MinValue);
        }

        /// <summary>
        /// 转换字节类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字节类型的值</returns>
        public static Byte GetByte(object obj, Byte defaultValue)
        {
            return GetByte(obj, false, defaultValue);
        }

        /// <summary>
        /// 强制转换字节类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>字节类型的值</returns>
        public static Byte GetConvertedByte(object obj)
        {
            return GetByte(obj, true, Byte.MinValue);
        }

        /// <summary>
        /// 强制转换字节类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="ifConvert">是否强制转换类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字节类型的值</returns>
        public static Byte GetConvertedByte(object obj, Byte defaultValue)
        {
            return GetByte(obj, true, defaultValue);
        }

        /// <summary>
        /// 转换字节类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="ifConvert">是否强制转换类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>字节类型的值</returns>
        public static Byte GetByte(object obj, bool ifConvert, Byte defaultValue)
        {
            Byte result = defaultValue;
            if ((obj != null) || (obj != DBNull.Value))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToByte(obj);
                    }
                    catch { }
                }
                else
                {
                    if (obj is Byte)
                    {
                        result = (Byte)obj;
                    }
                }
            }
            return result;
        }

        #endregion

        #region byte[]类型

        public static byte[] GetBytes(object obj)
        {
            byte[] result = new byte[1];
            if ((obj != null) || (obj != DBNull.Value))
            {
                try
                {
                    result = (byte[])obj;
                }
                catch { }
            }
            return result;
        }

        #endregion

        #region DateTime类型

        /// <summary>
        /// 转换时间类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>时间值</returns>
        public static DateTime GetDateTime(object obj)
        {
            return GetDateTime(obj, false, DateTime.MinValue);
        }

        /// <summary>
        /// 转换时间类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="ifConvert">是否强制转换类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>时间值</returns>
        public static DateTime GetDateTime(object obj, DateTime defaultValue)
        {
            return GetDateTime(obj, false, defaultValue);
        }

        /// <summary>
        /// 强制转换时间类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>时间值</returns>
        public static DateTime GetConvertedDateTime(object obj)
        {
            return GetDateTime(obj, true, DateTime.MinValue);
        }

        /// <summary>
        /// 强制转换时间类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="ifConvert">是否强制转换类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>时间值</returns>
        public static DateTime GetConvertedDateTime(object obj, DateTime defaultValue)
        {
            return GetDateTime(obj, true, defaultValue);
        }

        /// <summary>
        /// 转换时间类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="ifConvert">是否强制转换类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>时间值</returns>
        public static DateTime GetDateTime(object obj, bool ifConvert, DateTime defaultValue)
        {
            DateTime result = defaultValue;
            if ((obj != null) || (obj != DBNull.Value))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToDateTime(obj);
                    }
                    catch { }
                }
                else
                {
                    if (obj is DateTime)
                    {
                        result = (DateTime)obj;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Int16 类型

        /// <summary>
        /// 转换Int16类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>Int16 类型的值</returns>
        public static short GetShort(object obj)
        {
            return GetShort(obj, false, Int16.MinValue);
        }

        /// <summary>
        /// 转换Int16类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int16 类型的值</returns>
        public static short GetShort(object obj, short defaultValue)
        {
            return GetShort(obj, false, defaultValue);
        }

        /// <summary>
        /// 强制转换Int16类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>Int16 类型的值</returns>
        public static short GetConvertedShort(object obj)
        {
            return GetShort(obj, true, Int16.MinValue);
        }

        /// <summary>
        /// 强制转换Int16类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int16 类型的值</returns>
        public static int GetConvertedShort(object obj, short defaultValue)
        {
            return GetShort(obj, true, defaultValue);
        }

        /// <summary>
        /// 转换Int16类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="ifConvert">是否强制转换类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int16 类型的值</returns>
        public static short GetShort(object obj, bool ifConvert, short defaultValue)
        {
            short result = defaultValue;
            if ((obj != null) || (obj != DBNull.Value))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToInt16(obj);
                    }
                    catch { }
                }
                else
                {
                    if (obj is short)
                    {
                        result = (short)obj;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Int32 类型

        /// <summary>
        /// 转换Int32类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>Int32 类型的值</returns>
        public static int GetInt(object obj)
        {
            return GetInt(obj, false, Int32.MinValue);
        }

        /// <summary>
        /// 转换Int32类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int32 类型的值</returns>
        public static int GetInt(object obj, int defaultValue)
        {
            return GetInt(obj, false, defaultValue);
        }

        /// <summary>
        /// 强制转换Int32类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>Int32 类型的值</returns>
        public static int GetConvertedInt(object obj)
        {
            return GetInt(obj, true, Int32.MinValue);
        }

        /// <summary>
        /// 强制转换Int32类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int32 类型的值</returns>
        public static int GetConvertedInt(object obj, int defaultValue)
        {
            return GetInt(obj, true, defaultValue);
        }

        /// <summary>
        /// 转换Int32类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="ifConvert">是否强制转换类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int32 类型的值</returns>
        public static int GetInt(object obj, bool ifConvert, int defaultValue)
        {
            int result = defaultValue;
            if ((obj != null) || (obj != DBNull.Value))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToInt32(obj);
                    }
                    catch { }
                }
                else
                {
                    if (obj is int)
                    {
                        result = (int)obj;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Int64类型

        /// <summary>
        /// 转换Int64类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>Int64 类型的值</returns>
        public static Int64 GetLong(object obj)
        {
            return GetLong(obj, false, Int64.MinValue);
        }

        /// <summary>
        /// 转换Int64类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int64 类型的值</returns>
        public static Int64 GetLong(object obj, Int64 defaultValue)
        {
            return GetLong(obj, false, defaultValue);
        }

        /// <summary>
        /// 强制转换Int64类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>Int64 类型的值</returns>
        public static Int64 GetConvertedLong(object obj)
        {
            return GetLong(obj, true, Int64.MinValue);
        }

        /// <summary>
        /// 强制转换Int64类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int64 类型的值</returns>
        public static Int64 GetConvertedLong(object obj, Int64 defaultValue)
        {
            return GetLong(obj, true, defaultValue);
        }

        /// <summary>
        /// 转换Int64类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="ifConvert">是否强制转换类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int64 类型的值</returns>
        public static Int64 GetLong(object obj, bool ifConvert, Int64 defaultValue)
        {
            Int64 result = defaultValue;
            if ((obj != null) || (obj != DBNull.Value))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToInt64(obj);
                    }
                    catch { }
                }
                else
                {
                    if (obj is Int64)
                    {
                        result = (Int64)obj;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Boolean类型

        /// <summary>
        /// 转换布尔类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>布尔值</returns>
        public static bool GetBoolean(object obj)
        {
            return GetBoolean(obj, false, false);
        }

        /// <summary>
        /// 转换布尔类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>布尔值</returns>
        public static bool GetBoolean(object obj, bool defaultValue)
        {
            return GetBoolean(obj, false, defaultValue);
        }

        /// <summary>
        /// 强制转换布尔类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>布尔值</returns>
        public static bool GetConvertedBoolean(object obj)
        {
            return GetBoolean(obj, true, false);
        }

        /// <summary>
        /// 强制转换布尔类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>布尔值</returns>
        public static bool GetConvertedBoolean(object obj, bool defaultValue)
        {
            return GetBoolean(obj, true, defaultValue);
        }

        /// <summary>
        /// 转换布尔类型数据
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <param name="ifConvert">是否强制转换类型</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>布尔值</returns>
        public static bool GetBoolean(object obj, bool ifConvert, bool defaultValue)
        {
            bool result = defaultValue;
            if ((obj != null) || (obj != DBNull.Value))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToBoolean(obj);
                    }
                    catch { }
                }
                else
                {
                    if (obj is bool)
                    {
                        result = (bool)obj;
                    }
                }
            }
            return result;
        }

        #endregion

        #endregion

        #region 设置数据 用途:(1)处理 sql 语句中的参数

        #region DateTime类型

        /// <summary>
        /// 设置时间类型的值
        /// </summary>
        /// <param name="value">处理前的时间值</param>
        /// <returns>处理后的时间值</returns>
        public static object SetDateTime(DateTime value)
        {
            if ((value == DateTime.MinValue) || (value == DateTime.MaxValue))
            {
                return DBNull.Value;
            }
            return value;
        }

        /// <summary>
        /// 设置时间类型的值
        /// </summary>
        /// <param name="value">处理前的时间值</param>
        /// <returns>处理后的时间值</returns>
        public static object SetDateTime(DateTime value, object defaultValue)
        {
            if ((value == DateTime.MinValue) || (value == DateTime.MaxValue))
            {
                return defaultValue;
            }
            return value;
        }

        #endregion

        #region Decimal 类型
        /// <summary>
        /// 设置数值类型的值
        /// </summary>
        /// <param name="value">处理前的数值</param>
        /// <returns>处理后的数值</returns>
        public static object SetDecimal(decimal value)
        {
            if ((value == decimal.MinValue) || (value == decimal.MaxValue))
            {
                return DBNull.Value;
            }

            return value;
        }
        #endregion

        #region Byte类型

        /// <summary>
        /// 设置 Byte 类型的值
        /// </summary>
        /// <param name="value">处理前的整型值</param>
        /// <returns>处理后值</returns>
        public static object SetByte(Byte value)
        {
            if ((value < Byte.MinValue) || (value > Byte.MaxValue))
            {
                return DBNull.Value;
            }
            return value;
        }

        #endregion

        #region Int16 类型

        /// <summary>
        /// 设置 Int16 类型的值
        /// </summary>
        /// <param name="value">处理前的整型值</param>
        /// <returns>处理后的整型值</returns>
        public static object SetShort(short value)
        {
            if ((value == short.MinValue) || (value == short.MaxValue))
            {
                return DBNull.Value;
            }
            return value;
        }

        #endregion

        #region Int32 类型

        /// <summary>
        /// 设置 Int32 类型的值
        /// </summary>
        /// <param name="value">处理前的整型值</param>
        /// <returns>处理后的整型值</returns>
        public static object SetInt(int value)
        {
            if ((value == int.MinValue) || (value == int.MaxValue))
            {
                return DBNull.Value;
            }
            return value;
        }

        #endregion

        #region Int64 类型

        /// <summary>
        /// 设置 Int64 类型的值
        /// </summary>
        /// <param name="value">处理前的整型值</param>
        /// <returns>处理后的整型值</returns>
        public static object SetLong(Int64 value)
        {
            if ((value == Int64.MinValue) || (value == Int64.MaxValue))
            {
                return DBNull.Value;
            }
            return value;
        }

        #endregion

        #endregion

        #region 赋值数据 用途:(1)给文本框赋值

        #region DateTime类型

        /// <summary>
        /// 将时间类型的值赋值给文本框
        /// </summary>
        /// <param name="value">处理前的时间值</param>
        /// <returns>处理后的时间值字符串</returns>
        public static string EndowStringOfDateTime(DateTime value)
        {
            if ((value == null) || (value == DateTime.MinValue) || (value == DateTime.MaxValue))
            {
                return string.Empty;
            }
            return value.ToString("d");
        }

        /// <summary>
        /// 将时间类型的值赋值给文本框
        /// </summary>
        /// <param name="value">处理前的时间值</param>
        /// <returns>处理后的时间值字符串</returns>
        public static string EndowStringOfLongTime(DateTime value)
        {
            if ((value == null) || (value == DateTime.MinValue) || (value == DateTime.MaxValue))
            {
                return string.Empty;
            }
            return value.ToString("F");
        }

        /// <summary>
        /// 将时间类型的值赋值给文本框
        /// </summary>
        /// <param name="value">处理前的时间值</param>
        /// <returns>处理后的时间值字符串</returns>
        public static string EndowStringOfDate(DateTime value)
        {
            if ((value == null) || (value == DateTime.MinValue) || (value == DateTime.MaxValue))
            {
                return string.Empty;
            }
            return value.ToString("d");
        }

        /// <summary>
        /// 将时间类型的值赋值给文本框
        /// </summary>
        /// <param name="value">处理前的时间值</param>
        /// <returns>处理后的时间值字符串</returns>
        public static string EndowStringOfDate(DateTime value, string fromat)
        {
            if ((value == null) || (value == DateTime.MinValue) || (value == DateTime.MaxValue))
            {
                return string.Empty;
            }
            return value.ToString(fromat);
        }

        #endregion

        #region Decimal类型

        /// <summary>
        /// 将数值类型的值赋值给文本框
        /// </summary>
        /// <param name="value">处理前的数值</param>
        /// <returns>处理后的数值字符串</returns>
        public static string EndowStringOfDecimal(Decimal value)
        {
            if ((value == Decimal.MinValue) || (value == Decimal.MaxValue))
            {
                return string.Empty;
            }
            return value.ToString();
        }

        #endregion

        #region Byte类型

        /// <summary>
        /// 将设置 Byte 类型的值赋给文本框
        /// </summary>
        /// <param name="value">处理前的整型值</param>
        /// <returns>处理后值</returns>
        public static object EndowStringOfByte(Byte value)
        {
            if ((value == Byte.MinValue) || (value == Byte.MaxValue))
            {
                return string.Empty;
            }
            return value.ToString(); ;
        }

        #endregion

        #region Int32类型

        /// <summary>
        /// 将整型类型的值赋值给文本框
        /// </summary>
        /// <param name="value">处理前的整型值</param>
        /// <returns>处理后的整型值字符串</returns>
        public static string EndowStringOfInt(int value)
        {
            if ((value == Int32.MinValue) || (value == Int32.MaxValue))
            {
                return string.Empty;
            }
            return value.ToString();
        }

        #endregion

        #region Int64类型

        /// <summary>
        /// 将长整型类型的值赋值给文本框
        /// </summary>
        /// <param name="value">处理前的长整型值</param>
        /// <returns>处理后的长整型值字符串</returns>
        public static string EndowStringOfLong(Int64 value)
        {
            if ((value == Decimal.MinValue) || (value == Decimal.MaxValue))
            {
                return string.Empty;
            }
            return value.ToString();
        }

        #endregion

        #region 枚举类型

        /// <summary>
        /// 将整型类型的值赋值给文本框
        /// </summary>
        /// <param name="value">处理前的整型值</param>
        /// <returns>处理后的整型值字符串</returns>
        public static int EndowStringOfEnum(int value)
        {
            if ((value == Int32.MinValue) || (value == Int32.MaxValue))
            {
                return -1;
            }
            return value;
        }

        #endregion

        #endregion

        #region 数据类型是否为空

        #region Byte 类型

        /// <summary>
        /// 判断 Byte 数据类型是否为空
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>空为true, 非空为 false</returns>
        public static bool IsNullValue(Byte value)
        {

            if ((value == Byte.MinValue) || (value == Byte.MaxValue))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Decimal类型

        /// <summary>
        /// 判断 Decimal 数据类型是否为空
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>空为true, 非空为 false</returns>
        public static bool IsNullValue(Decimal value)
        {

            if ((value == Decimal.MinValue) || (value == Decimal.MaxValue))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Int32 类型

        /// <summary>
        /// 判断 Int32 数据类型是否为空
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>空为true, 非空为 false</returns>
        public static bool IsNullValue(Int32 value)
        {

            if ((value == Int32.MinValue) || (value == Int32.MaxValue))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region DateTime 类型


        /// <summary>
        /// 判断 DateTime 数据类型是否为空
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>空为true, 非空为 false</returns>
        public static bool IsNullValue(DateTime value)
        {
            if ((value == DateTime.MinValue) || (value == DateTime.MaxValue) || value <= SQLServerMinValueDateTime || value >= SQLServerMaxValueDateTime)
            {
                return true;
            }

            return false;
        }

        #endregion


        #endregion

        #region 正则表达式验证

        /// <summary>
        /// 是否是合法的Email地址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmailAddress(string value)
        {
            bool result = true;
            if (!regexEmail.IsMatch(value))
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 是否是整形
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt32(string value)
        {
            bool result = true;
            if (!regexInt32.IsMatch(value))
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 是否是实数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDecimal(string value)
        {
            bool result = true;
            if (!regexDecimal.IsMatch(value))
            {
                result = false;
            }

            return result;
        }
        

        #endregion

        #region DbType与Type之间的转换

        /// <summary>
        /// Type转换为DbType
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static DbType TypeToDbType(Type t)
        {
            DbType dbt;
            try
            {
                dbt = (DbType)Enum.Parse(typeof(DbType), t.Name);
            }
            catch
            {
                dbt = DbType.Object;
            }
            return dbt;
        }

        /// <summary>
        /// DbType转换为Type
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static Type ConvertType(DbType dbType)
        {
            Type toReturn = typeof(DBNull);

            switch (dbType)
            {
                case DbType.UInt64:
                    toReturn = typeof(UInt64);
                    break;

                case DbType.Int64:
                    toReturn = typeof(Int64);
                    break;

                case DbType.Int32:
                    toReturn = typeof(Int32);
                    break;

                case DbType.UInt32:
                    toReturn = typeof(UInt32);
                    break;

                case DbType.Single:
                    toReturn = typeof(float);
                    break;

                case DbType.Date:
                case DbType.DateTime:
                case DbType.Time:
                    toReturn = typeof(DateTime);
                    break;

                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                    toReturn = typeof(string);
                    break;

                case DbType.UInt16:
                    toReturn = typeof(UInt16);
                    break;

                case DbType.Int16:
                    toReturn = typeof(Int16);
                    break;

                case DbType.SByte:
                    toReturn = typeof(byte);
                    break;

                case DbType.Object:
                    toReturn = typeof(object);
                    break;

                case DbType.VarNumeric:
                case DbType.Decimal:
                    toReturn = typeof(decimal);
                    break;

                case DbType.Currency:
                    toReturn = typeof(double);
                    break;

                case DbType.Binary:
                    toReturn = typeof(byte[]);
                    break;

                case DbType.Double:
                    toReturn = typeof(Double);
                    break;

                case DbType.Guid:
                    toReturn = typeof(Guid);
                    break;

                case DbType.Boolean:
                    toReturn = typeof(bool);
                    break;
            }

            return toReturn;
        }

        #endregion
    }
}
