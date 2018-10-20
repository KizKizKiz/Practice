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
            Database.Log = (s) => Debug.Write(s);
        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<AnimalType> Squads { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().
                ToTable("Animals").
                Map<Spider>(m => {
                    m.Requires("Discr").HasValue("Sp");
                }).
                Map<Butterfly>(m => {
                    m.Requires("Discr").HasValue("But");
                }).
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
