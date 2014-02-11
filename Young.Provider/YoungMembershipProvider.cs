using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Young.Provider
{
    public class YoungMembershipProvider : MembershipProvider
    {
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
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

     

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
