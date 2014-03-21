using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Young.CustomTable;
using Young.DAL;
using Young.Model;
using Young.Web.Models;
using Young.Web.Models.Column;

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

        public ActionResult Column(string code)
        {
            var table = CustomTableTools.GetTableByCode(code);

            return View(new LineTextColumnModel
            {
                TableCode = code,
                TableName = table.Name
            });
        }

        public ActionResult AddColumn(string tcode, ColumnType type)
        {
            var table = CustomTableTools.GetTableByCode(tcode);
            switch (type)
            {
                case ColumnType.Date:
                    return RedirectToAction("AddDateColumn", new DateColumnModel
                        {
                            TableCode = tcode,
                            TableName = table.Name
                        });
                case ColumnType.Number:
                    return RedirectToAction("AddNumberColumn", new NumberColumnModel
                        {
                            TableCode = tcode,
                            TableName = table.Name
                        });
                case ColumnType.RichText:
                    return RedirectToAction("AddRichTextColumn", new RichTextColumnModel
                        {
                            TableCode = tcode,
                            TableName = table.Name
                        });
                default:
                    return View(new LineTextColumnModel
                        {
                            TableCode = tcode,
                            TableName = table.Name
                        });
            }
        }

        public ActionResult AddNumberColumn(NumberColumnModel model)
        {
            return View(model);
        }
        public ActionResult AddRichTextColumn(RichTextColumnModel model)
        {
            return View(model);
        }
        public ActionResult AddDateColumn(DateColumnModel model)
        {
            return View(model);
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
