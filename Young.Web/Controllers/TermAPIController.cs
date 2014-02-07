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
                Description = root.Description
            };
            tmp.children = new List<JsonTmp>();
            foreach (var item in root.Chirdren)
            {
                tmp.children.Add(Convert(item));
            }
            SetNodeIcon(tmp, root.ParentId == null);
            return tmp;
        }

        private void SetNodeIcon(JsonTmp node,bool isRoot)
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/termapi
        public void Post([FromBody]string value)
        {
           
        }

        // PUT api/termapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/termapi/5
        public void Delete(int id)
        {
        }
    }

    public class JsonTmp
    {
        public int id { get; set; }
        public string text { get; set; }
        public string Description { get; set; }
        public string iconCls { get; set; }
        public ICollection<JsonTmp> children { get; set; }

    }
}
