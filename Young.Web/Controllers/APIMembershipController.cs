using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Young.Web.Models;

namespace Young.Web.Controllers
{
    public class APIMembershipController : ApiController
    {
        // GET api/apimembership
        public IEnumerable<string> GetAllUsers()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/apimembership/5
        public string Get(int id)
        {
            return "value";
        }
      
        // POST api/apimembership
        public ResultModel PostCreateUser(UserModel user)
        {
            string question = string.Empty;
            string answer = string.Empty;
            if (Membership.RequiresQuestionAndAnswer)
            {
                question = user.PasswordQuestion;
                answer = user.PasswordAnswer;
            }
            MembershipCreateStatus status;
            Membership.CreateUser(user.UserName, user.Password, user.Email, question, answer, user.IsApproved, out status);
            var result = new ResultModel{ IsSuccess = status== MembershipCreateStatus.Success};
            switch (status)
            {
                case MembershipCreateStatus.DuplicateEmail:
                    result.Message = "已有重复Email";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    result.Message = "已有重复主键";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    result.Message = "已有重复用户登录名";
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
                    result.Message = "无效用户登录名";
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
            return result;
        }

        // PUT api/apimembership/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/apimembership/5
        public void Delete(int id)
        {
        }
    }
}
