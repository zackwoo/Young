using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Young.DAL;

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

        public bool LockUser()
        {
            using (var db = new DataBaseContext())
            {
                var key = Convert.ToInt32(this.ProviderUserKey);
                var user = db.Users.SingleOrDefault(f => f.ID == key);
                if (user==null)
                {
                    return false;
                }
                user.IsLock = true;
                db.SaveChanges();
            }
            return true;
        }
    }
}
