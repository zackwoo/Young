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
    public class MembershipAPIController : ApiController
    {
        // GET api/membershipapi
        public IEnumerable<string> GetAllUsers()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/membershipapi/5
        public string Get(int id)
        {
            return "value";
        }
      
        // POST api/membershipapi
        public ResultModel PostCreateUser(UserModel user)
        {
            var newUser = Membership.CreateUser(user.UserName, user.Password, user.Email);
            
            
            return new ResultModel{ IsSuccess = true,Message=user.Email};
        }

        // PUT api/membershipapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/membershipapi/5
        public void Delete(int id)
        {
        }
    }
}
