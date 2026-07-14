//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ShowModeHelper.cs
// 描述: 展现模式帮助类
// 作者：ChenJie 
// 编写日期：2017/11/4
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;


namespace AppFramework.Reference.CustomLibrary
{
    /// <summary>
    /// 展现模式帮助类
    /// </summary>
    public sealed class ShowModeHelper
    {
        #region 公有静态方法

        /// <summary>
        /// 查询展示模式分类节点集合
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public static IList<CommonNode> GetQueriedShowModes()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            IList<CommonNode> commonNodesOnFirstLevel = UserEnumHelper.GetCommonNodes(typeof(CommonShowMode));
            foreach (CommonNode commonNode in commonNodesOnFirstLevel)
            {
                commonNode.IsLeaf = false;
                commonNodes.Add(commonNode);
            }

            IList<CommonNode> commonNodesOnSecondLevel = UserEnumHelper.GetCommonNodes(typeof(QueriedShowMode));
            foreach (var childNode in commonNodesOnSecondLevel)
            {
                foreach (var parentNode in commonNodesOnFirstLevel)
                {
                    CommonShowMode commonShowMode = (CommonShowMode)parentNode.NodeId;
                    if (childNode.NodeId < parentNode.NodeId)
                    {
                        childNode.ParentNodeId = parentNode.NodeId;
                        commonNodes.Add(childNode);
                        break;
                    }

                }
            }
            return commonNodes;
        }

        /// <summary>
        /// 填报的表格展示模式分类节点集合
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public static IList<CommonNode> GetFormShowModes()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            IList<CommonNode> commonNodesOnFirstLevel = UserEnumHelper.GetCommonNodes(typeof(CommonShowMode));
            foreach (CommonNode commonNode in commonNodesOnFirstLevel)
            {
                commonNode.IsLeaf = false;
                commonNodes.Add(commonNode);
            }

            IList<CommonNode> commonNodesOnSecondLevel = UserEnumHelper.GetCommonNodes(typeof(FormShowStyle));
            foreach (var childNode in commonNodesOnSecondLevel)
            {
                foreach (var parentNode in commonNodesOnFirstLevel)
                {
                    CommonShowMode commonShowMode = (CommonShowMode)parentNode.NodeId;
                    if (childNode.NodeId < parentNode.NodeId)
                    {
                        childNode.ParentNodeId = parentNode.NodeId;
                        commonNodes.Add(childNode);
                        break;
                    }

                }
            }

            return commonNodes;
        }

        #endregion
    }
}
