//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ValidationHelper.cs
// 描述: 通用对象操作类
// 作者：ChenJie 
// 编写日期：2017/08/16
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 校验帮助类
    /// </summary>
    public sealed class AuthorityHelper
    {
        /// <summary>
        /// 检查字段权限
        /// </summary>
        /// <param name="authority">长整形类型</param>
        /// <param name="pos">从低位往高位计算，最低位位置为0</param>
        /// <returns></returns>
        public static bool CheckAuthority(Int64 authority, byte pos)
        {
            bool result = false;
            if (pos > 0)
            {
                result = ((authority >> pos) & 1L) == 1L;
            }
            else
            {
                result = (authority & 1L) == 1L;
            }

            return result;
        }

        /// <summary>
        /// 获得移动后的值
        /// </summary>
        /// <param name="authority"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Int64 GetShiftedValue(Int64 authority, byte pos)
        {
            Int64 result = 0;

            if (pos > 0)
            {
                result = (1L << pos) | authority;
            }
            else
            {
                result = 1L | authority;
            }

            return result;
        }

        /// <summary>
        /// 获取新的值
        /// </summary>
        /// <param name="checkedComboBoxEdit"></param>
        /// <returns></returns>
        public static Int64 GetAuthority(Int64 authority, byte pos, bool value)
        {
            Int64 newAuthority = 0;

            if (pos > 0)
            {
                if (value)
                {
                    newAuthority = (1L << pos) | authority;
                }
                else
                {
                    newAuthority = ~(1L << pos) & authority;
                }                
            }
            else
            {
                if (value)
                {
                    newAuthority = 1L | authority;
                }
                else
                {
                    newAuthority = ~1L & authority;
                }
            }

            return newAuthority;
        }
    }
}
