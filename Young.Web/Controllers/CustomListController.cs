using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            
            model.Add(new CustomColumnModel
            {
                DisplayName = "标题",
                Type =CustomColumnType.TextLine
            });
            model.Add(new CustomColumnModel
            {
                DisplayName = "描述",
                Type =CustomColumnType.TextArea
            });
            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}
