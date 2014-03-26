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
        private static TableDetailModel GetTableDetailModel(string tcode)
        {
            ColumnModelBuilder builder = new ColumnModelBuilder();
            var table = CustomTableTools.GetTableByCode(tcode, true);
            var model = new TableDetailModel
            {
                TableCode = tcode,
                TableName = table.Name,
                Columns = table.Columns.Select(builder.BuildColumnModel)
            };
            model.SearchColumns = model.Columns.Where(f => f.IsForSearch);
            model.ListColumns = model.Columns.Where(f => f.IsForList);
            return model;
        }

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
            var model = GetTableDetailModel(code);

            return View(model);
        }

        public ActionResult List(string tcode)
        {
            var model = GetTableDetailModel(tcode);

            return View(model);
        }

        public ActionResult AddSearchColumn(string tablecode, string colcode)
        {
            CustomTableTools.SetSearchColumn(colcode);
            return RedirectToAction("Detail", new { code = tablecode });
        }
        public ActionResult RemoveSearchColumn(string tablecode, string colcode)
        {
            CustomTableTools.RemoveSearchColumn(colcode);
            return RedirectToAction("Detail", new { code = tablecode });
        }
        public ActionResult AddListColumn(string tablecode, string colcode)
        {
            CustomTableTools.SetListColumn(colcode);
            return RedirectToAction("Detail", new { code = tablecode });
        }
        public ActionResult RemoveListColumn(string tablecode, string colcode)
        {
            CustomTableTools.RemoveListColumn(colcode);
            return RedirectToAction("Detail", new { code = tablecode });
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
                throw new NullReferenceException();
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
                    var lineTextModel = (LineTextColumnModel)model;
                    lineTextModel.Length = ((LineTextType)column).DatabaseColumnLength;
                    return RedirectToAction("LineTextColumn", lineTextModel);
            }
        }
       
        [HttpPost]
        [ActionName("LineTextColumn")]
        [ValidateInput(false)]
        public ActionResult PostLineTextColumn(LineTextColumnModel model)
        {
            ColumnModelBuilder builder = new ColumnModelBuilder();
            var column = builder.BuildLineTextType(model);
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
            return View(model);
        }
        #endregion
    }
}
