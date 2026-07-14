//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： UserEnumHelper.cs
// 描述： 枚举属性描述类
// 作者：ChenJie 
// 编写日期：2016-08-25
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using AppFramework.Core;

namespace AppFramework.Core
{
    /// <summary>
    /// 枚举属性描述类
    /// </summary>
    public sealed class UserEnumHelper
    {
        #region 缓存

        /// <summary>
        /// 枚举反射缓存
        /// </summary>
        private static Hashtable cachedEnum = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());

        #endregion

        #region 私有变量

        /// <summary>
		/// 得到对枚举的描述文本
		/// </summary>
		/// <param name="enumType">枚举类型</param>
		/// <returns></returns>
		public static string GetDescription(Type enumType)
        {
            DescriptionAttribute[] eds = (DescriptionAttribute[])enumType.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (eds.Length != 1) return string.Empty;

            return eds[0].Description;
        }

        /// <summary>
        /// 根据枚举值获得枚举对象
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static EnumItem GetEnumItem(object enumValue)
        {
            List<EnumItem> enumItems = GetEnumItems(enumValue.GetType());
            foreach (EnumItem enumItem in enumItems)
            {
                if (enumItem.Value == Convert.ToByte(enumValue)) return enumItem;
            }

            return null;
        }

        /// <summary>
        /// 获得指定枚举类型中，指定值的描述文本。
        /// </summary>
        /// <param name="enumValue">枚举值，不要作任何类型转换</param>
        /// <returns>描述字符串</returns>
        public static string GetEnumText(object enumValue)
        {
            List<EnumItem> enumItems = GetEnumItems(enumValue.GetType());
            foreach (EnumItem enumItem in enumItems)
            {
                if (enumItem.Value == Convert.ToByte(enumValue)) return enumItem.Text;
            }

            return string.Empty;
        }

        /// <summary>
        /// 获得枚举值
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<EnumItem> GetEnumItems(Type enumType)
        {
            List<EnumItem> enumItems = null;

            //缓存中没有找到，通过反射获得字段的描述信息
            if (cachedEnum.ContainsKey(enumType.FullName) == false)
            {
                enumItems = new List<EnumItem>();
                foreach (var value in Enum.GetValues(enumType))
                {
                    object[] objAttrs = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                    if (objAttrs != null && objAttrs.Length > 0)
                    {
                        DescriptionAttribute descAttr = objAttrs[0] as DescriptionAttribute;
                        enumItems.Add(new EnumItem(descAttr.Description, Convert.ToByte(value)));
                    }
                }
                cachedEnum[enumType.FullName] = enumItems;
            }
            else
            {
                enumItems = (List<EnumItem>)cachedEnum[enumType.FullName];
            }
            if (enumItems.Count <= 0)
            {
                throw new NotSupportedException("枚举类型[" + enumType.Name + "]未定义属性描述");
            }

            return enumItems;
        }

        /// <summary>
        /// 获得枚举的个数
        /// </summary>
        /// <returns></returns>
        public static int GetEnumCount(Type enumType)
        {
            int count = 0;

            try
            {
                count = Enum.GetValues(enumType).Length;
            }
            catch { };
            
            return count;
        }

        /// <summary>
        /// 获得枚举最大值
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static int GetMaxValue(Type enumType)
        {
            int max = 0;

            foreach (int i in Enum.GetValues(enumType))
            {
                if (i > max) max = i;               
            }

            return max;
        }

        /// <summary>
        /// 获得枚举最小值
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static int GetMinValue(Type enumType)
        {
            int min = 0;

            foreach (int i in Enum.GetValues(enumType))
            {
                if (i < min) min = i;
            }

            return min;
        }

        /// <summary>
        /// 获取最大最小值
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static MaxMinValue GetMaxAndMinValue(Type enumType)
        {
            int max = 0;
            int min = 0;

            foreach (int i in Enum.GetValues(enumType))
            {
                if (i > max) max = i;
                if (i < min) min = i;
            }

            return new MaxMinValue(max, min);
        }

        /// <summary>
        /// 获得枚举列表
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IList<CommonNode> GetCommonNodes(Type enumType)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            IList<EnumItem> enumItems = GetEnumItems(enumType);

            foreach (EnumItem enumItem in enumItems)
            {
                commonNodes.Add(new CommonNode(enumItem.Value, decimal.MinValue, enumItem.Text, true));
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得授权的枚举列表
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IList<CommonNode> GetCommonNodes(Type enumType, Int64 authority)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();            

            IList<EnumItem> enumItems = GetEnumItems(enumType);
            foreach (EnumItem enumItem in enumItems)
            {
                if(AuthorityHelper.CheckAuthority(authority, enumItem.Value))
                {
                    commonNodes.Add(new CommonNode(enumItem.Value, decimal.MinValue, enumItem.Text, true));
                }                
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得枚举列表
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IList<CommonNode> GetCommonNodes(Type enumType, decimal parentNodeId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            IList<EnumItem> enumItems = GetEnumItems(enumType);

            foreach (EnumItem enumItem in enumItems)
            {
                commonNodes.Add(new CommonNode(enumItem.Value, parentNodeId, enumItem.Text, true));
            }

            return commonNodes;
        }
        /// <summary>
        /// 获得枚举节点对象
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static CommonNode GetCommonNode(object enumValue)
        {
            EnumItem enumItem = GetEnumItem(enumValue);

            return new CommonNode(enumItem.Value, decimal.MinValue, enumItem.Text, true);
        }

        /// <summary>
        /// 通过文本获得枚举值
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte GetEnumValue(Type enumType, string text)
        {
            byte enumValue = byte.MinValue;

            List<EnumItem> enumItems = GetEnumItems(enumType);
            EnumItem enumItem = enumItems.Find(item => item.Text.Equals(text));
            if (enumItem != null)
            {
                enumValue = enumItem.Value;
            }

            return enumValue;
        }

        /// <summary>
        /// 获得指定枚举类型中，指定值的描述文本。
        /// </summary>
        /// <param name="associatedDataFieldCategory">枚举值，不要作任何类型转换</param>
        /// <returns>描述字符串</returns>
        public static string GetEnumText(AssociatedDataFieldCategory associatedDataFieldCategory)
        {
            string description = GetDescription(typeof(AssociatedDataFieldCategory));
            string[] texts = description.Split('|');

            return texts[(int)associatedDataFieldCategory];            
        }

        #endregion
    }
}
