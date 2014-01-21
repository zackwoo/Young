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
    public  class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("YoungDB")
        {
         
        }
    
        
        public DbSet<TermEntity> Terms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<BasePropertyEntity>().HasKey(x => x.ID);
            //modelBuilder.Entity<BasePropertyEntity>().Property(x => x.ID)
            //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 

            //modelBuilder.Entity<BooleanPropertyEntity>().ToTable("BooleanPropertyEntity");
            //modelBuilder.Entity<DatePropertyEntity>().ToTable("DatePropertyEntity");
            //modelBuilder.Entity<DoublePropertyEntity>().ToTable("DoublePropertyEntity");
            //modelBuilder.Entity<TermPropertyEntity>().ToTable("TermPropertyEntity");
            //modelBuilder.Entity<StringPropertyEntity>().ToTable("StringPropertyEntity");
            //modelBuilder.Entity<IntPropertyEntity>().ToTable("IntPropertyEntity"); 
        }

    }


}
