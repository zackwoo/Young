using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Young.Provider
{
    public class YoungMembershipUser : MembershipUser
    {
        public YoungMembershipUser(string providerName, string name, object providerUserKey, string email,
                                   string passwordQuestion, string comment, bool isApproved, bool isLockedOut,
                                   DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate,
                                   DateTime lastPasswordChangedDate, DateTime lastLockoutDate, string displayName)
            : base(
                providerName, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut,
                creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
        {
            DisplayName = displayName;
        }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
