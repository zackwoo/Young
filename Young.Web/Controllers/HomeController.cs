using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Young.Tools.Objects;

namespace Young.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string password)
        {
            if (name == "zack" && password == "123456")
            {
                FormsAuthentication.SetAuthCookie("zack", true);

            }
            return Redirect("/Static/Admin/Home.html");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Static/Admin/Home.html");
        }
    }
}
