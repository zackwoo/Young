using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Young.Web.Models
{
    public class AuthenticationModel
    {
        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }
    }
}