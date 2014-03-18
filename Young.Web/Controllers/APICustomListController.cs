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
        // GET api/apicustomlist
        public IEnumerable<CustomListItemModel> Get(int page = 1, int rows = 10)
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

        // POST api/apicustomlist
        public void Post(CustomListCommandModel command)
        {
            if (command.CommandType == CommandType.Create)
            {
                using (var db = new DataBaseContext())
                {
                    var model = new CustomListEntity
                        {
                            Name = command.DisplayName,
                            Description = command.Description,
                            CreatData = DateTime.Now,
                            CustomColumnEntities = new List<CustomColumnEntity>()
                        };
                    foreach (var customColumnModel in command.CustomColumnModels)
                    {
                        model.CustomColumnEntities.Add(new CustomColumnEntity
                            {
                                ColumnType = customColumnModel.Type,
                                Condition = customColumnModel.Condition,
                                Description = customColumnModel.Description,
                                Name = customColumnModel.DisplayName,
                                InnerName = Guid.NewGuid().ToString("N")
                            });
                    }
                    db.CustomList.Add(model);
                    db.SaveChanges();
                }
            }
        }
    }
}
