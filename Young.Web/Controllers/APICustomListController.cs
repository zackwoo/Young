using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Young.DAL;
using Young.Model;
using Young.Web.Models;
using Young.Web.Models.Command;

namespace Young.Web.Controllers
{
    public class APICustomListController : ApiController
    {
        // GET api/apicustomlist
        public IEnumerable<CustomListItemModel> Get(int page =0,int rows=0)
        {
            using (var db = new DataBaseContext())
            {
                IEnumerable<CustomListItemModel> foo;
                if (page == 0 && rows == 0)
                {
                    foo = from bar in db.CustomList.ToList()
                          select new CustomListItemModel
                              {
                                  Name = bar.Name,
                                  Description = bar.Description,
                                  ID = bar.ID,
                                  CreatData = bar.CreatData
                              };
                }
                else
                {
                    foo = from bar in db.CustomList.OrderBy(f=>f.ID).Skip((page-1)*rows).Take(rows)
                          select new CustomListItemModel
                              {
                                  Name = bar.Name,
                                  Description = bar.Description,
                                  ID = bar.ID,
                                  CreatData = bar.CreatData
                              };
                }
                return foo.ToList();
            }
        }

        // GET api/apicustomlist/5
        public string Get(int id)
        {
            return "value";
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

        // PUT api/apicustomlist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/apicustomlist/5
        public void Delete(int id)
        {
        }
    }
}
