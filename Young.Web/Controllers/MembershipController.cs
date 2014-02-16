using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Young.Web.Models;

namespace Young.Web.Controllers
{
    public class MembershipController : Controller
    {
        //
        // GET: /Membership/

        public ActionResult Index()
        {
            return View(new SysConfigModel());
        }
    }
}
