//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomCryptography.cs
// 描述: 对称加密处理接口
// 作者：ChenJie 
// 编写日期：2016-07-29
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Reference.CustomLibrary
{
    /// <summary>
    /// 对称加密处理接口
    /// </summary>
    public interface ICryptography
    {
        /// <summary>
        /// 使用对称密钥进行加密
        /// </summary>
        /// <param name="provider">加密类型提供者</param>
        /// <param name="plainText">明文</param>
        /// <returns>密文</returns>
        string Encrypt(string plainText);

        /// <summary>
        /// 使用对称密钥进行加密
        /// </summary>
        /// <param name="provider">加密类型提供者</param>
        /// <param name="plainText">明文</param>
        /// <returns>密文</returns>
        string NewEncrypt(string plainText);

        /// <summary>
        /// 使用对称密钥进行加密
        /// </summary>
        /// <param name="plainText">密文</param>
        /// <returns>明文</returns>
        string Decrypt(string decryptedText);

        /// <summary>
        /// 使用对称密钥进行加密
        /// </summary>
        /// <param name="plainText">密文</param>
        /// <returns>明文</returns>
        string NewDecrypt(string decryptedText);
    }
}
