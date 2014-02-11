using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Young.DAL;
using Young.Model;

namespace Young.Provider
{
    public class YoungMembershipProvider : MembershipProvider
    {
        private MembershipUser ConvertUser(UserEntity user)
        {
            var mu = new MembershipUser("providerName", user.UserName, user.ID, user.Email, "passwordQuestion",
                                        "comment", !user.IsLock, user.IsLock, user.RegisterTime, user.LastLoginTime,
                                        user.LastOperationTime, user.LastPasswordChangedTime, user.LastLockoutTime);

            return mu;
        }

        #region 内部参数
        /// <summary>
        /// 程序集名称
        /// </summary>
        private string name;
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string connStr;
        /// <summary>
        /// 标示用户是否可以请求重置它们的密码
        /// </summary>
        private bool enablePasswordReset = false;
        /// <summary>
        /// 标示用户是否可以请求将密码发送给它们.可以选择是否需要用户回答安全问题
        /// </summary>
        private bool enablePasswordRetrieval = false;
        /// <summary>
        /// 当前应用程序的名称
        /// </summary>
        private string applicationName;
        /// <summary>
        /// 加密字节
        /// </summary>
        //private byte[] decryptionKey;
        /// <summary>
        /// 解密字节
        /// </summary>
        //private byte[] validationKey;
        /// <summary>
        /// 标示了在获取或重置密码时,是否要回答安全问题
        /// </summary>
        private bool requiresQuestionAndAnswer;
        /// <summary>
        /// 标示了是否允许存储具有同样邮箱的多个用户
        /// </summary>
        private bool requiresUniqueEmail = false;
        /// <summary>
        /// 描述存储成员的加密格式
        /// </summary>
        private MembershipPasswordFormat passwordFormat = MembershipPasswordFormat.Hashed;
        /// <summary>
        /// 用户可以输入错误密码的次数
        /// </summary>
        private int maxInvalidPasswordAttempts;
        /// <summary>
        /// 一个密码所需非英文字母的最小个数
        /// </summary>
        private int minRequiredNonAlphanumericCharacters;
        /// <summary>
        /// 标示了密码的最小长度
        /// </summary>
        private int minRequiredPasswordLength;
        /// <summary>
        /// 标示当前用户达到最大错误密码输入次数时，用户被锁定的时间
        /// </summary>
        private int passwordAttemptWindow;
        /// <summary>
        /// 用来验证密码的正则表达式.如果验证失败,说明密码安全性太低,密码将被拒绝
        /// </summary>
        private string passwordStrengthRegularExpression;
        #endregion
        #region 属性

        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {

            get { return minRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return requiresUniqueEmail; }
        }

        public override bool EnablePasswordReset
        {
            get { return enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return enablePasswordRetrieval; }
        }

