using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataBaseGenerator.Core.Data;



namespace DataBaseGenerator.Core.Data
{
    public static class DataBaseCommand
    {
        public static List<Patient> GetAllPatients()
        {
            using (BaseGenerateContext dataBase = new BaseGenerateContext())
            {
                var result = dataBase.Patients.ToList();
                return result;
            }
        }


        public static string CreatePatient(int iD, string lastName, string name, string middleName, 
            string patientId, DateTime birthDate, string sex, string address, string addInfo, string occupation)
        {
            string result = "Patient created";

            using (BaseGenerateContext dataBase = new BaseGenerateContext())
            {
                bool checkIsExist = dataBase.Patients.Any(
                    element => element.IdPatient == iD && element.LastName == lastName && element.FirstName == name && element.MiddleName == middleName
                    && element.PatientId == patientId && element.BirthDate == birthDate && element.Sex == sex && element.Address == address && element.AddInfo == addInfo);
                
                if (!checkIsExist)
                {
                    Patient newPatient = new Patient
                    {
                        IdPatient = iD, LastName = lastName, FirstName = name, MiddleName = middleName, PatientId = patientId, BirthDate = birthDate,
                        Sex = sex, Address = address, AddInfo = addInfo, Occupation = occupation
                    };

                    dataBase.Patients.Add(newPatient);
                    dataBase.SaveChanges();

                    result = "Done";
                   
                }

                return result;
            }
        }


        public static string DeletePatient(Patient patient)
        {
            string result = "Patient is not create";

            using (BaseGenerateContext dataBase = new BaseGenerateContext())
            {
                dataBase.Patients.Remove(patient);
                dataBase.SaveChanges();

                result = $"Сделано! Пацоент {patient.LastName} удален из базы";
                
            }

            return result;
        }


        public static string EditePatient(Patient oldPatient, int iD, string lastName, string name)
        {
            string result = "Patient is not create";

            using (BaseGenerateContext dataBase = new BaseGenerateContext())
            {
                Patient patient = dataBase.Patients.FirstOrDefault(position => position.IdPatient == oldPatient.IdPatient);
                if (patient != null)
                {
                    patient.IdPatient = iD;
                    patient.LastName = lastName;
                    patient.FirstName = name;
                    dataBase.SaveChanges();

                    result = $"Done!!! Patient data {patient.LastName} changed";
                }
            }

            return result;
        }
    }
}
