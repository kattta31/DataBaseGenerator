using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseGenerator.Core;
using Microsoft.EntityFrameworkCore;

namespace DataBaseGenerator.UI.Wpf.Data
{
    public class BaseGenerate : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public BaseGenerate()
        {
            Database.EnsureCreated();
        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=localhost;database=worklistgenerator;user=root;password=root", new MySqlServerVersion(new Version(10, 4, 17)));
        }

    }
}
