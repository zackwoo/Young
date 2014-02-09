using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Young.DAL;
using Newtonsoft.Json;
using Young.Model;


namespace Young.Web.Controllers
{
    public class TermAPIController : ApiController
    {
        // GET api/termapi
        public ICollection<JsonTmp> Get()
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                var root = db.Terms.FirstOrDefault(f => f.ParentId == null);

                //var json = JsonConvert.SerializeObject(Convert(root), Formatting.None);
                //return json;
                List<JsonTmp> tmp = new List<JsonTmp>();
                tmp.Add(Convert(root));
                return tmp;
            }
        }

        private JsonTmp Convert(TermEntity root)
        {
            JsonTmp tmp = new JsonTmp
            {
                id = root.ID,
                text = root.Name,
                Description = root.Description,
                attributes = new { isSystem = root.IsSystem }
            };
            tmp.children = new List<JsonTmp>();
            foreach (var item in root.Chirdren)
            {
                tmp.children.Add(Convert(item));
            }
            SetNodeIcon(tmp, root.ParentId == null);
            return tmp;
        }

        private void SetNodeIcon(JsonTmp node, bool isRoot)
        {
            if (isRoot)
            {
                node.iconCls = "icon-book-open";
            }
            else if (node.children.Count > 0)
            {
                node.iconCls = "icon-application-cascade";
            }
            else
            {
                node.iconCls = "icon-tag-blue";
            }

        }

        // GET api/termapi/5
        public JsonTmp Get(int id)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                var single = db.Terms.SingleOrDefault(f => f.ID == id);

                if (single == null) return null;
                return new JsonTmp
                    {
                        id = single.ID,
                        text = single.Name,
                        Description = single.Description
                    };
            }
        }

        // POST api/termapi
        public JsonTmp Post(JsonTmp item)
        {
            var parentID = item.id;// 父类ID
            TermEntity entity = new TermEntity
            {
                IsSystem = false,
                Name = item.text,
                ParentId = parentID,
                Description = item.Description
            };
            using (DataBaseContext db = new DataBaseContext())
            {
                db.Terms.Add(entity);
                db.SaveChanges();
            }
            item.id = entity.ID;
            return item;
        }

        // PUT api/termapi/5
        public void Put(JsonTmp item)
        {
            //update item
            using (DataBaseContext db = new DataBaseContext())
            {
                var single = db.Terms.Single(f => f.ID == item.id);
                single.Name = item.text;
                single.Description = item.Description;
                db.SaveChanges();
            }
        }

        // DELETE api/termapi/5
        public bool Delete(int id)
        {
            try
            {
                using (DataBaseContext db = new DataBaseContext())
                {
                    var single = db.Terms.SingleOrDefault(f => f.ID == id);
                    db.Terms.Remove(single);
                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }

    public class JsonTmp
    {
        public int id { get; set; }
        public string text { get; set; }
        public string Description { get; set; }
        public string iconCls { get; set; }
        public ICollection<JsonTmp> children { get; set; }
        public object attributes { get; set; }
    }
}
