//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CommonObjHelper.cs
// 描述: 通用对象操作类
// 作者：ChenJie 
// 编写日期：2016/08/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 通用对象操作类
    /// </summary>
    public sealed class CommonObjHelper
    {
        /// <summary>
        /// 通用对象操作类
        /// </summary>
        /// <param name="commonNodes"></param>
        /// <returns></returns>
        public static IList<decimal> GetCommonNodeIds(IList<CommonNode> commonNodes)
        {
            IList<decimal> commonNodeIds = new List<decimal>();
            if (commonNodes != null && commonNodes.Count > 0)
            {
                foreach (CommonNode commonNode in commonNodes)
                {
                    commonNodeIds.Add(commonNode.NodeId);
                }
            }

            return commonNodeIds;
        }

        /// <summary>
        /// 获取节点的格式化后的名称
        /// </summary>
        /// <param name="commonNodes"></param>
        /// <returns></returns>
        public static string GetCommonNodeNamesWithSemicolon(IList<CommonNode> commonNodes)
        {
            return GetCommonNodeNames(commonNodes, "; ");
        }

        /// <summary>
        /// 获取节点的格式化后的名称
        /// </summary>
        /// <param name="commonNodes"></param>
        /// <returns></returns>
        public static string GetCommonNodeNamesWithComma(IList<CommonNode> commonNodes)
        {
            return GetCommonNodeNames(commonNodes, ", ");
        }

        /// <summary>
        /// 获取节点的格式化后的值
        /// </summary>
        /// <param name="commonNodes"></param>
        /// <returns></returns>
        public static string GetCommonNodeIdsWithComma(IList<CommonNode> commonNodes)
        {
            return GetCommonNodeIds(commonNodes, ", ");
        }

        /// <summary>
        /// 获取节点的格式化后的名称
        /// </summary>
        /// <param name="commonNodes"></param>
        /// <returns></returns>
        public static string GetCommonNodeIds(IList<CommonNode> commonNodes, string formattedChar)
        {
            if (commonNodes == null || commonNodes.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            foreach (CommonNode commonNode in commonNodes)
            {
                sb.AppendFormat("{0}{1}", commonNode.NodeId, formattedChar);
            }
            if (commonNodes.Count > 0)
            {
                int len = formattedChar.Length;
                if (sb.Length > len)
                {
                    sb.Remove(sb.Length - len, len);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取节点的格式化后的名称
        /// </summary>
        /// <param name="commonNodes"></param>
        /// <returns></returns>
        public static string GetCommonNodeNames(IList<CommonNode> commonNodes, string formattedChar)
        {
            if (commonNodes == null || commonNodes.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            foreach (CommonNode commonNode in commonNodes)
            {
                sb.AppendFormat("{0}{1}", commonNode.NodeName, formattedChar);
            }
            if (commonNodes.Count > 0)
            {
                int len = formattedChar.Length;
                if (sb.Length > len)
                {
                    sb.Remove(sb.Length - len, len);
                }
            }

            return sb.ToString();
        }

    }
}
