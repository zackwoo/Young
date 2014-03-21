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

        #region 不同类型列 action
        public ActionResult AddColumn(string tcode, ColumnType type)
        {
            var table = CustomTableTools.GetTableByCode(tcode);
            ColumnModel model = new ColumnModel { TableName = table.Name, TableCode = tcode };
            ColumnModelBuilder builder = new ColumnModelBuilder();
            switch (type)
            {
                case ColumnType.Date:
                    return RedirectToAction("DateColumn", builder.BuildDateColumn(model));
                case ColumnType.Number:
                    return RedirectToAction("NumberColumn", builder.BuildNumberColumn(model));
                case ColumnType.RichText:
                    return RedirectToAction("RichTextColumn", builder.BuildRichTextColumn(model));
                default:
                    return RedirectToAction("LineTextColumn", builder.BuildLineTextColumn(model));
            }
        }
        public ActionResult LineTextColumn(LineTextColumnModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("LineTextColumn")]
        public ActionResult PostLineTextColumn(LineTextColumnModel model)
        {
            return View(model);
        }

        public ActionResult NumberColumn(NumberColumnModel model)
        {
            return View(model);
        }
        [HttpPost]
        [ActionName("NumberColumn")]
        public ActionResult PostNumberColumn(NumberColumnModel model)
        {
            return View(model);
        }
        public ActionResult RichTextColumn(RichTextColumnModel model)
        {
            return View(model);
        }
        [HttpPost]
        [ActionName("RichTextColumn")]
        public ActionResult PostRichTextColumn(RichTextColumnModel model)
        {
            return View(model);
        }
        public ActionResult DateColumn(DateColumnModel model)
        {
            return View(model);
        }
        [HttpPost]
        [ActionName("DateColumn")]
        public ActionResult PostDateColumn(DateColumnModel model)
        {
            return View(model);
        }
        #endregion
    }
}
