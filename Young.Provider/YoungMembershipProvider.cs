using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
            return new YoungMembershipUser(this.name, user.UserName, user.ID, user.Email, user.PasswordQuestion,
                                           "comment", !user.IsLock, user.IsLock, user.RegisterTime, user.LastLoginTime,
                                           user.LastActivityTime, user.LastPasswordChangedTime, user.LastLockoutTime,
                                           user.DisplayName);
        }

        #region 内部参数
        /// <summary>
        /// 程序集名称
        /// </summary>
        private string name;
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

        internal string EncodePassword(string pass)
        {
            //加密盐
            string salt = "21EC2020-3AEA-1069-A2DD-08002B30309D";
            if (passwordFormat ==  MembershipPasswordFormat.Clear) // MembershipPasswordFormat.Clear
                return pass;
            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bAll = new byte[bSalt.Length + bIn.Length];
            byte[] bRet = null;
            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
            if (passwordFormat == MembershipPasswordFormat.Hashed)
            {
                HashAlgorithm s = HashAlgorithm.Create(Membership.HashAlgorithmType);
                bRet = s.ComputeHash(bAll);
            }
            else
            {
                bRet = EncryptPassword(bAll);
            }
            return Convert.ToBase64String(bRet);
        }
        internal string UnEncodePassword(string pass)
        {
            switch (passwordFormat)
            {
                case  MembershipPasswordFormat.Clear: // MembershipPasswordFormat.Clear:
                    return pass;
                case  MembershipPasswordFormat.Hashed: // MembershipPasswordFormat.Hashed:
                    throw new ProviderException("密码不可逆");
                default:
                    byte[] bIn = Convert.FromBase64String(pass);
                    byte[] bRet = DecryptPassword(bIn);
                    if (bRet == null)
                        return null;
                    return Encoding.Unicode.GetString(bRet, 16, bRet.Length - 16);
            }
        }
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        internal bool ValidatePassword(string pass)
        {
            //MinRequiredNonAlphanumericCharacters
            if (pass.Length < MinRequiredPasswordLength)
                return false;
            var reg = new Regex("[^a-zA-Z]");
            if (reg.Matches(pass).Count < MinRequiredNonAlphanumericCharacters)
                return false;
            if (!string.IsNullOrEmpty(PasswordStrengthRegularExpression))
            {
                return Regex.IsMatch(pass, PasswordStrengthRegularExpression);
            }
            return true;
        }

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
            base.Initialize(name, config);
        }


        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            
            if (ValidateUser(username, oldPassword) && ValidatePassword(newPassword))
            {
                using (var db = new DataBaseContext())
                {
                    var user = db.Users.SingleOrDefault(f => f.UserName == username);
                    if (user != null)
                    {
                        user.Password = EncodePassword(newPassword);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            if (ValidateUser(username, password))
            {
                using (var db = new DataBaseContext())
                {
                    var user = db.Users.SingleOrDefault(f => f.UserName == username);
                    if (user != null)
                    {
                        user.PasswordQuestion = newPasswordQuestion;
                        user.PasswordAnswer = newPasswordAnswer;
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            if (!ValidatePassword(password))
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            var user = new UserEntity
                {
                    UserName = username,
                    Password = EncodePassword(password),
                    Email = email,
                    IsLock = false,
                    IsApproved = isApproved,
                    DisplayName = username,
                    RegisterTime = DateTime.Now,
                    LastActivityTime = DateTime.Now,
                    LastLockoutTime = DateTime.Now,
                    LastLoginTime = DateTime.Now,
                    LastPasswordChangedTime = DateTime.Now,
                    PasswordAnswer = passwordAnswer,
                    PasswordQuestion = passwordQuestion,
                    IsDelete = false
                };
            using (var db = new DataBaseContext())
            {
                if (db.Users.Any(f=>f.UserName == username))
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                    return null;
                }
                if (RequiresUniqueEmail && db.Users.Any(f => f.Email == email))
                {
                    status = MembershipCreateStatus.DuplicateEmail;
                    return null;
                }
                db.Users.Add(user);
                db.SaveChanges();
                status = MembershipCreateStatus.Success;
            }
            return ConvertUser(user);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    var user = db.Users.Single(f => f.UserName == username);
                    user.IsDelete = true;
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }



        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            using (var db = new DataBaseContext())
            {
                totalRecords = db.Users.Where(f => f.Email.Contains(emailToMatch)).Count();
                var list = db.Users.Where(f => f.Email.Contains(emailToMatch)).Skip(pageIndex * pageSize).Take(pageSize);
                MembershipUserCollection result = new MembershipUserCollection();
                foreach (var item in list)
                {
                    result.Add(ConvertUser(item));
                }
                return result;
            }
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            using (var db = new DataBaseContext())
            {
                totalRecords = db.Users.Where(f => f.UserName.Contains(usernameToMatch)).Count();
                var list = db.Users.Where(f => f.UserName.Contains(usernameToMatch)).Skip(pageIndex * pageSize).Take(pageSize);
                MembershipUserCollection result = new MembershipUserCollection();
                foreach (var item in list)
                {
                    result.Add(ConvertUser(item));
                }
                return result;
            }
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            using (var db = new DataBaseContext())
            {
                totalRecords = db.Users.Count();
                var list = db.Users.Skip(pageIndex * pageSize).Take(pageSize);
                MembershipUserCollection result = new MembershipUserCollection();
                foreach (var item in list)
                {
                    result.Add(ConvertUser(item));
                }
                return result;
            }
        }

        public override int GetNumberOfUsersOnline()
        {
            using (var db = new DataBaseContext())
            {
                var time = DateTime.Now.AddMinutes(Membership.UserIsOnlineTimeWindow*0-1);
                return db.Users.Count(f => f.LastActivityTime > time);
            }
        }

        public override string GetPassword(string username, string answer)
        {
            if (!EnablePasswordRetrieval) throw new NotSupportedException("应用程序不支持取回密码。");
            using (var db = new DataBaseContext())
            {
                UserEntity user;
                if (RequiresQuestionAndAnswer)
                {
                    user = db.Users.SingleOrDefault(f => f.UserName == username && f.PasswordAnswer == answer);
                }
                else
                {
                    user = db.Users.SingleOrDefault(f => f.UserName == username);
                }
                if (user == null)
                {
                    throw new MembershipPasswordException("无" + username + "用户或者密码答案不匹配");
                }
                return UnEncodePassword(user.Password);
            }
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (var db = new DataBaseContext())
            {
                var user = db.Users.SingleOrDefault(f => f.UserName == username);
                if (user == null) return null;
                if (userIsOnline)
                {
                    user.LastActivityTime = DateTime.Now;
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
                var user = db.Users.SingleOrDefault(f => f.ID == id);
                if (user == null) return null;
                if (userIsOnline)
                {
                    user.LastActivityTime = DateTime.Now;
                    db.SaveChanges();
                }
                return ConvertUser(user);
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            using (var db = new DataBaseContext())
            {
                var user = db.Users.FirstOrDefault(f => f.Email == email);
                if (user == null)
                {
                    return string.Empty;
                }
                return user.UserName;
            }
        }

        public override string ResetPassword(string username, string answer)
        {
            if (!EnablePasswordReset) throw new NotSupportedException("应用程序不允许重置密码。");
            using (var db = new DataBaseContext())
            {
                UserEntity user;
                if (RequiresQuestionAndAnswer)
                {
                    user = db.Users.SingleOrDefault(f => f.UserName == username && f.PasswordAnswer == answer);
                }
                else
                {
                    user = db.Users.SingleOrDefault(f => f.UserName == username);
                }                
                if (user == null)
                {
                    throw new MembershipPasswordException("无" + username + "用户或者密码答案不匹配");
                }
                var newPassword = Membership.GeneratePassword(MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters);
                user.Password = EncodePassword(newPassword);
                user.LastPasswordChangedTime = DateTime.Now;
                db.SaveChanges();
                return newPassword;
            }            
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
            var youngUser = membershipUser as YoungMembershipUser;
            var id = Convert.ToInt32(youngUser.ProviderUserKey);

            using (var db = new DataBaseContext())
            {
                var user = db.Users.Single(f => f.ID == id);
                user.IsLock = youngUser.IsLockedOut;
                user.LastActivityTime = youngUser.LastActivityDate;
                user.LastLockoutTime = youngUser.LastLockoutDate;
                user.LastLoginTime = youngUser.LastLoginDate;
                user.LastPasswordChangedTime = youngUser.LastPasswordChangedDate;
                user.DisplayName = youngUser.DisplayName;
                db.SaveChanges();
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            var dbPassword = EncodePassword(password);
            using (var db = new DataBaseContext())
            {
                return db.Users.Any(f => f.UserName == username && f.Password == dbPassword);
            }
        }
    }
}
