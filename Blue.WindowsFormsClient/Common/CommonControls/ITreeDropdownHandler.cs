using System;
//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ITreeDropdownHandler.cs
// 描述: 通用的下拉选择项操作接口
// 作者：ChenJie 
// 编写日期：2018-01-17
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 通用的下拉选择项操作接口
    /// </summary>
    public interface ITreeDropdownHandler
    {
        #region 属性

        /// <summary>
        /// 根节点编号
        /// </summary>
        decimal RootNodeId
        {
            get;
            set;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        byte NodeType
        {
            get;
            set;
        }

        /// <summary>
        /// 通用节点契约
        /// </summary>
        ICommonNodeContract CommonNodeContract
        {
            get;
            set;
        }

        /// <summary>
        /// 查询节点操作契约
        /// </summary>
        ICommonNodeContract QueriedCommonNodeContract
        {
            set;
            get;
        }

        /// <summary>
        /// 显示父辈节点名称
        /// </summary>
        bool ParentNamesShowed
        {
            get;
            set;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化获得节点
        /// </summary>
        /// <returns></returns>
        IList<CommonNode> InitTree();        

        /// <summary>
        /// 扩展后获得的节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        IList<CommonNode> AfterExpand(TreeNode treeNode);

        /// <summary>
        /// 查询获得的节点
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IList<CommonNode> Query(string text);


        /// <summary>
        /// 获取提示
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        string GetToolTipText(TreeNode treeNode);

        #endregion

    }
}
