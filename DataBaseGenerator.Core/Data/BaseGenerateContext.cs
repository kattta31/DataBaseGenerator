using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseGenerator.Core;
using Microsoft.EntityFrameworkCore;

namespace DataBaseGenerator.Core.Data
{
    public class BaseGenerateContext : DbContext
    {
        
        public DbSet<Patient> Patient { get; set; }

        public DbSet<WorkList> WorkList { get; set; }
        
        public BaseGenerateContext()
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=localhost;database=medxregistry;user=root;password=root", new MySqlServerVersion(new Version(10, 4, 17)));
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Patient>().ToTable("Patient");

        //    // Configure Primary Keys  

        //    modelBuilder.Entity<Patient>().HasKey(patient => patient.ID_Patient).HasName("PK_Patient");

        //    // Configure indexes 



        //    // Configure columns 

        //    modelBuilder.Entity<Patient>().Property(patient => patient.LastName).HasColumnType("varchar(64)").IsRequired();
        //}
    }
}
