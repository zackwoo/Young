using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Young.DAL;
using Young.Model;

namespace Young.Provider
{
    public class YoungRoleProvider : RoleProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
            if (config["applicationName"] != null)
            {
                ApplicationName = config["applicationName"];
            }
        }
        
        public override string ApplicationName
        {
            get;
            set;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                CheckArgumentName(roleName);
            }
            foreach (var username in usernames)
            {
                CheckArgumentName(username);
            }
            using (var db = new DataBaseContext())
            {
                var roles = db.Roles.Where(f => roleNames.Contains(f.Name));
                var users = db.Users.Where(f => usernames.Contains(f.UserName));
                foreach (var userEntity in users)
                {
                    if (!userEntity.Roles.Any())
                    {
                        userEntity.Roles.AddRange(roles);
                    }
                    else
                    {
                        foreach (var roleEntity in roles)
                        {
                            if (!userEntity.Roles.Exists(f=>f.Name == roleEntity.Name))
                            {
                                userEntity.Roles.Add(roleEntity);
                            }
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public override void CreateRole(string roleName)
        {
            CheckArgumentName(roleName);
            if (RoleExists(roleName))
            {
                throw new ProviderException("已经存在相同的角色名称");
            }
            using (var db = new DataBaseContext())
            {

                db.Roles.Add(new RoleEntity
                    {
                        IsSystem = false,
                        Name = roleName
                    });
                db.SaveChanges();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            CheckArgumentName(roleName);
            using (var db = new DataBaseContext())
            {
                var role = db.Roles.SingleOrDefault(f => f.Name == roleName);
                if (role == null) return true;
                if (throwOnPopulatedRole && role.Users.Any())
                {
                    throw new ProviderException("roleName 具有一个或多个成员");
                }
                db.Roles.Remove(role);
                db.SaveChanges();
                return true;
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            CheckArgumentName(roleName);
            using (var db = new DataBaseContext())
            {
                var role = db.Roles.Single(f => f.Name == roleName);
                return role.Users.FindAll(f => f.UserName.Contains(usernameToMatch)).Select(f => f.UserName).ToArray();
            }
        }

        public override string[] GetAllRoles()
        {
            using (var db = new DataBaseContext())
            {
                return db.Roles.Select(f => f.Name).ToArray();
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            CheckArgumentName(username);
            using (var db = new DataBaseContext())
            {
                var user = db.Users.Single(f => f.UserName == username);
                return user.Roles.Select(f => f.Name).ToArray();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            CheckArgumentName(roleName);
            using (var db = new DataBaseContext())
            {
                var role= db.Roles.Single(f => f.Name == roleName);
                return role.Users.Select(f => f.UserName).ToArray();
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            CheckArgumentName(roleName);
            CheckArgumentName(username);
            using (var db = new DataBaseContext())
            {
                var role = db.Roles.Single(f => f.Name == roleName);
                return role.Users.Any(f => f.UserName == username);
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                CheckArgumentName(roleName);
            }
            foreach (var username in usernames)
            {
                CheckArgumentName(username);
            }
            using (var db  = new DataBaseContext())
            {
                var roles = db.Roles.Where(f => roleNames.Contains(f.Name));
                foreach (var roleEntity in roles)
                {
                    roleEntity.Users.RemoveAll(f => usernames.Contains(f.UserName));
                }
                db.SaveChanges();
            }
        }

        public override bool RoleExists(string roleName)
        {
            CheckArgumentName(roleName);
            using (var db = new DataBaseContext())
            {
                return db.Roles.Any(f => f.Name == roleName);
            }
        }

        internal void CheckArgumentName(string roleName)
        {
            if (roleName == null)
            {
                throw new ArgumentNullException("roleName");
            }
            if (roleName == string.Empty || roleName.Contains(','))
            {
                throw new ArgumentException("角色名为空或者包含逗号");
            }
        }
    }
}
