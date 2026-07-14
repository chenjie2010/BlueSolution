using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Blue.WebAPI
{
    public class UserIdentity<TKey> : IIdentity
    {

        public UserIdentity(IUser<TKey> user)
        {
            if (user != null)
            {
                IsAuthenticated = true;
                UserId = user.UserId;
                Name = user.LoginName.ToString();
                DisplayName = user.DisplayName;
            }
        }

        public string AuthenticationType
        {
            get { return "CustomAuthentication"; }
        }

        public TKey UserId { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public string Name { get; private set; }

        public string DisplayName { get; private set; }
    }

    public class UserPrincipal<TKey> : IPrincipal
    {
        public UserPrincipal(UserIdentity<TKey> identity)
        {
            Identity = identity;
        }

        public UserPrincipal(IUser<TKey> user)
            : this(new UserIdentity<TKey>(user))
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public UserIdentity<TKey> Identity { get; private set; }

        IIdentity IPrincipal.Identity
        {
            get { return Identity; }
        }


        bool IPrincipal.IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUser<T>
    {
        T UserId { get; set; }
        string LoginName { get; set; }
        string DisplayName { get; set; }
    }
}
