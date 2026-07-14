//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: SoftwareVersionValidator.cs
// 描述: 利用服务器端软件保存的相关信息对客户端信息进行验证
// 作者：ChenJie 
// 编写日期：2016/07/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;

namespace Blue.BusinessLogic
{
    /// <summary>
    /// 软件信息验证类：利用服务器端软件保存的相关信息对客户端信息进行验证
    /// </summary>
    public class SoftwareVersionValidator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SoftwareVersionValidator()
        {
        }

        /// <summary>
        /// 是否是最新版本
        /// </summary>
        /// <param name="clientVersion"></param>
        /// <returns></returns>
        public bool IsLastestVersion(string clientVersion)
        {
            bool lastest = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(clientVersion))
                {
                    string lastestVersion = CryptographyHelper.Decrypt(AppSettingHelper.ClientVersion);
                    lastest = lastestVersion.Equals(clientVersion);
                }
            }
            catch { }

            return lastest;
        }

        /// <summary>
        /// 验证客户端版本
        /// </summary>
        /// <param name="clientVersion"></param>
        /// <returns></returns>
        public bool ValidateClientVersion(string clientVersion)
        {
            bool pass = false;

            try
            {
                /* 主版本.次版本.编译标识符 举例：3.01.001 */
                if (!string.IsNullOrWhiteSpace(clientVersion))
                {
                    string versionConsistency = CryptographyHelper.Decrypt(AppSettingHelper.CompatibleClientVersions);
                    if (!string.IsNullOrWhiteSpace(versionConsistency))
                    {
                        string[] serverInfos = versionConsistency.Split('|');
                        if (serverInfos != null && serverInfos.Length > 0)
                        {
                            foreach (string serverInfo in serverInfos)
                            {
                                if (serverInfo.Equals(clientVersion))
                                {
                                    pass = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            return pass;
        }

        /// <summary>
        /// 从配置文件中获取服务器端软件注册信息
        /// </summary>
        /// <returns>服务器端软件注册信息</returns>
        public RegisterInfo GetRegisterInfo()
        {
            RegisterInfo registerInfo = null;

            if (!string.IsNullOrWhiteSpace(AppSettingHelper.ServerRegisterCode))
            {
                string registerCode = CryptographyHelper.Decrypt(AppSettingHelper.ServerRegisterCode);

                if (!string.IsNullOrWhiteSpace(registerCode))
                {
                    /* 硬盘序列号|单位名称|软件版本|关键字|注册日期 */
                    string[] infos = registerCode.Split('|');
                    if (infos != null && infos.Length == 5)
                    {
                        registerInfo = new RegisterInfo(infos[1], infos[3], (SoftwareVersion)(Convert.ToInt32(infos[2])), Convert.ToDateTime(infos[4]));
                    }
                }
            }
            if (registerInfo == null)
            {
                return new RegisterInfo();
            }

            return registerInfo;
        }
    }
}
