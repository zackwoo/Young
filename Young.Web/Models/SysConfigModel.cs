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
        [Display(Name = "应用程序名")]
        public string MembershipApplicationName
        {
            get { return Membership.ApplicationName; }
        }
        [Display(Name = "是否允许重置密码")]
        public bool MembershipEnablePasswordReset
        {
            get { return Membership.EnablePasswordReset; }
        }
        [Display(Name = "是否允许取回密码")]
        public bool MembershipEnablePasswordRetrieval
        {
            get { return Membership.EnablePasswordRetrieval; }
        }
        [Display(Name = "允许的无效密码尝试次数")]
        public int MembershipMaxInvalidPasswordAttempts
        {
            get { return Membership.MaxInvalidPasswordAttempts; }
        }
        [Display(Name = "密码中必须包含的最少特殊字符数")]
        public int MembershipMinRequiredNonAlphanumericCharacters
        {
            get { return Membership.MinRequiredNonAlphanumericCharacters; }
        }
        [Display(Name = "密码最小长度")]
        public int MembershipMinRequiredPasswordLength
        {
            get { return Membership.MinRequiredPasswordLength; }
        }
        [Display(Name = "连续密码失败尝试次数有效时间间隔")]
        public int MembershipPasswordAttemptWindow
        {
            get { return Membership.PasswordAttemptWindow; }
        }
        [Display(Name = "有效密码的正则表达式")]
        public string MembershipPasswordStrengthRegularExpression
        {
            get { return Membership.PasswordStrengthRegularExpression; }
        }
        [Display(Name = "是否启用密码问题")]
        public bool MembershipRequiresQuestionAndAnswer
        {
            get { return Membership.RequiresQuestionAndAnswer; }
        }
        [Display(Name = "默认联机有效时间")]
        public int MembershipUserIsOnlineTimeWindow
        {
            get { return Membership.UserIsOnlineTimeWindow; }
        }
    }
}