//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomFormatter.cs
// 描述: 自定义格式化字符串
// 作者：ChenJie 
// 编写日期：2018-02-19
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFramework.Core
{
    /// <summary>
    /// 自定义格式化字符串
    /// </summary>
    public class CustomFormatter : IFormatProvider, ICustomFormatter
    {
        // The GetFormat method of the IFormatProvider interface.
        // This must return an object that provides formatting services for the specified type.
        public object GetFormat(System.Type type)
        {
            return this;
        }
        // The Format method of the ICustomFormatter interface.
        // This must format the specified value according to the specified format settings.
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string formatValue = arg.ToString();
            if (format == "a")
            {
                byte audit = 0;
                try
                {
                    audit = Convert.ToByte(arg);
                }
                catch { }
                string result = string.Empty;
                switch (audit)
                {
                    case 0:
                        result = "未审";
                        break;

                    case 1:
                        result = "初审";
                        break;

                    case 2:
                        result = "终审";
                        break;
                }
                return result;
            }
            else if (format == "b")
            {
                if (Convert.ToBoolean(arg))
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
            else if (format == "c")
            {
                if (Convert.ToBoolean(arg))
                {
                    return "已提交";
                }
                else
                {
                    return "未提交";
                }
            }
            else if (format == "d")
            {
                string result = string.Empty;
                try
                {
                    byte instanceState = Convert.ToByte(arg);
                    result = UserEnumHelper.GetEnumText((InstanceState)instanceState); 
                }
                catch { }

                return result;                
            }
            else if (format == "e")
            {
                return string.Empty;
            }
            else if (format == "f")
            {
                byte reviewStatus = 0;
                try
                {
                    if (arg != null && arg != DBNull.Value)
                    {
                        reviewStatus = Convert.ToByte(arg);
                    }
                }
                catch { }
                string result = string.Empty;
                switch (reviewStatus)
                {
                    case 0:
                    case 1:
                        result = "评审未完成";
                        break;

                    case 2:
                    case 3:
                        result = "评审完成";
                        break;
                }
                return result;
            }
            else
            {
                return formatValue;
            }

        }
    }
}
