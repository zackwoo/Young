using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public ResultModel Post(UserModel user)
        {
            return new ResultModel{ IsSuccess = true};
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
