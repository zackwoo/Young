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
            var instance = Util.QueryInstanceByProperty("用户对象", "用户登录名", "zack");
            return View();
        }
    }
}
