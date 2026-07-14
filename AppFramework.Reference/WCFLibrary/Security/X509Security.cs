//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: X509Security.cs
// 描述: X5092 证书
// 作者：ChenJie 
// 编写日期：2016-07-27
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace AppFramework.Reference.WCFLibrary
{
    /// <summary>
    /// X5092 证书
    /// </summary>
    public class X509Security
    {
        #region 只读变量

        private readonly string fileNameOfX509Certificate2;
        private readonly string passwordOfX509Certificate2;
        private readonly string filePathOfX509Certificate2;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public X509Security()
        {

            // "makecert - sr LocalMachine - ss My - a sha1 - n CN = Blue_WCF_Server - sky exchange - pe"
            // "certmgr - add - r LocalMachine - s My - c - n Blue_WCF_Server - s TrustedPeople"
            fileNameOfX509Certificate2 = "Blue_WCF_Server";
            passwordOfX509Certificate2 = "jiechenjiechen";
            filePathOfX509Certificate2 = string.Format(@"{0}WCFLibrary\Security\{1}.pfx", AppDomain.CurrentDomain.BaseDirectory, fileNameOfX509Certificate2); 
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileNameOfX509Certificate2"></param>
        /// <param name="passwordOfX509Certificate2"></param>
        /// <param name="filePathOfX509Certificate2"></param>
        public X509Security(string fileNameOfX509Certificate2, string passwordOfX509Certificate2, string filePathOfX509Certificate2)
        {
            this.fileNameOfX509Certificate2 = fileNameOfX509Certificate2;
            this.passwordOfX509Certificate2 = passwordOfX509Certificate2;
            this.filePathOfX509Certificate2 = filePathOfX509Certificate2;
        }
        
        #endregion

        #region 公有方法

        /// <summary>
        /// 检查相同 X509.2 证书是否已经在系统中包含
        /// </summary>
        /// <param name="filePath">证书路径</param>
        /// <param name="password">证书密码</param>
        /// <returns>是否包含</returns>
        public bool ContainsX509Certificate2(string filePath, string password)
        {
            bool result = false;

            //(1)证书路径 (2)证书的私钥保护密码 (3)表示此证书的私钥以后还可以导出
            X509Certificate2 serverX509Certificate2 = new X509Certificate2(filePath, password, 
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            result = store.Certificates.Contains(serverX509Certificate2);
            store.Close();

            return result;
        }


        /// <summary>
        /// 导出 X509.2 证书
        /// </summary>
        /// <param name="filePath">证书路径</param>
        /// <param name="fileName">证书名称</param>
        /// <param name="password">证书密码</param>
        public void ExportX509Certificate2(string filePath, string fileName, string password)
        {
            //再将上面导入到当前用户的个人证书存储区内的证书导出为证书文件：
            //新建指向当前用户，个人证书存贮区的X509Store对象
            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            //轮询存储区中的所有证书
            foreach (X509Certificate2 myX509Certificate2 in store.Certificates)
            {
                //将证书的名称跟要导出的证书MyTestCert比较,找到要导出的证书
                if (myX509Certificate2.Subject.Equals(string.Format("CN={0}", fileName)))
                {
                    //证书导出到byte[]中，password为私钥保护密码
                    //第一个参数X509ContentType.Pfx表示要导出为含有私钥的pfx证书形式，第二个参数为私钥保护密码。                  
                    byte[] CertByte = myX509Certificate2.Export(X509ContentType.Pfx, password);
                    //如果要导出为不含私钥的cer证书，第一个参数使用X509ContentType.Cert，表示导出为不含私钥的cer证书，也就不需要密码了
                    //byte[] CertByte = myX509Certificate2.Export(X509ContentType.Cert);

                    //将证书的字节流写入到证书文件
                    FileStream fStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    fStream.Write(CertByte, 0, CertByte.Length);
                    fStream.Close();
                }
            }
            store.Close();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeName"></param>
        /// <param name="serverX509Certificate2"></param>
        public void Save(StoreName storeName, X509Certificate2 serverX509Certificate2)
        {
            //新建指向本地计算机，个人证书存贮区的X509Store对象
            X509Store store = new X509Store(storeName, StoreLocation.CurrentUser);
            bool exist = store.Certificates.Contains(serverX509Certificate2);
            if (!exist)
            {
                store.Open(OpenFlags.ReadWrite);
                store.Add(serverX509Certificate2);
                store.Close();
            }
        }

         /// <summary>
        /// 导入默认 X509.2 证书
        /// </summary>
        public void VerifyAndImportX509Certificate2()
        {
            VerifyAndImportX509Certificate2(filePathOfX509Certificate2, passwordOfX509Certificate2);
        }

        /// <summary>
        /// 导入 X509.2 证书
        /// </summary>
        /// <param name="filePath">证书路径</param>
        /// <param name="password">证书密码</param>
        public void VerifyAndImportX509Certificate2(string filePath, string password)
        {
            //(1)证书路径 (2)证书的私钥保护密码 (3)表示此证书的私钥以后还可以导出
            X509Certificate2 serverX509Certificate2 = new X509Certificate2(filePath, password, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            Save(StoreName.AuthRoot, serverX509Certificate2);
            Save(StoreName.My, serverX509Certificate2);
        }

        #endregion
    }
}
