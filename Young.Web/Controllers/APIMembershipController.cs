using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Young.DAL;
using Young.Provider;
using Young.Web.Models;
using Young.Web.Models.EasyUIView;

namespace Young.Web.Controllers
{
    public class APIMembershipController : ApiController
    {
        // GET api/apimembership
        public DataGridModel<MembershipUser> GetAllUsers(int page, int rows)
        {
            var result = new DataGridModel<MembershipUser>();
            int total;
            var users = Membership.GetAllUsers(page - 1, rows, out total);
            foreach (MembershipUser membershipUser in users)
            {
                result.rows.Add(membershipUser);
            }
            result.total = total;
            return result;
        }

        // GET api/apimembership/5
        public string Get(int id)
        {
            return "value";
        }
      
        // POST api/apimembership
        public ResultModel PostUser(string category,UserModel user)
        {
            if (category == "new")
            {
                return CreateUser(user);
            }
            if (category == "edit")
            {
                return EditUser(user);
            }
            if (category == "resetpwd")
            {
                ResultModel result = new ResultModel();
                try
                {
                    var newPassword = Membership.Provider.ResetPassword(user.UserName, user.PasswordAnswer);
                    result.IsSuccess = true;
                    result.Message = newPassword;
                }
                catch (MembershipPasswordException)
                {
                    result.IsSuccess = false;
                    result.Message = "无该用户";                    
                }
                return result;
            }
            return null;
        }

        private ResultModel EditUser(UserModel user)
        {
            var member = Membership.GetUser(user.UserName, false) as YoungMembershipUser;
            if (member==null)
            {
                return new ResultModel
                {
                    IsSuccess = false,
                    Message = string.Format("未找到用户{0}", user.UserName)
                };
            }
            member.DisplayName = user.DisplayName;
            if (!Membership.Provider.RequiresUniqueEmail)
            {//非唯一Email时可修改Email地址
                member.Email = user.Email;
            }
            if (!string.IsNullOrEmpty(user.DepartID))
            {
                member.DepartmentID = Convert.ToInt32(user.DepartID);
            }
            if (!string.IsNullOrEmpty(user.PostionID))
            {
                member.PostionID = Convert.ToInt32(user.PostionID);
            }
            Membership.UpdateUser(member);
            return new ResultModel
            {
                IsSuccess = true
            };
        }
        private ResultModel CreateUser(UserModel user)
        {
            string question = "密码问题";
            string answer = "密码答案";
            if (Membership.RequiresQuestionAndAnswer)
            {
                question = user.PasswordQuestion;
                answer = user.PasswordAnswer;
            }
            MembershipCreateStatus status;
            Membership.CreateUser(user.UserName, user.Password, user.Email, question, answer, user.IsApproved, out status);
            var result = new ResultModel { IsSuccess = status == MembershipCreateStatus.Success };
            switch (status)
            {
                case MembershipCreateStatus.DuplicateEmail:
                    result.Message = "已有重复Email";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    result.Message = "已有重复主键";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    result.Message = "已有重复用户登录账号";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    result.Message = "无效问题答案";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    result.Message = "无效邮件地址";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    result.Message = "无效密码";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    result.Message = "无效主键";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    result.Message = "无效问题";
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    result.Message = "无效用户登录账号";
                    break;
                case MembershipCreateStatus.ProviderError:
                    result.Message = "提供程序错误";
                    break;
                case MembershipCreateStatus.Success:
                    result.Message = "创建成功";
                    break;
                case MembershipCreateStatus.UserRejected:
                    result.Message = "因为提供程序定义的某个原因而未创建用户";
                    break;
            }
            if (result.IsSuccess)
            {
                using (var db = new DataBaseContext())
                {
                    var userInfo = db.Users.Single(f => f.UserName == user.UserName);
                    if (!string.IsNullOrEmpty(user.DepartID))
                    {
                        var did = Convert.ToInt32(user.DepartID);
                        var depart = db.Terms.Single(f => f.ID == did);
                        userInfo.Department = depart;
                    }
                    if (!string.IsNullOrEmpty(user.PostionID))
                    {
                        var pid = Convert.ToInt32(user.PostionID);
                        var postion = db.Terms.Single(f => f.ID == pid);
                        userInfo.Position = postion;
                    }
                    db.SaveChanges();
                }
            }
            return result;
        }

       
        // PUT api/apimembership
        public ResultModel PutLockOrUnLockUsers(string category,string providerUserKeys)
        {
            if (string.IsNullOrEmpty(providerUserKeys))
            {
                return new ResultModel
                    {
                        Message = "请选定用户",
                        IsSuccess = false
                    };
            }
            var keys = providerUserKeys.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var key in keys)
            {
                object id = key;
                var user = Membership.GetUser(id, false) as YoungMembershipUser;
                if (category == "lock")
                {
                    user.LockUser();
                }
                else
                {
                    user.UnlockUser();
                }
            }
            return new ResultModel
                {
                    IsSuccess = true
                };
        }

        // DELETE api/apimembership/5
        public void Delete(int id)
        {
        }
    }
}
