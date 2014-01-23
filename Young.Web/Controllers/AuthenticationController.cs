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
    public class AuthenticationController : ApiController
    {
        public AuthenticationModel Get()
        {
            var model = new AuthenticationModel
                {
                    IsAuthenticated = User.Identity.IsAuthenticated,
                    Name = User.Identity.Name
                };
            return model;
        }
    }
}
