using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Young.Model;
using Young.Model.Base;


namespace Young.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("YoungDB")
        {

        }


        public DbSet<TermEntity> Terms { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>()
                        .HasMany(e => e.Roles)
                        .WithMany(e => e.Users)
                        .Map(m =>
                            {
                                m.ToTable("Users_Roles");
                                m.MapLeftKey("UserID");
                                m.MapRightKey("RoleID");
                            });
        }

    }


}
