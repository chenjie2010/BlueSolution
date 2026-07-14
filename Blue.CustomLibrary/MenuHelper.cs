//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: MenuHelper.cs
// 描述: 菜单帮助类
// 作者：ChenJie 
// 编写日期：2017/12/14
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;

namespace Blue.CustomLibrary
{
    /// <summary>
    /// 菜单帮助类
    /// </summary>
    public sealed class MenuHelper
    {
        #region 公有静态方法

        /// <summary>
        /// 获得子菜单地址
        /// </summary>
        /// <param name="businessMenu"></param>
        /// <param name="subMenuId"></param>
        /// <returns></returns>
        public static string GetSubMenuURL(BusinessMenu businessMenu, decimal subMenuId)
        {
            string prefix = string.Empty;
            switch (businessMenu)
            {
                case BusinessMenu.PersonalData:
                    prefix = "PersonalDataModule";
                    break;

                case BusinessMenu.DataAuditing:
                    prefix = "DataAuditedModule";
                    break;

                case BusinessMenu.CommonBusiness:
                    prefix = "BusinessModule";
                    break;

                case BusinessMenu.MyWork:
                    prefix = "WorkflowModule";
                    break;

                case BusinessMenu.UserData:
                    prefix = "DataFilledModule";
                    break;

                case BusinessMenu.Auditing:
                    prefix = "AuditingModule";
                    break;

                case BusinessMenu.DataQuery:
                    prefix = "QueryModule";
                    break;

                case BusinessMenu.Report:
                    prefix = "ReportModule";
                    break;

                default:
                    throw new ArgumentException("不支持该类型。");
            }

            return string.Format(@"~\{0}\Index.aspx?SubMenuId={1}", prefix, subMenuId);
        }

        /// <summary>
        /// 获得工作流子菜单
        /// </summary>
        /// <returns></returns>
        public static List<WebSubMenu> GetWorkflowSubMenus()
        {
            List<WebSubMenu> webSubMenus = new List<WebSubMenu>();
            List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(WorkflowBusinessType));
            foreach (EnumItem enumItem in enumItems)
            {
                string subMenuIconName = string.Empty;
                string subMenuURL = string.Empty;
                WorkflowBusinessType workflowBusinessType = (WorkflowBusinessType)enumItem.Value;
                int menuId = (byte)BusinessMenu.MyWork * (-10) - (byte)workflowBusinessType;
                switch (workflowBusinessType)
                {
                    case WorkflowBusinessType.WorkflowAuditing:
                        subMenuURL = "WorkflowModule/WorkflowAuditing.aspx";
                        subMenuIconName = "fa-calendar-o";
                        break;

                    case WorkflowBusinessType.WorkflowAudited:
                        subMenuURL = "WorkflowModule/WorkflowAudited.aspx";
                        subMenuIconName = " fa-calendar-check-o";
                        break;

                    case WorkflowBusinessType.WorkflowStatistics:
                        subMenuURL = "WorkflowModule/WorkflowStatistics.aspx";
                        subMenuIconName = "fa-pie-chart";
                        break;
                }
                webSubMenus.Add(new WebSubMenu(menuId, enumItem.Text, subMenuIconName, subMenuURL));
            }

            return webSubMenus;
        }

        /// <summary>
        /// 获得填报审核子菜单
        /// </summary>
        /// <returns></returns>
        public static List<WebSubMenu> GetDataFilledSubMenus()
        {
            List<WebSubMenu> webSubMenus = new List<WebSubMenu>();
            List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(DataFilledBusinessType));
            foreach (EnumItem enumItem in enumItems)
            {
                string subMenuIconName = string.Empty;
                string subMenuURL = string.Empty;
                DataFilledBusinessType dataFilledBusinessType = (DataFilledBusinessType)enumItem.Value;
                int menuId = (byte)BusinessMenu.Auditing * (-10) - (byte)dataFilledBusinessType;
                switch (dataFilledBusinessType)
                {
                    case DataFilledBusinessType.DataAuditing:
                        subMenuURL = "AuditingModule/DataAuditing.aspx";
                        subMenuIconName = "fa-laptop";
                        break;

                    case DataFilledBusinessType.DataAudited:
                        subMenuURL = "AuditingModule/DataAudited.aspx";
                        subMenuIconName = " fa-check-circle-o";
                        break;

                    case DataFilledBusinessType.DataStatistics:
                        subMenuURL = "AuditingModule/DataStatistics.aspx";
                        subMenuIconName = "fa-line-chart";
                        break;
                }
                webSubMenus.Add(new WebSubMenu(menuId, enumItem.Text, subMenuIconName, subMenuURL));
            }

