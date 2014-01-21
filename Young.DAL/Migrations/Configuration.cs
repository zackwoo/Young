using Young.Model;
using Young.Model.Base;

namespace Young.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Young.DAL.DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Young.DAL.DataBaseContext context)
        {
            
           
        }
    }
}
