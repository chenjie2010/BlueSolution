//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： UserLogonStateConvert.cs
// 描述： 用户登录枚举类型转换类
// 作者：ChenJie 
// 编写日期：2016-08-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using AppFramework.Core;

namespace AppFramework.Core.ClientConfig
{
    /// <summary>
    /// 自定义的枚举 UserLogonState 转换器类
    /// </summary>
    public class UserLogonStateConvert : ConfigurationConverterBase
    {
        /// <summary>
        /// 验证类型是否正确
        /// </summary>
        /// <param name="value">源类型</param>
        /// <param name="expected">目标类型</param>
        /// <returns>如果类型正确，则为 true；否则为 false</returns>
        internal bool ValidateType(object value, Type expected)
        {
            bool result = false;
            if ((value != null) && (value.GetType() == expected))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 确定是否允许此转换
        /// </summary>
        /// <param name="ctx">用于类型转换的 System.ComponentModel.ITypeDescriptorContext 对象</param>
        /// <param name="type">要转换的源 System.Type</param>
        /// <returns>如果允许转换，则为 true；否则为 false</returns>
        public override bool CanConvertTo(ITypeDescriptorContext ctx, Type type)
        {
            return (type == typeof(UserLogonState));
        }

        /// <summary>
        ///  确定是否允许此转换
        /// </summary>
        /// <param name="ctx">用于类型转换的 System.ComponentModel.ITypeDescriptorContext 对象</param>
        /// <param name="type">要转换到的类型</param>
        /// <returns>如果允许转换，则为 true；否则为 false</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext ctx, Type type)
        {
            return (type == typeof(string));
        }

        /// <summary>
        /// 使用参数将给定的值对象转换为指定的类型
        /// </summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext，提供格式上下文</param>
        /// <param name="culture"> CultureInfo。如果传递 null，则采用当前区域性。</param>
        /// <param name="value">要转换到的 System.Type</param>
        /// <param name="destinationType">参数要转换到的 System.Type</param>
        /// <returns>转换的 value 的 System.Object</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
            object value, Type destinationType)
        {
            string userLogonType = string.Empty;
            if (value != null)
            {
                userLogonType = value.ToString();
            }
            else
            {
                throw new Exception("参数错误");
            }
            return userLogonType;
        }

        /// <summary>
        /// 使用指定的上下文和区域性信息将给定的对象转换为此转换器的类型
        /// </summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext，提供格式上下文</param>
        /// <param name="culture"> 用作当前区域性的 CultureInfo</param>
        /// <param name="value"> 要转换的 System.Object</param>
        /// <returns>转换的 value 的 System.Object</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            UserLogonState userLogonState;
            
            //方法一
            Type keysType = typeof(UserLogonState); //获得Type对象
            string stringValue = (string)value;
            
            userLogonState = (UserLogonState)Enum.Parse(keysType, stringValue);            

            /*
            //方法二
            Dictionary<int, UserLogonState> userLogonStateMap; //声明一个Dictionary来存储整型值和枚举值的对应关系            
            Type keysType = typeof(UserLogonState); //获得Type对象
            userLogonStateMap = new Dictionary<int, UserLogonState>();

            //将枚举的元素转成字符串
            foreach (string s in Enum.GetNames(keysType))
            {
                int keyCode = Int32.Parse(Enum.Format(keysType, Enum.Parse(keysType, s), "d"));  //获取元素基础值
                userLogonState = (UserLogonState)Enum.Parse(keysType, s);
                //获取元素值
                if (!userLogonStateMap.Keys.Contains(keyCode))   
                {
                    userLogonStateMap.Add(keyCode, userLogonState);   //写入到字典中
                }
            }
            userLogonState = userLogonStateMap[enumValue];
             */

            return userLogonState;
        }
   }
}
