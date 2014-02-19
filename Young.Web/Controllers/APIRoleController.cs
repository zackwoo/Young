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
    public class APIRoleController : ApiController
    {
        // DELETE api/apirole?roleName=xxx
        public ResultModel Delete(string roleName)
        {
            ResultModel result = new ResultModel();
            try
            {
                Roles.DeleteRole(roleName, true);
                result.IsSuccess = true;
            }
            catch
            {
                result.IsSuccess = false;
                result.Message = "未能删除角色";
            }
            return result;
        }

        public ResultModel PostCreaetRole(string roleName)
        {
            ResultModel result = new ResultModel();
            try
            {
                Roles.CreateRole(roleName);
                result.IsSuccess = true;
            }
            catch
            {
                result.IsSuccess = false;
                result.Message = "请联系管理员";
            }
            return result;
        }

        public ResultModel PutUsers(string category,string roleName,string userNameArry)
        {
            var result = new ResultModel
                {
                    IsSuccess = true
                };
            var usernames = userNameArry.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (category == "add")
            {
                //添加用户
                try
                {
                    Roles.AddUsersToRole(usernames, roleName);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                }
                return result;
            }
            else if (category == "remove")
            {
                //移除用户
                try
                {
                    Roles.RemoveUsersFromRole(usernames, roleName);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                }
                return result;
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "指令错误";
                return result;
            }
        }

        public string[] GetUserNameByRoleName(string roleName)
        {
            return Roles.GetUsersInRole(roleName);
        }
    }
}
