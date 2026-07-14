using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Blue.WebAPI
{
    public class AccountController
    {
        private readonly IAuthenticationService _authenticationService = null;

        public AccountController()
        {
            //this._authenticationService = IocManager.Intance.Reslove<IAuthenticationService>();
        }

        /// <summary>
        /// 登录API
        /// </summary>
        /// <param name="loginIdorEmail">登录帐号(邮箱或者其他LoginID)</param>
        /// <param name="hashedPassword">加密后的密码，这里避免明文，客户端加密后传到API端</param>
        /// <param name="deviceType">客户端的设备类型</param>
        /// <param name="clientId">客户端识别号, 一般在APP上会有一个客户端识别号</param>
        /// <remarks>其他的登录位置啥的，是要客户端能传的东西，都可以在这里扩展进来</remarks>
        /// <returns></returns>
        [Route("account/login")]
        public SessionObject Login(string loginIdorEmail, string hashedPassword, int deviceType = 0, string clientId = "")
        {
            return null;
            //if (string.IsNullOrEmpty(loginIdorEmail))
            //    throw new ApiException("username can't be empty.", "RequireParameter_username");
            //if (string.IsNullOrEmpty(hashedPassword))
            //    throw new ApiException("hashedPassword can't be empty.", "RequireParameter_hashedPassword");

            //int timeout = 60;

            //var nowUser = _authenticationService.GetUserByLoginId(loginIdorEmail);
            //if (nowUser == null)
            //    throw new ApiException("Account Not Exists", "Account_NotExits");

            //#region Verify Password
            //if (!string.Equals(nowUser.Password, hashedPassword))
            //{
            //    throw new ApiException("Wrong Password", "Account_WrongPassword");
            //}
            //#endregion

            //if (!nowUser.IsActive)
            //    throw new ApiException("The user is inactive.", "InactiveUser");

            //UserDevice existsDevice = _authenticationService.GetUserDevice(nowUser.UserId, deviceType);// Session.QueryOver<UserDevice>().Where(x => x.AccountId == nowAccount.Id && x.DeviceType == deviceType).SingleOrDefault();
            //if (existsDevice == null)
            //{
            //    string passkey = MD5CryptoProvider.GetMD5Hash(nowUser.UserId + nowUser.LoginName + DateTime.UtcNow.ToString() + Guid.NewGuid().ToString());
            //    existsDevice = new UserDevice()
            //    {
            //        UserId = nowUser.UserId,
            //        CreateTime = DateTime.UtcNow,
            //        ActiveTime = DateTime.UtcNow,
            //        ExpiredTime = DateTime.UtcNow.AddMinutes(timeout),
            //        DeviceType = deviceType,
            //        SessionKey = passkey
            //    };

            //    _authenticationService.AddUserDevice(existsDevice);
            //}
            //else
            //{
            //    existsDevice.ActiveTime = DateTime.UtcNow;
            //    existsDevice.ExpiredTime = DateTime.UtcNow.AddMinutes(timeout);
            //    _authenticationService.UpdateUserDevice(existsDevice);
            //}
            //nowUser.Password = "";
            //return new SessionObject() { SessionKey = existsDevice.SessionKey, LogonUser = nowUser };
        }
    }
}
