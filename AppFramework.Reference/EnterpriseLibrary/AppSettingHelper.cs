//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppSettingHelper.cs
// 描述: 提供配置文件访问类
// 作者：ChenJie 
// 编写日期：2016-06-29
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using AppFramework.Reference.CustomLibrary;

namespace AppFramework.Reference.EnterpriseLibrary
{
    /// <summary>
    /// 获取系统配置文件的值
    /// </summary>
    public static class AppSettingHelper
    {
        #region 常量

        /// <summary>
        /// 配置文件名称
        /// </summary>
        private const string APP_SETTING_FIELE_NAME = @"EnterpriseLibrary\AppConfigFiles\AppSetting.config";

        #endregion

        #region 配置关键字

        /// <summary>
        /// DAL 程序集的名称的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DAL_ASSEMBLY_NAME = "DAL";

        /// <summary>
        /// WCFServices 程序集的名称的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_WCF_SERVICES = "WCFServices";

        /// <summary>
        /// 用户验证类程序集的名称的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_MEMBERSHIP_PROVIDER = "MembershipProvider";

        /// <summary>
        /// 获得默认文件的格式的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_FILE_FORMAT = "DefaultFileFormat";

        /// <summary>
        /// 默认文件的大小的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_FILE_SIZE = "DefaultFileSize";

        /// <summary>
        /// 获得默认文档的格式的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_DOC_FORMAT = "DefaultDocFormat";

        /// <summary>
        /// 获得默认文档的大小的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_DOC_SIZE = "DefaultDocSize";

        /// <summary>
        /// 获得默认图片的格式的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_PICTURE_FORMAT = "DefaultPictureFormat";

        /// <summary>
        /// 获得默认图片的大小的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_PICTURE_SIZE = "DefaultPictureSize";

        /// <summary>
        /// 获得默认PDF的格式的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_PDF_FORMAT = "DefaultPDFFormat";

        /// <summary>
        /// 获得默认PDF的大小的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_PDF_SIZE = "DefaultPDFSize";

        /// <summary>
        /// 获得 EXCEL 格式的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_EXCEL_FORMAT = "DefaultExcelFormat";

        /// <summary>
        /// 获得 EXCEL 大小的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_EXCEL_SIZE = "DefaultExcelSize";

        /// <summary>
        /// 默认的邮件附件个数
        /// </summary>
        private const string DEFAULT_MAIL_ATTACHMENTS = "DefaultMailAttachments";

        /// <summary>
        /// 默认的邮件图片个数
        /// </summary>
        private const string DEFAULT_MAIL_PICTURE_COUNT = "DefaultMailPictureCount";

        /// <summary>
        /// 默认邮件的图片大小
        /// </summary>
        private const string DEFAULT_MAIL_PICTURE_SIZE = "DefaultMailPictureSize";

        /// <summary>
        /// 默认邮件的内容字数大小
        /// </summary>
        private const string DEFAULT_MAIL_CHARACTERS_SIZE = "DefaultMailCharactersSize";

        /// <summary>
        /// 文档默认的附件个数
        /// </summary>
        private const string DEFAULT_DOC_ATTACHMENTS = "DefaultDocAttachments";

        /// <summary>
        /// 文档默认的图片个数
        /// </summary>
        private const string DEFAULT_DOC_PICTURE_COUNT = "DefaultDocPictureCount";

        /// <summary>
        /// 文档默认的图片大小
        /// </summary>
        private const string DEFAULT_DOC_PICTURE_SIZE = "DefaultDocPictureSize";

        /// <summary>
        /// 文档默认内容字数大小
        /// </summary>
        private const string DEFAULT_DOC_CHARACTERS_SIZE = "DefaultDocCharactersSize";

        /// <summary>
        /// 发送邮件的用户名
        /// </summary>
        private const string DEFAULT_EMIAL_NAME = "DefaultEmailName";

        /// <summary>
        /// 发送邮件的密码
        /// </summary>
        private const string DEFAULT_EMAIL_PASSWORD = "DefaultEmailPassword";

        /// <summary>
        /// SMTP 地址
        /// </summary>
        private const string DEFAULT_SMTP_ADDRESS = "DefaultSmtpAddress";

        /// <summary>
        /// 第三方校验字符串过期时间
        /// </summary>
        private const string DEFAULT_VALIDATOR_SPAN = "DefaultValidatorSpan";

        /// <summary>
        /// 用户名最小长度
        /// </summary>
        private const string DEFAULT_USERNMAE_MIN_LENGTH = "DefaultUserNameMinLength";

        /// <summary>
        /// 用户名最大长度
        /// </summary>
        private const string DEFAULT_USERNMAE_MAX_LENGTH = "DefaultUserNameMaxLength";

        /// <summary>
        /// 密码最小长度
        /// </summary>
        private const string DEFAULT_PASSWORD_MIN_LENGTH = "DefaultPaswordMinLength";

        /// <summary>
        /// 密码最大长度
        /// </summary>
        private const string DEFAULT_PASSWORD_MAX_LENGTH = "DefaultPaswordMaxLength";

        /// <summary>
        /// 用户姓名最小长度
        /// </summary>
        private const string DEFAULT_USER_ACTUAL_NAME_MIN_LENGTH = "DefaultUserActualNameMinLength";

        /// <summary>
        /// 用户姓名最大长度
        /// </summary>
        private const string DEFAULT_USER_ACTUAL_NAME_MAX_LENGTH = "DefaultUserActualNameMaxLength";

        /// <summary>
        /// 身份证号码最大长度
        /// </summary>
        private const string DEFAULT_IDENTITY_MAX_LENGTH = "DefaultIdentityMinLength";

