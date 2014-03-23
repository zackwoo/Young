using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Young.CustomTable;
using Young.CustomTable.ColumnType;
using Young.DAL;
using Young.Model;
using Young.Web.Models;
using Young.Web.Models.Column;
using Young.Web.Models.CustomList;

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
                Code = Young.Util.Sequence.GetNewSequence(6,"T_"),
                Description = model.Description,
                Name = model.Name,
                CreateTime = DateTime.Now
            });
            return RedirectToAction("index");
        }

        public ActionResult Detail(string code)
        {
            ColumnModelBuilder builder = new ColumnModelBuilder();
            var table = CustomTableTools.GetTableByCode(code,true);
            var model = new TableDetailModel
            {
                TableCode = code,
                TableName = table.Name
            };
            model.Columns = table.Columns.Select(f => builder.BuildColumnModel(f));
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
        public ActionResult EditColumn(string tname,string code)
        {
            var table = CustomTableTools.GetTableByName(tname);
            var column = CustomTableTools.GetColumn(tname, code);
            if (column==null)
            {
                throw new ArgumentNullException("column");
            }
            ColumnModelBuilder builder = new ColumnModelBuilder();
            var model = builder.BuildColumnModel(column);
            model.TableCode = table.Code;
            model.TableName = table.Name;
            switch (model.ColumnType)
            {
                case ColumnType.Date:
                    return RedirectToAction("DateColumn", model);
                case ColumnType.Number:
                    return RedirectToAction("NumberColumn", model);
                case ColumnType.RichText:
                    return RedirectToAction("RichTextColumn", model);
                default:
                    return RedirectToAction("LineTextColumn", model);
            }
        }
       
        [HttpPost]
        [ActionName("LineTextColumn")]
        [ValidateInput(false)]
        public ActionResult PostLineTextColumn(LineTextColumnModel model)
        {
            ColumnModelBuilder builder = new ColumnModelBuilder();
            var column = builder.BuildLineTextType(model);
            CustomTableTools.AddColumn(model.TableName, column);
            return RedirectToAction("Detail", new { code = model.TableCode });
        }

        [HttpPost]
        [ActionName("NumberColumn")]
        [ValidateInput(false)]
        public ActionResult PostNumberColumn(NumberColumnModel model)
        {
            ColumnModelBuilder builder = new ColumnModelBuilder();
            var column = builder.BuildNumberType(model);
            if (model.IsNew)
            {
                CustomTableTools.AddColumn(model.TableName, column);
            }
            else
            {
                CustomTableTools.EditColumn(model.TableName, column);
            }
            
            return RedirectToAction("Detail", new { code = model.TableCode });
        }
        [HttpPost]
        [ActionName("RichTextColumn")]
        [ValidateInput(false)]
        public ActionResult PostRichTextColumn(RichTextColumnModel model)
        {
            ColumnModelBuilder builder = new ColumnModelBuilder();
            var column = builder.BuildRichTextType(model);
            if (model.IsNew)
            {
                CustomTableTools.AddColumn(model.TableName, column);
            }
            else
            {
                CustomTableTools.EditColumn(model.TableName, column);
            }
            return RedirectToAction("Detail", new { code = model.TableCode });
        }
        [HttpPost]
        [ActionName("DateColumn")]
        [ValidateInput(false)]
        public ActionResult PostDateColumn(DateColumnModel model)
        {
            ColumnModelBuilder builder = new ColumnModelBuilder();
            var column = builder.BuildDateType(model);
            if (model.IsNew)
            {
                CustomTableTools.AddColumn(model.TableName, column);
            }
            else
            {
                CustomTableTools.EditColumn(model.TableName, column);
            }
            return RedirectToAction("Detail", new { code = model.TableCode });
        }
        [ValidateInput(false)]
        public ActionResult RichTextColumn(RichTextColumnModel model)
        {
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult DateColumn(DateColumnModel model)
        {
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult NumberColumn(NumberColumnModel model)
        {
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult LineTextColumn(LineTextColumnModel model)
        {
            var column = ColumnTypeFactory.CreateLineTextType();
            return View(model);
        }
        #endregion
    }
}
