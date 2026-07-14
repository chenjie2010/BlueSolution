//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonCategoryInfo.cs
// 描述： 通用分类信息类
// 作者：ChenJie 
// 编写日期：2016-09-13
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 通用分类信息类
    /// </summary>
    public class ExtendedCommonNode : CommonNode
    {
        #region 内部成员变量

        private string _notes;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public ExtendedCommonNode()
        {
            _notes = string.Empty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="parentNodeId"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        /// <param name="isLeaf"></param>
        /// <param name="notes"></param>
        public ExtendedCommonNode(decimal nodeId, decimal parentNodeId, string nodeName, string nodeValue, bool isLeaf, string notes) 
            : base(nodeId, parentNodeId, nodeName, nodeValue, isLeaf)
        {
            _notes = notes;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 备注
        /// </summary>
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                if (_notes == value)
                    return;
                _notes = value;
            }
        }

        #endregion
    }
}