            return webSubMenus;
        }

        /// <summary>
        /// 获得信息审核子菜单
        /// </summary>
        /// <param name="dataAuditingType"></param>
        /// <returns></returns>
        public static WebSubMenu GetDataAuditingSubMenus(DataAuditingType dataAuditingType)
        {
            string subMenuName = UserEnumHelper.GetEnumText(dataAuditingType);
            string subMenuIconName = string.Empty;
            string subMenuURL = string.Empty;
            int menuId = (byte)BusinessMenu.DataAuditing * (-10) - (byte)dataAuditingType;
            switch (dataAuditingType)
            {
                case DataAuditingType.Personal:
                    subMenuURL = "DataAuditedModule/Personal.aspx";
                    subMenuIconName = "fa-user";
                    break;

                case DataAuditingType.Group:
                    subMenuURL = "DataAuditedModule/Group.aspx";
                    subMenuIconName = " fa-users";
                    break;
            }

            return new WebSubMenu(menuId, subMenuName, subMenuIconName, subMenuURL);
        }

        /// <summary>
        /// 获得信息审核子菜单
        /// </summary>
        /// <returns></returns>
        public static List<WebSubMenu> GetDataAuditingSubMenus()
        {
            List<WebSubMenu> webSubMenus = new List<WebSubMenu>();
            List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(DataAuditingType));
            foreach (EnumItem enumItem in enumItems)
            {
                string subMenuIconName = string.Empty;
                string subMenuURL = string.Empty;
                DataAuditingType dataAuditingType = (DataAuditingType)enumItem.Value;
                int menuId = (byte)BusinessMenu.DataAuditing * (-10) - (byte)dataAuditingType;
                switch (dataAuditingType)
                {
                    case DataAuditingType.Personal:
                        subMenuURL = "DataAuditedModule/Personal.aspx";
                        subMenuIconName = "fa-user";
                        break;

                    case DataAuditingType.Group:
                        subMenuURL = "DataAuditedModule/Group.aspx";
                        subMenuIconName = " fa-users";
                        break;
                }
                webSubMenus.Add(new WebSubMenu(menuId, enumItem.Text, subMenuIconName, subMenuURL));
            }

            return webSubMenus;
        }

        /// <summary>
        /// 获得数据查询子菜单
        /// </summary>
        /// <returns></returns>
        public static List<WebSubMenu> GetDataQueryingSubMenus()
        {
            List<WebSubMenu> webSubMenus = new List<WebSubMenu>();
            List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(DataQueryingType));
            foreach (EnumItem enumItem in enumItems)
            {
                string subMenuIconName = string.Empty;
                string subMenuURL = string.Empty;
                DataQueryingType dataQueryingType = (DataQueryingType)enumItem.Value;
                int menuId = (byte)BusinessMenu.DataQuery * (-10) - (byte)dataQueryingType;
                switch (dataQueryingType)
                {
                    case DataQueryingType.DataQuerying:
                        subMenuURL = "QueryModule/DataQuerying.aspx";
                        subMenuIconName = "fa-search-plus";
                        break;                        
                }
                webSubMenus.Add(new WebSubMenu(menuId, enumItem.Text, subMenuIconName, subMenuURL));
            }

            return webSubMenus;
        }


        /// <summary>
        /// 获得 Web 子菜单路径
        /// </summary>
        /// <param name="businessMenu"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static string GetMenuURL(BusinessMenu businessMenu, decimal menuId)
        {
            string menuURL = string.Empty;

            switch (businessMenu)
            {
                case BusinessMenu.PersonalData:
                    menuURL = string.Format("DataModule/Index.aspx?MenuId = ", menuId);
                    break;

                case BusinessMenu.DataAuditing:
                    menuURL = string.Format("AuditingModule/Index.aspx?MenuId = ", menuId);
                    break;

                case BusinessMenu.MyWork:
                    menuURL = string.Format("WorkflowModule/Index.aspx?MenuId = ", menuId);
                    break;

                case BusinessMenu.CommonBusiness:
                    menuURL = string.Format("BusinessModule/Index.aspx?MenuId = ", menuId);
                    break;

                case BusinessMenu.UserData:
                    menuURL = string.Format("DataFilledModule/Index.aspx?MenuId = ", menuId);
                    break;

                case BusinessMenu.Auditing:
                    menuURL = string.Format("DataAuditedModule/Index.aspx?MenuId = ", menuId);
                    break;

                case BusinessMenu.DataQuery:
                    menuURL = string.Format("QueryModule/Index.aspx?MenuId = ", menuId);
                    break;

                case BusinessMenu.Report:
                    menuURL = string.Format("ReportModule/Index.aspx?MenuId = ", menuId);
                    break;

                case BusinessMenu.DataBussiness:
                    menuURL = string.Format("DataBussinessModule/Index.aspx?MenuId = ", menuId);
                    break;
            }

            return menuURL;
        }

        /// <summary>
        /// 获得主菜单名称
        /// </summary>
        /// <param name="businessMenu"></param>
        /// <returns></returns>
        public static string GetMenuIconName(BusinessMenu businessMenu)
        {
            string iconName = string.Empty;

            switch (businessMenu)
            {
                case BusinessMenu.PersonalData:
                    iconName = "fa-id-card-o";
                    break;

                case BusinessMenu.DataAuditing:
                    iconName = "fa-check-circle";
                    break;

                case BusinessMenu.MyWork:
                    iconName = "fa-desktop";
                    break;

                case BusinessMenu.CommonBusiness:
                    iconName = "fa-reorder";
                    break;

                case BusinessMenu.UserData:
                    iconName = "fa-tablet";
                    break;

                case BusinessMenu.Auditing:
                    iconName = " fa-tasks";
                    break;

                case BusinessMenu.DataQuery:
                    iconName = "fa-search-plus";
                    break;

                case BusinessMenu.Report:
                    iconName = "fa-bar-chart-o";
                    break;

                case BusinessMenu.DataBussiness:
                    iconName = "fa-gear";
                    break;
            }

            return iconName;
        }

        /// <summary>
        /// 获得自定义业务节点列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public static IList<CommonNode> GetCustomBusiness()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            IList<CommonNode> commonNodesOnFirstLevel = UserEnumHelper.GetCommonNodes(typeof(BusinessMenu));
            foreach (CommonNode commonNode in commonNodesOnFirstLevel)
            {
                commonNode.IsLeaf = false;
                commonNodes.Add(commonNode);
                CommonNode childNode = null;
                BusinessMenu menuBusinessType = (BusinessMenu)commonNode.NodeId;
                switch (menuBusinessType)
                {
                    case BusinessMenu.MyWork:
                        childNode = UserEnumHelper.GetCommonNode(CustomBusiness.None);
                        break;

                    case BusinessMenu.CommonBusiness:
                        childNode = UserEnumHelper.GetCommonNode(CustomBusiness.None);
                        break;                   

                    case BusinessMenu.Report:
                        break;
                }
                if (childNode != null)
                {
                    childNode.ParentNodeId = commonNode.NodeId;
                    commonNodes.Add(childNode);
                }
            }

            return commonNodes;
        }

        #endregion
    }
}
