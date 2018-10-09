using System;
using System.Data.Entity;
using System.Diagnostics;
using DataBinding.Core;
using System.ComponentModel.DataAnnotations.Schema;
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
                ToTable("Animals").
                Property(m=>m.ID).
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);               
            modelBuilder.Entity<AnimalType>().
                ToTable("Squads").
                HasMany(m => m.Animals).
                WithRequired().
                HasForeignKey(k => k.SquadId);
            modelBuilder.Entity<AnimalType>().
                ToTable("Squads").
                HasKey(m => m.Type).
                Property(m=>m.Type).
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);          
        }
    }
}
