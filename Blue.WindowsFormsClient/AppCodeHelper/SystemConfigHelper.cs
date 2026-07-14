//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: SystemConfigHelper.cs
// 描述: 网络连接测试类
// 作者：ChenJie 
// 编写日期：2016-08-25
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 系统配置帮助类
    /// </summary>
    public sealed class SystemConfigHelper
    {
        #region 静态变量

        private static IList<EnumItem> DepartmentPorpertiesItems = null;

        #endregion

        #region 契约接口

        private static readonly ISystemConfigContract systemConfigContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SystemConfigHelper()
        {
            systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
        }

        #endregion

        #region 公有静态方法

        /// <summary>
        /// 获取单位属性
        /// </summary>
        /// <returns></returns>
        public static IList<CommonNode> GetDepartmentPorperties()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            IList<EnumItem> enumItems = GetDepartmentPorperty();
            foreach (EnumItem enumItem in enumItems)
            {
                commonNodes.Add(new CommonNode(enumItem.Value, decimal.MinValue, enumItem.Text));
            }

            return commonNodes;
        }

        /// <summary>
        /// 获取单位属性
        /// </summary>
        /// <returns></returns>
        public static IList<EnumItem> GetDepartmentPorperty()
        {
            if (DepartmentPorpertiesItems == null)
            {
                string departmentPorperty = systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.DepartmentProperty);

                if (string.IsNullOrWhiteSpace(departmentPorperty))
                {
                    DepartmentPorpertiesItems = UserEnumHelper.GetEnumItems(typeof(DepartmentProperty));
                }
                else
                {
                    DepartmentPorpertiesItems = GetEnumItems(departmentPorperty);
                }
            }

            return DepartmentPorpertiesItems;
        }

        /// <summary>
        /// 获取证件类型
        /// </summary>
        /// <returns></returns>
        public static IList<EnumItem> GetIdentificationType()
        {
            IList<EnumItem> enumItems = null;

            string identityType = systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.IdentityType);
            if (string.IsNullOrWhiteSpace(identityType))
            {
                enumItems = UserEnumHelper.GetEnumItems(typeof(IdentificationType));
            }
            else
            {
                enumItems = GetEnumItems(identityType);
            }

            return enumItems;
        }

        /// <summary>
        /// 格式化表格中的数据
        /// </summary>
        public static void SetColumnDisplayText(DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null || e.Value == DBNull.Value)
            {
                return;
            }
            int pos = e.Column.FieldName.LastIndexOf('_');
            string fieldName = e.Column.FieldName.Substring(pos + 1);
            if (fieldName == "AuditedStatus")
            {
                AuditedStatus auditedStatus = (AuditedStatus)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(auditedStatus);
            }
            else if (fieldName == "CurrentState")
            {
                CurrentState currentState = (CurrentState)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(currentState);
            }
            else if (fieldName == "DepartmentProperty")
            {
                IList<EnumItem> enumItems = SystemConfigHelper.GetDepartmentPorperty();
                byte value = Convert.ToByte(e.Value);
                int index = enumItems.FindIndex(enumItem => enumItem.Value.Equals(value));
                if (index >= 0)
                {
                    e.DisplayText = enumItems[index].Text;
                }
            }
        }

        #endregion

        #region 私有静态方法

        /// <summary>
        /// 通过自定义字符串获取枚举定义
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static IList<EnumItem> GetEnumItems(string value)
        {
            IList<EnumItem> enumItems = new List<EnumItem>();

            try
            {
                string[] departmentPorperties = value.Split('|');
                for (int i = 0; i < departmentPorperties.Length; i++)
                {
                    string[] keyAndValues = departmentPorperties[i].Split(',');
                    enumItems.Add(new EnumItem(keyAndValues[0].Trim(), Convert.ToByte(keyAndValues[1])));
                }
            }
            catch
            {
                throw new ArgumentException("系统枚举定义配置错误。");
            }

            return enumItems;
        }

        #endregion
    }
}
