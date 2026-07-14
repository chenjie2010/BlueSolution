using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace AppFramework.Reference.CustomLibrary
{
    /// <summary>
    /// 加密操作类(装饰模式)
    /// </summary>
    public sealed class CryptographyHelper
    {
        #region 私有变量

        private static readonly ICryptography symmetricCryptography;

        #endregion
        
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static CryptographyHelper()
        {
            symmetricCryptography = new DESCryptography();
        }

        #region 私有变量

        /// <summary>
        /// 使用对称密钥进行加密
        /// </summary>
        /// <param name="provider">加密类型提供者</param>
        /// <param name="plainText">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string plainText)
        {
            return symmetricCryptography.Encrypt(plainText);
        }

        /// <summary>
        /// 使用对称密钥进行加密
        /// </summary>
        /// <param name="provider">加密类型提供者</param>
        /// <param name="plainText">明文</param>
        /// <returns>密文</returns>
        public static string NewEncrypt(string plainText)
        {
            return symmetricCryptography.NewEncrypt(plainText);
        }

        /// <summary>
        /// 使用对称密钥进行加密
        /// </summary>
        /// <param name="plainText">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string decryptedText)
        {
            return symmetricCryptography.Decrypt(decryptedText);
        }


        /// <summary>
        /// 使用对称密钥进行加密
        /// </summary>
        /// <param name="plainText">密文</param>
        /// <returns>明文</returns>
        public static string NewDecrypt(string decryptedText)
        {
            return symmetricCryptography.NewDecrypt(decryptedText);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetMD5(string data)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue = Encoding.UTF8.GetBytes(data);
            byte[] bytHash = md5.ComputeHash(bytValue);
            md5.Clear();

            StringBuilder sb = new StringBuilder();

            string sTemp = string.Empty;
            for (int i = 0; i < bytHash.Length; i++)
            {
                sb.Append(bytHash[i].ToString("X").PadLeft(2, '0'));
            }
                                  
            //foreach (byte b in bytHash)
            //{
            //    sb.Append(b.ToString("x2"));
            //}

            return sb.ToString();

            
        }

        /// <summary>
        /// SHA512
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Hash(string data)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(data);
            byte[] bytHash = sha512.ComputeHash(bytValue);
            sha512.Clear();

            string result = string.Empty;
            for (int i = 0; i < bytHash.Length; i++)
            {
                result += bytHash[i].ToString("X").PadLeft(2, '0');
            }

            return result.ToLower();
        }

        #endregion

    }
}
