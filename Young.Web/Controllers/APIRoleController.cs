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
    }
}
