using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Young.CustomTable;
using Young.DAL;
using Young.Model;
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
            var model = new CustomListModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(CustomListModel model)
        {

            CustomTableTools.AddCustomTable(new YoungTable
            {
                Code = Guid.NewGuid().ToString("N"),
                Description = model.Description,
                Name = model.Name,
                CreateTime = DateTime.Now
            });
            return RedirectToAction("index");
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult List(int id)
        {
            using (var db = new DataBaseContext())
            {
                var info = db.CustomList.Single(f => f.ID == id);
                ViewBag.Name = info.Name;
                ViewBag.ID = id;
            }
            return View();
        }
    }
}
