using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Young.Web.Models;

namespace Young.Web.Controllers
{
    public class CustomListController : Controller
    {
        //
        // GET: /CustomList/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}
