﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Map entities to tables


            modelBuilder.Entity<Patient>().ToTable("Patient");

            modelBuilder.Entity<WorkList>().ToTable("WorkList");


            #endregion

            #region Configure Primary Keys


            modelBuilder.Entity<Patient>().HasKey(patient => patient.PatientID).HasName("PK_Patient");

            modelBuilder.Entity<Patient>().HasCharSet("Utf8");

            modelBuilder.Entity<WorkList>().HasKey(worklist => worklist.ID_WorkList).HasName("PK_WorkList");

            modelBuilder.Entity<WorkList>().HasCharSet("Utf8");


            #endregion

            #region Configure indexes


            modelBuilder.Entity<Patient>().HasIndex(patient => patient.PatientID).HasDatabaseName("Idx_Primary");

            modelBuilder.Entity<Patient>().HasIndex(patient => patient.PatientID).HasDatabaseName("PatientID_Unique");





            #endregion

            #region Configure columns table Patient


            modelBuilder.Entity<Patient>().Property(patient => patient.ID_Patient).HasColumnType("INT UNSIGNED NOT NULL").IsRequired();

            modelBuilder.Entity<Patient>().Property(patient => patient.LastName).HasColumnType("varchar(64)").IsRequired();

            modelBuilder.Entity<Patient>().Property(patient => patient.FirstName).HasColumnType("varchar(64)").IsRequired();

            modelBuilder.Entity<Patient>().Property(patient => patient.MiddleName).HasColumnType("varchar(64)").IsRequired();

            modelBuilder.Entity<Patient>().Property(patient => patient.PatientID).HasColumnType("varchar(64)").IsRequired();

            modelBuilder.Entity<Patient>().Property(patient => patient.BirthDate).HasColumnType("date").IsRequired();

            modelBuilder.Entity<Patient>().Property(patient => patient.Sex).HasColumnType("varchar(1)").IsRequired();

            modelBuilder.Entity<Patient>().Property(patient => patient.Address).HasColumnType("varchar(64)").IsRequired();

            modelBuilder.Entity<Patient>().Property(patient => patient.AddInfo).HasColumnType("text").IsRequired();

            modelBuilder.Entity<Patient>().Property(patient => patient.Occupation).HasColumnType("varchar(64)").IsRequired();


            #endregion

            #region Configure columns table WorkList


            modelBuilder.Entity<WorkList>().Ignore(patient => patient.WorkListID);

            modelBuilder.Entity<WorkList>().Property(patient => patient.ID_WorkList).HasColumnType("INT UNSIGNED NOT NULL").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.ID_Patient).HasColumnType("int").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.CreateDate).HasColumnType("date").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.CreateTime).HasColumnType("DATETIME").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.CompleteDate).HasColumnType("date").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.CompleteTime).HasColumnType("DATETIME").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.State).HasColumnType("varchar(16)").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.SOPInstanceUID).HasColumnType("varchar(64)").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.Modality).HasColumnType("varchar(16)").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.StationAeTitle).HasColumnType("varchar(16)").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.ProcedureStepStartDateTime).HasColumnType("DATETIME").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.PerformingPhysiciansName).HasColumnType("varchar(324)").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.StudyDescription).HasColumnType("varchar(64)").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.ReferringPhysiciansName).HasColumnType("varchar(324)").IsRequired();

            modelBuilder.Entity<WorkList>().Property(patient => patient.RequestingPhysician).HasColumnType("varchar(324)").IsRequired();


            #endregion


        }
    }
}
