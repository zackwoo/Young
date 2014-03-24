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
      
        public void DeleteColumn(string code)
        {
            CustomTableTools.DeleteColumn(code);
        }
    }
}
