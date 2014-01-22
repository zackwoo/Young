using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace Young.Web.Controllers
{
    public class AuthenticationController : ApiController
    {
        // POST api/authentication
        public void Post([FromBody]string name, [FromBody]string password)
        {
            if (name == "zack" && password == "zack")
            {
                FormsAuthentication.SetAuthCookie("zack", true);
            }
        }
    }
}
