using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.DAL;
using Young.Model;

namespace Young.BLL
{
    public class RoleBusiness
    {
        public static ICollection<RoleEntity> QueryAllRoles()
        {
            using (var db = new DataBaseContext())
            {
                return db.Roles.ToList();
            }
        }
    }
}
