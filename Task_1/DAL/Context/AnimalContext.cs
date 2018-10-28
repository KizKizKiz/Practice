namespace Task_1.DAL.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Task_1.Core;
    using ConsoleApp.Model;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics;

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
                WithRequired(r => r.Squad).
                HasForeignKey(c=>c);
        }
    }
}
