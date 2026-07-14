using System;
using System.ComponentModel;
using System.Globalization;

namespace AppFramework.WinFormsControls
{
    /// <summary>
    /// 字符串转换器
    /// </summary>
    public class StringArrayConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                throw base.GetConvertFromException(value);
            }
            if (((string)value).Length == 0)
            {
                return new string[0];
            }
            string[] textArray = ((string)value).Split(new char[] { ',' });
            for (int i = 0; i < textArray.Length; i++)
            {
                textArray[i] = textArray[i].Trim();
            }
            return textArray;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                throw base.GetConvertToException(value, destinationType);
            }
            if (value == null)
            {
                return string.Empty;
            }
            return string.Join(",", (string[])value);
        }
    }
}
