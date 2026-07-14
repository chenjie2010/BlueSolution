//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： UserDataHelper.cs
// 描述： 用户数据帮助类
// 作者：ChenJie 
// 编写日期：2016/08/09
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AppFramework.Core
{
    /// <summary>
    /// 用户数据帮助类
    /// </summary>
    public sealed class UserDataHelper
    {
        /// <summary>
        /// 身份证号码最小长度
        /// </summary>
        private const int MIN_IDENTITY_LENGTH = 15;

        /// <summary>
        /// 文本内容最大长度
        /// </summary>
        private const int MAX_TEXT_LENGTH = 4000;

        /// <summary>
        /// 抽取字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string ExtractString(string text, char start, char end)
        {
            string result = string.Empty;

            int pos1 = text.IndexOf(start);
            int pos2 = text.IndexOf(end);
            if (pos2 > (pos1 + 1) && pos1 >= 0)
            {
                result = text.Substring(pos1 + 1, pos2 - pos1 - 1);
            }

            return result;            
        }

        /// <summary>
        /// 获得树形编码
        /// </summary>
        /// <param name="parentTreeCode"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetTreeCode(string parentTreeCode, int index)
        {
            string treeCode = string.Empty;

            if (index.ToString().Length == 1)
            {
                treeCode = string.Format("{0}00{1}", parentTreeCode, index);
            }
            else if (index.ToString().Length == 2)
            {
                treeCode = string.Format("{0}0{1}", parentTreeCode, index);
            }
            else if (index.ToString().Length == 3)
            {
                treeCode = string.Format("{0}{1}", parentTreeCode, index);
            }
            else
            {
                throw new ArgumentException("编码参数不能超过3位。");
            }

            return treeCode;
        }

        /// <summary>
        /// 验证登录用户使用的是身份证号码还是用户名
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ValidationMode GetUserValidationType(string key)
        {
            ValidationMode validationMode = ValidationMode.UserName;

            if (MatchIdentity(key))
            {
                validationMode = ValidationMode.UserIdentity;
            }
            else if (MatchMobilePhoneNumber(key))
            {
                validationMode = ValidationMode.MobilePhone;
            }
            else if(MatchEmail(key))
            {
                validationMode = ValidationMode.Email;
            }

            return validationMode;
        }

        /// <summary>
        /// 是否匹配身份证号码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MatchIdentity(string key)
        {
            return Regex.IsMatch(key, @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否匹配证件号码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MatchIdentificationNumber(string key)
        {
            bool result = true;

            if(key.Length >= MIN_IDENTITY_LENGTH)
            {
                result = Regex.IsMatch(key, @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase);
            }

            return result;
        }

        /// <summary>
        /// 是否匹配手机号
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MatchMobilePhoneNumber(string key)
        {
            return Regex.IsMatch(key, @"^1[0-9]{10}$");
        }

        /// <summary>
        /// 是否以手机号开头
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool StartWithMobilePhoneNumber(string key)
        {
            return Regex.IsMatch(key, @"^(13[0-9]|15[012356789]|17[013678]|18[0-9]|14[57])[0-9]*$");
        }

        /// <summary>
        /// 是否匹配电子邮件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MatchEmail(string key)
        {
            return Regex.IsMatch(key, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }

        /// <summary>
        /// 匹配不超过4000个字符串长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MatchStringLength(string key)
        {
            bool result = false;

            if (Regex.IsMatch(key, "^[0-9]*[1-9][0-9]*$"))
            {
                int len = Convert.ToInt32(key);
                if (len > 0 && len < MAX_TEXT_LENGTH)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 匹配正整数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MatchPositiveInteger(string key)
        {
            return Regex.IsMatch(key, "^[0-9]*[1-9][0-9]*$");
        }        

        /// <summary>
        /// 匹配正整数并包含0
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MatchDigit(string key)
        {
            return Regex.IsMatch(key, @"^\d+$");
        }

        /// <summary>
        /// 匹配字节
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MatchByte(string key)
        {
            bool result = false;

            if (Regex.IsMatch(key, @"^-?\d+$"))
            {
                int value = Convert.ToInt32(key);
                if (value >= 0 && value <= byte.MaxValue)
                {
                    result = true;
                }
            };

            return result;
        }

        /// <summary>
        /// 匹配整数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MatchInteger(string key)
        {
            Regex regex = new Regex(@"^-?\d+$");
            return regex.IsMatch(key);
        }

        /// <summary>
        /// 匹配实数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool MatchDecimal(string key)
        {
            Regex regex = new Regex(@"^(\-)?\d+(\.\d{1,2})?$");
            return regex.IsMatch(key);
        }

        /// <summary>
        /// 校验密码复杂度
        /// </summary>
        /// <param name="passowrd"></param>
        /// <returns></returns>
        public static bool ValidatePasowdString(string passowrd)
        {
            var regex = new Regex(@"
                                    (?=.*[0-9])                     #必须包含数字
                                    (?=.*[a-zA-Z])                  #必须包含小写或大写字母
                                    (?=([\x21-\x7e]+)[^a-zA-Z0-9])  #必须包含特殊符号
                                    ", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

            return regex.IsMatch(passowrd);
        }
    }
}