        /// <summary>
        /// 身份证号码最小长度
        /// </summary>
        private const string DEFAULT_IDENTITY_MIN_LENGTH = "DefaultIdentityMaxLength";

        /// <summary>
        /// 用户图片最小长度
        /// </summary>
        private const string DEFAULT_USER_PHOTO_MIN_LENGTH = "DefaultUserPhotoMinLength";

        /// <summary>
        /// 用户图片最大长度
        /// </summary>
        private const string DEFAULT_USER_PHOTO_MAX_LENGTH = "DefaultUserPhotoMaxLength";

        /// <summary>
        /// 实数类型的小数位最大长度
        /// </summary>
        private const string DEFAULT_DECIMAL_DIGIT_MAX_LENGTH = "DefaultDecimalDigitMaxLength";

        /// <summary>
        /// 枚举类型的字符串默认长度
        /// </summary>
        private const string DEFAULT_ENUM_STRING_LENGTH = "DefaultEnumStringLength";

        /// <summary>
        /// 多选枚举类型的字符串默认长度
        /// </summary>
        private const string DEFAULT_MULTI_ENUM_STRING_LENGTH = "DefaultMultiEnumStringLength";        

        /// <summary>
        /// 用户锁定最小间隔时间(单位：小时)
        /// </summary>
        private const string DEFAULT_USER_LOCKED_MIN_SPAN = "DefaultUserLockedMinSpan";

        /// <summary>
        /// 用户登录重试次数
        /// </summary>
        private const string DEFAULT_RETRY_TIMES = "DefaultRetryTimes";

        /// <summary>
        /// 人事办公自动化平台
        /// </summary>
        private const string DEFAULT_SYSTEM_NAME = "DefaultSystemName";

        /// <summary>
        /// 用户名标签
        /// </summary>
        private const string DEFAULT_USER_NAME_LABEL = "UserNameLabelInfo";

        /// <summary>
        /// 客户端
        /// </summary>
        private const string DEFAULT_SYSTEM_CLIENT_NAME = "DefaultSystemClientName";

        /// <summary>
        /// 服务器端
        /// </summary>
        private const string DEFAULT_SYSTEM_SERVER_NAME = "DefaultSystemServerName";

        /// <summary>
        /// Web端
        /// </summary>
        private const string DEFAULT_SYSTEM_WEB_NAME = "DefaultSystemWebName";

        /// <summary>
        /// 用户工具
        /// </summary>
        private const string DEFAULT_USER_TOOL_NAME = "DefaultUserToolName";

        /// <summary>
        /// 默认注册工具名称
        /// </summary>
        private const string DEFAULT_USER_REGISTER = "DefaultUserRegister";

        /// <summary>
        /// 试用天数
        /// </summary>
        private const string TRIAL_DAYS = "TrialDays";

        /// <summary>
        /// 服务器端的 Windows Service 名称的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_WINDOWS_SERVICE_NAME = "WindowsServiceName";

        /// <summary>
        /// Windows Service 启动时间的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_WINODWS_SERVICE_START_TIME = "WindowsServiceStartTime";

        /// <summary>
        /// 客户端版本号
        /// </summary>
        private const string CLINET_VERSION = "ClientVersion";

        /// <summary>
        /// 客户端发布日期
        /// </summary>
        private const string CLIENT_RELEASED_DATE = "ClientReleasedDate";

        /// <summary>
        /// 服务器端版本号
        /// </summary>
        private const string SERVER_VERSION = "ServerVersion";

        /// <summary>
        /// 服务器端发布日期
        /// </summary>
        private const string SERVER_RELEASED_DATE = "ServerReleasedDate";

        /// <summary>
        /// 当前服务器端兼容的客户端版本号
        /// </summary>
        private const string COMPATIBLE_CLINET_VERSIONS = "CompatibleClientVersions";

        /// <summary>
        /// 客户端注册码
        /// </summary>
        private const string CLIENT_REGISTER_CODE = "ClientRegisterCode";

        /// <summary>
        /// 服务器端注册码
        /// </summary>
        private const string SERVER_REGISTER_CODE = "ServerRegisterCode";

        /// <summary>
        /// 域名关键字
        /// </summary>
        private const string DOMAIN_NAME = "DomainName";

        /// <summary>
        /// 数据备份目录
        /// </summary>
        private const string DATA_BACKIUP_DIRECTORY = "DataBackupDirectory";

        /// <summary>
        /// 用户保存所有文件的默认根目录的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_ROOT_DIR_OF_SAVED_FILES = "DefaultRootDirOfSavedFiles";

        /// <summary>
        /// 用户保存照片的默认子目录的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_PHOTOS = "DefaultSubDirOfSavedPhotos";

        /// <summary>
        /// 文件上传的默认子目录的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_UPLAOD_FILES = "DefaultSubDirOfUploadFiles";

        /// <summary>
        /// 用户保存消息附件(邮件、通知、即时消息等)的默认子目录的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_MESSAGE_FILESS = "DefaultSubDirOfSavedMessageFiles";

        /// <summary>
        /// 保存报表表套文件的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_REPORTING_FILESS = "DefaultSubDirOfReportingFiles";

        /// <summary>
        /// 保存报表表套快照文件的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_REPORTING_SNAPSHOT_FILESS = "DefaultSubDirOfReportingSnapshotFiles";

        /// <summary>
        /// 用户数据备份的默认子目录的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_BACKUP_FILES = "DefaultSubDirOfBackupFiles";

        /// <summary>
        /// 菜单图片的默认子目录的关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_ICONS = "DefaultSubDirOfSavedIcons";

        /// <summary>
        /// 模板目录关键字
        /// </summary>
        private const string DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_TEMPLATE_FILES = "DefaultSubDirOfTemplateFiles";

