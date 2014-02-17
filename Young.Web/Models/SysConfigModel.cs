using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Young.Web.Models
{
    public class SysConfigModel
    {
        [Display(Name = "应用程序名", GroupName = "Membership")]
        public string MembershipApplicationName
        {
            get { return Membership.ApplicationName; }
        }
        [Display(Name = "启用重置密码", GroupName = "Membership")]
        public bool MembershipEnablePasswordReset
        {
            get { return Membership.EnablePasswordReset; }
        }
        [Display(Name = "启用取回密码", GroupName = "Membership")]
        public bool MembershipEnablePasswordRetrieval
        {
            get { return Membership.EnablePasswordRetrieval; }
        }
        [Display(Name = "启用密码问题", GroupName = "Membership")]
        public bool MembershipRequiresQuestionAndAnswer
        {
            get { return Membership.RequiresQuestionAndAnswer; }
        }
        [Display(Name = "启用唯一Email", GroupName = "Membership")]
        public bool RequiresUniqueEmail
        {
            get
            {
                return Membership.Provider.RequiresUniqueEmail;
            }
        }
        [Display(Name = "允许的无效密码尝试次数", GroupName = "Membership")]
        public int MembershipMaxInvalidPasswordAttempts
        {
            get { return Membership.MaxInvalidPasswordAttempts; }
        }
        [Display(Name = "密码中必须包含的最少特殊字符数", GroupName = "Membership")]
        public int MembershipMinRequiredNonAlphanumericCharacters
        {
            get { return Membership.MinRequiredNonAlphanumericCharacters; }
        }
        [Display(Name = "密码最小长度", GroupName = "Membership")]
        public int MembershipMinRequiredPasswordLength
        {
            get { return Membership.MinRequiredPasswordLength; }
        }
        [Display(Name = "连续密码失败尝试次数有效时间间隔", GroupName = "Membership")]
        public int MembershipPasswordAttemptWindow
        {
            get { return Membership.PasswordAttemptWindow; }
        }
        [Display(Name = "有效密码的正则表达式", GroupName = "Membership")]
        public string MembershipPasswordStrengthRegularExpression
        {
            get { return Membership.PasswordStrengthRegularExpression; }
        }
        [Display(Name = "默认联机有效时间", GroupName = "Membership")]
        public int MembershipUserIsOnlineTimeWindow
        {
            get { return Membership.UserIsOnlineTimeWindow; }
        }
        [Display(Name = "应用程序名", GroupName = "Roles")]
        public string RoleApplicationName { get { return Roles.ApplicationName; } }
       
    }
}