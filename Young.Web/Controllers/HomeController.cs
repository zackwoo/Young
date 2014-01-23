using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Young.Tools.Objects;

namespace Young.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string name, string password)
       {
           if (name=="zack" && password=="123456")
           {
               System.Web.Security.FormsAuthentication.SetAuthCookie("zack", true);
           }
           return Redirect("/Static/Admin/Home.html");
       }
    }
}