        /// <summary>
        /// 客户端保存临时文件的子目录
        /// </summary>
        private const string DEFAULT_CLIENT_TMP_DIR_OF_SAVED__FILES = "DefaultClientTmpDirOfSavedFiles";

        /// <summary>
        /// 默认的年的值
        /// </summary>
        private const string YEAR = "Year";

        /// <summary>
        /// 默认的年月日的值
        /// </summary>
        private const string YEAR_MONTH_DAY = "YearMonthDay";

        /// <summary>
        /// Windows 服务启动异常内容
        /// </summary>
        private const string WINDOWS_SERVICE_EXCEPTION = "WindowsServiceException";

        /// <summary>
        /// Web API 服务异常内容 
        /// </summary>
        private const string WEB_API_EXCEPTION = "WEB_API_EXCEPTION";

        /// <summary>
        /// WebAPI 单点登录验证端口
        /// </summary>
        private const string WEB_API_VALIDATION_PORT = "WebAPIValidationPort";

        /// <summary>
        /// WebAPI 数据访问端口
        /// </summary>
        private const string WEB_API_DATA_PORT = "WebAPIDataPort";

        /// <summary>
        /// 每级枚举编码的长度
        /// </summary>
        private const string ENUM_CODE_LENGTH = "EnumCodeLength";

        /// <summary>
        /// 每页记录数
        /// </summary>
        private const string DEFAULT_PAGE_SIZE = "DefaultPageSize";

        /// <summary>
        /// Web 每页记录数
        /// </summary>
        private const string DEFAULT_WEB_PAGE_SIZE = "DefaultWebPageSize";         

        /// <summary>
        /// Web子菜单节点图片
        /// </summary>
        private const string DEFAULT_ICON_ON_SUB_MENU = "DefaultIconOnSubMenu";

        /// <summary>
        /// 对第三提供接口
        /// </summary>
        private const string ENABLE_INTERFACE = "EnableInterface";

        /// <summary>
        /// 四川大学名称
        /// </summary>
        private const string SCU_COMPANY_NAME = "ScuCompanyName";

        /// <summary>
        /// 公司名称
        /// </summary>
        private const string DEFAULT_COMPANY_NAME = "DefaultCompanyName";

        /// <summary>
        /// 最新备份时间
        /// </summary>
        private const string LASTEST_ABCKUP_TIME = "LastestBackupTime";

        /// <summary>
        /// 备份异常信息
        /// </summary>
        private const string BACKUP_EXCEPTION = "BackupException";

        /// <summary>
        /// 新的WEB服务器地址
        /// </summary>
        private const string NEW_WEB_SERVER_ADDRESS = "NewWebServerAddress";

        /// <summary>
        /// 单点登录地址
        /// </summary>
        private const string SINGLE_SIGN_ADDRESS = "SingleSignAddress";
                
        /// <summary>
        /// 单点登录接口
        /// </summary>
        private const string SINGLE_SIGN_INTERFACE = "SingleSignInterface";

        /// <summary>
        /// SSO：唯一编码的名称
        /// </summary>
        private const string UNIQUE_CODE_NAME = "UniqueCodeName";

        /// <summary>
        /// SSO：客户端编号
        /// </summary>
        private const string SSO_CLIENT_ID = "SSOClientId";

        /// <summary>
        /// SSO：客户端密码
        /// </summary>
        private const string SSO_CLIENT_PASSWORD = "SSOPassword";

        /// <summary>
        /// Web地址
        /// </summary>
        private const string WEB_ADDRESS = "WebAddress";

        /// <summary>
        /// SSO：校验地址
        /// </summary>
        private const string SSO_AVLIDATION_ADDRESS = "SSOValidationAddress";

        /// <summary>
        /// SSO：接口地址
        /// </summary>
        private const string SSO_INTERFACE_ADDRESS = "SSOInterfaceAddress";

        #endregion

        #region 静态属性

        #region 配置文件的绝对路径（只读）

        /// <summary>
        /// 配置文件的绝对路径
        /// </summary>
        public static string AppSettingFullFileName
        {
            get
            {
                return appSettingFullFileName;
            }
        }

        /// <summary>
        /// 域名名称
        /// </summary>
        public static string DomainNameCaption
        {
            get
            {
                return DOMAIN_NAME;
            }
        }

        #endregion

        #region 值

