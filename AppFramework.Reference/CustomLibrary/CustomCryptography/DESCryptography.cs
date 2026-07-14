//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomCryptography.cs
// 描述: 对称加密处理接口
// 作者：ChenJie 
// 编写日期：2016-07-29
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace AppFramework.Reference.CustomLibrary
{
    /// <summary>
    /// 对称加密类
    /// </summary>
    public class DESCryptography : ICryptography
    {
        #region 私有常量

        private const string KEY_FILE = "AppFramework.Reference.CustomLibrary.CustomCryptography.SystemKey.config";

        #endregion

        #region 私有变量

        private readonly DESCryptoServiceProvider provider = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DESCryptography()
        {
            //创建一个新的 DES key.
            provider = new DESCryptoServiceProvider();
            //设置数据加密标准 (DES) 算法的机密密钥和对称算法的初始化向量的值
            byte[] Key = null;
            byte[] IV = null;
            GetKEYAndIV(out Key, out IV);
            provider.Key = Key;
            provider.IV = IV;
        }

        #endregion

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>密文</returns>
        public string Encrypt(string plainText)
        {
            string encryptData = string.Empty;

            if (!string.IsNullOrWhiteSpace(plainText))
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                ////---------- 方法一 ----------------------------
                ////创建一个 MemoryStream 对象
                //MemoryStream ms = new MemoryStream();
                //// 创建一个加密流
                //CryptoStream encStream = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write);
                //// 创建一个 StreamWriter 对象
                //StreamWriter sw = new StreamWriter(encStream);
                //sw.WriteLine(plainText);
                //sw.Close();
                //encryptData = Convert.ToBase64String(ms.ToArray());
                //ms.Close();
                ////---------- 方法一 结束 ----------------------------

                //---------- 方法二 ----------------------------
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, provider.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                cs.FlushFinalBlock();
                encryptData = Convert.ToBase64String(ms.ToArray());
                //---------- 方法二 结束 ----------------------------

                //清空数组中的内容
                Array.Clear(plainTextBytes, 0, plainTextBytes.Length);
            }
            return encryptData;
        }


        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>密文</returns>
        public string NewEncrypt(string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
            {
                return string.Empty;
            }
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            ICryptoTransform desEncrypt = provider.CreateEncryptor();
            byte[] result = desEncrypt.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
            
            return BitConverter.ToString(result);
        }

        /// <summary>
        ///  解密字符串
        /// </summary>
        /// <param name="decryptedText">密文</param>
        /// <param name="key">对称算法的的抽象基类</param>
        /// <returns>明文</returns>
        public string Decrypt(string decryptedText)
        {
            string decryptData = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(decryptedText))
                {
                    byte[] decryptedByteArray = Convert.FromBase64String(decryptedText);

                    ////---------- 方法一 ----------------------------
                    ////为解密字符串创建一个 MemoryStream 对象
                    //MemoryStream ms = new MemoryStream(decryptedByteArray);
                    ////创建一个 CryptoStream 对象
                    //CryptoStream encStream = new CryptoStream(ms, key.CreateDecryptor(), CryptoStreamMode.Read);
                    //// 创建一个 StreamReader 对象
                    //StreamReader sr = new StreamReader(encStream);
                    //decryptData = sr.ReadLine();
                    //sr.Close();
                    //encStream.Close();
                    //ms.Close();
                    ////---------- 方法一 结束 ----------------------------

                    //---------- 方法二 ---------------------------------
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, provider.CreateDecryptor(), CryptoStreamMode.Write);
                    cs.Write(decryptedByteArray, 0, decryptedByteArray.Length);
                    cs.FlushFinalBlock();
                    decryptData = Encoding.UTF8.GetString(ms.ToArray());
                    //---------- 方法二 结束 ----------------------------

                    //清空数组中的内容
                    Array.Clear(decryptedByteArray, 0, decryptedByteArray.Length);
                }

            }
            catch { }

            return decryptData;
        }

        /// <summary>
        ///  解密字符串
        /// </summary>
        /// <param name="decryptedText">密文</param>
        /// <param name="key">对称算法的的抽象基类</param>
        /// <returns>明文</returns>
        public string NewDecrypt(string decryptedText)
        {
            string decryptData = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(decryptedText))
                {
                    string[] sinput = decryptedText.Split("-".ToCharArray());
                    byte[] data = new byte[sinput.Length];
                    for (int i = 0; i < sinput.Length; i++)
                    {
                        data[i] = byte.Parse(sinput[i], NumberStyles.HexNumber);
                    }
                    ICryptoTransform desencrypt = provider.CreateDecryptor();
                    byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);

                    return Encoding.UTF8.GetString(result);
                }
            }
            catch { }

            return decryptData;
        }

        /// <summary>
        /// 产生对称算法的初始化向量(IV和数据加密标准 (DES) 算法的机密密钥(key)
        /// </summary>
        private void GenerateIVAndKey()
        {
            //创建一个新的 DES key.
            DESCryptoServiceProvider key = new DESCryptoServiceProvider();
            byte[] Key = key.Key;
            byte[] IV = key.IV;
            UnicodeEncoding converter = new UnicodeEncoding();
            string KeyValue = Convert.ToBase64String(Key);
            string IVValue = Convert.ToBase64String(IV);
        }

        /// <summary>
        /// 获得数据加密标准 (DES) 算法的机密密钥和对称算法的初始化向量
        /// </summary>
        /// <param name="Key">数据加密标准 (DES) 算法的机密密钥</param>
        /// <param name="IV">对称算法的初始化向量</param>
        private void GetKEYAndIV(out byte[] Key, out byte[] IV)
        {
            Key = null;
            IV = null;
            using (Stream xmlInputStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(KEY_FILE))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlInputStream);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("Root").ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    if (xn.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    switch (xn.Name)
                    {
                        case "IV":
                            IV = Convert.FromBase64String(xn.InnerXml);
                            break;
                        case "KEY":
                            Key = Convert.FromBase64String(xn.InnerXml);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
