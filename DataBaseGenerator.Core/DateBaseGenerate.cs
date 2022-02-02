using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;


namespace DataBaseGenerator.Core
{
    public class DateBaseGenerate : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        //Проверка на то что если базаданных не существует, то она создасться
        //
        public DateBaseGenerate()
        {
            Database.EnsureCreated();
        }





        //public DateBaseGenerate(DbContextOptions<DateBaseGenerate> options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=localhost;database=worklistgenerator;user=root;password=root", new MySqlServerVersion(new Version(10, 4, 17)));
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Map entities to tables 
        //    modelBuilder.Entity<Patient>().ToTable("Patients");

        //    // Configure Primary Keys  
        //    modelBuilder.Entity<Patient>().HasKey(p => p.Id).HasName("PK_Users");


        //    // Configure indexes 
        //    modelBuilder.Entity<Patient>().HasIndex(p => p.LastName).HasDatabaseName("Idx_LastName");
        //    modelBuilder.Entity<Patient>().HasIndex(p => p.Name).HasDatabaseName("Idx_Name");

        //    // Configure columns 
        //    modelBuilder.Entity<Patient>().Property(p => p.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
        //    modelBuilder.Entity<Patient>().Property(p => p.LastName).HasColumnType("nvarchar(50)").IsRequired();
        //    modelBuilder.Entity<Patient>().Property(p => p.Name).HasColumnType("nvarchar(50)").IsRequired();

        //    // Configure relationships  

        //}
    }
}
