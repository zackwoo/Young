using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Young.CustomTable;
using Young.CustomTable.ViewModel;
using Young.Web.Models.Column;
using Young.Web.Models.CustomList;
using Young.Web.Models.DynamicTable;

namespace Young.Web.Controllers
{
    /// <summary>
    /// 动态列表数据处理控制器
    /// </summary>
    public class DynamicTableController : Controller
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

        public ActionResult Index(string tcode)
        {
            var model = GetTableDetailModel(tcode);

            return View(model);
        }

        public ActionResult AddData(string tcode)
        {
            var table = CustomTableTools.GetTableByCode(tcode, true);
            var model = new DataEditorModel
            {
                TableCode = tcode,
                TableName = table.Name,
                ColumnTypes = table.Columns
            };
            return View(model);
        }
        [HttpPost]        
        public ActionResult AddData(YoungTableDataModel model,string tableCode)
        {
            CustomTableTools.SaveData(model, tableCode);
            return RedirectToAction("index", new { tcode = tableCode });
        }
        public ActionResult EditData(string tcode,int id)
        {
            var table = CustomTableTools.GetTableByCode(tcode, true);
            var model = new DataEditorModel
            {
                TableCode = tcode,
                TableName = table.Name,
                ColumnTypes = table.Columns 
            };
            //TODO set values
            return View(model);
        }
    }
}
