//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CurrentUser.cs
// 描述: 保存当前用户全局信息
// 作者：ChenJie 
// 编写日期：2017-08-17
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using AppFramework.Core;
using Blue.CustomLibrary;
using Blue.Model.UserModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 保存当前用户全局信息，属于单件模式（Singleton Pattern）
    /// </summary>
    public class CurrentUser
    {
        #region 内部成员变量

        private decimal _userId;
        private decimal _userTypeId;
        private string _userTypeName;
        private string _userTypeCode;
        private decimal _depId;
        private string _depName;
        private string _depCode;
        private string _depValue;
        private string _userName = string.Empty;
        private string _userActualName = string.Empty;
        private bool _passowrdQueried = false;
        private bool _workflow = false;
        private bool _auditing = false;
        private bool _userAdded = false;
        private bool _userEdited = false;
        private bool _dataSwap = false;
        private bool _personalAduting = false;
        private bool _groupAduting = false;
        private bool _infoAduting = false;
        private bool _personInfoQueried = false;
        private bool _statisticalDataQueried = false;
        private UserProperty _userProperty;
        private UserLogonState _currentUserLogonState;
        private Dictionary<byte, IList<CustomMenuInfo>> _catalogMenuIds = new Dictionary<byte, IList<CustomMenuInfo>>();
        private Dictionary<decimal, IList<ExtendedCustomBusinessInfo>> _extendedCustomBusinessInfo = new Dictionary<decimal, IList<ExtendedCustomBusinessInfo>>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        CurrentUser()
        {

        }

        #endregion

        #region 嵌套类

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly CurrentUser instance = new CurrentUser();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 唯一实例
        /// </summary>
        public static CurrentUser Instance
        {
            get
            {
                return Nested.instance;
            }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public decimal UserId
        {
            get
            {
                return _userId;
            }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public decimal UserTypeId
        {
            get
            {
                return _userTypeId;
            }
        }

        /// <summary>
        /// 用户类型名称
        /// </summary>
        public string UserTypeName
        {
            get
            {
                return _userTypeName;
            }
            set
            {
                if (_userTypeName == value)
                    return;
                _userTypeName = value;
            }
        }

        /// <summary>
        /// 用户类型编码
        /// </summary>
        public string UserTypeCode
        {
            get { return _userTypeCode; }
            set
            {
                if (_userTypeCode == value)
                    return;
                _userTypeCode = value;
            }
        }

        /// <summary>
        /// 单位编号
        /// </summary>
        public decimal DepId
        {
            get
            {
                return _depId;
            }
        }

        /// <summary>
        /// 用户单位名称
        /// </summary>
        public string DepName
        {
            get
            {
                return _depName;
            }
            set
            {
                if (_depName == value)
                    return;
                _depName = value;
            }
        }

        /// <summary>
        /// 用户单位编码
        /// </summary>
        public string DepCode
        {
            get
            {
                return _depCode;
            }
            set
            {
                if (_depCode == value)
                    return;
                _depCode = value;
            }
        }

        /// <summary>
        /// 用户单位值
        /// </summary>
        public string DepValue
        {
            get
            {
                return _depValue;
            }
            set
            {
                if (_depValue == value)
                    return;
                _depValue = value;
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserActualName
        {
            get
            {
                return _userActualName;
            }
        }

        /// <summary>
        /// 用户属性
        /// </summary>
        public UserProperty UserProperty
        {
            get
            {
                return _userProperty;
            }
        }

        /// <summary>
        /// 查询密码权限
        /// </summary>
        public bool PassowrdQueried
        {
            get
            {
                return _passowrdQueried;
            }
        }

        /// <summary>
        /// 处理工作流权限
        /// </summary>
        public bool Workflow
        {
            get
            {
                return _workflow;
            }
        }

        /// <summary>
        /// 用户增加
        /// </summary>
        public bool UserAdded
        {
            get
            {
                return _userAdded;
            }
        }

        /// <summary>
        /// 用户编辑
        /// </summary>
        public bool UserEdited
        {
            get
            {
                return _userEdited;
            }
        }

        /// <summary>
        /// 数据转表
        /// </summary>
        public bool DataSwap
        {
            get
            {
                return _dataSwap;
            }
        }

        /// <summary>
        /// 数据填报审核
        /// </summary>
        public bool Auditing
        {
            get
            {
                return _auditing;
            }
        }

        /// <summary>
        /// 个人数据审核
        /// </summary>
        public bool PersonalAduting
        {
            get
            {
                return _personalAduting;
            }
        }

        /// <summary>
        /// 分组数据审核
        /// </summary>
        public bool GroupAduting
        {
            get
            {
                return _groupAduting;
            }
        }

        /// <summary>
        /// 信息审核
        /// </summary>
        public bool InfoAduting
        {
            get
            {
                return _infoAduting;
            }
        }

        /// <summary>
        /// 个人信息查询
        /// </summary>
        public bool PersonInfoQueried
        {
            get
            {
                return _personInfoQueried;
            }
        }

        /// <summary>
        /// 统计信息查询
        /// </summary>
        public bool StatisticalDataQueried
        {
            get
            {
                return _statisticalDataQueried;
            }
        }

        /// <summary>
        /// 菜单分类编号集合（二级菜单栏目）
        /// </summary>
        public Dictionary<byte, IList<CustomMenuInfo>> CatalogMenuIds
        {
            get
            {
                return _catalogMenuIds;
            }
        }

        /// <summary>
        /// 业务名称
        /// </summary>
        public Dictionary<decimal, IList<ExtendedCustomBusinessInfo>> ExtendedCustomBusinessInfos
        {
            get
            {
                return _extendedCustomBusinessInfo;
            }
        }

        /// <summary>
        /// 登录状态
        /// </summary>
        public UserLogonState CurrentUserLogonState
        {
            get
            {
                return _currentUserLogonState;
            }
            set
            {
                _currentUserLogonState = value;
            }
        }

        /// <summary>
        /// 获得本机IP地址
        /// </summary>
        public IPAddress CurrentIPAdress
        {
            get
            {
                IPAddress clinetIP = null;
                IPAddress[] address_list = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress ipAddress in address_list)
                {
                    //IP地址正则表达式
                    Regex r = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
                    if (r.IsMatch(ipAddress.ToString()))
                    {
                        clinetIP = ipAddress;
                    }
                }
                if (clinetIP == null)
                {
                    clinetIP = address_list[0];
                }

                return clinetIP;
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 设置用户信息
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="userProperty"></param>
        /// <param name="roleAuthority"></param>
        /// <param name="customMenuInfos"></param>
        /// <param name="extendedCustomBusinessInfos"></param>
        public void SetUserInfo(CommonUserInfo commonUserInfo, UserProperty userProperty, RoleAuthority roleAuthority,
            IList<CustomMenuInfo> customMenuInfos, IList<ExtendedCustomBusinessInfo> extendedCustomBusinessInfos)
        {
            _userId = commonUserInfo.UserId;
            _userTypeId = commonUserInfo.UserTypeId;
            _userTypeName = commonUserInfo.UserTypeName;
            _userTypeCode = commonUserInfo.UserTypeCode;
            _depId = commonUserInfo.DepId;
            _depName = commonUserInfo.DepName;
            _depCode = commonUserInfo.DepCode;
            _depValue = commonUserInfo.DepValue;
            _userName = commonUserInfo.UserName;
            _userActualName = commonUserInfo.UserActualName;
            _userProperty = userProperty;

            if (customMenuInfos != null && customMenuInfos.Count > 0)
            {
                foreach (CustomMenuInfo customMenuInfo in customMenuInfos)
                {
                    IList<CustomMenuInfo> menuInfos = null;
                    if (!_catalogMenuIds.ContainsKey(customMenuInfo.MenuType))
                    {
                        menuInfos = new List<CustomMenuInfo>();
                        _catalogMenuIds.Add(customMenuInfo.MenuType, menuInfos);
                    }
                    else
                    {
                        menuInfos = _catalogMenuIds[customMenuInfo.MenuType];
                    }
                    menuInfos.Add(customMenuInfo);

                    IList<ExtendedCustomBusinessInfo> businessInfos = null;
                    if (!_extendedCustomBusinessInfo.ContainsKey(customMenuInfo.MenuId))
                    {
                        businessInfos = new List<ExtendedCustomBusinessInfo>();
                        _extendedCustomBusinessInfo.Add(customMenuInfo.MenuId, businessInfos);
                    }
                    else
                    {
                        businessInfos = _extendedCustomBusinessInfo[customMenuInfo.MenuId];
                    }
                    foreach (ExtendedCustomBusinessInfo extendedCustomBusinessInfo in extendedCustomBusinessInfos)
                    {
                        if (extendedCustomBusinessInfo.MenuId == customMenuInfo.MenuId)
                        {
                            businessInfos.Add(extendedCustomBusinessInfo);
                        }
                    }
                }
            }
            IList<EnumItem> menuAuthorities = UserEnumHelper.GetEnumItems(typeof(MenuAuthority));
            foreach (EnumItem enumItem in menuAuthorities)
            {
                bool result = AuthorityHelper.CheckAuthority(roleAuthority.MenuAuthority, enumItem.Value);
                MenuAuthority menuAuthority = (MenuAuthority)enumItem.Value;
                switch (menuAuthority)
                {
                    case MenuAuthority.Workflow:
                        _workflow = result;
                        break;

                    case MenuAuthority.Auditing:
                        _auditing = result;
                        break;

                    case MenuAuthority.UserAdded:
                        _userAdded = result;
                        break;

                    case MenuAuthority.UserEdited:
                        _userEdited = result;
                        break;

                    case MenuAuthority.DataSwap:
                        _dataSwap = result;
                        break;
                }
            }
            IList<EnumItem> menuSubAuthorities = UserEnumHelper.GetEnumItems(typeof(MenuSubAuthority));
            foreach (EnumItem enumItem in menuSubAuthorities)
            {
                bool result = AuthorityHelper.CheckAuthority(roleAuthority.MenuSubAuthority, enumItem.Value);
                MenuSubAuthority menuSubAuthority = (MenuSubAuthority)enumItem.Value;
                switch (menuSubAuthority)
                {
                    case MenuSubAuthority.PersonalAuditing:
                        _personalAduting = result;
                        break;

                    case MenuSubAuthority.InfoAuditing:
                        _infoAduting = result;
                        break;

                    case MenuSubAuthority.GroupAuditing:
                        _groupAduting = result;
                        break;

                    case MenuSubAuthority.StatisticsQuery:
                        _statisticalDataQueried = result;
                        break;
                }
            }
            
            IList<EnumItem> roleProperties = UserEnumHelper.GetEnumItems(typeof(RoleProperty));
            foreach (EnumItem enumItem in roleProperties)
            {
                bool result = AuthorityHelper.CheckAuthority(roleAuthority.RoleProperty, enumItem.Value);
                RoleProperty roleProperty = (RoleProperty)enumItem.Value;
                switch (roleProperty)
                {
                    case RoleProperty.PassowrdQueried:
                        _passowrdQueried = result;
                        break;
                }
            }
        }

        #endregion
    }
}
