using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Young.CustomTable;
using Young.DAL;
using Young.Model;
using Young.Web.Models;
using Young.Web.Models.Command;

namespace Young.Web.Controllers
{
    public class APICustomListController : ApiController
    {
      
        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="code"></param>
        public void DeleteColumn(string code)
        {
            CustomTableTools.DeleteColumn(code);
        }

        /// <summary>
        /// 获取自定义列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public IEnumerable<CustomListItemModel> Get(int page = 1, int rows = int.MaxValue)
        {
            var list = CustomTableTools.GetTableByPaging(page - 1, rows);

            var foo = from bar in list
                      select new CustomListItemModel
                      {
                          Name = bar.Name,
                          Description = bar.Description,
                          Code = bar.Code,
                          CreateDate = bar.CreateTime
                      };
            return foo.ToList();
        }
        /// <summary>
        /// 重置数据库
        /// </summary>
        public void PostResetDatabase(string tcode)
        {
            CustomTableTools.ResetDatabase(tcode);
        }
    }
}