        #endregion
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            this.name = name;
            if (config["applicationName"] != null)
            {
                applicationName = config["applicationName"];
            }
            if (config["enablePasswordRetrieval"] != null)
            {
                enablePasswordRetrieval = Convert.ToBoolean(config["enablePasswordRetrieval"]);
            }
            if (config["enablePasswordReset"] != null)
            {
                enablePasswordReset = Convert.ToBoolean(config["enablePasswordReset"]);
            }
            if (config["requiresQuestionAndAnswer"] != null)
            {
                requiresQuestionAndAnswer = Convert.ToBoolean(config["requiresQuestionAndAnswer"]);
            }
            if (config["requiresUniqueEmail"] != null)
            {
                requiresUniqueEmail = Convert.ToBoolean(config["requiresUniqueEmail"]);
            }
            if (config["maxInvalidPasswordAttempts"] != null)
            {
                maxInvalidPasswordAttempts = Convert.ToInt32(config["maxInvalidPasswordAttempts"]);
            }
            if (config["minRequiredNonAlphanumericCharacters"] != null)
            {
                minRequiredNonAlphanumericCharacters = Convert.ToInt32(config["minRequiredNonAlphanumericCharacters"]);
            }
            if (config["minRequiredPasswordLength"] != null)
            {
                minRequiredPasswordLength = Convert.ToInt32(config["minRequiredPasswordLength"]);
            }
            if (config["passwordAttemptWindow"] != null)
            {
                passwordAttemptWindow = Convert.ToInt32(config["passwordAttemptWindow"]);
            }
            if (config["passwordStrengthRegularExpression"] != null)
            {
                passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
            }
            if (config["passwordFormat"] != null)
            {
                switch (config["passwordFormat"].ToLower())
                {
                    case "clear":
                        passwordFormat = MembershipPasswordFormat.Clear;
                        break;
                    case "hashed":
                        passwordFormat = MembershipPasswordFormat.Hashed;
                        break;
                    case "encrypted":
                        passwordFormat = MembershipPasswordFormat.Encrypted;
                        break;
                    default:
                        throw new ConfigurationErrorsException(string.Format("未知的加密格式 \"{0}\".", config["passwordFormat"]));
                }
            }
            connStr = ConfigurationManager.ConnectionStrings[config["connectionStringName"]].ConnectionString;
            base.Initialize(name, config);
        }


        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (ValidateUser(username,oldPassword))
            {
                using (var db = new DataBaseContext())
                {
                    var user = db.Users.SingleOrDefault(f => f.UserName == username);
                    if (user!=null)
                    {
                        user.Password = newPassword;
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var user = new UserEntity
                {
                    UserName = username,
                    Password = password,
                    Email = email,
                    IsLock = isApproved,
                    DisplayName = username,
                    RegisterTime = DateTime.Now,
                    LastOperationTime = DateTime.Now,
                    LastLockoutTime = DateTime.Now,
                    LastLoginTime = DateTime.Now,
                    LastPasswordChangedTime = DateTime.Now
                };
            using (var db = new DataBaseContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
                status = MembershipCreateStatus.Success;
            }
            return ConvertUser(user);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }



        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            using (var db = new DataBaseContext())
            {
                var time = DateTime.Now.AddMinutes(-15d);//15分钟内有过操作的用户算在线用户
                return db.Users.Count(f => f.LastOperationTime > time);
            }
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (var db = new DataBaseContext())
            {
                var user = db.Users.SingleOrDefault(f => f.UserName == username);
                if (user == null) return null;
                if (userIsOnline)
                {
                    user.LastOperationTime = DateTime.Now;
                    db.SaveChanges();
                }
                return ConvertUser(user);
            }
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            int id = Convert.ToInt32(providerUserKey);
            using (var db = new DataBaseContext())
            {
                var user = db.Users.Single(f => f.ID == id);
                if (userIsOnline)
                {
                    user.LastOperationTime = DateTime.Now;
                    db.SaveChanges();
                }
                return ConvertUser(user);
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            using (var db = new DataBaseContext())
            {
                var user = db.Users.SingleOrDefault(f => f.Email == email);
                if (user==null)
                {
                    return string.Empty;
                }
                return user.UserName;
            }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var user = db.Users.Single(f => f.UserName == userName);
                    user.IsLock = false;
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {

                return false;
            }
        }

        public override void UpdateUser(MembershipUser membershipUser)
        {
            var id = Convert.ToInt32(membershipUser.ProviderUserKey);
            using (var db = new DataBaseContext())
            {
                var user = db.Users.Single(f => f.ID == id);
                user.IsLock = membershipUser.IsLockedOut;
                user.LastOperationTime= membershipUser.LastActivityDate;
                user.LastLockoutTime = membershipUser.LastLockoutDate;
                user.LastLoginTime = membershipUser.LastLoginDate;
                user.LastPasswordChangedTime = membershipUser.LastPasswordChangedDate;
                db.SaveChanges();
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            using (var db = new DataBaseContext())
            {
                return db.Users.Any(f => f.UserName == username && f.Password == password);
            }
        }
    }
}
