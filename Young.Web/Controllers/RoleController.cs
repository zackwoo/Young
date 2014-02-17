using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Young.BLL;
using Young.Web.Models;

namespace Young.Web.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/

        public ActionResult Index()
        {
            var roles = RoleBusiness.QueryAllRoles();
            RoleCollectionModel model = new RoleCollectionModel();
            foreach (var roleEntity in roles)
            {
                model.Add(new RoleModel
                    {
                        IsSystem = roleEntity.IsSystem,
                        RoleName = roleEntity.Name
                    });
            }
            return View(model);
        }

    }
}
