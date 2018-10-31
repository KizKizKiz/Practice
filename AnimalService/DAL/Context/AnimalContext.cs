using System.Data.Entity;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;
using Practice.Core;

namespace Service.DAL.Context
{
    public partial class AnimalContext : DbContext
    {
        public AnimalContext()
            : base("DbConnection")
        {
            Database.SetInitializer(new DatabaseInitializer());
            Database.Log = (s) => Debug.Write(s);
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AnimalType> Squads { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().
                ToTable("Animals").
                Property(m=>m.ID).
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            modelBuilder.Entity<AnimalType>().
                HasKey(m=>m.Type).
                ToTable("Squads").  
                Ignore(m=>m.ID).
                HasMany(m => m.Animals).
                WithRequired().
                HasForeignKey(k => k.Squad);                 
        }
    }
}
