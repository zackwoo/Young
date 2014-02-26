using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Young.DAL;
using Young.Web.Models;
using Young.Web.Models.Command;
using Young.Web.Models.EasyUIView;

namespace Young.Web.Controllers
{
    /// <summary>
    /// 对自定义列表提供数据读取输入API
    /// </summary>
    public class APICustomListDataController : ApiController
    {
        // GET api/apicustomlistdata/5
        //获取自定义列表栏目集合
        public IList<CustomColumnModel> GetCustomColumnByID(int id)
        {
            IList<CustomColumnModel> ccList = new List<CustomColumnModel>();
            using (var db = new DataBaseContext())
            {
                var info = db.CustomList.Single(f => f.ID == id);
                foreach (var columnEntity in info.CustomColumnEntities)
                {
                    ccList.Add(new CustomColumnModel
                        {
                            Condition = columnEntity.Condition,
                            Description = columnEntity.Description,
                            DisplayName = columnEntity.Name,
                            InnerName = columnEntity.InnerName,
                            Type = columnEntity.ColumnType,
                            ID = columnEntity.ID
                        });
                }
            }
            return ccList;
        }
        // GET api/apicustomlistdata?name=xxx
        public IEnumerable<CustomColumnModel> GetCustomColumnByName(string name)
        {
            var id= GetIDByName(name);
            if (id == 0) return null;
            return GetCustomColumnByID(id);
        }

        /// <summary>
        /// 供Easy ui Table使用
        /// </summary>
        /// <param name="clistId">自定义列表ID</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataGridModel<CustomDataGridRowModel> GetTableData(int clistId, int page = 0, int rows = 0)
        {
            var result = new DataGridModel<CustomDataGridRowModel>();
            using (var db = new DataBaseContext())
            {
                var clist = db.CustomList.Single(f => f.ID == clistId);
                result.total = clist.DataEntities.Count();
                var foo =
                    clist.DataEntities.OrderBy(f => f.ID)
                         .Skip((page - 1)*rows)
                         .Take(rows)
                         .Select(f => new CustomDataGridRowModel
                             {
                                 ID = f.ID,
                                 JsonData = f.Data
                             });
                result.rows.AddRange(foo.ToArray());
            }
            return result;
        }


        //根据自定义列表名称获取自定义列表ID,如果没有该自定义列表返回0
        private int GetIDByName(string name)
        {
            using (var db = new DataBaseContext())
            {
                var info = db.CustomList.SingleOrDefault(f => f.Name == name);
                if (info!=null)
                {
                    return info.ID;
                }
            }
            return 0;
        }

        

        // POST api/apicustomlistdata
        public void Post(CustomDataCommandModel data)
        {
            switch (data.CommandType)
            {
                case CommandType.Create:
                    AddRecord(data.CustomListName, data.JsonData);
                    break;
                case CommandType.Edit:
                    UpdateRecord(data.CustomListName,data.ID, data.JsonData);
                    break;
                case CommandType.Delete:
                    DeleteRecord(data.CustomListName, data.ID);
                    break;
            }
        }

        private void DeleteRecord(string customListName, int id)
        {
            using (var db = new DataBaseContext())
            {
                var clist = db.CustomList.Single(f => f.Name == customListName);
                var dataEntity = clist.DataEntities.Single(f => f.ID == id);
                clist.DataEntities.Remove(dataEntity);
                db.SaveChanges();
            }
        }

        private void UpdateRecord(string customListName, int id, string data)
        {
            using (var db = new DataBaseContext())
            {
                var clist = db.CustomList.Single(f => f.Name == customListName);
                var cdata = clist.DataEntities.Single(f => f.ID == id);
                cdata.Data = data;
                db.SaveChanges();
            }
        }

        private void AddRecord(string customListName,string data)
        {
            using (var db = new DataBaseContext())
            {
                var clist = db.CustomList.Single(f => f.Name == customListName);
                clist.DataEntities.Add(new Model.CustomListDataEntity
                    {
                        Data = data
                    });

                db.SaveChanges();
            }
        }

    }
}
