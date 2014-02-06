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
        public string Get()
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                var root = db.Terms.FirstOrDefault(f => f.ParentId == null);

                var json = JsonConvert.SerializeObject(Convert(root), Formatting.None);
                return json;
            }
        }

        private JsonTmp Convert(TermEntity root)
        {
            JsonTmp tmp = new JsonTmp
            {
                ID = root.ID,
                Name = root.Name,
                Description = root.Description
            };
            tmp.Children = new List<JsonTmp>();
            foreach (var item in root.Chirdren)
            {
                tmp.Children.Add(Convert(item));
            }
            return tmp;
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
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<JsonTmp> Children { get; set; }

    }
}
