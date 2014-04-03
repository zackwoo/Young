using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Young.CustomTable.ViewModel;


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
            if (Membership.ValidateUser(name, password))
            {
                FormsAuthentication.SetAuthCookie(name, true);
                return RedirectToAction("index", "Webmaster");
            }
            else
                return RedirectToAction("Login", "home");
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
