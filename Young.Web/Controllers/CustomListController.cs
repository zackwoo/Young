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
        //基本信息赋值
        private static void SetColumnBaseInfo(ColumnModel model, ColumnTypeBase column)
        {
            column.Code = model.Code;
            column.CustomValidationErrorMessage = model.CustomValidationErrorMessage;
            column.CustomValidationRegularExpression = model.CustomValidationRegularExpression;
            column.DatabaseType = model.DatabaseType;
            column.Description = model.Description;
            column.IsNeedCustomValidation = model.IsNeedCustomValidation;
            column.IsRequired = model.IsRequired;
            column.Name = model.Name;
        }
        [HttpPost]
        [ActionName("LineTextColumn")]
        [ValidateInput(false)]
        public ActionResult PostLineTextColumn(LineTextColumnModel model)
        {
            var column = ColumnTypeFactory.CreateLineTextType();
            SetColumnBaseInfo(model, column);
            column.DatabaseColumnLength = model.Length;
            CustomTableTools.AddColumn(model.TableName, column);
            return RedirectToAction("column", new { code = model.TableCode });
        }

        [HttpPost]
        [ActionName("NumberColumn")]
        [ValidateInput(false)]
        public ActionResult PostNumberColumn(NumberColumnModel model)
        {
            var column = ColumnTypeFactory.CreateNumberType();
            SetColumnBaseInfo(model, column);
            CustomTableTools.AddColumn(model.TableName, column);
            return RedirectToAction("column", new { code = model.TableCode });
        }
        [HttpPost]
        [ActionName("RichTextColumn")]
        [ValidateInput(false)]
        public ActionResult PostRichTextColumn(RichTextColumnModel model)
        {
            var column = ColumnTypeFactory.CreateRichTextType();
            SetColumnBaseInfo(model, column);
            CustomTableTools.AddColumn(model.TableName, column);
            return RedirectToAction("column", new { code = model.TableCode });
        }
        [HttpPost]
        [ActionName("DateColumn")]
        [ValidateInput(false)]
        public ActionResult PostDateColumn(DateColumnModel model)
        {
            var column = ColumnTypeFactory.CreateDateType();
            SetColumnBaseInfo(model, column);
            CustomTableTools.AddColumn(model.TableName, column);
            return RedirectToAction("column", new { code = model.TableCode });
        }
        public ActionResult RichTextColumn(RichTextColumnModel model)
        {
            return View(model);
        }

        public ActionResult DateColumn(DateColumnModel model)
        {
            return View(model);
        }
        public ActionResult NumberColumn(NumberColumnModel model)
        {
            return View(model);
        }
        public ActionResult LineTextColumn(LineTextColumnModel model)
        {
            var column = ColumnTypeFactory.CreateLineTextType();
            return View(model);
        }
        #endregion
    }
}
