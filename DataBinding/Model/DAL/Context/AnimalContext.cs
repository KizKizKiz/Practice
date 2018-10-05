using System;
using System.Data.Entity;
using System.Diagnostics;
using DataBinding.Core;
namespace DataBinding.Model.DAL.Context
{    
    public partial class AnimalContext : DbContext
    {
        public AnimalContext()
            : base("DbConnection")
        {            
            Database.SetInitializer(new DatabaseInitializer());
            Database.Log = s => Debug.Write(s);            
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AnimalType> Squads { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().
                HasKey(key => key.ID).
                ToTable("Animals");
            modelBuilder.Entity<AnimalType>().
                ToTable("Squads").
                HasMany(m => m.Animals).
                WithRequired(m=>m.AnimalType).
                HasForeignKey(k=>k.SquadId);
        }
    }
}
