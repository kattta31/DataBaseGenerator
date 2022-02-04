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


        public BaseGenerateContext()
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=localhost;database=medxregistry;user=root;password=root", new MySqlServerVersion(new Version(10, 4, 17)));
        }

    }
}
