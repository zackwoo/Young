using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Young.DAL;
using Young.Web.Models;

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
        public void Post(object data)
        {
            var i = data;
        }

        // PUT api/apicustomlistdata/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/apicustomlistdata/5
        public void Delete(int id)
        {
        }
    }
}