        /// <summary>
        /// DAL 程序集的名称
        /// </summary>
        public static string DALAssemblyName
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DAL_ASSEMBLY_NAME);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DAL_ASSEMBLY_NAME, value);
            }
        }

        /// <summary>
        ///  WCFServices 程序集的名称
        /// </summary>
        /// <returns></returns>
        public static string WCFServicesName
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_WCF_SERVICES);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_WCF_SERVICES, value);
            }
        }

        /// <summary>
        /// 用户验证类程序集的名称
        /// </summary>
        /// <returns></returns>
        public static string MembershipProviderName
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_MEMBERSHIP_PROVIDER);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_MEMBERSHIP_PROVIDER, value);
            }
        }

        /// <summary>
        /// 默认文件的格式
        /// </summary>
        public static string DefaultFileFormat
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_FILE_FORMAT);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_FILE_FORMAT, value);
            }
        }

        /// <summary>
        /// 默认文件的大小
        /// </summary>
        /// <returns></returns>
        public static int DefaultFileSize
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_FILE_SIZE));
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_FILE_SIZE, value.ToString());
            }
        }

        /// <summary>
        /// 获得默认文档的格式
        /// </summary>
        /// <returns></returns>
        public static string DefaultDocFormat
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_DOC_FORMAT);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_DOC_FORMAT, value);
            }
        }

        /// <summary>
        /// 获得默认文档的大小
        /// </summary>
        /// <returns></returns>
        public static Int64 DefaultDocSize
        {
            get
            {
                return Convert.ToInt64(GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_DOC_SIZE));
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_DOC_SIZE, value.ToString());
            }
        }

        /// <summary>
        /// 获得默认图片的格式
        /// </summary>
        /// <returns></returns>
        public static string DefaultPictureFormat
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_PICTURE_FORMAT);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_PICTURE_FORMAT, value);
            }
        }

        /// <summary>
        /// 获得默认图片的大小
        /// </summary>
        /// <returns></returns>
        public static int DefaultPictureSize
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_PICTURE_SIZE));
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_PICTURE_SIZE, value.ToString());
            }
        }

        /// <summary>
        /// 获得默认PDF的格式
        /// </summary>
        /// <returns></returns>
        public static string DefaultPDFFormat
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_PDF_FORMAT);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_PDF_FORMAT, value);
            }
        }

        /// <summary>
        /// 获得默认PDF的大小
        /// </summary>
        /// <returns></returns>
        public static int DefaultPDFSize
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_PDF_SIZE));
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_PDF_SIZE, value.ToString());
            }
        }

        /// <summary>
        /// 获得默认EXCEL的格式
        /// </summary>
        /// <returns></returns>
        public static string DefaultExcelFormat
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_EXCEL_FORMAT);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_EXCEL_FORMAT, value);
            }
        }

        /// <summary>
        /// 获得默认EXCEL的大小
        /// </summary>
        /// <returns></returns>
        public static int DefaultExcelSize
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_EXCEL_SIZE));
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_EXCEL_SIZE, value.ToString());
            }
        }


        /// <summary>
        /// 获得默认邮件附件个数
        /// </summary>
        /// <returns></returns>
        public static int DefaultMailAttachments
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_MAIL_ATTACHMENTS));
            }
            set
            {
                SetAppSettingByName(DEFAULT_MAIL_ATTACHMENTS, value.ToString());
            }
        }

        /// <summary>
        /// 默认的邮件图片个数
        /// </summary>
        /// <returns></returns>
        public static int DefaultMailPictureCount
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_MAIL_PICTURE_COUNT));
            }
            set
            {
                SetAppSettingByName(DEFAULT_MAIL_PICTURE_COUNT, value.ToString());
            }
        }

        /// <summary>
        /// 默认邮件的图片大小
        /// </summary>
        /// <returns></returns>
        public static int DefaultMailPictureSize
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_MAIL_PICTURE_SIZE));
            }
            set
            {
                SetAppSettingByName(DEFAULT_MAIL_PICTURE_SIZE, value.ToString());
            }
        }

        /// <summary>
        /// 默认邮件的内容字数大小
        /// </summary>
        /// <returns></returns>
        public static int DefaultMailCharactersSize
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_MAIL_CHARACTERS_SIZE));
            }
            set
            {
                SetAppSettingByName(DEFAULT_MAIL_CHARACTERS_SIZE, value.ToString());
            }
        }

        /// <summary>
        /// 文档默认的附件个数
        /// </summary>
        /// <returns></returns>
        public static int DefaultDocAttachments
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_DOC_ATTACHMENTS));
            }
            set
            {
                SetAppSettingByName(DEFAULT_DOC_ATTACHMENTS, value.ToString());
            }
        }

        /// <summary>
        /// 文档默认的图片个数
        /// </summary>
        /// <returns></returns>
        public static int DefaultDocPictureCount
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_DOC_PICTURE_COUNT));
            }
            set
            {
                SetAppSettingByName(DEFAULT_DOC_PICTURE_COUNT, value.ToString());
            }
        }

        /// <summary>
        /// 文档默认的图片大小
        /// </summary>
        /// <returns></returns>
        public static int DefaultDocPictureSize
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_DOC_PICTURE_SIZE));
            }
            set
            {
                SetAppSettingByName(DEFAULT_DOC_PICTURE_SIZE, value.ToString());
            }
        }

        /// <summary>
        /// 文档默认内容字数大小
        /// </summary>
        /// <returns></returns>
        public static int DefaultDocCharactersSize
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_DOC_CHARACTERS_SIZE));
            }
            set
            {
                SetAppSettingByName(DEFAULT_DOC_CHARACTERS_SIZE, value.ToString());
            }
        }

        /// <summary>
        /// 发送邮件的用户名
        /// </summary>
        public static string DefaultEmailName
        {
            get
            {
                return GetAppSettingByName(DEFAULT_EMIAL_NAME);
            }
            set
            {
                SetAppSettingByName(DEFAULT_EMIAL_NAME, value);
            }
        }

        /// <summary>
        /// 发送邮件的密码
        /// </summary>
        public static string DefaultEmailPassword
        {
            get
            {
                return GetAppSettingByName(DEFAULT_EMAIL_PASSWORD);
            }
            set
            {
                SetAppSettingByName(DEFAULT_EMAIL_PASSWORD, value);
            }
        }

        /// <summary>
        /// SMTP 地址
        /// </summary>
        public static string DefaultSmtpAddress
        {
            get
            {
                return GetAppSettingByName(DEFAULT_SMTP_ADDRESS);
            }
            set
            {
                SetAppSettingByName(DEFAULT_SMTP_ADDRESS, value);
            }
        }

        /// <summary>
        /// 第三方校验字符串过期时间
        /// </summary>
        public static int DefaultValidatorSpan
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_VALIDATOR_SPAN));
            }
            set
            {
                SetAppSettingByName(DEFAULT_VALIDATOR_SPAN, value.ToString());
            }
        }
        
        /// <summary>
        /// 用户名最小长度
        /// </summary>
        public static int DefaultUserNameMinLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_USERNMAE_MIN_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_USERNMAE_MIN_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 用户名最大长度
        /// </summary>
        public static int DefaultUserNameMaxLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_USERNMAE_MAX_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_USERNMAE_MAX_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 密码最小长度
        /// </summary>
        public static int DefaultPaswordMinLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_PASSWORD_MIN_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_USERNMAE_MIN_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 密码最大长度
        /// </summary>
        public static int DefaultPaswordMaxLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_PASSWORD_MAX_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_USERNMAE_MAX_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 用户姓名最小长度
        /// </summary>
        public static int DefaultUserActualNameMinLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_USER_ACTUAL_NAME_MIN_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_USER_ACTUAL_NAME_MIN_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 用户姓名最大长度
        /// </summary>
        public static int DefaultUserActualNameMaxLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_USER_ACTUAL_NAME_MAX_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_USER_ACTUAL_NAME_MAX_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 身份证号码最小长度
        /// </summary>
        public static int DefaultIdentityMinLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_IDENTITY_MAX_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_IDENTITY_MAX_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 身份证号码最大长度
        /// </summary>
        public static int DefaultIdentityMaxLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_IDENTITY_MIN_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_IDENTITY_MIN_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 用户图片最小长度
        /// </summary>
        public static int DefaultUserPhotoMinLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_USER_PHOTO_MIN_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_USER_PHOTO_MIN_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 用户图片最大长度
        /// </summary>
        public static int DefaultUserPhotoMaxLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_USER_PHOTO_MAX_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_USER_PHOTO_MAX_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 实数类型的小数位最大长度
        /// </summary>
        public static int DefaultDecimalDigitMaxLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_DECIMAL_DIGIT_MAX_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_DECIMAL_DIGIT_MAX_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 枚举类型的字符串默认长度
        /// </summary>
        public static int DefaultEnumStringLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_ENUM_STRING_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_ENUM_STRING_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 多选枚举类型的字符串默认长度
        /// </summary>
        public static int DefaultMultiEnumStringLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_MULTI_ENUM_STRING_LENGTH));
            }
            set
            {
                SetAppSettingByName(DEFAULT_MULTI_ENUM_STRING_LENGTH, value.ToString());
            }
        }        

        /// <summary>
        /// 用户锁定最小间隔时间(单位：小时)
        /// </summary>
        public static int DefaultUserLockedMinSpan
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_USER_LOCKED_MIN_SPAN));
            }
            set
            {
                SetAppSettingByName(DEFAULT_USER_LOCKED_MIN_SPAN, value.ToString());
            }
        }

        /// <summary>
        /// 用户登录重试次数
        /// </summary>
        public static int DefaultRetryTimes
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_RETRY_TIMES));
            }
            set
            {
                SetAppSettingByName(DEFAULT_RETRY_TIMES, value.ToString());
            }
        }

        /// <summary>
        /// 人事办公自动化平台
        /// </summary>
        public static string DefaultSystemName
        {
            get
            {
                return GetAppSettingByName(DEFAULT_SYSTEM_NAME);
            }
            set
            {
                SetAppSettingByName(DEFAULT_SYSTEM_NAME, value);
            }
        }

        /// <summary>
        /// 用户名标签
        /// </summary>
        public static string UserNameLabelInfo
        {
            get
            {
                return GetAppSettingByName(DEFAULT_USER_NAME_LABEL);
            }
            set
            {
                SetAppSettingByName(DEFAULT_USER_NAME_LABEL, value);
            }
        }
                

        /// <summary>
        /// 客户端
        /// </summary>
        public static string DefaultSystemClientName
        {
            get
            {
                return GetAppSettingByName(DEFAULT_SYSTEM_CLIENT_NAME);
            }
            set
            {
                SetAppSettingByName(DEFAULT_SYSTEM_CLIENT_NAME, value);
            }
        }

        /// <summary>
        /// 服务器端
        /// </summary>
        public static string DefaultSystemServerName
        {
            get
            {
                return GetAppSettingByName(DEFAULT_SYSTEM_SERVER_NAME);
            }
            set
            {
                SetAppSettingByName(DEFAULT_SYSTEM_SERVER_NAME, value);
            }
        }

        /// <summary>
        /// Web端
        /// </summary>
        public static string DefaultSystemWebName
        {
            get
            {
                return GetAppSettingByName(DEFAULT_SYSTEM_WEB_NAME);
            }
            set
            {
                SetAppSettingByName(DEFAULT_SYSTEM_WEB_NAME, value);
            }
        }

        /// <summary>
        /// 用户工具
        /// </summary>
        public static string DefaultUserToolName
        {
            get
            {
                return GetAppSettingByName(DEFAULT_USER_TOOL_NAME);
            }
            set
            {
                SetAppSettingByName(DEFAULT_USER_TOOL_NAME, value);
            }
        }

        /// <summary>
        /// 默认注册工具名称
        /// </summary>
        public static string DefaultUserRegister
        {
            get
            {
                return GetAppSettingByName(DEFAULT_USER_REGISTER);
            }
            set
            {
                SetAppSettingByName(DEFAULT_USER_REGISTER, value);
            }
        }

        /// <summary>
        /// 试用版本时间
        /// </summary>
        public static int TrialDays
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(TRIAL_DAYS));
            }
            set
            {
                SetAppSettingByName(TRIAL_DAYS, value.ToString());
            }
        }

        /// <summary>
        /// 服务器端的 Windows Service 名称
        /// </summary>
        /// <returns></returns>
        public static string WindowsServiceName
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_WINDOWS_SERVICE_NAME);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_WINDOWS_SERVICE_NAME, value);
            }
        }

        /// <summary>
        /// Windows Service 启动时间
        /// </summary>
        /// <returns></returns>
        public static string WindowsServiceStartTime
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_WINODWS_SERVICE_START_TIME);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_WINODWS_SERVICE_START_TIME, value);
            }
        }

        /// <summary>
        /// 客户端版本号
        /// </summary>
        public static string ClientVersion
        {
            get
            {
                return GetAppSettingByName(CLINET_VERSION);
            }
            set
            {
                SetAppSettingByName(CLINET_VERSION, value);
            }
        }

        /// <summary>
        /// 客户端发布日期
        /// </summary>
        public static string ClientReleasedDate
        {
            get
            {
                return GetAppSettingByName(CLIENT_RELEASED_DATE);
            }
            set
            {
                SetAppSettingByName(CLIENT_RELEASED_DATE, value);
            }
        }

        /// <summary>
        /// 服务器端版本号
        /// </summary>
        public static string ServerVersion
        {
            get
            {
                return GetAppSettingByName(SERVER_VERSION);
            }
            set
            {
                SetAppSettingByName(SERVER_VERSION, value);
            }
        }

        /// <summary>
        /// 服务器端发布日期
        /// </summary>
        public static string ServerReleasedDate
        {
            get
            {
                return GetAppSettingByName(SERVER_RELEASED_DATE);
            }
            set
            {
                SetAppSettingByName(SERVER_RELEASED_DATE, value);
            }
        }

        /// <summary>
        /// 当前服务器端兼容的客户端版本号
        /// </summary>
        public static string CompatibleClientVersions
        {
            get
            {
                return GetAppSettingByName(COMPATIBLE_CLINET_VERSIONS);
            }
            set
            {
                SetAppSettingByName(COMPATIBLE_CLINET_VERSIONS, value);
            }
        }

        /// <summary>
        /// 客户端注册码
        /// </summary>
        public static string ClientRegisterCode
        {
            get
            {
                return GetAppSettingByName(CLIENT_REGISTER_CODE);
            }
            set
            {
                SetAppSettingByName(CLIENT_REGISTER_CODE, value);
            }
        }

        /// <summary>
        /// 服务器端注册码
        /// </summary>
        public static string ServerRegisterCode
        {
            get
            {
                return GetAppSettingByName(SERVER_REGISTER_CODE);
            }
            set
            {
                SetAppSettingByName(SERVER_REGISTER_CODE, value);
            }
        }

        /// <summary>
        /// 域名关键字
        /// </summary>
        public static string DomainName
        {
            get
            {
                return GetAppSettingByName(DOMAIN_NAME);
            }
            set
            {
                SetAppSettingByName(DOMAIN_NAME, value);
            }
        }

        /// <summary>
        /// 数据备份目录
        /// </summary>
        public static string DataBackupDirectory
        {
            get
            {
                return GetAppSettingByName(DATA_BACKIUP_DIRECTORY);
            }
            set
            {
                SetAppSettingByName(DATA_BACKIUP_DIRECTORY, value);
            }
        }

        /// <summary>
        /// Web 目录
        /// 服务器端和 Web 端目录共享，因此 Web 目录 与 DefaultRootDirOfSavedFiles 一致
        /// </summary>
        public static string WebDirectory
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_ROOT_DIR_OF_SAVED_FILES);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_ROOT_DIR_OF_SAVED_FILES, value);
            }
        }

        /// <summary>
        /// 默认值的用户保存所有文件的默认根目
        /// </summary>
        /// <returns></returns>
        public static string DefaultRootDirOfSavedFiles
        {
            get
            {
                if (string.IsNullOrWhiteSpace(WebDirectory))
                {
                    return AppDomain.CurrentDomain.BaseDirectory;
                }

                return WebDirectory;
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_ROOT_DIR_OF_SAVED_FILES, value);
            }
        }

        /// <summary>
        /// 用户保存照片的默认子目录
        /// </summary>
        /// <returns></returns>
        public static string DefaultSubDirOfSavedPhotos
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_PHOTOS);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_PHOTOS, value);
            }
        }

        /// <summary>
        /// 文件上传的默认子目录
        /// </summary>
        /// <returns></returns>
        public static string DefaultSubDirOfUploadFiles
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_UPLAOD_FILES);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_UPLAOD_FILES, value);
            }
        }

        /// <summary>
        /// 用户保存消息附件(邮件、通知、即时消息等)的默认子目录
        /// </summary>
        /// <returns></returns>
        public static string DefaultSubDirOfSavedMessageFiles
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_MESSAGE_FILESS);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_MESSAGE_FILESS, value);
            }
        }

        /// <summary>
        /// 保存报表表套文件的默认子目录
        /// </summary>
        public static string DefaultSubDirOfReportingFiles
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_REPORTING_FILESS);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_REPORTING_FILESS, value);
            }
        }

        /// <summary>
        /// 保存报表表套快照文件的默认子目录
        /// </summary>
        public static string DefaultSubDirOfReportingSnapshotFiles
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_REPORTING_SNAPSHOT_FILESS);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_REPORTING_SNAPSHOT_FILESS, value);
            }
        }

        /// <summary>
        /// 用户数据备份的默认子目录的关键字
        /// </summary>
        /// <returns></returns>
        public static string DefaultSubDirOfBackupFiles
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_BACKUP_FILES);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_BACKUP_FILES, value);
            }
        }

        /// <summary>
        /// 菜单图片的默认子目录的关键字
        /// </summary>
        /// <returns></returns>
        public static string DefaultSubDirOfSavedIcons
        {
            get
            {
                return GetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_ICONS);
            }
            set
            {
                SetAppSettingByName(DEFAULT_KEY_OF_DEFAULT_SUB_DIR_OF_SAVED_ICONS, value);
            }
        }



        /// <summary>
        /// 客户端保存临时文件的子目录
        /// </summary>
        public static string DefaultClientTmpDirOfSavedFiles
        {
            get
            {
                return GetAppSettingByName(DEFAULT_CLIENT_TMP_DIR_OF_SAVED__FILES);
            }
            set
            {
                SetAppSettingByName(DEFAULT_CLIENT_TMP_DIR_OF_SAVED__FILES, value);
            }
        }

        /// <summary>
        /// 默认的年的值
        /// </summary>
        public static string Year
        {
            get
            {
                return GetAppSettingByName(YEAR);
            }
            set
            {
                SetAppSettingByName(YEAR, value);
            }
        }

        /// <summary>
        /// 默认的年月日的值
        /// </summary>
        public static string YearMonthDay
        {
            get
            {
                return GetAppSettingByName(YEAR_MONTH_DAY);
            }
            set
            {
                SetAppSettingByName(YEAR_MONTH_DAY, value);
            }
        }

        /// <summary>
        /// Windows 服务启动异常内容
        /// </summary>
        public static string WindowsServiceException
        {
            get
            {
                return GetAppSettingByName(WINDOWS_SERVICE_EXCEPTION);
            }
            set
            {
                SetAppSettingByName(WINDOWS_SERVICE_EXCEPTION, value);
            }
        }

        /// <summary>
        /// Web API 服务异常内容
        /// </summary>
        public static string WebAPIException
        {
            get
            {
                return GetAppSettingByName(WEB_API_EXCEPTION);
            }
            set
            {
                SetAppSettingByName(WEB_API_EXCEPTION, value);
            }
        }



        /// <summary>
        /// WebAPI 单点登录验证端口
        /// </summary>
        public static string WebAPIValidationPort
        {
            get
            {
                return GetAppSettingByName(WEB_API_VALIDATION_PORT);
            }
            set
            {
                SetAppSettingByName(WEB_API_VALIDATION_PORT, value);
            }
        }

        /// <summary>
        /// WebAPI 数据访问端口
        /// </summary>
        public static string WebAPIDataPort
        {
            get
            {
                return GetAppSettingByName(WEB_API_DATA_PORT);
            }
            set
            {
                SetAppSettingByName(WEB_API_DATA_PORT, value);
            }
        }

        /// <summary>
        /// 每级枚举编码的长度
        /// </summary>
        public static int EnumCodeLength
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(ENUM_CODE_LENGTH));
            }
            set
            {
                SetAppSettingByName(ENUM_CODE_LENGTH, value.ToString());
            }
        }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public static int DefaultPageSize
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_PAGE_SIZE));
            }
            set
            {
                SetAppSettingByName(DEFAULT_PAGE_SIZE, value.ToString());
            }
        }

        /// <summary>
        /// Web 每页记录数
        /// </summary>
        public static int DefaultWebPageSize
        {
            get
            {
                return Convert.ToInt32(GetAppSettingByName(DEFAULT_WEB_PAGE_SIZE));
            }
            set
            {
                SetAppSettingByName(DEFAULT_WEB_PAGE_SIZE, value.ToString());
            }
        }        

        /// <summary>
        /// Web子菜单节点图片
        /// </summary>
        public static string DefaultIconOnSubMenu
        {
            get
            {
                return GetAppSettingByName(DEFAULT_ICON_ON_SUB_MENU);
            }
            set
            {
                SetAppSettingByName(DEFAULT_ICON_ON_SUB_MENU, value);
            }
        }

        /// <summary>
        /// 对第三提供接口
        /// </summary>
        public static bool EnableInterface
        {
            get
            {
                return Convert.ToBoolean(Convert.ToByte(GetAppSettingByName(ENABLE_INTERFACE)));
            }
            set
            {
                SetAppSettingByName(ENABLE_INTERFACE, value ? "1" : "0");
            }
        }

        /// <summary>
        /// 四川大学名称
        /// </summary>
        public static string ScuCompanyName
        {
            get
            {
                return GetAppSettingByName(SCU_COMPANY_NAME);
            }
            set
            {
                SetAppSettingByName(SCU_COMPANY_NAME, value);
            }
        }

        /// <summary>
        /// 公司名称
        /// </summary>
        public static string DefaultCompanyName
        {
            get
            {
                return GetAppSettingByName(DEFAULT_COMPANY_NAME);
            }
            set
            {
                SetAppSettingByName(DEFAULT_COMPANY_NAME, value);
            }
        }

        /// <summary>
        /// 最新备份时间
        /// </summary>
        public static string LastestBackupTime
        {
            get
            {
                return GetAppSettingByName(LASTEST_ABCKUP_TIME);
            }
            set
            {
                SetAppSettingByName(LASTEST_ABCKUP_TIME, value);
            }
        }

        /// <summary>
        /// 备份异常信息
        /// </summary>
        public static string BackupException
        {
            get
            {
                return GetAppSettingByName(BACKUP_EXCEPTION);
            }
            set
            {
                SetAppSettingByName(BACKUP_EXCEPTION, value);
            }
        }

        /// <summary>
        /// 新的Web登录地址
        /// </summary>
        public static string NewWebServerAddress
        {
            get
            {
                return GetAppSettingByName(NEW_WEB_SERVER_ADDRESS);
            }
            set
            {
                SetAppSettingByName(NEW_WEB_SERVER_ADDRESS, value);
            }
        }

        /// <summary>
        /// 单点登录地址
        /// </summary>
        public static string SingleSignAddress
        {
            get
            {
                return GetAppSettingByName(SINGLE_SIGN_ADDRESS);
            }
            set
            {
                SetAppSettingByName(SINGLE_SIGN_ADDRESS, value);
            }
        }

        

        /// <summary>
        /// 单点登录接口
        /// </summary>
        public static string SingleSignInterface
        {
            get
            {
                return GetAppSettingByName(SINGLE_SIGN_INTERFACE);
            }
            set
            {
                SetAppSettingByName(SINGLE_SIGN_INTERFACE, value);
            }
        }

        /// <summary>
        ///  SSO：唯一编码的名称
        /// </summary>
        public static string UniqueCodeName
        {
            get
            {
                return GetAppSettingByName(UNIQUE_CODE_NAME);
            }
            set
            {
                SetAppSettingByName(UNIQUE_CODE_NAME, value);
            }
        }

        /// <summary>
        /// SSO：客户端编号
        /// </summary>
        public static string SSOClientId
        {
            get
            {
                return GetAppSettingByName(SSO_CLIENT_ID);
            }
            set
            {
                SetAppSettingByName(SSO_CLIENT_ID, value);
            }
        }

        /// <summary>
        /// SSO：客户端密码
        /// </summary>
        public static string SSOPassword
        {
            get
            {
                return GetAppSettingByName(SSO_CLIENT_PASSWORD);
            }
            set
            {
                SetAppSettingByName(SSO_CLIENT_PASSWORD, value);
            }
        }
        
        /// <summary>
        /// Web地址
        /// </summary>
        public static string WebAddress
        {
            get
            {
                return GetAppSettingByName(WEB_ADDRESS);
            }
            set
            {
                SetAppSettingByName(WEB_ADDRESS, value);
            }
        }

        /// <summary>
        /// SSO：校验地址
        /// </summary>
        public static string SSOValidationAddress
        {
            get
            {
                return GetAppSettingByName(SSO_AVLIDATION_ADDRESS);
            }
            set
            {
                SetAppSettingByName(SSO_AVLIDATION_ADDRESS, value);
            }
        }

        /// <summary>
        /// SSO：接口地址
        /// </summary>
        public static string SSOInterfaceAddress
        {
            get
            {
                return GetAppSettingByName(SSO_INTERFACE_ADDRESS);
            }
            set
            {
                SetAppSettingByName(SSO_INTERFACE_ADDRESS, value);
            }
        }


        #endregion

        #region 只读静态变量

        /// <summary>
        /// 配置文件的完整路径名称
        /// </summary>
        private static readonly string appSettingFullFileName;

        /// <summary>
        /// 配置文件
        /// </summary>
        private static readonly Configuration config;

        /// <summary>
        /// 配置文件段
        /// </summary>
        private static readonly AppSettingsSection appSettingsSection;

        /// <summary>
        /// 缓存对象
        /// </summary>
        private static readonly CustomFileCache customFileCache;

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object obj = new object();

        #endregion

        #region 构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static AppSettingHelper()
        {

            try
            {
                appSettingFullFileName = GetAppSettingFullFileName(AppDomain.CurrentDomain.BaseDirectory);
                if (File.Exists(appSettingFullFileName))
                {
                    ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                    file.ExeConfigFilename = appSettingFullFileName;
                    config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                    appSettingsSection = (AppSettingsSection)config.GetSection("appSettings");
                    customFileCache = new CustomFileCache(appSettingFullFileName);
                }
                else
                {
                    throw new Exception("自定义配置文件路径为空。");
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.NotifyRethrowNoWrapPolicy(ex);
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得 AppSetting.config 的路径
        /// </summary>
        /// <param name="baseDirectory"></param>
        /// <returns></returns>
        public static string GetAppSettingFullFileName(string baseDirectory)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(baseDirectory);
            if (!baseDirectory.EndsWith(@"\"))
            {
                sb.Append(@"\");
            }
            sb.Append(APP_SETTING_FIELE_NAME);

            return sb.ToString();
        }

        /// <summary>
        /// 获得配置文件的对应值
        /// </summary>
        /// <param name="name">关键字</param>
        /// <returns>值</returns>
        public static string GetAppSettingByName(string name)
        {
            string value = string.Empty;
            try
            {
                /*  缓存中不存在则从文件中读取, 否则从缓存中读取 */
                lock (obj)
                {
                    if (!customFileCache.Contains(name))
                    {
                        value = appSettingsSection.Settings[name].Value;
                        customFileCache[name] = value;
                    }
                    else
                    {
                        value = customFileCache[name].ToString();
                    }
                }

                return value;
            }
            catch { }

            return value;
        }

        /// <summary>
        /// 设置配置文件的对应值
        /// </summary>
        /// <param name="name">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void SetAppSettingByName(string name, string value)
        {
            try
            {
                lock (obj)
                {
                    /* 1.保存到文件中 */
                    appSettingsSection.Settings[name].Value = value;
                    config.Save(ConfigurationSaveMode.Modified);

                    /* 2. 刷新缓存内容 */
                    customFileCache[name] = value;
                }
            }
            catch { }
        }

        #endregion

        #region 读取和保存关键字的方法

        /// <summary>
        /// 设置配置文件的对应值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void SetAppSettingByName(string path, string name, string value)
        {
            try
            {
                ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                file.ExeConfigFilename = appSettingFullFileName;
                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                AppSettingsSection settingsSection = (AppSettingsSection)config.GetSection("appSettings");

                lock (obj)
                {
                    /* 1.保存到文件中 */
                    settingsSection.Settings[name].Value = value;
                    config.Save(ConfigurationSaveMode.Modified);
                }
            }
            catch { }
        }

        /// <summary>
        /// 获得配置文件的对应值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">关键字</param>
        /// <returns>值</returns>
        public static string GetAppSettingByName(string path, string name)
        {
            string value = string.Empty;
            try
            {
                ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                file.ExeConfigFilename = appSettingFullFileName;
                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                AppSettingsSection settingsSection = (AppSettingsSection)config.GetSection("appSettings");
                lock (obj)
                {
                    value = appSettingsSection.Settings[name].Value;
                }

                return value;
            }
            catch { }

            return value;
        }


        #endregion

        #endregion
    }
}
