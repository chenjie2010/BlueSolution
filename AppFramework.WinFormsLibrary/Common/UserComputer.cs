//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserComputer.cs
// 描述: 当前计算机信息
// 作者：ChenJie 
// 编写日期：2017-04-26
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace AppFramework.WinFormsLibrary
{
    /// <summary>
    /// 当前计算机信息
    /// </summary>
    public class UserComputer
    {
        #region 内部成员变量

        private string _cpuId;
        private string _diskId;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        UserComputer()
        {
            _cpuId = string.Empty;
            _diskId = string.Empty;
        }

        #endregion

        #region 嵌套类

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly UserComputer instance = new UserComputer();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 唯一实例
        /// </summary>
        public static UserComputer Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        /// <summary>
        /// CPU 编号
        /// </summary>
        public string CPUId
        {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(_cpuId))
                    {
                        ManagementClass mc = new ManagementClass("Win32_Processor");
                        ManagementObjectCollection moc = mc.GetInstances();
                        foreach (ManagementObject mo in moc)
                        {
                            _cpuId = mo.Properties["ProcessorId"].Value.ToString();
                            if (!string.IsNullOrWhiteSpace(_cpuId))
                            {
                                break;
                            }
                        }
                    }
                }
                catch { }

                return _cpuId;
            }
        }

        /// <summary>
        /// 硬盘编号
        /// </summary>
        public string DiskId
        {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(_diskId))
                    {
                        ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                        ManagementObjectCollection moc = mc.GetInstances();
                        foreach (ManagementObject mo in moc)
                        {
                            _diskId = (string)mo.Properties["Model"].Value;
                            if (!string.IsNullOrWhiteSpace(_diskId))
                            {
                                break;
                            }
                        }
                    }
                }
                catch { }

                return _diskId;
            }
        }

        #endregion
    }
}
